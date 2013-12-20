Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addAttachFileFormat
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO FORMATO ARCHIVO ANEXO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN FORMATO ARCHIVO ANEXO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objAttachFileFormat As New AttachFileFormatEntity

                    Try
                        ' cargar el registro referenciado
                        objAttachFileFormat = facade.loadAttachFileFormat(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objAttachFileFormat.id
                        Me.txtcode.Text = objAttachFileFormat.code
                        Me.txtname.Text = objAttachFileFormat.name
                        Me.txtcreatedate.Text = objAttachFileFormat.createdate
                        Me.txtiduser.Text = objAttachFileFormat.USERNAME
                        Me.ddlenabled.SelectedValue = objAttachFileFormat.enabled

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objAttachFileFormat = Nothing

                    End Try

            End Select

            'Se inicializan las variables de la página
            ViewState("ValidCode") = True
        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        If (CBool(ViewState("ValidCode"))) Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objAttachFileFormat As New AttachFileFormatEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Try
                ' cargar los valores registrados por el usuario
                objAttachFileFormat.code = Me.txtcode.Text
                objAttachFileFormat.name = Me.txtname.Text
                objAttachFileFormat.createdate = Now
                objAttachFileFormat.iduser = applicationCredentials.UserID
                objAttachFileFormat.enabled = Me.ddlenabled.SelectedValue

                ' almacenar la entidad
                objAttachFileFormat.id = facade.addAttachFileFormat(applicationCredentials, objAttachFileFormat)

                ' ir al administrador
                Response.Redirect("searchAttachFileFormat.aspx")

            Catch oex As Threading.ThreadAbortException
                ' no hacer nada

            Catch ex As Exception

                ' ir a error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/errors/error.aspx")
                Response.End()

            Finally

                ' liberar recursos
                objAttachFileFormat = Nothing
                facade = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchAttachFileFormat.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (CBool(ViewState("ValidCode"))) Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objAttachFileFormat As New AttachFileFormatEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar el registro referenciado
            objAttachFileFormat = facade.loadAttachFileFormat(applicationCredentials, Request.QueryString("id"))

            Try
                ' cargar los datos
                objAttachFileFormat.code = Me.txtcode.Text
                objAttachFileFormat.name = Me.txtname.Text
                objAttachFileFormat.enabled = Me.ddlenabled.SelectedValue

                ' modificar el registro
                facade.updateAttachFileFormat(applicationCredentials, objAttachFileFormat)

                ' ir al administrador
                Response.Redirect("searchAttachFileFormat.aspx")

            Catch oex As Threading.ThreadAbortException
                ' no hacer nada

            Catch ex As Exception

                ' ir a error
                Session("sError") = ex.Message
                Session("sUrl") = Request.UrlReferrer.PathAndQuery
                Response.Redirect("~/errors/error.aspx")
                Response.End()

            Finally

                ' liberar recursos
                facade = Nothing
                objAttachFileFormat = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteAttachFileFormat(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchAttachFileFormat.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDelete.Click

        ' ocultar algunos botones
        Me.btnAddData.Visible = False
        Me.btnSave.Visible = True
        Me.btnDelete.Visible = True
        Me.btnCancelDelete.Visible = False
        Me.btnConfirmDelete.Visible = False
        Me.lblDelete.Visible = False
        Me.btnCancel.Visible = True

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' ocultar algunos botones
        Me.btnSave.Visible = False
        Me.btnDelete.Visible = False
        Me.btnConfirmDelete.Visible = True
        Me.btnCancel.Visible = False
        Me.btnCancelDelete.Visible = True
        Me.lblDelete.Visible = True

    End Sub

    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            If facade.verifyAttachFileFormatCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.ForeColor = Drawing.Color.Red
                lblHelpcode.Text = "Este código ya existe, por favor cambielo"
                rfvcode.IsValid = 0
                ViewState("ValidCode") = False
                Me.txtcode.Focus()
            Else
                lblHelpcode.Text = ""
                rfvcode.IsValid = 1
                ViewState("ValidCode") = True
                Me.txtname.Focus()
            End If

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try
    End Sub

#End Region

#Region "Metodos"

#End Region

End Class
