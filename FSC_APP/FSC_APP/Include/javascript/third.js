//Javascript para modulo de Third (actores) por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 27/06/2013

//region variables globales//
var J_combos = [];
var id_third_editar;
//region variables globales//

$(document).ready(function() {

    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');

    //funcion carga combos//
    load_combos();
    //funcion para validar el tipo de persona y nos mustres los controles indicados//
    validartipo();

    //validamos si creamos la idea o editamos//
    if (sURLVariables[0] == "op=edit") {
        id_third_editar = sURLVariables[1].replace("id=", "");
        //cambiamos el nombre del control
        $("#SaveActors").attr("value", "Guardar cambios");
        Charge_combos();
    }
    else {
        //funcion que verifica si el actor//
        verificode();
    }


    $("#marco_datos_juridica").css("display", "none");
    $("#marco_datos_contact").css("display", "none");

    $("#ctl00_cphPrincipal_Lbltitle2").css("display", "none");

    $("#li_document_principal").css("display", "none");
    $("#li_sex").css("display", "none");

    $("#li_contact").css("display", "none");
    $("#li_actions").css("display", "none");
    $("#li_phone").css("display", "none");
    $("#li_direccion").css("display", "none");
    $("#li_mail").css("display", "none");

    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
    $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");

    $("#tabsthird").tabs();
    $("#SaveActors").button();
    $("#ctl00_cphPrincipal_linkcancelar").button();

});

//funcion que carga combos del modulo actores
function load_combos() {

    $.ajax({
        url: "AjaxAddThird.aspx",
        type: "GET",
        data: { "action": "load_combos" },
        success: function(result) {

            result = JSON.parse(result);

            charge_CatalogList(result[0], "DDLtypepeople",0);
            charge_CatalogList(result[1], "DDL_tipo_doc"),0;
            charge_CatalogList(result[1], "DDL_tipo_doc1",0);
            charge_CatalogList(result[2], "DDL_sex",0);
      
        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de personas del actor.");
        }
    });
}


//funcion que carga combos de actores con los datos guardados previamente
function Charge_combos() {

    $.ajax({
        url: "AjaxAddThird.aspx",
        type: "GET",
        data: { "action": "Charge_combos", "Id_third": id_third_editar },
        success: function(result) {


        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de personas del actor.");
        }
    });
}

//funcion que muestra los objetos que corresponden a cada persona
function validartipo() {

    $("#DDLtypepeople").change(function() {

        if ($("#DDLtypepeople").val() == 1) {

            //persona natural
            $("#ctl00_cphPrincipal_HFperson").val(0);

            $("#ctl00_cphPrincipal_Lbltitle2").css("display", "block");

            $("#ctl00_cphPrincipal_lblcode").text("No. de Documento (Ingresar sin guiones, puntos o espacios).");
            $("#ctl00_cphPrincipal_lblname").text("Nombre del actor");

            $("#marco_datos_juridica").css("display", "none");
            $("#marco_datos_contact").css("display", "block");

            $("#li_document_principal").css("display", "block");
            $("#li_sex").css("display", "block");

            $("#li_contact").css("display", "none");
            $("#li_actions").css("display", "none");
            $("#li_phone").css("display", "block");
            $("#li_direccion").css("display", "block");
            $("#li_mail").css("display", "block");

        }
        else {
            // persona juridica
            $("#ctl00_cphPrincipal_HFperson").val(1);

            $("#ctl00_cphPrincipal_Lbltitle2").css("display", "block");

            $("#ctl00_cphPrincipal_lblcode").text("NIT (Ingresar sin guiones, puntos o espacios. Incluir dígito de verificación)");
            $("#ctl00_cphPrincipal_lblname").text("Nombre de la Organización");

            $("#li_document_principal").css("display", "none");
            $("#li_sex").css("display", "none");

            $("#marco_datos_juridica").css("display", "block");
            $("#marco_datos_contact").css("display", "block");

            $("#li_contact").css("display", "block");
            $("#li_actions").css("display", "none");
            $("#li_phone").css("display", "block");
            $("#li_direccion").css("display", "none");
            $("#li_mail").css("display", "block");

        }
    });
}

