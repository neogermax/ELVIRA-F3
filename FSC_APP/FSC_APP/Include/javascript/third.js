//Javascript para modulo de Third (actores) por parte de MG GROUP Ltda.
//Autor: German Rodriguez
//Fecha Inicio: 27/06/2013

//validacion para la fecha que no sea mayor de la fecha actual
$(document).ready(function() {

    $("#ctl00_cphPrincipal_Lblcontact").css("display", "none");
    $("#ctl00_cphPrincipal_Txtcontact").css("display", "none");

    $("#ctl00_cphPrincipal_Lblactions").css("display", "none");
    $("#ctl00_cphPrincipal_Txtactions").css("display", "none");

    $("#ctl00_cphPrincipal_Lblphone").css("display", "none");
    $("#ctl00_cphPrincipal_Txtphone").css("display", "none");

    $("#ctl00_cphPrincipal_Lblmail").css("display", "none");
    $("#ctl00_cphPrincipal_Txtemail").css("display", "none");

    $("#ctl00_cphPrincipal_lblreplegal").css("display", "none");
    $("#ctl00_cphPrincipal_Txtreplegal").css("display", "none");

    $("#ctl00_cphPrincipal_Lbltipodocument").css("display", "none");
    $("#ctl00_cphPrincipal_DDL_tipo_doc").css("display", "none");

    $("#ctl00_cphPrincipal_Lbldocrep").css("display", "none");
    $("#ctl00_cphPrincipal_Txtdocrep").css("display", "none");

    $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");

    $("#ctl00_cphPrincipal_Lbltitle1").css("display", "none");
    $("#ctl00_cphPrincipal_Lbltitle2").css("display", "none");


    validartipo();
    verificode();
    guardar();
});

function validartipo() {

    $("#ctl00_cphPrincipal_DDLtypepeople").change(function() {

        if ($("#ctl00_cphPrincipal_DDLtypepeople").val() == 'True') {

            $("#ctl00_cphPrincipal_Lbltitle1").css("display", "none");
            $("#ctl00_cphPrincipal_Lbltitle2").css("display", "block");

            $("#ctl00_cphPrincipal_lblcode").text("Nombre, Sin guiones, puntos ó espacios. Incluye digito de verificación");
            $("#ctl00_cphPrincipal_lblname").text("Nombre del actor");

            $("#ctl00_cphPrincipal_Lblcontact").css("display", "block");
            $("#ctl00_cphPrincipal_Txtcontact").css("display", "block");

            $("#ctl00_cphPrincipal_Lblactions").css("display", "block");
            $("#ctl00_cphPrincipal_Txtactions").css("display", "block");

            $("#ctl00_cphPrincipal_Lblphone").css("display", "block");
            $("#ctl00_cphPrincipal_Txtphone").css("display", "block");

            $("#ctl00_cphPrincipal_Lblmail").css("display", "block");
            $("#ctl00_cphPrincipal_Txtemail").css("display", "block");

            $("#ctl00_cphPrincipal_lblreplegal").css("display", "none");
            $("#ctl00_cphPrincipal_Txtreplegal").css("display", "none");

            $("#ctl00_cphPrincipal_Lbltipodocument").css("display", "none");
            $("#ctl00_cphPrincipal_DDL_tipo_doc").css("display", "none");

            $("#ctl00_cphPrincipal_Lbldocrep").css("display", "none");
            $("#ctl00_cphPrincipal_Txtdocrep").css("display", "none");

        }
        else {

            $("#ctl00_cphPrincipal_Lbltitle1").css("display", "block");
            $("#ctl00_cphPrincipal_Lbltitle2").css("display", "block");

            $("#ctl00_cphPrincipal_lblcode").text("Nit, Sin guiones, puntos ó espacios. Incluye digito de verificación");
            $("#ctl00_cphPrincipal_lblname").text("Razón social");

            $("#ctl00_cphPrincipal_Lblcontact").css("display", "block");
            $("#ctl00_cphPrincipal_Txtcontact").css("display", "block");

            $("#ctl00_cphPrincipal_Lblactions").css("display", "block");
            $("#ctl00_cphPrincipal_Txtactions").css("display", "block");

            $("#ctl00_cphPrincipal_Lblphone").css("display", "block");
            $("#ctl00_cphPrincipal_Txtphone").css("display", "block");

            $("#ctl00_cphPrincipal_Lblmail").css("display", "block");
            $("#ctl00_cphPrincipal_Txtemail").css("display", "block");

            $("#ctl00_cphPrincipal_lblreplegal").css("display", "block");
            $("#ctl00_cphPrincipal_Txtreplegal").css("display", "block");

            $("#ctl00_cphPrincipal_Lbltipodocument").css("display", "block");
            $("#ctl00_cphPrincipal_DDL_tipo_doc").css("display", "block");

            $("#ctl00_cphPrincipal_Lbldocrep").css("display", "block");
            $("#ctl00_cphPrincipal_Txtdocrep").css("display", "block");

        }
    });
}

function verificode() {
    $("#ctl00_cphPrincipal_txtcode").blur(function() {
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

    });

}
