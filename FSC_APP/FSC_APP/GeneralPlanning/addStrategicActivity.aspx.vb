Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addStrategicActivity
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

            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR NUEVA ACTIVIDAD DE LA ESTRATÉGIA."

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
                    Me.rfvid.Enabled = False
                    Me.rfviduser.Enabled = False
                    Me.rfvcreatedate.Enabled = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR ACTIVIDAD DE LA ESTRATÉGIA."

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
					Dim objStrategicActivity As New StrategicActivityEntity

                    Try
                        ' cargar el registro referenciado
                        objStrategicActivity = facade.loadStrategicActivity(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objStrategicActivity.id
                        Me.txtcode.Text = objStrategicActivity.code
                        Me.txtname.Text = objStrategicActivity.name
                        Me.txtdescription.Text = objStrategicActivity.description
                        Me.ddlidstrategy.SelectedValue = objStrategicActivity.idstrategy
                        Me.txtbegindate.Text = objStrategicActivity.begindate.ToString("yyyy/MM/dd")
                        Me.txtenddate.Text = objStrategicActivity.enddate.ToString("yyyy/MM/dd")
                        Me.txtestimatedvalue.Text = objStrategicActivity.estimatedvalue
                        Me.ddlidresponsible.SelectedValue = objStrategicActivity.idresponsible
                        Me.ddlenabled.SelectedValue = objStrategicActivity.enabled
                        Me.txtiduser.Text = objStrategicActivity.USERNAME
                        Me.txtcreatedate.Text = objStrategicActivity.createdate

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objStrategicActivity = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objStrategicActivity As New StrategicActivityEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        Try
            ' cargar los valores registrados por el usuario
            objStrategicActivity.code = Me.txtcode.Text
            objStrategicActivity.name = Me.txtname.Text
            objStrategicActivity.description = Me.txtdescription.Text
            objStrategicActivity.idstrategy = Me.ddlidstrategy.SelectedValue
            objStrategicActivity.begindate = Me.txtbegindate.Text
            objStrategicActivity.enddate = Me.txtenddate.Text
            objStrategicActivity.estimatedvalue = Me.txtestimatedvalue.Text
            objStrategicActivity.idresponsible = Me.ddlidresponsible.SelectedValue
            objStrategicActivity.enabled = Me.ddlenabled.SelectedValue
            objStrategicActivity.iduser = applicationCredentials.UserID
            objStrategicActivity.createdate = Now

            ' almacenar la entidad
            objStrategicActivity.id = facade.addStrategicActivity(applicationCredentials, objStrategicActivity)

            ' ir al administrador
            Response.Redirect("searchStrategicActivity.aspx")

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
            objStrategicActivity = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchStrategicActivity.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objStrategicActivity As New StrategicActivityEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        'Cargar el objeto referenciado
        objStrategicActivity = facade.loadStrategicActivity(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos
            objStrategicActivity.code = Me.txtcode.Text
            objStrategicActivity.name = Me.txtname.Text
            objStrategicActivity.description = Me.txtdescription.Text
            objStrategicActivity.idstrategy = Me.ddlidstrategy.SelectedValue
            objStrategicActivity.begindate = Me.txtbegindate.Text
            objStrategicActivity.enddate = Me.txtenddate.Text
            objStrategicActivity.estimatedvalue = Me.txtestimatedvalue.Text
            objStrategicActivity.idresponsible = Me.ddlidresponsible.SelectedValue
            objStrategicActivity.enabled = Me.ddlenabled.SelectedValue

            ' modificar el registro
            facade.updateStrategicActivity(applicationCredentials, objStrategicActivity)

            ' ir al administrador
            Response.Redirect("searchStrategicActivity.aspx")

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
            objStrategicActivity = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteStrategicActivity(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchStrategicActivity.aspx")

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
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidstrategy.DataSource = facade.getStrategyList(applicationCredentials, enabled:="1", order:="code")
            Else
                Me.ddlidstrategy.DataSource = facade.getStrategyList(applicationCredentials, order:="code")
            End If
            Me.ddlidstrategy.DataValueField = "Id"
            Me.ddlidstrategy.DataTextField = "Code"
            Me.ddlidstrategy.DataBind()

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

            If facade.verifyStrategicActivityCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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
