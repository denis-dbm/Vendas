using MinhaEmpresa.Vendas.Infra.Persistencia;
using MinhaEmpresa.Vendas.Web.Application;
using System.Net.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace MinhaEmpresa.Vendas.Web.Filters
{
    public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IDependencyScope escopo = actionExecutedContext.Request.GetDependencyScope();
            var contexto = escopo.GetService<VendasContext>();
            
            if (contexto != null && contexto.ChangeTracker.HasChanges() && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                contexto.SaveChanges();
            }
        }
    }
}