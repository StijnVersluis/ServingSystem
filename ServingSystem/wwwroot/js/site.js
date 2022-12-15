// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Open a Table via the modal
$(document).ready(function () {
    if ($("#priceInput").length > 0) $("#priceInput").val($("#priceInput").val().replace(",", "."))
})

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

function RemoveFromOrder(tableId, productId) {
    fetch(window.location.origin + "/Order/RemoveProduct", {
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
        .then(resp => resp.text())
        .then(() => {
            $("#SaveOrderBtn").removeClass("d-none")
            $("#CreateOrderBtn").addClass("d-none")
        })
        .then(window.location.reload(true))
}

function SaveOrder(tableId) {
    fetch(window.location.origin + "/Order/Save/" + tableId)
        .then(resp => resp.text())
        .then(() => {
            $("#SaveOrderBtn").addClass("d-none")
            $("#CreateOrderBtn").removeClass("d-none")
        })
        .then(window.location.reload(true))
}

function FilterUnopenedTables() {
    let value = $("#UnopenedTableFilterInput").val();
    fetch(window.location.origin + "/Table/GetFilteredUnopenTables?filter=" + value)
        .then(resp => resp.text())
        .then(data => {
            $("#NewTableList").html("");
            $("#NewTableList").html(data);
        })
}

function CloseTable(tableId) {
    if (confirm("Close Table?")) {
        window.location = "/Table/CloseTable/" + tableId;
    }
}

function EmptyFilter() {
    $("#UnopenedTableFilterInput").val("");
    FilterUnopenedTables();
}

function numberInputKommaToDot() {

}