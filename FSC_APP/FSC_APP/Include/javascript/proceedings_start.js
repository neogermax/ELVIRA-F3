var timer = setTimeout("fix();", 2000);
var contador = 0;

$(document).ready(function() {
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "none");
    guardar();
    cambiartitle();
    cambiartitle2();
    var timer = setTimeout("fix();", 2000);
    validafecha();
    revisarfechas();
});

function fix() {

    if ($.trim($("#ctl00_cphPrincipal_Txtdurationcontract").val()).length > 0) {
        $("#ctl00_cphPrincipal_Txtdurationcontract").trigger("blur");
        clearTimeout(timer);
    }
}


function revisarfechas() {
    $(".validarfechas").blur(function() {
        var datofecha = $(this).val();
        $("#ctl00_cphPrincipal_HFdatetrim").val($("#ctl00_cphPrincipal_HFdatetrim").val() + datofecha + "_");
    });
}

function validafecha() {
    $("#ctl00_cphPrincipal_Txtdurationcontract").blur(function() {
        //Ejecutar el calculo de la fecha
        $.ajax({
            url: "AjaxProceedings_start.aspx",
            type: "GET",
            data: { "action": "calculafechas", "fecha": $("#ctl00_cphPrincipal_Txtdatestartcontract").val(), "duracion": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_Txtdateendcontract").val(result);
                $("#ctl00_cphPrincipal_HFdend").val(result);
            },
            error: function() {
                $("#ctl00_cphPrincipal_Txtdurationcontract").val("");
            }
        });
    })
}

//funcion q ejecuta el menzaje
function guardar() {
    $("#ctl00_cphPrincipal_Btnexport").click(function() {
        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo").text("El acta de inicio se generó con éxito");
        $("#ctl00_cphPrincipal_containerSuccess_2").css("display", "block");
        $("#ctl00_cphPrincipal_Lblmsginfo_2").text("El acta de inicio se generó con éxito");
    });
}


function cambiartitle() {
    $("#ctl00_cphPrincipal_ddlTypeThird").change(function() {
        //Cambiar el tipo de actor en los labels
        $("#ctl00_cphPrincipal_Lblaporteect").text('Aporte del ' + $(this).val() + ' en Efectivo:');
        $("#ctl00_cphPrincipal_Lblaporteesp").text('Aporte del ' + $(this).val() + ' en Especie:');
    })
}

function cambiartitle2() {
    $("#ctl00_cphPrincipal_ddlTypeoF").change(function() {
        //Cambiar el tipo de actor en los labels
        $("#ctl00_cphPrincipal_Lbltitleprincipal").text('Información general del ' + $(this).val());
        $("#ctl00_cphPrincipal_Lblobjective").text('Objeto del  ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblsupervisor").text('Nombre del Supervisor(a) del ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblfechasuscript").text('Fecha de suscripción del ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblduration").text('Duración del ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblstartdate").text('Fecha de Inicio del ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblenddate").text('Fecha de Terminación del ' + $(this).val() + ' :');
        $("#ctl00_cphPrincipal_Lblvaluescont").text('Valor del ' + $(this).val() + ' en $:');
        $("#ctl00_cphPrincipal_Lblinformatiomeject").text('INFORMACIÓN DE LA EJECUCIÓN DEL ' + $(this).val());
    })
}


