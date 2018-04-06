using MinhaEmpresa.Vendas.Dominio.Base;
using System.Linq;

namespace MinhaEmpresa.Vendas.Dominio.Base
{
    public interface IRepositorioCrud<T> where T : ObjetoDominio
    {
        T PorId(int id);
        IQueryable<T> Consultar();
        void Adicionar(T obj);
        void Remover(T obj);
    }
}
