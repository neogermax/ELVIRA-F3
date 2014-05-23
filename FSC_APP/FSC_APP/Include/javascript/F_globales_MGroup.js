
//funcion q valida los campos de fecha segun 
function validar_campofecha(strsfecha, strhelp) {

    $("#" + strsfecha).change(function() {

        var fecha = $("#" + strsfecha).val();

        if (/[0-9]{4}[\/][0-9]{2}[\/][0-9]{2}/i.test(fecha)) {

            $("#" + strhelp).text("");
        }
        else {
            $("#" + strsfecha).val("");
            $("#" + strsfecha).focus();
            $("#" + strhelp).text("este campo no cumple con el formato de fecha designada yyyy/mm/dd ");
        }

    });

}


//fucion para añadir los miles a los numeros refactorizada
function addCommasrefactor(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    return output;

}


//funcion q genera ventana de avance de carga

function carga_eventos(str_objeto) {

    $("#" + str_objeto).css("display", "none");

    $(document).ajaxStart(function() {
        $(this).show($("#" + str_objeto).css("display", "block"));
    }).ajaxStop(function() {
        $(this).hide($("#" + str_objeto).css("display", "none"));
    });

}