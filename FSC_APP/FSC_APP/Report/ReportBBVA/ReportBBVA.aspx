<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_ReportBBVA_ReportBBVA" Codebehind="ReportBBVA.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <form id="frm01" name="frm01" method="post" action="ReportBBVA.aspx">
       <br />
    <br />
    <table style="width: 69%">
        <tr>
            <td colspan="6" style="text-align: center">
                <b>Fecha Creación</b></td>
        </tr>
        <tr>
            <td>
                Desde:</td>
            <td colspan="3">
                <asp:TextBox ID="txtStartCreationDate" runat="server" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartCreationDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvStartCreationDate" runat="server" ControlToValidate="txtStartCreationDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                </td>
            <td>
                Hasta:</td>
            <td>
                <asp:TextBox ID="txtEndCreationDate" runat="server" MaxLength="10" 
                    AutoCompleteType="Disabled"></asp:TextBox>
                <cc1:CalendarExtender ID="txtEndCreationDate_CalendarExtender" runat="server" TargetControlID="txtEndCreationDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvStartEndCreationDate" runat="server" ControlToValidate="txtEndCreationDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                     <asp:CompareValidator ID="cvFechaIncioFechaFin0" runat="server" ControlToCompare="txtStartCreationDate"
                                        ControlToValidate="txtEndCreationDate" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
               
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <b>Fecha Inicio</b></td>
        </tr>
        <tr>
            <td>
                Desde:</td>
            <td colspan="3">
                <asp:TextBox ID="txtStartBeginDate" runat="server" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="ceEndCreationDate" runat="server" TargetControlID="txtStartBeginDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvStartBeginDate" runat="server" ControlToValidate="txtStartBeginDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                </td>
            <td>
                Hasta:</td>
            <td>
                <asp:TextBox ID="txtStartEndDate" runat="server" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="txtStartEndDate_CalendarExtender" runat="server" TargetControlID="txtStartEndDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvStartEndDate" runat="server" ControlToValidate="txtStartEndDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartBeginDate"
                                        ControlToValidate="txtStartEndDate" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <b>Fecha Terminación</b></td>
        </tr>
        <tr>
            <td>
                Desde:</td>
            <td colspan="3">
                <asp:TextBox ID="txtEndStartDate" runat="server"></asp:TextBox>
                      <cc1:CalendarExtender ID="txtEndStartDate_CalendarExtender" 
                    runat="server" TargetControlID="txtEndStartDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvEndStartDate" runat="server" ControlToValidate="txtEndStartDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                      </td>
            <td>
                Hasta:</td>
            <td>
                <asp:TextBox ID="txtEndEndDate" runat="server"></asp:TextBox>
                      <cc1:CalendarExtender ID="ceEndTime" runat="server" TargetControlID="txtEndEndDate"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvEndEndDate" runat="server" ControlToValidate="txtEndEndDate"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtEndStartDate"
                                        ControlToValidate="txtEndEndDate" Display="Dynamic" ErrorMessage="El valor de la fecha inicio no puede ser superior al valor de la fecha finalización"
                                        Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                      </td>
        </tr>
        <tr>
            <td>
                Situación Laboral:</td>
            <td>
                <asp:DropDownList ID="ddlLaboralSituation" runat="server">
                    <asp:ListItem>Todos</asp:ListItem>
                    <asp:ListItem>Asalariado Declarante</asp:ListItem>
                    <asp:ListItem>Asalariado No Declarante</asp:ListItem>
                    <asp:ListItem>Pensionado Declarante</asp:ListItem>
                    <asp:ListItem>Pensionado No Declarante</asp:ListItem>
                    <asp:ListItem>Independiente Declarante</asp:ListItem>
                    <asp:ListItem>Independiente No Declarant</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Tipo Cliente:</td>
            <td>
                <asp:DropDownList ID="ddlKindofClient" runat="server">
                    <asp:ListItem>Todos</asp:ListItem>
                    <asp:ListItem Value="Cliente BBVA ">Cliente BBVA</asp:ListItem>
                    <asp:ListItem>No cliente</asp:ListItem>
                    <asp:ListItem>Cliente/No Cliente prioritario</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                Actividad:</td>
            <td>
                <asp:DropDownList ID="ddlActivity" runat="server">
                    <asp:ListItem>Todos</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Radicado:</td>
            <td>
                <asp:TextBox ID="txtRadicado" runat="server"></asp:TextBox>
            </td>
            <td>
                Nombre Solicitante:</td>
            <td>
                <asp:TextBox ID="txtSolicitante" runat="server"></asp:TextBox>
            </td>
            <td>
                Identificación del Cliente:</td>
            <td>
                <asp:TextBox ID="txtIdentificacion" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="text-align: center">
                <asp:Button ID="btnClick" runat="server" Text="Reporte" />
            </td>
        </tr>
    </table>
&nbsp;<br />
    <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" DisplayGroupTree="False" />
    <br />
    </form>
</asp:Content>

