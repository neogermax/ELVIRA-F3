
//Javascript para modulo de Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

//Funcion perteneciente a el evento onload del elemento body

var arrayUbicacion = [];
var arrayActor = [];
var arrayActorFlujo = [];
var arraycomponenteing = [];
var arraycomponente = [];
var arraycomponentedesechado = [];
var arrayValorflujoTotal = [];
var arrayinputflujos = [];
var arrayflujosdepago = [];
var matriz_flujos = [];
var reversedesembolsos = [];
var arrayFiles = [];

var filescharge = [];
var args = [];
var valI1;
var valI2;
var valI3;
var idfile;
var switch_editar = 0;
var swhich_validar_estado_1 = 0;
var s_revisarflujos = 0;

var idfile;
var S_eliminar;

//Array.prototype.remove = function(from, to) {
//    var rest = this.slice((to || from) + 1 || this.length);
//    this.length = from < 0 ? this.length + from : from;
//    return this.push.apply(this, rest);
//};


$(document).ready(function() {


    operacionesIdea();
    comboactor();
    var timer = setTimeout("fix();", 2000);
    validafecha();
    validafecha2();
    separarvaloresFSC();

    ClineEstrategic();
    Cprogram();
    Cdeptos();
    Cmunip();
    Cactors();
    CtypeContract();
    cargarcomponente();
    startdate();
    Ctype_project();
    Cpopulation();
    validarporcentaje();

    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");
    //$("#tabsIdea").tabs();

    $("#matriz").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_location").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_Actors").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
    $("#T_flujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_files").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_Actorsflujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_detalle_desembolso").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#SaveIdea").button();
    $("#B_add_location").button();
    $("#BtnaddActors").button();
    $("#Btn_add_flujo").button();
    $("#Btnaddcomponent").button();
    $("#Btndeletecomponent").button();
    $("#ctl00_cphPrincipal_linkactors").button();
    $("#close_dialog").button();
    $("#Btncharge_file").button();
    $("#fileupload").button();



    //generar el la ventana emergente
    $("#dialog").dialog({
        modal: true,
        minWidth: 700,
        closeOnEscape: true,
        autoOpen: false
    });



    //validar el option buton del iva
    $("#ctl00_cphPrincipal_RBnList_iva").click(function() {

        var option_iva = $("#ctl00_cphPrincipal_RBnList_iva :checked").val();

        if (option_iva == 1) {
            $("#ctl00_cphPrincipal_vrdiner").text("Vr Dinero  (Recuerde incluir los valores con IVA)");
        }
        else {
            $("#ctl00_cphPrincipal_vrdiner").text("Vr Dinero");
        }

    });

    $("#tabsIdea").tabs();

    var entradaflujos = 0;
    //validar que pestaña esta ingresando
    $("#tabsIdea").click(function() {

        var active = $("#tabsIdea").tabs("option", "active");
        var idtabs = $("#tabsIdea ul>li a").eq(active).attr('href');
        //validar si esl la de flujos de pago
        if (idtabs == "#flujos") {
            //validar si es la primera entrada       
            if (entradaflujos == 0) {
                var tamaño_flujos = $("#T_Actorsflujos tr").length - 2;
                //validar la cantidad de actores
                if (tamaño_flujos == 1) {
                    var Aflujos = arrayActorFlujo[itemarrayflujos].actorsVal;
                    $("#txtinput" + Aflujos).attr("disabled", "disabled");
                    entradaflujos = 1;
                    s_revisarflujos = 1;
                }
            }
            // $("#tabsIdea").tabs({ active: 4});
        }
        else { entradaflujos = 0; }
    });


});


//funcion para dispara en el autoload fuciones de fechas
function fix() {

    if ($.trim($("#ctl00_cphPrincipal_Txtday").val()).length > 0) {
        var timer = setTimeout("fix();", 2000);
        $("#ctl00_cphPrincipal_Txtday").trigger("blur");
        clearTimeout(timer);
    }

    if ($.trim($("#ctl00_cphPrincipal_txtduration").val()).length > 0) {
        var timer = setTimeout("fix();", 2000);
        $("#ctl00_cphPrincipal_txtduration").trigger("blur");
        clearTimeout(timer);
    }
}

//funcion de refactorizacion idea fase 3 ---- autor:German Rodriguez MGgroup

