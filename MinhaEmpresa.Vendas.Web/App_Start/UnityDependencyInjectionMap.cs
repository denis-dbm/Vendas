using Microsoft.Practices.Unity;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using MinhaEmpresa.Vendas.Infra.Persistencia;
using MinhaEmpresa.Vendas.Infra.Persistencia.Repositorios;

namespace MinhaEmpresa.Vendas.Web.App_Start
{
    public static class UnityDependencyInjectionMap
    {
        static void Setup(IUnityContainer container)
        {
            //orm
            container
                .RegisterType<VendasContext, VendasContext>(new HierarchicalLifetimeManager(), new InjectionConstructor());

            //repositorios
            container
                .RegisterType<IRepositorioCliente, RepositorioCliente>(new HierarchicalLifetimeManager())
                .RegisterType<IRepositorioProduto, RepositorioProduto>(new HierarchicalLifetimeManager())
                .RegisterType<IRepositorioPedidoVenda, RepositorioPedidoVenda>(new HierarchicalLifetimeManager());
        }

        public static IUnityContainer CreateContainer()
        {
            UnityContainer container = new UnityContainer();
            Setup(container);
            return container;
        }
    }
}