<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchDocuments" title="searchDocuments" Codebehind="searchDocuments.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="title">Título</asp:ListItem>
                                <asp:ListItem Value="description">Descripción</asp:ListItem>
                                <asp:ListItem Value="editedforname">Editado por</asp:ListItem>
                                <asp:ListItem Value="visibilitylevelname">Nivel visibilidad</asp:ListItem>
                                <asp:ListItem Value="documenttypename">Tipo documento</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="entityName">Entidad origen</asp:ListItem>                                
                                <asp:ListItem Value="attachfile">Anexo</asp:ListItem>
                                <asp:ListItem Value="projectName">Proyecto</asp:ListItem>
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
	                            <asp:ListItem Value="title">Título</asp:ListItem>
	                            <asp:ListItem Value="description">Descripción</asp:ListItem>
	                            <asp:ListItem Value="editedforname">Editado por</asp:ListItem>
	                            <asp:ListItem Value="visibilitylevelname">Nivel visibilidad</asp:ListItem>
	                            <asp:ListItem Value="documenttypename">Tipo documento</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha</asp:ListItem>
	                            <asp:ListItem Value="username">Usuario</asp:ListItem>
	                            <asp:ListItem Value="entityName">Entidad origen</asp:ListItem>	                            
	                            <asp:ListItem Value="attachfile">Anexo</asp:ListItem>
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
                            AllowPaging="True" Width="100%">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addDocuments.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="title" HeaderText="Título" />
		                        <asp:BoundField DataField="description" HeaderText="Descripción" />
		                        <asp:BoundField DataField="EDITEDFORNAME" HeaderText="Editado por" />
		                        <asp:BoundField DataField="VISIBILITYLEVELNAME" 
                                    HeaderText="Nivel visibilidad" />
		                        <asp:BoundField DataField="DOCUMENTTYPENAME" HeaderText="Tipo documento" />
		                        <asp:BoundField DataField="createdate" HeaderText="Fecha" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
		                        <asp:BoundField DataField="ENTITYNAME" HeaderText="Entidad origen" />	
		                                          
		                        <asp:HyperLinkField DataNavigateUrlFields="attachfile" 
                                    DataTextField="attachfile" HeaderText="Anexo" Target="_blank" />
                               <asp:BoundField DataField="ProjectName" HeaderText="Nombre Proyecto" />	
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
