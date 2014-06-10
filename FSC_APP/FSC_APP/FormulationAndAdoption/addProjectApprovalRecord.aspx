<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false" Inherits="FSC_APP.addProjectApprovalRecord"
    Title="addProjectApprovalRecord" CodeBehind="addProjectApprovalRecord.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />

    <script src="../Include/javascript/idea_aproval.js" type="text/javascript"></script>

    <link href="../css/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <script src="../Include/javascript/F_globales_MGroup.js" type="text/javascript"></script>

    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>

    <link href="../css/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

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

    <br />
    <div id="containerSuccess" runat="server" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <div id="containerSuccess2" runat="server" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="/images/alert_icon.png" width="24px" alt="Save" />
        <asp:Label ID="Lblnewcampos" runat="server" Style="font-size: 14pt; color: #8A0808;"></asp:Label>
    </div>
    <div id="container_wait" runat="server" visible="true" style="width: 50%; text-align: center;
        border: 15px solid #cecece; background: #E8E8DC; height: 200px; line-height: 50px;
        vertical-align: middle; z-index: 1000; position: absolute; left: 25%; border-radius: 40px;">
        <img style="margin-top: 15px;" src="../images/charge_emerging.gif" width="120px"
            alt="images" />
        <asp:Label ID="Label22" runat="server" Text="Cargando información espere un momento..."
            Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <div id="tabsaproval">
        <ul>
            <li><a href="#informacion">Información</a></li>
        </ul>
    </div>
    <div id="informacion">
        <ul>
            <li>
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblHelpid" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblidproject" runat="server" Text="Idea en Proceso"></asp:Label>
                <select id="ddlidproject" class="Ccombo">
                    <asp:DropDownList ID="ddlidproject" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                </select>
                <asp:Label ID="lblHelpidproject" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="Lblnameidea" runat="server" Text="Idea"></asp:Label>
                <asp:TextBox ID="Txtnameidea" runat="server" Width="70%" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
            </li>
        </ul>
        <ul class="left">
            <li>
                <asp:Label ID="Lbllinees" runat="server" Text="Línea Estratégica"></asp:Label>
                <asp:TextBox ID="Txtline" runat="server" Width="400px" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="Lblprogram" runat="server" Text="Objetivos Estratégicos"></asp:Label>
                <asp:TextBox ID="Txtprogram" runat="server" Width="400px" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblapprovaldate" runat="server" Text="Fecha de aprobación"></asp:Label>
                <asp:TextBox ID="txtapprovaldate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="cesapprovaldate" runat="server" TargetControlID="txtapprovaldate"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>
                <asp:Label ID="lblHelpapprovaldate" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblactnumber" runat="server" Text="Número de acta"></asp:Label>
                <asp:TextBox ID="txtactnumber" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblHelpactnumber" runat="server" ForeColor="#990000"></asp:Label>
            </li>
        </ul>
        <ul class="right">
            <li>
                <asp:Label ID="Label2" runat="server" Text="Aporte FSC"></asp:Label>
                <asp:TextBox ID="TxtaportFSC" runat="server" Width="400px" MaxLength="30" onkeyup="format(this)"
                    onchange="format(this)" Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtaportFSC"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
           --%>
                <asp:Label ID="Label7" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="Label8" runat="server" Text="Aporte Contrapartida"></asp:Label>
                <asp:TextBox ID="Txtaportcontra" runat="server" Width="400px" MaxLength="30" onkeyup="format(this)"
                    onchange="format(this)" Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txtaportcontra"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
          --%>
                <asp:Label ID="Label9" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblapprovedvalue" runat="server" Text="Valor aprobado"></asp:Label>
                <asp:TextBox ID="txtapprovedvalue" runat="server" Width="400px" MaxLength="30" Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtapprovedvalue"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
          --%>
                <asp:Label ID="lblHelpapprovedvalue" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblapproved" runat="server" Text="Aprobado por"></asp:Label>
                <asp:DropDownList ID="ddlapproved" runat="server" CssClass="Ccombo">
                    <asp:ListItem Value="1">Comité de Contratación</asp:ListItem>
                    <asp:ListItem Value="0">Junta Directiva</asp:ListItem>
                    <asp:ListItem Value="2">Pre Comité de Contratación</asp:ListItem>
                </asp:DropDownList>
                <%--    <asp:RequiredFieldValidator ID="rfvapproved" runat="server" ControlToValidate="ddlapproved"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            --%>
                <asp:Label ID="lblHelpapproved" runat="server" ForeColor="#990000"></asp:Label>
            </li>
        </ul>
        <ul>
            <li>
                <asp:Label ID="lblattachment" runat="server" Text="Archivo adjunto"></asp:Label>
                <asp:FileUpload runat="server" ID="fuattachment"></asp:FileUpload>
                <asp:HyperLink runat="server" Target="_blank" ID="hlattachment" Visible="False"></asp:HyperLink>
                <asp:Label ID="lblHelpattachment" runat="server" ForeColor="#990000"></asp:Label>
                <asp:Label ID="Lbltitlearchivo" runat="server" ForeColor="#9bbb58"></asp:Label>
            </li>
            <li id="li_001" runat="server" visible="false">
                <asp:Label ID="lblcomments" runat="server" Text="Comentarios"></asp:Label>
                <asp:TextBox ID="txtcomments" runat="server" Width="400px" MaxLength="255" Height="70px"
                    TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="lblHelpcomments" runat="server" Text=""></asp:Label>
                <asp:Label ID="lcodeaproved" runat="server" Text="Código proyecto"></asp:Label>
                <asp:TextBox ID="txtcodeapproved" runat="server" Width="396px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" ForeColor="#990000"></asp:Label>
                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txtcodeapproved"
                    ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlenabled">
                    <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                </asp:DropDownList>
                <%--        <asp:RequiredFieldValidator ID="rfvapproved0" runat="server" ControlToValidate="ddlenabled"
                    ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <asp:Label ID="lblHelpenabled" runat="server" ForeColor="#990000"></asp:Label>
                <asp:Label ID="lblBPMMessage" runat="server" Text="Por Favor Seleccione la Actividad"
                    Visible="False"></asp:Label>
                <asp:RadioButtonList ID="rblCondition" runat="server" Visible="False">
                </asp:RadioButtonList>
            </li>
            <li id="li_code" runat="server" visible="false">
                <asp:Label ID="lblcode" runat="server" Text="Código aprobación"></asp:Label>
                <asp:TextBox ID="txtcode" runat="server" Width="399px" MaxLength="50" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                    ErrorMessage="* Falta por diligenciar"></asp:RequiredFieldValidator>
                <asp:Label ID="lblHelpcode" runat="server" ForeColor="#990000"></asp:Label>
            </li>
        </ul>
        <div id="gridthird">
        </div>
        <ul>
            <li>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <input id="SaveApproval" type="button" value="Aprobar Idea" name="Save_Approval"
                    onclick="return SaveApproval_onclick()" />
                <a id="linkcancelar" runat="server" href="~/ResearchAndDevelopment/searchIdea.aspx"
                    title="cancelar" style="height: 2em;">Cancelar</a>
                <asp:Button ID="btnAddData" runat="server" Text="Aprobar Idea" Visible="false" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <%--  <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />--%>
            </li>
        </ul>
    </div>
    <br />
    <div id="containerSuccess_down" runat="server" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="../images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation_down" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <table id="derrr" style="width: 100%">
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancelDelete" runat="server" Text="Cancelar" CausesValidation="False" />
                <asp:Label ID="lblDelete" runat="server" Text="Esta seguro que desea eliminar el registro?"
                    ForeColor="Red"></asp:Label>
                <asp:HiddenField ID="HDaaprovalr" runat="server" />
                <asp:HiddenField ID="HDline" runat="server" />
                <asp:HiddenField ID="HDidea" runat="server" />
                <asp:HiddenField ID="HDvalue" runat="server" />
                <asp:HiddenField ID="filename" runat="server" />
                <asp:HiddenField ID="HDprogram" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
