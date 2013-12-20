<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_ResearchAndDevelopment_ResultInquest" Codebehind="ResultInquest.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="ResultInquest.aspx">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblidProject">Nombre del proyecto</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidproject" runat="server" />
            </td>
            <td>
                <asp:Label ID="lblRegistrado" runat="server" Text="Código"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblInquest" runat="server" Text="Nombre Encuesta"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInquest" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <asp:Button ID="btMake" runat="server" Text="Generar" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" Width="350px"
        GroupTreeStyle-Font-Size="8pt" Height="50px" PageToTreeRatio="5" HyperlinkTarget="_blank" />
    <br />
    </form>
</asp:Content>
