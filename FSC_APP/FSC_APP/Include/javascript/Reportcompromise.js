$(document).ready(function() {
    consultcompromise();
    revisarfechas();
});


function consultcompromise() {
    $("#ctl00_cphPrincipal_ddlproyect").change(function() {
        var idpro = $(this).val();
        $("#ctl00_cphPrincipal_HDreportact").val(idpro);
        $("#ctl00_cphPrincipal_HFdatetrim").val("");
        $("#ctl00_cphPrincipal_HFidtrim").val("");
        $("#ctl00_cphPrincipal_txtStartingDate").removeAttr('disabled');
        $("#ctl00_cphPrincipal_txtEndingDate").removeAttr('disabled');
        $("#ctl00_cphPrincipal_txtStartingDate").val("");
        $("#ctl00_cphPrincipal_txtEndingDate").val("");
        $("#ctl00_cphPrincipal_tablacomrpomise").css("display", "none");

    });
}

function revisarfechas() {
    $(".validarfechas").blur(function() {
        var datofecha = $(this).val();
        $("#ctl00_cphPrincipal_HFdatetrim").val($("#ctl00_cphPrincipal_HFdatetrim").val() + datofecha + "_");
        var datoid = $(this).attr("id");
        datoid = datoid.replace('txtfechafin', '');
        var idfin = $("#txtidpos" + datoid).text();
        $("#ctl00_cphPrincipal_HFidtrim").val($("#ctl00_cphPrincipal_HFidtrim").val() + idfin + "_");
    });
}
