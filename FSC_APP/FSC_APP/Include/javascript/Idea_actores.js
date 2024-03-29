
//cargar combo actores
function Cactors() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_Actors" },
        success: function(result) {
            $("#ddlactors").html(result);
            $("#ddlactors").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los actores.");
        }
    });
}

//agregar ubicaciones por el metodo de tablas html
function BtnaddActors_onclick() {

    //inicializamos las variables
    var valdiner = 0;
    var valespecie = 0;
    var valtotal = 0;
    var valdinergrid = 0;
    var valespeciegrid = 0;
    var valtotalgrid = 0;
    var valdinergridfsc = 0;
    var valespeciegridfsc = 0;
    var valtotalgridfsc = 0;

    $("#ctl00_cphPrincipal_txtvalortotalflow").val("");
    $("#ctl00_cphPrincipal_txtfechapago").val("");
    $("#ctl00_cphPrincipal_txtporcentaje").val("");
    $("#ctl00_cphPrincipal_Lbltotalvalor").text("");
    $("#ctl00_cphPrincipal_txtentregable").val("");

    //capturamos si se va agregar a fuljos de pagos o no
    var flujo_in = $("#ctl00_cphPrincipal_RBListflujo :checked").val();

    //validamos si va recibir aporte en efectivo 
    if (flujo_in == 1) {

        //validamos si el array de flujo actores ya tiene flujos generados
        if (swhich_flujos_exist == 1) {

            alert("Se ha detectado información el la pestaña de flujos de pagos, al eliminar el actor toda la información se perdera!");

            $("#Btn_add_flujo").removeAttr("disabled");

            var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";
            htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

            //cargamos el div donde se generara la tabla actores
            $("#T_flujosContainer").html("");
            $("#T_flujosContainer").html(htmlTableflujos);

            arrayValorflujoTotal[0] = 0;

            arrayflujosdepago = [];
            arrayinputflujos = [];
            matriz_flujos = [];
            reversedesembolsos = [];

            swhich_flujos_exist = 0;


            //reconstruimos la tabla con los datos
            $("#T_flujos").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });
        }
    }
    //funcion para agregar actor
    add_actor_grid();

}

//funtion para agreagar al grid despues de validar si hay flujos de datos
function add_actor_grid() {

    if ($("#ctl00_cphPrincipal_RBListflujo :checked").val() == null) {
        $("#ctl00_cphPrincipal_Lblflujosinf").text("Escoja si o no");
    }
    else {
        $("#ctl00_cphPrincipal_Lblflujosinf").text("");
        //validamos si el combo actor este selecionado
        if ($("#ddlactors :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_Lblactorrep").text("debe seleccionar almenos un actor");
        }
        else {
            $("#ctl00_cphPrincipal_Lblactorrep").text("");

            //capturamos los valores de deseados
            var actorsVal = $("#ddlactors").val();
            var actorsName = $("#ddlactors :selected").text();
            var tipoactors = $("#ctl00_cphPrincipal_ddlType :selected").text();
            var contact = $("#ctl00_cphPrincipal_Txtcontact").val();
            var cedula = $("#ctl00_cphPrincipal_Txtcedulacont").val();
            var telefono = $("#ctl00_cphPrincipal_Txttelcont").val();
            var email = $("#ctl00_cphPrincipal_Txtemail").val();

            var totalconsulta = suma_verificar($("#ctl00_cphPrincipal_Txtvrdiner").val(), $("#ctl00_cphPrincipal_Txtvresp").val());


            if ($("#ctl00_cphPrincipal_Txtvrdiner").val() == "") {
                var diner = 0;
            }
            else {
                var diner = $("#ctl00_cphPrincipal_Txtvrdiner").val();
            }

            if ($("#ctl00_cphPrincipal_Txtvresp").val() == "") {
                var especie = 0;
            }
            else {
                var especie = $("#ctl00_cphPrincipal_Txtvresp").val();
            }

            if ($("#ctl00_cphPrincipal_Txtaportfscocomp").val() == "") {
                var total = totalconsulta;
            }
            else {
                var total = $("#ctl00_cphPrincipal_Txtaportfscocomp").val();
            }

            var flujo_in = $("#ctl00_cphPrincipal_RBListflujo :checked").val();


            if (flujo_in == 1) {
                var estado_flujo = "s";
            }
            else {
                var estado_flujo = "n";
            }

            //creamos json para guardarlos en un array
            var jsonActor = { "actorsVal": actorsVal, "actorsName": actorsName, "tipoactors": tipoactors, "contact": contact, "cedula": cedula, "telefono": telefono, "email": email, "diner": diner, "especie": especie, "total": total, "estado_flujo": estado_flujo };

            //recorremos el array para revisar repetidos        
            var validerepetido = 0;
            for (iArray in arrayActor) {
                if (actorsVal == arrayActor[iArray].actorsVal) {
                    validerepetido = 1;
                }
            }

            //validamos si hay repetidos 
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_Lblactorrep").text("El actor ya fue ingresado");
            }
            else {
                $("#ctl00_cphPrincipal_Lblactorrep").text("");

                //cargamos el array con el json
                if (flujo_in == 1) {
                    arrayActorFlujo.push(jsonActor);
                }
                //cargamos el array con el json
                arrayActor.push(jsonActor);
                //llama la funcion crear la tabla de grid principal
                crear_tabla_inf_prin();
                //llama la funcion crear la tabla de actores
                crear_tabla_actores();
                //llama la funcion crear la tabla de flujo_actor
                crear_tabla_flujo_actor();

                //limpiamos los campos para empesar el ciclo de nuevo
                $("#ctl00_cphPrincipal_Txtcontact").val("");
                $("#ctl00_cphPrincipal_Txtcedulacont").val("");
                $("#ctl00_cphPrincipal_Txttelcont").val("");
                $("#ctl00_cphPrincipal_Txtemail").val("");
                $("#ctl00_cphPrincipal_Txtvrdiner").val("");
                $("#ctl00_cphPrincipal_Txtvresp").val("");
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val("");
                $("#ddlactors").val("-1");
                $("#ddlactors").trigger("liszt:updated");
            }
        }
    }
}


