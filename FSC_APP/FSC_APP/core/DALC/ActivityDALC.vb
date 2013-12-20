Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ActivityDALC

    ' contantes
    Const MODULENAME As String = "ActivityDALC"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyNumber(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal number As String, _
                                Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(Number) AS cont FROM Activity WHERE isLastVersion = 1 AND Number = '" & number & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Number) AS cont FROM Activity WHERE isLastVersion = 1 AND Number = '" & number & "' AND id <> '" & id & "'")

            End If

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyNumber = False

                Else

                    ' retornar que existe
                    verifyNumber = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Activity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo Activity
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Activity As ActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Activity(" & _
             "number," & _
             "title," & _
             "idcomponent," & _
             "description," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "idPhase," & _
             "idKey," & _
             "isLastVersion" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Activity.number & "',")
            sql.AppendLine("'" & Activity.title & "',")
            sql.AppendLine("'" & Activity.idcomponent & "',")
            sql.AppendLine("'" & Activity.description & "',")
            sql.AppendLine("'" & Activity.enabled & "',")
            sql.AppendLine("'" & Activity.iduser & "',")
            sql.AppendLine("'" & Activity.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Activity.idphase & "',")
            sql.AppendLine("'" & Activity.idKey & "',")
            sql.AppendLine("'" & Activity.isLastVersion & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            If Activity.idKey = 0 Then

                ' limpiar el sql
                sql.Remove(0, sql.Length)

                ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
                sql.AppendLine("Update Activity SET")
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
            Throw New Exception("Error al insertar el Activity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Activity por el Id
    ''' </summary>
    ''' <param name="idActivity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ActivityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objActivity As New ActivityEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("SELECT act.*, obj.IdProject, com.IdObjective, com.Name as componentName, pro.Name AS projectName, ")
            sql.Append("obj.Name AS objectiveName,  apu.Name AS userName, ")
            sql.Append(" ISNULL((SELECT TOP(1) CriticalPath FROM SubActivity WHERE act.IsLastVersion='1' and  act.idkey = SubActivity.IdActivity), 0)  as CriticalPath ")
            sql.Append(" FROM Activity AS act INNER JOIN ")
            sql.Append("Component AS com ON act.IdComponent = com.idkey and com.IsLastVersion='1' INNER JOIN ")
            sql.Append("Objective AS obj ON com.IdObjective = obj.idkey and  obj.IsLastVersion='1' INNER JOIN ")
            sql.Append("Project AS pro ON obj.IdProject =pro.idkey and pro.IsLastVersion='1' INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON act.IdUser = apu.ID ")

            'Se verifica si se desea consultar la última versión de la actividad requerida.
            If (consultLastVersion) Then
                sql.Append(" WHERE act.IsLastVersion='1' and act.idkey = " & idActivity)
            Else
                sql.Append(" WHERE act.id = " & idActivity)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objActivity.id = data.Rows(0)("id")
                objActivity.number = data.Rows(0)("number")
                objActivity.title = data.Rows(0)("title")
                objActivity.idcomponent = data.Rows(0)("idcomponent")
                objActivity.description = data.Rows(0)("description")
                objActivity.enabled = data.Rows(0)("enabled")
                objActivity.iduser = data.Rows(0)("iduser")
                objActivity.createdate = data.Rows(0)("createdate")
                objActivity.USERNAME = data.Rows(0)("userName")
                objActivity.COMPONENTNAME = data.Rows(0)("componentName")
                objActivity.idobjective = data.Rows(0)("idobjective")
                objActivity.OBJECTIVENAME = data.Rows(0)("objectiveName")
                objActivity.idproject = data.Rows(0)("idproject")
                objActivity.PROJECTNAME = data.Rows(0)("projectName")
                objActivity.CRITICALPATH = data.Rows(0)("criticalpath")
                objActivity.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objActivity.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objActivity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Activity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="number"></param>
    ''' <param name="title"></param>
    ''' <param name="idcomponent"></param>
    ''' <param name="componentname"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="idobjective"></param>
    ''' <param name="objectivename"></param>
    ''' <param name="description"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="criticalpathtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="order"></param>
    ''' <returns>un objeto de tipo List(Of ActivityEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal number As String = "", _
        Optional ByVal title As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idobjective As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal criticalpathtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ActivityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objActivity As ActivityEntity
        Dim ActivityList As New List(Of ActivityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("SELECT act.*, obj.IdProject, com.IdObjective, com.Name as componentName, pro.Name AS projectName, ")
            sql.Append("obj.Name AS objectiveName,  apu.Name AS userName, ")
            sql.Append(" ISNULL((SELECT TOP(1) CriticalPath FROM SubActivity WHERE act.IsLastVersion='1' and act.idkey = SubActivity.IdActivity), 0)  as CriticalPath ")
            sql.Append(" FROM Activity AS act INNER JOIN ")
            sql.Append("Component AS com ON act.IdComponent = com.idkey and com.IsLastVersion='1' INNER JOIN ")
            sql.Append("Objective AS obj ON com.IdObjective = obj.idkey and  obj.IsLastVersion='1' INNER JOIN ")
            sql.Append("Project AS pro ON obj.IdProject =  pro.idkey and pro.IsLastVersion='1' INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON act.IdUser = apu.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " act.id ='" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " act.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not number.Equals("") Then

                sql.Append(where & " act.number like '%" & number & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not title.Equals("") Then

                sql.Append(where & " act.title like '%" & title & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcomponent.Equals("") Then

                sql.Append(where & " act.idcomponent ='" & idcomponent & "'")
                where = " AND "

            End If
            ' verificar si hay entrada de datos para el campo
            If Not componentname.Equals("") Then

                sql.Append(where & " com.Name like '%" & componentname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " act.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " act.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " act.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not criticalpathtext.Equals("") Then

                sql.Append(where & " ISNULL((SELECT TOP(1) CriticalPath FROM SubActivity WHERE act.IsLastVersion='1' and act.idkey = SubActivity.IdActivity), 0) IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'SI' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'NO' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & criticalpathtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " act.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, act.createdate, 103) like '%" & createdate & "%'")
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
            If Not idKey.Equals("") Then

                sql.Append(where & "  act.idKey = '" & idKey & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   act.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "projectname"
                        sql.Append(" ORDER BY pro.Name ")
                    Case "objectivename"
                        sql.Append(" ORDER BY obj.Name ")
                    Case "componentname"
                        sql.Append(" ORDER BY com.Name")
                    Case "criticalpath"
                        sql.Append(" ORDER BY criticalpath")
                    Case Else
                        sql.Append(" ORDER BY act." & order)
                End Select


            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objActivity = New ActivityEntity

                ' cargar el valor del campo
                objActivity.id = row("id")
                objActivity.number = row("number")
                objActivity.title = row("title")
                objActivity.idcomponent = row("idcomponent")
                objActivity.description = row("description")
                objActivity.enabled = row("enabled")
                objActivity.iduser = row("iduser")
                objActivity.createdate = row("createdate")
                objActivity.USERNAME = row("userName")
                objActivity.COMPONENTNAME = row("componentName")
                objActivity.idobjective = row("idobjective")
                objActivity.OBJECTIVENAME = row("objectiveName")
                objActivity.idproject = row("idproject")
                objActivity.PROJECTNAME = row("projectName")
                objActivity.CRITICALPATH = row("criticalpath")
                objActivity.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                objActivity.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                ActivityList.Add(objActivity)

            Next

            ' retornar el objeto
            getList = ActivityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Activity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objActivity = Nothing
            ActivityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Activity
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Activity As ActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            sql.AppendLine("Update Activity SET")
            sql.AppendLine(" isLastVersion = 0")
            sql.AppendLine("WHERE id = " & Activity.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            add(objApplicationCredentials, Activity)

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
            Throw New Exception("Error al modificar el Activity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Activity de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idActivity As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Activity ")
            SQL.AppendLine(" where id = '" & idActivity & "' ")
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
            Throw New Exception("Error al elimiar el Activity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
