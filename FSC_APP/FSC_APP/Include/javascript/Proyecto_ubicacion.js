//cargar combo de departamentos
function Cdeptos() {
    $.ajax({
        url: "AjaxAddProject.aspx",
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
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "C_munip", "iddepto": $(this).val() },
            success: function(result) {
                $("#ddlCity").html(result);
                $("#ddlCity").trigger("liszt:updated");
            },
            error: function(msg) {
                alert("No ha seleccionado el departamento.");
                $("#ddlCity").html("<option>Seleccione...</opption>");
                $("#ddlCity").trigger("liszt:updated");
            }
        });
    });
}


//agregar ubicaciones por el metodo de tablas html
function Add_location_onclick() {

    $("#ctl00_cphPrincipal_Lblinfubicacion").text("");

    //validamos si el combo departamento este selecionado
    if ($("#ddlDepto :selected").text() == 'Seleccione...') {
        $("#ctl00_cphPrincipal_LblubicacionRep").text("Debe seleccionar almenos un departamento");
    }
    else {
        //validamos si el combo municipio este selecionado
        if ($("#ddlCity :selected").text() == 'Seleccione...') {
            $("#ctl00_cphPrincipal_LblubicacionRep").text("");
            $("#ctl00_cphPrincipal_LblubicacionRep").text("Debe seleccionar almenos un municipio");
        }
        else {

            $("#ctl00_cphPrincipal_LblubicacionRep").text("");

            //capturamos los valores de los combos
            var deptoVal = $("#ddlDepto").val();
            var deptoName = $("#ddlDepto :selected").text();
            var cityVal = $("#ddlCity").val();
            var cityName = $("#ddlCity :selected").text();

            //creamos json para guardarlos en un array
            var jsonUbicacion = { "DeptoVal": deptoVal, "DeptoName": deptoName, "CityVal": cityVal, "CityName": cityName };

            //recorremos el array para revisar repetidos
            var validerepetido = 0;
            for (iArray in arrayUbicacion) {
                if (deptoName == arrayUbicacion[iArray].DeptoName && cityName == arrayUbicacion[iArray].CityName) {
                    validerepetido = 1;
                }
            }

            //validamos si hay repetidos 
            if (validerepetido == 1) {
                $("#ctl00_cphPrincipal_LblubicacionRep").text("La ubicación ya fue ingresada");
            }
            else {
                $("#ctl00_cphPrincipal_LblubicacionRep").text("");

                //cargamos el array con el json
                arrayUbicacion.push(jsonUbicacion);

                Crear_tabla_ubicacion();

                $("#ddlDepto").val("Seleccione...");
                $("#ddlDepto").trigger("liszt:updated");
                $("#ddlCity").val("Seleccione...");
                $("#ddlCity").trigger("liszt:updated");
        
            }
        }
    }
}

//creamos la tabla de ubicaciones
function Crear_tabla_ubicacion() {

    var htmlTable = "<table id='T_location' border='2' cellpadding='2' cellspacing='2' style='width: 100%;'><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead><tbody>";

    for (itemArray in arrayUbicacion) {
        var strdelete = arrayUbicacion[itemArray].DeptoName + "_" + arrayUbicacion[itemArray].CityName;
        htmlTable += "<tr><td>" + arrayUbicacion[itemArray].DeptoName + "</td><td>" + arrayUbicacion[itemArray].CityName + "</td><td><input type ='button' class= 'deleteUbicacion' value= 'Eliminar'  onclick=\"deleteUbicacion('" + strdelete + "')\" ></input></td></tr>";
    }
    htmlTable += "</tbody></table>";

    //cargamos el div donde se generara la tabla
    $("#T_locationContainer").html("");
    $("#T_locationContainer").html(htmlTable);

    //agregamos atributos de eliminar fila
    $(".deleteUbicacion").click(function() {
        $(this).parent().parent().remove();
    });

    //reconstruimos la tabla con los datos 
    $("#T_location").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

}



//funcion para cargar  array ubicaciones en ediccion
function view_ubicacion_array() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_ubicacion_array", "ididea": idea_buscar },
        success: function(result) {

            array_ubicacion_ed = result.split("|");

            for (itemArray in array_ubicacion_ed) {

                var recibeubi = JSON.parse(array_ubicacion_ed[itemArray]);
                arrayUbicacion.push(recibeubi);
            }

            //llamamos la funcion q nos genera la tabla
            Crear_tabla_ubicacion();
        },
        error: function(msg) {
            alert("No se pueden cargar las ubicaciones seleccionadas de la idea = " + idea_buscar);
        }
    });
}


//borrar de la grilla html de ubicaciones 
function deleteUbicacion(str) {
    //recorremos el array
    for (itemArray in arrayUbicacion) {
        //construimos la llave de validacion
        var id = arrayUbicacion[itemArray].DeptoName + "_" + arrayUbicacion[itemArray].CityName;
        //validamos el dato q nos trae la funcion
        if (str == id) {
            //borramos la ubicacion deseada
            delete arrayUbicacion[itemArray];
            //arrayUbicacion.splice(arrayUbicacion[itemArray].CityName, 1);
        }
    }
}
