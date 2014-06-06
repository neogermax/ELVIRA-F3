$(document).ready(function() {
    ObtenerLevel2();
});

function ObtenerLevel2() {
    //Traer el grupo del usuario para la consulta
    var grupouser = $("#HFFrm").value;

    $.ajax({
        url: "http://" + host + "/NewMenu/ajaxMenu.aspx",
        type: "GET",
        data: { "action": "loadmenu", "level": "2" },
        success: function(result) {
            arrayLevel2 = result.split(",");
        }
    });

    $.ajax({
        url: "http://" + host + "/NewMenu/ajaxMenu.aspx",
        type: "GET",
        data: { "action": "loadmenu", "level": "3" },
        success: function(result) {
            arrayLevel3 = result.split(",");
        }
    });

    $.ajax({
        url: "http://" + host + "/NewMenu/ajaxMenu.aspx",
        type: "GET",
        data: { "action": "loadmenu", "level": "4" },
        success: function(result) {
            arrayLevel4 = result.split(",");
        }
    });


}
