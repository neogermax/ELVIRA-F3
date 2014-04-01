
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
var entradaflujos = 0;

var idfile;
var S_eliminar;

var ideditar;

var contadorrestar = 0;

$(document).ready(function() {

    //capturamos la url
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    //validamos si creamos la idea o editamos
    if (sURLVariables[0] == "op=edit") {
        ideditar = sURLVariables[1].replace("id=", "");
        //        alert(ideditar);

        operacionesIdea();
        actors_transanccion();
        comboactor();

        var timer = setTimeout("fix();", 2000);
        validafecha();
        validafecha2();
        separarvaloresFSC();

        Cdeptos();
        Cmunip();
        Cactors();
        CtypeContract();
        startdate();
        Ctype_project();
        Cpopulation();
        validarporcentaje();
        ClineEstrategic();
        Cprogram();


        view_ubicacion();
        View_actores();
        View_matriz_principal();
        View_flujos_p();

        ClineEstrategic_edit();


        $("#SaveIdea").css("display", "none");
        $("#Export").css("display", "block");
        $("#EditIdea").css("display", "block");
    }
    else {

        operacionesIdea();
        actors_transanccion();
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

        $("#SaveIdea").css("display", "block");
        $("#Export").css("display", "none");
        $("#EditIdea").css("display", "none");
    }

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
    $("#Export").button();
    $("#EditIdea").button();
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


});


