Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class InquestDALC

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Inquest WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Inquest WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Inquest
    ''' </summary>
    ''' <param name="Inquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Inquest As InquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Inquest(" & _
             "code," & _
             "name," & _
             "idproject," & _
             "projectphase," & _
             "idusergroup," & _
             "enabled," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Inquest.code & "',")
            sql.AppendLine("'" & Inquest.name & "',")
            sql.AppendLine("'" & Inquest.idproject & "',")
            sql.AppendLine("'" & Inquest.projectphase & "',")
            sql.AppendLine("'" & Inquest.idusergroup & "',")
            sql.AppendLine("'" & Inquest.enabled & "',")
            sql.AppendLine("'" & Inquest.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar la encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Inquest por el Id
    ''' </summary>
    ''' <param name="idInquest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquest As Integer) As InquestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objInquest As New InquestEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Inquest.*, Project.Name AS ProjectName, UserGroup.Name AS UserGroupName")
            sql.Append(" FROM Inquest ")
            sql.Append(" INNER JOIN Project ON Inquest.IdProject = Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.UserGroup UserGroup ON Inquest.IdUserGroup = UserGroup.Id ")
            sql.Append(" WHERE Inquest.Id = " & idInquest)


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objInquest.id = data.Rows(0)("id")
                objInquest.code = data.Rows(0)("code")
                objInquest.name = data.Rows(0)("name")
                objInquest.idproject = data.Rows(0)("idproject")
                objInquest.projectphase = data.Rows(0)("projectphase")
                objInquest.idusergroup = data.Rows(0)("idusergroup")
                objInquest.enabled = data.Rows(0)("enabled")
                objInquest.createdate = data.Rows(0)("createdate")
                objInquest.PROJECTNAME = data.Rows(0)("ProjectName")
                objInquest.USERGROUPNAME = data.Rows(0)("UserGroupName")

            End If

            ' retornar el objeto
            Return objInquest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la encuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objInquest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="projectphase"></param>
    ''' <param name="idusergroup"></param>
    ''' <param name="usergroupname"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of InquestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal projectphase As String = "", _
        Optional ByVal idusergroup As String = "", _
        Optional ByVal usergroupname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of InquestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objInquest As InquestEntity
        Dim InquestList As New List(Of InquestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try

            ' construir la sentencia
            sql.Append(" SELECT Inquest.*, Project.Name AS ProjectName, UserGroup.Name AS UserGroupName")
            sql.Append(" FROM Inquest ")
            sql.Append(" INNER JOIN Project ON Inquest.IdProject = Project.idkey and Project.IsLastVersion='1' ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.UserGroup UserGroup ON Inquest.IdUserGroup = UserGroup.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Inquest.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Inquest.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Inquest.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Inquest.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " Inquest.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectphase.Equals("") Then

                sql.Append(where & " Inquest.projectphase like '%" & projectphase & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idusergroup.Equals("") Then

                sql.Append(where & " Inquest.idusergroup = '" & idusergroup & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not usergroupname.Equals("") Then

                sql.Append(where & " UserGroup.Name like '%" & usergroupname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Inquest.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Inquest.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Inquest.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                'Ordernar
                Select Case order
                    Case "projectname"
                        sql.Append(" ORDER BY Project.Name ")
                    Case "usergroupname"
                        sql.Append(" ORDER BY UserGroup.Name ")
                    Case Else
                        sql.Append(" ORDER BY Inquest." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objInquest = New InquestEntity

                ' cargar el valor del campo
                objInquest.id = row("id")
                objInquest.code = row("code")
                objInquest.name = row("name")
                objInquest.idproject = row("idproject")
                objInquest.projectphase = row("projectphase")
                objInquest.idusergroup = row("idusergroup")
                objInquest.enabled = row("enabled")
                objInquest.createdate = row("createdate")
                objInquest.PROJECTNAME = row("ProjectName")
                objInquest.USERGROUPNAME = row("UserGroupName")

                ' agregar a la lista
                InquestList.Add(objInquest)

            Next

            ' retornar el objeto
            getList = InquestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de encuestas.")

        Finally
            ' liberando recursos
            sql = Nothing
            objInquest = Nothing
            InquestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Inquest
    ''' </summary>
    ''' <param name="Inquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Inquest As InquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Inquest SET")
            sql.AppendLine(" code = '" & Inquest.code & "',")
            sql.AppendLine(" name = '" & Inquest.name & "',")
            sql.AppendLine(" idproject = '" & Inquest.idproject & "',")
            sql.AppendLine(" projectphase = '" & Inquest.projectphase & "',")
            sql.AppendLine(" idusergroup = '" & Inquest.idusergroup & "',")
            sql.AppendLine(" enabled = '" & Inquest.enabled & "',")
            sql.AppendLine(" createdate = '" & Inquest.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE Inquest.id = " & Inquest.id)

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
            Throw New Exception("Error al modificar la encuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Inquest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idInquest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Inquest ")
            SQL.AppendLine(" where id = '" & idInquest & "' ")

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
            Throw New Exception("Error al elimiar la encuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite cargar la lista de proyectos existentes en el sistema
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">ordenamiento</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal enabled As String, ByVal order As String) As List(Of ProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable

        Try

            ' construir la sentencia
            sql.Append("SELECT Id, idkey, Name, Code")
            sql.Append(" FROM Project  where IsLastVersion='1' ")
            If (enabled.Length > 0) Then sql.Append(" And Project.Enabled = " & enabled)
            sql.Append(" ORDER BY Project." & order & "")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity()

                ' cargar el valor del campo
                objProject.id = row("id")
                objProject.idKey = row("idkey")
                objProject.name = row("name")
                objProject.code = row("code")
                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getProjectList = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de proyectos.")

        Finally
            ' liberando recursos
            sql = Nothing
            objProject = Nothing
            ProjectList = Nothing
            data = Nothing

        End Try

    End Function

End Class
