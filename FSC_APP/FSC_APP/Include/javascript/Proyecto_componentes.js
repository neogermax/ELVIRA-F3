//--------------------fuciones de crear combos -------------------------
//cargar combo de programas
function Cprogram() {

    $("#ddlStrategicLines").change(function() {
        var confirmar = true;

        if ($("#componentesseleccionados").html() != "") {

            confirmar = confirm("Usted acaba de cambiar de linea estratégica la información diligenciada se perdera! desea cambiarla?", "SI", "NO");

        }

        if (confirmar) {
            $("#seleccionarcomponente").html("");
            $("#componentesseleccionados").html("");

            loadChildrenLineStrategic($(this));
        }

        else {
            //dejamos los componentes como estaban

            $(this).val(line_strategic);
            $(this).trigger("liszt:updated");
        }
    });
}

function loadChildrenLineStrategic(obj) {

    line_strategic = $(obj).val();
    console.log(line_strategic);

    $.ajax({
    url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_program", "idlinestrategic": line_strategic },
        success: function(result) {

            $("#ddlPrograms").html(result);
            $("#ddlPrograms").trigger("liszt:updated");
            cargarcomponente();

        },
        error: function(msg) {
            alert("No se pueden cargar los programas de la linea estrategica selecionada.");
        }
    });
}

//funcion que recarga el combo de programa
function cargar_programas(contenido) {

    $("#ddlPrograms").html(contenido);
    $("#ddlPrograms").trigger("liszt:updated");

    cargarcomponente();

}

//cargar double lisbox componentes de programa
function cargarcomponente() {

    var editable;
    var id_idea;
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');

    if (sURLVariables[0] == "op=edit") {
        editable = 1;
        id_idea = ideditar;
    }
    else {
        editable = 0;
        id_idea = 0;
    }


    $("#ddlPrograms").change(function() {

        if (id_idea == 0 && idea_buscar != null) {
            id_idea = idea_buscar;
            editable = 1;
        }

        $.ajax({
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "C_component", "idprogram": $(this).val(), "estado_proceso": editable, "id": id_idea },
            success: function(result) {

                $("#seleccionarcomponente").html(result);

                //darle atributos de seleccione
                $(".seleccione").click(function() {
                    var swhich_array_component_exist = 0;

                    var validaarray = $(this).attr("id");
                    //validamos si el array esta vacio
                    if (arraycomponente.length == 0) {
                        arraycomponente.push($(this).attr("id"));
                    }
                    else {
                        //recorremos elarray si ya habiamos ingresado el componente
                        for (itemArray in arraycomponente) {
                            if (validaarray == arraycomponente[itemArray]) {
                                swhich_array_component_exist = 1;

                            }
                        }
                        if (swhich_array_component_exist == 0) {
                            arraycomponente.push($(this).attr("id"));
                        }

                    }
                });
                //Compoentes Style
                $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
                    $(this).css("background", "#9bbb58");
                    $(this).css("color", "#fff");
                });
            },
            error: function(msg) {
                alert("No se pueden cargar los componentes del programa selecionado.");
            }
        });
    });
}

// agregar componentes al <ul componentesseleccionados>
function Btnaddcomponent_onclick() {

    $("#ctl00_cphPrincipal_Lblinformationcomponent").text("");

    for (itemArray in arraycomponente) {

        var htmlcomponente = $("#" + arraycomponente[itemArray]).html();

        //crea la lista nueva
        var htmlresult = "<li id = 'select" + arraycomponente[itemArray] + "' class = 'des_seleccionar' >" + htmlcomponente + "</li>";

        var id_componente = arraycomponente[itemArray];
        id_componente = id_componente.replace("add", "");
        arraycomponente_archivar.push(id_componente);

        //se asigna la lista al ul
        $("#componentesseleccionados").append(htmlresult);
        //eliminar del ul de seleccionar
        $("#" + arraycomponente[itemArray]).remove();

    }

    arraycomponente = [];

    //darle atributos de seleccione
    $(".des_seleccionar").click(function() {

        var swhich_array_componentdesechado_exist = 0;

        var validaarray = $(this).attr("id");
        //validamos si el array esta vacio
        if (arraycomponentedesechado.length == 0) {
            arraycomponentedesechado.push($(this).attr("id"));
        }
        else {
            //recorremos elarray si ya habiamos ingresado el componente
            for (itemArray in arraycomponentedesechado) {
                if (validaarray == arraycomponentedesechado[itemArray]) {
                    swhich_array_componentdesechado_exist = 1;

                }
            }
            if (swhich_array_componentdesechado_exist == 0) {
                arraycomponentedesechado.push($(this).attr("id"));
            }

        }
    });
    //Compoentes Style
    $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
        $(this).css("background", "#9bbb58");
        $(this).css("color", "#fff");
    });
}

