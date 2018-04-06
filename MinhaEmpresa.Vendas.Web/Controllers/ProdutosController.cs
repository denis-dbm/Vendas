using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using MinhaEmpresa.Vendas.Web.Models.Produto;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MinhaEmpresa.Vendas.Web.Controllers
{
    public class ProdutosController : ApiController
    {
        public ProdutosController(IRepositorioProduto repositorio)
        {
            RepositorioProduto = repositorio;
        }

        protected IRepositorioProduto RepositorioProduto { get; set; }

        public IHttpActionResult Get(int id)
        {
            Produto produto = RepositorioProduto.PorId(id);

            if (produto == null)
                return NotFound();

            return Ok(new ProdutoModel()
            {
                Descricao = produto.Descricao,
                Id = produto.Id,
                Valor = produto.Valor
            });
        }

        public IQueryable<ProdutoModel> Get()
        {
            return RepositorioProduto.Consultar()
                .OrderBy(e => e.Descricao)
                .Select(e => new ProdutoModel()
                {
                    Descricao = e.Descricao,
                    Id = e.Id,
                    Valor = e.Valor
                });
        }

        [HttpGet]
        [Route("api/produtos/tres-produtos-mais-caros")]
        public IEnumerable<ProdutoModel> ListarTresProdutosMaisCaros(decimal limiteValor = decimal.MaxValue)
        {
            return RepositorioProduto.ListarTresProdutosMaisCaros(limiteValor)
                .Select(e => new ProdutoModel()
                {
                    Descricao = e.Descricao,
                    Id = e.Id,
                    Valor = e.Valor
                });
        }
    }
}
