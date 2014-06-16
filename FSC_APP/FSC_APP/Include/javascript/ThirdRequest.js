//TODO: Juan Camilo Martinez Morales
//16/05/2014
//Document: ThirdRequest.js
//Load data select thirds 
var arrayActor = [];

function loadThird() {
    $.ajax({
        url: "../ResearchAndDevelopment/AjaxAddIdea.aspx",
        type: "GET",
        data: {
            "action": "C_Actors"
        },
        success: function (result) {
            $("#ddlactors").html(result);
            $("#listThirds").html(result);
            $("#ddlactors").trigger("liszt:updated");
        },
        error: function (msg) {
            alert("No se pueden cargar los actores.");
        }
    });
}

//Get data information for thirds
function SearchThirdInformation() {
    $("#ddlactors").change(function () {
        $.ajax({
            url: "../ResearchAndDevelopment/AjaxAddIdea.aspx",
            type: "GET",
            data: {
                "action": "buscar",
                "id": $(this).val()
            },
            success: function (result) {
                result = JSON.parse(result);

                $("#ctl00_cphPrincipal_Txtcontact").val(result.Contact);
                $("#ctl00_cphPrincipal_Txtcedulacont").val(result.documents);
                $("#ctl00_cphPrincipal_Txttelcont").val(result.phone);
                $("#ctl00_cphPrincipal_Txtemail").val(result.Email);
                $("#ctl00_cphPrincipal_HDIDTHIRD").val(result.idthird);
                $("#ctl00_cphPrincipal_HDNAMETHIRD").val(result.name);
                $("#ctl00_cphPrincipal_lblavertenactors").text("");

            },
            error: function () {
                alert("los datos de terceros no pudieron ser cargados.");
            }
        });
    });
}

//Global function for sum values money third
function sumMoneyValuesThird(domControlOne, domControlTwo) {
    var rev = $("#" + domControlOne).val();
    rev = rev.replace(/\./gi, '');
    var value = parseInt(rev);

    if (isNaN(value)) {
        value = 0;
        $("#" + domControlOne).val(value);
    } else {
        var rev2 = $("#" + domControlTwo).val();
        rev2 = rev2.replace(/\./gi, '');

        var value2 = parseInt(rev2);

        if (isNaN(value2)) {
            value2 = 0;
        } else {
            var resultSum = 0;
            resultSum = value + value2;
            $("#txtTotalThird").val(resultSum);
            $("#txtTotalThird").trigger("focus");
            $("#txtTotalThird").trigger("change");
        }
    }

}

//Set function for sum money values
function setEventChangeSum() {

    $("#ctl00_cphPrincipal_Txtvrdiner").change(function () {
        sumMoneyValuesThird("ctl00_cphPrincipal_Txtvrdiner", "ctl00_cphPrincipal_Txtvresp");
    });

    $("#ctl00_cphPrincipal_Txtvresp").change(function () {
        sumMoneyValuesThird("ctl00_cphPrincipal_Txtvresp", "ctl00_cphPrincipal_Txtvrdiner");
    });

    $("#ctl00_cphPrincipal_Txtvresp").change(function () {
        sumMoneyValuesThird("ctl00_cphPrincipal_Txtvresp", "ctl00_cphPrincipal_Txtvrdiner");
    });

    $("#txtTotalThird").change(function () {
        $("#ctl00_cphPrincipal_Txtaportfscocomp").text($(this).val());
    });

}

