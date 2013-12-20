Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AttachFileFormatDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM AttachFileFormat WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM AttachFileFormat WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de Formato archivo anexo. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo AttachFileFormat
    ''' </summary>
    ''' <param name="AttachFileFormat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal AttachFileFormat As AttachFileFormatEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO AttachFileFormat(" & _
             "code," & _
             "name," & _
             "createdate," & _
             "iduser," & _
             "enabled" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & AttachFileFormat.code & "',")
            sql.AppendLine("'" & AttachFileFormat.name & "',")
            sql.AppendLine("'" & AttachFileFormat.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & AttachFileFormat.iduser & "',")
            sql.AppendLine("'" & AttachFileFormat.enabled & "')")

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
            Throw New Exception("Error al insertar el AttachFileFormat. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un AttachFileFormat por el Id
    ''' </summary>
    ''' <param name="idAttachFileFormat"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idAttachFileFormat As Integer) As AttachFileFormatEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objAttachFileFormat As New AttachFileFormatEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT AttachFileFormat.*, ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM AttachFileFormat ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON AttachFileFormat.IdUser = ApplicationUser.Id  ")
            sql.Append(" WHERE AttachFileFormat.Id = " & idAttachFileFormat)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objAttachFileFormat.id = data.Rows(0)("id")
				objAttachFileFormat.code = data.Rows(0)("code")
				objAttachFileFormat.name = data.Rows(0)("name")
				objAttachFileFormat.createdate = data.Rows(0)("createdate")
				objAttachFileFormat.iduser = data.Rows(0)("iduser")
                objAttachFileFormat.enabled = data.Rows(0)("enabled")
                objAttachFileFormat.USERNAME = data.Rows(0)("ApplicationUserName")

            End If

            ' retornar el objeto
            Return objAttachFileFormat

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un AttachFileFormat. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objAttachFileFormat = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="createdate"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <returns>un objeto de tipo List(Of AttachFileFormatEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal order As String = "") As List(Of AttachFileFormatEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objAttachFileFormat As AttachFileFormatEntity
        Dim AttachFileFormatList As New List(Of AttachFileFormatEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT AttachFileFormat.*, ApplicationUser.Name AS ApplicationUserName ")
            sql.Append(" FROM AttachFileFormat ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ON AttachFileFormat.IdUser = ApplicationUser.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " AttachFileFormat.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " AttachFileFormat.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " AttachFileFormat.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " AttachFileFormat.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, AttachFileFormat.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " AttachFileFormat.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " AttachFileFormat.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " AttachFileFormat.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case Else
                        sql.Append(" ORDER BY AttachFileFormat." & order)
                End Select


            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objAttachFileFormat = New AttachFileFormatEntity

                ' cargar el valor del campo
                objAttachFileFormat.id = row("id")
                objAttachFileFormat.code = row("code")
                objAttachFileFormat.name = row("name")
                objAttachFileFormat.createdate = row("createdate")
                objAttachFileFormat.iduser = row("iduser")
                objAttachFileFormat.enabled = row("enabled")
                objAttachFileFormat.USERNAME = row("ApplicationUserName")

                ' agregar a la lista
                AttachFileFormatList.Add(objAttachFileFormat)

            Next

            ' retornar el objeto
            getList = AttachFileFormatList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de AttachFileFormat. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objAttachFileFormat = Nothing
            AttachFileFormatList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo AttachFileFormat
    ''' </summary>
    ''' <param name="AttachFileFormat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal AttachFileFormat As AttachFileFormatEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update AttachFileFormat SET")
            sql.AppendLine(" code = '" & AttachFileFormat.code & "',")
            SQL.AppendLine(" name = '" & AttachFileFormat.name & "',")           
            sql.AppendLine(" enabled = '" & AttachFileFormat.enabled & "'")
            SQL.AppendLine("WHERE id = " & AttachFileFormat.id)

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
            Throw New Exception("Error al modificar el AttachFileFormat. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el AttachFileFormat de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idAttachFileFormat As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from AttachFileFormat ")
            SQL.AppendLine(" where id = '" & idAttachFileFormat & "' ")

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
            Throw New Exception("Error al elimiar el AttachFileFormat. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           
End Class