//--------------------------------------------------------------------------crear idea-----------------------------------------------------------------------------------------------
function SaveIdea_onclick() {

    var fsc_exist = 0;
    //validar si las pestañas ubicacion, actores estan diligenciadas
    if (arrayActor.length == 0 || arrayUbicacion.length == 0) {
        if (arrayActor.length == 0 && arrayUbicacion.length != 0) {
            $("#ctl00_cphPrincipal_Lblactorrep").text("Debe almenos tener un actor");
            $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña actores");
        }
        if (arrayActor.length != 0 && arrayUbicacion.length == 0) {
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("Debe tener almenos una ubicación");
            $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña ubicación");
        }
        if (arrayActor.length == 0 && arrayUbicacion.length == 0) {
            $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña actores y ubicaciones");
            $("#ctl00_cphPrincipal_Lblactorrep").text("Debe almenos tener un actor");
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("Debe tener almenos una ubicación");
        }
    }

    else {
        //recorrer actores revisando si la FSC fue ingresada
        for (iArray in arrayActor) {
            if (4 == arrayActor[iArray].actorsVal) {
                fsc_exist = 1;
            }
        }
        //validar si un componente fue ingresado
        var textoLista = $("#componentesseleccionados").html();

        if (textoLista == "") {
            $("#ctl00_cphPrincipal_Lblinformationcomponent").text("Debe tener almenos un componente");
            $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña componentes");

        }
        else {
            $("#ctl00_cphPrincipal_Lblinformationcomponent").text("");
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("");
            $("#ctl00_cphPrincipal_Lblactorrep").text("");

            //validar si los campos obligatorios fueron dilegenciados
            if ($("#ddlPupulation :selected").text() == 'Seleccione...' || $("#ddlmodcontract :selected").text() == 'Seleccione...' || $("#ddlStrategicLines :selected").text() == 'Seleccione...' || $("#ddlPrograms :selected").text() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '' || $("#ctl00_cphPrincipal_Txtdatecierre").val() == '' || arrayUbicacion.length == 0 || fsc_exist == 0) {

                //validar linea estrategica
                if ($("#ddlStrategicLines :selected").text() == 'Seleccione...') {
                    $("#ctl00_cphPrincipal_lblinfls").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblinfls").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar programas
                if ($("#ddlPrograms :selected").text() == 'Seleccione...') {
                    $("#ctl00_cphPrincipal_lblinpro").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblinpro").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar tipo de proyectos
                //                if ($("#ddltype_proyect :selected").text() == 'Seleccione...') {
                //                    $("#ctl00_cphPrincipal_Lblhelptproyect").text("Campo Requerido");
                //                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                //                }
                //                else {
                //                    $("#ctl00_cphPrincipal_Lblhelptproyect").text("");
                //                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                //                }
                //validar poblacion
                if ($("#ddlPupulation :selected").text() == 'Seleccione...') {
                    $("#ctl00_cphPrincipal_lblHelppopulation").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelppopulation").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar tipo de contratacion
                if ($("#ddlmodcontract :selected").text() == 'Seleccione....') {
                    $("#ctl00_cphPrincipal_Lblmodcontract").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_Lblmodcontract").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar nombre
                if ($("#ctl00_cphPrincipal_txtname").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpname").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelpname").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar justificacion
                if ($("#ctl00_cphPrincipal_txtjustification").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpjustification").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelpjustification").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validarobjetivos
                if ($("#ctl00_cphPrincipal_txtobjective").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpobjective").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelpobjective").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar fecha de inicio
                if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpstartdate").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar duracion en dias
                if ($("#ctl00_cphPrincipal_Lbldateend").val() == '') {
                    $("#ctl00_cphPrincipal_Lblhelpenddate").text("Campo Requerido");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_Lblhelpenddate").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }
                //validar  si la FSC fue ingresada
                if (fsc_exist == 1) {
                    $("#ctl00_cphPrincipal_Lblactorrep").text("");
                }
                else {
                    $("#ctl00_cphPrincipal_Lblactorrep").text("La idea no guardo con exito: la FSC debe ser un actor obligatorio");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: la FSC debe ser un actor obligatorio");
                }
            }

            else {

                //crear arrays para el ingreso de las listas de json
                var listubicaciones = [];
                var listactores = [];
                var Str_listcomponentes = $("#componentesseleccionados").html();

                $("#ctl00_cphPrincipal_lblinfls").val("");
                $("#ctl0_cphPrincipal_lblinpro").val("");
                $("#ctl00_cphPrincipal_lblHelpname").text("");
                $("#ctl00_cphPrincipal_lblHelpjustification").text("");
                $("#ctl00_cphPrincipal_lblHelpobjective").text("");
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
                $("#ctl00_cphPrincipal_lbldia").text("");

                //recorer array para el ingreso de ubicaciones
                for (item in arrayUbicacion) {
                    listubicaciones.push(JSON.stringify(arrayUbicacion[item]));
                }
                //recorer array para el ingreso de actores
                for (item in arrayActor) {
                    listactores.push(JSON.stringify(arrayActor[item]));
                }

                //recorer array para el ingreso de componentes
             


                //crear comunicacion ajax para el ingreso de los datos de la idea
                $.ajax({
                    url: "AjaxAddIdea.aspx",
                    type: "GET",
                    //crear json
                    data: { "action": "save", "code": $("#ctl00_cphPrincipal_txtcode").val(),
                        "linea_estrategica": $("#ddlStrategicLines").val(),
                        "programa": $("#ddlPrograms").val(),
                        "nombre": $("#ctl00_cphPrincipal_txtname").val(),
                        "justificacion": $("#ctl00_cphPrincipal_txtjustification").val(),
                        "objetivo": $("#ctl00_cphPrincipal_txtobjective").val(),
                        "objetivo_esp": $("#ctl00_cphPrincipal_txtareadescription").val(),
                        "Resultados_Benef": $("#ctl00_cphPrincipal_txtresults").val(),
                        "Resultados_Ges_c": $("#ctl00_cphPrincipal_txtresulgc").val(),
                        "Resultados_Cap_i": $("#ctl00_cphPrincipal_txtresulci").val(),
                        "Fecha_inicio": $("#ctl00_cphPrincipal_txtstartdate").val(),
                        "mes": $("#ctl00_cphPrincipal_txtduration").val(),
                        "dia": $("#ctl00_cphPrincipal_Txtday").val(),
                        "Fecha_fin": $("#ctl00_cphPrincipal_Txtdatecierre").val(),
                        "Población": $("#ddlPupulation").val(),
                        "contratacion": $("#ddlmodcontract").val(),
                        "A_Mfsc": $("#ValueMoneyFSC").val(),
                        "A_Efsc": $("#ValueEspeciesFSC").val(),
                        "A_Mcounter": $("#ValueMoneyCounter").val(),
                        "A_Ecounter": $("#ValueEspeciesCounter").val(),
                        "cost": $("#ValueCostotal").val(),
                        "obligaciones": $("#ctl00_cphPrincipal_Txtobligationsoftheparties").val(),
                        "riesgo": $("#ctl00_cphPrincipal_Txtriesgos").val(),
                        "mitigacion": $("#ctl00_cphPrincipal_Txtaccionmitig").val(),
                        "presupuestal": $("#ctl00_cphPrincipal_Txtroutepresupuestal").val(),
                        "iva": $("#ctl00_cphPrincipal_RBnList_iva :checked").val(),
                      //  "listcomponentes": Str_listcomponentes.toString(),
                        "listubicaciones": listubicaciones.toString(),
                        "listactores": listactores.toString()
                        
                    },
                    //mostrar resultados de la creacion de la idea
                    success: function(result) {
                        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                        $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
                        $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                        $("#ddlStrategicLines").focus();
                    },
                    error: function() {
                        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                        $("#ctl00_cphPrincipal_lblsaveinformation").text("Se genero error al entrar a la operacion Ajax");
                    }
                });
            }
        }
    }
}

//agregar ubicaciones por el metodo de tablas html
function Add_location_onclick() {

    $("#ctl00_cphPrincipal_Lblinfubicacion").text("");

    //validamos si el combo departamento este selecionado
    if ($("#ddlDepto :selected").text() == 'Seleccione...') {
        $("#ctl00_cphPrincipal_LblubicacionRep").text("Debe seleccionar almenos un departamento");
    }
    else {
        //validamos si el combo municipio este selecionado
        if ($("#ddlCity :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_LblubicacionRep").text("");
            $("#ctl00_cphPrincipal_LblubicacionRep").text("Debe seleccionar almenos un municipio");
        }
        else {

            $("#ctl00_cphPrincipal_LblubicacionRep").text("");

            //capturamos los valores de los combos
            var deptoVal = $("#ddlDepto").val();
            var deptoName = $("#ddlDepto :selected").text();
            var cityVal = $("#ddlCity").val();
            var cityName = $("#ddlCity :selected").text();

            //creamos json para guardarlos en un array
            var jsonUbicacion = { "DeptoVal": deptoVal, "DeptoName": deptoName, "CityVal": cityVal, "CityName": cityName };

            //recorremos el array para revisar repetidos
            var validerepetido = 0;
            for (iArray in arrayUbicacion) {
                if (deptoName == arrayUbicacion[iArray].DeptoName && cityName == arrayUbicacion[iArray].CityName) {
                    validerepetido = 1;
                }
            }

            //validamos si hay repetidos 
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_LblubicacionRep").text("La ubicación ya fue ingresada");
            }
            else {
                $("#ctl00_cphPrincipal_LblubicacionRep").text("");

                //cargamos el array con el json
                arrayUbicacion.push(jsonUbicacion);

                //creamos la tabla de ubicaciones

                var htmlTable = "<table id='T_location' border='2' cellpadding='2' cellspacing='2' style='width: 100%;'><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead><tbody>";

                for (itemArray in arrayUbicacion) {
                    var strdelete = arrayUbicacion[itemArray].DeptoName + "_" + arrayUbicacion[itemArray].CityName;
                    htmlTable += "<tr><td>" + arrayUbicacion[itemArray].DeptoName + "</td><td>" + arrayUbicacion[itemArray].CityName + "</td><td><input type ='button' class= 'deleteUbicacion' value= 'Eliminar' onclick='deleteUbicacion(/" + strdelete + "/)' ></input></td></tr>";
                }
                htmlTable += "</tbody></table>";

                //cargamos el div donde se generara la tabla
                $("#T_locationContainer").html("");
                $("#T_locationContainer").html(htmlTable);

                //agregamos atributos de eliminar fila
                $(".deleteUbicacion").click(function() {
                    $(this).parent().parent().remove();
                });

                //reconstruimos la tabla con los datos 
                $("#T_location").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });
            }
        }
    }
}


//borrar de la grilla html de ubicaciones 
function deleteUbicacion(str) {
    //recorremos el array
    for (itemArray in arrayUbicacion) {
        //construimos la llave de validacion
        var id = "/" + arrayUbicacion[itemArray].DeptoName + "_" + arrayUbicacion[itemArray].CityName + "/";
        //validamos el dato q nos trae la funcion
        if (str == id) {
            //borramos la ubicacion deseada
            delete arrayUbicacion[itemArray];
            //arrayUbicacion.splice(arrayUbicacion[itemArray].CityName, 1);
        }
    }
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
                var total = 0;
            }
            else {
                var total = $("#ctl00_cphPrincipal_Txtaportfscocomp").val();
            }



            //creamos json para guardarlos en un array
            var jsonActor = { "actorsVal": actorsVal, "actorsName": actorsName, "tipoactors": tipoactors, "contact": contact, "cedula": cedula, "telefono": telefono, "email": email, "diner": diner, "especie": especie, "total": total };

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


                var flujo_in = $("#ctl00_cphPrincipal_RBListflujo :checked").val();

                //cargamos el array con el json
                if (flujo_in == 1) {
                    arrayActorFlujo.push(jsonActor);
                }
                //cargamos el array con el json
                arrayActor.push(jsonActor);

                //creamos la tabla de actores
                var htmlTableActores = "<table id='T_Actors' align='center' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th width='1'></th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Total</th><th>Eliminar</th></tr></thead><tbody>";
                //creamos la tabla de flujo actores
                if (flujo_in == 1) {
                    var htmltableAflujos = "<table id='T_Actorsflujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th width='1'></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>";
                }
                //creamos la tabla matrizde informacion principal
                var htmltablamatriz = "<table id='matriz' border='1' cellpadding='1' cellspacing='1' style='width: 100%'><thead><tr><th width='1'></th><th></th><th>Efectivo</th><th>Especie</th><th>Total</th></tr></thead><tbody>";

                for (itemArray in arrayActor) {
                    htmlTableActores += "<tr id='actor" + arrayActor[itemArray].actorsVal + "' ><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActor[itemArray].actorsVal + "</td><td>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].tipoactors + "</td><td>" + arrayActor[itemArray].contact + "</td><td>" + arrayActor[itemArray].cedula + "</td><td>" + arrayActor[itemArray].telefono + "</td><td>" + arrayActor[itemArray].email + "</td><td>" + arrayActor[itemArray].diner + "</td><td>" + arrayActor[itemArray].especie + "</td><td>" + arrayActor[itemArray].total + "</td><td><input type ='button' value= 'Eliminar' onclick=\"deleteActor('" + arrayActor[itemArray].actorsVal + "')\"></input></td></tr>";
                    htmltablamatriz += "<tr id= 'matriz" + arrayActor[itemArray].actorsVal + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActor[itemArray].actorsVal + "</td><td style='text-align: left'>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].diner + "</td><td> " + arrayActor[itemArray].especie + "</td><td> " + arrayActor[itemArray].total + " </td></tr>";

                }
                //creamos ciclo para los actores que si tienen flujo de pago
                for (itemarrayflujos in arrayActorFlujo) {
                    if (flujo_in == 1) {
                        htmltableAflujos += "<tr id='flujo" + arrayActorFlujo[itemarrayflujos].actorsVal + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActorFlujo[itemarrayflujos].actorsVal + "</td><td>" + arrayActorFlujo[itemarrayflujos].actorsName + "</td><td id= 'value" + arrayActorFlujo[itemarrayflujos].actorsVal + "' >" + arrayActorFlujo[itemarrayflujos].diner + "</td><td><input id='" + "txtinput" + arrayActorFlujo[itemarrayflujos].actorsVal + "' onkeyup='formatvercionsuma(this)' onchange='formatvercionsuma(this)'  onblur=\"sumar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\" onfocus=\"restar_flujos('" + arrayActorFlujo[itemarrayflujos].actorsVal + "')\"></input></td><td id='desenbolso" + arrayActorFlujo[itemarrayflujos].actorsVal + "'>" + arrayActorFlujo[itemarrayflujos].diner + "</td></tr>";
                    }
                }
                //se anexa columna para totales
                htmlTableActores += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id='val1'></td><td id='val2'>0</td><td id='val3'>0</td><td></td></tr></tbody></table>";

                if (flujo_in == 1) {
                    htmltableAflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td id='tflujosing'></td><td id='totalflujos'>0</td></td id='tflujosdesen'><td></tr></tbody></table>";
                }
                htmltablamatriz += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Valor Total</td><td id='valueMoneytotal'>0</td><td id='ValueEspeciestotal'>0</td><td id='ValueCostotal'>0</td></tr></tbody></table>";

                //cargamos el div donde se generara la tabla actores
                $("#T_ActorsContainer").html("");
                $("#T_ActorsContainer").html(htmlTableActores);

                if (flujo_in == 1) {
                    //cargamos el div donde se generara la tabla flujo de actores
                    $("#T_AflujosContainer").html("");
                    $("#T_AflujosContainer").html(htmltableAflujos);
                }

                $("#T_matrizcontainer").html("");
                $("#T_matrizcontainer").html(htmltablamatriz);


                //llama la funcion sumar en la grilla de actores
                sumar_grid_actores();

                //llama la funcion de buscar la FSC 
                // buscarFSC();

                //llamar la funcion de buscar diferentes a la FSC
                // buscarothers();

                //llamar la funcion suma de primera columna efectivo
                sumavalores_gridprincipal();

                //llamar la funcion suma segunda columna especie
                //sumaespecie_gridprincipal();

                //llamar la funcion suma tercera columna total
                //sumatotal_gridprincipal();

                //reconstruimos la tabla con los datos 
                $("#T_Actors").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });

                if (flujo_in == 1) {
                    //reconstruimos la tabla con los datos 
                    $("#T_Actorsflujos").dataTable({
                        "bJQueryUI": true,
                        "bDestroy": true
                    });
                }
                //reconstruimos la tabla con los datos
                $("#matriz").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });
                //limpiamos los campos para empesar el ciclo de nuevo
                $("#ctl00_cphPrincipal_Txtcontact").val("");
                $("#ctl00_cphPrincipal_Txtcedulacont").val("");
                $("#ctl00_cphPrincipal_Txttelcont").val("");
                $("#ctl00_cphPrincipal_Txtemail").val("");
                $("#ctl00_cphPrincipal_Txtvrdiner").val("");
                $("#ctl00_cphPrincipal_Txtvresp").val("");
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val("");

            }
        }
    }
}

