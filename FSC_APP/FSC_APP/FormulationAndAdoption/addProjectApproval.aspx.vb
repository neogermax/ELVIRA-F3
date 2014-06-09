Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data

Partial Class FormulationAndAdoption_addProjectApproval
    Inherits System.Web.UI.Page

#Region "propiedades"
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
            Session("lblTitle") = "APROBACIÓN DE UN PROYECTO."

        End If


    End Sub
#End Region

#Region "metodos"





 

#End Region


End Class
