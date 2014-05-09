$(document).ready(function() {

    $("#ctl00_cphPrincipal_DivConfirm").css("display", "none");
    foto();
    validar();
    fecha();
    //personas();
    validacontrato();
    arreglo();
    buscaractores();
    loadproject();
    polizas();
    guardarproyecto();
    setTimeout("tabs();", 1000);
    //cargar();

})

//Funcion que muestra el div de confirmación al momento de finalizar contratación
function polizas() {

    $("#ctl00_cphPrincipal_btnFinishContract").click(function() {
        $("#ctl00_cphPrincipal_DivConfirm").css("display", "block");
        $("#ctl00_cphPrincipal_lblConfirmation").text("Revise la información ingresada, una vez presione el botón confirmar, esta NO podrá ser modificada.");
    });

    //Funcion que chequea el control "Requiere poliza" al cargar la pagina

    if ($("#ctl00_cphPrincipal_HFPolRequired").val() == 1) {
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_PolizaRequired").prop('checked', true);
    }

}

function personas() {
    //Validar el tipo de contratista
    $("input[name='ctl00$cphPrincipal$TabContainer1$TabPanel2$tipopersona']").change(function() {

        //Limpiar los controles
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtNit").val("");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").val("");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").val("");

        if ($("#ctl00_cphPrincipal_TabContainer1_TabPanel2_tipopersona_0").is(":checked")) {
            //Ocultar Controles
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").css("display", "none");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtContractorName").css("display", "none");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").css("display", "none");
            //Ocultar Labels
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label5").css("display", "none");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label6").css("display", "none");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label7").css("display", "none");
            $.ajax({
                url: "ajaxcontracrequest.aspx",
                type: "GET",
                data: { "action": "loadactors", "tipopersona": "1", "id": $("#ctl00_cphPrincipal_TabContainer1_TabPanel4_ddlactors").val() },
                success: function(result) {
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").html(result);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").trigger("liszt:updated");
                },
                error: function()
                { alert("Los datos de terceros no pudieron ser cargados."); }
            });

        }
        else {
            //Mostrar Controles
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").css("display", "block");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtContractorName").css("display", "block");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").css("display", "block");
            //Mostrar Labels
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label5").css("display", "block");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label6").css("display", "block");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label7").css("display", "block");
            //funcion para poblar el combo 
            $.ajax({
                url: "ajaxcontracrequest.aspx",
                type: "GET",
                data: { "action": "loadactors", "tipopersona": "0", "id": $("#ctl00_cphPrincipal_TabContainer1_TabPanel4_ddlactors").val() },
                success: function(result) {
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").html(result);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").trigger("liszt:updated");
                },
                error: function()
                { alert("Los datos de terceros no pudieron ser cargados."); }
            });

        }
    });

    //Traer los datos del contratista
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").change(function() {
        $.ajax({
            url: "ajaxcontracrequest.aspx",
            type: "GET",
            data: { "action": "buscar", "id": $(this).val() },
            success: function(result) {
                result = JSON.parse(result);

                $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtNit").val(result.nit);
                $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").val(result.replegal);
                $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").val(result.documents);
                $("#ctl00_cphPrincipal_HFtextactor").val(result.name);

            },
            error: function()
            { alert("Los datos de terceros no pudieron ser cargados."); }
        });


    });

}

function foto() {
    $("a.pretty").prettyPhoto({
        callback: function() {
            $.ajax({
                url: "/ResearchAndDevelopment/ajaxaddidea_drop_list_third.aspx",
                type: "GET",
                data: { "action": "loadthirdcontract", "id": $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").val(), "type": $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_tipopersona_0").is(":checked") },
                success: function(result) {
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").html(result);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_ddlActor").trigger("liszt:updated");
                },
                error: function()
                { alert("Los datos de terceros no pudieron ser cargados."); }
            });
        }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        modal: true,
        social_tools: false
    });

}
function validar() {

    if ($("#ctl00_cphPrincipal_TabContainer1_TabPanel2_tipopersona_0").is(":checked")) {
        //Ocultar Controles
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").css("display", "none");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtContractorName").css("display", "none");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").css("display", "none");
        //Ocultar Labels
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label5").css("display", "none");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label6").css("display", "none");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label7").css("display", "none");
    }
    else {
        //Mostrar Controles
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtLegalRepresentative").css("display", "block");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtContractorName").css("display", "block");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_txtIdentificationNumber").css("display", "block");
        //Mostrar Labels
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label5").css("display", "block");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label6").css("display", "block");
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel2_Label7").css("display", "block");
    }
}

