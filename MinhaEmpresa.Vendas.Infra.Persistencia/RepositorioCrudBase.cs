using MinhaEmpresa.Vendas.Dominio.Base;
using System.Linq;

namespace MinhaEmpresa.Vendas.Infra.Persistencia
{
    public abstract class RepositorioCrudBase<T> : RepositorioBase, IRepositorioCrud<T> where T : ObjetoDominio
    {
        protected RepositorioCrudBase(VendasContext contexto)
            : base(contexto)
        {
        }

        public T PorId(int id)
        {
            return Contexto.Set<T>().Where(e => e.Id == id).SingleOrDefault();
        }

        public IQueryable<T> Consultar()
        {
            return Contexto.Set<T>();
        }

        public void Adicionar(T obj)
        {
            Contexto.Set<T>().Add(obj);
            ObjetoAdicionado(obj);
        }

        protected virtual void ObjetoAdicionado(T obj)
        {
        }

        public void Remover(T obj)
        {
            Contexto.Set<T>().Remove(obj);
        }
    }
}
