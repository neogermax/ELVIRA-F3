
//Javascript para modulo de Aprobacion Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

//validacion para combo box
$(document).ready(function() {

    //traer_ideas_aprobadas();
    //buscar_datos();

    validar_new_campos();
    validaraprobacion();
   
    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
    $("#tabsaproval").tabs();

    carga_eventos("ctl00_cphPrincipal_container_wait");
    validar_campofecha('ctl00_cphPrincipal_txtapprovaldate', 'ctl00_cphPrincipal_lblHelpapprovaldate');
    //$("#SaveApproval").button();


});


function traer_ideas_aprobadas() {

    $.ajax({
        url: "ajaxaddProjectApprovalRecord.aspx",
        type: "GET",
        data: { "action": "charge_idea_approval" },

        success: function(result) {

            alert(result);
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

//        validar_datos_idea(Id_idea);
//        traer_datos_idea(Id_idea);
//        traer_actores_idea(Id_idea);

    });
}




function validar_new_campos() {

    $("#ctl00_cphPrincipal_ddlidproject").change(function() {
        $.ajax({
            url: "ajaxaddProjectApprovalRecord.aspx",
            type: "GET",
            data: { "action": "validar_campos_new", "code": $(this).val() },
            success: function(result) {

                if (result != "Falta por diligenciar :") {

                    $("#ctl00_cphPrincipal_Lblnewcampos").text(result);
                    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "block");
                    $("#ctl00_cphPrincipal_btnAddData").css("display", "none");
                }
                else {
                    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
                }

            },
            error: function()
            { alert("No se pueden cargar las validaciones solicitadas."); }
        });

    });

}


//function operacionesaprobacion() {

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


//    $("#ctl00_cphPrincipal_TxtaportFSC").blur(function() {
//        var rev = $(this).val();
//        rev = rev.replace(/\./gi, '');
//        var val = parseInt(rev);

//        if (isNaN(val)) {
//            val = 0;
//            $("#ctl00_cphPrincipal_TxtaportFSC").val(val);
//        }
//        else {
//            var rev2 = $("#ctl00_cphPrincipal_Txtaportcontra").val();

//            rev2 = rev2.replace(/\./gi, '');
//            var val2 = parseInt(rev2);
//            if (isNaN(val2)) { val2 = 0; }
//            else {
//                var suma = 0;
//                suma = val + val2;
//                addCommas(suma);
//            }
//        }
//    });

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
//                addCommas(suma);
//            }
//        }
//    });
//}

//function addCommas(str) {
//    var amount = new String(str);
//    amount = amount.split("").reverse();

//    var output = "";
//    for (var i = 0; i <= amount.length - 1; i++) {
//        output = amount[i] + output;
//        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
//    }
//    $("#ctl00_cphPrincipal_txtapprovedvalue").val(output);
//    $("#ctl00_cphPrincipal_HDvalue").val(output);
//}

function validaraprobacion() {
    //validar campos llenos
    $("#ctl00_cphPrincipal_ddlidproject").change(function() {
        $("#ctl00_cphPrincipal_btnAddData").css("display", "block");
        $("#ctl00_cphPrincipal_Label4").text("");
        $("#ctl00_cphPrincipal_Label5").text("");

        $.ajax({
            url: "ajaxaddProjectApprovalRecord.aspx",
            type: "GET",
            data: { "action": "validarideas", "code": $(this).val(), "name": $('#ctl00_cphPrincipal_ddlidproject option:selected').html() },
            success: function(result) {
                if (result != "") {
                    result = JSON.parse(result);
                    if (result.mensaje != "") {

                        //alert(result.mensaje);
                        $("#ctl00_cphPrincipal_Label4").text("Falta diligenciar : " + result.mensaje);
                        $("#ctl00_cphPrincipal_btnAddData").css("display", "none");
                        //  window.location = "/FSC/ResearchAndDevelopment/searchIdea.aspx";
                    }
                    else
                    //  $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
                        $("#ctl00_cphPrincipal_txtcodeapproved").val(result.noaprobacion);
                }
            },
            error: function()
            { alert("No se pueden cargar las validaciones solicitadas."); }
        });
        //traer datos de la idea        
        $.ajax({
            url: "ajaxaddProjectApprovalRecord.aspx",
            type: "GET",
            data: { "action": "buscar", "code": $(this).val() },
            success: function(result) {
                result = JSON.parse(result);
                if (result == "0") {
                    $("#ctl00_cphPrincipal_Label5").text("falta por diligenciar: línea estratégica ");
                    $("#ctl00_cphPrincipal_btnAddData").css("display", "none");

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
                    //$("#gridthird").val(result.html);
                }
            },
            error: function()
            { alert("No se pueden cargar los datos de la idea solicitada."); }
        });
        //traer actores de la idea
        $.ajax({
            url: "ajaxaddProjectApprovalRecord.aspx",
            type: "GET",
            data: { "action": "buscaractores", "code": $(this).val() },
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
    });

    //validacion para la fecha que no sea mayor de la fecha actual 

    $("#ctl00_cphPrincipal_txtapprovaldate").blur(function() {
        var fecha = new Date();
        var fechacampo = new Date($("#ctl00_cphPrincipal_txtapprovaldate").val());

        if (fechacampo > fecha) {
            alert("la fecha debe ser menor al dia de hoy.");
            $("#ctl00_cphPrincipal_txtapprovaldate").val("");
            $("#ctl00_cphPrincipal_txtapprovaldate").focus();
        }
    });

}

