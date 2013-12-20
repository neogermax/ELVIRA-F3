Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class PERSPECTIVEDALC

    ' contantes
    Const MODULENAME As String = "PERSPECTIVEDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Perspective WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Perspective WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo PERSPECTIVE
    ''' </summary>
    ''' <param name="PERSPECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal PERSPECTIVE As PERSPECTIVEEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO PERSPECTIVE(" & _
             "code," & _
             "name," & _
             "identerprise," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")           
            sql.AppendLine("'" & PERSPECTIVE.code & "',")
            sql.AppendLine("'" & PERSPECTIVE.name & "',")
            sql.AppendLine("'" & PERSPECTIVE.identerprise & "',")
            sql.AppendLine("'" & PERSPECTIVE.enabled & "',")
            sql.AppendLine("'" & PERSPECTIVE.iduser & "',")
            sql.AppendLine("'" & PERSPECTIVE.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el PERSPECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un PERSPECTIVE por el Id
    ''' </summary>
    ''' <param name="idPERSPECTIVE"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idPERSPECTIVE As Integer) As PERSPECTIVEEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objPERSPECTIVE As New PERSPECTIVEEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT per.*, apu.Name AS apuName, ent.Name AS entName ")
            sql.Append(" FROM Perspective AS per INNER JOIN ")
            sql.Append(" Enterprise AS ent ON per.IdEnterprise = ent.Id INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON per.IdUser = apu.ID ")
            sql.Append(" WHERE per.Id = " & idPERSPECTIVE)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objPERSPECTIVE.id = data.Rows(0)("id")
				objPERSPECTIVE.code = data.Rows(0)("code")
				objPERSPECTIVE.name = data.Rows(0)("name")
				objPERSPECTIVE.identerprise = data.Rows(0)("identerprise")
				objPERSPECTIVE.enabled = data.Rows(0)("enabled")
				objPERSPECTIVE.iduser = data.Rows(0)("iduser")
                objPERSPECTIVE.createdate = data.Rows(0)("createdate")
                objPERSPECTIVE.USERNAME = data.Rows(0)("apuName")
                objPERSPECTIVE.ENTERPRISENAME = data.Rows(0)("entName")

            End If

            ' retornar el objeto
            Return objPERSPECTIVE

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un PERSPECTIVE. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objPERSPECTIVE = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="identerprise"></param>
    ''' <param name="enterprisename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of PERSPECTIVEEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal identerprise As String = "", _
        Optional ByVal enterprisename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of PERSPECTIVEEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objPERSPECTIVE As PERSPECTIVEEntity
        Dim PERSPECTIVEList As New List(Of PERSPECTIVEEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT per.*, apu.Name AS apuName, ent.Name AS entName ")
            sql.Append(" FROM Perspective AS per INNER JOIN ")
            sql.Append(" Enterprise AS ent ON per.IdEnterprise = ent.Id INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON per.IdUser = apu.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " per.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " per.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " per.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " per.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not identerprise.Equals("") Then

                sql.Append(where & " per.IdEnterprise = '" & identerprise & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enterprisename.Equals("") Then

                sql.Append(where & " ent.Name like '%" & enterprisename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " per.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " per.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " per.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, per.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "enterprisename"
                        sql.Append(" ORDER BY ent.Name ")
                    Case Else
                        sql.Append(" ORDER BY per." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objPERSPECTIVE = New PERSPECTIVEEntity

                ' cargar el valor del campo
                objPERSPECTIVE.id = row("id")
                objPERSPECTIVE.code = row("code")
                objPERSPECTIVE.name = row("name")
                objPERSPECTIVE.identerprise = row("identerprise")
                objPERSPECTIVE.enabled = row("enabled")
                objPERSPECTIVE.iduser = row("iduser")
                objPERSPECTIVE.createdate = row("createdate")
                objPERSPECTIVE.USERNAME = row("apuName")
                objPERSPECTIVE.ENTERPRISENAME = row("entName")

                ' agregar a la lista
                PERSPECTIVEList.Add(objPERSPECTIVE)

            Next

            ' retornar el objeto
            getList = PERSPECTIVEList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de PERSPECTIVE. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objPERSPECTIVE = Nothing
            PERSPECTIVEList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo PERSPECTIVE
    ''' </summary>
    ''' <param name="PERSPECTIVE"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal PERSPECTIVE As PERSPECTIVEEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update PERSPECTIVE SET")                      
            SQL.AppendLine(" code = '" & PERSPECTIVE.code & "',")           
            SQL.AppendLine(" name = '" & PERSPECTIVE.name & "',")           
            SQL.AppendLine(" identerprise = '" & PERSPECTIVE.identerprise & "',")           
            SQL.AppendLine(" enabled = '" & PERSPECTIVE.enabled & "',")           
            SQL.AppendLine(" iduser = '" & PERSPECTIVE.iduser & "',")           
            SQL.AppendLine(" createdate = '" & PERSPECTIVE.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")       
            SQL.AppendLine("WHERE id = " & PERSPECTIVE.id)

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
            Throw New Exception("Error al modificar el PERSPECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el PERSPECTIVE de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idPERSPECTIVE As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from PERSPECTIVE ")
            SQL.AppendLine(" where id = '" & idPERSPECTIVE & "' ")

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
            Throw New Exception("Error al elimiar el PERSPECTIVE. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
