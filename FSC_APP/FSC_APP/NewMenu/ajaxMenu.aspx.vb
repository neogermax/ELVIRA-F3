Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Class NewMenu_ajaxMenu
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim action As String

        Try

            action = Request.QueryString("action").ToString

            Select Case action

                Case "loadmenu"

                    loadMenuByLevel(applicationCredentials, Session("mMenu").ToString, Request.QueryString("level").ToString())

                Case Else
            End Select

        Catch ex As Exception

        End Try


    End Sub

    Public Function loadMenuByLevel(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
          ByVal idGrupo As Integer, ByVal Level As Integer) As String

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim temporal As String

        Try

            sql.Append("use FSC_eSecurity ")
            sql.Append("select TextField from menu ")
            sql.Append("inner join MenusByUserGroup ")
            sql.Append("on menu.Id = MenusByUserGroup.IdMenu ")
            sql.Append("inner join UserGroup ")
            sql.Append("on UserGroup.id = MenusByUserGroup.IdUserGroup ")
            sql.Append("where UserGroup.id = " & idGrupo & " and menu.level = " & Level & " ;")

            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                For i = 0 To data.Rows.Count - 1

                    temporal = temporal & "," & data.Rows(i)("textfield")

                Next i

            End If

            Response.Write(temporal)

        Catch ex As Exception

        End Try

    End Function

End Class
