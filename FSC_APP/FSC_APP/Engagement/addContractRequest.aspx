<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.addContractRequest"
    Title="addContractRequest" CodeBehind="addContractRequest.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <%--codigo nuevo--%>
    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Include/javascript/contractrequest.js" type="text/javascript"></script>

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script>
        $(function() {
            $("#tabs").tabs();
        });
    </script>

    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Términos de Referencia</a></li>
            <li><a href="#tabs-2">Datos Generales</a></li>
            <li><a href="#tabs-3">Actores</a></li>
            <li><a href="#tabs-4">Polizas</a></li>
        </ul>
        <div id="tabs-1">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="Ccombo">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtProject" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpidproject" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="Export_Terms" runat="server" Text="Exportar Términos de Referencia" /><asp:Label
                            ID="lblExportTerms" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs-2">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblProjInfo" runat="server" Text="Proyecto"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblProjectNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <asp:Label ID="lblrequestnumber" runat="server" Text="No. Solicitud"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <asp:TextBox ID="txtrequestnumber" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblHelprequestnumber" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <asp:Label ID="lblidmanagement" runat="server" Text="Gerencia"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <asp:DropDownList ID="ddlManagement" runat="server" CssClass="Ccombo" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpidmanagement" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <asp:Label ID="lblidcontractnature" runat="server" Text="Naturaleza de la Contratación"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <asp:DropDownList ID="ddlContractNature" runat="server" CssClass="Ccombo">
                            <asp:ListItem Value="1">Contrato</asp:ListItem>
                            <asp:ListItem Value="2">Convenio</asp:ListItem>
                            <asp:ListItem Value="3">Orden de prestación de servicios</asp:ListItem>
                            <asp:ListItem Value="4">Orden de compraventa</asp:ListItem>
                            <asp:ListItem Value="5">Otro si</asp:ListItem>
                            <asp:ListItem Value="6">Otros...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpidcontractnature" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <br />
                        <asp:Label ID="lblcontractnumberadjusted" runat="server" Text="Número de contrato"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <br />
                        <asp:TextBox ID="txtcontractnumberadjusted" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpcontractnumberadjusted" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <asp:DropDownList ID="ddlEnabled" runat="server" CssClass="Ccombo">
                            <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                            <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblSubscriptionDate" runat="server" Text="Fecha de Suscripción"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtSubscriptionDate" runat="server" MaxLength="50" Width="400px"></asp:TextBox><cc1:CalendarExtender
                            ID="CalendarExtender2" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtSubscriptionDate">
                        </cc1:CalendarExtender>
                        <asp:CompareValidator ID="cvsubscriptiondate" runat="server" ControlToValidate="txtSubscriptionDate"
                            ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblInfoSubscriptionDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblInitialDate" runat="server" Text="Fecha de Inicio"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtInitialDate" runat="server" Width="400px" MaxLength="50"></asp:TextBox><cc1:CalendarExtender
                            ID="cesapprovaldate" runat="server" TargetControlID="txtInitialDate" Format="yyyy/MM/dd"
                            Enabled="True">
                        </cc1:CalendarExtender>
                        <asp:CompareValidator ID="cvinitialdate" runat="server" ErrorMessage="aaaa/mm/dd"
                            Type="Date" ControlToValidate="txtInitialDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblinformation" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblContractDuration" runat="server" Text="Duración en meses (Recibe hasta 2 decimales)"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtContractDuration" runat="server" Width="400px" MaxLength="5"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblNfoContractDuration" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblEndingDate" runat="server" Text="Fecha de finalización"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtEndingDate" runat="server" Width="400px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblNfoEndingdate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 187px">
                        <br />
                        <asp:Label ID="Label20" runat="server" Text="Supervisor"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtSupervisor" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                            Rows="2" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblConfidential" runat="server" Text="Acuerdo de confidencialidad"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <br />
                        <asp:DropDownList ID="ddlConfidential" runat="server" CssClass="Ccombo">
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                            <asp:ListItem Value="3">No aplica</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblNfoConfidential" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Label ID="lblSignedContract" runat="server" Text="Contrato firmado"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:CheckBox ID="chkSignedContract" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblNfoSigned" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 187px">
                        <br />
                        <asp:Label ID="lblObs" runat="server" Text="Observaciones"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <asp:TextBox ID="txtObs" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                            Rows="2" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <br />
                        <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <br />
                        <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 183px">
                        <br />
                        <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
                    </td>
                    <td style="width: 481px">
                        <br />
                        <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAddData" runat="server" Text="Guardar Información" /><asp:Button
                            ID="btnSave" runat="server" CausesValidation="False" Text="Guardar Información" /><asp:Button
                                ID="btnFinishContract" runat="server" CausesValidation="False" Text="Finalizar proceso de contratación" /><asp:Button
                                    ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" /><asp:Button
                                        ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" /><asp:Button
                                            ID="btnExport" runat="server" Text="Exportar" CausesValidation="False" /><asp:Label
                                                ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar Solicitud" CausesValidation="False" /><asp:Button
                            ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                        &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                </tr></table>
        </div>
        <div id="tabs-3">
            <table width="100%">
                <tr>
                    <td>
                        <p>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    </td>
                    <td id="GVTHIRD" runat="server" style="width: 85%">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGridViewNfo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tabs-4">
            <asp:CheckBox ID="PolizaRequired" runat="server" /><asp:Label ID="lblPolizaRequired"
                runat="server" Text="Requiere Poliza?"></asp:Label><div id="divPoliza" runat="server">
                    <fieldset>
                        <legend>Datos de la póliza</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblPolizaconsec" runat="server" Text="Nombre de la aseguradora"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolizaConsec" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPolizanumber" runat="server" Text="Número de la Póliza"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPolizaNumber" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblExpeditiondate" runat="server" Text="Fecha de Expedición"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExpeditionDate" runat="server" MaxLength="50" Width="400px"></asp:TextBox><cc1:CalendarExtender
                                        ID="cesexpeditiondate" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtExpeditionDate">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator ID="cvexpeditiondate" runat="server" ControlToValidate="txtExpeditionDate"
                                        ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Conceptos</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <td>
                                        <asp:Label ID="lblPolizaSubject" runat="server" Text="Concepto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                    </td>
                                </td>
                                <td>
                                    <td>
                                        <asp:Label ID="lblInitDatePoliza" runat="server" Text="Inicio de Vigencia"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInitDatePoliza" runat="server" MaxLength="50"></asp:TextBox><cc1:CalendarExtender
                                            ID="ceInitDatePoliza" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtInitDatePoliza">
                                        </cc1:CalendarExtender>
                                        <asp:CompareValidator ID="cvInitDatePoliza" runat="server" ControlToValidate="txtInitDatePoliza"
                                            ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td> 
                                </td>
                                <td>
                                    <td>
                                        <asp:Label ID="lblFinishDatePoliza" runat="server" Text="Fin de Vigencia"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFinishDatePoliza" runat="server" MaxLength="50"></asp:TextBox><cc1:CalendarExtender
                                            ID="ceFinishDatePoliza" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtFinishDatePoliza">
                                        </cc1:CalendarExtender>
                                        <asp:CompareValidator ID="cvFinishDatePoliza" runat="server" ControlToValidate="txtFinishDatePoliza"
                                            ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    </td>
                                </td>
                            </tr>
                            <tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="addConcept" runat="server" Text="Agregar Concepto" CausesValidation="False" /><asp:Label
                                            ID="lblAddPolizaNfo" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvPolizaConcept" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                            <asp:TemplateField HeaderText="Concepto">
                                                <ItemTemplate>
                                                    <asp:Label ID="concepto" runat="server" Text='<%# Eval("concepto") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inicio de Vigencia">
                                                <ItemTemplate>
                                                    <asp:Label ID="inivig" runat="server" Text='<%# Eval("inivig") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fin de Vigencia">
                                                <ItemTemplate>
                                                    <asp:Label ID="finvig" runat="server" Text='<%# Eval("finvig") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
        </div>
        <%--fin codigo nuevo--%>
        <br />
        <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
            border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
            vertical-align: middle;">
            <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
            <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
        </div>
        <br />
        <div id="DivConfirm" runat="server" style="width: 100%; text-align: center; border: 2px solid #cecece;
            background: #E8E8DC; height: 40px; line-height: 40px; vertical-align: middle;">
            <img style="margin-top: 5px;" src="/images/alert_icon.png" width="24px" alt="Save" />
            <asp:Label ID="lblConfirmation" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
        </div>
        <br />
        <asp:HiddenField ID="HFContractRequest" runat="server" />
        <asp:HiddenField ID="HFEndDate" runat="server" />
        <asp:HiddenField ID="HFtextactor" runat="server" />
        <asp:HiddenField ID="HF_ID_Project" runat="server" />
        <asp:HiddenField ID="HFProject" runat="server" />
        <asp:HiddenField ID="HFActivetab" runat="server" />
        <asp:HiddenField ID="HFPolRequired" runat="server" />
</asp:Content>
