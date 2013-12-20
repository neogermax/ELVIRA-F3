<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_ResearchAndDevelopment_ReportStatistics" Codebehind="ReportStatistics.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" Width="350px"
        GroupTreeStyle-Font-Size="8pt" Height="50px" PageToTreeRatio="5" HyperlinkTarget="_blank"
        DisplayGroupTree="False" />
    <br />
</asp:Content>
