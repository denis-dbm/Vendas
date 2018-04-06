function PedidoVendaService() {

    this.consultar = function (params) {
        var queryString = '';

        if ($.isPlainObject(params))
            queryString = $.param(params);

        return $.get('/api/pedidosvenda/?' + queryString);
    };

    this.fazerPedido = function (pedido) {
        return $.post('/api/pedidosVenda/', pedido);
    }
}