using MinhaEmpresa.Vendas.Web.App_Start;
using MinhaEmpresa.Vendas.Web.Application;
using MinhaEmpresa.Vendas.Web.Filters;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Query;

namespace MinhaEmpresa.Vendas.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = UnityDependencyInjectionMap.CreateContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API filters
            config.Filters.Add(new UnitOfWorkActionFilterAttribute());
            config.AddODataQueryFilter(new EnableQueryAttribute()
            {
                AllowedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount,
                MaxTop = 100
            });
        }
    }
}
