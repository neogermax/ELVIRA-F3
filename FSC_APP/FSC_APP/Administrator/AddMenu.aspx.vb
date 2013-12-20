Imports Gattaca.Application.Credentials

Partial Class AddMenu
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then

            ' definiendo los objetos
            Dim objMenu As New MenuEntity
            Dim facade As New Facade
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' Definiendo la operacion a realizar
            Dim sOp As String = Request.QueryString("Op").ToString

            Try
                ' operacion
                Select Case sOp

                    Case "Add"
                        ' oculatando controles para modificar
                        Me.btnUpdate.Visible = False
                        Me.btnDelete.Visible = False

                    Case "Edit"
                        'oculatando controles para crear
                        Me.btnNew.Visible = False

                        ' creando y cargando la aplicacion
                        objMenu = facade.GetMenu(applicationCredentials, Request.QueryString("IdMenu"))

                        ' creando y cargando la applicacion
                        Me.txtTextField.Text = objMenu.sTextField
                        Me.txtURL.Text = objMenu.sURL
                        Me.dllEnabled.SelectedValue = objMenu.sEnabled
                        Me.txtSortOrdeR.Text = objMenu.iSortOrden

                    Case Else

                End Select

            Catch ex As Exception

                ' guardando y enviando a la pagina de error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/Errors/Error.aspx")

            Finally

                ' liberar recursos
                objMenu = Nothing
                facade = Nothing
                sOp = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        ' definiendo los objetos
        Dim objMenu As New MenuEntity
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' creando y cargando la applicacion
            objMenu.sTextField = Me.txtTextField.Text
            objMenu.sURL = Me.txtURL.Text
            objMenu.sEnabled = Me.dllEnabled.SelectedValue
            objMenu.iSortOrden = Me.txtSortOrdeR.Text

            ' agregar 
            facade.Add(applicationCredentials, objMenu)

        Catch ex As Exception
            ' guardando y enviando a la pagina de error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/Errors/Error.aspx")

        Finally
            ' liberar recursos
            objMenu = Nothing
            facade = Nothing

        End Try

        ' dirigiendose a la lista de aplicaciones
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        ' definiendo los objetos
        Dim objMenu As New MenuEntity
        Dim objMenuDALC As New MenuDALC
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' creando y cargando ellenguaje
            objMenu.iId = Request.QueryString("IdMenu")
            objMenu.sTextField = Me.txtTextField.Text
            objMenu.sURL = Me.txtURL.Text
            objMenu.sEnabled = Me.dllEnabled.SelectedValue
            objMenu.iSortOrden = Me.txtSortOrdeR.Text

            ' modificar
            objMenuDALC.Update(applicationCredentials, objMenu)

        Catch ex As Exception
            ' guardando y enviando a la pagina de error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/Errors/Error.aspx")

        Finally
            ' liberar recursos
            objMenu = Nothing
            objMenuDALC = Nothing

        End Try

        ' dirigiendose a la lista de aplicaciones
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' ocultando los botones
        Me.btnDelete.Visible = False
        Me.btnUpdate.Visible = False
        Me.btnCancel.Visible = False

        ' mostrando los de confirmar
        Me.btnConfirmDelete.Visible = True
        Me.btnCancelDelete.Visible = True
        Me.lblConfirmDelete.Visible = True

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click
        ' definiendo los objetos
        Dim objMenuDALC As New MenuDALC
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar
            objMenuDALC.Delete(applicationCredentials, Request.QueryString("IdMenu"))

        Catch ex As Exception
            ' guardando y enviando a la pagina de error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/Errors/Error.aspx")

        Finally
            ' liberar recursos
            objMenuDALC = Nothing

        End Try

        ' dirigiendose a la lista de aplicaciones
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ' dirigiendose a la lista de aplicaciones
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

    Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDelete.Click
        ' dirigiendose a la lista de aplicaciones
        Response.Redirect("~/Administrator/AdminMenu.aspx")

    End Sub

#End Region

End Class

