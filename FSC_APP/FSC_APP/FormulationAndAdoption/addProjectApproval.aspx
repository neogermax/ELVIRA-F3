<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    Inherits="FSC_APP.FormulationAndAdoption_addProjectApproval" Title="Página sin título"
    CodeBehind="addProjectApproval.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/project_approval.js" type="text/javascript"></script>

    <link href="../css/elvira_F3.css" rel="stylesheet" type="text/css" />
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
        <img style="margin-top: 5px;" src="../images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <div id="containerSuccess2" runat="server" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 80px; line-height: 40px;
        vertical-align: middle; border-radius: 15px;">
        <img style="margin-top: 5px;" src="../images/alert_icon.png" width="24px" alt="Save" />
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
            <%--<li>
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblHelpid" runat="server" ForeColor="#990000"></asp:Label>
            </li>--%>
            <br />
            <li>
                <asp:Label ID="lblidproject" runat="server" Text="Proyecto en Proceso"></asp:Label>
                <select id="ddlproyect" class="Ccombo">
                    <asp:DropDownList ID="ddlproyect" runat="server" CssClass="Ccombo">
                    </asp:DropDownList>
                </select>
                <asp:Label ID="lblHelpidproject" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="Lblnameidea" runat="server" Text="Proyecto"></asp:Label>
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
                <%-- <cc1:CalendarExtender ID="cesapprovaldate" runat="server" TargetControlID="txtapprovaldate"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>--%>
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
                <asp:Label ID="Label7" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="Label8" runat="server" Text="Aporte Contrapartida"></asp:Label>
                <asp:TextBox ID="Txtaportcontra" runat="server" Width="400px" MaxLength="30" onkeyup="format(this)"
                    onchange="format(this)" Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <asp:Label ID="Label9" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblapprovedvalue" runat="server" Text="Valor aprobado"></asp:Label>
                <asp:TextBox ID="txtapprovedvalue" runat="server" Width="400px" MaxLength="30" Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <asp:Label ID="lblHelpapprovedvalue" runat="server" ForeColor="#990000"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblapproved" runat="server" Text="Aprobado por"></asp:Label>
                <asp:DropDownList ID="ddlapproved" runat="server" CssClass="Ccombo">
                    <asp:ListItem Value="1">Comité de Contratación</asp:ListItem>
                    <asp:ListItem Value="0">Junta Directiva</asp:ListItem>
                    <asp:ListItem Value="2">Pre Comité de Contratación</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblHelpapproved" runat="server" ForeColor="#990000"></asp:Label>
            </li>
        </ul>
        <br />
        <div id="gridthird">
        </div>
        <br />
        <ul>
            <li>
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <input id="SaveApproval" type="button" value="Aprobar Proyecto" name="Save_Approval"
                    onclick="return SaveApproval_onclick()" />
                <%--      <asp:Button ID="btnAddData" runat="server" Text="Aprobar Idea" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />--%>
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
    <%-- <h1>
        <span style="font-family: arial,helvetica,sans-serif;">Aprobación Proyecto</span></h1>
    <p>
        &nbsp;</p>--%>
    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
        <tbody>
            <%--  <tr>
                <td style="width: 10%;">
                    <strong><span style="font-family: arial,helvetica,sans-serif;">Proyecto</span></strong>
                </td>
                <td style="text-align: left;">
                </td>
                <td style="width: 30%;">
                </td>
            </tr>--%>
        </tbody>
    </table>
</asp:Content>
