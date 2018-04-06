using MinhaEmpresa.Vendas.Dominio.Base;

namespace MinhaEmpresa.Vendas.Dominio
{
    public class Cliente : ObjetoDominio
    {
        static Cliente()
        {
            ConsumidorGenerico = new Cliente();
            ConsumidorGenerico.Id = 1;
            ConsumidorGenerico.Nome = "CONSUMIDOR GENERICO";
            ConsumidorGenerico.Cpf = string.Empty;
        }

        public static readonly Cliente ConsumidorGenerico;

        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}
