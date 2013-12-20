<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addAccumulationIndicatorSet"
    Title="addAccumulationIndicatorSet" Codebehind="addAccumulationIndicatorSet.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <cc2:TabContainer ID="TabContainer1" runat="server" Width="100%"
        ActiveTabIndex="0" ScrollBars="Vertical">
        <cc2:TabPanel runat="server" HeaderText="Configurar Acumulación de Indicador" ID="TabPanel1"
            Width="90%" TabIndex="0">
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" 
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblindicatorcode" runat="server" Text="Código del Indicador"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtindicatorcode" runat="server" Width="400px" MaxLength="50" 
                                    Enabled="False"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvidindicator" runat="server" ControlToValidate="txtindicatorcode"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpindicatorcode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblname" runat="server" Text="Nombre/Descripción Entidad"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlname" runat="server" Enabled="False">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="ddlname"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbltype" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddltype" runat="server" Enabled="False">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvtype" runat="server" ControlToValidate="ddltype"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelptype" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" 
                                    AutoPostBack="True" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="255" 
                                    Height="70px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ControlToValidate="txtdescription"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpdescription" runat="server"></asp:Label>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                                    ErrorMessage="*" ValidationGroup="setInd"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Modificar" 
                                    ValidationGroup="setInd" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="Lista de Indicadores" ID="TabPanel2" Width="90%"
            TabIndex="1">
            <ContentTemplate>
                <div>
                    <table  border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <cc1:DoubleListBox ID="dlbIndicator" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
