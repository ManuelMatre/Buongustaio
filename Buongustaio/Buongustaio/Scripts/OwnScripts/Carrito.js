var totalCarritoHtml = document.getElementById("orden");
var totalHtml = document.getElementById('total-pago');
var carrito = new Object;
var totalPlatillos = 0;
var totalPago = 0;

function agregarProducto(producto) {
    var item = new Object();
    item.cantidad = document.getElementById(producto.Id).value;
    if (item.cantidad != ""&& item.cantidad != 0){
        totalPago += (producto.Precio * item.cantidad);
        item.platillo = producto;
        carrito[Object.keys(carrito).length] = item;
        numPlatillos(item.cantidad);
        addModal(item);
    }
    totalHtml.innerHTML = 'Total: $' + totalPago;
}

function enviarPedido(actionLink) {
    var token = $('[name=__RequestVerificationToken]').val();
    var hayContenido = false;
    for (var i = 0; i < Object.keys(carrito).length; i++) {
        if (carrito[i] != null) {
            hayContenido = true;
            break;
        }
    }
    if (hayContenido){
        $.post(actionLink, {
            __RequestVerificationToken: token,
            Id: '1',
            Cliente: '6622782870',
            Pedido: JSON.stringify(carrito),
            Fecha: '02-02-2016'
        })
        .done(function (response) {
            if (response['url'] != 'Error') {
                id = response['ordenId'];
                getPago(); // fucion ubicada en la vista
                window.location.href = newUrl;
            } else {
                alert(response['ordenId']);
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
    totalPlatillos += parseInt(cantidad);
    totalCarritoHtml.innerHTML = totalPlatillos;
}

function addModal(myItem) {
    var row = document.getElementById("rw-item");
    var table = document.getElementById('tb-carrito');
    var clone = row.cloneNode(true);
    clone.style.display = 'table-row';
    clone.attributes['id'].value = "" + Object.keys(carrito).length - 1;
        clone.children[0].textContent = myItem.cantidad;
        clone.children[1].textContent = myItem.platillo.Descripcion;
        clone.children[2].textContent = '$' + (myItem.cantidad * myItem.platillo.Precio);
        //one.children[3].children[0].value = Object.keys(carrito).length - 1;
        //table.appendChild(clone);
        table.insertAdjacentElement('afterBegin', clone);
}

function eliminarCarrito(data) {
    var rowNum = data.parentNode.parentNode.attributes['id'].value;
    var row = document.getElementById(rowNum);
    numPlatillos(-(carrito[rowNum].cantidad));
    //totalPlatillos -= carrito[rowNum].cantidad;
    totalPago -= (carrito[rowNum].cantidad * carrito[rowNum].platillo.Precio);
    if (totalPago < 0) totalPago = 0;
    carrito[rowNum] = null;
    var table = document.getElementById('tb-carrito');
    table.removeChild(row);
    totalHtml.innerHTML = 'Total: $' + totalPago.toFixed(2);
}