<%@ Page Title="Matriz de indicadores" Language="VB" MasterPageFile="~/Master/mpAdmin.master"
    AutoEventWireup="false" Inherits="FSC_APP.Report_reporMatrixIndicator" Codebehind="reporMatrixIndicator.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblidProject">Nombre del proyecto</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidproject" runat="server">
                                <asp:ListItem Value="0">Seleccione proyecto</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="ddlidproject" 
                                ErrorMessage="Por favor seleccione un proyecto de la lista" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbltype">Tipo de indicador</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddltype" runat="server">
                                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>                            
                                <asp:ListItem Text="Beneficiarios" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Capacidad instalada" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Gestión del conocimiento" Value="3"></asp:ListItem>
                            </asp:DropDownList>
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
                <table>
                    <tr>
                        <td>
                            <CR:CrystalReportViewer ID="crvMatrixIndicator" runat="server" AutoDataBind="true"
                                HasCrystalLogo="False" HasToggleGroupTreeButton="False" DisplayGroupTree="False"
                                HasDrillUpButton="True" HasPageNavigationButtons="True" HasViewList="False" BorderStyle="Inset"
                                BorderWidth="1px" BorderColor="#006A7A" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
