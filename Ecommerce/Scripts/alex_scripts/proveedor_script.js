$(function () {
    var id_producto; 
    $('table tr td').on('click', function () {
        event.preventDefault();
        id_producto = $(this).closest('tr').find(":text:eq(0)");
        $("#id_producto").val($(id_producto).val());
        $("#cantidad_modal").modal("show");
    });
    $("#cerrar_modalbtn").click(function () {
        $("#cantidad_modal").modal("hide");
    })
});