function fecha() {

    //Validacion fechas contrato
    $("#ctl00_cphPrincipal_txtInitialDate").change(function() {
        if ($("#ctl00_cphPrincipal_txtSubscriptionDate").val() == '') {
            $("#ctl00_cphPrincipal_lblinformation").css("color", "red");
            $("#ctl00_cphPrincipal_blinformation").text("La fecha de suscripción no puede estar vacía.");
        }
        else {
            $("#ctl00_cphPrincipal_lblinformation").text("");
            var fechasuscrip = $("#ctl00_cphPrincipal_txtSubscriptionDate").val();
            var fechaini = $("#ctl00_cphPrincipal_txtInitialDate").val();

            if (fechasuscrip > fechaini) {
                $("#ctl00_cphPrincipal_lblinformation").css("color", "red");
                $("#ctl00_cphPrincipal_lblinformation").text("La fecha de inicio no puede ser anterior a la fecha de suscripción.");
                $("#ctl00_cphPrincipal_txtInitialDate").val("");
            }
            else {
                $("#ctl00_cphPrincipal_lblinformation").text("");
            }
        }
    })

    $("#ctl00_cphPrincipal_txtContractDays").change(function() {
        var dias = $("#ctl00_cphPrincipal_txtContractDays").val();
        if (/^([\d])+$/.test(dias)) {
            $("#ctl00_cphPrincipal_lblNfoContractDays").text("");
        } else {
            $("#ctl00_cphPrincipal_lblNfoContractDays").css("color","red");
            $("#ctl00_cphPrincipal_lblNfoContractDays").text("El valor de los días debe ser numérico.");
            $("#ctl00_cphPrincipal_txtContractDays").val("");
        }
    })

    //Validacion fechas contrato
    $("#ctl00_cphPrincipal_txtLiquidationDate").change(function() {
        if ($("#ctl00_cphPrincipal_txtEndingDate").val() == '') {
            $("#ctl00_cphPrincipal_lblNfoEndingdate").css("color", "red");
            $("#ctl00_cphPrincipal_lblNfoEndingdate").text("La fecha de cierre no se ha diligenciado.");
            $("#ctl00_cphPrincipal_txtLiquidationDate").val("");
        }
        else {
            $("#ctl00_cphPrincipal_lblNfoEndingdate").text("");
            var fechacierre = $("#ctl00_cphPrincipal_txtEndingDate").val();
            var fechaliquida = $("#ctl00_cphPrincipal_txtLiquidationDate").val();

            if (fechacierre > fechaliquida) {
                $("#ctl00_cphPrincipal_lblNfoEndingdate").css("color", "red");
                $("#ctl00_cphPrincipal_lblNfoEndingdate").text("La fecha de cierre no puede ser anterior a la fecha de liquidación.");
                $("#ctl00_cphPrincipal_txtLiquidationDate").val("");
            }
            else {
                $("#ctl00_cphPrincipal_lblNfoEndingdate").text("");
            }
        }
    })

    //Limpiar campos fechas de contrato
    $("#ctl00_cphPrincipal_txtSubscriptionDate").change(function() {
        $("#ctl00_cphPrincipal_txtInitialDate").val("");
    })

    //Limpiar campos fechas de poliza
    $("#ctl00_cphPrincipal_txtExpeditionDate").change(function() {
        $("#ctl00_cphPrincipal_txtFinishdate").val("");
    })

    //Validacion fechas conceptos de poliza
    $("#ctl00_cphPrincipal_txtFinishDatePoliza").change(function() {
        if ($("#ctl00_cphPrincipal_txtInitDatePoliza").val() != "" && $("#ctl00_cphPrincipal_txtFinishDatePoliza").val() != "") {
            if ($("#ctl00_cphPrincipal_txtInitDatePoliza").val() > $("#ctl00_cphPrincipal_txtFinishDatePoliza").val()) {
                $("#ctl00_cphPrincipal_lblAddPolizaNfo").css("color", "red");
                $("#ctl00_cphPrincipal_lblAddPolizaNfo").text("La fecha de fin de vigencia no debe se inferior a la fecha de inicio de la vigencia.");
            } else {
            $("#ctl00_cphPrincipal_lblAddPolizaNfo").text("");
            }
        }
    })

    $("#ctl00_cphPrincipal_txtInitDatePoliza").change(function() {
        $("#ctl00_cphPrincipal_txtFinishDatePoliza").trigger("change");
    })

    //Validacion fechas poliza
    $("#ctl00_cphPrincipal_txtFinishdate").change(function() {
        if ($("#ctl00_cphPrincipal_txtExpeditionDate").val() == '') {
            $("#ctl00_cphPrincipal_lblInfo2").css("color", "red");
            $("#ctl00_cphPrincipal_lblInfo2").text("La fecha de expedición no puede estar vacía.");
            $("#ctl00_cphPrincipal_txtContractDuration").val("");
        }
        else {
            $("#ctl00_cphPrincipal_lblInfo2").text("");
            var fechasuscrip = $("#ctl00_cphPrincipal_txtExpeditionDate").val();
            var fechaini = $("#ctl00_cphPrincipal_txtFinishdate").val();

            if (fechasuscrip > fechaini) {
                $("#ctl00_cphPrincipal_lblInfo2").css("color", "red");
                $("#ctl00_cphPrincipal_lblInfo2").text("La fecha de vencimiento de la poliza no puede ser anterior a la fecha de expedición.");
                $("#ctl00_cphPrincipal_txtFinishdate").val("");
                $("#ctl00_cphPrincipal_txtContractDuration").val("");
            }
            else {
                $("#ctl00_cphPrincipal_lblInfo2").text("");
            }
        }
    })

    $("#ctl00_cphPrincipal_txtContractDuration").change(function() {

        //Verificar que el numero cumpla con las caracteristicas
        var validacion = /(\d+)(((.|,)\d+)+)?/

        if ($("#ctl00_cphPrincipal_txtContractDuration").val().search(validacion) == -1) {
            $("#ctl00_cphPrincipal_lblNfoContractDuration").css("color", "red");
            $("#ctl00_cphPrincipal_lblNfoContractDuration").text("El valor ingresado en la duración no es correcto.");
            $("#ctl00_cphPrincipal_txtContractDuration").val("");
            $("#ctl00_cphPrincipal_txtContractDuration").focus();
        }
        else {
            $("#ctl00_cphPrincipal_lblNfoContractDuration").text("");
        }

        //Ejecutar el calculo de la fecha
        $.ajax({
            url: "ajaxcontracrequest.aspx",
            type: "GET",
            data: { "action": "calculafechas", "fecha": $("#ctl00_cphPrincipal_txtInitialDate").val(), "duracion": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_txtEndingDate").val(result);
                $("#ctl00_cphPrincipal_HFEndDate").val(result);
            },
            error: function() { //alert("Hay un error con el calculo de las fechas"); 
                $("#ctl00_cphPrincipal_txtContractDuration").val("");
            }
        });
    })

    //Verificar si es edicion
    if ($.trim($("#ctl00_cphPrincipal_txtContractDuration").val()).length > 0) {
        //Fix
        $("#ctl00_cphPrincipal_txtContractDuration").trigger("change");
    }

}



