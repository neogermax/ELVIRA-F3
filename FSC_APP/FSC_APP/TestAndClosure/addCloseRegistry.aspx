<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addCloseRegistry" Title="addCloseRegistry" Codebehind="addCloseRegistry.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" src="../js/General.js"></script>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="Registro de Cierre" ID="TabPanel1" Width="90%"
            TabIndex="0">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlidproject" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvidproject" runat="server" ControlToValidate="ddlidproject"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpidproject" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblprojectObjective" runat="server" Text="Objetivo del proyecto"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtprojectObjective" runat="server" TextMode="MultiLine" MaxLength="800"
                                    Enabled="False" Rows="5" Width="400px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpProjectObjective" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblclosingdate" runat="server" Text="Fecha de cierre"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtclosingdate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <cc1:CalendarExtender ID="cesclosingdate" runat="server" TargetControlID="txtclosingdate"
                                    Format="yyyy/MM/dd" Enabled="True">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvclosingdate" runat="server" 
                                    ControlToValidate="txtclosingdate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvclosingdate" runat="server" ErrorMessage="aaaa/mm/dd"
                                    Type="Date" ControlToValidate="txtclosingdate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpclosingdate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblweakness" runat="server" Text="Debilidad" ></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtweakness" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpweakness" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblopportunity" runat="server" Text="Oportunidad"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtopportunity" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpopportunity" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblstrengths" runat="server" Text="Fortaleza"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtstrengths" runat="server" Width="400px" MaxLength="300" TextMode="MultiLine" onkeypress="return textboxMultilineMaxNumber(this,300)"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpstrengths" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbllearningfornewprojects" runat="server" Text="Aprendizajes para nuevos Proyectos"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlearningfornewprojects" runat="server" Width="400px" MaxLength="300"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblHelplearningfornewprojects" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblgoodpractice" runat="server" Text="Marcar como buena Practica"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlgoodpractice" runat="server">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvgoodpractice" runat="server" ControlToValidate="ddlgoodpractice"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpgoodpractice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblregistrationdate" runat="server" Text="Fecha de Registro"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtregistrationdate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtregistrationdate"
                                    Format="yyyy/MM/dd" Enabled="True">
                                </cc1:CalendarExtender>
                                <asp:CompareValidator ID="cvregistrationdate" runat="server" ErrorMessage="aaaa/mm/dd"
                                    Type="Date" ControlToValidate="txtregistrationdate" Operator="DataTypeCheck"
                                    SetFocusOnError="True"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpregistrationdate" runat="server"></asp:Label>
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
                                <asp:RequiredFieldValidator ID="rfvenabled" runat="server" ControlToValidate="ddlenabled"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" />
                                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
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
        <cc1:TabPanel runat="server" HeaderText="Lista de los Proveedores (operadores)" ID="TabPanel2" Width="90%"
            TabIndex="1">
            <ContentTemplate>
                <div>
                    <table style="width: 90%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvthirdbyproject" AutoGenerateColumns="False" AllowPaging="True"
                                    runat="server">
                                    <Columns>                                        
                                        <asp:BoundField DataField="OPERATORNAME" HeaderText="Nombre" />
                                        <asp:HyperLinkField DataNavigateUrlFields="idoperator" DataNavigateUrlFormatString="../ResearchAndDevelopment/addSupplierEvaluation.aspx?op=add&IdThird={0}" HeaderText="Evaluar Proveedor" Text="Evaluar" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
