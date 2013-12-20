Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class IndicatorDALC

    ' contantes
    Const MODULENAME As String = "IndicatorDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Indicator WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Indicator WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Indicator
    ''' </summary>
    ''' <param name="Indicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal Indicator As IndicatorEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Indicator(" & _
             "LevelIndicator," & _
             "identities," & _
             "code," & _
             "description," & _
             "type," & _
             "goal," & _
             "greenvalue," & _
             "yellowvalue," & _
             "redvalue," & _
             "assumptions," & _
             "sourceverification," & _
             "enabled," & _
             "iduser," & _
             "idResponsable," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Indicator.levelindicator & "',")
            sql.AppendLine("'" & Indicator.identity & "',")
            sql.AppendLine("'" & Indicator.code & "',")
            sql.AppendLine("'" & Indicator.description & "',")
            sql.AppendLine("'" & Indicator.type & "',")
            sql.AppendLine("'" & Indicator.goal & "',")
            sql.AppendLine("'" & Indicator.greenvalue & "',")
            sql.AppendLine("'" & Indicator.yellowvalue & "',")
            sql.AppendLine("'" & Indicator.redvalue & "',")
            sql.AppendLine("'" & Indicator.assumptions & "',")
            sql.AppendLine("'" & Indicator.sourceverification & "',")
            sql.AppendLine("'" & Indicator.enabled & "',")
            sql.AppendLine("'" & Indicator.iduser & "',")
            sql.AppendLine("'" & Indicator.idresponsable & "',")
            sql.AppendLine("'" & Indicator.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Indicator. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un Indicator por el Id
    ''' </summary>
    ''' <param name="idIndicator"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idIndicator As Integer) As IndicatorEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objIndicator As New IndicatorEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Indicator.Id, Indicator.LevelIndicator, Indicator.IdEntities, " & _
                       "        Indicator.Code, Indicator.Description, Indicator.Type, " & _
                       "        Indicator.Goal, Indicator.GreenValue, Indicator.YellowValue, " & _
                       "        Indicator.RedValue, Indicator.Assumptions, Indicator.SourceVerification, Indicator.IdResponsable, " & _
                       "        Indicator.Enabled, Indicator.IdUser, Indicator.CreateDate, " & _
                       "        CASE WHEN Indicator.LEVELINDICATOR = '1.1' THEN " & _
                       "                 (SELECT StrategicLine.NAME FROM StrategicLine WHERE Indicator.IdEntities = StrategicLine.Id)  " & _
                       "             WHEN Indicator.LEVELINDICATOR = '1.2' THEN " & _
                       "                (SELECT Strategy.NAME FROM Strategy WHERE Indicator.IdEntities = Strategy.Id) " & _
                       "             WHEN Indicator.LEVELINDICATOR = '2' THEN " & _
                       "                (SELECT Program.NAME FROM Program WHERE Indicator.IdEntities = Program.Id) " & _
                       "             WHEN Indicator.LEVELINDICATOR = '3' THEN " & _
                       "                (SELECT Project.NAME FROM Project WHERE Indicator.IdEntities = Project.idkey and Project.IsLastVersion='1') " & _
                       "             ELSE 'No disponible' " & _
                       "        END AS entityname, ApplicationUser.Name AS username, ApplicationUser1.Name as ResponsableName " & _
                       " FROM Indicator INNER JOIN " & _
                       "        " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Indicator.IdUser = ApplicationUser.ID LEFT JOIN" & _
                       "        " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser1 ON Indicator.IdResponsable = ApplicationUser1.ID ")
            sql.Append(" WHERE Indicator.Id = " & idIndicator)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
                objIndicator.id = data.Rows(0)("id")
                objIndicator.levelindicator = data.Rows(0)("LevelIndicator")
                objIndicator.identity = data.Rows(0)("identities")
				objIndicator.code = data.Rows(0)("code")
				objIndicator.description = data.Rows(0)("description")
				objIndicator.type = data.Rows(0)("type")
				objIndicator.goal = data.Rows(0)("goal")
				objIndicator.greenvalue = data.Rows(0)("greenvalue")
				objIndicator.yellowvalue = data.Rows(0)("yellowvalue")
				objIndicator.redvalue = data.Rows(0)("redvalue")
				objIndicator.assumptions = data.Rows(0)("assumptions")
				objIndicator.sourceverification = data.Rows(0)("sourceverification")
				objIndicator.enabled = data.Rows(0)("enabled")
				objIndicator.iduser = data.Rows(0)("iduser")
                objIndicator.createdate = data.Rows(0)("createdate")
                objIndicator.USERNAME = data.Rows(0)("userName")
                objIndicator.ENTITYNAME = IIf(Not IsDBNull(data.Rows(0)("entityname")), data.Rows(0)("entityname"), 0)
                objIndicator.idresponsable = IIf(Not IsDBNull(data.Rows(0)("idResponsable")), data.Rows(0)("idResponsable"), 0)
                objIndicator.RESPONSABLENAME = IIf(Not IsDBNull(data.Rows(0)("ResponsableName")), data.Rows(0)("ResponsableName"), "")
            End If
            

            ' retornar el objeto
            Return objIndicator

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Indicator. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objIndicator = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="description"></param>
    ''' <param name="type"></param>
    ''' <param name="goal"></param>
    ''' <param name="greenvalue"></param>
    ''' <param name="yellowvalue"></param>
    ''' <param name="redvalue"></param>
    ''' <param name="assumptions"></param>
    ''' <param name="sourceverification"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="entityname"></param>
    ''' <param name="levelname"></param>
    ''' <param name="levelindicator"></param>
    ''' <returns>un objeto de tipo List(Of IndicatorEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal goal As String = "", _
        Optional ByVal greenvalue As String = "", _
        Optional ByVal yellowvalue As String = "", _
        Optional ByVal redvalue As String = "", _
        Optional ByVal assumptions As String = "", _
        Optional ByVal sourceverification As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal entityname As String = "", _
        Optional ByVal levelname As String = "", _
        Optional ByVal levelindicator As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIndicator As IndicatorEntity
        Dim IndicatorList As New List(Of IndicatorEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Indicator.Id, Indicator.LevelIndicator, Indicator.IdEntities, " & _
                       "        Indicator.Code, Indicator.Description, Indicator.Type, " & _
                       "        Indicator.Goal, Indicator.GreenValue, Indicator.YellowValue, " & _
                       "        Indicator.RedValue, Indicator.Assumptions, Indicator.SourceVerification, " & _
                       "        Indicator.Enabled, Indicator.IdUser, Indicator.IdResponsable,  ApplicationUser1.Name as ResponsableName,  Indicator.CreateDate, " & _
                       "        CASE WHEN Indicator.LEVELINDICATOR = '1.1' THEN " & _
                       "                (SELECT StrategicLine.NAME FROM StrategicLine WHERE Indicator.IdEntities = StrategicLine.Id)  " & _
                       "             WHEN Indicator.LEVELINDICATOR = '1.2' THEN " & _
                       "                (SELECT Strategy.NAME FROM Strategy WHERE Indicator.IdEntities = Strategy.Id) " & _
                       "             WHEN Indicator.LEVELINDICATOR = '2' THEN " & _
                       "                (SELECT Program.NAME FROM Program WHERE Indicator.IdEntities = Program.Id) " & _
                       "             WHEN Indicator.LEVELINDICATOR = '3' THEN " & _
                       "                (SELECT max(Project.NAME) FROM Project WHERE Indicator.IdEntities = Project.idkey and Project.IsLastVersion='1') " & _
                       "             ELSE 'No disponible' " & _
                       "        END AS entityname, ApplicationUser.Name AS username " & _
                       " FROM Indicator INNER JOIN " & _
                       "        " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Indicator.IdUser = ApplicationUser.ID LEFT JOIN" & _
                       "        " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser1 ON Indicator.IdResponsable = ApplicationUser1.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Indicator.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Indicator.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Indicator.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " Indicator.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not type.Equals("") Then

                sql.Append(where & " Indicator.type IN ")
                sql.Append(" (SELECT Value FROM ( ")
                sql.Append(" SELECT 'Beneficiarios' AS IndType, 1 AS Value ")
                sql.Append(" UNION SELECT 'Capacidad instalada' AS IndType, 2 AS Value ")
                sql.Append(" UNION SELECT 'Gestión del conocimiento' AS IndType, 3 AS Value) Temp ")
                sql.Append(" WHERE IndType LIKE '%" & type & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not goal.Equals("") Then

                sql.Append(where & " Indicator.goal like '%" & goal & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not greenvalue.Equals("") Then

                sql.Append(where & " Indicator.greenvalue like '%" & greenvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not yellowvalue.Equals("") Then

                sql.Append(where & " Indicator.yellowvalue like '%" & yellowvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not redvalue.Equals("") Then

                sql.Append(where & " Indicator.redvalue like '%" & redvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not assumptions.Equals("") Then

                sql.Append(where & " Indicator.assumptions like '%" & assumptions & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not sourceverification.Equals("") Then

                sql.Append(where & " Indicator.sourceverification like '%" & sourceverification & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Indicator.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Indicator.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Indicator.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Indicator.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not levelname.Equals(String.Empty) Then
                sql.Append(where & " Indicator.levelindicator IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'Primer Nivel - Linea Estrategica' AS levName, '1.1' AS Value ")
                sql.Append(" UNION SELECT 'Primer Nivel - Estrategia' AS levName, '1.2' AS Value ")
                sql.Append(" UNION SELECT 'Segundo Nivel - Programa' AS levName, '2' AS Value ")
                sql.Append(" UNION SELECT 'Tercer Nivel - Proyecto' AS levName, '3' AS Value) Temp ")
                sql.Append(" WHERE levName LIKE '%" & levelname & "%') ")
                where = " AND "
            End If

            If Not entityname.Equals(String.Empty) Then
                sql.Append(where)
                sql.Append("        CASE WHEN Indicator.LEVELINDICATOR = '1.1' THEN " & _
                           "                (SELECT StrategicLine.NAME FROM StrategicLine WHERE Indicator.IdEntities = StrategicLine.Id)  " & _
                           "             WHEN Indicator.LEVELINDICATOR = '1.1' THEN " & _
                            "                (SELECT Strategy.NAME FROM Strategy WHERE Indicator.IdEntities = Strategy.Id) " & _
                           "             WHEN Indicator.LEVELINDICATOR = '2' THEN " & _
                           "                (SELECT Program.NAME FROM Program WHERE Indicator.IdEntities = Program.Id) " & _
                           "             WHEN Indicator.LEVELINDICATOR = '3' THEN " & _
                           "                (SELECT Project.NAME FROM Project WHERE Indicator.IdEntities = Project.idkey and Project.IsLastVersion='1') " & _
                           "             ELSE 'No disponible' " & _
                           "        END LIKE '%" & entityname & "%' ")
            End If

            If Not levelindicator.Equals(String.Empty) Then
                sql.Append(where & " Indicator.levelindicator = '" & levelindicator & "'")
                where = " AND "
            End If

            'If Not order.Equals(String.Empty) Then

            '    ' ordernar
            '    Select Case order
            '        Case "username"
            '            sql.Append(" ORDER BY username ")
            '        Case "entityname"
            '            sql.Append(" ORDER BY entityname ")
            '        Case Else
            '            sql.Append(" ORDER BY Indicator." & order)
            '    End Select

            'End If

            sql.Append(" ORDER BY createdate DESC")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objIndicator = New IndicatorEntity

                ' cargar el valor del campo
                objIndicator.id = row("id")
                objIndicator.levelindicator = row("LevelIndicator")
                objIndicator.identity = row("identities")
                objIndicator.code = row("code")
                objIndicator.description = row("description")
                objIndicator.type = row("type")
                objIndicator.goal = row("goal")
                objIndicator.greenvalue = row("greenvalue")
                objIndicator.yellowvalue = row("yellowvalue")
                objIndicator.redvalue = row("redvalue")
                objIndicator.assumptions = IIf(Not IsDBNull(row("assumptions")), row("assumptions"), "")
                objIndicator.sourceverification = IIf(Not IsDBNull(row("sourceverification")), row("sourceverification"), "")
                objIndicator.enabled = row("enabled")
                objIndicator.iduser = row("iduser")
                objIndicator.createdate = row("createdate")
                objIndicator.USERNAME = row("userName")
                objIndicator.idresponsable = IIf(Not IsDBNull(row("idResponsable")), row("idResponsable"), 0)
                objIndicator.RESPONSABLENAME = IIf(Not IsDBNull(row("ResponsableName")), row("ResponsableName"), "")

                If Not IsDBNull(row("entityname")) Then

                    objIndicator.ENTITYNAME = row("entityname")
                Else
                    objIndicator.ENTITYNAME = ""
                End If

                ' agregar a la lista
                IndicatorList.Add(objIndicator)

            Next

            ' retornar el objeto
            getList = IndicatorList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Indicator. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objIndicator = Nothing
            IndicatorList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo Indicator
    ''' </summary>
    ''' <param name="Indicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal Indicator As IndicatorEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Indicator SET")
            sql.AppendLine(" levelindicator = '" & Indicator.levelindicator & "',")
            sql.AppendLine(" identities = '" & Indicator.identity & "',")
            SQL.AppendLine(" code = '" & Indicator.code & "',")           
            SQL.AppendLine(" description = '" & Indicator.description & "',")           
            SQL.AppendLine(" type = '" & Indicator.type & "',")           
            SQL.AppendLine(" goal = '" & Indicator.goal & "',")           
            SQL.AppendLine(" greenvalue = '" & Indicator.greenvalue & "',")           
            SQL.AppendLine(" yellowvalue = '" & Indicator.yellowvalue & "',")           
            SQL.AppendLine(" redvalue = '" & Indicator.redvalue & "',")           
            SQL.AppendLine(" assumptions = '" & Indicator.assumptions & "',")           
            SQL.AppendLine(" sourceverification = '" & Indicator.sourceverification & "',")           
            SQL.AppendLine(" enabled = '" & Indicator.enabled & "',")           
            sql.AppendLine(" iduser = '" & Indicator.iduser & "',")
            sql.AppendLine(" idResponsable = '" & Indicator.idresponsable & "',")
            sql.AppendLine(" createdate = '" & Indicator.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            SQL.AppendLine("WHERE id = " & Indicator.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Indicator. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el Indicator de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idIndicator As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Indicator ")
            SQL.AppendLine(" where id = '" & idIndicator & "' ")

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
            Throw New Exception("Error al elimiar el Indicator. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
