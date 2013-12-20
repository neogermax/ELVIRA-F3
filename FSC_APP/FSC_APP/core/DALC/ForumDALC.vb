Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ForumDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Forum
    ''' </summary>
    ''' <param name="Forum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Forum As ForumEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Forum(" & _
             "idproject," & _
             "subject," & _
             "message," & _
             "attachment," & _
             "updateddate," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Forum.idproject & "',")
            sql.AppendLine("'" & Forum.subject & "',")
            sql.AppendLine("'" & Forum.message & "',")
            sql.AppendLine("'" & Forum.attachment & "',")
            sql.AppendLine("'" & Forum.updateddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Forum.enabled & "',")
            sql.AppendLine("'" & Forum.iduser & "',")
            sql.AppendLine("'" & Forum.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Forum. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Forum por el Id
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer) As ForumEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objForum As New ForumEntity
        Dim objReplyDALC As New ReplyDALC
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Forum.Id, Forum.IdProject, Forum.Subject, Forum.Message, Forum.Attachment, Forum.UpdatedDate, Forum.Enabled, ")
            sql.Append(" 	Forum.IdUser, Forum.CreateDate, Project.Name AS projectname, ApplicationUser.Name AS username, ")
            sql.Append(" 	(SELECT TOP (1) ApplicationUser_1.Name ")
            sql.Append(" 	 FROM Reply INNER JOIN ")
            sql.Append(" 		    " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 ON Reply.IdUser = ApplicationUser_1.ID ")
            sql.Append("     WHERE (Reply.IdForum = Forum.Id) ")
            sql.Append("     ORDER BY Reply.UpdateDate DESC) AS lastreplyusername, ")
            sql.Append(" 	(SELECT TOP (1) CreateDate ")
            sql.Append(" 	 FROM Reply As Reply_c ")
            sql.Append("     WHERE (IdForum = Forum.Id) ")
            sql.Append("     ORDER BY UpdateDate DESC) AS lastreplycreatedate, ")
            sql.Append("    (SELECT COUNT(Id) AS cont ")
            sql.Append("     FROM Reply AS Reply_1 ")
            sql.Append("     WHERE (IdForum = Forum.Id)) AS replycount ")
            sql.Append(" FROM Forum INNER JOIN ")
            sql.Append(" 	Project ON Forum.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Forum.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Forum.Id = " & idForum)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objForum.id = data.Rows(0)("id")
                objForum.idproject = data.Rows(0)("idproject")
                objForum.subject = data.Rows(0)("subject")
                objForum.message = data.Rows(0)("message")
                objForum.attachment = IIf(Not IsDBNull(data.Rows(0)("attachment")), data.Rows(0)("attachment"), "")
                objForum.updateddate = IIf(Not IsDBNull(data.Rows(0)("updateddate")), data.Rows(0)("updateddate"), "")
                objForum.enabled = data.Rows(0)("enabled")
                objForum.iduser = data.Rows(0)("iduser")
                objForum.createdate = data.Rows(0)("createdate")
                objForum.USERNAME = data.Rows(0)("username")
                objForum.PROJECTNAME = data.Rows(0)("projectname")
                objForum.LASTREPLYUSERNAME = IIf(Not IsDBNull(data.Rows(0)("lastreplyusername")), data.Rows(0)("lastreplyusername"), "")
                objForum.REPLYCOUNT = data.Rows(0)("replycount")
                objForum.LASTREPLYCREATEDATE = IIf(Not IsDBNull(data.Rows(0)("lastreplycreatedate")), data.Rows(0)("lastreplycreatedate"), Nothing)
                objForum.REPLYLIST = objReplyDALC.getList(objApplicationCredentials, idforum:=objForum.id)

            End If

            ' retornar el objeto
            Return objForum

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Forum. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objForum = Nothing
            objReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="subject"></param>
    ''' <param name="message"></param>
    ''' <param name="attachment"></param>
    ''' <param name="updateddate"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="replycount"></param> 
    ''' <param name="lastreplyusername"></param>
    ''' <param name="lastreplycreatedate"></param>
    ''' <returns>un objeto de tipo List(Of ForumEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal subject As String = "", _
        Optional ByVal message As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal updateddate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal replycount As String = "", _
        Optional ByVal lastreplyusername As String = "", _
        Optional ByVal lastreplycreatedate As String = "", _
        Optional ByVal order As String = "") As List(Of ForumEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objForum As ForumEntity
        Dim ForumList As New List(Of ForumEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim objReplyDALC As New ReplyDALC
        Dim count As New Integer
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Forum.Id, Forum.IdProject, Forum.Subject, Forum.Message, Forum.Attachment, Forum.UpdatedDate, Forum.Enabled, ")
            sql.Append(" 	Forum.IdUser, Forum.CreateDate, Project.Name AS projectname, ApplicationUser.Name AS username, ")
            sql.Append(" 	(SELECT TOP (1) ApplicationUser_1.Name ")
            sql.Append(" 	 FROM Reply INNER JOIN ")
            sql.Append(" 		    " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 ON Reply.IdUser = ApplicationUser_1.ID ")
            sql.Append("     WHERE (Reply.IdForum = Forum.Id) ")
            sql.Append("     ORDER BY Reply.UpdateDate DESC) AS lastreplyusername, ")
            sql.Append(" 	(SELECT TOP (1) CreateDate ")
            sql.Append(" 	 FROM Reply As Reply_c ")
            sql.Append("     WHERE (IdForum = Forum.Id) ")
            sql.Append("     ORDER BY UpdateDate DESC) AS lastreplycreatedate, ")
            sql.Append("    (SELECT COUNT(Id) AS cont ")
            sql.Append("     FROM Reply AS Reply_1 ")
            sql.Append("     WHERE (IdForum = Forum.Id)) AS replycount ")
            sql.Append(" FROM Forum INNER JOIN ")
            sql.Append(" 	Project ON Forum.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Forum.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Forum.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Forum.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " Forum.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not subject.Equals("") Then

                sql.Append(where & " Forum.subject like '%" & subject & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not message.Equals("") Then

                sql.Append(where & " Forum.message like '%" & message & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " Forum.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not updateddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR,  Forum.updateddate , 103) like '%" & updateddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Forum.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Forum.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Forum.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR,  Forum.createdate , 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not replycount.Equals("") Then

                If Integer.TryParse(replycount, count) Then
                    sql.Append(where & " (SELECT COUNT(Id) AS cont ")
                    sql.Append(" FROM Reply AS Reply_2 ")
                    sql.Append(" WHERE (IdForum = Forum.Id)) = '" & count.ToString & "'")
                    where = " AND "
                End If

            End If

            ' verificar si hay entrada de datos para el campo
            If Not lastreplyusername.Equals("") Then

                sql.Append(where & " (SELECT TOP (1) ApplicationUser_3.Name ")
                sql.Append(" FROM Reply as Reply_3 INNER JOIN ")
                sql.Append("    " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_3 ON Reply_3.IdUser = ApplicationUser_3.ID ")
                sql.Append(" WHERE (Reply_3.IdForum = Forum.Id) ")
                sql.Append(" ORDER BY Reply_3.UpdateDate DESC) like '%" & lastreplyusername & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not lastreplycreatedate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, (SELECT TOP (1) CreateDate ")
                sql.Append(" 	 FROM Reply As Reply_c ")
                sql.Append("     WHERE (IdForum = Forum.Id) ")
                sql.Append("     ORDER BY UpdateDate DESC), 103)  like '%" & lastreplycreatedate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case "projectname"
                        sql.Append(" ORDER BY projectname ")
                    Case "replycount"
                        sql.Append(" ORDER BY replycount ")
                    Case "lastreplyusername"
                        sql.Append(" ORDER BY lastreplyusername ")
                    Case "lastreplycreatedate"
                        sql.Append(" ORDER BY lastreplycreatedate ")
                    Case Else
                        sql.Append(" ORDER BY Forum." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objForum = New ForumEntity

                ' cargar el valor del campo
                objForum.id = row("id")
                objForum.idproject = row("idproject")
                objForum.subject = row("subject")
                objForum.message = row("message")
                objForum.attachment = IIf(Not IsDBNull(row("attachment")), row("attachment"), "")
                objForum.updateddate = IIf(Not IsDBNull(row("updateddate")), row("updateddate"), "")
                objForum.enabled = row("enabled")
                objForum.iduser = row("iduser")
                objForum.createdate = row("createdate")
                objForum.USERNAME = row("username")
                objForum.PROJECTNAME = row("projectname")
                objForum.LASTREPLYUSERNAME = IIf(Not IsDBNull(row("lastreplyusername")), row("lastreplyusername"), "")
                objForum.REPLYCOUNT = row("replycount")
                objForum.LASTREPLYCREATEDATE = IIf(Not IsDBNull(row("lastreplycreatedate")), row("lastreplycreatedate"), Nothing)
                objForum.REPLYLIST = objReplyDALC.getList(objApplicationCredentials, idforum:=objForum.id)

                ' agregar a la lista
                ForumList.Add(objForum)

            Next

            ' retornar el objeto
            getList = ForumList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Forum. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objForum = Nothing
            ForumList = Nothing
            data = Nothing
            where = Nothing
            objReplyDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Forum
    ''' </summary>
    ''' <param name="Forum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Forum As ForumEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Forum SET")
            SQL.AppendLine(" idproject = '" & Forum.idproject & "',")
            SQL.AppendLine(" subject = '" & Forum.subject & "',")
            SQL.AppendLine(" message = '" & Forum.message & "',")
            SQL.AppendLine(" attachment = '" & Forum.attachment & "',")
            sql.AppendLine(" updateddate = '" & Forum.updateddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            SQL.AppendLine(" enabled = '" & Forum.enabled & "',")
            SQL.AppendLine(" iduser = '" & Forum.iduser & "',")
            sql.AppendLine(" createdate = '" & Forum.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            SQL.AppendLine("WHERE id = " & Forum.id)

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
            Throw New Exception("Error al modificar el Forum. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Forum de una forma
    ''' </summary>
    ''' <param name="idForum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idForum As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Forum ")
            SQL.AppendLine(" where id = '" & idForum & "' ")

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
            Throw New Exception("Error al elimiar el Forum. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
