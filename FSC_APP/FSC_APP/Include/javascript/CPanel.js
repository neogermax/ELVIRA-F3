//-Carga de la página-//
$(document).ready(function() {
//-Deshabilitado y cambio cursor-//
    deshabilitar("#btnlearnedlessons");
    deshabilitar("#btnproceedclose");
    deshabilitar("#btneditcont");
    deshabilitar("#btnapproval");
    deshabilitar("#btnmodification");
    deshabilitar("#btntracing");
    deshabilitar("#btnindicators");
    deshabilitar("#btnreports");
    deshabilitar("#btnriskmanagement");
    deshabilitar("#btncrono");
    //-Cursor mano-//
    habilitar("#ViewProject");
    habilitar("#btnprojectapproval");
    habilitar("#btncontratacion");
    habilitar("#btnproceed");
})

//-Formatos botones-//
function habilitar(control) {
    $(control).css("cursor", "pointer");
    $(control).css("opacity", "1");
};

function deshabilitar(control) {
    $(control).css("opacity", "0.5");
    $(control).css("cursor", "not-allowed");
    $(control).attr('onclick','').unbind('click');
};

//-Funciones del panel de control-//
function ViewProject_onclick() {
    window.location.href = '/FormulationAndAdoption/addProject.aspx?op=edit&id=' + getParameterByName('id');
};


function ProjectApproval_onclick() {
    window.location.href = '/FormulationAndAdoption/addProject.aspx?op=edit&id=' + getParameterByName('id') + '&apr=1';
};

function Contract_onclick() {

    //Obtener estado de contratación//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getcontractstatus", "proyectid": getParameterByName('id') },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                window.location.href = '/Engagement/addContractRequest.aspx?op=edit&ID=' + result;
            } else {
                window.location.href = '/Engagement/addContractRequest.aspx?op=add';
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos de contratación.");
        }
    });
};

function Proceed_onclick() {
    window.location.href = '/FomsProceedings/Proceedings_stars.aspx?cid=' + getParameterByName('id');
};

//-Extraer parametros QueryString-//
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}