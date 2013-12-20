Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class DocumentsByEntityDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo DocumentsByEntity
    ''' </summary>
    ''' <param name="DocumentsByEntity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal DocumentsByEntity As DocumentsByEntityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO DocumentsByEntity(" & _
             "iddocuments," & _
             "idnentity," & _
             "entityName" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & DocumentsByEntity.iddocuments & "',")
            sql.AppendLine("'" & DocumentsByEntity.idnentity & "',")
            sql.AppendLine("'" & DocumentsByEntity.entityname & "')")

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
            Throw New Exception("Error al insertar el documento por entidad." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un DocumentsByEntity por el Id
    ''' </summary>
    ''' <param name="idDocumentsByEntity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentsByEntity As Integer) As DocumentsByEntityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objDocumentsByEntity As New DocumentsByEntityEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM DocumentsByEntity ")
            sql.Append(" WHERE Id = " & idDocumentsByEntity)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objDocumentsByEntity.id = data.Rows(0)("id")
                objDocumentsByEntity.iddocuments = data.Rows(0)("iddocuments")
                objDocumentsByEntity.idnentity = data.Rows(0)("idnentity")
                objDocumentsByEntity.entityname = data.Rows(0)("entityName")

            End If

            ' retornar el objeto
            Return objDocumentsByEntity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un documento por entidad.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objDocumentsByEntity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="iddocuments"></param>
    ''' <param name="idnentity"></param>
    ''' <param name="nameentity"></param>
    ''' <returns>un objeto de tipo List(Of DocumentsByEntityEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal iddocuments As String = "", _
        Optional ByVal idnentity As String = "", _
        Optional ByVal entityName As String = "", _
        Optional ByVal order As String = "") As List(Of DocumentsByEntityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objDocumentsByEntity As DocumentsByEntityEntity
        Dim DocumentsByEntityList As New List(Of DocumentsByEntityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM DocumentsByEntity ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iddocuments.Equals("") Then

                sql.Append(where & " iddocuments like '%" & iddocuments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idnentity.Equals("") Then

                'sql.Append(where & " idnentity like '%" & idnentity & "%'")
                sql.Append(where & " idnentity = '" & idnentity & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not entityName.Equals("") Then

                'sql.Append(where & " entityName like '%" & entityName & "%'")
                sql.Append(where & " entityName = '" & entityName & "'")
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
                objDocumentsByEntity = New DocumentsByEntityEntity

                ' cargar el valor del campo
                objDocumentsByEntity.id = row("id")
                objDocumentsByEntity.iddocuments = row("iddocuments")
                objDocumentsByEntity.idnentity = row("idnentity")
                objDocumentsByEntity.entityname = row("entityName")

                ' agregar a la lista
                DocumentsByEntityList.Add(objDocumentsByEntity)

            Next

            ' retornar el objeto
            getList = DocumentsByEntityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de documentos por entidad. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objDocumentsByEntity = Nothing
            DocumentsByEntityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo DocumentsByEntity
    ''' </summary>
    ''' <param name="DocumentsByEntity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal DocumentsByEntity As DocumentsByEntityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update DocumentsByEntity SET")
            sql.AppendLine(" iddocuments = '" & DocumentsByEntity.iddocuments & "',")
            sql.AppendLine(" idnentity = '" & DocumentsByEntity.idnentity & "',")
            sql.AppendLine(" entityName = '" & DocumentsByEntity.entityname & "'")
            sql.AppendLine(" WHERE id = " & DocumentsByEntity.id)

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
            Throw New Exception("Error al modificar el documento por entidad." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el DocumentsByEntity de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocumentsByEntity As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from DocumentsByEntity ")
            SQL.AppendLine(" where id = '" & idDocumentsByEntity & "' ")

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
            Throw New Exception("Error al elimiar el documento por entidad." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el registro asociado a un documento y entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idDocument">identificador del documento</param>
    ''' <param name="idEntity">identificador de la entidad</param>
    ''' <param name="nameEntity">nombre de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteByDocumentAndEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocument As Integer, ByVal idEntity As Integer, ByVal entityName As String) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" DELETE FROM DocumentsByEntity")
            SQL.AppendLine(" WHERE IdDocuments = " & idDocument & " AND IdnEntity = " & idEntity & " AND EntityName = '" & entityName & "'")

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
            Throw New Exception("Error al elimiar el Documento. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el registro asociado a una entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idEntity">identificador de la entidad</param>
    ''' <param name="nameEntity">nombre de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idEntity As Integer, ByVal entityName As String) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from DocumentsByEntity")
            SQL.AppendLine(" where IdnEntity = " & idEntity & " AND EntityName = '" & entityName & "'")

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
            Throw New Exception("Error al elimiar el Documents. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
