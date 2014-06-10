//Javascript para modulo de Aprobacion Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

var confirmacion = 0;

$(document).ready(function() {

    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");

    $("#tabsaproval").tabs();

    carga_eventos("ctl00_cphPrincipal_container_wait");
    validar_campofecha('ctl00_cphPrincipal_txtapprovaldate', 'ctl00_cphPrincipal_lblHelpapprovaldate');

    traer_ideas_aprobadas();
    buscar_datos();
    Valida_fecha_acta();
    
    $("#SaveApproval").button();
    $("#ctl00_cphPrincipal_linkcancelar").button();


});

//funcion para cargar el combo con las ideas para aprobacion 
function traer_ideas_aprobadas() {

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "GET",
        data: { "action": "charge_idea_approval" },

        success: function(result) {

            $("#ddlidproject").html(result);
            $("#ddlidproject").trigger("liszt:updated");

        },
        error: function()
        { alert("No se pueden cargar el combo de proyecto para aprobar."); }
    });

}


//traer datos segun la seleccion de la idea aprovada
function buscar_datos() {

    $("#ddlidproject").change(function() {

        var Id_idea = $(this).val();

        traer_datos_idea(Id_idea);
        traer_actores_idea(Id_idea);
        validar_datos_idea(Id_idea);

    });
}

// funcion q valida que cumpla con los requisitos para aprobar la idea
function validar_datos_idea(STR_Id_Idea) {

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "GET",
        data: { "action": "validar_modulo_completo", "code": STR_Id_Idea },
        success: function(result) {

            if (result != "VACIO") {

                $("#ctl00_cphPrincipal_Lblnewcampos").text(result);
                $("#ctl00_cphPrincipal_containerSuccess2").css("display", "block");
                $("#SaveApproval").css("display", "none");

                //LIMPIAMOS CAMPOS
                $("#ctl00_cphPrincipal_Txtnameidea").val("");
                $("#ctl00_cphPrincipal_Txtline").val("");
                $("#ctl00_cphPrincipal_Txtprogram").val("");
                $("#ctl00_cphPrincipal_txtapprovedvalue").val("");
                $("#ctl00_cphPrincipal_TxtaportFSC").val("");
                $("#ctl00_cphPrincipal_Txtaportcontra").val("");
                $("#ctl00_cphPrincipal_txtcreatedate").val("");
                $("#ctl00_cphPrincipal_txtiduser").val("");

                $("#gridthird").css("display", "none");
            }
            else {
                $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
                $("#SaveApproval").css("display", "block");

                $("#gridthird").css("display", "block");
            }


        },
        error: function()
        { alert("No se pueden cargar los actores de la idea solicitada."); }
    });
}


//funcion para traer los actores en un tabla de la idea a aprobar
function traer_actores_idea(STR_Id_Idea) {

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "GET",
        data: { "action": "buscaractores", "code": STR_Id_Idea },
        success: function(result) {
            $("#gridthird").html(result);

            $("#T_Actors").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });
        },
        error: function()
        { alert("No se pueden cargar los actores de la idea solicitada."); }
    });
}


//traer datos de la idea        
function traer_datos_idea(STR_Id_Idea) {

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "GET",
        data: { "action": "buscar", "code": STR_Id_Idea },
        success: function(result) {
            result = JSON.parse(result);
            if (result == "0") {

            }
            else {
                $("#ctl00_cphPrincipal_Txtnameidea").val(result.name);
                $("#ctl00_cphPrincipal_Txtline").val(result.line);
                $("#ctl00_cphPrincipal_Txtprogram").val(result.program);
                $("#ctl00_cphPrincipal_txtapprovedvalue").val(result.value);
                $("#ctl00_cphPrincipal_TxtaportFSC").val(result.FSC);
                $("#ctl00_cphPrincipal_Txtaportcontra").val(result.otro);
                $("#ctl00_cphPrincipal_txtcreatedate").val(result.CreateDate);
                $("#ctl00_cphPrincipal_txtiduser").val(result.user_fsc);
            }
        },
        error: function()
        { alert("No se pueden cargar los datos de la idea solicitada."); }
    });
}


