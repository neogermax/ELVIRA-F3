<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.addIdea"
    Title="addIdea" CodeBehind="addIdea.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script src="../Include/javascript/mdfu.js" type="text/javascript"></script>

    <script src="../Include/javascript/idea.js" type="text/javascript"></script>

    <link href="../css/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <link href="../css/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>

    <style>
        #informacion
        {
            color: #333333;
            font-family: Tahoma, Geneva, sans-serif;
        }
        #informacion ul
        {
            width: 100%;
            columns: 2;
            -webkit-columns: 2;
            -moz-columns: 2;
        }
        #informacion li
        {
            width: 100%;
        }
        #informacion label, input[type='text'], textarea, select, span
        {
            display: block;
            margin-bottom: 1em;
        }
        #informacion table th, #informacion table td
        {
            text-align: center;
        }
        #anexos
        {
            color: #333333;
            font-family: Tahoma, Geneva, sans-serif;
        }
        #anexos ul
        {
            width: 100%;
        }
        #anexos li
        {
            width: 100%;
        }
        #anexos label, input[type='text'], textarea, select
        {
            display: block;
            margin-bottom: 1em;
        }
        #ubicacion
        {
            color: #333333;
            font-family: Tahoma, Geneva, sans-serif;
        }
        #ubicacion ul
        {
            width: 100%;
        }
        #ubicacion li
        {
            width: 100%;
        }
        #ubicacion label, #ubicacion input[type='text'], #ubicacion textarea, #ubicacion select, #ubicacion span
        {
            display: block;
            margin-bottom: 1em;
        }
        #actores
        {
            color: #333333;
            font-family: Tahoma, Geneva, sans-serif;
        }
        #actores ul
        {
            width: 100%;
        }
        #actores li
        {
            width: 100%;
        }
        #actores label, input[type='text'], textarea, select, span
        {
            display: block;
            margin-bottom: 1em;
        }
    </style>

    <script type="text/javascript">

        var arrayUbicacion = Array.prototype;

        function format(input) {
            var num = input.value.replace(/\./g, "");
            if (!isNaN(num)) {
                num = num.toString().split("").reverse().join("").replace(/(?=\d*\.?)(\d{3})/g, "$1.");
                num = num.split("").reverse().join("").replace(/^[\.]/, "");
                input.value = num;
            }

            else {
                alert('Solo se permiten numeros');
                input.value = input.value.replace(/[^\d\.]*/g, "");
            }
        }

        $(document).ready(function() {
            ClineEstrategic();
            Cprogram();
            Cdeptos();
            Cmunip();
            Cactors();
            CtypeContract();
            $("#ctl00_cphPrincipal_containerSuccess").css("display", "none");
            $("#tabsIdea").tabs();
            $("#matriz").dataTable({
                "bJQueryUI": true
            });
            $("#T_location").dataTable({
                "bJQueryUI": true,
                "bDestroy": true
            });



        });

        //funcion de refactorizacion idea fase 3 ---- autor:German Rodriguez MGgroup
        //guardar idea
        function SaveIdea_onclick() {

            if ($("#ddlStrategicLines").val() == 'Seleccione...' || $("#ddlPrograms").val() == 'Seleccione...' || $("#ctl00_cphPrincipal_txtname").val() == '' || $("#ctl00_cphPrincipal_txtjustification").val() == '' || $("#ctl00_cphPrincipal_txtobjective").val() == '' || $("#ctl00_cphPrincipal_txtstartdate").val() == '' || $("#ctl00_cphPrincipal_txtduration").val() == '') {

                if ($("#ddlStrategicLines").val() == 'Seleccione...') {
                    $("#ctl00_cphPrincipal_lblinfls").val("Campo es requerido");
                }
                if ($("#ddlPrograms").val() == 'Seleccione...') {
                    $("#ctl00_cphPrincipal_lblinpro").val("Campo es requerido");
                }
                if ($("#ctl00_cphPrincipal_txtname").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpname").text("Campo es requerido");
                }
                if ($("#ctl00_cphPrincipal_txtjustification").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpjustification").text("Campo es requerido");
                }
                if ($("#ctl00_cphPrincipal_txtobjective").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpobjective").text("Campo es requerido");
                }
                if ($("#ctl00_cphPrincipal_txtstartdate").val() == '') {
                    $("#ctl00_cphPrincipal_lblHelpstartdate").text("Campo es requerido");
                }
                if ($("#ctl00_cphPrincipal_txtduration").val() == '') {
                    $("#ctl00_cphPrincipal_lbldia").text("Campo es requerido");
                }

            }

            else {

                $("#ctl00_cphPrincipal_lblinfls").val("");
                $("#ctl0_cphPrincipal_lblinpro").val("");
                $("#ctl00_cphPrincipal_lblHelpname").text("");
                $("#ctl00_cphPrincipal_lblHelpjustification").text("");
                $("#ctl00_cphPrincipal_lblHelpobjective").text("");
                $("#ctl00_cphPrincipal_lblHelpstartdate").text("");
                $("#ctl00_cphPrincipal_lbldia").text("");

                $.ajax({
                    url: "AjaxAddIdea.aspx",
                    type: "GET",
                    data: { "action": "save", "code": $("#ctl00_cphPrincipal_txtcode").val(),
                        "linea_estrategica": $("#ddlStrategicLines").val(),
                        "programa": $("#ddlPrograms").val(),
                        "nombre": $("#ctl00_cphPrincipal_txtname").val(),
                        "justificacion": $("#ctl00_cphPrincipal_txtjustification").val(),
                        "objetivo": $("#ctl00_cphPrincipal_txtobjective").val(),
                        "objetivo_esp": $("#ctl00_cphPrincipal_txtareadescription").val(),
                        "Resultados_Benef": $("#ctl00_cphPrincipal_txtresults").val(),
                        "Resultados_Ges_c": $("#ctl00_cphPrincipal_txtresulgc").val(),
                        "Resultados_Cap_i": $("#ctl00_cphPrincipal_txtresulci").val(),
                        "Fecha_inicio": $("#ctl00_cphPrincipal_txtstartdate").val(),
                        "mes": $("#ctl00_cphPrincipal_txtduration").val(),
                        "dia": $("#ctl00_cphPrincipal_Txtday").val(),
                        "Fecha_fin": $("#ctl00_cphPrincipal_Txtdatecierre").val(),
                        "Población": $("#ctl00_cphPrincipal_ddlPupulation").val(),
                        "contratacion": $("#ddlmodcontract").val(),
                        "A_Mfsc": $("#ctl00_cphPrincipal_ValueMoneyFSC").val(),
                        "A_Efsc": $("#ctl00_cphPrincipal_ValueEspeciesFSC").val(),
                        "A_Mcounter": $("#ctl00_cphPrincipal_ValueMoneyCounter").val(),
                        "A_Ecounter": $("#ctl00_cphPrincipal_ValueEspeciesCounter").val(),
                        "cost": $("#ctl00_cphPrincipal_ValueCostotal").val()
                    },
                    success: function(result) {
                        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                        $("#ctl00_cphPrincipal_lblsaveinformation").text(result);
                    },
                    error: function() {
                        $("#ctl00_cphPrincipal_containerSuccess").css("display", "block");
                        $("#ctl00_cphPrincipal_lblsaveinformation").text("Se genero error al entrar a la operacion Ajax");
                    }
                });
            }
        }


        function Add_location_onclick() {

            var deptoVal = $("#ddlDepto").val();
            var deptoName = $("#ddlDepto :selected").text();

            var cityVal = $("#ddlCity").val();
            var cityName = $("#ddlCity :selected").text();

            var jsonUbicacion = { "DeptoVal": deptoVal, "DeptoName": deptoName, "CityVal": cityVal, "CityName": cityName };

            arrayUbicacion.push(jsonUbicacion);

            var htmlTable = "<table id='T_location' border='2' cellpadding='2' cellspacing='2' style='width: 100%;'><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead> <tbody>";

            for (itemArray in arrayUbicacion) {
                htmlTable += "<tr><td>" + arrayUbicacion[itemArray].DeptoName + "</td><td>" + arrayUbicacion[itemArray].CityName + "</td><td><button>Eliminar</button></td></tr>";
            }

            htmlTable += "</tbody></table>";

            $("#T_locationContainer").html("");
            $("#T_locationContainer").html(htmlTable);

            //console.log(htmlTable);

            /*$("#T_location").dataTable({
            "bJQueryUI": true,
            "bDestroy": true
            });*/

        }


        //cargar combo de lineas estrategicas
        function ClineEstrategic() {
            $.ajax({
                url: "AjaxAddIdea.aspx",
                type: "GET",
                data: { "action": "C_linestrategic" },
                success: function(result) {
                    $("#ddlStrategicLines").html(result);
                    $("#ddlStrategicLines").trigger("liszt:updated");
                },
                error: function(msg) {
                    alert("No se pueden cargar las lineas strategicas.");
                }
            });
        }

        //cargar combo de programas
        function Cprogram() {
            $("#ddlStrategicLines").change(function() {
                $.ajax({
                    url: "AjaxAddIdea.aspx",
                    type: "GET",
                    data: { "action": "C_program", "idlinestrategic": $(this).val() },
                    success: function(result) {
                        $("#ddlPrograms").html(result);
                        $("#ddlPrograms").trigger("liszt:updated");
                    },
                    error: function(msg) {
                        alert("No se pueden cargar los programas de la linea estrategica selecionada.");
                    }
                });
            });
        }

        //cargar combo de departamentos
        function Cdeptos() {
            $.ajax({
                url: "AjaxAddIdea.aspx",
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
                    url: "AjaxAddIdea.aspx",
                    type: "GET",
                    data: { "action": "C_munip", "iddepto": $(this).val() },
                    success: function(result) {
                        $("#ddlCity").html(result);
                        $("#ddlCity").trigger("liszt:updated");
                    },
                    error: function(msg) {
                        alert("No se pueden cargar los municipios del departamento seleccionado.");
                    }
                });
            });
        }

        //cargar combo actores
        function Cactors() {
            $.ajax({
                url: "AjaxAddIdea.aspx",
                type: "GET",
                data: { "action": "C_Actors" },
                success: function(result) {
                    $("#ddlactors").html(result);
                    $("#ddlactors").trigger("liszt:updated");
                },
                error: function(msg) {
                    alert("No se pueden cargar los actores.");
                }
            });
        }

        //cargar combo tipos de contratos
        function CtypeContract() {
            $.ajax({
                url: "AjaxAddIdea.aspx",
                type: "GET",
                data: { "action": "C_typecontract" },
                success: function(result) {
                    $("#ddlmodcontract").html(result);
                    $("#ddlmodcontract").trigger("liszt:updated");
                },
                error: function(msg) {
                    alert("No se pueden cargar los tipos de contrato.");
                }
            });
        }
 
    </script>

    <br />
    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <table style="width: 100%">
        <tr>
            <td>
                <asp:HiddenField ID="HDIDTHIRD" runat="server" />
                <asp:HiddenField ID="HDaddidea" runat="server" />
                <asp:HiddenField ID="HFdate" runat="server" />
                <asp:HiddenField ID="HFEndDate" runat="server" />
                <asp:HiddenField ID="HDvaluestotal" runat="server" />
                <asp:HiddenField ID="HDNAMETHIRD" runat="server" />
            </td>
        </tr>
    </table>
    <%--REFACTOR IDEA--%>
    <div id="tabsIdea">
        <ul>
            <li><a href="#informacion">Información Principal</a></li>
            <li><a href="#anexos">Archivos Anexos</a></li>
            <li><a href="#ubicacion">Ubicación</a></li>
            <li><a href="#actores">Actores</a></li>
            <li><a href="#componentes">Componentes de Programa</a></li>
            <li><a href="#flujos">Flujos de pago</a></li>
        </ul>
        <div id="informacion">
            <ul>
                <li>
                    <asp:Label ID="Label7" runat="server" Text="Línea Estrátegica "></asp:Label>
                    <select id="ddlStrategicLines" class="Ccombo">
                        <asp:DropDownList ID="ddlStrategicLines" runat="server">
                        </asp:DropDownList>
                    </select>
                    <asp:Label ID="lblinfls" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label8" runat="server" Text="Programa"></asp:Label>
                    <select id="ddlPrograms" class="Ccombo">
                        <asp:DropDownList ID="ddlPrograms" runat="server">
                        </asp:DropDownList>
                    </select>
                    <asp:Label ID="lblinpro" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li visible="false">
                    <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                    <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtcode" runat="server" MaxLength="50" Width="400px" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </li>
                <li>
                    <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtname" runat="server" MaxLength="255" Width="400px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                        ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpname" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblJustificacion" runat="server" Text="Justificación"></asp:Label>
                    <asp:TextBox ID="txtjustification" runat="server" MaxLength="255" Width="400px" Rows="2"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtjustification"
                        ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpjustification" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblobjective" runat="server" Text="Objetivo"></asp:Label>
                    <asp:TextBox ID="txtobjective" runat="server" MaxLength="255" Width="400px" Rows="2"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtobjective"
                        ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpobjective" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblareadescription" runat="server" Text="Objetivos Específicos"></asp:Label>
                    <asp:TextBox ID="txtareadescription" runat="server" MaxLength="4000" Width="400px"
                        Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpareadescription" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblresults" runat="server" Text="Resultados en Beneficiarios"></asp:Label>
                    <asp:TextBox ID="txtresults" runat="server" MaxLength="4000" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpresults" runat="server" ForeColor="#CC0000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblresulgc" runat="server" Text="Resultados en Gestión del conocimiento"></asp:Label>
                    <asp:TextBox ID="txtresulgc" runat="server" MaxLength="4000" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" ForeColor="#CC0000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblresultci" runat="server" Text="Resultados en Capacidad instalada"></asp:Label>
                    <asp:TextBox ID="txtresulci" runat="server" MaxLength="4000" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" ForeColor="#CC0000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblstartdate" runat="server" Text="Fecha de inicio"></asp:Label>
                    <asp:TextBox ID="txtstartdate" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cestartdate" runat="server" Enabled="true" Format="yyyy/MM/dd"
                        TargetControlID="txtstartdate">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtstartdate"
                        ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvstartdate" runat="server" ControlToValidate="txtstartdate"
                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="true" Type="Date"></asp:CompareValidator>
                    <asp:Label ID="lblHelpstartdate" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblduration" runat="server" Text="Mes"></asp:Label>
                    <asp:TextBox ID="txtduration" runat="server" MaxLength="5" Width="100px" Rows="2"></asp:TextBox>
                    <asp:Label ID="Lbldia" runat="server" Text="Dia"></asp:Label>
                    <asp:TextBox ID="Txtday" runat="server" MaxLength="5" Width="100px" Rows="2"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Lbldateend" runat="server" Text="Fecha de Finalización"></asp:Label>
                    <asp:TextBox ID="Txtdatecierre" runat="server" MaxLength="255" Width="100px" Rows="2"
                        Enabled="False"></asp:TextBox>
                    <asp:Label ID="Label22" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblpopulation" runat="server" Text="Población"></asp:Label>
                    <asp:DropDownList ID="ddlPupulation" runat="server" CssClass="Ccombo">
                        <asp:ListItem Value="1">Personas con Discapacidad</asp:ListItem>
                        <asp:ListItem Value="2">Personas Mayores</asp:ListItem>
                        <asp:ListItem Value="3">Persona con discapacidad y personas mayores</asp:ListItem>
                        <asp:ListItem Value="4">Otros</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblHelppopulation" runat="server"></asp:Label>
                </li>
                <%-- <li>
                    <asp:Label ID="lblfsccontribution" runat="server" Text="Aporte FSC"></asp:Label>
                    <asp:TextBox ID="txtfsccontribution" runat="server" Width="400px" MaxLength="15"
                        onkeyup="format(this)" onchange="format(this)" Enabled="False"></asp:TextBox>
                    <asp:Label ID="lblHelpfsccontribution" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcounterpartvalue" runat="server" Text="Valor conliapartida"></asp:Label>
                    <asp:TextBox ID="txtcounterpartvalue" runat="server" Width="400px" MaxLength="15"
                        ValidationGroup="generalProject" onkeyup="format(this)" onchange="format(this)"
                        Enabled="False"></asp:TextBox>
                    <asp:Label ID="lblHelpcounterpartvalue" runat="server"></asp:Label>
                </li>--%>
                <li>
                    <asp:Label ID="Label19" runat="server" Text="Modalidad de Contratación"></asp:Label>
                    <select id="ddlmodcontract" class="Ccombo">
                        <asp:DropDownList ID="ddlmodcontract" runat="server">
                        </asp:DropDownList>
                    </select>
                    <asp:Label ID="Label20" runat="server"></asp:Label>
                </li>
            </ul>
            <br />
            <table id="matriz" border="2" cellpadding="2" cellspacing="2" style="width: 100%;">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <span><strong style="font-size: large">Efectivo&nbsp;</strong> </span>
                        </th>
                        <th>
                            <span><strong style="font-size: large">Especie&nbsp;</strong> </span>
                        </th>
                        <th>
                            <span><strong style="font-size: large">Total&nbsp;</strong> </span>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <span><strong style="font-size: large">Aportes FSC&nbsp;</strong> </span>
                        </td>
                        <td>
                            <asp:Label ID="ValueMoneyFSC" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueEspeciesFSC" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueCostFSC" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span><strong style="font-size: large">Aportes Socios&nbsp;</strong> </span>
                        </td>
                        <td>
                            <asp:Label ID="ValueMoneyCounter" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueEspeciesCounter" runat="server" BorderStyle="Solid"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueCostCounter" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span><strong style="font-size: large">Valor Total&nbsp;</strong> </span>
                        </td>
                        <td>
                            <asp:Label ID="valueMoneytotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueEspeciestotal" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="ValueCostotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <ul>
                <li>
                    <asp:Label ID="lblcost" runat="server" Text="Valor Total"></asp:Label>
                    <asp:Label ID="txtcost" runat="server" Width="400px"></asp:Label>
                    <asp:Label ID="lblHelpcost" runat="server"></asp:Label>
                </li>
                <li id="li5" runat="server" visible="False">
                    <asp:Label ID="lblsource" runat="server" Text="Fuente"></asp:Label>
                    <asp:DropDownList ID="ddlSource" runat="server">
                        <asp:ListItem>Investigación</asp:ListItem>
                        <asp:ListItem>Propuesta de un Socio</asp:ListItem>
                        <asp:ListItem>Propuesta de un Operador</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSource"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="infoGenral"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpsource" runat="server"></asp:Label>
                </li>
                <li id="li6" runat="server" visible="False">
                    <asp:Label ID="lblconvocatory" runat="server" Text="Convocatoria"></asp:Label>
                    <asp:DropDownList ID="ddlSummoning" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSummoning"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="infoGenral"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpconvocatory" runat="server"></asp:Label>
                </li>
                <li id="li7" runat="server" visible="False">
                    <asp:Label ID="lblstrategydescription" runat="server" Text="Descripción de la estrategia"></asp:Label>
                    <asp:TextBox ID="txtstrategydescription" runat="server" MaxLength="4000" Width="400px"
                        Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpsliategydescription" runat="server"></asp:Label>
                </li>
                <li id="li8" runat="server" visible="False">
                    <asp:Label ID="lblstartprocess" runat="server" Text="Crear proceso"></asp:Label>
                    <asp:CheckBox ID="chkStartProcess" runat="server" />
                    <asp:Label ID="lblHelpstartprocess" runat="server"></asp:Label>
                </li>
                <li id="li1" runat="server" visible="False">
                    <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                    <asp:DropDownList ID="ddlenabled" runat="server">
                        <asp:ListItem Text="Habilitado" Value="true"></asp:ListItem>
                        <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblHelpattachfile" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtiduser" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                    <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                    <asp:TextBox ID="txtcreatedate" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                    <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Button ID="btnAddData" runat="server" Text="Guardar" ValidationGroup="infoGenral"
                        Visible="false" />
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="infoGenral" />
                    <asp:Button ID="btnDelete" runat="server" Text="Eliminar Idea" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                    <asp:Button ID="btnexportword" runat="server" Text="Exportar términos de referencia"
                        ValidationGroup="infoGenral" />
                    <input id="SaveIdea" type="button" value="Crear Idea" name="Save_Idea" onclick="SaveIdea_onclick()" /></li>
                <li>
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                    <asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el regislio?"
                        ForeColor="Red"></asp:Label>
                </li>
            </ul>
        </div>
        <div id="anexos">
            <ul>
                <li id="tableAttachments">
                    <asp:Label ID="lblattachfile" runat="server" Text="Archivo anexo"></asp:Label>
                </li>
                <li>
                    <img src="../App_Themes/GattacaAdmin/Images/attach.gif" alt="" />
                    <a id="lnkAttch" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'"
                        style="cursor: hand" onclick="AddFileInput()">Adjuntar un archivo</a>
                    <asp:Label ID="Label12" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="obser" runat="server" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="txtobser" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                </li>
                <li id="tdFileInputs">
                    <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addDocuments.aspx?op=edit&id={0}&isPopup=True"
                                        HeaderText="Edición" Text="Editar" Target="_blank" />
                                    <asp:TemplateField HeaderText="Eliminación" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                OnClientClick="return confirm('Esta seguro?')" Text="Eliminar"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="title" HeaderText="Título" />
                                    <asp:BoundField DataField="description" HeaderText="Descripción" />
                                    <asp:BoundField DataField="EDITEDFORNAME" HeaderText="Editado por" />
                                    <asp:BoundField DataField="VISIBILITYLEVELNAME" HeaderText="Nivel visibilidad" />
                                    <asp:BoundField DataField="DOCUMENTTYPENAME" HeaderText="Tipo documento" />
                                    <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                    <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                    <asp:HyperLinkField DataNavigateUrlFields="attachfile" DataTextField="attachfile"
                                        HeaderText="Anexo" Target="_blank" />
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btnRefresh" runat="server" Text="Actualizar Listado" /></ContentTemplate>
                    </asp:UpdatePanel>
                </li>
            </ul>
        </div>
        <div id="ubicacion">
            <ul>
                <li>
                    <asp:Label ID="Label1" runat="server" Text="Departamento"></asp:Label>
                    <select id="ddlDepto" class="Ccombo">
                        <asp:DropDownList ID="ddlDepto" runat="server">
                        </asp:DropDownList>
                    </select>
                </li>
                <li>
                    <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
                    <select id="ddlCity" class="Ccombo">
                        <asp:DropDownList ID="ddlCity" runat="server">
                        </asp:DropDownList>
                    </select>
                </li>
                <li>
                    <asp:Button ID="btnAgregarubicacion" runat="server" CausesValidation="False" Text="Agregar Ubicación" />
                    <input id="B_add_location" type="button" value="Agregar Ubicación" name="Add_location"
                        onclick="Add_location_onclick()" />
                </li>
            </ul>
            <br />
            <div id="T_locationContainer">
                <table id="T_location" border="2" cellpadding="2" cellspacing="2" style="width: 100%;">
                    <thead>
                        <tr>
                            <th>
                                <span><strong style="font-size: large">Departamento&nbsp;</strong> </span>
                            </th>
                            <th>
                                <span><strong style="font-size: large">Ciudad&nbsp;</strong> </span>
                            </th>
                            <th>
                                <span><strong style="font-size: large">Eliminar</strong> </span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <ul>
                <li>
                    <asp:GridView ID="gvLocations" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Departamento">
                                <ItemTemplate>
                                    <asp:Label ID="lbldepto" runat="server" Text='<%# Eval("Depto.name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ciudad">
                                <ItemTemplate>
                                    <asp:Label ID="lblcity" runat="server" Text='<%# Eval("City.name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </li>
            </ul>
        </div>
        <div id="actores">
            <ul>
                <li>
                    <asp:Label ID="Label2" runat="server" Text="Actor"></asp:Label>
                </li>
                <li>
                    <select id="ddlactors" class="Ccombo">
                        <asp:DropDownList ID="ddlactors" runat="server">
                        </asp:DropDownList>
                    </select>
                    <a id="linkactors" runat="server" href="~/GeneralPlanning/addThird.aspx?op=add?iframe=true&width=100%&height=100%"
                        title="Nuevo actor" class="pretty">CREAR NUEVO ACTOR</a> </li>
                <li>
                    <asp:Label ID="Label6" runat="server" Text="Tipo"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="Ccombo">
                        <asp:ListItem>Operador</asp:ListItem>
                        <asp:ListItem>Socio Operador</asp:ListItem>
                        <asp:ListItem>Socio</asp:ListItem>
                        <asp:ListItem>Cliente</asp:ListItem>
                        <asp:ListItem>Contratante</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="li111" runat="server" visible="False">
                    <asp:Label ID="Label4" runat="server" Text="Aporte o rol"></asp:Label>
                    <asp:TextBox ID="txtActions" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li id="li112" runat="server" visible="False">
                    <asp:Label ID="Label5" runat="server" Text="Experiencias"></asp:Label>
                    <asp:TextBox ID="txtExperiences" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label9" runat="server" Text="Contacto"></asp:Label>
                    <asp:TextBox ID="Txtcontact" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label15" runat="server" Text="C.C"></asp:Label>
                    <asp:TextBox ID="Txtcedulacont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label16" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="Txttelcont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label17" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="Txtemail" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="vrdiner" runat="server" Text="Vr Dinero"></asp:Label>
                    <asp:TextBox ID="Txtvrdiner" runat="server" Width="200px" MaxLength="22" Rows="3"
                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label13" runat="server" Text="Vr Especie"></asp:Label>
                    <asp:TextBox ID="Txtvresp" runat="server" Width="200px" MaxLength="22" Rows="3" onkeyup="format(this)"
                        onchange="format(this)"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label14" runat="server" Text="Total Aporte del Actor"></asp:Label>
                    <asp:TextBox ID="Txtaportfscocomp" runat="server" Width="200px" MaxLength="30" Rows="3"
                        Enabled="False"></asp:TextBox>
                </li>
                <li>
                    <asp:Button ID="btnAddThird" runat="server" Text="Agregar Actor" ValidationGroup="thirdBYIdea" />
                    <asp:Label ID="lblavertenactors" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        ForeColor="Red"></asp:Label>
                </li>
                <li>
                    <asp:GridView ID="gvThirds" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdactor" runat="server" Text='<%# Eval("idthird") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actores">
                                <ItemTemplate>
                                    <asp:Label ID="lblIname" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="lbltype" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contacto">
                                <ItemTemplate>
                                    <asp:Label ID="lblIcontact" runat="server" Text='<%# Eval("contact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Documento Identidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdocument" runat="server" Text='<%# Eval("documents") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Teléfono">
                                <ItemTemplate>
                                    <asp:Label ID="lblphone" runat="server" Text='<%# Eval("phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo electrónico">
                                <ItemTemplate>
                                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr Dinero">
                                <ItemTemplate>
                                    <asp:Label ID="lblMoney" runat="server" Text='<%# Eval("Vrmoney") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vr Especie">
                                <ItemTemplate>
                                    <asp:Label ID="lblespecies" runat="server" Text='<%# Eval("VrSpecies") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Actor">
                                <ItemTemplate>
                                    <asp:Label ID="lbltolfsc" runat="server" Text='<%# Eval("FSCorCounterpartContribution") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </li>
                <li id="totales" runat="server" visible="false">
                    <asp:Label ID="Label18" runat="server" Text=" Totales"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub1" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Textsub2" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub3" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                </li>
            </ul>
        </div>
        <div id="componentes">
            <ul>
                <li>
                    <cc2:DoubleListBox ID="dlbActivity" runat="server" Width="100%" />
                </li>
            </ul>
        </div>
        <div id="flujos">
            <ul>
                <li>
                    <asp:Label ID="lblvalortotal" runat="server" Text="Valor Total"></asp:Label>
                    <asp:TextBox ID="txtvalortotalflow" runat="server" Width="186px" MaxLength="50" Text="0"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblfechapago" runat="server" Text="Fecha de pago"></asp:Label>
                    <asp:TextBox ID="txtfechapago" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <cc1:CalendarExtender ID="cesfechapago" runat="server" TargetControlID="txtfechapago"
                        Format="yyyy/MM/dd" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfechapago"
                        ErrorMessage="el campo fecha esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblhelpfechapago" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje"></asp:Label>
                    <asp:TextBox ID="txtporcentaje" Text="0" runat="server" MaxLength="50" Width="95px"></asp:TextBox>
                    <asp:Label ID="lblFlowNfo" runat="server" Text="." ForeColor="White"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPorcent" runat="server" ControlToValidate="txtporcentaje"
                        ErrorMessage="el campo esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Label ID="lblvalor" runat="server" Text="Valor"></asp:Label>
                    <asp:TextBox ID="txtvalorpartial" runat="server" MaxLength="50" ReadOnly="true" Width="182px"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="lblentregable" runat="server" Text="Entregable"></asp:Label>
                    <asp:TextBox ID="txtentregable" runat="server" Height="100px" MaxLength="8000" onkeypress="return textboxMultilineMaxNumber(this,800)"
                        TextMode="MultiLine" Width="450px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtentregable"
                        ErrorMessage="el campo esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:Button ID="BtnAddPayment" runat="server" Text="Agregar Pago" />
                </li>
                <li>
                    <asp:GridView ID="gvPaymentFlow" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="idproject" Visible="false">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblfecha" runat="server" Text='<%# Eval("fecha","{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Porcentaje">
                                <ItemTemplate>
                                    <asp:Label ID="lblporcentaje" runat="server" Text='<%# Eval("porcentaje")&"%" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entregable">
                                <ItemTemplate>
                                    <asp:Label ID="lblentregable" runat="server" Text='<%# Eval("entregable") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ididea" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblidea" runat="server" Text='<%# Eval("ididea") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor parcial">
                                <ItemTemplate>
                                    <asp:Label ID="lblidea" runat="server" Text='<%#  Eval("valorparcial","{0:C1}") %>'></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="200px" />
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </li>
                <li>
                    <asp:Label ID="lblmsjporcent" runat="server" ForeColor="Red"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="LblFlujodePagoPorcentajeIzquierda" runat="server" Font-Bold="True"
                        Font-Names="Arial Narrow" Font-Size="1.2em" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblmensajeexitoerror" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        ForeColor="Red" Font-Size="1.2em"></asp:Label>
                    <asp:Label ID="LblFlujodePagoPorcentajeDerecha" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        Font-Size="1.2em" ForeColor="Red"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblExceed100" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        Font-Size="1.2em" ForeColor="Red"></asp:Label>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
