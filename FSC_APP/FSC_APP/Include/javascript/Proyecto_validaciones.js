//funcion para validar si el proyecto esta aprobado
function aprobacion_proyecto() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "aprobacion_proyecto", "idproject": ideditar },
        success: function(result) {

            if (result == 1) {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text("Este Proyecto ya se encuentra aprobado y NO puede ser modificado!");
                $("#SaveProject").css("display", "none");
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



// funcion validar proyecto madre
function validar_proyecto_madre() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "proyecto_madre", "idproject": ideditar },
        success: function(result) {

            if (result == 1) {
                desabled_mother();

                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text("Este es un proyecto Madre, No puede ser modificado.");

            }

        },
        error: function(msg) {
            alert("No se pueden cargar los documentos anexos de la idea = " + ideditar);
        }
    });

}


//funcion para desabilitar todos los controles si es proyecto madre
function desabled_mother() {

    //componentes
    $("#ddlStrategicLines").attr("disabled", "disabled");
    $("#ddlPrograms").attr("disabled", "disabled");
    $("#Btnaddcomponent").attr("disabled", "disabled");
    $("#Btndeletecomponent").attr("disabled", "disabled");

    //informacion
    $("#ctl00_cphPrincipal_txtid").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtname").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtjustification").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtobjective").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtareadescription").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtresults").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtresulgc").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtresulci").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtothersresults").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtstartdate").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtduration").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtday").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtdatecierre").attr("disabled", "disabled");
    $("#ddlPupulation").attr("disabled", "disabled");
    $("#ddlmodcontract").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtobligationsoftheparties").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtriesgos").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtaccionmitig").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtroutepresupuestal").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtcode").attr("disabled", "disabled");
    $("#dll_estado").attr("disabled", "disabled");


    //ubicacion
    $("#ddlDepto").attr("disabled", "disabled");
    $("#ddlCity").attr("disabled", "disabled");
    $("#B_add_location").attr("disabled", "disabled");

    //actores
    $("#ddlactors").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_linkactors").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_ddlType").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtcontact").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtcedulacont").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txttelcont").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtemail").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtvrdiner").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtvresp").attr("disabled", "disabled");
    $("#BtnaddActors").attr("disabled", "disabled");

    //flujos
    $("#ctl00_cphPrincipal_txtvalortotalflow").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtfechapago").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtporcentaje").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_txtentregable").attr("disabled", "disabled");
    $("#Btn_add_flujo").attr("disabled", "disabled");

    //anexos
    $("#fileupload").attr("disabled", "disabled");
    $("#ctl00_cphPrincipal_Txtdecription").attr("disabled", "disabled");
    $("#Btncharge_file").attr("disabled", "disabled");

    //escondemos los div de proyecto madre
    $("#ctl00_cphPrincipal_container_date_mother_actores").css("display", "none");
    $("#ctl00_cphPrincipal_container_date_mother_flujos").css("display", "none");
    $("#ctl00_cphPrincipal_container_date_mother").css("display", "none");

    $("#SaveProject").css("display", "none");
    $("#dll_estado").val(1);
}


