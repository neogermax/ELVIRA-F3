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
        Dim tipoacta As Integer
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
            Case "getproceeding"
                tipoacta = Request.QueryString("type").ToString()
                proyectid = Request.QueryString("proyectid").ToString()
                actas(proyectid, tipoacta, applicationCredentials)
            Case Else
        End Select

    End Sub

#Region "Funciones"

    Public Function buscacontratacion(ByVal idProject As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consulta de los datos de actores por id
        sql.Append("select RequestNumber from ContractRequest where IdProject = " & idProject)
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

    Public Function proyectoaprobado(ByVal idProject As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consulta estado de aprobacion por id
        sql.Append("SELECT Typeapproval FROM Project WHERE id = " & idProject)

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

    Function contratofinalizado(ByVal idProject As Integer, ByVal objAppilcationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consultar el estado de contratacion por el id del proyecto
        sql.Append("select finished from ContractRequest where IdProject = " & idProject)

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

    Function actas(ByVal idProject As Integer, ByVal tipo As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        sql.Append("SELECT Acta_id FROM [FSC_eProject].[dbo].[Proceeding_Logs] where Tipo_Acta_id = " & tipo & " and project_id = " & idProject)

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            'Tiene acta de inicio
            Dim objresult As String
            objresult = data.Rows(0)("Acta_id")
            Response.Write(objresult)
        Else
            'No tiene acta de inicio
        End If

    End Function
#End Region

End Class