//funcion que valida los miles en tiempo real
function formatvercionsuma(input) {

    var num = input.value.replace(/\./g, "");
    if (!isNaN(num)) {
        num = num.toString().split("").reverse().join("").replace(/(?=\d*\.?)(\d{3})/g, "$1.");
        num = num.split("").reverse().join("").replace(/^[\.]/, "");
        input.value = num;
    }

    else {
        alert('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, "");
    }
}


//agregar pagos al grid general de flujos
function Btn_add_flujo_onclick() {

    if ($("#ctl00_cphPrincipal_txtporcentaje").val() == "" || $("#ctl00_cphPrincipal_txtvalortotalflow").val() == "" || $("#ctl00_cphPrincipal_txtfechapago").val() == "" || $("#ctl00_cphPrincipal_txtentregable").val() == "") {
        $("#ctl00_cphPrincipal_Lblinformationexist").text("Estos campos deben ser diligenciados antes de ingresar algún valor en la grilla de flujos!");


    }
    else {

        //inicializamos variables
        var valuecomparative = $("#ctl00_cphPrincipal_Lbltotalvalor").text();
        //quitamos el valor decimal
        var arrseparar = valuecomparative.split('.');
        valuecomparative = arrseparar[0].replace(/\,/gi, '');

        var sumapagos = $("#totalflujos").text();
        sumapagos = sumapagos.replace(/\./gi, '');

        //validamos si el valor a guardar concorda con el porcentaje deseado
        if (valuecomparative == sumapagos) {

            //capturamos campos para guardar en el grid flujos
            var tflujos = $("#totalflujos").text();
            var N_pago = $("#ctl00_cphPrincipal_txtvalortotalflow").val();
            var fecha_pago = $("#ctl00_cphPrincipal_txtfechapago").val();
            var porcentaje = $("#ctl00_cphPrincipal_txtporcentaje").val() + " %";
            var valor_pago = $("#ctl00_cphPrincipal_Lbltotalvalor").text();
            var entrega = $("#ctl00_cphPrincipal_txtentregable").val();

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
                    //cargamos el array con el json
                    arrayflujosdepago.push(jsonflujo);
                    //creamos la tabla de flujo de pagos      
                    var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";

                    for (itemArray in arrayflujosdepago) {
                        htmlTableflujos += "<tr id='flow" + arrayflujosdepago[itemArray].N_pago + "' ><td>" + arrayflujosdepago[itemArray].N_pago + "</td><td>" + arrayflujosdepago[itemArray].fecha_pago + "</td><td>" + arrayflujosdepago[itemArray].porcentaje + "</td><td>" + arrayflujosdepago[itemArray].entrega + "</td><td>" + arrayflujosdepago[itemArray].tflujos + "</td><td><input type ='button' value= 'Editar' onclick=\"editflujo('" + arrayflujosdepago[itemArray].N_pago + "','" + arrayflujosdepago[itemArray].fecha_pago + "',' " + arrayflujosdepago[itemArray].porcentaje + "','" + arrayflujosdepago[itemArray].entrega + "','" + arrayflujosdepago[itemArray].tflujos + "')\" ></input><input type ='button' value= 'Eliminar' onclick=\" eliminarflujo('" + arrayflujosdepago[itemArray].N_pago + "')\"></input></td><td><input type ='button' value= 'Detalle' onclick=\"traerdetalles('" + arrayflujosdepago[itemArray].N_pago + "',this)\"></input></td></tr>";
                    }
                    htmlTableflujos += "<tr><td>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

                    htmlTableflujos += "</tbody></table>";
                    //cargamos el div donde se generara la tabla actores
                    $("#T_flujosContainer").html("");
                    $("#T_flujosContainer").html(htmlTableflujos);


                    swhich_flujos_exist = 1;

                    //reconstruimos la tabla con los datos
                    $("#T_flujos").dataTable({
                        "bJQueryUI": true,
                        "bDestroy": true
                    });
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

                //lamar funcion para la suma de los subtotales de flujos de pagos
                sumarflujospagos();
                //validamos si los desembolsos son del 100%
                if (validaporcentaje == 100) {
                    $("#Btn_add_flujo").attr("disabled", "disabled");
                }
            }

        }
        else {

            alert("el valor total de los actores debe ser igual al porcentaje calculado");

            //inicializamos el array
            arrayValorflujoTotal[0] = 0;
            $("#totalflujos").text(0);


            //recorremos la tabla de flujo de pagos
            $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

                arrayinputflujos = $(this).find("td").slice(0, 1);

                if ($(arrayinputflujos[0]).html() != null) {
                    var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();
                    var iddesembolso = "#desenbolso" + $(arrayinputflujos[0]).html();

                    var desembolso = $(idflujo).val();
                    desembolso = desembolso.replace(/\./gi, '');

                    var datgriddes = $(iddesembolso).html();
                    datgriddes = datgriddes.replace(/\./gi, '');

                    var summadesen;
                    summadesen = parseInt(desembolso) + parseInt(datgriddes);
                    $(iddesembolso).text(addCommasrefactor(summadesen));
                    //$(idflujo).val("");
                }
            });

        }
    }
}

