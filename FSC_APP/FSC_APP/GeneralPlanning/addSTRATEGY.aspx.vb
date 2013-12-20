Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addSTRATEGY
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
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            'Cargar los combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA ESTRATÉGIA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.rfvid.Enabled = False
                    Me.rfviduser.Enabled = False
                    Me.rfvcreatedate.Enabled = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA ESTRATÉGIA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    
                    ' definir los objetos
                    Dim objSTRATEGY As New STRATEGYEntity
                    Dim objPerspective As New PERSPECTIVEEntity
                    Dim objStrategicObjective As New STRATEGICOBJECTIVEEntity
                    Dim facade As New Facade

                    Try
                        ' cargar el registro referenciado
                        objSTRATEGY = facade.loadStrategy(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objSTRATEGY.id
                        Me.txtcode.Text = objSTRATEGY.code
                        Me.txtname.Text = objSTRATEGY.name
                        Me.ddlenabled.SelectedValue = objSTRATEGY.enabled
                        Me.txtiduser.Text = objSTRATEGY.USERNAME
                        Me.txtcreatedate.Text = objSTRATEGY.createdate

                        'Cargando los combos en orden
                        objStrategicObjective = facade.loadStrategicObjective(applicationCredentials, objSTRATEGY.idstrategicobjective)
                        objPerspective = facade.loadPerspective(applicationCredentials, objStrategicObjective.idperspective)
                        ddlidenterprise.SelectedValue = objPerspective.identerprise
                        ddlidenterprise_SelectedIndexChanged(sender, e)
                        ddlidmanagment.SelectedValue = objSTRATEGY.idmanagment
                        ddlidperspective.SelectedValue = objPerspective.id
                        ddlidperspective_SelectedIndexChanged(sender, e)
                        ddlidstrategicobjective.SelectedValue = objSTRATEGY.idstrategicobjective

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        objSTRATEGY = Nothing
                        facade = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim objSTRATEGY As New STRATEGYEntity
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objSTRATEGY.code = Me.txtcode.Text
            objSTRATEGY.name = Me.txtname.Text
            objSTRATEGY.idstrategicobjective = Me.ddlidstrategicobjective.SelectedValue
            objSTRATEGY.idmanagment = Me.ddlidmanagment.SelectedValue
            objSTRATEGY.enabled = Me.ddlenabled.SelectedValue
            objSTRATEGY.iduser = applicationCredentials.UserID
            objSTRATEGY.createdate = Now

            ' almacenar la entidad
            objSTRATEGY.id = facade.addStrategy(applicationCredentials, objSTRATEGY)

            ' ir al administrador
            Response.Redirect("searchSTRATEGY.aspx")

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
            objSTRATEGY = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchSTRATEGY.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim objSTRATEGY As New STRATEGYEntity
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'cargar el registro referenciado
        objSTRATEGY = facade.loadStrategy(applicationCredentials, Request.QueryString("id"))

        Try
            ' cargar los datos
            objSTRATEGY.id = Me.txtid.Text
            objSTRATEGY.code = Me.txtcode.Text
            objSTRATEGY.name = Me.txtname.Text
            objSTRATEGY.idstrategicobjective = Me.ddlidstrategicobjective.SelectedValue
            objSTRATEGY.idmanagment = Me.ddlidmanagment.SelectedValue
            objSTRATEGY.enabled = Me.ddlenabled.SelectedValue

            ' modificar el registro
            facade.updateStrategy(applicationCredentials, objSTRATEGY)

            ' ir al administrador
            Response.Redirect("searchSTRATEGY.aspx")

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
            objSTRATEGY = Nothing
            facade = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteStrategy(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchSTRATEGY.aspx")

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

    Protected Sub ddlidenterprise_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidenterprise.SelectedIndexChanged

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidperspective.DataValueField = "Id"
            Me.ddlidperspective.DataTextField = "Code"
            Me.ddlidperspective.DataBind()

            If (ddlidperspective.Items.Count > 0) Then

                ' cargar la lista de los tipos
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, enabled:="1", order:="code")
                Else
                    Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, order:="code")
                End If
                Me.ddlidstrategicobjective.DataValueField = "Id"
                Me.ddlidstrategicobjective.DataTextField = "Code"
                Me.ddlidstrategicobjective.DataBind()

            Else
                ddlidstrategicobjective.Items.Clear()

            End If

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidmanagment.DataSource = facade.getManagementList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidmanagment.DataSource = facade.getManagementList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidmanagment.DataValueField = "Id"
            Me.ddlidmanagment.DataTextField = "Code"
            Me.ddlidmanagment.DataBind()

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

    Protected Sub ddlidperspective_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidperspective.SelectedIndexChanged

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, order:="code")
            End If
            Me.ddlidstrategicobjective.DataValueField = "Id"
            Me.ddlidstrategicobjective.DataTextField = "Code"
            Me.ddlidstrategicobjective.DataBind()

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

#Region "Metodos"

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidenterprise.DataSource = facade.getEnterpriseList(applicationCredentials, enabled:="1", order:="code")
            Else
                Me.ddlidenterprise.DataSource = facade.getEnterpriseList(applicationCredentials, order:="code")
            End If
            Me.ddlidenterprise.DataValueField = "Id"
            Me.ddlidenterprise.DataTextField = "Code"
            Me.ddlidenterprise.DataBind()

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidperspective.DataValueField = "Id"
            Me.ddlidperspective.DataTextField = "Code"
            Me.ddlidperspective.DataBind()

            If (ddlidperspective.Items.Count > 0) Then

                ' cargar la lista de los tipos
                If (Request.QueryString("op").Equals("add")) Then
                    Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, enabled:="1", order:="code")
                Else
                    Me.ddlidstrategicobjective.DataSource = facade.getStrategicObjectiveList(applicationCredentials, idperspective:=ddlidperspective.SelectedValue, order:="code")
                End If
                Me.ddlidstrategicobjective.DataValueField = "Id"
                Me.ddlidstrategicobjective.DataTextField = "Code"
                Me.ddlidstrategicobjective.DataBind()

            Else
                ddlidstrategicobjective.Items.Clear()

            End If

            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidmanagment.DataSource = facade.getManagementList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidmanagment.DataSource = facade.getManagementList(applicationCredentials, identerprise:=ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidmanagment.DataValueField = "Id"
            Me.ddlidmanagment.DataTextField = "Code"
            Me.ddlidmanagment.DataBind()

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

            If facade.verifyStrategyCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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

#End Region
    
End Class
