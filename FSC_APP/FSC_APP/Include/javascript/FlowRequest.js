//TODO: Juan Camilo Martinez Morales
//16/05/2014
//Document: FlowRequest.js
//agregar pagos al grid general de flujos
var arrayValorflujoTotal = [];
var entradaflujos = 0;
var arrayflujosdepago = [];
var edit_flujo_inicializa = 0;
var edit_swhich_fx = 0;
var swhich_flujos_exist = 0;
var swhich_validar_estado_1 = 0;
var matriz_flujos = [];
var reversedesembolsos = [];
var switch_editar = 0;

function Btn_add_flujo_onclick() {

    if ($("#ctl00_cphPrincipal_txtporcentaje").val() == "" || $("#ctl00_cphPrincipal_txtvalortotalflow").val() == "" || $("#ctl00_cphPrincipal_txtfechapago").val() == "" || $("#ctl00_cphPrincipal_txtentregable").val() == "") {
        $("#ctl00_cphPrincipal_Lblinformationexist").text("Estos campos deben ser diligenciados antes de ingresar algún valor en la grilla de flujos!");

    } else {


        //inicializamos variables
        var valuecomparative = $("#ctl00_cphPrincipal_Lbltotalvalor").text();
        //quitamos el valor decimal
        //var arrseparar = valuecomparative.split('.');
        valuecomparative = valuecomparative.replace(/\./gi, '');
        //alert(valuecomparative);
        var sumapagos = $("#totalflujos").text();
        sumapagos = sumapagos.replace(/\./gi, '');
        var opesumagos = sumapagos;
        //validamos si el valor a guardar concorda con el porcentaje deseado
        if (valuecomparative == sumapagos) {

            //capturamos campos para guardar en el grid flujos
            var tflujos = $("#totalflujos").text();
            tflujos = tflujos.replace(/\./gi, '');
            var N_pago = $("#ctl00_cphPrincipal_txtvalortotalflow").val();
            var fecha_pago = $("#ctl00_cphPrincipal_txtfechapago").val();
            var porcentaje = $("#ctl00_cphPrincipal_txtporcentaje").val() + " %";
            //            var valor_pago = valuecomparative;

            var entrega = cambio_text_flujos($("#ctl00_cphPrincipal_txtentregable").val());
            var entregas_sin = $("#ctl00_cphPrincipal_txtentregable").val();

            var idpago;
            var Aportante;
            var desembolso;
            var idaportante;

            //creamos json para guardarlos en un array
            var jsonflujo = {
                "N_pagos": N_pago,
                "fecha": fecha_pago,
                "porcentaje": porcentaje,
                "entregable": entrega,
                "valortotal": tflujos
            };


            //recorremos el array para revisar repetidos        
            var validerepetido = 0;
            for (iArray in arrayflujosdepago) {
                if (N_pago == arrayflujosdepago[iArray].N_pagos) {
                    validerepetido = 1;
                }
            }


            //validamos si hay repetidos
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_Lblinformation_flujos").text("El pago No " + N_pago + " ya fue registrado");
            } else {
                $("#ctl00_cphPrincipal_Lblinformation_flujos").text("");

                //inicializamos las variables
                var validaporcentaje = 0;

                //capturamos los porcentaje registrados
                validaporcentaje = $("#porcentaje").text();
                validaporcentaje = validaporcentaje.replace('%', '');


                //capturamos el porcentaje a ingresar
                var anticipaporcentaje = $("#ctl00_cphPrincipal_txtporcentaje").val();
                //sumamos los valores
                validaporcentaje = parseInt(validaporcentaje) + parseInt(anticipaporcentaje);

                //validamos que no ingresen mas del 100% del valor ingresado
                if (validaporcentaje > 100) {
                    $("#ctl00_cphPrincipal_Lblinformation_flujos").text("Los desembolsos no deben superar el 100%");
                } else {
                    $("#ctl00_cphPrincipal_Lblinformationexist").text("");
                    edit_swhich_fx = 0;
                    //cargamos el array con el json
                    arrayflujosdepago.push(jsonflujo);

                    //Se actualiza el formato de la tabla T_Flujos
                    refreshTablePaymentFlow();

                    swhich_flujos_exist = 1;
                    
                }


                var contadormatriz = 0;
                //recorremos la tabla de flujo de pagos para guardar los detalles
                $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

                    arrayinputflujos = $(this).find("td").slice(0, 3);

                    if ($(arrayinputflujos[0]).html() != null) {


                        idpago = $("#ctl00_cphPrincipal_txtvalortotalflow").val();
                        idaportante = $(arrayinputflujos[0]).html();
                        Aportante = $(arrayinputflujos[1]).html();
                        var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();

                        desembolso = $(idflujo).val();
                        var jsonflujodetalle = {
                            "idpago": idpago,
                            "idaportante": idaportante,
                            "Aportante": Aportante,
                            "desembolso": desembolso
                        };

                        //cargamos el array con el json
                        matriz_flujos.push(jsonflujodetalle);

                    }
                });

                //Limpia valores de tabla flujo actores
                $(".money").each(function () {
                    $(this).val("");
                });


                //limpiamos campos
                $("#totalflujos").text("0");
                $("#ctl00_cphPrincipal_txtvalortotalflow").val("");
                $("#ctl00_cphPrincipal_txtfechapago").val("");
                $("#ctl00_cphPrincipal_txtporcentaje").val("");
                $("#ctl00_cphPrincipal_Lbltotalvalor").text("");
                $("#ctl00_cphPrincipal_txtentregable").val("");

                //lamar funcion para la suma de los subtotales de flujos de pagos
                sumarflujospagos();
                //validamos si los desembolsos son del 100%
                if (validaporcentaje == 100) {
                    $("#Btn_add_flujo").attr("disabled", "disabled");
                }
            }

        } else {

            // $(idflujo).val("");

            if (parseInt(valuecomparative) < parseInt(opesumagos)) {
                alert("el valor no debe ser mayor que el porcentaje aprobado");

                swhich_validar_estado_1 = 1;
                arrayValorflujoTotal[0] = 0;
                $("#totalflujos").text(0);

                sumarvaloresflujosprincipal();

            } else {
                alert("el valor total de los actores debe ser igual al porcentaje calculado");

                //inicializamos el array
                arrayValorflujoTotal[0] = 0;
                $("#totalflujos").text(0);
                sumarvaloresflujosprincipal();

            }

        }
    }
}

