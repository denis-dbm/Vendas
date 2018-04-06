using MinhaEmpresa.Vendas.Dominio.Base;

namespace MinhaEmpresa.Vendas.Dominio
{
    public class Produto : ObjetoDominio
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