//funcion que valida actor repetido en la base de datos
function verificode() {

    $("#ctl00_cphPrincipal_txtcode").change(function() {
        $.ajax({
            url: "AjaxAddThird.aspx",
            type: "GET",
            data: { "action": "verifico",
                "nit": $("#ctl00_cphPrincipal_txtcode").val()
            },
            success: function(result) {
                //mandamos el mesaje de repetido y bloqueamos controles
                if (result == 1) {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                    $("#ctl00_cphPrincipal_lblexit2").text("El Actor ya existe, verificar la información ");
                    $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
                    $("#ctl00_cphPrincipal_lblexit2_down").text("El Actor ya existe, verificar la información ");

                    $("#ctl00_cphPrincipal_txtcode").val("");
                    $("#ctl00_cphPrincipal_txtcode").focus();
                    $("#SaveActors").css("display", "none");

                }
                //desbloqueamos controles y seguimos el proseso
                else {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
                    $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");

                    $("#SaveActors").css("display", "block");

                }
            },
            error: function() {
                $("#ctl00_cphPrincipal_txtcode").val("");
            }
        });
    });
}

//boton que habilita el guardado
function SaveActors_onclick() {

    //revisamos q los campos esten diligenciados
    var value_campos = Validar_campos_dilingenciados();

    if (value_campos == 1) {
        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
        $("#ctl00_cphPrincipal_lblexit2").text("Faltan por diligenciar campos requeridos revisar por favor!");
        $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
        $("#ctl00_cphPrincipal_lblexit2_down").text("Faltan por diligenciar campos requeridos revisar por favor!");

    }
    else {
        $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
        $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");
        $("#ctl00_cphPrincipal_lblexit2").text("");

        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        //validamos si creamos la idea o editamos
        if (sURLVariables[0] == "op=edit") {

            //llamar la funcion editar actor
            editar_actor();
        }
        else {
            //llamar funcion para guardar
            Crear_Actor();
        }


    }
}

//funcion que valida campos obligatorios
function Validar_campos_dilingenciados() {

    var valida_campos;
    //validar persona natural
    if ($("#ctl00_cphPrincipal_HFperson").val() == 0) {

        valida_campos = valida_natural();
    }
    //validar persona juridica
    else {
        valida_campos = valida_juridica();
    }

    return valida_campos;
}

//funcion q valida los campos activos de la persona natural
function valida_natural() {

    var valida_campos;

    if ($("#ctl00_cphPrincipal_txtname").val() == "" || $("#DDLtypepeople").val() == 0 || $("#DDL_tipo_doc1").val() == 0 || $("#DDL_sex").val() == 0) {

        //campo nombre
        if ($("#ctl00_cphPrincipal_txtname").val() == "") {
            $("#ctl00_cphPrincipal_lblHelpname").text("Campo obligatorio");
            $("#ctl00_cphPrincipal_txtname").focus();
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpname").text("");
        }
        //combo persona
        if ($("#DDLtypepeople").val() == 0) {
            $("#ctl00_cphPrincipal_LblDDLtypepeople").text("Campo obligatorio");
        }
        else {
            $("#ctl00_cphPrincipal_LblDDLtypepeople").text("");
        }
        //combo de documento PN
        if ($("#DDL_tipo_doc1").val() == 0) {
            $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc1").text("Campo obligatorio");
        }
        else {
            $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc1").text("");
        }
        //combo de genero
        if ($("#DDL_sex").val() == 0) {
            $("#ctl00_cphPrincipal_LblHELPSEX").text("Campo obligatorio");
        }
        else {
            $("#ctl00_cphPrincipal_LblHELPSEX").text("");
        }

        valida_campos = 1;
    }
    //limpiamos los campos de mensaje obligatorios
    else {
        $("#ctl00_cphPrincipal_lblHelpname").text("");
        $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc1").text("");
        $("#ctl00_cphPrincipal_LblDDLtypepeople").text("");
        $("#ctl00_cphPrincipal_LblHELPSEX").text("");
        valida_campos = 0;
    }
    //devolvemos el valor de la validacion
    return valida_campos;

}


