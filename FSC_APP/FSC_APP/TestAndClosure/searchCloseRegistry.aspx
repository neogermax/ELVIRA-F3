<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchCloseRegistry" Title="searchCloseRegistry" Codebehind="searchCloseRegistry.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="closingdate">Fecha de cierre</asp:ListItem>
                                <asp:ListItem Value="goodpractice">Buena práctica</asp:ListItem>
                                <asp:ListItem Value="registrationdate">Fecha de registro</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
                                <asp:ListItem Value="id">Id</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="closingdate">Fecha de cierre</asp:ListItem>
                                <asp:ListItem Value="goodpractice">Buena práctica</asp:ListItem>
                                <asp:ListItem Value="registrationdate">Fecha de registro</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upSubTitle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Width="100%" PageSize="10">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addCloseRegistry.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
                                <asp:BoundField DataField="closingdate" HeaderText="Fecha de cierre" />
                                <asp:BoundField DataField="GOODPRACTICETEXT" HeaderText="Buena práctica" />
                                <asp:BoundField DataField="registrationdate" HeaderText="Fecha de registro" />
                                <asp:BoundField DataField="ENABLEDTEXT" HeaderText="Estado" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
