﻿<%@ page title="" language="VB" masterpagefile="~/MasterPages/Security.master" autoeventwireup="false" inherits="Administrator_Message_addMessage, App_Web_8azofsar" theme="GattacaAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="cssLabelTitle" Text="Mensajes de notificación"></asp:Label><hr />
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" align="center" 
                    bgcolor="White" style="width: 409px">
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Codigo"></asp:Label>
                        </td>
                        <td style="text-align: justify" class="style2">
                            <asp:Label ID="lblCodigo" runat="server" Text="Codigo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Mensaje"></asp:Label>
                        </td>
                        <td style="text-align: justify" class="style2">
                            <asp:TextBox ID="txtNombre" runat="server" Rows="6" TextMode="MultiLine" 
                                MaxLength="300" Height="107px" Width="344px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIDCompany" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="text-align: center" class="style2">
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
