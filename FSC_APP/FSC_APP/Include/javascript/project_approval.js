//Javascript para modulo de aprobacion proyecto por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 25/08/2013

$(document).ready(function() {

    //    consultaprojecto();
    $("#ctl00_cphPrincipal_containerSuccess2").css("display", "none");
    $("#tabsaproval").tabs();

    carga_eventos("ctl00_cphPrincipal_container_wait");
    validar_campofecha('ctl00_cphPrincipal_txtapprovaldate', 'ctl00_cphPrincipal_lblHelpapprovaldate');

    traer_proyectos_aprobados();
    traer_datos_proyecto();
    traer_actores_proyecto();

});

//funcion para traer los actores en un tabla del proyecto a aprobar
function traer_actores_proyecto() {

    $("#ddlproyect").change(function() {
        //traer actores de la idea
        $.ajax({
            url: "AjaxaddProjectApproval.aspx",
            type: "GET",
            data: { "action": "buscar_actores", "code": $(this).val() },
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
}



//funcion para traer los proyectos para aprobacion
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

function traer_datos_proyecto() {

    $("#ddlproyect").change(function() {

        //traer datos de la idea        
        $.ajax({
            url: "AjaxaddProjectApproval.aspx",
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
                    $("#ctl00_cphPrincipal_txtcreatedate").val(result.CreateDate);
                    $("#ctl00_cphPrincipal_txtiduser").val(result.user_fsc);


                }
            },
            error: function()
            { alert("No se pueden cargar los datos de la idea solicitada."); }
        });

    });
}


function consultaprojecto() {

    $("#ctl00_cphPrincipal_ddlproyect").change(function() {
        var idcode = $(this).val();
        //alert(idcode);
        window.location = "/FormulationAndAdoption/addProject.aspx?op=edit&id= " + idcode + "&apr=1";

    });
}