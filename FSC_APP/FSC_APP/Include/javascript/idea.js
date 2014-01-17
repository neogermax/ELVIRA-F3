
//Javascript para modulo de Idea por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 28/05/2013

//Funcion perteneciente a el evento onload del elemento body

var arrayUbicacion = [];
var arrayActor = [];
var valI1;
var valI2;
var valI3;

$(document).ready(function() {
    operacionesIdea();
    comboactor();
    var timer = setTimeout("fix();", 2000);
    validafecha();
    validafecha2();
    separarvaloresFSC();

    ClineEstrategic();
    Cprogram();
    Cdeptos();
    Cmunip();
    Cactors();
    CtypeContract();
    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#tabsIdea").tabs();
    $("#matriz").dataTable({
        "bJQueryUI": true
    });
    $("#T_location").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
    $("#T_Actors").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });


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


//funcion de refactorizacion idea fase 3 ---- autor:German Rodriguez MGgroup
//guardar idea
function SaveIdea_onclick() {

    if ($("#ddlStrategicLines :selected").text() == 'Seleccione...' || $("#ddlPrograms :selected").text() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '' || $("#ctl00_cphPrincipal_txtduration").val() == '' || arrayUbicacion.length == 0) {

        if ($("#ddlStrategicLines :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_lblinfls").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblinfls").text("");
        }
        if ($("#ddlPrograms :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_lblinpro").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblinpro").text("");
        }

        if ($("#ctl00_cphPrincipal_txtname").val() == '') {
            $("#ctl00_cphPrincipal_lblHelpname").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpname").text("");
        }

        if ($("#ctl00_cphPrincipal_txtjustification").val() == '') {
            $("#ctl00_cphPrincipal_lblHelpjustification").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpjustification").text("");
        }

        if ($("#ctl00_cphPrincipal_txtobjective").val() == '') {
            $("#ctl00_cphPrincipal_lblHelpobjective").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpobjective").text("");
        }

        if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
            $("#ctl00_cphPrincipal_lblHelpstartdate").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
        }

        if ($("#ctl00_cphPrincipal_txtduration").val() == '') {
            $("#ctl00_cphPrincipal_lbldia").text("Campo Requerido");
        }
        else {
            $("#ctl00_cphPrincipal_lbldia").text("");
        }
        if (arrayUbicacion.length == 0) {
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("Debe almenos tener una ubicación");
        }
        else {
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("");
        }

    }

    else {
        var listubicaciones = [];

        $("#ctl00_cphPrincipal_lblinfls").val("");
        $("#ctl0_cphPrincipal_lblinpro").val("");
        $("#ctl00_cphPrincipal_lblHelpname").text("");
        $("#ctl00_cphPrincipal_lblHelpjustification").text("");
        $("#ctl00_cphPrincipal_lblHelpobjective").text("");
        $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
        $("#ctl00_cphPrincipal_lbldia").text("");

        for (item in arrayUbicacion) {
            listubicaciones.push(JSON.stringify(arrayUbicacion[item]));
        }

        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "save", "code": $("#ctl00_cphPrincipal_txtcode").val(),
                "linea_estrategica": $("#ddlStrategicLines").val(),
                "programa": $("#ddlPrograms").val(),
                "nombre": $("#ctl00_cphPrincipal_txtname").val(),
                "justificacion": $("#ctl00_cphPrincipal_txtjustification").val(),
                "objetivo": $("#ctl00_cphPrincipal_txtobjective").val(),
                "objetivo_esp": $("#ctl00_cphPrincipal_txtareadescription").val(),
                "Resultados_Benef": $("#ctl00_cphPrincipal_txtresults").val(),
                "Resultados_Ges_c": $("#ctl00_cphPrincipal_txtresulgc").val(),
                "Resultados_Cap_i": $("#ctl00_cphPrincipal_txtresulci").val(),
                "Fecha_inicio": $("#ctl00_cphPrincipal_txtstartdate").val(),
                "mes": $("#ctl00_cphPrincipal_txtduration").val(),
                "dia": $("#ctl00_cphPrincipal_Txtday").val(),
                "Fecha_fin": $("#ctl00_cphPrincipal_Txtdatecierre").val(),
                "Población": $("#ctl00_cphPrincipal_ddlPupulation").val(),
                "contratacion": $("#ddlmodcontract").val(),
                "A_Mfsc": $("#ctl00_cphPrincipal_ValueMoneyFSC").val(),
                "A_Efsc": $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(),
                "A_Mcounter": $("#ctl00_cphPrincipal_ValueMoneyCounter").val(),
                "A_Ecounter": $("#ctl00_cphPrincipal_ValueEspeciesCounter").val(),
                "cost": $("#ctl00_cphPrincipal_ValueCostotal").val(),
                "obligaciones": $("#ctl00_cphPrincipal_Txtobligationsoftheparties").val(),
                "iva": $("#ctl00_cphPrincipal_Chkiva").val(),
                "listubicaciones": listubicaciones.toString()

            },
            success: function(result) {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
            },
            error: function() {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblsaveinformation").text("Se genero error al entrar a la operacion Ajax");
            }
        });
    }
}


