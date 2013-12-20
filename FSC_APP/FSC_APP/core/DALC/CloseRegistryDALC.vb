Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class CloseRegistryDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo CloseRegistry
    ''' </summary>
    ''' <param name="CloseRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal CloseRegistry As CloseRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO CloseRegistry(" & _
             "idproject," & _
             "closingdate," & _
             "weakness," & _
             "opportunity," & _
             "strengths," & _
             "learningfornewprojects," & _
             "goodpractice," & _
             "registrationdate," & _
             "enabled," & _
             "iduser" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & CloseRegistry.idproject & "',")
            If CloseRegistry.closingdate = defaultDate Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & CloseRegistry.closingdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine("'" & CloseRegistry.weakness & "',")
            sql.AppendLine("'" & CloseRegistry.opportunity & "',")
            sql.AppendLine("'" & CloseRegistry.strengths & "',")
            sql.AppendLine("'" & CloseRegistry.learningfornewprojects & "',")
            sql.AppendLine("'" & CloseRegistry.goodpractice & "',")
            If CloseRegistry.registrationdate = defaultDate Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & CloseRegistry.registrationdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine("'" & CloseRegistry.enabled & "',")
            sql.AppendLine("'" & CloseRegistry.iduser & "')")

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
            Throw New Exception("Error al insertar el CloseRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un CloseRegistry por el Id
    ''' </summary>
    ''' <param name="idCloseRegistry"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idCloseRegistry As Integer) As CloseRegistryEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objCloseRegistry As New CloseRegistryEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT CloseRegistry.Id, CloseRegistry.IdProject, CloseRegistry.ClosingDate, CloseRegistry.Weakness, CloseRegistry.Opportunity, ")
            sql.Append(" 	CloseRegistry.Strengths, CloseRegistry.LearningForNewProjects, CloseRegistry.GoodPractice, CloseRegistry.RegistrationDate, ")
            sql.Append(" 	CloseRegistry.Enabled, CloseRegistry.IdUser, Project.Name AS projectname, ApplicationUser.Name AS username ")
            sql.Append(" FROM CloseRegistry INNER JOIN ")
            sql.Append(" 	Project ON CloseRegistry.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON CloseRegistry.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE CloseRegistry.Id = " & idCloseRegistry)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objCloseRegistry.id = data.Rows(0)("id")
				objCloseRegistry.idproject = data.Rows(0)("idproject")
                objCloseRegistry.closingdate = IIf(Not IsDBNull(data.Rows(0)("closingdate")), data.Rows(0)("closingdate"), Nothing)
				objCloseRegistry.weakness = data.Rows(0)("weakness")
				objCloseRegistry.opportunity = data.Rows(0)("opportunity")
				objCloseRegistry.strengths = data.Rows(0)("strengths")
				objCloseRegistry.learningfornewprojects = data.Rows(0)("learningfornewprojects")
				objCloseRegistry.goodpractice = data.Rows(0)("goodpractice")
                objCloseRegistry.registrationdate = IIf(Not IsDBNull(data.Rows(0)("registrationdate")), data.Rows(0)("registrationdate"), Nothing)
				objCloseRegistry.enabled = data.Rows(0)("enabled")
                objCloseRegistry.iduser = data.Rows(0)("iduser")
                objCloseRegistry.USERNAME = data.Rows(0)("username")
                objCloseRegistry.PROJECTNAME = data.Rows(0)("projectname")

            End If

            ' retornar el objeto
            Return objCloseRegistry

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un CloseRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objCloseRegistry = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales</param>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="closingdate"></param>
    ''' <param name="weakness"></param>
    ''' <param name="opportunity"></param>
    ''' <param name="strengths"></param>
    ''' <param name="learningfornewprojects"></param>
    ''' <param name="goodpractice"></param>
    ''' <param name="goodpracticetext"></param>
    ''' <param name="registrationdate"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="order"></param>
    ''' <returns>un objeto de tipo List(Of CloseRegistryEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal closingdate As String = "", _
        Optional ByVal weakness As String = "", _
        Optional ByVal opportunity As String = "", _
        Optional ByVal strengths As String = "", _
        Optional ByVal learningfornewprojects As String = "", _
        Optional ByVal goodpractice As String = "", _
        Optional ByVal goodpracticetext As String = "", _
        Optional ByVal registrationdate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal order As String = "") As List(Of CloseRegistryEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objCloseRegistry As CloseRegistryEntity
        Dim CloseRegistryList As New List(Of CloseRegistryEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT CloseRegistry.Id, CloseRegistry.IdProject, CloseRegistry.ClosingDate, CloseRegistry.Weakness, CloseRegistry.Opportunity, ")
            sql.Append(" 	CloseRegistry.Strengths, CloseRegistry.LearningForNewProjects, CloseRegistry.GoodPractice, CloseRegistry.RegistrationDate, ")
            sql.Append(" 	CloseRegistry.Enabled, CloseRegistry.IdUser, Project.Name AS projectname, ApplicationUser.Name AS username ")
            sql.Append(" FROM CloseRegistry INNER JOIN ")
            sql.Append(" 	Project ON CloseRegistry.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON CloseRegistry.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " CloseRegistry.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " CloseRegistry.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " CloseRegistry.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not closingdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, CloseRegistry.closingdate, 103) like '%" & closingdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not weakness.Equals("") Then

                sql.Append(where & " CloseRegistry.weakness like '%" & weakness & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not opportunity.Equals("") Then

                sql.Append(where & " CloseRegistry.opportunity like '%" & opportunity & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not strengths.Equals("") Then

                sql.Append(where & " CloseRegistry.strengths like '%" & strengths & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not learningfornewprojects.Equals("") Then

                sql.Append(where & " CloseRegistry.learningfornewprojects like '%" & learningfornewprojects & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not goodpractice.Equals("") Then

                sql.Append(where & " CloseRegistry.goodpractice = '" & goodpractice & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not goodpracticetext.Equals("") Then

                sql.Append(where & " CloseRegistry.GoodPractice IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Si' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'No' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & goodpracticetext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not registrationdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, CloseRegistry.registrationdate, 103) like '%" & registrationdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " CloseRegistry.enabled like '%" & enabled & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " CloseRegistry.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " CloseRegistry.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
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
                        sql.Append(" ORDER BY CloseRegistry." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objCloseRegistry = New CloseRegistryEntity

                ' cargar el valor del campo
                objCloseRegistry.id = row("id")
                objCloseRegistry.idproject = row("idproject")
                objCloseRegistry.closingdate = IIf(Not IsDBNull(row("closingdate")), row("closingdate"), Nothing)
                objCloseRegistry.weakness = row("weakness")
                objCloseRegistry.opportunity = row("opportunity")
                objCloseRegistry.strengths = row("strengths")
                objCloseRegistry.learningfornewprojects = row("learningfornewprojects")
                objCloseRegistry.goodpractice = row("goodpractice")
                objCloseRegistry.registrationdate = IIf(Not IsDBNull(row("registrationdate")), row("registrationdate"), Nothing)
                objCloseRegistry.enabled = row("enabled")
                objCloseRegistry.iduser = row("iduser")
                objCloseRegistry.USERNAME = row("username")
                objCloseRegistry.PROJECTNAME = row("projectname")
                ' agregar a la lista
                CloseRegistryList.Add(objCloseRegistry)

            Next

            ' retornar el objeto
            getList = CloseRegistryList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de CloseRegistry. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objCloseRegistry = Nothing
            CloseRegistryList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo CloseRegistry
    ''' </summary>
    ''' <param name="CloseRegistry"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal CloseRegistry As CloseRegistryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("Update CloseRegistry SET")
            sql.AppendLine(" idproject = '" & CloseRegistry.idproject & "',")
            If CloseRegistry.closingdate = defaultDate Then
                sql.AppendLine(" closingdate = NULL, ")
            Else
                sql.AppendLine(" closingdate = '" & CloseRegistry.closingdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine(" weakness = '" & CloseRegistry.weakness & "',")
            sql.AppendLine(" opportunity = '" & CloseRegistry.opportunity & "',")
            sql.AppendLine(" strengths = '" & CloseRegistry.strengths & "',")
            sql.AppendLine(" learningfornewprojects = '" & CloseRegistry.learningfornewprojects & "',")
            sql.AppendLine(" goodpractice = '" & CloseRegistry.goodpractice & "',")
            If CloseRegistry.registrationdate = defaultDate Then
                sql.AppendLine(" registrationdate = NULL, ")
            Else
                sql.AppendLine(" registrationdate = '" & CloseRegistry.registrationdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine(" enabled = '" & CloseRegistry.enabled & "',")
            sql.AppendLine(" iduser = '" & CloseRegistry.iduser & "'")
            sql.AppendLine("WHERE id = " & CloseRegistry.id)

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
            Throw New Exception("Error al modificar el CloseRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el CloseRegistry de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idCloseRegistry As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from CloseRegistry ")
            SQL.AppendLine(" where id = '" & idCloseRegistry & "' ")

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
            Throw New Exception("Error al elimiar el CloseRegistry. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
