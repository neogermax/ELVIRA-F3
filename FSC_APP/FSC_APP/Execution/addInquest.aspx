<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.addInquest" Title="addInquest" Codebehind="addInquest.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblcode" runat="server" Text="Código"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtcode" runat="server" Width="400px" MaxLength="50" AutoPostBack="True"
                            CausesValidation="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="txtcode" Display="Dynamic"
                            ErrorMessage="Este código ya existe, por favor cambielo" SetFocusOnError="True">Este código ya existe, por favor cambielo</asp:CustomValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblname" runat="server" Text="Nombre/Descripción"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:TextBox ID="txtname" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblidproject" runat="server" Text="Proyecto"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:DropDownList ID="ddlProject" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidproject" runat="server" ControlToValidate="ddlProject"
                    ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpidproject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblprojectphase" runat="server" Text="Fase Proyecto"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:DropDownList ID="ddlProjectPhase" runat="server">
                    <asp:ListItem>Planeación General</asp:ListItem>
                    <asp:ListItem>Investigación y Desarrollo</asp:ListItem>
                    <asp:ListItem>Formulación y Aprobación</asp:ListItem>
                    <asp:ListItem>Planeación Operativa</asp:ListItem>
                    <asp:ListItem>Contratación</asp:ListItem>
                    <asp:ListItem>Ejecución</asp:ListItem>
                    <asp:ListItem>Evaluación y Cierre</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvprojectphase" runat="server" ControlToValidate="ddlProjectPhase"
                    ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpprojectphase" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblidusergroup" runat="server" Text="Grupo Destinatario"></asp:Label>
            </td>
            <td style="width: 429px">
                <asp:DropDownList ID="ddlUserGroup" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidusergroup" runat="server" ControlToValidate="ddlUserGroup"
                    ErrorMessage="*" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpidusergroup" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
            </td>
            <td style="width: 429px">
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
            <td style="width: 130px">
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de Creación"></asp:Label>
            </td>
            <td style="width: 429px">
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
