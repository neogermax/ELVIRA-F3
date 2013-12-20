Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addDocuments
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve una bandera que indica si el formaulario actual
    ''' debe comportarse como un formulario emergente.
    ''' </summary>
    ''' <value>valor de la bandera</value>
    ''' <returns>true si es emergente, false en caso contrario</returns>
    ''' <remarks></remarks>
    Private Property IsPopup() As Boolean
        Get
            Return DirectCast(ViewState("isPopup"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("isPopup") = value
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
            Me.IsPopup = Convert.ToBoolean(Request.QueryString("isPopup"))
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar los combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO DOCUMENTO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.ddleditedfor.Enabled = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN DOCUMENTO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.ddleditedfor.Enabled = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.rfvattachfile.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objDocuments As New DocumentsEntity

                    Try
                        ' cargar el registro referenciado
                        objDocuments = facade.loadDocuments(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objDocuments.id
                        Me.txttitle.Text = objDocuments.title
                        Me.txtdescription.Text = objDocuments.description
                        Me.ddleditedfor.SelectedValue = objDocuments.ideditedfor
                        Me.ddlVisibilityLevel.SelectedValue = objDocuments.idvisibilitylevel
                        Me.ddlDocumentType.SelectedValue = objDocuments.iddocumenttype
                        Me.txtcreatedate.Text = objDocuments.createdate
                        Me.txtiduser.Text = objDocuments.USERNAME
                        Me.ddlenabled.SelectedValue = objDocuments.enabled
                        If objDocuments.ENTITYNAME = "ProjectEntity" Then
                            ddlProject.SelectedValue = objDocuments.idnEntity
                        Else
                            ddlProject.SelectedValue = 0
                        End If
                        ' cargar y habilitar el archivo anexo
                        Me.hlattachfile.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objDocuments.attachfile
                        Me.hlattachfile.Text = objDocuments.attachfile
                        Me.hlattachfile.Visible = True

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objDocuments = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objDocuments As New DocumentsEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objDocuments.title = Me.txttitle.Text
            objDocuments.description = Me.txtdescription.Text
            objDocuments.ideditedfor = Me.ddleditedfor.SelectedValue
            objDocuments.idvisibilitylevel = Me.ddlVisibilityLevel.SelectedValue
            objDocuments.iddocumenttype = Me.ddlDocumentType.SelectedValue
            objDocuments.createdate = Now
            objDocuments.iduser = applicationCredentials.UserID
            objDocuments.attachfile = PublicFunction.LoadFile(Request)
            objDocuments.enabled = Me.ddlenabled.SelectedValue

            ' almacenar la entidad
            objDocuments.id = facade.addDocuments(applicationCredentials, objDocuments)
            Dim documentByEntity As New DocumentsByEntityEntity()

            documentByEntity.iddocuments = objDocuments.id
            If ddlProject.SelectedValue = "0" Then
                documentByEntity.idnentity = objDocuments.id
                documentByEntity.entityname = objDocuments.GetType.ToString()
            Else
                documentByEntity.idnentity = ddlProject.SelectedValue
                documentByEntity.entityname = "ProjectEntity"

            End If


            'Se llama al metodo que permite almacenar la información del objeto documento por entidad
            facade.addDocumentsByEntity(applicationCredentials, documentByEntity)

            ' ir al administrador
            Response.Redirect("searchDocuments.aspx")

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
            objDocuments = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        'Se verifica si el formulario actual es un formulario emergente
        If (Me.IsPopup) Then
            'Se llama al metodo que permite cerrar el formualrio actual
            Me.closeForm()
        Else
            ' ir al administrador
            Response.Redirect("searchDocuments.aspx")
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objDocuments As New DocumentsEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' cargar el registro referenciado
        objDocuments = facade.loadDocuments(applicationCredentials, Request.QueryString("id"))

        Try
            ' cargar los datos
            objDocuments.title = Me.txttitle.Text
            objDocuments.description = Me.txtdescription.Text
            objDocuments.ideditedfor = Me.ddleditedfor.SelectedValue
            objDocuments.idvisibilitylevel = Me.ddlVisibilityLevel.SelectedValue
            objDocuments.iddocumenttype = Me.ddlDocumentType.SelectedValue
            objDocuments.enabled = Me.ddlenabled.SelectedValue

            ' cargar el archivo
            objDocuments.attachfile = PublicFunction.LoadFile(Request)

            ' si no se modifico el archivo
            If objDocuments.attachfile.Equals(String.Empty) Then
                'cargar el anterior
                objDocuments.attachfile = Me.hlattachfile.Text
            Else
                objDocuments.ISMODIFIED = True
                objDocuments.ATTACHFILEOLD = Me.hlattachfile.Text
            End If

            ' modificar el registro
            facade.updateDocuments(applicationCredentials, objDocuments)
            Dim documentByEntity As New DocumentsByEntityEntity()

            If ddlProject.SelectedValue <> "0" Then
                documentByEntity.idnentity = ddlProject.SelectedValue
                documentByEntity.entityname = "ProjectEntity"
                documentByEntity.iddocuments = objDocuments.id
                'borra la asociación entre documento y entidad
                facade.deleteDocumentsByEntity(applicationCredentials, objDocuments.documentByEntityId)
                'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                facade.addDocumentsByEntity(applicationCredentials, documentByEntity)
            ElseIf ddlProject.SelectedValue = "0" Then
                documentByEntity.idnentity = ddlProject.SelectedValue
                documentByEntity.entityname = "DocumentsEntity"
                documentByEntity.iddocuments = objDocuments.id
                'borra la asociación entre documento y entidad
                facade.deleteDocumentsByEntity(applicationCredentials, objDocuments.documentByEntityId)
                'Se llama al metodo que permite almacenar la información del objeto documento por entidad
                facade.addDocumentsByEntity(applicationCredentials, documentByEntity)

            End If



            'Se verifica si el formulario actual es un formulario emergente
            If (Me.IsPopup) Then
                'Se llama al metodo que permite cerrar el formualrio actual
                Me.closeForm()
            Else
                ' ir al administrador
                Response.Redirect("searchDocuments.aspx")
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
            objDocuments = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteDocuments(applicationCredentials, Request.QueryString("Id"), Me.hlattachfile.Text)

            'Se verifica si el formulario actual es un formulario emergente
            If (Me.IsPopup) Then
                'Se llama al metodo que permite cerrar el formualrio actual
                Me.closeForm()
            Else
                'Ir al administrador
                Response.Redirect("searchDocuments.aspx")
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
            Me.ddleditedfor.DataSource = facade.getUserList(applicationCredentials)
            Me.ddleditedfor.DataValueField = "Id"
            Me.ddleditedfor.DataTextField = "Code"
            Me.ddleditedfor.DataBind()
            'Se asigna el valor del usuario actual
            Me.ddleditedfor.SelectedValue = applicationCredentials.UserID

            'Se agregan las rutinas para cargar el combo de Nivel de visibilidad
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlVisibilityLevel.DataSource = facade.getVisibilityLevelList(applicationCredentials, enabled:="T", order:="Code")
            Else
                Me.ddlVisibilityLevel.DataSource = facade.getVisibilityLevelList(applicationCredentials, enabled:="T", order:="Code")
            End If
            Me.ddlVisibilityLevel.DataValueField = "Id"
            Me.ddlVisibilityLevel.DataTextField = "Code"
            Me.ddlVisibilityLevel.DataBind()

            'Se agregan las rutinas para cargar el combo de tipos de documento
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlDocumentType.DataSource = facade.getDocumentTypeList(applicationCredentials, enabled:="1", order:="Code")
            Else
                Me.ddlDocumentType.DataSource = facade.getDocumentTypeList(applicationCredentials, order:="Code")
            End If

            ' cargar la lista de los proyectos
            Me.ddlProject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Code")
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

            'Se agrega el item todos
            Me.ddlProject.Items.Add(New ListItem("Otro", "0"))
            Me.ddlProject.SelectedValue = 0


            Me.ddlDocumentType.DataValueField = "Id"
            Me.ddlDocumentType.DataTextField = "Code"
            Me.ddlDocumentType.DataBind()

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
    ''' Metodo para cerrar esta pagina
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub closeForm()

        ' actualizar la ventana padre
        'ScriptManager.RegisterStartupScript(Me, GetType(String), "reload", "<script language='javascript'>parent.location.reload();alert('Formulario Agregado.');</script>", False)

        ' cerrar esta pagina
        ScriptManager.RegisterStartupScript(Me, GetType(String), "close", "<script>window.close()</script>", False)

    End Sub

#End Region

End Class
