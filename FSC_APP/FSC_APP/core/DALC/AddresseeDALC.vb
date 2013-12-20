Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AddresseeDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Addressee
    ''' </summary>
    ''' <param name="Addressee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Addressee As AddresseeEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Addressee(" & _
             "name," & _
             "email," & _
             "idusergroup," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Addressee.name & "',")
            sql.AppendLine("'" & Addressee.email & "',")
            sql.AppendLine("'" & Addressee.idusergroup & "',")
            sql.AppendLine("'" & Addressee.enabled & "',")
            sql.AppendLine("'" & Addressee.iduser & "',")
            sql.AppendLine("'" & Addressee.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Destinatario." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Addressee por el Id
    ''' </summary>
    ''' <param name="idAddressee"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAddressee As Integer) As AddresseeEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objAddressee As New AddresseeEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Addressee.* , ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM Addressee ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Addressee.IdUser = ApplicationUser.Id  ")
            sql.Append(" WHERE Addressee.Id = " & idAddressee)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objAddressee.id = data.Rows(0)("id")
                objAddressee.name = data.Rows(0)("name")
                objAddressee.email = data.Rows(0)("email")
                objAddressee.idusergroup = data.Rows(0)("idusergroup")
                objAddressee.enabled = data.Rows(0)("enabled")
                objAddressee.iduser = data.Rows(0)("iduser")
                objAddressee.createdate = data.Rows(0)("createdate")
                objAddressee.USERNAME = data.Rows(0)("ApplicationUserName")

            End If

            ' retornar el objeto
            Return objAddressee

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Destinatario.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objAddressee = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="name"></param>
    ''' <param name="email"></param>
    ''' <param name="idusergroup"></param>
    ''' <param name="usergroupname"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of AddresseeEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal idusergroup As String = "", _
        Optional ByVal usergroupname As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of AddresseeEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objAddressee As AddresseeEntity
        Dim AddresseeList As New List(Of AddresseeEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Addressee.*, ApplicationUser.Name AS ApplicationUserName, UserGroup.Name AS UserGroupName")
            sql.Append(" FROM Addressee ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Addressee.IdUser = ApplicationUser.Id  ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.UserGroup UserGroup ON Addressee.IdUserGroup = UserGroup.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Addressee.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Addressee.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Addressee.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not email.Equals("") Then

                sql.Append(where & " Addressee.email like '%" & email & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idusergroup.Equals("") Then

                sql.Append(where & " Addressee.idusergroup = '" & idusergroup & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not usergroupname.Equals("") Then

                sql.Append(where & " UserGroup.Name like '%" & usergroupname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Addressee.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Addressee.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Addressee.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Addressee.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                'Ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case "usergroupname"
                        sql.Append(" ORDER BY UserGroup.Name ")
                    Case Else
                        sql.Append(" ORDER BY Addressee." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objAddressee = New AddresseeEntity

                ' cargar el valor del campo
                objAddressee.id = row("id")
                objAddressee.name = row("name")
                objAddressee.email = row("email")
                objAddressee.idusergroup = row("idusergroup")
                objAddressee.enabled = row("enabled")
                objAddressee.iduser = row("iduser")
                objAddressee.createdate = row("createdate")
                objAddressee.USERNAME = row("ApplicationUserName")
                objAddressee.USERGROUPNAME = row("UserGroupName")

                ' agregar a la lista
                AddresseeList.Add(objAddressee)

            Next

            ' retornar el objeto
            getList = AddresseeList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Destinatarios.")

        Finally
            ' liberando recursos
            sql = Nothing
            objAddressee = Nothing
            AddresseeList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Addressee
    ''' </summary>
    ''' <param name="Addressee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Addressee As AddresseeEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Addressee SET")
            sql.AppendLine(" name = '" & Addressee.name & "',")
            sql.AppendLine(" email = '" & Addressee.email & "',")
            sql.AppendLine(" idusergroup = '" & Addressee.idusergroup & "',")
            sql.AppendLine(" enabled = '" & Addressee.enabled & "',")
            sql.AppendLine(" iduser = '" & Addressee.iduser & "',")
            sql.AppendLine(" createdate = '" & Addressee.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE id = " & Addressee.id)

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
            Throw New Exception("Error al modificar el Destinatario." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Addressee de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAddressee As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Addressee ")
            SQL.AppendLine(" where id = '" & idAddressee & "' ")

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
            Throw New Exception("Error al elimiar el Destinatario." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
