function PedidosViewModel(clienteService, pedidoVendaService) {
    var self = this;
    var pedidosDataTable;
    this.pesquisa = ko.observable({
        idCliente: ko.observable(null),
        numeroPedido: ko.observable(null),
        entregaInicio: ko.observable(null),
        entregaFim: ko.observable(null)
    });
    this.pedidosVenda = ko.observableArray([]);
    this.clientes = ko.observableArray([]);

    this.pesquisar = function () {
        var params = {
            idCliente: self.pesquisa().idCliente(),
            numeroPedido: self.pesquisa().numeroPedido(),
            entregaInicio: self.pesquisa().entregaInicio(),
            entregaFim: self.pesquisa().entregaFim()
        };

        if (!$.isNumeric(params.idCliente)) {
            delete params.idCliente;
        }

        if (params.numeroPedido && !$.isNumeric(params.numeroPedido)) {
            params.numeroPedido = -1;
        }

        if (params.entregaInicio && (!params.entregaFim || params.entregaFim < params.entregaInicio)) {
            toastr.clear();
            toastr.warning('Período de entrega deve possuir início e fim, sendo fim posterior ao início', 'Validação');
            return;
        }

        pedidoVendaService.consultar(params).done(function (data) {
            if (pedidosDataTable) {
                pedidosDataTable.destroy();
                $('#pedidos-tbody').empty();
            }

            self.pedidosVenda(data);
            pedidosDataTable = $('#pedidos').DataTable();
        });
    };
    
    clienteService.listar().done(function (data) {
        self.clientes(data);
    });
}

$(function () {
    var clienteService = new ClienteService();
    var pedidoVendaService = new PedidoVendaService();
    ko.applyBindings(new PedidosViewModel(clienteService, pedidoVendaService));

    $('#cliente').select2();
});