<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addDocuments" Title="addDocuments" Codebehind="addDocuments.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltitle" runat="server" Text="Título"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttitle" runat="server" Width="400px" MaxLength="255" Rows="2"
                    TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelptitle" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldescription" runat="server" Text="Descripción"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdescription" runat="server" Width="400px" MaxLength="255" Rows="2"
                    TextMode="MultiLine" onkeypress="return textboxAreaMaxNumber(this,255)"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpdescription" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbleditedfor" runat="server" Text="Editado por"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddleditedfor" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpeditedfor" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbllevelvisibility" runat="server" Text="Nivel visibilidad"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlVisibilityLevel" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelplevelvisibility" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldoumenttype" runat="server" Text="Tipo documento"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDocumentType" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpdoumenttype" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Asociar Documento a Proyecto</td>
            <td>
                <asp:DropDownList ID="ddlProject" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblattachfile" runat="server" Text="Anexo"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="fuattachfile" runat="server" Width="400px" />
                <asp:RequiredFieldValidator ID="rfvattachfile" runat="server" ControlToValidate="fuattachfile"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:HyperLink ID="hlattachfile" runat="server" Visible="false" Target="_blank">Descargar</asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
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
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpattachfile" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
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
    </table>
</asp:Content>
