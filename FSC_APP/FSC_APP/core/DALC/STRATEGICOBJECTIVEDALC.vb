Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class STRATEGICOBJECTIVEDALC

    ' contantes
    Const MODULENAME As String = "STRATEGICOBJECTIVEDALC"


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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicObjective WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicObjective WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo STRATEGICOBJECTIVE
    ''' </summary>
    ''' <param name="STRATEGICOBJECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal STRATEGICOBJECTIVE As STRATEGICOBJECTIVEEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO StrategicObjective(" & _
             "code," & _
             "name," & _
             "year," & _
             "idperspective," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.code & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.name & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.year & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.idperspective & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.enabled & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.iduser & "',")
            sql.AppendLine("'" & STRATEGICOBJECTIVE.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el STRATEGICOBJECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un STRATEGICOBJECTIVE por el Id
    ''' </summary>
    ''' <param name="idSTRATEGICOBJECTIVE"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSTRATEGICOBJECTIVE As Integer) As STRATEGICOBJECTIVEEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSTRATEGICOBJECTIVE As New STRATEGICOBJECTIVEEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT StrategicObjective.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM StrategicObjective INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON StrategicObjective.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE StrategicObjective.Id = " & idSTRATEGICOBJECTIVE)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objSTRATEGICOBJECTIVE.id = data.Rows(0)("id")
				objSTRATEGICOBJECTIVE.code = data.Rows(0)("code")
				objSTRATEGICOBJECTIVE.name = data.Rows(0)("name")
				objSTRATEGICOBJECTIVE.year = data.Rows(0)("year")
				objSTRATEGICOBJECTIVE.idperspective = data.Rows(0)("idperspective")
				objSTRATEGICOBJECTIVE.enabled = data.Rows(0)("enabled")
				objSTRATEGICOBJECTIVE.iduser = data.Rows(0)("iduser")
                objSTRATEGICOBJECTIVE.createdate = data.Rows(0)("createdate")
                objSTRATEGICOBJECTIVE.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objSTRATEGICOBJECTIVE

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un STRATEGICOBJECTIVE. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSTRATEGICOBJECTIVE = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="year"></param>
    ''' <param name="idperspective"></param>
    ''' <param name="perspectivename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of STRATEGICOBJECTIVEEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal year As String = "", _
        Optional ByVal idperspective As String = "", _
        Optional ByVal perspectivename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of STRATEGICOBJECTIVEEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSTRATEGICOBJECTIVE As STRATEGICOBJECTIVEEntity
        Dim STRATEGICOBJECTIVEList As New List(Of STRATEGICOBJECTIVEEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT sto.*, apu.Name AS userName, per.Name AS perspectiveName ")
            sql.Append(" FROM StrategicObjective AS sto INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON sto.IdUser = apu.ID INNER JOIN ")
            sql.Append(" Perspective AS per ON sto.IdPerspective = per.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " sto.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " sto.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " sto.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " sto.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not year.Equals("") Then

                sql.Append(where & " sto.year like '%" & year & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idperspective.Equals("") Then

                sql.Append(where & " sto.IdPerspective = '" & idperspective & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not perspectivename.Equals("") Then

                sql.Append(where & " per.Name like '%" & perspectivename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " sto.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " sto.enabled  IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " sto.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, sto.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "perspectivename"
                        sql.Append(" ORDER BY per.Name ")
                    Case Else
                        sql.Append(" ORDER BY sto." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSTRATEGICOBJECTIVE = New STRATEGICOBJECTIVEEntity

                ' cargar el valor del campo
                objSTRATEGICOBJECTIVE.id = row("id")
                objSTRATEGICOBJECTIVE.code = row("code")
                objSTRATEGICOBJECTIVE.name = row("name")
                objSTRATEGICOBJECTIVE.year = row("year")
                objSTRATEGICOBJECTIVE.idperspective = row("idperspective")
                objSTRATEGICOBJECTIVE.enabled = row("enabled")
                objSTRATEGICOBJECTIVE.iduser = row("iduser")
                objSTRATEGICOBJECTIVE.createdate = row("createdate")
                objSTRATEGICOBJECTIVE.USERNAME = row("userName")
                objSTRATEGICOBJECTIVE.PERSPECTIVENAME = row("perspectiveName")

                ' agregar a la lista
                STRATEGICOBJECTIVEList.Add(objSTRATEGICOBJECTIVE)

            Next

            ' retornar el objeto
            getList = STRATEGICOBJECTIVEList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de STRATEGICOBJECTIVE. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSTRATEGICOBJECTIVE = Nothing
            STRATEGICOBJECTIVEList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo STRATEGICOBJECTIVE
    ''' </summary>
    ''' <param name="STRATEGICOBJECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal STRATEGICOBJECTIVE As STRATEGICOBJECTIVEEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update STRATEGICOBJECTIVE SET")
            sql.AppendLine(" code = '" & STRATEGICOBJECTIVE.code & "',")
            sql.AppendLine(" name = '" & STRATEGICOBJECTIVE.name & "',")
            sql.AppendLine(" year = '" & STRATEGICOBJECTIVE.year & "',")
            sql.AppendLine(" idperspective = '" & STRATEGICOBJECTIVE.idperspective & "',")
            sql.AppendLine(" enabled = '" & STRATEGICOBJECTIVE.enabled & "',")
            sql.AppendLine(" iduser = '" & STRATEGICOBJECTIVE.iduser & "',")
            sql.AppendLine(" createdate = '" & STRATEGICOBJECTIVE.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "' ")
            sql.AppendLine("WHERE id = " & STRATEGICOBJECTIVE.id)

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
            Throw New Exception("Error al modificar el STRATEGICOBJECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el STRATEGICOBJECTIVE de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSTRATEGICOBJECTIVE As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from STRATEGICOBJECTIVE ")
            SQL.AppendLine(" where id = '" & idSTRATEGICOBJECTIVE & "' ")

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
            Throw New Exception("Error al elimiar el STRATEGICOBJECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
