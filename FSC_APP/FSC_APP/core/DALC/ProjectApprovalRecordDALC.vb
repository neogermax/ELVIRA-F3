Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProjectApprovalRecordDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM ProjectApprovalRecord WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM ProjectApprovalRecord WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de REGISTRAR APROBACIÓN DEL PROYECTO. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo ProjectApprovalRecord
    ''' </summary>
    ''' <param name="ProjectApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProjectApprovalRecord As ProjectApprovalRecordEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ProjectApprovalRecord(" & _
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
            sql.AppendLine("'" & ProjectApprovalRecord.idproject & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.comments & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.attachment & "',")
            If (ProjectApprovalRecord.approvaldate <> defaultDate) Then
                sql.AppendLine("'" & ProjectApprovalRecord.approvaldate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" NULL,")
            End If
            sql.AppendLine("'" & ProjectApprovalRecord.actnumber & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.approvedvalue.ToString().Replace(".", " ") & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.approved & "',")
            'TODO:43 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            sql.AppendLine("'" & ProjectApprovalRecord.codeapprovedidea & "',")
            'TODO:43 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            'cierre de cambio
            sql.AppendLine("'" & ProjectApprovalRecord.enabled & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.iduser & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.IdProcessInstance & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.IdActivityInstance & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.aportFSC.ToString().Replace(".", " ") & "',")
            sql.AppendLine("'" & ProjectApprovalRecord.aportOtros.ToString().Replace(".", " ") & "',")
            'TODO:44 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
            '12-06-2013 GERMAN RODRIGUEZ MGgroup
            sql.AppendLine("'" & ProjectApprovalRecord.Ididea & "')")
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
            sql.AppendLine("Update ProjectApprovalRecord SET")
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
            Throw New Exception("Error al insertar el ProjectApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProjectApprovalRecord por el Id
    ''' </summary>
    ''' <param name="idProjectApprovalRecord"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectApprovalRecord As Integer) As ProjectApprovalRecordEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProjectApprovalRecord As New ProjectApprovalRecordEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ProjectApprovalRecord.*, Project.Name AS projectname, ApplicationUser.Name AS username ")
            sql.Append(" FROM ProjectApprovalRecord INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ProjectApprovalRecord.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" 	Project ON ProjectApprovalRecord.IdProject = Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" WHERE ProjectApprovalRecord.Id = " & idProjectApprovalRecord)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProjectApprovalRecord.id = data.Rows(0)("id")
                objProjectApprovalRecord.idproject = data.Rows(0)("idproject")
                objProjectApprovalRecord.code = data.Rows(0)("code")
                objProjectApprovalRecord.comments = data.Rows(0)("comments")
                objProjectApprovalRecord.attachment = data.Rows(0)("attachment")
                objProjectApprovalRecord.approvaldate = IIf(Not IsDBNull(data.Rows(0)("approvaldate")), data.Rows(0)("approvaldate"), Nothing)
                objProjectApprovalRecord.actnumber = data.Rows(0)("actnumber")
                objProjectApprovalRecord.approvedvalue = data.Rows(0)("approvedvalue")
                objProjectApprovalRecord.approved = data.Rows(0)("approved")
                'TODO:45 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                objProjectApprovalRecord.codeapprovedidea = data.Rows(0)("codeapprovedidea")
                'TODO:45 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                'cierre de cambio
                objProjectApprovalRecord.enabled = data.Rows(0)("enabled")
                objProjectApprovalRecord.iduser = data.Rows(0)("iduser")
                objProjectApprovalRecord.createdate = data.Rows(0)("createdate")
                objProjectApprovalRecord.PROJECTNAME = data.Rows(0)("projectname")
                objProjectApprovalRecord.USERNAME = data.Rows(0)("username")
                objProjectApprovalRecord.IdProcessInstance = data.Rows(0)("IdProcessInstance")
                objProjectApprovalRecord.IdActivityInstance = data.Rows(0)("IdActivityInstance")
                'TODO:46 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                objProjectApprovalRecord.Ididea = data.Rows(0)("Ididea")
                'TODO:46 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
                '12-06-2013 GERMAN RODRIGUEZ MGgroup
                'cierre de cambio


            End If

            ' retornar el objeto
            Return objProjectApprovalRecord

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProjectApprovalRecord = Nothing

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
    ''' <returns>un objeto de tipo List(Of ProjectApprovalRecordEntity)</returns>
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
        Optional ByVal Ididea As String = "") As List(Of ProjectApprovalRecordEntity)

        ' TODO: 47 CAMPOS NUEVOS PARA LA VALIDACION DE APROBACION IDEA
        ' 16-06-2013 GERMAN RODRIGUEZ MGgroup
        ' cierre de cambio


        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProjectApprovalRecord As ProjectApprovalRecordEntity
        Dim ProjectApprovalRecordList As New List(Of ProjectApprovalRecordEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ProjectApprovalRecord.*, idea.Name AS projectname , ApplicationUser.Name AS username   ")
            sql.Append(" FROM ProjectApprovalRecord INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ProjectApprovalRecord.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" idea ON  idea.Id = ProjectApprovalRecord.Ididea	 ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " idea.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approvaldate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ProjectApprovalRecord.approvaldate, 103) like '%" & approvaldate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not actnumber.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.actnumber like '%" & actnumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approvedvalue.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.approvedvalue like '%" & approvedvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not approved.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.approved = '" & approved & "'")
                where = " AND "

            End If

            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            ' verificar si hay entrada de datos para el campo
            If Not codeapprovedidea.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.codeapprovedidea = '" & codeapprovedidea & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not approvedtext.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.approved IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Junta Directiva' AS appro, 1 AS Value ")
                sql.Append(" UNION SELECT 'Dirección Ejecutiva' AS appro, 2 AS Value ")
                sql.Append(" UNION SELECT 'Direccion Ejecutiva' AS appro, 2 AS Value) Temp ")
                sql.Append(" WHERE appro LIKE '%" & approvedtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            'If Not createdate.Equals("") Then

            '    sql.Append(where & " CONVERT(NVARCHAR, ProjectApprovalRecord.createdate, 103) like '%" & createdate & "%'")
            '    where = " AND "

            'End If

            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            ' verificar si hay entrada de datos para el campo
            If Not Ididea.Equals("") Then

                sql.Append(where & " ProjectApprovalRecord.Ididea = '" & Ididea & "'")
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
                        sql.Append(" ORDER BY ProjectApprovalRecord.CreateDate desc ")
                End Select

            End If

            sql.Append(" ORDER BY ProjectApprovalRecord.CreateDate desc ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProjectApprovalRecord = New ProjectApprovalRecordEntity

                ' cargar el valor del campo
                objProjectApprovalRecord.id = row("id")
                objProjectApprovalRecord.idproject = row("idproject")
                objProjectApprovalRecord.code = row("code")
                objProjectApprovalRecord.comments = row("comments")
                objProjectApprovalRecord.attachment = row("attachment")
                objProjectApprovalRecord.approvaldate = IIf(Not IsDBNull(row("approvaldate")), row("approvaldate"), Nothing)
                objProjectApprovalRecord.actnumber = row("actnumber")
                objProjectApprovalRecord.approvedvalue = row("approvedvalue")
                objProjectApprovalRecord.approved = row("approved")
                ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
                objProjectApprovalRecord.codeapprovedidea = IIf(Not IsDBNull(row("codeapprovedidea")), row("codeapprovedidea"), Nothing)
                objProjectApprovalRecord.enabled = row("enabled")
                objProjectApprovalRecord.iduser = row("iduser")
                objProjectApprovalRecord.createdate = row("createdate")
                objProjectApprovalRecord.USERNAME = row("username")
                objProjectApprovalRecord.PROJECTNAME = row("projectname")
                objProjectApprovalRecord.IdProcessInstance = row("IdProcessInstance")
                objProjectApprovalRecord.IdActivityInstance = row("IdActivityInstance")
                ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
                objProjectApprovalRecord.Ididea = IIf(Not IsDBNull(row("Ididea")), row("Ididea"), Nothing)

                ' agregar a la lista
                ProjectApprovalRecordList.Add(objProjectApprovalRecord)

            Next

            ' retornar el objeto
            getList = ProjectApprovalRecordList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProjectApprovalRecord. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProjectApprovalRecord = Nothing
            ProjectApprovalRecordList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProjectApprovalRecord
    ''' </summary>
    ''' <param name="ProjectApprovalRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProjectApprovalRecord As ProjectApprovalRecordEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("Update ProjectApprovalRecord SET")
            sql.AppendLine(" idproject = '" & ProjectApprovalRecord.idproject & "',")
            sql.AppendLine(" code = '" & ProjectApprovalRecord.code & "',")
            sql.AppendLine(" comments = '" & ProjectApprovalRecord.comments & "',")
            sql.AppendLine(" attachment = '" & ProjectApprovalRecord.attachment & "',")
            If ProjectApprovalRecord.approvaldate <> defaultDate Then
                sql.AppendLine(" approvaldate = '" & ProjectApprovalRecord.approvaldate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            Else
                sql.AppendLine(" approvaldate = NULL,")
            End If
            sql.AppendLine(" actnumber = '" & ProjectApprovalRecord.actnumber & "',")
            sql.AppendLine(" approvedvalue = '" & ProjectApprovalRecord.approvedvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" approved = '" & ProjectApprovalRecord.approved & "',")
            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            sql.AppendLine(" codeapprovedidea = '" & ProjectApprovalRecord.codeapprovedidea & "',")
            sql.AppendLine(" enabled = '" & ProjectApprovalRecord.enabled & "',")
            sql.AppendLine(" iduser = '" & ProjectApprovalRecord.iduser & "',")
            sql.AppendLine(" createdate = '" & ProjectApprovalRecord.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine(" IdProcessInstance = '" & ProjectApprovalRecord.IdProcessInstance & "',")
            sql.AppendLine(" IdActivityInstance = '" & ProjectApprovalRecord.IdActivityInstance & "',")
            ' CAMPO NUEVO PARA VALIDAR APROBACION IDEA German Rodriguez MGgroup
            sql.AppendLine(" Ididea = '" & ProjectApprovalRecord.Ididea & "'")

            sql.AppendLine("WHERE id = " & ProjectApprovalRecord.id)

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
            Throw New Exception("Error al modificar el ProjectApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProjectApprovalRecord de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectApprovalRecord As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProjectApprovalRecord ")
            SQL.AppendLine(" where id = '" & idProjectApprovalRecord & "' ")

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
            Throw New Exception("Error al elimiar el ProjectApprovalRecord. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
