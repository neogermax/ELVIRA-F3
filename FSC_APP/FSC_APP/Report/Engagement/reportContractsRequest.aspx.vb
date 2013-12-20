Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_Engagement_reportContractsRequest
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
            Session("lblTitle") = "Estado del proceso de solicitud y elaboración de contratos."

            'Se llama al metodo que pobla los combos
            loadCombos()
        End If

    End Sub

    Protected Sub ddlManagement_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlManagement.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de proyectos
        Me.LoadDropDownListProjectByManagement()

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        'Se llama al metodo que recarga el reporte
        loadReport()

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo encargado de cargar el reporte requerido
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadReport()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myProject As String = ""
        Dim myManagement As String = ""
        Dim myDateStartRequest As String = ""
        Dim myDateEndRequest As String = ""
        Dim myContractorName As String = ""
        Dim myIdContractRequestList As String = ""

        Try
            'Se recuperan los valores de los filtros
            myManagement = Request.Form("ctl00$cphPrincipal$ddlManagement")
            myProject = Request.Form("ctl00$cphPrincipal$ddlProject")
            myDateStartRequest = Request.Form("ctl00$cphPrincipal$txtDateStartRequest")
            myDateEndRequest = Request.Form("ctl00$cphPrincipal$txtDateEndRequest")
            myContractorName = Request.Form("ctl00$cphPrincipal$txtContractorName")

            'Se verifica si viene algun filtro por el nombre del contratista
            If (myContractorName.Length > 0) Then

                'Se llama al metodo que consulta la información de este reqporte segun los filtros ingresados
                myIdContractRequestList = facade.loadConsultGeneralContractRequest(applicationCredentials, myManagement, myProject, myDateStartRequest, myDateEndRequest, myContractorName)

            End If

            'Se Adjunta las tablas requeridas
            ReportDataSet.Tables.Add(facade.loadReportContractRequest(applicationCredentials, myIdContractRequestList, myManagement, myProject, myDateStartRequest, myDateEndRequest).Copy())
            ReportDataSet.DataSetName = "dsRptContractsRequest.xsd"
            ReportDataSet.Tables(0).TableName = "vReportContractsRequest"

            'Se adjunta la tabla con los datos de los contratistas
            ReportDataSet.Tables.Add(facade.loadReportContractorsNameByContractRequest(applicationCredentials, myIdContractRequestList).Copy())
            ReportDataSet.Tables(1).TableName = "vReportContractorByContractRequest"

            'Se carga el reporte
            reportDoc.Load(Server.MapPath("ContractRequest.rpt"))
            reportDoc.SetDataSource(ReportDataSet)
            Me.crvContracsRequest.ReportSource = reportDoc

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
    ''' Permite poblar los combos de lformulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try            

            'Se agregan las rutinas para poblar el combo de Gerencias
            Me.ddlManagement.DataSource = facade.getManagementList(applicationCredentials, enabled:="1", order:="Code")
            Me.ddlManagement.DataValueField = "Id"
            Me.ddlManagement.DataTextField = "Code"
            Me.ddlManagement.DataBind()

            'Se agrega el item todos
            Me.ddlManagement.Items.Add(New ListItem("Todos", ""))
            Me.ddlManagement.SelectedValue = ""

            'Se llama al metodo que permite poblar el combo de proyectos
            Me.LoadDropDownListProjectByManagement()

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
    ''' Metodo que permite poblar el combo de proyectos por gerencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListProjectByManagement()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se pobla el combo
            Me.ddlProject.DataSource = facade.getProjectByManagementList(applicationCredentials, idManagement:=Me.ddlManagement.SelectedValue, enabled:="1", order:="Code")
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

            'Se agrega el item todos
            Me.ddlProject.Items.Add(New ListItem("Todos", ""))
            Me.ddlProject.SelectedValue = ""

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
