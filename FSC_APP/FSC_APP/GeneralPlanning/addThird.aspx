<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addThird" Title="addThird" Codebehind="addThird.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript" src="../Include/javascript/jquery-1.6.1.min.js"></script>

    <script src="../Include/javascript/third.js" type="text/javascript"></script>

<script type="text/javascript">
    //Función que permite solo Números
    function ValidaSoloNumeros() {
        if ((event.keyCode < 48) || (event.keyCode > 57))
            event.returnValue = false;
    }
</script>


    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblexit" runat="server" Style="font-size: 1.1em;"></asp:Label>
    </div>
    <br />
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
                <asp:Label ID="lblcode" runat="server" Text="NIT"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" AutoPostBack="True" onkeypress="ValidaSoloNumeros()" onkeychange="ValidaSoloNumeros()" onkeyup="ValidaSoloNumeros()"></asp:TextBox>
                <asp:Label ID="lblinf" runat="server" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblname" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtname"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Tipo de Persona"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDLtypepeople" runat="server">
                    <asp:ListItem Text="Persona Natural" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Persona Juridica" Value="False"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Label ID="Lblcontact" runat="server" Text="Contacto"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txtcontact" runat="server" Width="400px" MaxLength="255" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Lblactions" runat="server" Text="Documento de Identidad"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txtactions" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Telefono"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txtphone" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txtemail" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Representante legal"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="Txtreplegal" runat="server" Width="400px" MaxLength="255"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="false">
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
                <asp:Label ID="lbliduser" runat="server" Text="Usuario creación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfviduser" runat="server" ControlToValidate="txtiduser"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha creación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcreatedate" runat="server" ControlToValidate="txtcreatedate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnAddData" runat="server" Text="Agregar Datos" OnClick="btnAddData_Click" />
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
                    ForeColor="Red"></asp:Label><asp:HiddenField ID="HFswchit" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
