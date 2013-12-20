Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_reportGeneralPlanAction
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
       

       
        If Not Page.IsPostBack Then
            If ddlyear.SelectedValue.ToString() <> "0" And ddlyear.SelectedValue.ToString() <> "" Then
                LoadReport()

            End If
            ' cargar el titulo
            Session("lblTitle") = "Reporte plan de acción general"
            loadCombos()
           

        End If
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlyear.SelectedIndexChanged
       
        Try
            If ddlyear.SelectedValue.ToString() <> "0" Then
                LoadReport()
            End If

        Catch ex As Exception
            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()
        Finally
          
        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=ReportePlanGeneral.xls")
        Response.Charset = "iso-8859-1"
        Response.ContentEncoding = Encoding.Default
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"
        Response.Write(lblTable.Text)
        Response.End()
    End Sub

#End Region
#Region "Metodos"
    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        Try

            ' cargar la lista de los tipos

            Me.ddlyear.DataSource = facade.getListYear(applicationCredentials)
            Me.ddlyear.DataValueField = "Year"
            Me.ddlyear.DataTextField = "Year"
            Me.ddlyear.DataBind()
            Me.ddlyear.Items.Insert(0, New ListItem("-- Seleccione --", "0"))



        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    Private Sub LoadReport()
        Dim reportName As String = "GeneralPlan"
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        dt = facade.loadReportGeneralPlan(applicationCredentials, ddlyear.SelectedValue.ToString())
        Dim dvFilterPerspective As New DataView
        Dim dvFilterStrategyStrategicLine As New DataView
        Dim dvFilterStrategyObjective As New DataView
        Dim dvFilterIndicator As New DataView
        Dim dtPerspectiva As New DataTable()
        Dim dtStrategyObjective As New DataTable()
        Dim dtIndicator As New DataTable()
        Dim dtStrategyStrategicLine As New DataTable()
        Dim sTabla As String
        Dim drRowStrategyObjective As DataRow
        Dim drRowIndicator As DataRow
        Dim inumIndicator As Integer
        Dim inumStrategyStrategicLine As Integer
        Dim inumRowSpan As Integer
        Dim inumFilasSpan As Integer
        Dim iCountStrategyStrategicLine As Integer = 1
        Dim iCountAcumulador As Integer = 0
        Dim iCount As Integer = 1
        Dim iCountStrategicObjective As Integer = 1
        Dim sMeasurementDateByIndicator As String = ""
        Try
            dvFilterPerspective = dt.DefaultView
            dtPerspectiva = dvFilterPerspective.ToTable(True, "IdPerspective", "Perspective").Copy()
            sTabla = "<table width='100%'  border='1' cellspacing='0' cellpadding='0'>"
            sTabla = sTabla + " <tr>    <td>Perspectiva</td> <td>Objetivo Estratégico</td>    <td>Indicadores</td> <td>Meta</td>"
            sTabla = sTabla + " <td bgcolor='#006600'>&nbsp;</td>    <td bgcolor='#FFFF00'>&nbsp;</td>    <td bgcolor='#FF0000'>&nbsp;</td>  <td>Fechas de Medición</td>  <td>Estrategia/Linea Estrategica</td>  </tr>"
            For Each drPerspective In dtPerspectiva.Rows
                dvFilterIndicator = dt.DefaultView
                dvFilterIndicator.RowFilter = "IdPerspective='" + drPerspective("IdPerspective").ToString() + "'"
                dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator")
                inumIndicator = dtIndicator.Rows.Count
                dtStrategyStrategicLine = dvFilterIndicator.ToTable(True, "IdStrategyStrategicLine")
                inumStrategyStrategicLine = dtStrategyStrategicLine.Rows.Count
                'Consultamos las estrategias 
                dvFilterStrategyObjective = dt.DefaultView
                dvFilterStrategyObjective.RowFilter = "IdPerspective='" + drPerspective("IdPerspective").ToString() + "'"
                dtStrategyObjective = dvFilterStrategyObjective.ToTable(True, "StrategicObjective", "IdStrategicObjective").Copy()
                inumRowSpan = 0
                For Each drRowStrategyObjective In dtStrategyObjective.Rows
                    'Determinamos el número de filas a Combiar para la perspectiva
                    dvFilterIndicator = dt.DefaultView
                    dvFilterIndicator.RowFilter = "IdStrategicObjective='" + drRowStrategyObjective("IdStrategicObjective").ToString() + "'"
                    dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator")
                    inumIndicator = dtIndicator.Rows.Count
                    dtStrategyStrategicLine = dvFilterIndicator.ToTable(True, "StrategyStrategicLine", "IdStrategyStrategicLine")
                    inumStrategyStrategicLine = dtStrategyStrategicLine.Rows.Count
                    If inumIndicator > inumStrategyStrategicLine Then
                        inumRowSpan = inumRowSpan + inumIndicator

                    Else
                        inumRowSpan = inumRowSpan + inumStrategyStrategicLine

                    End If
                Next
                'el primer elemento la perspectiva
                sTabla = sTabla + "<tr><td rowspan='" + inumRowSpan.ToString() + "'>" + drPerspective("Perspective") + "</td>"

                'Consultamos los objetivos estrategicos
                For Each drRowStrategyObjective In dtStrategyObjective.Rows
                    dvFilterIndicator = dt.DefaultView
                    dvFilterIndicator.RowFilter = "IdStrategicObjective='" + drRowStrategyObjective("IdStrategicObjective").ToString() + "'"
                    dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator")
                    inumIndicator = dtIndicator.Rows.Count
                    dtStrategyStrategicLine = dvFilterIndicator.ToTable(True, "StrategyStrategicLine", "IdStrategyStrategicLine", "Type", "IdStrategyStrategicLine1")
                    inumStrategyStrategicLine = dtStrategyStrategicLine.Rows.Count
                    'Consultamos cual es mayor entre los indicadores y las strategias o Lineas Estrategicas
                    If inumIndicator > inumStrategyStrategicLine Then
                        inumRowSpan = inumIndicator
                        inumFilasSpan = Math.Ceiling(inumIndicator / inumStrategyStrategicLine)
                    Else
                        inumRowSpan = inumStrategyStrategicLine
                        'dividimos para saber la cantidad de filas a combinar
                        inumFilasSpan = Math.Ceiling(inumStrategyStrategicLine / inumIndicator)
                    End If
                    ' el segundo elemento el objetivo estrategico
                    If iCountStrategicObjective = 1 Then
                        sTabla = sTabla + "<td rowspan='" + inumRowSpan.ToString() + "'>" + drRowStrategyObjective("StrategicObjective") + "</td>"
                    Else
                        sTabla = sTabla + "<tr><td rowspan='" + inumRowSpan.ToString() + "'>" + drRowStrategyObjective("StrategicObjective") + "</td>"
                    End If

                    iCountStrategicObjective = iCountStrategicObjective + 1
                    'Consultamos los indicadores que tiene un objetivo
                    dvFilterIndicator = dt.DefaultView
                    dvFilterIndicator.RowFilter = "IdStrategicObjective='" + drRowStrategyObjective("IdStrategicObjective").ToString() + "'"
                    dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator", "Description", "Goal", "GreenValue", "YellowValue", "RedValue")
                    For Each drRowIndicator In dtIndicator.Rows
                        ' consultamos las fechas de medición 
                        'sMeasurementDateByIndicator = loadMeasurementDateByIndicator(drRowIndicator("IdIndicator"))
                        sMeasurementDateByIndicator = loadMeasurementDateByIndicator(0)
                        If iCount = 1 Then
                            If inumStrategyStrategicLine > inumIndicator Then
                                sTabla = sTabla + "<td   rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("Description").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("Goal").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + drRowIndicator("RedValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + sMeasurementDateByIndicator + "</td>"
                            Else
                                sTabla = sTabla + "<td   >" + drRowIndicator("Description").ToString() + "</td>" + "<td  >" + drRowIndicator("Goal").ToString() + "</td>" + "<td >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td >" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td >" + sMeasurementDateByIndicator + "</td>"
                            End If

                        Else
                            If inumStrategyStrategicLine > inumIndicator Then
                                sTabla = sTabla + "<tr>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "' >" + drRowIndicator("Description").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "' >" + drRowIndicator("Goal").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "' >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "'>" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "' >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "' >" + sMeasurementDateByIndicator + "</td>"
                            Else
                                sTabla = sTabla + "<tr>" + "<td  >" + drRowIndicator("Description").ToString() + "</td>" + "<td  >" + drRowIndicator("Goal").ToString() + "</td>" + "<td  >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td >" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td  >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td  >" + sMeasurementDateByIndicator + "</td>"
                            End If
                        End If
                        'si las estrategias son mayores que los indicadores las estrategias no se combinan
                        If inumStrategyStrategicLine > inumIndicator Then
                            Dim iFor As Integer
                            For iFor = iCountAcumulador To dtStrategyStrategicLine.Rows.Count - 1


                                If iCountStrategyStrategicLine = 1 Then
                                    sTabla = sTabla + "<td   >" + IIf(dtStrategyStrategicLine.Rows(iFor)("Type").ToString() = "Linea Estrategica", "<a href='reportStrategicLineDetail.aspx?IdStrategicLine=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>", "<a href='reportStrategyDetail.aspx?IdStrategy=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>") + dtStrategyStrategicLine.Rows(iFor)("StrategyStrategicLine").ToString() + "</a></td>" + "</tr>"
                                Else
                                    sTabla = sTabla + "<tr><td   >" + IIf(dtStrategyStrategicLine.Rows(iFor)("Type").ToString() = "Linea Estrategica", "<a href='reportStrategicLineDetail.aspx?IdStrategicLine=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>", "<a href='reportStrategyDetail.aspx?IdStrategy=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>") + dtStrategyStrategicLine.Rows(iFor)("StrategyStrategicLine").ToString() + "</a></td>" + "</tr>"
                                End If


                                If iCountStrategyStrategicLine >= inumFilasSpan Then
                                    iCountAcumulador = iCountStrategyStrategicLine
                                    Exit For
                                End If


                                iCountStrategyStrategicLine = iCountStrategyStrategicLine + 1
                            Next

                            iCountStrategyStrategicLine = 1
                        Else
                            'si los indicadores son mayores que las estrategias las estrategias se combinan
                            Dim iFor As Integer
                            For iFor = iCountAcumulador To dtStrategyStrategicLine.Rows.Count - 1
                                If iCountStrategyStrategicLine = 1 And iCountAcumulador = 0 Then
                                    sTabla = sTabla + "<td rowspan='" + inumFilasSpan.ToString() + "'   >" + IIf(dtStrategyStrategicLine.Rows(iFor)("Type").ToString() = "Linea Estrategica", "<a href='reportStrategicLineDetail.aspx?IdStrategicLine=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>", "<a href='reportStrategyDetail.aspx?IdStrategy=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>") + dtStrategyStrategicLine.Rows(iFor)("StrategyStrategicLine").ToString() + "</a></td>" + "</tr>"

                                Else
                                    If inumFilasSpan < iCount Then
                                        sTabla = sTabla + "<td  rowspan='" + Convert.ToString(inumRowSpan - inumFilasSpan) + "'   >" + IIf(dtStrategyStrategicLine.Rows(iFor)("Type").ToString() = "Linea Estrategica", "<a href='reportStrategicLineDetail.aspx?IdStrategicLine=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>", "<a href='reportStrategyDetail.aspx?IdStrategy=" + dtStrategyStrategicLine.Rows(iFor)("IdStrategyStrategicLine1").ToString() + "'>") + dtStrategyStrategicLine.Rows(iFor)("StrategyStrategicLine").ToString() + "</a></td>" + "</tr>"
                                        iCountAcumulador = iFor + 1
                                    End If
                                End If

                                If iFor = 0 Then
                                    iCountAcumulador = iFor + 1
                                    Exit For
                                End If
                                iCountStrategyStrategicLine = iCountStrategyStrategicLine + 1

                            Next
                            iCountStrategyStrategicLine = 1
                        End If
                        iCount = iCount + 1
                    Next
                    iCountAcumulador = 0
                    iCount = 1
                Next

                iCountStrategicObjective = 1
            Next
            sTabla = sTabla + "</table>"
            lblTable.Text = sTabla
       

        Catch ex As Exception
            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()
        Finally
            dt = Nothing
            dtPerspectiva = Nothing
            dtStrategyObjective = Nothing
            dvFilterPerspective = Nothing
            dvFilterStrategyObjective = Nothing
            dtPerspectiva = Nothing
            drRowStrategyObjective = Nothing
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
