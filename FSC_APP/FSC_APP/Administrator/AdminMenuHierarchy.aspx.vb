Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class Administrator_AdminMenuHierarchy
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
            Dim dtMenu As New DataTable
            Dim coll As New List(Of MenuEntity)
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Dim objMenu As MenuEntity = objMenuDALC.GetMenu(applicationCredentials, Request.QueryString("IdMenu"))

            ' mostrando el nombre del usuario
            Me.lblMenuName.Text = objMenu.sTextField

            ' obteniendo la lista de los usuarios
            coll = objMenuDALC.GetMenuList(applicationCredentials, order:="textfield")

            For Each menu As MenuEntity In coll

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenuHierarchy.AviableItems.Items.Add(New ListItem(menu.sTextField.Trim, menu.iId))

            Next

            ' obteniendo la lista de los usuarios
            coll = objMenuDALC.GetMenusByMenu(applicationCredentials, Request.QueryString("IdMenu"), order:="textfield")

            For Each menu As MenuEntity In coll

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenuHierarchy.SelectedItems.Items.Add(New ListItem(menu.sTextField.Trim, menu.iId))

                ' agregando a la lista de seleccionadas las que pertenecen al area
                Me.dlbMenuHierarchy.AviableItems.Items.Remove(New ListItem(menu.sTextField.Trim, menu.iId))

            Next

        End If

    End Sub

    Public Sub setSettings()

        ' asignanado los estilos
        Me.dlbMenuHierarchy.AviableItems.CssClass = "cssDdl"
        Me.dlbMenuHierarchy.AviableItemsSearch.CssClass = "cssTextBoxForm"
        Me.dlbMenuHierarchy.AviableItemsTitle.CssClass = "cssLabelTitle"
        Me.dlbMenuHierarchy.AvivableItemsSearchTitle.CssClass = "cssLabelForm"

        Me.dlbMenuHierarchy.SelectedItems.CssClass = "cssDdl"
        Me.dlbMenuHierarchy.SelectedItemsSearch.CssClass = "cssTextBoxForm"
        Me.dlbMenuHierarchy.SelectedItemsTitle.CssClass = "cssLabelTitle"
        Me.dlbMenuHierarchy.SelectedItemsSearchTitle.CssClass = "cssLabelForm"

        Me.dlbMenuHierarchy.OneToRigth.CssClass = "cssButton"
        Me.dlbMenuHierarchy.AllToRigth.CssClass = "cssButton"
        Me.dlbMenuHierarchy.OneToLeft.CssClass = "cssButton"
        Me.dlbMenuHierarchy.AllToLeft.CssClass = "cssButton"

        Me.dlbMenuHierarchy.AviableItems.Width = Unit.Percentage(100)

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
        For Each Row As ListItem In Me.dlbMenuHierarchy.SelectedItems.Items
            ' almacenando cada actividad seleccionada 
            sActivatedList = sActivatedList & CInt(Row.Value) & ","

        Next

        ' limpiando la lista
        sActivatedList = sActivatedList.Trim(",")

        ' eliminado las actividades que tenga el area o las areas tengan
        objMenuDALC.DeleteMenusByMenu(applicationCredentials, Request.QueryString("IdMenu"))

        ' eliminado las actividades que tenga el area o las areas tengan
        objMenuDALC.AddMenusByMenu(applicationCredentials, Request.QueryString("IdMenu"), sActivatedList)

        ' redirigiendo a lista areas
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

End Class