//funcion q valida los campos activos de la persona natural
function valida_juridica() {

    var valida_campos;

    if ($("#ctl00_cphPrincipal_txtname").val() == "" || $("#DDLtypepeople").val() == 0 || $("#DDL_tipo_doc").val() == 0) {

        //campo nombre
        if ($("#ctl00_cphPrincipal_txtname").val() == "") {
            $("#ctl00_cphPrincipal_lblHelpname").text("Campo obligatorio");
            $("#ctl00_cphPrincipal_txtname").focus();
        }
        else {
            $("#ctl00_cphPrincipal_lblHelpname").text("");
        }
        //combo persona
        if ($("#DDLtypepeople").val() == 0) {
            $("#ctl00_cphPrincipal_LblDDLtypepeople").text("Campo obligatorio");
        }
        else {
            $("#ctl00_cphPrincipal_LblDDLtypepeople").text("");
        }
        //combo de documento PJ
        if ($("#DDL_tipo_doc").val() == 0) {
            $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc").text("Campo obligatorio");
        }
        else {
            $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc").text("");
        }

        valida_campos = 1;
    }
    //limpiamos los campos de mensaje obligatorios
    else {
        $("#ctl00_cphPrincipal_LblDDLtypepeople").text("");
        $("#ctl00_cphPrincipal_lblHelpname").text("");
        $("#ctl00_cphPrincipal_LblhelpDDL_tipo_doc").text("");
        valida_campos = 0;
    }
    //devolvemos el valor de la validacion
    return valida_campos;

}

//funcion q guarda en la base de datos
function Crear_Actor() {

    var document;

    //validar persona natural
    if ($("#ctl00_cphPrincipal_HFperson").val() == 0) {
        document = $("#DDL_tipo_doc1").val();
    }
    //validar persona juridica
    else {
        document = $("#DDL_tipo_doc").val();
    }

    $.ajax({
        url: "AjaxAddThird.aspx",
        type: "POST",
        //crear json
        data: { "action": "save",
            "Type_people": $("#DDLtypepeople").val(),
            "Type_document": document,
            "Document": $("#ctl00_cphPrincipal_txtcode").val(),
            "Name": $("#ctl00_cphPrincipal_txtname").val(),
            "Legal_representative": $("#ctl00_cphPrincipal_Txtreplegal").val(),
            "L_rep_doc": $("#ctl00_cphPrincipal_Txtdocrep").val(),
            "Sex": $("#DDL_sex").val(),
            "Contact": $("#ctl00_cphPrincipal_Txtcontact").val(),
            "Phone": $("#ctl00_cphPrincipal_Txtphone").val(),
            "Address": $("#ctl00_cphPrincipal_Txtdireccion").val(),
            "Email": $("#ctl00_cphPrincipal_Txtemail").val()
        },
        //mostrar resultados de la creacion de la idea
        success: function(result) {

            if (result == "1") {

                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblexit").text("El Actor se creó Exitosamente");
                $("#ctl00_cphPrincipal_lblexit2").text("");
                $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
                $("#ctl00_cphPrincipal_lblexit_down").text("El Actor se creó Exitosamente");
                $("#ctl00_cphPrincipal_lblexit2_down").text("");
                $("#SaveActors").css("display", "none");
                clear();
            }
            else {
                $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                $("#ctl00_cphPrincipal_lblexit2").text("Falla temporal con la conexión");
                $("#ctl00_cphPrincipal_lblexit").text("");
                $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "block");
                $("#ctl00_cphPrincipal_lblexit2_down").text("Falla temporal con la conexión");
                $("#ctl00_cphPrincipal_lblexit_down").text("");

            }

        },
        error: function() {

            $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
            $("#SaveActors").css("display", "block");
            $("#ctl00_cphPrincipal_lblexit2").text("Se genero error al entrar a la operacion Ajax :");
            $("#ctl00_cphPrincipal_containerSuccess_down").css("display", "none");
            $("#ctl00_cphPrincipal_lblexit2_down").text("Se genero error al entrar a la operacion Ajax :");

        }

    });
}

function editar_actor() {

}

//funcion q borra los campos despues de guardar
function clear() {

    $("#ctl00_cphPrincipal_txtcode").val("");
    $("#ctl00_cphPrincipal_txtname").val("");
    $("#ctl00_cphPrincipal_Txtreplegal").val("");
    $("#ctl00_cphPrincipal_Txtdocrep").val("");
    $("#ctl00_cphPrincipal_Txtphone").val("");
    $("#ctl00_cphPrincipal_Txtdireccion").val("");
    $("#ctl00_cphPrincipal_Txtemail").val("");
    $("#ctl00_cphPrincipal_txtcode").focus();
}