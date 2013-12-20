<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FormulationAndAdoption_addProjectApproval"
    Title="Página sin título" Codebehind="addProjectApproval.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/project_approval.js" type="text/javascript"></script>

    <h1>
        <span style="font-family: arial,helvetica,sans-serif;">Aprobación Proyecto</span></h1>
    <p>
        &nbsp;</p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="width: 10%;">
                    <strong><span style="font-family: arial,helvetica,sans-serif;">Proyecto</span></strong>
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlproyect" runat="server" CssClass="Ccombo" Width="50%">
                    </asp:DropDownList>
                </td>
                <td style="width: 30%;">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
