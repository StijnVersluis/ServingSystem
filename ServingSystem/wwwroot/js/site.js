// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
