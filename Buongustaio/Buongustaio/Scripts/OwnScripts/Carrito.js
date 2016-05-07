var carrito = [];

function agregarProducto(producto) {
    var id = producto.attributes["value"].value;

    var item = new Object();
    item.id = id;
    item.cantidad = document.getElementById(id).value;

    carrito[carrito.length] = item;
}

/*function enviarPedido() {
    $.post('sendInfo', {
        //_token: $('meta[name=csrf-token]').attr('content'),
        //validate: (counter - totalPages),
        array: JSON.stringify(carrito),
        user: userid
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
}*/

