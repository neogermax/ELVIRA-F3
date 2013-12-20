<%@ Control Language="VB" AutoEventWireup="false" Inherits="FSC_APP.Controls_User"
    CodeBehind="User.ascx.vb" %>
<table style="width: 100%">
    <tr>
        <td>
            <span style="font-size: 16px;"><span style="font-family: tahoma,geneva,sans-serif;">
                <span style="color: rgb(0, 128, 128);">Usuario:
                    <asp:Label ID="lblUserName" runat="server" SkinID="Label"></asp:Label>
                </span></span></span>
        </td>
        <td>
            <a href="/NewMenu/Menu.aspx">
                <asp:Image ID="imgMenu" runat="server" Height="36px" ImageUrl="~/images/img_menus.png"
                    ToolTip="Inicio" Style="text-align: center" AlternateText="Inicio" />
                <%--Regresar al Menú--%>
            </a>
        </td>
        <td>
            <a href="/security/login.aspx">
                <asp:Image ID="imgLogout" runat="server" Height="36px" ImageUrl="~/images/img_logout.png"
                    ToolTip="Cerrar Sesión" Style="text-align: center" AlternateText="Cerrar Sesión" />
                <%--Cerrar Sesión--%>
            </a>
        </td>
    </tr>
</table>
<div id="info" style="visibility: hidden">
    Ultimo Acceso:
    <asp:Label ID="lblLastLogin" runat="server" SkinID="Label"></asp:Label>
</div>
