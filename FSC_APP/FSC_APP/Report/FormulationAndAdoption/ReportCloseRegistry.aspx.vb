Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_FormulationAndAdoption_ReportCloseRegistry
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

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Page.IsPostBack Then

            'Se llama al metodo que recarga el reporte
            Me.LoadReport()

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ' cargar el titulo
            Session("lblTitle") = "REGISTRO DE PROYECTO CERRADOS"

            'Se llama al metodo que recarga el reporte
            Me.LoadReport()

        End If

    End Sub

#End Region

#Region "Metodos"

    Private Sub LoadReport()

        Dim facade As New ReportDALC
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        Dim dtOperatorsByProject As New DataTable()
        Dim sIdProject As String = ""

        Try
            'Se recuperan los valores de los filtros
            If Not Request.QueryString("idProject") Is Nothing Then
                sIdProject = Request.QueryString("idProject")

                'Se ejecuta la instrucción que permite poblar el reporte
                dt = facade.loadCloseRegistry(applicationCredentials, sIdProject)

                'Se ejecuta la instruccion que permite consultar los operadores por proyecto
                dtOperatorsByProject = facade.loadOperatorsByProject(applicationCredentials, sIdProject)

                Dim rd As New ReportDocument
                Dim ds As New DataSet()
                ds.DataSetName = "dsFormulation"

                'Se asignan las tablas con los registros consultados al dataset
                ds.Tables.Add(dt.Copy())
                ds.Tables(0).TableName = "dtReportCloseRegistry"
                ds.Tables.Add(dtOperatorsByProject.Copy())
                ds.Tables(1).TableName = "dtOperatorByProject"

                'Se carga el reporte
                rd.Load(Server.MapPath("CloseRegistry.rpt"))
                rd.SetDataSource(ds)
                Me.crvReport.ReportSource = rd

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

#End Region

End Class
