$(document).ready(function() {
    $("#cancelEdition").click(function() {
        crear_tabla_flujos_pagos();
    });

    $("#ctl00_cphPrincipal_txtporcentaje").change(function() {
        if ($("#porcentaje").html() != null) {
            var porcentajeGlobal = removeCommasAndConvertFloat($.trim($("#porcentaje").html().replace('%', '')));
            var porcentajeLocal = removeCommasAndConvertFloat($(this).val());

            if ((porcentajeGlobal + porcentajeLocal) > 100) {
                alert("El porcentaje ingresado es invalido, verifique el porcentaje disponible para flujos de pago.");
                $(this).val("0");
                recalcValues();
            }
        }
    });
});

//agregar pagos al grid general de flujos
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
            var porcentaje = $("#ctl00_cphPrincipal_txtporcentaje").val();
            //            var valor_pago = valuecomparative;

            var entrega = $("#ctl00_cphPrincipal_txtentregable").val();
            var entregas_sin = $("#ctl00_cphPrincipal_txtentregable").val();

            var idpago;
            var Aportante;
            var desembolso;
            var idaportante;
            var isUpdate = false;

            //creamos json para guardarlos en un array
            var jsonflujo = {
                "N_pago": N_pago,
                "fecha_pago": fecha_pago,
                "porcentaje": porcentaje,
                "entrega": entrega,
                "tflujos": tflujos
            };


            //recorremos el array para revisar repetidos        
            var validerepetido = 0;
            for (iArray in arrayflujosdepago) {
                if (N_pago == arrayflujosdepago[iArray].N_pago) {
                    validerepetido = 1;
                }
            }
            if (validerepetido == 1) {
                isUpdate = confirm("El pago No " + N_pago + " ya fue registrado, desea actualizarlo?");

                console.log(isUpdate);
                if (isUpdate) {
                    updateFlow(N_pago, tflujos, $("#totalflujos").text());
                } else {
                    $("#ctl00_cphPrincipal_Lblinformation_flujos").text("El pago No " + N_pago + " ya fue registrado");
                }

                //recorremos la tabla de flujo de pagos para guardar los detalles
                $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

                    arrayinputflujos = $(this).find("td").slice(0, 3);

                    if ($(arrayinputflujos[0]).html() != null) {


                        idpago = $("#ctl00_cphPrincipal_txtvalortotalflow").val();
                        idaportante = $(arrayinputflujos[0]).html();
                        Aportante = $(arrayinputflujos[1]).html();
                        var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();

                        desembolso = $(idflujo).val();

                        if (isUpdate) {
                            updateDetailsFlow(idpago, desembolso, idaportante);
                        }
                    }
                });
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

                    //llamamos funcion que crea la tabla  de flujos de pago
                    crear_tabla_flujos_pagos();
                }

                var contadormatriz = 0;

                //recorremos la tabla de flujo de pagos para guardar los detalles
                $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

                    arrayinputflujos = $(this).find("td").slice(0, 3);

                    if ($(arrayinputflujos[0]).html() != null) {


                        idpago = $("#ctl00_cphPrincipal_txtvalortotalflow").val();
                        idaportante = $(arrayinputflujos[0]).html();
                        Aportante = $(arrayinputflujos[1]).html();
                        var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();

                        desembolso = $(idflujo).val();

                        if (isUpdate) {
                            updateDetailsFlow(idpago, desembolso, idaportante);

                        } else {

                            var jsonflujodetalle = {
                                "idpago": idpago,
                                "idaportante": idaportante,
                                "Aportante": Aportante,
                                "desembolso": desembolso
                            };
                            matriz_flujos.push(jsonflujodetalle);
                        }

                    }
                });

                //recorremos la tabla de flujo de pagos  borrar los pagos
                $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

                    arrayinputflujos = $(this).find("td").slice(0, 1);

                    if ($(arrayinputflujos[0]).html() != null) {
                        var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();
                        $(idflujo).val("");
                    }

                });


                //limpiamos campos
                arrayValorflujoTotal[0] = 0;
                $("#totalflujos").text(0);
                if (validaporcentaje < 100) {
                    $("#ctl00_cphPrincipal_txtvalortotalflow").val("");
                    $("#ctl00_cphPrincipal_txtfechapago").val("");
                    $("#ctl00_cphPrincipal_txtporcentaje").val("");
                    $("#ctl00_cphPrincipal_Lbltotalvalor").text("");
                    $("#ctl00_cphPrincipal_txtentregable").val("");
                }

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
    crear_tabla_flujos_pagos();
}

