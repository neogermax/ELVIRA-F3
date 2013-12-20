<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchContractRequest" Title="searchContractRequest" Codebehind="searchContractRequest.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript">
        function exportModalidades() {
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>';
            var base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) };
            var format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) };
            var table = $("#ctl00_cphPrincipal_gvDataexp");
            var ctx = { worksheet: "Busqueda Contratacion!" || 'Worksheet', table: $(table).html() };
            window.location.href = uri + base64(format(template, ctx));

        }
    </script>

    <br />
    <div id="containerSuccess" runat="server" visible="false" style="width: 100%; text-align: center;
        border: 2px solid #cecece; background: #E8E8DC; height: 40px; line-height: 40px;
        vertical-align: middle;">
        <img style="margin-top: 5px;" src="/images/save_icon.png" width="24px" alt="Save" />
        <asp:Label ID="lblsaveinformation" runat="server" Style="font-size: 14pt;"></asp:Label>
    </div>
    <br />
    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <%--<asp:ListItem Value="requestnumberlike">No. Solicitud</asp:ListItem>--%>
                                <%--<asp:ListItem Value="managementname">Gerencia</asp:ListItem>--%>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="idcontractnature">Naturaleza de Contratación</asp:ListItem>
                                <asp:ListItem Value="contractnumberadjusted">No. de Contrato</asp:ListItem>
                                <%--<asp:ListItem Value="username">Usuario</asp:ListItem>--%>
                                <%--<asp:ListItem Value="enabledtext">Estado</asp:ListItem>--%>
                                <asp:ListItem Value="createdate">Fecha Creación Contratación</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblStatus" runat="server" Text="Estado de contratación"></asp:Label>
                            <asp:DropDownList ID="ddlFinished" runat="server">
                                <asp:ListItem Value="all">Todos</asp:ListItem>
                                <asp:ListItem Value="proccess">Pendiente finalizar proceso</asp:ListItem>
                                <asp:ListItem Value="finished">Proceso Finalizado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <%--<asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
                                <asp:ListItem Value="requestnumber">No. Solicitud</asp:ListItem>
                                <asp:ListItem Value="managementname">Gerencia</asp:ListItem>
                                <asp:ListItem Value="projectname">Proyecto</asp:ListItem>
                                <asp:ListItem Value="idcontractnature">Naturaleza Contrato</asp:ListItem>
                                <asp:ListItem Value="contractnumberadjusted">No. Contrato que se ajusta</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha Creación</asp:ListItem>
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upSubTitle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upData" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            Width="100%">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="requestnumber" DataNavigateUrlFormatString="addContractRequest.aspx?op=edit&ID={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="requestnumber" HeaderText="No. Solicitud" />
                                <asp:BoundField DataField="MANAGEMENTNAME" HeaderText="Gerencia" />
                                <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
                                <asp:BoundField DataField="idcontractnature" HeaderText="Naturaleza Contrato" />
                                <asp:BoundField DataField="contractnumberadjusted" HeaderText="No. de Contrato" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha Creación">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvDataexp" Style="display: none" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="requestnumber" HeaderText="No. Solicitud" />
                                <asp:BoundField DataField="MANAGEMENTNAME" HeaderText="Gerencia" />
                                <asp:BoundField DataField="PROJECTNAME" HeaderText="Proyecto" />
                                <asp:BoundField DataField="idcontractnature" HeaderText="Naturaleza Contrato" />
                                <asp:BoundField DataField="contractnumberadjusted" HeaderText="No. de Contrato" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha Creación">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <button id="export" onclick="exportModalidades(); return false;" runat="server">
                    Exportar Busqueda</button>
            </td>
        </tr>
    </table>
</asp:Content>
