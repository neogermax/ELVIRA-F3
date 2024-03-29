﻿<%@ page language="VB" masterpagefile="~/MasterPages/Security.master" autoeventwireup="false" inherits="Administrator_Tools_TransactionStats, App_Web_dvz2jfrn" title="Untitled Page" theme="GattacaAdmin" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updUsers" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="height: 20px">
                        <asp:Label ID="lblTitle" runat="server" CssClass="cssLabelTitle" Text="Estadísticas del Sistema. Transacciones"></asp:Label>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px">
                        <asp:Label ID="lbUsuario" runat="server" Visible="false" Text="Usuario:"></asp:Label>&nbsp;
                        <asp:DropDownList ID="ddlUsuarios" runat="server" Visible="false" Width="250px" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="height: 20px" colspan="3">
                                    <asp:Label ID="Label3" runat="server" CssClass="cssLabelTitle" Text="Operaciones"></asp:Label>
                                </td>
                                <td align="center" rowspan="4" width="550" valign="top">
                                    <asp:Chart ID="chartOperaciones" runat="server" ImageType="Png" ImageLocation="~/TempImageFiles/"
                                        ImageStorageMode="UseImageLocation" BorderWidth="2" Palette="BrightPastel" BackColor="#D3DFF0"
                                        BorderDashStyle="Solid" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                        Visible="false" BorderColor="26, 59, 105" IsSoftShadows="False" Width="500px"
                                        Height="400px">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        <Legends>
                                            <asp:Legend Enabled="True" TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                                IsEquallySpacedItems="True" Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False"
                                                Docking="Bottom" Name="Default">
                                            </asp:Legend>
                                        </Legends>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent"
                                                BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                <Area3DStyle PointGapDepth="0" Rotation="10" Enable3D="False" Inclination="15" IsRightAngleAxes="False"
                                                    WallWidth="0" IsClustered="False" />
                                                <AxisY LineColor="64, 64, 64, 64">
                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisY>
                                                <AxisX LineColor="64, 64, 64, 64">
                                                    <LabelStyle Font="Trebuchet MS, 8.25pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="3">
                                    <asp:GridView ID="gvDataO" runat="server" Width="100%" CssClass="cssGrid">
                                        <Columns>
                                            <asp:BoundField DataFormatString="{0:d}" DataField="Fecha" HeaderText="Fecha" />
                                        </Columns>
                                        <RowStyle CssClass="cssItemStyle" />
                                        <HeaderStyle CssClass="cssHeaderStyle" />
                                        <AlternatingRowStyle CssClass="cssAlternatingItemStyle" />
                                        <FooterStyle Wrap="True" BackColor="Silver" Font-Bold="True" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    Fecha Inicial:
                                    <asp:TextBox ID="txtInicio" runat="server" Height="20px" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="txtInicio_MaskedEditExtender"
                                        ControlToValidate="txtInicio" Display="Dynamic" ErrorMessage="Fecha inválida"
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="grpO"></cc1:MaskedEditValidator>
                                    <cc1:CalendarExtender ID="Calendarextender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtInicio">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="txtInicio_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtInicio">
                                    </cc1:MaskedEditExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtInicio"
                                        WatermarkText="dd/mm/aaaa">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                                <td align="center" valign="top">
                                    Fecha Final:
                                    <asp:TextBox ID="txtFin" runat="server" Height="20px" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="txtFin_MaskedEditExtender"
                                        ControlToValidate="txtFin" Display="Dynamic" ErrorMessage="Fecha inválida" InvalidValueMessage="Fecha inválida"
                                        ValidationGroup="grpO"></cc1:MaskedEditValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtFin">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="txtFin_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFin">
                                    </cc1:MaskedEditExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtFin"
                                        WatermarkText="dd/mm/aaaa">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                                <td align="center" rowspan="2" valign="middle">
                                    <asp:Button ID="btnConsultarO" runat="server" Text="Consultar" ValidationGroup="grpO" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="2">
                                    Grupo:
                                    <asp:CheckBoxList ID="chklOperaciones" runat="server" RepeatDirection="Horizontal"
                                        RepeatColumns="3">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="height: 20px" colspan="3">
                                    <asp:Label ID="Label1" runat="server" CssClass="cssLabelTitle" Text="Tendencias Consolidadas"></asp:Label>
                                </td>
                                <td align="center" rowspan="3" width="550" valign="top">
                                    <asp:Chart ID="chartConsolidadas" runat="server" ImageType="Png" ImageLocation="~/TempImageFiles/"
                                        ImageStorageMode="UseImageLocation" BorderWidth="2" Palette="BrightPastel" BackColor="#D3DFF0"
                                        BorderDashStyle="Solid" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                                        Visible="false" BorderColor="26, 59, 105" IsSoftShadows="False" Width="500px">
                                        <Titles>
                                            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                                                ForeColor="26, 59, 105">
                                            </asp:Title>
                                        </Titles>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series ChartArea="ChartArea1" Name="Series1" BorderColor="64, 64, 64, 64" Color="180, 65, 140, 240"
                                                Font="Trebuchet MS, 8.25pt, style=Bold" ChartType="FastLine" CustomProperties="LabelsRadialLineSize=1, LabelStyle=outside">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent"
                                                BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                <Area3DStyle PointGapDepth="0" Rotation="10" Enable3D="False" Inclination="15" IsRightAngleAxes="False"
                                                    WallWidth="0" IsClustered="False" />
                                                <AxisY LineColor="64, 64, 64, 64">
                                                    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisY>
                                                <AxisX LineColor="64, 64, 64, 64">
                                                    <LabelStyle Font="Trebuchet MS, 8.25pt" />
                                                    <MajorGrid LineColor="64, 64, 64, 64" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top" colspan="3">
                                    <asp:GridView ID="gvDataC" runat="server" Width="100%" CssClass="cssGrid" ShowFooter="True">
                                        <Columns>
                                            <asp:BoundField DataFormatString="{0:d}" DataField="Fecha" HeaderText="Fecha" />
                                        </Columns>
                                        <RowStyle CssClass="cssItemStyle" />
                                        <HeaderStyle CssClass="cssHeaderStyle" />
                                        <AlternatingRowStyle CssClass="cssAlternatingItemStyle" />
                                        <FooterStyle Wrap="True" BackColor="Silver" Font-Bold="True" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    Fecha Inicial:
                                    <asp:TextBox ID="txtInicioC" runat="server" Height="20px" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="txtInicioC_MaskedEditExtender"
                                        ControlToValidate="txtInicioC" Display="Dynamic" ErrorMessage="Fecha inválida"
                                        InvalidValueMessage="Fecha inválida" ValidationGroup="grpC"></cc1:MaskedEditValidator>
                                    <cc1:CalendarExtender ID="txtInicioC_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" TargetControlID="txtInicioC">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="txtInicioC_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtInicioC">
                                    </cc1:MaskedEditExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtInicioC"
                                        WatermarkText="dd/mm/aaaa">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                                <td align="center" valign="top">
                                    Fecha Final:
                                    <asp:TextBox ID="txtFinC" runat="server" Height="20px" Width="90px"></asp:TextBox>
                                    <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="txtFinC_MaskedEditExtender"
                                        ControlToValidate="txtFinC" Display="Dynamic" ErrorMessage="Fecha inválida" InvalidValueMessage="Fecha inválida"
                                        ValidationGroup="grpC"></cc1:MaskedEditValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtFinC">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="txtFinC_MaskedEditExtender" runat="server" AcceptNegative="Left"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFinC">
                                    </cc1:MaskedEditExtender>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFinC"
                                        WatermarkText="dd/mm/aaaa">
                                    </cc1:TextBoxWatermarkExtender>
                                </td>
                                <td align="center" valign="middle">
                                    <asp:Button ID="btnConsultarC" runat="server" Text="Consultar" ValidationGroup="grpC" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
