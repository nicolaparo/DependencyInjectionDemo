using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{
    public static class ServiceCollectionExtensions
    {
        public static void AddControllers(this IServiceCollection services)
        {
            foreach (var type in typeof(WebApiApplication).Assembly.GetTypes())
            {
                if (typeof(IHttpController).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    services.AddScoped(type);
                else if (typeof(ControllerBase).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    services.AddScoped(type);
            }

        }
    }
}
