function NovoPedidoViewModel(clienteService, produtoService, pedidoVendaService) {
    function _criarNovoPedido() {
        var p = {
            IdCliente: ko.observable(null),
            DataEntrega: ko.observable(null),
            ValorTotal: ko.observable((0).toFixed(2)),
            Itens: ko.observableArray([])
        };

        p.Itens().index = {};

        return p;
    }

    function _criarNovoItem() {
        var i = {
            IdProduto: ko.observable(null),
            DescricaoProduto: ko.observable(null),
            Qtde: ko.observable(null),
            Valor: ko.observable(null),
            ValorTotal: ko.observable(null)
        };
        
        i.IdProduto.subscribe(function (value) {
            if (!$.isNumeric(value)) {
                i.Valor(null);
                i.Qtde(null);
                return;
            }

            var produtos = self.produtos();
            i.Valor(produtos[produtos.index[value]].Valor.toFixed(2));
            i.Qtde(1);
        });

        return i;
    }

    var self = this;
    this.clientes = ko.observableArray([]);
    this.produtos = ko.observableArray([]);
    this.novoPedido = ko.observable(_criarNovoPedido());
    this.novoItem = ko.observable(_criarNovoItem());

    this.adicionarItem = function () {
        var item = self.novoItem();

        if (item == null || !$.isNumeric(item.IdProduto())) {
            toastr.warning('Selecione um produto', 'Validação');
            return;
        }

        if (!$.isNumeric(item.Qtde()) || item.Qtde() < 1) {
            toastr.warning('Quantidade maior que zero requerida', 'Validação');
            return;
        }

        var itemExistente = self.novoPedido().Itens().index[item.IdProduto()];
        var qtdeExistente = 0;
        var totalItemAnterior = 0;

        if (itemExistente) {
            if (confirm("Produto na lista. Deseja acrescentar a quantidade?")) {
                item = itemExistente;
                qtdeExistente = parseFloat(item.Qtde());
                totalItemAnterior = parseFloat(item.ValorTotal());
            } else {
                return;
            }
        }

        var produtos = self.produtos();
        var descricaoProduto = produtos[produtos.index[item.IdProduto()]].Descricao;
        var qtde = parseFloat(item.Qtde()) + qtdeExistente;
        var valor = parseFloat(item.Valor());
        var valorTotalItem = qtde * valor;
        var totalPedido = parseFloat(self.novoPedido().ValorTotal());

        item.DescricaoProduto(descricaoProduto);
        item.ValorTotal(valorTotalItem.toFixed(2));

        if (qtdeExistente == 0 && totalItemAnterior == 0) {
            self.novoPedido().Itens.push(item);
            self.novoPedido().Itens().index[item.IdProduto()] = item;
            self.novoPedido().ValorTotal((totalPedido + valorTotalItem).toFixed(2));
        } else {
            item.Qtde(qtde);
            self.novoPedido().ValorTotal((totalPedido + (valorTotalItem - totalItemAnterior)).toFixed(2));
        }

        self.novoItem(_criarNovoItem());
    };

    this.removerItem = function (item) {
        self.novoPedido().Itens.remove(item);
        delete self.novoPedido().Itens().index[item.IdProduto()];
        var totalPedido = parseFloat(self.novoPedido().ValorTotal()) - parseFloat(item.ValorTotal());
        self.novoPedido().ValorTotal(totalPedido.toFixed(2));
    };

    this.cancelarPedido = function () {
        if (confirm('Deseja cancelar o pedido?')) {
            Navigation.navigate(RouteTable.home.pedidos);
        }
    }

    this.confirmarPedido = function () {
        if (self.novoPedido().DataEntrega() == null) {
            toastr.warning('Data de entrega é requerida', 'Validação');
            return;
        }

        if (self.novoPedido().Itens().length == 0) {
            toastr.warning('Pedido precisa conter ao menos um item', 'Validação');
            return;
        }

        if (!confirm('Deseja confirmar o pedido?')) {
            return;
        }

        var pedido = {
            IdCliente: self.novoPedido().IdCliente(),
            DataEntrega: self.novoPedido().DataEntrega(),
            Itens: []
        };

        $.each(self.novoPedido().Itens(), function (i, e) {
            var item = {
                IdProduto: e.IdProduto(),
                Quantidade: e.Qtde(),
                Valor: e.Valor()
            };
            pedido.Itens.push(item);
        });
        
        pedidoVendaService.fazerPedido(pedido).done(function () {
            toastr.success('Pedido confirmado!', 'Pedido de Venda');

            setTimeout(function () {
                Navigation.navigate(RouteTable.home.pedidos);
            }, 2000);
        }).fail(function (data) {
            var mensagemErro;

            if ($.isPlainObject(data.responseJSON) && data.responseJSON.Message) {
                mensagemErro = data.responseJSON.Message;
            } else {
                mensagemErro = 'Erro desconhecido no processamento';
            }

            toastr.error(mensagemErro, 'Pedido de Venda');
        });
    }

    clienteService.listar().done(function (data) {
        self.clientes(data);
    });

    produtoService.listar().done(function (data) {
        data.index = {};

        $(data).each(function (i, e) {
            if (e.Id) {
                data.index[e.Id.toString()] = i;
            }
        });
        
        self.produtos(data);
    });
}

$(function () {
    var clienteService = new ClienteService();
    var produtoService = new ProdutoService();
    var pedidoVendaService = new PedidoVendaService();
    ko.applyBindings(new NovoPedidoViewModel(clienteService, produtoService, pedidoVendaService));

    $('#clientePedido').select2();
    $('#produtoItem').select2();
});