function updateFlow(N_pago, tflujos) {
    for (var item in arrayflujosdepago) {

        if (arrayflujosdepago[item].N_pago == $.trim(N_pago)) {
            arrayflujosdepago[item].fecha_pago = $("#ctl00_cphPrincipal_txtfechapago").val();
            arrayflujosdepago[item].porcentaje = $("#ctl00_cphPrincipal_txtporcentaje").val();
            arrayflujosdepago[item].entrega = $("#ctl00_cphPrincipal_txtentregable").val();
            arrayflujosdepago[item].tflujos = tflujos;
            break;
        }
    }
}

function updateDetailsFlow(idpago, desembolso, idaportante) {
    for (var item in matriz_flujos) {
        if (matriz_flujos[item].idpago == $.trim(idpago) && matriz_flujos[item].idaportante == idaportante) {
            matriz_flujos[item].desembolso = desembolso;
        }
    }

    setTimeout("crear_tabla_flujos_pagos();", 1000);
}

function recalcValues() {

    for (var item in arrayActorFlujo) {
        $("#desenbolso" + arrayActorFlujo[item].actorsVal).html($.trim($("#value" + arrayActorFlujo[item].actorsVal).html()));
    }

    for (var item in matriz_flujos) {
        var valuePartial = $("#desenbolso" + matriz_flujos[item].idaportante).html();
        valuePartial = valuePartial.replace(/\./gi, '');

        var detailFlow = matriz_flujos[item].desembolso;
        detailFlow = detailFlow.replace(/\./gi, '');

        var totalOperation = parseInt(valuePartial) - parseInt(detailFlow);

        $("#desenbolso" + matriz_flujos[item].idaportante).html(addCommasrefactor(totalOperation));
    }
}

function clearFieldsFlows() {
    $("#ctl00_cphPrincipal_txtvalortotalflow").val("");
    $("#ctl00_cphPrincipal_txtfechapago").val("");
    $("#ctl00_cphPrincipal_txtporcentaje").val("");
    $("#ctl00_cphPrincipal_txtentregable").val("");
    $("#ctl00_cphPrincipal_Lbltotalvalor").html("");
    //$(".money").trigger("blur");
    $(".money").val("");
}

//creamos la tabla de flujo de pagos
function crear_tabla_flujos_pagos(notClear) {

    recalcValues();

    if (!notClear) {
        setTimeout("clearFieldsFlows();", 500);
    }

    var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";

    for (itemArray in arrayflujosdepago) {
        var entregacomas = arrayflujosdepago[itemArray].entrega;
       // entregacomas = entregacomas.replace(/¬/g, ',');

        var pagoadd = arrayflujosdepago[itemArray].tflujos;
        pagoadd = addCommasrefactor(pagoadd);

        htmlTableflujos += "<tr id='flow" + arrayflujosdepago[itemArray].N_pago + "' ><td>" + arrayflujosdepago[itemArray].N_pago + "</td><td>" + arrayflujosdepago[itemArray].fecha_pago + "</td><td>" + arrayflujosdepago[itemArray].porcentaje + " %</td><td>" + entregacomas + "</td><td>" + pagoadd + "</td><td><input class='editFlow' type ='button' value= 'Editar' onclick=\"editflujo('" + itemArray + "')\" ></input><input type ='button' value= 'Eliminar' onclick=\" eliminarflujo('" + arrayflujosdepago[itemArray].N_pago + "')\"></input></td><td><input type ='button' value= 'Detalle' onclick=\"traerdetalles('" + arrayflujosdepago[itemArray].N_pago + "',this)\"></input></td></tr>";

    }
    htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

    htmlTableflujos += "</tbody></table>";
    //cargamos el div donde se generara la tabla actores
    $("#T_flujosContainer").html("");
    $("#T_flujosContainer").html(htmlTableflujos);

    swhich_flujos_exist = 1;
    //lamar funcion para la suma de los subtotales de flujos de pagos
    sumarflujospagos();

    //reconstruimos la tabla con los datos
    $("#T_flujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $(".editFlow").click(function() {
        $(this).parent().parent().remove();
        sumarflujospagos();
    });

    recalcValues();
}


