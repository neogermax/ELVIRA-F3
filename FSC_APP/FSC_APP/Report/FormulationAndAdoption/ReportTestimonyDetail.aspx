<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_ReportTestimonyDetail" Codebehind="ReportTestimonyDetail.aspx.vb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
  <CR:CrystalReportViewer ID="crvReport" runat="server" 
                                AutoDataBind="true" Width="350px" GroupTreeStyle-Font-Size="8pt" 
                                Height="50px" PageToTreeRatio="5" 
        HyperlinkTarget="_blank" />
</asp:Content>



