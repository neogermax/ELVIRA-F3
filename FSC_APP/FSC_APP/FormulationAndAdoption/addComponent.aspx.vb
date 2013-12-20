Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addComponent
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
            Dim consultLastVersion As Boolean = True
            If Not (Request.QueryString("consultLastVersion") Is Nothing) Then consultLastVersion = Request.QueryString("consultLastVersion")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim objObjective As New ObjectiveEntity

            'Cargar Combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO COMPONENTE."

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
                    Me.rfvid.Visible = False
                    Me.rfviduser.Visible = False
                    Me.rfvcreatedate.Visible = False

                Case "edit", "show"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN COMPONENTE."

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
                    Dim objComponent As New ComponentEntity

                    Try
                        ' cargar el registro referenciado
                        objComponent = facade.loadComponent(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objComponent.id
                        Me.txtcode.Text = objComponent.code
                        Me.txtname.Text = objComponent.name
                        Me.ddlidobjective.SelectedValue = objComponent.idobjective
                        Me.ddlenabled.SelectedValue = objComponent.enabled
                        Me.txtiduser.Text = objComponent.USERNAME
                        Me.txtcreatedate.Text = objComponent.createdate
                        objObjective = facade.loadObjective(applicationCredentials, objComponent.idobjective)
                        Me.ddlidproject.SelectedValue = objObjective.idproject
                        ddlidproject_SelectedIndexChanged(sender, e)
                        Me.ddlidproject.Enabled = False



                        ' guardar
                        ViewState("idKey") = objComponent.idKey

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' limpiar label
                            Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            loadVersions(objComponent.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase del componente es la fase de cerrado
                            Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            If (objComponent.idphase.ToString() = idClosedState) Then
                                'Se oculta el botón grabar y el botón eliminar
                                Me.btnSave.Visible = False
                                Me.btnDelete.Visible = False
                            End If

                        End If

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objComponent = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objComponent As New ComponentEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objComponent.code = Me.txtcode.Text
            objComponent.name = Me.txtname.Text
            objComponent.idobjective = Me.ddlidobjective.SelectedValue
            objComponent.enabled = Me.ddlenabled.SelectedValue
            objComponent.iduser = applicationCredentials.UserID
            objComponent.createdate = Now
            objComponent.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            ' almacenar la entidad
            objComponent.id = facade.addComponent(applicationCredentials, objComponent)

            ' ir al administrador
            Response.Redirect("searchComponent.aspx")

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
            objComponent = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchComponent.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objComponent As New ComponentEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'Cargar el registro referenciado
        objComponent = facade.loadComponent(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objComponent.code = Me.txtcode.Text
            objComponent.name = Me.txtname.Text
            objComponent.idobjective = Me.ddlidobjective.SelectedValue
            objComponent.enabled = Me.ddlenabled.SelectedValue
            objComponent.iduser = applicationCredentials.UserID
            objComponent.createdate = Now
            objComponent.idphase = facade.ComponentPhaseProject(applicationCredentials, Me.ddlidobjective.SelectedValue)
            ' modificar el registro
            facade.updateComponent(applicationCredentials, objComponent)

            ' ir al administrador
            Response.Redirect("searchComponent.aspx")

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
            objComponent = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteComponent(applicationCredentials, Request.QueryString("Id"), ViewState("idKey"))

            ' ir al administrador
            Response.Redirect("searchComponent.aspx")

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

    Protected Sub ddlidproject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidproject.SelectedIndexChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, order:="code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

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

    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        'Verificar código
        verifyCode()
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idClosedState As String = ""

        Try
            'Se consulta el código correspondiente a la fase de "Evaluación y Cierre"
            idClosedState = ConfigurationManager.AppSettings("IdClosedState").ToString()

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidproject.DataSource = facade.getProjectListNotInPhase(applicationCredentials, idphase:=idClosedState, enabled:="1", order:="Code", isLastVersion:="1")                
            Else
                Me.ddlidproject.DataSource = facade.getProjectListNotInPhase(applicationCredentials, idphase:=idClosedState, order:="Code", isLastVersion:="1")
            End If
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()

            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, order:="code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

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

    Private Function verifyCode() As Boolean
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifyComponentCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.Text = "Este código ya existe, por favor cambielo"
                rfvcode.IsValid = 0
                verifyCode = 0
            Else
                lblHelpcode.Text = ""
                rfvcode.IsValid = 1
                verifyCode = 1
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
    End Function

    Public Sub loadVersions(ByVal idKey As String)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim list As List(Of ComponentEntity)

        Try
            ' cargar la lista de versiones anteriores
            list = facade.getComponentList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

            Me.gvVersion.DataSource = list
            Me.gvVersion.DataBind()

            If list.Count > 0 Then

                ' mensaje
                Me.lblVersion.text = "Versiones Anteriores Registradas"

            Else

                ' mensaje
                Me.lblVersion.text = "No Hay Versiones Anteriores Registradas"

            End If

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
