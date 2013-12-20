<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" ValidateRequest="false" EnableEventValidation="false"
    Inherits="FSC_APP.addContractRequest" Title="addContractRequest" Codebehind="addContractRequest.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../Pretty/css/prettyPhoto.css" rel="stylesheet" type="text/css" />

    <script src="../Pretty/js/jquery.prettyPhoto.js" type="text/javascript"></script>

    <script src="../Include/javascript/contractrequest.js" type="text/javascript"></script>

    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <cc1:TabPanel ID="TabPanel9" runat="server" HeaderText="Términos de Referencia">
            <ContentTemplate>
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Datos Generales" ID="TabPanel1" Width="600px">
            <HeaderTemplate>
                Datos Generales
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblProjInfo" runat="server" Text="Proyecto"></asp:Label>
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtProjectNumber" runat="server" ReadOnly="True" Width="400px" 
                                        Font-Bold="True" ForeColor="Black"></asp:TextBox>--%><asp:Label ID="lblProjectNumber"
                                            runat="server"></asp:Label></td>
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
                                    <%--<asp:RequiredFieldValidator ID="rfvManagement" runat="server" ControlToValidate="ddlManagement"
                                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
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
                                    <%--<asp:RequiredFieldValidator ID="rfvContractNatural" runat="server" ControlToValidate="ddlContractNature"
                                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
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
                                        ErrorMessage="aaaa/mm/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSubscriptionDate"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
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
                                        Type="Date" ControlToValidate="txtInitialDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInitialDate"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
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
                                    <asp:TextBox ID="txtContractDuration" runat="server" Width="400px" MaxLength="5"></asp:TextBox><%--<asp:RequiredFieldValidator ID="rfvContractDuration" runat="server" ControlToValidate="txtContractDuration"
                                        ErrorMessage="*" ForeColor="Red"> </asp:RequiredFieldValidator>--%>
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
                                    <asp:TextBox ID="txtEndingDate" runat="server" Width="400px" MaxLength="50" ReadOnly="True"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEndingDate"
                                        ErrorMessage="*" ForeColor="Red"> </asp:RequiredFieldValidator>--%>
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
                                        ForeColor="Red"></asp:Label></td>
                            </tr>
                            </tr></table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Actores">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Contratistas Persona Natural">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label3" runat="server" Text="Nit"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNitContractorNaturalPerson" runat="server" MaxLength="50" Width="250px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNitContractorNaturalPerson"
                                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ToolTip="Requerido"
                                        ValidationGroup="ContractorNaturalPerson">*</asp:RequiredFieldValidator>
                                    &nbsp;<asp:Label ID="lblHelpNitContractorNaturalPerson" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label1" runat="server" Text="Nombre del contratista"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractorNameContractorNaturalPerson" runat="server" MaxLength="150"
                                        Width="500px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txtContractorNameContractorNaturalPerson" ErrorMessage="RequiredFieldValidator"
                                            SetFocusOnError="True" ToolTip="Requerido" ValidationGroup="ContractorNaturalPerson">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Button ID="btnAddContractorNaturalPerson" runat="server" Text="Agregar" ValidationGroup="ContractorNaturalPerson" />
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
                                    <asp:GridView ID="gvContractorNaturalPerson" runat="server" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                            <asp:TemplateField HeaderText="Nit">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre del contratista">
                                                <ItemTemplate>
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
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Objeto y Valor">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label8" runat="server" Text="Objeto del Contrato"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubjectContract" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                                        Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label9" runat="server" Text="Productos o Entregables"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProductsOrDeliverables" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                                        Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label10" runat="server" Text="Valor del Contrato"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractValue" runat="server" MaxLength="15" Width="200px" onkeyup="format(this)"
                                        onchange="format(this)"></asp:TextBox><%--    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtContractValue"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>
