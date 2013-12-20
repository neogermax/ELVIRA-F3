Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_ResearchAndDevelopment_ReportGoodPractice
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
            Session("lblTitle") = "LISTADO DE BUENAS PRÁCTICAS"
            loadCombosProject()

        End If

    End Sub

    Protected Sub btMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMake.Click
        LoadReport()
    End Sub

    Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged
        LoadDropDownCities()
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadCombosProject()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dtTable As New DataTable()
        Dim dvTable As New DataView()

        Try
            ' cargar la lista de los tipos
            Me.ddlidStrategicLine.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1")
            Me.ddlidStrategicLine.DataValueField = "Id"
            Me.ddlidStrategicLine.DataTextField = "Code"
            Me.ddlidStrategicLine.DataBind()
            Me.ddlidStrategicLine.Items.Insert(0, New ListItem(" Seleccione Linea Estrategica ", ""))

            'Cargar la lista de los Departamentos
            LoadDropDownDepto()

            'Carga la lista de municipos
            LoadDropDownCities()

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

        Dim facade As New ReportFacadeTemp
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        Dim sIdStrategicLine As String = ""
        Dim sIdDepto As String = ""
        Dim sIdCity As String = ""
        Dim miOperador As String = ""
        Dim miFechaInicio As String = ""
        Dim miFechaFin As String = ""

        Try
            'Se recuperan los valores de los filtros
            sIdStrategicLine = Request.Form("ctl00$cphPrincipal$ddlidStrategicLine")
            sIdCity = Request.Form("ctl00$cphPrincipal$ddlCity")
            sIdDepto = Request.Form("ctl00$cphPrincipal$ddlDepto")
            miOperador = Request.Form("ctl00$cphPrincipal$txtOperador")
            miFechaInicio = Request.Form("ctl00$cphPrincipal$txtFechaInicio")
            miFechaFin = Request.Form("ctl00$cphPrincipal$txtFechaFin")

            'Se ejecuta la instrucción que permite poblar el reporte
            dt = facade.loadReportGoodPractice(applicationCredentials, sIdStrategicLine, sIdDepto, sIdCity, miOperador, miFechaInicio, miFechaFin)

            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "ResearchAndDevelopment"
            ds.Tables(0).TableName = "dtReportGoodpractice"

            rd.Load(Server.MapPath("GoodPractice.rpt"))
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

    ''' <summary>
    ''' Permite cargar la lista de departamentos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownDepto()
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        ' cargar la lista de los departamentos
        Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, order:="Depto.Code")
        Me.ddlDepto.DataValueField = "Id"
        Me.ddlDepto.DataTextField = "Name"
        Me.ddlDepto.DataBind()
        Me.ddlDepto.Items.Insert(0, New ListItem(" Seleccione Departamento ", ""))

    End Sub

    ''' <summary>
    ''' Permite cargar la lista de municipios segun un depto seleccionado.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownCities()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se carga la lista de los municipos

            Me.ddlCity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, order:="City.Code")
            Me.ddlCity.DataValueField = "Id"
            Me.ddlCity.DataTextField = "Name"
            Me.ddlCity.DataBind()
            Me.ddlCity.Items.Insert(0, New ListItem(" Seleccione Ciudad ", ""))

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
