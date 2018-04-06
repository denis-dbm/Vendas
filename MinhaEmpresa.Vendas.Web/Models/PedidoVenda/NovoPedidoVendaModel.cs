using System;
using System.ComponentModel.DataAnnotations;

namespace MinhaEmpresa.Vendas.Web.Models.PedidoVenda
{
    public class NovoPedidoVendaModel
    {
        [Required(ErrorMessage = "Cliente é requerido")]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Data de entrega é requerida")]
        public DateTime DataEntrega { get; set; }
        public ItemPedidoVendaModel[] Itens { get; set; }
    }
}