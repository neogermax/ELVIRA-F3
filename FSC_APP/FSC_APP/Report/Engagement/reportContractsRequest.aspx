<%@ Page Title="Estado del proceso de solicitud y elaboración de contratos" Language="VB"
    MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    Inherits="FSC_APP.Report_Engagement_reportContractsRequest" Codebehind="reportContractsRequest.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportContractsRequest.aspx">
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1">Gerencia solicitante</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManagement" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblProject">Proyecto</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbDate1" runat="server" Text="Fecha solicitud entre"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateStartRequest" runat="server" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ceDateStartRequest" runat="server" TargetControlID="txtDateStartRequest"
                                        Format="yyyy/MM/dd" Enabled="True" PopupPosition="TopRight">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator ID="cvDateStartReg" runat="server" ControlToValidate="txtDateStartRequest"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbDate2" runat="server" Text="Y"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateEndRequest" runat="server" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="ceDateEndReg" runat="server" TargetControlID="txtDateEndRequest"
                                        Format="yyyy/MM/dd" PopupPosition="TopRight">
                                    </cc1:CalendarExtender>
                                    <asp:CompareValidator ID="cvDateEndRequest" runat="server" ControlToValidate="txtDateEndRequest"
                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                                        Display="Dynamic"></asp:CompareValidator>
                                    <asp:CompareValidator ID="cv2DateEndRequest" runat="server" ControlToCompare="txtDateStartRequest"
                                        ControlToValidate="txtDateEndRequest" Display="Dynamic" ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final"
                                        Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label3">Nombre contratista</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtContractorName" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="text-align: left">
            <td>
                <asp:Button ID="btnShow" runat="server" Text="Generar" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <CR:CrystalReportViewer ID="crvContracsRequest" runat="server" AutoDataBind="true"
                                DisplayGroupTree="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