function refreshTablePaymentFlow() {
    //creamos la tabla de flujo de pagos      
    var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead>";
    htmlTableflujos += "<tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th>";
    htmlTableflujos += "<th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>";
    htmlTableflujos += "Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";

    for (itemArray in arrayflujosdepago) {

        var valorTotalMask = arrayflujosdepago[itemArray].valortotal;

        valorTotalMask = addThousandChar(valorTotalMask);
        
        htmlTableflujos += "<tr id='flow" + parseIntNull($.trim(arrayflujosdepago[itemArray].N_pagos)) + "' ><td>" + parseIntNull($.trim(arrayflujosdepago[itemArray].N_pagos)) + "</td><td>";
        htmlTableflujos += arrayflujosdepago[itemArray].fecha + "</td><td>" + parseInt(arrayflujosdepago[itemArray].porcentaje) + "</td><td>" + arrayflujosdepago[itemArray].entregable;
        htmlTableflujos += "</td><td>" + valorTotalMask + "</td><td><input type ='button' value= 'Editar'";
        htmlTableflujos += " class=\"btn\" style='background-image: none;' onclick=\"editflujo(" + itemArray + ")\" >";
        htmlTableflujos += "</input><input type ='button' class=\"btn btn-warning\" style='background-image: none;' value= 'Eliminar' onclick=\" eliminarflujo('" + arrayflujosdepago[itemArray].N_pagos + "')\"></input>";
        htmlTableflujos += "</td><td><input type ='button' value= 'Detalle' onclick=\"traerdetalles('" + $.trim(arrayflujosdepago[itemArray].N_pagos) + "',this)\"></input></td></tr>";
    }
    htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td>";
    htmlTableflujos += "<td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

    htmlTableflujos += "</tbody></table>";
    
    $("#T_flujosContainer").html("");
    $("#T_flujosContainer").html(htmlTableflujos);
    
    $("#T_flujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}

function cambio_text_flujos(str_txt) {

    str_txt = str_txt.replace(/,/g, '¬');
    str_txt = str_txt.replace(/\n/g, ' ');
    str_txt = str_txt.replace(/\r/g, '');
    str_txt = str_txt.replace(/\t/g, '');
    str_txt = str_txt.replace(/\n\r/g, ' ');
    str_txt = str_txt.replace(/\r\n/g, ' ');
    str_txt = str_txt.replace(/\"/g, '\"');

    return (str_txt);
}

function loadPaymentFlowsByProject() {
    $.ajax({
        url: "AjaxRequest.aspx",
        type: "POST",
        data: {
            "action": "loadFlowProject",
            "idProject": idproject
        },
        success: function (result) {
            var resultJson = JSON.parse(result.toString());
            arrayflujosdepago = JSON.parse(resultJson.toString());
            refreshTablePaymentFlow();
            refreshTableFlow();
            sumarflujospagos();
        },
        error: function (msg) {
            //
        }
    });
}

function loadPaymentDetailsFlowsByProject() {
    $.ajax({
        url: "AjaxRequest.aspx",
        type: "POST",
        data: {
            "action": "loadDetailsFlowsProject",
            "idProject": idproject
        },
        success: function (result) {
            var resultJson = JSON.parse(result.toString());
            matriz_flujos = JSON.parse(resultJson.toString());
        },
        error: function (msg) {
            //
        }
    });
}


function View_flujos_p_array() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: {
            "action": "View_flujos_p_array",
            "ididea": ideditar
        },
        success: function (result) {

            if (result == "vacio") {
                //    alert(result + " flujo actores");
                arrayflujosdepago = [];
                swhich_flujos_exist = 0;

            } else {
                arrayflujosdepago_ed = result.split("|");

                for (itemArray in arrayflujosdepago_ed) {

                    var recibeact = JSON.parse(arrayflujosdepago_ed[itemArray]);
                    arrayflujosdepago.push(recibeact);
                }
                swhich_flujos_exist = 1;
            }
        },
        error: function (msg) {
            alert("No se pueden cargar los flujos de pago de la idea = " + ideditar);
        }
    });

}


