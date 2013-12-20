<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchActivity" Title="searchActivity" Codebehind="searchActivity.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="title">Título</asp:ListItem>
                                <asp:ListItem Value="number">Número</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="objectivename">Objetivo</asp:ListItem>
                                <asp:ListItem Value="componentname">Componente</asp:ListItem>
                                <asp:ListItem Value="criticalpath">Pertenece a ruta crítica</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
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
                                <asp:ListItem Value="title">Título</asp:ListItem>
                                <asp:ListItem Value="number">Número</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="objectivename">Objetivo</asp:ListItem>
                                <asp:ListItem Value="componentname">Componente</asp:ListItem>
                                <asp:ListItem Value="criticalpath">Pertenece a ruta crítica</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
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
                                <asp:HyperLinkField DataNavigateUrlFields="idkey" DataNavigateUrlFormatString="addActivity.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="number" HeaderText="Número" />
                                <asp:BoundField DataField="title" HeaderText="Título" />
                                <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
                                <asp:BoundField DataField="OBJECTIVENAME" HeaderText="Objetivo" />
                                <asp:BoundField DataField="COMPONENTNAME" HeaderText="Componente" />
                                <asp:BoundField DataField="CRITICALPATH" HeaderText="Pertenece a Ruta Crítica" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de creación" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
