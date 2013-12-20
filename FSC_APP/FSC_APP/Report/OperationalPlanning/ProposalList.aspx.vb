Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_OperationalPlanning_ProposalList
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
            Session("lblTitle") = "LISTA DE PROPUESTAS"
            loadCombosStrategicLine()

        End If

    End Sub

    Protected Sub ddlStrategicLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrategicLine.SelectedIndexChanged
        loadCombosProject()
    End Sub

    Protected Sub btMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMake.Click
        LoadReport()
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadCombosStrategicLine()
        ' definir los objetos
        Dim facade As New ReportFacadeTemp
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dtTable As New DataTable()
        Dim dvTable As New DataView()


        Try
            ' cargar la lista de los tipos
            dtTable = facade.getProposalList(applicationCredentials)
            dvTable = dtTable.DefaultView
            dtTable = dvTable.ToTable(True, "StrategicLineId", "StrategicLineCode")
            Me.ddlStrategicLine.DataSource = dtTable
            Me.ddlStrategicLine.DataValueField = "StrategicLineId"
            Me.ddlStrategicLine.DataTextField = "StrategicLineCode"
            Me.ddlStrategicLine.DataBind()
            Me.ddlStrategicLine.Items.Insert(0, New ListItem(" Seleccione Linea Estrategica ", ""))

            'Se llama al metodo que permite poblar el combo de los proyectos
            Me.loadCombosProject()

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

    Public Sub loadCombosProject()

        ' definir los objetos
        Dim facade As New ReportFacadeTemp
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dtTable As New DataTable()
        Dim dvTable As New DataView()


        Try
            ' cargar la lista de los tipos
            dtTable = facade.getProposalList(applicationCredentials, idStrategicLine:=ddlStrategicLine.SelectedValue)
            dvTable = dtTable.DefaultView
            dtTable = dvTable.ToTable(True, "Projectidkey", "ProjectCode")
            Me.ddlidproject.DataSource = dtTable
            Me.ddlidproject.DataValueField = "Projectidkey"
            Me.ddlidproject.DataTextField = "ProjectCode"
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
        Dim sIdStrategicLine As String = ""
        Dim myCode As String = ""
        Dim miFechaInicio As String = ""
        Dim miFechaFin As String = ""
        Dim myOperator As String = ""
        Dim sState As String = ""
        Try

            sIdStrategicLine = Request.Form("ctl00$cphPrincipal$ddlStrategicLine")
            If Not Request.Form("ctl00$cphPrincipal$ddlidproject") Is Nothing AndAlso Not Request.Form("ctl00$cphPrincipal$ddlidproject").Equals("0") Then
                sIdProject = Request.Form("ctl00$cphPrincipal$ddlidproject")
            End If
            myCode = Request.Form("ctl00$cphPrincipal$txtcode")
            miFechaInicio = Request.Form("ctl00$cphPrincipal$txtFechaInicio")
            miFechaFin = Request.Form("ctl00$cphPrincipal$txtFechaFin")
            myOperator = Request.Form("ctl00$cphPrincipal$txtOperador")
            If Not Request.Form("ctl00$cphPrincipal$ddlEstado") Is Nothing AndAlso Not Request.Form("ctl00$cphPrincipal$ddlEstado").Equals("0") Then
                sState = Request.Form("ctl00$cphPrincipal$ddlEstado")
            End If

            'Se realiza la consulta que permite poblar al reporte requerido.
            dt = facade.getProposalList(applicationCredentials, sIdStrategicLine, sIdProject, myCode, miFechaInicio, miFechaFin, myOperator, sState)

            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.Tables.Add(dt.Copy())
            ds.DataSetName = "dsRptOperationalPlanning"
            ds.Tables(0).TableName = "dtProposalList"

            rd.Load(Server.MapPath("ProposalList.rpt"))
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
