<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addDocumentType" title="addDocumentType" Codebehind="addDocumentType.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 164px"> 
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td style="width: 676px">
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td style="width: 164px"> 
                            <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
                        </td>
                        <td style="width: 676px">
                            <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvcode" runat="server" 
                                ControlToValidate="txtcode" ErrorMessage="*" Display="Dynamic" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="txtcode" 
                                Display="Dynamic" ErrorMessage="Este código ya existe, por favor cambielo" 
                                SetFocusOnError="True">Este código ya existe, por favor cambielo</asp:CustomValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td style="width: 164px"> 
                            <asp:Label ID="lblname" runat="server" Text="Nombre/Descripción"></asp:Label>
                        </td>
                        <td style="width: 676px">
                            <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="50" 
                                TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
							<asp:RequiredFieldValidator ID="rfvname" runat="server" 
                                ControlToValidate="txtname" ErrorMessage="*" Display="Dynamic" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td style="width: 164px"> 
                            <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                        </td>
                        <td style="width: 676px">
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
                        <td style="width: 164px"> 
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td style="width: 676px">
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
						</td>
                    </tr>
                    <tr>
                        <td style="width: 164px"> 
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                        </td>
                        <td style="width: 676px">
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
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
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" 
                                CausesValidation="False" />
                            <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False"/>
                            &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                </table>
</asp:Content>

