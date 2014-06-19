$(document).ready(function() {
    cargar();
    $('#tblReport').dataTable();
});

function cargar() {
    $.ajax({
        url: "../Report/AjaxReportContractual.aspx",
        type: "POST",
        data: { "action": "loadreport" },
        success: function(result) {
            $("#tblReport").html(result);
        },
        error: function() {
            alert("Hubo un error al generar el reporte. Por favor vuelva a intentarlo.")
        }
    });
}