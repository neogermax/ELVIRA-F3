Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class QuestionsByInquestContentDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo QuestionsByInquestContent
    ''' </summary>
    ''' <param name="QuestionsByInquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal QuestionsByInquestContent As QuestionsByInquestContentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO QuestionsByInquestContent(" & _
             "idinquestcontent," & _
             "questiontext," & _
             "questiontype" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & QuestionsByInquestContent.idinquestcontent & "',")
            sql.AppendLine("'" & QuestionsByInquestContent.questiontext & "',")
            sql.AppendLine("'" & QuestionsByInquestContent.questiontype & "')")

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
            Throw New Exception("Error al insertar la pregunta por contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un QuestionsByInquestContent por el Id
    ''' </summary>
    ''' <param name="idQuestionsByInquestContent"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idQuestionsByInquestContent As Integer) As QuestionsByInquestContentEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objQuestionsByInquestContent As New QuestionsByInquestContentEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM QuestionsByInquestContent ")
            sql.Append(" WHERE QuestionsByInquestContent.Id = " & idQuestionsByInquestContent)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objQuestionsByInquestContent.id = data.Rows(0)("id")
                objQuestionsByInquestContent.idinquestcontent = data.Rows(0)("idinquestcontent")
                objQuestionsByInquestContent.questiontext = data.Rows(0)("questiontext")
                objQuestionsByInquestContent.questiontype = data.Rows(0)("questiontype")

            End If

            ' retornar el objeto
            Return objQuestionsByInquestContent

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una pregunta por contenido de encuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objQuestionsByInquestContent = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idinquestcontent"></param>
    ''' <param name="questiontext"></param>
    ''' <param name="questiontype"></param>
    ''' <returns>un objeto de tipo List(Of QuestionsByInquestContentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal questiontext As String = "", _
        Optional ByVal questiontype As String = "", _
        Optional ByVal order As String = "") As List(Of QuestionsByInquestContentEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objQuestionsByInquestContent As QuestionsByInquestContentEntity
        Dim QuestionsByInquestContentList As New List(Of QuestionsByInquestContentEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM QuestionsByInquestContent ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idinquestcontent.Equals("") Then

                sql.Append(where & " idinquestcontent like '%" & idinquestcontent & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not questiontext.Equals("") Then

                sql.Append(where & " questiontext like '%" & questiontext & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not questiontype.Equals("") Then

                sql.Append(where & " questiontype like '%" & questiontype & "%'")
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
                objQuestionsByInquestContent = New QuestionsByInquestContentEntity

                ' cargar el valor del campo
                objQuestionsByInquestContent.id = row("id")
                objQuestionsByInquestContent.idinquestcontent = row("idinquestcontent")
                objQuestionsByInquestContent.questiontext = row("questiontext")
                objQuestionsByInquestContent.questiontype = row("questiontype")

                ' agregar a la lista
                QuestionsByInquestContentList.Add(objQuestionsByInquestContent)

            Next

            ' retornar el objeto
            getList = QuestionsByInquestContentList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de preguntas por contenido de encuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            objQuestionsByInquestContent = Nothing
            QuestionsByInquestContentList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo QuestionsByInquestContent
    ''' </summary>
    ''' <param name="QuestionsByInquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal QuestionsByInquestContent As QuestionsByInquestContentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update QuestionsByInquestContent SET")
            sql.AppendLine(" idinquestcontent = '" & QuestionsByInquestContent.idinquestcontent & "',")
            sql.AppendLine(" questiontext = '" & QuestionsByInquestContent.questiontext & "',")
            sql.AppendLine(" questiontype = '" & QuestionsByInquestContent.questiontype & "'")
            sql.AppendLine(" WHERE id = " & QuestionsByInquestContent.id)

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
            Throw New Exception("Error al modificar la pregunta por contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el QuestionsByInquestContent de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idQuestionsByInquestContent As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from QuestionsByInquestContent ")
            SQL.AppendLine(" where id = '" & idQuestionsByInquestContent & "' ")

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
            Throw New Exception("Error al elimiar la pregunta por contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite eliminar todas las preguntas relacionadas con una encuesta determinada.
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idInquestContent">identificador del contenido de la encuesta</param>
    ''' <remarks></remarks>
    Public Sub deleteAllByInquestContent(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer)

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" DELETE FROM QuestionsByInquestContent ")
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
