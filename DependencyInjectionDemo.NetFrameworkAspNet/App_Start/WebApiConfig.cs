using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IDependencyResolver dependencyResolver)
        {
            // Web API configuration and services
            config.DependencyResolver = dependencyResolver;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
