
//Javascript para modulo de Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

//Funcion perteneciente a el evento onload del elemento body
$(document).ready(function() {
    operacionesIdea();
    comboactor();
    var timer = setTimeout("fix();", 2000);
    validafecha();
    validafecha2();
    separarvaloresFSC();
});


function fix() {

    if ($.trim($("#ctl00_cphPrincipal_Txtday").val()).length > 0) {
        var timer = setTimeout("fix();", 2000);
        $("#ctl00_cphPrincipal_Txtday").trigger("blur");
        clearTimeout(timer);
    }

    if ($.trim($("#ctl00_cphPrincipal_txtduration").val()).length > 0) {
        var timer = setTimeout("fix();", 2000);
        $("#ctl00_cphPrincipal_txtduration").trigger("blur");
        clearTimeout(timer);
    }
}

function validafecha() {
    $("#ctl00_cphPrincipal_Txtday").blur(function() {
        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            alert("El campo fecha de inicio debe estar diligenciado!");
            $("#ctl00_cphPrincipal_Txtday").val("");
            $("#ctl00_cphPrincipal_txtduration").val("");
            $("#ctl00_cphPrincipal_txtstartdate").focus();
        }
        else {
            //Ejecutar el calculo de la fecha
            $.ajax({
                url: "AjaxAddIdea.aspx",
                type: "GET",
                data: { "action": "calculafechas",
                    "fecha": $("#ctl00_cphPrincipal_txtstartdate").val(),
                    "duracion": $("#ctl00_cphPrincipal_txtduration").val(),
                    "dias": $(this).val()
                },
                success: function(result) {
                    $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                    $("#ctl00_cphPrincipal_HFEndDate").val(result);
                    $("#ctl00_cphPrincipal_HFdate").val(result);
                },
                error: function() {
                    $("#ctl00_cphPrincipal_txtduration").val("");
                    $("#ctl00_cphPrincipal_Txtday").val("");
                }
            });

        }
    })
}

function validafecha2() {
    $("#ctl00_cphPrincipal_txtduration").blur(function() {
        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            alert("El campo fecha de inicio debe estar diligenciado!");
            $("#ctl00_cphPrincipal_Txtday").val("");
            $("#ctl00_cphPrincipal_txtduration").val("");
            $("#ctl00_cphPrincipal_txtstartdate").focus();
        }
        else {

            //Ejecutar el calculo de la fecha
            $.ajax({
                url: "AjaxAddIdea.aspx",
                type: "GET",
                data: { "action": "calculafechas",
                    "fecha": $("#ctl00_cphPrincipal_txtstartdate").val(),
                    "duracion": $(this).val(),
                    "dias": $("#ctl00_cphPrincipal_Txtday").val()
                },
                success: function(result) {
                    $("#ctl00_cphPrincipal_Txtdatecierre").val(result);
                    $("#ctl00_cphPrincipal_HFEndDate").val(result);
                    $("#ctl00_cphPrincipal_HFdate").val(result);
                },
                error: function() {
                    $("#ctl00_cphPrincipal_txtduration").val("");
                    $("#ctl00_cphPrincipal_Txtday").val("");

                }
            });
        }
    })
}


