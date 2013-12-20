Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProgramDALC

    ' contantes
    Const MODULENAME As String = "ProgramDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Program WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Program WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Program
    ''' </summary>
    ''' <param name="Program"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Program As ProgramEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Program(" & _
             "code," & _
             "name," & _
             "idStrategicLine," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Program.code & "',")
            sql.AppendLine("'" & Program.name & "',")
            sql.AppendLine("'" & Program.idStrategicLine & "',")
            sql.AppendLine("'" & Program.enabled & "',")
            sql.AppendLine("'" & Program.iduser & "',")
            sql.AppendLine("'" & Program.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Program. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Program por el Id
    ''' </summary>
    ''' <param name="idProgram"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgram As Integer) As ProgramEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProgram As New ProgramEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT mav.*, pro.Name AS StrategicLineName, apu.Name AS userName ")
            sql.Append(" FROM Program AS mav INNER JOIN ")
            sql.Append(" StrategicLine AS pro ON mav.IdStrategicLine =  pro.id  INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON mav.IdUser = apu.ID ")
            sql.Append(" WHERE mav.Id = " & idProgram)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProgram.id = data.Rows(0)("id")
                objProgram.code = data.Rows(0)("code")
                objProgram.name = data.Rows(0)("name")
                objProgram.idStrategicLine = data.Rows(0)("idStrategicLine")
                objProgram.enabled = data.Rows(0)("enabled")
                objProgram.iduser = data.Rows(0)("iduser")
                objProgram.createdate = data.Rows(0)("createdate")
                objProgram.USERNAME = data.Rows(0)("userName")
                objProgram.StrategicLineNAME = data.Rows(0)("StrategicLineName")

            End If

            ' retornar el objeto
            Return objProgram

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Program. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProgram = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ProgramEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                Optional ByVal id As String = "", _
                                Optional ByVal idlike As String = "", _
                                Optional ByVal code As String = "", _
                                Optional ByVal name As String = "", _
                                Optional ByVal idStrategicLine As String = "", _
                                Optional ByVal StrategicLinename As String = "", _
                                Optional ByVal enabled As String = "", _
                                Optional ByVal enabledtext As String = "", _
                                Optional ByVal iduser As String = "", _
                                Optional ByVal username As String = "", _
                                Optional ByVal createdate As String = "", _
                                Optional ByVal order As String = "") As List(Of ProgramEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProgram As ProgramEntity
        Dim ProgramList As New List(Of ProgramEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT mav.*, pro.Name AS StrategicLineName, apu.Name AS userName ")
            sql.Append(" FROM Program AS mav INNER JOIN ")
            sql.Append(" StrategicLine AS pro ON mav.IdStrategicLine =  pro.Id  INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON mav.IdUser = apu.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " mav.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " mav.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " mav.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " mav.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then

                sql.Append(where & " mav.IdStrategicLine = '" & idStrategicLine & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not StrategicLinename.Equals("") Then

                sql.Append(where & " pro.Name like '%" & StrategicLinename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " mav.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " mav.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " mav.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, mav.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "StrategicLinename"
                        sql.Append(" ORDER BY pro.Name ")
                    Case Else
                        sql.Append(" ORDER BY mav." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProgram = New ProgramEntity

                ' cargar el valor del campo
                objProgram.id = row("id")
                objProgram.code = row("code")
                objProgram.name = row("name")
                objProgram.idStrategicLine = row("idStrategicLine")
                objProgram.enabled = row("enabled")
                objProgram.iduser = row("iduser")
                objProgram.createdate = row("createdate")
                objProgram.USERNAME = row("userName")
                objProgram.StrategicLineNAME = row("StrategicLineName")

                ' agregar a la lista
                ProgramList.Add(objProgram)

            Next

            ' retornar el objeto
            getList = ProgramList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Program. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProgram = Nothing
            ProgramList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Program
    ''' </summary>
    ''' <param name="Program"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Program As ProgramEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Program SET")
            sql.AppendLine(" code = '" & Program.code & "',")
            sql.AppendLine(" name = '" & Program.name & "',")
            sql.AppendLine(" idStrategicLine = '" & Program.idStrategicLine & "',")
            sql.AppendLine(" enabled = '" & Program.enabled & "'")
            sql.AppendLine("WHERE id = " & Program.id)

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
            Throw New Exception("Error al modificar el Program. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Program de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgram As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Program ")
            SQL.AppendLine(" where id = '" & idProgram & "' ")

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
            Throw New Exception("Error al elimiar el Program. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