function cambio_text_flujos(str_txt) {

    str_txt = str_txt.replace(/,/g, '¬');
    str_txt = str_txt.replace(/\n/g, ' ');
    str_txt = str_txt.replace(/\r/g, '');
    str_txt = str_txt.replace(/\t/g, '');
    str_txt = str_txt.replace(/\n\r/g, ' ');
    str_txt = str_txt.replace(/\r\n/g, ' ');
    str_txt = str_txt.replace(/"/g, " ");

    return (str_txt);
}


function View_detalle_flujo_array() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: {
            "action": "View_detalle_flujo_array",
            "ididea": idea_buscar
        },
        success: function(result) {

            if (result == "vacio") {

                matriz_flujos = [];

            } else {

                matriz_flujos_ed = result.split("|");

                for (itemArray in matriz_flujos_ed) {

                    var recibeact = JSON.parse(matriz_flujos_ed[itemArray]);
                    matriz_flujos.push(recibeact);
                }
            }
        },
        error: function(msg) {
            alert("No se pueden cargar los flujos de pago de la idea = " + ideditar);
        }
    });

}


function View_flujos_p_array() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: {
            "action": "View_flujos_p_array",
            "ididea": idea_buscar
        },
        success: function(result) {

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
            //llamamos funcion que crea la tabla  de flujos de pago
            crear_tabla_flujos_pagos();

        },
        error: function(msg) {
            alert("No se pueden cargar los flujos de pago de la idea = " + ideditar);
        }
    });

}


