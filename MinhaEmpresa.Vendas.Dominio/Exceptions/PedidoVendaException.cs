using System;

namespace MinhaEmpresa.Vendas.Dominio.Exceptions
{
    public class PedidoVendaException : Exception
    {
        public PedidoVendaException(string message)
            : this(message, null)
        {
        }

        public PedidoVendaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
