<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addForum" Title="addForum" Codebehind="addForum.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
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
                <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidproject" runat="server" Enabled="False">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="lblHelpidproject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblsubject" runat="server" Text="Asunto"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtsubject" runat="server" Width="400px" MaxLength="150" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="txtsubject"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpsubject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblmessage" runat="server" Text="Mensaje"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtmessage" runat="server" Width="400px" MaxLength="150" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvmessage" runat="server" ControlToValidate="txtmessage"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpmessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblattachment" runat="server" Text="Archivo"></asp:Label>
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
                <asp:Label ID="lblupdateddate" runat="server" Text="Fecha actualización"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtupdateddate" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvupdateddate" runat="server" ControlToValidate="txtupdateddate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <cc1:CalendarExtender ID="cesupdateddate" runat="server" TargetControlID="txtupdateddate"
                    Format="yyyy/MM/dd" Animated="true">
                </cc1:CalendarExtender>
            </td>
            <td>
                <asp:Label ID="lblHelpupdateddate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
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
    </table>    
    <table style="width: 100%">
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
