//-Funciones del panel de control-//
function ViewProject_onclick() {
    window.location.href = '/FormulationAndAdoption/addProject.aspx?op=edit&id=' + getParameterByName('id');
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

//-Extraer parametros QueryString-//
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}