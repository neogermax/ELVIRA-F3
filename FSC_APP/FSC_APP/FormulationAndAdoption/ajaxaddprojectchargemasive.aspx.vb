Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Class FormulationAndAdoption_ajaxaddprojectchargemasive
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        'trae el jquery para hacer todo por debajo del servidor
        action = Request.QueryString("action").ToString()

        Select action
            Case "validar"
                'convierte la variable y llama funcion para la validacion de la idea
                id_b = Convert.ToInt32(Request.QueryString("id").ToString())
                cargemasivo(id_b, applicationCredentials)


            Case Else
        End Select
    End Sub


    Public Function cargemasivo(ByVal idproyect As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String


        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim menzaje As String
        Dim path As String = ""


    End Function



End Class
