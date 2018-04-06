using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace MinhaEmpresa.Vendas.Web.Application
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        private readonly IUnityContainer container;

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            var newInstance = new UnityDependencyResolver(child);
            child.RegisterInstance<IDependencyScope>(newInstance, new HierarchicalLifetimeManager()); //service locator for controllers
            return newInstance;
        }

        public void Dispose()
        {
            //unregister service locator to avoid StackOverflowException
            var thisContainer = container.Registrations.Where(e => e.RegisteredType == typeof(IDependencyScope));

            foreach (var c in thisContainer)
                c.LifetimeManager.SetValue(null);
                
            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return new List<object>();
            }
        }
    }
}