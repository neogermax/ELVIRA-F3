<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addExecutionContractualPlanRegistry"
    Title="addExecutionContractualPlanRegistry" Codebehind="addExecutionContractualPlanRegistry.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" src="../js/General.js"></script>

    <div>
        <asp:UpdatePanel ID="upAddData" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 195px">
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td style="width: 716px">
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidproject" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvidproject" runat="server" ControlToValidate="ddlidproject"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidproject" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblconcept" runat="server" Text="Concepto"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtconcept" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine"
                                onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvconcept" runat="server" ControlToValidate="txtconcept"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpconcept" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltype" runat="server" Text="Tipo de contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlContractType" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvtype" runat="server" ControlToValidate="ddlContractType"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelptype" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltotalcost" runat="server" Text="Valor estimado del contrato"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttotalcost" runat="server" Width="400px" MaxLength="15" onkeyup="format(this)" onchange="format(this)" ></asp:TextBox>
                            <%--<asp:CompareValidator ID="cvtotalcost" runat="server" ControlToValidate="txttotalcost"
                                ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                                Type="Double" Display="Dynamic"></asp:CompareValidator>--%>
                                        <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txttotalcost"
                                Mask="9,999,999,999,999.99"
                                MaskType="Number"
                                InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True"
                                Enabled="True" CultureAMPMPlaceholder="" 
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                               
                                ></cc1:MaskedEditExtender> --%>
                                
                        </td>
                        <td>
                            <asp:Label ID="lblHelptotalcost" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblengagementdate" runat="server" 
                                Text="Fecha prevista de contratación"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtengagementdate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <cc1:CalendarExtender ID="cecengagementdate" runat="server" Enabled="True" Format="yyyy/MM/dd"
                                TargetControlID="txtengagementdate">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="cvengagementdate" runat="server" ControlToValidate="txtengagementdate"
                                Display="Dynamic" ErrorMessage="aaaa/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True"
                                Type="Date"></asp:CompareValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpengagementdate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcomments" runat="server" Text="Observaciones"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcomments" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine"
                                onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcomments" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 195px">
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td style="width: 716px">
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 195px">
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                        </td>
                        <td style="width: 716px">
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddData" runat="server" Text="Agregar detalle" />
                            <asp:Button ID="btnEditData" runat="server" Text="Modificar detalle" Visible="false" />
                            <asp:Button ID="btnCancelEditData" runat="server" Text="Cancelar edición del detalle"
                                Visible="false" CausesValidation="False" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                            Width="100%" PageSize="10">
                                            <Columns>
                                                <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
                                                <asp:BoundField DataField="concept" HeaderText="Concepto" />
                                                <asp:BoundField DataField="contractTypeName" HeaderText="Tipo de contrato" />
                                                <asp:BoundField DataField="totalcost" HeaderText="Valor total" />
                                                <asp:BoundField DataField="engagementdate" HeaderText="Fecha de contratación" />
                                                <asp:BoundField DataField="comments" HeaderText="Comentarios" />
                                                <asp:BoundField DataField="createdate" HeaderText="Fecha de creación" />
                                                <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                <asp:CommandField ShowEditButton="true" SelectText="Editar" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Agregar datos" 
                                            CausesValidation="False"  />
                                        <asp:Button ID="btnSave" runat="server" Text="Guardar" 
                                            CausesValidation="False" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                                        &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
