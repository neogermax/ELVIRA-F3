//-Funciones del panel de control-//
function ViewProject_onclick() {
    window.location.href = '/FormulationAndAdoption/addProject.aspx?op=edit&id=' + getParameterByName('id');
};

//-Extraer parametros QueryString-//
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
