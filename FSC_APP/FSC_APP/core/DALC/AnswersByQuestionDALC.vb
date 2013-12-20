Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AnswersByQuestionDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo AnswersByQuestion
    ''' </summary>
    ''' <param name="AnswersByQuestion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal AnswersByQuestion As AnswersByQuestionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO AnswersByQuestion(" & _
             "idquestionsbyinquestcontent," & _
             "idinquestcontent," & _
             "answer" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & AnswersByQuestion.idquestionsbyinquestcontent & "',")
            sql.AppendLine("'" & AnswersByQuestion.idinquestcontent & "',")
            sql.AppendLine("'" & AnswersByQuestion.answer & "')")

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
            Throw New Exception("Error al insertar la respuesta por pregunta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un AnswersByQuestion por el Id
    ''' </summary>
    ''' <param name="idAnswersByQuestion"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByQuestion As Integer) As AnswersByQuestionEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objAnswersByQuestion As New AnswersByQuestionEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM AnswersByQuestion ")
            sql.Append(" WHERE AnswersByQuestion.Id = " & idAnswersByQuestion)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objAnswersByQuestion.id = data.Rows(0)("id")
                objAnswersByQuestion.idquestionsbyinquestcontent = data.Rows(0)("idquestionsbyinquestcontent")
                objAnswersByQuestion.idinquestcontent = data.Rows(0)("idinquestcontent")
                objAnswersByQuestion.answer = data.Rows(0)("answer")

            End If

            ' retornar el objeto
            Return objAnswersByQuestion

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una respuesta por pregunta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objAnswersByQuestion = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idquestionsbyinquestcontent"></param>
    ''' <param name="idinquestcontent"></param>
    ''' <param name="answer"></param>
    ''' <returns>un objeto de tipo List(Of AnswersByQuestionEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idquestionsbyinquestcontent As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal answer As String = "", _
        Optional ByVal order As String = "") As List(Of AnswersByQuestionEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objAnswersByQuestion As AnswersByQuestionEntity
        Dim AnswersByQuestionList As New List(Of AnswersByQuestionEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM AnswersByQuestion ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idquestionsbyinquestcontent.Equals("") Then

                sql.Append(where & " idquestionsbyinquestcontent like '%" & idquestionsbyinquestcontent & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idinquestcontent.Equals("") Then

                sql.Append(where & " idinquestcontent like '%" & idinquestcontent & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not answer.Equals("") Then

                sql.Append(where & " answer like '%" & answer & "%'")
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
                objAnswersByQuestion = New AnswersByQuestionEntity

                ' cargar el valor del campo
                objAnswersByQuestion.id = row("id")
                objAnswersByQuestion.idquestionsbyinquestcontent = row("idquestionsbyinquestcontent")
                objAnswersByQuestion.answer = row("answer")

                ' agregar a la lista
                AnswersByQuestionList.Add(objAnswersByQuestion)

            Next

            ' retornar el objeto
            getList = AnswersByQuestionList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de respuestas por pregunta.")

        Finally
            ' liberando recursos
            sql = Nothing
            objAnswersByQuestion = Nothing
            AnswersByQuestionList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AnswersByQuestion
    ''' </summary>
    ''' <param name="AnswersByQuestion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AnswersByQuestion As AnswersByQuestionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update AnswersByQuestion SET")
            sql.AppendLine(" idquestionsbyinquestcontent = '" & AnswersByQuestion.idquestionsbyinquestcontent & "',")
            sql.AppendLine(" idinquestcontent = '" & AnswersByQuestion.idinquestcontent & "',")
            sql.AppendLine(" answer = '" & AnswersByQuestion.answer & "'")
            sql.AppendLine(" WHERE id = " & AnswersByQuestion.id)

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
            Throw New Exception("Error al modificar la respuesta por pregunta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AnswersByQuestion de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAnswersByQuestion As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from AnswersByQuestion ")
            SQL.AppendLine(" where id = '" & idAnswersByQuestion & "' ")

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
            Throw New Exception("Error al elimiar la respuesta por pregunta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite eliminar todas las respuestas relacionadas con un contenido de encuesta determinada.
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idQuestionsByInquestContent">identificador de la pregunta</param>
    ''' <remarks></remarks>
    Public Sub deleteAllByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer)

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" DELETE FROM AnswersByQuestion ")
            SQL.AppendLine(" WHERE IdInquestContent = '" & idInquestContent & "'")

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
            Throw New Exception("Error al elimiar las preguntas de la encuesta actual." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Sub

End Class
