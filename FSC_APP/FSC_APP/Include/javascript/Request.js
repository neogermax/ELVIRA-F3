// Creator: Juan Camilo Martinez Morales
// Date: 16/05/2014
// File: Rquest.js

var arrayTypeRequest=[];

$(document).ready(function() {
    HideAreas();
    Fechas();
    partialSave();

    $(".txtarAlcance").change(function() {
        var idControl = $(this).attr("id");
        switch (idControl) {
            case "txtarObjective2":
                projectObject.Objective = $(this).val();
                break;

            case "txtarSpecificObjectives2":
                projectObject.ZoneDescription = $(this).val();
                break;

            case "txtarInstalledCapacityResults2":
                projectObject.ResultsInstalledCapacity = $(this).val();
                break;

            case "txtarBenefitiaryResults2":
                projectObject.Results = $(this).val();
                break;

            case "txtarKnowledgeResults2":
                projectObject.ResultsKnowledgeManagement = $(this).val();
                break;

            case "txtarOtherResults2":
                projectObject.OtherResults = $(this).val();
                break;

            case "txtarPartObligations2":
                projectObject.obligationsoftheparties = $(this).val();
                break;
        }
    });
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

            $("#project-title").html("SOLICITUD DE PROYECTO: " + result.Name.toUpperCase());
            $("#information-contract").html(result.Code.toUpperCase());
            $("#numberRequest").html(numberRequest);
            $("#dateRequest").html("<strong>Fecha de solicitud: </strong>" + Now());
            var BeginDate = new Date(parseFloat(result.BeginDate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            var EndDate = new Date(parseFloat(result.completiondate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            $("#startDate").html(BeginDate.localeFormat("dd/MM/yyyy"));
            $("#closeDate").html(EndDate.localeFormat("dd/MM/yyyy"));

            //
            $("#txtarObjective").val(projectObject.Objective);
            $("#txtarSpecificObjectives").val(projectObject.ZoneDescription);
            $("#txtarInstalledCapacityResults").val(projectObject.ResultsInstalledCapacity);
            $("#txtarBenefitiaryResults").val(projectObject.Results);
            $("#txtarKnowledgeResults").val(projectObject.ResultsKnowledgeManagement);
            $("#txtarOtherResults").val(projectObject.OtherResults);
            $("#txtarPartObligations").val(projectObject.obligationsoftheparties);
            //
            
            if(projectObject.Justification != undefined){
                $("#txtJustification").val(projectObject.Justification);
            }
            
            if(projectObject.Other_Request != undefined){
                $("#txtarRequest").val(projectObject.Other_Request);
            }
            
            if(projectObject.StartSuspension_Date != undefined)  
            {
                var StartSuspension_Date = new Date(parseFloat(projectObject.StartSuspension_Date.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
                $("#ctl00_cphPrincipal_txtStartSuspend").val(StartSuspension_Date.localeFormat("dd/MM/yyyy"));
            }
            
            if(projectObject.EndSuspension_Date != undefined){    
                var EndSuspension_Date = new Date(parseFloat(projectObject.EndSuspension_Date.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
                $("#ctl00_cphPrincipal_txtEndSuspend").val(EndSuspension_Date.localeFormat("dd/MM/yyyy"));
            }
            
            if(projectObject.RestartType != undefined){    
                $("#ctl00_cphPrincipal_ddlRestartType").val(projectObject.RestartType);
            }
            
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
            data: { 
                "idProject": idproject, 
                "action": "saveInformationRerquest", 
                "projectInformation": JSON.stringify(projectObject), 
                "thirdsInformation": JSON.stringify(arrayActor), 
                "flowsInformation": JSON.stringify(arrayflujosdepago), 
                "detailsInformation": JSON.stringify(matriz_flujos), 
                "other_request": $("#txtarRequest").val(), 
                "InformationTypeRequest": JSON.stringify(arrayTypeRequest), 
                "StartSuspensionDate": $("#ctl00_cphPrincipal_txtStartSuspend").val(), 
                "EndSuspensionDate": $("#ctl00_cphPrincipal_txtEndSuspend").val() , 
                "RestartType": $("#ctl00_cphPrincipal_ddlRestartType").val(), 
                "OldThird": $("#listThirdsByProject").val(), 
                "NewThird": $("#listThirds").val()
            },
            success: function(result) {
                alert("La solicitud se almaceno correctamente!");
            },
            error: function() {
                alert("Opsss! Algo salio mal, por favor intentelo mas tarde.")
            }
        });
    });
    
    getRequestTypeForProject();
    
});

//Get Request Type For Project
function getRequestTypeForProject() {
    $.ajax({
        url: "AjaxRequest.aspx",
        type: "POST",
        data: {
            "action": "getRequestTypeForProject",
            "idProject": idproject
        },
        success: function (result) {
            result = result.replace(/\//g, "").replace(/\\/g, "");
            var resultJson = JSON.parse(result);
            //arrayflujosdepago = JSON.parse(resultJson.toString());
            arrayTypeRequest = resultJson;
            
        },
        error: function (msg) {
            //
        }
    });
}

//Partial Sve
function partialSave() {
    $("#buttonSavePartial").click(function() {
        var JSONTypeRequest = { "IdRequest": 0, "IdRequestSubType": $("input[name='subgroup']:checked").val(), "IdRequestType": $("#typeRequest").val() };
        //Se agrega categoria de solicitud como objeto en el arreglo
        
        var existCategory = false;
        for(var item in arrayTypeRequest){
            if(arrayTypeRequest[item].IdRequestType == $("#typeRequest").val())
            {
                existCategory = true;
            }
        }
        
        if(!existCategory){
            arrayTypeRequest.push(JSONTypeRequest);
        }
        
        alert("Se almacenó la información de la solicitud en estado parcial, no olvide guardar antes de salir o cerrar esta ventana.");
    });
}

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

var controles = ["Objective", "SpecificObjectives", "InstalledCapacityResults", "BenefitiaryResults", "KnowledgeResults", "OtherResults", "PartObligations"];

function HideAreas() {
    var elem;
    var controltmp;
    

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
