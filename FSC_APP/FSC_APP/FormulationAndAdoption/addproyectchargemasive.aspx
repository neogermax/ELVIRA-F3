<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.FormulationAndAdoption_addproyectchargemasive"
    Title="addproyectchargemasive.aspx" Codebehind="addproyectchargemasive.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/chargemasive.js" type="text/javascript"></script>

    <p>
    </p>
    <h3 style="color: rgb(102, 102, 102); font-family: Arial, Helvetica, sans-serif;
        background-color: rgb(255, 255, 255);">
        CRONOGRAMA DE ACTIVIDADES</h3>
    <p>
        <span style="font-family: arial,helvetica,sans-serif;"><span style="font-size: 11px;">
            <strong><span style="color: rgb(102, 102, 102); background-color: rgb(255, 255, 255);">
                Este módulo, le permitirá cargar actividades y sub-actividades al proyecto seleccionado.</span></strong></span></span></p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="width: 40%;">
                    <span style="font-size: 11px;"><strong><span style="color: rgb(102, 102, 102); font-family: Arial, Helvetica, sans-serif;
                        background-color: rgb(255, 255, 255);">A continuación podrá descargar la plantilla
                        para el cargue.</span></strong></span>
                </td>
                <td style="text-align: center;">
                </td>
            </tr>
            <tr>
                <td style="width: 40%;">
                    <asp:Button ID="Btnlinkdownload" runat="server" Text="Descargar Formato de cronograma" />
                </td>
                <td style="text-align: center;">
                </td>
            </tr>
        </tbody>
    </table>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="width: 15%;">
                    <span style="font-family: arial,helvetica,sans-serif;"><strong>Proyecto</strong></span>
     
                </td>
                <td>
                    <asp:DropDownList ID="ddlproyect" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                </td>
            </tr>
        </tbody>
    </table>
    <p>
    </p>
    <div id="Div1" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle; color: #FFFFCC;">
        <asp:Label ID="Lbltitle" runat="server" Style="font-size: 14pt;" Font-Bold="True"
            ForeColor="#9bbb58"></asp:Label>
    </div>
    <p>
    </p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="text-align: center;">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <br />
                    <asp:Button ID="Btnuppcharge" runat="server" Text="Cargar Cronograma" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <tr>
                <td style="width: 15%;">
                </td>
                <td style="width: 15%; text-align: center;">
                    <strong><span style="color: rgb(102, 102, 102); font-family: Arial, Helvetica, sans-serif;
                        background-color: rgb(255, 255, 255);">Archivo Seleccionado:</span></strong>
                </td>
                <td style="text-align: left;">
                    <asp:Label ID="Lblarchivo" runat="server" Font-Bold="True" Font-Names="Arial Black"
                        ForeColor="#9bbb58"></asp:Label>
                </td>
                <td style="width: 15%;">
                </td>
            </tr>
        </tbody>
    </table>
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblmsginfo" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
        <asp:Label ID="Lblmsginfored" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
    </div>
    <asp:HiddenField ID="HFid" runat="server" />
    <asp:HiddenField ID="HFval" runat="server" />
    <asp:HiddenField ID="HFtitle" runat="server" />
    <table style="width: 100%">
       <tr id="linkcrono" runat="server">
                <td align="center" colspan="6">
                    <a style="font-size: 14pt;" id="linkcompromise" runat="server" href="~/Report/FormulationAndAdoption/reportProjectChronogram.aspx"
                        title="cronograma">Consultar Cronograma</a>
                </td>
            </tr>
        <tr id="vali" runat="server">
            <td align="center" width="50%">
                <asp:Button ID="Btndeleteandcharge" runat="server" Text="Remplazar Cronograma" />
            </td>
            <td align="center" width="50%">
                <asp:Button ID="Btnaddcharge" runat="server" Text="Agregar" />
            </td>
        </tr>
    </table>
</asp:Content>
