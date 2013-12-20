Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Application.ExceptionManager

Public Class UserDALC

    ' definir el nombre
    Const MODULENAME As String = "UserDALC"

    ''' <summary>
    ''' Cargar lista de usuarios de la aplicacion
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim data As DataTable

        Try
            ' construir la sentencia
            sSQL.Append(" SELECT Id, Name, Code")
            sSQL.Append(" FROM ApplicationUser ")

            ' ejecutar la intruccion
            getList = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sDatabase:="DB99")
            'getList = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los usuarios.")

        Finally
            ' liberando recursos
            sSQL = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar el nomrbe del usuario
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getName(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal id As Integer) As String

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim data As DataTable

        Try
            ' construir la sentencia
            sSQL.Append(" SELECT Name")
            sSQL.Append(" FROM ApplicationUser ")
            sSQL.Append(" Where Id =  " & id)

            ' ejecutar la intruccion
            'data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString)
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sDatabase:="DB99")
            If data.Rows.Count > 0 Then

                getName = data.Rows(0)("Name")

            Else

                getName = "No Registrado"

            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getName")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el nombre del usuario.")

        Finally
            ' liberando recursos
            sSQL = Nothing
            data = Nothing

        End Try

    End Function

End Class
