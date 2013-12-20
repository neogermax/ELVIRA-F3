Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addSubActivity
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
            Dim defaultDate As New DateTime

            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR NUEVA SUB ACTIVIDAD."

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
                    Me.rfvid.Visible = False
                    Me.rfviduser.Visible = False
                    Me.rfvcreatedate.Visible = False

                Case "edit", "show"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR SUB ACTIVIDAD."

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
                    Dim objSubActivity As New SubActivityEntity

                    Try
                        ' cargar el registro referenciado
                        objSubActivity = facade.loadSubActivity(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objSubActivity.id
                        'Cargar adecuadamente los combos
                        Dim objActiviy As New ActivityEntity
                        Dim objComponent As New ComponentEntity
                        Dim objObjective As New ObjectiveEntity
                        Dim objProject As New ProjectEntity

                        objActiviy = facade.loadActivity(applicationCredentials, objSubActivity.idactivity)
                        objComponent = facade.loadComponent(applicationCredentials, objActiviy.idcomponent)
                        objObjective = facade.loadObjective(applicationCredentials, objComponent.idobjective)
                        objProject = facade.loadProject(applicationCredentials, objObjective.idproject)
                        Me.ddlidproject.SelectedValue = objProject.idKey
                        Me.ddlidproject_SelectedIndexChanged(sender, e)
                        Me.ddlidproject.Enabled = False
                        Me.ddlidobjective.SelectedValue = objObjective.idKey
                        Me.ddlidobjective_SelectedIndexChanged(sender, e)
                        Me.ddlidcomponent.SelectedValue = objComponent.idKey
                        Me.ddlidcomponent_SelectedIndexChanged(sender, e)
                        Me.ddlidactivity.SelectedValue = objSubActivity.idactivity

                        Me.ddltype.SelectedValue = objSubActivity.type
                        Me.txtnumber.Text = objSubActivity.number
                        Me.txtname.Text = objSubActivity.name
                        Me.txtdescription.Text = objSubActivity.description
                        Me.ddlidresponsible.SelectedValue = objSubActivity.idresponsible
                        Me.txtbegindate.Text = IIf((objSubActivity.begindate = defaultDate), "", objSubActivity.begindate)
                        Me.txtenddate.Text = IIf((objSubActivity.enddate = defaultDate), "", objSubActivity.enddate)
                        Me.txttotalcost.Text = objSubActivity.totalcost.ToString("#,###")
                        Me.txtduration.Text = objSubActivity.duration
                        Me.txtfsccontribution.Text = objSubActivity.fsccontribution.ToString("#,###")
                        Me.txtofcontribution.Text = objSubActivity.ofcontribution.ToString("#,###")
                        Me.cbcriticalpath.Checked = objSubActivity.criticalpath
                        Me.cbrequiresapproval.Checked = objSubActivity.requiresapproval
                        Me.ddlenabled.SelectedValue = objSubActivity.enabled
                        Me.txtiduser.Text = objSubActivity.USERNAME
                        Me.txtcreatedate.Text = objSubActivity.createdate

                        ' cargar y habilitar el archivo anexo
                        Me.hlattachment.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objSubActivity.attachment
                        Me.hlattachment.Text = objSubActivity.attachment
                        Me.hlattachment.Visible = True

                        ' guardar
                        ViewState("idKey") = objSubActivity.idKey

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' limpiar label
                            Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            loadVersions(objSubActivity.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase de la subactividad es la fase de cerrado
                            Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            If (objSubActivity.idphase.ToString() = idClosedState) Then
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
                        objSubActivity = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objSubActivity As New SubActivityEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verficación de código
        If Not verifyNumber() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objSubActivity.idactivity = Me.ddlidactivity.SelectedValue
            objSubActivity.type = Me.ddltype.SelectedValue
            objSubActivity.number = Me.txtnumber.Text
            objSubActivity.name = Me.txtname.Text
            objSubActivity.description = Me.txtdescription.Text
            objSubActivity.idresponsible = Me.ddlidresponsible.SelectedValue
            objSubActivity.begindate = IIf((Me.txtbegindate.Text = ""), Nothing, Me.txtbegindate.Text)
            objSubActivity.enddate = IIf((Me.txtenddate.Text = ""), Nothing, Me.txtenddate.Text)
            objSubActivity.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            objSubActivity.duration = IIf((Me.txtduration.Text = ""), 0, Me.txtduration.Text)
            objSubActivity.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
            objSubActivity.ofcontribution = PublicFunction.ConvertStringToDouble(Me.txtofcontribution.Text)
            objSubActivity.attachment = PublicFunction.LoadFile(Request)
            objSubActivity.criticalpath = Me.cbcriticalpath.Checked
            objSubActivity.requiresapproval = Me.cbrequiresapproval.Checked
            objSubActivity.enabled = Me.ddlenabled.SelectedValue
            objSubActivity.iduser = applicationCredentials.UserID
            objSubActivity.createdate = Now
            ' se busca la fase del proyecto actual
            objSubActivity.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            ' almacenar la entidad
            objSubActivity.id = facade.addSubActivity(applicationCredentials, objSubActivity)

            ' ir al administrador
            Response.Redirect("searchSubActivity.aspx")

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
            objSubActivity = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchSubActivity.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objSubActivity As New SubActivityEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verficación de código
        If Not verifyNumber() Then
            Return
        End If

        'cargar el registrio referenciado
        objSubActivity = facade.loadSubActivity(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos
            objSubActivity.idactivity = Me.ddlidactivity.SelectedValue
            objSubActivity.type = Me.ddltype.SelectedValue
            objSubActivity.number = Me.txtnumber.Text
            objSubActivity.name = Me.txtname.Text
            objSubActivity.description = Me.txtdescription.Text
            objSubActivity.idresponsible = Me.ddlidresponsible.SelectedValue
            objSubActivity.begindate = IIf((Me.txtbegindate.Text = ""), Nothing, Me.txtbegindate.Text)
            objSubActivity.enddate = IIf((Me.txtenddate.Text = ""), Nothing, Me.txtenddate.Text)
            objSubActivity.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            objSubActivity.duration = IIf((Me.txtduration.Text = ""), 0, Me.txtduration.Text)
            objSubActivity.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
            objSubActivity.ofcontribution = PublicFunction.ConvertStringToDouble(Me.txtofcontribution.Text)
            objSubActivity.criticalpath = Me.cbcriticalpath.Checked
            objSubActivity.requiresapproval = Me.cbrequiresapproval.Checked
            objSubActivity.enabled = Me.ddlenabled.SelectedValue
            objSubActivity.iduser = applicationCredentials.UserID
            objSubActivity.createdate = Now
            ' se busca la fase del proyecto actual
            objSubActivity.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)
            'Cargar el archivo
            objSubActivity.attachment = PublicFunction.LoadFile(Request)

            ' si no se modifico el archivo
            If objSubActivity.attachment.Equals(String.Empty) Then

                'cargar el anterior
                objSubActivity.attachment = Me.hlattachment.Text

            End If

            ' modificar el registro
            facade.updateSubActivity(applicationCredentials, objSubActivity)

            ' ir al administrador
            Response.Redirect("searchSubActivity.aspx")

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
            objSubActivity = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteSubActivity(applicationCredentials, Request.QueryString("Id"), ViewState("idKey"))

            ' ir al administrador
            Response.Redirect("searchSubActivity.aspx")

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
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=ddlidproject.SelectedValue, order:="Code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

            If ddlidobjective.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
                Else
                    Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, order:="Code", isLastVersion:="1")
                End If
                Me.ddlidcomponent.DataValueField = "idkey"
                Me.ddlidcomponent.DataTextField = "Code"
                Me.ddlidcomponent.DataBind()
            Else
                ddlidcomponent.Items.Clear()
            End If

            If ddlidcomponent.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, enabled:="1", order:="title", isLastVersion:="1")
                Else
                    Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, order:="title", isLastVersion:="1")
                End If
                Me.ddlidactivity.DataValueField = "idkey"
                Me.ddlidactivity.DataTextField = "Title"
                Me.ddlidactivity.DataBind()
            Else
                ddlidactivity.Items.Clear()
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
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlidcomponent.DataSource = facade.getComponentList(applicationCredentials, idobjective:=ddlidobjective.SelectedValue, order:="Code", isLastVersion:="1")
            End If
            Me.ddlidcomponent.DataValueField = "idkey"
            Me.ddlidcomponent.DataTextField = "Code"
            Me.ddlidcomponent.DataBind()

            If ddlidcomponent.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, enabled:="1", order:="title", isLastVersion:="1")
                Else
                    Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, order:="title", isLastVersion:="1")
                End If
                Me.ddlidactivity.DataValueField = "idkey"
                Me.ddlidactivity.DataTextField = "Title"
                Me.ddlidactivity.DataBind()
            Else
                ddlidactivity.Items.Clear()
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
                Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, enabled:="1", order:="title", isLastVersion:="1")
            Else
                Me.ddlidactivity.DataSource = facade.getActivityList(applicationCredentials, idcomponent:=ddlidcomponent.SelectedValue, order:="title", isLastVersion:="1")
            End If
            Me.ddlidactivity.DataValueField = "idkey"
            Me.ddlidactivity.DataTextField = "Title"
            Me.ddlidactivity.DataBind()


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
        'Verificar número
        verifyNumber()
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

            Me.ddlidresponsible.DataSource = facade.getUserList(applicationCredentials)
            Me.ddlidresponsible.DataValueField = "Id"
            Me.ddlidresponsible.DataTextField = "Code"
            Me.ddlidresponsible.DataBind()

            Dim x As New Object
            Dim y As New System.EventArgs

            ddlidproject_SelectedIndexChanged(x, y)

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

    Private Function verifyNumber() As Boolean
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifySubActivityNumber(applicationCredentials, Me.txtnumber.Text, Me.txtid.Text) Then
                lblHelpnumber.Text = "Este código ya existe, por favor cambielo"
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
        Dim list As List(Of SubActivityEntity)

        Try
            ' cargar la lista de versiones anteriores
            list = facade.getSubActivityList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

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
