<%@ Page Title="Plan de contratación" Language="VB" MasterPageFile="~/Master/mpAdmin.master"
    AutoEventWireup="false" Inherits="FSC_APP.Report_OperationalPlanning_reportRecruitmentPlan" Codebehind="reportRecruitmentPlan.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportRecruitmentPlan.aspx">
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblProject">Proyecto</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProject" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label1">Fase</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProjectPhase" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnShow" runat="server" Text="Generar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <CR:CrystalReportViewer ID="crvRecruitmentPlan" runat="server" AutoDataBind="true"
                                DisplayGroupTree="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
