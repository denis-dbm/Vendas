using MinhaEmpresa.Vendas.Dominio;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;

namespace MinhaEmpresa.Vendas.Infra.Persistencia
{
    public class VendasContext : DbContext, IUnitOfWork
    {
        public VendasContext()
            : base("Vendas")
        {
            Database.SetInitializer(new VendasDbInitializer());
        }

        public VendasContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<PedidoVenda> PedidosVenda { get; set; }
        public DbSet<ItemPedidoVenda> ItensPedidosVenda { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        void IUnitOfWork.Commit()
        {
            SaveChanges();
        }
    }
}