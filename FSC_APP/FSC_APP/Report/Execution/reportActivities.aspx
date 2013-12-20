<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportActivities" EnableEventValidation="false" Codebehind="reportActivities.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportActivities.aspx">
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblProject">Proyecto</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="Label2">Componente</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlComponent" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1">Responsable</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlResponsible" runat="server" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="Label3">Estado</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlState" runat="server">
                                        <asp:ListItem Text="Ajustar Informacion" Value="Ajustar Informacion" />
                                        <asp:ListItem Text="Aprobar" Value="Aprobar" />
                                        <asp:ListItem Text="Revisar Avance" Value="Revisar Avance" />
                                        <asp:ListItem Text="Todos" Value="" Selected="True" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 173px">
                                    <asp:Label runat="server" ID="lblFechaInicio0" Text="Fecha final entre" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDateBegin" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="cvEndDateBegin" runat="server" ControlToValidate="txtEndDateBegin"
                                        Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Operator="DataTypeCheck"
                                        Type="Date"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndDateBegin"
                                        PopupPosition="TopLeft">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 136px">
                                    <asp:Label ID="lblFechaFin0" runat="server" Text="Y"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDateEnd" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID="cvFechaIncioFechaFin0" runat="server" ControlToCompare="txtEndDateBegin"
                                        ControlToValidate="txtEndDateEnd" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                    <asp:CompareValidator ID="cvEndDateEnd" runat="server" ControlToValidate="txtEndDateEnd"
                                        Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Type="Date"
                                        Operator="DataTypeCheck"></asp:CompareValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDateEnd"
                                        PopupPosition="TopLeft">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="Generar" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" DisplayGroupTree="False" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