function View_flujos_actors() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: {
            "action": "View_flujos_actors",
            "ididea": ideditar
        },
        success: function (result) {

            //cargamos el div donde se generara la tabla actores
            $("#T_AflujosContainer").html("");
            $("#T_AflujosContainer").html(result);

            //reconstruimos la tabla con los datos
            $("#T_Actorsflujos").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });

            //lamar la funcionsumar actores
            sumar_grid_actores();

        },
        error: function (msg) {
            alert("No se pueden cargar los actores de flujos de pago de la idea = " + ideditar);
        }
    });

}

//funtion crear array de flujos de pagos
function View_flujos_actors_array() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: {
            "action": "View_flujos_actors_array",
            "ididea": ideditar
        },
        success: function (result) {

            if (result == "vacio") {
                //    alert(result);
                arrayActorFlujo = [];

            } else {
                arrayactorflujo_ed = result.split("|");

                // alert(arrayactorflujo_ed);
                for (itemArray in arrayactorflujo_ed) {

                    var recibeact = JSON.parse(arrayactorflujo_ed[itemArray]);
                    // alert(recibeact);
                    arrayActorFlujo.push(recibeact);
                }
            }

        },
        error: function (msg) {
            alert("No se pueden cargar los actores de flujos de pago de la idea = " + ideditar);
        }
    });

}



