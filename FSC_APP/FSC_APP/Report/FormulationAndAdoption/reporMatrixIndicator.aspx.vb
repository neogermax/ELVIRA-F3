﻿Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_reporMatrixIndicator
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
            Session("lblTitle") = "Matriz de indicadores."
            loadCombos()

            'Se verifica si viene un identificador de proyecto por la URL
            If Not (Request.QueryString("idProject") Is Nothing) Then
                Me.ddlidproject.SelectedValue = Request.QueryString("idProject").ToString()

                'Se llama al metodo que recarga el reporte
                Me.loadReport(Request.QueryString("idProject").ToString())
            End If

        ElseIf ddlidproject.SelectedValue <> "0" Then
            'loadReport()
            ''Me.crvMatrixIndicator.RefreshReport()
        End If
    End Sub

    Protected Sub btMake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btMake.Click
        If ddlidproject.SelectedValue = "0" Then
            'MsgBox("Por favor seleccione un proyecto de la lista", MsgBoxStyle.OkOnly, "FSC")
        Else
            loadReport()
        End If
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadReport(Optional ByVal idKeyProject As String = "")

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myIdKeyProject As Integer = 0
        Dim myTypeIndicator As Integer = 0
        Dim myYear As Integer = 0
        Dim myProjectPhase As String = ""


        Try

            'Se recuperan los valores de los filtros
            If (idKeyProject.Length > 0) Then
                myIdKeyProject = CInt(idKeyProject)
            Else
                'myIdKeyProject = ddlidproject.SelectedValue
                myIdKeyProject = Request.Form("ctl00$cphPrincipal$ddlidproject")
            End If

            If Not (Request.Form("ctl00$cphPrincipal$ddltype") Is Nothing) Then myTypeIndicator = CInt(Request.Form("ctl00$cphPrincipal$ddltype").ToString())
            If Not (Request.Form("ctl00$cphPrincipal$ddlyear") Is Nothing) Then myYear = CInt(Request.Form("ctl00$cphPrincipal$ddlyear").ToString())
            If Not (Request.Form("ctl00$cphPrincipal$ddlProjectPhase") Is Nothing) Then myProjectPhase = Request.Form("ctl00$cphPrincipal$ddlProjectPhase")

            reportDoc.Load(Server.MapPath("MatrixIndicator.rpt"))
            reportDoc.SetDataSource(facade.MatrixIndicator(applicationCredentials, myIdKeyProject, myTypeIndicator, myYear, myProjectPhase))
            Me.crvMatrixIndicator.ReportSource = reportDoc

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

    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim it As New ListItem("Seleccione proyecto", "0")

        For i As Integer = 2000 To 2030
            Me.ddlyear.Items.Add(i)
        Next

        Try
            'Me.ddlyear.SelectedValue = Now.Year

            ' cargar la lista de los tipos
            Me.ddlidproject.DataSource = facade.getProjectListToDropDownList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Name")
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Name"
            Me.ddlidproject.DataBind()

            Me.ddlidproject.Items.Add(it)
            Me.ddlidproject.Items.FindByValue("0").Selected = True

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
