using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Exceptions;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using MinhaEmpresa.Vendas.Web.Application;
using MinhaEmpresa.Vendas.Web.Models.Cliente;
using MinhaEmpresa.Vendas.Web.Models.PedidoVenda;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace MinhaEmpresa.Vendas.Web.Controllers
{
    public class PedidosVendaController : ApiController
    {
        public PedidosVendaController(IDependencyScope serviceLocator)
        {
            RepositorioCliente = serviceLocator.GetService<IRepositorioCliente>();
            RepositorioProduto = serviceLocator.GetService<IRepositorioProduto>();
            RepositorioPedidoVenda = serviceLocator.GetService<IRepositorioPedidoVenda>();
        }

        protected IRepositorioCliente RepositorioCliente { get; set; }

        protected IRepositorioProduto RepositorioProduto { get; set; }

        protected IRepositorioPedidoVenda RepositorioPedidoVenda { get; set; }

        public IHttpActionResult Get(int id)
        {
            PedidoVenda pedidoVenda = RepositorioPedidoVenda.PorId(id);

            if (pedidoVenda == null)
                return NotFound();

            var pedidoVendaModel = new PedidoVendaModel();
            pedidoVendaModel.Cliente = new ClienteModel()
            {
                Cpf = pedidoVenda.Cliente.Cpf,
                Id = pedidoVenda.Cliente.Id,
                Nome = pedidoVenda.Cliente.Nome
            };
            pedidoVendaModel.DataEntrega = pedidoVenda.DataEntrega;
            pedidoVendaModel.Id = pedidoVenda.Id;
            pedidoVendaModel.Itens = pedidoVenda.Itens.Select(e => new ItemPedidoVendaModel()
            {
                IdProduto = e.Produto.Id,
                DescricaoProduto = e.Produto.Descricao,
                Quantidade = e.Quantidade,
                Valor = e.Valor,
                ValorTotal = e.ValorTotal
            }).ToArray();
            pedidoVendaModel.ValorTotal = pedidoVenda.ValorTotal;
            return Ok(pedidoVendaModel);
        }

        [HttpGet]
        public IQueryable<CabecalhoPedidoModel> Consultar(int? idCliente = null, int? numeroPedido = null, DateTime? entregaInicio = null, DateTime? entregaFim = null)
        {
            var consulta = RepositorioPedidoVenda.Consultar();

            if (idCliente != null)
                consulta = consulta.Where(e => e.Cliente.Id == idCliente);

            if (numeroPedido != null)
                consulta = consulta.Where(e => e.Numero == numeroPedido);

            if (entregaInicio != null)
                consulta = consulta.Where(e => e.DataEntrega >= entregaInicio);

            if (entregaFim != null)
                consulta = consulta.Where(e => e.DataEntrega <= entregaFim);

            return consulta.Select(e => new CabecalhoPedidoModel()
            {
                Id = e.Id,
                Numero = e.Numero,
                RazaoCliente = e.Cliente.Nome,
                ValorTotal = e.ValorTotal
            });
        }

        [HttpPost]
        public IHttpActionResult FazerPedido([FromBody]NovoPedidoVendaModel pedido)
        {
            PedidoVenda pedidoVenda;
            Cliente cliente = RepositorioCliente.PorId(pedido.IdCliente);

            var pedidoBuilder = PedidoVenda.Novo()
                .Cliente(cliente)
                .EntregaEm(pedido.DataEntrega);

            foreach (var item in pedido.Itens)
            {
                Produto produto = RepositorioProduto.PorId(item.IdProduto);

                if (produto == null)
                    return BadRequest();

                pedidoBuilder.CompraProduto(produto, item.Quantidade, item.Valor);
            }

            try
            {
                pedidoVenda = pedidoBuilder.Construir();
                RepositorioPedidoVenda.Adicionar(pedidoVenda);
            }
            catch (PedidoVendaException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtRoute("DefaultApi", new { controller = "PedidoVenda", id = pedidoVenda.Id }, new CabecalhoPedidoModel()
            {
                Id = pedidoVenda.Id,
                Numero = pedidoVenda.Numero,
                RazaoCliente = pedidoVenda.Cliente.Nome,
                ValorTotal = pedidoVenda.ValorTotal
            });
        }
    }
}