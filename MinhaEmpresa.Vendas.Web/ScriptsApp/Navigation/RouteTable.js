var RouteTable;

function _RouteTable() {
    this.home = {
        pedidos: '/Home/Pedidos',
        novoPedido: '/Home/NovoPedido'
    };
}

$(function () {
    RouteTable = new _RouteTable();
});