function sumarvaloresflujosprincipal() {
    //recorremos la tabla de flujo de pagos
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

        arrayinputflujos = $(this).find("td").slice(0, 1);

        if ($(arrayinputflujos[0]).html() != null) {
            var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();
            var iddesembolso = "#desenbolso" + $(arrayinputflujos[0]).html();

            var desembolso = $(idflujo).val();

            //validamos q el campo venga vacio
            if (desembolso == "") {
                desembolso = 0;

            } else {
                desembolso = desembolso.replace(/\./gi, '');
            }


            var datgriddes = $(iddesembolso).html();
            datgriddes = datgriddes.replace(/\./gi, '');

            var summadesen;
            summadesen = parseInt(desembolso) + parseInt(datgriddes);
            $(iddesembolso).text(addThousandChar(summadesen));
            $(idflujo).val("");
        }
    });

}


//funcion detalles de desembolsos para el grid flujo de pagos
function traerdetalles(str_idpago) {

    var htmlTableflujosdetalles = "<table id='T_detalle_desembolso' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>No pago</th><th width='1' style='color: #D3D6FF; font-size: 0.1em;'></th><th>Aportante</th><th>desembolso</th></tr></thead><tbody>";


    for (itemArray in matriz_flujos) {
        if (matriz_flujos[itemArray].N_pago == str_idpago) {
            htmlTableflujosdetalles += "<tr><td>" + matriz_flujos[itemArray].N_pago + "</td><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>" + matriz_flujos[itemArray].IdAportante + "</td><td>" + matriz_flujos[itemArray].Aportante + "</td><td>" + matriz_flujos[itemArray].Desembolso + "</td></tr>";
        }
    }

    htmlTableflujosdetalles += "</tbody></table></br></br> <div style='text-align: right'><input id='close_dialog' type='button' value='X' name='close_dialog' onclick=' x()' /></div>";

    //cargamos el div donde se generara la tabla actores
    $("#dialog").html("");
    $("#dialog").html(htmlTableflujosdetalles);

    $("#T_detalle_desembolso").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
    $("#close_dialog").button();

    $("#dialog").dialog("open", "show");

}

//fnucion para cerrar la ventana de detalles
function x() {
    $("#dialog").dialog("close");
}

var porcentaje;
//funcion para editar
function editflujo(index) {
    
    //capturamos los datos otraves para la edicion
    $("#ctl00_cphPrincipal_txtvalortotalflow").val($.trim(arrayflujosdepago[index].N_pagos));
    $("#ctl00_cphPrincipal_txtfechapago").val(arrayflujosdepago[index].fecha);
    $("#ctl00_cphPrincipal_txtporcentaje").val(parseInt(arrayflujosdepago[index].porcentaje.replace(' %', '')));
    // tflujos = tflujos.replace(/\./gi, ',');
    $("#ctl00_cphPrincipal_Lbltotalvalor").text(arrayflujosdepago[index].valortotal);
    $("#ctl00_cphPrincipal_txtentregable").val(arrayflujosdepago[index].entregable);

    switch_editar = 1;
    //lamar funcion borrar flujos de pagos
    eliminarflujo(arrayflujosdepago[index].N_pagos);
    //llamar suma de pagos
    sumarflujospagos();
}


