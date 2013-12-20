<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchProjectApprovalRecord" title="searchProjectApprovalRecord" Codebehind="searchProjectApprovalRecord.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                                <asp:ListItem Value="projectname">Nombre de la idea</asp:ListItem>
                               <%-- <asp:ListItem Value="code">Código aprobación</asp:ListItem>--%>
                                <%--<asp:ListItem Value="comments">Comentarios</asp:ListItem>
                                <asp:ListItem Value="attachment">Archivo adjunto</asp:ListItem>
                                <asp:ListItem Value="approvaldate">Fecha de aprobación</asp:ListItem>--%>
                                <asp:ListItem Value="actnumber">Número de acta</asp:ListItem>
                               <%-- <asp:ListItem Value="approvedvalue">Valor aprobado</asp:ListItem>
                                <asp:ListItem Value="approvedtext">Aprobado por</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>--%>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                              <%--  <asp:ListItem Value="createdate">Fecha de aprobación</asp:ListItem>--%>
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
	                            <asp:ListItem Value="projectname">Idea Aprobada</asp:ListItem>
	                            <asp:ListItem Value="code">Código aprobación</asp:ListItem>
	                            <asp:ListItem Value="comments">Comentarios</asp:ListItem>
	                            <asp:ListItem Value="attachment">Archivo adjunto</asp:ListItem>
	                            <asp:ListItem Value="approvaldate">Fecha de aprobación</asp:ListItem>
	                            <asp:ListItem Value="actnumber">Número de acta</asp:ListItem>
	                            <asp:ListItem Value="approvedvalue">Valor aprobado</asp:ListItem>
	                            <asp:ListItem Value="approved">Aprobado por</asp:ListItem>
	                            <asp:ListItem Value="enabled">Estado</asp:ListItem>
	                            <asp:ListItem Value="username">Usuario</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                            AllowPaging="True" Width="100%">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" 
                                    DataNavigateUrlFormatString="addProjectApprovalRecord.aspx?op=edit&id={0}" 
                                    HeaderText="Operaciones" Text="Editar" Visible="False" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
		                        <asp:BoundField DataField="PROJECTNAME" HeaderText="Nombre de la Idea" />
		                        <asp:BoundField DataField="code" HeaderText="Código aprobación" />
		                        <asp:BoundField DataField="comments" HeaderText="Comentarios" Visible="False" />
		                        <asp:BoundField DataField="attachment" HeaderText="Archivo adjunto" />
		                        <asp:BoundField DataField="approvaldate" HeaderText="Fecha de aprobación" 
                                    Visible="False" />
		                        <asp:BoundField DataField="actnumber" HeaderText="Número de acta" />
		                        <asp:BoundField DataField="approvedvalue" HeaderText="Valor aprobado" 
                                    Visible="False" />
		                        <asp:BoundField DataField="APPROVEDTEXT" HeaderText="Aprobado por" />
		                        <asp:BoundField DataField="enabled" HeaderText="Estado" Visible="False" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
		                        <asp:BoundField DataField="createdate" HeaderText="Fecha de aprobación" />
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
