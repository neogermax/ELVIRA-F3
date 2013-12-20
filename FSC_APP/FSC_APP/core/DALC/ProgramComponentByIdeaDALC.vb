Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProgramComponentByIdeaDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ProgramComponentByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponentByIdea As ProgramComponentByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ProgramComponentByIdea(" & _
             "ididea," & _
             "idProgramComponent" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ProgramComponentByIdea.ididea & "',")
            sql.AppendLine("'" & ProgramComponentByIdea.idProgramComponent & "')")

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
            Throw New Exception("Error al insertar el ProgramComponentByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponentByIdea por el Id
    ''' </summary>
    ''' <param name="idProgramComponentByIdea"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByIdea As Integer) As ProgramComponentByIdeaEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProgramComponentByIdea As New ProgramComponentByIdeaEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ProgramComponentByIdea ")
            sql.Append(" WHERE Id = " & idProgramComponentByIdea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProgramComponentByIdea.id = data.Rows(0)("id")
                objProgramComponentByIdea.ididea = data.Rows(0)("ididea")
                objProgramComponentByIdea.idProgramComponent = data.Rows(0)("idProgramComponent")

            End If

            ' retornar el objeto
            Return objProgramComponentByIdea

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProgramComponentByIdea = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="ididea"></param>
    ''' <param name="idProgramComponent"></param>
    ''' <returns>un objeto de tipo List(Of ProgramComponentByIdeaEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentByIdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProgramComponentByIdea As ProgramComponentByIdeaEntity
        Dim ProgramComponentByIdeaList As New List(Of ProgramComponentByIdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT ProgramComponentByIdea.*, ProgramComponent.IdProgram, Program.IdStrategicLine,  ProgramComponent.Code ")
            sql.Append(" FROM ProgramComponentByIdea ")
            sql.Append(" INNER JOIN ProgramComponent ON ProgramComponentByIdea.idProgramComponent = ProgramComponent.Id ")
            sql.Append(" INNER JOIN Program ON ProgramComponent.IdProgram = Program.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ididea.Equals("") Then

                sql.Append(where & " ididea like '%" & ididea & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProgramComponent.Equals("") Then

                sql.Append(where & " idProgramComponent like '%" & idProgramComponent & "%'")
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
                objProgramComponentByIdea = New ProgramComponentByIdeaEntity

                ' cargar el valor del campo
                objProgramComponentByIdea.id = row("id")
                objProgramComponentByIdea.ididea = row("ididea")
                objProgramComponentByIdea.idProgramComponent = row("idProgramComponent")
                objProgramComponentByIdea.IDProgram = row("IdProgram")
                objProgramComponentByIdea.IDStrategicLine = row("IdStrategicLine")
                objProgramComponentByIdea.CODE = row("Code")

                ' agregar a la lista
                ProgramComponentByIdeaList.Add(objProgramComponentByIdea)

            Next

            ' retornar el objeto
            getList = ProgramComponentByIdeaList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponentByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProgramComponentByIdea = Nothing
            ProgramComponentByIdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ProgramComponentByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponentByIdea As ProgramComponentByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ProgramComponentByIdea SET")
            sql.AppendLine(" id = '" & ProgramComponentByIdea.id & "',")
            sql.AppendLine(" ididea = '" & ProgramComponentByIdea.ididea & "',")
            sql.AppendLine(" idProgramComponent = '" & ProgramComponentByIdea.idProgramComponent & "',")
            sql.AppendLine("WHERE id = " & ProgramComponentByIdea.id)

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
            Throw New Exception("Error al modificar el ProgramComponentByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponentByIdea de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponentByIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProgramComponentByIdea ")
            SQL.AppendLine(" where id = '" & idProgramComponentByIdea & "' ")

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
            Throw New Exception("Error al elimiar el ProgramComponentByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra la informacion de las Componentes del Programa de una idea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProgramComponentByIdea ")
            SQL.AppendLine(" where IdIdea = '" & idIdea & "' ")

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
            Throw New Exception("Error al elimiar el ProgramComponentByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
