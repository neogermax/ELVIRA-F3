
count = 0;

function AddFileInput() {
    var tdFileInputs = document.getElementById('tdFileInputs');
    var id = (new Date()).getTime();
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

