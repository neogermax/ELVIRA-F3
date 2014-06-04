//agregar pagos al grid general de flujos
function Btn_add_flujo_onclick() {

    if ($("#ctl00_cphPrincipal_txtporcentaje").val() == "" || $("#ctl00_cphPrincipal_txtvalortotalflow").val() == "" || $("#ctl00_cphPrincipal_txtfechapago").val() == "" || $("#ctl00_cphPrincipal_txtentregable").val() == "") {
        $("#ctl00_cphPrincipal_Lblinformationexist").text("Estos campos deben ser diligenciados antes de ingresar algún valor en la grilla de flujos!");

    }
    else {

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
            var jsonflujo = { "N_pago": N_pago, "fecha_pago": fecha_pago, "porcentaje": porcentaje, "entrega": entrega, "tflujos": tflujos };


            //recorremos el array para revisar repetidos        
            var validerepetido = 0;
            for (iArray in arrayflujosdepago) {
                if (N_pago == arrayflujosdepago[iArray].N_pago) {
                    validerepetido = 1;
                }
            }


            //validamos si hay repetidos
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_Lblinformation_flujos").text("El pago No " + N_pago + " ya fue registrado");
            }
            else {
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
                }
                else {
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
                        var jsonflujodetalle = { "idpago": idpago, "idaportante": idaportante, "Aportante": Aportante, "desembolso": desembolso };

                        //cargamos el array con el json
                        matriz_flujos.push(jsonflujodetalle);


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
                $("#ctl00_cphPrincipal_txtvalortotalflow").val("");
                $("#ctl00_cphPrincipal_txtfechapago").val("");
                $("#ctl00_cphPrincipal_txtporcentaje").val("");
                $("#ctl00_cphPrincipal_Lbltotalvalor").text("");
                $("#ctl00_cphPrincipal_txtentregable").val("");


                //validamos si los desembolsos son del 100%
                if (validaporcentaje == 100) {
                    $("#Btn_add_flujo").attr("disabled", "disabled");
                }
            }

        }
        else {

            // $(idflujo).val("");

            if (parseInt(valuecomparative) < parseInt(opesumagos)) {
                alert("el valor no debe ser mayor que el porcentaje aprobado");

                swhich_validar_estado_1 = 1;
                arrayValorflujoTotal[0] = 0;
                $("#totalflujos").text(0);

                sumarvaloresflujosprincipal();

            }
            else {
                alert("el valor total de los actores debe ser igual al porcentaje calculado");

                //inicializamos el array
                arrayValorflujoTotal[0] = 0;
                $("#totalflujos").text(0);
                sumarvaloresflujosprincipal();

            }

        }
    }
}

