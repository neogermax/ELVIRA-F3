// Creator: Juan Camilo Martinez Morales
// Date: 16/05/2014
// File: Rquest.js

var arrayTypeRequest=[];
var JSONThirdCesion;


// idproject depend from page father and save into variable named idproject

var numberRequest;
var swhich_flujos_exist;
var projectObject = {};
//ready event for elements DOM into page
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
    
    $("#listThirds").change(function(){
        $.ajax({
            url: "../FormulationAndAdoption/ajaxRequest.aspx",
            type: "POST",
            data: {
                "action": "getLastContactForProjectByThird",
                "idProject": idproject,
                "idThird": $(this).val()
            },
            success: function (result) {
                if(result != ""){
                    result = JSON.parse(result);
                    
                    $("#listTypeThird").val($.trim(result.Type));
                    $("#txtNameContact").val($.trim(result.Name));
                    $("#txtCCThird").val($.trim(result.Documents));
                    $("#txtPhoneThird").val($.trim(result.Phone));
                    $("#txtEmailThird").val($.trim(result.Email));
                }else{
                    $("#listTypeThird").val("");
                    $("#txtNameContact").val("");
                    $("#txtCCThird").val("");
                    $("#txtPhoneThird").val("");
                    $("#txtEmailThird").val("");
                }
                
                
            },
            error: function (msg) {
                alert("No se pueden cargar los actores.");
            }        
        });
    });

    //Ajax transaction for get project information
    $.ajax({
        url: "../FormulationAndAdoption/ajaxRequest.aspx",
        type: "POST",
        data: { "idProject": idproject, "action": "getInformationProject" },
        success: function(result) {
            result = JSON.parse(result);
    
            projectObject = result[0];

            $("#project-title").html("SOLICITUD DE PROYECTO: " + projectObject.Name.toUpperCase() + " - " + projectObject.idKey );
            $("#information-contract").html("<strong>No. CONTRATO: </strong>" + result[1]);
            
            if(projectObject.Id == 0){
                numberRequest = "N/A";
            }else{
                numberRequest = projectObject.Id;
            }
            
            $("#numberRequest").html(numberRequest);
            $("#dateRequest").html("<strong>Fecha de solicitud: </strong>" + Now());
            var BeginDate = new Date(parseFloat(projectObject.BeginDate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            var EndDate = new Date(parseFloat(projectObject.completiondate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));
            $("#startDate").html("Fecha de Inicio: " + BeginDate.localeFormat("dd/MM/yyyy") + " - ");
            $("#closeDate").html("Fecha de Liquidación: " + EndDate.localeFormat("dd/MM/yyyy"));

            //
            $("#txtarObjective").val($.trim(projectObject.Objective) == ""? "N/A": projectObject.Objective);
            $("#txtarSpecificObjectives").val($.trim(projectObject.ZoneDescription)  == ""? "N/A": projectObject.ZoneDescription);
            $("#txtarInstalledCapacityResults").val($.trim(projectObject.ResultsInstalledCapacity)  == ""? "N/A": projectObject.ResultsInstalledCapacity);
            $("#txtarBenefitiaryResults").val($.trim(projectObject.Results)   == ""? "N/A": projectObject.Results);
            $("#txtarKnowledgeResults").val($.trim(projectObject.ResultsKnowledgeManagement)  == ""? "N/A": projectObject.ResultsKnowledgeManagement);
            $("#txtarOtherResults").val($.trim(projectObject.OtherResults)  == ""? "N/A": projectObject.OtherResults);
            $("#txtarPartObligations").val($.trim(projectObject.obligationsoftheparties) == ""? "N/A": projectObject.obligationsoftheparties);
            //
            
            if(projectObject.Justification_Request != undefined){
                $("#txtJustification").val(projectObject.Justification_Request);
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
            
            if(projectObject.completiondate != undefined){
                var completiondate = new Date(parseFloat(projectObject.completiondate.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));    
                $("#txtNewDateClose").val(completiondate.localeFormat("dd/MM/yyyy"));
            }
            
            if(projectObject.Settlement_Date != undefined){    
                var Settlement_Date = new Date(parseFloat(projectObject.Settlement_Date.replace(/Date/g, "").replace(/\//g, "").replace(/["'()]/g, "").toString()));    
                $("#txtNewDateSettlement").val(Settlement_Date.localeFormat("dd/MM/yyyy"));
            }else{
                $("#txtNewDateSettlement").val(result[2]);
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
                "NewThird": $("#listThirds").val(),
                "JSONThirdCession": JSON.stringify(JSONThirdCesion),
                "SettlementDate": $("#txtNewDateSettlement").val(),
                "CompletitionDate" : $("#txtNewDateClose").val()
            },
            success: function(result) {
                alert("La solicitud se almaceno correctamente!");
                console.log(host + '/FormulationAndAdoption/CPMain.aspx?id=' + idproject);
                window.location = 'http://' + host + '/FormulationAndAdoption/CPMain.aspx?id=' + idproject;
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
            showOptionsSelect();
        },
        error: function (msg) {
            //
        }
    });
}

function showOptionsSelect(){
    
    for(var item in arrayTypeRequest){
        switch (arrayTypeRequest[item].IdRequestType){
            case 1:
            case 3:
            case 5:
            default:
                $("#typeRequest").html("<option value='1'>1. Adición, Prórroga, Entregables</option><option value='3'>3. Alcance</option><option value='5'>5. Otros</option>");
                $("#typeRequest").trigger("change");
            break;
            case 2:
                $("#typeRequest").html("<option value='2'>2. Suspensión</option>");
                $("#typeRequest").trigger("change");
            break;
            case 4:
                $("#typeRequest").html("<option value='4'>4. Cesión</option>");
                $("#typeRequest").trigger("change");
            break;
            
        }
        break;
        
    }
}

//Partial Sve
function partialSave() {
    $("#buttonSavePartial").click(function() {
        var JSONTypeRequest = { "IdRequest": 0, "IdRequestSubType": $("input[name='subgroup']:checked").val(), "IdRequestType": $("#typeRequest").val() };
        //Se agrega categoria de solicitud como objeto en el arreglo
        
        if($("#typeRequest").val() == 4){
            JSONThirdCesion = { "Type": $("#listTypeThird").val() , "Name": $("#listThirds :selected").text(), "Contact": $("#txtNameContact").val() , "Documents": $("#txtCCThird").val() ,"Phone": $("#txtPhoneThird").val() ,"Email": $("#txtEmailThird").val() }
        }
        
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
        showOptionsSelect();
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
