var carrito = [];
var totalOrden = 0;
var totalCarrito = document.getElementById("orden");

function agregarProducto(producto) {
    var id = producto.attributes["value"].value;
    var item = new Object();
    item.id = id;
    item.cantidad = document.getElementById(id).value;
    if (item.cantidad != "") {
        carrito[carrito.length] = item;
        numPlatillos(item.cantidad);
    }
}

function enviarPedido(actionLink) {
    if (carrito.length != 0) {
        $.post(actionLink, {
            Id: '1',
            Cliente: '6622782870',
            Pedido: JSON.stringify(carrito),
            Fecha: '02-02-2016'
        })
        .done(function () {
            alert("Orden Registrada", "", "success");
        })
        .fail(function () {
            alert("Error", "", "success");
        });
        var url = '@Url.Action("SomeAction", "SomeController")';
        // do something with the url client side variable, for example redirect
        window.location.href = url;
    } else {
        alert("Seleccione primero los platillos que desee ordenar.")
    }
}

function numPlatillos(cantidad) {
    totalOrden += parseInt(cantidad);
    totalCarrito.innerHTML = totalOrden;
}

function carritoEstatus() {
    if (carrito.length != 0) {
        var str = "";
        for (var i = 0; i < carrito.length; i++) {
            str += (i + 1) + " " + carrito[i]["cantidad"] + " " + carrito[i]["id"] + "\n";
        }
        var eliminar = prompt(str + "Eliminar registro número:");
        if (eliminar != "" && eliminar <= carrito.length) {
            numPlatillos(-carrito[eliminar - 1]["cantidad"]);
            carrito[eliminar - 1] = carrito[carrito.length - 1];
            carrito.length = carrito.length - 1;
        }
    }
}

