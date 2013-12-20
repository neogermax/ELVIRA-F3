<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchRisk" title="searchRisk" Codebehind="searchRisk.aspx.vb" %>
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
                                <asp:ListItem Value="name">Nombre/Descripción</asp:ListItem>
                                <asp:ListItem Value="description">Descripción</asp:ListItem>
                                <asp:ListItem Value="componentname">Componente</asp:ListItem>                                 
                                <asp:ListItem Value="riskimpact">Impacto del riesgo</asp:ListItem>                                
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>                                
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
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
                            <asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
	                            <asp:ListItem Value="id">Id</asp:ListItem>
	                            <asp:ListItem Value="code">Código</asp:ListItem>
	                            <asp:ListItem Value="name">Nombre/Descripción</asp:ListItem>
	                            <asp:ListItem Value="description">Descripción</asp:ListItem> 	                            
	                            <asp:ListItem Value="riskimpact">Impacto del riesgo</asp:ListItem>
	                            <asp:ListItem Value="enabled">Estado</asp:ListItem>
	                            <asp:ListItem Value="iduser">Usuario</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addRisk.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
		                        <asp:BoundField DataField="code" HeaderText="Código" />
		                        <asp:BoundField DataField="name" HeaderText="Nombre/Descripción" />
		                        <asp:BoundField DataField="description" HeaderText="Descripción" />	
		                        <asp:BoundField DataField="COMPONENTLISTTEXT" HeaderText="Componentes" HtmlEncode = "false"/>			                       
		                        <asp:BoundField DataField="riskimpact" HeaderText="Impacto del Riesgo" />		                        
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
