using MinhaEmpresa.Vendas.Web.Models.Cliente;
using System;

namespace MinhaEmpresa.Vendas.Web.Models.PedidoVenda
{
    public class PedidoVendaModel
    {
        public int Id { get; set; }
        public ClienteModel Cliente { get; set; }
        public DateTime DataEntrega { get; set; }
        public ItemPedidoVendaModel[] Itens { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}