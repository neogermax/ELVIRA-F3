Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class OperatorByProjectDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo OperatorByProject
    ''' </summary>
    ''' <param name="OperatorByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal OperatorByProject As OperatorByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO OperatorByProject(" & _
             "idproject," & _
             "idoperator" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & OperatorByProject.idproject & "',")
            sql.AppendLine("'" & OperatorByProject.idoperator & "')")

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
            Throw New Exception("Error al insertar el OperatorByProject. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un OperatorByProject por el Id
    ''' </summary>
    ''' <param name="idOperatorByProject"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idOperatorByProject As Integer) As OperatorByProjectEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objOperatorByProject As New OperatorByProjectEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT OperatorByProject.Id, OperatorByProject.IdProject, OperatorByProject.IdOperator, Third.Name AS operatorname ")
            sql.Append(" FROM OperatorByProject INNER JOIN ")
            sql.Append(" Third ON OperatorByProject.IdOperator = Third.Id ")
            sql.Append(" WHERE OperatorByProject.Id = " & idOperatorByProject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objOperatorByProject.id = data.Rows(0)("id")
                objOperatorByProject.idproject = data.Rows(0)("idproject")
                objOperatorByProject.idoperator = data.Rows(0)("idoperator")
                objOperatorByProject.OPERATORNAME = data.Rows(0)("operatorname")

            End If

            ' retornar el objeto
            Return objOperatorByProject

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un OperatorByProject. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objOperatorByProject = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproject"></param>
    ''' <param name="idoperator"></param>
    ''' <returns>un objeto de tipo List(Of OperatorByProjectEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idproject As String = "", _
								Optional ByVal idoperator As String = "", _
								Optional order as string = "") As List(Of OperatorByProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objOperatorByProject As OperatorByProjectEntity
        Dim OperatorByProjectList As New List(Of OperatorByProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT OperatorByProject.Id, OperatorByProject.IdProject, OperatorByProject.IdOperator, Third.Name AS operatorname ")
            sql.Append(" FROM OperatorByProject INNER JOIN ")
            sql.Append(" Third ON OperatorByProject.IdOperator = Third.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " idproject = '" & idproject & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idoperator.Equals("") Then

                sql.Append(where & " idoperator = '" & idoperator & "'")
                where = " AND "

            End If
             
            If Not order.Equals(String.Empty) Then
            
				' ordernar
				SQL.Append(" ORDER BY " & order)
            
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objOperatorByProject = New OperatorByProjectEntity

				' cargar el valor del campo
				objOperatorByProject.id = row("id")
				objOperatorByProject.idproject = row("idproject")
                objOperatorByProject.idoperator = row("idoperator")
                objOperatorByProject.OPERATORNAME = row("operatorname")

                ' agregar a la lista
                OperatorByProjectList.Add(objOperatorByProject)

            Next

            ' retornar el objeto
            getList = OperatorByProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de OperatorByProject. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objOperatorByProject = Nothing
            OperatorByProjectList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo OperatorByProject
    ''' </summary>
    ''' <param name="OperatorByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal OperatorByProject As OperatorByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update OperatorByProject SET")
            SQL.AppendLine(" idproject = '" & OperatorByProject.idproject & "',")           
            SQL.AppendLine(" idoperator = '" & OperatorByProject.idoperator & "'")           
            SQL.AppendLine("WHERE id = " & OperatorByProject.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el OperatorByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el OperatorByProject de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idOperatorByProject As Integer, _
       Optional ByVal idProject As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from OperatorByProject ")
            If idProject = 0 Then
                SQL.AppendLine(" where id = '" & idOperatorByProject & "' ")
            Else
                SQL.AppendLine(" where idProject = '" & idProject & "' ")
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
            Throw New Exception("Error al elimiar el OperatorByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
