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
var arrayValorflujoTotal = [];
var arrayinputflujos = [];

var arrayflujosdepago = [];
var arrayflujosdepago_ed = [];

var matriz_flujos = [];
var matriz_flujos_ed = [];

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

var swhich_flujos_exist;

var idea_buscar;

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

$(document).ready(function() {

    //capturamos la url
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    //validamos si creamos la idea o editamos
    if (sURLVariables[0] == "op=edit") {
        ideditar = sURLVariables[1].replace("id=", "");

        operacionesIdea();
        actors_transanccion();
        comboactor();

        //        var timer = setTimeout("fix();", 2000);
        validafecha();
        validafecha2();

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
        cargarcomponente();

        //edit_component_view();

        //        view_ubicacion();
        //        view_ubicacion_array();

        // View_actores();
        // View_actores_array();

        //     View_matriz_principal();

        //        View_flujos_p();
        //        View_flujos_p_array();

        //        View_flujos_actors();
        //        View_flujos_actors_array();

        //        View_detalle_flujo_array();

        //                aprobacion_idea();

        // View_anexos();
        //View_anexos_array();


        //var timer_cline_edit = setTimeout("ClineEstrategic_edit();", 2000);

        var timer_cline_edit = setTimeout("Cpopulation_view();", 2000);
        var timer_cline_edit = setTimeout("Ctypcontract_view();", 2000);
        var itemarrayflujos = 0;

        $("#SaveIdea").css("display", "none");
        //        $("#Export").css("display", "block");
    }
    else {

        operacionesIdea();
        actors_transanccion();
        comboactor();
        cargar_ideas_aprobadas();
        //traer datos de la idea
        verificar_dat_idea();

        var timer = setTimeout("fix();", 2000);
        validafecha();
        validafecha2();

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
        //        $("#Export").css("display", "none");
    }

    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containererrors").css("display", "none");


    $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");

    $("#tabsIdea").tabs();

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
    //    $("#Export").button();
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
            valor_iva = 1;
        }
        else {
            $("#ctl00_cphPrincipal_vrdiner").text("Vr Dinero");
            valor_iva = 0;
        }

    });

    $("#tabsproyecto").tabs();


    //validar que pestaña esta ingresando
    $("#tabsproyecto").click(function() {

        var active = $("#tabsproyecto").tabs("option", "active");
        var idtabs = $("#tabsproyecto ul>li a").eq(active).attr('href');
        //validar si esl la de flujos de pago
        if (idtabs == "#flujos") {
            //    alert(idtabs);
            //validar si es la primera entrada       
            if (entradaflujos == 0) {
                var tamaño_flujos = $("#T_Actorsflujos tr").length - 2;

                //validar la cantidad de actores
                if (tamaño_flujos == 1) {
                    var Aflujos = arrayActorFlujo[itemarrayflujos].actorsVal;
                    //   alert(Aflujos);
                    $("#txtinput" + Aflujos).attr("disabled", "disabled");
                    $("#desenbolso" + Aflujos).text("");


                    entradaflujos = 1;
                    s_revisarflujos = 1;
                }
            }

            $("#tabsproyecto").tabs({ active: 4 });
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


//funcion q verifica los datos de la idea si los trae o no
function verificar_dat_idea() {

    $("#ddlididea").change(function() {

        //capturamos el valor de la idea del combo de ideas aprobadas
        idea_buscar = $(this).val();

        confirmar = confirm("¿Traer datos de la Idea aprobada?", "SI", "NO");
        if (confirmar) {

            //pestaña descripcion del proyecto
            traer_datos_idea_inf_p();
            View_matriz_principal();

            //pestaña ubicacion del proyecto
            view_ubicacion();
            view_ubicacion_array();

            //pestaña actores del proyecto
            View_actores();
            View_actores_array();

            //pestaña flujos del proyecto
            View_flujos_p();
            View_flujos_p_array();

            View_flujos_actors();
            View_flujos_actors_array();

            View_detalle_flujo_array();

            //pestaña anexos del proyecto
            View_anexos();
            View_anexos_array();

            //pestaña componentes del proyecto
            ClineEstrategic_edit();
            edit_component_view();

            //bloquemos los controles de la pestaña componentes
            $("#ddlStrategicLines").attr("disabled", "disabled");
            $("#ddlPrograms").attr("disabled", "disabled");
            $("#Btnaddcomponent").attr("disabled", "disabled");
            $("#Btndeletecomponent").attr("disabled", "disabled");


        }
        else {
        }

    });

}