//funcion para traer los terceros de la idea a aprobar
function buscaractores() {

    //Fix


    $("#ctl00_cphPrincipal_ddlProject").trigger("change");

    $("#ctl00_cphPrincipal_ddlProject").change(function() {
        var proyecto = document.getElementById("ctl00_cphPrincipal_ddlProject");
        var selproyecto = proyecto.options[proyecto.selectedIndex].text;
        $("#ctl00_cphPrincipal_lblProjectNumber").text(selproyecto)
        $.ajax({
            url: "/FormulationAndAdoption/ajaxaddProjectApprovalRecordshearch.aspx",
            type: "GET",
            data: { "action": "buscaractorescontrato", "code": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_GVTHIRD").html(result);
            },
            error: function() {
                $("#ctl00_cphPrincipal_lblNfoContractDuration").text("No se pueden cargar los actores de la idea solicitada.");
            }
        });
    });
}

function guardarproyecto() {
    $("#ctl00_cphPrincipal_ddlProject").change(function() {
        loadproject($("#ctl00_cphPrincipal_ddlProject").val());
        var idproject = $("#ctl00_cphPrincipal_ddlProject").val();
        $("#ctl00_cphPrincipal_HF_ID_Project").val(idproject);
    });
}

//-Traer datos de proyecto-//
function loadproject(proyecto) {

    //Obtener fechas de proyecto//
    $.ajax({
        url: "ajaxcontracrequest.aspx",
        type: "GET",
        data: { "action": "getproject", "proyectid": $("#ctl00_cphPrincipal_ddlProject").val(), "columna": "BeginDate" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                $("#ctl00_cphPrincipal_txtInitialDate").val(result);
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del proyecto.");
        }
    });

    //Obtener duracion de proyecto//
    $.ajax({
        url: "ajaxcontracrequest.aspx",
        type: "GET",
        data: { "action": "getproject", "proyectid": $("#ctl00_cphPrincipal_ddlProject").val(), "columna": "duration" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                $("#ctl00_cphPrincipal_txtContractDuration").val(result);
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del proyecto.");
        }
    });

    //Obtener finalizacion de proyecto//
    $.ajax({
        url: "ajaxcontracrequest.aspx",
        type: "GET",
        data: { "action": "getproject", "proyectid": $("#ctl00_cphPrincipal_ddlProject").val(), "columna": "completiondate" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                $("#ctl00_cphPrincipal_txtEndingDate").val(result);
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del proyecto.");
        }
    });

}

