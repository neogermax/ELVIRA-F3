<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addIndicatorInformation"
    Title="addIndicatorInformation" Codebehind="addIndicatorInformation.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="lblgoal" runat="server" Text="Meta"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtgoal" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="Label1" runat="server" Text="Registrar Valor Actual"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtValue" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValue"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
            <td>
                <asp:Label ID="lblDate" runat="server" Text="Fecha Medición"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateMesuare" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="cesbegindate" runat="server" TargetControlID="txtDateMesuare"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvbegindate" runat="server" ErrorMessage="aaaa/mm/dd" Type="Date"
                    ControlToValidate="txtDateMesuare" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            </td>
            
        </tr>
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="Label2" runat="server" Text="Observaciones"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtComments" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"
                            onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtComments"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 136px">
                        <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                    </td>
                    <td style="width: 543px">
                        <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvRecords" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Comentarios">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container, "DataItem.comments")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container, "DataItem.value")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Registro">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container, "DataItem.registrationdate")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <%--Panel de funciones--%>
            <table>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnAddData" runat="server" Text="Registrar" />
                        <asp:Button ID="btnSave" runat="server" Text="Guardar" CausesValidation="False" />
                        <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
