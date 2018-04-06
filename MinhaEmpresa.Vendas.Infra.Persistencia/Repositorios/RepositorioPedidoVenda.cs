using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Repositorios;

namespace MinhaEmpresa.Vendas.Infra.Persistencia.Repositorios
{
    public class RepositorioPedidoVenda : RepositorioCrudBase<PedidoVenda>, IRepositorioPedidoVenda
    {
        public RepositorioPedidoVenda(VendasContext contexto)
            : base(contexto)
        {
        }

        protected override void ObjetoAdicionado(PedidoVenda obj)
        {
            // força a gravação no banco de dados para geração do ID instantaneamente
            Contexto.SaveChanges();
            obj.Numero = obj.Id;
        }
    }
}