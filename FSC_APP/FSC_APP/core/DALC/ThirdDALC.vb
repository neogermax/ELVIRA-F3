Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ThirdDALC

    ' contantes
    Const MODULENAME As String = "ThirdDALC"

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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Third WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Third WHERE Code = '" & code & "' AND id <> '" & id & "'")

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
    ''' Registar un nuevo Third
    ''' </summary>
    ''' <param name="Third"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Third As ThirdEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Third(" & _
             "code," & _
             "name," & _
             "contact," & _
             "documents," & _
             "phone," & _
             "email," & _
             "actions," & _
             "experiences," & _
             "enabled," & _
             "personanatural," & _
             "representantelegal," & _
             "iduser," & _
             "tipodocumento," & _
             "docrepresentante," & _
             "createdate," & _
             "direccion," & _
             "sex" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Third.code & "',")
            sql.AppendLine("'" & Third.name & "',")
            sql.AppendLine("'" & Third.contact & "',")
            sql.AppendLine("'" & Third.documents & "',")
            sql.AppendLine("'" & Third.phone & "',")
            sql.AppendLine("'" & Third.email & "',")
            sql.AppendLine("' ',")
            sql.AppendLine("' ',")
            sql.AppendLine("'" & Third.enabled & "',")
            sql.AppendLine("'" & Third.PersonaNatural & "',")
            sql.AppendLine("'" & Third.representantelegal & "',")
            sql.AppendLine("'" & Third.iduser & "',")
            sql.AppendLine("'" & Third.tipodocumento & "',")
            sql.AppendLine("'" & Third.docrepresentante & "',")
            sql.AppendLine("'" & Third.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Third.direccion & "',")
            sql.AppendLine("'" & Third.sex & "')")

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
            Throw New Exception("Error al insertar el Third. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Third por el Id
    ''' </summary>
    ''' <param name="idThird"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThird As Integer) As ThirdEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objThird As New ThirdEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Third.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM Third INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Third.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE Third.Id = " & idThird)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objThird.id = data.Rows(0)("id")
                objThird.code = data.Rows(0)("code")
                objThird.name = data.Rows(0)("name")

                If IsDBNull(data.Rows(0)("contact")) = False Then
                    objThird.contact = data.Rows(0)("contact")
                End If

                If IsDBNull(data.Rows(0)("documents")) = False Then
                    objThird.documents = data.Rows(0)("documents")
                End If

                If IsDBNull(data.Rows(0)("phone")) = False Then
                    objThird.phone = data.Rows(0)("phone")
                End If

                If IsDBNull(data.Rows(0)("email")) = False Then
                    objThird.email = data.Rows(0)("email")
                End If

                If IsDBNull(data.Rows(0)("actions")) = False Then
                    objThird.actions = data.Rows(0)("actions")
                End If

                If IsDBNull(data.Rows(0)("experiences")) = False Then
                    objThird.experiences = data.Rows(0)("experiences")
                End If

                If IsDBNull(data.Rows(0)("PersonaNatural")) = False Then
                    objThird.PersonaNatural = data.Rows(0)("PersonaNatural")
                End If

                If IsDBNull(data.Rows(0)("RepresentanteLegal")) = False Then
                    objThird.representantelegal = data.Rows(0)("RepresentanteLegal")
                End If

                objThird.enabled = data.Rows(0)("enabled")
                objThird.iduser = data.Rows(0)("iduser")
                objThird.createdate = data.Rows(0)("createdate")
                objThird.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objThird

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Third. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objThird = Nothing

        End Try

    End Function

    Public Function getList2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal id As String = "", _
       Optional ByVal idlike As String = "", _
       Optional ByVal code As String = "", _
       Optional ByVal name As String = "", _
       Optional ByVal enabled As String = "", _
       Optional ByVal enabledtext As String = "", _
       Optional ByVal iduser As String = "", _
       Optional ByVal username As String = "", _
       Optional ByVal createdate As String = "", _
       Optional ByVal order As String = "") As List(Of ThirdEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objThird As ThirdEntity
        Dim ThirdList As New List(Of ThirdEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Third.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM Third INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Third.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Third.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Third.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Third.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Third.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Third.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Third.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Third.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Third.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case Else
                        sql.Append(" ORDER BY Third.createDate desc")
                End Select

            End If

            sql.Append(" ORDER BY Third.createDate desc")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objThird = New ThirdEntity

                ' cargar el valor del campo
                objThird.id = row("id")
                objThird.code = row("code")
                objThird.name = row("name")
                objThird.enabled = row("enabled")
                objThird.iduser = row("iduser")
                objThird.createdate = row("createdate")
                objThird.USERNAME = row("userName")

                ' agregar a la lista
                ThirdList.Add(objThird)

            Next

            ' retornar el objeto
            getList2 = ThirdList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objThird = Nothing
            ThirdList = Nothing
            data = Nothing
            where = Nothing

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
    ''' <returns>un objeto de tipo List(Of ThirdEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal contact As String = "", _
        Optional ByVal documents As String = "", _
        Optional ByVal phone As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal actions As String = "", _
        Optional ByVal experiences As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ThirdEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objThird As ThirdEntity
        Dim ThirdList As New List(Of ThirdEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia

            sql.Append(" select Third.id,Third.Name from Third ")
            sql.Append(" order by (Name) ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objThird = New ThirdEntity

                ' cargar el valor del campo
                objThird.id = row("id")
                objThird.name = row("name")

                ' agregar a la lista
                ThirdList.Add(objThird)

            Next

            ' retornar el objeto
            getList = ThirdList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objThird = Nothing
            ThirdList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Third
    ''' </summary>
    ''' <param name="Third"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Third As ThirdEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Third SET")
            sql.AppendLine(" code = '" & Third.code & "',")
            sql.AppendLine(" name = '" & Third.name & "',")
            sql.AppendLine(" contact = '" & Third.contact & "',")
            sql.AppendLine(" documents = '" & Third.documents & "',")
            sql.AppendLine(" phone = '" & Third.phone & "',")
            sql.AppendLine(" email = '" & Third.email & "',")
            sql.AppendLine(" actions = ' ',")
            sql.AppendLine(" experiences = '" & Third.experiences & "',")
            sql.AppendLine(" RepresentanteLegal = '" & Third.representantelegal & "',")
            sql.AppendLine(" PersonaNatural = '" & Third.PersonaNatural & "',")
            sql.AppendLine(" enabled = '" & Third.enabled & "',")
            sql.AppendLine(" iduser = '" & Third.iduser & "',")
            sql.AppendLine(" createdate = '" & Third.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & Third.id)

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
            Throw New Exception("Error al modificar el Third. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    Public Function update_add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Third As ThirdEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Third SET")
            sql.AppendLine(" contact = '" & Third.contact & "',")
            sql.AppendLine(" documents = '" & Third.documents & "',")
            sql.AppendLine(" phone = '" & Third.phone & "',")
            sql.AppendLine(" email = '" & Third.email & "'")
            sql.AppendLine("WHERE id = " & Third.id)

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
            Throw New Exception("Error al modificar el Third. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Third de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThird As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Third ")
            SQL.AppendLine(" where id = '" & idThird & "' ")

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
            Throw New Exception("Error al elimiar el Third. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    Public Function getListPersonas(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                Optional ByVal personanatural As String = "") As List(Of ThirdEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objThird As ThirdEntity
        Dim ThirdList As New List(Of ThirdEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia

            sql.Append(" select Third.id,Third.Name, Third.PersonaNatural from Third ")
            sql.Append(" where Third.PersonaNatural = " & personanatural)
            sql.Append(" order by (Name) ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objThird = New ThirdEntity

                ' cargar el valor del campo
                objThird.id = row("id")
                objThird.name = row("name")

                ' agregar a la lista
                ThirdList.Add(objThird)

            Next

            ' retornar el objeto
            getListPersonas = ThirdList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objThird = Nothing
            ThirdList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

End Class