//agregar ubicaciones por el metodo de tablas html
function BtnaddActors_onclick() {

    //inicializamos las variables
    var valdiner = 0;
    var valespecie = 0;
    var valtotal = 0;
    var valdinergrid = 0;
    var valespeciegrid = 0;
    var valtotalgrid = 0;
    var valdinergridfsc = 0;
    var valespeciegridfsc = 0;
    var valtotalgridfsc = 0;
    if($("input[name='ctl00$cphPrincipal$RBListflujo']:checked").val() == "1"){
        alert("Se ha detectado información el la pestaña de flujos de pagos, al eliminar el actor toda la información se perdera!");

        var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";
        htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

        //cargamos el div donde se generara la tabla actores
        $("#T_flujosContainer").html("");
        $("#T_flujosContainer").html(htmlTableflujos);

        arrayValorflujoTotal[0] = 0;

        arrayflujosdepago = [];
        arrayinputflujos = [];
        matriz_flujos = [];
        reversedesembolsos = [];

        swhich_flujos_exist = 0;

        //reconstruimos la tabla con los datos
        $("#T_flujos").dataTable({
            "bJQueryUI": true,
            "bDestroy": true
        });
    }

    add_actor_grid();
    
}

//funtion para agreagar al grid despues de validar si hay flujos de datos
function add_actor_grid() {

    if ($("#ctl00_cphPrincipal_RBListflujo :checked").val() == null) {
        $("#ctl00_cphPrincipal_Lblflujosinf").text("Escoja si o no");
    } else {
        $("#ctl00_cphPrincipal_Lblflujosinf").text("");
        //validamos si el combo actor este selecionado
        if ($("#ddlactors :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_Lblactorrep").text("debe seleccionar almenos un actor");
        } else {
            $("#ctl00_cphPrincipal_Lblactorrep").text("");

            //capturamos los valores de deseados
            var actorsVal = $("#ddlactors").val();
            var actorsName = $("#ddlactors :selected").text();
            var tipoactors = $("#ctl00_cphPrincipal_ddlType :selected").text();
            var contact = $("#ctl00_cphPrincipal_Txtcontact").val();
            var cedula = $("#ctl00_cphPrincipal_Txtcedulacont").val();
            var telefono = $("#ctl00_cphPrincipal_Txttelcont").val();
            var email = $("#ctl00_cphPrincipal_Txtemail").val();

            var totalconsulta = $("#txtTotalThird").val();


            if ($("#ctl00_cphPrincipal_Txtvrdiner").val() == "") {
                var diner = 0;
            } else {
                var diner = $("#ctl00_cphPrincipal_Txtvrdiner").val();
            }

            if ($("#ctl00_cphPrincipal_Txtvresp").val() == "") {
                var especie = 0;
            } else {
                var especie = $("#ctl00_cphPrincipal_Txtvresp").val();
            }

            if ($("#ctl00_cphPrincipal_Txtaportfscocomp").val() == "") {
                var total = totalconsulta;
            } else {
                var total = $("#ctl00_cphPrincipal_Txtaportfscocomp").val();
            }

            var requireFlowThird = $("#ctl00_cphPrincipal_RBListflujo :checked").val();


            if (requireFlowThird == 1) {
                var estado_flujo = "s";
            } else {
                var estado_flujo = "n";
            }

            //creamos json para guardarlos en un array
            var jsonActor = {
                "IdThird": parseInt(actorsVal),
                "Name": actorsName,
                "Type": tipoactors,
                "Contact": contact,
                "Documents": cedula,
                "Phone": telefono,
                "Email": email,
                "Vrmoney": diner,
                "VrSpecies": especie,
                "FSCorCounterpartContribution": total,
                "generatesflow": estado_flujo
            };

            //recorremos el array para revisar repetidos        
            var validerepetido = 0;
            for (iArray in arrayActor) {
                console.log("sd: " + actorsVal);
                if (actorsVal == arrayActor[iArray].IdThird) {
                    validerepetido = 1;
                }
            }
            //validamos si hay repetidos 
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_Lblactorrep").text("El actor ya existe en la lista.");
            } else {
                $("#ctl00_cphPrincipal_Lblactorrep").text("");

                //cargamos el array con el json
                arrayActor.push(jsonActor);

                refreshTableThird();

                if (requireFlowThird == 1) {
                    refreshTableFlow();
                }
                //reconstruimos la tabla con los datos
                $("#matriz").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });
                //limpiamos los campos para empesar el ciclo de nuevo
                $("#ctl00_cphPrincipal_Txtcontact").val("");
                $("#ctl00_cphPrincipal_Txtcedulacont").val("");
                $("#ctl00_cphPrincipal_Txttelcont").val("");
                $("#ctl00_cphPrincipal_Txtemail").val("");
                $("#ctl00_cphPrincipal_Txtvrdiner").val("");
                $("#ctl00_cphPrincipal_Txtvresp").val("");
                $("#ctl00_cphPrincipal_Txtaportfscocomp").val("");

            }
        }
    }
}

