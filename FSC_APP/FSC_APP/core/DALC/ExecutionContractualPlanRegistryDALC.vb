Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ExecutionContractualPlanRegistryDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ExecutionContractualPlanRegistry
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ExecutionContractualPlanRegistry As ExecutionContractualPlanRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ExecutionContractualPlanRegistry(" & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ExecutionContractualPlanRegistry.iduser & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistry.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el ExecutionContractualPlanRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ExecutionContractualPlanRegistry por el Id
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistry"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistry As Integer) As ExecutionContractualPlanRegistryEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objExecutionContractualPlanRegistry As New ExecutionContractualPlanRegistryEntity
        Dim objExecutionContractualPlanRegistryEntityDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT ExecutionContractualPlanRegistry.*, ApplicationUser.Name as userName ")
            sql.Append(" FROM ExecutionContractualPlanRegistry INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ExecutionContractualPlanRegistry.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE ExecutionContractualPlanRegistry.Id = " & idExecutionContractualPlanRegistry)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objExecutionContractualPlanRegistry.id = data.Rows(0)("id")
                objExecutionContractualPlanRegistry.iduser = data.Rows(0)("iduser")
                objExecutionContractualPlanRegistry.createdate = data.Rows(0)("createdate")
                objExecutionContractualPlanRegistry.username = data.Rows(0)("userName")
                objExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails = objExecutionContractualPlanRegistryEntityDetailsDALC.getList(objApplicationCredentials, idexecutioncontractualplanregistry:=objExecutionContractualPlanRegistry.id)

            End If

            ' retornar el objeto
            Return objExecutionContractualPlanRegistry

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objExecutionContractualPlanRegistry = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="comments"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ExecutionContractualPlanRegistryEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal ProjectName As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionContractualPlanRegistryEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objExecutionContractualPlanRegistry As ExecutionContractualPlanRegistryEntity
        Dim ExecutionContractualPlanRegistryList As New List(Of ExecutionContractualPlanRegistryEntity)
        Dim objExecutionContractualPlanRegistryEntityDetailsDALC As New ExecutionContractualPlanRegistryDetailsDALC
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT DISTINCT ExecutionContractualPlanRegistry.*, ApplicationUser.Name as userName ")
            sql.Append(" FROM ExecutionContractualPlanRegistry INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ExecutionContractualPlanRegistry.IdUser = ApplicationUser.ID  INNER JOIN ")
            sql.Append(" ExecutionContractualPlanRegistryDetails ON ExecutionContractualPlanRegistryDetails.IdExecutionContractualPlanRegistry=ExecutionContractualPlanRegistry.Id INNER JOIN")
            sql.Append(" Project ON Project.IdKey=ExecutionContractualPlanRegistryDetails.IdProject and Project.islastversion=1")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistry.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistry.id like '%" & idlike & "%'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not ProjectName.Equals("") Then

                sql.Append(where & " Project.Name like '%" & ProjectName & "%'")
                where = " AND "

            End If



            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistry.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ExecutionContractualPlanRegistry.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case Else
                        sql.Append(" ORDER BY ExecutionContractualPlanRegistry." & order)
                End Select


            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objExecutionContractualPlanRegistry = New ExecutionContractualPlanRegistryEntity

                ' cargar el valor del campo
                objExecutionContractualPlanRegistry.id = row("id")
                objExecutionContractualPlanRegistry.iduser = row("iduser")
                objExecutionContractualPlanRegistry.createdate = row("createdate")
                objExecutionContractualPlanRegistry.username = row("userName")
                objExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails = objExecutionContractualPlanRegistryEntityDetailsDALC.getList(objApplicationCredentials, idexecutioncontractualplanregistry:=objExecutionContractualPlanRegistry.id)

                ' agregar a la lista
                ExecutionContractualPlanRegistryList.Add(objExecutionContractualPlanRegistry)

            Next

            ' retornar el objeto
            getList = ExecutionContractualPlanRegistryList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ExecutionContractualPlanRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objExecutionContractualPlanRegistry = Nothing
            ExecutionContractualPlanRegistryList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ExecutionContractualPlanRegistry
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ExecutionContractualPlanRegistry As ExecutionContractualPlanRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ExecutionContractualPlanRegistry SET")
            'sql.AppendLine(" comments = '" & ExecutionContractualPlanRegistry.comments & "'")
            sql.AppendLine("WHERE id = " & ExecutionContractualPlanRegistry.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el ExecutionContractualPlanRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ExecutionContractualPlanRegistry de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistry As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ExecutionContractualPlanRegistry ")
            SQL.AppendLine(" where id = '" & idExecutionContractualPlanRegistry & "' ")

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al elimiar el ExecutionContractualPlanRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