//creamos la tabla matrizde informacion principal
function crear_tabla_inf_prin() {

    var htmltablamatriz = "<table id='matriz' border='1' cellpadding='1' cellspacing='1' style='width: 100%'><thead><tr><th width='1'></th><th></th><th>Efectivo</th><th>Especie</th><th>Total</th></tr></thead><tbody>";

    for (itemArray in arrayActor) {
        htmltablamatriz += "<tr id= 'matriz" + arrayActor[itemArray].actorsVal + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActor[itemArray].actorsVal + "</td><td style='text-align: left'>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].diner + "</td><td> " + arrayActor[itemArray].especie + "</td><td> " + arrayActor[itemArray].total + " </td></tr>";
    }

    htmltablamatriz += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Valor Total</td><td id='valueMoneytotal'>0</td><td id='ValueEspeciestotal'>0</td><td id='ValueCostotal'>0</td></tr></tbody></table>";

    $("#T_matrizcontainer").html("");
    $("#T_matrizcontainer").html(htmltablamatriz);

    //llamar la funcion suma de primera columna efectivo
    sumavalores_gridprincipal();

    //reconstruimos la tabla con los datos
    $("#matriz").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
}

//creamos la tabla de actores
function crear_tabla_actores() {

    var htmlTableActores = "<table id='T_Actors' align='center' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th width='1'></th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Total</th><th>Eliminar</th></tr></thead><tbody>";

    for (itemArray in arrayActor) {
        htmlTableActores += "<tr id='actor" + arrayActor[itemArray].actorsVal + "' ><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActor[itemArray].actorsVal + "</td><td>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].tipoactors + "</td><td>" + arrayActor[itemArray].contact + "</td><td>" + arrayActor[itemArray].cedula + "</td><td>" + arrayActor[itemArray].telefono + "</td><td>" + arrayActor[itemArray].email + "</td><td>" + arrayActor[itemArray].diner + "</td><td>" + arrayActor[itemArray].especie + "</td><td>" + arrayActor[itemArray].total + "</td><td><input type ='button'class= 'deleteActor' value= 'Eliminar' onclick=\"deleteActor('" + arrayActor[itemArray].actorsVal + "')\"></input></td></tr>";
    }

    //se anexa columna para totales
    htmlTableActores += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id='val1'></td><td id='val2'>0</td><td id='val3'>0</td><td></td></tr></tbody></table>";

    //cargamos el div donde se generara la tabla actores
    $("#T_ActorsContainer").html("");
    $("#T_ActorsContainer").html(htmlTableActores);

    //agregamos atributos de eliminar fila
    $(".deleteActor").click(function() {
    });

    //llama la funcion sumar en la grilla de actores
    sumar_grid_actores();

    //reconstruimos la tabla con los datos 
    $("#T_Actors").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
}


