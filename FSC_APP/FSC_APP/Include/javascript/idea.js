//Javascript para modulo de Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

//Funcion perteneciente a el evento onload del elemento body

var arrayUbicacion = [];
var array_ubicacion_ed = [];

var arrayActor = [];
var array_actores_ed = [];

var arrayActorFlujo = [];
var arrayactorflujo_ed = [];

var arraycomponenteing = [];
var arraycomponente = [];
var arraycomponentedesechado = [];
var arraycomponente_archivar = [];
var arraycomponente_archivar_ed = [];

var arrayValorflujoTotal = [];
var arrayinputflujos = [];

var arrayflujosdepago = [];
var arrayflujosdepago_ed = [];

var matriz_flujos = [];
var matriz_flujos_ed = [];


var reversedesembolsos = [];

var arrayFiles = [];
var arrayFiles_ed = [];

var filescharge = [];

var arraycompo = [];

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
var valor_iva;
var ideditar;
var edit_line_strategic;
var edit_program;
var edit_flujo_inicializa;
var edit_swhich_fx;
var contadorrestar = 0;
var validateflujos_save;
var componentes_editados;
var contar_program = 0;
var itemarrayflujos;
var control_edit_compo;
var load_edit;

var line_strategic;
$(document).ready(function() {

    load_combos();

    Cmunip();
    Cpopulation();
    cargarcomponente();

    comboactor();

    valide_date_depto();

    operacionesIdea();
    actors_transanccion();
    validarporcentaje();
    startdate();
    validafecha();
    validafecha2();

    borrar_carpeta();

    //capturamos la url
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    //validamos si creamos la idea o editamos
    //editar idea
    if (sURLVariables[0] == "op=edit") {
        ideditar = sURLVariables[1].replace("id=", "");

        var timer = setTimeout("fix();", 2000);

        componentes_editados = 1;

        $("#ddlStrategicLines").ready(function() {
            ClineEstrategic_edit();
        });

        View_componentes_array();
        view_ubicacion_array();
        View_actores_array();
        View_flujos_p_array();
        View_flujos_actors_array();
        View_detalle_flujo_array();
        View_anexos_array();

        edit_component_view();

        load_idarchive();

        aprobacion_idea();

        var timer_cline_edit = setTimeout("Cpopulation_view();", 2000);
        var timer_cline_edit = setTimeout("Ctypcontract_view();", 2000);
        var timer_cline_edit = setTimeout("Ctypaproval_view();", 2000);

        itemarrayflujos = 0;

        $("#li_estado").css("display", "compact");
        $("#SaveIdea").attr("value", "Guardar cambios");
        $("#Export").css("display", "compact");
        load_edit = 0;
    }
    //crear idea
    else {

        load_edit = 1;

        $("#li_estado").css("display", "none");
        var timer = setTimeout("fix();", 2000);

        Cprogram();

        $("#SaveIdea").attr("value", "Crear Idea");
        $("#Export").css("display", "none");

    }

    //validar campos fechas
    validar_campofecha('ctl00_cphPrincipal_txtstartdate', 'ctl00_cphPrincipal_lblHelpstartdate');
    validar_campofecha('ctl00_cphPrincipal_txtfechapago', 'ctl00_cphPrincipal_helpfechapago');
    carga_eventos("ctl00_cphPrincipal_container_wait");



    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containererrors").css("display", "none");
    $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");
    $('#ctl00_cphPrincipal_container_wait').css("display", "none");


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
    $("#PartialSaved").button();
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
    $("#cancelEdition").button();

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
            valor_iva = 1;
        }
        else {
            $("#ctl00_cphPrincipal_vrdiner").text("Vr Dinero");
            valor_iva = 0;
        }

    });

    $("#tabsIdea").tabs();

    //validar que pestaña esta ingresando
    $("#tabsIdea").click(function() {

        var itemarrayflujos = 0;

        var active = $("#tabsIdea").tabs("option", "active");
        var idtabs = $("#tabsIdea ul>li a").eq(active).attr('href');
        //validar si esl la de flujos de pago
        if (idtabs == "#flujos") {
            //    alert(idtabs);
            //validar si es la primera entrada       
            if (entradaflujos == 0) {
                var tamaño_flujos = $("#T_Actorsflujos tr").length - 2;

                //alert(tamaño_flujos);
                //validar la cantidad de actores
                if (tamaño_flujos == 1) {
                    if (arrayActorFlujo[0] != undefined) {
                        var Aflujos = arrayActorFlujo[0].actorsVal;
                        //   alert(Aflujos);
                        $("#txtinput" + Aflujos).attr("disabled", "disabled");
                    }
                    //$("#desenbolso" + Aflujos).text("");
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

    var verificar_actor = 1;
    var verificar_ubicaciones = 1;
    var verificar_componentes = 1;
    var verificar_flujos = 1;
    var validar_flujos = 0;
    var verificar_informacion_p = 1;
    var verificar_C_resultados = 1;
    var verificar_C_fechas = 1;
    var fsc_exist = 0;
    var F_iva = 1;
    var F_linea_Es = 1;
    var F_objetivo = 1;
    var F_tipo_pro = 1;
    var F_poblacion = 1;
    var F_contrato = 1;
    var F_nombre = 1;
    var F_justifica = 1;
    var F_objetivo_campo = 1;
    var mensaje_info_idea = "";
    var F_fecha_ini = 1;


    $("#ctl00_cphPrincipal_Lblerrors_save_idea").text(mensaje_info_idea);
    //validar si las pestañas ubicacion, actores estan diligenciadas
    //validar si hay actores actor
    if (arrayActor.length == 0) {
        verificar_actor = 0;
    }
    //si existe actor verificar si esta la fundacion incluida
    else {
        for (iArray in arrayActor) {
            if (4 == arrayActor[iArray].actorsVal) {
                fsc_exist = 1;
            }
        }
    }
    //validar si hay ubicaciones actor
    if (arrayUbicacion.length == 0) {
        verificar_ubicaciones = 0;
    }

    //validar si un componente fue ingresado
    var textoLista = $("#componentesseleccionados").html();

    //validar si hay componentes ingresados
    if (textoLista == "") {
        verificar_componentes = 0;
    }

    //revisamos si hay flujos de pago
    if (arrayflujosdepago.length != 0) {
        verificar_flujos = 0;
        //capturamos el valor del porcentaje
        var validaporcentaje = $("#porcentaje").text();
        validaporcentaje = validaporcentaje.replace('%', '');

        //y validamos si esta al 100 porciento
        if (validaporcentaje == 100) {
            validar_flujos = 1;
            verificar_flujos = 1;
        }
    }

    //revisamos q esten diligenciados los campos de resultados
    if ($("#ctl00_cphPrincipal_Txtothersresults").val() == '' && $("#ctl00_cphPrincipal_txtresults").val() == '' && $("#ctl00_cphPrincipal_txtresulgc").val() == '' && $("#ctl00_cphPrincipal_txtresulci").val() == '') {
        verificar_C_resultados = 0;
    }

    //revisamos  q esten diligenciados los campos de mes y dia
    if ($("#ctl00_cphPrincipal_txtduration").val() == '' && $("#ctl00_cphPrincipal_Txtday").val() == '') {
        verificar_C_fechas = 0;
    }

    //validar campos de informacion principal obligatorios
    if ($("#ctl00_cphPrincipal_RBnList_iva :checked").val() == null || $("#ddlPupulation :selected").text() == 'Seleccione...' || $("#ddlmodcontract :selected").text() == 'Seleccione...' || $("#ddlStrategicLines :selected").text() == 'Seleccione...' || $("#ddlPrograms :selected").text() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '') {

        verificar_informacion_p = 0;

        if ($("#ctl00_cphPrincipal_RBnList_iva :checked").val() == null) {
            F_iva = 0;
        }
        //validar linea estrategica
        if ($("#ddlStrategicLines :selected").text() == 'Seleccione...') {
            F_linea_Es = 0;
        }
        //validar programas
        if ($("#ddlPrograms :selected").text() == 'Seleccione...') {
            F_objetivo = 0;
        }
        //validar tipo de proyectos
        //        if ($("#ddltype_proyect :selected").text() == 'Seleccione...') {
        //            F_tipo_pro = 0;
        //        }
        //validar poblacion
        if ($("#ddlPupulation :selected").text() == 'Seleccione...') {
            F_poblacion = 0;
        }
        //validar tipo de contratacion
        if ($("#ddlmodcontract :selected").text() == 'Seleccione....') {
            F_contrato = 0;
        }
        //validar nombre
        if ($("#ctl00_cphPrincipal_txtname").val() == '') {
            F_nombre = 0;
        }
        //validar justificacion
        if ($("#ctl00_cphPrincipal_txtjustification").val() == '') {
            F_justifica = 0;
        }
        //validarobjetivos
        if ($("#ctl00_cphPrincipal_txtobjective").val() == '') {
            F_objetivo_campo = 0;
        }
        //validar fecha de inicio
        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            F_fecha_ini = 0;
        }
    }
    //   alert("verificar_C_fechas" + verificar_C_fechas + "verificar_informacion_p:" + verificar_informacion_p + " verificar_actor:" + verificar_actor + " verificar_ubicaciones:" + verificar_ubicaciones + " verificar_componentes" + verificar_componentes + " verificar_flujos:" + verificar_flujos + " verificar_C_resultados:" + verificar_C_resultados + " fsc_exist" + fsc_exist);
    //VALIDAR RESULTADOS DE LAS VALIDACIONES
    if (F_fecha_ini == 1 && verificar_C_fechas == 1 && verificar_actor == 1 && verificar_ubicaciones == 1 && verificar_componentes == 1 && verificar_flujos == 1 && verificar_C_resultados == 1 && fsc_exist == 1 && verificar_informacion_p == 1) {

        $("#ctl00_cphPrincipal_containererrors").css("display", "none");

        //LAMAR FUNCION GUARDAR
        $("#ctl00_cphPrincipal_lblHelpresults").text("");
        $("#ctl00_cphPrincipal_Label10").text("");
        $("#ctl00_cphPrincipal_Label11").text("");
        $("#ctl00_cphPrincipal_Label23").text("");

        $("#ctl00_cphPrincipal_lblinfls").text("");
        $("#ctl0_cphPrincipal_lblinpro").text("");
        $("#ctl00_cphPrincipal_lblHelpname").text("");
        $("#ctl00_cphPrincipal_lblHelpjustification").text("");
        $("#ctl00_cphPrincipal_lblHelpobjective").text("");
        $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
        $("#ctl00_cphPrincipal_lbldia").text("");
        $("#ctl00_cphPrincipal_Lblmodcontract").text("");
        $("#ctl00_cphPrincipal_lblHelppopulation").text("");
        $("#ctl00_cphPrincipal_Lblhelpenddate").text("");
        $("#ctl00_cphPrincipal_Lblhelpiva").text("");
        $("#ctl00_cphPrincipal_Lblhelpduraton").text("");
        $("#ctl00_cphPrincipal_Lblhelpday").text("");

        //capturamos la url
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');

        if (sURLVariables[0] == "op=edit") {
            editar_idea();
            copiar_archivos();
        }
        else {
            Crear_idea();
            copiar_archivos();
        }
    }
    else {

        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        //validamos si creamos la idea o editamos
        if (sURLVariables[0] == "op=edit") {
            //CREAR MENSAJE
            mensaje_info_idea = "No se han realizados los cambios en la Idea. Existe información sin diligenciar, verifique la(s) pestaña(s): ";
        }
        else {

            //CREAR MENSAJE
            mensaje_info_idea = "La idea no ha sido creada. Existe información sin diligenciar, verifique la(s) pestaña(s): ";
        }
        //informacion
        if (verificar_informacion_p == 0) {
            //cuerpo del mensaje
            mensaje_info_idea += " Informacion Principal ";
            //nombre
            if (F_nombre == 0) {
                $("#ctl00_cphPrincipal_lblHelpname").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpname").text("");
            }
            //objetivo
            if (F_objetivo_campo == 0) {
                $("#ctl00_cphPrincipal_lblHelpobjective").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpobjective").text("");
            }
            //justificacion
            if (F_justifica == 0) {
                $("#ctl00_cphPrincipal_lblHelpjustification").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpjustification").text("");
            }
            //fecha
            if (F_fecha_ini == 0) {
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
            }
            //iva
            if (F_iva == 0) {
                $("#ctl00_cphPrincipal_Lblhelpiva").text("Escoja si o no");
            }
            else {
                $("#ctl00_cphPrincipal_Lblhelpiva").text("");
            }
            //linea estrategica
            if (F_linea_Es == 0) {
                $("#ctl00_cphPrincipal_lblinfls").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblinfls").text("");
            }
            //programa
            if (F_objetivo == 0) {
                $("#ctl00_cphPrincipal_lblinpro").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblinpro").text("");
            }
            //poblacion
            if (F_poblacion == 0) {
                $("#ctl00_cphPrincipal_lblHelppopulation").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelppopulation").text("");
            }
            //contrato 
            if (F_contrato == 0) {
                $("#ctl00_cphPrincipal_Lblmodcontract").text("Campo Requerido");
            }
            else {
                $("#ctl00_cphPrincipal_Lblmodcontract").text("");
            }
            //tipo de proyecto
            //            if (F_tipo_pro == 0) {
            //                $("#ctl00_cphPrincipal_Lblhelptproyect").text("Campo Requerido");
            //            }
            //            else {
            //                $("#ctl00_cphPrincipal_Lblhelptproyect").text("");
            //            }

        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpiva").text("");
            $("#ctl00_cphPrincipal_lblinfls").text("");
            $("#ctl00_cphPrincipal_lblinpro").text("");
            $("#ctl00_cphPrincipal_lblHelppopulation").text("");
            $("#ctl00_cphPrincipal_Lblmodcontract").text("");
            $("#ctl00_cphPrincipal_lblHelpjustification").text("");
            $("#ctl00_cphPrincipal_lblHelpobjective").text("");
            $("#ctl00_cphPrincipal_lblHelpname").text("");
            $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
        }
        //campos resutados
        if (verificar_C_resultados == 0) {
            if (verificar_informacion_p == 1) {
                mensaje_info_idea += " Informacion Principal ";
            }

            $("#ctl00_cphPrincipal_lblHelpresults").text("Algunos de los resultados debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Label10").text("Algunos de los resultados debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Label11").text("Algunos de los resultados debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Label23").text("Algunos de los resultados debe ser diligenciado.");
        }
        else {

            $("#ctl00_cphPrincipal_lblHelpresults").text("");
            $("#ctl00_cphPrincipal_Label10").text("");
            $("#ctl00_cphPrincipal_Label11").text("");
            $("#ctl00_cphPrincipal_Label23").text("");
        }
        //campos de dia y mes
        if (verificar_C_fechas == 0) {
            if (verificar_informacion_p == 1 && verificar_C_resultados == 1) {
                mensaje_info_idea += " Informacion Principal ";
            }

            $("#ctl00_cphPrincipal_Lblhelpduraton").text("El campo mes debe ser diligenciado.");
            $("#ctl00_cphPrincipal_Lblhelpday").text("El campo dia debe ser dilegenciado");
        }
        else {
            $("#ctl00_cphPrincipal_Lblhelpduraton").text("");
            $("#ctl00_cphPrincipal_Lblhelpday").text("");
        }

        //componente
        if (verificar_componentes == 0) {
            mensaje_info_idea += " Planeación Estratégica";
            $("#ctl00_cphPrincipal_Lblinformationcomponent").text("Debe tener almenos un componente");
        }
        else {
            $("#ctl00_cphPrincipal_Lblinformationcomponent").text("");
        }
        //ubicaciones
        if (verificar_ubicaciones == 0) {
            mensaje_info_idea += " Ubicación";
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("Debe tener almenos una ubicación");
        }
        else {
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("");
        }
        //actores
        if (verificar_actor == 0) {
            mensaje_info_idea += " Actores";
            $("#ctl00_cphPrincipal_Lblactorrep").text("Debe almenos tener un actor");
        }
        else {
            //existe la fundacion
            if (fsc_exist == 0) {
                mensaje_info_idea += " Actores:(La Funcacion Saldarriaga Concha debe ser un actor obligatorio)";
                $("#ctl00_cphPrincipal_Lblactorrep").text("La Funcacion Saldarriaga Concha debe ser un actor obligatorio");
            }
            else {
                $("#ctl00_cphPrincipal_Lblactorrep").text("");
            }
        }
        //flujos

        if (verificar_flujos == 0) {
            if (validar_flujos == 0) {
                mensaje_info_idea += " Flujos de pago (Los flujos generados no estan al 100%)";
                $("#ctl00_cphPrincipal_Lblinformation_flujos").text("Los desembolsos no estan al 100%");
            }
        }
        else {
            $("#ctl00_cphPrincipal_Lblinformation_flujos").text("");
        }
        $("#ctl00_cphPrincipal_containererrors").css("display", "block");
        $("#ctl00_cphPrincipal_Lblerrors_save_idea").text(mensaje_info_idea);
    }

}


function PartialSaved_onclick() {

    var validate_data = validate_partial();

    if (validate_data == 0) {
        $("#ctl00_cphPrincipal_containererrors").css("display", "none");
        PartialSaved();
    }
    else {

        $("#ctl00_cphPrincipal_containererrors").css("display", "block");
        $("#ctl00_cphPrincipal_Lblerrors_save_idea").text("no puede guardar parcialmente minimo debe tener un nombre la idea!");
    }
}


function validate_partial() {

    var F_nombre = 1;
    var validate_dato;

    if ($("#ctl00_cphPrincipal_txtname").val() == '') {
        F_nombre = 0;
    }

    if (F_nombre == 0) {
        $("#ctl00_cphPrincipal_lblHelpname").text("Campo Requerido");
        validate_dato = 1;
    }
    else {
        $("#ctl00_cphPrincipal_lblHelpname").text("");
        validate_dato = 0;
    }

    return validate_dato;
}

//crear idea 
function Crear_idea() {

    //crear arrays para el ingreso de las listas de json
    var listubicaciones = [];
    var listactores = [];
    var listflujos = [];
    var listdetallesflujos = [];
    var listfiles = [];

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
    listubicaciones = JSON.stringify(arrayUbicacion);
    //recorer array para el ingreso de actores
    listactores = JSON.stringify(arrayActor);
    //recorer array para el ingreso de flujos
    listflujos = JSON.stringify(arrayflujosdepago);
    //recorer array para el ingreso de los detalles flujos
    listdetallesflujos = JSON.stringify(matriz_flujos);
    //recorer array para el ingreso de flujos
    listfiles = JSON.stringify(arrayFiles);

    var tflujos = $("#ValueCostotal").text();
    tflujos = tflujos.replace(/\./gi, '');
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
            "Resultados_otros_resul": cambio_text($("#ctl00_cphPrincipal_Txtothersresults").val()),
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
            "cost": tflujos,
            "obligaciones": cambio_text($("#ctl00_cphPrincipal_Txtobligationsoftheparties").val()),
            "riesgo": cambio_text($("#ctl00_cphPrincipal_Txtriesgos").val()),
            "mitigacion": cambio_text($("#ctl00_cphPrincipal_Txtaccionmitig").val()),
            "presupuestal": cambio_text($("#ctl00_cphPrincipal_Txtroutepresupuestal").val()),
            "iva": valor_iva,
            "listcomponentes": Str_listcomponentes.toString(),
            "listubicaciones": listubicaciones,
            "listflujos": listflujos,
            "listdetallesflujos": listdetallesflujos,
            "listfiles": listfiles,
            "tipo_estado": $("#dll_estado").val(),
            "listactores": listactores

        },
        //mostrar resultados de la creacion de la idea
        success: function(result) {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveIdea").css("display", "none");
            $("#Export").css("display", "-webkit-inline-box");
            $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
            $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            $("#ctl00_cphPrincipal_Txtobligationsoftheparties").focus();
        },
        error: function() {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveIdea").css("display", "compact");
            $("#ctl00_cphPrincipal_lblsaveinkdformation").text("Se genero error al entrar a la operacion Ajax :");
        }
    });

}

