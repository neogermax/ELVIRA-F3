
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
    cargarcomponente();
    addcomponent();

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
    $("#T_flujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#T_Actorsflujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    $("#SaveIdea").button();
    $("#B_add_location").button();
    $("#BtnaddActors").button();
    $("#Btn_add_flujo").button();


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

    var fsc_exist = 0;

    if (arrayActor.length == 0 || arrayUbicacion.length == 0) {
        if (arrayActor.length == 0 && arrayUbicacion.length != 0) {
            $("#ctl00_cphPrincipal_Lblactorrep").text("Debe almenos tener un actor");
            $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña actores");
        }
        if (arrayActor.length != 0 && arrayUbicacion.length == 0) {
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("debe tener almenos una ubicación");
            $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña ubicación");
        }
        if (arrayActor.length == 0 && arrayUbicacion.length == 0) {
            $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña actores y ubicaciones");
            $("#ctl00_cphPrincipal_Lblactorrep").text("Debe almenos tener un actor");
            $("#ctl00_cphPrincipal_Lblinfubicacion").text("debe tener almenos una ubicación");
        }

    }

    else {
        for (iArray in arrayActor) {
            if (4 == arrayActor[iArray].actorsVal) {
                fsc_exist = 1;
            }
        }
        $("#ctl00_cphPrincipal_Lblinfubicacion").text("");
        $("#ctl00_cphPrincipal_Lblactorrep").text("");

        if ($("#ddlStrategicLines :selected").text() == 'Seleccione...' || $("#ddlPrograms :selected").text() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '' || $("#ctl00_cphPrincipal_txtduration").val() == '' || arrayUbicacion.length == 0 || fsc_exist == 0) {

            if ($("#ddlStrategicLines :selected").text() == 'Seleccione...') {
                $("#ctl00_cphPrincipal_lblinfls").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblinfls").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ddlPrograms :selected").text() == 'Seleccione...') {
                $("#ctl00_cphPrincipal_lblinpro").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblinpro").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ctl00_cphPrincipal_txtname").val() == '') {
                $("#ctl00_cphPrincipal_lblHelpname").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpname").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ctl00_cphPrincipal_txtjustification").val() == '') {
                $("#ctl00_cphPrincipal_lblHelpjustification").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpjustification").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ctl00_cphPrincipal_txtobjective").val() == '') {
                $("#ctl00_cphPrincipal_lblHelpobjective").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpobjective").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if ($("#ctl00_cphPrincipal_txtduration").val() == '') {
                $("#ctl00_cphPrincipal_lbldia").text("Campo Requerido");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("Revisar la pestaña información");
            }
            else {
                $("#ctl00_cphPrincipal_lbldia").text("");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("");
            }
           
            if (fsc_exist == 1) {
                $("#ctl00_cphPrincipal_Lblactorrep").text("");
            }
            else {
                $("#ctl00_cphPrincipal_Lblactorrep").text("la FSC debe ser un actor obligatorio");
                $("#ctl00_cphPrincipal_Lbladvertencia").text("la FSC debe ser un actor obligatorio");
            }
        }

        else {
            var listubicaciones = [];
            var listactores = [];

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

            for (item in arrayActor) {
                listactores.push(JSON.stringify(arrayActor[item]));
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
                    "A_Mfsc": $("#ValueMoneyFSC").val(),
                    "A_Efsc": $("#ValueEspeciesFSC").val(),
                    "A_Mcounter": $("#ValueMoneyCounter").val(),
                    "A_Ecounter": $("#ValueEspeciesCounter").val(),
                    "cost": $("#ValueCostotal").val(),
                    "obligaciones": $("#ctl00_cphPrincipal_Txtobligationsoftheparties").val(),
                    "iva": $("#ctl00_cphPrincipal_Chkiva").val(),
                    "listubicaciones": listubicaciones.toString(),
                    "listactores": listactores.toString()

                },
                success: function(result) {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                    $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
                    $("#ctl00_cphPrincipal_Lbladvertencia").text("");
                    $("#ddlStrategicLines").focus();
                },
                error: function() {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                    $("#ctl00_cphPrincipal_lblsaveinformation").text("Se genero error al entrar a la operacion Ajax");
                }
            });

        }

    }

}


