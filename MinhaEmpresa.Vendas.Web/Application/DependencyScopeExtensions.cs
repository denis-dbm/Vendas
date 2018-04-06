using System.Web.Http.Dependencies;

namespace MinhaEmpresa.Vendas.Web.Application
{
    public static class DependencyScopeExtensions
    {
        public static T GetService<T>(this IDependencyScope scope)
        {
            return (T)scope.GetService(typeof(T));
        }
    }
}