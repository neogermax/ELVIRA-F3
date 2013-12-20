Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addSTRATEGICOBJECTIVE
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

            ' cargar las listas
            loadCombos()
            For i As Integer = 1990 To 2030
                ddlyear.Items.Add(i.ToString())
            Next

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO OBJETIVO ESTRATÉGICO."

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
                    Session("lblTitle") = "EDITAR UN OBJETIVO ESTRATÉGICO."

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
                    Dim objSTRATEGICOBJECTIVE As New STRATEGICOBJECTIVEEntity
                    Dim objPerspective As New PERSPECTIVEEntity
                    Dim facade As New Facade

                    Try
                        ' cargar el registro referenciado
                        objSTRATEGICOBJECTIVE = facade.loadStrategicObjective(applicationCredentials, Request.QueryString("id"))

                        'Carga la perspectiva
                        objPerspective = facade.loadPerspective(applicationCredentials, objSTRATEGICOBJECTIVE.idperspective)


                        ' mostrar los valores
                        Me.txtid.Text = objSTRATEGICOBJECTIVE.id
                        Me.txtcode.Text = objSTRATEGICOBJECTIVE.code
                        Me.txtname.Text = objSTRATEGICOBJECTIVE.name
                        Me.ddlyear.SelectedValue = objSTRATEGICOBJECTIVE.year
                        Me.ddlidenterprise.SelectedValue = objPerspective.identerprise
                        ddlidenterprise_SelectedIndexChanged(sender, e)
                        Me.ddlidperspective.SelectedValue = objSTRATEGICOBJECTIVE.idperspective
                        Me.ddlenabled.SelectedValue = objSTRATEGICOBJECTIVE.enabled
                        Me.txtiduser.Text = objSTRATEGICOBJECTIVE.USERNAME
                        Me.txtcreatedate.Text = objSTRATEGICOBJECTIVE.createdate

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        objSTRATEGICOBJECTIVE = Nothing
                        facade = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim objSTRATEGICOBJECTIVE As New STRATEGICOBJECTIVEEntity
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objSTRATEGICOBJECTIVE.code = Me.txtcode.Text
            objSTRATEGICOBJECTIVE.name = Me.txtname.Text
            objSTRATEGICOBJECTIVE.year = Me.ddlyear.SelectedValue
            objSTRATEGICOBJECTIVE.idperspective = Me.ddlidperspective.SelectedValue
            objSTRATEGICOBJECTIVE.enabled = Me.ddlenabled.SelectedValue
            objSTRATEGICOBJECTIVE.iduser = applicationCredentials.UserID
            objSTRATEGICOBJECTIVE.createdate = Now

            ' almacenar la entidad
            objSTRATEGICOBJECTIVE.id = facade.addStrategicObjective(applicationCredentials, objSTRATEGICOBJECTIVE)

            ' ir al administrador
            Response.Redirect("searchSTRATEGICOBJECTIVE.aspx")

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
            objSTRATEGICOBJECTIVE = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchSTRATEGICOBJECTIVE.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim objSTRATEGICOBJECTIVE As New STRATEGICOBJECTIVEEntity
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'cargar el registro referenciado
        objSTRATEGICOBJECTIVE = facade.loadStrategicObjective(applicationCredentials, Request.QueryString("id"))

        Try
            ' cargar los datos
            objSTRATEGICOBJECTIVE.code = Me.txtcode.Text
            objSTRATEGICOBJECTIVE.name = Me.txtname.Text
            objSTRATEGICOBJECTIVE.year = Me.ddlyear.SelectedValue
            objSTRATEGICOBJECTIVE.idperspective = Me.ddlidperspective.SelectedValue
            objSTRATEGICOBJECTIVE.enabled = Me.ddlenabled.SelectedValue

            ' modificar el registro
            facade.updateStrategicObjective(applicationCredentials, objSTRATEGICOBJECTIVE)

            ' ir al administrador
            Response.Redirect("searchSTRATEGICOBJECTIVE.aspx")

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
            objSTRATEGICOBJECTIVE = Nothing
            facade = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteStrategicObjective(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchSTRATEGICOBJECTIVE.aspx")

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
        'Verificar el código
        verifyCode()
    End Sub

    Protected Sub ddlidenterprise_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidenterprise.SelectedIndexChanged

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=Me.ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=Me.ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidperspective.DataValueField = "Id"
            Me.ddlidperspective.DataTextField = "Code"
            Me.ddlidperspective.DataBind()

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
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=Me.ddlidenterprise.SelectedValue, enabled:="1", order:="code")
            Else
                Me.ddlidperspective.DataSource = facade.getPerspectiveList(applicationCredentials, identerprise:=Me.ddlidenterprise.SelectedValue, order:="code")
            End If
            Me.ddlidperspective.DataValueField = "Id"
            Me.ddlidperspective.DataTextField = "Code"
            Me.ddlidperspective.DataBind()

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

            If facade.verifyStrategicObjectiveCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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
