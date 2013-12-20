<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_ReportCloseRegistry" Codebehind="ReportCloseRegistry.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table>
        <tr>
            <td>
                <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" Width="350px"
                    GroupTreeStyle-Font-Size="8pt" Height="50px" PageToTreeRatio="5" HyperlinkTarget="_blank"
                    DisplayGroupTree="False" />
            </td>
        </tr>
    </table>
</asp:Content>
