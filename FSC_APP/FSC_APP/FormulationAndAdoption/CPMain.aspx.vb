Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class CPMain
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
            Session("lblTitle") = "PANEL DE CONTROL"

            'obtener los parámetros
            Dim op As String = Request.QueryString("op")
            Dim projectid As String = Request.QueryString("id")

            'obtener codigo del rol
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim codigogrupo As String = Session("mMenu")
            codigogrupo = codigogrupo.Replace("_", "")
            Dim codeUser As DataTable = getCodeGroup(codigogrupo, applicationCredentials)
            codigogrupo = codeUser(0)(0).ToString()

            'obtener datos del projecto
            Dim codeProyect As DataTable = getProyect(projectid, applicationCredentials)
            Me.lbltitulo.Text = "Proyecto: " & codeProyect(0)(0).ToString()


            'lbltitulo.Text = "Proyecto " & projectid

            'validar opciones de acuerdo al rol
            If (codigogrupo = "ADMIN") Then
                'Me.btnverproyecto.Disabled = True
            End If

        End If

    End Sub

#End Region

#Region "Funciones"

    Public Function getCodeGroup(ByVal idGroup As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Try
            'consulta del codigo del grupo
            sql.Append(" USE [FSC_eSecurity] ")
            sql.Append("SELECT ug.Code, ug.Name FROM UserGroup ug ")
            sql.Append("WHERE ug.id =" & idGroup)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function getProyect(ByVal idProyect As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Try
            'consulta del proyecto
            sql.Append("select cast(id as nvarchar) + ' - ' + Name as campo from project ")
            sql.Append("WHERE id =" & idProyect)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)
            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

End Class

