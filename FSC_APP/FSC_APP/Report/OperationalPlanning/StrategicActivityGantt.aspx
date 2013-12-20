<%@ Page Title="" Language="VB" MasterPageFile="~/Master/mpAdmin.master" AutoEventWireup="false" Inherits="FSC_APP.Report_FormulationAndAdoption_StrategicActivityGantt" Codebehind="StrategicActivityGantt.aspx.vb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="Server">

<head>
    <link rel="stylesheet" type="text/css" href="../../Include/javascript/jsgantt/jsgantt.css" />

    <script type="text/javascript" src="../../Include/javascript/jsgantt/jsgantt.js"></script>

    <style type="text/css">
        .style1
        {
            font-family: "Berlin Sans FB";
            font-size: medium;
        }
    </style>
</head>
      
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblidstrategy" runat="server" Text="Estrategia"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlidstrategy" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Mostrar" />
            </td>
        </tr>
        <tr>
        <td colspan="3">
        <hr />
        </td>
        </tr>
        <tr>
            <td colspan="3">
                <div style="position: relative" class="gantt" id="GanttChartDIV" style="Width=100% Height=500px">
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
                g.setCaptionType('Resource');  // Set to Show Caption (None,Caption,Resource,Duration,Complete)
                g.setShowStartDate(1); // Show/Hide Start Date(0/1)
                g.setShowEndDate(1); // Show/Hide End Date(0/1)
                g.setDateInputFormat('dd/dd/yyyy')  // Set format of input dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')
                g.setDateDisplayFormat('dd/mm/yyyy') // Set format to display dates ('mm/dd/yyyy', 'dd/mm/yyyy', 'yyyy-mm-dd')
                g.setFormatArr("day", "week", "month", "quarter") // Set format options (up to 4 : "minute","hour","day","week","month","quarter")

                if (g) {
                    JSGantt.parseXML("tareas.xml", g);
                    g.Draw();
                    g.DrawDependencies();
                }

            }
            
    </script>

</asp:Content>
