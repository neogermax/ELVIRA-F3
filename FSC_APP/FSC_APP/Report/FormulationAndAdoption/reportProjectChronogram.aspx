<%@ Page Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_reportProjectChronogram"
    Title="Untitled Page" Codebehind="reportProjectChronogram.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">
    <link rel="stylesheet" type="text/css" href="../../Include/javascript/jsgantt/jsgantt.css" />

    <script type="text/javascript" src="../../Include/javascript/jsgantt/jsgantt.js"></script>

    <script src="../../Include/javascript/jquery-1.6.1.min.js" type="text/javascript"></script>

    <script src="../../Include/javascript/chosen.jquery.min.js" type="text/javascript"></script>

    <table width="100%">
        <tr>
            <td style="width: 107px">
                Proyecto
            </td>
            <td>
                <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="Ccombo">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvProject" runat="server" ControlToValidate="ddlProject"
                    Display="Dynamic" ErrorMessage="Seleccione un proyecto" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 107px">
                Componente
            </td>
            <td>
                <asp:DropDownList ID="ddlComponent" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvComponent" runat="server" ControlToValidate="ddlComponent"
                    Display="Dynamic" ErrorMessage="Seleccione un componente" InitialValue="0"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td style="width: 107px">
                <asp:Label runat="server" ID="Label1">Fase</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlProjectPhase" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnSearch" runat="server" Text="Mostrar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="position: relative" class="gantt" id="GanttChartDIV" style="width=100% height=500px">
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var g = new JSGantt.GanttChart('g', document.getElementById('GanttChartDIV'), 'day');

        if (g) {

            g.setShowRes(1); // Show/Hide Responsible (0/1)
            g.setShowDur(1); // Show/Hide Duration (0/1)
            g.setShowComp(0); // Show/Hide % Complete(0/1)
            g.setCaptionType('Duration');  // Set to Show Caption (None,Caption,Resource,Duration,Complete)
            g.setShowStartDate(1); // Show/Hide Start Date(0/1)
            g.setShowEndDate(1); // Show/Hide End Date(0/1)
            g.setDateInputFormat('dd/mm/yyyy')  // Set format of input dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')
            g.setDateDisplayFormat('dd/mm/yyyy') // Set format to display dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')
            g.setFormatArr("day", "week", "month", "quarter") // Set format options (up to 4 : "minute","hour","day","week","month","quarter")

            if (g) {
                JSGantt.parseXML("actividades.xml", g);
                g.Draw();
                g.DrawDependencies();
            }

        }
            
    </script>

</asp:Content>
