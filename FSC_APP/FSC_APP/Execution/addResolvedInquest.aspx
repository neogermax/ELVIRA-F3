<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addResolvedInquest" Title="addResolvedInquest" Codebehind="addResolvedInquest.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="lblMessage" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="color: Black" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Table ID="tbQuestions" runat="server">
                </asp:Table>
            </td>
        </tr>
    </table>
    <%--Panel de funciones--%>
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Registrar" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
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
</asp:Content>
