var carrito = [];

function agregarProducto(producto) {
    var id = producto.attributes["value"].value;

    var item = new Object();
    item.id = id;
    item.cantidad = document.getElementById(id).value;

    carrito[carrito.length] = item;
}

function enviarPedido() {
    $.post('../Ordenes/Create', {
        Id: '0',
        Cliente: '6622782870',
        Pedido:JSON.stringify(carrito),
        Fecha: '02-02-2016'
    })
    .done(function (data) {
        if (data == 0) {
            swal("Examen enviado", "", "success");
            setTimeout(function () {
                window.location.assign("/modules/selectTest")
            }, 3000);
        } else {
            alert(data);
        }
    })
    .fail(function () {
        alert("error");
    });
}

