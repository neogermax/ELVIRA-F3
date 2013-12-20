
$(document).ready(function() {
    consultaacta();
});


function consultaacta() {
    $("#ctl00_cphPrincipal_ddlproyect").change(function() {
        var idpro = $(this).val();
        $("#ctl00_cphPrincipal_HDreportact").val(idpro);
    });

}
