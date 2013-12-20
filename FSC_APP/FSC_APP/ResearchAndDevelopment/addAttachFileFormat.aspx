<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addAttachFileFormat" Title="addAttachFileFormat" Codebehind="addAttachFileFormat.aspx.vb" %>

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

    <table style="width: 100%">
        <tr>
            <td style="width: 144px">
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td style="width: 442px">
                <asp:TextBox ID="txtid" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">
                Código
            </td>
            <td style="width: 442px">
                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                    ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">
                Nombre
            </td>
            <td style="width: 442px">
                <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="255" Rows="2" onkeypress="return textboxMultilineMaxNumber(this,255)"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                    ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">
                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
            </td>
            <td style="width: 442px">
                <asp:DropDownList ID="ddlenabled" runat="server">
                    <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">
                <asp:Label ID="lbliduser" runat="server" Text="Usuario Creación"></asp:Label>
            </td>
            <td style="width: 442px">
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 144px">
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha Creación"></asp:Label>
            </td>
            <td style="width: 442px">
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
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
