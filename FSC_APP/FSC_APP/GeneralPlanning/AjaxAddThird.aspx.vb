
Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Public Class GeneralPlanning_AjaxAddThird
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim code As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'trae el jquery para hacer todo por debajo del servidor
        action = Request.QueryString("action").ToString()

        Select Case action

            Case "verifico"
                code = Request.QueryString("nit").ToString()
                verificarnit(code)
            Case Else
        End Select
    End Sub

    Public Function verificarnit(ByVal codigo As String)

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim Result As String

        If facade.verifyThirdCode(applicationCredentials, codigo) Then

            Result = 1
        Else
            Result = 0
        End If

        Response.Write(Result)
    End Function

End Class