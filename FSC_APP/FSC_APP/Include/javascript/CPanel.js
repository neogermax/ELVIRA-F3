//-Carga de la página-//
$(document).ready(function() {
    //-Establecer estado inicial de los controles-//
    inicial();
    //-Verificar el estado del panel de control-//
    verifica();
})

//-Deshabilitar todo-//
function inicial() {
//    var botones = ["#btnlearnedlessons", "btnproceedclose", "btneditcont", "btnapproval", "btnmodification", "btntracing", "btnindicators", "btnreports", "btnriskmanagement", "btncrono", "btncontratacion", "btnproceed"];
//    //-Deshabilitar-//
//    for (var i = 0; i < botones.length; i++) {
//        //Recorre el array de controles
//        pendiente(botones[i]);
//    }

    pendiente("#btnlearnedlessons");
    //pendiente("#btnproceedclose");
    pendiente("#btneditcont");
    pendiente("#btnapproval");
    pendiente("#btnmodification");
    pendiente("#btnindicators");
    pendiente("#btnreports");
    pendiente("#btnriskmanagement");
    pendiente("#btncrono");
    pendiente("#btncontratacion");
    //pendiente("#btnproceed");
};

//-Verificacion inicial-//
function verifica() {
    //Obtener estado del proyecto//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getproyectstatus", "proyectid": getParameterByName('id') },
        success: function(result) {
            //Busca registros de contratación
            if (result == "1") {
                completo("#btnprojectapproval");
                habilitar("#btncontratacion");
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del proyecto.");
        }
    });

    //Obtener estado de contratacion//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getcontractfinished", "proyectid": getParameterByName('id') },
        success: function(result) {
            //Busca registros de contratación
            if (result == "True") {
                completo("#btncontratacion");
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del contrato.");
        }
    });

    //Obtener acta de inicio//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getproceeding", "proyectid": getParameterByName('id'), "type": "1" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                completo("#btnproceed");
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del acta de inicio.");
        }
    });

    //Obtener acta de seguimiento//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getproceeding", "proyectid": getParameterByName('id'), "type": "2" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                completo("#btntracing");
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del acta de seguimiento.");
        }
    });

    //Obtener acta de cierre//
    $.ajax({
        url: "ajaxCPMain.aspx",
        type: "GET",
        data: { "action": "getproceeding", "proyectid": getParameterByName('id'), "type": "3" },
        success: function(result) {
            //Busca registros de contratación
            if (result != "") {
                completo("#btnproceedclose");
            }
        },
        error: function()
        //Error
        {
            alert("Hubo un error al consultar los datos del acta de cierre.");
        }
    });
    
};


//-Formatos botones-//
function habilitar(control) {
    $(control).css("cursor", "pointer");
    $(control).css("opacity", "1");
};

function completo(control) {
    $(control).css("background", "#056F67");
    $(control).css("border", "1px solid #056F67");
    $(control).css("opacity", "1");
};

function pendiente(control) {
    $(control).css("background", "#FCBB31");
    $(control).css("border", "1px solid #FCBB31");
    $(control).css("opacity", "1");
};

function desactivar(control) {
    $(control).css("opacity", "0.5");
    $(control).css("cursor", "not-allowed");
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

function Modification_onclick()
{
    window.location = '/FormulationAndAdoption/request.aspx?idproject=' + getParameterByName('id');
}