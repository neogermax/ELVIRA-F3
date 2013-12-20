
//Javascript para modulo de creacion de proyecto por parte de MG GROUP Ltda.
//Autor: Hernán Gómez  MG Group
//Fecha Inicio: 11/06/2013

//validacion para combo box


$(document).ready(function() {

    projectoperate();
    comboactor();
    updateFieldFinalization();

    //var timer = setTimeout("fix();", 2000);

    var fscContribucion = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").val();

    fscContribucion = fscContribucion.replace(/\./gi, ',');

    $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val(fscContribucion);

    if ($("#ctl00_cphPrincipal_hdididea").val() != "") {
        reloadthirds_components($("#ctl00_cphPrincipal_hdididea").val());
    }
    //funcion que hace visible la pestaña aclaratorio si el usuario selecciona 'aclaratorio'
    //en tipo de aprobacion
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_ddltipoaprobacion").change(function() {

        if ($(this).val() == 3) {
            $("#ctl00_cphPrincipal_TabContainer1_tbpnAclaratorio").css("display", "block")
        }
    });
   
});



//function fix() {

//    if ($.trim($("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").val()).length > 0) {
//        var timer = setTimeout("fix();", 2000);
//        $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").trigger("blur");
//        clearTimeout(timer);
//    }
//}


// Funcion para actualizar campo finalizacion si se cambia el campo duracion
function updateFieldFinalization() {
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").focusout(function() {

        //Verificar que el numero cumpla con las caracteristicas
        var validacion = /(\d+)(((.|,)\d+)+)?/

        if ($("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").val() == "") {

            $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtContractDuration").focus();
        }
        else {
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").text("");
        }


        //Ejecutar el calculo de la fecha
        $.ajax({
            url: "ajaxChargeTextfieldProject.aspx",
            type: "GET",
            data: { "action": "calculafechas", "fecha": $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtbegindate").val(), "duration": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_TextFinalizacion").html(result);

                $("#ctl00_cphPrincipal_hdfechainicio").val($("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtbegindate").val());
                $("#ctl00_cphPrincipal_hdfechafinalizacion").val(result);


            },
            error: function() {
                $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").val("");
            }
        });
    })


}



