Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SummoningDALC

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Summoning WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Summoning WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de Summoning. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function


    ''' <summary> 
    ''' Registar un nuevo Summoning
    ''' </summary>
    ''' <param name="Summoning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal Summoning As SummoningEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Summoning(" & _
             "code," & _
             "name," & _
             "description," & _
             "idproject," & _
             "begindate," & _
             "enddate," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Summoning.code & "',")
            sql.AppendLine("'" & Summoning.name & "',")
            sql.AppendLine("'" & Summoning.description & "',")
            sql.AppendLine("'" & Summoning.idproject & "',")
            sql.AppendLine("'" & Summoning.begindate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & Summoning.enddate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & Summoning.enabled & "',")
            sql.AppendLine("'" & Summoning.iduser & "',")
            sql.AppendLine("'" & Summoning.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Summoning. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un Summoning por el Id
    ''' </summary>
    ''' <param name="idSummoning"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSummoning As Integer) As SummoningEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSummoning As New SummoningEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append("SELECT     Summoning.*,Project.Name AS PROJECTNAME, ApplicationUser.Name AS USERNAME")
            sql.Append(" FROM Summoning INNER JOIN Project ON Summoning.IdProject = Project.idKey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Summoning.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Summoning.Id = " & idSummoning)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objSummoning.id = data.Rows(0)("id")
				objSummoning.code = data.Rows(0)("code")
				objSummoning.name = data.Rows(0)("name")
				objSummoning.description = data.Rows(0)("description")
				objSummoning.idproject = data.Rows(0)("idproject")
				objSummoning.begindate = data.Rows(0)("begindate")
				objSummoning.enddate = data.Rows(0)("enddate")
				objSummoning.enabled = data.Rows(0)("enabled")
				objSummoning.iduser = data.Rows(0)("iduser")
                objSummoning.createdate = data.Rows(0)("createdate")
                objSummoning.USERNAME = data.Rows(0)("USERNAME")
                objSummoning.PROJECTNAME = data.Rows(0)("PROJECTNAME")

            End If

            ' retornar el objeto
            Return objSummoning

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Summoning. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSummoning = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <param name="idproject"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="enabled"></param>
    ''' <param name="iduser"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of SummoningEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
          Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idproject As String = "", _
          Optional ByVal projectname As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of SummoningEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSummoning As SummoningEntity
        Dim SummoningList As New List(Of SummoningEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append("SELECT     Summoning.*,Project.Name AS PROJECTNAME, ApplicationUser.Name AS USERNAME")
            sql.Append(" FROM Summoning INNER JOIN Project ON Summoning.IdProject = Project.idKey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Summoning.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Summoning.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Summoning.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Summoning.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Summoning.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " Summoning.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & "  Project.Id = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

          

            ' verificar si hay entrada de datos para el campo
            If Not begindate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Summoning.begindate, 103) like '%" & begindate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Summoning.enddate, 103) like '%" & enddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & "  Summoning.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Summoning.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & "  ApplicationUser.Id = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Summoning.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then
                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case "projectname"
                        sql.Append(" ORDER BY Project.Name ")
                    Case Else
                        sql.Append(" ORDER BY Summoning." & order)
                End Select




            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSummoning = New SummoningEntity

                ' cargar el valor del campo
                objSummoning.id = row("id")
                objSummoning.code = row("code")
                objSummoning.name = row("name")
                objSummoning.description = row("description")
                objSummoning.idproject = row("idproject")
                objSummoning.begindate = row("begindate")
                objSummoning.enddate = row("enddate")
                objSummoning.enabled = row("enabled")
                objSummoning.iduser = row("iduser")
                objSummoning.createdate = row("createdate")
                objSummoning.USERNAME = row("USERNAME")
                objSummoning.PROJECTNAME = row("PROJECTNAME")
                ' agregar a la lista
                SummoningList.Add(objSummoning)

            Next

            ' retornar el objeto
            getList = SummoningList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Summoning. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSummoning = Nothing
            SummoningList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Summoning
    ''' </summary>
    ''' <param name="Summoning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Summoning As SummoningEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Summoning SET")
            sql.AppendLine(" code = '" & Summoning.code & "',")
            sql.AppendLine(" name = '" & Summoning.name & "',")
            sql.AppendLine(" description = '" & Summoning.description & "',")
            sql.AppendLine(" idproject = '" & Summoning.idproject & "',")
            sql.AppendLine(" begindate = '" & Summoning.begindate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" enddate = '" & Summoning.enddate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" enabled = '" & Summoning.enabled & "',")
            sql.AppendLine(" iduser = '" & Summoning.iduser & "',")
            sql.AppendLine(" createdate = '" & Summoning.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & Summoning.id)

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
            Throw New Exception("Error al modificar el Summoning. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Summoning de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSummoning As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Summoning ")
            SQL.AppendLine(" where id = '" & idSummoning & "' ")

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
            Throw New Exception("Error al elimiar el Summoning. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
