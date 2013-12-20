<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_reportClosedProjectsList" Codebehind="reportClosedProjectsList.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="reportClosedProjectsList.aspx">
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label2" Text="Linea Estrategica" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStrategicLine" runat="server" AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblProject" Text="Proyecto" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProject" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnShow" runat="server" Text="Consultar" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Width="100%">
                            <Columns>
                                <asp:BoundField DataField="IdKeyProject" HeaderText="IdKey. Proyecto" Visible="false" />
                                <asp:HyperLinkField Target="_blank" DataNavigateUrlFields="IdKeyProject" DataNavigateUrlFormatString="ReportCloseRegistry.aspx?idProject={0}"
                                    HeaderText="Proyecto" DataTextField="ProjectName" />
                                <asp:BoundField DataField="BeginDate" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MMM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClosingDate" HeaderText="Fecha Cierre" DataFormatString="{0:dd/MMM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCost" HeaderText="Valor" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GoodPractice" HeaderText="Es Buena Práctica">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
