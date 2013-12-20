Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class Report_reportActorsMap
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

            'Se poblan los combos del formulario
            Me.LoadCombos()

        End If

        ' poner el titulo
        Session("lblTitle") = "Reporte mapa de actores"

    End Sub

    Protected Sub bt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt.Click

        ' cargar el reporte
        LoadReport()

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Permite poblar las diferentes listas del formulario actual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCombos()

        ' definir los objetos
        Dim objFacade As New Facade()
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)

        Try
            ' cargar los usuarios registrados
            Me.ddlThird.DataSource = objFacade.getThirdList(applicationCredentials, enabled:="1", order:="Code")
            Me.ddlThird.DataValueField = "Id"
            Me.ddlThird.DataTextField = "Code"
            Me.ddlThird.DataBind()

            ' agregar la opcion de todos
            Me.ddlThird.Items.Add(New ListItem("Todos", ""))
            Me.ddlThird.SelectedValue = ""

            'Agregar la opcion de todos para el combo tipo
            Me.ddlType.Items.Add(New ListItem("Todos", ""))
            Me.ddlType.SelectedValue = ""

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            objFacade = Nothing

        End Try
    End Sub

    ''' <summary>
    ''' Permite cargar el reporte de mapa de actores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadReport()

        'Declaracion de objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dt As New DataTable()
        Dim idsCities As String = ""
        Dim idsActivities As String = ""
        Dim myThird As String = ""
        Dim myTxtStartCreationDate As String = ""
        Dim myTxtEndCreationDate As String = ""
        Dim myType As String = ""

        Try
            'Se recuperan los valores de los filtros
            myThird = Request.Form("ctl00$cphPrincipal$ddlThird")
            myTxtStartCreationDate = Request.Form("ctl00$cphPrincipal$txtStartCreationDate")
            myTxtEndCreationDate = Request.Form("ctl00$cphPrincipal$txtEndCreationDate")
            myType = Request.Form("ctl00$cphPrincipal$ddlType")

            'Se recupera el resultado de la consulta
            dt = facade.loadReportActorsMap(applicationCredentials, myThird, myTxtStartCreationDate, myTxtEndCreationDate, myType)

            'Se configuran los objetos
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsRptActorsMap.xsd"
            ds.Tables(0).TableName = "vReportActorsMap"

            'Se carga el reporte
            rd.Load(Server.MapPath("ActorsMap.rpt"))
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