--%><%--      <asp:CompareValidator ID="cvContractValue" runat="server" ControlToValidate="txtContractValue"
                                        ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Double"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label11" runat="server" Text="Monto del Aporte"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContributionAmount" runat="server" MaxLength="15" Width="200px"
                                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox><%-- <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtContributionAmount"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%><%-- <asp:CompareValidator ID="cvContributionAmount" runat="server" ControlToValidate="txtContributionAmount"
                                        ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Double"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label12" runat="server" Text="Monto Honorarios Consultor por institución"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFeesConsultantByInstitution" runat="server" MaxLength="15" Width="200px"
                                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox><%--    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtFeesConsultantByInstitution"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%><%-- <asp:CompareValidator ID="cvFeesConsultantByInstitution" runat="server" ControlToValidate="txtFeesConsultantByInstitution"
                                        ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Double"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label13" runat="server" Text="Monto Total Honorarios Consultor Integral"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotalFeesIntegralConsultant" runat="server" MaxLength="15" Width="200px"
                                        onkeyup="format(this)" onchange="format(this)"></asp:TextBox><%--   <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                TargetControlID="txtTotalFeesIntegralConsultant"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%><%-- <asp:CompareValidator ID="cvTotalFeesIntegralConsultant" runat="server" ControlToValidate="txtTotalFeesIntegralConsultant"
                                        ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                        Type="Double"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label14" runat="server" Text="Monto Aporte Institución Beneficiaria"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContributionAmountRecipientInstitution" runat="server" MaxLength="15"
                                        Width="200px" onkeyup="format(this)" onchange="format(this)"></asp:TextBox><%--   <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                TargetControlID="txtContributionAmountRecipientInstitution"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%><%-- <asp:CompareValidator ID="cvContributionAmountRecipientInstitution" runat="server"
                                        ControlToValidate="txtContributionAmountRecipientInstitution" ErrorMessage="Valor numérico"
                                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Double"></asp:CompareValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label15" runat="server" Text="Moneda"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrency" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ControlToValidate="ddlCurrency"
                                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Lista de Pagos">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label16" runat="server" Text="Valor"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValue" runat="server" MaxLength="15" Width="200px" onkeyup="format(this)"
                                        onchange="format(this)"></asp:TextBox><asp:RequiredFieldValidator ID="rfvValue" runat="server"
                                            ControlToValidate="txtValue" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True"
                                            ToolTip="Requerido" ValidationGroup="PaymentsList" Display="Dynamic">*</asp:RequiredFieldValidator><%--         <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                TargetControlID="txtValue"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%><%--   <asp:CompareValidator ID="cvValue" runat="server" ControlToValidate="txtValue" ErrorMessage="Valor numérico"
                                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" ValidationGroup="PaymentsList"
                                        Display="Dynamic"></asp:CompareValidator>--%>&nbsp;<asp:Label ID="lblHelpValue" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label17" runat="server" Text="Porcentaje"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPercentage" runat="server" Width="50px" MaxLength="4"></asp:TextBox>
                                    %<asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" ToolTip="Requerido"
                                        ValidationGroup="PaymentsList" Display="Dynamic">*</asp:RequiredFieldValidator><asp:RangeValidator
                                            ID="rvPercentage" runat="server" ControlToValidate="txtPercentage" Display="Dynamic"
                                            ErrorMessage="Debe digitar un valor entre 0 (cero) y 100 (cien)" MaximumValue="100"
                                            MinimumValue="0" SetFocusOnError="True" Type="Double" ValidationGroup="PaymentsList"></asp:RangeValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label18" runat="server" Text="Fecha"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdate" runat="server" MaxLength="50" Width="200px"></asp:TextBox><cc1:CalendarExtender
                                        ID="cestartdate" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtdate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdate"
                                        ErrorMessage="*" ValidationGroup="PaymentsList" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:CompareValidator
                                            ID="cvstartdate" runat="server" ControlToValidate="txtdate" ErrorMessage="yyyy/MM/dd"
                                            Operator="DataTypeCheck" SetFocusOnError="True" Type="Date" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Button ID="btnAddPaymentsList" runat="server" Text="Agregar" ValidationGroup="PaymentsList" />
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
                                    <asp:GridView ID="gvPaymentsList" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pocentaje">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
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
        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Datos del Contrato">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label19" runat="server" Text="Duración Contrato"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractDuration2" runat="server" MaxLength="150" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label27" runat="server" Text="Fecha Inicio"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" MaxLength="20" Width="200px"></asp:TextBox><cc1:CalendarExtender
                                        ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                                    &nbsp;<asp:CompareValidator ID="cvStartDateContractData" runat="server" ControlToValidate="txtStartDate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label28" runat="server" Text="Fecha Finalización"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDate" runat="server" MaxLength="20" Width="200px"></asp:TextBox><cc1:CalendarExtender
                                        ID="ceEndDate" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="txtEndDate">
                                    </cc1:CalendarExtender>
                                    &nbsp;<asp:CompareValidator ID="cvEnDateStartDateContractData" runat="server" ControlToValidate="txtEndDate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator><asp:CompareValidator ID="CompareValidator1"
                                            runat="server" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate"
                                            Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                                            Operator="GreaterThanEqual" SetFocusOnError="True"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label21" runat="server" Text="Vigencia Presupuestal"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBudgetValidity" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                                        Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label22" runat="server" Text="Datos de Contacto"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactData" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                                        Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label23" runat="server" Text="Dirección electrónica"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" Width="500px"></asp:TextBox>
                                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email inválido"
                                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label24" runat="server" Text="Teléfono"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="Polizas">
            <HeaderTemplate>
                Polizas
            </HeaderTemplate>
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="PolizaRequired" runat="server" /><asp:Label ID="lblPolizaRequired"
                            runat="server" Text="Requiere Poliza?"></asp:Label><div id="divPoliza" runat="server">
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
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPolizaSubject" runat="server" Text="Concepto"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="gvPolizaConcept" runat="server" Width="50%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                    <asp:TemplateField HeaderText="Concepto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="concepto" runat="server" Text='<%# Eval("concepto") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="addConcept" runat="server" Text="Agregar Concepto" CausesValidation="False" /><asp:Label
                                                    ID="lblAddPolizaNfo" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel8" runat="server" HeaderText="Observaciones">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label31" runat="server" Text="Observaciones Adicionales"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAdditionalComments" runat="server" MaxLength="255" onkeypress="return textboxAreaMaxNumber(this,255)"
                                        Rows="2" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label25" runat="server" Text="Requiere Acta de Inicio"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkStartActRequires" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label26" runat="server" Text="Fecha Aviso Vencimiento"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateNoticeExpiration" runat="server" MaxLength="20" Width="250px"></asp:TextBox><cc1:CalendarExtender
                                        ID="ceDateNoticeExpiration" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                        TargetControlID="txtDateNoticeExpiration">
                                    </cc1:CalendarExtender>
                                    &nbsp;<asp:CompareValidator ID="cvDateNoticeExpiration" runat="server" ControlToValidate="txtDateNoticeExpiration"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="lblContractNumber" runat="server" Text="No. de Contrato"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContractNumber" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 187px">
                                    <asp:Label ID="Label29" runat="server" Text="Orden de Compra"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPurchaseOrder" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <%--<asp:RequiredFieldValidator ID="rfvManagement" runat="server" ControlToValidate="ddlManagement"
                                        Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>--%>
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
    <asp:HiddenField ID="HFPolRequired" runat="server" />
</asp:Content>
