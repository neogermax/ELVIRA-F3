<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_proceedings_Reportcompromise"
    Title="Reporte Compromisos" Codebehind="Reportcompromise.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../../Include/javascript/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Include/javascript/ReportCompromise.js"></script>

    <script src="../../Include/javascript/chosen.jquery.min.js" type="text/javascript"></script>

    <h1>
        <span style="font-family: arial,helvetica,sans-serif;">Compromisos Registrados</span></h1>
    <p>
        &nbsp;</p>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tr>
            <td style="width: 15%; text-align: left;">
                <span style="font-family: arial,helvetica,sans-serif;"><strong>&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;Proyecto</strong></span>
            </td>
            <td>
                <asp:DropDownList ID="ddlproyect" runat="server" CssClass="Ccombo">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tr>
            <td colspan="4">
                <p>
                </p>
            </td>
        </tr>
        <tr id="fec1" runat="server">
            <td style="width: 15%; text-align: left;">
                <span style="font-family: arial,helvetica,sans-serif;"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Rango de Fechas</strong></span>
            </td>
            <td style="text-align: left;">
                <span style="font-family: arial,helvetica,sans-serif;"><strong>Fecha Inicial</strong></span>
            </td>
            <td style="text-align: left;">
                <span style="font-family: arial,helvetica,sans-serif;"><strong>Fecha Final</strong></span>
            </td>
            <td style="width: 55%; text-align: center;">
            </td>
        </tr>
        <tr  id="fec2" runat="server"> 
            <td style="width: 15%; text-align: center;">
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtStartingDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="cestartingdate" runat="server" TargetControlID="txtStartingDate"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEndingDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="ceendingdate" runat="server" TargetControlID="txtendingdate"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
            </td>
            <td style="width: 55%; text-align: center;">
            </td>
        </tr>
        <tr>
            <td style="width: 15%; text-align: center;">
            </td>
            <td style="text-align: left;">
                <asp:CompareValidator ID="cvstartingdate" runat="server" ErrorMessage="aaaa/mm/dd"
                    Type="Date" ControlToValidate="txtStartingDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            </td>
            <td style="text-align: left;">
                <asp:CompareValidator ID="cvendingdate" runat="server" ErrorMessage="aaaa/mm/dd"
                    Type="Date" ControlToValidate="txtendingdate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            </td>
            <td style="width: 55%; text-align: center;">
        </tr>
        <tr>
            <td colspan="5">
                <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
                    border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
                    vertical-align: middle;">
                    <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                    <asp:Label ID="Lblmsginfo" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
                    <asp:Label ID="Lblmsginfo3" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
            </td>
            <td colspan="3" style="text-align: left;">
                <asp:Button ID="Btnvisulizar" runat="server" Text="Visualizar Compromisos" ValidationGroup="compromise" />
                <asp:HiddenField ID="HDreportact" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tablacomrpomise" runat="server" border="0" cellpadding="1" cellspacing="1"
        style="width: 100%;">
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <h2>
                    <span style="font-family: arial">Compromisos por Finalizar</span></h2>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 5%; background-color: #f3ab32; display: none;">
                <span style="color: #ffffff;"><strong>Id</strong></span>
            </td>
            <td style="text-align: center; width: 25%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Responsable</strong></span>
            </td>
            <td style="text-align: center; width: 40%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Compromisos</strong></span>
            </td>
            <td style="text-align: center; width: 15%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Fecha de Compromiso</strong></span>
            </td>
            <td style="text-align: center; width: 15%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Fecha de Finalización</strong></span>
            </td>
        </tr>
        <tr>
            <td id="comrpomise" runat="server" colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: center;">
                <asp:Button ID="Btnguardar" runat="server" Text="Guardar Fecha Compromisos" ValidationGroup="compromise" />
                <asp:HiddenField ID="HFdatetrim" runat="server" />
                <asp:HiddenField ID="HFidtrim" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <h2>
                    <span style="font-family: arial">Compromisos Finalizados</span></h2>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 5%; background-color: #f3ab32; display: none;">
                <span style="color: #ffffff;"><strong>Id</strong></span>
            </td>
            <td style="text-align: center; width: 25%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Responsable</strong></span>
            </td>
            <td style="text-align: center; width: 40%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Compromisos</strong></span>
            </td>
            <td style="text-align: center; width: 15%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Fecha de Compromiso</strong></span>
            </td>
            <td style="text-align: center; width: 15%; background-color: #f3ab32;">
                <span style="color: #ffffff;"><strong>Fecha de Finalización</strong></span>
            </td>
        </tr>
        <tr>
            <td id="comnull" runat="server" colspan="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div id="containerSuccess2" runat="server" visible="true" style="width: 100%; text-align: center;
                    border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
                    vertical-align: middle;">
                    <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                    <asp:Label ID="Lblmsginfo2" runat="server" Style="font-size: 14pt; color: #FF0040;"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
