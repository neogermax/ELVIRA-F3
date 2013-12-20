Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class Administrator_AdminMenu
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

        If Not (Page.IsPostBack) Then

            Try
                ' limpiar el campo de busqueda
                Session("sField") = ""

                ' cargar el titulo
                Session("lblTitle") = "ADMINISTRACION DE MENUS"

                ' cargando los proyectos existentes
                Me.gvData.DataSource = GetData()
                Me.gvData.DataBind()

            Catch ex As Exception
                ' guardando y enviando a la pagina de error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/Errors/Error.aspx")

            End Try

        End If

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizando el indice
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargando los datos
        Me.gvData.DataSource = GetData()
        Me.gvData.DataBind()

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' guardar el campo de busqueda
        Session("sField") = Me.rblSearch.SelectedValue

        ' actualizando el indice
        Me.gvData.PageIndex = 0

        ' cargando los datos
        Me.gvData.DataSource = GetData()
        Me.gvData.DataBind()

    End Sub

    Public Function GetData() As List(Of MenuEntity)

        ' construyendo el objeto
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim textField As String = ""
        Dim url As String = ""
        Dim enabled As String = ""
        Dim id As String = ""
        Dim sortorder As String = ""
        Dim menulist As New List(Of MenuEntity)


        Try

            If Session("sField") = Nothing Then

                Session("sField") = ""

            End If

            Select Case Session("sField").ToString
                Case "TextField" : textField = CStr(Me.txtSearch.Text.Trim())
                Case "URL" : url = CStr(Me.txtSearch.Text.Trim())
                Case "Enabled" : enabled = CStr(Me.txtSearch.Text.Trim())
                Case "id" : id = CStr(Me.txtSearch.Text.Trim())
                Case "sortOrder" : sortorder = CStr(Me.txtSearch.Text.Trim())
                Case Else
                    ' no hacer nada
            End Select

            menulist = facade.GetMenuList(applicationCredentials, id, textField, url, enabled, sortorder)

            ' verificar resultado busqueda
            If menulist.Count > 0 Then
                Me.lblMessage.Visible = False
            Else
                Me.lblMessage.Visible = True
            End If

            GetData = menulist

        Catch ex As Exception
            ' guardando y enviando a la pagina de error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/Errors/Error.aspx")

        Finally
            ' liberar recursos
            facade = Nothing

        End Try

    End Function

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        ' redirigiendo a crear
        Response.Redirect("~/Administrator/AddMenu.aspx?Op=Add")

    End Sub

    Protected Sub btnUpdateMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateMenu.Click


        ' construyendo el objeto
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            ' contrir el menu
            facade.buildApplicationMenus3(applicationCredentials, Server.MapPath(PublicFunction.getSettingValue("MenuPath")))

        Catch ex As Exception
            ' guardando y enviando a la pagina de error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/Errors/Error.aspx")

        Finally
            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

End Class

