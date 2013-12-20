<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportStrategicLineDetail"
    Title="Untitled Page" Codebehind="reportStrategicLineDetail.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
   <center><asp:DropDownList ID="ddlStrategicLine" runat="server" AutoPostBack="True">    </asp:DropDownList></center>

    <br />
    <br />
    <asp:Label ID="lblTable" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button ID="btnExport" runat="server" Text="Exportar" />
    <br />
</asp:Content>