//creamos la tabla de flujo actores
function crear_tabla_flujo_actor(isEditLoad) {

    var htmltableAflujos = "<table id='T_Actorsflujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th width='1'></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>";

    for (itemarrayflujos in arrayActorFlujo) {

        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        //validamos si creamos la idea o editamos
        var disponible = arrayActorFlujo[itemarrayflujos].diner;

        if (isEditLoad) {
            disponible = 0;
        }


        htmltableAflujos += "<tr id='flujo" + arrayActorFlujo[itemarrayflujos].actorsVal + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActorFlujo[itemarrayflujos].actorsVal + "</td><td>" + arrayActorFlujo[itemarrayflujos].actorsName + "</td><td id= 'value" + arrayActorFlujo[itemarrayflujos].actorsVal + "' >" + arrayActorFlujo[itemarrayflujos].diner + "</td><td><input class='money' id='" + "txtinput" + arrayActorFlujo[itemarrayflujos].actorsVal + "' onkeyup='formatvercionsuma(this)' onchange='formatvercionsuma(this)'  onblur=\"sumar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\" onfocus=\"restar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\"></input></td><td id='desenbolso" + arrayActorFlujo[itemarrayflujos].actorsVal + "'>" + disponible + "</td></tr>";
    }

    htmltableAflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td id='tflujosing'></td><td id='totalflujos'>0</td></td id='tflujosdesen'><td></tr></tbody></table>";

    //cargamos el div donde se generara la tabla flujo de actores
    $("#T_AflujosContainer").html("");
    $("#T_AflujosContainer").html(htmltableAflujos);

    sumar_flujos_actores();

    //reconstruimos la tabla con los datos 
    $("#T_Actorsflujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}

//funcion para la suma de valor en efectivo en el grid de flujo_actores
function sumar_flujos_actores() {

    var valdinerflujos = 0;

    //recorremos la tabla flujo de actores para calcular los totales
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {
        var arrayValuesflujos = $(this).find("td").slice(0, 4);
        //validamos si hay campos null en la tabla flujos actores
        if ($(arrayValuesflujos[0]).html() != null) {
            //capturamos e incrementamos los valores para la suma

            valdinerflujos = valdinerflujos + parseInt($(arrayValuesflujos[2]).html().replace(/\./gi, ''));

            if (isNaN(valdinerflujos)) {
                valdinerflujos = 0;
            }
            //cargamos los campos con la operacion realizada
            $("#tflujosing").text(addCommasrefactor(valdinerflujos));
        } else {
            $("#tflujosing").text(0);
        }
    });

}


//funtion crear array de flujos de pagos
function View_flujos_actors_array() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: {
            "action": "View_flujos_actors_array",
            "ididea": idea_buscar
        },
        success: function(result) {

            if (result == "vacio") {

                arrayActorFlujo = [];

            } else {
                arrayactorflujo_ed = result.split("|");

                for (itemArray in arrayactorflujo_ed) {
                    if (arrayactorflujo_ed[itemArray] != "") {
                        var recibeact = JSON.parse(arrayactorflujo_ed[itemArray]);
                        arrayActorFlujo.push(recibeact);
                    }
                }
            }

            //llama la funcion crear la tabla de flujo_actor
            crear_tabla_flujo_actor(true);

        },
        error: function(msg) {
            alert("No se pueden cargar los actores de flujos de pago de la idea = " + ideditar);
        }
    });

}



function sumarvaloresflujosprincipal() {
    //recorremos la tabla de flujo de pagos
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

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
            $(iddesembolso).text(addCommasrefactor(summadesen));
            $(idflujo).val("");
        }
    });

}


