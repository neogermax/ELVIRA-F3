<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addIndicator" Title="addIndicator" Codebehind="addIndicator.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">    
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" 
        ActiveTabIndex="1">
        <cc1:TabPanel runat="server" HeaderText="Datos generales del indicador" ID="TabPanel1"
            Width="90%" TabIndex="0">
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"
                                    ValidationGroup="IndicatorForm"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLevel" runat="server" Text="Nivel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1.1">Primer Nivel - Linea Estrategica</asp:ListItem>
                                    <asp:ListItem Value="1.2">Primer Nivel - Estrategia</asp:ListItem>
                                    <asp:ListItem Value="2">Segundo Nivel - Programa</asp:ListItem>
                                    <asp:ListItem Value="3">Tercer Nivel - Proyecto</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblHelplevel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblentity" runat="server" Text="Entidad"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlentity" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpentity" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" 
                                    AutoPostBack="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                    ErrorMessage="*" ValidationGroup="IndicatorForm"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="255" 
                                    TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ControlToValidate="txtdescription"
                                    ErrorMessage="*" ValidationGroup="IndicatorForm"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpdescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbltype" runat="server" Text="Tipo"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddltype" runat="server">
                                    <asp:ListItem Text="Beneficiarios" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Capacidad instalada" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Gestión del conocimiento" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblHelptype" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblgoal" runat="server" Text="Meta"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtgoal" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpgoal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblgreenvalue" runat="server" Text="Valor verde"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtgreenvalue" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpgreenvalue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblyellowvalue" runat="server" Text="Valor amarillo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtyellowvalue" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpyellowvalue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblredvalue" runat="server" Text="Valor rojo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtredvalue" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpredvalue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblassumptions" runat="server" Text="Supuestos"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtassumptions" runat="server" Width="400px" MaxLength="250"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpassumptions" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblsourceverification" runat="server" Text="Fuente de verificación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtsourceverification" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpsourceverification" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlenabled" runat="server">
                                    <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr>
            <td>
                <asp:Label ID="lblresponsible" runat="server" Text="Responsable"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidresponsible" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidresponsible" runat="server" ControlToValidate="ddlidresponsible"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpresponsible" runat="server" Text=""></asp:Label>
            </td>
        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbliduser" runat="server" Text="Usuario creación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                    ErrorMessage="*" ValidationGroup="IndicatorForm"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha creación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                                    ErrorMessage="*" ValidationGroup="IndicatorForm"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" ValidationGroup="IndicatorForm" />
                                <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="IndicatorForm" />
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Fechas de medición para el indicador" ID="TabPanel2"
            Width="600" TabIndex="0">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upDate" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <cc1:CalendarExtender ID="cesDate" runat="server" TargetControlID="txtDate" Format="yyyy/MM/dd"
                                            Animated="true">
                                        </cc1:CalendarExtender>
                                        <br />
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    Fecha Medición</td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" 
                                                        ControlToValidate="txtDate" ErrorMessage="*" 
                                                        ValidationGroup="IndicatorFormDate"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="txtDate" 
                                                        ErrorMessage="yyyy/MM/dd" Operator="DataTypeCheck" SetFocusOnError="True" 
                                                        Type="Date" ValidationGroup="IndicatorFormDate"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Medición Esperada</td>
                                                <td>
                                                    <asp:TextBox ID="txtMeasure" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMedicionEsperada" runat="server" 
                                                        ControlToValidate="txtMeasure" Display="Dynamic" ErrorMessage="*" 
                                                        ValidationGroup="IndicatorFormDate"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cvNumeric" runat="server" 
                                                        ControlToValidate="txtMeasure" Display="Dynamic" 
                                                        ErrorMessage="La medición debe ser numérica" Operator="DataTypeCheck" 
                                                        Type="Double" ValidationGroup="IndicatorFormDate"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Tipo de Medición</td>
                                                <td>
                                                    <asp:DropDownList ID="dpMeasureType" runat="server">
                                                        <asp:ListItem>Porcentaje</asp:ListItem>
                                                        <asp:ListItem Value="Numerico">Numérico</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:Button ID="btnAddDate" runat="server" Text="Agregar Fecha" 
                                                        ValidationGroup="IndicatorFormDate" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="showDate" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvDate" AutoGenerateColumns="False" AllowPaging="True" runat="server">
                                            <Columns>
                                                <asp:CommandField SelectText="Quitar" ShowSelectButton="True" />
                                                <asp:BoundField DataField="measurementdate" HeaderText="Fechas" />
                                                  <asp:BoundField DataField="measure" HeaderText="Medición" />
                                                    <asp:BoundField DataField="measuretype" HeaderText="Tipo de medición" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHelpdatelist" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
