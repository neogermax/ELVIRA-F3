
//cargar combo tipos de  proyecto
function Ctype_project() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_type_project" },
        success: function(result) {
            $("#ddltype_proyect").html(result);
            $("#ddltype_proyect").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de proyecto.");
        }
    });
}

//cargar combo tipos de  poblacion segun el proyecto seleccionado
function Cpopulation() {
    // $("#ddltype_proyect").change(function() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_population", "idpopulation": 1 }, //$(this).val()
        success: function(result) {
            $("#ddlPupulation").html(result);
            $("#ddlPupulation").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los datos de la poblacion.");
        }
    });
    //  });
}


function Cpopulation_view() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "Cpopulation_view", "ididea": ideditar },
        success: function(result) {

            $("#ddlPupulation").val(result);
            $("#ddlPupulation").trigger("liszt:updated");

        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });

}



//cargar combo tipos de contratos
function CtypeContract() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_typecontract" },
        success: function(result) {
            $("#ddlmodcontract").html(result);
            $("#ddlmodcontract").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de contrato.");
        }
    });
}

function Ctypcontract_view() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "Ctypcontract_view", "ididea": ideditar },
        success: function(result) {

            $("#ddlmodcontract").val(result);
            $("#ddlmodcontract").trigger("liszt:updated");

        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });

}


//funcion para cargar matriz principal edicion
function View_matriz_principal() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_matriz_principal", "ididea": idea_buscar },
        success: function(result) {

            //cargamos el div donde se generara la tabla actores
            $("#T_matrizcontainer").html("");
            $("#T_matrizcontainer").html(result);

            //reconstruimos la tabla con los datos
            $("#matriz").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });

            //llamar la funcion suma de primera columna efectivo
            sumavalores_gridprincipal();


        },
        error: function(msg) {
            alert("No se pueden cargar los actores de informacion principal de la idea = " + ideditar);
        }
    });
}





//funcion suma de primera columna efectivo
function sumavalores_gridprincipal() {

    var valdinergridprincipal = 0;
    var valespeciegridprincipal = 0;
    var valtotalgridprincipal = 0;
    var option_0 = 0;


    $("#matriz tr").slice(0, $("#matriz tr").length - 1).each(function() {
        var arrayValuesgridprincipal = $(this).find("td").slice(0, 5);
        //validamos si hay campos null en la tabla actores

        if ($(arrayValuesgridprincipal[0]).html() != null) {
            //validamos si es diferente a la FSC

            option_0 = 1;
            //capturamos e incrementamos los valores para la suma
            valdinergridprincipal = valdinergridprincipal + parseInt($(arrayValuesgridprincipal[2]).html().replace(/\./gi, ''));
            valespeciegridprincipal = valespeciegridprincipal + parseInt($(arrayValuesgridprincipal[3]).html().replace(/\./gi, ''));
            valtotalgridprincipal = valtotalgridprincipal + parseInt($(arrayValuesgridprincipal[4]).html().replace(/\./gi, ''));

            //validamos valores si vienen vacios
            if (isNaN(valdinergridprincipal)) {
                valdinergridprincipal = 0;
            }
            if (isNaN(valespeciegridprincipal)) {
                valespeciegridprincipal = 0;
            }
            if (isNaN(valtotalgridprincipal)) {
                valtotalgridprincipal = 0;
            }

            //cargamos los campos con la operacion realizada
            $("#valueMoneytotal").text(addCommasrefactor(valdinergridprincipal));
            $("#ValueEspeciestotal").text(addCommasrefactor(valespeciegridprincipal));
            $("#ValueCostotal").text(addCommasrefactor(valtotalgridprincipal));

            $("#ctl00_cphPrincipal_HDvalorpagoflujo").val(addCommasrefactor(valdinergridprincipal));
        }

        //validamos si la opcion escojida esta vacia y le asignamos 0
        if (option_0 == 0) {
            $("#valueMoneytotal").text(0);
            $("#ValueEspeciestotal").text(0);
            $("#ValueCostotal").text(0);
        }

    });
}




function validafecha() {

    $("#ctl00_cphPrincipal_Txtday").blur(function() {
        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            alert("El campo fecha de inicio debe estar diligenciado!");
            $("#ctl00_cphPrincipal_Txtday").val("");
            $("#ctl00_cphPrincipal_txtduration").val("");
            $("#ctl00_cphPrincipal_txtstartdate").focus();
        }
        else {
            //Ejecutar el calculo de la fecha
            $.ajax({
                url: "AjaxAddProject.aspx",
                type: "GET",
                data: { "action": "calculafechas",
                    "fecha": $("#ctl00_cphPrincipal_txtstartdate").val(),
                    "duracion": $("#ctl00_cphPrincipal_txtduration").val(),
                    "dias": $(this).val()
                },
                success: function(result) {
                    $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                    $("#ctl00_cphPrincipal_HFEndDate").val(result);
                    $("#ctl00_cphPrincipal_HFdate").val(result);
                },
                error: function() {
                    $("#ctl00_cphPrincipal_txtduration").val("");
                    $("#ctl00_cphPrincipal_Txtday").val("");
                }
            });

        }
    })
}