function View_actores_array() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "View_actores_array", "ididea": ideditar },
        success: function(result) {

            if (result == "") {
                arrayActor = [];
            }
            else {
                arrayActor = JSON.parse(result);
            }

            //llamamos funcion crea la tabla del descripcion de proyecto
            crear_tabla_inf_prin();
            //llama la funcion crear la tabla de actores
            crear_tabla_actores();

        },
        error: function(msg) {
            alert("No se pueden cargar los actores en general de la idea = " + ideditar);
        }
    });
}

//borrar de la grilla html de actores
function deleteActor(str) {

    if (swhich_flujos_exist == 1) {
        validar_exist_flujos();
    }

    var idactor = "#actor" + str;
    $(idactor).remove();

    var idflujo = "#flujo" + str;
    $(idflujo).remove();

    var idmatriz = "#matriz" + str;
    $(idmatriz).remove();

    //recorremos el array
    for (itemArray in arrayActor) {
        //construimos la llave de validacion
        var id = arrayActor[itemArray].actorsVal;
        //validamos el dato q nos trae la funcion

        if (str == id) {
            //borramos el actor deseado
            //delete arrayActor[itemArray];
            arrayActor.splice(itemArray, 1);
        }
    }
    //recorremos el array
    for (itemArrayflujo in arrayActorFlujo) {
        //construimos la llave de validacion
        var idflujo = arrayActorFlujo[itemArrayflujo].actorsVal;
        //validamos el dato q nos trae la funcion

        if (str == idflujo) {
            //borramos el actor deseado
            //delete arrayActorFlujo[itemArrayflujo];
            arrayActorFlujo.splice(itemArrayflujo, 1);
        }
    }

    //lamar la funcionsumar actores
    sumar_grid_actores();
    //llamar la funcion suma de grid principal
    sumavalores_gridprincipal();
    //llamar la funcion sumar flujos actores
    sumar_flujos_actores();

    recalcValues();
    $("#Btn_add_flujo").removeAttr("disabled");

}

//valida si se han ingresado flujos de pago y los reinicia
function validar_exist_flujos() {

    alert("Se ha detectado información el la pestaña de flujos de pagos, al eliminar el actor toda la información se perdera!");

    $("#totalflujos").text(0);
    //recorremos la tabla de flujo de pagos
    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

        arrayinputflujos = $(this).find("td").slice(0, 1);

        if ($(arrayinputflujos[0]).html() != null) {
            var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();
            $(idflujo).val("");
        }
    });

    var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";
    htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

    //cargamos el div donde se generara la tabla actores
    $("#T_flujosContainer").html("");
    $("#T_flujosContainer").html(htmlTableflujos);

    arrayValorflujoTotal[0] = 0;

    arrayflujosdepago = [];
    arrayinputflujos = [];
    matriz_flujos = [];
    reversedesembolsos = [];

    swhich_flujos_exist = 0;

    //reconstruimos la tabla con los datos
    $("#T_flujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}

//funcion para la suma de valoes en el grid de actores
function sumar_grid_actores() {

    //inicializamos las variables
    var valdiner = 0;
    var valdinerflujos = 0;
    var valespecie = 0;
    var valtotal = 0;

    //recorremos la tabla actores para calcular los totales
    $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {
        var arrayValuesActors = $(this).find("td").slice(7, 10);
        //validamos si hay campos null en la tabla actores
        if ($(arrayValuesActors[0]).html() != null) {

            //capturamos e incrementamos los valores para la suma
            valdiner = valdiner + parseInt($(arrayValuesActors[0]).html().replace(/\./gi, ''));

            valespecie = valespecie + parseInt($(arrayValuesActors[1]).html().replace(/\./gi, ''));
            valtotal = valtotal + parseInt($(arrayValuesActors[2]).html().replace(/\./gi, ''));
            //validamos valores si vienen vacios
            if (isNaN(valdiner)) {
                valdiner = 0;
            }
            if (isNaN(valespecie)) {
                valespecie = 0;
            }
            if (isNaN(valtotal)) {
                valtotal = 0;
            }

            //cargamos los campos con la operacion realizada
            $("#val1").text(addCommasrefactor(valdiner));
            $("#val2").text(addCommasrefactor(valespecie));
            $("#val3").text(addCommasrefactor(valtotal));
        }
        else {
            $("#val1").text(0);
            $("#val2").text(0);
            $("#val3").text(0);
        }
    });
}


