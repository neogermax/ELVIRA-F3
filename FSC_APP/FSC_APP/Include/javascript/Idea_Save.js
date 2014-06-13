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

    //recorer array para el ingreso de actores
    for (item in arrayActor) {
        listactores.push(JSON.stringify(arrayActor[item]));
    }

    //recorer array para el ingreso de flujos
    for (item in arrayflujosdepago) {
        listflujos.push(JSON.stringify(arrayflujosdepago[item]));
        console.log(arrayflujosdepago[item]);

    }
    //validar si el array tiene datos   
    if (listflujos.length == 0) {
        listflujos[0] = "vacio_ojo";
    }

    for (item in matriz_flujos) {
        listdetallesflujos.push(JSON.stringify(matriz_flujos[item]));
    }

    //validar si el array tiene datos
    if (listdetallesflujos.length == 0) {
        listdetallesflujos[0] = "vacio_ojo";
    }

    //recorrer el array para el ingreso archivos 
    for (item in arrayFiles) {
        listfiles.push(JSON.stringify(arrayFiles[item]));
    }

    //validar si tiene datos
    if (listfiles.length == 0) {
        listfiles[0] = "vacio_ojo";
    }

}