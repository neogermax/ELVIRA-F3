Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data.SqlClient

Public Class DetailedcashflowsDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
     ByVal detailedcashflow As DetailedcashflowsEntity) As Long

        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Dim Desembolso = Replace(detailedcashflow.Desembolso, ".", "")
        Try

            ' construir la sentencia
            sql.AppendLine(" insert into Detailedcashflows(IdIdea,IdProject,N_pago,IdAportante,Aportante,mother,Desembolso) ")
            sql.AppendLine("VALUES ( ")

            If detailedcashflow.IdIdea = 0 Then
                sql.AppendLine("NULL , ")
            Else
                sql.AppendLine(detailedcashflow.IdIdea & ", ")
            End If

            If detailedcashflow.IdProject = 0 Then
                sql.AppendLine("NULL , ")
            Else
                sql.AppendLine(detailedcashflow.IdProject & ", ")
            End If

            sql.AppendLine(detailedcashflow.N_pago & ", ")
            sql.AppendLine(detailedcashflow.IdAportante & ", ")
            sql.AppendLine("'" & detailedcashflow.Aportante & "', ")
            sql.AppendLine("'" & detailedcashflow.mother & "', ")
            sql.AppendLine("'" & detailedcashflow.Desembolso & "') ")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            ' finalizar la transaccion
            CtxSetComplete()

            ' retornar
            Return num


        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al insertar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try



    End Function

    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal IdIdea As Integer, ByRef type As String) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        SQL.AppendLine(" Delete from Detailedcashflows ")

        If Type = "Idea" Then
            SQL.AppendLine(" where ididea = '" & IdIdea & "' ")
        Else
            SQL.AppendLine(" where idproject = '" & IdIdea & "' ")
        End If
        'Ejecutar la Instruccion
        GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

        ' finalizar la transaccion
        CtxSetComplete()

        Try


        Catch ex As Exception
            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar los detalles de la idea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try


    End Function

End Class
