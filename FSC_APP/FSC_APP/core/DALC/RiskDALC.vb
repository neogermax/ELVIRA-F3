Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class RiskDALC

    ' contantes
    Const MODULENAME As String = "RiskDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Risk WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Risk WHERE  Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Risk
    ''' </summary>
    ''' <param name="Risk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Risk As RiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Risk(" & _
             "code," & _
             "name," & _
             "description," & _
             "whatcanhappen," & _
             "riskimpact," & _
             "ocurrenceprobability," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
                       ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Risk.code & "',")
            sql.AppendLine("'" & Risk.name & "',")
            sql.AppendLine("'" & Risk.description & "',")
            sql.AppendLine("'" & Risk.whatcanhappen & "',")
            sql.AppendLine("'" & Risk.riskimpact & "',")
            sql.AppendLine("'" & Risk.ocurrenceprobability & "',")
            sql.AppendLine("'" & Risk.enabled & "',")
            sql.AppendLine("'" & Risk.iduser & "',")
            sql.AppendLine("'" & Risk.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")
            'sql.AppendLine("'" & Risk.idphase & "',")
            'sql.AppendLine("'" & Risk.idKey & "',")
            'sql.AppendLine("'" & Risk.isLastVersion & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            'If Risk.idKey = 0 Then

            '    ' limpiar el sql
            '    sql.Remove(0, sql.Length)

            '    ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            '    sql.AppendLine("Update Risk SET")
            '    sql.AppendLine(" idKey = '" & num & "',")
            '    sql.AppendLine(" isLastVersion = 1")
            '    sql.AppendLine("WHERE id = " & num)

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
            Throw New Exception("Error al insertar el Risk. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Risk por el Id
    ''' </summary>
    ''' <param name="idRisk"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idRisk As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As RiskEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objRisk As New RiskEntity
        Dim objComponentByRiskDALC As New ComponentByRiskDALC
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Risk.Id, Risk.Code, Risk.Name, Risk.Description, Risk.WhatCanHappen, " & _
                       "        Risk.RiskImpact, Risk.OcurrenceProbability, Risk.Enabled, Risk.IdUser, " & _
                       "        Risk.CreateDate, ApplicationUser.Name AS username" & _
                       " FROM Risk INNER JOIN " & _
                       " " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Risk.IdUser = ApplicationUser.ID ")

            'Se verifica si se desea consultar la última versión del riesgo requerido.
            If (consultLastVersion) Then
                sql.Append(" WHERE Risk.id = " & idRisk)
            Else
                sql.Append(" WHERE Risk.id = " & idRisk)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objRisk.id = data.Rows(0)("id")
                'objRisk.idKey = data.Rows(0)("idkey")
                objRisk.code = data.Rows(0)("code")
                objRisk.name = data.Rows(0)("name")
                objRisk.description = data.Rows(0)("description")
                objRisk.whatcanhappen = data.Rows(0)("whatcanhappen")
                objRisk.riskimpact = data.Rows(0)("riskimpact")
                objRisk.ocurrenceprobability = data.Rows(0)("ocurrenceprobability")
                objRisk.enabled = data.Rows(0)("enabled")
                objRisk.iduser = data.Rows(0)("iduser")
                objRisk.createdate = data.Rows(0)("createdate")
                objRisk.USERNAME = data.Rows(0)("userName")
                objRisk.componentlist = objComponentByRiskDALC.getList(objApplicationCredentials, idrisk:=objRisk.id, order:="riskname")
                'objRisk.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                'objRisk.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objRisk

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Risk. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objRisk = Nothing
            objComponentByRiskDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="whatcanhappen"></param>
    ''' <param name="riskimpact"></param>
    ''' <param name="ocurrenceprobability"></param>
    ''' <param name="enabled"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="idcomponent"></param>
    ''' <param name="componentname"></param>
    ''' <returns>un objeto de tipo List(Of RiskEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal whatcanhappen As String = "", _
        Optional ByVal riskimpact As String = "", _
        Optional ByVal ocurrenceprobability As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of RiskEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objRisk As RiskEntity
        Dim RiskList As New List(Of RiskEntity)
        Dim objComponentByRiskDALC As New ComponentByRiskDALC
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Risk.Id, Risk.Code, Risk.Name, Risk.Description, Risk.WhatCanHappen, " & _
                       "        Risk.RiskImpact, Risk.OcurrenceProbability, Risk.Enabled, Risk.IdUser, " & _
                       "        Risk.CreateDate, ApplicationUser.Name AS username " & _
                       " FROM Risk INNER JOIN " & _
                       " " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Risk.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Risk.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Risk.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Risk.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Risk.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " Risk.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not whatcanhappen.Equals("") Then

                sql.Append(where & " Risk.whatcanhappen like '%" & whatcanhappen & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not riskimpact.Equals("") Then

                sql.Append(where & " Risk.riskimpact IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Alto' AS Impact, 1 AS Value ")
                sql.Append(" UNION SELECT 'Medio' AS Impact, 2 AS Value ")
                sql.Append(" UNION SELECT 'Bajo' AS Impact, 3 AS Value) Temp ")
                sql.Append(" WHERE Impact LIKE '%" & riskimpact & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ocurrenceprobability.Equals("") Then

                sql.Append(where & " Risk.ocurrenceprobability like '%" & ocurrenceprobability & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Risk.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Risk.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Risk.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(VARCHAR, Risk.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcomponent.Equals("") Then

                sql.Append(where & " (  Risk.id IN " & _
                                   "    (SELECT ComponentByRisk.IdRisk " & _
                                   "        FROM ComponentByRisk INNER JOIN " & _
                                   "            Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1' " & _
                                   "     WHERE (Component.Id = '%" & idcomponent & "%' and  Component.IsLastVersion='1')))")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " (  Risk.id IN " & _
                                   "    (SELECT ComponentByRisk.IdRisk " & _
                                   "        FROM ComponentByRisk INNER JOIN " & _
                                   "            Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1'  INNER JOIN " & _
                                   "             Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' " & _
                                   "     WHERE (Objective.IdProject = '" & idproject & "')))")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not componentname.Equals("") Then

                sql.Append(where & " ( Risk.id IN " & _
                                   "    (SELECT ComponentByRisk.IdRisk " & _
                                   "        FROM ComponentByRisk INNER JOIN " & _
                                   "            Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1' " & _
                                   "     WHERE (Component.Name LIKE '%" & componentname & "%')))")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idKey.Equals("") Then

                sql.Append(where & "  Risk.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            'If Not isLastVersion.Equals("") Then

            '    sql.Append(where & "   Risk.isLastVersion = '" & isLastVersion & "'")
            '    where = " AND "

            'End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case Else
                        sql.Append(" ORDER BY Risk." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objRisk = New RiskEntity

                ' cargar el valor del campo
                objRisk.id = row("id")
                objRisk.code = row("code")
                objRisk.name = row("name")
                objRisk.description = row("description")
                objRisk.whatcanhappen = row("whatcanhappen")
                objRisk.riskimpact = row("riskimpact")
                objRisk.ocurrenceprobability = row("ocurrenceprobability")
                objRisk.enabled = row("enabled")
                objRisk.iduser = row("iduser")
                objRisk.createdate = row("createdate")
                objRisk.USERNAME = row("userName")
                objRisk.componentlist = objComponentByRiskDALC.getList(objApplicationCredentials, idrisk:=objRisk.id, order:="riskname")
                'objRisk.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                'objRisk.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                RiskList.Add(objRisk)

            Next

            ' retornar el objeto
            getList = RiskList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Risk. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objRisk = Nothing
            RiskList = Nothing
            objComponentByRiskDALC = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Risk
    ''' </summary>
    ''' <param name="Risk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Risk As RiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            'sql.AppendLine("Update Risk SET")
            'sql.AppendLine(" isLastVersion = 0")
            'sql.AppendLine("WHERE id = " & Risk.id)
            sql.AppendLine("Update Risk SET")
            sql.AppendLine(" code ='" & Risk.code & "',")
            sql.AppendLine(" name ='" & Risk.name & "',")
            sql.AppendLine(" Description ='" & Risk.description & "',")
            sql.AppendLine(" whatcanhappen ='" & Risk.whatcanhappen & "',")
            sql.AppendLine(" riskimpact ='" & Risk.riskimpact & "',")
            sql.AppendLine(" ocurrenceprobability ='" & Risk.ocurrenceprobability & "',")
            sql.AppendLine(" enabled ='" & Risk.enabled & "',")
            sql.AppendLine(" iduser ='" & Risk.iduser & "',")
            sql.AppendLine("createdate ='" & Risk.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & Risk.id)


            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            'add(objApplicationCredentials, Risk)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Risk. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Risk de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal idRisk As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Risk ")
            SQL.AppendLine(" where id = '" & idRisk & "' ")
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
            Throw New Exception("Error al elimiar el Risk. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
