Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ExecutionContractualPlanRegistryDetailsDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ExecutionContractualPlanRegistryDetails
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistryDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ExecutionContractualPlanRegistryDetails As ExecutionContractualPlanRegistryDetailsEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ExecutionContractualPlanRegistryDetails(" & _
             "idexecutioncontractualplanregistry," & _
             "idproject," & _
             "concept," & _
             "idContractType," & _
             "totalcost," & _
             "engagementdate," & _
             "comments," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.idexecutioncontractualplanregistry & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.idproject & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.concept & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.contractType & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.totalcost.ToString().Replace(",", ".") & "',")
            If ExecutionContractualPlanRegistryDetails.engagementdate <> New DateTime Then
                sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.engagementdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" NULL,")
            End If
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.comments & "',")
            sql.AppendLine("'" & ExecutionContractualPlanRegistryDetails.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el ExecutionContractualPlanRegistryDetails. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ExecutionContractualPlanRegistryDetails por el Id
    ''' </summary>
    ''' <param name="idExecutionContractualPlanRegistryDetails"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistryDetails As Integer) As ExecutionContractualPlanRegistryDetailsEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objExecutionContractualPlanRegistryDetails As New ExecutionContractualPlanRegistryDetailsEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append("SELECT ExecutionContractualPlanRegistryDetails.*, Project.Name as projectName, ContractType.Name AS contractTypeName ")
            sql.Append("FROM ExecutionContractualPlanRegistryDetails INNER JOIN ")
            sql.Append(" Project ON ExecutionContractualPlanRegistryDetails.IdProject =Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" LEFT OUTER JOIN  ContractType ON ExecutionContractualPlanRegistryDetails.IdContractType=ContractType.Id")
            sql.Append(" WHERE ExecutionContractualPlanRegistryDetails.Id = " & idExecutionContractualPlanRegistryDetails)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objExecutionContractualPlanRegistryDetails.id = data.Rows(0)("id")
                objExecutionContractualPlanRegistryDetails.idexecutioncontractualplanregistry = data.Rows(0)("idexecutioncontractualplanregistry")
                objExecutionContractualPlanRegistryDetails.idproject = data.Rows(0)("idproject")
                objExecutionContractualPlanRegistryDetails.concept = data.Rows(0)("concept")
                objExecutionContractualPlanRegistryDetails.contractType = data.Rows(0)("idContractType")
                objExecutionContractualPlanRegistryDetails.totalcost = data.Rows(0)("totalcost")
                objExecutionContractualPlanRegistryDetails.engagementdate = data.Rows(0)("engagementdate")
                objExecutionContractualPlanRegistryDetails.comments = data.Rows(0)("comments")
                objExecutionContractualPlanRegistryDetails.createdate = data.Rows(0)("createdate")
                objExecutionContractualPlanRegistryDetails.projectname = data.Rows(0)("projectName")
                objExecutionContractualPlanRegistryDetails.contractTypeName = data.Rows(0)("contractTypeName")

            End If

            ' retornar el objeto
            Return objExecutionContractualPlanRegistryDetails

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objExecutionContractualPlanRegistryDetails = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idexecutioncontractualplanregistry"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="concept"></param>
    ''' <param name="type"></param>
    ''' <param name="typetext"></param>
    ''' <param name="totalcost"></param>
    ''' <param name="engagementdate"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ExecutionContractualPlanRegistryDetailsEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idexecutioncontractualplanregistry As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal concept As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal engagementdate As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionContractualPlanRegistryDetailsEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objExecutionContractualPlanRegistryDetails As ExecutionContractualPlanRegistryDetailsEntity
        Dim ExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append("SELECT ExecutionContractualPlanRegistryDetails.*, Project.Name as projectName, ContractType.Name AS contractTypeName ")
            sql.Append("FROM ExecutionContractualPlanRegistryDetails INNER JOIN ")
            sql.Append(" Project ON ExecutionContractualPlanRegistryDetails.IdProject =Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" LEFT OUTER JOIN  ContractType ON ExecutionContractualPlanRegistryDetails.IdContractType=ContractType.Id")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idexecutioncontractualplanregistry.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.idexecutioncontractualplanregistry = '" & idexecutioncontractualplanregistry & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not concept.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.concept like '%" & concept & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not type.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.idContractType like '%" & type & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not typetext.Equals("") Then

                'Definir los tipos

            End If

            ' verificar si hay entrada de datos para el campo
            If Not totalcost.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.totalcost like '%" & totalcost & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not engagementdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ExecutionContractualPlanRegistryDetails.engagementdate, 103) like '%" & engagementdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " ExecutionContractualPlanRegistryDetails.comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ExecutionContractualPlanRegistryDetails.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                sql.Append(" ORDER BY " & order)

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objExecutionContractualPlanRegistryDetails = New ExecutionContractualPlanRegistryDetailsEntity

                ' cargar el valor del campo
                objExecutionContractualPlanRegistryDetails.id = row("id")
                objExecutionContractualPlanRegistryDetails.idexecutioncontractualplanregistry = row("idexecutioncontractualplanregistry")
                objExecutionContractualPlanRegistryDetails.idproject = row("idproject")
                objExecutionContractualPlanRegistryDetails.concept = row("concept")
                objExecutionContractualPlanRegistryDetails.contractType = row("idContractType")
                objExecutionContractualPlanRegistryDetails.totalcost = IIf(IsDBNull(row("totalcost")), 0, row("totalcost"))
                objExecutionContractualPlanRegistryDetails.engagementdate = IIf(IsDBNull(row("engagementdate")), Nothing, row("engagementdate"))
                objExecutionContractualPlanRegistryDetails.comments = IIf(IsDBNull(row("comments")), "", row("comments"))
                objExecutionContractualPlanRegistryDetails.createdate = row("createdate")
                objExecutionContractualPlanRegistryDetails.projectname = row("projectname")
                objExecutionContractualPlanRegistryDetails.contractTypeName = row("contractTypeName")

                ' agregar a la lista
                ExecutionContractualPlanRegistryDetailsList.Add(objExecutionContractualPlanRegistryDetails)

            Next

            ' retornar el objeto
            getList = ExecutionContractualPlanRegistryDetailsList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ExecutionContractualPlanRegistryDetails. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objExecutionContractualPlanRegistryDetails = Nothing
            ExecutionContractualPlanRegistryDetailsList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ExecutionContractualPlanRegistryDetails
    ''' </summary>
    ''' <param name="ExecutionContractualPlanRegistryDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ExecutionContractualPlanRegistryDetails As ExecutionContractualPlanRegistryDetailsEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ExecutionContractualPlanRegistryDetails SET")
            sql.AppendLine(" idexecutioncontractualplanregistry = '" & ExecutionContractualPlanRegistryDetails.idexecutioncontractualplanregistry & "',")
            sql.AppendLine(" idproject = '" & ExecutionContractualPlanRegistryDetails.idproject & "',")
            sql.AppendLine(" concept = '" & ExecutionContractualPlanRegistryDetails.concept & "',")
            sql.AppendLine(" idContractType = '" & ExecutionContractualPlanRegistryDetails.contractType & "',")
            sql.AppendLine(" totalcost = '" & ExecutionContractualPlanRegistryDetails.totalcost.ToString().Replace(",", ".") & "',")
            If ExecutionContractualPlanRegistryDetails.engagementdate <> New DateTime Then
                sql.AppendLine(" engagementdate = '" & ExecutionContractualPlanRegistryDetails.engagementdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" engagementdate = NULL, ")
            End If
            sql.AppendLine(" comments = '" & ExecutionContractualPlanRegistryDetails.comments & "'")
            sql.AppendLine(" createdate = '" & ExecutionContractualPlanRegistryDetails.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & ExecutionContractualPlanRegistryDetails.id)

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
            Throw New Exception("Error al modificar el ExecutionContractualPlanRegistryDetails. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ExecutionContractualPlanRegistryDetails de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecutionContractualPlanRegistryDetails As Integer, _
       Optional ByVal idExecutionContractualPlanRegistry As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ExecutionContractualPlanRegistryDetails ")
            If idExecutionContractualPlanRegistry <> 0 Then
                SQL.AppendLine(" where IdExecutionContractualPlanRegistry = '" & idExecutionContractualPlanRegistry & "' ")
            Else
                SQL.AppendLine(" where id = '" & idExecutionContractualPlanRegistryDetails & "' ")
            End If

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
            Throw New Exception("Error al elimiar el ExecutionContractualPlanRegistryDetails. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