//creamos la tabla de flujo de pagos
function crear_tabla_flujos_pagos() {

    var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";

    for (itemArray in arrayflujosdepago) {

        var entregacomas = arrayflujosdepago[itemArray].entrega;
        entregacomas = entregacomas.replace(/¬/g, ',');

        var pagoadd = arrayflujosdepago[itemArray].tflujos;
        pagoadd = addCommasrefactor(pagoadd);

        htmlTableflujos += "<tr id='flow" + arrayflujosdepago[itemArray].N_pago + "' ><td>" + arrayflujosdepago[itemArray].N_pago + "</td><td>" + arrayflujosdepago[itemArray].fecha_pago + "</td><td>" + arrayflujosdepago[itemArray].porcentaje + "</td><td>" + entregacomas + "</td><td>" + pagoadd + "</td><td><input type ='button' value= 'Editar' onclick=\"editflujo('" + arrayflujosdepago[itemArray].N_pago + "','" + arrayflujosdepago[itemArray].fecha_pago + "',' " + arrayflujosdepago[itemArray].porcentaje + "','" + arrayflujosdepago[itemArray].entrega + "','" + pagoadd + "')\" ></input><input type ='button' value= 'Eliminar' onclick=\" eliminarflujo('" + arrayflujosdepago[itemArray].N_pago + "')\"></input></td><td><input type ='button' value= 'Detalle' onclick=\"traerdetalles('" + arrayflujosdepago[itemArray].N_pago + "',this)\"></input></td></tr>";
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



function View_detalle_flujo_array() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_detalle_flujo_array", "ididea": idea_buscar },
        success: function(result) {

            if (result == "vacio") {

                // alert(result + " detalles");
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

function View_flujos_p_array() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_flujos_p_array", "ididea": idea_buscar },
        success: function(result) {

            if (result == "vacio") {
                //    alert(result + " flujo actores");
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


//creamos la tabla de flujo actores
function crear_tabla_flujo_actor() {


    var htmltableAflujos = "<table id='T_Actorsflujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th width='1'></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>";

    for (itemarrayflujos in arrayActorFlujo) {
        htmltableAflujos += "<tr id='flujo" + arrayActorFlujo[itemarrayflujos].actorsVal + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActorFlujo[itemarrayflujos].actorsVal + "</td><td>" + arrayActorFlujo[itemarrayflujos].actorsName + "</td><td id= 'value" + arrayActorFlujo[itemarrayflujos].actorsVal + "' >" + arrayActorFlujo[itemarrayflujos].diner + "</td><td><input id='" + "txtinput" + arrayActorFlujo[itemarrayflujos].actorsVal + "' onkeyup='formatvercionsuma(this)' onchange='formatvercionsuma(this)'  onblur=\"sumar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\" onfocus=\"restar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\"></input></td><td id='desenbolso" + arrayActorFlujo[itemarrayflujos].actorsVal + "'>" + arrayActorFlujo[itemarrayflujos].diner + "</td></tr>";
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
        }
        else {
            $("#tflujosing").text(0);
        }
    });

}


//funtion crear array de flujos de pagos
function View_flujos_actors_array() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_flujos_actors_array", "ididea": idea_buscar },
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
                    // alert(recibeact);
                    arrayActorFlujo.push(recibeact);
                }
            }

            //llama la funcion crear la tabla de flujo_actor
            crear_tabla_flujo_actor();

        },
        error: function(msg) {
            alert("No se pueden cargar los actores de flujos de pago de la idea = " + idea_buscar);
        }
    });

}


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

            }
            else {
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
function editflujo(strN_pago, fecha_pago, porcentaje, entrega, tflujos) {

    //capturamos los datos otraves para la edicion
    $("#ctl00_cphPrincipal_txtvalortotalflow").val(strN_pago);
    $("#ctl00_cphPrincipal_txtfechapago").val(fecha_pago);
    porcentaje = porcentaje.replace(' %', '');
    porcentaje = porcentaje.replace(' ', '');
    $("#ctl00_cphPrincipal_txtporcentaje").val(porcentaje);
    // tflujos = tflujos.replace(/\./gi, ',');
    $("#ctl00_cphPrincipal_Lbltotalvalor").text(tflujos);
    $("#ctl00_cphPrincipal_txtentregable").val(entrega);

    switch_editar = 1;
    //lamar funcion borrar flujos de pagos

    eliminarflujo(strN_pago);
    //llamar suma de pagos
    sumarflujospagos();
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
            delete arrayflujosdepago[itemArray];
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

            var jsonreverdesembolsos = { "actorsreverse": actorsreverse, "desembolsorev": desembolsorev };

            //cargamos el array con el json
            reversedesembolsos.push(jsonreverdesembolsos);

            delete matriz_flujos[itemArraymatriz];
        }
    }


    if (switch_editar == 0) {
        //boton eliminar
        eliminar_flujos();
    }
    else {
        switch_editar = 0;
        //boton editar
        editar_flujos();
    }
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

    $("#totalflujos").text("");
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

        var valorarraytotal = arrayValorflujoTotal[0];
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
        }
        else {
            valorarraytotal = parseInt(valorarraytotal) - parseInt(restaoperador);
        }

        if (edit_flujo_inicializa == 1) {
            restaoperador = $(idflujo).val();
            restaoperador = restaoperador.replace(/\./gi, '');
            arrayValorflujoTotal[0] = 0;
            //    alert("ojo restar 1 " + arrayValorflujoTotal[0]);
            edit_flujo_inicializa = 0;
            edit_swhich_fx = 1;

        }
        else {
            //        alert("ojo restar 2 " + arrayValorflujoTotal[0]);
            arrayValorflujoTotal[0] = valorarraytotal;

        }

        if (swhich_validar_estado_1 != 1) {
            if (inicial != desenbolso) {
                var desembolsototal = parseInt(desenbolso) + parseInt(restaoperador);

                // $("#ctl00_cphPrincipal_Txtpruebas").val(desembolsototal);
                $(iddesenbolso).text(addCommasrefactor(desembolsototal));
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
    }
    else {
        ValuesActorsflujos = ValuesActorsflujos.replace(/\./gi, '');
    }


    var opeValuesActorsflujos = parseInt(ValuesActorsflujos);

    //funtion validar limite de actores
    var resultado_val = validar_limite_actores(opeValuesActorsflujos, opevaluesActorsdesembolso, opevaluesActorslimit, totaldesembolso, tr_Iddes);

    //validamos si es el primer registro del array
    if (arrayValorflujoTotal.length == 0) {
        valtotaldiner = valtotaldiner + opeValuesActorsflujos;
        //ingresamos el valor en un array estatico
        arrayValorflujoTotal[0] = valtotaldiner;
        //     alert("ojo sumar 1 " + valtotaldiner);
        $("#totalflujos").text(addCommasrefactor(valtotaldiner));
    }
    else {

        if (arrayValorflujoTotal[0] < 0) {
            // arrayValorflujoTotal[0] = 0;

            valtotaldiner = arrayValorflujoTotal[0];

            if (edit_flujo_inicializa == 1) {
                valtotaldiner = valtotaldiner + opeValuesActorsflujos;
                edit_flujo_inicializa = 0;
                //          alert("ojo sumar 2 " + valtotaldiner);   
            }
            else {
                valtotaldiner = valtotaldiner + opeValuesActorsflujos;
                //         alert("ojo sumar 2 1/2 " + valtotaldiner); 

            }


            arrayValorflujoTotal[0] = valtotaldiner;
            //  alert(valtotaldiner);
            $("#totalflujos").text(addCommasrefactor(valtotaldiner));

        }
        else {
            valtotaldiner = arrayValorflujoTotal[0];
            valtotaldiner = valtotaldiner + opeValuesActorsflujos;
            //      alert("ojo sumar 3 " + valtotaldiner);

            arrayValorflujoTotal[0] = valtotaldiner;
            $("#totalflujos").text(addCommasrefactor(valtotaldiner));
        }
        //ingresamos el valor en un array estatico

    }

    if (resultado_val == 1) {
        $("#totalflujos").text("");
    }


}


