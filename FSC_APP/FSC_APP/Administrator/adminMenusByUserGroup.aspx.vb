Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class Administrator_adminMenusByUserGroup
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Application("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Application("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ' dar formato
            setSettings()

            ' declarando los objetos
            Dim objMenuDALC As MenuDALC = New MenuDALC()
            Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
            Dim dtMenu As New DataTable
            Dim coll As New List(Of UserGroupEntity)
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Dim objMenu As MenuEntity = objMenuDALC.GetMenu(applicationCredentials, Request.QueryString("IdMenu"))

            ' mostrando el nombre del usuario
            Me.lblMenuName.Text = objMenu.sTextField

            ' obteniendo la lista de los usuarios
            coll = objUserGroupDALC.GetUsersGroupList(applicationCredentials)

            For Each userGroup As UserGroupEntity In coll

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenusByUserGroup.AviableItems.Items.Add(New ListItem(userGroup.sName.Trim, userGroup.iID))

            Next

            ' obteniendo la lista de los usuarios
            coll = objMenuDALC.GetUsersByMenu(applicationCredentials, Request.QueryString("IdMenu"))

            For Each userGroup As UserGroupEntity In coll

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenusByUserGroup.SelectedItems.Items.Add(New ListItem(userGroup.sName.Trim, userGroup.iID))

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenusByUserGroup.AviableItems.Items.Remove(New ListItem(userGroup.sName.Trim, userGroup.iID))

            Next

        End If

    End Sub

    Public Sub setSettings()

        ' asignanado los estilos
        Me.dlbMenusByUserGroup.AviableItems.CssClass = "cssDdl"
        Me.dlbMenusByUserGroup.AviableItemsSearch.CssClass = "cssTextBoxForm"
        Me.dlbMenusByUserGroup.AviableItemsTitle.CssClass = "cssLabelTitle"
        Me.dlbMenusByUserGroup.AvivableItemsSearchTitle.CssClass = "cssLabelForm"

        Me.dlbMenusByUserGroup.SelectedItems.CssClass = "cssDdl"
        Me.dlbMenusByUserGroup.SelectedItemsSearch.CssClass = "cssTextBoxForm"
        Me.dlbMenusByUserGroup.SelectedItemsTitle.CssClass = "cssLabelTitle"
        Me.dlbMenusByUserGroup.SelectedItemsSearchTitle.CssClass = "cssLabelForm"

        Me.dlbMenusByUserGroup.OneToRigth.CssClass = "cssButton"
        Me.dlbMenusByUserGroup.AllToRigth.CssClass = "cssButton"
        Me.dlbMenusByUserGroup.OneToLeft.CssClass = "cssButton"
        Me.dlbMenusByUserGroup.AllToLeft.CssClass = "cssButton"

        Me.dlbMenusByUserGroup.AviableItems.Width = Unit.Percentage(100)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' redirigiendo a lista areas
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ' definir los objetos
        Dim objMenuDALC As MenuDALC = New MenuDALC()
        Dim sActivatedList As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obteniendo la lista de actividades para quitar
        For Each Row As ListItem In Me.dlbMenusByUserGroup.SelectedItems.Items
            ' almacenando cada actividad seleccionada 
            sActivatedList = sActivatedList & CInt(Row.Value) & ","

        Next

        ' limpiando la lista
        sActivatedList = sActivatedList.Trim(",")

        ' eliminado las actividades que tenga el area o las areas tengan
        objMenuDALC.DeleteUserGroupsByMenu(applicationCredentials, Request.QueryString("IdMenu"))

        ' eliminado las actividades que tenga el area o las areas tengan
        objMenuDALC.AddUserGroupsByMenu(applicationCredentials, Request.QueryString("IdMenu"), sActivatedList)

        ' redirigiendo a lista areas
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

End Class

