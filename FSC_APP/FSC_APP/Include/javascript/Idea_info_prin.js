
//cargar combo tipos de  proyecto
//function Ctype_project() {
//    $.ajax({
//        url: "AjaxAddIdea.aspx",
//        type: "GET",
//        data: { "action": "C_type_project" },
//        success: function(result) {
//            $("#ddltype_proyect").html(result);
//            $("#ddltype_proyect").trigger("liszt:updated");
//        },
//        error: function(msg) {
//            alert("No se pueden cargar los tipos de proyecto.");
//        }
//    });
//}

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
                        url: "AjaxAddIdea.aspx",
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
        var str_url = "addIdea.aspx?op=export&" + ideditar;
        // alert(str_url);
    }
    else {
        var str_url = "addIdea.aspx?op=export";

    }

    //  $("#linkExport").attr({ href: str_url });
    $("#Export").attr("href", str_url);
}

function Ctypeaproval() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_type_aproval", "type": "I" },
        success: function(result) {
            $("#dll_estado").html(result);
            $("#dll_estado").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los estados de la idea.");
        }
    });

}
