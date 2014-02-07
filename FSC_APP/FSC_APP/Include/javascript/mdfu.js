var array_archivo = [];
count = 0;

function AddFileInput(str) {


//    var tdFileInputs = document.getElementById('tdFileInputs');
//    var id = (new Date()).getTime();
//    var file = document.createElement('input');
//    file.setAttribute('type', 'file');
//    file.setAttribute('id', id);
//    file.setAttribute('name', id);
//    file.setAttribute('size', '80');
//    tdFileInputs.appendChild(file);
//    $(file).button();
//    var a = document.createElement('input');
//    a.setAttribute('id', 'remove_' + id);
//    a.setAttribute('value', 'Eliminar');
//    //a.innerHTML = "Borrar";
//    a.onclick = RemoveFileInput;
//    tdFileInputs.appendChild(a);
//    $(a).button();
//    var lnkAttch = document.getElementById('lnkAttch');


    //capturamos los valores traidos del input file
    var idcount = count;
    var nameArchivo = str.toString();
    

    //creamos json para guardarlos en un array
    var jsonarchivos = { "idcount": idcount, "nameArchivo": nameArchivo };

    array_archivo.push(jsonarchivos);

    var htmlarchivos = "<table id='T_archivos' border='1' cellpadding='1' cellspacing='1' style='width: 100%;'><thead><tr><th scope='col'>Archivo</th><th scope='col'>Visualizar</th><th scope='col'>Eliminar</th></tr></thead><tbody>";

    for (itemArray in array_archivo) {
        htmlarchivos += "<tr><td id= 'archivo" + array_archivo[itemArray].idcount + "'>" + array_archivo[itemArray].nameArchivo + "</td><td><input type ='button' value= 'Visualizar' ></input></td><td><input type ='button' value= 'Eliminar' onclick=\"deleteArchivo('" + array_archivo[itemArray].idcount + "', this)\" ></input></td></tr>";
        count = count + 1;
    }
    htmlarchivos += "</tbody></table>";

    $("#tdFileInputs").html(htmlarchivos);

    $("#T_archivos").dataTable({
        "bJQueryUI": true,
        "bDestroy": true
    });

    if (count > 0) {
        //    lnkAttch.innerHTML = "Adjuntar otro archivo";

        //document.getElementById("btnSend").style.display = "";
    }
    else {
        //document.getElementById("btnSend").style.display = "none";
        //     lnkAttch.innerHTML = "Adjuntar un archivo";

    }
}


function deleteArchivo(str, objbutton) {

    $(objbutton).parent().parent().remove();
    var idflujo = "#" + str;
    $(idflujo).remove();

    //recorremos el array
    for (itemArray in array_archivo) {
        //construimos la llave de validacion
        var id = array_archivo[itemArray].idcount;
        //validamos el dato q nos trae la funcion

        if (str == id) {
            //borramos el actor deseado
            delete array_archivo[itemArray];
            //arrayActor.splice(arrayActor[itemArray].actorsName, 1);
        }
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
        //    lnkAttch.innerHTML = "Adjuntar otro archivo";
        //document.getElementById("btnSend").style.display = "";
    }
    else {
        //     lnkAttch.innerHTML = "Adjuntar un archivo";
        //document.getElementById("btnSend").style.display = "none";
    }
}


//funcion nueva para realizar voriosarchivos adjuntos en acta de inicio
//autor:german rodriguez
function AddFileInput2() {

    var tdFileInputs = document.getElementById('tdFileInputs');
    var id = (new Date()).getTime();
    var file = document.createElement('input');
    file.setAttribute('type', 'file');
    file.setAttribute('id', id);
    file.setAttribute('name', id);
    file.setAttribute('size', '80');
    tdFileInputs.appendChild(file);

    var b = document.createElement('b');
    b.setAttribute('id', 'lbl_' + id);
    b.innerHTML = "descripcion :";
    tdFileInputs.appendChild(b);

    var text = document.createElement('input');
    text.setAttribute('name', id);
    text.setAttribute('type', 'text');
    text.setAttribute('id', 'text_' + id);
    text.setAttribute('size', '60');
    text.setAttribute('class', 'cssTextBox');
    tdFileInputs.appendChild(text);

    var a = document.createElement('a');
    a.setAttribute('id', 'remove_' + id);
    a.innerHTML = "Borrar<br>";
    a.onclick = RemoveFileInput2;
    tdFileInputs.appendChild(a);

    var lnkAttch = document.getElementById('lnkAttch2');
    count = count + 1;
    if (count > 0) {
        lnkAttch.innerHTML = "Adjuntar otro archivo";

    }
    else {
        lnkAttch.innerHTML = "Adjuntar un archivo";
    }
}

//funcion nueva para realizar voriosarchivos adjuntos en acta de inicio
//autor:german rodriguez

function RemoveFileInput2(e) {
    var Event = e ? e : window.event;
    var obj = Event.target ? Event.target : window.event.srcElement;
    var tdFileInputs = document.getElementById('tdFileInputs');
    var a = document.getElementById(obj.id);
    tdFileInputs.removeChild(a);

    var fileInputId = obj.id.replace('remove_', '');
    var fileInput = document.getElementById(fileInputId);
    tdFileInputs.removeChild(fileInput);
    console.log(e);
    console.log(this);
    //se captura el id del control que dispara
    var id = $(this).attr("id");
    id = id.replace("remove_", "");
    //se dispara el borrar con el id capturado
    $("#text_" + id).remove();
    $("#lbl_" + id).remove();
    $(this).remove();

    var lnkAttch = document.getElementById('lnkAttch2');
    count = count - 1;
    if (count > 0) {
        lnkAttch.innerHTML = "Adjuntar otro archivo";

    }
    else {
        lnkAttch.innerHTML = "Adjuntar un archivo";

    }
}

