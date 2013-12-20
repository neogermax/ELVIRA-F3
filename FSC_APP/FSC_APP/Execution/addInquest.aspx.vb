Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addInquest
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

            ' cargar los combos
            Me.loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA ENCUESTA."

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

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA ENCUESTA."

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

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objInquest As New InquestEntity

                    Try
                        ' cargar el registro referenciado
                        objInquest = facade.loadInquest(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objInquest.id
                        Me.txtcode.Text = objInquest.code
                        Me.txtname.Text = objInquest.name
                        Me.ddlProject.SelectedValue = objInquest.idproject
                        Me.ddlProjectPhase.SelectedValue = objInquest.projectphase
                        Me.ddlUserGroup.SelectedValue = objInquest.idusergroup
                        Me.ddlenabled.SelectedValue = objInquest.enabled
                        Me.txtcreatedate.Text = objInquest.createdate

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objInquest = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        If Page.IsValid Then


            ' definir los objetos
            Dim facade As New Facade
            Dim objInquest As New InquestEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Try
                ' cargar los valores registrados por el usuario
                objInquest.code = Me.txtcode.Text
                objInquest.name = Me.txtname.Text
                objInquest.idproject = Me.ddlProject.SelectedValue
                objInquest.projectphase = Me.ddlProjectPhase.SelectedValue
                objInquest.idusergroup = Me.ddlUserGroup.SelectedValue
                objInquest.enabled = Me.ddlenabled.SelectedValue
                objInquest.createdate = Now()

                ' almacenar la entidad
                objInquest.id = facade.addInquest(applicationCredentials, objInquest)

                ' ir al administrador
                Response.Redirect("searchInquest.aspx")

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
                objInquest = Nothing
                facade = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchInquest.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Page.IsValid Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objInquest As New InquestEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar el registro referenciado
            objInquest = facade.loadInquest(applicationCredentials, Request.QueryString("id"))

            Try
                ' cargar los datos
                objInquest.code = Me.txtcode.Text
                objInquest.name = Me.txtname.Text
                objInquest.idproject = Me.ddlProject.SelectedValue
                objInquest.projectphase = Me.ddlProjectPhase.SelectedValue
                objInquest.idusergroup = Me.ddlUserGroup.SelectedValue
                objInquest.enabled = Me.ddlenabled.SelectedValue

                ' modificar el registro
                facade.updateInquest(applicationCredentials, objInquest)

                ' ir al administrador
                Response.Redirect("searchInquest.aspx")

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
                objInquest = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteInquest(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchInquest.aspx")

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

    Protected Sub cvCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvCode.ServerValidate
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se veirifica el tipo de documento
            If facade.verifyInquestCode(applicationCredentials, args.Value, Me.txtid.Text) Then
                args.IsValid = False
                Me.txtcode.Focus()
            Else
                args.IsValid = True
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

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se llama al metodo que permite cargar el combo de proyectos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlProject.DataSource = facade.getProjectListByInquest(applicationCredentials, "1", "Code")
            Else
                Me.ddlProject.DataSource = facade.getProjectListByInquest(applicationCredentials, "", "Code")
            End If

            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

            'Se llama al metodo que permite cargar el combo de grupos de usuario
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlUserGroup.DataSource = facade.getUserGroupList(applicationCredentials, enabledOnly:=True, order:="UserGroup.Code")
            Else
                Me.ddlUserGroup.DataSource = facade.getUserGroupList(applicationCredentials, enabledOnly:=False, order:="UserGroup.Code")
            End If

            Me.ddlUserGroup.DataValueField = "iId"
            Me.ddlUserGroup.DataTextField = "sCode"
            Me.ddlUserGroup.DataBind()

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