function operacionesIdea() {

    //suma de campos de actores en el formulario de idea dinero + especies
    //2-08-2013 GERMAN RODRIGUEZ

    $("#ctl00_cphPrincipal_btnAddThird").click(function() {

        var rev = $("#ctl00_cphPrincipal_Txtvrdiner").val();
        rev = rev.replace(/\./gi, '');
        var vd = parseInt(rev);

        var rev2 = $("#ctl00_cphPrincipal_Txtvresp").val();
        rev2 = rev2.replace(/\./gi, '');
        var ve = parseInt(rev2);

        if (isNaN(vd)) {
            vd = 0;
            $("#ctl00_cphPrincipal_Txtvrdiner").val(vd);
        }
        else {
            if (isNaN(ve)) {
                ve = 0;
                $("#ctl00_cphPrincipal_Txtvresp").val(ve);
            }
            else {
                var suma = 0;
                suma = vd + ve;
                addCommas(suma);
            }
        }
        suma = vd + ve;
        addCommas(suma);
    });

    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvrdiner").blur(function() {
        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var val = parseInt(rev);

        if (isNaN(val)) {
            val = 0;
            $("#ctl00_cphPrincipal_Txtvrdiner").val(val);
        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvresp").val();

            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);
            if (isNaN(val2)) { val2 = 0; }
            else {
                var suma = 0;
                suma = val + val2;
                addCommas(suma);
            }
        }
    });
    //suma de campos de actores en el formulario de idea dinero + especies
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_Txtvresp").blur(function() {
        var rev = $(this).val();
        rev = rev.replace(/\./gi, '');
        var val = parseInt(rev);
        if (isNaN(val)) {
            val = 0;
            $("#ctl00_cphPrincipal_Txtvresp").val(val);
        }
        else {
            var rev2 = $("#ctl00_cphPrincipal_Txtvrdiner").val();
            rev2 = rev2.replace(/\./gi, '');
            var val2 = parseInt(rev2);
            if (isNaN(val2)) { val2 = 0; }
            else {
                var suma = 0;
                suma = val + val2;
                addCommas(suma);
            }
        }
    });

    //montaje de jquery para validar los campo meses
    //22-07-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_txtduration").blur(function() {
        var rev = $(this).val();
        var printer = rev.replace(/"."/gi, '');

        if (isNaN(printer)) {
            $(this).css("border", "2px solid red");
            alert("El campo diligenciado no puede tener valores texto solo numericos.");
            $(this).val("");
            $(this).focus();
        } else {
            $(this).css("border", "2px solid #DEDEDE");
        }
    });

    $("#ctl00_cphPrincipal_Txtday").blur(function() {
        var rev = $(this).val();
        var printer = rev.replace(/"."/gi, '');

        if (isNaN(printer)) {
            $(this).css("border", "2px solid red");
            alert("El campo diligenciado no puede tener valores texto solo numericos.");
            $(this).val("");
            $(this).focus();
        } else {
            $(this).css("border", "2px solid #DEDEDE");
        }
    });


    //montaje de jquery para LOS TRES CAMPOS DE RESULTADO
    //31-05-2013 GERMAN RODRIGUEZ
    $("#ctl00_cphPrincipal_txtresults, #ctl00_cphPrincipal_txtresulgc, #ctl00_cphPrincipal_txtresulci, #ctl00_cphPrincipal_txtstartdate ").blur(function() {
        if ($("#ctl00_cphPrincipal_txtresults").val() == '' && $("#ctl00_cphPrincipal_txtresulgc").val() == '' && $("#ctl00_cphPrincipal_txtresulci").val() == '') {
            $(this).css("border", "2px solid red");
            $("#ctl00_cphPrincipal_lblHelpresults").text("Algunos de los resultados debe ser diligenciado por favor.")
            $("#ctl00_cphPrincipal_Label10").text("Algunos de los resultados debe ser diligenciado por favor.")
            $("#ctl00_cphPrincipal_Label11").text("Algunos de los resultados debe ser diligenciado por favor.")
        } else {
            $(this).css("border", "2px solid #DEDEDE");
            $("#ctl00_cphPrincipal_lblHelpresults").text("")
            $("#ctl00_cphPrincipal_Label10").text("")
            $("#ctl00_cphPrincipal_Label11").text("")

        }
    });
    //FUNCION PARA EL MONTAJE DE VENTANAS EMERGENTES CON PRETTY PHOTO Y ACTUALIZACION DE COMBO DE TERCEROS EN IDEA 
    //10-06-2013 GERMAN RODRIGUEZ
    $("a.pretty").prettyPhoto({
        callback: function() {
            $.ajax({
                url: "ajaxaddidea_drop_list_third.aspx",
                type: "GET",
                data: { "action": "cargarthird", "id": $("#ddlactors").val() },
                success: function(result) {
                    $("#ddlactors").html(result);
                    $("#ddlactors").trigger("liszt:updated");
                },
                error: function()
                { alert("los datos de terceros no pudieron ser cargados."); }
            });
        }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        modal: true,
        social_tools: false
    });


}

function comboactor() {
    $("#ddlactors").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "buscar", "id": $(this).val() },
            success: function(result) {
                result = JSON.parse(result);

                $("#ctl00_cphPrincipal_Txtcontact").val(result.contact);
                $("#ctl00_cphPrincipal_Txtcedulacont").val(result.documents);
                $("#ctl00_cphPrincipal_Txttelcont").val(result.phone);
                $("#ctl00_cphPrincipal_Txtemail").val(result.email);
                $("#ctl00_cphPrincipal_HDIDTHIRD").val(result.idthird);
                $("#ctl00_cphPrincipal_HDNAMETHIRD").val(result.name);
                $("#ctl00_cphPrincipal_lblavertenactors").text("");

            },
            error: function()
            { alert("los datos de terceros no pudieron ser cargados."); }
        });
    });
}

//montaje de jquery para recorrer el campo par montarle comas para los miles
//22-07-2013 GERMAN RODRIGUEZ

function addCommas(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    $("#ctl00_cphPrincipal_Txtaportfscocomp").val(output);
}

function addCommas2(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    $("#ctl00_cphPrincipal_ValueCostFSC").val(output);
}

//cambio de discriminacion de aportes por FSC Y Actor
//07-01-2014 GERMAN RODRIGUEZ MGgroup

function separarvaloresFSC() {
    $("#ctl00_cphPrincipal_btnAddThird").click(function() {

        //caturamos valor de aporte efectivo
        var VMFSC = $("#ctl00_cphPrincipal_Txtvrdiner").val();
        VMFSC = VMFSC.replace(/\./gi, '');
        var VMFSCO = parseInt(VMFSC);

        //capturamos valor en especie
        var VEFSC = $("#ctl00_cphPrincipal_Txtvresp").val();
        VEFSC = VEFSC.replace(/\./gi, '');
        var VEFSCO = parseInt(VEFSC);

        //validamos si esta el valor o no tiene
        if (isNaN(VMFSCO)) {
            VMFSCO = 0;
            $("#ctl00_cphPrincipal_ValueMoneyFSC").val(VMFSCO);
        }
        else {
            //validamos si esta el valor o no tiene
            if (isNaN(VEFSCO)) {
                VEFSCO = 0;
                $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(VEFSCO);
            }
            else {
                //mostramos los valores capturados en el grid
                $("#ctl00_cphPrincipal_ValueMoneyFSC").val(VMFSCO);
                $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(VEFSCO);
                var suma = 0;
                //realizamos la operacion
                suma = VMFSCO + VEFSCO;
                addCommas2(suma);
            }
        }
    });
}
