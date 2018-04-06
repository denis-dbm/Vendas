using MinhaEmpresa.Vendas.Dominio.Base;

namespace MinhaEmpresa.Vendas.Dominio
{
    public class ItemPedidoVenda : ObjetoDominio
    {
        protected ItemPedidoVenda()
        {
        }

        protected internal ItemPedidoVenda(Produto produto, double quantidade, decimal valor)
        {
            Produto = produto;
            Quantidade = quantidade;
            Valor = valor;
            ValorTotal = (decimal)quantidade * valor;
        }

        public virtual PedidoVenda Pedido { get; protected internal set; }
        public virtual Produto Produto { get; protected set; }
        public double Quantidade { get; protected set; }
        public decimal Valor { get; protected set; }
        public decimal ValorTotal { get; protected set; }
    }
}
