<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_reportIdeaInventory" Codebehind="reportIdeaInventory.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table width="70%">
        <tr>
            <td>
                <asp:Label ID="lbDate1" runat="server" Text="Fecha Registro Inicial"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateStartReg" runat="server" MaxLength="10"></asp:TextBox>
                <asp:CompareValidator ID="cvDateStartReg" runat="server" ControlToValidate="txtDateStartReg"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                <cc1:CalendarExtender ID="ceDateStartReg" runat="server" TargetControlID="txtDateStartReg"
                    Format="yyyy/MM/dd" Enabled="True" PopupPosition="TopRight">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lbDate2" runat="server" Text="Fecha Registro Final"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateEndReg" runat="server" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="ceDateEndReg" runat="server" TargetControlID="txtDateEndReg"
                    Format="yyyy/MM/dd" PopupPosition="TopRight">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvDateEndReg" runat="server" ControlToValidate="txtDateEndReg"
                    ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"
                    Display="Dynamic"></asp:CompareValidator>
                <asp:CompareValidator ID="cv2DateEndReg" runat="server" ControlToCompare="txtDateStartReg"
                    ControlToValidate="txtDateEndReg" Display="Dynamic" ErrorMessage="El valor de la fecha inicial no puede ser superior al valor de la fecha final"
                    Operator="GreaterThanEqual" Type="Date" SetFocusOnError="True"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Nombre Idea"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtIdeaName" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbUser" runat="server" Text="Linea Estrategica"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStrategicLines" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Fuente"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSource" runat="server">
                    <asp:ListItem>Investigación</asp:ListItem>
                    <asp:ListItem>Propuesta de un Socio</asp:ListItem>
                    <asp:ListItem>Propuesta de un Operador</asp:ListItem>
                    <asp:ListItem>Convocatoria</asp:ListItem>
                    <asp:ListItem>FSC</asp:ListItem>
                    <asp:ListItem>Operador</asp:ListItem>
                    <asp:ListItem>Tercero</asp:ListItem>
                </asp:DropDownList>
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
                <asp:Label ID="Label6" runat="server" Text="Municipios"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCities" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Costo Inicial"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtStartCost" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                <asp:CompareValidator ID="cvStartCost" runat="server" ControlToValidate="txtStartCost"
                    ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Double" Display="Dynamic"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Costo Final"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEndCost" runat="server" MaxLength="15" Width="120px"></asp:TextBox>
                <asp:CompareValidator ID="cvEndCost" runat="server" ControlToValidate="txtEndCost"
                    ErrorMessage="Valor numérico" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Double" Display="Dynamic"></asp:CompareValidator>
                <asp:CompareValidator ID="cv2EndCost" runat="server" ControlToCompare="txtStartCost"
                    ControlToValidate="txtEndCost" Display="Dynamic" ErrorMessage="El valor del costo inicial no puede ser superior al valor del costo final."
                    Operator="GreaterThanEqual" Type="Double" SetFocusOnError="True"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Estado" Visible="true"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Visible="true">
                    <asp:ListItem>En Proceso</asp:ListItem>
                    <asp:ListItem>Aprobado</asp:ListItem>
                    <asp:ListItem>En banco de ideas</asp:ListItem>
                    <asp:ListItem Text="Todos" Value="" Selected="True" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>
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
                        <asp:BoundField DataField="IdIdea" HeaderText="Id" Visible="false" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Fecha de Registro">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="IdIdea" DataNavigateUrlFormatString="reportIdeaInventory.aspx?op=report&id={0}"
                            HeaderText="Nombre Idea" DataTextField="Name" />
                        <asp:BoundField DataField="StrategicLineName" HeaderText="Linea Estrategica al que pertenece" />
                        <asp:BoundField DataField="Source" HeaderText="Fuente" />
                        <asp:BoundField DataField="CityName" HeaderText="Ubicación" />
                        <asp:BoundField DataField="Cost" HeaderText="Costo" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="State" HeaderText="Estado" Visible="true" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="true" 
                    DisplayGroupTree="False" HyperlinkTarget="_blank" />
            </td>
        </tr>
    </table>
</asp:Content>