//funcion para eliminar los flujos de pagos en el grid
function eliminarflujo(strN_pago) {

    swhich_validar_estado_1 = 0;
    //recorremos el array de flujos de pagos
    for (itemArray in arrayflujosdepago) {
        //construimos la llave de validacion
        var id = arrayflujosdepago[itemArray].N_pagos;
        //validamos el dato q nos trae la funcion

        if (strN_pago == id) {
            //borramos el actor deseado
            delete arrayflujosdepago[itemArray];
        }
    }

    //recorremos el array detalles de pagos
    for (itemArraymatriz in matriz_flujos) {
        //construimos la llave de validacion
        var idmatriz = matriz_flujos[itemArraymatriz].N_pago;
        //validamos el dato q nos trae la funcion

        if (strN_pago == idmatriz) {
            //borramos el actor deseado

            var actorsreverse;
            var desembolsorev;

            actorsreverse = matriz_flujos[itemArraymatriz].IdAportante;

            desembolsorev = matriz_flujos[itemArraymatriz].Desembolso;

            var jsonreverdesembolsos = {
                "actorsreverse": actorsreverse,
                "desembolsorev": desembolsorev
            };

            //cargamos el array con el json
            reversedesembolsos.push(jsonreverdesembolsos);

            delete matriz_flujos[itemArraymatriz];
        }
    }


    if (switch_editar == 0) {
        //boton eliminar
        eliminar_flujos();
    } else {
        switch_editar = 0;
        //boton editar
        editar_flujos();
    }
    reversedesembolsos = [];

    var idflow = "#flow" + $.trim(strN_pago);
    //borramos de la vista el td seleccionado 
    $(idflow).remove();

    //habilitamos el botn de agregar pagos
    $("#Btn_add_flujo").removeAttr("disabled");
    //llamar suma de pagos
    sumarflujospagos();
}


//funcion q edita los flujos de pago
function editar_flujos() {

    edit_flujo_inicializa = 1;
    var totalreverdesenbolso = 0;

    //recorremos la tabla flujo de actores
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

        arrayinputflujos = $(this).find("td").slice(0, 1);

        if ($(arrayinputflujos[0]).html() != null) {

            var idflujo = "#value" + $(arrayinputflujos[0]).html();

            //RECORREMOS EL ARRAY DE REVERSA DESEMBOLSOS
            for (itemdesembolsos in reversedesembolsos) {

                //validamos si el codigo del actor del array es igual al de la tabla
                if ($(arrayinputflujos[0]).html() == reversedesembolsos[itemdesembolsos].actorsreverse) {

                    var input = "#txtinput" + $(arrayinputflujos[0]).html();

                    //capturamos el valor del array y lo llevamos al campo de texto deseado
                    var demvolsobsumar = reversedesembolsos[itemdesembolsos].desembolsorev;
                    $(input).val(demvolsobsumar);

                    //totalizamos valores y asctualizamos el campo de total
                    totalreverdesenbolso = totalreverdesenbolso + parseInt(reversedesembolsos[itemdesembolsos].desembolsorev.replace(/\./gi, ''));
                    $("#totalflujos").text(addThousandChar(totalreverdesenbolso));

                }
            }
        }
    });

}

//funcion que elimina los flujos de pagos
function eliminar_flujos() {

    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

        arrayinputflujos = $(this).find("td").slice(0, 1);

        if ($(arrayinputflujos[0]).html() != null) {

            var idflujo = "#value" + $(arrayinputflujos[0]).html();

            var input = "#txtinput" + $(arrayinputflujos[0]).html();
            var input = $(input).val();
            input = input.replace(/\./gi, '');

            for (itemdesembolsos in reversedesembolsos) {

                if ($(arrayinputflujos[0]).html() == reversedesembolsos[itemdesembolsos].actorsreverse) {

                    var iddesembolso = "#desenbolso" + $(arrayinputflujos[0]).html();
                    var desembosoreal = $(iddesembolso).html();

                    desembosoreal = desembosoreal.replace(/\./gi, '');
                    var demvolsobsumar = reversedesembolsos[itemdesembolsos].desembolsorev;
                    demvolsobsumar = demvolsobsumar.replace(/\./gi, '');

                    var sumarever;

                    sumarever = parseInt(desembosoreal) + parseInt(demvolsobsumar);
                    $(iddesembolso).text(sumarever);
                }
            }
            $(input).text("");
        }
    });
}

var arrayeditarflujos = [];

function returnValueTotalFlow() {
    var totalValueToProgram = 0;
    $(".money").each(function () {
        totalValueToProgram = totalValueToProgram + parseInt($(this).val().replace(/\./g, ""));
    })
    return totalValueToProgram;
}

