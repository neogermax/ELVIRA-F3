Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ObjectiveByActivitDALC

    ' contantes
    Const MODULENAME As String = "ObjectiveByActivityDALC"

    ''' <summary> 
    ''' Registar un nuevo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ObjectiveByActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ObjectiveByActivity As ObjectiveByActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable


        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ObjectiveByActivity(" & _
             "idActivity," & _
             "idObjective" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ObjectiveByActivity.idactivity & "',")
            sql.AppendLine("'" & ObjectiveByActivity.idobjective & "')")

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
            Throw New Exception("Error al insertar el ObjectiveByActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponentByIdea por el Id
    ''' </summary>
    ''' <param name="idObjectiveByActivity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjectiveByActivity As Integer) As ObjectiveByActivityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim ObjectiveByActivity As New ObjectiveByActivityEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ObjectiveyByActivity ")
            sql.Append(" WHERE Id = " & idObjectiveByActivity)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                ObjectiveByActivity.id = data.Rows(0)("id")
                ObjectiveByActivity.idactivity = data.Rows(0)("idactivity")
                ObjectiveByActivity.idobjective = data.Rows(0)("idobjective")

            End If

            ' retornar el objeto
            Return ObjectiveByActivity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ObjectiveByActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            ObjectiveByActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idactivity"></param>
    ''' <param name="idobjective"></param>
    ''' <returns>un objeto de tipo List(Of ProgramComponentByIdeaEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idactivity As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal order As String = "") As List(Of ObjectiveByActivityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim ObjectiveByActivity As ObjectiveByActivityEntity
        Dim ObjectiveByActivityList As New List(Of ObjectiveByActivityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT ObjectiveByActivity.*")
            sql.Append(" FROM  ObjectiveByActivity ")


            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idactivity.Equals("") Then

                sql.Append(where & " idactivity = '" & idactivity & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idobjective.Equals("") Then

                sql.Append(where & " idobjective = '" & idobjective & "'")
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
                ObjectiveByActivity = New ObjectiveByActivityEntity

                ' cargar el valor del campo
                ObjectiveByActivity.id = row("id")
                ObjectiveByActivity.idactivity = row("idActivity")
                ObjectiveByActivity.idobjective = row("idObjective")

                ' agregar a la lista
                ObjectiveByActivityList.Add(ObjectiveByActivity)

            Next

            ' retornar el objeto
            getList = ObjectiveByActivityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ObjectiveByActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            ObjectiveByActivity = Nothing
            ObjectiveByActivityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponentByIdea
    ''' </summary>
    ''' <param name="ObjectiveByActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ObjectiveByActivity As ObjectiveByActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ObjectiveByActivity SET")
            sql.AppendLine(" id = '" & ObjectiveByActivity.id & "',")
            sql.AppendLine(" idactivity= '" & ObjectiveByActivity.idactivity & "',")
            sql.AppendLine(" idobjective = '" & ObjectiveByActivity.idobjective & "',")
            sql.AppendLine("WHERE id = " & ObjectiveByActivity.id)

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
            Throw New Exception("Error al modificar el ObjectiveByActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponentByIdea de una forma
    ''' </summary>
    ''' <param name="idObjectiveByActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjectiveByActivity As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ObjectiveByActivity ")
            SQL.AppendLine(" where id = '" & idObjectiveByActivity & "' ")

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
            Throw New Exception("Error al elimiar el ObjectiveByActivity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra la informacion de las Componentes del Programa de una idea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idActivity">identificador de la idea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ObjectiveByActivity ")
            SQL.AppendLine(" where IdActivity = '" & idActivity & "' ")

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
            Throw New Exception("Error al elimiar el ObjectiveByActivity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
