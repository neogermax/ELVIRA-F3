Imports Microsoft.VisualBasic
Imports Gattaca.Entity.eSecurity
Imports System.Data
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager




Public Class Documentstmp

    Const MODULENAME As String = "DocumentstmpDALC"

    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal documentstmp As DocumentstmpEntity) As Long
        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Documentstmp(namefile)  VALUES " & documentstmp.namefile)
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
            Throw New Exception("Error al insertar el Documents. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

End Class
