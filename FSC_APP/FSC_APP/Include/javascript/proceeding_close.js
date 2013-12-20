$(document).ready(function() {
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "none");
    reemplazo();
    guardar();
})

function reemplazo() {
    $("#ctl00_cphPrincipal_ddlTypeThird").change(function() {
        //Cambiar el tipo de actor en los labels
        $("#ctl00_cphPrincipal_lblOperator").text($(this).val());
    })

    $("#ctl00_cphPrincipal_ddlTypeoF").change(function() {
        //Cambiar el tipo de objeto en los labels
        $("#ctl00_cphPrincipal_lblDates").text('Fechas del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblObjectContract").text('Objeto del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblContractInitialValue").text('Valor inicial del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblContractAditions").text('Adiciones al ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblFinalValue").text('Valor final del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblWeakness").text('Debilidades del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblOportunity").text('Oportunidades del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblStrong").text('Fortalezas del ' + $(this).val() + ':');
        $("#ctl00_cphPrincipal_lblLearning").text('Aprendizajes del ' + $(this).val() + ':');

    })

}

//funcion q ejecuta el menzaje
function guardar() {
    $("#ctl00_cphPrincipal_btnExport").click(function() {
        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo").text("El acta de cierre se generó con éxito");
        $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo_2").text("El acta de cierre se generó con éxito");
    });
}

//EJECUCION DE PRETTY PHOTO
$("a.pretty").prettyPhoto({
    callback: function() {
    }, /* Called when prettyPhoto is closed */
    ie6_fallback: true,
    modal: true,
    social_tools: false
});