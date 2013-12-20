Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class MitigationDALC

    ' contantes
    Const MODULENAME As String = "MitigationDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Mitigation WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Mitigation WHERE  Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de Mitigation. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo Mitigation
    ''' </summary>
    ''' <param name="Mitigation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Mitigation As MitigationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Mitigation(" & _
             "code," & _
             "name," & _
             "description," & _
             "impactonrisk," & _
             "idresponsible," & _
             "enabled," & _
             "iduser," & _
             "createdate)")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Mitigation.code & "',")
            sql.AppendLine("'" & Mitigation.name & "',")
            sql.AppendLine("'" & Mitigation.description & "',")
            sql.AppendLine("'" & Mitigation.impactonrisk & "',")
            sql.AppendLine("'" & Mitigation.idresponsible & "',")
            sql.AppendLine("'" & Mitigation.enabled & "',")
            sql.AppendLine("'" & Mitigation.iduser & "',")
            sql.AppendLine("'" & Mitigation.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")
        
            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            'If Mitigation.idKey = 0 Then

            '    ' limpiar el sql
            '    sql.Remove(0, sql.Length)

            '    ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            '    'sql.AppendLine("Update Mitigation SET")
            '    'sql.AppendLine(" idKey = '" & num & "',")
            '    'sql.AppendLine(" isLastVersion = 1")
            '    'sql.AppendLine("WHERE id = " & num)

            '    'Ejecutar la Instruccion
            '    GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'End If

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
            Throw New Exception("Error al insertar el Mitigation. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Mitigation por el Id
    ''' </summary>
    ''' <param name="idMitigation"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigation As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As MitigationEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objMitigation As New MitigationEntity
        Dim objMitigationByRiskDALC As New MitigationByRiskDALC
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT mit.*, apu.Name AS userName, resp.Name AS responsibleName ")
            sql.Append(" FROM Mitigation AS mit INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON mit.IdUser = apu.ID INNER JOIN ")
            'sql.Append(" Risk AS ris ON mit.IdRisk = ris.idkey and ris.IsLastVersion='1' INNER JOIN ")
            sql.Append("  " & dbSecurityName & ".dbo.ApplicationUser AS resp ON mit.IdResponsible = resp.ID  ")

            'Se verifica si se desea consultar la última versión de la mitigación requerida.
            If (consultLastVersion) Then
                sql.Append(" WHERE mit.id= " & idMitigation)
            Else
                sql.Append(" WHERE mit.id = " & idMitigation)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objMitigation.id = data.Rows(0)("id")
                objMitigation.code = data.Rows(0)("code")
                objMitigation.name = data.Rows(0)("name")
                objMitigation.description = data.Rows(0)("description")
                objMitigation.impactonrisk = data.Rows(0)("impactonrisk")
                objMitigation.idresponsible = data.Rows(0)("idresponsible")
                objMitigation.enabled = data.Rows(0)("enabled")
                objMitigation.iduser = data.Rows(0)("iduser")
                objMitigation.createdate = data.Rows(0)("createdate")
                objMitigation.USERNAME = data.Rows(0)("userName")
                objMitigation.RESPONSIBLENAME = data.Rows(0)("responsibleName")
                'objMitigation.RISKNAME = data.Rows(0)("riskName")
                'objMitigation.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objMitigation.risklist = objMitigationByRiskDALC.getList(objApplicationCredentials, idmitigation:=objMitigation.id, order:="riskname")
                'objMitigation.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objMitigation

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Mitigation. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objMitigation = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="idrisk"></param>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <param name="impactonrisk"></param>
    ''' <param name="idresponsable"></param>
    '''  <param name="responsablename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="iduser"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of MitigationEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal idrisk As String = "", _
        Optional ByVal riskname As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal impactonrisk As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "", _
        Optional ByVal idproject As String = "") As List(Of MitigationEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objMitigation As MitigationEntity
        Dim MitigationList As New List(Of MitigationEntity)
        Dim objMitigationByRiskDALC As New MitigationByRiskDALC
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT mit.*, apu.Name AS userName, resp.Name AS responsibleName")
            sql.Append(" FROM  Mitigation as mit INNER JOIN ")
            'sql.Append("   Mitigation as mit INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON mit.IdUser = apu.ID INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser as resp ON mit.IdResponsible= resp.ID  ")
            'sql.Append(" Risk AS ris ON mit.IdRisk = ris.idkey and ris.IsLastVersion='1' INNER JOIN ")
            'sql.Append("  ComponentByRisk ON ris.idkey = ComponentByRisk.IdRisk and ris.islastversion='1' ON Component.idkey = ComponentByRisk.IdComponent and Component.islastversion='1' ")



            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " mit.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " mit.id like '%" & idlike & "%'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " mit.code like '%" & code & "%'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not riskname.Equals("") Then

                sql.Append(where & " ris.Name like '%" & riskname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " mit.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " mit.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not impactonrisk.Equals("") Then

                sql.Append(where & " mit.impactonrisk IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Alto' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'Medio' AS Estate, 2 AS Value")
                sql.Append(" UNION  SELECT 'Bajo' AS Estate, 3 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & impactonrisk & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idresponsible.Equals("") Then

                sql.Append(where & " mit.IdResponsible = '" & idresponsible & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not responsiblename.Equals("") Then

                sql.Append(where & " resp.Name like '%" & responsiblename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " mit.enabled ='" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " mit.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " mit.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, mit.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idKey.Equals("") Then

                sql.Append(where & "  mit.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            'If Not isLastVersion.Equals("") Then

            '    sql.Append(where & "   mit.isLastVersion = '" & isLastVersion & "'")
            '    where = " AND "

            'End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " Objective.idProject = '" & idproject & "'")
                where = " AND "

            End If


            If Not order.Equals(String.Empty) Then

                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "riskname"
                        sql.Append(" ORDER BY ris.riskname")
                    Case "responsiblename"
                        sql.Append(" ORDER BY resp.Name ")
                    Case Else
                        sql.Append(" ORDER BY mit." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objMitigation = New MitigationEntity

                ' cargar el valor del campo
                objMitigation.id = row("id")
                objMitigation.code = row("code")
                objMitigation.risklist = objMitigationByRiskDALC.getList(objApplicationCredentials, id:=objMitigation.id, order:="riskname")
                objMitigation.name = row("name")
                objMitigation.description = row("description")
                objMitigation.impactonrisk = row("impactonrisk")
                objMitigation.idresponsible = row("idresponsible")
                objMitigation.enabled = row("enabled")
                objMitigation.iduser = row("iduser")
                objMitigation.createdate = row("createdate")
                objMitigation.USERNAME = row("userName")
                objMitigation.RESPONSIBLENAME = row("responsibleName")
                'objMitigation.RISKNAME = row("riskName")
                'objMitigation.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                'objMitigation.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                MitigationList.Add(objMitigation)

            Next

            ' retornar el objeto
            getList = MitigationList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Mitigation. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objMitigation = Nothing
            MitigationList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Mitigation
    ''' </summary>
    ''' <param name="Mitigation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Mitigation As MitigationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            'sql.AppendLine("Update Mitigation SET")
            'sql.AppendLine(" isLastVersion = 0")
            'sql.AppendLine("WHERE id = " & Mitigation.id)

            sql.AppendLine("Update Mitigation Set")
            sql.AppendLine(" code = '" & Mitigation.code & "',")
            sql.AppendLine(" name = '" & Mitigation.name & "',")
            sql.AppendLine(" impactonrisk = '" & Mitigation.impactonrisk & "',")
            sql.AppendLine(" idresponsible = '" & Mitigation.idresponsible & "',")
            sql.AppendLine(" enabled = '" & Mitigation.enabled & "',")
            sql.AppendLine(" iduser = '" & Mitigation.iduser & "',")
            sql.AppendLine(" createdate = '" & Mitigation.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "' ")
            sql.AppendLine("WHERE id = " & Mitigation.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            'add(objApplicationCredentials, Mitigation)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Mitigation. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Mitigation de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigation As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Mitigation ")
            SQL.AppendLine(" where id = '" & idMitigation & "' ")
            'SQL.AppendLine("    OR idKey = '" & idKey & "' ")

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
            Throw New Exception("Error al elimiar el Mitigation. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Consulta La version de un Proyecto
    ''' </summary>
    ''' <param name="idRisk"></param>
    ''' <remarks></remarks>
    Public Function PhaseProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idRisk As Integer) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objObjective As New ObjectiveEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT Distinct Project.IdPhase ")
            sql.Append(" FROM Project INNER JOIN ")
            sql.Append("    Objective ON Project.idkey = Objective.IdProject and Project.islastversion='1' INNER JOIN ")
            sql.Append("  Component ON Objective.idkey = Component.IdObjective and Objective.islastversion='1' INNER JOIN ")
            sql.Append(" ComponentByRisk ON Component.idkey = ComponentByRisk.IdComponent and Component.islastversion='1' INNER JOIN ")
            sql.Append(" Risk ON ComponentByRisk.IdRisk = Risk.id ")
            sql.Append(" WHERE     (Risk.id =" & idRisk & " ) ")

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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "PhaseProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la fase del proyecto para un riesgo. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objObjective = Nothing

        End Try

    End Function



End Class