//funcion para restar los valores ingresados erronamente en los input de grilla de actores
function restar_flujos(str) {

    //$("#totalflujos").text("");
    //construimos el input a validar
    var idflujo = "#txtinput" + str;
    var iddesenbolso = "#desenbolso" + str;
    var idinicial = "#value" + str;

    //validamos si el input esta vacio
    if ($(idflujo).val() != "") {

        //capturamos valores
        var restaoperador = $(idflujo).val();

        if (restaoperador == "") {
            restaoperador = 0;
        }

        restaoperador = restaoperador.replace(/\./gi, '');

        var valorarraytotal = returnValueTotalFlow();
        // alert("valor capturado para restar ->" + valorarraytotal);

        var desenbolso = $(iddesenbolso).html();
        desenbolso = desenbolso.replace(/\./gi, '');

        var inicial = $(idinicial).html();
        inicial = inicial.replace(/\./gi, '');


        //restamos del array de operacion

        if (edit_swhich_fx == 1) {
            restaoperador = $(idflujo).val();
            restaoperador = restaoperador.replace(/\./gi, '');
            valorarraytotal = parseInt(valorarraytotal) - 0;
            arrayeditarflujos[0] = valorarraytotal;

            //        alert("ojo restar 3 " + arrayeditarflujos[0]);
        } else {
            valorarraytotal = parseInt(valorarraytotal) - parseInt(restaoperador);
        }

        if (edit_flujo_inicializa == 1) {
            restaoperador = $(idflujo).val();
            restaoperador = restaoperador.replace(/\./gi, '');
            arrayValorflujoTotal[0] = 0;
            //    alert("ojo restar 1 " + arrayValorflujoTotal[0]);
            edit_flujo_inicializa = 0;
            edit_swhich_fx = 1;

        } else {
            //        alert("ojo restar 2 " + arrayValorflujoTotal[0]);
            arrayValorflujoTotal[0] = valorarraytotal;

        }

        if (swhich_validar_estado_1 != 1) {
            if (inicial != desenbolso) {
                var desembolsototal = parseInt(desenbolso) + parseInt(restaoperador);

                // $("#ctl00_cphPrincipal_Txtpruebas").val(desembolsototal);
                $(iddesenbolso).text(addThousandChar(desembolsototal));
                swhich_validar_estado_1 = 0;
            }
        }
        //$(idflujo).focus();
    }

}


//sumar flujos de pagos
function sumar_flujos(str) {

    //inicializamos las variables
    var valdinerflujo = 0;
    var valorlimite = 0;
    var valtotaldiner = 0;
    var totaldesembolso = 0;

    var tr_Id = "#value" + str;
    var valuesActorslimit = $(tr_Id).html();
    valuesActorslimit = valuesActorslimit.replace(/\./gi, '');
    var opevaluesActorslimit = parseInt(valuesActorslimit);

    var tr_Iddes = "#desenbolso" + str;
    var valuesActorsdesembolso = $(tr_Iddes).html();
    valuesActorsdesembolso = valuesActorsdesembolso.replace(/\./gi, '');
    var opevaluesActorsdesembolso = parseInt(valuesActorsdesembolso);

    //capturamos el valor deseado
    var id = "#txtinput" + str;
    var ValuesActorsflujos = $(id).val();

    if (ValuesActorsflujos == "") {
        ValuesActorsflujos = 0;
    } else {
        ValuesActorsflujos = ValuesActorsflujos.replace(/\./gi, '');
    }


    var opeValuesActorsflujos = parseInt(ValuesActorsflujos);

    //funtion validar limite de actores
    var resultado_val = validar_limite_actores(opeValuesActorsflujos, opevaluesActorsdesembolso, opevaluesActorslimit, totaldesembolso, tr_Iddes);

    //Asigna valor total a flujos de pago
    $("#totalflujos").text(addThousandChar(returnValueTotalFlow()));
    //Limpia valor total en caso de error en las validaciones
    if (resultado_val == 1) {
        $("#totalflujos").text("");
    }

}


