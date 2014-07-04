<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false"
    Inherits="FSC_APP.searchIdea" Title="searchIdea" CodeBehind="searchIdea.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

    <script type="text/javascript">
        function exportModalidades() {
            var uri = 'data:application/vnd.ms-excel;base64,';
            var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>';
            var base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) };
            var format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) };
            var table = $("#ctl00_cphPrincipal_gvDataexp");
            var ctx = { worksheet: "Busqueda Idea!" || 'Worksheet', table: $(table).html() };
            window.location.href = uri + base64(format(template, ctx));

        }
    </script>

    <table style="width: 100%">
        <tr>
            <td align="left">
                <table style="width: 100%">
                    <tr valign="top">
                        <td>
                            <asp:RadioButtonList ID="rblSearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="idlike">Id</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="name">Nombre</asp:ListItem>
                                <asp:ListItem Value="StrategicLineName">Linea Estrategica</asp:ListItem>
                                <asp:ListItem Value="ProgramName">Programa</asp:ListItem>
                                <%-- <asp:ListItem Value="ProgramComponentName">Componente del Programa</asp:ListItem>
                                <asp:ListItem Value="startprocesstext">Proceso Creado</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>--%>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <%--     <asp:ListItem Value="enabledtext">Estado</asp:ListItem>--%>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblStatus" runat="server" Text="Estado de la idea"></asp:Label>
                            <asp:DropDownList ID="ddlFinished" runat="server">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Aprobado</asp:ListItem>
                                <asp:ListItem Value="2">No aprobado</asp:ListItem>
                                <asp:ListItem Value="3">Pendiente de aprobación</asp:ListItem>
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
                                <asp:ListItem Value="id">Id</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="name">Nombre</asp:ListItem>
                                <asp:ListItem Value="StrategicLineName">Linea Estrategica</asp:ListItem>
                                <asp:ListItem Value="ProgramName">Programa</asp:ListItem>
                                <asp:ListItem Value="ProgramComponentName">Componente del Programa</asp:ListItem>
                                <asp:ListItem Value="startprocess">Proceso Creado</asp:ListItem>
                                <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <asp:ListItem Value="enabled">Estado</asp:ListItem>
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
                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addIdea.aspx?op=edit&id={0}"
                                    HeaderText="Operaciones" Text="Editar" />
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="code" HeaderText="Código" />
                                <asp:BoundField DataField="name" HeaderText="Nombre" />
                                <asp:BoundField DataField="StrategicLineNAME" HeaderText="Linea Estrategica" />
                                <asp:BoundField DataField="ProgramNAME" HeaderText="Programa" />
                                <asp:BoundField DataField="ProgramComponentNAME" HeaderText="Componente del Programa" />
                                <asp:BoundField DataField="startprocess" HeaderText="Proceso Creado" Visible="False" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de Creación" />
                                <asp:BoundField DataField="StartDate" HeaderText="Fecha de Inicio" />
                                <asp:BoundField DataField="Enddate" HeaderText="Fecha de Finalización" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" Visible="False" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvDataexp" Style="display: none" runat="server" AutoGenerateColumns="False"
                            AllowPaging="false" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="code" HeaderText="Código" />
                                <asp:BoundField DataField="name" HeaderText="Nombre" />
                                <asp:BoundField DataField="StrategicLineNAME" HeaderText="Linea Estrategica" />
                                <asp:BoundField DataField="ProgramNAME" HeaderText="Programa" />
                                <asp:BoundField DataField="ProgramComponentNAME" HeaderText="Componente del Programa" />
                                <asp:BoundField DataField="startprocess" HeaderText="Proceso Creado" Visible="False" />
                                <asp:BoundField DataField="USERNAME" HeaderText="Usuario" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de Creación" />
                                <asp:BoundField DataField="StartDate" HeaderText="Fecha de Inicio" />
                                <asp:BoundField DataField="Enddate" HeaderText="Fecha de Finalización" />
                                <asp:BoundField DataField="enabled" HeaderText="Estado" Visible="False" />
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