function Add_location_onclick() {

    var deptoVal = $("#ddlDepto").val();
    var deptoName = $("#ddlDepto :selected").text();

    var cityVal = $("#ddlCity").val();
    var cityName = $("#ddlCity :selected").text();

    var jsonUbicacion = { "DeptoVal": deptoVal, "DeptoName": deptoName, "CityVal": cityVal, "CityName": cityName };

    //    for (iArray in arrayUbicacion) {
    //        if (deptoName == arrayUbicacion[iArray].DeptoName && cityName == arrayUbicacion[iArray].CityName) {
    //            $("#ctl00_cphPrincipal_LblubicacionRep").text("La ubicación ya fue ingresada");
    //            break;
    //        }
    //    }
    //    $("#ctl00_cphPrincipal_LblubicacionRep").text("");

    arrayUbicacion.push(jsonUbicacion);
    var htmlTable = "<table id='T_location' border='2' cellpadding='2' cellspacing='2' style='width: 100%;'><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead><tbody>";


    for (itemArray in arrayUbicacion) {
        htmlTable += "<tr><td>" + arrayUbicacion[itemArray].DeptoName + "</td><td>" + arrayUbicacion[itemArray].CityName + "</td><td><button>Eliminar</button></td></tr>";
    }

    htmlTable += "</tbody></table>";

    $("#T_locationContainer").html("");
    $("#T_locationContainer").html(htmlTable);

    $("#T_location").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}

var valdinerIni = 0;
var valdiner = 0;
var valdinerope = 0;
var valespecie;
var valtotal;

function BtnaddActors_onclick() {


    var actorsVal = $("#ddlactors").val();
    var actorsName = $("#ddlactors :selected").text();
    var tipoactors = $("#ctl00_cphPrincipal_ddlType :selected").text();
    var contact = $("#ctl00_cphPrincipal_Txtcontact").val();
    var cedula = $("#ctl00_cphPrincipal_Txtcedulacont").val();
    var telefono = $("#ctl00_cphPrincipal_Txttelcont").val();
    var email = $("#ctl00_cphPrincipal_Txtemail").val();
    var diner = $("#ctl00_cphPrincipal_Txtvrdiner").val();
    var especie = $("#ctl00_cphPrincipal_Txtvresp").val();
    var total = $("#ctl00_cphPrincipal_Txtaportfscocomp").val();

    var jsonActor = { "actorsVal": actorsVal, "actorsName": actorsName, "tipoactors": tipoactors, "contact": contact, "cedula": cedula, "telefono": telefono, "email": email, "diner": diner, "especie": especie, "total": total };

    arrayActor.push(jsonActor);

    var htmlTableActores = "<table id='T_Actors' align='center' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>id</th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Especie</th><th>Eliminar</th></tr></thead>";

    for (itemArray in arrayActor) {
        htmlTableActores += "<tr><td>" + arrayActor[itemArray].actorsVal + "</td><td>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].tipoactors + "</td><td>" + arrayActor[itemArray].contact + "</td><td>" + arrayActor[itemArray].cedula + "</td><td>" + arrayActor[itemArray].telefono + "</td><td>" + arrayActor[itemArray].email + "</td><td>" + arrayActor[itemArray].diner + "</td><td>" + arrayActor[itemArray].especie + "</td><td>" + arrayActor[itemArray].total + "</td><td><button>Eliminar</button></td></tr>";
    }

    htmlTableActores += "<tr><td>1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id='val1'></td><td id='val2'>0</td><td id='val3'>0</td><td></td></tr>";

    htmlTableActores += "</tbody></table>";


    $("#T_ActorsContainer").html("");
    $("#T_ActorsContainer").html(htmlTableActores);

    $("#T_Actors tr").each(function() {

        var arrayValuesActors = $(this).find("td").slice(7, 10);

        if ($(arrayValuesActors[0]).html() != null) {

            var countactors = arrayActor.length;
            if (countactors == 1) {
                valdinerIni = parseInt($(arrayValuesActors[0]).html().replace(/\./gi, ''));
                valdiner = valdinerIni;
            }
            else {
                valdinerope = parseInt($(arrayValuesActors[0]).html().replace(/\./gi, ''));
                valdinerIni = valdinerIni + valdinerope;
                valdiner = valdinerIni;
               
            }

            //        valespecie = parseInt($(arrayValuesActors[1]).html().replace(/\./gi, ''));
            //      valtotal = parseInt($(arrayValuesActors[2]).html().replace(/\./gi, ''));

            console.log(valdiner);
            //    console.log(valespecie);
            //    console.log(valtotal);

            $("#val1").text(valdiner);
            //   $("#val2").text(addCommasrefactor(valespecie));
            //   $("#val3").text(addCommasrefactor(valtotal));

        }
            
    });

    $("#T_Actors").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}

