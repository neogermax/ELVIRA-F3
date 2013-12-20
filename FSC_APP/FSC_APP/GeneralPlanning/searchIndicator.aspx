<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchIndicator" title="searchIndicator" Codebehind="searchIndicator.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="description">Descripción</asp:ListItem>
                                <asp:ListItem Value="type">Tipo</asp:ListItem>
                                <asp:ListItem Value="levelname">Nivel</asp:ListItem>
                                <asp:ListItem Value="entityname">Entidad</asp:ListItem>                               
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha creación</asp:ListItem>
							</asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <%--<asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
	                            <asp:ListItem Value="id">Id</asp:ListItem>
	                            <asp:ListItem Value="code">Código</asp:ListItem>
	                            <asp:ListItem Value="description">Descripción</asp:ListItem>
	                            <asp:ListItem Value="type">Tipo</asp:ListItem>
	                            <asp:ListItem Value="levelindicator">Nivel</asp:ListItem>
                                <asp:ListItem Value="entityname">Entidad</asp:ListItem>                               
	                            <asp:ListItem Value="enabled">Estado</asp:ListItem>
	                            <asp:ListItem Value="username">Usuario</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha creación</asp:ListItem>
							</asp:DropDownList>--%>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addIndicator.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addAccumulationIndicatorSet.aspx?ind={0}" HeaderText="Operaciones" Text="Configurar Acumulación" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
		                        <asp:BoundField DataField="code" HeaderText="Código" />
		                        <asp:BoundField DataField="description" HeaderText="Descripción" />
		                        <asp:BoundField DataField="TYPE" HeaderText="Tipo" />
		                        <asp:BoundField DataField="LEVELNAME" HeaderText="Nivel" />
		                        <asp:BoundField DataField="ENTITYNAME" HeaderText="Entidad" />
		                        <asp:BoundField DataField="goal" HeaderText="Meta" />
		                        <asp:BoundField DataField="enabled" HeaderText="Estado" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
		                        <asp:BoundField DataField="RESPONSABLENAME" HeaderText="Nombre Responsable" />
		                        <asp:BoundField DataField="createdate" HeaderText="Fecha Creacion" />
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
