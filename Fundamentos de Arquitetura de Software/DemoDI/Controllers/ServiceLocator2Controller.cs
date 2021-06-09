using DemoDI.Cases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoDI.Controllers
{
    public class ServiceLocator2Controller : Controller
    {
        public static void Index([FromServices] IServiceProvider serviceProvider)
        {
            // Retorna null se não estiver registrado
            serviceProvider.GetRequiredService<IClienteServices>().AdicionarCliente(new Cliente());
        }
    }
}