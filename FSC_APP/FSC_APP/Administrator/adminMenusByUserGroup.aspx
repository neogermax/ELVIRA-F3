<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Administrator_adminMenusByUserGroup" title="Untitled Page" Codebehind="adminMenusByUserGroup.aspx.vb" %>

<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                <tbody>
                    <tr>
                        <td align="left" width="60%">
                            <asp:Label ID="lblTitle" runat="server" CssClass="cssLabelTitle">ADMINISTRACION DE GRUPOS DE USUARIO POR MENU</asp:Label></td>
                        <td align="right">
                            <asp:Label ID="lblMenuName" runat="server" CssClass="cssLabelForm"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <cc1:DoubleListBox ID="dlbMenusByUserGroup" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="cssButton" OnClick="btnUpdate_Click"
                                Text="Guardar" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="cssButton" Text="Cancelar" /></td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

