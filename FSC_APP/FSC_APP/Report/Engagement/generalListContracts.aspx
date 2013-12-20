<%@ Page Title="Listado general de contratos" Language="VB" MasterPageFile="~/Master/mpAdmin.master"
    AutoEventWireup="false" Inherits="FSC_APP.Report_Engagement_GeneralListContracts" Codebehind="generalListContracts.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <form id="frm01" name="frm01" method="post" action="generalListContracts.aspx">
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" Text="Gerencia" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManagement" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="Label2" Text="LINEA ESTRATEGICA" />
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
                                <td>
                                    <asp:Label runat="server" ID="Label4" Text="Estado del contrato" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlContractState" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbDate1" runat="server" Text="Vigencia presupuestal"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtEffectiveBudget" runat="server" MaxLength="10" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbDate2" runat="server" Text="Socio / Contratista"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtContractorName" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label3">Supervisor</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtSupervisor" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
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
                                <asp:BoundField DataField="ContractNumber" HeaderText="Número del contrato" Visible="false" />
                                <asp:BoundField DataField="RequestNumber" HeaderText="Número de la solicitud" Visible="false" />
                                <asp:HyperLinkField DataNavigateUrlFields="ContractNumber,RequestNumber" DataNavigateUrlFormatString="generalListContracts.aspx?op=report&contractNumber={0}&requestNumber={1}"
                                    HeaderText="Número del contrato" DataTextField="ContractNumber" />
                                <asp:BoundField DataField="ManagementName" HeaderText="Gerencia" />
                                <asp:BoundField DataField="StrategicLineName" HeaderText="Linea Estrategica" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Proyecto" />
                                <asp:BoundField DataField="ContractorName" HeaderText="Socio / Contratista" />
                                <asp:BoundField DataField="ContractValue" HeaderText="Valor Total" 
                                    DataFormatString="{0:c}" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectContract" HeaderText="Objeto" />
                                <asp:BoundField DataField="ContractState" HeaderText="Estado del contrato" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upReport" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <CR:CrystalReportViewer ID="crvGeneralListContracts" runat="server" AutoDataBind="true"
                                        DisplayGroupTree="False" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
