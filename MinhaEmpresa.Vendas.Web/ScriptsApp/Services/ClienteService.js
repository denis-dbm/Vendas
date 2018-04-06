function ClienteService() {

    this.listar = function () {
        return $.get('/api/clientes/');
    };
}