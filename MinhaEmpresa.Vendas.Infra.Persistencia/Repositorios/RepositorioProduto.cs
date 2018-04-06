using System;
using System.Collections.Generic;
using MinhaEmpresa.Vendas.Dominio;
using MinhaEmpresa.Vendas.Dominio.Repositorios;
using System.Data;

namespace MinhaEmpresa.Vendas.Infra.Persistencia.Repositorios
{
    public class RepositorioProduto : RepositorioCrudBase<Produto>, IRepositorioProduto
    {
        public RepositorioProduto(VendasContext contexto)
            : base(contexto)
        {
        }

        public IEnumerable<Produto> ListarTresProdutosMaisCaros(decimal limiteValor)
        {
            List<Produto> produtos = new List<Produto>();

            using (var cmd = Contexto.Database.Connection.CreateCommand())
            {
                if (Contexto.Database.Connection.State == ConnectionState.Closed)
                {
                    Contexto.Database.Connection.Open();
                }

                cmd.CommandText = "SELECT TOP 3 * FROM Produto WHERE Valor <= @limiteValor ORDER BY Valor DESC";
                cmd.CommandType = CommandType.Text;
                var param = cmd.CreateParameter();
                param.ParameterName = "limiteValor";
                param.Value = limiteValor;
                cmd.Parameters.Add(param);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Produto p = new Produto();
                    p.Descricao = Convert.ToString(reader["Descricao"]);
                    p.Valor = Convert.ToDecimal(reader["Valor"]);

                    produtos.Add(p);
                }

                reader.Close();
            }

            return produtos;
        }
    }
}
