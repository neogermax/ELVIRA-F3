Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Class FormulationAndAdoption_ajaxaddProjectApprovalRecordshearch
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        'trae el jquery para hacer todo por debajo del servidor
        action = Request.QueryString("action").ToString()

        Select Case action

            Case "buscaractorescontrato"
                id_b = Convert.ToInt32(Request.QueryString("code").ToString())
                buscardatethirdCotract(id_b, applicationCredentials, Request.QueryString("id"))

       
        End Select
    End Sub



    Public Function buscardatethirdCotract(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal idThird As Integer) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable


        'consulta de los datos de actores por id

        sql.Append("SELECT IdThird, IdProject, third.Name, ThirdByProject.Type, Third.contact, Third.documents, third.phone, third.email ")
        sql.Append("FROM ThirdByProject ")
        sql.Append("inner join Third on ThirdByProject.IdThird = Third.Id ")
        sql.Append("where IdProject = " & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String
        html = "<table  style=""font-family: 'Calibri'; width: 85%;"" border=1 cellspacing=0 cellpadding=2 bordercolor=""666633"" >"
        html &= " <tr>"
        html &= " <td colspan=""7"">"
        html &= " <h2 align=center> Actores </h2>"
        html &= " </tr>"
        html &= " </td>"
        html &= " <tr><td>Nombre</td><td>Tipo</td><td>Contacto</td><td>Documento</td><td>Tel&eacute;fono</td><td>E-Mail</td></tr>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", itemDataTable(2), itemDataTable(3), itemDataTable(4), itemDataTable(5), itemDataTable(6), itemDataTable(7))
        Next
        html &= " </table>"

        Response.Write(html)


    End Function



End Class