//funcion que guarda la edicion de idea
function editar_idea() {

    //crear arrays para el ingreso de las listas de json
    var listubicaciones = [];
    var listactores = [];
    var listflujos = [];
    var listdetallesflujos = [];
    var listfiles = [];

    valor_iva = $("#ctl00_cphPrincipal_HDiva").val();

    var Str_listcomponentes = $("#componentesseleccionados").html();
    Str_listcomponentes = Str_listcomponentes.replace(/"/g, "_");
    Str_listcomponentes = Str_listcomponentes.replace(/<li/g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/li>/g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/</g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/>/g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/class=/g, "*");
    Str_listcomponentes = Str_listcomponentes.replace(/id=/g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/_add/g, "");
    Str_listcomponentes = Str_listcomponentes.replace(/_selectadd/g, "");

    //recorer array para el ingreso de ubicaciones
    listubicaciones = JSON.stringify(arrayUbicacion);
    //recorer array para el ingreso de actores
    listactores = JSON.stringify(arrayActor);
    //recorer array para el ingreso de flujos
    listflujos = JSON.stringify(arrayflujosdepago);
    //recorer array para el ingreso de los detalles flujos
    listdetallesflujos = JSON.stringify(matriz_flujos);
    //recorer array para el ingreso de flujos
    listfiles = JSON.stringify(arrayFiles);

    var tflujos = $("#ValueCostotal").text();
    tflujos = tflujos.replace(/\./gi, '');

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "POST",
        //crear json
        data: { "action": "edit", "code": $("#ctl00_cphPrincipal_txtid").val(),
            "linea_estrategica": $("#ddlStrategicLines").val(),
            "programa": $("#ddlPrograms").val(),
            "nombre": cambio_text($("#ctl00_cphPrincipal_txtname").val()),
            "justificacion": cambio_text($("#ctl00_cphPrincipal_txtjustification").val()),
            "objetivo": cambio_text($("#ctl00_cphPrincipal_txtobjective").val()),
            "objetivo_esp": cambio_text($("#ctl00_cphPrincipal_txtareadescription").val()),
            "Resultados_Benef": cambio_text($("#ctl00_cphPrincipal_txtresults").val()),
            "Resultados_Ges_c": cambio_text($("#ctl00_cphPrincipal_txtresulgc").val()),
            "Resultados_Cap_i": cambio_text($("#ctl00_cphPrincipal_txtresulci").val()),
            "Resultados_otros_resul": cambio_text($("#ctl00_cphPrincipal_Txtothersresults").val()),
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
            "cost": tflujos,
            "obligaciones": cambio_text($("#ctl00_cphPrincipal_Txtobligationsoftheparties").val()),
            "riesgo": cambio_text($("#ctl00_cphPrincipal_Txtriesgos").val()),
            "mitigacion": cambio_text($("#ctl00_cphPrincipal_Txtaccionmitig").val()),
            "presupuestal": cambio_text($("#ctl00_cphPrincipal_Txtroutepresupuestal").val()),
            "iva": valor_iva,
            "listcomponentes": Str_listcomponentes.toString(),
            "listubicaciones": listubicaciones,
            "listflujos": listflujos,
            "listdetallesflujos": listdetallesflujos,
            "listfiles": listfiles,
            "tipo_estado": $("#dll_estado").val(),
            "listactores": listactores

        },
        //mostrar resultados de la creacion de la idea
        success: function(result) {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveIdea").css("display", "none");
            $("#Export").css("display", "compact");
            $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
            $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            $("#ctl00_cphPrincipal_Txtobligationsoftheparties").focus();
        },
        error: function() {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveIdea").css("display", "compact");
            $("#ctl00_cphPrincipal_lblsaveinkdformation").text("Se genero error al entrar a la operacion Ajax :");
        }
    });

}

