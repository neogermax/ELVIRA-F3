Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ReplyDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Reply
    ''' </summary>
    ''' <param name="Reply"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal Reply As ReplyEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Reply(" & _
             "idforum," & _
             "iduser," & _
             "subject," & _
             "attachment," & _
             "updatedate," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Reply.idforum & "',")
            sql.AppendLine("'" & Reply.iduser & "',")
            sql.AppendLine("'" & Reply.subject & "',")
            sql.AppendLine("'" & Reply.attachment & "',")
            sql.AppendLine("'" & Reply.updatedate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Reply.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Reply. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un Reply por el Id
    ''' </summary>
    ''' <param name="idReply"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idReply As Integer) As ReplyEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objReply As New ReplyEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Reply.Id, Reply.IdForum, Reply.IdUser, Reply.Subject, Reply.Attachment, Reply.UpdateDate, Reply.CreateDate, ")
            sql.Append(" 	Forum.Subject AS forumsubject, ApplicationUser.Name AS username ")
            sql.Append(" FROM Reply INNER JOIN ")
            sql.Append(" 	Forum ON Reply.IdForum = Forum.Id INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Reply.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Reply.Id = " & idReply)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objReply.id = data.Rows(0)("id")
				objReply.idforum = data.Rows(0)("idforum")
				objReply.iduser = data.Rows(0)("iduser")
				objReply.subject = data.Rows(0)("subject")
                objReply.attachment = IIf(Not IsDBNull(data.Rows(0)("attachment")), data.Rows(0)("attachment"), "")
				objReply.updatedate = data.Rows(0)("updatedate")
                objReply.createdate = data.Rows(0)("createdate")
                objReply.USERNAME = data.Rows(0)("username")
                objReply.FORUMSUBJECT = data.Rows(0)("forumsubject")

            End If

            ' retornar el objeto
            Return objReply

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Reply. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objReply = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar el ultimo Reply por el Id del foro
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Function lastByForum(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer) As ReplyEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objReply As New ReplyEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Reply.Id, Reply.IdForum, Reply.IdUser, Reply.Subject, Reply.Attachment, Reply.UpdateDate, Reply.CreateDate, ")
            sql.Append(" 	Forum.Subject AS forumsubject, ApplicationUser.Name AS username ")
            sql.Append(" FROM Reply INNER JOIN ")
            sql.Append(" 	Forum ON Reply.IdForum = Forum.Id INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Reply.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Reply.IdForum = " & idForum)
            sql.Append("ORDER BY dbo.Reply.UpdateDate DESC ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objReply.id = data.Rows(0)("id")
                objReply.idforum = data.Rows(0)("idforum")
                objReply.iduser = data.Rows(0)("iduser")
                objReply.subject = data.Rows(0)("subject")
                objReply.attachment = IIf(Not IsDBNull(data.Rows(0)("attachment")), data.Rows(0)("attachment"), "")
                objReply.updatedate = data.Rows(0)("updatedate")
                objReply.createdate = data.Rows(0)("createdate")
                objReply.USERNAME = data.Rows(0)("username")
                objReply.FORUMSUBJECT = data.Rows(0)("forumsubject")

            End If

            ' retornar el objeto
            Return objReply

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Reply por IdForum. . ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objReply = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idforum"></param>
    ''' <param name="iduser"></param>
    ''' <param name="forumsubject"></param>
    ''' <param name="username"></param>
    ''' <param name="subject"></param>
    ''' <param name="attachment"></param>
    ''' <param name="updatedate"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ReplyEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idforum As String = "", _
        Optional ByVal forumsubject As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal subject As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal updatedate As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ReplyEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objReply As ReplyEntity
        Dim ReplyList As New List(Of ReplyEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Reply.Id, Reply.IdForum, Reply.IdUser, Reply.Subject, Reply.Attachment, Reply.UpdateDate, Reply.CreateDate, ")
            sql.Append(" 	Forum.Subject AS forumsubject, ApplicationUser.Name AS username ")
            sql.Append(" FROM Reply INNER JOIN ")
            sql.Append(" 	Forum ON Reply.IdForum = Forum.Id INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Reply.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Reply.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Reply.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idforum.Equals("") Then

                sql.Append(where & " Reply.idforum = '" & idforum & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not forumsubject.Equals("") Then

                sql.Append(where & " Forum.Subject like '%" & forumsubject & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Reply.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not subject.Equals("") Then

                sql.Append(where & " Reply.subject like '%" & subject & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " Reply.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not updatedate.Equals("") Then

                sql.Append(where & " CONVERT(NAVRCHAR, Reply.updatedate, 103) like '%" & updatedate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NAVRCHAR, Reply.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case "forumsubject"
                        sql.Append(" ORDER BY forumsubject ")
                    Case Else
                        sql.Append(" ORDER BY " & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objReply = New ReplyEntity

                ' cargar el valor del campo
                objReply.id = row("id")
                objReply.idforum = row("idforum")
                objReply.iduser = row("iduser")
                objReply.subject = row("subject")
                objReply.attachment = row("attachment")
                objReply.updatedate = row("updatedate")
                objReply.createdate = row("createdate")
                objReply.attachment = IIf(Not IsDBNull(row("attachment")), row("attachment"), "")
                objReply.updatedate = row("updatedate")
                objReply.createdate = row("createdate")
                objReply.USERNAME = row("username")
                objReply.FORUMSUBJECT = row("forumsubject")

                ' agregar a la lista
                ReplyList.Add(objReply)

            Next

            ' retornar el objeto
            getList = ReplyList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Reply. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objReply = Nothing
            ReplyList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo Reply
    ''' </summary>
    ''' <param name="Reply"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal Reply As ReplyEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Reply SET")
            SQL.AppendLine(" idforum = '" & Reply.idforum & "',")           
            SQL.AppendLine(" iduser = '" & Reply.iduser & "',")           
            SQL.AppendLine(" subject = '" & Reply.subject & "',")           
            SQL.AppendLine(" attachment = '" & Reply.attachment & "',")           
            sql.AppendLine(" updatedate = '" & Reply.updatedate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine(" createdate = '" & Reply.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            SQL.AppendLine("WHERE id = " & Reply.id)

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
            Throw New Exception("Error al modificar el Reply. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el Reply de una forma
    ''' </summary>
    ''' <param name="idReply"></param>
    ''' <param name="idForum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idReply As Integer, _
       Optional ByVal idForum As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Reply ")
            If idForum = 0 Then
                SQL.AppendLine(" where id = '" & idReply & "' ")
            Else
                SQL.AppendLine(" where idforum = '" & idForum & "' ")
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
            Throw New Exception("Error al elimiar el Reply. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