function traer_valores_madre(Str_estado_creacion, Str_id_project) {

    if (Str_id_project == 0) {
        Str_id_project = idea_buscar;
    }

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "traer_valores_madre", "ididea": Str_id_project, "estado": Str_estado_creacion },
        success: function(result) {

            result = JSON.parse(result);

            var total_value = addCommasrefactor(result.total_value_mother);
            var disponible = addCommasrefactor(result.ressiduo_valor_mother);

            $("#ctl00_cphPrincipal_Txtvalor_mother").val(total_value);
            $("#ctl00_cphPrincipal_Txtvalor_disponible").val(disponible);
            $("#ctl00_cphPrincipal_Txtdate_start_mother").val(result.BeginDate);
            $("#ctl00_cphPrincipal_Txtdate_end_mother").val(result.completiondate);

            $("#ctl00_cphPrincipal_Txtvalor_mother_flujos").val(total_value);
            $("#ctl00_cphPrincipal_Txtvalor_disponible_flujos").val(disponible);
            $("#ctl00_cphPrincipal_Txtdate_start_mother_flujos").val(result.BeginDate);
            $("#ctl00_cphPrincipal_Txtdate_end_mother_flujos").val(result.completiondate);

            $("#ctl00_cphPrincipal_Txtvalor_mother_actores").val(total_value);
            $("#ctl00_cphPrincipal_Txtvalor_disponible_actores").val(disponible);
            $("#ctl00_cphPrincipal_Txtdate_start_mother_actores").val(result.BeginDate);
            $("#ctl00_cphPrincipal_Txtdate_end_mother_actores").val(result.completiondate);

            $("#ctl00_cphPrincipal_container_date_mother_actores").css("display", "block");
            $("#ctl00_cphPrincipal_container_date_mother_flujos").css("display", "block");
            $("#ctl00_cphPrincipal_container_date_mother").css("display", "block");

            flujos_disponible = result.ressiduo_valor_mother;
            flujos_disponible = parseInt(flujos_disponible);

            var fecha_actual = new Date();
            fecha_limite_madre = new Date(result.completiondate);
            fecha_inicial_madre = new Date(result.BeginDate);

            var mensaje;
            var S_mensaje_flujo;
            var S_mensaje_fecha;
            //validamos saldo disponible
            if (flujos_disponible <= 0) {
                S_mensaje_flujo = 1;
            }
            else {
                S_mensaje_flujo = 0;
            }
            //validacion para la fecha limite del proyecto madre
            if (fecha_actual > fecha_limite_madre) {
                S_mensaje_fecha = 1;
            }
            else {
                S_mensaje_fecha = 0;
            }

            if (S_mensaje_fecha == 0 && S_mensaje_flujo == 0) {

                $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
                $("#ctl00_cphPrincipal_lblsaveinformation").text("");
                $("#SaveProject").css("display", "block");
                $("#Export").css("display", "none");

            }

            else {

                if (S_mensaje_fecha == 1 && S_mensaje_flujo == 1) {
                    mensaje = " No tiene saldo disponible en el proyecto Madre y el tiempo para este proyecto expiro! "
                }
                else {

                    if (S_mensaje_flujo == 1) {
                        mensaje = "No tiene saldo disponible en el proyecto Madre!";
                    }
                    if (S_mensaje_fecha == 1) {
                        mensaje = " El tiempo para este proyecto expiro!";
                    }

                }

                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text(mensaje);
                $("#SaveProject").css("display", "none");
                //$("#Export").css("display", "-webkit-inline-box");

            }

        },
        error: function(msg) {
            alert("No se pueden cargar los fdatos del proyecto madre ");
        }
    });

}



//funcion q verifica los datos de la idea si los trae o no
function verificar_dat_idea() {

    $("#ddlididea").change(function() {

        //capturamos el valor de la idea del combo de ideas aprobadas
        idea_buscar = $(this).val();
        str_ideabuscar = $("#ddlididea option:selected").text();
        validar_ini_ed = 0;

        traer_valores_madre("crear", 0);

        $("#SaveProject").css("display", "block");

        confirmar = confirm("¿Traer datos de la Idea aprobada?", "SI", "NO");
        if (confirmar) {

            componentes_editados = 1;
            //pestaña descripcion del proyecto
            traer_datos_idea_inf_p();
            //pestaña ubicacion del proyecto
            view_ubicacion_array();
            //pestaña actores del proyecto
            //View_actores_array();
            //pestaña flujos del proyecto
            // View_flujos_p_array();
            // View_flujos_actors_array();
            // View_detalle_flujo_array();

            //pestaña anexos del proyecto
            View_anexos_array();
            //pestaña componentes del proyecto
            $("#ddlStrategicLines").ready(function() {
                ClineEstrategic_edit();
            });

            edit_component_view();

            charge_date_list_idea(idea_buscar);

            //bloquemos los controles de la pestaña componentes
            $("#ddlStrategicLines").attr("disabled", "disabled");

            validar_ini_ed = 0;
        }
        else {

            //pestaña componentes del proyecto
            $("#ddlStrategicLines").ready(function() {
                ClineEstrategic_edit();
            });

            edit_component_view();

            //bloquemos los controles de la pestaña componentes
            $("#ddlStrategicLines").attr("disabled", "disabled");
            validar_ini_ed = 0;
            
            charge_date_list_idea(idea_buscar);

        }

    });

}
