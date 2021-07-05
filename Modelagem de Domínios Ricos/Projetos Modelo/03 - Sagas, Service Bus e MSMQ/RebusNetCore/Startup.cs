using Core.Messages;
using Core.Messages.IntegrationEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pagamento;
using Pagamento.Commands;
using Pedido;
using Pedido.Commands;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;

namespace RebusNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure and register Rebus

            var nomeFila = "fila_rebus";

            services.AddRebus(configure => configure
                //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), nomeFila))
                .Transport(t => t.UseRabbitMq("amqp://localhost", nomeFila))
                //.Subscriptions(s => s.StoreInMemory())
                .Routing(r =>
                {
                    r.TypeBased()
                        .MapAssemblyOf<Message>(nomeFila)
                        .MapAssemblyOf<RealizarPedidoCommand>(nomeFila)
                        .MapAssemblyOf<RealizarPagamentoCommand>(nomeFila);
                })
                .Sagas(s => s.StoreInMemory())
                .Options(o =>
                {
                    o.SetNumberOfWorkers(1);
                    o.SetMaxParallelism(1);
                    o.SetBusName("Demo Rebus");
                })
            );

            // Register handlers 
            services.AutoRegisterHandlersFromAssemblyOf<PagamentoCommandHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<PedidoSaga>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRebus(c =>
            {
                c.Subscribe<PedidoRealizadoEvent>().Wait();
                c.Subscribe<PagamentoRealizadoEvent>().Wait();
                c.Subscribe<PedidoFinalizadoEvent>().Wait();
                c.Subscribe<PagamentoRecusadoEvent>().Wait();
                c.Subscribe<PedidoCanceladoEvent>().Wait();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
