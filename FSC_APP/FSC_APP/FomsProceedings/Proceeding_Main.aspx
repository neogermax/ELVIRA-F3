<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FomsProceedings_Proceeding_Main"
    Title="Página sin título" Codebehind="Proceeding_Main.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <div id="dvProyecto" runat="server">
        <table style="width: 80%">
            <tr>
                <td colspan="3">
                    Por favor elija un proyecto y a continuación haga click en el botón Aceptar
                    <p>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProyecto" runat="server" Text="Proyecto"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlprojectsvalidate" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnAccept" runat="server" Text="Aceptar" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvRedir" runat="server">
        <table style="width: 80%">
            <tr>
                <td>
                    El tipo de acta elegido no es válido, por favor haga click en el botón Aceptar e
                    intente nuevamente.
                </td>
                <td>
                    <asp:Button ID="btnRedir" runat="server" Text="Aceptar" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
