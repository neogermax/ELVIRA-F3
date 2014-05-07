Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class ajaxCPMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim action As String
        Dim proyectid As Integer
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        action = Request.QueryString("action").ToString()

        Select Case action
            Case "getcontractstatus"
                proyectid = Request.QueryString("proyectid").ToString()
                buscacontratacion(proyectid, applicationCredentials)
            Case "getproyectstatus"
                proyectid = Request.QueryString("proyectid").ToString()
                proyectoaprobado(proyectid, applicationCredentials)
            Case "getcontractfinished"
                proyectid = Request.QueryString("proyectid").ToString()
                contratofinalizado(proyectid, applicationCredentials)
            Case Else
        End Select

    End Sub

#Region "Funciones"

    Public Function buscacontratacion(ByVal idProyect As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consulta de los datos de actores por id
        sql.Append("select RequestNumber from ContractRequest where IdProject = " & idProyect)
        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            'Tiene contratación
            Dim objresult As String
            objresult = data.Rows(0)("RequestNumber")
            Response.Write(objresult)
        Else
            'No tiene contratación
        End If


    End Function

    Public Function proyectoaprobado(ByVal idProyect As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consulta estado de aprobacion por id
        sql.Append("SELECT Typeapproval FROM Project WHERE id = " & idProyect)

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            'Tiene estado el contrato
            Dim objresult As String
            objresult = data.Rows(0)("Typeapproval")
            Response.Write(objresult)
        Else
            'No tiene estado el contrato
        End If

    End Function

    Function contratofinalizado(ByVal idproyect As Integer, ByVal objAppilcationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consultar el estado de contratacion por el id del proyecto
        sql.Append("select finished from ContractRequest where IdProject = " & idproyect)

        data = GattacaApplication.RunSQLRDT(objAppilcationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            'Tiene contratacion
            Dim objresult As String
            objresult = data.Rows(0)("finished")
            Response.Write(objresult)
        Else
            'No tiene contratacion
        End If

    End Function

#End Region

End Class