function validar_limite_actores(opeValuesActorsflujos, opevaluesActorsdesembolso, opevaluesActorslimit, totaldesembolso, tr_Iddes) {
    var error_actor = 0;

    //capturamos el valor limite del actor
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

        //validamos que el valor deseado no supere al limite
        if (opevaluesActorslimit < opeValuesActorsflujos) {
            alert("el valor ingresado no debe superar al ingresado en los actores");
            error_actor = 1;
            if (opevaluesActorsdesembolso != opevaluesActorslimit) {
                var desembolsototal2 = parseInt(opevaluesActorsdesembolso) + parseInt(opeValuesActorsflujos);
                $(tr_Iddes).text(addThousandChar(desembolsototal2));
                // $(id).focus();
            }

            swhich_validar_estado_1 = 1;

            opeValuesActorsflujos = 0;
        } else {
            if (opevaluesActorsdesembolso < opeValuesActorsflujos) {
                alert("el valor ingresado no debe superar al desembolso disponible");
                error_actor = 1;
                swhich_validar_estado_1 = 1;
                opeValuesActorsflujos = 0;

            } else {
                if (opevaluesActorslimit == opevaluesActorsdesembolso) {
                    totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
                    $(tr_Iddes).text(addThousandChar(totaldesembolso));

                } else {
                    totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
                    $(tr_Iddes).text(addThousandChar(totaldesembolso));

                }
            }
        }
    });
    return error_actor;
}





//funcion suma de flujos de pagos
function sumarflujospagos() {

    //inicializamos las variables
    var valporcentaje = 0;
    var valtotalflujo = 0;
    //recorremos la tabla actores para calcular los totales
    $("#T_flujos tr").slice(0, $("#T_flujos tr").length - 1).each(function () {
        var arrayValueflujos = $(this).find("td").slice(2, 7);

        //validamos si hay campos null en la tabla actores
        if ($(arrayValueflujos[0]).html() != null) {

            //capturamos e incrementamos los valores para la suma
            valporcentaje = valporcentaje + parseFloat($(arrayValueflujos[0]).html().replace('%', ''));
            valtotalflujo = valtotalflujo + parseInt($(arrayValueflujos[2]).html().replace(/\./gi, ''));

            //validamos valores si vienen vacios
            if (isNaN(valporcentaje)) {
                valporcentaje = 0;
            }
            if (isNaN(valtotalflujo)) {
                valtotalflujo = 0;
            }

            //cargamos los campos con la operacion realizada
            $("#porcentaje").text(valporcentaje + " %");
            $("#totalflujospagos").text(addThousandChar(valtotalflujo));
        } else {

            $("#porcentaje").text("0 %");
            $("#totalflujospagos").text("0");
        }
    });
}


