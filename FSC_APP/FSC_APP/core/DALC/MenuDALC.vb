Option Strict On

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports System.IO
Imports System.Web
Imports System.Collections
Imports Gattaca.Application.ExceptionManager
Imports System.Text

Public Class MenuDALC

    ' contantes
    Const MODULENAME As String = "MenuDALC"

#Region "persistance managing method"

    ''' <summary>
    ''' Objetivo:  Construir una Menu y cargarla
    ''' Entradas:  El Id
    ''' Autor:     Diego Armando Gomez
    ''' Fecha:     09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal iId As Integer) As MenuEntity
        ' definiendo los objtos
        Dim dtMenu As DataTable = Nothing
        Dim sSQL As New StringBuilder
        Dim objMenu As New MenuEntity

        ' construir la sentencia
        sSQL.Append("SELECT * FROM Menu ")
        sSQL.Append(" WHERE ID = " & iId)

        Try
            'obtener la Menu
            dtMenu = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            If dtMenu.Rows.Count > 0 Then

                ' cargar los datos en el objeto
                objMenu.iId = Gattaca.Application.Tools.Formatter.GetInteger(dtMenu.Rows(0)("ID"))
                objMenu.sTextField = Gattaca.Application.Tools.Formatter.GetString(dtMenu.Rows(0)("TextField"))
                objMenu.sURL = Gattaca.Application.Tools.Formatter.GetString(dtMenu.Rows(0)("URL"))
                objMenu.sEnabled = Gattaca.Application.Tools.Formatter.GetString(dtMenu.Rows(0)("Enabled"))
                objMenu.iSortOrden = Gattaca.Application.Tools.Formatter.GetInteger(dtMenu.Rows(0)("SortOrder"))

            End If

            ' regresar el objteto
            Return objMenu

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un menu")

        Finally
            ' liberando recursos
            objMenu = Nothing
            sSQL = Nothing
            dtMenu = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Objetivo:  Agregar un menu
    ''' Entradas:  
    ''' Pre:       Debe estar cargado el objeto.   
    ''' Autor:     Diego Armando Gomez
    ''' Fecha:     09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="objMenu"></param>
    ''' <remarks></remarks>
    Public Sub Add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal objMenu As MenuEntity)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        ' construir la sentencia
        sSQL.Append("INSERT INTO Menu (TextField,URL,Enabled,SortOrder) ")
        sSQL.Append("VALUES (")
        sSQL.Append("'" & objMenu.sTextField & "',")
        sSQL.Append("'" & objMenu.sURL & "',")
        sSQL.Append("'" & objMenu.sEnabled & "',")
        sSQL.Append("'" & objMenu.iSortOrden & "')")

        Try
            ' ejecutar la intruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "Add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear una menu")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Objetivo:  Modificar los datos de una Menu
    ''' Entradas:  
    ''' Pre:       Deben estar cargados los valores del objeto.   
    ''' Autor:     Diego Armando Gomez
    ''' Fecha:     09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="objMenu"></param>
    ''' <remarks></remarks>
    Public Sub Update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal objMenu As MenuEntity)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        ' construir la sentencia
        sSQL.Append("UPDATE Menu SET ")
        sSQL.Append(" TextField='" & objMenu.sTextField & "',")
        sSQL.Append(" URL='" & objMenu.sURL & "',")
        sSQL.Append(" Enabled='" & objMenu.sEnabled & "',")
        sSQL.Append(" SortOrder='" & objMenu.iSortOrden & "'")
        sSQL.Append(" WHERE id = " & objMenu.iId)

        Try
            ' ejecutar la intruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "Update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar una Menu")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Objetivo:  Eliminar un Menu
    ''' Entradas: 
    ''' Pre:       Deben estar cargados los valores del objeto.   
    ''' Autor:     Diego Armando Gomez
    ''' Fecha:     09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <remarks></remarks>
    Public Sub Delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal iId As Integer)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        ' construir la sentencia
        sSQL.Append("DELETE FROM Menu ")
        sSQL.Append(" WHERE ID = " & iId)

        Try
            ' ejecutar la intruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "Delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar un Menu")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="textField"></param>
    ''' <param name="url"></param>
    ''' <param name="enabled"></param>
    ''' <param name="sortOrden"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenuList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                Optional ByVal id As String = "", _
                                Optional ByVal textField As String = "", _
                                Optional ByVal url As String = "", _
                                Optional ByVal enabled As String = "", _
                                Optional ByVal sortOrder As String = "", _
                                Optional ByVal order As String = "sortOrder, textField") As List(Of MenuEntity)
        ' definiendo los objtos
        Dim sSQL As New StringBuilder
        Dim sWhere = " WHERE "
        Dim coll As New List(Of MenuEntity)
        Dim dtMenu As DataTable
        Dim objMenu As MenuEntity

        ' construir la sentencia
        sSQL.Append(" SELECT *")
        sSQL.Append(" FROM Menu ")

        ' verificar si es por nombre
        If id <> "" Then
            sSQL.Append(sWhere & " id = '" & id & "'")
            sWhere = " AND "

        End If

        ' verificar si es por nombre
        If textField <> "" Then
            sSQL.Append(sWhere & " textField like '%" & textField & "%'")
            sWhere = " AND "

        End If

        ' verificar si es por url
        If url <> "" Then
            sSQL.Append(sWhere & " url like '%" & url & "%'")
            sWhere = " AND "

        End If

        ' verificar si es por url
        If enabled <> "" Then
            sSQL.Append(sWhere & " enabled like '%" & enabled & "%'")
            sWhere = " AND "

        End If

        ' verificar si es por url
        If sortOrder <> "" Then
            sSQL.Append(sWhere & " sortOrder = '" & sortOrder & "'")
            sWhere = " AND "

        End If

        ' ordenando
        sSQL.Append(" Order By " & order)

        Try
            'obtener la Menu
            dtMenu = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            For Each row As DataRow In dtMenu.Rows

                ' crear
                objMenu = New MenuEntity

                ' cargar los datos en el objeto
                objMenu.iId = Gattaca.Application.Tools.Formatter.GetInteger(row("ID"))
                objMenu.sTextField = Gattaca.Application.Tools.Formatter.GetString(row("TextField"))
                objMenu.sURL = Gattaca.Application.Tools.Formatter.GetString(row("URL"))
                objMenu.sEnabled = Gattaca.Application.Tools.Formatter.GetString(row("Enabled"))
                objMenu.iSortOrden = Gattaca.Application.Tools.Formatter.GetInteger(row("SortOrder"))

                ' agregar
                coll.Add(objMenu)

            Next

            ' regresar el objteto
            Return coll

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenuList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Menus")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="Id"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMenusByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal Id As Integer, _
                                    Optional ByVal order As String = "idParent") As List(Of MenuEntity)
        ' definiendo los objtos
        Dim sSQL As New StringBuilder
        Dim sWhere = " WHERE "
        Dim coll As New List(Of MenuEntity)
        Dim dtMenu As DataTable
        Dim objMenu As MenuEntity

        ' construir la sentencia
        sSQL.Append(" SELECT Menu.* ")
        sSQL.Append(" FROM MenuHierarchy ")
        sSQL.Append(" INNER JOIN Menu ON MenuHierarchy.IdMenu = Menu.Id ")
        sSQL.Append(" WHERE idParent = " & Id)

        ' ordenando
        sSQL.Append(" ORDER BY " & order)

        Try
            'obtener la Menu
            dtMenu = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            For Each row As DataRow In dtMenu.Rows

                ' crear
                objMenu = New MenuEntity

                ' cargar los datos en el objeto
                objMenu.iId = Gattaca.Application.Tools.Formatter.GetInteger(row("ID"))
                objMenu.sTextField = Gattaca.Application.Tools.Formatter.GetString(row("TextField"))
                objMenu.sURL = Gattaca.Application.Tools.Formatter.GetString(row("URL"))
                objMenu.sEnabled = Gattaca.Application.Tools.Formatter.GetString(row("Enabled"))
                objMenu.iSortOrden = Gattaca.Application.Tools.Formatter.GetInteger(row("SortOrder"))

                ' agregar
                coll.Add(objMenu)

            Next

            ' regresar el objteto
            Return coll

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenusByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Menus hijos")

        Finally
            ' liberando recursos
            sSQL = Nothing
            sWhere = Nothing
            coll = Nothing
            dtMenu = Nothing
            objMenu = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Objetivo:      eliminar la información básica de un usuario
    ''' Entradas:      Cliente, id del usuario que ejecuta la aplicacion, id del usuario a eliminar 
    ''' Salidas:       Objeto tipo 
    ''' Autor:         Diego Armando Gomez
    ''' Creado:        09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <remarks></remarks>
    Public Sub DeleteMenusByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal iId As Integer)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        ' construir la sentencia
        sSQL.AppendLine(" Delete FROM MenuHierarchy")
        sSQL.AppendLine(" WHERE idParent = " & iId)

        Try
            'obtener el requerimiento
            GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "DeleteMenusByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al borrar Menues asociados")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Objetivo:      eliminar la información básica de un usuario
    ''' Entradas:      Cliente, id del usuario que ejecuta la aplicacion, id del usuario a eliminar 
    ''' Salidas:       Objeto tipo 
    ''' Autor:         Diego Armando Gomez
    ''' Creado:        07/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <remarks></remarks>
    Public Sub AddMenusByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal iId As Integer, _
                        ByVal list As String)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        Try

            If Not list.Equals("") Then

                For Each idMenu As String In list.Split(",".ToCharArray)

                    ' construir la sentencia
                    sSQL.AppendLine(" INSERT INTO MenuHierarchy(IDMenu,idParent)")
                    sSQL.AppendLine(" VALUES (" & idMenu & "," & iId & ")")

                Next

                'obtener el requerimiento
                GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

                ' finalizar la transaccion
                CtxSetComplete()

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "AddMenusByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear menues asociados")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    '*******************************************************************
    'Objetivo:      Regresa un objetocon la información básica de un usuario
    'Entradas:      Credenciales, Id del Usuario Solicitado
    'Salidas:       Objeto tipo 
    'Autor:         Diego Armando Gomez
    'creado:        09/04/2008
    '********************************************************************
    Public Function GetUsersByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal id As Integer, _
                                    Optional ByVal order As String = "Name") As List(Of UserGroupEntity)

        ' definiendo los objetos
        Dim dtUser As DataTable
        Dim sql As New StringBuilder
        Dim usersGroupList As New List(Of UserGroupEntity)

        ' construir la sentencia
        sql.AppendLine(" SELECT * ")
        sql.AppendLine(" FROM UserGroup ")
        sql.AppendLine(" INNER JOIN MenusByUserGroup ON UserGroup.Id = MenusByUserGroup.IdUserGroup ")
        sql.AppendLine(" WHERE idMenu = " & id)

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
            GetUsersByMenu = usersGroupList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetUsersByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

        Finally
            ' liberar recursos
            sql = Nothing
            dtUser = Nothing
            usersGroupList = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Objetivo:      eliminar la información básica de un usuario
    ''' Entradas:      Cliente, id del usuario que ejecuta la aplicacion, id del usuario a eliminar 
    ''' Salidas:       Objeto tipo 
    ''' Autor:         Diego Armando Gomez
    ''' Creado:        09/04/2008
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iId"></param>
    ''' <remarks></remarks>
    Public Sub DeleteUserGroupsByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal iId As Integer)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        ' construir la sentencia
        sSQL.AppendLine(" Delete FROM MenusByUserGroup")
        sSQL.AppendLine(" WHERE idMenu = " & iId)

        Try
            'obtener el requerimiento
            GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "DeleteUserGroupsByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al borrar grupos de usuarios asociados a un menu")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Objetivo:      eliminar la información básica de un usuario
    ''' Entradas:      Cliente, id del usuario que ejecuta la aplicacion, id del usuario a eliminar 
    ''' Salidas:       Objeto tipo 
    ''' Autor:         Diego Armando Gomez
    ''' Creado:        07/04/2008
    ''' </summary>
    ''' <param name="objApplication"></param>
    ''' <param name="iId"></param>
    ''' <remarks></remarks>
    Public Sub AddUserGroupsByMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                        ByVal iId As Integer, _
                        ByVal list As String)

        ' definiendo los objtos
        Dim sSQL As New StringBuilder

        Try

            If Not list.Equals("") Then

                For Each idUserGroup As String In list.Split(",".ToCharArray)

                    ' construir la sentencia
                    sSQL.AppendLine(" INSERT INTO MenusByUserGroup(IDUserGroup,idMenu)")
                    sSQL.AppendLine(" VALUES (" & idUserGroup & "," & iId & ")")

                Next

                'obtener el requerimiento
                GattacaApplication.RunSQL(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

                ' finalizar la transaccion
                CtxSetComplete()

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "AddUserGroupsByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al crear los grupos de usuarios asociados a un menu")

        Finally
            ' liberando recursos
            sSQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildApplicationMenus(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim sFileName As New StringBuilder
        Dim sql As New StringBuilder
        Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
        Dim groupList As New List(Of UserGroupEntity)
        Dim listG As String = ""
        Dim menusColls As Collection

        ' obteniendo la lista de los usuarios
        groupList = objUserGroupDALC.GetUsersGroupList(objApplicationCredentials, enabledOnly:=True)

        For Each userGroup As UserGroupEntity In groupList

            listG += userGroup.iID & ","

        Next

        ' obtener las permitaciones
        menusColls = permutar(listG.Trim().Split(",".ToCharArray))

        For Each menu As String In menusColls

            If Not menu.Equals("") Then

                ' ordenar
                menu = ordenar(menu.Trim(",".ToCharArray))

                ' cargando los datos basicos del usuario
                Dim dt As DataTable = GattacaApplication.RunSQLRDT(objApplicationCredentials, buildSqlByCombinationGroup(menu), sProductName:="VBSecurity")

                ' contruir el menu para cada objeto
                buildMenu(objApplicationCredentials, menu, dt, sMenuPath)

            End If

        Next

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idUserGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function buildSqlByCombinationGroup(ByVal idUserGroup As String) As String

        ' definir los objetos
        Dim sql As New StringBuilder

        ' contriur menu
        sql.Append(" SELECT * ")
        sql.Append(" FROM  ")
        sql.Append(" (SELECT Parent.TextField AS ParentTextField, Parent.URL AS ParentURL, Childs.TextField AS ChildsTextField,   ")
        sql.Append(" Childs.URL AS ChildsURL,   ")
        sql.Append(" 	Parent.SortOrder AS ParentSortOrder, Childs.SortOrder AS ChildsSortOrder  ")
        sql.Append(" FROM MenusByUserGroup  ")
        sql.Append(" INNER JOIN (SELECT * FROM Menu  ")
        sql.Append(" WHERE Menu.Id Not In (  ")
        sql.Append(" SELECT Idmenu FROM MenuHierarchy)) Parent ON MenusByUserGroup.Idmenu = Parent.Id  ")
        sql.Append(" INNER JOIN (SELECT Id, TextField, URL, SortOrder, MenuHierarchy.IdParent  ")
        sql.Append(" FROM menu  ")
        sql.Append(" INNER JOIN MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu  ")
        sql.Append(" WHERE Menu.Enabled = 'T') Childs ON Parent.Id = Childs.IdParent  ")
        sql.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & idUserGroup & ")")
        sql.Append(" union   ")
        sql.Append(" SELECT Menu.TextField as ParentTextField, Menu.URL AS ParentURL, null AS ChildsTextField, null AS ChildsURL,   ")
        sql.Append(" 	Menu.SortOrder AS ParentSortOrder, null AS ChildsSortOrder  ")
        sql.Append(" FROM Menu  ")
        sql.Append(" INNER JOIN MenusByUserGroup ON MenusByUserGroup.IdMenu = Menu.Id  ")
        sql.Append(" WHERE Menu.Id Not In (SELECT IdMenu FROM MenuHierarchy) ")
        sql.Append(" And Menu.Id IN (SELECT IdMenu FROM MenusByUserGroup WHERE IdUserGroup IN (" & idUserGroup & "))")
        sql.Append(" ) Temp  ")
        sql.Append(" ORDER BY ParentSortOrder, ChildsSortOrder ")

        ' retornar el menu
        Return sql.ToString

    End Function

    Private Function buildSqlByCombinationGroup2(ByVal idUserGroup As String) As String

        ' definir los objetos
        Dim sSQL As New StringBuilder

        ' contriur menu
        sSQL.Append(" Select *")
        sSQL.Append(" FROM(")
        sSQL.Append(" SELECT     Menu.Id AS MenuId, Menu.TextField AS Description, Menu.URL AS Url, ")
        sSQL.Append(" Menu.Enabled AS Enabled, Menu.SortOrder AS Position,")
        sSQL.Append(" CASE WHEN MenuHierarchy.IdParent IS null THEN Menu.Id ELSE MenuHierarchy.IdParent END AS IdParent")
        sSQL.Append(" FROM         Menu LEFT OUTER JOIN")
        sSQL.Append(" MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu")
        sSQL.Append(" ) AS T")
        sSQL.Append(" WHERE (t.MenuId IN (SELECT     Menu.Id AS MenuId")
        sSQL.Append(" FROM         Menu INNER JOIN MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu")
        sSQL.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & idUserGroup & ")) OR t.IdParent IN (SELECT     Menu.Id AS MenuId")
        sSQL.Append(" FROM         Menu INNER JOIN MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu")
        sSQL.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & idUserGroup & "))) AND T.Enabled = 'T'")
        sSQL.Append(" ORDER BY t.Position")

        ' retornar el menu
        Return sSQL.ToString

    End Function

    Private Function buildSqlByCombinationGroup3(ByVal idUserGroup As String) As String

        ' definir los objetos
        Dim sSQL As New StringBuilder

        ' contriur menu

        sSQL.Append(" SELECT     Menu.Id AS MenuId, Menu.TextField AS Description, Menu.URL AS Url, Menu.Enabled AS Enabled, Menu.SortOrder AS Position,")
        sSQL.Append(" MenusByUserGroup.IdUserGroup")
        sSQL.Append(" FROM         Menu INNER JOIN")
        sSQL.Append(" MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu")
        sSQL.Append(" WHERE     MenusByUserGroup.IdUserGroup IN (" & idUserGroup & ") OR Menu.Enabled = 'T'")
        sSQL.Append(" ORDER BY Menu.SortOrder")
        'sSQL.Append(" SELECT     ")
        'sSQL.Append(" Menu.Id AS MenuId, ")
        'sSQL.Append(" Menu.TextField AS Description, ")
        'sSQL.Append(" Menu.URL AS Url, ")
        'sSQL.Append(" Menu.Enabled AS Enabled, ")
        'sSQL.Append(" Menu.SortOrder AS Position,")
        'sSQL.Append(" CASE WHEN MenuHierarchy.IdParent IS NULL THEN Menu.Id ELSE MenuHierarchy.IdParent END AS IdParent, ")
        'sSQL.Append(" Case WHEN MenusByUserGroup.IdUserGroup IS NULL THEN -1 ELSE MenusByUserGroup.IdUserGroup END AS IdUserGroup ")
        'sSQL.Append(" FROM")
        'sSQL.Append(" Menu LEFT OUTER JOIN")
        'sSQL.Append(" MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu LEFT OUTER JOIN")
        'sSQL.Append(" MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu")
        'sSQL.Append(" WHERE")
        'sSQL.Append(" (MenusByUserGroup.IdUserGroup IN (" & idUserGroup & ") OR MenusByUserGroup.IdUserGroup IS NULL) ")
        'sSQL.Append(" AND (Menu.Enabled = 'T')")
        'sSQL.Append(" ORDER BY Menu.SortOrder")


        ' retornar el menu
        Return sSQL.ToString

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="groups"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function permutar(ByVal groups() As String) As Collection

        ' definir los objetos
        Dim coll As New Collection

        ' agregar el primer elemento
        coll.Add(groups(0))

        ' para todos los elementos
        For i As Integer = 1 To groups.Length - 1

            ' combinarlos
            coll = combinar(groups(i), coll)

        Next

        ' retornarlo
        permutar = coll

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="element"></param>
    ''' <param name="coll"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function combinar(ByVal element As String, _
                    ByVal coll As Collection) As Collection

        ' definir la nueva colleccion
        Dim coll2 As New Collection

        For Each el As String In coll

            ' agregar todos los elementos existentes
            coll2.Add(el & "," & element)

        Next

        For Each el As String In coll2

            ' concatenar el nuevo elemento todos los elementos existentes
            coll.Add(el)

        Next

        ' agregar el elemento
        coll.Add(element)

        ' retornar la collecion generada
        Return coll

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="menu"></param>
    ''' <param name="dt"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Private Sub buildMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal menu As String, _
                                ByVal dt As DataTable, _
                                ByVal sMenuPath As String)

        ' definiendo los objtos
        Dim sMenu As New StringBuilder
        Dim sFileName As New StringBuilder

        Try

            ' encabezado
            sMenu.Append("<?xml version=""1.0"" encoding=""iso-8859-1"" ?>")
            sMenu.Append(vbCrLf)
            sMenu.Append("<menu>")

            If dt.Rows.Count > 0 Then

                Dim sEndNode As String = ""

                For Each drRow As DataRow In dt.Rows

                    ' verificar si es un nuevo nodo
                    If IsDBNull(drRow("ChildsTextField")) Then

                        ' cerrar el nodo anterior
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)
                        sEndNode = "</MenuItem>"

                        ' agregar nodo padre
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drRow("ParentTextField"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drRow("ParentTextField"))
                        'sMenu.Append(""" NavigateUrlField="""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drRow("ParentURL"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")
                        sMenu.Append(vbCrLf)
                        ' ir al sigiuente nodo
                        'Continue For

                    Else
                        ' agregar nodo hijo
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drRow("ChildsTextField"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drRow("ChildsTextField"))
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drRow("ChildsURL"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")

                        ' cerrar el nodo
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)

                    End If

                Next

            End If

            ' cerrar el archivo
            sMenu.Append(" </MenuItem></menu>")

            ' contruir elnombre
            sFileName.Append("mMenu")
            sFileName.Append(menu.Replace(",", ""))
            sFileName.Append(".xml")

            ' Abre de escritura, indica que las nuevas líneas deberán ser adicionadas (Append). También especifica formato de caracteres UTF8.
            Dim sNewDetailXML As StreamWriter = New StreamWriter(sMenuPath & sFileName.ToString, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

            ' Adiciona el string.
            sNewDetailXML.WriteLine(sMenu)

            ' Cierra el Stream.
            sNewDetailXML.Close()

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sMenu = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="menuList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ordenar(ByVal menuList As String) As String

        ' definir los objetos
        Dim menu As String = ""
        Dim list() As String = menuList.Split(",".ToCharArray)

        ' ordernar por burbuja
        For i As Integer = 0 To list.Length - 1

            For j As Integer = i To list.Length - 2

                If CInt(list(i)) > CInt(list(j + 1)) Then

                    Dim temp As String = list(i)
                    list(i) = list(j + 1)
                    list(j + 1) = temp

                End If

            Next

        Next

        ' armar de nuevo
        For i As Integer = 0 To list.Length - 1

            menu += list(i) & ","

        Next

        ' retornar
        Return menu.Trim(",".ToCharArray)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iIdUser"></param>
    ''' <param name="sIdUserGroupList"></param>
    ''' <param name="sMenuPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function writeMenuToUser(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal iIdUser As Integer, _
                                    ByVal sIdUserGroupList As String, _
                                    ByVal sMenuPath As String) As String
        ' definir los objetos
        Dim sFileName As New StringBuilder

        ' contruir elnombre
        sFileName.Append("mMenu")
        sFileName.Append(iIdUser)
        sFileName.Append(".xml")

        ' construir el menu
        Dim sMenu As String = buildMenuToUser(objApplicationCredentials, sIdUserGroupList)

        If sMenu.Equals("") Then

            ' no tiene menus configurados
            Throw New Exception("El Perfil de usuario ingresado tiene acceso restringido a la aplicacion. Por favor, comuniquese con el administrador.")

        End If

        ' creando el archivo
        ' Abre de escritura, indica que las nuevas líneas deberán ser adicionadas (Append). También especifica formato de caracteres UTF8.
        Dim sNewDetailXML As StreamWriter = New StreamWriter(sMenuPath & sFileName.ToString, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

        ' Adiciona el string.
        sNewDetailXML.WriteLine(sMenu)

        ' Cierra el Stream.
        sNewDetailXML.Close()

        ' devolver el nombredel archivo
        Return sFileName.ToString()

    End Function

    ''' <summary>
    ''' Crea el menu dinámico para el usuario
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="iIdUser"></param>
    ''' <param name="sIdUserGroupList"></param>
    ''' <param name="sMenuPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function writeMenuToUser2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal iIdUser As Integer, _
                                    ByVal sIdUserGroupList As String, _
                                    ByVal sMenuPath As String) As String
        ' definir los objetos
        Dim sFileName As New StringBuilder

        ' contruir elnombre
        sFileName.Append("mMenu")
        sFileName.Append(iIdUser)
        sFileName.Append(".xml")

        ' construir el menu
        Dim sMenu As String = buildMenuToUser2(objApplicationCredentials, sIdUserGroupList)

        If sMenu.Equals("") Then

            ' no tiene menus configurados
            Throw New Exception("El Perfil de usuario ingresado tiene acceso restringido a la aplicacion. Por favor, comuniquese con el administrador.")

        End If

        ' creando el archivo
        ' Abre de escritura, indica que las nuevas líneas deberán ser adicionadas (Append). También especifica formato de caracteres UTF8.
        Dim sNewDetailXML As StreamWriter = New StreamWriter(sMenuPath & sFileName.ToString, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

        ' Adiciona el string.
        sNewDetailXML.WriteLine(sMenu)

        ' Cierra el Stream.
        sNewDetailXML.Close()

        ' devolver el nombredel archivo
        Return sFileName.ToString()

    End Function

    ''' <summary>
    ''' Cargar los menus del usuario
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sIdUserGroupList">Grupo del usuario</param>
    ''' <returns>String con la estructura en xml del menu</returns>
    ''' <remarks></remarks>
    Public Function buildMenuToUser(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sIdUserGroupList As String) As String

        ' definiendo los objtos
        Dim dtData As DataTable
        Dim sSQL As New StringBuilder
        Dim sMenu As New StringBuilder

        Try

            ' construir la sentencia
            'sSQL.Append(" SELECT Parents.TextField as ParentTextField, Parents.URL AS ParentURL, Childs.TextField as ChildsTextField, Childs.URL AS ChildsURL, ")
            'sSQL.Append(" 	Parents.SortOrder AS ParentSortOrder, Childs.SortOrder AS ChildsSortOrder ")
            'sSQL.Append(" FROM 	(SELECT * FROM Menu  ")
            'sSQL.Append(" 		WHERE Menu.Id NOT IN (SELECT MenuHierarchy.IdMenu FROM MenuHierarchy)) Parents ")
            'sSQL.Append(" LEFT OUTER JOIN (SELECT * FROM Menu  ")
            'sSQL.Append(" 		INNER JOIN MenuHierarchy ON MenuHierarchy.IdMenu  = Menu.Id)  Childs ON Parents.Id = Childs.IdParent ")
            'sSQL.Append(" ORDER BY Parents.SortOrder, Childs.SortOrder ")

            sSQL.Append(" SELECT * ")
            sSQL.Append(" FROM ")
            sSQL.Append(" (SELECT Parent.TextField AS ParentTextField, Parent.URL AS ParentURL, Childs.TextField AS ChildsTextField,  ")
            sSQL.Append(" Childs.URL AS ChildsURL,  ")
            sSQL.Append(" 	Parent.SortOrder AS ParentSortOrder, Childs.SortOrder AS ChildsSortOrder ")
            sSQL.Append(" FROM MenusByUserGroup ")
            sSQL.Append(" INNER JOIN (SELECT * FROM Menu ")
            sSQL.Append(" WHERE Menu.Id Not In ( ")
            sSQL.Append(" SELECT IdMenu FROM MenuHierarchy)) Parent ON MenusByUserGroup.IdMenu = Parent.Id ")
            sSQL.Append(" INNER JOIN (SELECT Id, TextField, URL, SortOrder, MenuHierarchy.IdParent ")
            sSQL.Append(" FROM Menu ")
            sSQL.Append(" INNER JOIN MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu ")
            sSQL.Append(" WHERE Menu.Enabled = 'T') Childs ON Parent.Id = Childs.IdParent ")
            sSQL.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & sIdUserGroupList & ")")
            sSQL.Append(" union  ")
            sSQL.Append(" SELECT Menu.TextField as ParentTextField, Menu.URL AS ParentURL, null AS ChildsTextField, null AS ChildsURL,  ")
            sSQL.Append(" 	Menu.SortOrder AS ParentSortOrder, null AS ChildsSortOrder ")
            sSQL.Append(" FROM Menu ")
            sSQL.Append(" INNER JOIN MenusByUserGroup ON MenusByUserGroup.IdMenu = Menu.Id ")
            sSQL.Append(" WHERE Menu.Id Not In (SELECT IdMenu FROM MenuHierarchy)")

            sSQL.Append(" And Menu.Id IN (SELECT IdMenu FROM MenusByUserGroup WHERE IdUserGroup IN (" & sIdUserGroupList & "))")

            sSQL.Append(" ) Temp ")
            sSQL.Append(" ORDER BY ParentSortOrder, ChildsSortOrder ")

            'obtener la aplicacion
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            If dtData.Rows.Count > 0 Then

                ' encabezado
                sMenu.Append("<?xml version=""1.0"" encoding=""iso-8859-1"" ?>")
                sMenu.Append(vbCrLf)
                sMenu.Append("<menu>")

                Dim sEndNode As String = ""

                For Each drRow As DataRow In dtData.Rows

                    ' verificar si es un nuevo nodo
                    If IsDBNull(drRow("ChildsTextField")) Then

                        ' cerrar el nodo anterior
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)
                        sEndNode = "</MenuItem>"

                        ' agregar nodo padre
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drRow("ParentTextField"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drRow("ParentTextField"))
                        'sMenu.Append(""" NavigateUrlField="""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drRow("ParentURL"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")
                        sMenu.Append(vbCrLf)
                        ' ir al sigiuente nodo
                        'Continue For

                    Else
                        ' agregar nodo hijo
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drRow("ChildsTextField"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drRow("ChildsTextField"))
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drRow("ChildsURL"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")

                        ' cerrar el nodo
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)

                    End If

                Next

                ' cerrar el archivo
                sMenu.Append(" </MenuItem></menu>")

            End If

            ' regresar el objteto
            Return sMenu.ToString

            ' finaliza la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancela la transaccion
            CtxSetAbort()

            ' publicar
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "buildMenuToUser")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sSQL = Nothing
            sMenu = Nothing
            dtData = Nothing

        End Try

    End Function
    ''' <summary>
    ''' crea un control menu con los niveles de padres e hijos
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="mnuPrincipal"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function createMenu(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                               ByVal mnuPrincipal As Menu, _
                               Optional ByVal order As String = "Menu.Id") As Menu
        ' definiendo los objtos
        Dim sSQL As New StringBuilder
        Dim sWhere = " WHERE "
        Dim coll As New List(Of MenuEntity)
        Dim dtMenuItems As New DataTable
        Dim objMenu As MenuEntity

        ' construir la sentencia
        sSQL.Append("SELECT     Menu.Id as MenuId, Menu.TextField as Description, Menu.URL as Url, ")
        sSQL.Append(" Menu.Enabled as Enabled, Menu.SortOrder as Position, ")
        sSQL.Append(" Case when MenuHierarchy.IdParent is null then Menu.Id else MenuHierarchy.IdParent end as IdParent")
        sSQL.Append(" FROM         Menu LEFT OUTER JOIN")
        sSQL.Append(" MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu")

        ' ordenando
        sSQL.Append(" ORDER BY " & order)

        Try
            'obtener la Menu
            dtMenuItems = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            'recorremos el datatable para agregar los elementos de que estaran en la cabecera del menú.
            For Each drMenuItem As Data.DataRow In dtMenuItems.Rows

                'esta condicion indica q son elementos padre.
                If drMenuItem("MenuId").Equals(drMenuItem("IdParent")) Then
                    Dim mnuMenuItem As New MenuItem
                    mnuMenuItem.Value = drMenuItem("MenuId").ToString
                    mnuMenuItem.Text = drMenuItem("Description").ToString
                    mnuMenuItem.NavigateUrl = drMenuItem("Url").ToString

                    'agregamos el Ítem al menú
                    mnuPrincipal.Items.Add(mnuMenuItem)

                    'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                    AddMenuItem(mnuMenuItem, dtMenuItems)
                End If
            Next

            ' regresar el objteto
            Return mnuPrincipal

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "GetMenusByMenu")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Menus hijos")

        Finally
            ' liberando recursos
            sSQL = Nothing
            sWhere = Nothing
            coll = Nothing
            dtMenuItems = Nothing
            objMenu = Nothing

        End Try
    End Function
    ''' <summary>
    ''' crea los submenus hijos para el control menu padre
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="mnuMenuItem"></param>
    ''' <param name="dtMenuItems"></param>
    ''' <remarks></remarks>
    Private Sub AddMenuItem(ByRef mnuMenuItem As MenuItem, ByVal dtMenuItems As Data.DataTable)

        'recorremos cada elemento del datatable para poder determinar cuales son elementos hijos
        'del menuitem dado pasado como parametro ByRef.
        For Each drMenuItem As Data.DataRow In dtMenuItems.Rows
            If drMenuItem("IdParent").ToString.Equals(mnuMenuItem.Value) AndAlso _
            Not drMenuItem("MenuId").Equals(drMenuItem("IdParent")) Then
                Dim mnuNewMenuItem As New MenuItem
                mnuNewMenuItem.Value = drMenuItem("MenuId").ToString
                mnuNewMenuItem.Text = drMenuItem("Description").ToString
                mnuNewMenuItem.NavigateUrl = drMenuItem("Url").ToString

                'Agregamos el Nuevo MenuItem al MenuItem que viene de un nivel superior.
                mnuMenuItem.ChildItems.Add(mnuNewMenuItem)

                'llamada recursiva para ver si el nuevo menú ítem aun tiene elementos hijos.
                AddMenuItem(mnuNewMenuItem, dtMenuItems)
            End If
        Next

    End Sub

    ''' <summary>
    ''' crea el menú dinámico en formato xml
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="menu"></param>
    ''' <param name="dt"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildMenu2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal menu As String, _
                                ByVal dt As DataTable, _
                                ByVal sMenuPath As String)

        ' definiendo los objtos
        Dim sMenu As New StringBuilder
        Dim sFileName As New StringBuilder
        'definir objetos
        Dim dtView As New DataView
        Dim dtParent As New DataTable


        Try

            If dt.Rows.Count > 0 Then

                ' convertir datatable a dataview
                dtView = dt.DefaultView

                ' filtrar los hijos
                dtView.RowFilter = "IdParent = MenuId"

                ' convertir el dataview a datatable
                dtParent = dtView.ToTable

                ' encabezado
                sMenu.Append("<?xml version=""1.0"" encoding=""iso-8859-1"" ?>")
                sMenu.Append(vbCrLf)
                sMenu.Append("<menu>")

                Dim sEndNode As String = ""

                For Each drMenuItem As Data.DataRow In dtParent.Rows

                    'esta condicion indica q son elementos padre.
                    If drMenuItem("MenuId").Equals(drMenuItem("IdParent")) Then

                        ' cerrar el nodo anterior
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)
                        sEndNode = "</MenuItem>"

                        ' agregar nodo padre
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drMenuItem("Description"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drMenuItem("Description"))
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drMenuItem("Url"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")
                        sMenu.Append(vbCrLf)

                        'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                        AddMenuItem2(sMenu, dt, drMenuItem("MenuId").ToString, sEndNode)
                    End If
                Next

                ' cerrar el archivo
                sMenu.Append(" </MenuItem></menu>")

            End If



            ' contruir elnombre
            sFileName.Append("mMenu")
            sFileName.Append(menu.Replace(",", "_"))
            sFileName.Append(".xml")

            ' Abre de escritura, indica que las nuevas líneas deberán ser adicionadas (Append). También especifica formato de caracteres UTF8.
            Dim sNewDetailXML As StreamWriter = New StreamWriter(sMenuPath & sFileName.ToString, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

            ' Adiciona el string.
            sNewDetailXML.WriteLine(sMenu)

            ' Cierra el Stream.
            sNewDetailXML.Close()

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sMenu = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' crea el menú dinámico en formato xml
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="menu"></param>
    ''' <param name="dtParents"></param>
    ''' <param name="dtSons"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildMenu3(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal menu As String, _
                                ByVal dtParents As DataTable, _
                                ByVal dtSons As DataTable, _
                                ByVal sMenuPath As String)

        ' definiendo los objtos
        Dim sMenu As New StringBuilder
        Dim sFileName As New StringBuilder
        'definir objetos
        Dim dtView As New DataView
        Dim dtParent As New DataTable


        Try

            If dtParents.Rows.Count > 0 Then

                ' encabezado
                sMenu.Append("<?xml version=""1.0"" encoding=""iso-8859-1"" ?>")
                sMenu.Append(vbCrLf)
                sMenu.Append("<menu>")

                Dim sEndNode As String = ""

                For Each drMenuItem As Data.DataRow In dtParents.Rows

                    ' cerrar el nodo anterior
                    sMenu.Append(sEndNode)
                    sMenu.Append(vbCrLf)
                    sEndNode = "</MenuItem>"

                    ' agregar nodo padre
                    sMenu.Append(" <MenuItem ValueField=""")
                    sMenu.Append(drMenuItem("Description"))
                    sMenu.Append(""" TextField=""")
                    sMenu.Append(drMenuItem("Description"))
                    sMenu.Append(""" NavigateUrlField=""")
                    sMenu.Append(drMenuItem("Url"))
                    sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                    sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                    sMenu.Append(" ToolTipField = """" > ")
                    sMenu.Append(vbCrLf)

                    'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                    AddMenuItem2(sMenu, dtSons, drMenuItem("MenuId").ToString, sEndNode)
                Next

                ' cerrar el archivo
                sMenu.Append(" </MenuItem></menu>")

            End If



            ' contruir elnombre
            sFileName.Append("mMenu_")
            sFileName.Append(menu.Replace(",", "_"))
            sFileName.Append(".xml")

            ' Abre de escritura, indica que las nuevas líneas deberán ser adicionadas (Append). También especifica formato de caracteres UTF8.
            Dim sNewDetailXML As StreamWriter = New StreamWriter(sMenuPath & sFileName.ToString, False, System.Text.Encoding.GetEncoding("iso-8859-1"))

            ' Adiciona el string.
            sNewDetailXML.WriteLine(sMenu)

            ' Cierra el Stream.
            sNewDetailXML.Close()

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sMenu = Nothing

        End Try

    End Sub
    ''' <summary>
    ''' crea los menus hijos según el menu padre
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="sMenu"></param>
    ''' <param name="dtMenuItems"></param>
    ''' <param name="idparent"></param>
    ''' <param name="sEndNode"></param>
    ''' <remarks></remarks>
    Private Sub AddMenuItem2(ByRef sMenu As StringBuilder, ByVal dtMenuItems As Data.DataTable, ByVal idparent As String, _
                                  ByVal sEndNode As String)

        'definir objetos
        Dim dtView As New DataView
        Dim dtSons As New DataTable

        Try
            ' convertir datatable a dataview
            dtView = dtMenuItems.DefaultView

            ' filtrar los hijos
            dtView.RowFilter = "IdParent = " & idparent

            ' convertir el dataview a datatable
            dtSons = dtView.ToTable

            'recorrer cada elemento del datatable para poder determinar cuales son elementos hijos
            For Each drMenuItem As Data.DataRow In dtSons.Rows

                'sMenu.Append(vbCrLf)
                sMenu.Append(" <MenuItem ValueField=""")
                sMenu.Append(drMenuItem("Description"))
                sMenu.Append(""" TextField=""")
                sMenu.Append(drMenuItem("Description"))
                sMenu.Append(""" NavigateUrlField=""")
                sMenu.Append(drMenuItem("Url"))
                sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                sMenu.Append(" ToolTipField = """" > ")
                sMenu.Append(vbCrLf)


                'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                AddMenuItem2(sMenu, dtMenuItems, drMenuItem("MenuId").ToString, sEndNode)

                ' cerrar el nodo
                sMenu.Append(" " & sEndNode)
                sMenu.Append(vbCrLf)
            Next

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally

            ' liberando objetos
            dtView = Nothing
            dtSons = Nothing

        End Try


    End Sub
    ''' <summary>
    ''' crea un menú dinámico para cada uno de los grupos de usuarios
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildApplicationMenus2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim sFileName As New StringBuilder
        Dim sql As New StringBuilder
        Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
        Dim groupList As New List(Of UserGroupEntity)
        Dim listG As String = ""
        Dim dtMenuItems As New DataTable
        Dim combine As New Combination
        Dim combineStringList As New List(Of String)
        Dim combineStringFinalList As New List(Of String)
        Dim valueList() As String

        Try
            ' obteniendo la lista de los usuarios
            groupList = objUserGroupDALC.GetUsersGroupList(objApplicationCredentials, enabledOnly:=True)

            For Each userGroup As UserGroupEntity In groupList

                If listG.Equals("") Then
                    listG = listG & userGroup.iID
                Else
                    listG = listG & "," & userGroup.iID
                End If

            Next

            valueList = listG.Trim().Split(",".ToCharArray)

            ' Obteniendo las combinaciones únicas posibles
            For i = 1 To (valueList.Length)
                combineStringList = combine.GetSubsets2(valueList, i)
                combineStringFinalList.AddRange(combineStringList)
            Next

            For Each menu As String In combineStringFinalList

                If Not menu.Equals("") Then

                    ' ordenar
                    menu = ordenar(menu.Trim(",".ToCharArray))

                    ' cargando los datos basicos del usuario
                    Dim dt As DataTable = GattacaApplication.RunSQLRDT(objApplicationCredentials, buildSqlByCombinationGroup2(menu), sProductName:="VBSecurity")

                    ' contruir el menu para cada objeto
                    buildMenu2(objApplicationCredentials, menu, dt, sMenuPath)

                End If

            Next

            Dim salta As Integer = 1

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sFileName = Nothing
            sql = Nothing
            groupList = Nothing
            listG = Nothing
            dtMenuItems = Nothing

        End Try


    End Sub

    ''' <summary>
    ''' crea un menú dinámico para cada uno de los grupos de usuarios
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sMenuPath"></param>
    ''' <remarks></remarks>
    Public Sub buildApplicationMenus3(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sMenuPath As String)

        ' definir los objetos
        Dim sFileName As New StringBuilder
        Dim sql As New StringBuilder
        Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
        Dim groupList As New List(Of UserGroupEntity)
        Dim listG As String = ""
        Dim dtMenuItems As New DataTable
        Dim combine As New Combination
        Dim combineStringList As New List(Of String)
        Dim combineStringFinalList As New List(Of String)
        Dim valueList() As String
        Dim dvMenuItems As New DataView
        Dim dt As New DataTable
        Dim dtDistinct As New DataTable
        Dim dtSons As New DataTable

        Try

            ' obteniendo la lista de los usuarios
            groupList = objUserGroupDALC.GetMenuUsersGroupList(objApplicationCredentials, enabledOnly:=True)

            For Each userGroup As UserGroupEntity In groupList

                If listG.Equals("") Then
                    listG = listG & userGroup.iID
                Else
                    listG = listG & "," & userGroup.iID
                End If

            Next

            ' obtener los menus para  los grupos de usuarios disponibles
            dtMenuItems = GattacaApplication.RunSQLRDT(objApplicationCredentials, buildSqlByCombinationGroup3(listG), sProductName:="VBSecurity")

            'construir la sentencia relaciones de menus
            sql.Append(" SELECT     MenuHierarchy.IdMenu as MenuId, Menu.TextField as Description, Menu.URL as Url, Menu.Enabled as Enabled, ")
            sql.Append(" Menu.SortOrder as Position, MenuHierarchy.IdParent as IdParent ")
            sql.Append(" FROM         MenuHierarchy INNER JOIN ")
            sql.Append(" Menu ON MenuHierarchy.IdMenu = Menu.Id")
            sql.Append(" WHERE Menu.Enabled = 'T'")
            sql.Append(" ORDER BY Menu.SortOrder")

            'obtiene las relaciones entre menus
            dtSons = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString, sProductName:="VBSecurity")

            ' separa la el grupo de usuarios en una lista
            valueList = listG.Trim().Split(",".ToCharArray)

            ' Obteniendo las combinaciones únicas posibles
            For i = 1 To (valueList.Length)
                combineStringList = combine.GetSubsets2(valueList, i)
                combineStringFinalList.AddRange(combineStringList)
            Next

            For Each menu As String In combineStringFinalList

                If Not menu.Equals("") Then

                    ' ordenar
                    menu = ordenar(menu.Trim(",".ToCharArray))

                    ' Filtra los menus según la combinación de grupos de usuarios
                    dvMenuItems = dtMenuItems.DefaultView
                    dvMenuItems.RowFilter = "IdUserGroup IN (" & menu & ")"
                    dt = dvMenuItems.ToTable

                    ' eliminación de menus repetidos
                    dvMenuItems = dt.DefaultView
                    dtDistinct = dvMenuItems.ToTable(True, "MenuId", "Description", "Url", "Enabled", "Position")

                    ' contruir el menu para cada objeto
                    buildMenu3(objApplicationCredentials, menu, dtDistinct, dtSons, sMenuPath)

                End If

            Next

            Dim salta As Integer = 1

        Catch oex As Exception

            ' publicar el error
            GattacaApplication.Publish(oex, objApplicationCredentials.ClientName, MODULENAME, "buildApplicationMenus3")
            ExceptionPolicy.HandleException(oex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sFileName = Nothing
            sql = Nothing
            groupList = Nothing
            listG = Nothing
            dtMenuItems = Nothing
            dvMenuItems = Nothing
            dt = Nothing
            dtDistinct = Nothing

        End Try


    End Sub
    ''' <summary>
    ''' crea el la estructura del menú dinámico para el grupo de usuarios
    ''' Creator: Sebastián Ramírez
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sIdUserGroupList"></param>
    ''' <param name="order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function buildMenuToUser2(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                    ByVal sIdUserGroupList As String, Optional ByVal order As String = "T.Position") As String

        ' definir los objetos
        Dim sFileName As New StringBuilder
        Dim sMenu As New StringBuilder
        Dim sql As New StringBuilder
        Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
        Dim groupList As New List(Of UserGroupEntity)
        Dim listG As String = ""
        Dim menusColls As Collection
        Dim sSQL As New StringBuilder
        Dim sWhere = " WHERE "
        Dim dtMenuItems As New DataTable
        Dim menus() As String

        ' construir la sentencia para obtener menus

        sSQL.Append(" Select *")
        sSQL.Append(" FROM(")
        sSQL.Append(" SELECT     Menu.Id AS MenuId, Menu.TextField AS Description, Menu.URL AS Url, ")
        sSQL.Append(" Menu.Enabled AS Enabled, Menu.SortOrder AS Position,")
        sSQL.Append(" CASE WHEN MenuHierarchy.IdParent IS null THEN Menu.Id ELSE MenuHierarchy.IdParent END AS IdParent")
        sSQL.Append(" FROM         Menu LEFT OUTER JOIN")
        sSQL.Append(" MenuHierarchy ON Menu.Id = MenuHierarchy.IdMenu")
        sSQL.Append(" ) AS T")
        sSQL.Append(" WHERE (t.MenuId IN (SELECT     Menu.Id AS MenuId")
        sSQL.Append(" FROM         Menu INNER JOIN MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu")
        sSQL.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & sIdUserGroupList & ")) OR t.IdParent IN (SELECT     Menu.Id AS MenuId")
        sSQL.Append(" FROM         Menu INNER JOIN MenusByUserGroup ON Menu.Id = MenusByUserGroup.IdMenu")
        sSQL.Append(" WHERE MenusByUserGroup.IdUserGroup IN (" & sIdUserGroupList & "))) and T.Enabled = 'T'")


        ' ordenando
        sSQL.Append(" ORDER BY " & order)

        Try

            'obtener los Menu
            dtMenuItems = GattacaApplication.RunSQLRDT(objApplicationCredentials, sSQL.ToString, sProductName:="VBSecurity")

            If dtMenuItems.Rows.Count > 0 Then

                ' encabezado
                sMenu.Append("<?xml version=""1.0"" encoding=""iso-8859-1"" ?>")
                sMenu.Append(vbCrLf)
                sMenu.Append("<menu>")

                Dim sEndNode As String = ""

                For Each drMenuItem As Data.DataRow In dtMenuItems.Rows

                    'esta condicion indica q son elementos padre.
                    If drMenuItem("MenuId").Equals(drMenuItem("PadreId")) Then

                        ' cerrar el nodo anterior
                        sMenu.Append(sEndNode)
                        sMenu.Append(vbCrLf)
                        sEndNode = "</MenuItem>"

                        ' agregar nodo padre
                        sMenu.Append(" <MenuItem ValueField=""")
                        sMenu.Append(drMenuItem("Description"))
                        sMenu.Append(""" TextField=""")
                        sMenu.Append(drMenuItem("Description"))
                        sMenu.Append(""" NavigateUrlField=""")
                        sMenu.Append(drMenuItem("Url"))
                        sMenu.Append(""" EnabledField=""True"" FormatString="""" ImageUrlField=""""")
                        sMenu.Append(" PopOutImageUrlField="""" SelectableField="""" SeparatorImageUrlField=""""  TargetField=""""")
                        sMenu.Append(" ToolTipField = """" > ")
                        sMenu.Append(vbCrLf)

                        'hacemos un llamado al metodo recursivo encargado de generar el árbol del menú.
                        AddMenuItem2(sMenu, dtMenuItems, drMenuItem("MenuId").ToString, sEndNode)

                    End If
                Next

                ' cerrar el archivo
                sMenu.Append(" </MenuItem></menu>")

            End If

            ' regresar el objteto
            Return sMenu.ToString

            ' finaliza la transaccion
            CtxSetComplete()

        Catch oex As Exception
            ' subir el error de nivel
            Throw New Exception("Error al cargar el menu del usuario")

        Finally
            ' liberando recursos
            sFileName = Nothing
            sql = Nothing
            groupList = Nothing
            listG = Nothing
            menusColls = Nothing
            sSQL = Nothing
            dtMenuItems = Nothing

        End Try


    End Function


#End Region

End Class
