<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addActivity" Title="addActivity" Codebehind="addActivity.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" language="javascript">
        function textboxMultilineMaxNumber(txt, maxLen) {
            try {
                if (txt.value.length > (maxLen - 1))

                    return false;
            } catch (e) {
            }
        }  
    </script>

    <cc1:TabContainer ID="TabContainer1" runat="server" Height="500px" Width="810px"
        ActiveTabIndex="0" ScrollBars="Vertical">
        <cc1:TabPanel runat="server" HeaderText="Información Básica" ID="TabPanel1" Width="600"
            TabIndex="0">
            <HeaderTemplate>
                Información Principal
            </HeaderTemplate>
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"
                                ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblnumber" runat="server" Text="Número"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnumber" runat="server" Width="400px" MaxLength="50" AutoPostBack="True"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvnumber" runat="server" ControlToValidate="txtnumber"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpnumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbltitle" runat="server" Text="Código"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttitle" runat="server" Width="400px" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelptitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidproject" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidproject" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblidobjective" runat="server" Text="Objetivo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidobjective" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidobjective" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblidcomponent" runat="server" Text="Componente"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlidcomponent" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvComponent" runat="server" ControlToValidate="ddlidcomponent"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpidcomponent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="200" TextMode="MultiLine"
                                onkeypress="return textboxMultilineMaxNumber(this,200)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ControlToValidate="txtdescription"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpdescription" runat="server"></asp:Label>
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
                            <asp:Label ID="lblHelpenabled" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpiduser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                                ErrorMessage="*" ValidationGroup="activity"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblHelpcreatedate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" ValidationGroup="activity" />
                            <asp:Button ID="btnSave" runat="server" Text="Guardar" ValidationGroup="activity" />
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                            <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                            &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                                ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<asp:Label ID="lblVersion" runat="server" Text="Versiones Anteriores"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
							<asp:GridView ID="gvVersion" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="createdate" HeaderText="Fecha" />
                                    <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                    <asp:BoundField DataField="number" HeaderText="Número" />
                                    <asp:HyperLinkField DataNavigateUrlFields="id" 
                                        DataNavigateUrlFormatString="addActivity.aspx?op=show&amp;id={0}&consultLastVersion=false"
                                        DataTextField="number" HeaderText="Número" Target="_blank" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" TabIndex="1" runat="server" HeaderText="Relación Objetivos">
            <HeaderTemplate>
                Relación Objetivos
            </HeaderTemplate>
            <ContentTemplate>
                &nbsp;
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td colspan="2">
                                    <cc2:DoubleListBox ID="dlbObjective" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
