<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportDetalleLineasEstrategicas" title="Untitled Page" Codebehind="reportStrategyDetail.aspx.vb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
<center>
    <asp:DropDownList ID="ddlEstrategia" runat="server" AutoPostBack="True">
    </asp:DropDownList></center>
    <br />
   <%-- <CR:CrystalReportViewer ID="crvReport" runat="server" 
    AutoDataBind="true" />--%><br />
    <asp:Label ID="lblTable" runat="server"></asp:Label>
    &nbsp;<br />
    <br />
    <asp:Button ID="btnExport" runat="server" Text="Exportar" />
    &nbsp; 
</asp:Content>

