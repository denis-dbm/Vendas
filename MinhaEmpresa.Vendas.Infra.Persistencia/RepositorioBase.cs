using System;

namespace MinhaEmpresa.Vendas.Infra.Persistencia
{
    public abstract class RepositorioBase
    {
        protected RepositorioBase(VendasContext contexto)
        {
            if (contexto == null)
                throw new ArgumentNullException("contexto");

            Contexto = contexto;
        }

        protected VendasContext Contexto { get; private set; }
    }
}