Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addProposal
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve una lista de objetos de tipo DocumentsEntity
    ''' </summary>
    ''' <value>una lista de objetos de tipo DocumentsEntity</value>
    ''' <returns>una lista de objetos de tipo DocumentsEntity</returns>
    ''' <remarks></remarks>
    Private Property DocumentsList() As List(Of DocumentsEntity)
        Get
            Return DirectCast(Session("documentsList"), List(Of DocumentsEntity))
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Session("documentsList") = value
        End Set
    End Property

#End Region

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

            'Cargar los combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA PROPUESTA."

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
                    Me.lblresponsiblereview.Visible = False
                    Me.txtresponsiblereview.Visible = False
                    Me.lblreviewdate.Visible = False
                    Me.txtreviewdate.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancel.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False

                    'Se deshabilita la pestaña de Registro de Revisión
                    Me.TabContainer1.Tabs(3).Visible = False

                    'Crear la lista de ubicaciones
                    Session("locationsList") = New List(Of LocationByProposalEntity)

                    ' cargar el id de la convocatoria
                    Dim IdSummoning As String = Request.QueryString("IdSummoning")

                    ' cargarlo
                    Me.ddlSummoning.SelectedValue = IdSummoning

                    ' desabilitarlo
                    Me.ddlSummoning.Enabled = False

                Case "edit", "show"

                   

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
                    Me.lblresponsiblereview.Enabled = False
                    Me.txtresponsiblereview.Enabled = False
                    Me.lblreviewdate.Enabled = False
                    Me.txtreviewdate.Enabled = False
                    Me.btnDelete.Visible = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False

                    'Se deshabilitan las pestañas de datos principales, archivos anexos y ubicaciones
                    Me.TabContainer1.Tabs(0).Enabled = False
                    Me.tableInfoLocations.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objProposal As New ProposalEntity

                    Try
                        ' cargar el registro referenciado
                        objProposal = facade.loadProposal(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objProposal.id
                        Me.ddlSummoning.SelectedValue = objProposal.idsummoning
                        Me.txtoperator.Text = objProposal.nameOperator
                        Me.txtoperatornit.Text = objProposal.operatornit
                        Me.txtprojectname.Text = objProposal.projectname
                        Me.txttarget.Text = objProposal.target
                        Me.txttargetpopulation.Text = objProposal.targetpopulation
                        Me.txtexpectedresults.Text = objProposal.expectedresults
                        Me.txttotalvalue.Text = objProposal.totalvalue.ToString("#,###")
                        Me.txtinputfsc.Text = objProposal.inputfsc.ToString("#,###")
                        Me.txtinputothersources.Text = objProposal.inputothersources.ToString("#,###")
                        Me.txtbriefprojectdescription.Text = objProposal.briefprojectdescription
                        Me.txtscore.Text = objProposal.score
                        Me.ddlResult.SelectedValue = objProposal.result
                        Me.txtresponsiblereview.Text = objProposal.RESPONSIBLEREVIEWNAME
                        Me.txtreviewdate.Text = objProposal.reviewdate
                        Me.ddlenabled.SelectedValue = objProposal.enabled
                        Me.txtcreatedate.Text = objProposal.createdate
                        Me.txtiduser.Text = objProposal.USERNAME

                        'Se carga la lista de documentos adjuntos y se almacena la lista en una variable de sesion.
                        Me.DocumentsList = objProposal.DOCUMENTLIST
                        Me.gvDocuments.DataSource = objProposal.DOCUMENTLIST
                        Me.gvDocuments.DataBind()

                        'Se carga la lista de ubicaciones por idea de la base de datos
                        Session("locationsList") = objProposal.LOCATIONSLIST
                        ' Se actualiza la informacion de la grilla
                        Me.gvLocations.DataSource = objProposal.LOCATIONSLIST
                        Me.gvLocations.DataBind()

                        Dim idProcessInstance As String = String.Empty
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
                            Session("lblTitle") = "MOSTRAR UNA PROPUESTA."

                            ' ocultar los botones para realizar modificaciones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                        Else

                            ' cargar el titulo
                            Session("lblTitle") = "EDITAR UNA PROPUESTA."

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
                        objProposal = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objProposal As New ProposalEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objProposal.idsummoning = Me.ddlSummoning.SelectedValue
            objProposal.nameOperator = Me.txtoperator.Text
            objProposal.operatornit = Me.txtoperatornit.Text
            objProposal.projectname = Me.txtprojectname.Text
            objProposal.target = Me.txttarget.Text
            objProposal.targetpopulation = Me.txttargetpopulation.Text
            objProposal.expectedresults = Me.txtexpectedresults.Text
            objProposal.totalvalue = PublicFunction.ConvertStringToDouble(Me.txttotalvalue.Text)
            objProposal.inputfsc = PublicFunction.ConvertStringToDouble(Me.txtinputfsc.Text)
            objProposal.inputothersources = PublicFunction.ConvertStringToDouble(Me.txtinputothersources.Text)
            objProposal.briefprojectdescription = Me.txtbriefprojectdescription.Text
            objProposal.enabled = Me.ddlenabled.SelectedValue
            objProposal.createdate = Now
            objProposal.iduser = applicationCredentials.UserID
            ''Datos de la pestaña registro de revisión 
            'objProposal.score = PublicFunction.ConvertStringToDouble(Me.txtscore.Text)
            'objProposal.result = Me.ddlResult.SelectedValue
            'objProposal.responsiblereview = applicationCredentials.UserID
            'objProposal.reviewdate = Now

            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objProposal, applicationCredentials.UserID)

            'Se garega la lista de ubicaciones agregada
            objProposal.LOCATIONSLIST = DirectCast(Session("locationsList"), List(Of LocationByProposalEntity))

            ' almacenar la entidad
            objProposal.id = facade.addProposal(applicationCredentials, objProposal)

            ' crear el proceso en el BPM
            objProposal.IdProcessInstance = GattacaApplication.createProcessInstance(applicationCredentials, PublicFunction.getSettingValue("BPM.ProcessCase.PR04"), _
                                                                                 "WebForm", "ProposalEntity", objProposal.id, 0)

            ' Iniciarlo
            objProposal.IdActivityInstance = GattacaApplication.startProcessInstance(applicationCredentials, objProposal.IdProcessInstance, _
                                                                                 PublicFunction.getSettingValue("BPM.ProcessCase.PR04"), _
                                                                                 "WebForm", "ProposalEntity", objProposal.id, 0)
            ' actualizar
            facade.updateProposal(applicationCredentials, objProposal)
            Alert("La propuesta se guardo con éxito", Me)
            ' cerrar esta pagina
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "close", "<script type='text/javascript'> window.close(); </script>", False)

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
            objProposal = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchProposal.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objProposal As New ProposalEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty

        ' cargar el registro referenciado
        objProposal = facade.loadProposal(applicationCredentials, Request.QueryString("id"))

        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")

        Try
            ' cargar los datos
            objProposal.idsummoning = Me.ddlSummoning.SelectedValue
            objProposal.nameOperator = Me.txtoperator.Text
            objProposal.operatornit = Me.txtoperatornit.Text
            objProposal.projectname = Me.txtprojectname.Text
            objProposal.target = Me.txttarget.Text
            objProposal.targetpopulation = Me.txttargetpopulation.Text
            objProposal.expectedresults = Me.txtexpectedresults.Text
            objProposal.totalvalue = PublicFunction.ConvertStringToDouble(Me.txttotalvalue.Text)
            objProposal.inputfsc = PublicFunction.ConvertStringToDouble(Me.txtinputfsc.Text)
            objProposal.inputothersources = PublicFunction.ConvertStringToDouble(Me.txtinputothersources.Text)
            objProposal.briefprojectdescription = Me.txtbriefprojectdescription.Text
            objProposal.enabled = Me.ddlenabled.SelectedValue

            'Datos de la pestaña registro de revisión 
            objProposal.score = PublicFunction.ConvertStringToDouble(Me.txtscore.Text)
            objProposal.result = Me.ddlResult.SelectedValue
            objProposal.responsiblereview = applicationCredentials.UserID
            objProposal.reviewdate = Now()

            'Se recupera la lista de documentos de la variable de sesion correspondiente
            If Not (Me.DocumentsList Is Nothing) Then objProposal.DOCUMENTLIST = Me.DocumentsList

            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objProposal, applicationCredentials.UserID)

            'Se agrega la lista de ubicaciones actual
            objProposal.LOCATIONSLIST = DirectCast(Session("locationsList"), List(Of LocationByProposalEntity))

            ' modificar el registro
            facade.updateProposal(applicationCredentials, objProposal)

            If idProcessInstance IsNot Nothing Then

                ' finalizar la actividad actual
                GattacaApplication.endActivityInstance(applicationCredentials, idProcessInstance, idActivityInstance, _
                                                       Me.rblCondition.SelectedValue, "Se ha modificado la propuesta", _
                                                       "", "", "", "")
                ' cerrar la ventana
                ' ir a la pagina de lista de tareas
                Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

            Else

                ' ir al administrador
                Response.Redirect("searchProposal.aspx")

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
            objProposal = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteProposal(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchProposal.aspx")

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

    Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged
        'Se llama al metodo que permite cargar el combo de ciudades
        Me.LoadDropDownCities()
    End Sub

    Protected Sub btnAgregarubicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarubicacion.Click

        'Se verifica que los combos de seleccion tengan valores
        If (Me.ddlDepto.SelectedValue.Length > 0 AndAlso Me.ddlCity.SelectedValue.Length > 0) Then
            ' definir los objetos
            Dim locationsList As List(Of LocationByProposalEntity)
            Dim location As New LocationByProposalEntity

            'Se inicializan los controles requeridos
            Me.lblLocationMessage.Text = ""

            ' cargarla de la session
            locationsList = DirectCast(Session("locationsList"), List(Of LocationByProposalEntity))

            location.DEPTO.id = Me.ddlDepto.SelectedValue
            location.DEPTO.name = Me.ddlDepto.SelectedItem.Text
            location.CITY.id = Me.ddlCity.SelectedValue
            location.CITY.name = Me.ddlCity.SelectedItem.Text

            If (locationsList.Exists(Function(unLocation) unLocation.CITY.id = location.CITY.id)) Then
                Me.lblLocationMessage.Text = "La ubicación que desea agregar ya se encuentra registrada, por favor verifique."
            Else

                ' agregarlos
                locationsList.Add(location)

                ' mostrar
                Me.gvLocations.DataSource = locationsList
                Me.gvLocations.DataBind()
            End If
        End If
    End Sub

    Protected Sub gvLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLocations.SelectedIndexChanged

        ' definir los objetos
        Dim locationList As List(Of LocationByProposalEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        locationList = DirectCast(Session("locationsList"), List(Of LocationByProposalEntity))

        ' remover el seleccionado
        locationList.RemoveAt(Me.gvLocations.SelectedIndex)

        ' mostrar
        Me.gvLocations.DataSource = locationList
        Me.gvLocations.DataBind()

        'Se selecciona la pestama de ubicaciones por idea
        Me.TabContainer1.ActiveTabIndex = 1

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

    Protected Sub gvDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocuments.RowDeleting

        'Se recupera la lista de documentos actual
        Dim documentsList As List(Of DocumentsEntity)
        documentsList = Me.DocumentsList

        'Se pone el estado de elminación al documento requerido
        documentsList(e.RowIndex).ISDELETED = True

        'Se oculta de la grilla el registro seleccionado
        Me.gvDocuments.Rows(e.RowIndex).Visible = False

    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        'Se llama al metodo que consulta la lista de documentos para el registro de propuesta actual
        Me.LoadDocumentsList()

        'Se actualiza la grilla.
        Me.gvDocuments.DataSource = Me.DocumentsList
        Me.gvDocuments.DataBind()

    End Sub

#End Region

#Region "Metodos"
    Public Sub Alert(ByVal msg As String, ByRef P As Page)
        Dim strScript As String
        strScript = "<script language=javascript> alert('" + msg + ".'); window.close();</script>"
        P.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", strScript)
    End Sub

    ''' <summary>
    ''' Permite cargar los archivos sleeccionados
    ''' </summary>
    ''' <param name="objIdea">objeto de tipo IdeaEntity</param>
    ''' <param name="userId">Identificador del usuario actual</param>
    ''' <remarks></remarks>
    Private Sub LoadFiles(ByVal objProposal As ProposalEntity, ByVal userId As Long)

        'Definiendo los objtetos
        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files

        'Se verifica que existan archivos por cargar
        If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objProposal.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else

                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objProposal.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objProposal.DOCUMENTLIST = Me.DocumentsList
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
                    objProposal.DOCUMENTLIST.Add(objDocument)

                End If

            Next

        End If

    End Sub

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
            'Se pobla el combo
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlSummoning.DataSource = facade.getSummoningList(applicationCredentials, enabled:="1", order:="Code")
            Else
                Me.ddlSummoning.DataSource = facade.getSummoningList(applicationCredentials, order:="Code")
            End If
            Me.ddlSummoning.DataValueField = "Id"
            Me.ddlSummoning.DataTextField = "Code"
            Me.ddlSummoning.DataBind()

            'Agregar la opcion No Aplica
            Me.ddlSummoning.Items.Add(New ListItem("No Aplica", "0"))

            'Se llama al metodo que permite cargar el combo de departamentos
            Me.LoadDropDownDepto(facade, applicationCredentials)

            'Cargar la lista de los municipos
            Me.LoadDropDownCities()

            ' seleccionar
            Me.ddlSummoning.SelectedValue = "0"

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
        Me.ddlDepto.DataTextField = "name"
        Me.ddlDepto.DataBind()

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
            Me.ddlCity.DataTextField = "name"
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
    ''' Permite actualizar la lista de archivos anexos a la idea actual
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocumentsList()

        'Definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se definen los objetos
            Dim documentsByEntityList As List(Of DocumentsByEntityEntity)

            'Se llama al metodo que permite consultar la lista de documentos para el registro de idea actual
            'Se carga la lista de documentos para el registro de idea actual
            documentsByEntityList = facade.getDocumentsByEntityList(applicationCredentials, idnentity:=Request.QueryString("id"), entityName:=GetType(ProposalEntity).ToString())

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

#End Region

End Class
