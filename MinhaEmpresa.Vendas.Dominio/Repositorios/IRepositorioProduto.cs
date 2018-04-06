using MinhaEmpresa.Vendas.Dominio.Base;
using System.Collections.Generic;

namespace MinhaEmpresa.Vendas.Dominio.Repositorios
{
    public interface IRepositorioProduto : IRepositorioCrud<Produto>
    {
        IEnumerable<Produto> ListarTresProdutosMaisCaros(decimal limiteValor);
    }
}