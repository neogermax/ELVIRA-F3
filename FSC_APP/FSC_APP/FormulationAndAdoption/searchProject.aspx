<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.searchProject" title="searchProject" Codebehind="searchProject.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" Runat="Server">
<script type="text/javascript">
    $(document).ready(function() {

//        if ($("#gvData").html() == null) {
//            
//            $("#export").css("display", "block");
//        }
//        if ($("#gvData").html() != null) {
//            $("#export").css("display", "block");
//        }

    });
    
    function exportModalidades() {
        var uri = 'data:application/vnd.ms-excel;base64,';
        var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>';
        var base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) };
        var format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) };

        var table = $("#ctl00_cphPrincipal_gvDataExport");
        var ctx = { worksheet: "Busqueda Proyecto!" || 'Worksheet', table: $(table).html() };
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
                                <asp:ListItem Value="id" Selected="True">Id</asp:ListItem>
                               <%-- <asp:ListItem Value="ideaname">Nombre del Proyecto</asp:ListItem>--%>
                                <asp:ListItem Value="name">Nombre del Proyecto</asp:ListItem>
                                <asp:ListItem Value="code">Código</asp:ListItem>
                                <asp:ListItem Value="StrategicLinename">Linea Estrategica</asp:ListItem>
                                <asp:ListItem Value="Programname"> Programa</asp:ListItem>
                                <%--<asp:ListItem Value="ProgramComponentname">Componente del Programa</asp:ListItem>--%>
                                <%--<asp:ListItem Value="currentactivityname">Actividad actual</asp:ListItem>--%>                                
                                <%--<asp:ListItem Value="enabled">Estado</asp:ListItem>--%>
                                <asp:ListItem Value="username">Usuario</asp:ListItem>
                                <%--<asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>--%>
							</asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar"/>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <%--<asp:Label ID="lblSort" runat="server" Text="Ordenar por"></asp:Label>
                            <asp:DropDownList ID="ddlSort" runat="server">
	                            <asp:ListItem Value="id">Id</asp:ListItem>
	                            <asp:ListItem Value="ideaname">Idea</asp:ListItem>
	                            <asp:ListItem Value="code">Código</asp:ListItem>
	                            <asp:ListItem Value="name">Nombre/Descripción</asp:ListItem>
	                            <asp:ListItem Value="StrategicLinename">Linea Estrategica</asp:ListItem>
	                            <asp:ListItem Value="Programname">Programa</asp:ListItem> 
	                            <asp:ListItem Value="enabled">Estado</asp:ListItem>
	                            <asp:ListItem Value="username">Usuario</asp:ListItem>
	                            <asp:ListItem Value="createdate">Fecha de creación</asp:ListItem>
	                            <asp:ListItem Value="completiondate">Fecha de finalización</asp:ListItem>
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
						<asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" PageSize="10">
							<Columns>
		                        <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="addProject.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                        <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="ideaname" HeaderText="Idea" />
                                <asp:BoundField DataField="code" HeaderText="Código" />
                                <asp:BoundField DataField="name" HeaderText="Nombre/Descripción" />
                                <asp:BoundField DataField="StrategicLineNAME" HeaderText="Linea Estrategica" />
                                <asp:BoundField DataField="ProgramNAME" HeaderText="Programa" />
                                <asp:BoundField DataField="ProgramComponentBYPROJECTLISTTEXT" HeaderText="Componentes del Programa" HtmlEncode = "false" />
                                <asp:BoundField DataField="BeginDate" HeaderText="Fecha de inicio" DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="completiondate" HeaderText="Fecha de finalización"  DataFormatString="{0:d}"/>
							
                                <asp:BoundField visible="false" DataField="FORUMLISTTEXT" HeaderText="Foros" HtmlEncode = "false" />
                                <asp:BoundField visible="false" DataField="CURRENTACTIVITYNAME" HeaderText="Actividad actual" HtmlEncode = "false" />                                
                                <asp:BoundField visible="false" DataField="enabled" HeaderText="Estado" />
                                <asp:BoundField DataField="username" HeaderText="Usuario" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de creación" />
                                </Columns>
						</asp:GridView>
						<asp:GridView ID="gvDataExport" style="display:none; " runat="server" AutoGenerateColumns="False" AllowPaging="False">
							<Columns>
		                        <%--<asp:HyperLinkField DataNavigateUrlFields="idkey" DataNavigateUrlFormatString="addProject.aspx?op=edit&id={0}" HeaderText="Operaciones" Text="Editar" />
		                --%>        <asp:BoundField DataField="id" HeaderText="Id" />
                                <asp:BoundField DataField="ideaname" HeaderText="Idea" />
                                <asp:BoundField DataField="code" HeaderText="Código" />
                                <asp:BoundField DataField="name" HeaderText="Nombre/Descripción" />
                                <asp:BoundField DataField="StrategicLineNAME" HeaderText="Linea Estrategica" />
                                <asp:BoundField DataField="ProgramNAME" HeaderText="Programa" />
                                <asp:BoundField DataField="ProgramComponentBYPROJECTLISTTEXT" HeaderText="Componentes del Programa" HtmlEncode = "false" />
                                <asp:BoundField DataField="BeginDate" HeaderText="Fecha de inicio" DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="completiondate" HeaderText="Fecha de finalización"  DataFormatString="{0:d}"/>
							
                                <asp:BoundField visible="false" DataField="FORUMLISTTEXT" HeaderText="Foros" HtmlEncode = "false" />
                                <asp:BoundField visible="false" DataField="CURRENTACTIVITYNAME" HeaderText="Actividad actual" HtmlEncode = "false" />                                
                                <asp:BoundField visible="false" DataField="enabled" HeaderText="Estado" />
                                <asp:BoundField DataField="username" HeaderText="Usuario" />
                                <asp:BoundField DataField="createdate" HeaderText="Fecha de creación" />
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
