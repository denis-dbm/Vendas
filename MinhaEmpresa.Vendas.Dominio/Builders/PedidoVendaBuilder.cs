using MinhaEmpresa.Vendas.Dominio.Exceptions;
using System;
using System.Collections.Generic;

namespace MinhaEmpresa.Vendas.Dominio.Builders
{
    public class PedidoVendaBuilder
    {
        public PedidoVendaBuilder()
            : this(DateTime.Today)
        {
        }

        public PedidoVendaBuilder(DateTime dataMinimaEntrega)
        {
            itens = new Dictionary<Produto, RequisicaoItem>();
            this.dataMinimaEntrega = dataMinimaEntrega;
        }

        private readonly DateTime dataMinimaEntrega;
        private DateTime dataEntrega;
        private Cliente cliente;
        private readonly IDictionary<Produto, RequisicaoItem> itens;

        public PedidoVendaBuilder EntregaEm(DateTime data)
        {
            dataEntrega = data;
            return this;
        }

        public PedidoVendaBuilder Cliente(Cliente cliente)
        {
            this.cliente = cliente;
            return this;
        }

        public PedidoVendaBuilder CompraProduto(Produto produto, double quantidade, decimal valor)
        {
            RequisicaoItem item;

            if (!itens.TryGetValue(produto, out item))
            {
                item = new RequisicaoItem();
                itens.Add(produto, item);
            }

            item.Quantidade = quantidade;
            item.Valor = valor;

            return this;
        }

        public PedidoVenda Construir()
        {
            if (dataEntrega < dataMinimaEntrega)
                throw new PedidoVendaException("Data de entrega deve ser de hoje em diante");

            List<ItemPedidoVenda> itensPedido = new List<ItemPedidoVenda>();
            PedidoVenda pedidoVenda;

            foreach (Produto p in itens.Keys)
            {
                var requisicao = itens[p];
                ItemPedidoVenda item = new ItemPedidoVenda(p, requisicao.Quantidade, requisicao.Valor);
                itensPedido.Add(item);
            }

            if (cliente == null)
            {
                cliente = Dominio.Cliente.ConsumidorGenerico;
            }

            pedidoVenda = new PedidoVenda(dataEntrega, cliente, itensPedido);
            Reiniciar();
            return pedidoVenda;
        }

        private void Reiniciar()
        {
            dataEntrega = DateTime.MinValue;
            cliente = null;
            itens.Clear();
        }

        class RequisicaoItem
        {
            internal double Quantidade { get; set; }
            internal decimal Valor { get; set; }
        }
    }
}