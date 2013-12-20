<%@ Page Language="VB" MasterPageFile="~/Master/mpError.master" AutoEventWireup="false" Inherits="FSC_APP.Errors_error" title="Untitled Page" Codebehind="error.aspx.vb" %>

<asp:Content ID="cphError" ContentPlaceHolderID="cphError" Runat="Server">
    <table style="width:100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTitle" runat="server" CssClass="cssLabelTitle" Text="ERROR" SkinID="LabelFormTitle"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="cssLabelForm" SkinID="LabelForm"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="upError" runat="server">
                    <ContentTemplate>
                <asp:Button ID="btnClose" runat="server" Text="Cerrar" CssClass="cssButton" SkinID="Button"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>

