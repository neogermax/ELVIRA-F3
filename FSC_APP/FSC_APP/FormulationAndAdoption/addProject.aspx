<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.addProject"
    Title="addProject" CodeBehind="addProject.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script src="../Include/javascript/mdfu.js" type="text/javascript"></script>

    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <link href="../css/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>

    <script src="../Include/javascript/Proyecto_ubicacion.js" type="text/javascript"></script>
    
    <script src="../Include/javascript/numeral.min.js"></script>
    
    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.fileupload.js" type="text/javascript"></script>

    <script src="../js/jquery.iframe-transport.js" type="text/javascript"></script>

    <script src="../Include/javascript/Project.js" type="text/javascript"></script>

    <script src="../Include/javascript/charge_textfield_project.js" type="text/javascript"></script>

    <script src="../Include/javascript/numeral.min.js"></script>

    <script type="text/javascript" language="javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1))
                    return false;
            } catch (e) {
            }
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

    

    <script type="text/javascript">

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

        //Funci�n que permite solo N�meros
        function ValidaSoloNumeros() {
            if ((event.keyCode < 48) || (event.keyCode > 57))
                event.returnValue = false;
        }

     
    </script>

    <br />
    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <div id="containererrors" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 120px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblerrors_save_idea" runat="server" Style="font-size: 14pt; color: #990000;"></asp:Label>
    </div>
    <div id="tabsproyecto">
        <ul>
            <li><a href="#componentes">Planeaci�n Estrat�gica</a></li>
            <li><a href="#informacion">Descripci�n del proyecto</a></li>
            <li><a href="#ubicacion">Ubicaci�n</a></li>
            <li><a href="#actores">Actores</a></li>
            <li><a href="#flujos">Flujos de pago</a></li>
            <li><a href="#anexos">Archivos Anexos</a></li>
        </ul>
        <div id="informacion">
            <ul>
                <li>
                    <asp:Label ID="lblididea" runat="server" Text="Idea"></asp:Label>
                    <asp:DropDownList ID="ddlididea" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                    <asp:Label ID="lblHelpididea" runat="server"></asp:Label>
                </li>
            </ul>
            <ul class="left">
                <li visible="false">
                    <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                    <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="8000"></asp:TextBox>
                    <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcode" runat="server" Text="C�digo" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtcode" runat="server" Width="487px" MaxLength="8000" Visible="False"
                        AutoPostBack="true"></asp:TextBox>
                    <asp:Label ID="lblHelpcode" Visible="False" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtname" runat="server" Width="450px" MaxLength="100" Height="50px"
                        TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,255)"></asp:TextBox>
                    <asp:Label ID="lblHelpname" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lbljustification" runat="server" Text="Justificaci�n"></asp:Label>
                    <asp:TextBox ID="txtjustification" runat="server" Width="450px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,8000)"></asp:TextBox>
                    <asp:Label ID="lblHelpjustification" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblobjective" runat="server" Text="Objetivo"></asp:Label>
                    <asp:TextBox ID="txtobjective" runat="server" Width="450px" MaxLength="8000" Height="100px"
                        TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,8000)"></asp:TextBox>
                    <asp:Label ID="lblHelpobjective" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblzonedescription" runat="server" Text="Objetivos espec�ficos"></asp:Label>
                    <asp:TextBox ID="txtzonedescription" runat="server" MaxLength="8000" Width="450px"
                        Rows="6" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="lblHelpzonedescription" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblresults" runat="server" Text="Resultados Beneficiarios"></asp:Label>
                    <asp:TextBox ID="txtresults" runat="server" Width="450px" MaxLength="8000" Height="100px"
                        TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="lblHelpresults" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label6" runat="server" Text="Resultados Gesti�n del Conocimiento"></asp:Label>
                    <asp:TextBox ID="TextResultGestConocimiento" runat="server" Width="450px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label8" runat="server" Text="Resultados Capacidad Instalada"></asp:Label>
                    <asp:TextBox ID="TextResCapacidInstal" runat="server" Width="450px" MaxLength="8000"
                        Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                    <asp:Label ID="Label9" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lblothersresults" runat="server" Text="Otros Resultados"></asp:Label>
                    <asp:TextBox ID="Txtothersresults" runat="server" MaxLength="2500" Width="400px"
                        Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Label30" runat="server" ForeColor="#990000"></asp:Label>
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
                    <asp:Label ID="Lblaccionmitig" runat="server" Text="Mitigaci�n del riesgo"></asp:Label>
                    <asp:TextBox ID="Txtaccionmitig" runat="server" MaxLength="2500" Width="400px" Rows="6"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:Label ID="Lblhelpaccionmitig" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblbegindate" runat="server" Text="Fecha de inicio"></asp:Label>
                    <asp:TextBox ID="txtbegindate" runat="server" Width="400px" MaxLength="80"></asp:TextBox>
                    <cc1:CalendarExtender ID="cesbegindate" runat="server" TargetControlID="txtbegindate"
                        Format="yyyy/MM/dd" Enabled="True">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblHelpbegindate" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Lbltitleduracion" runat="server" Text="Duraci�n"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblduration" runat="server" Text="Meses"></asp:Label>
                    <asp:TextBox ID="txtduration" runat="server" MaxLength="5" Width="100px" Rows="2"
                        onkeypress="ValidaSoloNumeros()" onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                    <asp:Label ID="lblHelpduration" runat="server" ForeColor="#990000"></asp:Label>
                    <asp:Label ID="Lbldia" runat="server" Text="D�as"></asp:Label>
                    <asp:TextBox ID="Txtday" runat="server" MaxLength="5" Width="100px" Rows="2" onkeypress="ValidaSoloNumeros()"
                        onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                    <asp:Label ID="Lblhelpday" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label10" runat="server" Text="Finalizaci�n"></asp:Label>
                    <asp:Label ID="TextFinalizacion" runat="server" Width="150px" MaxLength="50"></asp:Label>
                    <asp:Label ID="Label11" runat="server"></asp:Label>
                </li>
                <li id="liproyecttype" runat="server" visible="false">
                    <asp:Label ID="Lbltype_project" runat="server" Text="Tipo de proyecto"> </asp:Label>
                    <select id="ddltype_proyect" class="Ccombo">
                        <asp:DropDownList ID="ddltype_proyect" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="Lblhelptproyect" runat="server" ForeColor="#990000"></asp:Label>
                    </select>
                </li>
                <li>
                    <asp:Label ID="lblpopulation" runat="server" Text="Poblaci�n"></asp:Label>
                    <select id="ddlpopulation" class="Ccombo">
                        <asp:DropDownList ID="ddlpopulation" runat="server">
                        </asp:DropDownList>
                    </select>
                    <asp:Label ID="lblHelppopulation" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label13" runat="server" Text="Modalidad de Contrataci�n"></asp:Label>
                    <select id="ddlmodcontract" class="Ccombo">
                        <asp:DropDownList ID="ddlmodcontract" runat="server">
                        </asp:DropDownList>
                    </select>
                    <asp:Label ID="Lblmodcontract" runat="server" ForeColor="#990000"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="Label21" runat="server" Text="�El IVA esta incluido en el valor total?"></asp:Label>
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
                                    Efectivo
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
                    <asp:Label ID="lblstrategicdescription" Visible="False" runat="server" Text="Descripci�n de la estrategia"></asp:Label>
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
                    &nbsp;<asp:HyperLink ID="hlattachment" runat="server" Visible="False" Target="_blank">Descargar</asp:HyperLink>
                    <asp:Label ID="lblHelpattachment" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="Label14" runat="server" Text="Aprobaci�n"></asp:Label>
                    <asp:DropDownList ID="ddltipoaprobacion" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlpopulation"
                        ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                    <asp:Label ID="Label15" runat="server"></asp:Label>
                    <asp:Label ID="lblidphase" runat="server" Text="Fase"></asp:Label>
                    <asp:DropDownList ID="ddlidphase" runat="server">
                        <asp:ListItem Value="1">Formulaci�n</asp:ListItem>
                        <asp:ListItem Value="2">Planeaci�n</asp:ListItem>
                        <asp:ListItem Value="3">Ejecuci�n</asp:ListItem>
                        <asp:ListItem Value="4">Evaluaci�n</asp:ListItem>
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
                    <asp:GridView Visible="False" ID="gvVersion" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                            <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                            <asp:BoundField DataField="code" HeaderText="Codigo" />
                            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addProject.aspx?op=show&amp;id={0}&consultLastVersion=false"
                                DataTextField="Name" HeaderText="Nombre" Target="_blank" />
                        </Columns>
                    </asp:GridView>
                </li>
                <li></li>
                <li>
                    <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                    <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creaci�n"></asp:Label>
                    <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                </li>
                <li>
                    <asp:Button ID="btntermsreference" runat="server" Text="Exportar t�rminos de referencia"
                        ValidationGroup="infoGenral" />
                    <asp:Button ID="btnAddData" runat="server" Text="Crear proyecto" ValidationGroup="infoGenral" />
                    <asp:Button ID="btnSave" runat="server" Text="Guardar cambios" ValidationGroup="infoGenral" />
                </li>
            </ul>
        </div>
        <div id="componentes">
        </div>
        <div id="ubicacion">
            <ul>
                <li>
                    <asp:Label ID="lbiddepto" runat="server" Text="Departamento"></asp:Label>
                    <select id="ddlDepto" class="Ccombo">
                        <asp:DropDownList ID="ddlDepto" runat="server">
                        </asp:DropDownList>
                    </select>
                </li>
                <br />
                <li>
                    <asp:Label ID="lbidcity" runat="server" Text="Municipio"></asp:Label>
                    <select id="ddlCity" class="Ccombo">
                        <asp:DropDownList ID="ddlCity" runat="server">
                        </asp:DropDownList>
                    </select>
                </li>
            </ul>
            <br />
            <ul>
                <li>
                    <asp:Button ID="btnAgregarubicacion" runat="server" Visible="false" CausesValidation="False"
                        Text="Agregar Ubicaci�n" />
                    <input id="B_add_location" type="button" value="Agregar Ubicaci�n" name="Add_location"
                        onclick="Add_location_onclick()" />
                </li>
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
                
            </ul>
        </div>
        <div id="actores">
        </div>
        <div id="flujos">
        </div>
        <div id="anexos">
        </div>
    </div>
    <%--<asp:Label ID="LabelErrorGeneral" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
        ForeColor="Red"></asp:Label>
    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblstatesuccess" runat="server" Style="color: #9bbb58; font-size: 14pt;"
            Text="El Proyecto se cre� exitosamente!"></asp:Label>
    </div>