function ClineEstrategic_edit() {
    var valor = 17;
    $("#ddlStrategicLines option[value=\"'" + valor + "'\"]").attr("selected", true);
}

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
            if ($("#ctl00_cphPrincipal_RBnList_iva :checked").val() == null || $("#ddlPupulation :selected").text() == 'Seleccione...' || $("#ddlmodcontract :selected").text() == 'Seleccione...' || $("#ddlStrategicLines :selected").text() == 'Seleccione...' || $("#ddlPrograms :selected").text() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '' || $("#ctl00_cphPrincipal_Txtdatecierre").val() == '' || arrayUbicacion.length == 0 || fsc_exist == 0) {

                if ($("#ctl00_cphPrincipal_RBnList_iva :checked").val() == null) {
                    $("#ctl00_cphPrincipal_Lblhelpiva").text("Escoja si o no");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                }
                else {
                    $("#ctl00_cphPrincipal_Lblhelpiva").text("");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                }

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

                if ($("#ctl00_cphPrincipal_txtresults").val() == '' && $("#ctl00_cphPrincipal_txtresulgc").val() == '' && $("#ctl00_cphPrincipal_txtresulci").val() == '') {
                   
                    $("#ctl00_cphPrincipal_lblHelpresults").text("Algunos de los resultados debe ser diligenciado.");
                    $("#ctl00_cphPrincipal_Label10").text("Algunos de los resultados debe ser diligenciado.");
                    $("#ctl00_cphPrincipal_Label11").text("Algunos de los resultados debe ser diligenciado.");
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("La idea no guardo con exito: Revisar la pestaña información");
                } else {
                   
                    $("#ctl00_cphPrincipal_lblHelpresults").text("");
                    $("#ctl00_cphPrincipal_Label10").text("");
                    $("#ctl00_cphPrincipal_Label11").text("");

                    $("#ctl00_cphPrincipal_lblinfls").val("");
                    $("#ctl0_cphPrincipal_lblinpro").val("");
                    $("#ctl00_cphPrincipal_lblHelpname").text("");
                    $("#ctl00_cphPrincipal_lblHelpjustification").text("");
                    $("#ctl00_cphPrincipal_lblHelpobjective").text("");
                    $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
                    $("#ctl00_cphPrincipal_lbldia").text("");
                    $("#ctl00_cphPrincipal_Lblmodcontract").text("");
                    $("#ctl00_cphPrincipal_lblHelppopulation").text("");
                    $("#ctl00_cphPrincipal_Lblhelpenddate").text("");
                    $("#ctl00_cphPrincipal_Lblhelpiva").text("");


                    //crear arrays para el ingreso de las listas de json
                    var listubicaciones = [];
                    var listactores = [];
                    var listflujos = [];

                    var Str_listcomponentes = $("#componentesseleccionados").html();
                    Str_listcomponentes = Str_listcomponentes.replace(/"/g, "_");
                    Str_listcomponentes = Str_listcomponentes.replace(/<li/g, "");
                    Str_listcomponentes = Str_listcomponentes.replace(/li>/g, "");
                    Str_listcomponentes = Str_listcomponentes.replace(/</g, "");
                    Str_listcomponentes = Str_listcomponentes.replace(/>/g, "");
                    Str_listcomponentes = Str_listcomponentes.replace(/class=/g, "*");
                    Str_listcomponentes = Str_listcomponentes.replace(/id=/g, "");
                    Str_listcomponentes = Str_listcomponentes.replace(/_selectadd/g, "");


                   
                    //recorer array para el ingreso de ubicaciones
                    for (item in arrayUbicacion) {
                        listubicaciones.push(JSON.stringify(arrayUbicacion[item]));
                    }
                    //recorer array para el ingreso de actores
                    for (item in arrayActor) {
                        listactores.push(JSON.stringify(arrayActor[item]));
                    }

                    //recorer array para el ingreso de flujos
                    for (item in arrayflujosdepago) {
                        listflujos.push(JSON.stringify(arrayflujosdepago[item]));
                    }

                    if (listflujos.length == 0) {
                        listflujos[0] = "vacio_ojo";
                    }


                    //crear comunicacion ajax para el ingreso de los datos de la idea
                    $.ajax({
                        url: "AjaxAddIdea.aspx",
                        type: "POST",
                        //crear json
                        data: { "action": "save", "code": $("#ctl00_cphPrincipal_txtcode").val(),
                            "linea_estrategica": $("#ddlStrategicLines").val(),
                            "programa": $("#ddlPrograms").val(),
                            "nombre": cambio_text($("#ctl00_cphPrincipal_txtname").val()),
                            "justificacion": cambio_text($("#ctl00_cphPrincipal_txtjustification").val()),
                            "objetivo": cambio_text($("#ctl00_cphPrincipal_txtobjective").val()),
                            "objetivo_esp": cambio_text($("#ctl00_cphPrincipal_txtareadescription").val()),
                            "Resultados_Benef": cambio_text($("#ctl00_cphPrincipal_txtresults").val()),
                            "Resultados_Ges_c": cambio_text($("#ctl00_cphPrincipal_txtresulgc").val()),
                            "Resultados_Cap_i": cambio_text($("#ctl00_cphPrincipal_txtresulci").val()),
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
                            "cost": $("#ValueCostotal").text(),
                            "obligaciones": cambio_text($("#ctl00_cphPrincipal_Txtobligationsoftheparties").val()),
                            "riesgo": cambio_text($("#ctl00_cphPrincipal_Txtriesgos").val()),
                            "mitigacion": cambio_text($("#ctl00_cphPrincipal_Txtaccionmitig").val()),
                            "presupuestal": cambio_text($("#ctl00_cphPrincipal_Txtroutepresupuestal").val()),
                            "iva": $("#ctl00_cphPrincipal_RBnList_iva :checked").val(),
                            "listcomponentes": Str_listcomponentes.toString(),
                            "listubicaciones": listubicaciones.toString(),
                            "listflujos": listflujos.toString(),
                            "listactores": listactores.toString()

                        },
                        //mostrar resultados de la creacion de la idea
                        success: function(result) {
                            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                            $("#SaveIdea").css("display", "none");
                            $("#Export").css("display", "block");
                            $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
                            $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                            $("#ctl00_cphPrincipal_Txtobligationsoftheparties").focus();
                        },
                        error: function() {
                            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                            $("#SaveIdea").css("display", "block");
                            $("#ctl00_cphPrincipal_lblsaveinkdformation").text("Se genero error al entrar a la operacion Ajax");
                        }
                    });

                }

            }
        }
    }
}


//funcion de cambio de comillas enters y tabulaciones
function cambio_text(str_txt) {

    str_txt.replace(/\n/g, "");
    str_txt = str_txt.replace(/\r/g, "");
    str_txt = str_txt.replace(/\t/g, "");
    str_txt = str_txt.replace(/\n\r/g, " ");
    str_txt = str_txt.replace(/\r\n/g, " ");

    return str_txt;
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







