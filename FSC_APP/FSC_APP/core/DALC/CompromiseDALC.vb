Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class CompromiseDALC
    Const MODULENAME As String = "CompromiseDALC"
    ''' <summary> 
    ''' TODO: Registar un nuevo Compromiso
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Compromise As CompromiseEntity, ByVal idacta As Integer) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Compromise(" & _
             "idproject," & _
             "liabilities," & _
             "responsible," & _
             "tracingdate," & _
             "email," & _
             "proceeding_log_id" & _
             ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Compromise.idproject & "',")
            sql.AppendLine("'" & Compromise.liabilities & "',")
            sql.AppendLine("'" & Compromise.responsible & "',")
            sql.AppendLine("'" & Compromise.tracingdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Compromise.email & "',")
            sql.AppendLine("'" & idacta & "')")


            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
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
            Throw New Exception("Error al insertar el compromiso. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

End Class
