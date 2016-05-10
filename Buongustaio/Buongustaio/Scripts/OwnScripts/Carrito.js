var carrito = [];

function agregarProducto(producto) {
    var id = producto.attributes["value"].value;

    var item = new Object();
    item.id = id;
    item.cantidad = document.getElementById(id).value;

    carrito[carrito.length] = item;
}

function enviarPedido(actionLink) {
    $.post(actionLink, {
        Id: '1',
        Cliente: '6622782870',
        Pedido:JSON.stringify(carrito),
        Fecha: '02-02-2016'
    })
    .done(function () {
        alert("Orden Registrada", "", "success");
    })
    .fail(function () {
        alert("Error", "", "success");
    });
}

