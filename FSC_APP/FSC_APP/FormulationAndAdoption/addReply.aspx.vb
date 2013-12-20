Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addReply
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

            'cargar los combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR RESPUESTA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.ddliduser.SelectedValue = applicationCredentials.UserID
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lblupdatedate.Visible = False
                    Me.txtupdatedate.Visible = False
                    Me.rfvupdatedate.Visible = False
                    Me.rfvid.Visible = False
                    Me.rfviduser.Visible = False
                    Me.rfvcreatedate.Visible = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR RESPUESTA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    
                    ' definir los objetos
                    Dim facade As New Facade
					Dim objReply As New ReplyEntity

                    Try
                        ' cargar el registro referenciado
                        objReply = facade.loadReply(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objReply.id
                        Me.ddliduser.SelectedValue = objReply.USERNAME
                        Me.txtsubject.Text = objReply.subject
                        Me.txtupdatedate.Text = objReply.updatedate
                        Me.txtcreatedate.Text = objReply.createdate
                        ' cargar y habilitar el archivo anexo
                        Me.hlattachment.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objReply.attachment
                        Me.hlattachment.Text = objReply.attachment
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
                        objReply = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objReply As New ReplyEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objReply.idforum = Me.ddlidforum.SelectedValue
            objReply.iduser = applicationCredentials.UserID
            objReply.subject = Me.txtsubject.Text
            objReply.attachment = PublicFunction.LoadFile(Request)
            objReply.updatedate = Now
            objReply.createdate = Now

            ' almacenar la entidad
            objReply.id = facade.addReply(applicationCredentials, objReply)

            ' ir al administrador
            Response.Redirect("projectForumPanel.aspx?id=" & Me.ddlidforum.SelectedValue)

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
            objReply = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("projectForumPanel.aspx?id=" & Me.ddlidforum.SelectedValue)

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objReply As New ReplyEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'cargar el registro referenciado
        objReply = facade.loadReply(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objReply.idforum = Me.ddlidforum.SelectedValue
            objReply.subject = Me.txtsubject.Text
            objReply.attachment = PublicFunction.LoadFile(Request)
            objReply.updatedate = Now

            ' modificar el registro
           facade.updateReply(applicationCredentials, objReply)

            ' ir al administrador
            Response.Redirect("projectForumPanel.aspx?id=" & Me.ddlidforum.SelectedValue)

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
            objReply = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteReply(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("projectForumPanel.aspx?id=" & Me.ddlidforum.SelectedValue)

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
        Dim idForum As String = Request.QueryString("idf")

        Try
            ' cargar la lista de los tipos
            Me.ddlidforum.DataSource = facade.getForumList(applicationCredentials)
            Me.ddlidforum.DataValueField = "Id"
            Me.ddlidforum.DataTextField = "Subject"
            Me.ddlidforum.DataBind()

            Me.ddliduser.DataSource = facade.getUserList(applicationCredentials)
            Me.ddliduser.DataValueField = "Id"
            Me.ddliduser.DataTextField = "Name"
            Me.ddliduser.DataBind()

            Me.ddlidforum.SelectedValue = idForum

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
