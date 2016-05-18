var carrito = new Object;
var totalOrden = 0;
var totalCarrito = document.getElementById("orden");

function agregarProducto(producto) {
    var id = producto.attributes["value"].value;
    var item = new Object();
    item.id = id;
    item.cantidad = document.getElementById(id).value;
    if (item.cantidad != "") {
        carrito[Object.keys(carrito).length] = item;
        numPlatillos(item.cantidad);
        addModal(item);
    }
}

function enviarPedido(actionLink) {
    var token = $('[name=__RequestVerificationToken]').val();
    if (Object.keys(carrito).length != 0) {
        $.post(actionLink, {
            __RequestVerificationToken: token,
            Id: '1',
            Cliente: '6622782870',
            Pedido: JSON.stringify(carrito),
            Fecha: '02-02-2016'
        })
        .done(function (response) {
            if (response != 'Error') {
                id = response['ordenId'];
                getPago();
                window.location.href = newUrl;
            } else {
                alert("Hubo un error en el servidor");
            }
        })
        .fail(function () {
            alert("Error", "", "");
        });
    } else {
        alert("Seleccione primero los platillos que desee ordenar.")
    }
}

function numPlatillos(cantidad) {
    totalOrden += parseInt(cantidad);
    totalCarrito.innerHTML = totalOrden;
}

function carritoEstatus() {
    if (Object.keys(carrito).length != 0) {
        var str = "";
        for (var i = 0; i < carrito.length; i++) {
            str += (i + 1) + " " + carrito[i]["cantidad"] + " " + carrito[i]["id"] + "\n";
        }
        var eliminar = prompt(str + "Eliminar registro número:");
        if (eliminar != "" && eliminar <= carrito.length) {
            numPlatillos(-carrito[eliminar - 1]["cantidad"]);
            carrito[eliminar - 1] = carrito[(Object.keys(carrito).length - 1)];
            carrito.length = carrito.length - 1;
        }
    }
}

function addModal(myItem) {
    /*var newTr = document.createElement("tr");
    var newTd = document.createElement("td");
    var newTdStyle = document.createElement("td");
    var tdAtrib = document.createAttribute("style=\"text-align:center;");
    newTdStyle.setAttribute(tdAtrib);*/

    var row = document.getElementById("rw-item");
    var table = document.getElementById('tb-carrito');
    var clone = row.cloneNode(true);
    clone.attributes['name'] = myItem.id;
        clone.children[0].textContent = myItem.cantidad;
        clone.children[1].textContent = myItem.id;
        clone.children[2].textContent = 'q';

    table.appendChild(clone);
}