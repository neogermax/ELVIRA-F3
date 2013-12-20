Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_reportIndicatorInventory
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
            LoadCombos()
        Else
            LoadReport()
        End If

        ' poner el titulo
        Session("lblTitle") = "Reporte inventario de indicadores"

    End Sub

    Protected Sub bt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt.Click

        ' cargar el reporte
        LoadReport()

    End Sub

#End Region

#Region "Métodos"

    Protected Sub LoadCombos()

        ' definir los objetos
        Dim objFacade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)

        Try

            ' cargar los usuarios registrados
            Me.ddlUser.DataSource = objFacade.loadIndicatorUsers(applicationCredentials)
            Me.ddlUser.DataValueField = "ID"
            Me.ddlUser.DataTextField = "Code"
            Me.ddlUser.DataBind()

            ' agregar la opcion de todos
            Me.ddlUser.Items.Add(New ListItem("Todos", ""))

            ' seleccionar
            Me.ddlUser.SelectedValue = ""

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

    Private Sub LoadReport()
        Dim facade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dtIndicator As New DataTable()
        Dim dtMeasurementDateByIndicator As New DataTable()
        Try

            dtIndicator = facade.loadReportIndicatorInventory(applicationCredentials, ddllevel.SelectedValue, tbDate1.Text, tbDate2.Text, Me.ddlUser.SelectedValue)
            dtMeasurementDateByIndicator = facade.loadMeasurementDateByIndicator(applicationCredentials, "")
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dtIndicator.Copy())
            ds.Tables(0).TableName = "vIndicatorInventory"
            ds.Tables.Add(dtMeasurementDateByIndicator.Copy())
            ds.Tables(1).TableName = "MeasurementDateByIndicator"
            ds.DataSetName = "dsIndicator"
            rd.Load(Server.MapPath("IndicatorInventory.rpt"))
            rd.SetDataSource(ds)
            Me.crvReport.ReportSource = rd
            Me.crvReport.DataBind()
        Catch ex As Exception
            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()
        Finally
            dtIndicator = Nothing
            dtMeasurementDateByIndicator = Nothing
            facade = Nothing
        End Try

    End Sub

#End Region
End Class
