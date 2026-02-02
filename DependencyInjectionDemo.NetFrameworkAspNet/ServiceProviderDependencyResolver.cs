using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DependencyInjectionDemo.NetFrameworkAspNet
{
    public class ServiceProviderDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private const string ScopeKey = "ServiceProviderScope";

        public ServiceProviderDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IServiceScope GetOrCreateScope()
        {
            var context = HttpContext.Current;
            if (context is null)
                return _serviceProvider.CreateScope();

            if (context.Items[ScopeKey] is IServiceScope scope)
                return scope;

            scope = _serviceProvider.CreateScope();
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
    }
}
