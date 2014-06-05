Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProgramComponentByProjectDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByProject
    ''' </summary>
    ''' <param name="ProgramComponentByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponentByProject As ProgramComponentByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ProgramComponentByProject(" & _
             "idproject," & _
             "idProgramComponent" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ProgramComponentByProject.idproject & "',")
            sql.AppendLine("'" & ProgramComponentByProject.idProgramComponent & "')")

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
            Throw New Exception("Error al insertar el ProgramComponentByProject. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponentByProject por el Id
    ''' </summary>
    ''' <param name="idProgramComponentByProject"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByProject As Integer) As ProgramComponentByProjectEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProgramComponentByProject As New ProgramComponentByProjectEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT ProgramComponentByProject.Id, ProgramComponentByProject.IdProject, ")
            sql.Append("    ProgramComponentByProject.IdProgramComponent, ProgramComponent.Name AS ProgramComponentname ")
            sql.Append(" FROM ProgramComponentByProject INNER JOIN ")
            sql.Append("    ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id ")
            sql.Append(" WHERE ProgramComponentByProject.Id = " & idProgramComponentByProject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProgramComponentByProject.id = data.Rows(0)("id")
                objProgramComponentByProject.idproject = data.Rows(0)("idproject")
                objProgramComponentByProject.idProgramComponent = data.Rows(0)("idProgramComponent")
                objProgramComponentByProject.ProgramComponentNAME = data.Rows(0)("ProgramComponentname")

            End If

            ' retornar el objeto
            Return objProgramComponentByProject

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProgramComponentByProject = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproject"></param>
    ''' <param name="idProgramComponent"></param>
    ''' <returns>un objeto de tipo List(Of ProgramComponentByProjectEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentByProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProgramComponentByProject As ProgramComponentByProjectEntity
        Dim ProgramComponentByProjectList As New List(Of ProgramComponentByProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT ProgramComponentByProject.Id, ProgramComponentByProject.IdProject, ")
            sql.Append("    ProgramComponentByProject.IdProgramComponent, ProgramComponent.Name AS ProgramComponentname ,ProgramComponent.Code ")
            sql.Append(" FROM ProgramComponentByProject INNER JOIN ")
            sql.Append("    ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id ")

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
            If Not idProgramComponent.Equals("") Then

                sql.Append(where & " idProgramComponent = '" & idProgramComponent & "'")
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
                objProgramComponentByProject = New ProgramComponentByProjectEntity

                ' cargar el valor del campo
                objProgramComponentByProject.id = row("id")
                objProgramComponentByProject.idproject = row("idproject")
                objProgramComponentByProject.idProgramComponent = row("idProgramComponent")
                objProgramComponentByProject.ProgramComponentNAME = row("ProgramComponentname")
                objProgramComponentByProject.CODE = row("Code")

                ' agregar a la lista
                ProgramComponentByProjectList.Add(objProgramComponentByProject)

            Next

            ' retornar el objeto
            getList = ProgramComponentByProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponentByProject. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProgramComponentByProject = Nothing
            ProgramComponentByProjectList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponentByProject
    ''' </summary>
    ''' <param name="ProgramComponentByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponentByProject As ProgramComponentByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ProgramComponentByProject SET")
            sql.AppendLine(" idproject = '" & ProgramComponentByProject.idproject & "',")
            sql.AppendLine(" idProgramComponent = '" & ProgramComponentByProject.idProgramComponent & "'")
            sql.AppendLine("WHERE id = " & ProgramComponentByProject.id)

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
            Throw New Exception("Error al modificar el ProgramComponentByProject. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponentByProject de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByProject As Integer, _
       ByVal idProject As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProgramComponentByProject ")
            If idProject = 0 Then
                SQL.AppendLine(" where id = '" & idProgramComponentByProject & "' ")
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
            Throw New Exception("Error al elimiar el ProgramComponentByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