function projectoperate() {
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_ddlididea").change(function() {
        confirmar = confirm("¿Traer datos de la Idea aprobada?", "SI", "NO");
        if (confirmar) {
            $.ajax({
                url: "ajaxChargeTextfieldProject.aspx",
                type: "GET",

                data: { "action": "getIdeaProject", "id": $(this).val() },

                remove_linebreaks: true,
                success: function(result) {

                    result = result.replace(/\n/g, "");
                    result = result.replace(/\r/g, "");
                    result = result.replace(/\t/g, "");
                    result = result.replace(/\n\r/g, " ");
                    result = result.replace(/\r\n/g, " ");


                    result = JSON.parse(result);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtlinestrat").html(result.Name);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtprograma").html(result.programa);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtobjective").val(result.Objective);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtjustification").val(result.Justification);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtzonedescription").val(result.AreaDescription);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtresults").val(result.Results);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_TextResultGestConocimiento").val(result.ResultsKnowledgeManagement);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_TextResCapacidInstal").val(result.ResultsInstalledCapacity);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtbegindate").val(result.StartDate);
                    var elem = result.StartDate.split('/');
                    var anioinicio = elem[2]
                    var i = anioinicio;

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").val(result.Duration);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_TextFinalizacion").html(result.finalizacion);

                    $("#ctl00_cphPrincipal_hdfechainicio").val(result.StartDate);

                    $("#ctl00_cphPrincipal_hdfechafinalizacion").val(result.finalizacion);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_ddlpopulation").val(result.Population);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").val(result.total);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").val(result.totalNoFsc);
                    //$("#ctl00_cphPrincipal_HiddenFieldFsc").val(result.total);


                    var amount = new String(result.total);
                    var amountInt = parseInt(amount);
                    amount = amount.split("").reverse();
                    var output = "";
                    for (var j = 0; j <= amount.length - 1; j++) {
                        output = amount[j] + output;
                        if ((j + 1) % 3 == 0 && (amount.length - 1) !== j) output = '.' + output;
                    }

                    var amount2 = new String(result.totalNoFsc);
                    var amount2Int = parseInt(amount2);
                    amount2 = amount2.split("").reverse();
                    var output2 = "";
                    for (var j = 0; j <= amount2.length - 1; j++) {
                        output2 = amount2[j] + output2;
                        if ((j + 1) % 3 == 0 && (amount2.length - 1) !== j) output2 = '.' + output2;
                    }

                    var sumtot = amountInt + amount2Int;
                    var sumtot = sumtot.toString()
                    sumtot = sumtot.split("").reverse();
                    var output3 = ""
                    for (var j = 0; j <= sumtot.length - 1; j++) {
                        output3 = sumtot[j] + output3;
                        if ((j + 1) % 3 == 0 && (sumtot.length - 1) !== j) output3 = '.' + output3;
                    }

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").val(output);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").val(output2);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txttotalcost").html(output3);


                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val() == $("#ctl00_cphPrincipal_Hdvtotalvalue").val();

                    $("#ctl00_cphPrincipal_Hdvtotalvalue").val(output);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtenabled").val(result.enabled);

                    $("#ctl00_cphPrincipal_HiddenFieldFsc").val(output);


                },

                error: function()
                { alert("No se pueden cargar las validaciones solicitadas."); }
            });
        }
        else {
            $.ajax({
                url: "ajaxChargeTextfieldProject.aspx",
                type: "GET",

                data: { "action": "getIdeaProject", "id": $(this).val() },


                success: function(result) {
                    result = result.replace(/\n/g, "");
                    result = result.replace(/\r/g, "");
                    result = result.replace(/\t/g, "");
                    result = result.replace(/\n\r/g, " ");
                    result = result.replace(/\r\n/g, " ");
                    result = JSON.parse(result);

                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtlinestrat").html(result.Name);
                    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtprograma").html(result.programa);

                },
                error: function()
                { alert("No se pueden cargar las validaciones solicitadas."); }
            });
        }



    });
    /************************************* traer actores idea aprobada ****************/

    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_ddlididea").change(function() {
        $.ajax({
            url: "ajaxChargeTextfieldProject.aspx",
            type: "GET",
            data: { "action": "getListActors", "id": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_TabContainer1_TabPanel4_gridthird").html(result);
            },
            error: function()
            { alert("No se pueden cargar los actores de la idea solicitada."); }
        });
    });
    /************************************* traer componentes programa de idea aprobada ****************/
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_ddlididea").change(function() {

        $.ajax({
            url: "ajaxChargeTextfieldProject.aspx",
            type: "GET",
            data: { "action": "getListComponentPrograms", "id": $(this).val() },
            success: function(result) {
                $("#ctl00_cphPrincipal_TabContainer1_TabPanelCompProgramas_gridComponentesPrograma").html(result);
            },
            error: function()
            { alert("No se pueden cargar los componentes del programa de la idea solicitada."); }

        });
    });



    //validacion para la fecha que no sea mayor de la fecha actual

    $("#ctl00_cphPrincipal_txtapprovaldate").blur(function() {
        alert("segunda entrada")
        var fecha = new Date();
        var fechacampo = new Date($("#ctl00_cphPrincipal_txtapprovaldate").val());

        if (fechacampo > fecha) {
            alert("la fecha debe ser menor al dia de hoy.");
            $("#ctl00_cphPrincipal_txtapprovaldate").val(" ");
            $("#ctl00_cphPrincipal_txtapprovaldate").focus();
        }
    });
    // calcular campo valor despues de salir del foco de porcentaje

    $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").focusout(function() {

        var porc = $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").val();

        if (porc > 100) {
            alert("El porcentaje debe ser menor o igual a 100");
            return false;
        }
        porc = Math.round(porc * 10) / 10;
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").val(porc);

        var txtvalortotalflow = $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val().replace(/\,/gi, '');

        var parcial = (parseFloat(porc) * parseFloat(txtvalortotalflow)) / 100;

        parcial = numeral(parcial).format('0,0.0');

        $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalorpartial").val(parcial)
    });

    //Validar que el porcentaje no supere el 100 por ciento, no tenga comas ni tenga mas de 2 decimas
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").change(function() {
        var expresion = /(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9])?$)/

        if (!expresion.test($("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").val())) {
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_lblFlowNfo").css("color", "red");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_lblFlowNfo").text("Recibe solo un decimal, separe con un punto.");
            $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtporcentaje").val("0");
        }

    });

    //calcular el valor total en pestaña principal despues de agregar valor contrapartida
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").focusout(function() {

        var valorFsc = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").val().replace(/\./gi, '');

        counterpart = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").val().replace(/\./gi, '');
        if (counterpart == "") {
            var counterpart = 0;

        }

        var vrtotal = parseFloat(valorFsc) + parseFloat(counterpart);

        var amount = new String(vrtotal);
        amount = amount.split("").reverse();

        var output = "";
        for (var i = 0; i <= amount.length - 1; i++) {
            output = amount[i] + output;
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
        }

        var amountFSC = new String(valorFsc);
        amountFSC = amountFSC.split("").reverse();

        var outputFSC = "";
        for (var i = 0; i <= amountFSC.length - 1; i++) {
            outputFSC = amountFSC[i] + outputFSC;
            if ((i + 1) % 3 == 0 && (amountFSC.length - 1) !== i) outputFSC = '.' + outputFSC;
        }

        $("#ctl00_cphPrincipal_Hdvtotalvalue").val(output);
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txttotalcost").html(output);


    });

    //calcular el valor total en pestaña principal despues de actualizar valor fsc
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").focusout(function() {

        var valorFsc = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtfsccontribution").val().replace(/\./gi, '');

        $("#ctl00_cphPrincipal_HiddenFieldFsc").val(valorFsc);
        counterpart = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").val().replace(/\./gi, '');
        if (counterpart == "") {
            var counterpart = 0;

        }


        var vrtotal = parseFloat(valorFsc) + parseFloat(counterpart);

        var amount = new String(vrtotal);
        amount = amount.split("").reverse();

        var output = "";
        for (var i = 0; i <= amount.length - 1; i++) {
            output = amount[i] + output;
            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
        }

        $("#ctl00_cphPrincipal_Hdvtotalvalue").val(output);
        $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txttotalcost").html(output);

        $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val(output);


    });

    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtcounterpartvalue").trigger("focusout");
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_TextFinalizacion").trigger("focusout");

    //funcion para actualizar los datos de finalizacion si cambia fecha de inicio
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtbegindate").focusout(function() {

    });

    //funcion para actualizar los datos de finalizacion si cambia duracion
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").focusout(function() {
        var duracion = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtduration").val();
        var fechainicio = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_txtbegindate").val();
        var fechainicio = new Date(fechainicio);

    });
    //FUNCION PARA DAR FORMATO CON PUNTOS EN EL CAMPO VALOR_TOTAL
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").focusout(function() {

        var vrtotal = numeral($("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val()).format('0,0');

        //        var amount = vrtotal.replace(".", "");
        //        
        //        amount = amount.split(",");
        //        var entero = amount[0];
        //        var decim = amount[1];

        //        if (decim == undefined) 
        //            decim = 0;

        //        var amount = new String(entero);

        //        var amount = amount.split("").reverse();

        //        var output = "";
        //        for (var i = 0; i <= amount.length - 1; i++) {
        //            output = amount[i] + output;
        //            if ((i + 1) % 3 == 0 && (amount.length - 1) !== i) output = '.' + output;
        //        }

        //        output = output + ',' + decim;

        $("#ctl00_cphPrincipal_TabContainer1_TabPanel7_txtvalortotalflow").val(vrtotal);
    });



    //FUNCION PARA EL MONTAJE DE VENTANAS EMERGENTES CON PRETTY PHOTO 
    //10-06-2013 GERMAN RODRIGUEZ
    $("a.pretty").prettyPhoto({
        callback: function() {
        }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        modal: true,
        social_tools: false
    });

   

}