//funcion detalles de desembolsos para el grid flujo de pagos
function traerdetalles(str_idpago) {

    var htmlTableflujosdetalles = "<table id='T_detalle_desembolso' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>No pago</th><th width='1' style='color: #D3D6FF; font-size: 0.1em;'></th><th>Aportante</th><th>desembolso</th></tr></thead><tbody>";


    for (itemArray in matriz_flujos) {
        if (matriz_flujos[itemArray].idpago == str_idpago) {
            htmlTableflujosdetalles += "<tr><td>" + matriz_flujos[itemArray].idpago + "</td><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>" + matriz_flujos[itemArray].idaportante + "</td><td>" + matriz_flujos[itemArray].Aportante + "</td><td>" + matriz_flujos[itemArray].desembolso + "</td></tr>";
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


//funcion para editar
function editflujo(index) {
    crear_tabla_flujo_actor();
    //capturamos los datos otraves para la edicion
    $("#ctl00_cphPrincipal_txtvalortotalflow").val(arrayflujosdepago[index].N_pago);
    $("#ctl00_cphPrincipal_txtfechapago").val(arrayflujosdepago[index].fecha_pago);
 //   porcentaje = porcentaje.replace(' %', '');
 //   porcentaje = porcentaje.replace(' ', '');
    $("#ctl00_cphPrincipal_txtporcentaje").val(arrayflujosdepago[index].porcentaje);
    // tflujos = tflujos.replace(/\./gi, ',');
    $("#ctl00_cphPrincipal_Lbltotalvalor").text(addCommasrefactor(arrayflujosdepago[index].tflujos));
    $("#ctl00_cphPrincipal_txtentregable").val(arrayflujosdepago[index].entrega);

    switch_editar = 1;

    //lamar funcion borrar flujos de pagos

    //eliminarflujo(strN_pago);

    //llamar suma de pagos

    $("#Btn_add_flujo").removeAttr("disabled");

    getDetailsForNpago(arrayflujosdepago[index].N_pago);

    sumarflujospagos();

    recalcValues();
}

//funcion para eliminar los flujos de pagos en el grid
function eliminarflujo(strN_pago) {

    swhich_validar_estado_1 = 0;
    //recorremos el array de flujos de pagos
    for (itemArray in arrayflujosdepago) {
        //construimos la llave de validacion
        var id = arrayflujosdepago[itemArray].N_pago;
        //validamos el dato q nos trae la funcion

        if (strN_pago == id) {
            //borramos el actor deseado
            if (switch_editar == 0) {
               // delete arrayflujosdepago[itemArray];
                arrayflujosdepago.splice(itemArray, 1);

            }
        }
    }

    //recorremos el array detalles de pagos
    for (itemArraymatriz in matriz_flujos) {
        //construimos la llave de validacion
        var idmatriz = matriz_flujos[itemArraymatriz].idpago;
        //validamos el dato q nos trae la funcion

        if (strN_pago == idmatriz) {
            //borramos el actor deseado

            var actorsreverse;
            var desembolsorev;

            actorsreverse = matriz_flujos[itemArraymatriz].idaportante;

            desembolsorev = matriz_flujos[itemArraymatriz].desembolso;

            var jsonreverdesembolsos = {
                "actorsreverse": actorsreverse,
                "desembolsorev": desembolsorev
            };

            //cargamos el array con el json
            reversedesembolsos.push(jsonreverdesembolsos);
            if (switch_editar == 0) {
                matriz_flujos.splice(itemArraymatriz, 1);
                //delete matriz_flujos[itemArraymatriz];
            }
        }
    }


    if (switch_editar == 0) {
        //boton eliminar
        //eliminar_flujos();
    } else {
        switch_editar = 0;
        //boton editar
        editar_flujos();
    }
    recalcValues();
    reversedesembolsos = [];

    var idflow = "#flow" + strN_pago;
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
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

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

                    $(input).trigger("focus");

                    //totalizamos valores y asctualizamos el campo de total
                    totalreverdesenbolso = totalreverdesenbolso + parseInt(reversedesembolsos[itemdesembolsos].desembolsorev.replace(/\./gi, ''));
                    $("#totalflujos").text(addCommasrefactor(totalreverdesenbolso));

                }
            }
        }
    });

}

