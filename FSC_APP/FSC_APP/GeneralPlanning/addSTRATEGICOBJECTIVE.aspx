<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addSTRATEGICOBJECTIVE" title="addSTRATEGICOBJECTIVE" Codebehind="addSTRATEGICOBJECTIVE.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
                <table style="width: 100%">
                    <tr>
                        <td> 
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblcode" runat="server" Text="C�digo"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" 
                                AutoPostBack="True"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblname" runat="server" Text="Nombre/Descripci�n"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="255" 
                                TextMode="MultiLine"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblyear" runat="server" Text="A�o"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlyear" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpyear" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblidenterprise" runat="server" Text="Empresa"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidenterprise" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidenterprise" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblidperspective" runat="server" Text="Perspectiva"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidperspective" runat="server">
                            </asp:DropDownList>
							<asp:RequiredFieldValidator ID="rfvidperspective" runat="server" 
                                ControlToValidate="ddlidperspective" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidperspective" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlenabled" runat="server">
                                <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                                <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario Creaci�n"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td> 
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creaci�n"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" />
							<asp:Button ID="btnSave" runat="server" Text="Guardar" />
							<asp:Button ID="btnDelete" runat="server" Text="Eliminar" 
                                CausesValidation="False" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                                CausesValidation="False"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" 
                                CausesValidation="False" />
                            <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" 
                                CausesValidation="False"/>
                            &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                </table>
</asp:Content>

