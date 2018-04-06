using MinhaEmpresa.Vendas.Dominio.Base;
using MinhaEmpresa.Vendas.Dominio.Builders;
using System;
using System.Collections.Generic;

namespace MinhaEmpresa.Vendas.Dominio
{
    public class PedidoVenda : ObjetoDominio
    {
        protected PedidoVenda()
        {
            Itens = new List<ItemPedidoVenda>();
        }

        protected internal PedidoVenda(DateTime dataEntrega, Cliente cliente, IEnumerable<ItemPedidoVenda> itens)
        {
            decimal valorTotal = 0;

            foreach (var i in itens)
            {
                AtribuirItemAoPedido(i);
                valorTotal += i.ValorTotal;
            }

            DataEntrega = dataEntrega;
            Cliente = cliente;
            Itens = new List<ItemPedidoVenda>(itens);
            ValorTotal = valorTotal;
        }

        private int _numero;

        public DateTime DataEntrega { get; protected set; }
        public int Numero
        {
            get { return _numero; }
            set
            {
                if (Id == 0 || _numero == 0)
                    _numero = value;
                else
                    throw new InvalidOperationException();
            }
        }
        public virtual Cliente Cliente { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public virtual ICollection<ItemPedidoVenda> Itens { get; protected set; }

        private void AtribuirItemAoPedido(ItemPedidoVenda item)
        {
            if (item.Pedido != null)
                throw new InvalidOperationException();

            item.Pedido = this;
        }

        public static PedidoVendaBuilder Novo()
        {
            return new PedidoVendaBuilder();
        }
    }
}