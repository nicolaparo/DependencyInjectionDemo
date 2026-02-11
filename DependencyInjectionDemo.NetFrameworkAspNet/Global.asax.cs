using DependencyInjectionDemo.NetFrameworkAspNet.Controllers;
using DependencyInjectionDemo.NetFrameworkAspNet.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var services = new ServiceCollection();

            services.AddControllers();
            services.AddScoped<IValuesService, ValuesService>();

            var serviceProvider = services.BuildServiceProvider();

            var dependencyResolver = new ServiceProviderDependencyResolverAdapter(serviceProvider);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(config => WebApiConfig.Register(config, dependencyResolver));

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
            DependencyResolver.SetResolver((System.Web.Mvc.IDependencyResolver) dependencyResolver);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