//funcion que elimina los flujos de pagos
function eliminar_flujos() {

    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

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
//funcion para restar los valores ingresados erronamente en los input de grilla de actores
function restar_flujos(str) {

    var valueToSum = removeCommasAndConvert($("#txtinput" + str).val());
    var valueDynamic = removeCommasAndConvert($("#desenbolso" + str).html());
    var totalValue = valueToSum + valueDynamic;

    if (totalValue > removeCommasAndConvert($("#value" + str).html())) {

    } else {
        $("#desenbolso" + str).html(addCommasrefactor(totalValue));
    }

    $("#totalflujos").text(addCommasrefactor(returnValueTotalFlow()));

}

function removeCommasAndConvert(valueToConvert) {
    if (valueToConvert == "" || valueToConvert == null) {
        return 0;
    } else {
        valueToConvert = valueToConvert.replace(/\./gi, '');
        valueToConvert = parseInt(valueToConvert);
        return valueToConvert;
    }
}

function removeCommasAndConvertFloat(valueToConvert, isDecimal) {
    if (valueToConvert == "" || valueToConvert == null) {
        return 0;
    } else {
        if (isDecimal)
            valueToConvert = valueToConvert;
        else
            valueToConvert = valueToConvert.replace(/\./gi, '');

        valueToConvert = parseFloat(valueToConvert);
        return valueToConvert;
    }
}
//sumar flujos de pagos
function sumar_flujos(str) {


    var valueToSum = removeCommasAndConvert($("#txtinput" + str).val());
    var valueDynamic = removeCommasAndConvert($("#desenbolso" + str).html());
    var totalValue = valueDynamic - valueToSum;

    if (totalValue < 0) {
        alert("El valor proporcionado no puede ser mayor que el saldo por programar.");
        $("#txtinput" + str).trigger("focus");
        $("#txtinput" + str).val("0");

    } else {
        $("#desenbolso" + str).html(addCommasrefactor(totalValue));
    }
    $("#totalflujos").text(addCommasrefactor(returnValueTotalFlow()));
}

function returnValueTotalFlow() {
    var totalValueToProgram = 0;
    $(".money").each(function() {
        if ($.trim($(this).val()) != "") {
            totalValueToProgram = totalValueToProgram + parseInt($(this).val().replace(/\./g, ""));
        }
    })
    return totalValueToProgram;
}

function validar_limite_actores(opeValuesActorsflujos, opevaluesActorsdesembolso, opevaluesActorslimit, totaldesembolso, tr_Iddes, idThird) {
    var error_actor = 0;

    //validamos que el valor deseado no supere al limite
    if (opevaluesActorslimit < opeValuesActorsflujos) {
        alert("el valor ingresado no debe superar al ingresado en los actores");
        $(idThird).trigger("focus");
    } else {

        if (opevaluesActorsdesembolso < opeValuesActorsflujos) {
            alert("el valor ingresado no debe superar el desembolso disponible");

            $(idThird).trigger("focus");

        } else {
            if (opevaluesActorslimit == opevaluesActorsdesembolso) {
                totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
                $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

            }
        }
    }
    return error_actor;
}

var swhich_flujos_exist;

//funcion suma de flujos de pagos
function sumarflujospagos() {

    //inicializamos las variables
    var valporcentaje = 0;
    var valtotalflujo = 0;
    //recorremos la tabla actores para calcular los totales
    $("#T_flujos tr").slice(0, $("#T_flujos tr").length - 1).each(function() {
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
            $("#totalflujospagos").text(addCommasrefactor(valtotalflujo));
        } else {

            $("#porcentaje").text("0 %");
            $("#totalflujospagos").text("0");
        }
    });
}


