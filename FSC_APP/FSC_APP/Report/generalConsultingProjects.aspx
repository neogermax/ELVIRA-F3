<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_generalConsultingProjects" Codebehind="generalConsultingProjects.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="90%">
                <tr>
                    <td>
                        <asp:Label ID="lbUser" runat="server" Text="Linea Estrategica"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStrategicLines" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Nombre Proyecto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjectName" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Departamento"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepto" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Municipio"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCities" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Población Objetivo"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTargetPopulation" runat="server">
                            <asp:ListItem Value="Personas con discapacidad">Personas con discapacidad</asp:ListItem>
                            <asp:ListItem Value="Personas Mayores">Personas Mayores</asp:ListItem>
                            <asp:ListItem Value="Otras">Otras</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Operador"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOperator" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Valor Total Entre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartTotalValue" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="cvStartTotalValue" runat="server" ControlToValidate="txtStartTotalValue"
                            ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                            Type="Double" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label9" runat="server" Text="Y"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndTotalValue" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="cvEndTotalValue" runat="server" ControlToValidate="txtEndTotalValue"
                            ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                            Type="Double" Display="Dynamic"></asp:CompareValidator>
                        <asp:CompareValidator ID="cv2EndTotalValue" runat="server" ControlToCompare="txtStartTotalValue"
                            ControlToValidate="txtEndTotalValue" Display="Dynamic" ErrorMessage="El valor total inicial no puede ser superior al valor total final."
                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Valor Aporte FSC Entre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartContributionValue" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartContributionValue"
                            ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                            Type="Double" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label10" runat="server" Text="Y"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndContributionValue" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                        <asp:CompareValidator ID="cvEndContributionValue" runat="server" ControlToValidate="txtEndContributionValue"
                            ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                            Type="Double" Display="Dynamic"></asp:CompareValidator>
                        <asp:CompareValidator ID="cv2EndContributionValue" runat="server" ControlToCompare="txtStartContributionValue"
                            ControlToValidate="txtEndContributionValue" Display="Dynamic" ErrorMessage="El valor inicial no puede ser superior al valor final."
                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Double"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbleffectivebudget" runat="server" Text="Vigencia Presupuestal"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddleffectivebudget" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Estado" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEstate" runat="server" Visible="false">
                            <asp:ListItem>Planeación General</asp:ListItem>
                            <asp:ListItem>Investigación y Desarrollo</asp:ListItem>
                            <asp:ListItem>Formulación y Aprobación</asp:ListItem>
                            <asp:ListItem>Planeación Operativa</asp:ListItem>
                            <asp:ListItem>Ejecución</asp:ListItem>
                            <asp:ListItem>Evaluación y Cierre</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbDate1" runat="server" Text="Fecha Cierre Entre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartClosingDate" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:CompareValidator ID="cvStartClosingDate" runat="server" ControlToValidate="txtStartClosingDate"
                            ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                            Display="Dynamic"></asp:CompareValidator>
                        <cc1:CalendarExtender ID="ceStartClosingDate" runat="server" TargetControlID="txtStartClosingDate"
                            Format="yyyy/MM/dd" Enabled="True">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbDate2" runat="server" Text="Y"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndClosingDate" runat="server" MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceEndClosingDate" runat="server" TargetControlID="txtEndClosingDate"
                            Format="yyyy/MM/dd">
                        </cc1:CalendarExtender>
                        <asp:CompareValidator ID="cvEndClosingDate" runat="server" ControlToValidate="txtEndClosingDate"
                            ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                            Display="Dynamic"></asp:CompareValidator>
                        <asp:CompareValidator ID="cv2EndClosingDate" runat="server" ControlToCompare="txtStartClosingDate"
                            ControlToValidate="txtEndClosingDate" Display="Dynamic" ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final"
                            Operator="GreaterThanEqual" SetFocusOnError="True"></asp:CompareValidator>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:Button ID="bt" runat="server" Text="Consultar" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IdKey" HeaderText="IdKey" Visible="true" />
                        <asp:BoundField DataField="Name" HeaderText="Nombre Proyecto" />
                        <asp:BoundField DataField="StrategicLineName" HeaderText="Linea Estrategica" />
                        <asp:BoundField DataField="Population" HeaderText="Población" />
                        <asp:BoundField DataField="BeginDate" HeaderText="Fecha Inicio">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ClosingDate" HeaderText="Fecha Cierre">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FSCContribution" HeaderText="Valor Aporte FSC" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalCost" HeaderText="Valor Total" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="" HeaderText="Estado" Visible="false" />
                        <asp:TemplateField HeaderText="Reporte">
                            <EditItemTemplate />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlReport" runat="server">
                                    <asp:ListItem Value="FormulationAndAdoption/reportBasicProjectData.aspx">Datos Básicos</asp:ListItem>
                                    <asp:ListItem Value="FormulationAndAdoption/reporMatrixIndicator.aspx">Matriz de indicadores</asp:ListItem>
                                    <asp:ListItem Value="reportExecutionPlan.aspx">Plan de Ejecución</asp:ListItem>
                                    <asp:ListItem Value="FormulationAndAdoption/reportProjectChronogram.aspx">Cronograma de actividades</asp:ListItem>
                                    <asp:ListItem Value="FormulationAndAdoption/reportRiskMatrix.aspx">Matriz de riesgos</asp:ListItem>
                                    <asp:ListItem Value="OperationalPlanning/reportRecruitmentPlan.aspx">Plan de contratación</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnView" runat="server" OnClick="lnkBtnView_Click">Ver</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
