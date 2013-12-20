<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportIndicatorInventory"
    Title="Untitled Page" Codebehind="reportIndicatorInventory.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblLevel" runat="server" Text="Nivel"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddllevel" runat="server">
                    <asp:ListItem Value="1.1">Linea Estrategica</asp:ListItem>
                    <asp:ListItem Value="1.2">Estrategia</asp:ListItem>
                    <asp:ListItem Value="2">Programa</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lbDate1" runat="server" Text="Fecha Inicial"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbDate1" runat="server"></asp:TextBox>
                       <cc1:CalendarExtender ID="ceDate1" runat="server" TargetControlID="tbDate1"
                    Format="yyyy/MM/dd"  PopupPosition="TopRight" >
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvDateStart" runat="server" ControlToValidate="tbDate1"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="lbDate2" runat="server" Text="Fecha Final"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbDate2" runat="server"></asp:TextBox>
                
                <asp:CompareValidator ID="cvDateEnd" runat="server" ControlToValidate="tbDate2"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                <asp:CompareValidator ID="cv2DateEnd" runat="server" ControlToCompare="tbDate1"
                    ControlToValidate="tbDate2" Display="Dynamic" ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final"
                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                
                <cc1:CalendarExtender ID="ceDate2" runat="server" TargetControlID="tbDate2"
                    Format="yyyy/MM/dd"  PopupPosition="TopRight" >
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lbUser" runat="server" Text="Responsable"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="bt" runat="server" Text="Mostrar Reporte" />
            </td>
        </tr>
    </table>
    <br />
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" />
    <br />
</asp:Content>
