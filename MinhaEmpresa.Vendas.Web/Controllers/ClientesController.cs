using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using MinhaEmpresa.Vendas.Web.Models.Cliente;
using System.Linq;
using System.Web.Http;

namespace MinhaEmpresa.Vendas.Web.Controllers
{
    public class ClientesController : ApiController
    {
        public ClientesController(IRepositorioCliente repositorio)
        {
            RepositorioCliente = repositorio;
        }

        protected IRepositorioCliente RepositorioCliente { get; set; }

        public IHttpActionResult Get(int id)
        {
            Cliente cliente = RepositorioCliente.PorId(id);

            if (cliente == null)
                return NotFound();

            return Ok(new ClienteModel()
            {
                Cpf = cliente.Cpf,
                Id = cliente.Id,
                Nome = cliente.Nome
            });
        }

        public IQueryable<ClienteModel> Get()
        {
            return RepositorioCliente.Consultar()
                .OrderBy(e => e.Nome)
                .Select(e => new ClienteModel()
                {
                    Cpf = e.Cpf,
                    Id = e.Id,
                    Nome = e.Nome
                });
        }
    }
}