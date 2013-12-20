Option Strict On

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Application.ExceptionManager

Public Class SettingDALC

    ' definir el nombre
    Const MODULENAME As String = "SettingDALC"

    ''' <summary>
    ''' Cargar lista de parametros de la aplicacion
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of Setting)

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim setting As Setting
        Dim settings As New List(Of Setting)
        Dim data As DataTable

        Try
            ' construir la sentencia
            sSQL.Append(" SELECT MoludeSettings.*  ")
            sSQL.Append(" FROM Module ")
            sSQL.Append(" INNER JOIN MoludeSettings ON Module.Id = MoludeSettings.IdModule ")
            sSQL.Append(" WHERE Module.Code = '" & objApplicationCredentials.ProductName & "'")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sDatabase:="DB99")
            'data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                setting = New Setting

                ' cargar los datos
                setting.id = Gattaca.Application.Tools.Formatter.GetInteger(row("id"))
                setting.parameterName = Gattaca.Application.Tools.Formatter.GetString(row("parameterName"))
                setting.parameterValue = Gattaca.Application.Tools.Formatter.GetString(row("parameterValue"))
                setting.idModule = Gattaca.Application.Tools.Formatter.GetInteger(row("idModule"))

                ' agregar a la lista
                settings.Add(setting)

            Next

            ' retornar el objeto
            getList = settings

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los parametros de la aplicacion. ")

        Finally
            ' liberando recursos
            sSQL = Nothing
            setting = Nothing
            settings = Nothing
            data = Nothing

        End Try

    End Function

End Class
