Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_FormulationAndAdoption_ReportTestimonyDetail
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


            ' cargar el titulo
            Session("lblTitle") = "DETALLE DEL TESTIMONIO"


        End If
        LoadReport()
    End Sub



#End Region

#Region "Metodos"
    Private Sub LoadReport()
        Dim facade As New ReportFacadeTemp
        Dim facade1 As New ReportFacadeTemp
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
      
        Dim sIdTestimony As String = ""

        Try
            sIdTestimony = Request.QueryString("IdTestimony")
            dt = facade.getTestimonyList(applicationCredentials, IdTestimony:=sIdTestimony)
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsFormulation"
            ds.Tables(0).TableName = "dtReportTestimonyList"
            rd.Load(Server.MapPath("TestimonyDetail.rpt"))
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
            dt = Nothing
            facade = Nothing
        End Try

    End Sub
#End Region
End Class
