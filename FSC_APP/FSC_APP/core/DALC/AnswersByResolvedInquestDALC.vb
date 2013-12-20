Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AnswersByResolvedInquestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo AnswersByResolvedInquest
    ''' </summary>
    ''' <param name="AnswersByResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AnswersByResolvedInquest As AnswersByResolvedInquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO AnswersByResolvedInquest(" & _
             "idresolvedinquest," & _
             "idquestionsbyinquestcontent," & _
             "idanswersbyquestion," & _
             "answertext" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & AnswersByResolvedInquest.idresolvedinquest & "',")
            sql.AppendLine("'" & AnswersByResolvedInquest.idquestionsbyinquestcontent & "',")
            If (AnswersByResolvedInquest.idanswersbyquestion > 0) Then
                sql.AppendLine("'" & AnswersByResolvedInquest.idanswersbyquestion & "',")
            Else
                sql.AppendLine("NULL,")
            End If
            sql.AppendLine("'" & AnswersByResolvedInquest.answertext & "')")

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
            Throw New Exception("Error al insertar la respuesta por encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AnswersByResolvedInquest por el Id
    ''' </summary>
    ''' <param name="idAnswersByResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByResolvedInquest As Integer) As AnswersByResolvedInquestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objAnswersByResolvedInquest As New AnswersByResolvedInquestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM AnswersByResolvedInquest ")
            sql.Append(" WHERE Id = " & idAnswersByResolvedInquest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objAnswersByResolvedInquest.id = data.Rows(0)("id")
                objAnswersByResolvedInquest.idresolvedinquest = data.Rows(0)("idresolvedinquest")
                objAnswersByResolvedInquest.idquestionsbyinquestcontent = data.Rows(0)("idquestionsbyinquestcontent")
                If Not (IsDBNull(data.Rows(0)("idanswersbyquestion"))) Then objAnswersByResolvedInquest.idanswersbyquestion = data.Rows(0)("idanswersbyquestion")
                objAnswersByResolvedInquest.answertext = data.Rows(0)("answertext")

            End If

            ' retornar el objeto
            Return objAnswersByResolvedInquest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una respuesta por encuesta resuelta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objAnswersByResolvedInquest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idresolvedinquest"></param>
    ''' <param name="idquestionsbyinquestcontent"></param>
    ''' <param name="idanswersbyquestion"></param>
    ''' <param name="answertext"></param>
    ''' <returns>un objeto de tipo List(Of AnswersByResolvedInquestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idresolvedinquest As String = "", _
        Optional ByVal idquestionsbyinquestcontent As String = "", _
        Optional ByVal idanswersbyquestion As String = "", _
        Optional ByVal answertext As String = "", _
        Optional ByVal order As String = "") As List(Of AnswersByResolvedInquestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objAnswersByResolvedInquest As AnswersByResolvedInquestEntity
        Dim AnswersByResolvedInquestList As New List(Of AnswersByResolvedInquestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM AnswersByResolvedInquest ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idresolvedinquest.Equals("") Then

                sql.Append(where & " idresolvedinquest like '%" & idresolvedinquest & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idquestionsbyinquestcontent.Equals("") Then

                sql.Append(where & " idquestionsbyinquestcontent like '%" & idquestionsbyinquestcontent & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idanswersbyquestion.Equals("") Then

                sql.Append(where & " idanswersbyquestion like '%" & idanswersbyquestion & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not answertext.Equals("") Then

                sql.Append(where & " answertext like '%" & answertext & "%'")
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
                objAnswersByResolvedInquest = New AnswersByResolvedInquestEntity

                ' cargar el valor del campo
                objAnswersByResolvedInquest.id = row("id")
                objAnswersByResolvedInquest.idresolvedinquest = row("idresolvedinquest")
                objAnswersByResolvedInquest.idquestionsbyinquestcontent = row("idquestionsbyinquestcontent")
                If Not (IsDBNull(row("idanswersbyquestion"))) Then objAnswersByResolvedInquest.idanswersbyquestion = row("idanswersbyquestion")
                objAnswersByResolvedInquest.answertext = row("answertext")

                ' agregar a la lista
                AnswersByResolvedInquestList.Add(objAnswersByResolvedInquest)

            Next

            ' retornar el objeto
            getList = AnswersByResolvedInquestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de respuestas por encuesta resuelta.")

        Finally
            ' liberando recursos
            sql = Nothing
            objAnswersByResolvedInquest = Nothing
            AnswersByResolvedInquestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AnswersByResolvedInquest
    ''' </summary>
    ''' <param name="AnswersByResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AnswersByResolvedInquest As AnswersByResolvedInquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update AnswersByResolvedInquest SET")
            sql.AppendLine(" id = '" & AnswersByResolvedInquest.id & "',")
            sql.AppendLine(" idresolvedinquest = '" & AnswersByResolvedInquest.idresolvedinquest & "',")
            sql.AppendLine(" idquestionsbyinquestcontent = '" & AnswersByResolvedInquest.idquestionsbyinquestcontent & "',")
            If (AnswersByResolvedInquest.idanswersbyquestion > 0) Then
                sql.AppendLine(" idanswersbyquestion = '" & AnswersByResolvedInquest.idanswersbyquestion & "',")
            Else
                sql.AppendLine(" idanswersbyquestion = NULL,")
            End If
            sql.AppendLine(" answertext = '" & AnswersByResolvedInquest.answertext & "'")
            sql.AppendLine(" WHERE id = " & AnswersByResolvedInquest.id)

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
            Throw New Exception("Error al modificar la respuesta por encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AnswersByResolvedInquest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByResolvedInquest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from AnswersByResolvedInquest ")
            SQL.AppendLine(" where id = '" & idAnswersByResolvedInquest & "' ")

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
            Throw New Exception("Error al elimiar la respuesta por encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
