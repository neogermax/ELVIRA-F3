//funcion q sube el archivo al servidor
function subirArchivos() {

    //validamos si seleccionaron un archivo
    if ($("#fileupload").val() != "") {

        //Añadimos la imagen de carga en el contenedor
        $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "block");

        $("#ctl00_cphPrincipal_LblHELPARCHIVE").text("");
        //capturamos los datos del input file
        var file = $("#fileupload");
        var dataFile = $("#fileupload")[0].files[0];

        //inicializamos el fordata para transferencia de archivos
        var data = new FormData();
        //asinamos el datafile a la variable archivo 
        data.append('archivo', dataFile);

        // data.ajaxStart(inicioEnvio);
        //transacion ajax
        $.ajax({
            url: "AjaxAddIdea.aspx",
            type: "POST",
            contentType: false,
            data: data,
            processData: false,
            success: function(result) {

                //creamos variables
                var filename = result;
                var objectfile = data;
                var description =  cambio_text($("#ctl00_cphPrincipal_Txtdecription").val());

                if (idfile == null) {
                    idfile = 1;
                }
                else {
                    idfile = idfile + 1;
                }

                //creamos json para guardarlos en un array
                var jsonFiles = { "idfile": idfile, "filename": filename, "objectfile": objectfile, "Description": description };
                //cargamos el array con el json
                arrayFiles.push(jsonFiles);

                var htmlTablefiles = "<table id='T_files' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th style='text-align: center;'>Archivo</th><th style='text-align: center;'>Observaciones</th><th style='text-align: center;'>Eliminar</th></tr></thead><tbody>";
                //recorremos el array para generar datos del la tabla anexos
                for (itemArray in arrayFiles) {
                    htmlTablefiles += "<tr id='archivo" + arrayFiles[itemArray].idfile + "'><td><a id='linkarchives' runat='server' href='/FSC_APP/document/temp/" + arrayFiles[itemArray].filename + "' target= '_blank' title='link'>" + arrayFiles[itemArray].filename + "</a></td><td style='text-align: left;'>" + arrayFiles[itemArray].Description + "</td><td style='text-align: center;'><input type ='button' value= 'Eliminar' onclick=\"deletefile('" + arrayFiles[itemArray].idfile + "')\"></input></td></tr>";
                }
                htmlTablefiles += "</tbody></table>";

                //cargamos el div donde se generara la tabla anexos
                $("#tdFileInputs").html("");
                $("#tdFileInputs").html(htmlTablefiles);

                //reconstruimos el pluging de la tabla
                $("#T_files").dataTable({
                    "bJQueryUI": true,
                    "bDestroy": true
                });

                $("#fileupload").val("");

                $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");
                $("#ctl00_cphPrincipal_Txtdecription").val("")

            },
            error: function(error) {
                alert("Ocurrió un error inesperado, por favor intente de nuevo mas tarde: " + error);
                console.log(error);
                $('#ctl00_cphPrincipal_gif_charge_Container').css("display", "none");
            }
        });
    }
    else {
        $("#ctl00_cphPrincipal_LblHELPARCHIVE").text("No ha seleccionado ningún archivo");

    }

}

//funcion  para borrar los archivos de la lista
function deletefile(stridfile) {

    var idarchivo = "#archivo" + stridfile;
    $(idarchivo).remove();

    for (itemArray in arrayFiles) {
        //construimos la llave de validacion
        var id = arrayFiles[itemArray].idfile;
        //validamos el dato q nos trae la funcion

        if (stridfile == id) {
            //borramos el actor deseado
            delete arrayFiles[itemArray];
        }
    }


}
