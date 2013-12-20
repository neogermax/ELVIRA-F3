Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class STRATEGYDALC

    ' contantes
    Const MODULENAME As String = "STRATEGYDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Strategy WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Strategy WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo STRATEGY
    ''' </summary>
    ''' <param name="STRATEGY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal STRATEGY As STRATEGYEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Strategy(" & _
             "code," & _
             "name," & _
             "idstrategicobjective," & _
             "idmanagment," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & STRATEGY.code & "',")
            sql.AppendLine("'" & STRATEGY.name & "',")
            sql.AppendLine("'" & STRATEGY.idstrategicobjective & "',")
            sql.AppendLine("'" & STRATEGY.idmanagment & "',")
            sql.AppendLine("'" & STRATEGY.enabled & "',")
            sql.AppendLine("'" & STRATEGY.iduser & "',")
            sql.AppendLine("'" & STRATEGY.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el STRATEGY. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un STRATEGY por el Id
    ''' </summary>
    ''' <param name="idSTRATEGY"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSTRATEGY As Integer) As STRATEGYEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSTRATEGY As New STRATEGYEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append("SELECT Strategy.*, ApplicationUser.Name AS userName ")
            sql.Append("FROM Strategy INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Strategy.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Strategy.Id = " & idSTRATEGY)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objSTRATEGY.id = data.Rows(0)("id")
				objSTRATEGY.code = data.Rows(0)("code")
				objSTRATEGY.name = data.Rows(0)("name")
				objSTRATEGY.idstrategicobjective = data.Rows(0)("idstrategicobjective")
				objSTRATEGY.idmanagment = data.Rows(0)("idmanagment")
				objSTRATEGY.enabled = data.Rows(0)("enabled")
				objSTRATEGY.iduser = data.Rows(0)("iduser")
                objSTRATEGY.createdate = data.Rows(0)("createdate")
                objSTRATEGY.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objSTRATEGY

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un STRATEGY. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSTRATEGY = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="idstrategicobjective"></param>
    ''' <param name="strategicobjectivename"></param>
    ''' <param name="idmanagment"></param>
    ''' <param name="managmentname"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of STRATEGYEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idmanagment As String = "", _
        Optional ByVal managmentname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of STRATEGYEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSTRATEGY As STRATEGYEntity
        Dim STRATEGYList As New List(Of STRATEGYEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT sty.*, apu.Name AS userName, sto.Name AS strategicObjectiveName, man.Name AS managementName ")
            sql.Append(" FROM Strategy AS sty INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON sty.IdUser = apu.ID INNER JOIN ")
            sql.Append(" StrategicObjective AS sto ON sty.IdStrategicObjective = sto.Id INNER JOIN ")
            sql.Append(" Management AS man ON sty.IdManagment = man.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " sty.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " sty.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " sty.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " sty.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idstrategicobjective.Equals("") Then

                sql.Append(where & " sty.IdStrategicObjective = '" & idstrategicobjective & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not strategicobjectivename.Equals("") Then

                sql.Append(where & " sto.Name like '%" & strategicobjectivename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idmanagment.Equals("") Then

                sql.Append(where & " sty.IdManagment = '" & idmanagment & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not managmentname.Equals("") Then

                sql.Append(where & " man.Name like '%" & managmentname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " sty.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " sty.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " sty.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, sty.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "strategicobjectivename"
                        sql.Append(" ORDER BY sto.Name ")
                    Case "managmentname"
                        sql.Append(" ORDER BY man.Name ")
                    Case Else
                        sql.Append(" ORDER BY sty." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSTRATEGY = New STRATEGYEntity

                ' cargar el valor del campo
                objSTRATEGY.id = row("id")
                objSTRATEGY.code = row("code")
                objSTRATEGY.name = row("name")
                objSTRATEGY.idstrategicobjective = row("idstrategicobjective")
                objSTRATEGY.idmanagment = row("idmanagment")
                objSTRATEGY.enabled = row("enabled")
                objSTRATEGY.iduser = row("iduser")
                objSTRATEGY.createdate = row("createdate")
                objSTRATEGY.USERNAME = row("userName")
                objSTRATEGY.STRATEGICOBJECTIVENAME = row("strategicObjectiveName")
                objSTRATEGY.MANAGEMENTNAME = row("managementName")

                ' agregar a la lista
                STRATEGYList.Add(objSTRATEGY)

            Next

            ' retornar el objeto
            getList = STRATEGYList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de STRATEGY. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSTRATEGY = Nothing
            STRATEGYList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo STRATEGY
    ''' </summary>
    ''' <param name="STRATEGY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal STRATEGY As STRATEGYEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update STRATEGY SET")
            SQL.AppendLine(" code = '" & STRATEGY.code & "',")           
            SQL.AppendLine(" name = '" & STRATEGY.name & "',")           
            SQL.AppendLine(" idstrategicobjective = '" & STRATEGY.idstrategicobjective & "',")           
            SQL.AppendLine(" idmanagment = '" & STRATEGY.idmanagment & "',")           
            SQL.AppendLine(" enabled = '" & STRATEGY.enabled & "',")           
            SQL.AppendLine(" iduser = '" & STRATEGY.iduser & "',")           
            sql.AppendLine(" createdate = '" & STRATEGY.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            SQL.AppendLine("WHERE id = " & STRATEGY.id)

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
            Throw New Exception("Error al modificar el STRATEGY. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el STRATEGY de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSTRATEGY As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from STRATEGY ")
            SQL.AppendLine(" where id = '" & idSTRATEGY & "' ")

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
            Throw New Exception("Error al elimiar el STRATEGY. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
