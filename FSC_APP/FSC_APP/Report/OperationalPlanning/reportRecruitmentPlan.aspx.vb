Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_OperationalPlanning_reportRecruitmentPlan
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
            Me.loadReport()

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ' cargar el titulo
            Session("lblTitle") = "Plan de contratación."
            Me.loadCombos()

            'Se verifica si viene un identificador de proyecto por la URL
            If Not (Request.QueryString("idProject") Is Nothing) Then
                Me.ddlProject.SelectedValue = Request.QueryString("idProject").ToString()

                'Se llama al metodo que recarga el reporte
                Me.loadReport(Request.QueryString("idProject").ToString())
            End If

        End If

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        'Se llama al metodo que recarga el reporte
        loadReport()

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite cargar el reporte requerido
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadReport(Optional ByVal idKeyProject As String = "")

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myProject As String = ""
        Dim myProjectPhase As String = ""

        Try

            'Se recuperan los valores de los filtros
            If (idKeyProject.Length > 0) Then
                myProject = idKeyProject
            Else
                myProject = Request.Form("ctl00$cphPrincipal$ddlProject")
            End If
            If Not (Request.Form("ctl00$cphPrincipal$ddlProjectPhase") Is Nothing) Then myProjectPhase = Request.Form("ctl00$cphPrincipal$ddlProjectPhase")

            'Se Adjunta las tablas requeridas
            ReportDataSet.Tables.Add(facade.loadReportRecruitmentPlan(applicationCredentials, myProject, myProjectPhase).Copy())
            ReportDataSet.DataSetName = "dsRptRecruitmentPlan.xsd"
            ReportDataSet.Tables(0).TableName = "vReportRecruitmentPlan"

            'Se carga el reporte
            reportDoc.Load(Server.MapPath("RecruitmentPlan.rpt"))
            reportDoc.SetDataSource(ReportDataSet)
            Me.crvRecruitmentPlan.ReportSource = reportDoc

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            ReportDataSet = Nothing
            reportDoc = Nothing
            facade = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Permite poblar los combos del formulario actual
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            Me.ddlProject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Code")
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

            'Se agrega el item todos
            Me.ddlProject.Items.Add(New ListItem("Todos", ""))
            Me.ddlProject.SelectedValue = ""

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

#End Region

End Class
