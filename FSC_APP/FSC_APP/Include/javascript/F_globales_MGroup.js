
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

    //DIV GENERICO PARA LA VENTANA EMERGENTE CON PARAMETROS Y DISEÑO//

    //COLOCAR EN LA PARTE SUPERIOR DESPUES DEL MODULO DE  <script></script>//

    // <div id="container_wait" runat="server" visible="true" style="width: 50%; text-align: center;
    //        border: 15px solid #cecece; background: #E8E8DC; height: 200px; line-height: 50px;
    //        vertical-align: middle;  z-index: 1000;position: absolute; left: 25%; border-radius: 40px;">
    //        <img style="margin-top: 15px;" src="../images/charge_emerging.gif" width="120px" alt="images" />
    //        <asp:Label ID="Label22" runat="server" Text="Cargando información espere un momento..." Style="font-size: 14pt;
    //            color: #9bbb58;"></asp:Label>
    // </div>

    $("#" + str_objeto).css("display", "none");

    $(document).ajaxStart(function() {
        $(this).show($("#" + str_objeto).css("display", "block"));
    }).ajaxStop(function() {
        $(this).hide($("#" + str_objeto).css("display", "none"));
    });
}

//cargar combos
function charge_CatalogList(objCatalog, nameList, selector) {

    var objList = $('[id$=' + nameList + ']');

    //recorremos para llenar el combo de
    for (var n = 0; n < objCatalog.length; n++) {
        objList[0].options[n] = new Option(objCatalog[n].descripcion, objCatalog[n].ID);
    };
    //actualizamos el combo
    $("#" + nameList).trigger("liszt:updated");
    //validamos si el combo lleva seleccione y posicionamos en el
    if (selector == 1) {

        $("#" + nameList).append("<option value='-1'>Seleccione...</option>");
        $("#" + nameList + " option[value= '-1'] ").attr("selected", true);
    }

  
}