function reloadthirds_components(ididea) {
    $.ajax({
        url: "ajaxChargeTextfieldProject.aspx",
        type: "GET",
        data: { "action": "getListActors", "id": ididea },
        success: function(result) {
            $("#thirdtable").html(result);
        },
        error: function()
        { /*alert("No se pueden cargar los actores de la idea solicitada.");*/ }
    });
    $.ajax({
        url: "ajaxChargeTextfieldProject.aspx",
        type: "GET",
        data: { "action": "getListComponentPrograms", "id": ididea },
        success: function(result) {
            $("#gridComponentesPrograma").html(result);
        },
        error: function()
        { /*alert("No se pueden cargar los componentes del programa de la idea solicitada.");*/ }

    });



}
count = 0;
function AddFileInputProject() {
    var tdFileInputs = document.getElementById('tdFileInputs');
    var id = (new Date()).getTime();
    var tipo = $("#ctl00_cphPrincipal_TabContainer1_TabPanel1_hdtypeapproval").val();
    var file = document.createElement('input');
    file.setAttribute('type', 'file');
    file.setAttribute('id', id);
    file.setAttribute('name', id);
    file.setAttribute('size', '80');
    tdFileInputs.appendChild(file);
    var a = document.createElement('a');
    a.setAttribute('id', 'remove_' + id);
    a.innerHTML = "Borrar<br>";
    a.onclick = RemoveFileInput;
    tdFileInputs.appendChild(a);
    var lnkAttch = document.getElementById('lnkAttch');
    count = count + 1;

    if (count > 0) {
        lnkAttch.innerHTML = "Adjuntar otro archivo";
        //document.getElementById("btnSend").style.display = "";
    }
    else {
        //document.getElementById("btnSend").style.display = "none";
        lnkAttch.innerHTML = "Adjuntar un archivo";
    }
}

