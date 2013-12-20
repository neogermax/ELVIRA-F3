Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class proceedings_logDALC

    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
     ByVal logs As proceeding_logsEntity) As String

        Dim sql As New StringBuilder
        Dim DataLogs As DataTable = New DataTable()
        Dim datalogfin As DataTable
        Try
            'consultar catidad de copias por actas
            sql.AppendLine("select count(project_id) + 1 from Proceeding_Logs where Project_Id = " & logs.project_id & " and tipo_Acta_id = " & logs.Tipo_acta_id)
            logs.acta_id = GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            sql = New StringBuilder

            'ingresamos a la tabla
            sql.AppendLine("INSERT INTO Proceeding_Logs(" & _
            "Project_Id," & _
            "Tipo_Acta_Id," & _
            "Acta_Id," & _
            "User_Id," & _
            "Create_Date," & _
            "fileName" & _
           ")")

            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & logs.project_id & "',")
            sql.AppendLine("'" & logs.Tipo_acta_id & "',")
            sql.AppendLine("'" & logs.acta_id & "',")
            sql.AppendLine("'" & logs.iduser & "',")
            sql.AppendLine("'" & logs.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & logs.file_name & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'Ejecutar el query
            datalogfin = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            Dim datologfinresult As String = ""

            ' id creado
            If datalogfin.Rows.Count > 0 Then
                datologfinresult = datalogfin.Rows(0)(0).ToString
            End If

            ' finalizar la transaccion
            CtxSetComplete()

            Return datologfinresult

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'sube el error de nivel
            Throw New Exception("Error al insertar el log del acta. " & ex.Message)

        End Try

    End Function

    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal nombrearchivo As String, ByVal actaid As String) As String

        Dim sql As New StringBuilder

        Try
            sql.AppendLine("use FSC_eProject ")
            sql.AppendLine("update Proceeding_Logs ")
            sql.AppendLine("set fileName = '" & nombrearchivo & "' ")
            sql.AppendLine("where Id = " & actaid)
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

            Return ""

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'sube el error de nivel
            Throw New Exception("Error al actualizar el log del acta. " & ex.Message)

        End Try

    End Function

End Class