function refreshTableFlow(requireFlowThird) {
    var htmltableAflujos = "<table id='T_Actorsflujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr>";
    htmltableAflujos += "<th width='1'></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>";


    //creamos ciclo para los actores que si tienen flujo de pago
    for (itemarrayflujos in arrayActor) {
        
        if($.trim(arrayActor[itemarrayflujos].generatesflow) == "s"){
            htmltableAflujos += "<tr id='flujo" + arrayActor[itemarrayflujos].IdThird + "'><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>";
            htmltableAflujos += arrayActor[itemarrayflujos].IdThird + "</td><td>" + arrayActor[itemarrayflujos].Name + "</td><td id= 'value";
            htmltableAflujos += arrayActor[itemarrayflujos].IdThird + "' >" + parseIntNull(arrayActor[itemarrayflujos].Vrmoney) + "</td><td><input id='";
            htmltableAflujos += "txtinput" + arrayActor[itemarrayflujos].IdThird + "' onblur=\"sumar_flujos('";
            htmltableAflujos += arrayActor[itemarrayflujos].IdThird + "')\" class=\"money\" value=\"0\" onfocus=\"restar_flujos('" + arrayActor[itemarrayflujos].IdThird;
            htmltableAflujos += "')\"></input></td><td id='desenbolso" + arrayActor[itemarrayflujos].IdThird + "'>" + parseIntNull(arrayActor[itemarrayflujos].Vrmoney);
            htmltableAflujos += "</td></tr>";
        }
    }
        
    htmltableAflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td id='tflujosing'></td><td id='totalflujos'>";
    htmltableAflujos += "0</td></td id='tflujosdesen'><td></tr></tbody></table>";


    $("#T_AflujosContainer").html("");
    $("#T_AflujosContainer").html(htmltableAflujos);
    
    //reconstruimos la tabla con los datos 
    $("#T_Actorsflujos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
    
    sumAllColumnsTableFlow();
    
    $(".money").maskMoney({thousands: '.', decimal:',', precision: 0, allowZero: true});
    $(".money").trigger("change");
}

function refreshTableThird() {
    //creamos la tabla de actores
    var htmlTableActores = "<table id='T_Actors' align='center' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr>";
    htmlTableActores += "<th width='1'></th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th>";
    htmlTableActores += "<th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Total</th><th>Eliminar</th></tr></thead><tbody>";

    //creamos la tabla de flujo actores
    for (itemArray in arrayActor) {
        htmlTableActores += "<tr id='actor" + arrayActor[itemArray].IdThird + "' ><td width='1' style='color: #D3D6FF;font-size: 0.1em;'>" + arrayActor[itemArray].IdThird;
        htmlTableActores += "</td><td>" + arrayActor[itemArray].Name + "</td><td>" + arrayActor[itemArray].Type + "</td><td>" + arrayActor[itemArray].Contact + "</td><td>";
        htmlTableActores += arrayActor[itemArray].Documents + "</td><td>" + arrayActor[itemArray].Phone + "</td><td>" + arrayActor[itemArray].Email + "</td><td>";
        htmlTableActores += parseIntNull(arrayActor[itemArray].Vrmoney) + "</td><td>" + parseIntNull(arrayActor[itemArray].VrSpecies) + "</td><td>";
        htmlTableActores += parseIntNull(arrayActor[itemArray].FSCorCounterpartContribution) + "</td><td><input class='btn btn-warning' style='background-image: none;'";
        htmlTableActores += " type ='button' value= 'Eliminar' onclick=\"deleteActor('" + arrayActor[itemArray].IdThird + "')\"></input></td></tr>";
    }

    //se anexa columna para totales
    htmlTableActores += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Total</td><td></td><td></td>";
    htmlTableActores += "<td></td><td></td><td></td><td id='val1'></td><td id='val2'>0</td><td id='val3'>0</td><td></td></tr></tbody></table>";

    //cargamos el div donde se generara la tabla actores
    $("#T_ActorsContainer").html("");
    $("#T_ActorsContainer").html(htmlTableActores);

    //llama la funcion sumar en la grilla de actores
    sumAllColumnsTableThirds();

    //llamar la funcion suma de primera columna efectivo

    //reconstruimos la tabla con los datos 
    $("#T_Actors").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });
}