function validafecha2() {

    $("#ctl00_cphPrincipal_txtduration").blur(function() {
        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            alert("El campo fecha de inicio debe estar diligenciado!");
            $("#ctl00_cphPrincipal_Txtday").val("");
            $("#ctl00_cphPrincipal_txtduration").val("");
            $("#ctl00_cphPrincipal_txtstartdate").focus();
        }
        else {

            //Ejecutar el calculo de la fecha
            $.ajax({
                url: "AjaxAddProject.aspx",
                type: "GET",
                data: { "action": "calculafechas",
                    "fecha": $("#ctl00_cphPrincipal_txtstartdate").val(),
                    "duracion": $(this).val(),
                    "dias": $("#ctl00_cphPrincipal_Txtday").val()
                },
                success: function(result) {
                    $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                    $("#ctl00_cphPrincipal_HFEndDate").val(result);
                    $("#ctl00_cphPrincipal_HFdate").val(result);
                },
                error: function() {
                    $("#ctl00_cphPrincipal_txtduration").val("");
                    $("#ctl00_cphPrincipal_Txtday").val("");

                }
            });
        }
    })
}

//funcion que calcula la fecha final segun los campos meses y dias 
function startdate() {
    $("#ctl00_cphPrincipal_txtstartdate").blur(function() {
        if ($("#ctl00_cphPrincipal_txtstartdate").val() != "") {
            if ($("#ctl00_cphPrincipal_txtduration").val() == "" && $("#ctl00_cphPrincipal_Txtday").val() == "") {
                $("#ctl00_cphPrincipal_Txtdatecierre").val($("#ctl00_cphPrincipal_txtstartdate").val());
            }
            else {
                if ($("#ctl00_cphPrincipal_txtduration").val() != "") {
                    $.ajax({
                        url: "AjaxAddProject.aspx",
                        type: "GET",
                        data: { "action": "calculafechas", "fecha": $(this).val(), "duracion": $("#ctl00_cphPrincipal_txtduration").val(), "dias": $("#ctl00_cphPrincipal_Txtday").val() },
                        success: function(result) {
                            $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                            $("#ctl00_cphPrincipal_HFEndDate").val(result);
                            $("#ctl00_cphPrincipal_HFdate").val(result);
                        },
                        error: function() {
                            $("#ctl00_cphPrincipal_txtduration").val("");
                            $("#ctl00_cphPrincipal_Txtday").val("");

                        }
                    });
                }
                else {
                    $.ajax({
                        url: "AjaxAddProject.aspx",
                        type: "GET",
                        data: { "action": "calculafechas", "fecha": $(this).val(), "duracion": $("#ctl00_cphPrincipal_txtduration").val(), "dias": $("#ctl00_cphPrincipal_Txtday").val() },
                        success: function(result) {
                            $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                            $("#ctl00_cphPrincipal_HFEndDate").val(result);
                            $("#ctl00_cphPrincipal_HFdate").val(result);
                        },
                        error: function() {
                            $("#ctl00_cphPrincipal_txtduration").val("");
                            $("#ctl00_cphPrincipal_Txtday").val("");
                        }
                    });
                }
            }
        }

    })


}


function operacionesIdea() {

    //montaje de jquery para validar los campo meses
    //22-07-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_txtduration").blur(function() {
        var rev = $(this).val();
        var printer = rev.replace(/"."/gi, '');

        if (isNaN(printer)) {
            $(this).css("border", "2px solid red");
            alert("El campo diligenciado no puede tener valores texto solo numericos.");
            $(this).val("");
            $(this).focus();
        } else {
            $(this).css("border", "2px solid #DEDEDE");
        }
    });

    $("#ctl00_cphPrincipal_Txtday").blur(function() {
        var rev = $(this).val();
        var printer = rev.replace(/"."/gi, '');

        if (isNaN(printer)) {
            $(this).css("border", "2px solid red");
            alert("El campo diligenciado no puede tener valores texto solo numericos.");
            $(this).val("");
            $(this).focus();
        } else {
            $(this).css("border", "2px solid #DEDEDE");
        }
    });


    //montaje de jquery para LOS TRES CAMPOS DE RESULTADO
    //31-05-2013 GERMAN RODRIGUEZ

}


function cargar_ideas_aprobadas() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_ideas_aprobada" },
        success: function(result) {
            $("#ddlididea").html(result);
            $("#ddlididea").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de proyecto.");
        }
    });

}

