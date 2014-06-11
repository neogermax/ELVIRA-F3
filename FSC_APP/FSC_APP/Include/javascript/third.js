//Javascript para modulo de Third (actores) por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 27/06/2013

//validacion para la fecha que no sea mayor de la fecha actual
$(document).ready(function() {

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

    $("#tabsthird").tabs();
    $("#SaveActors").button();
    $("#ctl00_cphPrincipal_linkcancelar").button();
        
    Charge_Type_people();

    validartipo();
    verificode();
    guardar();
});

//cargar el tipo de personas
function Charge_Type_people() {

    $.ajax({
        url: "AjaxAddThird.aspx",
        type: "GET",
        data: { "action": "Charge_Type_people" },
        success: function(result) {

            $("#DDLtypepeople").html(result);
            $("#DDLtypepeople").trigger("liszt:updated");

        },
        error: function(msg) {
            alert("No se pueden cargar los tipos de personas del actor.");
        }
    });

}

////cargar tipo de documetas del actor
//function Charge_Type_Document() {

//    $.ajax({
//        url: "AjaxAddThird.aspx",
//        type: "GET",
//        data: { "action": "Charge_Type_Document" },
//        success: function(result) {

//            $("#DDL_tipo_doc").html(result);
//            $("#DDL_tipo_doc").trigger("liszt:updated");

//            $("#DDL_tipo_doc1").html(result);
//            $("#DDL_tipo_doc1").trigger("liszt:updated");

//        },
//        error: function(msg) {
//            alert("No se pueden cargar los tipos de documentos del actor.");
//        }
//    });

//}


function validartipo() {

    $("#DDLtypepeople").change(function() {


        if ($("#DDLtypepeople").val() == 1) {

            $("#ctl00_cphPrincipal_HFperson").val(0);

            //persona natural
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

function verificode() {

    $("#ctl00_cphPrincipal_txtcode").change(function() {
        $.ajax({
            url: "AjaxAddThird.aspx",
            type: "GET",
            data: { "action": "verifico",
                "nit": $("#ctl00_cphPrincipal_txtcode").val()
            },
            success: function(result) {
                if (result == 1) {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                    $("#ctl00_cphPrincipal_lblexit2").text("El Actor ya existe, verificar la información ");
                    $("#ctl00_cphPrincipal_txtcode").val("");
                }
                else {
                    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
                }
            },
            error: function() {
                $("#ctl00_cphPrincipal_txtcode").val("");
            }
        });
    });
}

function guardar() {
    $("#ctl00_cphPrincipal_btnAddData").click(function() {

        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
        $("#ctl00_cphPrincipal_lblexit").text("El Actor se creó Exitosamente");
        $("#ctl00_cphPrincipal_btnAddData").css("display", "none");
    });

}