//funcion detalles de desembolsos para el grid flujo de pagos
function traerdetalles(str_idpago) {

    var htmlTableflujosdetalles = "<table id='T_detalle_desembolso' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>No pago</th><th>Id aportante</th><th>Aportante</th><th>desembolso</th></tr></thead><tbody>";


    for (itemArray in matriz_flujos) {
        if (matriz_flujos[itemArray].idpago == str_idpago) {
            htmlTableflujosdetalles += "<tr><td>" + matriz_flujos[itemArray].idpago + "</td><td>" + matriz_flujos[itemArray].idaportante + "</td><td>" + matriz_flujos[itemArray].Aportante + "</td><td>" + matriz_flujos[itemArray].desembolso + "</td></tr>";
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
    tflujos = tflujos.replace(/\./gi, ',');
    $("#ctl00_cphPrincipal_Lbltotalvalor").text(tflujos + ".0");
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
    else {
        //boton editar
        switch_editar = 0;
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
    reversedesembolsos = [];

    var idflow = "#flow" + strN_pago;
    //borramos de la vista el td seleccionado 
    $(idflow).remove();

    //habilitamos el botn de agregar pagos
    $("#Btn_add_flujo").removeAttr("disabled");
    //llamar suma de pagos
    sumarflujospagos();
}


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

        var desenbolso = $(iddesenbolso).html();
        desenbolso = desenbolso.replace(/\./gi, '');

        var inicial = $(idinicial).html();
        inicial = inicial.replace(/\./gi, '');

        //restamos del array de operacion
        valorarraytotal = parseInt(valorarraytotal) - parseInt(restaoperador);
        arrayValorflujoTotal[0] = valorarraytotal;

        if (swhich_validar_estado_1 != 1) {
            if (inicial != desenbolso) {
                var desembolsototal = parseInt(desenbolso) + parseInt(restaoperador);

                // $("#ctl00_cphPrincipal_Txtpruebas").val(desembolsototal);
                $(iddesenbolso).text(addCommasrefactor(desembolsototal));
                swhich_validar_estado_1 = 0;
            }
        }
        //        $(idflujo).val("");
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


    //capturamos el valor limite del actor

    $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function() {

        //validamos que el valor deseado no supere al limite
        if (opevaluesActorslimit < opeValuesActorsflujos) {
            alert("el valor ingresado no debe superar al ingresado en los actores");

            if (opevaluesActorsdesembolso != opevaluesActorslimit) {
                var desembolsototal2 = parseInt(opevaluesActorsdesembolso) + parseInt(opeValuesActorsflujos);
                $(tr_Iddes).text(addCommasrefactor(desembolsototal2));
            }

            swhich_validar_estado_1 = 1;
            // $(id).val("");
            opeValuesActorsflujos = 0;
        }

        if (opevaluesActorslimit == opevaluesActorsdesembolso) {
            totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
            $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

        }
        else {
            totaldesembolso = opevaluesActorsdesembolso - opeValuesActorsflujos;
            $(tr_Iddes).text(addCommasrefactor(totaldesembolso));

        }

        if (opevaluesActorsdesembolso < opeValuesActorsflujos) {
            alert("el valor ingresado no debe superar al desembolso disponible");
            //$(id).val("");
            swhich_validar_estado_1 = 1;
            opeValuesActorsflujos = 0;
        }

    });

    var test2 = $(tr_Iddes).html();
    test2 = test2.replace(/\./gi, '');

    //validamos si es el primer registro del array
    if (arrayValorflujoTotal.length == 0) {
        valtotaldiner = valtotaldiner + opeValuesActorsflujos;
        //ingresamos el valor en un array estatico
        arrayValorflujoTotal[0] = valtotaldiner;
        $("#totalflujos").text(addCommasrefactor(valtotaldiner));
    }
    else {

        if (arrayValorflujoTotal[0] < 0) {
            arrayValorflujoTotal[0] = 0;
            valtotaldiner = arrayValorflujoTotal[0];
            valtotaldiner = valtotaldiner + opeValuesActorsflujos;
        }
        else {
            valtotaldiner = arrayValorflujoTotal[0];
            valtotaldiner = valtotaldiner + opeValuesActorsflujos;
        }
        //ingresamos el valor en un array estatico
        arrayValorflujoTotal[0] = valtotaldiner;
        $("#totalflujos").text(addCommasrefactor(valtotaldiner));
    }

    //capturo valores de nuevo
    var valuecomparative = $("#ctl00_cphPrincipal_Lbltotalvalor").text();

    //quitamos el valor decimal
    var arrseparar = valuecomparative.split('.');
    valuecomparative = arrseparar[0].replace(/\,/gi, '');

    var sumapagos = $("#totalflujos").text();
    var opesumagos = sumapagos.replace(/\./gi, '');

    //comparo los valor contra el porcentaje seleccionado

    if (parseInt(valuecomparative) < parseInt(opesumagos)) {
        alert("el valor no debe ser mayor que el porcentaje aprobado");

        //comparo el limite con tra el campo desembolso para ver si es el inicial o no

        if (test2 != opevaluesActorslimit) {
            var desembolsototal2 = parseInt(test2) + parseInt(opeValuesActorsflujos);
            $(tr_Iddes).text(addCommasrefactor(desembolsototal2));
        }
        swhich_validar_estado_1 = 1;
        //$(id).val("");
        valtotaldiner = arrayValorflujoTotal[0];
        valtotaldiner = valtotaldiner - opeValuesActorsflujos;
        arrayValorflujoTotal[0] = valtotaldiner;
        $("#totalflujos").text(addCommasrefactor(valtotaldiner));
    }
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


//borrar de la grilla html de actores
function deleteActor(str) {

    // $(objbutton).parent().parent().remove();
    if (swhich_flujos_exist == 1) {
        alert("Se ha detectado información el la pestaña de flujos de pagos, al eliminar el actor toda la información se perdera!");

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
                delete arrayActor[itemArray];
                //arrayActor.splice(arrayActor[itemArray].actorsName, 1);
            }
        }
        //recorremos el array
        for (itemArrayflujo in arrayActorFlujo) {
            //construimos la llave de validacion
            var idflujo = arrayActorFlujo[itemArrayflujo].actorsVal;
            //validamos el dato q nos trae la funcion

            if (str == idflujo) {
                //borramos el actor deseado
                delete arrayActorFlujo[itemArrayflujo];
            }
        }

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
        htmlTableflujos += "<tr><td>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

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

        //lamar la funcionsumar actores
        sumar_grid_actores();
        //llamar la funcion suma de grid principal
        sumavalores_gridprincipal();

        //        $("#T_Actors").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });

        //        //reconstruimos la tabla con los datos 
        //        $("#T_Actorsflujos").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });

        //        //reconstruimos la tabla con los datos
        //        $("#matriz").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });


    }
    else {

        var idflujo = "#flujo" + str;
        $(idflujo).remove();

        var idmatriz = "#matriz" + str;
        $(idmatriz).remove();

        var idactor = "#actor" + str;
        $(idactor).remove();

        //recorremos el array
        for (itemArray in arrayActor) {
            //construimos la llave de validacion
            var id = arrayActor[itemArray].actorsVal;
            //validamos el dato q nos trae la funcion

            if (str == id) {
                //borramos el actor deseado
                delete arrayActor[itemArray];
                //arrayActor.splice(arrayActor[itemArray].actorsName, 1);
            }
        }
        //recorremos el array
        for (itemArrayflujo in arrayActorFlujo) {
            //construimos la llave de validacion
            var idflujo = arrayActorFlujo[itemArrayflujo].actorsVal;
            //validamos el dato q nos trae la funcion

            if (str == idflujo) {
                //borramos el actor deseado
                delete arrayActorFlujo[itemArrayflujo];
            }
        }
        //lamar la funcionsumar actores
        sumar_grid_actores();
        //llamar la funcion suma de grid principal
        sumavalores_gridprincipal();


        //        $("#T_Actors").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });

        //        //reconstruimos la tabla con los datos 
        //        $("#T_Actorsflujos").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });

        //        //reconstruimos la tabla con los datos
        //        $("#matriz").dataTable({
        //            "bJQueryUI": true,
        //            "bDestroy": true
        //        });

    }

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

//funcion de buscar en la grilla de actores  la FSC para el grid principal
//function buscarFSC() {

//    var valdinergridfsc = 0;
//    var valespeciegridfsc = 0;
//    var valtotalgridfsc = 0;
//    var option_0 = 0;

//    //recorremos la tabla actores para calcular los totales
//    $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {
//        var arrayValuesActors2 = $(this).find("td").slice(0, 10);
//        //validamos si hay campos null en la tabla actores
//        if ($(arrayValuesActors2[0]).html() != null) {
//            //validamos si es la FSC
//            if ($(arrayValuesActors2[0]).html() == 4) {

//                option_0 = 1;
//                //capturamos e incrementamos los valores para la suma
//                valdinergridfsc = parseInt($(arrayValuesActors2[7]).html().replace(/\./gi, ''));
//                valespeciegridfsc = parseInt($(arrayValuesActors2[8]).html().replace(/\./gi, ''));
//                valtotalgridfsc = parseInt($(arrayValuesActors2[9]).html().replace(/\./gi, ''));

//                //validamos valores si vienen vacios
//                if (isNaN(valdinergridfsc)) {
//                    valdinergridfsc = 0;
//                }
//                if (isNaN(valespeciegridfsc)) {
//                    valespeciegridfsc = 0;
//                }
//                if (isNaN(valtotalgridfsc)) {
//                    valtotalgridfsc = 0;
//                }

//                //cargamos los campos con la operacion realizada
//                $("#ValueMoneyFSC").text(addCommasrefactor(valdinergridfsc));
//                $("#ValueEspeciesFSC").text(addCommasrefactor(valespeciegridfsc));
//                $("#ValueCostFSC").text(addCommasrefactor(valtotalgridfsc));
//            }
//        }
//        //validamos si la opcion escojida esta vacia y le asignamos 0
//        if (option_0 == 0) {
//            $("#ValueMoneyFSC").text(0);
//            $("#ValueEspeciesFSC").text(0);
//            $("#ValueCostFSC").text(0);
//        }

//    });

//}

// funcion de todos los q son actores diferentes a la fsc y sumarlos en el grid principal
//function buscarothers() {

//    var valdinergrid = 0;
//    var valespeciegrid = 0;
//    var valtotalgrid = 0;
//    var option_0 = 0;

//    //recorremos la tabla actores para calcular los totales
//    $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {
//        var arrayValuesActors3 = $(this).find("td").slice(0, 10);
//        //validamos si hay campos null en la tabla actores

//        if ($(arrayValuesActors3[0]).html() != null) {
//            //validamos si es diferente a la FSC
//            if ($(arrayValuesActors3[0]).html() != 4) {

//                option_0 = 1;
//                //capturamos e incrementamos los valores para la suma
//                valdinergrid = valdinergrid + parseInt($(arrayValuesActors3[7]).html().replace(/\./gi, ''));
//                valespeciegrid = valespeciegrid + parseInt($(arrayValuesActors3[8]).html().replace(/\./gi, ''));
//                valtotalgrid = valtotalgrid + parseInt($(arrayValuesActors3[9]).html().replace(/\./gi, ''));

//                //validamos valores si vienen vacios
//                if (isNaN(valdinergrid)) {
//                    valdinergrid = 0;
//                }
//                if (isNaN(valespeciegrid)) {
//                    valespeciegrid = 0;
//                }
//                if (isNaN(valtotalgrid)) {
//                    valtotalgrid = 0;
//                }

//                //cargamos los campos con la operacion realizada
//                $("#ValueMoneyCounter").text(addCommasrefactor(valdinergrid));
//                $("#ValueEspeciesCounter").text(addCommasrefactor(valespeciegrid));
//                $("#ValueCostCounter").text(addCommasrefactor(valtotalgrid));
//            }
//        }
//        //validamos si la opcion escojida esta vacia y le asignamos 0
//        if (option_0 == 0) {
//            $("#ValueMoneyCounter").text(0);
//            $("#ValueEspeciesCounter").text(0);
//            $("#ValueCostCounter").text(0);
//        }


//    });
//}

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


    //captura los valores 
    //    var dinerfsc = $("#ValueMoneyFSC").text();
    //    dinerfsc = dinerfsc.replace(/\./gi, '');
    //    var opedinerfsc = parseInt(dinerfsc);
    //    //captura los valores 
    //    var dinerothers = $("#ValueMoneyCounter").text();
    //    dinerothers = dinerothers.replace(/\./gi, '');
    //    var opedinerothers = parseInt(dinerothers);

    //    //validar campo vacio
    //    if (isNaN(opedinerfsc)) {
    //        opedinerfsc = 0;
    //        $("#ValueMoneyFSC").val(opedinerfsc);
    //    }
    //    else {
    //        //validar campo vacio
    //        if (isNaN(opedinerothers)) {
    //            opedinerothers = 0;
    //            $("#ValueMoneyCounter").val(opedinerothers);
    //        }
    //        else {
    //            //realizar suma con los valores deseados
    //            var sumadiner = 0;
    //            sumadiner = opedinerfsc + opedinerothers;
    //            $("#valueMoneytotal").text(addCommasrefactor(sumadiner));
    //            $("#ctl00_cphPrincipal_HDvalorpagoflujo").val(addCommasrefactor(sumadiner));
    //        }
    //    }
    //    //realizar suma con los valores deseados
    //    sumadiner = opedinerfsc + opedinerothers;
    //    $("#valueMoneytotal").text(addCommasrefactor(sumadiner));
    //    $("#ctl00_cphPrincipal_HDvalorpagoflujo").val(addCommasrefactor(sumadiner));
}

