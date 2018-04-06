namespace MinhaEmpresa.Vendas.Web.Models.PedidoVenda
{
    public class CabecalhoPedidoModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string RazaoCliente { get; set; }
        public decimal ValorTotal { get; set; }
    }
}