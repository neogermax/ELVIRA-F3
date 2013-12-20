Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Partial Class Report_OperationalPlanning_ProposalDetail
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
            Session("lblTitle") = "DETALLE DE LA PROPUESTA"

        End If
        LoadReport()
    End Sub

#End Region

#Region "Metodos"

    Private Sub LoadReport()
        Dim facade As New ReportFacadeTemp
        Dim facade1 As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        Dim sIdProposal As String = ""

        Try
            sIdProposal = Request.QueryString("IdProposal")
            dt = facade.getProposalList(applicationCredentials, idProposal:=sIdProposal)
            dt1 = facade.getProposalLocation(applicationCredentials, idProposal:=sIdProposal)
            dt2 = facade1.loadReportAttachmentsByIdea(applicationCredentials, idEntity:=sIdProposal, entityName:="ProposalEntity")
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsRptOperationalPlanning"
            ds.Tables(0).TableName = "dtProposalList"
            ds.Tables.Add(dt1.Copy())
            ds.Tables(1).TableName = "dtProposalLocation"
            ds.Tables.Add(dt2.Copy())
            ds.Tables(2).TableName = "dtReportAttachments"
            rd.Load(Server.MapPath("ProposalDetail.rpt"))
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
