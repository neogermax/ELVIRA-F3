Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class STRATEGICLINEDALC

    ' contantes
    Const MODULENAME As String = "STRATEGICLINEDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicLine WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM StrategicLine WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo LINEA ESTRATEGICA
    ''' </summary>
    ''' <param name="StrategicLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal StrategicLine As StrategicLineEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO StrategicLine(" & _
             "code," & _
             "name," & _
             "idstrategicobjective," & _
             "idmanagment," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine(" VALUES (")
            sql.AppendLine("'" & StrategicLine.code & "',")
            sql.AppendLine("'" & StrategicLine.name & "',")
            sql.AppendLine("'" & StrategicLine.idstrategicobjective & "',")
            sql.AppendLine("'" & StrategicLine.idmanagment & "',")
            sql.AppendLine("'" & StrategicLine.enabled & "',")
            sql.AppendLine("'" & StrategicLine.iduser & "',")
            sql.AppendLine("'" & StrategicLine.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el StrategicLine. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un StrategicLine por el Id
    ''' </summary>
    ''' <param name="idStrategicLine"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicLine As Integer) As StrategicLineEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objStrategicLine As New StrategicLineEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT StrategicLine.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM StrategicLine INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser  ON StrategicLine.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE StrategicLine.Id = " & idStrategicLine)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objStrategicLine.id = data.Rows(0)("id")
                objStrategicLine.code = data.Rows(0)("code")
                objStrategicLine.name = data.Rows(0)("name")
                objStrategicLine.idstrategicobjective = data.Rows(0)("idstrategicobjective")
                objStrategicLine.idmanagment = data.Rows(0)("idmanagment")
                objStrategicLine.enabled = data.Rows(0)("enabled")
                objStrategicLine.iduser = data.Rows(0)("iduser")
                objStrategicLine.createdate = data.Rows(0)("createdate")
                objStrategicLine.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objStrategicLine

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un StrategicLine. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objStrategicLine = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="idstrategicobjective"></param>
    ''' <param name="idmanagment"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of StrategicLineEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idmanagment As String = "", _
        Optional ByVal managementname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of StrategicLineEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objStrategicLine As StrategicLineEntity
        Dim StrategicLineList As New List(Of StrategicLineEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT pro.*, apu.Name AS userName, man.Name AS managementName, sto.Name AS strategicObjectiveName ")
            sql.Append(" FROM StrategicLine AS pro INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON pro.IdUser = apu.ID INNER JOIN ")
            sql.Append(" Management AS man ON pro.IdManagment = man.Id INNER JOIN ")
            sql.Append(" StrategicObjective AS sto ON pro.IdStrategicObjective = sto.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " pro.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " pro.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " pro.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " pro.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idstrategicobjective.Equals("") Then

                sql.Append(where & " pro.IdStrategicObjective = '" & idstrategicobjective & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not strategicobjectivename.Equals("") Then

                sql.Append(where & " sto.Name like '%" & strategicobjectivename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idmanagment.Equals("") Then

                sql.Append(where & " pro.IdManagment = '" & idmanagment & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not managementname.Equals("") Then

                sql.Append(where & " man.Name like '%" & managementname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " pro.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " pro.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " pro.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, pro.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "strategicobjectivename"
                        sql.Append(" ORDER BY sto.Name ")
                    Case "managementname"
                        sql.Append(" ORDER BY man.Name ")
                    Case Else
                        sql.Append(" ORDER BY pro." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objStrategicLine = New StrategicLineEntity

                ' cargar el valor del campo
                objStrategicLine.id = row("id")
                objStrategicLine.code = row("code")
                objStrategicLine.name = row("name")
                objStrategicLine.idstrategicobjective = row("idstrategicobjective")
                objStrategicLine.idmanagment = row("idmanagment")
                objStrategicLine.enabled = row("enabled")
                objStrategicLine.iduser = row("iduser")
                objStrategicLine.createdate = row("createdate")
                objStrategicLine.USERNAME = row("userName")
                objStrategicLine.STRATEGICOBJECTIVENAME = row("strategicObjectiveName")
                objStrategicLine.MANAGEMENTNAME = row("managementName")

                ' agregar a la lista
                StrategicLineList.Add(objStrategicLine)

            Next

            ' retornar el objeto
            getList = StrategicLineList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicLine. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objStrategicLine = Nothing
            StrategicLineList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo StrategicLine
    ''' </summary>
    ''' <param name="StrategicLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal StrategicLine As StrategicLineEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update StrategicLine SET")
            sql.AppendLine(" code = '" & StrategicLine.code & "',")
            sql.AppendLine(" name = '" & StrategicLine.name & "',")
            sql.AppendLine(" idstrategicobjective = '" & StrategicLine.idstrategicobjective & "',")
            sql.AppendLine(" idmanagment = '" & StrategicLine.idmanagment & "',")
            sql.AppendLine(" enabled = '" & StrategicLine.enabled & "',")
            sql.AppendLine(" iduser = '" & StrategicLine.iduser & "',")
            sql.AppendLine(" createdate = '" & StrategicLine.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & StrategicLine.id)

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
            Throw New Exception("Error al modificar el StrategicLine. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el StrategicLine de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idStrategicLine As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from StrategicLine ")
            SQL.AppendLine(" where id = '" & idStrategicLine & "' ")

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
            Throw New Exception("Error al elimiar el StrategicLine. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
