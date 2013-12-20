<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    ValidateRequest="false" EnableEventValidation="false"
    Inherits="FSC_APP.addProjectApprovalRecord" Title="addProjectApprovalRecord" Codebehind="addProjectApprovalRecord.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script src="../Include/javascript/idea_aproval.js" type="text/javascript"></script>

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
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt; color: #9bbb58;"></asp:Label>
    </div>
    <br />
    <table style="width: 100%">
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblid" runat="server" Text="Id"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtid" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvid" runat="server" ControlToValidate="txtid" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; width: 131px;">
                <asp:Label ID="lblidproject" runat="server" Text="Idea en Proceso"></asp:Label>
            </td>
            <td style="width: 577px; height: 30px;">
                <asp:DropDownList ID="ddlidproject" runat="server" CssClass="Ccombo">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvidproject" runat="server" ControlToValidate="ddlidproject"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 30px">
                <asp:Label ID="lblHelpidproject" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="Lblnameidea" runat="server" Text="Idea"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="Txtnameidea" runat="server" Width="400px" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="Lbllinees" runat="server" Text="Línea Estratégica"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="Txtline" runat="server" Width="400px" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="Lblprogram" runat="server" Text="Programa"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="Txtprogram" runat="server" Width="400px" MaxLength="50" Height="25px"
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblapprovaldate" runat="server" Text="Fecha de aprobación"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtapprovaldate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <cc1:CalendarExtender ID="cesapprovaldate" runat="server" TargetControlID="txtapprovaldate"
                    Format="yyyy/MM/dd" Enabled="True">
                </cc1:CalendarExtender>
                <asp:CompareValidator ID="cvapprovaldate" runat="server" ErrorMessage="aaaa/mm/dd"
                    Type="Date" ControlToValidate="txtapprovaldate" Operator="DataTypeCheck" SetFocusOnError="True"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtapprovaldate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblactnumber" runat="server" Text="Número de acta"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtactnumber" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtactnumber"
                    ErrorMessage="* Falta por diligenciar"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpactnumber" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 131px">
                <asp:Label ID="lblcode" runat="server" Text="Código aprobación"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtcode" runat="server" Width="399px" MaxLength="50" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtcode"
                    ErrorMessage="* Falta por diligenciar"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpcode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 131px">
                <asp:Label ID="lcodeaproved" runat="server" Text="Código proyecto"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtcodeapproved" runat="server" Width="396px" MaxLength="50" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txtcodeapproved"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="False">
            <td style="width: 131px">
                <asp:Label ID="lblcomments" runat="server" Text="Comentarios"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtcomments" runat="server" Width="400px" MaxLength="255" Height="70px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpcomments" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="Label2" runat="server" Text="Aporte FSC"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="TxtaportFSC" runat="server" Width="400px" MaxLength="30" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtaportFSC"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px; height: 32px;">
                <asp:Label ID="Label8" runat="server" Text="Aporte Contrapartida"></asp:Label>
            </td>
            <td style="width: 577px; height: 32px;">
                <asp:TextBox ID="Txtaportcontra" runat="server" Width="400px" MaxLength="30" onkeyup="format(this)"
                    onchange="format(this)"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txtaportcontra"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 32px">
                <asp:Label ID="Label9" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblapprovedvalue" runat="server" Text="Valor aprobado"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtapprovedvalue" runat="server" Width="400px" MaxLength="30" 
                    Enabled="False"></asp:TextBox>
                <%#DataBinder.Eval(Container, "DataItem.Depto.name")%>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtapprovedvalue"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpapprovedvalue" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px; height: 25px;">
                <asp:Label ID="lblapproved" runat="server" Text="Aprobado por"></asp:Label>
            </td>
            <td style="width: 577px; height: 25px;">
                <asp:DropDownList ID="ddlapproved" runat="server">
                    <asp:ListItem Value="1">Comité de Contratación</asp:ListItem>
                    <asp:ListItem Value="0">Junta Directiva</asp:ListItem>
                    <asp:ListItem Value="2">Pre Comité de Contratación</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvapproved" runat="server" ControlToValidate="ddlapproved"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td style="height: 25px">
                <asp:Label ID="lblHelpapproved" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblattachment" runat="server" Text="Archivo adjunto"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:FileUpload runat="server" ID="fuattachment"></asp:FileUpload>
                <asp:HyperLink runat="server" Target="_blank" ID="hlattachment" Visible="False"></asp:HyperLink>
            </td>
            <td>
                <asp:Label ID="lblHelpattachment" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="Lbltitlearchivo" runat="server" ForeColor="#9bbb58"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblHelpapprovaldate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 131px">
                <asp:Label ID="lblenabled" runat="server" Text="Estado"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:DropDownList runat="server" ID="ddlenabled">
                    <asp:ListItem Text="Habilitado" Value="True"></asp:ListItem>
                    <asp:ListItem Text="Deshabilitado" Value="False"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvapproved0" runat="server" ControlToValidate="ddlenabled"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="lblHelpenabled" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
            <td id="gridthird" style="width: 577px">
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lbliduser" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtiduser" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpiduser" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 131px">
                <asp:Label ID="lblcreatedate" runat="server" Text="Fecha de creación"></asp:Label>
            </td>
            <td style="width: 577px">
                <asp:TextBox ID="txtcreatedate" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblHelpcreatedate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblBPMMessage" runat="server" Text="Por Favor Seleccione la Actividad"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:RadioButtonList ID="rblCondition" runat="server" Visible="False">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnAddData" runat="server" Text="Aprobar Idea" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar" />
                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CausesValidation="False" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="False" />
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
