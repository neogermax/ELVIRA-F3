<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchSubActivityInformationRegistry" title="searchSubActivityInformationRegistry" Codebehind="searchSubActivityInformationRegistry.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="subactivityname">Subactividad</asp:ListItem>
                                <asp:ListItem Value="description">Descripción</asp:ListItem>
                                <asp:ListItem Value="begindate">Fecha Inicial</asp:ListItem>
                                <asp:ListItem Value="enddate">Fecha Final</asp:ListItem>
                                <asp:ListItem Value="comments">Comentarios</asp:ListItem>
                                <asp:ListItem Value="attachment">Archivo adjunto</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>                                
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
                                <asp:ListItem Value="subactivityname">Subactividad</asp:ListItem>
                                <asp:ListItem Value="description">Descripción</asp:ListItem>
                                <asp:ListItem Value="begindate">Fecha Inicial</asp:ListItem>
                                <asp:ListItem Value="enddate">Fecha Final</asp:ListItem>
                                <asp:ListItem Value="comments">Comentarios</asp:ListItem>
                                <asp:ListItem Value="attachment">Archivo adjunto</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
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
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addSubActivityInformationRegistry.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
		                        <asp:BoundField DataField="SUBACTIVITYNAME" HeaderText="SubActividad" />
		                        <asp:BoundField DataField="description" HeaderText="Descripción" />
		                        <asp:BoundField DataField="begindate" HeaderText="Fecha Inicial" />
		                        <asp:BoundField DataField="enddate" HeaderText="Fecha Final" />
		                        <asp:BoundField DataField="comments" HeaderText="Comentarios" />
		                        <asp:BoundField DataField="attachment" HeaderText="Archivo adjunto" />
		                        <asp:BoundField DataField="STATETEXT" HeaderText="Estado" />
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