function validar_limite_actores(opeValuesActorsflujos, opevaluesActorsdesembolso, opevaluesActorslimit, totaldesembolso, tr_Iddes) {
    var error_actor = 0;

    //capturamos el valor limite del actor
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

        //validamos que el valor deseado no supere al limite
        if (opevaluesActorslimit < opeValuesActorsflujos) {
            alert("el valor ingresado no debe superar al ingresado en los actores");
            error_actor = 1;
            if (opevaluesActorsdesembolso != opevaluesActorslimit) {
                var desembolsototal2 = parseInt(opevaluesActorsdesembolso) + parseInt(opeValuesActorsflujos);
                $(tr_Iddes).text(addCommasrefactor(desembolsototal2));
                // $(id).focus();
            }

            swhich_validar_estado_1 = 1;

            opeValuesActorsflujos = 0;
        }
        else {
            if (opevaluesActorsdesembolso < opeValuesActorsflujos) {
                alert("el valor ingresado no debe superar al desembolso disponible");
                error_actor = 1;
                swhich_validar_estado_1 = 1;
                opeValuesActorsflujos = 0;

            }
            else {
                if (opevaluesActorslimit == opevaluesActorsdesembolso) {
                    totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
                    $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

                }
                else {
                    totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
                    $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

                }
            }
        }
    });
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
        }
        else {

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
        }
        else {
            $("#ctl00_cphPrincipal_Lblinformationexist").text("");
            $("#ctl00_cphPrincipal_txtporcentaje").removeAttr("disabled");
            $("#ctl00_cphPrincipal_txtfechapago").removeAttr("disabled");

        }
    });

    // calcular campo valor despues de salir del foco de porcentaje
    $("#ctl00_cphPrincipal_txtporcentaje").focusout(function() {

        var porc = $("#ctl00_cphPrincipal_txtporcentaje").val();
        //calcula el porcentaje
        porc = Math.round(porc * 10) / 10;
        $("#ctl00_cphPrincipal_txtporcentaje").val(porc);

        var txtvalortotalflow = $("#tflujosing").text();

        valortotalflow = txtvalortotalflow.replace(/\./gi, '');
        var valortotalflow = parseInt(valortotalflow);

        //realiza la operacion del porcentaje seleccionado
        var parcial = (parseFloat(porc) * parseFloat(valortotalflow)) / 100;
        parcial = Math.round(parcial);

        //parcial = numeral(parcial).format('0,0');
        var valcomas = addCommasrefactor(parcial);


        $("#ctl00_cphPrincipal_Lbltotalvalor").text(valcomas);

        if (s_revisarflujos == 1) {

            var idflujos = arrayActorFlujo[0].actorsVal;

            valuecomparative = valcomas;

            $("#txtinput" + idflujos).val(valuecomparative);
            $("#totalflujos").text(valuecomparative);
        }


    });

    //Validar que el porcentaje no supere el 100 por ciento, no tenga comas ni tenga mas de 2 decimas
    $("#ctl00_cphPrincipal_txtporcentaje").change(function() {
        var expresion = /(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9])?$)/

        if (!expresion.test($("#ctl00_cphPrincipal_txtporcentaje").val())) {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("El porcentaje debe ser menor o igual a 100");
            $("#ctl00_cphPrincipal_txtporcentaje").val("");
            $("#ctl00_cphPrincipal_txtporcentaje").focus();
        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("");

        }
        if ($("#ctl00_cphPrincipal_txtporcentaje").val() == 0) {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("El porcentaje debe ser mayor a 0");
            $("#ctl00_cphPrincipal_txtporcentaje").val("");
            $("#ctl00_cphPrincipal_txtporcentaje").focus();
        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("");
        }

    });
}
