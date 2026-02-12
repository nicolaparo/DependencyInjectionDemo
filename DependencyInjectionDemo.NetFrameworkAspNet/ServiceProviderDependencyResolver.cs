using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{
    public class ServiceProviderDependencyResolver : IDependencyResolver
        , System.Web.Mvc.IDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceScope scope;

        private const string ScopeKey = "ServiceProviderScope";

        public ServiceProviderDependencyResolver(IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
        }

        private ServiceProviderDependencyResolver(IServiceScope scope)
        {
            this.scope = scope;
            serviceProvider = scope.ServiceProvider;
        }

        private IServiceScope GetOrCreateScope()
        {
            var context = HttpContext.Current;
            if (context is null)
                return serviceProvider.CreateScope();

            if (context.Items[ScopeKey] is IServiceScope scope)
                return scope;

            scope = serviceProvider.CreateScope();
            context.Items[ScopeKey] = scope;
            context.DisposeOnPipelineCompleted(scope);

            return scope;
        }

        public object GetService(Type serviceType)
        {
            var scope = GetOrCreateScope();
            return scope.ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var scope = GetOrCreateScope();
            var service = scope.ServiceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(serviceType));
            return service as IEnumerable<object> ?? Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new ServiceProviderDependencyResolver(serviceProvider.CreateScope());
        }

        public void Dispose()
        {
            scope?.Dispose();
        }
    }
}
