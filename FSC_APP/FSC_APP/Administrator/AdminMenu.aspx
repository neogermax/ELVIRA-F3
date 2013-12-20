<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Administrator_AdminMenu" Title="Gattaca e-Project - v8" Codebehind="AdminMenu.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
            <table width="100%">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rblSearch" runat="server" CssClass="cssTextBoxForm" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="id">Id</asp:ListItem>
                                        <asp:ListItem Value="TextField">Nombre</asp:ListItem>
                                        <asp:ListItem Value="URL">Direccion</asp:ListItem>
                                        <asp:ListItem Value="Enabled">Habilitado</asp:ListItem>
                                        <asp:ListItem Value="sortOrder">Orden</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 130px">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBoxForm"></asp:TextBox>
                                </td>
                                <td>
                                   <asp:Button
                                        ID="btnSearch" runat="server" CssClass="cssButton" Text="Buscar" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlSort" runat="server">
                                        <asp:ListItem Value="id">Id</asp:ListItem>
                                        <asp:ListItem Value="textField">Nombre</asp:ListItem>
                                        <asp:ListItem Value="url">Dirección</asp:ListItem>
                                        <asp:ListItem Value="enabled">Habilitado</asp:ListItem>
                                        <asp:ListItem Value="sortOrder">Orden</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            PageSize="15" Width="100%">
                            <FooterStyle CssClass="cssFooterStyle" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="iId" DataNavigateUrlFormatString="~/Administrator/AddMenu.aspx?Op=Edit&amp;IdMenu={0}"
                                    HeaderText="Nombre" DataTextField="sTextField">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="sURL" HeaderText="Direccion" />
                                <asp:BoundField DataField="sEnabled" HeaderText="Habilitado" />
                                <asp:BoundField DataField="iSortOrden" HeaderText="Orden" />
                                <asp:HyperLinkField DataNavigateUrlFields="iId" DataNavigateUrlFormatString="~/Administrator/AdminMenuHierarchy.aspx?IdMenu={0}"
                                    HeaderText="Hijos" Text="Ver">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="iId" DataNavigateUrlFormatString="~/Administrator/adminMenusByUserGroup.aspx?IdMenu={0}"
                                    HeaderText="Grupos de Usuario" Text="Ver">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:HyperLinkField>
                            </Columns>
                            <RowStyle CssClass="cssItemStyle" />
                            <PagerStyle CssClass="cssPager" />
                            <HeaderStyle CssClass="cssHeaderStyle" />
                            <AlternatingRowStyle CssClass="cssAlternatingItemStyle" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" 
                            Text="La búsqueda no produjo Resultados" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td >
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" CssClass="cssButton" Text="Crear Menu" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnUpdateMenu" runat="server" Text="Actualizar Menus" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
</asp:Content>
