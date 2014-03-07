<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false"
    Inherits="FSC_APP.addProject" Title="addProject" Codebehind="addProject.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

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

    <asp:Label ID="LabelErrorGeneral" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
        ForeColor="Red"></asp:Label>
    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblstatesuccess" runat="server" Style="color: #9bbb58; font-size: 14pt;"
            Text="El Proyecto se creó exitosamente!"></asp:Label>
    </div>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="2"
        AutoPostBack="True">
        <cc1:TabPanel runat="server" HeaderText="Información principal" ID="TabPanel1" Width="90%">
            <HeaderTemplate>
                Información principal
            </HeaderTemplate>
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessageValidacionNombre" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="8000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"
                                    ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblididea" runat="server" Text="Idea"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlididea" runat="server" CssClass="Ccombo">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlididea"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpididea" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="Label4" runat="server" Text="Línea Estratégica"></asp:Label>
                            </td>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="txtlinestrat" runat="server" Width="400px" AutoPostBack="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="17%" style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="Label2" runat="server" Text="Programa"></asp:Label>
                            </td>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="txtprograma" Width="300px" runat="server" AutoPostBack="True"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcode" runat="server" Text="Código" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcode" runat="server" Width="487px" MaxLength="8000" Visible="False"
                                    AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvcode" Visible="False" runat="server" ControlToValidate="txtcode"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcode" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtname" runat="server" Width="450px" MaxLength="100" Height="50px"
                                    TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,255)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblantecedent" Visible="False" runat="server" Text="Antecedente"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtantecedent" Visible="False" runat="server" Width="450px" MaxLength="8000"
                                    Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,800)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpantecedent" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbljustification" runat="server" Text="Justificación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtjustification" runat="server" Width="450px" MaxLength="8000"
                                    Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,8000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpjustification" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblobjective" runat="server" Text="Objetivo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtobjective" runat="server" Width="450px" MaxLength="8000" Height="100px"
                                    TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,8000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpobjective" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblzonedescription" runat="server" Text="Objetivos específicos"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtzonedescription" runat="server" MaxLength="8000" Width="450px"
                                    Rows="6" TextMode="MultiLine" 
                                    onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpzonedescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblresults" runat="server" Text="Resultados Beneficiarios"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtresults" runat="server" Width="450px" MaxLength="8000" Height="100px"
                                    TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpresults" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Resultados Gestión del Conocimiento"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextResultGestConocimiento" runat="server" Width="450px" MaxLength="8000"
                                    Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Resultados Capacidad Instalada"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextResCapacidInstal" runat="server" Width="450px" MaxLength="8000"
                                    Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblbegindate" runat="server" Text="Fecha de inicio"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtbegindate" runat="server" Width="400px" MaxLength="80"></asp:TextBox>
                                <cc1:CalendarExtender ID="cesbegindate" runat="server" TargetControlID="txtbegindate"
                                    Format="yyyy/MM/dd" Enabled="True">
                                </cc1:CalendarExtender>
                                <asp:CompareValidator ID="cvbegindate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                                    ControlToValidate="txtbegindate" Operator="DataTypeCheck" SetFocusOnError="True"
                                    ValidationGroup="generalProject"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpbegindate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblduration" runat="server" Text="Duración en meses "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtduration" runat="server" Width="400px" MaxLength="5"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpduration" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="Label10" runat="server" Text="Finalización"></asp:Label>
                            </td>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="TextFinalizacion" runat="server" Width="150px" MaxLength="50"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="lblpopulation" runat="server" Text="Población"></asp:Label>
                            </td>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:DropDownList ID="ddlpopulation" runat="server">
                                    <asp:ListItem Value="1">Personas con discapacidad</asp:ListItem>
                                    <asp:ListItem Value="2">Personas Mayores</asp:ListItem>
                                    <asp:ListItem Value="3">Persona con discapacidad y personas mayores</asp:ListItem>
                                    <asp:ListItem Value="4">Otras</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlpopulation"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelppopulation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblstrategicdescription" Visible="False" runat="server" Text="Descripción de la estrategia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtstrategicdescription" Visible="False" runat="server" Width="450px"
                                    MaxLength="800" Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,4000)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpstrategicdescription" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblpurpose" runat="server" Visible="False" Text="Finalidad"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpurpose" runat="server" Visible="False" Width="450px" MaxLength="8000"
                                    Height="100px" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,800)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelppurpose" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;vertical-align: middle;">
                                <asp:Label ID="lblfsccontribution" runat="server" Text="Aporte FSC"></asp:Label>
                            </td>
                            <td style="height: 35px;vertical-align: middle;">
                                <asp:TextBox ID="txtfsccontribution" runat="server" Width="246px" MaxLength="25"
                                    onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpfsccontribution" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcounterpartvalue" runat="server" Text="Valor contrapartida"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcounterpartvalue" runat="server" Width="244px" MaxLength="25"
                                    ValidationGroup="generalProject" onkeyup="format(this)" 
                                    onchange="format(this)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcounterpartvalue" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="lbltotalcost" runat="server" Text="Valor total"></asp:Label>
                            </td>
                            <td style="height: 30px;vertical-align: middle;">
                                <asp:Label ID="txttotalcost" runat="server" Width="150px" MaxLength="15" onkeyup="format(this);"
                                    onchange="format(this);"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblHelptotalcost" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbleffectivebudget" runat="server" Text="Vigencia presupuestal"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddleffectivebudget" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddleffectivebudget"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpeffectivebudget" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblattachment" runat="server" Text="Archivo anexo" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fuattachment" runat="server" Visible="False" />
                                &nbsp;<asp:HyperLink ID="hlattachment" runat="server" Visible="False" Target="_blank">Descargar</asp:HyperLink></td>
                            <td>
                                <asp:Label ID="lblHelpattachment" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="Aprobación"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddltipoaprobacion" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlpopulation"
                                    ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblidphase" runat="server" Text="Fase"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlidphase" runat="server">
                                    <asp:ListItem Value="1">Formulación</asp:ListItem>
                                    <asp:ListItem Value="2">Planeación</asp:ListItem>
                                    <asp:ListItem Value="3">Ejecución</asp:ListItem>
                                    <asp:ListItem Value="4">Evaluación</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Visible="False" runat="server"
                                    ControlToValidate="ddlidphase" ErrorMessage="*" ValidationGroup="generalProject"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpidphase" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblenabled" runat="server" Text="Estado" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtenabled" runat="server" ReadOnly="True" Width="200px" Visible="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpenabled" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblmodifotrosi" runat="server" Text="Modificacion otro si"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="checkvalor" runat="server" Text="valor" />
                                <asp:CheckBox ID="checktiempo" runat="server" Text="tiempo" />
                                <asp:CheckBox ID="checkalcance" runat="server" Text="alcance" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label Visible="False" ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView Visible="False" ID="gvVersion" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                        <asp:BoundField DataField="code" HeaderText="Codigo" />
                                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addProject.aspx?op=show&amp;id={0}&consultLastVersion=false"
                                            DataTextField="Name" HeaderText="Nombre" Target="_blank" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btntermsreference" runat="server" Text="Exportar términos de referencia"
                                    ValidationGroup="infoGenral" />
                                <asp:Button ID="btnAddData" runat="server" Text="Crear proyecto" ValidationGroup="infoGenral" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Guardar cambios" ValidationGroup="infoGenral" />
                            </td>
                        </tr>
                    </table>
                </div>
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
                                                    <asp:Label ID="lbiddepto" runat="server" Text="Departamento"></asp:Label>
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
                                                    <asp:Label ID="lbidcity" runat="server" Text="Ciudad"></asp:Label>
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
                                                    <asp:Button ID="btnAddProjectLocation" runat="server" Text="Guardar Ubicación" ValidationGroup="projectLocation" />
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
                                                    <asp:GridView ID="gvprojectLocation" AutoGenerateColumns="False" AllowPaging="True"
                                                        runat="server" Width="100%">
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
                                                    <asp:TextBox ID="txtvalortotalflow" runat="server" Width="186px" MaxLength="50" 
                                                        Text="0"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtporcentaje" Text="0" runat="server" MaxLength="50" 
                                                        Width="95px"></asp:TextBox>
                                                    <asp:Label ID="lblFlowNfo" runat="server" Text="." ForeColor="White"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPorcent" runat="server" ControlToValidate="txtporcentaje"
                                                        ErrorMessage="el campo esta vacio" ValidationGroup="validat"></asp:RequiredFieldValidator>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblvalor" runat="server" Text="Valor"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtvalorpartial" runat="server" MaxLength="50" ReadOnly="true" 
                                                            Width="182px"></asp:TextBox>
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
                                                HeaderText="Edición" Text="Editar" Target="_blank" Visible="false" />
                                            <asp:TemplateField HeaderText="Eliminación" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                        OnClientClick="return confirm('Esta seguro?')" Text="Eliminar"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="title" HeaderText="Título" Visible="false" />
                                            <asp:BoundField DataField="description" HeaderText="Descripción" Visible="false" />
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
                    title="Carga Masiva" class="pretty">Cargue Cronograma de Actividades y 
                Subactividadese Actividades y Subactividades</a>
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
                    ForeColor="Red"></asp:Label><asp:Label ID="lblsaveinformation" runat="server" Font-Bold="True"
                        Font-Names="Arial Black" ForeColor="#04B404"></asp:Label></td>
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
</asp:Content>
