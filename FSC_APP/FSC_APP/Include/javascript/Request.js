// Creator: Juan Camilo Martinez Morales
// Date: 16/05/2014
// File: Rquest.js

$(document).ready(function() {
    HideAreas();
    Fechas();
})

// idproject depend from page father and save into variable named idproject

var numberRequest;
var swhich_flujos_exist;
var projectObject = {};
//ready event for elements DOM into page
$(document).ready(function() {
    numberRequest = (((idproject * 365) + 638) * 715953);
    //Ajax transaction for get project information
    $.ajax({
        url: "../FormulationAndAdoption/ajaxRequest.aspx",
        type: "POST",
        data: { "idProject": idproject, "action": "getInformationProject" },
        success: function(result) {
            result = JSON.parse(result);

            projectObject = result;
            
            $("#project-title").html("SOLICITUD DE PROYECTO: " +  result.Name.toUpperCase());
            $("#information-contract").html(result.Code.toUpperCase());
            $("#numberRequest").html(numberRequest);
            $("#dateRequest").html("<strong>Fecha de solicitud: </strong>" + Now());
            var BeginDate = new Date(parseFloat(result.BeginDate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            var EndDate = new Date(parseFloat(result.completiondate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            $("#startDate").html(BeginDate.localeFormat("dd/MM/yyyy"));
            $("#closeDate").html(EndDate.localeFormat("dd/MM/yyyy"));

        },
        error: function() {
            alert("Opsss! Algo salio mal, por favor intentelo mas tarde.")
        }
    });
    //End ---- Ajax transaction for get project information
    
    $("#buttonSaveRequest").click(function(){
        projectObject.Justification = $("#txtJustification").val();
        $.ajax({
            url: "../FormulationAndAdoption/ajaxRequest.aspx",
            type: "POST",
            data: {"idProject": idproject, "action": "saveInformationRerquest", "projectInformation": JSON.stringify(projectObject), "thirdsInformation": JSON.stringify(arrayActor), "flowsInformation": JSON.stringify(arrayflujosdepago), "detailsInformation": JSON.stringify(matriz_flujos)},
            success: function(result){
                console.log(result);
            },
            error: function(){
                alert("Opsss! Algo salio mal, por favor intentelo mas tarde.")
            }
        });
    });
});

//Function for get now date
function Now() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    return today = dd + '/' + mm + '/' + yyyy;
}

function Fechas() {

    $("#ctl00_cphPrincipal_txtEndSuspend").change(function() {
        var fini = $("#ctl00_cphPrincipal_txtStartSuspend").val();
        var ffin = $("#ctl00_cphPrincipal_txtEndSuspend").val();
        if (fini > ffin) {
            $("#ctl00_cphPrincipal_lblInfoSuspend").css("color", "red");
            $("#ctl00_cphPrincipal_lblInfoSuspend").text("La fecha de fin no puede ser menor a la fecha de inicio.");
            $("#ctl00_cphPrincipal_txtEndSuspend").text("");
        } else {
            $("#ctl00_cphPrincipal_lblInfoSuspend").text("");
        }
    });

}

function HideAreas() {
    var elem;
    var controltmp;
    var controles = ["Objective", "SpecificObjectives", "InstalledCapacityResults", "BenefitiaryResults", "KnowledgeResults", "OtherResults", "PartObligations"];

    for (elem in controles) {
        //Label
        controltmp = '#lbl' + controles[elem] + '2'
        ocultar(controltmp);
        //Textarea
        controltmp = '#txtar' + controles[elem] + '2'
        ocultar(controltmp);
    }
}



function mostrar(control) {
    var controltmp = control;
    controltmp = '#' + controltmp.replace("chk", "lbl") + '2';
    control = '#' + control;
    if ($(control).is(":checked") == true) {
        //Aparece
        $(controltmp).css("display", "block");
        controltmp = controltmp.replace("lbl", "txtar");
        var controlorig = control.replace("chk", "txtar");
        $(controltmp).css("display", "block");
        var texto = $(controlorig).val();
        //Pasar el texto de la izquierda
        controltmp = controltmp.replace("#", "");
        document.getElementById(controltmp).value = texto;
    } else {
        //Desaparece
        $(controltmp).css("display", "none");
        controltmp = controltmp.replace("lbl", "txtar");
        $(controltmp).css("display", "none");
        $(controltmp).val = ("");
    }

}

function ocultar(control) {
    $(control).css("display", "none");
};
