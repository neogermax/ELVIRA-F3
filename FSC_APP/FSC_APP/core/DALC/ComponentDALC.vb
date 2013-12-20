Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ComponentDALC

    ' contantes
    Const MODULENAME As String = "ComponentDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Component WHERE isLastVersion = 1 AND Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Component WHERE isLastVersion = 1 AND Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de ENTERPRISE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo Component
    ''' </summary>
    ''' <param name="Component"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Component As ComponentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Component(" & _
             "code," & _
             "name," & _
             "idobjective," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "idphase," & _
             "idKey," & _
             "isLastVersion" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Component.code & "',")
            sql.AppendLine("'" & Component.name & "',")
            sql.AppendLine("'" & Component.idobjective & "',")
            sql.AppendLine("'" & Component.enabled & "',")
            sql.AppendLine("'" & Component.iduser & "',")
            sql.AppendLine("'" & Component.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Component.idphase & "',")
            sql.AppendLine("'" & Component.idKey & "',")
            sql.AppendLine("'" & Component.isLastVersion & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            If Component.idKey = 0 Then

                ' limpiar el sql
                sql.Remove(0, sql.Length)

                ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
                sql.AppendLine("Update Component SET")
                sql.AppendLine(" idKey = '" & num & "',")
                sql.AppendLine(" isLastVersion = 1")
                sql.AppendLine("WHERE id = " & num)

                'Ejecutar la Instruccion
                GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            End If

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
            Throw New Exception("Error al insertar el Component. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Component por el Id
    ''' </summary>
    ''' <param name="idComponent"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponent As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ComponentEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objComponent As New ComponentEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT com.*, apu.Name as userName, pro.Name AS projectName, obj.Name AS objectiveName ")
            sql.Append(" FROM Component AS com INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON com.IdUser = apu.ID INNER JOIN ")
            sql.Append(" Objective AS obj ON com.IdObjective = obj.idkey and  obj.IsLastVersion='1' INNER JOIN ")
            sql.Append(" Project AS pro ON obj.IdProject = pro.idkey and pro.IsLastVersion='1' ")

            'Se verifica si se desea consultar la última versión del objetivo requerido.
            If (consultLastVersion) Then
                sql.Append(" WHERE com.IsLastVersion='1' and com.idkey = " & idComponent)
            Else
                sql.Append(" WHERE com.id = " & idComponent)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objComponent.id = data.Rows(0)("id")
                objComponent.code = data.Rows(0)("code")
                objComponent.name = data.Rows(0)("name")
                objComponent.idobjective = data.Rows(0)("idobjective")
                objComponent.enabled = data.Rows(0)("enabled")
                objComponent.iduser = data.Rows(0)("iduser")
                objComponent.createdate = data.Rows(0)("createdate")
                objComponent.USERNAME = data.Rows(0)("userName")
                objComponent.PROJECTNAME = data.Rows(0)("projectName")
                objComponent.OBJECTIVENAME = data.Rows(0)("objectiveName")
                objComponent.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objComponent.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objComponent

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Component. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objComponent = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="projectname"></param>
    ''' <param name="idobjective"></param>
    ''' <param name="objectivename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ComponentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ComponentEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objComponent As ComponentEntity
        Dim ComponentList As New List(Of ComponentEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT com.*, apu.Name as userName, pro.Name AS projectName, obj.Name AS objectiveName ")
            sql.Append(" FROM Component AS com INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON com.IdUser = apu.ID INNER JOIN ")
            sql.Append(" Objective AS obj ON com.IdObjective = obj.idkey and  obj.IsLastVersion='1' INNER JOIN ")
            sql.Append(" Project AS pro ON obj.IdProject = pro.idkey and pro.IsLastVersion='1' ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " com.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " com.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " com.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " com.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " com.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " obj.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " pro.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idobjective.Equals("") Then

                sql.Append(where & "  com.idobjective= '" & idobjective & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not objectivename.Equals("") Then

                sql.Append(where & " obj.Name like '%" & objectivename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " com.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " com.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " com.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, com.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idKey.Equals("") Then

                sql.Append(where & "  com.idKey = '" & idKey & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   com.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "projectname"
                        sql.Append(" ORDER BY pro.Name ")
                    Case "objectivename"
                        sql.Append(" ORDER BY obj.Name ")
                    Case "idproject"
                        sql.Append(" ORDER BY pro.IdProject ")
                    Case Else
                        sql.Append(" ORDER BY com." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objComponent = New ComponentEntity

                ' cargar el valor del campo
                objComponent.id = row("id")
                objComponent.code = row("code")
                objComponent.name = row("name")
                objComponent.idobjective = row("idobjective")
                objComponent.enabled = row("enabled")
                objComponent.iduser = row("iduser")
                objComponent.createdate = row("createdate")
                objComponent.USERNAME = row("userName")
                objComponent.PROJECTNAME = row("projectName")
                objComponent.OBJECTIVENAME = row("objectiveName")
                objComponent.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                objComponent.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                ComponentList.Add(objComponent)

            Next

            ' retornar el objeto
            getList = ComponentList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Component. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objComponent = Nothing
            ComponentList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Component
    ''' </summary>
    ''' <param name="Component"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Component As ComponentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            sql.AppendLine("Update Component SET")
            sql.AppendLine(" isLastVersion = 0")
            sql.AppendLine("WHERE id = " & Component.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            add(objApplicationCredentials, Component)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Component. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Component de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idComponent As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Component ")
            SQL.AppendLine(" where id = '" & idComponent & "' ")
            SQL.AppendLine("    OR idKey = '" & idKey & "' ")

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
            Throw New Exception("Error al elimiar el Component. " & ex.Message)


        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <remarks></remarks>
    Public Function PhaseProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjective As Integer) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objObjective As New ObjectiveEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT DISTINCT Project.IdPhase ")
            sql.Append(" FROM Component INNER JOIN ")
            sql.Append("  Objective ON Component.IdObjective = Objective.idkey and Objective.islastversion='1' INNER JOIN ")
            sql.Append("  Project ON Objective.IdProject = Project.idkey and Project.islastversion='1' ")
            sql.Append(" where Objective.idkey= " & idObjective & " and Component.islastversion='1' ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)



            ' retornar el objeto
            Return CLng(data.Rows(0)("IdPhase"))

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "VersionProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar version del proyecto para un componente. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objObjective = Nothing

        End Try

    End Function

End Class
