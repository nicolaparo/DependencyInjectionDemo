using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{
    public class WebApiDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceScope scope;

        public WebApiDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private WebApiDependencyResolver(IServiceScope scope)
        {
            this.scope = scope;
            serviceProvider = scope.ServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var service = serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(serviceType));
            return service as IEnumerable<object> ?? Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new WebApiDependencyResolver(serviceProvider.CreateScope());
        }

        public void Dispose()
        {
            scope?.Dispose();
        }
    }
}
