<%@ Page Language="VB" MasterPageFile="~/Master/mpLogin.master" AutoEventWireup="false" Inherits="FSC_APP.Security_login" Title="FSC - Sistema para la Gestion de Proyectos - Login" Codebehind="login.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphLogin" runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
--%>
    <style media="all" type="text/css">
        #contenedor
        {
            background-image: url(/FSC/App_Themes/GattacaAdmin/Images/Template/V9/bg.jpg);
            background-repeat: repeat-x;
        }
        .tablalogin
        {
            background-color: #81843D;
            -moz-border-radius: 25px;
            -webkit-border-radius: 25px;
            border-radius: 25px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 14px #000000;
            -webkit-box-shadow: 0px 0px 14px #000000;
            box-shadow: 0px 0px 14px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
        }
        .style3
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: normal;
            color: #E8911E;
            font-size: x-large;
            margin-left: 5px;
        }
    </style>
    </div>
    <table style="background-color: #81843D; margin: 0 auto; clear: both; margin-right: 10%"
        width="320px" class="tablalogin" align="right">
        <tr>
            <td valign="bottom" align="center">
                <div class="cssTitleLogin">
                </div>
                <table width="79%" border="0" align="center" cellpadding="0" cellspacing="10">
                    <tr>
                        <td class="cssNone">
                            <span class="style3">Cliente</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlClient" runat="server" DataSourceID="ClientDataSource" CssClass="FormularioLogin"
                                DataTextField="Name" DataValueField="Value" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssNone">
                            <span class="style3">Usuario</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtUser" runat="server" TabIndex="1" BackColor="White" Width="100%"
                                Font-Bold="false" Font-Size="X-Large" ForeColor="#67656A"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsr" runat="server" ControlToValidate="txtUser"
                                Display="Dynamic" ErrorMessage="Dato Requerido" Font-Bold="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="cssNone">
                            <span class="style3">Contraseña</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtPw" runat="server" TextMode="Password" BackColor="White" Width="100%"
                                TabIndex="2" Font-Size="X-Large" Style="margin-bottom: 25px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPw" runat="server" ControlToValidate="txtPw" Display="Dynamic"
                                ErrorMessage="Dato Requerido" Font-Bold="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 10px">
                        <td>
                        </td>
                    </tr>
                </table>
                 <td rowspan="6" align="center" class="cssBtnLoginBPO" style="bottom: auto; background-position: bottom">
                            <asp:Button ID="btnLogIn" runat="server" TabIndex="3" />
                        </td>
                <table width="100%" height="0" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="cssTxtFooterLogin">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>
--%>
    <asp:XmlDataSource ID="ClientDataSource" runat="server" DataFile="~/Include/Server/LicenseInformation.xml">
    </asp:XmlDataSource>
</asp:Content>
