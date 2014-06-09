//Javascript para modulo de aprobacion proyecto por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 25/08/2013

var confirmacion = 0;

$(document).ready(function() {

    //    consultaprojecto();
    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");


    $("#tabsaproval").tabs();

    carga_eventos("ctl00_cphPrincipal_container_wait");
    validar_campofecha('ctl00_cphPrincipal_txtapprovaldate', 'ctl00_cphPrincipal_lblHelpapprovaldate');

    traer_proyectos_aprobados();
    buscar_datos();
    //operacionesaprobacion();
    $("#SaveApproval").button();
    $("#ctl00_cphPrincipal_linkcancelar").button();
   
    

});


//funcion para cargar el combo con los proyectos para aprobacion 
function traer_proyectos_aprobados() {

    $.ajax({
        url: "AjaxaddProjectApproval.aspx",
        type: "GET",
        data: { "action": "charge_proyect_approval" },

        success: function(result) {

            $("#ddlproyect").html(result);
            $("#ddlproyect").trigger("liszt:updated");

        },
        error: function()
        { alert("No se pueden cargar el combo de proyecto para aprobar."); }
    });

}

//traer datos segun la seleccion delproyecto aprovado
function buscar_datos() {

    $("#ddlproyect").change(function() {

        var Id_proyect = $(this).val();

        validar_datos_proyecto(Id_proyect);

        traer_datos_proyecto(Id_proyect);
        traer_actores_proyecto(Id_proyect);

    });
}


function validar_datos_proyecto(STR_Id_Project) {

    $.ajax({
        url: "AjaxaddProjectApproval.aspx",
        type: "GET",
        data: { "action": "validar_modulo_completo", "code": STR_Id_Project },
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
                $("#ctl00_cphPrincipal_HDline").val("");
                $("#ctl00_cphPrincipal_HDprogram").val("");
                $("#ctl00_cphPrincipal_HDidea").val("");
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


//funcion para traer los actores en un tabla del proyecto a aprobar
function traer_actores_proyecto(STR_Id_Project) {

    //traer actores de la idea
    $.ajax({
        url: "AjaxaddProjectApproval.aspx",
        type: "GET",
        data: { "action": "buscar_actores", "code": STR_Id_Project },
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



function traer_datos_proyecto(STR_Id_Project) {

    //traer datos de la idea        
    $.ajax({
        url: "AjaxaddProjectApproval.aspx",
        type: "GET",
        data: { "action": "buscar", "code": STR_Id_Project },
        success: function(result) {
            result = JSON.parse(result);
            if (result == "0") {

            }
            else {
                $("#ctl00_cphPrincipal_Txtnameidea").val(result.name);
                $("#ctl00_cphPrincipal_Txtline").val(result.line);
                $("#ctl00_cphPrincipal_Txtprogram").val(result.program);
                $("#ctl00_cphPrincipal_txtapprovedvalue").val(result.value);
                $("#ctl00_cphPrincipal_HDline").val(result.line);
                $("#ctl00_cphPrincipal_HDprogram").val(result.program);
                $("#ctl00_cphPrincipal_HDidea").val(result.name);
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


function SaveApproval_onclick() {

    var value_campos = Validar_campos_dilingenciados();

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

        guardar_aprobacion_proyecto();

    }
}


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


function guardar_aprobacion_proyecto() {

    var id_idea = $("#ddlproyect option:selected").html();
    alert(id_idea);

    $.ajax({
        url: "AjaxaddProjectApproval.aspx",
        type: "POST",
        //crear json
        data: { "action": "save",
            "id_project": $("#ddlproyect").val(),
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

////function operacionesaprobacion() {

//    $("#ctl00_cphPrincipal_btnAddData").click(function() {

//        var rev = $("#ctl00_cphPrincipal_TxtaportFSC").val();
//        rev = rev.replace(/\./gi, '');
//        var vd = parseInt(rev);

//        var rev2 = $("#ctl00_cphPrincipal_Txtaportcontra").val()
//        rev2 = rev2.replace(/\./gi, '');
//        var ve = parseInt(rev2);

//        if (isNaN(vd)) { vd = 0; }
//        else {
//            if (isNaN(ve)) { ve = 0; }
//            else {
//                var suma = 0;
//                suma = vd + ve;
//                addCommas(suma);
//            }
//        }
//        suma = vd + ve;
//        addCommas(suma);
//    });


////    $("#ctl00_cphPrincipal_TxtaportFSC").blur(function() {
////        var rev = $(this).val();
////        rev = rev.replace(/\./gi, '');
////        var val = parseInt(rev);

////        if (isNaN(val)) {
////            val = 0;
////            $("#ctl00_cphPrincipal_TxtaportFSC").val(val);
////        }
////        else {
////            var rev2 = $("#ctl00_cphPrincipal_Txtaportcontra").val();

////            rev2 = rev2.replace(/\./gi, '');
////            var val2 = parseInt(rev2);
////            if (isNaN(val2)) { val2 = 0; }
////            else {
////                var suma = 0;
////                suma = val + val2;

////                $("#ctl00_cphPrincipal_txtapprovedvalue").val(addCommasrefactor(suma));
////                $("#ctl00_cphPrincipal_HDvalue").val(addCommasrefactor(suma));
////            }
////        }
////    });

//    $("#ctl00_cphPrincipal_Txtaportcontra").blur(function() {
//        var rev = $(this).val();
//        rev = rev.replace(/\./gi, '');
//        var val = parseInt(rev);
//        if (isNaN(val)) {
//            val = 0;
//            $("#ctl00_cphPrincipal_Txtaportcontra").val(val);
//        }
//        else {
//            var rev2 = $("#ctl00_cphPrincipal_TxtaportFSC").val();
//            rev2 = rev2.replace(/\./gi, '');
//            var val2 = parseInt(rev2);
//            if (isNaN(val2)) { val2 = 0; }
//            else {
//                var suma = 0;
//                suma = val + val2;

//                $("#ctl00_cphPrincipal_txtapprovedvalue").val(addCommasrefactor(suma));
//                $("#ctl00_cphPrincipal_HDvalue").val(addCommasrefactor(suma));
//            }
//        }
//    });
//}