function traer_datos_idea_inf_p() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",

        data: { "action": "getIdeaProject_inf_p", "id": idea_buscar },

        remove_linebreaks: true,
        success: function(result) {

            result = result.replace(/\n/g, "");
            result = result.replace(/\r/g, "");
            result = result.replace(/\t/g, "");
            result = result.replace(/\n\r/g, " ");
            result = result.replace(/\r\n/g, " ");


            result = JSON.parse(result);

            //campos de texto planos
            $("#ctl00_cphPrincipal_txtjustification").val(result.Justification);
            $("#ctl00_cphPrincipal_txtobjective").val(result.Objective);
            $("#ctl00_cphPrincipal_txtareadescription").val(result.AreaDescription);
            $("#ctl00_cphPrincipal_txtresults").val(result.Results);
            $("#ctl00_cphPrincipal_txtresulgc").val(result.ResultsKnowledgeManagement);
            $("#ctl00_cphPrincipal_txtresulci").val(result.ResultsInstalledCapacity);
            $("#ctl00_cphPrincipal_Txtothersresults").html(result.OtherResults);
            $("#ctl00_cphPrincipal_Txtobligationsoftheparties").html(result.obligationsoftheparties);
            $("#ctl00_cphPrincipal_Txtroutepresupuestal").val(result.BudgetRoute);
            $("#ctl00_cphPrincipal_Txtriesgos").val(result.RisksIdentified);
            $("#ctl00_cphPrincipal_Txtaccionmitig").val(result.RiskMitigation);
            $("#ctl00_cphPrincipal_txtstartdate").val(result.StartDate);
            $("#ctl00_cphPrincipal_txtduration").val(result.Duration);
            $("#ctl00_cphPrincipal_Txtday").val(result.days);

            //carga de combos de pestaña descripcion proyecto

            $("#ddlmodcontract").val(result.Idtypecontract);
            $("#ddlmodcontract").trigger("liszt:updated");

            $("#ddlPupulation").val(result.Population);
            $("#ddlPupulation").trigger("liszt:updated");

            //cargamos el control de aplica iva
            $("#ctl00_cphPrincipal_RBnList_iva :radio[value='" + result.ideaappliesIVA + "']").attr('checked', true);
            valor_iva = result.ideaappliesIVA;
            //calcula la fecha de finalizacion
            var timer = setTimeout("fix();", 2000);

        },

        error: function()
        { alert("No se pueden cargar los datos de la idea solicitada."); }
    });

}



//funcion para exportar los terminos de referencia
function Export_onclick() {

    var listubicaciones = [];
    var listactores = [];

    //recorer array para el ingreso de ubicaciones
    for (item in arrayUbicacion) {
        listubicaciones.push(JSON.stringify(arrayUbicacion[item]));
    }
    //recorer array para el ingreso de actores
    for (item in arrayActor) {
        listactores.push(JSON.stringify(arrayActor[item]));
    }

    //capturamos la url
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    //validamos si creamos la idea o editamos
    if (sURLVariables[0] == "op=edit") {
        ideditar = sURLVariables[1];
        // alert(ideditar);
        var str_url = "addProject.aspx?op=export&" + ideditar;
        // alert(str_url);
    }
    else {
        var str_url = "addProject.aspx?op=export";

    }

    //  $("#linkExport").attr({ href: str_url });
    $("#Export").attr("href", str_url);
}

function Ctypeaproval() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_type_aproval", "type": "P" },
        success: function(result) {
            $("#dll_estado").html(result);
            $("#dll_estado").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los estados de la idea.");
        }
    });

}



function validarcampos_fecha_madre() {

    $("#ctl00_cphPrincipal_txtstartdate").blur(function() {

        var fecha_ing = new Date($("#ctl00_cphPrincipal_txtstartdate").val());

        if (fecha_limite_madre < fecha_ing || fecha_ing < fecha_inicial_madre) {

            $("#ctl00_cphPrincipal_txtstartdate").focus();
            $("#ctl00_cphPrincipal_lblHelpstartdate").text("La fecha no debe superar los rango de fechas del proyecto madre");

        }
    })

    $("#ctl00_cphPrincipal_txtduration").change(function() {

        var fecha_ing = new Date($("#ctl00_cphPrincipal_Txtdatecierre").val());
        if (fecha_limite_madre < fecha_ing) {

            $("#ctl00_cphPrincipal_txtduration").val("");
            $("#ctl00_cphPrincipal_txtduration").focus();
            $("#ctl00_cphPrincipal_Lblhelpduraton").text("La fecha de cierre no debe superar la fecha de cierre del proyecto madre");
        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpduraton").text("");
        }

    })

    $("#ctl00_cphPrincipal_Txtday").change(function() {

    var fecha_ing = new Date($("#ctl00_cphPrincipal_Txtdatecierre").val());
    
    if (fecha_limite_madre < fecha_ing) {

        $("#ctl00_cphPrincipal_Txtday").val("");
        $("#ctl00_cphPrincipal_Txtday").focus();
        $("#ctl00_cphPrincipal_Lblhelpday").text("La fecha de cierre no debe superar la fecha de cierre del proyecto madre");
    }
    else {
        $("#ctl00_cphPrincipal_Lblhelpday").text("");
    }


    })
}