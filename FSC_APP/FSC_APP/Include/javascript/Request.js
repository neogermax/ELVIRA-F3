// Creator: Juan Camilo Martinez Morales
// Date: 16/05/2014
// File: Rquest.js

// idproject depend from page father and save into variable named idproject

var numberRequest;
var swhich_flujos_exist;
//ready event for elements DOM into page
$(document).ready(function(){
    numberRequest = (((idproject * 365) + 638) * 715953);
    //Ajax transaction for get project information
    $.ajax({
        url: "../FormulationAndAdoption/ajaxRequest.aspx",
        type: "POST",
        data: {"idProject": idproject, "action": "getInformationProject"},
        success: function(result){
            result = JSON.parse(result);
            console.log(result);
            
            $("#project-title").html("SOLICITUD DE PROYECTO: " +  result.Name.toUpperCase());
            $("#information-contract").html(result.Code.toUpperCase());
            $("#numberRequest").html(numberRequest);
            $("#dateRequest").html("<strong>Fecha de solicitud: </strong>" + Now());
            var BeginDate =  new Date(parseFloat(result.BeginDate.replace(/Date/g,"").replace(/\//g,"").replace(/["'()]/g,"").toString()));
            var EndDate =  new Date(parseFloat(result.completiondate.replace(/Date/g,"").replace(/\//g,"").replace(/["'()]/g,"").toString()));
            $("#startDate").html(BeginDate.localeFormat("dd/MM/yyyy"));
            $("#closeDate").html(EndDate.localeFormat("dd/MM/yyyy"));
        },
        error: function(){
            alert("Opsss! Algo salio mal, por favor intentelo mas tarde.")
        }
    });
    //End ---- Ajax transaction for get project information
});

//Function for get now date
function Now(){
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    var yyyy = today.getFullYear();

    if(dd<10) {
        dd='0'+dd
    } 

    if(mm<10) {
        mm='0'+mm
    } 

    return today = dd+'/'+mm+'/'+yyyy;
}