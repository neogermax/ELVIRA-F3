<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addContractExecution" Title="addContractExecution" Codebehind="addContractExecution.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <asp:LinkButton ID="lbMessage" runat="server" CausesValidation="False" ForeColor="Red"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="1">
        <cc1:TabPanel runat="server" HeaderText="Datos Generales" ID="TabPanel1" Width="600px">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblidcontractrequest" runat="server" Text="Id Solicitud Contrato"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:DropDownList ID="ddlcontractrequest" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvidcontractrequest" runat="server" ControlToValidate="ddlcontractrequest"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvContractrequest" runat="server" ControlToValidate="ddlcontractrequest"
                                        Display="Dynamic" ErrorMessage="Este código ya existe, por favor cambielo" SetFocusOnError="True">Esta ejecución de contrato ya existe, por favor verifique.</asp:CustomValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpidcontractrequest" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblstartdate" runat="server" Text="Fecha de Inicio"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtstartdate" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cestartdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                        TargetControlID="txtstartdate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvstartdate" runat="server" ControlToValidate="txtstartdate"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvstartdate" runat="server" ControlToValidate="txtstartdate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpstartdate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblpaymentdate" runat="server" Text="Fecha de Pago"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtpaymentdate" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cepaymentdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                        TargetControlID="txtpaymentdate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvpaymentdate" runat="server" ControlToValidate="txtpaymentdate"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvpaymentdate" runat="server" ControlToValidate="txtpaymentdate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelppaymentdate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblcontractnumber" runat="server" Text="Número de Contrato"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtcontractnumber" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcontractnumber" runat="server" ControlToValidate="txtcontractnumber"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpcontractnumber" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblordernumber" runat="server" Text="Número de Orden"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtordernumber" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvordernumber" runat="server" ControlToValidate="txtordernumber"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpordernumber" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblclosingcomments" runat="server" Text="Comentarios de Cierre"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtclosingcomments" runat="server" Width="400px" MaxLength="50"
                                        TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvclosingcomments" runat="server" ControlToValidate="txtclosingcomments"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpclosingcomments" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblclosingdate" runat="server" Text="Fecha de Cierre"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtclosingdate" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ceclosingdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                        TargetControlID="txtclosingdate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvclosingdate" runat="server" ControlToValidate="txtclosingdate"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvclosingdate" runat="server" ControlToValidate="txtclosingdate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpclosingdate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblfinalpaymentdate" runat="server" Text="Fecha de Pago Final"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtfinalpaymentdate" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cefinalpaymentdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                        TargetControlID="txtfinalpaymentdate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvfinalpaymentdate" runat="server" ControlToValidate="txtfinalpaymentdate"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvfinalpaymentdate" runat="server" ControlToValidate="txtfinalpaymentdate"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpfinalpaymentdate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblvalue" runat="server" Text="Valor"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtvalue" runat="server" Width="200px" MaxLength="15" onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvvalue" runat="server" ControlToValidate="txtvalue"
                                        ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                      <%--    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtvalue"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%>
                                   <%-- <asp:CompareValidator ID="cvValue" runat="server" ControlToValidate="txtValue" ErrorMessage="Valor numérico"
                                        Operator="DataTypeCheck" SetFocusOnError="True" Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpvalue" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 149px">
                                    <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de Creación"></asp:Label>
                                </td>
                                <td style="width: 449px">
                                    <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Lista de Pagos Registrados">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPaymentsList" runat="server" AutoGenerateColumns="False" Width="90%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container, "DataItem.Id")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container, "DataItem.value", "{0:c}")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pocentaje">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container, "DataItem.Percentage", "{0:n}")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container, "DataItem.datePaymentsList", "{0:yyyy/MM/dd}")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha Pago Final">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFinalPaymentDate" runat="server" MaxLength="10" Width="200px"
                                                        Text='<%# Eval("finalPaymentDate","{0:yyyy/MM/dd}") %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="cesFinalPaymentDate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                                        TargetControlID="txtFinalPaymentDate">
                                                    </cc1:CalendarExtender>
                                                    <asp:CompareValidator ID="cvFinalPaymentDate" runat="server" ControlToValidate="txtFinalPaymentDate"
                                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                                        Display="Dynamic"></asp:CompareValidator>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor Pago final">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFinalPaymentValue" runat="server" MaxLength="15" Width="200px"
                                                        Text='<%# System.String.Format("{0:#,###;(#,###);Zero}", Eval("finalPaymentValue")) %>'  onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                                  <%--                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtFinalPaymentValue"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender>--%>
                                                 <%--   <asp:CompareValidator ID="cvFinalPaymentValue" runat="server" ControlToValidate="txtFinalPaymentValue"
                                                        ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                                        Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
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
    </cc1:TabContainer>
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
