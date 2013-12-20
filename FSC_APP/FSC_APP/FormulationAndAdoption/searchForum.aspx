<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchForum" title="searchForum" Codebehind="searchForum.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="projectname" Selected="True">Proyecto</asp:ListItem>
                                <asp:ListItem Value="subject">Tema</asp:ListItem>
                                <asp:ListItem Value="username">Autor</asp:ListItem>                                                             
                                <asp:ListItem Value="replycount">No. de respuestas</asp:ListItem>                                
                                <asp:ListItem Value="lastreplyusername">Último autor</asp:ListItem>
                                <asp:ListItem Value="lastreplycreatedate">Fecha de último mensaje</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
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
	                            <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="subject">Tema</asp:ListItem>
                                <asp:ListItem Value="username">Autor</asp:ListItem>                                                                
                                <asp:ListItem Value="replycount">No. de respuestas</asp:ListItem>                                
                                <asp:ListItem Value="lastreplyusername">Último autor</asp:ListItem>
                                <asp:ListItem Value="lastreplycreatedate">Fecha de último mensaje</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                            AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addForum.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" Visible="false" />
		                        <asp:HyperLinkField DataTextField="PROJECTNAME" DataNavigateUrlFields="id" DataNavigateUrlFormatString="projectForumPanel.aspx?id={0}" HeaderText="Proyecto - Ir al foro" /> 
		                        <asp:BoundField DataField="subject" HeaderText="Tema" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Autor" />	
		                        <asp:BoundField DataField="REPLYCOUNT" HeaderText="No. de respuestas" />		                        
		                        <asp:BoundField DataField="LASTREPLYUSERNAME" HeaderText="Último autor" />
		                        <asp:BoundField DataField="LASTREPLYCREATEDATE" HeaderText="Fecha de último mensaje" />
		                        <asp:BoundField DataField="createdate" HeaderText="Fecha de creación" />
		                        <asp:BoundField DataField="enabled" HeaderText="Estado" />
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