//funcion valida las operaciones de flujo de pagos
function validarporcentaje() {

    // evento que verifica si han registrado actores para el flujo de pagos
    $("#ctl00_cphPrincipal_txtvalortotalflow").focusout(function() {
        var existvalueactors = $("#ctl00_cphPrincipal_HDvalorpagoflujo").val();

        //valida actores
        if (existvalueactors == "") {
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
    $("#ctl00_cphPrincipal_txtporcentaje").focusout(function() {

        var porc = $("#ctl00_cphPrincipal_txtporcentaje").val();

        var valortotalflow = $("#tflujosing").text();
        valortotalflow = valortotalflow.replace(/\./gi, '');

        valortotalflow = parseInt(valortotalflow);

        //realiza la operacion del porcentaje seleccionado
        var parcial = (parseFloat(porc) * parseFloat(valortotalflow)) / 100;

        $("#ctl00_cphPrincipal_Lbltotalvalor").text(addCommasrefactor(Math.floor(parcial)));

        if (s_revisarflujos == 1) {

            var itemarrayflujos = 0;
            if (arrayActorFlujo[0] != undefined) {
                var idflujos = arrayActorFlujo[0].actorsVal;

                valuecomparative = Math.floor(parcial);

                $("#txtinput" + idflujos).val(addCommasrefactor(valuecomparative));
                $("#totalflujos").text(addCommasrefactor(valuecomparative));
                value_disponible(idflujos);
            }
        }

    });

    //Validar que el porcentaje no supere el 100 por ciento, no tenga comas ni tenga mas de 2 decimas
    $("#ctl00_cphPrincipal_txtporcentaje").change(function() {
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

function value_disponible(id_actor) {


    var tr_Id = "#value" + id_actor;
    var valuesActorslimit = $(tr_Id).html();
    valuesActorslimit = valuesActorslimit.replace(/\./gi, '');
    var opevaluesActorslimit = parseInt(valuesActorslimit);

    var id = "#txtinput" + id_actor;
    var ValuesActorsflujos = $(id).val();
    ValuesActorsflujos = ValuesActorsflujos.replace(/\./gi, '');
    var opeValuesActorsflujos = parseInt(ValuesActorsflujos);

    var tr_Iddes = "#desenbolso" + id_actor;
    var valuesActorsdesembolso = $(tr_Iddes).html();
    valuesActorsdesembolso = valuesActorsdesembolso.replace(/\./gi, '');

    var totaldesembolso = 0;
    if (matriz_flujos.length == 0) {
        totaldesembolso = valuesActorslimit - opeValuesActorsflujos;
    } else {
        totaldesembolso = valuesActorsdesembolso - opeValuesActorsflujos;
    }

    $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

}

function getDetailsForNpago(N_pago) {
    for (var item in matriz_flujos) {
        if (matriz_flujos[item].idpago == N_pago) {
            console.log(matriz_flujos[item].desembolso);
            $("#txtinput" + matriz_flujos[item].idaportante).val(matriz_flujos[item].desembolso);
            $("#txtinput" + matriz_flujos[item].idaportante).trigger("focus");
            $("#txtinput" + matriz_flujos[item].idaportante).trigger("blur");
        }
    }
}



////////////////////////////------------------------------------------------------------------------------------



function View_flujos_actors_project() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_flujos_actors_project", "idproject": ideditar },
        success: function(result) {

            if (result == "vacio") {
                //    alert(result);
                arrayActorFlujo = [];

            }

            else {
                arrayactorflujo_ed = result.split("|");

                // alert(arrayactorflujo_ed);
                for (itemArray in arrayactorflujo_ed) {
                    var recibeact = JSON.parse(arrayactorflujo_ed[itemArray]);
                    arrayActorFlujo.push(recibeact);

                    //llama la funcion crear la tabla de flujo_actor
                    crear_tabla_flujo_actor();
                }
            }
        },
        error: function(msg) {
            alert("No se pueden cargar los actores de flujos de pago de la idea = " + idea_buscar);
        }
    });

}

//creamos la funcion para traer los datos de flujo de pagos en edicion
function View_flujos_p_project() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_flujos_p_project", "idproject": ideditar },
        success: function(result) {

            if (result == "vacio") {
                arrayflujosdepago = [];
                swhich_flujos_exist = 0;
            }
            else {
                arrayflujosdepago_ed = result.split("|");

                for (itemArray in arrayflujosdepago_ed) {

                    var recibeact = JSON.parse(arrayflujosdepago_ed[itemArray]);
                    arrayflujosdepago.push(recibeact);
                }
                swhich_flujos_exist = 1;
            }
            //llamamos funcion que crea la tabla  de flujos de pago
            crear_tabla_flujos_pagos();

        },
        error: function(msg) {
            alert("No se pueden cargar los flujos de pago de la idea = " + idea_buscar);
        }
    });

}

function View_detalle_flujo_project() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_detalle_flujo_project", "idproject": ideditar },
        success: function(result) {

            if (result == "vacio") {

                matriz_flujos = [];

            }
            else {

                matriz_flujos_ed = result.split("|");

                for (itemArray in matriz_flujos_ed) {

                    var recibeact = JSON.parse(matriz_flujos_ed[itemArray]);
                    matriz_flujos.push(recibeact);
                }
            }

        },
        error: function(msg) {
            alert("No se pueden cargar los flujos de pago de la idea = " + idea_buscar);
        }
    });

}
