Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addForum
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

            'Cargar Combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO FORO."

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
                    Me.lblupdateddate.Visible = False
                    Me.txtupdateddate.Visible = False
                    'Me.lblReplyTitle.Visible = False
                    'Me.btnAddReply.Visible = False
                    Me.ddlidproject.Enabled = True
                    Me.txtiduser.Visible = False
                    Me.rfvid.Visible = False
                    Me.rfviduser.Visible = False
                    Me.rfvcreatedate.Visible = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN FORO."

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
                    Me.lblupdateddate.Enabled = False
                    Me.lblupdateddate.Enabled = False
                    
                    ' definir los objetos
                    Dim facade As New Facade
					Dim objForum As New ForumEntity

                    Try
                        ' cargar el registro referenciado
                        objForum = facade.loadForum(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objForum.id
                        Me.ddlidproject.SelectedValue = objForum.idproject
                        Me.txtsubject.Text = objForum.subject
                        Me.txtmessage.Text = objForum.message
                        Me.txtupdateddate.Text = objForum.updateddate
                        Me.ddlenabled.SelectedValue = objForum.enabled
                        Me.txtiduser.Text = objForum.USERNAME
                        Me.txtcreatedate.Text = objForum.createdate
                        ' cargar y habilitar el archivo anexo
                        Me.hlattachment.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objForum.attachment
                        Me.hlattachment.Text = objForum.attachment
                        Me.hlattachment.Visible = True

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objForum = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objForum As New ForumEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objForum.idproject = Me.ddlidproject.SelectedValue
            objForum.subject = Me.txtsubject.Text
            objForum.message = Me.txtmessage.Text
            objForum.attachment = PublicFunction.LoadFile(Request)
            objForum.enabled = Me.ddlenabled.SelectedValue
            objForum.iduser = applicationCredentials.UserID
            objForum.updateddate = Now
            objForum.createdate = Now

            ' almacenar la entidad
            objForum.id = facade.addForum(applicationCredentials, objForum)

            ' ir al administrador
            Response.Redirect("searchForum.aspx")

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
            objForum = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchForum.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objForum As New ForumEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Cargar el registro referenciado
        objForum = facade.loadForum(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objForum.idproject = Me.ddlidproject.SelectedValue
            objForum.subject = Me.txtsubject.Text
            objForum.message = Me.txtmessage.Text
            objForum.enabled = Me.ddlenabled.SelectedValue
            objForum.updateddate = Now

            'Cargar el archivo
            objForum.attachment = PublicFunction.LoadFile(Request)

            ' si no se modifico el archivo
            If objForum.attachment.Equals(String.Empty) Then

                'cargar el anterior
                objForum.attachment = Me.hlattachment.Text

            End If

            ' modificar el registro
           facade.updateForum(applicationCredentials, objForum)

            ' ir al administrador
            Response.Redirect("searchForum.aspx")

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
            objForum = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteForum(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchForum.aspx")

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

#End Region

#Region "Metodos"

    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="code")
            Else
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", order:="code")
            End If
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

#End Region
    
End Class
