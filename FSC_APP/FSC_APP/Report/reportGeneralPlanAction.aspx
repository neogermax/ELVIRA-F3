<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportGeneralPlanAction"
    Title="Untitled Page" Codebehind="reportGeneralPlanAction.aspx.vb" %>

<%--@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <center>
        <table>
            <tr>
                <td>
                    Año
                </td>
                <td>
                    <asp:DropDownList ID="ddlyear" runat="server" Style="text-align: center" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </center>
    <br />
    <asp:Label ID="lblTable" runat="server" Text=""></asp:Label>
    <br />
    <br />
  
    <br />
    <asp:Button ID="btnExport" runat="server" Text="Exportar" />
    <%--  <CR:CrystalReportViewer ID="crvReport" runat="server" 
    AutoDataBind="true" HyperlinkTarget="_blank" />--%>
    <br />
</asp:Content>
