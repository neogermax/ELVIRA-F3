<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_OperationalPlanning_StateSummoning" Codebehind="StateSummoning.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="StateSummoning.aspx">
    <table>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblStrategicLine">Nombre de la Linea Estrategica</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStrategicLine" runat="server" AutoPostBack="True" />
            </td>
            <td>
                <asp:Label runat="server" ID="lblidProject">Nombre del proyecto</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidproject" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblcode">Código de la Convocatoria</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcode" runat="server" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblFechaInicio">Fecha Inicio</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="cvFechaInicio" runat="server" ControlToValidate="txtFechaInicio"
                    Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Operator="DataTypeCheck"
                    Type="Date"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaInicio"
                    PopupPosition="TopLeft">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="cvFechaIncioFechaFin" runat="server" ControlToCompare="txtFechaInicio"
                    ControlToValidate="txtFechaFin" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                <asp:CompareValidator ID="cvFechaFin" runat="server" ControlToValidate="txtFechaFin"
                    Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Type="Date"
                    Operator="DataTypeCheck"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFechaFin"
                    PopupPosition="TopLeft">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <asp:Button ID="btMake" runat="server" Text="Generar" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" Width="350px"
        GroupTreeStyle-Font-Size="8pt" Height="50px" PageToTreeRatio="5" DisplayGroupTree="False" />
    <br />
    </form>
</asp:Content>
