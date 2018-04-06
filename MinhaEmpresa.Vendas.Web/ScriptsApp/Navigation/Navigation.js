var Navigation;

function _Navigation() {
    this.navigate = function (url) {
        window.location.href = url;
    }
}

$(function () {
    Navigation = new _Navigation();
});