// agregar componentes al <ul seleccionarcomponente>
function Btndeletecomponent_onclick() {

    for (itemArray in arraycomponentedesechado) {

        var htmlcomponente = $("#" + arraycomponentedesechado[itemArray]).html();

        //crea la lista nueva
        var htmlresult = "<li id = '" + arraycomponentedesechado[itemArray].replace('select', '') + "' class = 'seleccione' >" + htmlcomponente + "</li>";
        var id_componente = arraycomponentedesechado[itemArray];

        id_componente = id_componente.replace("selectadd", "");

        for (itemArray_bor in arraycomponente_archivar) {
            if (id_componente == arraycomponente_archivar[itemArray_bor]) {
                delete arraycomponente_archivar[itemArray_bor];
            }
        }

        //se asigna la lista al ul
        $("#seleccionarcomponente").append(htmlresult);


        //eliminar del ul de seleccionado
        $("#" + arraycomponentedesechado[itemArray]).remove();

    }

    arraycomponentedesechado = [];

    //darle atributos de seleccione
    $(".seleccione").click(function() {

        var swhich_array_component_exist = 0;

        var validaarray = $(this).attr("id");
        //validamos si el array esta vacio
        if (arraycomponente.length == 0) {
            arraycomponente.push($(this).attr("id"));
        }
        else {
            //recorremos elarray si ya habiamos ingresado el componente
            for (itemArray in arraycomponente) {
                if (validaarray == arraycomponente[itemArray]) {
                    swhich_array_component_exist = 1;

                }
            }
            if (swhich_array_component_exist == 0) {
                arraycomponente.push($(this).attr("id"));
            }

        }

    });

    //Compoentes Style
    $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
        $(this).css("background", "#9bbb58");
        $(this).css("color", "#fff");
    });

}

//-----------------traer los datos--------------
//-----------------alcacenar combos-------------

//funcion que posiciona el combo en la linea estrategica de la idea seleccionada
function ClineEstrategic_edit() {
    if (componentes_editados == 1) {

        //ajax que posiciona la linea estrategica de la idea conasultada
        $.ajax({
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "View_line_strategic", "ididea": idea_buscar },
            success: function(result) {

                $("#ddlStrategicLines").val(result);
                $("#ddlStrategicLines").trigger("liszt:updated");

                edit_line_strategic = result;

                Cprogram();
                loadChildrenLineStrategic($("#ddlStrategicLines")[0]);
                view_Cprogram();
                
            },
            error: function(msg) {
                alert("No se pueden cargar la linea estrategica deseada.");
            }
        });

    }
}

//cargar los programas seleccionados de la linea seleccionada anteriormente "ClineEstrategic_edit()"
function Cprogram_edit() {

    if (componentes_editados == 1) {

        var str_edit_line_strategic = edit_line_strategic;

        $.ajax({
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "C_program", "idlinestrategic": str_edit_line_strategic },
            success: function(result) {
            cargar_programas(result);
            },
            error: function(msg) {
                alert("No se pueden cargar los programas de la linea estrategica selecionada.");
            }
        });
        var timer_program_edit = setTimeout("view_Cprogram();", 4000);
    }
    else {
        Cprogram();
    }

}

//funcion que posiciona el combo del programa de la idea seleccionada
function view_Cprogram() {

    var editable;
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');

    if (sURLVariables[0] == "op=edit") {
        idea_buscar = ideditar;
        editable = 1;
    }
    else {
        editable = 0;
    }

    //ajax que posiciona el programa de la idea conasultada
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_program", "ididea": idea_buscar, "estado_proceso": editable },
        success: function(result) {

            $("#ddlPrograms").val(result);
            $("#ddlPrograms").trigger("liszt:updated");

            cargarcomponente();
            edit_program = result;
        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });
    componentes_editados = 0;
}

