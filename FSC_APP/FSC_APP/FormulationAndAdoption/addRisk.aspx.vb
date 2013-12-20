Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addRisk
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
            Dim objComponent As New ComponentEntity
            Dim objObjective As New ObjectiveEntity
            Dim Item As New ListItem
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            TabContainer1.TabIndex = 0
            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO RIESGO."

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
                    Session("lblTitle") = "EDITAR UN RIESGO."

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
                    Dim objRisk As New RiskEntity

                    Try
                        ' cargar el registro referenciado
                        objRisk = facade.loadRisk(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objRisk.id
                        Me.txtcode.Text = objRisk.code
                        Me.txtname.Text = objRisk.name
                        Me.txtdescription.Text = objRisk.description
                        Me.txtwhatcanhappen.Text = objRisk.whatcanhappen
                        Me.ddlriskimpact.SelectedValue = objRisk.riskimpact
                        Me.ddlocurrenceprobability.SelectedValue = objRisk.ocurrenceprobability
                        Me.ddlenabled.SelectedValue = objRisk.enabled
                        Me.txtiduser.Text = objRisk.USERNAME
                        Me.txtcreatedate.Text = objRisk.createdate

                        'Cargar la lista de componentes
                        If (objRisk.componentlist.Count > 0) Then
                            objComponent = facade.loadComponent(applicationCredentials, objRisk.componentlist.First.idcomponent)
                            objObjective = facade.loadObjective(applicationCredentials, objComponent.idobjective)
                            Me.ddlidproject.SelectedValue = objObjective.idproject
                            Me.ddlidproject_SelectedIndexChanged(sender, e)
                            Me.ddlidproject.Enabled = True
                            Me.ddlidobjective.SelectedValue = objObjective.id
                            Me.ddlidobjective_SelectedIndexChanged(sender, e)
                            For Each objComponentByRisk As ComponentByRiskEntity In objRisk.componentlist
                                Item = Me.dlbComponent.AviableItems.Items.FindByValue(objComponentByRisk.idcomponent)
                                Me.dlbComponent.AviableItems.Items.Remove(Item)
                                'If Not (Item Is Nothing) Then
                                '    Me.dlbComponent.SelectedItems.Items.Add(Item)
                                'End If
                                Me.dlbComponent.SelectedItems.Items.Add(New ListItem(objComponentByRisk.CODE, objComponentByRisk.idcomponent))
                            Next
                        End If

                        ' guardar
                        ViewState("id") = objRisk.id

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' limpiar label
                            'Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            'loadVersions(objRisk.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase del riesgo es la fase de cerrado
                            'Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            'If (objRisk.idphase.ToString() = idClosedState) Then
                            '    'Se oculta el botón grabar y el botón eliminar
                            '    Me.btnSave.Visible = False
                            '    Me.btnDelete.Visible = False
                            'End If

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
                        objRisk = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objRisk As New RiskEntity
        Dim objComponentByRiskList As New List(Of ComponentByRiskEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objRisk.code = Me.txtcode.Text
            objRisk.name = Me.txtname.Text
            objRisk.description = Me.txtdescription.Text
            objRisk.whatcanhappen = Me.txtwhatcanhappen.Text
            objRisk.riskimpact = Me.ddlriskimpact.SelectedValue
            objRisk.ocurrenceprobability = Me.ddlocurrenceprobability.SelectedValue
            objRisk.enabled = Me.ddlenabled.SelectedValue
            objRisk.iduser = applicationCredentials.UserID
            objRisk.createdate = Now
            ' se busca la fase del proyecto actual
            'objRisk.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)

            'Cargar la lista de componentes
            For Each item As ListItem In Me.dlbComponent.SelectedItems.Items
                Dim objComponentByRisk As New ComponentByRiskEntity
                objComponentByRisk.idcomponent = item.Value
                objComponentByRiskList.Add(objComponentByRisk)
            Next

            'Asignar la lista de componentes
            objRisk.componentlist = objComponentByRiskList

            ' almacenar la entidad
            objRisk.id = facade.addRisk(applicationCredentials, objRisk)

            ' ir al administrador
            Response.Redirect("searchRisk.aspx")

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
            objRisk = Nothing
            facade = Nothing
            objComponentByRiskList = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchRisk.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objRisk As New RiskEntity
        Dim objComponentByRiskList As New List(Of ComponentByRiskEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'Cargar el registro referenciado
        objRisk = facade.loadRisk(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objRisk.code = Me.txtcode.Text
            objRisk.name = Me.txtname.Text
            objRisk.description = Me.txtdescription.Text
            objRisk.whatcanhappen = Me.txtwhatcanhappen.Text
            objRisk.riskimpact = Me.ddlriskimpact.SelectedValue
            objRisk.ocurrenceprobability = Me.ddlocurrenceprobability.SelectedValue
            objRisk.enabled = Me.ddlenabled.SelectedValue
            objRisk.iduser = applicationCredentials.UserID
            objRisk.createdate = Now
            ' se busca la fase del proyecto actual
            'objRisk.idphase = facade.ObjectiveVersionProject(applicationCredentials, Me.ddlidproject.SelectedValue)

            'Cargar la lista de componentes
            For Each item As ListItem In Me.dlbComponent.SelectedItems.Items
                Dim objComponentByRisk As New ComponentByRiskEntity
                objComponentByRisk.idcomponent = item.Value
                objComponentByRiskList.Add(objComponentByRisk)
            Next

            'Asignar la lista de componentes
            objRisk.componentlist = objComponentByRiskList

            ' modificar el registro
            facade.updateRisk(applicationCredentials, objRisk)

            ' ir al administrador
            Response.Redirect("searchRisk.aspx")

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
            objRisk = Nothing
            objComponentByRiskList = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteRisk(applicationCredentials, Request.QueryString("Id"), ViewState("id"))

            ' ir al administrador
            Response.Redirect("searchRisk.aspx")

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
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=Me.ddlidproject.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=Me.ddlidproject.SelectedValue, order:="code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

            If Me.ddlidobjective.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
                Else
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, order:="code", isLastVersion:="1")
                End If
                Me.dlbComponent.AviableItems.DataValueField = "idkey"
                Me.dlbComponent.AviableItems.DataTextField = "Code"
                Me.dlbComponent.AviableItems.DataBind()
            Else
                Me.dlbComponent.AviableItems.Items.Clear()
                Me.dlbComponent.SelectedItems.Items.Clear()
            End If
            Dim miItem As ListItem
            For Each item As ListItem In Me.dlbComponent.SelectedItems.Items

                miItem = Me.dlbComponent.AviableItems.Items.FindByValue(item.Value)
                ' cargar los valores seleccionados
                dlbComponent.AviableItems.Items.Remove(miItem)
            Next

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
            If Me.ddlidobjective.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
                Else
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, order:="code", isLastVersion:="1")
                End If
                Me.dlbComponent.AviableItems.DataValueField = "idkey"
                Me.dlbComponent.AviableItems.DataTextField = "Code"
                Me.dlbComponent.AviableItems.DataBind()
            Else
                Me.dlbComponent.AviableItems.Items.Clear()
                Me.dlbComponent.SelectedItems.Items.Clear()
            End If
            Dim miItem As ListItem
            For Each item As ListItem In Me.dlbComponent.SelectedItems.Items

                miItem = Me.dlbComponent.AviableItems.Items.FindByValue(item.Value)
                ' cargar los valores seleccionados
                dlbComponent.AviableItems.Items.Remove(miItem)
            Next
            'Limpiar items
            'Me.dlbComponent.SelectedItems.Items.Clear()

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
        'Verificar Código
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

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=Me.ddlidproject.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.ddlidobjective.DataSource = facade.getObjectiveList(applicationCredentials, idproject:=Me.ddlidproject.SelectedValue, order:="code", isLastVersion:="1")
            End If
            Me.ddlidobjective.DataValueField = "idkey"
            Me.ddlidobjective.DataTextField = "Code"
            Me.ddlidobjective.DataBind()

            If Me.ddlidobjective.Items.Count > 0 Then
                If (Request.QueryString("op").Equals("add")) Then
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, enabled:="1", order:="code", isLastVersion:="1")
                Else
                    Me.dlbComponent.AviableItems.DataSource = facade.getComponentList(applicationCredentials, idobjective:=Me.ddlidobjective.SelectedValue, order:="code", isLastVersion:="1")
                End If
                Me.dlbComponent.AviableItems.DataValueField = "idkey"
                Me.dlbComponent.AviableItems.DataTextField = "Code"
                Me.dlbComponent.AviableItems.DataBind()
            Else
                Me.dlbComponent.AviableItems.Items.Clear()
                Me.dlbComponent.SelectedItems.Items.Clear()
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

    Private Function verifyCode() As Boolean
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifyRiskCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.Text = "Este código ya existe, por favor cámbielo"
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

    'Public Sub loadVersions(ByVal idKey As String)

    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
    '    Dim list As List(Of RiskEntity)

    '    Try
    '        ' cargar la lista de versiones anteriores
    '        list = facade.getRiskList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

    '        Me.gvVersion.DataSource = list
    '        Me.gvVersion.DataBind()

    '        If list.Count > 0 Then

    '            ' mensaje
    '            Me.lblVersion.Text = "Versiones Anteriores Registradas"

    '        Else

    '            ' mensaje
    '            Me.lblVersion.Text = "No Hay Versiones Anteriores Registradas"

    '        End If

    '    Catch ex As Exception

    '        'mostrando el error
    '        Session("serror") = ex.Message
    '        Session("sUrl") = Request.UrlReferrer.PathAndQuery
    '        Response.Redirect("~/errors/error.aspx")
    '        Response.End()

    '    Finally

    '        ' liberar recursos
    '        facade = Nothing

    '    End Try

    'End Sub

#End Region

End Class
