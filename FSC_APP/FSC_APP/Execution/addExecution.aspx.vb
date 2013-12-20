Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addExecution
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
            ' cargar los combos
            loadCombos()
            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA EJECUCIÓN"

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    Me.btnRefresh.Visible = False

                    'Se crea la variable de session que almacena la lista de testimonios por registro de ejecución
                    Session("TestimonyList") = New List(Of TestimonyEntity)
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA EJECUCIÓN."

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
                    Dim objExecution As New ExecutionEntity

                    Try
                        ' cargar el registro referenciado
                        objExecution = facade.loadExecution(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objExecution.id
                        'Me.txtidproject.Text = objExecution.idproject
                        Me.txtqualitativeindicators.Text = objExecution.qualitativeindicators
                        Me.txtlearning.Text = objExecution.learning
                        Me.txtadjust.Text = objExecution.adjust
                        Me.txtachievements.Text = objExecution.achievements
                        Me.ddlenabled.SelectedValue = objExecution.enable
                        Me.txtiduser.Text = objExecution.USERNAME
                        Me.txtcreatedate.Text = objExecution.createdate
                        Me.ddlProject.SelectedValue = objExecution.idproject
                        If (objExecution.DOCUMENTLIST Is Nothing OrElse objExecution.DOCUMENTLIST.Count = 0) Then Me.btnRefresh.Visible = False

                        'Se carga la lista de documentos adjuntos
                        'Se almacena la lista en una variable de sesion.
                        Me.DocumentsList = objExecution.DOCUMENTLIST
                        Me.gvDocuments.DataSource = objExecution.DOCUMENTLIST
                        Me.gvDocuments.DataBind()

                        'Se carga la lista de testimonios por ejecucion de la base de datos
                        Session("TestimonyList") = objExecution.TESTIMONYLIST
                        ' Se actualiza la informacion de la grilla
                        Me.gvTestimony.DataSource = objExecution.TESTIMONYLIST
                        Me.gvTestimony.DataBind()


                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objExecution = Nothing

                    End Try

            End Select
            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0


        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objExecution As New ExecutionEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objExecution.idproject = Me.ddlProject.SelectedValue
            objExecution.qualitativeindicators = Me.txtqualitativeindicators.Text
            objExecution.learning = Me.txtlearning.Text
            objExecution.adjust = Me.txtadjust.Text
            objExecution.achievements = Me.txtachievements.Text
            objExecution.enable = Me.ddlenabled.SelectedValue
            objExecution.iduser = applicationCredentials.UserID
            objExecution.createdate = Now

            objExecution.TESTIMONYLIST = DirectCast(Session("TestimonyList"), List(Of TestimonyEntity))
            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objExecution, applicationCredentials.UserID)

            ' almacenar la entidad
            objExecution.id = facade.addExecution(applicationCredentials, objExecution)

            ' ir al administrador
            Response.Redirect("searchExecution.aspx")

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
            objExecution = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchExecution.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objExecution As New ExecutionEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los datos
            objExecution.idproject = Me.ddlProject.SelectedValue
            objExecution.qualitativeindicators = Me.txtqualitativeindicators.Text
            objExecution.learning = Me.txtlearning.Text
            objExecution.adjust = Me.txtadjust.Text
            objExecution.achievements = Me.txtachievements.Text
            objExecution.enable = ddlenabled.SelectedValue
            objExecution.id = txtid.Text

            'Se recupera la lista de documentos de la variable de sesion correspondiente
            If Not (Me.DocumentsList Is Nothing) Then objExecution.DOCUMENTLIST = Me.DocumentsList

            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objExecution, applicationCredentials.UserID)

            'Se agrega la lista de testimonio 
            objExecution.TESTIMONYLIST = DirectCast(Session("TestimonyList"), List(Of TestimonyEntity))

            ' modificar el registro
            facade.updateExecution(applicationCredentials, objExecution)

            ' ir al administrador
            Response.Redirect("searchExecution.aspx")

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
            objExecution = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteExecution(applicationCredentials, Request.QueryString("Id"), Me.DocumentsList)

            ' ir al administrador
            Response.Redirect("searchExecution.aspx")

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


    Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click, btnCancelDelete.Click

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
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite cargar los archivos seleccionados
    ''' </summary>
    ''' <param name="objexecution">objeto de tipo ExecutionEntity</param>
    ''' <param name="userId">Identificador del usuario actual</param>
    ''' <remarks></remarks>
    Private Sub LoadFiles(ByVal objexecution As ExecutionEntity, ByVal userId As Long)

        'Definiendo los objtetos
        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files

        'Se verifica que existan archivos por cargar
        If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objexecution.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else
                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objexecution.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objexecution.DOCUMENTLIST = Me.DocumentsList
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
                    objexecution.DOCUMENTLIST.Add(objDocument)

                End If

            Next

        End If

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
    ''' Metodo que permite cargar el combo del proyecto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListProject()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idClosedState As String = ""

        Try
            'Se consulta el código correspondiente a la fase de "Evaluación y Cierre"
            idClosedState = ConfigurationManager.AppSettings("IdClosedState").ToString()

            'Se pobla el combo
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlProject.DataSource = facade.getProjectListNotInPhase(applicationCredentials, idphase:=idClosedState, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlProject.DataSource = facade.getProjectListNotInPhase(applicationCredentials, idphase:=idClosedState, order:="Code", isLastVersion:="1")
            End If
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

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
          

            'Se llama al metodo que permite cargar el combo de departamentos
            Me.LoadDropDownDepto(facade, applicationCredentials)

            'Cargar la lista de los municipos
            Me.LoadDropDownCities()

            'Carga la lista de proyectos
            Me.LoadDropDownListProject()

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
            documentsByEntityList = facade.getDocumentsByEntityList(applicationCredentials, idnentity:=Request.QueryString("id"), entityName:=GetType(TestimonyEntity).ToString())

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