function Add_location_onclick() {

    $("#ctl00_cphPrincipal_Lblinfubicacion").text("");

    var deptoVal = $("#ddlDepto").val();
    var deptoName = $("#ddlDepto :selected").text();

    var cityVal = $("#ddlCity").val();
    var cityName = $("#ddlCity :selected").text();

    var jsonUbicacion = { "DeptoVal": deptoVal, "DeptoName": deptoName, "CityVal": cityVal, "CityName": cityName };
    var validerepetido = 0;

    for (iArray in arrayUbicacion) {
        if (deptoName == arrayUbicacion[iArray].DeptoName && cityName == arrayUbicacion[iArray].CityName) {
            validerepetido = 1;
        }
    }

    if (validerepetido == 1) {
        $("#ctl00_cphPrincipal_LblubicacionRep").text("La ubicación ya fue ingresada");
    }
    else {
        $("#ctl00_cphPrincipal_LblubicacionRep").text("");

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

}


function BtnaddActors_onclick() {

    var valdiner = 0;
    var valespecie = 0;
    var valtotal = 0;

    var valdinergrid = 0;
    var valespeciegrid = 0;
    var valtotalgrid = 0;

    var valdinergridfsc = 0;
    var valespeciegridfsc = 0;
    var valtotalgridfsc = 0;

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
    var validerepetido = 0;

    for (iArray in arrayActor) {
        if (actorsVal == arrayActor[iArray].actorsVal) {
            validerepetido = 1;
        }
    }

    if (validerepetido == 1) {
        $("#ctl00_cphPrincipal_Lblactorrep").text("El actor ya fue ingresado");
    }
    else {
        $("#ctl00_cphPrincipal_Lblactorrep").text("");
        arrayActor.push(jsonActor);

        var htmlTableActores = "<table id='T_Actors' align='center' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>id</th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Especie</th><th>Eliminar</th></tr></thead><tbody>";
        var htmltableAflujos = "<table id='T_Actorsflujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th>Id</th><th>Aportante</th><th>Valor en efectivo</th><th>Valor a cancelar</th></tr></thead><tbody>";


        for (itemArray in arrayActor) {
            htmlTableActores += "<tr><td>" + arrayActor[itemArray].actorsVal + "</td><td>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].tipoactors + "</td><td>" + arrayActor[itemArray].contact + "</td><td>" + arrayActor[itemArray].cedula + "</td><td>" + arrayActor[itemArray].telefono + "</td><td>" + arrayActor[itemArray].email + "</td><td>" + arrayActor[itemArray].diner + "</td><td>" + arrayActor[itemArray].especie + "</td><td>" + arrayActor[itemArray].total + "</td><td><button>Eliminar</button></td></tr>";
            htmltableAflujos += "<tr><td>" + arrayActor[itemArray].actorsVal + "</td><td>" + arrayActor[itemArray].actorsName + "</td><td>" + arrayActor[itemArray].diner + "</td><td><input id=" + arrayActor[itemArray].actorsVal + " onkeyup='format(this)' onchange='format(this)'></input></td></tr>";
        }


        htmlTableActores += "<tr><td>1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id='val1'></td><td id='val2'>0</td><td id='val3'>0</td><td></td></tr>";
        htmltableAflujos += "<tr><td>1000</td><td>Total</td><td id='tflujosing'></td><td id='totalflujos'>0</td></tr>";
        htmlTableActores += "</tbody></table>";
        htmltableAflujos += "</tbody></table>";

        $("#T_ActorsContainer").html("");
        $("#T_ActorsContainer").html(htmlTableActores);

        $("#T_AflujosContainer").html("");
        $("#T_AflujosContainer").html(htmltableAflujos);



        $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {

            var arrayValuesActors = $(this).find("td").slice(7, 10);

            if ($(arrayValuesActors[0]).html() != null) {

                valdiner = valdiner + parseInt($(arrayValuesActors[0]).html().replace(/\./gi, ''));
                valespecie = valespecie + parseInt($(arrayValuesActors[1]).html().replace(/\./gi, ''));
                valtotal = valtotal + parseInt($(arrayValuesActors[2]).html().replace(/\./gi, ''));

                $("#val1").text(addCommasrefactor(valdiner));
                $("#tflujosing").text(addCommasrefactor(valdiner));

                $("#val2").text(addCommasrefactor(valespecie));
                $("#val3").text(addCommasrefactor(valtotal));

            }

        });



        var switch_fsc = 0;
        //buscar la fsc para guardar en el grid principal
        $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {
            var arrayValuesActors2 = $(this).find("td").slice(0, 10);
            if ($(arrayValuesActors2[0]).html() != null) {
                if ($(arrayValuesActors2[0]).html() == 4) {

                    valdinergridfsc = parseInt($(arrayValuesActors2[7]).html().replace(/\./gi, ''));
                    valespeciegridfsc = parseInt($(arrayValuesActors2[8]).html().replace(/\./gi, ''));
                    valtotalgridfsc = parseInt($(arrayValuesActors2[9]).html().replace(/\./gi, ''));

                    $("#ValueMoneyFSC").text(addCommasrefactor(valdinergridfsc));
                    $("#ValueEspeciesFSC").text(addCommasrefactor(valespeciegridfsc));
                    $("#ValueCostFSC").text(addCommasrefactor(valtotalgridfsc));
                }
            }
        });



        //buscar todos los q son actores diferentes a la fsc y sumarlos
        $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function() {
            var arrayValuesActors2 = $(this).find("td").slice(0, 10);
            if ($(arrayValuesActors2[0]).html() != null) {
                if ($(arrayValuesActors2[0]).html() != 4) {

                    valdinergrid = valdinergrid + parseInt($(arrayValuesActors2[7]).html().replace(/\./gi, ''));
                    valespeciegrid = valespeciegrid + parseInt($(arrayValuesActors2[8]).html().replace(/\./gi, ''));
                    valtotalgrid = valtotalgrid + parseInt($(arrayValuesActors2[9]).html().replace(/\./gi, ''));

                    $("#ValueMoneyCounter").text(addCommasrefactor(valdinergrid));
                    $("#ValueEspeciesCounter").text(addCommasrefactor(valespeciegrid));
                    $("#ValueCostCounter").text(addCommasrefactor(valtotalgrid));
                }
            }
        });

        //suma de primera columna
        var dinerfsc = $("#ValueMoneyFSC").text();
        dinerfsc = dinerfsc.replace(/\./gi, '');
        var opedinerfsc = parseInt(dinerfsc);

        var dinerothers = $("#ValueMoneyCounter").text();
        dinerothers = dinerothers.replace(/\./gi, '');
        var opedinerothers = parseInt(dinerothers);

        if (isNaN(opedinerfsc)) {
            opedinerfsc = 0;
            $("#ValueMoneyFSC").val(opedinerfsc);
        }
        else {
            if (isNaN(opedinerothers)) {
                opedinerothers = 0;
                $("#ValueMoneyCounter").val(opedinerothers);
            }
            else {
                var sumadiner = 0;
                sumadiner = opedinerfsc + opedinerothers;
                $("#valueMoneytotal").text(addCommasrefactor(sumadiner));
                $("#ctl00_cphPrincipal_txtvalortotalflow").val(addCommasrefactor(sumadiner));
            }
        }
        sumadiner = opedinerfsc + opedinerothers;
        $("#valueMoneytotal").text(addCommasrefactor(sumadiner));
        $("#ctl00_cphPrincipal_txtvalortotalflow").val(addCommasrefactor(sumadiner));

        //suma segunda columna
        var especiefsc = $("#ValueEspeciesFSC").text();
        especiefsc = especiefsc.replace(/\./gi, '');
        var opeespeciefsc = parseInt(especiefsc);

        var especieothers = $("#ValueEspeciesCounter").text();
        especieothers = especieothers.replace(/\./gi, '');
        var opeespecieothers = parseInt(especieothers);

        if (isNaN(opeespeciefsc)) {
            opeespeciefsc = 0;
            $("#ValueEspeciesFSC").val(opeespeciefsc);
        }
        else {
            if (isNaN(opeespecieothers)) {
                opeespecieothers = 0;
                $("#ValueEspeciesCounter").val(opeespecieothers);
            }
            else {
                var sumaespecie = 0;
                sumaespecie = opeespeciefsc + opeespecieothers;
                $("#ValueEspeciestotal").text(addCommasrefactor(sumaespecie));
            }
        }
        sumaespecie = opeespeciefsc + opeespecieothers;
        $("#ValueEspeciestotal").text(addCommasrefactor(sumaespecie));

        //suma tercera columna
        var totalfsc = $("#ValueCostFSC").text();
        totalfsc = totalfsc.replace(/\./gi, '');
        var opetotalfsc = parseInt(totalfsc);

        var totalothers = $("#ValueCostCounter").text();
        totalothers = totalothers.replace(/\./gi, '');
        var opetotalothers = parseInt(totalothers);

        if (isNaN(opetotalfsc)) {
            opetotalfsc = 0;
            $("#ValueCostFSC").val(opetotalfsc);
        }
        else {
            if (isNaN(opetotalothers)) {
                opetotalothers = 0;
                $("#ValueCostCounter").val(opetotalothers);
            }
            else {
                var sumatotal = 0;
                sumatotal = opetotalfsc + opetotalothers;
                $("#ValueCostotal").text(addCommasrefactor(sumatotal));
            }
        }
        sumatotal = opetotalfsc + opetotalothers;
        $("#ValueCostotal").text(addCommasrefactor(sumatotal));

        $("#T_Actors").dataTable({
            "bJQueryUI": true,
            "bDestroy": true
        });

        $("#T_Actorsflujos").dataTable({
            "bJQueryUI": true,
            "bDestroy": true
        });

    }

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


//cargar double lisbox componentes de programa
function cargarcomponente() {
    $("#ddlPrograms").change(function() {
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "GET",
            data: { "action": "C_component", "idprogram": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_dlbActivity_ctl08").html(result);
            },
            error: function(msg) {
                alert("No se pueden cargar los componentes del programa selecionado.");
            }
        });
    });
}

//pasar de un list box al otro
function addcomponent() {
    $("#ctl00_cphPrincipal_dlbActivity_ctl10").click(function() {
        if ($("#ctl00_cphPrincipal_dlbActivity_ctl08").val() == "") {

        }
        else {
            var valcomp = $("#ctl00_cphPrincipal_dlbActivity_ctl08").val()
            var textcomp = $("#ctl00_cphPrincipal_dlbActivity_ctl08 :selected").text()
            $("#ctl00_cphPrincipal_dlbActivity_ctl14").val(valcomp)
            $("#ctl00_cphPrincipal_dlbActivity_ctl14").text(textcomp)
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
                data: { "action": "cargarthird" },
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
