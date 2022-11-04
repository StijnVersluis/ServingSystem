﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Open a Table via the modal

$('#NewTableModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var name = button.data('name') // Extract info from data-* attributes
    var id = button.data('id') // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)
    modal.find('.modal-body .modal-table-name').html(name)
    modal.find('.modal-footer .modal-table-id-input').val(id)
    let currentDate = new Date();
    modal.find('.modal-body .modal-current-time').html(currentDate.getHours() + ":" + currentDate.getMinutes())
})

function AddToOrder(tableId, productId) {
    fetch(window.location.origin + "/Order/AddProduct", {
        method: "POST",
        body: JSON.stringify({
            tableid: tableId,
            productid: productId
        }),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
        .then(resp => resp.text())
        .then(data => window.location.reload(true))
}


function CreateOrder(tableId) {
    fetch(window.location.origin + "/Table/CreateOrder", {
        method: "POST",
        body: JSON.stringify({
            tableid: tableId,
        }),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
        .then(window.location.reload(true))
}

function SaveOrder(tableId) {
    fetch(window.location.origin + "/Order/Save/" + tableId)
        .then(resp => resp.text())
        .then(data => window.location.reload(true))
}
