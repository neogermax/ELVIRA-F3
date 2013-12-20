<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.actas_acta_star" Title="actas_acta_star" Codebehind="Proceedings_stars.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DoubleListBox" Namespace="DoubleListBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/proceedings_start.js" type="text/javascript"></script>

    <script type="text/javascript">

        function format(input) {
            var num = input.value.replace(/\./g, "");
            if (!isNaN(num)) {
                num = num.toString().split("").reverse().join("").replace(/(?=\d*\.?)(\d{3})/g, "$1.");
                num = num.split("").reverse().join("").replace(/^[\.]/, "");
                input.value = num;
            }

            else {
                alert('Solo se permiten numeros');
                input.value = input.value.replace(/[^\d\.]*/g, "");
            }
        }
    </script>

    <div id="containerSuccess" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblmsginfo" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <p>
    </p>
    <p>
    </p>
    <table runat="server" cellpadding="1" cellspacing="1" id="Acta_Inicio" style="width: 80%;"
        align="center">
        <tr>
            <td style="border: 1px solid #000000;" colspan="2" rowspan="1">
                <img id="ctl00_headerLeft" src="../App_Themes/GattacaAdmin/Images/Template/header_leftBPO.jpg"
                    style="border-width: 0px;">
            </td>
            <td colspan="4" style="text-align: center; border: 1px solid #000000; font-size: large;"
                bgcolor="#F2F2F2">
                <b>ACTA DE INICIO </b>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lbltitleprincipal" runat="server" Text="Información general del contrato"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Nombre del
                    <asp:DropDownList ID="ddlTypeThird" runat="server" CssClass="Ccombo" Width="127px">
                        <asp:ListItem Value="Socio">Socio</asp:ListItem>
                        <asp:ListItem Value="Operador">Operador</asp:ListItem>
                        <asp:ListItem Value="Socio Operador">Socio Operador</asp:ListItem>
                    </asp:DropDownList>
                </b>
            </td>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Número del
                    <asp:DropDownList ID="ddlTypeoF" runat="server" CssClass="Ccombo" Width="127px">
                        <asp:ListItem Value="Contrato">Contrato</asp:ListItem>
                        <asp:ListItem Value="Convenio">Convenio</asp:ListItem>
                    </asp:DropDownList>
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; height: 44px;">
                <asp:TextBox ID="Txtnameoperator" runat="server" Width="100%"></asp:TextBox>
                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="Txtvaluesletters"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            --%>
            </td>
            <td colspan="3" style="border: solid 1px #000000; height: 44px;">
                <asp:TextBox ID="Txtnumbercontract" runat="server" Width="100%"></asp:TextBox>
                <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="TxtaportFSCefect"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblobjective" runat="server" Text="Objeto del contrato:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtobjectcontract" runat="server" Width="100%" Height="55px" TextMode="MultiLine"></asp:TextBox>
                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtaporFSCesp"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Nombre</b>
                <asp:TextBox ID="Txttitledinamic" runat="server" Width="244px"></asp:TextBox>
                <b>:</b>
            </td>
            <td colspan="3" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtnameprofiniciative" runat="server" Width="100%"></asp:TextBox>
                <%--          <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="Txtaportotrosefect"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
          --%>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblsupervisor" runat="server" Text="Nombre del Supervisor(a) del contrato :"
                    Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtnamesupervisor" runat="server" Width="100%"></asp:TextBox>
                <%--             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="Txtaportotrosesp"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
       --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblvaluescont" runat="server" Text="Valor del contrato en $:" Font-Bold="True"></asp:Label>
                <b></b>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtvaluescontract" runat="server" Width="100%" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txtfechasuscript"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>En Letras: </b>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtvaluesletters" runat="server" Width="100%" Height="38px" TextMode="MultiLine"></asp:TextBox>
                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txtdatestartcontract"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Aporte de la Fundación Saldarriaga Concha en Efectivo: </b>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="TxtaportFSCefect" runat="server" Width="100%" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txtdateendcontract"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
          --%>
            </td>
            <td colspan="2" style="border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;">
                <b>Aporte de la Fundación Saldarriaga Concha en Especie:</b>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="TxtaporFSCesp" runat="server" Width="100%" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="Txtsoportsjuridique"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblaporteect" runat="server" Text="Aporte del Socio Operador en Efectivo:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtaportotrosefect" runat="server" Width="100%" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="Txtaseguradora"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
            <td colspan="2" style="border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;">
                <asp:Label ID="Lblaporteesp" runat="server" Text="Aporte del Socio Operador en Especie:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtaportotrosesp" runat="server" Width="100%" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="Txtejecucion"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblfechasuscript" runat="server" Text="Fecha de suscripción del contrato:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtfechasuscript" runat="server" Width="100%"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txtfechasuscript"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="dd/mm/aaaa"
                    Type="Date" ControlToValidate="Txtfechasuscript" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="Txtobservaciones"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
       --%>
            </td>
            <td colspan="2" style="border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;">
                <asp:Label ID="Lblduration" runat="server" Text="Duración del Contrato:" Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtdurationcontract" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <asp:Label ID="Lblstartdate" runat="server" Text="Fecha de Inicio del contrato:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtdatestartcontract" runat="server" Width="100%"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Txtdatestartcontract"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="dd/mm/aaaa"
                    Type="Date" ControlToValidate="Txtdatestartcontract" Operator="DataTypeCheck"
                    SetFocusOnError="True"></asp:CompareValidator>
                <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
         --%>
            </td>
            <td colspan="2" style="border: solid 1px #000000; font-weight: 700; background-color: #F2F2F2;">
                <asp:Label ID="Lblenddate" runat="server" Text="Fecha de Terminación del contrato:"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtdateendcontract" runat="server" Width="100%"></asp:TextBox>
                <cc1:CalendarExtender ID="ceapprovaldate" runat="server" TargetControlID="Txtdateendcontract"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvapprovaldate" runat="server" ErrorMessage="dd/mm/aaaa"
                    Type="Date" ControlToValidate="Txtdateendcontract" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="TextBox2"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="6" style="border: solid 1px #000000; font-weight: 700;
                background-color: #F2F2F2;">
                Información sobre el soporte juridico
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtsoportsjuridique" runat="server" Width="100%" Height="48px" TextMode="MultiLine">Que el día &#8230;.  del mes &#8230;. de 2013, fue aprobada la garantía única con los siguientes amparos:</asp:TextBox>
                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="TextBox3"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Nombre de la aseguradora : </b>
            </td>
            <td colspan="3" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtaseguradora" runat="server" Width="100%"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr align="center">
            <td style="border: solid 1px #000000; background-color: #F2F2F2;" rowspan="2">
                <b>No. de la Poliza</b>
            </td>
            <td style="border: solid 1px #000000; background-color: #F2F2F2;" rowspan="2">
                <b>Concepto</b>
            </td>
            <td style="border: solid 1px #000000; background-color: #F2F2F2;" colspan="4">
                <b>Vigencia</b>
            </td>
        </tr>
        <tr align="center">
            <td style="border: solid 1px #000000; background-color: #F2F2F2;" colspan="2">
                <b>Desde</b>
            </td>
            <td style="border: solid 1px #000000; background-color: #F2F2F2;" colspan="2">
                <b>Hasta</b>
            </td>
        </tr>
        <tr>
            <td id="tablapoliza" runat="server">
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <div id="warning" runat="server" visible="true" style="width: 100%; text-align: center;
                    border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
                    vertical-align: middle;">
                    <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                    <asp:Label ID="Lblwarning" runat="server" Style="font-size: 14pt; color:  #FF0040;"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="6" style="border: solid 1px #000000; font-weight: 700;
                background-color: #F2F2F2;">
                <asp:Label ID="Lblinformatiomeject" runat="server" Text="Información de la ejecución del contrato"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtejecucion" runat="server" Width="99%" Height="73px" TextMode="MultiLine">En Bogotá a los fecha (###) días del mes de #### de 201# se reunieron #############, #####################, ##################, ################ y ################# en calidad de encargados de las areas como aparece al pie de la firma y ############### como Contratista, con el fin de dar inicio al contrato anteriormente citado.										</asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="6" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Observaciones</b>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" style="border: solid 1px #000000;">
                <asp:TextBox ID="Txtobservaciones" runat="server" Width="100%" Height="48px" TextMode="MultiLine"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: solid 1px; border-color: #000000;" bgcolor="#F2F2F2">
                <b>Para constancia de lo anterior, firman la presenta acta los que en ella intervinieron
                el,
            </td>
            <td colspan="4" style="border: solid 1px; border-color: #000000;">
                <asp:TextBox ID="txtFinishDate" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="cefinishdate" runat="server" TargetControlID="txtFinishDate"
                    Format="dd/MM/yyyy" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvfinishdate" runat="server" ErrorMessage="aaaa/mm/dd"
                    Type="Date" ControlToValidate="txtFinishDate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="6" style="border: solid 1px #000000; background-color: #F2F2F2;">
                <b>Firmas</b>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2; text-align: center;">
                <b>Cargo:</b>
            </td>
            <td colspan="3" style="border: solid 1px #000000; background-color: #F2F2F2; text-align: center;">
                <b>Nombre:</b>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox2" runat="server" Width="100%"></asp:TextBox>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TextBox4"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox3" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox4" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox5" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox6" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox7" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox8" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox9" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox10" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox11" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td colspan="3" style="border: solid 1px #000000; text-align: center;">
                <asp:TextBox ID="TextBox12" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="6" rowspan="1" style="text-align: center;">
                <asp:Button ID="Btnexport" runat="server" Text="Exportar acta de inicio" />
            </td>
        </tr>
    </table>
    <p>
    </p>
    <p>
    </p>
    <div id="containerSuccess_2" runat="server" visible="true" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblmsginfo_2" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <table id="tablevalide" runat="server" width="100%">
        <tr>
            <td>
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <div id="Div1" runat="server" visible="true" style="width: 100%; text-align: center;
                    border: 2px solid #cecece; background: #E8E8DC; height: 100px; line-height: 40px;
                    vertical-align: middle;">
                    <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
                    <asp:Label ID="Lblinformationvalide" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                </p>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:Button ID="Btnexportvalidate" runat="server" Text="Exportar acta de inicio" />
            </td>
        </tr>
    </table>
    <table id="buttons" runat="server" style="width: 100%">
        <tr>
            <td>
                <asp:HiddenField ID="HFactstar" runat="server" />
                <asp:HiddenField ID="HFnope" runat="server" />
                <asp:HiddenField ID="HFvalues" runat="server" />
                <asp:HiddenField ID="HFtabladat" runat="server" />
                <asp:HiddenField ID="HFdstart" runat="server" />
                <asp:HiddenField ID="HFobject" runat="server" />
                <asp:HiddenField ID="HFcontdinamic" runat="server" />
                <asp:HiddenField ID="HFdatetrim" runat="server" />
                <asp:HiddenField ID="HFdend" runat="server" />
                <asp:HiddenField ID="HFduration" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
