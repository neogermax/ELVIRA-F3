//Javascript para modulo de carga masiva por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 25/08/2013

//validacion para combo box
$(document).ready(function() {
    $("#ctl00_cphPrincipal_containerSuccess").css("display : none");
    revcombo();
});

function revcombo() {

    $("#ctl00_cphPrincipal_ddlproyect").change(function() {
        var idcode = $(this).val();
        var nameproyect = $('#ctl00_cphPrincipal_ddlproyect option:selected').html();
        //alert(idcode);
        $("#ctl00_cphPrincipal_HFid").val(idcode);
        $("#ctl00_cphPrincipal_Lbltitle").text("proyecto: " + nameproyect);
        $("#ctl00_cphPrincipal_HFtitle").val("proyecto: " + nameproyect);
        $("#ctl00_cphPrincipal_containerSuccess").css("display : none");
    });
}

//function guardar() {
//    $("#ctl00_cphPrincipal_Btnuppcharge").change(function() {
//        $.ajax({
//        url: "ajaxaddprojectchargemasive.aspx",
//            type: "GET",
//            data: { "action": "validar", "id": $("#ctl00_cphPrincipal_HFid").val() },
//            success: function(result) {
//                result = JSON.parse(result);
//                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
//                $("#ctl00_cphPrincipal_Lblmsginfo").val(result.contact);
//              

//            },
//            error: function()
//            { alert("los datos de terceros no pudieron ser cargados."); }
//        });
//    });

//}