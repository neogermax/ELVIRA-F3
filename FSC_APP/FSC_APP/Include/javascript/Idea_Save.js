//construccion de arreglos para ingreso//
var listubicaciones = [];
var listactores = [];
var listflujos = [];
var listdetallesflujos = [];
var listfiles = [];


function PartialSaved() {

    ChargeOfArrangements();

}

function ChargeOfArrangements() {

    //recorer array para el ingreso de ubicaciones
    for (item in arrayUbicacion) {
        listubicaciones.push(JSON.stringify(arrayUbicacion[item]));
    }

    //validar si el array tiene datos
    if (listubicaciones.length == 0) {
        listubicaciones[0] = "vacio_ojo";
    }

   
}