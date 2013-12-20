Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_reportDetalleLineasEstrategicas
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
        Dim sIdStrategy As String = ""
        If Not Page.IsPostBack Then
            Try

                ' cargar el titulo
                Session("lblTitle") = "Reporte detalle estrategia"
                'si entra una estrategia por parametro entonces se busca sino es asi deja al usuario que lo busque por el menu de selección
                sIdStrategy = Request.QueryString("IdStrategy")
                If Not sIdStrategy Is Nothing Then
                    LoadReport(sIdStrategy)
                End If
                'Carga el combo con las estrategias disponibles
                loadCombos()

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


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ' rutina para exportar la tabla html a excel
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=ReporteDetalleEstrategia.xls")
        Response.Charset = "iso-8859-1"
        Response.ContentEncoding = Encoding.Default
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.xls"
        Response.Write(lblTable.Text)
        Response.End()
    End Sub

    Protected Sub ddlEstrategia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstrategia.SelectedIndexChanged
        ' carga el reporte segun la selección
        LoadReport(ddlEstrategia.SelectedValue.ToString())
    End Sub
#End Region


#Region "Metodos"
    Private Sub LoadReport(ByVal sIdStrategy As String)
        'Dim facade As New ReportFacade
        'Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        'Dim dt As New DataTable()
        'Try

        '    dt = facade.loadReportStrategyDetail(applicationCredentials, Request("IdStrategy").ToString())

        '    Dim rd As New ReportDocument
        '    Dim ds As New DataSet()
        '    ds.Tables.Add(dt.Copy())
        '    ds.DataSetName = "dsReports"
        '    ds.Tables(0).TableName = "StrategyDetail"

        '    rd.Load(Server.MapPath("StrategyDetail.rpt"))
        '    rd.SetDataSource(ds)
        '    Me.crvReport.ReportSource = rd
        '    Me.crvReport.DataBind()



        'Catch ex As Exception
        '    'mostrando el error
        '    Session("serror") = ex.Message
        '    Session("sUrl") = Request.UrlReferrer.PathAndQuery
        '    Response.Redirect("~/errors/error.aspx")
        '    Response.End()
        'Finally
        '    dt = Nothing
        '    facade = Nothing
        'End Try

        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        dt = facade.loadReportStrategyDetail(applicationCredentials, sIdStrategy)
        Dim dvFilterStrategyStrategicLine As New DataView
        Dim dvFilterIndicator As New DataView
        Dim dtIndicator As New DataTable()
        Dim dtStrategy As New DataTable()
        Dim sTabla As String
        Dim drRowIndicator As DataRow
        Dim inumIndicator As Integer
        Dim inumStrategy As Integer
        Dim inumRowSpan As Integer
        Dim inumFilasSpan As Integer
        Dim iCountStrategy As Integer = 1
        Dim iCountAcumulador As Integer = 0
        Dim iCount As Integer = 1
        Dim sMeasurementDateByIndicator As String = ""
        Try
            If sIdStrategy > 0 Then
                dvFilterIndicator = dt.DefaultView
                dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator", "Description", "Goal", "GreenValue", "YellowValue", "RedValue")
                sTabla = "<table width='100%'  border='1' cellspacing='0' cellpadding='0'>"
                sTabla = sTabla + " <tr>    <td>Indicadores</td> <td>Meta</td>"
                sTabla = sTabla + " <td bgcolor='#006600'>&nbsp;</td>    <td bgcolor='#FFFF00'>&nbsp;</td>    <td bgcolor='#FF0000'>&nbsp;</td> <td>Fechas de Medición</td>   <td>Actividad</td> <td>Descripción</td> <td> Fecha Inicio</td> <td>Costo</td>  </tr>"
                'dvFilterIndicator = dt.DefaultView
                'dtIndicator = dvFilterIndicator.ToTable(True, "IdIndicator")
                inumIndicator = dtIndicator.Rows.Count
                dtStrategy = dvFilterIndicator.ToTable(True, "StrategicActivityName", "IdStrategicActivity", "ActivityDescription", "BeginDate", "EstimatedValue")
                inumStrategy = dtStrategy.Rows.Count
                For Each drRowIndicator In dtIndicator.Rows

                    'Consultamos cual es mayor entre los indicadores y las strategias o Linea Estrategica
                    If inumIndicator > inumStrategy Then
                        inumRowSpan = inumIndicator
                        If inumStrategy = 0 Then
                            inumFilasSpan = Math.Ceiling(inumIndicator / 1)
                        Else
                            inumFilasSpan = Math.Ceiling(inumIndicator / inumStrategy)
                        End If
                    Else
                        inumRowSpan = inumStrategy
                        'dividimos para saber la cantidad de filas a combinar
                        inumFilasSpan = Math.Ceiling(inumStrategy / inumIndicator)
                    End If
                    ' consultamos las fechas de medición 
                    sMeasurementDateByIndicator = loadMeasurementDateByIndicator(drRowIndicator("IdIndicator"))

                    If iCount = 1 Then
                        If inumStrategy >= inumIndicator Then
                            sTabla = sTabla + "<tr><td   rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("Description").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("Goal").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "' >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + drRowIndicator("RedValue").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'>" + sMeasurementDateByIndicator + "</td>"
                        Else
                            sTabla = sTabla + "<tr><td   >" + drRowIndicator("Description").ToString() + "</td>" + "<td  >" + drRowIndicator("Goal").ToString() + "</td>" + "<td >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td >" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td >" + sMeasurementDateByIndicator + "</td>"
                        End If

                    Else
                        If inumStrategy > inumIndicator Then
                            sTabla = sTabla + "<tr>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "' >" + drRowIndicator("Description").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "' >" + drRowIndicator("Goal").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "' >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "'>" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "' >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td rowspan='" + Convert.ToString(inumFilasSpan) + "' >" + sMeasurementDateByIndicator + "</td>"
                        Else
                            sTabla = sTabla + "<tr>" + "<td  >" + drRowIndicator("Description").ToString() + "</td>" + "<td  >" + drRowIndicator("Goal").ToString() + "</td>" + "<td  >" + drRowIndicator("GreenValue").ToString() + "</td>" + "<td >" + drRowIndicator("YellowValue").ToString() + "</td>" + "<td  >" + drRowIndicator("RedValue").ToString() + "</td>" + "<td  >" + sMeasurementDateByIndicator + "</td>"
                        End If
                    End If

                    'si las estrategias son mayores que los indicadores las estrategias no se combinan
                    If inumStrategy >= inumIndicator Then

                        Dim iFor As Integer
                        For iFor = dtStrategy.Rows.Count - inumStrategy To dtStrategy.Rows.Count - 1


                            If iCountStrategy = 1 Then
                                sTabla = sTabla + "<td   >" + dtStrategy.Rows(iFor)("StrategicActivityName").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("ActivityDescription").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("BeginDate").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("EstimatedValue").ToString() + "</td>" + "</tr>"
                                inumStrategy = inumStrategy - 1
                            Else
                                sTabla = sTabla + "<tr><td   >" + dtStrategy.Rows(iFor)("StrategicActivityName").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("ActivityDescription").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("BeginDate").ToString() + "</td>" + "<td   >" + dtStrategy.Rows(iFor)("EstimatedValue").ToString() + "</td>" + "</tr>"
                                inumStrategy = inumStrategy - 1
                            End If


                            If iCountStrategy >= inumFilasSpan Then
                                iCountAcumulador = iCountStrategy
                                iCountStrategy = iCountStrategy + 1
                                Exit For
                            End If
                            iCountStrategy = iCountStrategy + 1


                        Next

                        iCountStrategy = 1
                    Else
                        'si los indicadores son mayores que las estrategias las estrategias se combinan
                        Dim iFor As Integer
                        For iFor = iCountAcumulador To dtStrategy.Rows.Count - 1
                            If iCountStrategy = 1 And iCountAcumulador = 0 Then
                                sTabla = sTabla + "<td rowspan='" + inumFilasSpan.ToString() + "'   >" + dtStrategy.Rows(iFor)("StrategicActivityName").ToString() + "</td>" + "<td  rowspan='" + inumFilasSpan.ToString() + "'  >" + dtStrategy.Rows(iFor)("ActivityDescription").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'   >" + dtStrategy.Rows(iFor)("BeginDate").ToString() + "</td>" + "<td  rowspan='" + inumFilasSpan.ToString() + "'  >" + dtStrategy.Rows(iFor)("EstimatedValue").ToString() + "</td>" + "</tr>"
                                inumStrategy = inumStrategy - 1
                            Else
                                If inumFilasSpan < iCount Then
                                    sTabla = sTabla + "<td  rowspan='" + Convert.ToString(inumFilasSpan) + "'   >" + dtStrategy.Rows(iFor)("StrategicActivityName").ToString() + "</td>""<td  rowspan='" + inumFilasSpan.ToString() + "'  >" + dtStrategy.Rows(iFor)("ActivityDescription").ToString() + "</td>" + "<td rowspan='" + inumFilasSpan.ToString() + "'   >" + dtStrategy.Rows(iFor)("BeginDate").ToString() + "</td>" + "<td  rowspan='" + inumFilasSpan.ToString() + "'  >" + dtStrategy.Rows(iFor)("EstimatedValue").ToString() + "</td>" + "</tr>"
                                    inumStrategy = inumStrategy - 1
                                    iCountAcumulador = iFor + 1
                                End If
                            End If

                            If iFor = 0 Then
                                iCountAcumulador = iFor + 1
                                Exit For
                            End If
                            iCountStrategy = iCountStrategy + 1

                        Next
                        iCountStrategy = 1
                    End If
                    iCount = iCount + 1
                    inumIndicator = inumIndicator - 1
                Next

                iCountAcumulador = 0
                iCount = 1
                sTabla = sTabla + "</table>"

                If dt.Rows.Count > 0 Then
                    lblTable.Text = "<center><b>Estrategia " + dt.Rows(0)("StrategyName").ToString() + "</b></center><br><br> " + sTabla
                End If
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
            dvFilterIndicator = Nothing
            dtIndicator = Nothing
            dtStrategy = Nothing
            dvFilterStrategyStrategicLine = Nothing
            drRowIndicator = Nothing
            facade = Nothing
        End Try

    End Sub


    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dtStrategy = facade.loadReportStrategyDetail(applicationCredentials)
        Dim dvStrategy = dtStrategy.DefaultView
        dtStrategy = dvStrategy.ToTable(True, "StrategyCode", "IdStrategy")

        Try

            ' cargar la lista de las estrategias

            Me.ddlEstrategia.DataSource = dtStrategy
            Me.ddlEstrategia.DataValueField = "IdStrategy"
            Me.ddlEstrategia.DataTextField = "StrategyCode"
            Me.ddlEstrategia.DataBind()
            Me.ddlEstrategia.Items.Insert(0, New ListItem("-- Seleccione --", "0"))



        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing
            dtStrategy = Nothing
            dvStrategy = Nothing

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