// funcion suma de segunda columna especie
//function sumaespecie_gridprincipal() {

//    //captura los valores 
//    var especiefsc = $("#ValueEspeciesFSC").text();
//    especiefsc = especiefsc.replace(/\./gi, '');
//    var opeespeciefsc = parseInt(especiefsc);

//    //captura los valores 
//    var especieothers = $("#ValueEspeciesCounter").text();
//    especieothers = especieothers.replace(/\./gi, '');
//    var opeespecieothers = parseInt(especieothers);

//    //validar campo vacio
//    if (isNaN(opeespeciefsc)) {
//        opeespeciefsc = 0;
//        $("#ValueEspeciesFSC").val(opeespeciefsc);
//    }
//    else {
//        //validar campo vacio
//        if (isNaN(opeespecieothers)) {
//            opeespecieothers = 0;
//            $("#ValueEspeciesCounter").val(opeespecieothers);
//        }
//        else {
//            //realizar suma con los valores deseados
//            var sumaespecie = 0;
//            sumaespecie = opeespeciefsc + opeespecieothers;
//            $("#ValueEspeciestotal").text(addCommasrefactor(sumaespecie));
//        }
//    }
//    //realizar suma con los valores deseados
//    sumaespecie = opeespeciefsc + opeespecieothers;
//    $("#ValueEspeciestotal").text(addCommasrefactor(sumaespecie));
//}

