using DemoDI.Cases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoDI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            #region Lifecycle

            services.AddTransient<IOperacaoTransient, Operacao>();
            services.AddScoped<IOperacaoScoped, Operacao>();
            services.AddSingleton<IOperacaoSingleton, Operacao>();
            services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));
            services.AddTransient<OperacaoService>();

            #endregion Lifecycle

            #region VidaReal

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteServices, ClienteServices>();

            #endregion VidaReal

            #region Generics

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion Generics

            #region MultiplasClasses

            services.AddTransient<ServiceA>();
            services.AddTransient<ServiceB>();
            services.AddTransient<ServiceC>();
            services.AddTransient<Func<string, IService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case @"A":
                        {
                            return serviceProvider.GetService<ServiceA>();
                        }
                    case @"B":
                        {
                            return serviceProvider.GetService<ServiceB>();
                        }
                    case @"C":
                        {
                            return serviceProvider.GetService<ServiceC>();
                        }
                    default:
                        {
                            return null;
                        }
                }
            });

            #endregion MultiplasClasses

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: @"default", @"{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}