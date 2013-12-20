Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ObjectiveDALC

    ' contantes
    Const MODULENAME As String = "ObjectiveDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Objective WHERE isLastVersion = 1 AND Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Objective WHERE isLastVersion = 1 AND Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Objective
    ''' </summary>
    ''' <param name="Objective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Objective As ObjectiveEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Objective(" & _
             "code," & _
             "name," & _
             "idproject," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "IdPhase," & _
             "idKey," & _
             "isLastVersion" & _
                ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Objective.code & "',")
            sql.AppendLine("'" & Objective.name & "',")
            sql.AppendLine("'" & Objective.idproject & "',")
            sql.AppendLine("'" & Objective.enabled & "',")
            sql.AppendLine("'" & Objective.iduser & "',")
            sql.AppendLine("'" & Objective.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Objective.idphase & "',")
            sql.AppendLine("'" & Objective.idKey & "',")
            sql.AppendLine("'" & Objective.isLastVersion & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            If Objective.idKey = 0 Then

                ' limpiar el sql
                sql.Remove(0, sql.Length)

                ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
                sql.AppendLine("Update Objective SET")
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
            Throw New Exception("Error al insertar el Objective. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Objective por el Id
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idObjective As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As ObjectiveEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objObjective As New ObjectiveEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Objective.*, ApplicationUser.Name AS userName, Project.Name AS projectName ")
            sql.Append(" FROM Objective ")
            sql.Append("    INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Objective.IdUser = ApplicationUser.ID ")
            sql.Append("    INNER JOIN Project ON Objective.IdProject = Project.idkey and Project.IsLastVersion='1' ")

            'Se verifica si se desea consultar la última versión del objetivo requerido.
            If (consultLastVersion) Then
                sql.Append(" WHERE Objective.IsLastVersion='1' And Objective.idkey = " & idObjective)
            Else
                sql.Append(" WHERE Objective.id = " & idObjective)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objObjective.id = data.Rows(0)("id")
                objObjective.code = data.Rows(0)("code")
                objObjective.name = data.Rows(0)("name")
                objObjective.idproject = data.Rows(0)("idproject")
                objObjective.enabled = data.Rows(0)("enabled")
                objObjective.iduser = data.Rows(0)("iduser")
                objObjective.createdate = data.Rows(0)("createdate")
                objObjective.USERNAME = data.Rows(0)("userName")
                objObjective.PROJECTNAME = data.Rows(0)("projectName")
                objObjective.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objObjective.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objObjective

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Objective. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objObjective = Nothing

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
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ObjectiveEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ObjectiveEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objObjective As ObjectiveEntity
        Dim ObjectiveList As New List(Of ObjectiveEntity)
        Dim data As DataTable
        Dim where As String = " WHERE"
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Objective.*, ApplicationUser.Name AS userName, Project.Name AS projectName ")
            sql.Append(" FROM Objective")
            sql.Append("    INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Objective.IdUser = ApplicationUser.ID ")
            sql.Append("    INNER JOIN Project ON Objective.IdProject = Project.idkey and Project.IsLastVersion='1' ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Objective.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & "   Objective.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & "   Objective.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & "   Objective.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & "  Objective.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & "   Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & "  Objective.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idKey.Equals("") Then

                sql.Append(where & "  Objective.idKey = '" & idKey & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   Objective.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & "   Objective.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")

                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & "   Objective.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & "   ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & "  CONVERT(NVARCHAR, Objective.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case "projectname"
                        sql.Append(" ORDER BY Project.Name ")
                    Case Else
                        sql.Append(" ORDER BY Objective." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objObjective = New ObjectiveEntity

                ' cargar el valor del campo
                objObjective.id = row("id")
                objObjective.code = row("code")
                objObjective.name = row("name")
                objObjective.idproject = row("idproject")
                objObjective.enabled = row("enabled")
                objObjective.iduser = row("iduser")
                objObjective.createdate = row("createdate")
                objObjective.USERNAME = row("userName")
                objObjective.PROJECTNAME = row("projectName")
                objObjective.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                objObjective.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                ObjectiveList.Add(objObjective)

            Next

            ' retornar el objeto
            getList = ObjectiveList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Objective. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objObjective = Nothing
            ObjectiveList = Nothing
            data = Nothing


        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Objective
    ''' </summary>
    ''' <param name="Objective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Objective As ObjectiveEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            sql.AppendLine("Update Objective SET")
            sql.AppendLine(" isLastVersion = 0")
            sql.AppendLine("WHERE id = " & Objective.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            add(objApplicationCredentials, Objective)

            ' limpiar el sql
            'sql.Remove(0, sql.Length)

            '' construir la sentencia
            'sql.AppendLine("Update Objective SET")
            'sql.AppendLine(" code = '" & Objective.code & "',")
            'sql.AppendLine(" name = '" & Objective.name & "',")
            'sql.AppendLine(" idproject = '" & Objective.idproject & "',")
            'sql.AppendLine(" enabled = '" & Objective.enabled & "',")
            'sql.AppendLine(" iduser = '" & Objective.iduser & "',")
            'sql.AppendLine(" createdate = '" & Objective.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            'sql.AppendLine(" idKey = '" & Objective.idKey & "',")
            'sql.AppendLine(" isLastVersion = '" & Objective.isLastVersion & "'")
            'sql.AppendLine("WHERE id = " & Objective.id)

            'Ejecutar la Instruccion
            'GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Objective. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Objective de una forma
    ''' </summary>
    ''' <param name="idObjective"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal idObjective As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" DELETE FROM Objective ")
            SQL.AppendLine(" WHERE id = '" & idObjective & "' ")
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
            Throw New Exception("Error al elimiar el Objective. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idproject"></param>
    ''' <remarks></remarks>
    Public Function VersionProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idproject As Integer) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objObjective As New ObjectiveEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT DISTINCT Project.IdPhase ")
            sql.Append(" FROM Project ")
            sql.Append(" WHERE Project.idkey= " & idproject & " and Project.islastversion='1' ")

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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Objective. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objObjective = Nothing

        End Try

    End Function


End Class