//funcion suma tercera columna total
//function sumatotal_gridprincipal() {

//    //captura los valores 
//    var totalfsc = $("#ValueCostFSC").text();
//    totalfsc = totalfsc.replace(/\./gi, '');
//    var opetotalfsc = parseInt(totalfsc);

//    //captura los valores 
//    var totalothers = $("#ValueCostCounter").text();
//    totalothers = totalothers.replace(/\./gi, '');
//    var opetotalothers = parseInt(totalothers);

//    //validar campo vacio
//    if (isNaN(opetotalfsc)) {
//        opetotalfsc = 0;
//        $("#ValueCostFSC").val(opetotalfsc);
//    }
//    else {
//        //validar campo vacio
//        if (isNaN(opetotalothers)) {
//            opetotalothers = 0;
//            $("#ValueCostCounter").val(opetotalothers);
//        }
//        else {
//            //realizar suma con los valores deseados
//            var sumatotal = 0;
//            sumatotal = opetotalfsc + opetotalothers;
//            $("#ValueCostotal").text(addCommasrefactor(sumatotal));
//        }
//    }
//    //realizar suma con los valores deseados
//    sumatotal = opetotalfsc + opetotalothers;
//    $("#ValueCostotal").text(addCommasrefactor(sumatotal));
//}

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

        var valortotalflow;
        var txtvalortotalflow = $("#ctl00_cphPrincipal_HDvalorpagoflujo").val();

        valortotalflow = txtvalortotalflow.replace(/\./gi, '');
        valortotalflow = parseInt(valortotalflow);

        //realiza la operacion del porcentaje seleccionado
        var parcial = (parseFloat(porc) * parseFloat(valortotalflow)) / 100;
        parcial = numeral(parcial).format('0,0.0');

        $("#ctl00_cphPrincipal_Lbltotalvalor").text(parcial);

        if (s_revisarflujos == 1) {
            var idflujos = arrayActorFlujo[itemarrayflujos].actorsVal;

            var arrseparar = parcial.split('.');
            valuecomparative = arrseparar[0].replace(/\,/gi, '.');

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
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("El porcentaje debe ser meayor a 0");
            $("#ctl00_cphPrincipal_txtporcentaje").val("");
            $("#ctl00_cphPrincipal_txtporcentaje").focus();
        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpporcentaje").text("");
        }

    });
}

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