//funcion valida las operaciones de flujo de pagos
function validarporcentaje() {

    // evento que verifica si han registrado actores para el flujo de pagos
    $("#ctl00_cphPrincipal_txtvalortotalflow").change(function () {
        var existvalueactors = arrayActor.length;

        //valida actores
        if (existvalueactors < 1) {
            $("#ctl00_cphPrincipal_Lblinformationexist").text("No se han agregado actores! debe ingresarlos");
            $("#ctl00_cphPrincipal_txtporcentaje").attr("disabled", "disabled");
            $("#ctl00_cphPrincipal_txtfechapago").attr("disabled", "disabled");
        } else {
            $("#ctl00_cphPrincipal_Lblinformationexist").text("");
            $("#ctl00_cphPrincipal_txtporcentaje").removeAttr("disabled");
            $("#ctl00_cphPrincipal_txtfechapago").removeAttr("disabled");

        }
    });

    // calcular campo valor despues de salir del foco de porcentaje
    $("#ctl00_cphPrincipal_txtporcentaje").change(function () {

        var porc = $("#ctl00_cphPrincipal_txtporcentaje").val();

        var valortotalflow = $("#tflujosing").text();
        valortotalflow = valortotalflow.replace(/\./gi, '');

        valortotalflow = parseInt(valortotalflow);

        //realiza la operacion del porcentaje seleccionado
        var resultSumPercentage = (parseFloat(porc) * parseFloat(valortotalflow)) / 100;
        resultSumPercentage = addThousandChar(Math.round(resultSumPercentage));

        $("#ctl00_cphPrincipal_Lbltotalvalor").text(resultSumPercentage);

        if (s_revisarflujos == 1) {

            var itemarrayflujos = 0;
            var idflujos = 0;

            for (itemThird in arrayActor) {
                if (arrayActor[itemThird].generatesflow == "s") {
                    idflujos = arrayActor[itemThird].IdThird;
                    break;
                }
            }

            $("#txtinput" + idflujos).val(resultSumPercentage);
            $("#totalflujos").text(resultSumPercentage);
        }


    });

    //validar que pestaña esta ingresando
    $("#tabsRequest").click(function () {

        var itemarrayflujos = 0;

        var active = $("#tabsRequest").tabs("option", "active");
        var idtabs = $("#tabsRequest ul>li a").eq(active).attr('href');
        //validar si esl la de flujos de pago
        if (idtabs == "#flujos") {
            //    alert(idtabs);
            //validar si es la primera entrada       
            if (entradaflujos == 0) {
                var tamaño_flujos = $("#T_Actorsflujos tr").length - 2;

                //alert(tamaño_flujos);
                //validar la cantidad de actores
                if (tamaño_flujos == 1) {
                    var Aflujos;

                    for (itemThird in arrayActor) {
                        if (arrayActor[itemThird].generatesflow == "s") {
                            Aflujos = arrayActor[itemThird].IdThird;
                            break;
                        }
                    }

                    $("#txtinput" + Aflujos).attr("disabled", "disabled");
                    $("#desenbolso" + Aflujos).text("");


                    entradaflujos = 1;
                    s_revisarflujos = 1;
                }
            }

            // $("#tabsIdea").tabs({ active: 4});
        }
        if (idtabs != "#flujos") {
            entradaflujos = 0;
            s_revisarflujos = 0;
        }

    });

    //Validar que el porcentaje no supere el 100 por ciento, no tenga comas ni tenga mas de 2 decimas
    $("#ctl00_cphPrincipal_txtporcentaje").change(function () {
        var expresion = /(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9])?$)/

        if (!expresion.test($("#ctl00_cphPrincipal_txtporcentaje").val())) {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("El porcentaje debe ser menor o igual a 100");
            $("#ctl00_cphPrincipal_txtporcentaje").val("");
            $("#ctl00_cphPrincipal_txtporcentaje").focus();
        } else {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("");

        }
        if ($("#ctl00_cphPrincipal_txtporcentaje").val() == 0) {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("El porcentaje debe ser mayor a 0");
            $("#ctl00_cphPrincipal_txtporcentaje").val("");
            $("#ctl00_cphPrincipal_txtporcentaje").focus();
        } else {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("");
        }

    });
}

function sumAllColumnsTableFlow() {
    var valdinerflujos = 0;
    //recorremos la tabla flujo de actores para calcular los totales
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {
        var arrayValuesflujos = $(this).find("td").slice(0, 4);
        //validamos si hay campos null en la tabla flujos actores
        if ($(arrayValuesflujos[0]).html() != null) {
            //capturamos e incrementamos los valores para la suma

            valdinerflujos = valdinerflujos + parseInt($(arrayValuesflujos[2]).html().replace(/\./gi, ''));

            if (isNaN(valdinerflujos)) {
                valdinerflujos = 0;
            }
            //cargamos los campos con la operacion realizada
            $("#tflujosing").text(addThousandChar(valdinerflujos));
        } else {
            $("#tflujosing").text(0);
        }
    });
}