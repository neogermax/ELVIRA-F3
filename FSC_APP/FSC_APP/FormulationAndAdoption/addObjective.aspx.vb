Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addObjective
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

            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO OBJETIVO."

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

                    ' limpiar label
                    Me.lblVersion.text = ""
                    
                Case "edit", "show"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN OBJETIVO."

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
                    Dim objObjective As New ObjectiveEntity

                    Try
                        ' cargar el registro referenciado
                        objObjective = facade.loadObjective(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objObjective.id
                        Me.txtcode.Text = objObjective.code
                        Me.txtname.Text = objObjective.name
                        Me.ddlidproject.SelectedValue = objObjective.idproject
                        Me.ddlidproject.Enabled = False
                        Me.ddlenabled.SelectedValue = objObjective.enabled
                        Me.txtiduser.Text = objObjective.USERNAME
                        Me.txtcreatedate.Text = objObjective.createdate

                        ' guardar
                        ViewState("idKey") = objObjective.idKey

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.visible = False

                            ' limpiar label
                            Me.lblVersion.text = ""

                        Else

                            'Cargar las versiones anteriores
                            loadVersions(objObjective.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase del objetivo es la fase de cerrado
                            Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            If (objObjective.idphase.ToString() = idClosedState) Then
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
                        objObjective = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objObjective As New ObjectiveEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objObjective.code = Me.txtcode.Text
            objObjective.name = Me.txtname.Text
            objObjective.idproject = Me.ddlidproject.SelectedValue
            objObjective.enabled = Me.ddlenabled.SelectedValue
            objObjective.iduser = applicationCredentials.UserID
            objObjective.createdate = Now
            'Consulta la fase actual del proyecto.
            objObjective.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            ' almacenar la entidad
            objObjective.id = facade.addObjective(applicationCredentials, objObjective)

            ' ir al administrador
            Response.Redirect("searchObjective.aspx")

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
            objObjective = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchObjective.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objObjective As New ObjectiveEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'Cargar el objeto referenciado
        objObjective = facade.loadObjective(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objObjective.code = Me.txtcode.Text
            objObjective.name = Me.txtname.Text
            objObjective.idproject = Me.ddlidproject.SelectedValue
            objObjective.enabled = Me.ddlenabled.SelectedValue
            objObjective.iduser = applicationCredentials.UserID
            objObjective.createdate = Now
            objObjective.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            ' modificar el registro
            facade.updateObjective(applicationCredentials, objObjective)

            ' ir al administrador
            Response.Redirect("searchObjective.aspx")

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
            objObjective = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteObjective(applicationCredentials, Request.QueryString("Id"), ViewState("idKey"))

            ' ir al administrador
            Response.Redirect("searchObjective.aspx")

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

            If facade.verifyObjectiveCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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
        Dim list As List(Of ObjectiveEntity)

        Try
            ' cargar la lista de versiones anteriores
            list = facade.getObjectiveList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

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
