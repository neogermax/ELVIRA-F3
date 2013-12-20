Imports Microsoft.VisualBasic
Imports System.data
Imports Gattaca.Application.Credentials
Imports System.Collections.Generic
Imports Gattaca.Application.ExceptionManager

Public Class UserGroupDALC

    ' contantes
    Const MODULENAME As String = "UserGroupDALC"

#Region "Metodos de Persistencia"

    '*******************************************************************
    'Objetivo:      Regresa un objetocon la información básica de un usuario
    'Entradas:      Credenciales, Id del Usuario Solicitado
    'Salidas:       Objeto tipo 
    'Autor:         Diego Armando Gomez
    'creado:        09/04/2008
    '********************************************************************
    Public Function GetUsersGroupList(ByVal objApplicationCredentials As ApplicationCredentials, _
                                    Optional ByVal order As String = "Name", _
                                    Optional ByVal enabledOnly As Boolean = True) As List(Of UserGroupEntity)

        ' definiendo los objetos
        Dim dtUser As DataTable
        Dim sql As New StringBuilder
        Dim usersGroupList As New List(Of UserGroupEntity)

        ' construir la sentencia
        sql.AppendLine(" SELECT * ")
        sql.AppendLine(" FROM UserGroup ")

        If enabledOnly Then
            ' solo los activos
            sql.AppendLine(" WHERE Enabled = 'T' ")

        End If

        'ordenar
        sql.AppendLine(" ORDER BY " & order)

        Try

            ' cargando los datos basicos del usuario
            dtUser = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString, sProductName:="VBSecurity")

            For Each row As DataRow In dtUser.Rows

                ' definir el objeto
                Dim objUserGroup As New UserGroupEntity

                ' si hay registros
                objUserGroup.iID = Gattaca.Application.Tools.Formatter.GetInteger(row("ID"))
                objUserGroup.sCode = Gattaca.Application.Tools.Formatter.GetString(row("Code")).Trim()
                objUserGroup.sName = Gattaca.Application.Tools.Formatter.GetString(row("Name")).Trim()
                objUserGroup.bIsEnabled = Gattaca.Application.Tools.Formatter.GetBoolean(row("Enabled"))


                ' agregar a la lista
                usersGroupList.Add(objUserGroup)

            Next

            ' retornar
            GetUsersGroupList = usersGroupList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los parametros de la aplicacion. ")

        Finally
            ' liberar recursos
            sql = Nothing
            dtUser = Nothing
            usersGroupList = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Obtiene los grupos a los que pertenece un usuario
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getUserGroups(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of UsersByGroupEntity)

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim data As DataTable
        Dim UserByGroupList As New List(Of UsersByGroupEntity)

        Try
            ' construir la sentencia
            'sSQL.Append(" SELECT IDApplicationUser, code ")
            'sSQL.Append(" FROM UserGroup , UsersByGroup ")
            'sSQL.Append(" WHERE ID = IDUserGroup ")
            'sSQL.Append(" AND IDApplicationUser = '" & objApplicationCredentials.UserID & "'")
            'sSQL.Append(" AND Enabled= 'T'")

            sSQL.Append(" SELECT IdUserGroup ")
            sSQL.Append(" FROM UsersByGroup ")
            sSQL.Append(" WHERE IDApplicationUser = '" & objApplicationCredentials.UserID & "'")
            sSQL.Append(" AND IDUserGroup IN (SELECT IdUserGroup  ")
            sSQL.Append(" 					FROM MenusByUserGroup ")
            sSQL.Append(" 					GROUP BY IdUserGroup) ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sDatabase:="DB99")

            'Hacer la lista
            For Each row As DataRow In data.Rows

                ' definir el objeto
                Dim objUserGroups As New UsersByGroupEntity

                ' si hay registros
                objUserGroups.code = row("IdUserGroup").ToString

                ' agregar a la lista
                UserByGroupList.Add(objUserGroups)

            Next

            ' retornar
            getUserGroups = UserByGroupList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getUserGroups")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los grupos del usuario.")

        Finally
            ' liberando recursos
            sSQL = Nothing
            data = Nothing

        End Try

    End Function


    '''' <summary>
    '''' obtener la lista de locaclidades por un usuario
    '''' </summary>
    '''' <param name="objApplication"></param>
    '''' <param name="idApplicationUser"></param>
    '''' <param name="order"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function GetUserGroupByUser(ByVal objApplication As ApplicationCredentials, _
    '                            ByVal idApplicationUser As Long, _
    '                            Optional ByVal order As String = "Name") As Collection

    '    ' definiendo los objetos
    '    Dim dtUser As DataTable
    '    Dim sql As New StringBuilder
    '    GetUserGroupByUser = New Collection

    '    ' construir la sentencia
    '    sql.AppendLine(" SELECT UserGroup.* ")
    '    sql.AppendLine(" FROM UserGroup ")
    '    sql.AppendLine(" INNER JOIN UsersByGroup ON UserGroup.Id = UsersByGroup.IdUserGroup ")
    '    sql.AppendLine(" WHERE UsersByGroup.IdApplicationUser = '" & idApplicationUser & "'")

    '    'ordenar
    '    sql.AppendLine(" ORDER BY " & order)

    '    Try

    '        ' cargando los datos basicos del usuario
    '        dtUser = GattacaApplication.RunSQLReturnDataTable(objApplication, sql.ToString)

    '        For Each row As DataRow In dtUser.Rows

    '            '' definir el objeto
    '            'Dim objUserGroup As New Gattaca.Entity.eSecurity.UserGroupEntity

    '            '' si hay registros
    '            'objUserGroup.ID = Gattaca.Application.Tools.Formatter.GetInteger(row("ID"))
    '            'objUserGroup.Code = Gattaca.Application.Tools.Formatter.GetString(row("Code")).Trim()
    '            'objUserGroup.Name = Gattaca.Application.Tools.Formatter.GetString(row("Name")).Trim()
    '            'objUserGroup.IsEnabled = Gattaca.Application.Tools.Formatter.GetBoolean(row("Enabled"))

    '            '' agregar a la lista
    '            'GetUserGroupByUser.Add(objUserGroup)

    '        Next

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al cargar los parametros de la aplicacion. ")

    '    Finally
    '        ' liberar recursos
    '        sql = Nothing
    '        dtUser = Nothing

    '    End Try

    'End Function

    '''' <summary>
    '''' Objetivo:  Agregar un usuario a un grupo
    '''' Entradas:  Objeto tipo registro
    '''' Pre:       Debe estar cargado el objeto.   
    '''' Autor:     Diego Armando Gomez
    '''' Fecha:     08/08/2007    
    '''' </summary>
    '''' <param name="objApplicationCredentials"></param>
    '''' <param name="IdApplicationUser"></param>
    '''' <param name="IdUserGroup"></param>
    '''' <remarks></remarks>
    'Public Sub AddUserToGroup(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                    ByVal IdApplicationUser As Long, _
    '                    ByVal IdUserGroup As Long)

    '    ' definiendo los objtos
    '    Dim sql As New StringBuilder

    '    ' eliminar los grupos a los que pertenezca el usuario
    '    sql.Append(" DELETE FROM UsersByGroup")
    '    sql.Append(" WHERE IdApplicationUser = '" & IdApplicationUser & "'")

    '    Try

    '        ' ejecutar la intruccion
    '        GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

    '        ' limpiar el sql
    '        sql.Remove(0, sql.Length)

    '        ' construir la sentencia
    '        sql.Append("INSERT INTO UsersByGroup (IdApplicationUser,IdUserGroup) ")
    '        sql.Append("VALUES (")
    '        sql.Append("'" & IdApplicationUser & "',")
    '        sql.Append("'" & IdUserGroup & "')")

    '        ' ejecutar la intruccion
    '        GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

    '        ' almacenar
    '        'JournalInsert(objApplicationCredentials, 1, "AddUserToGroup")

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()


    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al cargar los parametros de la aplicacion. ")

    '    Finally
    '        ' liberando recursos
    '        sql = Nothing

    '    End Try

    'End Sub


    '''' <summary>
    '''' Objetivo:      
    '''' Entradas:      
    '''' Salidas:       Objeto tipo 
    '''' Autor:         Diego Armando Gomez
    '''' Creado:        09/04/2008
    '''' </summary>
    '''' <param name="objApplication"></param>
    '''' <param name="iId"></param>
    '''' <remarks></remarks>
    'Public Sub DeleteUserGroupsByUser(ByVal objApplication As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                    ByVal iId As Integer)

    '    ' definiendo los objtos
    '    Dim sSQL As New StringBuilder

    '    ' construir la sentencia
    '    sSQL.AppendLine(" Delete FROM UsersByGroup")
    '    sSQL.AppendLine(" WHERE IDApplicationUser = " & iId)

    '    Try
    '        'obtener el requerimiento
    '        GattacaApplication.RunSQL(objApplication, sSQL.ToString)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar 
    '        Gattaca.Application.ExceptionPublisher.Publish(New Exception("Error. " & ex.Message()), 1, MODULENAME, "DeleteUserGroupsByUser", sSQL.ToString)

    '        ' subir el error de nivel
    '        Throw New Exception("Error al borrar gruopos asociados al usuario")

    '    Finally
    '        ' liberando recursos
    '        sSQL = Nothing

    '    End Try

    'End Sub

    '''' <summary>
    '''' Objetivo:      
    '''' Entradas:      
    '''' Salidas:       Objeto tipo 
    '''' Autor:         Diego Armando Gomez
    '''' Creado:        07/04/2008
    '''' </summary>
    '''' <param name="objApplication"></param>
    '''' <param name="iId"></param>
    '''' <remarks></remarks>
    'Public Sub AddUserGroupsByUser(ByVal objApplication As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                    ByVal iId As Integer, _
    '                    ByVal list As String)

    '    ' definiendo los objtos
    '    Dim sSQL As New StringBuilder

    '    Try

    '        If Not list.Equals("") Then

    '            For Each idUserGroup As String In list.Split(",")

    '                ' construir la sentencia
    '                sSQL.AppendLine(" INSERT INTO UsersByGroup(IDApplicationUser,IDUserGroup)")
    '                sSQL.AppendLine(" VALUES (" & iId & "," & idUserGroup & ")")

    '            Next

    '            'obtener el requerimiento
    '            GattacaApplication.RunSQL(objApplication, sSQL.ToString)

    '            ' finalizar la transaccion
    '            CtxSetComplete()

    '        End If

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar 
    '        Gattaca.Application.ExceptionPublisher.Publish(New Exception("Error. " & ex.Message()), 1, MODULENAME, "AddUserGroupsByUser", sSQL.ToString)

    '        ' subir el error de nivel
    '        Throw New Exception("Error al crear los grupos asociados a un usuario")

    '    Finally
    '        ' liberando recursos
    '        sSQL = Nothing

    '    End Try

    'End Sub


    ''' <summary>
    ''' determina si el usuario es administrador o no
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isAdministrator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal idUser As Integer) As Boolean

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim data As DataTable

        Try
            ' construir la sentencia
            sSQL.Append(" SELECT COUNT(*) AS Value")
            sSQL.Append(" FROM UsersByGroup ")
            sSQL.Append(" WHERE IDUserGroup = 1")
            sSQL.Append("   AND IDApplicationUser = '" & idUser & "'")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' retornar
            isAdministrator = CBool(data.Rows(0)("Value"))

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "isAdministrator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los grupos del usuario.")

        Finally
            ' liberando recursos
            sSQL = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Verificar si un usuario tiene acceso a una interface
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkAccess(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal idForm As String) As Boolean

        ' definiendo los objetos
        Dim sSQL As New StringBuilder
        Dim data As DataTable

        ' no tiene permisos
        checkAccess = False

        Try
            ' construir la sentencia
            sSQL.Append(" SELECT COUNT(*) AS TOTAL ")
            sSQL.Append(" FROM UsersByGroup  ")
            sSQL.Append(" WHERE IDApplicationUser = '" & objApplicationCredentials.UserID & "'")
            sSQL.Append(" AND IDUserGroup IN (SELECT IdUserGroup ")
            sSQL.Append(" 					FROM MenusByUserGroup ")
            sSQL.Append(" 					WHERE IdMenu = (SELECT IdParent  ")
            sSQL.Append(" 									FROM Menu ")
            sSQL.Append(" 										INNER JOIN MenuHierarchy ON Menu.id = MenuHierarchy.IdMenu ")
            sSQL.Append(" 									WHERE Id = '" & idForm & "')) ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sDatabase:="DB99")

            If data.Rows.Count > 0 Then

                ' verificar si tiene permisos
                If CInt(data.Rows(0)("TOTAL").ToString) > 0 Then

                    ' Tiene Permisos
                    checkAccess = True

                End If

            End If

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el erro
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "checkAccess")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los permisos del usuario.")

        Finally
            ' liberando recursos
            sSQL = Nothing
            data = Nothing

        End Try

    End Function


    '*******************************************************************
    'Objetivo:      Regresa un objetocon la información básica de un usuario
    'Entradas:      Credenciales, Id del Usuario Solicitado
    'Salidas:       Objeto tipo 
    'Autor:         Diego Armando Gomez
    'creado:        09/04/2008
    '********************************************************************
    Public Function GetMenuUsersGroupList(ByVal objApplicationCredentials As ApplicationCredentials, _
                                    Optional ByVal order As String = "IdUserGroup", _
                                    Optional ByVal enabledOnly As Boolean = True) As List(Of UserGroupEntity)

        ' definiendo los objetos
        Dim dtUser As DataTable
        Dim sql As New StringBuilder
        Dim usersGroupList As New List(Of UserGroupEntity)

        ' construir la sentencia
        sql.AppendLine(" SELECT DISTINCT IdUserGroup ")
        sql.AppendLine(" FROM MenusByUserGroup ")
        'ordenar
        sql.AppendLine(" ORDER BY " & order)

        Try

            ' cargando los datos basicos del usuario
            dtUser = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString, sProductName:="VBSecurity")

            For Each row As DataRow In dtUser.Rows

                ' definir el objeto
                Dim objUserGroup As New UserGroupEntity

                ' si hay registros
                objUserGroup.iID = Gattaca.Application.Tools.Formatter.GetInteger(row("IdUserGroup"))

                ' agregar a la lista
                usersGroupList.Add(objUserGroup)

            Next

            ' retornar
            GetMenuUsersGroupList = usersGroupList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenuUsersGroupList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los menus. ")

        Finally
            ' liberar recursos
            sql = Nothing
            dtUser = Nothing
            usersGroupList = Nothing

        End Try

    End Function

#End Region

End Class

