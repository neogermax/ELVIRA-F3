Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class IdeaApprovalRecordDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo IdeaApprovalRecord
    ''' </summary>
    ''' <param name="IdeaApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal IdeaApprovalRecord As IdeaApprovalRecordEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO IdeaApprovalRecord(" & _
             "idproject," & _
             "comments," & _
             "attachment," & _
             "approvaldate," & _
             "actnumber," & _
             "approvedvalue," & _
             "approved," & _
             "codeapprovedidea," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "IdProcessInstance," & _
             "IdActivityInstance," & _
             "aportFSC," & _
             "aportOtros," & _
             "Ididea" & _
             ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & IdeaApprovalRecord.idproject & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.comments & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.attachment & "',")
            If (IdeaApprovalRecord.approvaldate <> defaultDate) Then
                sql.AppendLine("'" & IdeaApprovalRecord.approvaldate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" NULL,")
            End If
            sql.AppendLine("'" & IdeaApprovalRecord.actnumber & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.approvedvalue.ToString().Replace(".", " ") & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.approved & "',")
            'TODO:43 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            sql.AppendLine("'" & IdeaApprovalRecord.codeapprovedidea & "',")
            'TODO:43 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            'cierre de cambio
            sql.AppendLine("'" & IdeaApprovalRecord.enabled & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.iduser & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.IdProcessInstance & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.IdActivityInstance & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.aportFSC.ToString().Replace(".", " ") & "',")
            sql.AppendLine("'" & IdeaApprovalRecord.aportOtros.ToString().Replace(".", " ") & "',")
            'TODO:44 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            sql.AppendLine("'" & IdeaApprovalRecord.Ididea & "')")
            'TODO:44 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            'cierre de cambio



            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            sql.Remove(0, sql.Length)
            'modifica el codigo con el mismo Id 
            sql.AppendLine("Update IdeaApprovalRecord SET")
            sql.AppendLine(" code = '" & num & "'")
            sql.AppendLine(" WHERE id = " & num)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)


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
            Throw New Exception("Error al insertar el IdeaApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un IdeaApprovalRecord por el Id
    ''' </summary>
    ''' <param name="idIdeaApprovalRecord"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdeaApprovalRecord As Integer) As IdeaApprovalRecordEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objIdeaApprovalRecord As New IdeaApprovalRecordEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT IdeaApprovalRecord.*, Project.Name AS projectname, ApplicationUser.Name AS username ")
            sql.Append(" FROM IdeaApprovalRecord INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON IdeaApprovalRecord.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" 	Project ON IdeaApprovalRecord.IdProject = Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" WHERE IdeaApprovalRecord.Id = " & idIdeaApprovalRecord)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objIdeaApprovalRecord.id = data.Rows(0)("id")
                objIdeaApprovalRecord.idproject = data.Rows(0)("idproject")
                objIdeaApprovalRecord.code = data.Rows(0)("code")
                objIdeaApprovalRecord.comments = data.Rows(0)("comments")
                objIdeaApprovalRecord.attachment = data.Rows(0)("attachment")
                objIdeaApprovalRecord.approvaldate = IIf(Not IsDBNull(data.Rows(0)("approvaldate")), data.Rows(0)("approvaldate"), Nothing)
                objIdeaApprovalRecord.actnumber = data.Rows(0)("actnumber")
                objIdeaApprovalRecord.approvedvalue = data.Rows(0)("approvedvalue")
                objIdeaApprovalRecord.approved = data.Rows(0)("approved")
                'TODO:45 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                objIdeaApprovalRecord.codeapprovedidea = data.Rows(0)("codeapprovedidea")
                'TODO:45 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                'cierre de cambio
                objIdeaApprovalRecord.enabled = data.Rows(0)("enabled")
                objIdeaApprovalRecord.iduser = data.Rows(0)("iduser")
                objIdeaApprovalRecord.createdate = data.Rows(0)("createdate")
                objIdeaApprovalRecord.PROJECTNAME = data.Rows(0)("projectname")
                objIdeaApprovalRecord.USERNAME = data.Rows(0)("username")
                objIdeaApprovalRecord.IdProcessInstance = data.Rows(0)("IdProcessInstance")
                objIdeaApprovalRecord.IdActivityInstance = data.Rows(0)("IdActivityInstance")
                'TODO:46 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                objIdeaApprovalRecord.Ididea = data.Rows(0)("Ididea")
                'TODO:46 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                'cierre de cambio


            End If

            ' retornar el objeto
            Return objIdeaApprovalRecord

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un IdeaApprovalRecord. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objIdeaApprovalRecord = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="code"></param>
    ''' <param name="comments"></param>
    ''' <param name="attachment"></param>
    ''' <param name="approvaldate"></param>
    ''' <param name="actnumber"></param>
    ''' <param name="approvedvalue"></param>
    ''' <param name="approved"></param>
    ''' <param name="approvedtext"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of IdeaApprovalRecordEntity)</returns>
    ''' <remarks></remarks>
    ''' TODO: 47 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
    ''' 16-06-2013 GERMAN RODRIGUEZ MGgroup

    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal approvaldate As String = "", _
        Optional ByVal actnumber As String = "", _
        Optional ByVal approvedvalue As String = "", _
        Optional ByVal approved As String = "", _
        Optional ByVal approvedtext As String = "", _
        Optional ByVal codeapprovedidea As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal Ididea As String = "") As List(Of IdeaApprovalRecordEntity)

        ' TODO: 47 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
        ' 16-06-2013 GERMAN RODRIGUEZ MGgroup
        ' cierre de cambio


        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIdeaApprovalRecord As IdeaApprovalRecordEntity
        Dim IdeaApprovalRecordList As New List(Of IdeaApprovalRecordEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT IdeaApprovalRecord.*, idea.Name AS projectname , ApplicationUser.Name AS username   ")
            sql.Append(" FROM IdeaApprovalRecord INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON IdeaApprovalRecord.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" idea ON  idea.Id = IdeaApprovalRecord.Ididea	 ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " idea.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approvaldate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, IdeaApprovalRecord.approvaldate, 103) like '%" & approvaldate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not actnumber.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.actnumber like '%" & actnumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approvedvalue.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.approvedvalue like '%" & approvedvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approved.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.approved = '" & approved & "'")
                where = " AND "

            End If

            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            ' verificar si hay entrada de datos para el campo
            If Not codeapprovedidea.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.codeapprovedidea = '" & codeapprovedidea & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not approvedtext.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.approved IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Junta Directiva' AS appro, 1 AS Value ")
                sql.Append(" UNION SELECT 'Dirección Ejecutiva' AS appro, 2 AS Value ")
                sql.Append(" UNION SELECT 'Direccion Ejecutiva' AS appro, 2 AS Value) Temp ")
                sql.Append(" WHERE appro LIKE '%" & approvedtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            'If Not createdate.Equals("") Then

            '    sql.Append(where & " CONVERT(NVARCHAR, IdeaApprovalRecord.createdate, 103) like '%" & createdate & "%'")
            '    where = " AND "

            'End If

            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            ' verificar si hay entrada de datos para el campo
            If Not Ididea.Equals("") Then

                sql.Append(where & " IdeaApprovalRecord.Ididea = '" & Ididea & "'")
                where = " AND "

            End If


            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case "projectname"
                        sql.Append(" ORDER BY projectname ")
                    Case Else
                        sql.Append(" ORDER BY IdeaApprovalRecord.CreateDate desc ")
                End Select

            End If

            sql.Append(" ORDER BY IdeaApprovalRecord.CreateDate desc ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objIdeaApprovalRecord = New IdeaApprovalRecordEntity

                ' cargar el valor del campo
                objIdeaApprovalRecord.id = row("id")
                objIdeaApprovalRecord.idproject = row("idproject")
                objIdeaApprovalRecord.code = row("code")
                objIdeaApprovalRecord.comments = row("comments")
                objIdeaApprovalRecord.attachment = row("attachment")
                objIdeaApprovalRecord.approvaldate = IIf(Not IsDBNull(row("approvaldate")), row("approvaldate"), Nothing)
                objIdeaApprovalRecord.actnumber = row("actnumber")
                objIdeaApprovalRecord.approvedvalue = row("approvedvalue")
                objIdeaApprovalRecord.approved = row("approved")
                ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
                objIdeaApprovalRecord.codeapprovedidea = IIf(Not IsDBNull(row("codeapprovedidea")), row("codeapprovedidea"), Nothing)
                objIdeaApprovalRecord.enabled = row("enabled")
                objIdeaApprovalRecord.iduser = row("iduser")
                objIdeaApprovalRecord.createdate = row("createdate")
                objIdeaApprovalRecord.USERNAME = row("username")
                objIdeaApprovalRecord.PROJECTNAME = row("projectname")
                objIdeaApprovalRecord.IdProcessInstance = row("IdProcessInstance")
                objIdeaApprovalRecord.IdActivityInstance = row("IdActivityInstance")
                ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
                objIdeaApprovalRecord.Ididea = IIf(Not IsDBNull(row("Ididea")), row("Ididea"), Nothing)

                ' agregar a la lista
                IdeaApprovalRecordList.Add(objIdeaApprovalRecord)

            Next

            ' retornar el objeto
            getList = IdeaApprovalRecordList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de IdeaApprovalRecord. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objIdeaApprovalRecord = Nothing
            IdeaApprovalRecordList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo IdeaApprovalRecord
    ''' </summary>
    ''' <param name="IdeaApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal IdeaApprovalRecord As IdeaApprovalRecordEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("Update IdeaApprovalRecord SET")
            sql.AppendLine(" idproject = '" & IdeaApprovalRecord.idproject & "',")
            sql.AppendLine(" code = '" & IdeaApprovalRecord.code & "',")
            sql.AppendLine(" comments = '" & IdeaApprovalRecord.comments & "',")
            sql.AppendLine(" attachment = '" & IdeaApprovalRecord.attachment & "',")
            If IdeaApprovalRecord.approvaldate <> defaultDate Then
                sql.AppendLine(" approvaldate = '" & IdeaApprovalRecord.approvaldate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" approvaldate = NULL,")
            End If
            sql.AppendLine(" actnumber = '" & IdeaApprovalRecord.actnumber & "',")
            sql.AppendLine(" approvedvalue = '" & IdeaApprovalRecord.approvedvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" approved = '" & IdeaApprovalRecord.approved & "',")
            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            sql.AppendLine(" codeapprovedidea = '" & IdeaApprovalRecord.codeapprovedidea & "',")
            sql.AppendLine(" enabled = '" & IdeaApprovalRecord.enabled & "',")
            sql.AppendLine(" iduser = '" & IdeaApprovalRecord.iduser & "',")
            sql.AppendLine(" createdate = '" & IdeaApprovalRecord.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine(" IdProcessInstance = '" & IdeaApprovalRecord.IdProcessInstance & "',")
            sql.AppendLine(" IdActivityInstance = '" & IdeaApprovalRecord.IdActivityInstance & "',")
            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            sql.AppendLine(" Ididea = '" & IdeaApprovalRecord.Ididea & "'")

            sql.AppendLine("WHERE id = " & IdeaApprovalRecord.id)

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
            Throw New Exception("Error al modificar el IdeaApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el IdeaApprovalRecord de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdeaApprovalRecord As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from IdeaApprovalRecord ")
            SQL.AppendLine(" where id = '" & idIdeaApprovalRecord & "' ")

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
            Throw New Exception("Error al elimiar el IdeaApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
