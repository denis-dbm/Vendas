using System.ComponentModel.DataAnnotations;

namespace MinhaEmpresa.Vendas.Web.Models.PedidoVenda
{
    public class ItemPedidoVendaModel
    {
        [Required(ErrorMessage = "Produto é requerido")]
        public int IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        [Required(ErrorMessage = "Quantidade é requerida")]
        public double Quantidade { get; set; }
        [Required(ErrorMessage = "Valor é requerido")]
        public decimal Valor { get; set; }
        public decimal? ValorTotal { get; set; }
    }
}