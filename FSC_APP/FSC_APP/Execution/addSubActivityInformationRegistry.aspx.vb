Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addSubActivityInformationRegistry
    Inherits System.Web.UI.Page

    Private Property DocumentsList() As List(Of DocumentsEntity)
        Get
            Return DirectCast(Session("documentsList"), List(Of DocumentsEntity))
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Session("documentsList") = value
        End Set
    End Property

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
            Dim idsubactivity As String = Request.QueryString("idsubactivity")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim objSubActivity As New SubActivityEntity
            Dim objActivity As New ActivityEntity
            Dim defaultDate As New DateTime
            TabContainer1.ActiveTabIndex = 0
            'Cargar combos
            loadCombos()
            'Se crea la variable de session que almacena la lista de testimonios por registro de ejecución
            Session("TestimonyList") = New List(Of TestimonyEntity)
            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "Registrar actividad."

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
                    Me.rfvid.Enabled = False
                    Me.rfviduser.Enabled = False
                    Me.rfvcreatedate.Enabled = False
                    Me.btnRefresh.Visible = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objSubActivityInformationRegistry As New SubActivityInformationRegistryEntity
                    Dim idProcessInstance As String = String.Empty
                    Try
                        ' cargar el registro referenciado
                        If idsubactivity <> "" Then

                            If facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity).Count > 0 Then
                                'idProcessInstance = Request.QueryString("idProcessInstance")

                                ' verificar si viene desde el BPM
                                'If idProcessInstance Is Nothing Then
                                objSubActivityInformationRegistry = facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity)(0)
                                'Else
                                '   objSubActivityInformationRegistry = facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity)(0)
                                'End If

                                ViewState("idSubActivityInformationRegistry") = objSubActivityInformationRegistry.id
                                Me.btnDelete.Visible = False
                            Else
                                objSubActivityInformationRegistry.idsubactivity = idsubactivity
                                Me.btnAddData.Visible = True
                                Me.btnSave.Visible = False
                                Me.btnDelete.Visible = False
                                Me.btnCancelDelete.Visible = False
                                Me.btnConfirmDelete.Visible = False
                            End If
                        Else
                            objSubActivityInformationRegistry = facade.loadSubActivityInformationRegistry(applicationCredentials, Request.QueryString("id"))
                        End If

                        If (objSubActivityInformationRegistry.DOCUMENTLIST Is Nothing OrElse objSubActivityInformationRegistry.DOCUMENTLIST.Count = 0) Then Me.btnRefresh.Visible = False

                        'Se carga la lista de documentos adjuntos
                        'Se almacena la lista en una variable de sesion.
                        Me.DocumentsList = objSubActivityInformationRegistry.DOCUMENTLIST
                        Me.gvDocuments.DataSource = objSubActivityInformationRegistry.DOCUMENTLIST
                        Me.gvDocuments.DataBind()

                        ' mostrar los valores
                        Me.txtid.Text = objSubActivityInformationRegistry.id
                        Me.txtdescription.Text = objSubActivityInformationRegistry.description
                        If objSubActivityInformationRegistry.begindate <> defaultDate Then
                            Me.txtbegindate.Text = objSubActivityInformationRegistry.begindate
                        End If
                        If objSubActivityInformationRegistry.enddate <> defaultDate Then
                            Me.txtenddate.Text = objSubActivityInformationRegistry.enddate
                        End If
                        Me.txtcomments.Text = objSubActivityInformationRegistry.comments
                        Me.txtiduser.Text = objSubActivityInformationRegistry.iduser
                        Me.txtcreatedate.Text = objSubActivityInformationRegistry.createdate
                        Me.ddlenabled.SelectedValue = objSubActivityInformationRegistry.state
                        Me.txtqualitativeindicators.Text = objSubActivityInformationRegistry.indicator
                        Me.txtObservation.Text = objSubActivityInformationRegistry.observation

                        'Cargar los combos
                        objSubActivity = facade.loadSubActivity(applicationCredentials, objSubActivityInformationRegistry.idsubactivity)
                        objActivity = facade.loadActivity(applicationCredentials, objSubActivity.idactivity)
                        Me.ddlidproject.SelectedValue = objActivity.idproject
                        ddlidproject_SelectedIndexChanged(sender, e)
                        Me.ddlidobjective.SelectedValue = objActivity.idobjective
                        ddlidobjective_SelectedIndexChanged(sender, e)
                        Me.ddlidcomponent.SelectedValue = objActivity.idcomponent
                        ddlidcomponent_SelectedIndexChanged(sender, e)
                        Me.ddlidactivity.SelectedValue = objActivity.idKey
                        ddlidactivity_SelectedIndexChanged(sender, e)
                        Me.ddlidsubactivity.SelectedValue = objSubActivityInformationRegistry.idsubactivity

                        Me.ddlidproject.Enabled = False
                        Me.ddlidobjective.Enabled = False
                        Me.ddlidcomponent.Enabled = False
                        Me.ddlidactivity.Enabled = False
                        Me.ddlidsubactivity.Enabled = False


                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objSubActivityInformationRegistry = Nothing

                    End Try


                Case "edit", "show"
                    TabContainer1.Tabs(1).Visible = False
                    TabContainer1.Tabs(2).Visible = False
                    TabContainer1.Tabs(3).Visible = False
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
                    Dim objSubActivityInformationRegistry As New SubActivityInformationRegistryEntity
                    Dim idProcessInstance As String = String.Empty
                    Try
                        ' cargar el registro referenciado
                        If idsubactivity <> "" Then

                            If facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity).Count > 0 Then
                                'idProcessInstance = Request.QueryString("idProcessInstance")

                                ' verificar si viene desde el BPM
                                'If idProcessInstance Is Nothing Then
                                objSubActivityInformationRegistry = facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity)(0)
                                'Else
                                '   objSubActivityInformationRegistry = facade.getSubActivityInformationRegistryList(applicationCredentials, idsubactivity:=idsubactivity)(0)
                                'End If

                                ViewState("idSubActivityInformationRegistry") = objSubActivityInformationRegistry.id
                                Me.btnDelete.Visible = False
                            Else
                                objSubActivityInformationRegistry.idsubactivity = idsubactivity
                                Me.btnAddData.Visible = True
                                Me.btnSave.Visible = False
                                Me.btnDelete.Visible = False
                                Me.btnCancelDelete.Visible = False
                                Me.btnConfirmDelete.Visible = False
                            End If
                        Else
                            objSubActivityInformationRegistry = facade.loadSubActivityInformationRegistry(applicationCredentials, Request.QueryString("id"))
                        End If

                        If (objSubActivityInformationRegistry.DOCUMENTLIST Is Nothing OrElse objSubActivityInformationRegistry.DOCUMENTLIST.Count = 0) Then Me.btnRefresh.Visible = False

                        'Se carga la lista de documentos adjuntos
                        'Se almacena la lista en una variable de sesion.
                        Me.DocumentsList = objSubActivityInformationRegistry.DOCUMENTLIST
                        Me.gvDocuments.DataSource = objSubActivityInformationRegistry.DOCUMENTLIST
                        Me.gvDocuments.DataBind()

                        ' mostrar los valores
                        Me.txtid.Text = objSubActivityInformationRegistry.id
                        Me.txtdescription.Text = objSubActivityInformationRegistry.description
                        If objSubActivityInformationRegistry.begindate <> defaultDate Then
                            Me.txtbegindate.Text = objSubActivityInformationRegistry.begindate
                        End If
                        If objSubActivityInformationRegistry.enddate <> defaultDate Then
                            Me.txtenddate.Text = objSubActivityInformationRegistry.enddate
                        End If
                        Me.txtcomments.Text = objSubActivityInformationRegistry.comments
                        Me.txtiduser.Text = objSubActivityInformationRegistry.iduser
                        Me.txtcreatedate.Text = objSubActivityInformationRegistry.createdate
                        Me.ddlenabled.SelectedValue = objSubActivityInformationRegistry.state
                        Me.txtqualitativeindicators.Text = objSubActivityInformationRegistry.indicator
                        Me.txtObservation.Text = objSubActivityInformationRegistry.observation

                        'Cargar los combos
                        objSubActivity = facade.loadSubActivity(applicationCredentials, objSubActivityInformationRegistry.idsubactivity)
                        objActivity = facade.loadActivity(applicationCredentials, objSubActivity.idactivity)
                        Me.ddlidproject.SelectedValue = objActivity.idproject
                        ddlidproject_SelectedIndexChanged(sender, e)
                        Me.ddlidobjective.SelectedValue = objActivity.idobjective
                        ddlidobjective_SelectedIndexChanged(sender, e)
                        Me.ddlidcomponent.SelectedValue = objActivity.idcomponent
                        ddlidcomponent_SelectedIndexChanged(sender, e)
                        Me.ddlidactivity.SelectedValue = objActivity.id
                        ddlidactivity_SelectedIndexChanged(sender, e)
                        Me.ddlidsubactivity.SelectedValue = objSubActivityInformationRegistry.idsubactivity

                        ' Deshabilitar controles
                        Me.lblid.Visible = False
                        Me.txtid.Visible = False
                        Me.lblHelpid.Visible = False
                        Me.ddlidproject.Enabled = False
                        Me.ddlidobjective.Enabled = False
                        Me.ddlidcomponent.Enabled = False
                        Me.ddlidactivity.Enabled = False
                        Me.ddlidsubactivity.Enabled = False
                        Me.lblcreatedate.Visible = False
                        Me.txtcreatedate.Visible = False
                        Me.lblHelpcreatedate.Visible = False
                        Me.lbliduser.Visible = False
                        Me.lblHelpiduser.Visible = False
                        Me.txtiduser.Visible = False


                        Dim idActivityInstance As String = String.Empty

                        ' cargar los valores del BPM
                        idProcessInstance = Request.QueryString("idProcessInstance")
                        idActivityInstance = Request.QueryString("idActivityInstance")

                        ' verificar si viene desde el BPM
                        If idProcessInstance IsNot Nothing Then

                            Me.lblBPMMessage.Visible = True
                            Me.rblCondition.Visible = True
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' cargar la lista de condiciones de la actividad
                            Dim conditions As Array = GattacaApplication.getConditions(applicationCredentials, idActivityInstance)

                            For Each condition As ListItem In conditions

                                ' cargar la lista de condiciones para la actividad
                                Me.rblCondition.Items.Add(New ListItem(condition.Text, condition.Value))

                            Next

                            ' seleccionar el primero
                            Me.rblCondition.SelectedIndex = 0

                            ' cargar el mensaje
                            Me.lblBPMMessage.Text = PublicFunction.getSettingValue("BPM.Condition.Message")

                        End If
                        If op.Equals("show") Then

                            ' cargar el titulo
                            Session("lblTitle") = "MOSTRAR REGISTRO DE ACTIVIDAD."

                            ' ocultar los botones para realizar modificaciones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                        Else

                            ' cargar el titulo
                            Session("lblTitle") = "EDITAR REGISTRO DE ACTIVIDAD."

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
                        objSubActivityInformationRegistry = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objSubActivityInformationRegistry As New SubActivityInformationRegistryEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objExecution As New ExecutionEntity
        Try
            ' cargar los valores registrados por el usuario
            objSubActivityInformationRegistry.idsubactivity = Me.ddlidsubactivity.SelectedValue
            objSubActivityInformationRegistry.description = Me.txtdescription.Text
            If Me.txtbegindate.Text <> "" Then
                objSubActivityInformationRegistry.begindate = Me.txtbegindate.Text
            End If
            If Me.txtenddate.Text <> "" Then
                objSubActivityInformationRegistry.enddate = Me.txtenddate.Text
            End If
            objSubActivityInformationRegistry.comments = Me.txtcomments.Text
            'objSubActivityInformationRegistry.attachment = PublicFunction.LoadFile(Request)
            'objSubActivityInformationRegistry.state = ""
            objSubActivityInformationRegistry.iduser = applicationCredentials.UserID
            objSubActivityInformationRegistry.createdate = Now
            objSubActivityInformationRegistry.observation = txtObservation.Text
            objSubActivityInformationRegistry.indicator = txtqualitativeindicators.Text

            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objSubActivityInformationRegistry, applicationCredentials.UserID)


            'Agregar una ejecución -----------------------------------
            ' cargar los valores registrados por el usuario
            objExecution.idproject = Me.ddlidproject.SelectedValue
            'objExecution.qualitativeindicators = Me.txtqualitativeindicators.Text
            objExecution.learning = Me.txtlearning.Text
            objExecution.adjust = Me.txtadjust.Text
            objExecution.achievements = Me.txtachievements.Text
            objExecution.enable = True
            'objExecution.observation = Me.txtObservation.Text
            objExecution.iduser = applicationCredentials.UserID
            objExecution.createdate = Now

            objExecution.TESTIMONYLIST = DirectCast(Session("TestimonyList"), List(Of TestimonyEntity))

            ' almacenar la entidad
            objExecution.id = facade.addExecution(applicationCredentials, objExecution)

            '---------------------------------------------------------


            ' almacenar la entidad
            objSubActivityInformationRegistry.id = facade.addSubActivityInformationRegistry(applicationCredentials, objSubActivityInformationRegistry)

            ' crear el proceso en el BPM
            objSubActivityInformationRegistry.IdProcessInstance = GattacaApplication.createProcessInstance(applicationCredentials, PublicFunction.getSettingValue("BPM.ProcessCase.PR05"), _
                                                                                 "WebForm", "SubActivityInformationRegistryEntity", objSubActivityInformationRegistry.id, 0)

            ' Iniciarlo
            objSubActivityInformationRegistry.IdActivityInstance = GattacaApplication.startProcessInstance(applicationCredentials, objSubActivityInformationRegistry.IdProcessInstance, _
                                                                                 PublicFunction.getSettingValue("BPM.ProcessCase.PR05"), _
                                                                                 "WebForm", "SubActivityInformationRegistryEntity", objSubActivityInformationRegistry.id, 0)
            ' almacenar la entidad
            facade.updateSubActivityInformationRegistry(applicationCredentials, objSubActivityInformationRegistry)

            ' ir al administrador
            Response.Clear()
            Response.Redirect("subActivityMainPanelTODO-LIST.aspx", False)

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
            objSubActivityInformationRegistry = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("subActivityMainPanelTODO-LIST.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objSubActivityInformationRegistry As New SubActivityInformationRegistryEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty

        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")
        'Cargar el registro referenciado
        If Request.QueryString("Id") <> Nothing Then
            objSubActivityInformationRegistry = facade.loadSubActivityInformationRegistry(applicationCredentials, Request.QueryString("Id"))
        Else
            objSubActivityInformationRegistry = facade.loadSubActivityInformationRegistry(applicationCredentials, txtid.Text)
        End If


        Try
            ' cargar los datos

            objSubActivityInformationRegistry.idsubactivity = Me.ddlidsubactivity.SelectedValue
            objSubActivityInformationRegistry.description = Me.txtdescription.Text
            If Me.txtbegindate.Text <> "" Then
                objSubActivityInformationRegistry.begindate = Me.txtbegindate.Text
            End If
            If Me.txtenddate.Text <> "" Then
                objSubActivityInformationRegistry.enddate = Me.txtenddate.Text
            End If
            objSubActivityInformationRegistry.comments = Me.txtcomments.Text
            objSubActivityInformationRegistry.observation = Me.txtObservation.Text
            objSubActivityInformationRegistry.indicator = Me.txtqualitativeindicators.Text
            ' objSubActivityInformationRegistry.state = Me.ddlenabled.SelectedValue

            'Se recupera la lista de documentos de la variable de sesion correspondiente
            If Not (Me.DocumentsList Is Nothing) Then objSubActivityInformationRegistry.DOCUMENTLIST = Me.DocumentsList

            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objSubActivityInformationRegistry, applicationCredentials.UserID)

            ' modificar el registro
            facade.updateSubActivityInformationRegistry(applicationCredentials, objSubActivityInformationRegistry)

            If idProcessInstance IsNot Nothing Then

                ' finalizar la actividad actual
                GattacaApplication.endActivityInstance(applicationCredentials, idProcessInstance, idActivityInstance, _
                                                       Me.rblCondition.SelectedValue, "Se ha modificado la tarea", _
                                                       "", "", "", "")
                ' cerrar la ventana
                ' ir a la pagina de lista de tareas
                Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

            Else

                ' ir al administrador
                Response.Redirect("subActivityMainPanelTODO-LIST.aspx", False)

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
            objSubActivityInformationRegistry = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Eliminar el registro
            facade.deleteIdea(applicationCredentials, CInt(Request.QueryString("id")), Me.DocumentsList)

            ' eliminar el registro
            facade.deleteSubActivityInformationRegistry(applicationCredentials, Request.QueryString("Id"), Me.DocumentsList)

            ' ir al administrador
            Response.Redirect("searchSubActivityInformationRegistry.aspx")

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
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, isLastVersion:="1", idproject:=ddlidproject.SelectedValue, enabled:="1", order:="Code")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, isLastVersion:="1", idproject:=ddlidproject.SelectedValue, order:="Code")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

            If Me.ddlidobjective.Items.Count > 0 Then
                ddlidobjective_SelectedIndexChanged(sender, e)
            Else
                Me.ddlidcomponent.Items.Clear()
                Me.ddlidactivity.Items.Clear()
                Me.ddlidsubactivity.Items.Clear()
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

    Protected Sub ddlidobjective_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidobjective.SelectedIndexChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos

            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, isLastVersion:="1", idobjective:=ddlidobjective.SelectedValue, enabled:="1", order:="Code")
            Else
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, isLastVersion:="1", idobjective:=ddlidobjective.SelectedValue, order:="Code")
            End If
            Me.ddlidcomponent.DataValueField = "idkey"
            Me.ddlidcomponent.DataTextField = "Code"
            Me.ddlidcomponent.DataBind()

            If Me.ddlidcomponent.Items.Count > 0 Then
                ddlidcomponent_SelectedIndexChanged(sender, e)
            Else
                Me.ddlidactivity.Items.Clear()
                Me.ddlidsubactivity.Items.Clear()
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

    Protected Sub ddlidcomponent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidcomponent.SelectedIndexChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos

            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, isLastVersion:="1", idcomponent:=ddlidcomponent.SelectedValue, enabled:="1", order:="title")
            Else
                Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, isLastVersion:="1", idcomponent:=ddlidcomponent.SelectedValue, order:="title")
            End If
            Me.ddlidactivity.DataValueField = "idkey"
            Me.ddlidactivity.DataTextField = "Title"
            Me.ddlidactivity.DataBind()

            If Me.ddlidactivity.Items.Count > 0 Then
                ddlidactivity_SelectedIndexChanged(sender, e)
            Else
                Me.ddlidsubactivity.Items.Clear()
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

    Protected Sub ddlidactivity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidactivity.SelectedIndexChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Me.ddlidcomponent.Items.Count > 0) Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidsubactivity.DataSource = facade.getSubActivityList(applicationCredentials, isLastVersion:="1", idactivity:=Me.ddlidactivity.SelectedValue, enabled:="1", order:="Name")
                Else
                    Me.ddlidsubactivity.DataSource = facade.getSubActivityList(applicationCredentials, isLastVersion:="1", idactivity:=Me.ddlidactivity.SelectedValue, order:="Name")
                End If
                Me.ddlidsubactivity.DataValueField = "idkey"
                Me.ddlidsubactivity.DataTextField = "Name"
                Me.ddlidsubactivity.DataBind()

            Else

                Me.ddlidsubactivity.Items.Clear()

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

    Protected Sub gvDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocuments.RowDeleting

        'Se recupera la lista de documentos actual
        Dim documentsList As List(Of DocumentsEntity)
        documentsList = Me.DocumentsList

        'Se pone el estado de elminación al documento requerido
        documentsList(e.RowIndex).ISDELETED = True

        'Se oculta de la grilla el registro seleccionado
        Me.gvDocuments.Rows(e.RowIndex).Visible = False

    End Sub

    Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocuments.RowDataBound

        Dim objHyperlink As HyperLink
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim miEntidad As DocumentsEntity = e.Row.DataItem
            objHyperlink = e.Row.Cells(9).Controls(0)
            If (miEntidad.attachfile.Length > 0) Then
                objHyperlink.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "/" & miEntidad.attachfile
                objHyperlink.Target = "_blank"
            End If
        End If
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        'Se llama al metodo que cpnsulta la lista de documentos para el registro de idea actual
        Me.LoadDocumentsList()

        'Se actualiza la grilla.
        Me.gvDocuments.DataSource = Me.DocumentsList
        Me.gvDocuments.DataBind()
        Me.upData.Update()

    End Sub



    Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged
        LoadDropDownCities()
    End Sub


    Protected Sub btnAddTestimony_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddTestimony.Click
        ' definir los objetos
        Dim TestimonyList As List(Of TestimonyEntity)
        Dim Testimony As New TestimonyEntity

        ' cargarla de la session
        TestimonyList = DirectCast(Session("TestimonyList"), List(Of TestimonyEntity))

        Testimony.name = Me.txtName.Text
        Testimony.phone = Me.txtPhone.Text
        Testimony.idcity = Me.ddlCity.SelectedValue
        Testimony.projectrole = Me.txtprojectrole.Text
        Testimony.sex = Me.ddlSex.SelectedValue
        Testimony.email = Me.txtEmail.Text
        Testimony.description = Me.txtDecription.Text
        Testimony.age = Me.txtAge.Text
        Testimony.DEPARTAMENTO = ddlDepto.SelectedItem.ToString()
        ' agregarlos
        TestimonyList.Add(Testimony)

        ' mostrar
        Me.gvTestimony.DataSource = TestimonyList
        Me.gvTestimony.DataBind()

        'Se llama al metodo que permite limpiar los controles de los terceros
        Me.CleanTestimony()

    End Sub



    Protected Sub gvTestimony_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTestimony.SelectedIndexChanged
        ' definir los objetos
        Dim TestimonyList As List(Of TestimonyEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        TestimonyList = DirectCast(Session("TestimonyList"), List(Of TestimonyEntity))

        ' remover el seleccionado
        TestimonyList.RemoveAt(Me.gvTestimony.SelectedIndex)

        ' mostrar
        Me.gvTestimony.DataSource = TestimonyList
        Me.gvTestimony.DataBind()

        'Se selecciona la pestaña del testimonio
        Me.TabContainer1.ActiveTabIndex = 1
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
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Code")
            Else
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", order:="Code")
            End If
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()

            Dim x As New Object
            Dim y As New System.EventArgs

            ddlidproject_SelectedIndexChanged(x, y)




            'Se llama al metodo que permite cargar el combo de departamentos
            Me.LoadDropDownDepto(facade, applicationCredentials)

            'Cargar la lista de los municipos
            Me.LoadDropDownCities()

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

    ''' <summary>
    ''' Permite cargar los archivos sleeccionados
    ''' </summary>
    ''' <param name="objSubActivityInformationRegistry">objeto de tipo objSubActivityInformationRegistry</param>
    ''' <param name="userId">Identificador del usuario actual</param>
    ''' <remarks></remarks>
    Private Sub LoadFiles(ByVal objSubActivityInformationRegistry As SubActivityInformationRegistryEntity, ByVal userId As Long)

        'Definiendo los objtetos
        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files

        'Se verifica que existan archivos por cargar
        If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objSubActivityInformationRegistry.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else
                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objSubActivityInformationRegistry.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objSubActivityInformationRegistry.DOCUMENTLIST = Me.DocumentsList
                End If

            End If

            'Se recorre la lista de archivos cargados al servidor
            For i As Integer = 0 To files.Count - 1

                Dim file As HttpPostedFile = files(i)
                If (file.ContentLength > 0) Then

                    strFileName = file.FileName.Split("\".ToCharArray)

                    ' dar nombre al anexo
                    fileName = Now.ToString("yyyyMMddhhmmss") & "_" & strFileName(strFileName.Length - 1)

                    ' determinanado la ruta destino
                    Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\" & fileName

                    'Subiendo el archivo al server
                    file.SaveAs(sFullPath)

                    'Se instancia un objeto de tipo documento y se pobla con la info. reuqerida.
                    Dim objDocument As New DocumentsEntity()
                    objDocument.attachfile = fileName
                    objDocument.createdate = Now
                    objDocument.iduser = userId
                    objDocument.enabled = True
                    objDocument.ISNEW = True

                    'Se agrega el objeto de tipo documento a la lista de documentos
                    objSubActivityInformationRegistry.DOCUMENTLIST.Add(objDocument)

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Permite actualizar la lista de archivos anexos a la idea actual
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocumentsList()

        'Definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id As Integer = 0

        If ViewState("idSubActivityInformationRegistry") <> Nothing Then
            id = ViewState("idSubActivityInformationRegistry")
        ElseIf Request.QueryString("id") <> Nothing Then
            id = Request.QueryString("id")
        End If

        Try

            'Se definen los objetos
            Dim documentsByEntityList As List(Of DocumentsByEntityEntity)

            'Se llama al metodo que permite consultar la lista de documentos para el registro de idea actual
            'Se carga la lista de documentos para el registro de idea actual
            documentsByEntityList = facade.getDocumentsByEntityList(applicationCredentials, idnentity:=id, entityName:=GetType(SubActivityInformationRegistryEntity).ToString())

            'Se verifica que existam documentos anexos al registro actual
            If (Not documentsByEntityList Is Nothing AndAlso documentsByEntityList.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In documentsByEntityList
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                Me.DocumentsList = facade.getDocumentsListByEntity(applicationCredentials, idsDocuments)
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


    ''' <summary>
    ''' Permite realizar la  limpieza de los controles de la pestaña de testimonio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanTestimony()
        txtAge.Text = ""
        txtDecription.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtName.Text = ""
        txtprojectrole.Text = ""
    End Sub

   

    ''' <summary>
    ''' Permite cargar la lista de municipios segun un depto seleccionado.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownCities()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se carga la lista de los municipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlCity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, enabled:="T", order:="City.Code")
            Else
                Me.ddlCity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, order:="City.Code")
            End If
            Me.ddlCity.DataValueField = "Id"
            Me.ddlCity.DataTextField = "Name"
            Me.ddlCity.DataBind()
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

    ''' <summary>
    ''' Permite cargar la lista de departamentos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownDepto(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        ' cargar la lista de los departamentos
        If (Request.QueryString("op").Equals("add")) Then
            Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, enabled:="T", order:="Depto.Code")
        Else
            Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, order:="Depto.Code")
        End If
        Me.ddlDepto.DataValueField = "Id"
        Me.ddlDepto.DataTextField = "Name"
        Me.ddlDepto.DataBind()

    End Sub

   

#End Region
    
End Class
