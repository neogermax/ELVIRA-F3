<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportActorsMap" Codebehind="reportActorsMap.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportActorsMap.aspx">
    <table>
        <tr>
            <td>
                <asp:Label ID="lbDate1" runat="server" Text="Fecha Creación Inicial"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Fecha Creación Final"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbDate2" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtStartCreationDate" runat="server" MaxLength="10"></asp:TextBox>
                <asp:CompareValidator ID="cvStartCreationDate" runat="server" ControlToValidate="txtStartCreationDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                <cc1:CalendarExtender ID="ceStartCreationDate" runat="server" TargetControlID="txtStartCreationDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:TextBox ID="txtEndCreationDate" runat="server" MaxLength="10"></asp:TextBox>
                <asp:CompareValidator ID="cvEndCreationDate" runat="server" ControlToValidate="txtEndCreationDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                <cc1:CalendarExtender ID="ceEndCreationDate" runat="server" TargetControlID="txtEndCreationDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:DropDownList ID="ddlThird" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem>Operador</asp:ListItem>
                    <asp:ListItem Value="Otros Actores">Otros Actores</asp:ListItem>
                    <asp:ListItem>Socio</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="bt" runat="server" Text="Mostrar Reporte" />
            </td>
        </tr>
    </table>
    <br />
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" DisplayGroupTree="False" />
    <br />
    </form>
</asp:Content>
