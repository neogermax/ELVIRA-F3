Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addActivity
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
                    Session("lblTitle") = "AGREGAR UNA NUEVA ACTIVIDAD."

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
                    Session("lblTitle") = "EDITAR UN ACTIVIDAD."

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
                    Dim objActivity As New ActivityEntity

                    Try
                        ' cargar el registro referenciado
                        objActivity = facade.loadActivity(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objActivity.id
                        Me.txtnumber.Text = objActivity.number
                        Me.txttitle.Text = objActivity.title
                        'Cargar adecuadamente los combos
                        Dim objComponent As New ComponentEntity
                        Dim objObjective As New ObjectiveEntity
                        Dim objProject As New ProjectEntity
                        objComponent = facade.loadComponent(applicationCredentials, objActivity.idcomponent)
                        objObjective = facade.loadObjective(applicationCredentials, objComponent.idobjective)
                        objProject = facade.loadProject(applicationCredentials, objObjective.idproject)
                        Me.ddlidproject.SelectedValue = objProject.idKey
                        Me.ddlidproject_SelectedIndexChanged(sender, e)
                        Me.ddlidproject.Enabled = False
                        Me.ddlidobjective.SelectedValue = objObjective.id
                        Me.ddlidobjective_SelectedIndexChanged(sender, e)
                        Me.ddlidcomponent.SelectedValue = objComponent.id

                        Me.ddlidcomponent.SelectedValue = objActivity.idcomponent
                        Me.txtdescription.Text = objActivity.description
                        Me.ddlenabled.SelectedValue = objActivity.enabled
                        Me.txtiduser.Text = objActivity.USERNAME
                        Me.txtcreatedate.Text = objActivity.createdate
                        Me.LoadObjectiveActivity(objActivity)

                        ' guardar
                        ViewState("idKey") = objActivity.idKey

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' limpiar label
                            Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            loadVersions(objActivity.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase de la actividad es la fase de cerrado
                            Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            If (objActivity.idphase.ToString() = idClosedState) Then
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
                        objActivity = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objActivity As New ActivityEntity
        Dim myObjectiveByActivityList As List(Of ObjectiveByActivityEntity) = New List(Of ObjectiveByActivityEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyNumber() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objActivity.number = Me.txtnumber.Text
            objActivity.title = Me.txttitle.Text
            objActivity.idcomponent = Me.ddlidcomponent.SelectedValue
            objActivity.description = Me.txtdescription.Text
            objActivity.enabled = Me.ddlenabled.SelectedValue
            objActivity.iduser = applicationCredentials.UserID
            objActivity.createdate = Now
            ' se busca la fase
            objActivity.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            'Se recorre la lista de objectivos seleccionadas
            For Each item As ListItem In Me.dlbObjective.SelectedItems.Items
                Dim myObjectiveByActivity As New ObjectiveByActivityEntity
                myObjectiveByActivity.idobjective = item.Value
                myObjectiveByActivityList.Add(myObjectiveByActivity)
            Next
            'Se almacena en el objeto idea la lista de objetivos obtenidos
            objActivity.OBJECTIVEBYACTIVITYLIST = myObjectiveByActivityList

            ' almacenar la entidad
            objActivity.id = facade.addActivity(applicationCredentials, objActivity)

            ' ir al administrador
            Response.Redirect("searchActivity.aspx")

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
            objActivity = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchActivity.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objActivity As New ActivityEntity
        Dim myObjectiveByActivityList As List(Of ObjectiveByActivityEntity) = New List(Of ObjectiveByActivityEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyNumber() Then
            Return
        End If

        'Cargar el registro referenciado
        objActivity = facade.loadActivity(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos
            objActivity.number = Me.txtnumber.Text
            objActivity.title = Me.txttitle.Text
            objActivity.idcomponent = Me.ddlidcomponent.SelectedValue
            objActivity.description = Me.txtdescription.Text
            objActivity.enabled = Me.ddlenabled.SelectedValue
            objActivity.iduser = applicationCredentials.UserID
            objActivity.createdate = Now
            ' se busca la fase
            objActivity.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            'Se recorre la lista de objectivos seleccionadas
            For Each item As ListItem In Me.dlbObjective.SelectedItems.Items
                Dim myObjectiveByActivity As New ObjectiveByActivityEntity
                myObjectiveByActivity.idobjective = item.Value
                myObjectiveByActivityList.Add(myObjectiveByActivity)
            Next
            'Se almacena en el objeto idea la lista de objetivos obtenidos
            objActivity.OBJECTIVEBYACTIVITYLIST = myObjectiveByActivityList
            ' modificar el registro
            facade.updateActivity(applicationCredentials, objActivity)

            ' ir al administrador
            Response.Redirect("searchActivity.aspx")

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
            objActivity = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteActivity(applicationCredentials, Request.QueryString("Id"), ViewState("idKey"))

            ' ir al administrador
            Response.Redirect("searchActivity.aspx")

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
            Dim idproject1 As String = "0"
            Dim idobjective1 As String = "0"
            If ddlidproject.SelectedValue <> "" Then
                idproject1 = ddlidproject.SelectedValue
            End If
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=idproject1, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=idproject1, order:="code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()


            If ddlidobjective.SelectedValue <> "" Then
                idobjective1 = ddlidobjective.SelectedValue
            End If
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=idobjective1, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=idobjective1, order:="code", isLastVersion:="1")
            End If
            Me.ddlidcomponent.DataValueField = "idkey"
            Me.ddlidcomponent.DataTextField = "Code"
            Me.ddlidcomponent.DataBind()

            Me.dlbObjective.SelectedItems.Items.Clear()
            LoadListObjectives()

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
            Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, isLastVersion:="1")
            Me.ddlidcomponent.DataValueField = "idkey"
            Me.ddlidcomponent.DataTextField = "Code"
            Me.ddlidcomponent.DataBind()

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

    Protected Sub txtnumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtnumber.TextChanged
        'Verificar Numero
        verifyNumber()
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
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, order:="Code", isLastVersion:="1")
            End If

            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, order:="Code", isLastVersion:="1")
            End If

            Me.ddlidcomponent.DataValueField = "idkey"
            Me.ddlidcomponent.DataTextField = "Code"
            Me.ddlidcomponent.DataBind()
            Me.dlbObjective.SelectedItems.Items.Clear()
            LoadListObjectives()

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
    ''' Metodo que permite cargar la lista de objetivos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListObjectives()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se verifica que exista una programa previamente seleccionada

            'Se pobla el combo
            If (Request.QueryString("op").Equals("add")) Then
                Me.dlbObjective.AviableItems.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.dlbObjective.AviableItems.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, order:="Code", isLastVersion:="1")
            End If
            Me.dlbObjective.AviableItems.DataValueField = "idkey"
            Me.dlbObjective.AviableItems.DataTextField = "Code"
            Me.dlbObjective.AviableItems.DataBind()
            'Limpiar items
            Me.dlbObjective.SelectedItems.Items.Clear()


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
    ''' Metodo que permite cargar la lista Objectivos por actividad almacenadas en la base de datos
    ''' </summary>
    ''' <param name="unObjIdActivity">onjeto de tipo IdeaEntity</param>
    ''' <remarks></remarks>
    Private Sub LoadObjectiveActivity(ByVal unObjIdActivity As ActivityEntity)

        'Se verifica que existan valores en la lista de actividades esppecificas
        If (unObjIdActivity.OBJECTIVEBYACTIVITYLIST.Count > 0) Then

            'Se limpia la lista de activiades especificas
            Me.dlbObjective.AviableItems.Items.Clear()
            'Se carga la lista de Componentes del Programa por idea de la base de datos
            Me.LoadListObjectives()
            'Se recorre la lista de actividades seleccionadas y almacenadas en la base de datos
            Dim miItem As ListItem
            For Each myObjectiveActivity As ObjectiveByActivityEntity In unObjIdActivity.OBJECTIVEBYACTIVITYLIST
                'Se elimina la Componente del Programa actual de la lista de disponibles.
                miItem = Me.dlbObjective.AviableItems.Items.FindByValue(myObjectiveActivity.idobjective.ToString())
                Me.dlbObjective.AviableItems.Items.Remove(miItem)
                'Se agrega en la lista de seleccionadas
                If Not (miItem Is Nothing) Then Me.dlbObjective.SelectedItems.Items.Add(miItem)
            Next
        End If

    End Sub

    Private Function verifyNumber() As Boolean
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifyActivityNumber(applicationCredentials, Me.txtnumber.Text, Me.txtid.Text) Then
                lblHelpnumber.Text = "Este número ya existe, por favor cámbielo"
                rfvnumber.IsValid = 0
                verifyNumber = 0
            Else
                lblHelpnumber.Text = ""
                rfvnumber.IsValid = 1
                verifyNumber = 1
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
        Dim list As List(Of ActivityEntity)

        Try
            ' cargar la lista de versiones anteriores
            list = facade.getActivityList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

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
