//Javascript para modulo de aprobacion proyecto por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 25/08/2013

//validacion para combo box
$(document).ready(function() {
    consultaprojecto();
});

function consultaprojecto() {

    $("#ctl00_cphPrincipal_ddlproyect").change(function() {
        var idcode = $(this).val();
        //alert(idcode);
        window.location = "/FormulationAndAdoption/addProject.aspx?op=edit&id= " + idcode + "&apr=1";
        
    });

}