//cargar combo de departamentos
function Cdeptos() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_deptos" },
        success: function(result) {
            $("#ddlDepto").html(result);
            $("#ddlDepto").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los departamentos.");
        }
    });
}

//cargar combo de municipios 
function Cmunip() {
    $("#ddlDepto").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_munip", "iddepto": $(this).val() },
            success: function(result) {
                $("#ddlCity").html(result);
                $("#ddlCity").trigger("liszt:updated");
            },
            error: function(msg) {
                alert("No ha seleccionado el departamento.");
                $("#ddlCity").html("<option>Seleccione...</opption>");
                $("#ddlCity").trigger("liszt:updated");
            }
        });
    });
}

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


//cargar double lisbox componentes de programa
function cargarcomponente() {

    $("#ddlPrograms").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_component", "idprogram": $(this).val() },
            success: function(result) {

                $("#seleccionarcomponente").html(result);

                //darle atributos de seleccione
                $(".seleccione").click(function() {
                    var swhich_array_component_exist = 0;

                    var validaarray = $(this).attr("id");
                    //validamos si el array esta vacio
                    if (arraycomponente.length == 0) {
                        arraycomponente.push($(this).attr("id"));
                    }
                    else {
                        //recorremos elarray si ya habiamos ingresado el componente
                        for (itemArray in arraycomponente) {
                            if (validaarray == arraycomponente[itemArray]) {
                                swhich_array_component_exist = 1;

                            }
                        }
                        if (swhich_array_component_exist == 0) {
                            arraycomponente.push($(this).attr("id"));
                        }

                    }
                });
                //Compoentes Style
                $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
                    $(this).css("background", "#9bbb58");
                    $(this).css("color", "#fff");
                });
            },
            error: function(msg) {
                alert("No se pueden cargar los componentes del programa selecionado.");
            }
        });
    });
}

// agregar componentes al <ul componentesseleccionados>
function Btnaddcomponent_onclick() {

    $("#ctl00_cphPrincipal_Lblinformationcomponent").text("");

    for (itemArray in arraycomponente) {

        var htmlcomponente = $("#" + arraycomponente[itemArray]).html();

        //crea la lista nueva
        var htmlresult = "<li id = 'select" + arraycomponente[itemArray] + "' class = 'des_seleccionar' >" + htmlcomponente + "</li>";
        //se asigna la lista al ul
        $("#componentesseleccionados").append(htmlresult);
        //eliminar del ul de seleccionar
        $("#" + arraycomponente[itemArray]).remove();

    }

    arraycomponente = [];

    //darle atributos de seleccione
    $(".des_seleccionar").click(function() {

        var swhich_array_componentdesechado_exist = 0;

        var validaarray = $(this).attr("id");
        //validamos si el array esta vacio
        if (arraycomponentedesechado.length == 0) {
            arraycomponentedesechado.push($(this).attr("id"));
        }
        else {
            //recorremos elarray si ya habiamos ingresado el componente
            for (itemArray in arraycomponentedesechado) {
                if (validaarray == arraycomponentedesechado[itemArray]) {
                    swhich_array_componentdesechado_exist = 1;

                }
            }
            if (swhich_array_componentdesechado_exist == 0) {
                arraycomponentedesechado.push($(this).attr("id"));
            }

        }
    });
    //Compoentes Style
    $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
        $(this).css("background", "#9bbb58");
        $(this).css("color", "#fff");
    });


}