--%>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0"
        AutoPostBack="True">
        <cc1:TabPanel runat="server" HeaderText="Informaci�n principal" ID="TabPanel1" Width="90%">
            <ContentTemplate>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Lista de fuentes" ID="TabPanel6" Enabled="False"
            Width="600px">
            <HeaderTemplate>
                Lista de fuentes
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="upSourceByProject" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Fuente:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSource" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvSource" runat="server" ControlToValidate="ddlSource"
                                                    ErrorMessage="*" ValidationGroup="sourceByProject"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnAddSource" runat="server" Text="Agregar Fuente" ValidationGroup="sourceByProject" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSourceMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvSourceByProject" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    Width="294px">
                                                    <Columns>
                                                        <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                        <asp:BoundField DataField="SOURCENAME" HeaderText="Nombre de la fuente" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Ubicaciones" ID="TabPanel2" Width="90%">
            <HeaderTemplate>
                Ubicaciones
            </HeaderTemplate>
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="upProjectLocation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 90%">
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:Label ID="x1" runat="server" Text="Departamento"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddliddepto" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHelpiddepto" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px">
                                                    <asp:Label ID="x2" runat="server" Text="Ciudad"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidcity" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvidcity" runat="server" ControlToValidate="ddlidcity"
                                                        ErrorMessage="*" ValidationGroup="projectLocation"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHelidcity" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Button ID="btnAddProjectLocation" runat="server" Text="Guardar Ubicaci�n" ValidationGroup="projectLocation" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 90%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvprojectLocation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <%--    cambios de forma del grid para leer los datos para los terminos de referencia
                                                        autor:german rodriguez --%>
                                                        <Columns>
                                                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                            <asp:BoundField DataField="IDCITY" HeaderText="Departamento" />
                                                            <asp:BoundField DataField="DEPTONAME" HeaderText="Departamento" />
                                                            <asp:BoundField DataField="CITYNAME" HeaderText="Ciudad" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHelpprojectLocation" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Flujos de pago" ID="TabPanel7" Width="90%">
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 90%">
                                <tr>
                                    <td>
                                        <table style="width: 90%">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text='Recuerde hacer click en "Agregar Pago" para efectuar los cambios'
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblvalortotal" runat="server" Text="Valor Total"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvalortotalflow" runat="server" Width="186px" MaxLength="50" Text="0"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblfechapago" runat="server" Text="Fecha de pago"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtfechapago" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cesfechapago" runat="server" TargetControlID="txtfechapago"
                                                        Format="yyyy/MM/dd" Enabled="True">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfechapago"
                                                        ErrorMessage="el campo fecha esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblhelpfechapago" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblporcentaje" runat="server" Text="Porcentaje"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtporcentaje" Text="0" runat="server" MaxLength="50" Width="95px"></asp:TextBox>
                                                    <asp:Label ID="lblFlowNfo" runat="server" Text="." ForeColor="White"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPorcent" runat="server" ControlToValidate="txtporcentaje"
                                                        ErrorMessage="el campo esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblvalor" runat="server" Text="Valor"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtvalorpartial" runat="server" MaxLength="50" ReadOnly="true" Width="182px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblentregable" runat="server" Text="Entregable"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtentregable" runat="server" Height="100px" MaxLength="8000" onkeypress="return textboxMultilineMaxNumber(this,800)"
                                                            TextMode="MultiLine" Width="450px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtentregable"
                                                            ErrorMessage="el campo esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Button ID="BtnAddPayment" runat="server" Text="Agregar Pago" />
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblmsjporcent" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td>
                                        <%--<asp:Label ID="Label16" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                            ForeColor="Red"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFlujodePagoPorcentajeIzquierda" runat="server" Font-Bold="True"
                                            Font-Names="Arial Narrow" Font-Size="1.2em" ForeColor="Red"></asp:Label>
                                        <asp:Label ID="lblmensajeexitoerror" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                            ForeColor="Red" Font-Size="1.2em"></asp:Label>
                                        <asp:Label ID="LblFlujodePagoPorcentajeDerecha" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                            Font-Size="1.2em" ForeColor="Red"></asp:Label>
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblExceed100" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                                Font-Size="1.2em" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Actores" ID="TabPanel4" Width="90%">
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="upoperatorbyproject" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table style="width: 90%">
                                <tr>
                                    <td>
                                        <table style="width: 90%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblOperator" runat="server" Text="Actor"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlidoperator" runat="server" CssClass="Ccombo">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblHelpidoperator" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbltipooperador" runat="server" Text="Tipo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddltipooperador" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label17" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Button ID="btnaddoperatorbyproject" runat="server" Text="Agregar actor" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 90%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvoperatorbyproject" AutoGenerateColumns="False" AllowPaging="True"
                                                        runat="server">
                                                        <Columns>
                                                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                            <asp:TemplateField HeaderText="id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdactor" runat="server" Text='<%# Eval("idthird") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltype" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actores">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIname" runat="server" Text='<%# Eval("name") %>'></asp:Label>
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
                                                            <asp:TemplateField HeaderText="Tel�fono">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblphone" runat="server" Text='<%# Eval("phone") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Correo electr�nico">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblemail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHelpoperatorbyproject" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="thirdtable">
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Componentes del Programa" ID="TabPanelCompProgramas"
            Width="90%">
            <ContentTemplate>
                <div id="gridComponentesPrograma">
                </div>
                <div id="lblcomponentesprograma" runat="server">
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Aclaratorio" ID="tbpnAclaratorio" Width="90%">
            <ContentTemplate>
                <label>
                    Observaciones
                </label>
                <asp:TextBox ID="txtaclaratorio" runat="server" TextMode="MultiLine" Height="50px"
                    MaxLength="8000">
                </asp:TextBox>
                <table style="width: 90%">
                    <tr>
                        <td style="height: 30px; vertical-align: middle;">
                            <asp:Label ID="Label4" runat="server" Text="L�nea Estrat�gica"></asp:Label>
                            <asp:Label ID="txtlinestrat" runat="server" Width="400px" AutoPostBack="True"></asp:Label>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="17%" style="height: 30px; vertical-align: middle;">
                            <asp:Label ID="Label2" runat="server" Text="Programa"></asp:Label>
                            <asp:Label ID="txtprograma" Width="300px" runat="server" AutoPostBack="True"></asp:Label>
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridViewAclaratorio" AutoGenerateColumns="False" AllowPaging="True"
                                runat="server">
                                <Columns>
                                    <asp:BoundField DataField="observation" HeaderText="Observacion">
                                        <ItemStyle Width="400"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Anexos" ID="TabPanelAnexos" Width="90%">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td nowrap="nowrap">
                                <img src="../App_Themes/GattacaAdmin/Images/attach.gif" alt="" />
                                <a id="lnkAttch" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'"
                                    style="cursor: hand" onclick="AddFileInputProject()">Adjuntar un archivo</a>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="label12" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdFileInputs" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnadanexo" runat="server" Text="Agregar anexo" />
                            </td>
                        </tr>
                        <tr>
                            <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addDocuments.aspx?op=edit&id={0}&isPopup=True"
                                                HeaderText="Edici�n" Text="Editar" Target="_blank" Visible="false" />
                                            <asp:TemplateField HeaderText="Eliminaci�n" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                        OnClientClick="return confirm('Esta seguro?')" Text="Eliminar"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="title" HeaderText="T�tulo" Visible="false" />
                                            <asp:BoundField DataField="description" HeaderText="Descripci�n" Visible="false" />
                                            <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                            <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                            <asp:HyperLinkField DataNavigateUrlFields="attachfile" DataTextField="attachfile"
                                                HeaderText="Anexo" Target="_blank" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Button ID="btnRefresh" runat="server" Text="Actualizar Listado" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </tr>
                        <tr>
                            <td>
                                <td>
                                </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <br />
    <table>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblBPMMessage" runat="server" Text="Por Favor Seleccione la Actividad"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:RadioButtonList ID="rblCondition" runat="server" Visible="False">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                <a id="linkcharge" runat="server" href="~/FormulationAndAdoption/addproyectchargemasive.aspx?iframe=true&width=100%&height=100%"
                    title="Carga Masiva" class="pretty">Cargue Cronograma de Actividades y Subactividadese
                    Actividades y Subactividades</a>
                <%-- 
                                                            <asp:TemplateField HeaderText="Departamento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptoID" runat="server" Text='<%# Eval("idcity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Departamento">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldepto" runat="server" Text='<%# Eval("DEPTONAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ciudad">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcity" runat="server" Text='<%# Eval("CITYNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label><asp:Label ID="lblsaveinformation1" runat="server" Font-Bold="True"
                        Font-Names="Arial Black" ForeColor="#04B404"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdvtotalvalue" runat="server" />
    <asp:HiddenField ID="hdididea" runat="server" />
    <asp:HiddenField ID="hdfechainicio" runat="server" />
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
