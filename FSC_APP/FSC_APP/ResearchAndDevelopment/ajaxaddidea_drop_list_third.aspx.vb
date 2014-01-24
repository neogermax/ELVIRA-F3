'TODO: SE CREA FORMULARIO AJAX PARA ACTUALIZAR EL COMBO  
'AUTOR: GERMAN RODRIGUEZ 10/07/2013

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Class ResearchAndDevelopment_ajaxaddidea_drop_list_third
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim action As String

        action = Request.QueryString("action").ToString()

        Select Case action
            Case "cargarthird"
                'convierte la variable y llama funcion para ACTUALIZAR EL COMBO
                cargardatethird(applicationCredentials)

            Case "loadthirdcontract"
                loadthirdcontract(applicationCredentials, Request.QueryString("id"), Request.QueryString("type"))

            Case Else
        End Select

    End Sub



    ' FUNCION QUE CONSULTA Y CARGA LOS DATOS EN UN HTML
    Public Function cargardatethird(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String


        Dim ThirdList As New List(Of ThirdEntity)
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        sql.Append(" select Third.id,Third.Name from Third ")
        sql.Append(" order by Name asc")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String = ""
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)

    End Function

    'Funcion para actualizar terceros en contratación.
    'Autor: Pedro Cruz
    Public Function loadthirdcontract(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idThird As Integer, ByVal natural As String) As String

        Dim ThirdList As New List(Of ThirdEntity)
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'preguntar si es persona natural o juridica
        If natural = "true" Then
            natural = 1
        Else
            natural = 0
        End If

        'Definir el query
        sql.Append("select Third.id, Third.name from Third ")
        sql.Append("where PersonaNatural = " & natural)
        sql.Append(" order by Name asc")

        'Ejecutar el query
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String = ""
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        'Retornar la consulta en el objeto
        Response.Write(html)

    End Function

End Class
