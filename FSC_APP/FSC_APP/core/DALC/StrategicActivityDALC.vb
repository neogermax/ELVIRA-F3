Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class StrategicActivityDALC

    ' contantes
    Const MODULENAME As String = "StrategicActivityDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicActivity WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicActivity WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo StrategicActivity
    ''' </summary>
    ''' <param name="StrategicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal StrategicActivity As StrategicActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO StrategicActivity(" & _
             "code," & _
             "name," & _
             "description," & _
             "idstrategy," & _
             "begindate," & _
             "enddate," & _
             "estimatedvalue," & _
             "idresponsible," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & StrategicActivity.code & "',")
            sql.AppendLine("'" & StrategicActivity.name & "',")
            sql.AppendLine("'" & StrategicActivity.description & "',")
            sql.AppendLine("'" & StrategicActivity.idstrategy & "',")
            sql.AppendLine("'" & StrategicActivity.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & StrategicActivity.enddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & StrategicActivity.estimatedvalue & "',")
            sql.AppendLine("'" & StrategicActivity.idresponsible & "',")
            sql.AppendLine("'" & StrategicActivity.enabled & "',")
            sql.AppendLine("'" & StrategicActivity.iduser & "',")
            sql.AppendLine("'" & StrategicActivity.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el StrategicActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un StrategicActivity por el Id
    ''' </summary>
    ''' <param name="idStrategicActivity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idStrategicActivity As Integer) As StrategicActivityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objStrategicActivity As New StrategicActivityEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT StrategicActivity.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM StrategicActivity INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON StrategicActivity.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE StrategicActivity.Id = " & idStrategicActivity)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objStrategicActivity.id = data.Rows(0)("id")
				objStrategicActivity.code = data.Rows(0)("code")
				objStrategicActivity.name = data.Rows(0)("name")
				objStrategicActivity.description = data.Rows(0)("description")
                objStrategicActivity.idstrategy = data.Rows(0)("idstrategy")
				objStrategicActivity.begindate = data.Rows(0)("begindate")
				objStrategicActivity.enddate = data.Rows(0)("enddate")
				objStrategicActivity.estimatedvalue = data.Rows(0)("estimatedvalue")
				objStrategicActivity.idresponsible = data.Rows(0)("idresponsible")
				objStrategicActivity.enabled = data.Rows(0)("enabled")
				objStrategicActivity.iduser = data.Rows(0)("iduser")
                objStrategicActivity.createdate = data.Rows(0)("createdate")
                objStrategicActivity.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objStrategicActivity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un StrategicActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objStrategicActivity = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <param name="idstrategy"></param>
    ''' <param name="strategyname"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="estimatedvalue"></param>
    ''' <param name="idresponsible"></param>
    ''' <param name="responsiblename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of StrategicActivityEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idstrategy As String = "", _
        Optional ByVal strategyname As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal estimatedvalue As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of StrategicActivityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objStrategicActivity As StrategicActivityEntity
        Dim StrategicActivityList As New List(Of StrategicActivityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT sta.*, str.Name AS strategyName, apu.Name AS userName, ")
            sql.Append(" (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser WHERE (sta.IdResponsible = ID)) AS responsibleName ")
            sql.Append(" FROM StrategicActivity AS sta INNER JOIN ")
            sql.Append(" Strategy AS str ON sta.IdStrategy = str.Id INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON sta.IdUser = apu.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " sta.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " sta.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " sta.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " sta.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " sta.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idstrategy.Equals("") Then

                sql.Append(where & " sta.IdStrategy = '" & idstrategy & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not strategyname.Equals("") Then

                sql.Append(where & " str.Name like '%" & strategyname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not begindate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, sta.begindate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, sta.enddate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not estimatedvalue.Equals("") Then

                sql.Append(where & " sta.estimatedvalue like '%" & estimatedvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idresponsible.Equals("") Then

                sql.Append(where & " sta.IdResponsible = '" & idresponsible & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not responsiblename.Equals("") Then

                sql.Append(where & " (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser WHERE (sta.IdResponsible = ID)) like '%" & responsiblename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " sta.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " sta.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " sta.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, sta.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "strategyname"
                        sql.Append(" ORDER BY str.Name ")
                    Case "responsiblename"
                        sql.Append(" ORDER BY (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser WHERE (sta.IdResponsible = ID)) ")
                    Case Else
                        sql.Append(" ORDER BY sta." & order)
                End Select


            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objStrategicActivity = New StrategicActivityEntity

                ' cargar el valor del campo
                objStrategicActivity.id = row("id")
                objStrategicActivity.code = row("code")
                objStrategicActivity.name = row("name")
                objStrategicActivity.description = row("description")
                objStrategicActivity.idstrategy = row("idstrategy")
                objStrategicActivity.begindate = row("begindate")
                objStrategicActivity.enddate = row("enddate")
                objStrategicActivity.estimatedvalue = row("estimatedvalue")
                objStrategicActivity.idresponsible = row("idresponsible")
                objStrategicActivity.enabled = row("enabled")
                objStrategicActivity.iduser = row("iduser")
                objStrategicActivity.createdate = row("createdate")
                objStrategicActivity.USERNAME = row("userName")
                objStrategicActivity.STRATEGYNAME = row("strategyName")
                objStrategicActivity.RESPONSIBLENAME = row("responsibleName")

                ' agregar a la lista
                StrategicActivityList.Add(objStrategicActivity)

            Next

            ' retornar el objeto
            getList = StrategicActivityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objStrategicActivity = Nothing
            StrategicActivityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo StrategicActivity
    ''' </summary>
    ''' <param name="StrategicActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal StrategicActivity As StrategicActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update StrategicActivity SET")
            SQL.AppendLine(" code = '" & StrategicActivity.code & "',")           
            SQL.AppendLine(" name = '" & StrategicActivity.name & "',")           
            SQL.AppendLine(" description = '" & StrategicActivity.description & "',")           
            sql.AppendLine(" idstrategy = '" & StrategicActivity.idstrategy & "',")
            sql.AppendLine(" begindate = '" & StrategicActivity.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine(" enddate = '" & StrategicActivity.enddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            SQL.AppendLine(" estimatedvalue = '" & StrategicActivity.estimatedvalue & "',")           
            SQL.AppendLine(" idresponsible = '" & StrategicActivity.idresponsible & "',")           
            SQL.AppendLine(" enabled = '" & StrategicActivity.enabled & "',")           
            SQL.AppendLine(" iduser = '" & StrategicActivity.iduser & "',")           
            sql.AppendLine(" createdate = '" & StrategicActivity.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE id = " & StrategicActivity.id)

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
            Throw New Exception("Error al modificar el StrategicActivity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el StrategicActivity de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idStrategicActivity As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from StrategicActivity ")
            SQL.AppendLine(" where id = '" & idStrategicActivity & "' ")

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
            Throw New Exception("Error al elimiar el StrategicActivity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
