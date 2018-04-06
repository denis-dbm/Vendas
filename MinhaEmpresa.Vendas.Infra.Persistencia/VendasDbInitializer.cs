using MinhaEmpresa.Vendas.Dominio;
using System;
using System.Linq;
using System.Data.Entity;

namespace MinhaEmpresa.Vendas.Infra.Persistencia
{
    public class VendasDbInitializer : CreateDatabaseIfNotExists<VendasContext>
    {
        protected override void Seed(VendasContext context)
        {
            var produtos = new Produto[]
            {
                new Produto() { Descricao = "Smart TV LG 42\"", Valor = 2100 },
                new Produto() { Descricao = "Notebook Dell Vostro 3550", Valor = 2750 },
                new Produto() { Descricao = "ZenFone 2 16GB", Valor = 1250 },
                new Produto() { Descricao = "Livro .Net Core Resumão", Valor = 250 },
                new Produto() { Descricao = "Mouse Logitech sem fio", Valor = 50 },
                new Produto() { Descricao = "MousePad cor sólida", Valor = 2.5m },
                new Produto() { Descricao = "Caneca linguagens - branco ou preto", Valor = 12 },
                new Produto() { Descricao = "Teclado Microsoft Wired 800", Valor = 131 },
                new Produto() { Descricao = "Impressora HP Deskjet Ink Advantage", Valor = 199.99m },
            };
            var clientes = new Cliente[]
            {
                new Cliente() { Cpf = "98262100565", Nome = "Denis Bittencourt" },
                new Cliente() { Cpf = "61202277764", Nome = "Diego Muniz" },
                new Cliente() { Cpf = "26361532410", Nome = "Dinorah Gabriel" },
                new Cliente() { Cpf = "78179211800", Nome = "Ronaldo Beraldo" }
            };

            // CONSUMIDOR GENERICO, sempre ID 1
            context.Clientes.Add(Cliente.ConsumidorGenerico);
            context.SaveChanges();

            context.Produtos.AddRange(produtos);
            context.Clientes.AddRange(clientes);

            var pedidoVenda1 = PedidoVenda.Novo()
                    .Cliente(clientes[0])
                    .EntregaEm(new DateTime(2020, 11, 30))
                    .CompraProduto(produtos[2], 2, 1250)
                    .CompraProduto(produtos[1], 1, 2500)
                    .Construir();
            pedidoVenda1.Numero = 1;

            var pedidoVenda2 = PedidoVenda.Novo()
                    .Cliente(clientes[1])
                    .EntregaEm(new DateTime(2020, 12, 29))
                    .CompraProduto(produtos[0], 1, 2000)
                    .CompraProduto(produtos[4], 1, 50)
                    .Construir();
            pedidoVenda2.Numero = 2;

            context.PedidosVenda.AddRange(new PedidoVenda[]
            {
                pedidoVenda1, pedidoVenda2
            });

            context.SaveChanges();
        }
    }
}
