<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportBasicProjectData"
    Title="Datos básicos del proyecto" Codebehind="reportBasicProjectData.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportBasicProjectData.aspx">
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblidProject">Proyecto</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidproject" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblcode">Código del proyecto</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcode" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblyear">Año</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlyear" runat="server">
                                <asp:ListItem Selected="True" Text="Seleccione año" Value="0"></asp:ListItem>
                            </asp:DropDownList>
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
                            <asp:Button ID="btMake" runat="server" Text="Generar" />
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
                            <CR:CrystalReportViewer ID="crvBasicProjectData" runat="server" AutoDataBind="true"
                                DisplayGroupTree="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
