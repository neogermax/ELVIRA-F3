<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_ReportOperatorList" Codebehind="ReportOperatorList.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="ReportOperatorList.aspx">
    <table>
        <tr>
            <td style="width: 173px">
                <asp:Label runat="server" ID="lblidProject0">Nombre del proyecto</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidproject" runat="server" />
            </td>
            <td style="width: 136px">
                <asp:Label runat="server" ID="lblidProject">Nombre de la Linea estrategica</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidStrategicLine" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 173px">
                <asp:Label ID="lblOperador" runat="server" Text="Operador"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOperador" runat="server"></asp:TextBox>
            </td>
            <td style="width: 136px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 173px">
                <asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDepto" runat="server" AutoPostBack="True" />
            </td>
            <td style="width: 136px">
                <asp:Label ID="lblCiudad" runat="server" Text="Ciudad"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCity" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 173px">
                <asp:Label runat="server" ID="lblFechaInicio">Fecha inicio proyecto entre</asp:Label>
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
            <td style="width: 136px">
                <asp:Label ID="lblFechaFin" runat="server" Text="Y"></asp:Label>
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
            <td style="width: 173px">
                <asp:Label runat="server" ID="lblFechaInicio0">Fecha fin proyecto entre</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicio0" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="cvFechaInicio0" runat="server" ControlToValidate="txtFechaInicio0"
                    Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Operator="DataTypeCheck"
                    Type="Date"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFechaInicio0"
                    PopupPosition="TopLeft">
                </cc1:CalendarExtender>
            </td>
            <td style="width: 136px">
                <asp:Label ID="lblFechaFin0" runat="server" Text="Y"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFin0" runat="server"></asp:TextBox>
                <asp:CompareValidator ID="cvFechaIncioFechaFin0" runat="server" ControlToCompare="txtFechaInicio0"
                    ControlToValidate="txtFechaFin0" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                <asp:CompareValidator ID="cvFechaFin0" runat="server" ControlToValidate="txtFechaFin0"
                    Display="Dynamic" ErrorMessage="yyyy/mm/dd" SetFocusOnError="True" Type="Date"
                    Operator="DataTypeCheck"></asp:CompareValidator>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFechaFin0"
                    PopupPosition="TopLeft">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="width: 173px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center; width: 136px;">
                <asp:Button ID="btMake" runat="server" Text="Generar" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" Width="350px"
        GroupTreeStyle-Font-Size="8pt" Height="50px" PageToTreeRatio="5" HyperlinkTarget="_blank"
        DisplayGroupTree="False" />
    <br />
    </form>
</asp:Content>
