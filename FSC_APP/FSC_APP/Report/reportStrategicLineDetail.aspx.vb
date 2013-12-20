Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_reportStrategicLineDetail
    Inherits System.Web.UI.Page
#Region "Eventos"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sIdStrategicLine As String = ""

        If Not Page.IsPostBack Then

            ' cargar el titulo
            Session("lblTitle") = "Reporte detalle del Linea Estrategica"
            'si entra un Linea Estrategica por parametro entonces se busca sino es asi deja al usuario que lo busque por el menu de selección
            sIdStrategicLine = Request.QueryString("IdStrategicLine")
            If Not sIdStrategicLine Is Nothing Then
                LoadReport(sIdStrategicLine)
            End If
            'Carga el combo con los Linea Estrategica disponibles
            loadCombos()
            Try

            Catch ex As Exception
                'mostrando el error
                Session("serror") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/errors/error.aspx")
                Response.End()
            Finally

            End Try

        End If
    End Sub
    Protected Sub ddlStrategicLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrategicLine.SelectedIndexChanged
        ' carga el reporte segun la selección
        LoadReport(ddlStrategicLine.SelectedValue.ToString())
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=ReporteDetalleLineaEstrategica.xls")
        Response.Charset = "iso-8859-1"
        Response.ContentEncoding = Encoding.Default
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"
        Response.Write(lblTable.Text)
        Response.End()
    End Sub
#End Region

