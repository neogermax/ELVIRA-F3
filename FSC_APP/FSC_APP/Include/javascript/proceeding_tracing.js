$(document).ready(function() {
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "none");
    reemplazo();
    guardar();
})

function reemplazo() {
    $("#ctl00_cphPrincipal_ddlTypeThird").change(function() {
        //Cambiar el tipo de actor en los labels
        //$("#ctl00_cphPrincipal_lblOperator").text($(this).val());
    })

    $("#ctl00_cphPrincipal_ddlTypeoF").change(function() {
        //Cambiar el tipo de objeto en los labels
        $("#ctl00_cphPrincipal_lblDates").text('Fechas del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblObject").text('Objeto del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblValue").text('Valor del ' + $(this).val() + ' $:');
        $("#ctl00_cphPrincipal_lblAdittions").text('Adiciones al ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_Label1").text('Valor final del ' + $(this).val() + ' en $:');

    })

}

//funcion q ejecuta el menzaje
function guardar() {
    $("#ctl00_cphPrincipal_Btnexport").click(function() {
        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo").text("El acta de seguimiento se generó con éxito");
        $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo_2").text("El acta de seguimiento se generó con éxito");
        var acta = $("#ctl00_cphPrincipal_HFacta").val();
        $("#ctl00_cphPrincipal_txtProceedNumber").val(acta);
    });
}