// agregar componentes al <ul seleccionarcomponente>
function Btndeletecomponent_onclick() {

    for (itemArray in arraycomponentedesechado) {

        var htmlcomponente = $("#" + arraycomponentedesechado[itemArray]).html();

        //crea la lista nueva
        var htmlresult = "<li id = '" + arraycomponentedesechado[itemArray].replace('select', '') + "' class = 'seleccione' >" + htmlcomponente + "</li>";

        //se asigna la lista al ul
        $("#seleccionarcomponente").append(htmlresult);

        
        //eliminar del ul de seleccionado
        $("#" + arraycomponentedesechado[itemArray]).remove();
      
    }

    arraycomponentedesechado = [];

    //darle atributos de seleccione
    $(".seleccione").click(function() {

        var swhich_array_component_exist = 0;

        var validaarray = $(this).attr("id");
        //validamos si el array esta vacio
        if (arraycomponente.length == 0) {
            arraycomponente.push($(this).attr("id"));
        }
        else {
            //recorremos elarray si ya habiamos ingresado el componente
            for (itemArray in arraycomponente) {
                if (validaarray == arraycomponente[itemArray]) {
                    swhich_array_component_exist = 1;

                }
            }
            if (swhich_array_component_exist == 0) {
                arraycomponente.push($(this).attr("id"));
            }

        }

    });
    //Compoentes Style
    $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
        $(this).css("background", "#9bbb58");
        $(this).css("color", "#fff");
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

    //suma de campos de actores en el formulario de idea dinero + especies
    //2-08-2013 GERMAN RODRIGUEZ

    $("#ctl00_cphPrincipal_btnAddThird").click(function() {

        var rev = $("#ctl00_cphPrincipal_Txtvrdiner").val();
        rev = rev.replace(/\./gi, '');
        var vd = parseInt(rev);

        var rev2 = $("#ctl00_cphPrincipal_Txtvresp").val();
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
                var suma = 0;
                suma = vd + ve;
                addCommas(suma);
            }
        }
        suma = vd + ve;
        addCommas(suma);
    });

    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvrdiner").blur(function() {
        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var val = parseInt(rev);

        if (isNaN(val)) {
            val = 0;
            $("#ctl00_cphPrincipal_Txtvrdiner").val(val);
        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvresp").val();

            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);
            if (isNaN(val2)) { val2 = 0; }
            else {
                var suma = 0;
                suma = val + val2;
                addCommas(suma);
            }
        }
    });

    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvresp").blur(function() {
        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var val = parseInt(rev);
        if (isNaN(val)) {
            val = 0;
            $("#ctl00_cphPrincipal_Txtvresp").val(val);
        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvrdiner").val();
            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);
            if (isNaN(val2)) { val2 = 0; }
            else {
                var suma = 0;
                suma = val + val2;
                addCommas(suma);
            }
        }
    });

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
    $("#ctl00_cphPrincipal_txtresults, #ctl00_cphPrincipal_txtresulgc, #ctl00_cphPrincipal_txtresulci, #ctl00_cphPrincipal_txtstartdate ").blur(function() {
        if ($("#ctl00_cphPrincipal_txtresults").val() == '' && $("#ctl00_cphPrincipal_txtresulgc").val() == '' && $("#ctl00_cphPrincipal_txtresulci").val() == '') {
            $(this).css("border", "2px solid red");
            $("#ctl00_cphPrincipal_lblHelpresults").text("Algunos de los resultados debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Label10").text("Algunos de los resultados debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Label11").text("Algunos de los resultados debe ser diligenciado.");
        } else {
            $(this).css("border", "2px solid #DEDEDE");
            $("#ctl00_cphPrincipal_lblHelpresults").text("");
            $("#ctl00_cphPrincipal_Label10").text("");
            $("#ctl00_cphPrincipal_Label11").text("");

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
//cargar el combo actor
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


//cambio de discriminacion de aportes por FSC Y Actor
//07-01-2014 GERMAN RODRIGUEZ MGgroup

function separarvaloresFSC() {
    $("#ctl00_cphPrincipal_btnAddThird").click(function() {

        //caturamos valor de aporte efectivo
        var VMFSC = $("#ctl00_cphPrincipal_Txtvrdiner").val();
        VMFSC = VMFSC.replace(/\./gi, '');
        var VMFSCO = parseInt(VMFSC);

        //capturamos valor en especie
        var VEFSC = $("#ctl00_cphPrincipal_Txtvresp").val();
        VEFSC = VEFSC.replace(/\./gi, '');
        var VEFSCO = parseInt(VEFSC);

        //validamos si esta el valor o no tiene
        if (isNaN(VMFSCO)) {
            VMFSCO = 0;
            $("#ctl00_cphPrincipal_ValueMoneyFSC").val(VMFSCO);
        }
        else {
            //validamos si esta el valor o no tiene
            if (isNaN(VEFSCO)) {
                VEFSCO = 0;
                $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(VEFSCO);
            }
            else {
                //mostramos los valores capturados en el grid
                $("#ctl00_cphPrincipal_ValueMoneyFSC").val(VMFSCO);
                $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(VEFSCO);
                var suma = 0;
                //realizamos la operacion
                suma = VMFSCO + VEFSCO;
                addCommas2(suma);
            }
        }
    });
}



function subirArchivos() {


    //validamos si seleccionaron un archivo
    if ($("#fileupload").val() != "") {

        //Añadimos la imagen de carga en el contenedor
        $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "block");

        $("#ctl00_cphPrincipal_LblHELPARCHIVE").text("");
        //capturamos los datos del input file
        var file = $("#fileupload");
        var dataFile = $("#fileupload")[0].files[0];

        //inicializamos el fordata para transferencia de archivos
        var data = new FormData();
        //asinamos el datafile a la variable archivo 
        data.append('archivo', dataFile);

        // data.ajaxStart(inicioEnvio);
        //transacion ajax
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "POST",
            contentType: false,
            data: data,
            processData: false,
            success: function(result) {

                //creamos variables
                var filename = result;
                var objectfile = data;

                if (idfile == null) {
                    idfile = 1;
                }
                else {
                    idfile = idfile + 1;
                }

                //creamos json para guardarlos en un array
                var jsonFiles = { "idfile": idfile, "filename": filename, "objectfile": objectfile };
                //            //cargamos el array con el json
                arrayFiles.push(jsonFiles);

                var htmlTablefiles = "<table id='T_files' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>Archivo</th><th style='text-align: center;'>Observaciones</th><th style='text-align: center;'>Eliminar</th></tr></thead><tbody>";
                //recorremos el array para generar datos del la tabla anexos
                for (itemArray in arrayFiles) {
                    htmlTablefiles += "<tr id='archivo" + arrayFiles[itemArray].idfile + "'><td><a id='linkarchives' runat='server' href='/FSC_APP/document/temp/" + arrayFiles[itemArray].filename + "' target= '_blank' title='link'>" + arrayFiles[itemArray].filename + "</a></td><td style='text-align: center;'><input id='txtinputobser" + arrayFiles[itemArray].idfile + "' ></input></td><td style='text-align: center;'><input type ='button' value= 'Eliminar' onclick=\"deletefile('" + arrayFiles[itemArray].idfile + "')\"></input></td></tr>";
                }
                htmlTablefiles += "</tbody></table>";

                //cargamos el div donde se generara la tabla anexos
                $("#tdFileInputs").html("");
                $("#tdFileInputs").html(htmlTablefiles);

                //reconstruimos el pluging de la tabla
                $("#T_files").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });

                $("#fileupload").val("");

                $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");

            },
            error: function(error) {
                alert("Ocurrió un error inesperado, por favor intente de nuevo mas tarde: " + error);
                console.log(error);
                $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");
            }
        });
    }
    else {
        $("#ctl00_cphPrincipal_LblHELPARCHIVE").text("No ha seleccionado ningún archivo");

    }

}

//hola
function deletefile(stridfile) {

    var idarchivo = "#archivo" + stridfile;
    $(idarchivo).remove();

    for (itemArray in arrayFiles) {
        //construimos la llave de validacion
        var id = arrayFiles[itemArray].idfile;
        //validamos el dato q nos trae la funcion

        if (stridfile == id) {
            //borramos el actor deseado
            delete arrayFiles[itemArray];
        }
    }


}


//function Cons_T_Anexos() {

//    var htmlTablefiles = "<table id='T_files' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>Archivo</th><th style='text-align: center;'>Observaciones</th><th style='text-align: center;'>Eliminar</th></tr></thead><tbody>";
//    //recorremos el array para generar datos del la tabla anexos

//    for (i = 0; i < args.length; i++) {
//        htmlTablefiles += "<tr id='archivo" + i + "'><td><a id='linkarchives' runat='server' href='/FSC_APP/document/temp/" + args[i].name + "' target= '_blank' title='link'>" + args[i].name + "</a></td><td style='text-align: center;'><input id='txtinputobser" + i + "' ></input></td><td style='text-align: center;'><input type ='button' value= 'Eliminar' onclick=\"deletefile('" + i + "')\"></input></td></tr>";
//    }

//    htmlTablefiles += "</tbody></table>";

//    //cargamos el div donde se generara la tabla anexos
//    $("#tdFileInputs").html("");
//    $("#tdFileInputs").html(htmlTablefiles);

//    //reconstruimos el pluging de la tabla
//    $("#T_files").dataTable({
//        "bJQueryUI": true,
//        "bDestroy": true
//    });

//}
