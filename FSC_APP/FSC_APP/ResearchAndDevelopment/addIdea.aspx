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
    </script>

    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="3">
        <cc1:TabPanel runat="server" ID="TabPanel1" Width="600" TabIndex="0">
            <HeaderTemplate>
                Información Principal
            </HeaderTemplate>
            <ContentTemplate>
                <table>
                    <tr>
                        <td width="10%">
                            <asp:Label ID="Label7" runat="server" Text="Línea Estrátegica "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStrategicLines" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlStrategicLines"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="17%">
                            <asp:Label ID="Label8" runat="server" Text="Programa"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPrograms" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPrograms"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr visible="false">
                        <td style="width: 172px; height: 26px;">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td style="height: 26px">
                            <asp:TextBox ID="txtid" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                        </td>
                        <td style="height: 26px">
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr1" runat="server" visible="False">
                        <td id="Td1" style="width: 172px" runat="server">
                            <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                        </td>
                        <td id="Td2" colspan="2" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtcode" runat="server" MaxLength="50" Width="400px" AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                        ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" MaxLength="255" Width="400px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpname" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblJustificacion" runat="server" Text="Justificación"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtjustification" runat="server" MaxLength="255" Width="400px" Rows="2"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtjustification"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpjustification" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblobjective" runat="server" Text="Objetivo"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtobjective" runat="server" MaxLength="255" Width="400px" Rows="2"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtobjective"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpobjective" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblareadescription" runat="server" Text="Objetivos Específicos"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtareadescription" runat="server" MaxLength="4000" Width="400px"
                                Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpareadescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblresults" runat="server" Text="Resultados en Beneficiarios"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtresults" runat="server" MaxLength="4000" Width="400px" Rows="6"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpresults" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="Lblresulgc" runat="server" Text="Resultados en Gestión del conocimiento"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtresulgc" runat="server" MaxLength="4000" Width="400px" Rows="6"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="Lblresultci" runat="server" Text="Resultados en Capacidad instalada"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtresulci" runat="server" MaxLength="4000" Width="400px" Rows="6"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblstartdate" runat="server" Text="Fecha de inicio"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtstartdate" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="cestartdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                TargetControlID="txtstartdate">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtstartdate"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvstartdate" runat="server" ControlToValidate="txtstartdate"
                                ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpstartdate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblduration" runat="server" Text="Duración en meses"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtduration" runat="server" MaxLength="5" Width="400px" Rows="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtname"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpduration" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="Label21" runat="server" Text="Fecha de Finalización"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txtdatecierre" runat="server" MaxLength="255" Width="197px" Rows="2"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblpopulation" runat="server" Text="Población"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPupulation" runat="server">
                                <asp:ListItem Value="1">Personas con Discapacidad</asp:ListItem>
                                <asp:ListItem Value="2">Personas Mayores</asp:ListItem>
                                <asp:ListItem Value="3">Persona con discapacidad y personas mayores</asp:ListItem>
                                <asp:ListItem Value="4">Otros</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPupulation"
                                ErrorMessage="*" ValidationGroup="infoGenral" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelppopulation" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server" visible="False">
                        <td id="Td3" runat="server">
                            <asp:Label ID="lblfsccontribution" runat="server" Text="Aporte FSC"></asp:Label>
                        </td>
                        <td id="Td4" runat="server">
                            <asp:TextBox ID="txtfsccontribution" runat="server" Width="400px" MaxLength="15"
                                onkeyup="format(this)" onchange="format(this)" Enabled="False"></asp:TextBox>
                        </td>
                        <td id="Td5" runat="server">
                            <asp:Label ID="lblHelpfsccontribution" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr3" runat="server" visible="False">
                        <td id="Td6" runat="server">
                            <asp:Label ID="lblcounterpartvalue" runat="server" Text="Valor contrapartida"></asp:Label>
                        </td>
                        <td id="Td7" runat="server">
                            <asp:TextBox ID="txtcounterpartvalue" runat="server" Width="400px" MaxLength="15"
                                ValidationGroup="generalProject" onkeyup="format(this)" onchange="format(this)"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td id="Td8" runat="server">
                            <asp:Label ID="lblHelpcounterpartvalue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr4">
                        <td id="Td9" style="width: 172px" runat="server">
                            <asp:Label ID="Label19" runat="server" Text="Modalidad de Contratación "></asp:Label>
                        </td>
                        <td id="Td10" runat="server">
                            <asp:DropDownList ID="ddlmodcontract" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td id="Td11" runat="server">
                            <asp:Label ID="Label20" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px height: 30px; vertical-align: middle;">
                            <asp:Label ID="lblcost" runat="server" Text="Valor Total"></asp:Label>
                        </td>
                        <td style="height: 30px; vertical-align: middle;">
                            <asp:Label ID="txtcost" runat="server" Width="400px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcost" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr5" runat="server" visible="False">
                        <td id="Td12" style="width: 172px" runat="server">
                            <asp:Label ID="lblsource" runat="server" Text="Fuente"></asp:Label>
                        </td>
                        <td id="Td13" runat="server">
                            <asp:DropDownList ID="ddlSource" runat="server">
                                <asp:ListItem>Investigación</asp:ListItem>
                                <asp:ListItem>Propuesta de un Socio</asp:ListItem>
                                <asp:ListItem>Propuesta de un Operador</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSource"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="infoGenral"></asp:RequiredFieldValidator>
                        </td>
                        <td id="Td14" runat="server">
                            <asp:Label ID="lblHelpsource" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr6" runat="server" visible="False">
                        <td id="Td15" style="width: 172px" runat="server">
                            <asp:Label ID="lblconvocatory" runat="server" Text="Convocatoria"></asp:Label>
                        </td>
                        <td id="Td16" runat="server">
                            <asp:DropDownList ID="ddlSummoning" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSummoning"
                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="infoGenral"></asp:RequiredFieldValidator>
                        </td>
                        <td id="Td17" runat="server">
                            <asp:Label ID="lblHelpconvocatory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr7" runat="server" visible="False">
                        <td id="Td18" style="width: 172px" runat="server">
                            <asp:Label ID="lblstrategydescription" runat="server" Text="Descripción de la estrategia"></asp:Label>
                        </td>
                        <td id="Td19" runat="server">
                            <asp:TextBox ID="txtstrategydescription" runat="server" MaxLength="4000" Width="400px"
                                Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td id="Td20" runat="server">
                            <asp:Label ID="lblHelpstrategydescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr8" runat="server" visible="False">
                        <td id="Td21" style="width: 172px" runat="server">
                            <asp:Label ID="lblstartprocess" runat="server" Text="Crear proceso"></asp:Label>
                        </td>
                        <td id="Td22" runat="server">
                            <asp:CheckBox ID="chkStartProcess" runat="server" />
                        </td>
                        <td id="Td23" runat="server">
                            <asp:Label ID="lblHelpstartprocess" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" visible="False">
                        <td style="width: 172px" runat="server">
                            <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        </td>
                        <td runat="server">
                            <asp:DropDownList ID="ddlenabled" runat="server">
                                <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                                <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td runat="server">
                            <asp:Label ID="lblHelpattachfile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiduser" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 172px">
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcreatedate" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px; vertical-align: middle;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAddData" runat="server" Text="Guardar" ValidationGroup="infoGenral" />
                            <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="infoGenral" />
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar Idea" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                            <asp:Button ID="btnexportword" runat="server" Text="Exportar términos de referencia"
                                ValidationGroup="infoGenral" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                            <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                            <asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" TabIndex="1" runat="server">
            <HeaderTemplate>
                Archivos Anexos
            </HeaderTemplate>
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <table id="tableAttachments">
                                <tr>
                                    <td style="width: 20%">
                                        <asp:Label ID="lblattachfile" runat="server" Text="Archivo anexo"></asp:Label>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td nowrap="nowrap">
                                                    <img src="../App_Themes/GattacaAdmin/Images/attach.gif" alt="" />
                                                    <a id="lnkAttch" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'"
                                                        style="cursor: hand" onclick="AddFileInput()">Adjuntar un archivo</a>
                                                </td>
                                                <td style="width: 40px">
                                                    <asp:Label ID="Label12" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 172px">
                                                    <asp:Label ID="obser" runat="server" Text="Descripción"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtobser" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="tdFileInputs" valign="top">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" TabIndex="1" runat="server">
            <HeaderTemplate>
                Ubicación
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 172px">
                                    <asp:Label ID="Label1" runat="server" Text="Departamento"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDepto" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 172px">
                                    <asp:Label ID="Label3" runat="server" Text="Municipio"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCity" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 172px">
                                    <asp:Button ID="btnAgregarubicacion" runat="server" CausesValidation="False" Text="Agregar Ubicación" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
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
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server">
            <HeaderTemplate>
                Actores
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%" align="left">
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label2" runat="server" Text="Actor"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlactors" runat="server" CssClass="Ccombo">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvActor" runat="server" ControlToValidate="ddlactors"
                                        Display="Dynamic" ErrorMessage="*" ToolTip="Requerido" ValidationGroup="thirdBYIdea"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtActor" runat="server" MaxLength="1500" Rows="3" Visible="False"
                                        Width="32px"></asp:TextBox>
                                    <a id="linkactors" runat="server" href="~/GeneralPlanning/addThird.aspx?op=add?iframe=true&width=100%&height=100%"
                                        title="Nuevo actor" class="pretty">CREAR NUEVO ACTOR</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label6" runat="server" Text="Tipo"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server">
                                        <asp:ListItem>Operador</asp:ListItem>
                                        <asp:ListItem>Socio Operador</asp:ListItem>
                                        <asp:ListItem>Socio</asp:ListItem>
                                        <asp:ListItem>Cliente</asp:ListItem>
                                        <asp:ListItem>Contratante</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="Tr9" runat="server" visible="false">
                                <td style="width: 135px">
                                    <asp:Label ID="Label4" runat="server" Text="Aporte o rol"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActions" runat="server" Width="500px" MaxLength="1500" Rows="3"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Tr10" runat="server" visible="false">
                                <td style="width: 135px">
                                    <asp:Label ID="Label5" runat="server" Text="Experiencias"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExperiences" runat="server" Width="500px" MaxLength="1500" Rows="3"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExperiences"
                                            ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ToolTip="Requerido"
                                            ValidationGroup="thirdBYIdea">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label9" runat="server" Text="Contacto"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtcontact" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label15" runat="server" Text="C.C"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtcedulacont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label16" runat="server" Text="Teléfono"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txttelcont" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label17" runat="server" Text="E-mail"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtemail" runat="server" Width="500px" MaxLength="1500" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="vrdiner" runat="server" Text="Vr Dinero"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtvrdiner" runat="server" Width="200px" MaxLength="22" Rows="3"
                                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label13" runat="server" Text="Vr Especie"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtvresp" runat="server" Width="200px" MaxLength="22" Rows="3" onkeyup="format(this)"
                                        onchange="format(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Label ID="Label14" runat="server" Text="Total Aporte del Actor"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Txtaportfscocomp" runat="server" Width="200px" MaxLength="30" Rows="3"
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 135px">
                                    <asp:Button ID="btnAddThird" runat="server" Text="Agregar Actor" ValidationGroup="thirdBYIdea" />
                                </td>
                                <td>
                                    <asp:Label ID="lblavertenactors" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
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
                                </td>
                            </tr>
                            <tr id="totales" runat="server" visible="false" align="right">
                                <td style="width: 135px">
                                    <asp:Label ID="Label18" runat="server" Text=" Totales"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label Style="text-align: center;" ID="Txtsub1" runat="server" Width="105px"
                                        BorderStyle="None"></asp:Label>
                                    <asp:Label Style="text-align: center;" ID="Textsub2" runat="server" Width="105px"
                                        BorderStyle="None"></asp:Label>
                                    <asp:Label Style="text-align: center;" ID="Txtsub3" runat="server" Width="105px"
                                        BorderStyle="None"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" runat="server">
            <HeaderTemplate>
                Componentes de Programa
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td colspan="2">
                                    <cc2:DoubleListBox ID="dlbActivity" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
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
</asp:Content>
