Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SubActivityInformationRegistryDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo SubActivityInformationRegistry
    ''' </summary>
    ''' <param name="SubActivityInformationRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal SubActivityInformationRegistry As SubActivityInformationRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO SubActivityInformationRegistry(" & _
             "idsubactivity," & _
             "description," & _
             "begindate," & _
             "enddate," & _
             "comments," & _
             "attachment," & _
             "Observation," & _
             "Indicator," & _
             "iduser," & _
             "createdate" & _
                     ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SubActivityInformationRegistry.idsubactivity & "',")
            sql.AppendLine("'" & SubActivityInformationRegistry.description & "',")
            If SubActivityInformationRegistry.begindate = defaultDate Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & SubActivityInformationRegistry.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            If SubActivityInformationRegistry.enddate = defaultDate Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & SubActivityInformationRegistry.enddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine("'" & SubActivityInformationRegistry.comments & "',")
            sql.AppendLine("'" & SubActivityInformationRegistry.attachment & "',")
            sql.AppendLine("'" & SubActivityInformationRegistry.observation & "',")
            If SubActivityInformationRegistry.indicator.Trim() = "" Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & SubActivityInformationRegistry.indicator & "',")
            End If
            sql.AppendLine("'" & SubActivityInformationRegistry.iduser & "',")
            sql.AppendLine("'" & SubActivityInformationRegistry.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el SubActivityInformationRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un SubActivityInformationRegistry por el Id
    ''' </summary>
    ''' <param name="idSubActivityInformationRegistry"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSubActivityInformationRegistry As Integer) As SubActivityInformationRegistryEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSubActivityInformationRegistry As New SubActivityInformationRegistryEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SubActivityInformationRegistry.Id, SubActivityInformationRegistry.IdSubActivity, SubActivityInformationRegistry.Description, ")
            sql.Append(" 	SubActivityInformationRegistry.BeginDate, SubActivityInformationRegistry.EndDate, SubActivityInformationRegistry.Comments, ")
            sql.Append(" 	SubActivityInformationRegistry.Attachment, SubActivityInformationRegistry.IdUser, SubActivityInformationRegistry.CreateDate, ")
            sql.Append(" 	SubActivityInformationRegistry.Observation, SubActivityInformationRegistry.Indicator, ")
            sql.Append(" (SELECT act.Name  ")
            sql.Append(" FROM " & dbBPMName & ".dbo.ProcessInstance as p1 INNER JOIN  ")
            sql.Append("   " & dbBPMName & ".dbo.ActivityInstance as act1 ON p1.ID = act1.IDProcessInstance INNER JOIN ")
            sql.Append("  " & dbBPMName & ".dbo.Activity as act ON act1.IDActivity = act.ID  ")
            sql.Append(" WHERE     (p1.EntryData = 'SubActivityInformationRegistryEntity') AND p1.IDEntryData=SubActivityInformationRegistry.Id AND (act1.ID =  ")
            sql.Append(" (SELECT     Max(p.ID) FROM  " & dbBPMName & ".dbo.ActivityInstance AS p ")
            sql.Append("  WHERE      p.IdProcessInstance = act1.IdProcessInstance))) as State,  ")
            sql.Append(" 	 SubActivity.Name AS subactivityname, ApplicationUser.Name AS username ")
            sql.Append(" FROM SubActivityInformationRegistry INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivityInformationRegistry.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" 	SubActivity ON SubActivityInformationRegistry.IdSubActivity = SubActivity.idkey and SubActivity.IsLastVersion='1' ")
            sql.Append(" WHERE SubActivityInformationRegistry.Id = " & idSubActivityInformationRegistry)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objSubActivityInformationRegistry.id = data.Rows(0)("id")
				objSubActivityInformationRegistry.idsubactivity = data.Rows(0)("idsubactivity")
				objSubActivityInformationRegistry.description = data.Rows(0)("description")
                objSubActivityInformationRegistry.begindate = IIf(IsDBNull(data.Rows(0)("begindate")), Nothing, data.Rows(0)("begindate"))
                objSubActivityInformationRegistry.enddate = IIf(IsDBNull(data.Rows(0)("enddate")), Nothing, data.Rows(0)("enddate"))
				objSubActivityInformationRegistry.comments = data.Rows(0)("comments")
                objSubActivityInformationRegistry.attachment = IIf(IsDBNull(data.Rows(0)("attachment")), "", data.Rows(0)("attachment"))
                objSubActivityInformationRegistry.observation = IIf(IsDBNull(data.Rows(0)("Observation")), "", data.Rows(0)("Observation"))
                objSubActivityInformationRegistry.indicator = IIf(IsDBNull(data.Rows(0)("Indicator")), "", data.Rows(0)("Indicator"))
				objSubActivityInformationRegistry.iduser = data.Rows(0)("iduser")
				objSubActivityInformationRegistry.createdate = data.Rows(0)("createdate")
                objSubActivityInformationRegistry.state = data.Rows(0)("State")
                objSubActivityInformationRegistry.USERNAME = data.Rows(0)("username")
                objSubActivityInformationRegistry.SUBACTIVITYNAME = data.Rows(0)("subactivityname")

            End If

            ' retornar el objeto
            Return objSubActivityInformationRegistry

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSubActivityInformationRegistry = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idsubactivity"></param>
    ''' <param name="subactivityname"></param>
    ''' <param name="description"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="comments"></param>
    ''' <param name="attachment"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <returns>un objeto de tipo List(Of SubActivityInformationRegistryEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idsubactivity As String = "", _
        Optional ByVal subactivityname As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal idstate As String = "", _
        Optional ByVal state As String = "", _
        Optional ByVal order As String = "") As List(Of SubActivityInformationRegistryEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSubActivityInformationRegistry As SubActivityInformationRegistryEntity
        Dim SubActivityInformationRegistryList As New List(Of SubActivityInformationRegistryEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SubActivityInformationRegistry.Id, SubActivityInformationRegistry.IdSubActivity, SubActivityInformationRegistry.Description, ")
            sql.Append(" 	SubActivityInformationRegistry.BeginDate, SubActivityInformationRegistry.EndDate, SubActivityInformationRegistry.Comments, ")
            sql.Append(" 	SubActivityInformationRegistry.Attachment, SubActivityInformationRegistry.IdUser, SubActivityInformationRegistry.CreateDate, ")
            sql.Append(" 	SubActivityInformationRegistry.Observation, SubActivityInformationRegistry.Indicator, ")
            sql.Append(" (SELECT act.Name  ")
            sql.Append(" FROM " & dbBPMName & ".dbo.ProcessInstance as p1 INNER JOIN  ")
            sql.Append("  " & dbBPMName & ".dbo.ActivityInstance as act1 ON p1.ID = act1.IDProcessInstance INNER JOIN ")
            sql.Append("  " & dbBPMName & ".dbo.Activity as act ON act1.IDActivity = act.ID  ")
            sql.Append(" WHERE     (p1.EntryData = 'SubActivityInformationRegistryEntity') AND p1.IDEntryData=SubActivityInformationRegistry.Id AND (act1.ID =  ")
            sql.Append(" (SELECT     Max(p.ID) FROM " & dbBPMName & ".dbo.ActivityInstance AS p ")
            sql.Append("  WHERE      p.IdProcessInstance = act1.IdProcessInstance))) as State,  ")
            sql.Append(" 	 SubActivity.Name AS subactivityname, ApplicationUser.Name AS username ")
            sql.Append(" FROM SubActivityInformationRegistry INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivityInformationRegistry.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.Append(" 	SubActivity ON SubActivityInformationRegistry.IdSubActivity = SubActivity.idkey and SubActivity.IsLastVersion='1' ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idsubactivity.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.idsubactivity = '" & idsubactivity & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not subactivityname.Equals("") Then

                sql.Append(where & " SubActivity.Name like '%" & subactivityname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not begindate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivityInformationRegistry.begindate, 103) like '%" & begindate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivityInformationRegistry.enddate, 103) like '%" & enddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivityInformationRegistry.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not state.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.state IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Pendiente' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'Vencida' AS Estate, 2 AS Value ")
                sql.Append(" UNION SELECT 'Cumplida' AS Estate, 3 AS Value ")
                sql.Append(" UNION SELECT 'Cancelada' AS Estate, 4 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & state & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idstate.Equals("") Then

                sql.Append(where & " SubActivityInformationRegistry.state = '" & idstate & "'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "useraname"
                        sql.Append(" ORDER BY ApplicationUser.Name")
                    Case "subactivityname"
                        sql.Append(" ORDER BY SubActivity.Name")
                    Case Else
                        sql.Append(" ORDER BY SubActivityInformationRegistry." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSubActivityInformationRegistry = New SubActivityInformationRegistryEntity

                ' cargar el valor del campo
                objSubActivityInformationRegistry.id = row("id")
                objSubActivityInformationRegistry.idsubactivity = row("idsubactivity")
                objSubActivityInformationRegistry.description = row("description")
                objSubActivityInformationRegistry.begindate = IIf(IsDBNull(row("begindate")), Nothing, row("begindate"))
                objSubActivityInformationRegistry.enddate = IIf(IsDBNull(row("enddate")), Nothing, row("enddate"))
                objSubActivityInformationRegistry.comments = row("comments")
                objSubActivityInformationRegistry.attachment = row("attachment")
                objSubActivityInformationRegistry.observation = row("Observation")
                objSubActivityInformationRegistry.indicator = row("Indicator")
                objSubActivityInformationRegistry.iduser = row("iduser")
                objSubActivityInformationRegistry.createdate = row("createdate")
                objSubActivityInformationRegistry.state = IIf(IsDBNull(row("state")), Nothing, row("state"))
                objSubActivityInformationRegistry.USERNAME = row("username")
                objSubActivityInformationRegistry.SUBACTIVITYNAME = row("subactivityname")

                ' agregar a la lista
                SubActivityInformationRegistryList.Add(objSubActivityInformationRegistry)

            Next

            ' retornar el objeto
            getList = SubActivityInformationRegistryList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SubActivityInformationRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSubActivityInformationRegistry = Nothing
            SubActivityInformationRegistryList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo SubActivityInformationRegistry
    ''' </summary>
    ''' <param name="SubActivityInformationRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal SubActivityInformationRegistry As SubActivityInformationRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("Update SubActivityInformationRegistry SET")
            SQL.AppendLine(" idsubactivity = '" & SubActivityInformationRegistry.idsubactivity & "',")           
            sql.AppendLine(" description = '" & SubActivityInformationRegistry.description & "',")
            If SubActivityInformationRegistry.begindate = defaultDate Then
                sql.AppendLine(" begindate = NULL,")
            Else
                sql.AppendLine(" begindate = '" & SubActivityInformationRegistry.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            If SubActivityInformationRegistry.enddate = defaultDate Then
                sql.AppendLine(" enddate = NULL,")
            Else
                sql.AppendLine(" enddate = '" & SubActivityInformationRegistry.enddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine(" comments = '" & SubActivityInformationRegistry.comments & "',")
            sql.AppendLine(" attachment = '" & SubActivityInformationRegistry.attachment & "',")
            sql.AppendLine(" Observation = '" & SubActivityInformationRegistry.observation & "',")
            sql.AppendLine(" Indicator = '" & SubActivityInformationRegistry.indicator & "',")
            sql.AppendLine(" iduser = '" & SubActivityInformationRegistry.iduser & "',")
            sql.AppendLine(" createdate = '" & SubActivityInformationRegistry.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            ' sql.AppendLine(" state = '" & SubActivityInformationRegistry.state & "'")
            sql.AppendLine("WHERE id = " & SubActivityInformationRegistry.id)

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
            Throw New Exception("Error al modificar el SubActivityInformationRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el SubActivityInformationRegistry de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSubActivityInformationRegistry As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from SubActivityInformationRegistry ")
            SQL.AppendLine(" where id = '" & idSubActivityInformationRegistry & "' ")

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
            Throw New Exception("Error al elimiar el SubActivityInformationRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
