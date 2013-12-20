<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchContractExecution" title="searchContractExecution" Codebehind="searchContractExecution.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="idcontractrequest">Solicitud de Contrato</asp:ListItem>
                                <asp:ListItem Value="closingcomments">Comentarios de cierre</asp:ListItem>                                
                                <asp:ListItem Value="contractnumber">Número de Contrato</asp:ListItem>
                                <asp:ListItem Value="ordernumber">Número de Orden</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
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
	                            <asp:ListItem Value="idcontractrequest">Solicitud de Contrato</asp:ListItem>
	                            <asp:ListItem Value="closingcomments">Comentarios de cierre</asp:ListItem>	                            
	                            <asp:ListItem Value="contractnumber">Número de Contrato</asp:ListItem>
	                            <asp:ListItem Value="ordernumber">Número de Orden</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="idcontractrequest" DataNavigateUrlFormatString="addContractExecution.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="idcontractrequest" HeaderText="Solicitud de Contrato" />
		                        <asp:BoundField DataField="closingcomments" HeaderText="Comentarios de cierre" />		                        
		                        <asp:BoundField DataField="contractnumber" HeaderText="Número de Contrato" />
		                        <asp:BoundField DataField="ordernumber" HeaderText="Número de Orden" />
		                        <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
							</Columns>
						</asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
