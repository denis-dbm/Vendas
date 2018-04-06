using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using System.Linq;

namespace MinhaEmpresa.Vendas.Infra.Persistencia.Repositorios
{
    public class RepositorioCliente : RepositorioCrudBase<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(VendasContext contexto)
            : base(contexto)
        {
        }
    }
}