function arreglo() {
    $("#ctl00_cphPrincipal_btnAddData").click(function() {
        fecha();
    })

    $("#ctl00_cphPrincipal_ddlContractNature").change(function() {
        $("#ctl00_cphPrincipal_txtcontractnumberadjusted").trigger("blur");
        validacontrato();
    })

}

function tabs() {
    if ($("#ctl00_cphPrincipal_HFActivetab").val() != 0) {
        $("#tabs").tabs("option", "active", $("#ctl00_cphPrincipal_HFActivetab").val());
    }
}

function validacontrato() {
    $("#ctl00_cphPrincipal_txtcontractnumberadjusted").blur(function() {
        $.ajax({
            url: "ajaxcontracrequest.aspx",
            type: "GET",
            data: { "action": "validarcontrato", "contrato": $(this).val() },
            success: function(result) {

                if (result == "OK" && $("#ctl00_cphPrincipal_ddlContractNature").val() != 5) {
                    $("#ctl00_cphPrincipal_lblHelpcontractnumberadjusted").css("color", "red");
                    $("#ctl00_cphPrincipal_lblHelpcontractnumberadjusted").text("El contrato diligenciado ya se encuentra asignado.");
                    $("#ctl00_cphPrincipal_txtcontractnumberadjusted").val("");
                    $("#ctl00_cphPrincipal_txtcontractnumberadjusted").focus();
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelpcontractnumberadjusted").text("");
                }
            },
            error: function() {
                alert("Ocurrio un error al validar el número del contrato");
            }
        });
    })

};

//-Extraer parametros QueryString-//
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}