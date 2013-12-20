<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addReply" Title="addReply" Codebehind="addReply.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblid" runat="server" Text="id" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50" 
                    Visible="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" 
                    ErrorMessage="*" Visible="False"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblidforum" runat="server" Text="Foro"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidforum" runat="server" Enabled="False">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidforum" runat="server" ControlToValidate="ddlidforum"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpidforum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblsubject" runat="server" Text="Asunto"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtsubject" runat="server" Width="400px" MaxLength="500" 
                    Height="70px" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="txtsubject"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpsubject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblattachment" runat="server" Text="Archivo adjunto"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="fuattachment" runat="server" />
                <asp:HyperLink ID="hlattachment" runat="server" Visible="false" Target="_blank">Descargar</asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="lblHelpattachment" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddliduser" runat="server" Enabled="False">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="ddliduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblupdatedate" runat="server" Text="Fecha de actualización"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtupdatedate" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvupdatedate" runat="server" ControlToValidate="txtupdatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpupdatedate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" 
                    CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                &nbsp;<asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