//funcion de cambio de comillas enters y tabulaciones
function cambio_text(str_txt) {

    str_txt = str_txt.replace(/\n/g, ' ');
    str_txt = str_txt.replace(/\r/g, '');
    str_txt = str_txt.replace(/\t/g, '');
    str_txt = str_txt.replace(/\n\r/g, ' ');
    str_txt = str_txt.replace(/\r\n/g, ' ');
    str_txt = str_txt.replace(/\"/g, '\"');

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

//valida si la idea fue aprobada y esconde el boton editar
function aprobacion_idea() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "aprobacion_idea", "ididea": ideditar },
        success: function(result) {

            if (result == 1) {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text("Esta Idea ya se encuentra aprobada y NO puede ser modificada!");
                $("#SaveIdea").css("display", "none");
                $("#dll_estado").attr("disabled", "disabled");
                $("#dll_estado").val(1);

            }
            else {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
                $("#SaveIdea").css("display", "compact");
                //   $("#dll_estado").val(3);
            }
        },
        error: function(msg) {
            alert("No se pueden cargar los documentos anexos de la idea = " + ideditar);
        }
    });
}

//funtion para borrar carpeta temp
function borrar_carpeta() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "borrar_archivos" },
        success: function(result) {
            //   alert("borrado");
        },
        error: function(msg) {
            alert("Ocurrio un problema al intentar eliminar los archivos temporales. ");
        }
    });
}

//FUNCION COPIAR ARCHIVOS DE LA CARPETA
function copiar_archivos() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "copiar_archivos" },
        success: function(result) {
            //  alert("copiado");
        },
        error: function(msg) {
            alert("Ocurrio un problema al intentar modificar los archivos temporales.");
        }
    });
}

//funcion de carga de listas
function load_combos() {

    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "load_combos", "type": "I" },
        success: function(result) {

            result = JSON.parse(result);

            charge_CatalogList(result[0], "ddlStrategicLines", 1);
            charge_CatalogList(result[1], "ddlmodcontract", 0);
            charge_CatalogList(result[5], "dll_estado", 1);
            charge_CatalogList(result[3], "ddlDepto", 1);
            charge_CatalogList(result[4], "ddlactors", 1);

        },
        error: function(msg) {
            alert("No se pueden cargar los combos de idea.");
        }
    });
}