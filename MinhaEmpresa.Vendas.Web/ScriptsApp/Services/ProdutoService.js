function ProdutoService() {

    this.listar = function () {
        return $.get('/api/produtos/');
    };
}