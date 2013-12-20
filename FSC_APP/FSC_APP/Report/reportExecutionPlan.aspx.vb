Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_reportExecutionPlan
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
            Session("lblTitle") = "Reporte plan de ejecución"
            loadCombos()

            'Se verifica si viene un identificador de proyecto por la URL
            If Not (Request.QueryString("idProject") Is Nothing) Then
                Me.ddlidproject.SelectedValue = Request.QueryString("idProject").ToString()

                'Se llama al metodo que recarga el reporte
                Me.LoadReport(Request.QueryString("idProject").ToString())
            End If
        End If
    End Sub

    Protected Sub btMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMake.Click
        LoadReport()
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        For i As Integer = 2000 To 2030
            ddlyear.Items.Add(i)
        Next

        Try
            ' cargar la lista de los tipos
            Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Code")
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()
            Me.ddlidproject.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

            ' cargar la lista de las fases de un proyecto
            Me.ddlProjectPhase.DataSource = facade.getProjectPhaseList(applicationCredentials, isenabled:="1", order:="name")
            Me.ddlProjectPhase.DataValueField = "id"
            Me.ddlProjectPhase.DataTextField = "name"
            Me.ddlProjectPhase.DataBind()

            'Se agrega el item todos
            Me.ddlProjectPhase.Items.Add(New ListItem("Todas", ""))
            Me.ddlProjectPhase.SelectedValue = ""

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

    Private Sub LoadReport(Optional ByVal idKeyProject As String = "")
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        Dim myProject As Integer = 0
        Dim myCode As String = ""
        Dim myYear As Integer = 0
        Dim myProjectPhase As String = ""

        Try
            'Se recuperan los valores de los filtros
            If (idKeyProject.Length > 0) Then
                myProject = idKeyProject
            Else
                myProject = Request.Form("ctl00$cphPrincipal$ddlidproject")
            End If

            'If Not (Request.Form("ctl00$cphPrincipal$txtcode") Is Nothing) Then myCode = Request.Form("ctl00$cphPrincipal$txtcode")
            If Not (Request.Form("ctl00$cphPrincipal$ddlyear") Is Nothing) Then myYear = Request.Form("ctl00$cphPrincipal$ddlyear")
            If Not (Request.Form("ctl00$cphPrincipal$ddlProjectPhase") Is Nothing) Then myProjectPhase = Request.Form("ctl00$cphPrincipal$ddlProjectPhase")

            'Se ejecuta la consulta que permite poblar el reporte
            dt = facade.loadReportExecutionPlan(applicationCredentials, myProject, myCode, myYear, myProjectPhase)

            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsFormulation"
            ds.Tables(0).TableName = "dtExecutionPlan"

            rd.Load(Server.MapPath("ExecutionPlan.rpt"))
            rd.SetDataSource(ds)
            Me.crvReport.ReportSource = rd

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
