<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.addProject"
    Title="addProject" CodeBehind="addProject.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript">

        //        var operacion = '<%= Request.QueryString("op") %>';

        //        if (operacion != "add") {
        //            ideditar = '<%= Request.QueryString("id") %>';
        //        }
        //   
    </script>

    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script src="../Include/javascript/mdfu.js" type="text/javascript"></script>

    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <link href="../css/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script src="../Include/javascript/proyecto_file.js" type="text/javascript"></script>

    <script src="../Include/javascript/proyecto_flujos.js" type="text/javascript"></script>

    <script src="../Include/javascript/proyecto_actores.js" type="text/javascript"></script>

    <script src="../Include/javascript/Proyecto_ubicacion.js" type="text/javascript"></script>

    <script src="../Include/javascript/proyecto_inf_prin.js" type="text/javascript"></script>

    <script src="../Include/javascript/F_globales_MGroup.js" type="text/javascript"></script>

    <script src="../Include/javascript/Proyecto_componentes.js" type="text/javascript"></script>

    <script src="../Include/javascript/Proyecto_validaciones.js" type="text/javascript"></script>
    
    <script src="../Include/javascript/numeral.min.js"></script>

    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.fileupload.js" type="text/javascript"></script>

    <script src="../js/jquery.iframe-transport.js" type="text/javascript"></script>

    <script src="../Include/javascript/Project.js" type="text/javascript"></script>

    <%-- <script src="../Include/javascript/charge_textfield_project.js" type="text/javascript"></script>--%>

    <script type="text/javascript" src="../Include/javascript/numeral.min.js"></script>

    <script type="text/javascript">


        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1))
                    return false;
            } catch (e) {
            }
        }

        //Función que permite solo Números
        function ValidaSoloNumeros() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                event.returnValue = false;
        }

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
    </script>

    <br />
    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="../images/save_icon.png" width="24px" alt="Save" /><asp:Label
            ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label></div>
    <div id="containererrors" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 120px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="../images/save_icon.png" width="24px" alt="Save" /><asp:Label
            ID="Lblerrors_save_idea" runat="server" Style="font-size: 14pt; color: #990000;"></asp:Label></div>
    <div id="container_wait" runat="server" visible="true" style="width: 50%; text-align: center;
        border: 15px solid #cecece; background: #E8E8DC; height: 200px; line-height: 50px;
        vertical-align: middle; z-index: 1000; position: absolute; left: 25%; border-radius: 40px;">
        <img style="margin-top: 15px;" src="../images/charge_emerging.gif" width="120px"
            alt="images" />
        <asp:Label ID="Label1" runat="server" Text="Cargando información espere un momento..."
            Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <div id="tabsproyecto">
        <ul>
            <li><a href="#componentes">Planeación Estratégica</a></li>
            <li><a href="#informacion">Descripción del proyecto</a></li>
            <li><a href="#ubicacion">Ubicación</a></li>
            <li><a href="#actores">Actores</a></li>
            <li><a href="#flujos">Flujos de pago</a></li>
            <li><a href="#anexos">Archivos Anexos</a></li>
        </ul>
        <div id="informacion">
            <ul>
                <li>
                    <div id="container_date_mother" runat="server" visible="true" style="width: 100%;
                        text-align: center; border: 3px solid #9bbb58; background: #E8E8DC; height: 70px;
                        vertical-align: middle; margin-top: 10px; border-radius: 15px;">
                        <ul id="mother_date" style="margin-top: 5px;">
                            <li>
                                <asp:Label ID="Lblvalor_mother" runat="server" Text="Valor total Madre"></asp:Label>
                                <asp:TextBox ID="Txtvalor_mother" runat="server" Width="150px" MaxLength="100" Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Llbvalor_disponible" runat="server" Text=" Valor Disponible"></asp:Label>
                                <asp:TextBox ID="Txtvalor_disponible" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Lbldate_start_mother" runat="server" Text=" Fecha Inicial Proyecto Madre"></asp:Label>
                                <asp:TextBox ID="Txtdate_start_mother" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Lbldate_end_mother" runat="server" Text=" Fecha final Proyecto Madre"></asp:Label>
                                <asp:TextBox ID="Txtdate_end_mother" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
            <br />
            <ul>
                <li id="li_C_idea">
                    <asp:Label ID="lblididea" runat="server" Text="Idea"></asp:Label>
                    <select id="ddlididea" class="Ccombo">
                    </select>
                    <asp:Label ID="lblHelpididea" runat="server"></asp:Label></li>
            </ul>
            <ul class="left">
                <li visible="false">
                    <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="8000"></asp:TextBox>
                    <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcode" runat="server" Text="Código" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="8000" Visible="False"
                        AutoPostBack="true"></asp:TextBox>
                    <asp:Label ID="lblHelpcode" Visible="False" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="100" Height="50px"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpname" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbljustification" runat="server" Text="Justificación"></asp:Label>
                    <asp:TextBox ID="txtjustification" runat="server" Width="400px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpjustification" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblobjective" runat="server" Text="Objetivo"></asp:Label>
                    <asp:TextBox ID="txtobjective" runat="server" Width="400px" MaxLength="8000" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpobjective" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblzonedescription" runat="server" Text="Objetivos específicos"></asp:Label>
                    <asp:TextBox ID="txtareadescription" runat="server" MaxLength="8000" Width="450px"
                        Rows="6" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="lblHelpareadescription" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblresults" runat="server" Text="Resultados Beneficiarios"></asp:Label>
                    <asp:TextBox ID="txtresults" runat="server" Width="400px" MaxLength="8000" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="lblHelpresults" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblresulgc" runat="server" Text="Resultados Gestión del Conocimiento"></asp:Label>
                    <asp:TextBox ID="txtresulgc" runat="server" Width="400px" MaxLength="8000" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblresultci" runat="server" Text="Resultados Capacidad Instalada"></asp:Label>
                    <asp:TextBox ID="txtresulci" runat="server" Width="400px" MaxLength="8000" Height="100px"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblothersresults" runat="server" Text="Otros Resultados"></asp:Label>
                    <asp:TextBox ID="Txtothersresults" runat="server" MaxLength="2500" Width="450px"
                        Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label23" runat="server" ForeColor="#990000"></asp:Label>
                </li>
            </ul>
            <ul class="right">
                <li>
                    <asp:Label ID="Lblobligationsoftheparties" runat="server" Text="Obligaciones de las partes"></asp:Label>
                    <asp:TextBox ID="Txtobligationsoftheparties" runat="server" MaxLength="2500" Width="400px"
                        Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Lblhelpobligationsoftheparties" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblroutepresupuestal" runat="server" Text="Ruta presupuestal"></asp:Label>
                    <asp:TextBox ID="Txtroutepresupuestal" runat="server" Width="400px" Rows="6" TextMode="MultiLine"
                        Height="40px"></asp:TextBox>
                    <asp:Label ID="Lblhelproutepresupuestal" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblriesgos" runat="server" Text=" Riesgos identificados"></asp:Label>
                    <asp:TextBox ID="Txtriesgos" runat="server" MaxLength="2500" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Lblhelpriesgos" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblaccionmitig" runat="server" Text="Mitigación del riesgo"></asp:Label>
                    <asp:TextBox ID="Txtaccionmitig" runat="server" MaxLength="2500" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Lblhelpaccionmitig" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblstartdate" runat="server" Text="Fecha de inicio"></asp:Label>
                    <asp:TextBox ID="txtstartdate" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                    <cc1:CalendarExtender ID="cestartdate" runat="server" Enabled="true" Format="yyyy/MM/dd"
                        TargetControlID="txtstartdate">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblHelpstartdate" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lbltitleduracion" runat="server" Text="Duración"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblduration" runat="server" Text="Meses"></asp:Label>
                    <asp:TextBox ID="txtduration" runat="server" MaxLength="5" Width="100px" Rows="2"
                        onkeypress="ValidaSoloNumeros()" onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                    <asp:Label ID="Lblhelpduraton" runat="server" ForeColor="#990000"></asp:Label>
                    <asp:Label ID="Lbldia" runat="server" Text="Días"></asp:Label>
                    <asp:TextBox ID="Txtday" runat="server" MaxLength="5" Width="100px" Rows="2" onkeypress="ValidaSoloNumeros()"
                        onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                    <asp:Label ID="Lblhelpday" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lbldateend" runat="server" Text="Fecha de Finalización"></asp:Label>
                    <asp:TextBox ID="Txtdatecierre" runat="server" MaxLength="255" Width="100px" Rows="2"
                        Enabled="False"></asp:TextBox>
                    <asp:Label ID="Lblhelpenddate" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li id="liproyecttype" runat="server" visible="false">
                    <asp:Label ID="Lbltype_project" runat="server" Text="Tipo de proyecto"></asp:Label>
                    <select id="ddltype_proyect" class="Ccombo">
                    </select>
                    <asp:Label ID="Lblhelptproyect" runat="server" ForeColor="#990000"></asp:Label></li>
                <li>
                    <asp:Label ID="lblpopulation" runat="server" Text="Población"></asp:Label>
                    <select id="ddlPupulation" class="Ccombo">
                    </select>
                    <asp:Label ID="lblHelppopulation" runat="server" ForeColor="#990000"></asp:Label></li>
                <li>
                    <asp:Label ID="Label13" runat="server" Text="Modalidad de Contratación"></asp:Label>
                    <select id="ddlmodcontract" class="Ccombo">
                    </select>
                    <asp:Label ID="Lblmodcontract" runat="server" ForeColor="#990000"></asp:Label></li>
                <li id="li_estado">
                    <asp:Label ID="Lblestado" runat="server" Text="Estado del proyecto"></asp:Label>
                    <select id="dll_estado" class="Ccombo">
                    </select>
                    <asp:Label ID="Lblhelp_estado" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label21" runat="server" Text="¿El IVA esta incluido en el valor total?"></asp:Label>
                    <asp:RadioButtonList ID="RBnList_iva" runat="server" Height="53px" RepeatDirection="Horizontal"
                        ValidationGroup="iva" Width="86px">
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="Lblhelpiva" runat="server" ForeColor="#990000"></asp:Label>
                </li>
            </ul>
            <ul>
                <div id="T_matrizcontainer">
                    <table id="matriz" border="1" cellpadding="1" cellspacing="1" style="width: 100%">
                        <thead>
                            <tr>
                                <th>
                                </th>
                                <th>
                                </th>
                                <th>
                                    Efectivo Efectivo
                                </th>
                                <th>
                                    Especie
                                </th>
                                <th>
                                    Total
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
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ul>
            <ul>
                <li id="li3333" runat="server" visible="False">
                    <asp:Label ID="lblMessageValidacionNombre" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"
                        ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlididea"
                        ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvcode" Visible="False" runat="server" ControlToValidate="txtcode"
                        ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblantecedent" Visible="False" runat="server" Text="Antecedente"></asp:Label>
                    <asp:TextBox ID="txtantecedent" Visible="False" runat="server" Width="450px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,800)"></asp:TextBox>
                    <asp:Label ID="lblHelpantecedent" Visible="False" runat="server"></asp:Label>
                </li>
                <li id="li5" runat="server" visible="False">
                    <asp:Label ID="lblstrategicdescription" Visible="False" runat="server" Text="Descripción de la estrategia"></asp:Label>
                    <asp:TextBox ID="txtstrategicdescription" Visible="False" runat="server" Width="450px"
                        MaxLength="800" Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="lblHelpstrategicdescription" Visible="False" runat="server"></asp:Label>
                </li>
                <li id="li6" runat="server" visible="False">
                    <asp:Label ID="lblpurpose" runat="server" Visible="False" Text="Finalidad"></asp:Label>
                    <asp:TextBox ID="txtpurpose" runat="server" Visible="False" Width="450px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,800)"></asp:TextBox>
                    <asp:Label ID="lblHelppurpose" Visible="False" runat="server"></asp:Label>
                </li>
                <li id="li7" runat="server" visible="False">
                    <asp:Label ID="lblfsccontribution" runat="server" Text="Aporte FSC"></asp:Label>
                    <asp:TextBox ID="txtfsccontribution" runat="server" Width="246px" MaxLength="25"
                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                    <asp:Label ID="lblHelpfsccontribution" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblcounterpartvalue" runat="server" Text="Valor contrapartida"></asp:Label>
                    <asp:TextBox ID="txtcounterpartvalue" runat="server" Width="244px" MaxLength="25"
                        ValidationGroup="generalProject" onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                    <asp:Label ID="lblHelpcounterpartvalue" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lbltotalcost" runat="server" Text="Valor total"></asp:Label>
                    <asp:Label ID="txttotalcost" runat="server" Width="150px" MaxLength="15" onkeyup="format(this);"
                        onchange="format(this);"></asp:Label>
                    <asp:Label ID="lblHelptotalcost" runat="server"></asp:Label>
                    <asp:Label ID="lbleffectivebudget" runat="server" Text="Vigencia presupuestal"></asp:Label>
                    <asp:DropDownList ID="ddleffectivebudget" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddleffectivebudget"
                        ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpeffectivebudget" runat="server"></asp:Label>
                </li>
                <li id="li8" runat="server" visible="False">
                    <asp:Label ID="lblattachment" runat="server" Text="Archivo anexo" Visible="False"></asp:Label>
                    <asp:FileUpload ID="fuattachment" runat="server" Visible="False" />
                    &nbsp;<asp:HyperLink ID="hlattachment" runat="server" Visible="False" Target="_blank">Descargar</asp:HyperLink><asp:Label
                        ID="lblHelpattachment" runat="server" Visible="False"></asp:Label><asp:Label ID="Label14"
                            runat="server" Text="Aprobación"></asp:Label><asp:DropDownList ID="ddltipoaprobacion"
                                runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlpopulation"
                        ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                    <asp:Label ID="lblidphase" runat="server" Text="Fase"></asp:Label>
                    <asp:DropDownList ID="ddlidphase" runat="server">
                        <asp:ListItem Value="1">Formulación</asp:ListItem>
                        <asp:ListItem Value="2">Planeación</asp:ListItem>
                        <asp:ListItem Value="3">Ejecución</asp:ListItem>
                        <asp:ListItem Value="4">Evaluación</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Visible="False" runat="server"
                        ControlToValidate="ddlidphase" ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblHelpidphase" Visible="False" runat="server"></asp:Label>
                </li>
                <li id="li1" runat="server" visible="False">
                    <asp:Label ID="lblenabled" runat="server" Text="Estado" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtenabled" runat="server" ReadOnly="True" Width="200px" Visible="False"></asp:TextBox>
                    <asp:Label ID="lblHelpenabled" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblmodifotrosi" runat="server" Text="Modificacion otro si"></asp:Label>
                    <asp:CheckBox ID="checkvalor" runat="server" Text="valor" />
                    <asp:CheckBox ID="checktiempo" runat="server" Text="tiempo" />
                    <asp:CheckBox ID="checkalcance" runat="server" Text="alcance" />
                    <asp:Label Visible="False" ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>
                </li>
                <li></li>
                <li>
                    <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                    <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                </li>
                <li>
                    <input id="SaveProject" type="button" value="Crear proyecto" name="Save_Project"
                        onclick="return SaveProject_onclick()" />
                    <a id="Export" href="#" onclick="Export_onclick();" target="_blank" style="height: 2em;">
                        Exportar términos de referencia</a>
                    <asp:Button ID="btntermsreference" runat="server" Text="Exportar términos de referencia"
                        ValidationGroup="infoGenral" Visible="false" />
                    <asp:Button ID="btnAddData" runat="server" Text="Crear proyecto" ValidationGroup="infoGenral"
                        Visible="false" />
                    <asp:Button ID="btnSave" runat="server" Text="Guardar cambios" ValidationGroup="infoGenral"
                        Visible="false" />
                </li>
            </ul>
        </div>
        <div id="componentes">
            <ul class="left">
                <li>
                    <asp:Label ID="Label18" runat="server" Text="Línea Estratégica "></asp:Label>
                    <select id="ddlStrategicLines" class="Ccombo">
                    </select>
                    <asp:Label ID="lblinfls" runat="server" ForeColor="#990000"></asp:Label></li>
            </ul>
            <ul class="right">
                <li>
                    <asp:Label ID="Label19" runat="server" Text="Objetivo estratégico"></asp:Label>
                    <select id="ddlPrograms" class="Ccombo">
                            <option value="">Seleccione...</option>
                    </select>
                    <asp:Label ID="lblinpro" runat="server" ForeColor="#990000"></asp:Label></li>
            </ul>
            <table style="margin: 0 auto;">
                <tr>
                    <td>
                        <asp:Label ID="Lbltitlecomponet" runat="server" Text="Componentes"></asp:Label>
                        <ul id="seleccionarcomponente"></ul>
                    </td>
                    <td style="width: 100px; vertical-align: middle;">
                        <input id="Btnaddcomponent" type="button" value=">>" name="Add_Componente" onclick="return Btnaddcomponent_onclick()" /><input
                            id="Btndeletecomponent" type="button" value="<<" name="Delete_Componente" onclick="return Btndeletecomponent_onclick()" />
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Componentes seleccionados"></asp:Label>
                        <ul id="componentesseleccionados"></ul>
                    </td>
                    <td id="viejocomp" runat="server" visible="false">
                        <cc2:DoubleListBox ID="dlbActivity" runat="server" Width="100%" />
                    </td>
                </tr>
            </table>
            <ul>
                <li>
                    <asp:Label ID="Lblinformationcomponent" runat="server" ForeColor="#990000"></asp:Label>
                </li>
            </ul>
        </div>
        <div id="ubicacion">
            <ul>
                <li>
                    <asp:Label ID="lbiddepto" runat="server" Text="Departamento"></asp:Label>
                    <select id="ddlDepto" class="Ccombo">
                    </select>
                </li>
                <br />
                <li>
                    <asp:Label ID="lbidcity" runat="server" Text="Municipio"></asp:Label>
                    <select id="ddlCity" class="Ccombo">
                                <option value="">Seleccione...</option>
                    </select>
                </li>
            </ul>
            <br />
            <ul>
                <li>
                    <asp:Button ID="btnAgregarubicacion" runat="server" Visible="false" CausesValidation="False"
                        Text="Agregar Ubicación" />
                    <input id="B_add_location" type="button" value="Agregar Ubicación" name="Add_location"
                        onclick="Add_location_onclick()" /></li>
                <li>
                    <asp:Label ID="Lblinfubicacion" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="LblubicacionRep" runat="server" ForeColor="#990000"></asp:Label>
                </li>
            </ul>
            <br />
            <div id="T_locationContainer">
                <table id="T_location" border="2" cellpadding="2" cellspacing="2" style="width: 100%;">
                    <thead>
                        <tr>
                            <th>
                                <span><strong style="font-size: large">Departamento&nbsp;</strong></span>
                            </th>
                            <th>
                                <span><strong style="font-size: large">Ciudad&nbsp;</strong></span>
                            </th>
                            <th>
                                <span><strong style="font-size: large">Eliminar</strong></span>
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
            </ul>
        </div>
        <div id="actores">
            <br />
            <ul>
                <li>
                    <asp:Label ID="Label22" runat="server" Text="Actor"></asp:Label>
                </li>
                <li>
                    <select id="ddlactors" class="Ccombo">
                    </select>
                    <a id="linkactors" runat="server" href="~/GeneralPlanning/addThird.aspx?prety=1&op=add&iframe=true&width=100%&height=100%"
                        title="Nuevo actor" class="pretty" style="height: 2em;">CREAR NUEVO ACTOR</a>
                </li>
                <li>
                    <asp:Label ID="Label53" runat="server" Text="Tipo"></asp:Label>
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="Ccombo">
                        <asp:ListItem>Operador</asp:ListItem>
                        <asp:ListItem>Socio Operador</asp:ListItem>
                        <asp:ListItem>Socio</asp:ListItem>
                        <asp:ListItem>Cliente</asp:ListItem>
                        <asp:ListItem>Contratante</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="li111" runat="server" visible="False">
                    <asp:Label ID="Label24" runat="server" Text="Aporte o rol"></asp:Label>
                    <asp:TextBox ID="txtActions" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li id="li112" runat="server" visible="False">
                    <asp:Label ID="Label25" runat="server" Text="Experiencias"></asp:Label>
                    <asp:TextBox ID="txtExperiences" runat="server" Width="500px" MaxLength="1500" Rows="3"
                        TextMode="MultiLine"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label26" runat="server" Text="Contacto"></asp:Label>
                    <asp:TextBox ID="Txtcontact" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label27" runat="server" Text="C.C"></asp:Label>
                    <asp:TextBox ID="Txtcedulacont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label28" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="Txttelcont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label29" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="Txtemail" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="vrdiner" runat="server" Text="Vr Dinero"></asp:Label>
                    <asp:TextBox ID="Txtvrdiner" runat="server" Width="200px" MaxLength="22" Rows="3"
                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                    <asp:Label ID="Lblhelpdinner" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label31" runat="server" Text="Vr Especie"></asp:Label>
                    <asp:TextBox ID="Txtvresp" runat="server" Width="200px" MaxLength="22" Rows="3" onkeyup="format(this)"
                        onchange="format(this)"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="Label32" runat="server" Text="Total Aporte del Actor"></asp:Label>
                    <asp:TextBox ID="Txtaportfscocomp" runat="server" Width="200px" MaxLength="30" Rows="3"
                        Enabled="False"></asp:TextBox>
                </li>
                <li>
                    <asp:Label ID="LblinformationFlujo" runat="server" Text="Requiere flujo de pago:"></asp:Label>
                    <asp:RadioButtonList ID="RBListflujo" runat="server" Height="53px" RepeatDirection="Horizontal"
                        ValidationGroup="flujo" Width="86px">
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="Lblflujosinf" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Button ID="btnAddThird" Visible="false" runat="server" Text="Agregar Actor"
                        ValidationGroup="thirdBYIdea" />
                    <input id="BtnaddActors" type="button" value="Agregar Actor" name="Add_actors" onclick="return BtnaddActors_onclick()" /><asp:Label
                        ID="lblavertenactors" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                        ForeColor="Red"></asp:Label></li>
                <li>
                    <asp:Label ID="Lblactorrep" runat="server" ForeColor="#990000"></asp:Label>
                </li>
            </ul>
            <br />
            <div id="container_date_mother_actores" runat="server" visible="true" style="width: 100%;
                text-align: center; border: 3px solid #9bbb58; background: #E8E8DC; height: 70px;
                vertical-align: middle; margin-top: 10px; border-radius: 15px;">
                <ul id="mother_date_actores" style="margin-top: 5px; display: inline-flex; float: left;">
                    <li>
                        <asp:Label ID="Lblvalor_mother_actores" runat="server" Text="Valor total Madre"></asp:Label>
                        <asp:TextBox ID="Txtvalor_mother_actores" runat="server" Width="150px" MaxLength="100"
                            Enabled="False"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Llbvalor_disponible_actores" runat="server" Text=" Valor Disponible"></asp:Label>
                        <asp:TextBox ID="Txtvalor_disponible_actores" runat="server" Width="150px" MaxLength="100"
                            Enabled="False"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Lbldate_start_mother_actores" runat="server" Text=" Fecha Inicial Proyecto Madre"></asp:Label>
                        <asp:TextBox ID="Txtdate_start_mother_actores" runat="server" Width="150px" MaxLength="100"
                            Enabled="False"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Label ID="Lbldate_end_mother_actores" runat="server" Text=" Fecha Final Proyecto Madre"></asp:Label>
                        <asp:TextBox ID="Txtdate_end_mother_actores" runat="server" Width="150px" MaxLength="100"
                            Enabled="False"></asp:TextBox>
                    </li>
                </ul>
            </div>
            <br />
            <div id="sucess_mother_help" runat="server" visible="true" style="width: 100%; text-align: center;
                border: 2px solid #9bbb58; background: #E8E8DC; height: 40px; line-height: 40px;
                vertical-align: middle; border-radius: 15px;">
                <asp:Label ID="Lblmesanje_mother" runat="server" Style="font-size: 14pt; color: #990000;"></asp:Label></div>
            <br />
            <br />
            <div id="T_ActorsContainer">
                <table id="T_Actors" align="center" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                    <thead>
                        <tr>
                            <th>
                            </th>
                            <th>
                                Actores
                            </th>
                            <th>
                                Tipo
                            </th>
                            <th>
                                Contacto
                            </th>
                            <th>
                                Documento Identidad
                            </th>
                            <th>
                                Teléfono
                            </th>
                            <th>
                                Correo electrónico
                            </th>
                            <th>
                                Vr Dinero
                            </th>
                            <th>
                                Vr Especie
                            </th>
                            <th>
                                Vr total
                            </th>
                            <th>
                                Eliminar
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
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
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
                <li id="totales" runat="server" visible="false">
                    <asp:Label ID="Label33" runat="server" Text=" Totales"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub1" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Textsub2" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                    <asp:Label Style="text-align: center;" ID="Txtsub3" runat="server" Width="105px"
                        BorderStyle="None"></asp:Label>
                </li>
            </ul>
        </div>
        <div id="flujos">
            <ul>
                <li>
                    <div id="container_date_mother_flujos" runat="server" visible="true" style="width: 100%;
                        text-align: center; border: 3px solid #9bbb58; background: #E8E8DC; height: 70px;
                        vertical-align: middle; margin-top: 10px; border-radius: 15px;">
                        <ul id="mother_date_flujos" style="margin-top: 5px;">
                            <li>
                                <asp:Label ID="Lblvalor_mother_flujos" runat="server" Text="Valor total Madre"></asp:Label>
                                <asp:TextBox ID="Txtvalor_mother_flujos" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Llbvalor_disponible_flujos" runat="server" Text=" Valor Disponible"></asp:Label>
                                <asp:TextBox ID="Txtvalor_disponible_flujos" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Lbldate_start_mother_flujos" runat="server" Text=" Fecha Inicial Proyecto Madre"></asp:Label>
                                <asp:TextBox ID="Txtdate_start_mother_flujos" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                            <li>
                                <asp:Label ID="Lbldate_end_mother_flujos" runat="server" Text=" Fecha Final Proyecto Madre"></asp:Label>
                                <asp:TextBox ID="Txtdate_end_mother_flujos" runat="server" Width="150px" MaxLength="100"
                                    Enabled="False"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
            <br />
            <div id="marco_1" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border: 3px solid #9bbb58; background: #E8E8DC; border-radius: 15px;"
                align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lbltitleflujo1" runat="server" Text="PASO 1: Infomación general del desembolso"
                            Font-Bold="True" Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <ul id="listFlujosPagos">
                    <li width="25%">
                        <asp:Label ID="lblvalortotal" runat="server" Text="Pago No"></asp:Label>
                        <asp:TextBox ID="txtvalortotalflow" runat="server" Width="100px" MaxLength="50" onkeychange="ValidaSoloNumeros()"
                            onkeyup="ValidaSoloNumeros()" onkeypress="ValidaSoloNumeros()"></asp:TextBox>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblfechapago" runat="server" Text="Fecha de pago"></asp:Label>
                        <asp:TextBox ID="txtfechapago" runat="server" Width="100px" MaxLength="50"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfechapago"
                            Format="yyyy/MM/dd" Enabled="True">
                        </cc1:CalendarExtender>
                        <asp:Label ID="helpfechapago" runat="server" ForeColor="#990000"></asp:Label>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje"></asp:Label>
                        <asp:TextBox ID="txtporcentaje" runat="server" MaxLength="50" Width="100px"></asp:TextBox>
                        <asp:Label ID="Lblhelpporcentaje" runat="server" ForeColor="#990000" Font-Bold="True"></asp:Label>
                    </li>
                    <li width="25%">
                        <asp:Label ID="lblvalor" runat="server" Text="Valor"></asp:Label>
                        <asp:Label ID="Lbltotalvalor" runat="server"></asp:Label>
                        <asp:TextBox ID="txtvalorpartial" runat="server" MaxLength="50" ReadOnly="true" Width="182px"
                            Visible="false"></asp:TextBox>
                    </li>
                </ul>
                <br />
                <asp:Label Style="clear: both; margin-left: 3em;" ID="lblentregable" runat="server"
                    Text="Entregable"></asp:Label>
                <asp:TextBox ID="txtentregable" runat="server" Height="100px" MaxLength="8000" TextMode="MultiLine"
                    Width="90%" Style="margin-left: 3em; margin-right: 7em;"></asp:TextBox>
                <asp:HiddenField ID="HDvalorpagoflujo" runat="server" />
                <asp:Label ID="Lblinformationexist" runat="server" ForeColor="#990000" Style="margin-left: 3em;
                    text-align: center;" Font-Bold="True"></asp:Label>
            </div>
            <br />
            <div id="marco_2" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border: 3px solid #9bbb58; background: #E8E8DC; border-radius: 15px;"
                align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lbltitleflujo2" runat="server" Text="PASO 2: Ingrese el detalle por cada socio. "
                            Font-Bold="True" Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <div id="T_AflujosContainer" style="margin-left: 3em; margin-right: 3em">
                    <table id="T_Actorsflujos" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="width: 1px">
                                </th>
                                <th>
                                    Aportante
                                </th>
                                <th>
                                    Valor total aporte
                                </th>
                                <th>
                                    Valor por programar
                                </th>
                                <th>
                                    Saldo por programar
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="width: 1px">
                                </td>
                                <td>
                                </td>
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
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lblinformation_flujos" runat="server" ForeColor="#990000"></asp:Label>
                    </li>
                    <li style="margin-left: 3em;">
                        <input id="Btn_add_flujo" type="button" value="Agregar pago" name="Add_flujo" onclick="return Btn_add_flujo_onclick()" /></li>
                </ul>
                <br />
            </div>
            <br />
            <div id="marco_3" style="border-top: solid; border-left: solid; border-right: solid;
                border-bottom: solid; border: 3px solid #9bbb58; background: #E8E8DC; border-radius: 15px;"
                align="left">
                <br />
                <ul>
                    <li style="margin-left: 3em;">
                        <asp:Label ID="Lblpaso3" runat="server" Text="Resumen programación desembolsos" Font-Bold="True"
                            Font-Size="Large"></asp:Label>
                    </li>
                </ul>
                <div id="T_flujosContainer" style="margin-left: 3em; margin-right: 3em">
                    <table id="T_flujos" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="text-align: center;">
                                    No pago
                                </th>
                                <th style="text-align: center;">
                                    Fecha
                                </th>
                                <th style="text-align: center;">
                                    Porcentaje
                                </th>
                                <th style="text-align: center;">
                                    Entregable
                                </th>
                                <th style="text-align: center;">
                                    Valor parcial
                                </th>
                                <th style="text-align: center;">
                                    Editar/Eliminar
                                </th>
                                <th style="text-align: center;">
                                    Detalle
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                                <td style="text-align: center;">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <input style="margin-left: 3em; margin-bottom: 15px;" type="button" id="cancelEdition" value="Cancelar Edición" />
            </div>
        </div>
        <div id="anexos">
            <ul>
                <li id="tableAttachments"></li>
                <li>
                    <input id="fileupload" type="file" name="files[]" /></li>
                <li><a id="lnkAttch" style="cursor: hand" onclick="AddFileInput(F1)" visible="false">
                </a></li>
                <li>
                    <asp:Label ID="LblHELPARCHIVE" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbldecription" runat="server" Text="Observaciones"></asp:Label>
                    <asp:TextBox ID="Txtdecription" runat="server" Height="50px" MaxLength="8000" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </li>
                <li>
                    <input id="Btncharge_file" type="button" value="Adjuntar un archivo" name="Add_files"
                        onclick="subirArchivos()" /></li>
                <li id="li5000" runat="server" visible="false">
                    <asp:Label ID="obser" runat="server" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="txtobser" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                </li>
            </ul>
            <br />
            <div id="gif_charge_Container" runat="server" visible="true" style="width: 100%;
                text-align: center; border: 2px solid #cecece; background: #E8E8DC; height: 80px;
                line-height: 40px; vertical-align: middle;">
                <img style="margin-top: 10px;" src="../images/cargando.gif" width="24px" alt="images" /><asp:Label
                    ID="Label12" runat="server" Text="Subiendo archivos..." Style="font-size: 14pt;
                    color: #9bbb58;"></asp:Label></div>
            <br />
            <div id="tdFileInputs">
                <table id="T_files" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
                    <thead>
                        <tr>
                            <th style="text-align: center;">
                            </th>
                            <th style="text-align: center;">
                                Archivo
                            </th>
                            <th style="text-align: center;">
                                Ver
                            </th>
                            <th style="text-align: center;">
                                Eliminar
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
                            <td>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="Hdvtotalvalue" runat="server" />
    <asp:HiddenField ID="hdididea" runat="server" />
    <asp:HiddenField ID="hdfechainicio" runat="server" />
    <asp:HiddenField ID="HDiva" runat="server" />
    <asp:HiddenField ID="hdfechafinalizacion" runat="server" />
    <asp:HiddenField ID="HiddenFieldFiles" runat="server" />
    <asp:HiddenField ID="HDIDTHIRD" runat="server" />
    <asp:HiddenField ID="HDswich_ubicacion" runat="server" />
    <asp:HiddenField ID="HDNAMETHIRD" runat="server" />
    <asp:HiddenField ID="HiddenFieldPorcentaje" runat="server" />
    <asp:HiddenField ID="HiddenFieldFsc" runat="server" />
    <div id="dialog" title="Desembolso detallado">
        <table id="T_detalle_desembolso" border="1" cellpadding="1" cellspacing="1" style="width: 100%;">
            <thead>
                <tr>
                    <th>
                        No pago
                    </th>
                    <th>
                        Id aportante
                    </th>
                    <th>
                        Aportante
                    </th>
                    <th>
                        desembolso
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
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
        <div>
            <input id="close_dialog" type="button" value="Cerrar X" name="close_dialog" />
        </div>
    </div>
</asp:Content>
