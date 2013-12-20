<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addRisk" Title="addRisk" Codebehind="addRisk.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <cc2:TabContainer ID="TabContainer1" runat="server" Height="500px" Width="810px"
        ActiveTabIndex="1" ScrollBars="Vertical">
        <cc2:TabPanel runat="server" HeaderText="Datos generales del riesgo" ID="TabPanel1"
            Width="600" TabIndex="0">
            <ContentTemplate>
                <div>
                    <table style="width: 100%">
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtid" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"
                                        ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcode" runat="server" MaxLength="50" Width="400px" AutoCompleteType="Disabled"
                                        AutoPostBack="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpcode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblname" runat="server" Text="Nombre/Descripción"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtname" runat="server" MaxLength="255" Width="400px" Height="70px"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpname" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdescription" runat="server" MaxLength="255" Width="400px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpdescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblwhatcanhappen" runat="server" Text="Que puede suceder"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtwhatcanhappen" runat="server" MaxLength="500" Width="400px" Height="70px"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpwhatcanhappen" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblriskimpact" runat="server" Text="Impacto del riesgo"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlriskimpact" runat="server">
                                        <asp:ListItem Value="1">Alta</asp:ListItem>
                                        <asp:ListItem Value="2">Media</asp:ListItem>
                                        <asp:ListItem Value="3">Baja</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvriskimpact" runat="server" ControlToValidate="ddlriskimpact"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpriskimpact" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblocurrenceprobability" runat="server" Text="Probabilidad de la ocurrencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlocurrenceprobability" runat="server">
                                        <asp:ListItem Value="1">Alta</asp:ListItem>
                                        <asp:ListItem Value="2">Media</asp:ListItem>
                                        <asp:ListItem Value="3">Baja</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvocurrenceprobability" runat="server" ControlToValidate="ddlocurrenceprobability"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpocurrenceprobability" runat="server"></asp:Label>
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
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="txtiduser" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcreatedate" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                                        ErrorMessage="*" ValidationGroup="riskForm"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" ValidationGroup="riskForm" />
                                    <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="riskForm" />
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" Text="Eliminar" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancelar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnConfirmDelete" runat="server" CausesValidation="False" Text="Eliminar" />
                                    <asp:Button ID="btnCancelDelete" runat="server" CausesValidation="False" Text="Cancelar" />
                                    &nbsp;<asp:Label ID="lblDelete" runat="server" ForeColor="Red" Text="Esta seguro que desea eliminar el registro?"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                  <%--  <asp:Label ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <%--<asp:GridView ID="gvVersion" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                            <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                            <asp:BoundField DataField="code" HeaderText="Codigo" />
                                            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addRisk.aspx?op=show&amp;id={0}&consultLastVersion=false"
                                                DataTextField="Name" HeaderText="Nombre" Target="_blank" />
                                        </Columns>
                                    </asp:GridView>--%>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
        <cc2:TabPanel runat="server" HeaderText="Componentes del proyecto" ID="TabPanel2" Width="600"
            TabIndex="1">
            <ContentTemplate>
                <div>
                <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                                <td>
                                    <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlidproject" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpidproject" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblidobjective" runat="server" Text="Objetivo"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlidobjective" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="lblHelpidobjective" runat="server"></asp:Label>
                                </td>
                            </tr>
                </table>
                    <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tbody>
                            <tr>
                                <td colspan="2">
                                    <cc1:DoubleListBox ID="dlbComponent" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="90%">
                        <tr>
                            <td>
                                <asp:Label ID="lblHelpProgramComponentByProject" runat="server" Text="Recuerde hacer click en guardar para efectuar los cambios"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc2:TabPanel>
    </cc2:TabContainer>
</asp:Content>
