
//funcion que posiciona el combo en la linea estrategica de la idea seleccionada
function ClineEstrategic_edit() {

    //ajax que posiciona la linea estrategica de la idea conasultada
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_line_strategic", "ididea": idea_buscar },
        success: function(result) {

            $("#ddlStrategicLines").val(result);
            $("#ddlStrategicLines").trigger("liszt:updated");

            edit_line_strategic = result;
        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });

    var timer_cline_edit = setTimeout("Cprogram_edit();", 3500);


}

//cargar los programas seleccionados de la linea seleccionada anteriormente "ClineEstrategic_edit()"
function Cprogram_edit() {

    var str_edit_line_strategic = edit_line_strategic;

    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_program", "idlinestrategic": str_edit_line_strategic },
        success: function(result) {
            $("#ddlPrograms").html(result);
            $("#ddlPrograms").trigger("liszt:updated");
        },
        error: function(msg) {
            alert("No se pueden cargar los programas de la linea estrategica selecionada.");
        }
    });
    var timer_program_edit = setTimeout("view_Cprogram();", 4000);

}

//funcion que posiciona el combo del programa de la idea seleccionada 
function view_Cprogram() {
    //ajax que posiciona el programa de la idea conasultada
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "View_program", "ididea": idea_buscar },
        success: function(result) {

            $("#ddlPrograms").val(result);
            $("#ddlPrograms").trigger("liszt:updated");

        },
        error: function(msg) {
            alert("No se pueden cargar la linea estrategica deseada.");
        }
    });
   
}


//cargar combo de lineas estrategicas
function ClineEstrategic() {
    $.ajax({
        url: "AjaxAddProject.aspx",
        type: "GET",
        data: { "action": "C_linestrategic" },
        success: function(result) {

            $("#ddlStrategicLines").html(result);
            $("#ddlStrategicLines").trigger("liszt:updated");

        },
        error: function(msg) {
            alert("No se pueden cargar las lineas stratégicas.");
        }
    });
}

//cargar combo de programas
function Cprogram() {
    $("#ddlStrategicLines").change(function() {
        $.ajax({
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "C_program", "idlinestrategic": $(this).val() },
            success: function(result) {

                var textoLista = $("#componentesseleccionados").html();

                if (textoLista == "") {
                  
                    if (contar_program == 0) {
                        arraycompo[0] = $("#ddlStrategicLines").val();
                    }
                  
                    $("#ddlPrograms").html(result);
                    $("#ddlPrograms").trigger("liszt:updated");
                }
                else {

                    if (contar_program == 0) {
                        arraycompo[1] = $("#ddlPrograms").val();
                        contar_program = 1;
                    }

                    confirmar = confirm("Usted acaba de cambiar de linea estratégica la información diligenciada se perdera! desea cambiarla?", "SI", "NO");
                    if (confirmar) {

                        $("#componentesseleccionados").html("");
                        $("#seleccionarcomponente").html("");
                        
                        arraycomponente = [];
                        
                        $("#ddlPrograms").html(result);
                        $("#ddlPrograms").trigger("liszt:updated");

                    }
                    else {
                    
                        $("#ddlStrategicLines").val(arraycompo[0]);
                        $("#ddlStrategicLines").trigger("liszt:updated");

                        $("#ddlPrograms").val(arraycompo[1]);
                        $("#ddlPrograms").trigger("liszt:updated");

                        contar_program = 0;
                        arraycompo = [];
                    }


                }
            },
            error: function(msg) {
                alert("No se pueden cargar los programas de la linea estrategica selecionada.");
            }
        });
    });
}



//cargar double lisbox componentes de programa
function cargarcomponente() {

    $("#ddlPrograms").change(function() {
        $.ajax({
            url: "AjaxAddProject.aspx",
            type: "GET",
            data: { "action": "C_component", "idprogram": $(this).val() },
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


// agregar componentes al <ul componentesseleccionados>
function Btnaddcomponent_onclick() {

    $("#ctl00_cphPrincipal_Lblinformationcomponent").text("");

    for (itemArray in arraycomponente) {

        var htmlcomponente = $("#" + arraycomponente[itemArray]).html();

        //crea la lista nueva
        var htmlresult = "<li id = 'select" + arraycomponente[itemArray] + "' class = 'des_seleccionar' >" + htmlcomponente + "</li>";
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