#Region "Metodos"
    Protected Sub LoadReport(ByVal sIdStrategicLine As String)
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        Dim sTable As New StringBuilder
        Dim sNameProgram As String
        Dim bMakeActivityTable As Boolean
        Dim drProgramComponents As DataRow()
        Dim bFirstTime As Boolean
        Dim sIndicatorstable As String
        Dim IdStrategicLine As String
        sNameProgram = Nothing
        bFirstTime = True
        bMakeActivityTable = True
        Try
            IdStrategicLine = sIdStrategicLine
            If IdStrategicLine > 0 Then
                sIndicatorstable = MakeIndicatorsTable(facade.loadIndicators(applicationCredentials, CInt(IdStrategicLine), -1))
                dt = facade.loadReportStrategicLineDetail(applicationCredentials, IdStrategicLine)
                sTable.AppendLine("<table border='1' cellspacing='0' cellpading='0' width='100%'>")
                sTable.AppendLine("<tr>")
                sTable.AppendLine("<td>")
                sTable.AppendLine("Título de la Linea Estrategica")
                sTable.AppendLine("</td>")
                sTable.AppendLine("<td>")
                sTable.AppendLine("Título de Programa")
                sTable.AppendLine("</td>")
                sTable.AppendLine("<td>")
                sTable.AppendLine("Componente del Programa")
                sTable.AppendLine("</td>")
                sTable.AppendLine("<td>Descripción</td>")
                sTable.AppendLine("<td valign='top' rowspan='" & dt.Rows.Count + 1 & "'>")
                sTable.AppendLine(sIndicatorstable)
                sTable.AppendLine("</td>")
                sTable.AppendLine("</tr>")
                sTable.AppendLine("<tr>")
                sTable.AppendLine("<td rowspan='" & dt.Rows.Count & "'>")
                If dt.Rows.Count > 0 Then
                    sTable.AppendLine(dt.Rows(0)("StrategicLineName").ToString())
                Else
                    sTable.AppendLine("")
                End If
                sTable.AppendLine("</td>")
                For Each Macro As DataRow In dt.Rows
                    If Not sNameProgram = Macro("ProgramName").ToString() And bMakeActivityTable Then
                        sNameProgram = Macro("ProgramName").ToString()
                        drProgramComponents = dt.Select("ProgramName ='" & sNameProgram & "'")
                        If Not bFirstTime Then
                            bFirstTime = False
                            sTable.AppendLine("<tr>")
                        End If
                        sTable.AppendLine("<td rowspan='" & drProgramComponents.Length & "'>")
                        sTable.AppendLine(sNameProgram)
                        sTable.AppendLine("</td>")
                        'bMakeActivityTable = False
                        sTable.AppendLine(MakeProgramComponentsTable(drProgramComponents))

                    End If
                Next



                sTable.AppendLine("</table>")
                lblTable.Text = sTable.ToString()
                'Response.Write(sTable.ToString())
            Else
                lblTable.Text = ""
            End If
        Catch ex As Exception
            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()
        Finally
            dt = Nothing
            facade = Nothing
        End Try
    End Sub

    Private Function MakeProgramComponentsTable(ByVal ProgramComponents As DataRow()) As String
        Dim sActivityTable As New StringBuilder

        For Each Activity As DataRow In ProgramComponents
            sActivityTable.AppendLine("<tr>")
            sActivityTable.AppendLine("<td>")
            sActivityTable.AppendLine(Activity("ProgramComponentName").ToString())
            sActivityTable.AppendLine("</td>")
            sActivityTable.AppendLine("<td>")
            sActivityTable.AppendLine(Activity("ProgramComponentDescription").ToString())
            sActivityTable.AppendLine("</td>")

            sActivityTable.AppendLine("</tr>")
        Next
        Return sActivityTable.ToString().Remove(0, 4)
    End Function

    Private Function MakeIndicatorsTable(ByVal dtIndicators As DataTable) As String
        Dim sMeasurementDateByIndicator As String = ""
        Dim sIndicatorsTable As New StringBuilder
        sIndicatorsTable.AppendLine("<table border='1' cellspacing='0' cellpading='0' height='100%'  width='100%'>")
        sIndicatorsTable.AppendLine("<tr>")
        sIndicatorsTable.AppendLine("<td>")
        sIndicatorsTable.AppendLine("Descripción")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("<td>")
        sIndicatorsTable.AppendLine("Meta")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("<td bgcolor='#006600'>")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("<td bgcolor='#FFFF00'>")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("<td bgcolor='#FF0000'>")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("<td>")
        sIndicatorsTable.AppendLine("Fechas de medición")
        sIndicatorsTable.AppendLine("</td>")
        sIndicatorsTable.AppendLine("</tr>")
        For Each Indicator As DataRow In dtIndicators.Rows
            sIndicatorsTable.AppendLine("<tr>")
            sIndicatorsTable.AppendLine("<td>")
            sIndicatorsTable.AppendLine(Indicator("Description"))
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("<td>")
            sIndicatorsTable.AppendLine(Indicator("Goal"))
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("<td>")
            sIndicatorsTable.AppendLine(Indicator("GreenValue"))
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("<td>")
            sIndicatorsTable.AppendLine(Indicator("YellowValue"))
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("<td>")
            sIndicatorsTable.AppendLine(Indicator("RedValue"))
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("<td>")
            sMeasurementDateByIndicator = loadMeasurementDateByIndicator(Indicator("IdIndicator").ToString())
            'sIndicatorsTable.AppendLine(Indicator("measurementDate"))
            sIndicatorsTable.AppendLine(sMeasurementDateByIndicator)
            sIndicatorsTable.AppendLine("</td>")
            sIndicatorsTable.AppendLine("</tr>")
        Next
        sIndicatorsTable.AppendLine("</table>")
        Return sIndicatorsTable.ToString()
    End Function

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dtStrategicLine = facade.loadReportStrategicLineDetail(applicationCredentials)
        Dim dvStrategicLine = dtStrategicLine.DefaultView
        dtStrategicLine = dvStrategicLine.ToTable(True, "StrategicLineCode", "IdStrategicLine")

        Try

            ' cargar la lista de las estrategias

            Me.ddlStrategicLine.DataSource = dtStrategicLine
            Me.ddlStrategicLine.DataValueField = "IdStrategicLine"
            Me.ddlStrategicLine.DataTextField = "StrategicLineCode"
            Me.ddlStrategicLine.DataBind()
            Me.ddlStrategicLine.Items.Insert(0, New ListItem("-- Seleccione --", "0"))



        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            dtStrategicLine = Nothing
            dvStrategicLine = Nothing
            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Carga las medidas de medición para un Indicador
    ''' </summary>
    ''' <param name="sIndicator">identificador Inidcador</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function loadMeasurementDateByIndicator(ByVal sIndicator As String) As String
        Dim dtMeasurementDateByIndicator As New DataTable()
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim sMeasurementDateByIndicator As String = ""
        Dim drMesurementDateByInidator As DataRow
        dtMeasurementDateByIndicator = facade.loadMeasurementDateByIndicator(applicationCredentials, sIndicator)
        For Each drMesurementDateByInidator In dtMeasurementDateByIndicator.Rows
            sMeasurementDateByIndicator = sMeasurementDateByIndicator + drMesurementDateByInidator("measurementDate") + "<br>"
        Next
        Return sMeasurementDateByIndicator
    End Function

#End Region


End Class
