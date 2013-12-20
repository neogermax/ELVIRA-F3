Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_FormulationAndAdoption_reportlearning
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
            Session("lblTitle") = "APRENDIZAJE, AJUSTES, LOGROS E INDICADORES CUALITATIVOS"
            loadCombosProject()

        End If

    End Sub

    Protected Sub btMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMake.Click
        LoadReport()
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
            Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, enabled:="1", isLastVersion:="1")
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()
            Me.ddlidproject.Items.Insert(0, New ListItem(" Seleccione Proyecto ", "0"))

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
        Dim sIdProject As String = ""
        Dim miRegistradoPor As String = ""
        Dim miFechaInicio As String = ""
        Dim miFechaFin As String = ""
        Dim miTipo As String = ""

        Try

            'Se recuperan los valores de los filtros
            If Not Request.Form("ctl00$cphPrincipal$ddlidproject") Is Nothing AndAlso Not Request.Form("ctl00$cphPrincipal$ddlidproject").Equals("0") Then
                sIdProject = Request.Form("ctl00$cphPrincipal$ddlidproject")
            End If
            miRegistradoPor = Request.Form("ctl00$cphPrincipal$txtRegistrado")
            miFechaInicio = Request.Form("ctl00$cphPrincipal$txtFechaInicio")
            miFechaFin = Request.Form("ctl00$cphPrincipal$txtFechaFin")
            miTipo = Request.Form("ctl00$cphPrincipal$ddlTipo")

            'Se ejecuta la consulta que permite poblar el reporte requerido
            dt = facade.getLearningList(applicationCredentials, sIdProject, miRegistradoPor, miFechaInicio, miFechaFin, miTipo)

            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsFormulation"
            ds.Tables(0).TableName = "dtReportLearning"
            Dim oTexto As TextObject
            rd.Load(Server.MapPath("learning.rpt"))
            oTexto = rd.ReportDefinition.Sections("Section2").ReportObjects("Texto")
            oTexto.Text = ddlTipo.SelectedItem.ToString()
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