//funcion q habilita el guardado de la  aprobacion proyecto
function SaveApproval_onclick() {

    //revisamos q los campos esten diligenciados
    var value_campos = Validar_campos_dilingenciados();

    //habilitamos confirmacion
    if (confirmacion == 0) {
        if (value_campos == 1) {

            $("#SaveApproval").attr("value", "Aprobar Proyecto");
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
            $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");

        }
        else {

            $("#SaveApproval").attr("value", "Confirmar aprobación");
            $("#ctl00_cphPrincipal_lblsaveinformation").text("Está seguro que desea aprobar el proyecto?, tenga en cuenta que una vez aprobado NO podrá ser modificado.");
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
            $("#ctl00_cphPrincipal_lblsaveinformation_down").text("Está seguro que desea aprobar el proyecto?, tenga en cuenta que una vez aprobado NO podrá ser modificado.");

            confirmacion = 1;
        }
    }
    else {
        //llamamos funcion que guarda
        guardar_aprobacion_proyecto();

    }
}

//funcion que valida campos diligenciados
function Validar_campos_dilingenciados() {

    var valida_campos;

    if ($("#ctl00_cphPrincipal_txtapprovaldate").val() == "" || $("#ctl00_cphPrincipal_txtactnumber").val() == "") {

        if ($("#ctl00_cphPrincipal_txtapprovaldate").val() == "") {
            $("#ctl00_cphPrincipal_lblHelpapprovaldate").text("Campo obligatorio");
            $("#ctl00_cphPrincipal_txtapprovaldate").focus();
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpapprovaldate").text("");
        }

        if ($("#ctl00_cphPrincipal_txtactnumber").val() == "") {
            $("#ctl00_cphPrincipal_lblHelpactnumber").text("Campo obligatorio");
            $("#ctl00_cphPrincipal_txtactnumber").focus();
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpactnumber").text("");
        }
        valida_campos = 1;
    }
    else {
        $("#ctl00_cphPrincipal_lblHelpapprovaldate").text("");
        $("#ctl00_cphPrincipal_lblHelpactnumber").text("");
        valida_campos = 0;
    }

    return valida_campos;
}

//funcion guardar aprobacion del proyecto
function guardar_aprobacion_proyecto() {

    var id_idea = $("#ddlidproject option:selected").html();

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "POST",
        //crear json
        data: { "action": "save",
            "id_project": $("#ddlidproject").val(),
            "id_idea": id_idea,
            "date": $("#ctl00_cphPrincipal_txtapprovaldate").val(),
            "N_acta": $("#ctl00_cphPrincipal_txtactnumber").val(),
            "aport_others": $("#ctl00_cphPrincipal_Txtaportcontra").val(),
            "aport_fsc": $("#ctl00_cphPrincipal_TxtaportFSC").val(),
            "aport_total": $("#ctl00_cphPrincipal_txtapprovedvalue").val(),
            "approval": $("#ctl00_cphPrincipal_ddlapproved").val()
        },
        //mostrar resultados de la creacion de la idea
        success: function(result) {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
            $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
            $("#ctl00_cphPrincipal_lblsaveinformation_down").text(result);
            $("#SaveApproval").css("display", "none");
        },
        error: function() {
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveApproval").css("display", "block");
            $("#ctl00_cphPrincipal_lblsaveinformation").text("Se genero error al entrar a la operacion Ajax :");
            $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");

        }
    });

}

//validacion para la fecha que no sea mayor de la fecha actual
function Valida_fecha_acta() {

    $("#ctl00_cphPrincipal_txtapprovaldate").change(function() {
        var fecha = new Date();
        var fechacampo = new Date($("#ctl00_cphPrincipal_txtapprovaldate").val());

        if (fechacampo < fecha) {
            alert("la fecha debe ser menor al dia de hoy.");
            $("#ctl00_cphPrincipal_txtapprovaldate").val("");
            $("#ctl00_cphPrincipal_txtapprovaldate").focus();
        }
    });
}