//funcion que sumas cuando agregan un actor desde el boton
function suma_verificar(strdiner, strespecies) {

    var suma = 0;

    // alert(strdiner + " _ " + strespecies);
    var rev = strdiner;
    rev = rev.replace(/\./gi, '');
    var vd = parseInt(rev);

    var rev2 = strespecies;
    rev2 = rev2.replace(/\./gi, '');
    var ve = parseInt(rev2);

    if (isNaN(vd)) {
        vd = 0;
        $("#ctl00_cphPrincipal_Txtvrdiner").val(vd);
    }
    else {
        if (isNaN(ve)) {
            ve = 0;
            $("#ctl00_cphPrincipal_Txtvresp").val(ve);
        }
        else {

            suma = vd + ve;
            $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));
        }
    }
    suma = vd + ve;
    $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));

    return suma;
}


function actors_transanccion() {

    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvrdiner").blur(function() {

        var suma = 0;

        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var valor = parseInt(rev);

        if (isNaN(valor)) {
            valor = 0;
            $("#ctl00_cphPrincipal_Txtvrdiner").val(valor);


        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvresp").val();

            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);

            if (isNaN(val2)) {
                val2 = 0;
                suma = valor + val2;
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));
            }
            else {

                suma = valor + val2;
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));
            }
        }
    });

    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvresp").blur(function() {

        var suma = 0;

        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var valor = parseInt(rev);
        if (isNaN(valor)) {
            valor = 0;
            $("#ctl00_cphPrincipal_Txtvresp").val(valor);

        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvrdiner").val();
            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);
            if (isNaN(val2)) {
                val2 = 0;
                suma = valor + val2;
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));
            }
            else {
                suma = valor + val2;
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val(addCommasrefactor(suma));

            }
        }
    });


    //  FUNCION PARA EL MONTAJE DE VENTANAS EMERGENTES CON PRETTY PHOTO Y ACTUALIZACION DE COMBO DE TERCEROS EN IDEA 
    //10-06-2013 GERMAN RODRIGUEZ
    $("a.pretty").prettyPhoto({
        callback: function() {
            $.ajax({
                url: "ajaxaddidea_drop_list_third.aspx",
                type: "GET",
                data: { "action": "cargarthird" },
                success: function(result) {
                    $("#ddlactors").html(result);
                    $("#ddlactors").trigger("liszt:updated");
                },
                error: function()
                { alert("los datos de terceros no pudieron ser cargados."); }
            });
        }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        modal: true,
        social_tools: false
    });

}

//funcion q treae los daots del actor seleccionado
function comboactor() {
    $("#ddlactors").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "buscar", "id": $(this).val() },
            success: function(result) {
                result = JSON.parse(result);

                $("#ctl00_cphPrincipal_Txtcontact").val(result.contact);
                $("#ctl00_cphPrincipal_Txtcedulacont").val(result.documents);
                $("#ctl00_cphPrincipal_Txttelcont").val(result.phone);
                $("#ctl00_cphPrincipal_Txtemail").val(result.email);
                $("#ctl00_cphPrincipal_HDIDTHIRD").val(result.idthird);
                $("#ctl00_cphPrincipal_HDNAMETHIRD").val(result.name);
                $("#ctl00_cphPrincipal_lblavertenactors").text("");

            },
            error: function()
            { alert("los datos de terceros no pudieron ser cargados."); }
        });
    });
}
