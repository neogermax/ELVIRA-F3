
//cargar combo de lineas estrategicas
function ClineEstrategic() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_linestrategic" },
        success: function(result) {
            $("#ddlStrategicLines").html(result);
            $("#ddlStrategicLines").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar las lineas strategicas.");
        }
    });
}

//cargar combo de programas
function Cprogram() {
    $("#ddlStrategicLines").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_program", "idlinestrategic": $(this).val() },
            success: function(result) {
                $("#ddlPrograms").html(result);
                $("#ddlPrograms").trigger("liszt:updated");
                $("#componentesseleccionados").html("");
            },
            error: function(msg) {
                alert("No se pueden cargar los programas de la linea estrategica selecionada.");
            }
        });
    });
}


//cargar combo tipos de  proyecto
function Ctype_project() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
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
        url: "AjaxAddIdea.aspx",
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

//cargar combo tipos de contratos
function CtypeContract() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
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


//funcion que posiciona el combo en la linea estrategica de la idea seleccionada
function ClineEstrategic_edit() {
    //ajax que posiciona la linea estrategica de la idea conasultada
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "View_line_strategic", "ididea": ideditar },
        success: function(result) {

            $("#ddlStrategicLines").val(result);
            $("#ddlStrategicLines").trigger("liszt:updated");

            edit_line_strategic = result;

        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });

    var timer_cline_edit = setTimeout("Cprogram_edit();", 2000);

}

//cargar los programas seleccionados de la linea seleccionada anteriormente "ClineEstrategic_edit()"
function Cprogram_edit() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_program", "idlinestrategic": edit_line_strategic },
        success: function(result) {
            $("#ddlPrograms").html(result);
            $("#ddlPrograms").trigger("liszt:updated");
            $("#componentesseleccionados").html("");
        },
        error: function(msg) {
            alert("No se pueden cargar los programas de la linea estrategica selecionada.");
        }
    });
    var timer_program_edit = setTimeout("view_Cprogram();", 2000);
}

//funcion que posiciona el combo del programa de la idea seleccionada 
function view_Cprogram() {
    //ajax que posiciona el programa de la idea conasultada
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "View_program", "ididea": ideditar },
        success: function(result) {

            $("#ddlPrograms").val(result);
            $("#ddlPrograms").trigger("liszt:updated");

            edit_program = result;

        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });
    var timer_program_edit = setTimeout("edit_component();", 2000);
    var timer_program_edit = setTimeout("edit_component_view();", 2000);

}


//funcion para cargar matriz principal edicion
function View_matriz_principal() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "View_matriz_principal", "ididea": ideditar },
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
                url: "AjaxAddIdea.aspx",
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
                url: "AjaxAddIdea.aspx",
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
                        url: "AjaxAddIdea.aspx",
                        type: "GET",
                        data: { "action": "calculafechas",
                            "fecha": $(this).val(),
                            "duracion": $("#ctl00_cphPrincipal_txtduration").val(),
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
                else {
                    $.ajax({
                        url: "AjaxAddIdea.aspx",
                        type: "GET",
                        data: { "action": "calculafechas",
                            "fecha": $(this).val(),
                            "duracion": $("#ctl00_cphPrincipal_txtduration").val(),
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


//montaje de jquery para recorrer el campo par montarle comas para los miles
//22-07-2013 GERMAN RODRIGUEZ

function addCommas(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    $("#ctl00_cphPrincipal_Txtaportfscocomp").val(output);
}

function addCommas2(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    $("#ctl00_cphPrincipal_ValueCostFSC").val(output);
}

//fucion para añadir los miles a los numeros refactorizada
function addCommasrefactor(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    return output;

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


    var str_url = "addIdea.aspx?op=export";

    //json de url
    var url_export = {

        "linea_estrategica": $("#ddlStrategicLines option:selected").text(),
        "programa": $("#ddlPrograms option:selected").text(),
        "Población": $("#ddlPupulation option:selected").text(),
        "contratacion": $("#ddlmodcontract option:selected").text(),
        "listubicaciones": listubicaciones.toString(),
        "listactores": listactores.toString()
    }

    str_url += "&linea_estrategica=" + url_export['linea_estrategica'];
    str_url += "&programa=" + url_export['programa'];
    str_url += "&Población=" + url_export['Población'];
    str_url += "&contratacion=" + url_export['contratacion'];
    str_url += "&listubicaciones=" + url_export['listubicaciones'];
    str_url += "&listactores=" + url_export['listactores'];


    //  $("#linkExport").attr({ href: str_url });
    $("#Export").attr("href", str_url);
}



