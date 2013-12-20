<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.AddMenu"  title="Gattaca e-Project - v8"  Codebehind="AddMenu.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
	<asp:UpdatePanel id="UpdatePanel1" runat="server">
		<contenttemplate>
			<table id="Table1" border="0" cellpadding="1" cellspacing="1" class="cssTable" width="100%">
					<tr>
							<td align="left" colspan="6">
									<asp:Label ID="lblTitle" runat="server" CssClass="cssLabelTitle">ITEM DEL MENU</asp:Label></td>
					</tr>
					<tr>
							<td colspan="2">
									<asp:Label ID="lblTextField" runat="server" CssClass="cssLabelForm">Texto</asp:Label></td>
							<td colspan="2">
									<asp:TextBox ID="txtTextField" runat="server" CssClass="cssTextBoxForm" Width="200px"></asp:TextBox></td>
							<td>
									<asp:Label ID="lblHelpTextField" runat="server" CssClass="cssLabelHelp"></asp:Label></td>
							<td>
									<asp:RequiredFieldValidator ID="rfvTextField" runat="server" ControlToValidate="txtTextField"
											CssClass="cssRequired" ErrorMessage="* El Nombre es necesario"></asp:RequiredFieldValidator></td>
					</tr>
					<tr>
							<td colspan="2">
									<asp:Label ID="lblURL" runat="server" CssClass="cssLabelForm">URL</asp:Label></td>
							<td colspan="2">
									<asp:TextBox ID="txtURL" runat="server" CssClass="cssTextBoxForm" Width="200px"></asp:TextBox></td>
							<td>
									<asp:Label ID="lblHelpURL" runat="server" CssClass="cssLabelHelp"></asp:Label></td>
							<td>
							</td>
					</tr>
					<tr>
							<td colspan="2">
									<asp:Label ID="lblEnabled" runat="server" CssClass="cssLabelForm">Estado</asp:Label></td>
							<td colspan="2">
                                <asp:DropDownList ID="dllEnabled" runat="server">
                                    <asp:ListItem Value="T">Habilitado</asp:ListItem>
                                    <asp:ListItem Value="F">DesHabilitado</asp:ListItem>
                                </asp:DropDownList></td>
							<td>
									<asp:Label ID="lblHelpEnabled" runat="server" CssClass="cssLabelHelp"></asp:Label></td>
							<td>
									</td>
					</tr>
					<tr>
							<td colspan="2">
									<asp:Label ID="lblSortOrdeR" runat="server" CssClass="cssLabelForm">Orden</asp:Label></td>
							<td colspan="2">
									<asp:TextBox ID="txtSortOrdeR" runat="server" CssClass="cssTextBoxForm" Width="200px"></asp:TextBox></td>
							<td>
									<asp:Label ID="lblHelpSortOrdeR" runat="server" CssClass="cssLabelHelp"></asp:Label></td>
							<td>
									<asp:RequiredFieldValidator ID="rfvSortOrdeR" runat="server" ControlToValidate="txtSortOrdeR"
											CssClass="cssRequired" ErrorMessage="* El orden es necesario"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvOrder" runat="server" ControlToValidate="txtSortOrdeR"
                                    CssClass="cssRequired" ErrorMessage="*Numerico" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
					</tr>
					<tr>
							<td colspan="6">
									<asp:Button ID="btnNew" runat="server" CssClass="cssButton" Text="Crear" /><asp:Button
											ID="btnUpdate" runat="server" CssClass="cssButton" Text="Modificar" /><asp:Button
													ID="btnDelete" runat="server" CausesValidation="False" CssClass="cssButton" Text="Eliminar" /><asp:Button
															ID="btnCancel" runat="server" CausesValidation="False" CssClass="cssButton" Text="Cancelar" /></td>
					</tr>
					<tr>
							<td colspan="6">
									<asp:Label ID="lblConfirmDelete" runat="server" CssClass="cssRequired" ForeColor="Red"
											Visible="False">Si elimina los cambios no pueden ser revertidos. Está seguro que desea eliminar?</asp:Label></td>
					</tr>
					<tr>
							<td colspan="6">
									<asp:Button ID="btnConfirmDelete" runat="server" CausesValidation="False" CssClass="cssButton"
											Text="Confirmar" Visible="False" />
									<asp:Button ID="btnCancelDelete" runat="server" CausesValidation="False" CssClass="cssButton"
											Text="Cancelar" Visible="False" /></td>
					</tr>
			</table>
</contenttemplate>
	</asp:UpdatePanel>
</asp:Content>

