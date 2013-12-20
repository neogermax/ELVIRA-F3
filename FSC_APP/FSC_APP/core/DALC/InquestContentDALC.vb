Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class InquestContentDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code">codigo que se desea verificar</param>
    ''' <returns>Verdadero si existe algún registro, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal code As String, _
    Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM InquestContent WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM InquestContent WHERE Code = '" & code & "' AND id <> '" & id & "'")

            End If

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyCode = False

                Else

                    ' retornar que existe
                    verifyCode = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo InquestContent
    ''' </summary>
    ''' <param name="InquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal InquestContent As InquestContentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO InquestContent(" & _
             "idinquest," & _
             "code," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & InquestContent.idinquest & "',")
            sql.AppendLine("'" & InquestContent.code & "',")
            sql.AppendLine("'" & InquestContent.enabled & "',")
            sql.AppendLine("'" & InquestContent.iduser & "',")
            sql.AppendLine("'" & InquestContent.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un InquestContent por el Id
    ''' </summary>
    ''' <param name="idInquestContent"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer) As InquestContentEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objInquestContent As New InquestContentEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT InquestContent.*, ApplicationUser.Name AS ApplicationUserName, Inquest.Name AS InquestName, Inquest.Enabled AS  InquestEnabled ")
            sql.Append(" FROM InquestContent ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = InquestContent.IdUser ")
            sql.Append(" INNER JOIN Inquest ON Inquest.Id = InquestContent.IdInquest ")
            sql.Append(" WHERE InquestContent.Id = " & idInquestContent)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objInquestContent.id = data.Rows(0)("id")
                objInquestContent.idinquest = data.Rows(0)("idinquest")
                objInquestContent.code = data.Rows(0)("code")
                objInquestContent.enabled = data.Rows(0)("enabled")
                objInquestContent.iduser = data.Rows(0)("iduser")
                objInquestContent.createdate = data.Rows(0)("createdate")
                objInquestContent.USERNAME = data.Rows(0)("ApplicationUserName")
                If Not (IsDBNull(data.Rows(0)("InquestName"))) Then objInquestContent.INQUESTNAME = data.Rows(0)("InquestName")
                objInquestContent.INQUESTENABLED = data.Rows(0)("InquestEnabled")

            End If

            ' retornar el objeto
            Return objInquestContent

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un contenido de encuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objInquestContent = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idinquest"></param>
    ''' <param name="code"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="questionText"></param>
    ''' <param name="questionType"></param>
    ''' <param name="answer"></param>
    ''' <returns>un objeto de tipo List(Of InquestContentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idinquest As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal questionText As String = "", _
        Optional ByVal questionType As String = "", _
        Optional ByVal answer As String = "", _
        Optional ByVal order As String = "") As List(Of InquestContentEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objInquestContent As InquestContentEntity
        Dim InquestContentList As New List(Of InquestContentEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append("SELECT InquestContent.*, ApplicationUser.Name AS ApplicationUserName, tabla1.QuestionId, (SELECT Top 1  QuestionsByInquestContent.QuestionText FROM  QuestionsByInquestContent")
            sql.Append(" WHERE  tabla1.QuestionId= QuestionsByInquestContent.Id ) as questionText, (SELECT Top 1  QuestionsByInquestContent.QuestionType FROM  QuestionsByInquestContent")
            sql.Append(" WHERE  tabla1.QuestionId= QuestionsByInquestContent.Id ) as questionType, (SELECT TOP 1 AnswersByQuestion.Answer FROM AnswersByQuestion")
            sql.Append(" WHERE tabla1.QuestionId  = AnswersByQuestion.IdQuestionsByInquestContent")
            ' verificar si hay entrada de datos para el campo
            If Not answer.Equals("") Then sql.Append(" AND answer like '%" & answer & "%'")
            sql.Append(") AS answer")
            sql.Append(" FROM InquestContent INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = InquestContent.IdUser")
            sql.Append(" INNER JOIN (SELECT InquestContent.Id, (SELECT Top 1  QuestionsByInquestContent.Id FROM  QuestionsByInquestContent")
            sql.Append(" LEFT JOIN AnswersByQuestion ON AnswersByQuestion.IdQuestionsByInquestContent = QuestionsByInquestContent.Id")
            sql.Append(" WHERE InquestContent.Id = QuestionsByInquestContent.IdInquestContent")
            ' verificar si hay entrada de datos para el campo
            If Not questionText.Equals("") Then sql.Append(" AND questionText like '%" & questionText & "%'")
            ' verificar si hay entrada de datos para el campo
            If Not questionType.Equals("") Then sql.Append(" AND questionType like '%" & questionType & "%'")
            ' verificar si hay entrada de datos para el campo
            If Not answer.Equals("") Then sql.Append(" AND answer like '%" & answer & "%'")
            sql.Append(" ) AS questionId FROM InquestContent) as tabla1")
            sql.Append(" ON tabla1.Id = InquestContent.Id")

            ' verificar si hay entrada de datos para los campos
            If (Not questionText.Equals("") OrElse Not questionType.Equals("") OrElse Not answer.Equals("")) Then

                sql.Append(where & " tabla1.QuestionId IS NOT NULL ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " InquestContent.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " InquestContent.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idinquest.Equals("") Then

                sql.Append(where & " InquestContent.idinquest = '" & idinquest & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " InquestContent.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " InquestContent.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " InquestContent.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " InquestContent.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, InquestContent.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case "questionText"
                        sql.Append(" ORDER BY questionText ")
                    Case "questionType"
                        sql.Append(" ORDER BY questionType ")
                    Case "answer"
                        sql.Append(" ORDER BY answer ")
                    Case Else
                        sql.Append(" ORDER BY InquestContent." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objInquestContent = New InquestContentEntity

                ' cargar el valor del campo
                objInquestContent.id = row("id")
                objInquestContent.idinquest = row("idinquest")
                objInquestContent.code = row("code")
                objInquestContent.enabled = row("enabled")
                objInquestContent.iduser = row("iduser")
                objInquestContent.createdate = row("createdate")
                objInquestContent.USERNAME = row("ApplicationUserName")
                If Not (IsDBNull(row("questionText"))) Then objInquestContent.QUESTIONTEXT = row("questionText")
                If Not (IsDBNull(row("questionType"))) Then objInquestContent.QUESTIONTYPE = row("questionType")
                If Not (IsDBNull(row("answer"))) Then objInquestContent.ANSWER = row("answer")

                ' agregar a la lista
                InquestContentList.Add(objInquestContent)

            Next

            ' retornar el objeto
            getList = InquestContentList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de contenido de encuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            objInquestContent = Nothing
            InquestContentList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo InquestContent
    ''' </summary>
    ''' <param name="InquestContent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal InquestContent As InquestContentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update InquestContent SET")
            sql.AppendLine(" idinquest = '" & InquestContent.idinquest & "',")
            sql.AppendLine(" code = '" & InquestContent.code & "',")
            sql.AppendLine(" enabled = '" & InquestContent.enabled & "',")
            sql.AppendLine(" iduser = '" & InquestContent.iduser & "',")
            sql.AppendLine(" createdate = '" & InquestContent.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE id = " & InquestContent.id)

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
            Throw New Exception("Error al modificar el contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el InquestContent de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquestContent As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from InquestContent ")
            SQL.AppendLine(" where id = '" & idInquestContent & "' ")

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
            Throw New Exception("Error al elimiar el contenido de encuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
