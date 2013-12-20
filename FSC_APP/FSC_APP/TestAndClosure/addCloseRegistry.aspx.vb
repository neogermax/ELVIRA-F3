Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addCloseRegistry
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
            Dim defaultDate As New DateTime


            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "Crear Registro de Cierres."

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
                    Me.rfvid.Visible = False
                    Me.rfviduser.Visible = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "Editar Registro de Cierres."

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
                    
                    ' definir los objetos
                    Dim facade As New Facade
					Dim objCloseRegistry As New CloseRegistryEntity

                    Try
                        ' cargar el registro referenciado
                        objCloseRegistry = facade.loadCloseRegistry(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objCloseRegistry.id
                        Me.ddlidproject.SelectedValue = objCloseRegistry.idproject
                        ddlidproject_SelectedIndexChanged(sender, e)
                        If objCloseRegistry.closingdate <> defaultDate Then
                            Me.txtclosingdate.Text = objCloseRegistry.closingdate
                        End If
                        Me.txtweakness.Text = objCloseRegistry.weakness
                        Me.txtopportunity.Text = objCloseRegistry.opportunity
                        Me.txtstrengths.Text = objCloseRegistry.strengths
                        Me.txtlearningfornewprojects.Text = objCloseRegistry.learningfornewprojects
                        Me.ddlgoodpractice.SelectedValue = objCloseRegistry.goodpractice
                        If objCloseRegistry.registrationdate <> defaultDate Then
                            Me.txtregistrationdate.Text = objCloseRegistry.registrationdate
                        End If
                        Me.ddlenabled.SelectedValue = objCloseRegistry.enabled
                        Me.txtiduser.Text = objCloseRegistry.USERNAME

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objCloseRegistry = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objCloseRegistry As New CloseRegistryEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objCloseRegistry.idproject = Me.ddlidproject.SelectedValue
            If Me.txtclosingdate.Text <> "" Then
                objCloseRegistry.closingdate = Me.txtclosingdate.Text
            End If
            objCloseRegistry.weakness = Me.txtweakness.Text
            objCloseRegistry.opportunity = Me.txtopportunity.Text
            objCloseRegistry.strengths = Me.txtstrengths.Text
            objCloseRegistry.learningfornewprojects = Me.txtlearningfornewprojects.Text
            objCloseRegistry.goodpractice = Me.ddlgoodpractice.SelectedValue
            If Me.txtregistrationdate.Text <> "" Then
                objCloseRegistry.registrationdate = Me.txtregistrationdate.Text
            End If
            objCloseRegistry.enabled = Me.ddlenabled.SelectedValue
            objCloseRegistry.iduser = applicationCredentials.UserID

            ' almacenar la entidad
            objCloseRegistry.id = facade.addCloseRegistry(applicationCredentials, objCloseRegistry)

            ' ir al administrador
            Response.Redirect("searchCloseRegistry.aspx")

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
            objCloseRegistry = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchCloseRegistry.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objCloseRegistry As New CloseRegistryEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Cargar el registro referenciado
        objCloseRegistry = facade.loadCloseRegistry(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos            
            objCloseRegistry.idproject = Me.ddlidproject.SelectedValue
            If Me.txtclosingdate.Text <> "" Then
                objCloseRegistry.closingdate = Me.txtclosingdate.Text
            Else
                objCloseRegistry.closingdate = Nothing
            End If
            objCloseRegistry.weakness = Me.txtweakness.Text
            objCloseRegistry.opportunity = Me.txtopportunity.Text
            objCloseRegistry.strengths = Me.txtstrengths.Text
            objCloseRegistry.learningfornewprojects = Me.txtlearningfornewprojects.Text
            objCloseRegistry.goodpractice = Me.ddlgoodpractice.SelectedValue
            If Me.txtregistrationdate.Text <> "" Then
                objCloseRegistry.registrationdate = Me.txtregistrationdate.Text
            Else
                objCloseRegistry.registrationdate = Nothing
            End If
            objCloseRegistry.enabled = Me.ddlenabled.SelectedValue

            ' modificar el registro
           facade.updateCloseRegistry(applicationCredentials, objCloseRegistry)

            ' ir al administrador
            Response.Redirect("searchCloseRegistry.aspx")

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
            objCloseRegistry = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteCloseRegistry(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchCloseRegistry.aspx")

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
        Dim objProject As New ProjectEntity

        Try
            objProject = facade.loadProject(applicationCredentials, ddlidproject.SelectedValue)
            Me.txtprojectObjective.Text = objProject.objective

            gvthirdbyproject.DataSource = objProject.operatorbyprojectlist
            gvthirdbyproject.DataBind()

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

            Dim s As New Object
            Dim e As New EventArgs
            ddlidproject_SelectedIndexChanged(s, e)

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