//cargar combo de lineas estrategicas
function ClineEstrategic() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_linestrategic" },
        success: function(result) {
            $("#ddlStrategicLines").html(result);
            $("#ddlStrategicLines").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar las lineas strategicas.");
        }
    });
}

//cargar combo de programas
function Cprogram() {
    $("#ddlStrategicLines").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_program", "idlinestrategic": $(this).val() },
            success: function(result) {
                $("#ddlPrograms").html(result);
                $("#ddlPrograms").trigger("liszt:updated");
            },
            error: function(msg) {
                alert("No se pueden cargar los programas de la linea estrategica selecionada.");
            }
        });
    });
}

//cargar combo de departamentos
function Cdeptos() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_deptos" },
        success: function(result) {
            $("#ddlDepto").html(result);
            $("#ddlDepto").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los departamentos.");
        }
    });
}

//cargar combo de municipios 
function Cmunip() {
    $("#ddlDepto").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_munip", "iddepto": $(this).val() },
            success: function(result) {
                $("#ddlCity").html(result);
                $("#ddlCity").trigger("liszt:updated");
            },
            error: function(msg) {
                alert("No se pueden cargar los municipios del departamento seleccionado.");
            }
        });
    });
}

//cargar combo actores
function Cactors() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_Actors" },
        success: function(result) {
            $("#ddlactors").html(result);
            $("#ddlactors").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los actores.");
        }
    });
}

//cargar combo tipos de contratos
function CtypeContract() {
    $.ajax({
        url: "AjaxAddIdea.aspx",
        type: "GET",
        data: { "action": "C_typecontract" },
        success: function(result) {
            $("#ddlmodcontract").html(result);
            $("#ddlmodcontract").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de contrato.");
        }
    });
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
            $("#ctl00_cphPrincipal_lblHelpresults").text("Algunos de los resultados debe ser diligenciado.")
            $("#ctl00_cphPrincipal_Label10").text("Algunos de los resultados debe ser diligenciado.")
            $("#ctl00_cphPrincipal_Label11").text("Algunos de los resultados debe ser diligenciado.")
        } else {
            $(this).css("border", "2px solid #DEDEDE");
            $("#ctl00_cphPrincipal_lblHelpresults").text("")
            $("#ctl00_cphPrincipal_Label10").text("")
            $("#ctl00_cphPrincipal_Label11").text("")

        }
    });

    //  FUNCION PARA EL MONTAJE DE VENTANAS EMERGENTES CON PRETTY PHOTO Y ACTUALIZACION DE COMBO DE TERCEROS EN IDEA 
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

function addCommasrefactor(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    return output;
    //$("#ctl00_cphPrincipal_ValueCostFSC").val(output);
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