function loadThirdsList(){
    var htmlSelect = "<option>Seleccione...</option>";
    for(var item in arrayActor){
        htmlSelect += "<option value='" + arrayActor[item].IdThird + "'>" + arrayActor[item].Name + "</option>";
    }
    $("#listThirdsByProject").html(htmlSelect);
}

//load data thirs by project
function loadDataThirsProject() {

    $.ajax({
        url: "AjaxRequest.aspx",
        type: "POST",
        data: {
            "action": "loadThirdProject",
            "idProject": idproject
        },
        success: function (result) {
            result = result.replace(/\//g, "").replace(/\\/g, "")
            
            var resultJson = JSON.parse(result.toString());
            //arrayActor = JSON.parse(resultJson.toString());
            arrayActor = resultJson;
            refreshTableThird();
            refreshTableFlow();
            loadThirdsList();
        },
        error: function (msg) {
            alert("No se pueden cargar los actores en general de la idea = " + ideditar);
        }
    });
}

//borrar de la grilla html de actores
function deleteActor(str) {

    // $(objbutton).parent().parent().remove();
    if (swhich_flujos_exist == 1) {
        alert("Se ha detectado información el la pestaña de flujos de pagos, al eliminar el actor toda la información se perdera!");

        var idactor = "#actor" + str;
        $(idactor).remove();

        var idflujo = "#flujo" + str;
        $(idflujo).remove();

        var idmatriz = "#matriz" + str;
        $(idmatriz).remove();
        //recorremos el array
        for (itemArray in arrayActor) {
            //construimos la llave de validacion
            var id = arrayActor[itemArray].IdThird;
            //validamos el dato q nos trae la funcion

            if (str == id) {
                //borramos el actor deseado
                delete arrayActor[itemArray];
                //arrayActor.splice(arrayActor[itemArray].Name, 1);
            }
        }
        //recorremos el array
        for (itemArrayflujo in arrayActor) {
            //construimos la llave de validacion
            var idflujo = arrayActor[itemArrayflujo].IdThird;
            //validamos el dato q nos trae la funcion

            if (str == idflujo) {
                //borramos el actor deseado
                delete arrayActor[itemArrayflujo];
            }
        }

        $("#totalflujos").text(0);
        //recorremos la tabla de flujo de pagos
        $("#T_Actorsflujos tr").slice(0, $("#T_Actorsflujos tr").length - 1).each(function () {

            arrayinputflujos = $(this).find("td").slice(0, 1);

            if ($(arrayinputflujos[0]).html() != null) {
                var idflujo = "#txtinput" + $(arrayinputflujos[0]).html();
                $(idflujo).val("");
            }
        });

        var htmlTableflujos = "<table id='T_flujos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>No pago</th><th style='text-align: center;'>Fecha</th><th style='text-align: center;'>Porcentaje</th><th style='text-align: center;'>Entregable</th><th style='text-align: center;'>Valor parcial</th><th style='text-align: center;'>Editar/Eliminar</th><th style='text-align: center;' >Detalle</th></tr></thead><tbody>";
        htmlTableflujos += "<tr><td width='1' style='color: #D3D6FF; font-size: 0.1em;'>1000</td><td>Porcentaje acumulado</td><td id='porcentaje'>0 %</td><td>Total</td><td id='totalflujospagos'>0</td><td></td><td></td></tr></tbody></table>";

        //cargamos el div donde se generara la tabla actores
        $("#T_flujosContainer").html("");
        $("#T_flujosContainer").html(htmlTableflujos);

        arrayValorflujoTotal[0] = 0;

        arrayflujosdepago = [];
        arrayinputflujos = [];
        matriz_flujos = [];
        reversedesembolsos = [];

        swhich_flujos_exist = 0;

        //reconstruimos la tabla con los datos
        $("#T_flujos").dataTable({
            "bJQueryUI": true,
            "bDestroy": true
        });

        //lamar la funcionsumar actores
        sumAllColumnsTableThirds();

    } else {

        var idflujo = "#flujo" + str;
        $(idflujo).remove();

        var idmatriz = "#matriz" + str;
        $(idmatriz).remove();

        var idactor = "#actor" + str;
        $(idactor).remove();

        //recorremos el array
        for (itemArray in arrayActor) {
            //construimos la llave de validacion
            var id = arrayActor[itemArray].IdThird;
            //validamos el dato q nos trae la funcion

            if (str == id) {
                //borramos el actor deseado
                delete arrayActor[itemArray];
                //arrayActor.splice(arrayActor[itemArray].Name, 1);
            }
        }
        //recorremos el array
        for (itemArrayflujo in arrayActor) {
            //construimos la llave de validacion
            var idflujo = arrayActor[itemArrayflujo].IdThird;
            //validamos el dato q nos trae la funcion

            if (str == idflujo) {
                //borramos el actor deseado
                delete arrayActor[itemArrayflujo];
            }
        }
        //lamar la funcionsumar actores
        sumAllColumnsTableThirds();
        //llamar la funcion suma de grid principal

    }

}

//Add char of thousand
function addThousandChar(str) {
    var amount = new String(str);
    amount = amount.split("").reverse();

    var output = "";
    for (var i = 0; i <= amount.length - 1; i++) {
        output = amount[i] + output;
        if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
    }
    return output;

}

//funcion para la suma de valoes en el grid de actores
function sumAllColumnsTableThirds() {

    //inicializamos las variables
    var valdiner = 0;
    var valdinerflujos = 0;
    var valespecie = 0;
    var valtotal = 0;




    //recorremos la tabla actores para calcular los totales
    $("#T_Actors tr").slice(0, $("#T_Actors tr").length - 1).each(function () {
        var arrayValuesActors = $(this).find("td").slice(7, 10);
        //validamos si hay campos null en la tabla actores
        if ($(arrayValuesActors[0]).html() != null) {

            //capturamos e incrementamos los valores para la suma
            valdiner = valdiner + parseInt($(arrayValuesActors[0]).html().replace(/\./gi, ''));

            valespecie = valespecie + parseInt($(arrayValuesActors[1]).html().replace(/\./gi, ''));
            valtotal = valtotal + parseInt($(arrayValuesActors[2]).html().replace(/\./gi, ''));
            //validamos valores si vienen vacios
            if (isNaN(valdiner)) {
                valdiner = 0;
            }
            if (isNaN(valespecie)) {
                valespecie = 0;
            }
            if (isNaN(valtotal)) {
                valtotal = 0;
            }

            //cargamos los campos con la operacion realizada
            $("#val1").text(addThousandChar(valdiner));
            $("#val2").text(addThousandChar(valespecie));
            $("#val3").text(addThousandChar(valtotal));
        } else {
            $("#val1").text(0);
            $("#val2").text(0);
            $("#val3").text(0);
        }
    });
}

function parseIntNull(number) {
    if (number == null) {
        return 0;
    } else {
        return number;
    }
}


