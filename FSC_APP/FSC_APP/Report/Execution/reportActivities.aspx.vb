Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_reportActivities
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
            Session("lblTitle") = "Reporte de Subactividades."
            Me.loadCombos()
        End If

    End Sub

    Protected Sub ddlProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProject.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de los componentes
        Me.LoadDropDownListComponent()

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
    Public Sub loadReport()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myProject As String = ""
        Dim myComponent As String = ""
        Dim myResponsible As String = ""
        Dim myState As String = ""
        Dim myEndDateStart As String = ""
        Dim myEndDateEnd As String = ""

        Try
            'Se recuperan los valores de los filtros
            myProject = Request.Form("ctl00$cphPrincipal$ddlProject")
            myComponent = Request.Form("ctl00$cphPrincipal$ddlComponent")
            myResponsible = Request.Form("ctl00$cphPrincipal$ddlResponsible")
            myState = Request.Form("ctl00$cphPrincipal$ddlState")
            myEndDateStart = Request.Form("ctl00$cphPrincipal$txtEndDateBegin")
            myEndDateEnd = Request.Form("ctl00$cphPrincipal$txtEndDateEnd")

            'Se Adjunta las tablas requeridas
            ReportDataSet.Tables.Add(facade.loadReportListActivities(applicationCredentials, myProject, myComponent, myResponsible, myState, myEndDateStart, myEndDateEnd).Copy())
            ReportDataSet.DataSetName = "dsRptActivities.xsd"
            ReportDataSet.Tables(0).TableName = "vReportActivities"

            'Se carga el reporte
            reportDoc.Load(Server.MapPath("Activities.rpt"))
            reportDoc.SetDataSource(ReportDataSet)
            Me.crvReport.ReportSource = reportDoc

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

            'Se llama al metodo que permite poblar el combo de los componentes
            Me.LoadDropDownListComponent()

            'Se pobla el combo de los responsables
            Me.ddlResponsible.DataSource = facade.getUserList(applicationCredentials)
            Me.ddlResponsible.DataValueField = "Id"
            Me.ddlResponsible.DataTextField = "Code"
            Me.ddlResponsible.DataBind()

            'Se agrega el item todos
            Me.ddlResponsible.Items.Add(New ListItem("Todos", ""))
            Me.ddlResponsible.SelectedValue = ""

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

    ''' <summary>
    ''' Permite poblar el combo de los componentes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListComponent()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los componentes
            Me.ddlComponent.DataSource = facade.getComponentList(applicationCredentials, idproject:=Me.ddlProject.SelectedValue, order:="name", isLastVersion:="1", enabled:="1")
            Me.ddlComponent.DataValueField = "idkey"
            Me.ddlComponent.DataTextField = "name"
            Me.ddlComponent.DataBind()

            'Se agrega el item todos
            Me.ddlComponent.Items.Add(New ListItem("Todos", ""))
            Me.ddlComponent.SelectedValue = ""

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