function RemoveFileInput(e) {
    var Event = e ? e : window.event;
    var obj = Event.target ? Event.target : window.event.srcElement;
    var tdFileInputs = document.getElementById('tdFileInputs');
    var a = document.getElementById(obj.id);
    tdFileInputs.removeChild(a);
    var fileInputId = obj.id.replace('remove_', '');
    var fileInput = document.getElementById(fileInputId);
    tdFileInputs.removeChild(fileInput);
    var lnkAttch = document.getElementById('lnkAttch');
    count = count - 1;
    if (count > 0) {
        lnkAttch.innerHTML = "Adjuntar otro archivo";
        //document.getElementById("btnSend").style.display = "";
    }
    else {
        lnkAttch.innerHTML = "Adjuntar un archivo";
        //document.getElementById("btnSend").style.display = "none";
    }
}

// funcion para capturar los datos del combo de actores en proyecto
//German Rodriguez MGgroup  30-10-2013

function comboactor() {
    $("#ctl00_cphPrincipal_TabContainer1_TabPanel4_ddlidoperator").change(function() {

        $("#ctl00_cphPrincipal_HDIDTHIRD").val($("#ctl00_cphPrincipal_TabContainer1_TabPanel4_ddlidoperator").val());
        //alert($("#ctl00_cphPrincipal_HDIDTHIRD").val());
        $("#ctl00_cphPrincipal_HDNAMETHIRD").val($("#ctl00_cphPrincipal_TabContainer1_TabPanel4_ddlidoperator option:selected").html());
       //alert($("#ctl00_cphPrincipal_HDNAMETHIRD").val());

    });
}