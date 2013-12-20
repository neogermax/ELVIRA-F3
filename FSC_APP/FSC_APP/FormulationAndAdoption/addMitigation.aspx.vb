Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addMitigation
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
            Dim Item As New ListItem
            'Cargar los combos
            loadCombos()
            TabContainer1.TabIndex = 0

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR NUEVA MITIGACIÓN."

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
                    Session("lblTitle") = "EDITAR UNA MITIGACIÓN."

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
                    Dim objMitigation As New MitigationEntity

                    Try
                        ' cargar el registro referenciado
                        objMitigation = facade.loadMitigation(applicationCredentials, Request.QueryString("id"), consultLastVersion)

                        ' mostrar los valores
                        Me.txtid.Text = objMitigation.id
                        Me.txtcode.Text = objMitigation.code
                        'Me.ddlidrisk.SelectedValue = objMitigation.idrisk
                        Me.txtname.Text = objMitigation.name
                        Me.ddlimpactonrisk.SelectedValue = objMitigation.impactonrisk
                        Me.ddlidresponsible.SelectedValue = objMitigation.idresponsible
                        Me.ddlenabled.SelectedValue = objMitigation.enabled
                        Me.txtiduser.Text = objMitigation.USERNAME
                        Me.txtcreatedate.Text = objMitigation.createdate


                        'Cargar la lista de componentes
                        If (objMitigation.risklist.Count > 0) Then
                            For Each objMitigationByRisk As MitigationByRiskEntity In objMitigation.risklist
                                Item = Me.dlbRisk.AviableItems.Items.FindByValue(objMitigationByRisk.idrisk)
                                Me.dlbRisk.AviableItems.Items.Remove(Item)
                                'If Not (Item Is Nothing) Then
                                '    Me.dlbComponent.SelectedItems.Items.Add(Item)
                                'End If
                                Me.dlbRisk.SelectedItems.Items.Add(New ListItem(objMitigationByRisk.CODE, objMitigationByRisk.idrisk))
                            Next
                        End If



                        ' guardar
                        ViewState("id") = objMitigation.id

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                            ' limpiar label
                            'Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            'loadVersions(objMitigation.idKey)

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase de la mitigación es la fase de cerrado
                            'Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            'If (objMitigation.idphase.ToString() = idClosedState) Then
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
                        objMitigation = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objMitigation As New MitigationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objMitigationByRiskList As New List(Of MitigationByRiskEntity)
        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objMitigation.code = Me.txtcode.Text
            'objMitigation.idrisk = Me.ddlidrisk.SelectedValue
            objMitigation.name = Me.txtname.Text
            objMitigation.description = ""
            objMitigation.impactonrisk = Me.ddlimpactonrisk.SelectedValue
            objMitigation.idresponsible = Me.ddlidresponsible.SelectedValue
            objMitigation.enabled = Me.ddlenabled.SelectedValue
            objMitigation.iduser = applicationCredentials.UserID
            objMitigation.createdate = Now
            ' consulta la fase del proyecto
            'objMitigation.idphase = facade.MitigationPhaseProject(applicationCredentials, Me.ddlidrisk.SelectedValue)
            ' objMitigation.idphase = 1


            'Cargar la lista de componentes
            For Each item As ListItem In Me.dlbRisk.SelectedItems.Items
                Dim objMitigationByRisk As New MitigationByRiskEntity
                objMitigationByRisk.idrisk = item.Value
                objMitigationByRiskList.Add(objMitigationByRisk)
            Next

            objMitigation.risklist = objMitigationByRiskList

            ' almacenar la entidad
            objMitigation.id = facade.addMitigation(applicationCredentials, objMitigation)

            ' ir al administrador
            Response.Redirect("searchMitigation.aspx")

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
            objMitigation = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchMitigation.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objMitigation As New MitigationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objMitigationByRiskList As New List(Of MitigationByRiskEntity)
        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'Cargar el registro referenciado
        objMitigation = facade.loadMitigation(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objMitigation.code = Me.txtcode.Text
            'objMitigation.idrisk = Me.ddlidrisk.SelectedValue
            objMitigation.name = Me.txtname.Text
            objMitigation.description = ""
            objMitigation.impactonrisk = Me.ddlimpactonrisk.SelectedValue
            objMitigation.idresponsible = Me.ddlidresponsible.SelectedValue
            objMitigation.enabled = Me.ddlenabled.SelectedValue
            objMitigation.iduser = applicationCredentials.UserID
            objMitigation.createdate = Now
            ' consulta la fase del proyecto
            'objMitigation.idphase = facade.MitigationPhaseProject(applicationCredentials, Me.ddlidrisk.SelectedValue)
            ' objMitigation.idphase = 1


            'Cargar la lista de componentes
            For Each item As ListItem In Me.dlbRisk.SelectedItems.Items
                Dim objMitigationByRisk As New MitigationByRiskEntity
                objMitigationByRisk.idrisk = item.Value
                objMitigationByRiskList.Add(objMitigationByRisk)
            Next

            objMitigation.risklist = objMitigationByRiskList

            ' modificar el registro
            facade.updateMitigation(applicationCredentials, objMitigation)

            ' ir al administrador
            Response.Redirect("searchMitigation.aspx")

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
            objMitigation = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteMitigation(applicationCredentials, Request.QueryString("Id"), ViewState("id"))

            ' ir al administrador
            Response.Redirect("searchMitigation.aspx")

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

        Try
            ' cargar la lista de los tipos
            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlidrisk.DataSource = facade.getRiskList(applicationCredentials, enabled:="1", order:="code", isLastVersion:="1")
            'Else
            '    Me.ddlidrisk.DataSource = facade.getRiskList(applicationCredentials, order:="code", isLastVersion:="1")
            'End If
            'Me.ddlidrisk.DataValueField = "idkey"
            'Me.ddlidrisk.DataTextField = "Code"
            'Me.ddlidrisk.DataBind()
            If (Request.QueryString("op").Equals("add")) Then
                Me.dlbRisk.AviableItems.DataSource = facade.getRiskList(applicationCredentials, enabled:="1", order:="code", isLastVersion:="1")
            Else
                Me.dlbRisk.AviableItems.DataSource = facade.getRiskList(applicationCredentials, order:="code", isLastVersion:="1")
            End If
            Me.dlbRisk.AviableItems.DataValueField = "id"
            Me.dlbRisk.AviableItems.DataTextField = "Code"
            Me.dlbRisk.AviableItems.DataBind()

            Dim miItem As ListItem
            For Each item As ListItem In Me.dlbRisk.SelectedItems.Items

                miItem = Me.dlbRisk.AviableItems.Items.FindByValue(item.Value)
                ' cargar los valores seleccionados
                dlbRisk.AviableItems.Items.Remove(miItem)
            Next

            Me.ddlidresponsible.DataSource = facade.getUserList(applicationCredentials)
            Me.ddlidresponsible.DataValueField = "Id"
            Me.ddlidresponsible.DataTextField = "Code"
            Me.ddlidresponsible.DataBind()

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

            If facade.verifyMitigationCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.Text = "Este número ya existe, por favor cámbielo"
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
    '    Dim list As List(Of MitigationEntity)

    '    Try
    '        ' cargar la lista de versiones anteriores
    '        list = facade.getMitigationList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

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