//cargar double lisbox componentes de programa de la idea seleccionada
function edit_component() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_component", "idprogram": edit_program },
        success: function(result) {

            $("#seleccionarcomponente").html(result);

            //darle atributos de seleccione
            $(".seleccione").click(function() {

                var swhich_array_component_exist = 0;
                var validaarray = $(this).attr("id");
                //validamos si el array esta vacio
                if (arraycomponente.length == 0) {
                    arraycomponente.push($(this).attr("id"));
                }
                else {
                    //recorremos elarray si ya habiamos ingresado el componente
                    for (itemArray in arraycomponente) {
                        if (validaarray == arraycomponente[itemArray]) {
                            swhich_array_component_exist = 1;

                        }
                    }
                    if (swhich_array_component_exist == 0) {
                        arraycomponente.push($(this).attr("id"));
                    }
                }
            });

            //Compoentes Style
            $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
                $(this).css("background", "#9bbb58");
                $(this).css("color", "#fff");
            });
        },
        error: function(msg) {
            alert("No se pueden cargar los componentes del programa selecionado.");
        }
    });

}

//traer datos de los componentes de la idea seleccionada
function edit_component_view() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_component", "ididea": idea_buscar },
        success: function(result) {

            $("#componentesseleccionados").html(result);

            //darle atributos de seleccione
            $(".des_seleccionar").click(function() {

                var swhich_array_componentdesechado_exist = 0;

                var validaarray = $(this).attr("id");
                //validamos si el array esta vacio
                if (arraycomponentedesechado.length == 0) {
                    arraycomponentedesechado.push($(this).attr("id"));
                }
                else {
                    //recorremos elarray si ya habiamos ingresado el componente
                    for (itemArray in arraycomponentedesechado) {
                        if (validaarray == arraycomponentedesechado[itemArray]) {
                            swhich_array_componentdesechado_exist = 1;

                        }
                    }
                    if (swhich_array_componentdesechado_exist == 0) {
                        arraycomponentedesechado.push($(this).attr("id"));
                    }

                }
            });
            //Compoentes Style
            $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
                $(this).css("background", "#9bbb58");
                $(this).css("color", "#fff");
            });
        },
        error: function(msg) {
            alert("No se pueden cargar los componentes del programa selecionado.");
        }
    });

}

//------------------------------------------------------------------
//funciones para buscar contra las tablas de proyecto
//------------------------------------------------------------------


function ClineEstrategic_edit_proyect() {

    //ajax que posiciona la linea estrategica de la idea conasultada
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_line_strategic_project", "idproject": ideditar },
        success: function(result) {

            $("#ddlStrategicLines").val(result);
            $("#ddlStrategicLines").trigger("liszt:updated");

            edit_line_strategic = result;

            Cprogram();
            loadChildrenLineStrategic($("#ddlStrategicLines")[0]);
           
        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });

}



function edit_component_view_project() {

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_component_project", "idproject": ideditar },
        success: function(result) {

            $("#componentesseleccionados").html(result);

            //darle atributos de seleccione
            $(".des_seleccionar").click(function() {

                var swhich_array_componentdesechado_exist = 0;

                var validaarray = $(this).attr("id");
                //validamos si el array esta vacio
                if (arraycomponentedesechado.length == 0) {
                    arraycomponentedesechado.push($(this).attr("id"));
                }
                else {
                    //recorremos elarray si ya habiamos ingresado el componente
                    for (itemArray in arraycomponentedesechado) {
                        if (validaarray == arraycomponentedesechado[itemArray]) {
                            swhich_array_componentdesechado_exist = 1;

                        }
                    }
                    if (swhich_array_componentdesechado_exist == 0) {
                        arraycomponentedesechado.push($(this).attr("id"));
                    }

                }
            });
            //Compoentes Style
            $("#seleccionarcomponente li, #componentesseleccionados li").click(function() {
                $(this).css("background", "#9bbb58");
                $(this).css("color", "#fff");
            });
        },
        error: function(msg) {
            alert("No se pueden cargar los componentes del programa selecionado.");
        }
    });

}