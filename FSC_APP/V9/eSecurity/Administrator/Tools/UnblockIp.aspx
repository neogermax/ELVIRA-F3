<%@ page title="" language="VB" masterpagefile="~/MasterPages/Security.master" autoeventwireup="false" inherits="Administrator_Tools_UnblockIp, App_Web_dvz2jfrn" theme="GattacaAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="center">
        <table style="width: 70%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblInstr" runat="server" 
                        Text="Ingrese la dirección IP (con sus respectivos puntos) o la identificación (sin puntos ni comas) segun sea el caso, luego presione el botón indicado"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblIdentification" runat="server" Text="IP o identificación"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbIdentification" runat="server" MaxLength="30"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="tbIdentification" Display="Dynamic" 
                        ErrorMessage="Dato Requerido"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" colspan="2" style="text-align: center">
                    <asp:Button ID="btnUnblockIP" runat="server" Text="Desbloquear IP" />
                    &nbsp;<asp:Button ID="btnUnblockIP0" runat="server" 
                        Text="Desbloquear identificación" />
                    &nbsp;<asp:Button ID="btnCancell" runat="server" 
                        Text="Cancelar" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblInformation" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

