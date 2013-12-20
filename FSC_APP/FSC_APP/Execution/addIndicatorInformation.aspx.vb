Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addIndicatorInformation
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve el identificador del indicador
    ''' </summary>
    ''' <value>Asigna el identificador del indicador</value>
    ''' <returns>Devuelve el identificador del indicador</returns>
    ''' <remarks></remarks>
    Private Property IdIndicator() As Integer
        Get
            Return DirectCast(ViewState("IdIndicator"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdIndicator") = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna o devuelve el identificador del Usuario
    ''' </summary>
    ''' <value>Asigna el identificador del Usuario</value>
    ''' <returns>Devuelve el identificador del Usuario</returns>
    ''' <remarks></remarks>
    Private Property IdMeasurementDateByIndicator() As Integer
        Get
            Return DirectCast(ViewState("IdMeasurementDateByIndicator"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdMeasurementDateByIndicator") = value
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

            'Se recupera el identificador del indicador y del usuario
            Me.IdMeasurementDateByIndicator = Request.QueryString("IdMeasurementDateByIndicator")
            Me.IdIndicator = Request.QueryString("IdIndicator")
            Dim defaultDate As New DateTime
            ''OJO BORRAR ESTO
            'Me.IdMeasurementDateByIndicator = 10
            'Me.IdIndicator = 19

            'Se llama al metodo que permite cargar la información de la descripción y la meta para el indicador requerido.
            Me.LoadInformationIndicator()

            'Se llama al metodo que permite poblar la información historica del indicador requerido.
            Me.LoadHistoricalRecords()

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA INFORMACIÓN DE INDICADOR."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lbldescription.Enabled = False
                    Me.txtdescription.ReadOnly = True
                    Me.lblgoal.Enabled = False
                    Me.txtgoal.Enabled = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA NUEVA INFORMACIÓN DE INDICADOR."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False


                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objIndicatorInformation As New IndicatorInformationEntity

                    Try
                        ' cargar el registro referenciado
                        objIndicatorInformation = facade.loadIndicatorInformation(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objIndicatorInformation.id
                        Me.txtdescription.Text = objIndicatorInformation.description
                        Me.txtgoal.Text = objIndicatorInformation.goal
                        Me.txtiduser.Text = objIndicatorInformation.iduser
                        Me.txtDateMesuare.Text = IIf((objIndicatorInformation.measuredate = defaultDate), "", objIndicatorInformation.measuredate)
                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objIndicatorInformation = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objIndicatorInformation As New IndicatorInformationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objIndicatorInformation.idmeasurementdatebyindicator = Me.IdMeasurementDateByIndicator
            objIndicatorInformation.idindicator = Me.IdIndicator
            objIndicatorInformation.description = Me.txtdescription.Text
            objIndicatorInformation.goal = Me.txtgoal.Text
            objIndicatorInformation.value = Me.txtValue.Text
            objIndicatorInformation.comments = Me.txtComments.Text
            objIndicatorInformation.registrationdate = Now()
            objIndicatorInformation.iduser = applicationCredentials.UserID
            objIndicatorInformation.measuredate = IIf((Me.txtDateMesuare.Text = ""), Nothing, Me.txtDateMesuare.Text)
            ' almacenar la entidad
            objIndicatorInformation.id = facade.addIndicatorInformation(applicationCredentials, objIndicatorInformation)

            ' ir al administrador
            Response.Redirect("subActivityMainPanelTODO-LIST.aspx")

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
            objIndicatorInformation = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("subActivityMainPanelTODO-LIST.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objIndicatorInformation As New IndicatorInformationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los datos
            objIndicatorInformation.id = CInt(Request.QueryString("id"))
            objIndicatorInformation.id = Me.txtid.Text
            objIndicatorInformation.description = Me.txtdescription.Text
            objIndicatorInformation.goal = Me.txtgoal.Text
            objIndicatorInformation.iduser = Me.txtiduser.Text
            objIndicatorInformation.measuredate = IIf((Me.txtDateMesuare.Text = ""), Nothing, Me.txtDateMesuare.Text)
            ' modificar el registro
            facade.updateIndicatorInformation(applicationCredentials, objIndicatorInformation)

            ' ir al administrador
            Response.Redirect("subActivityMainPanelTODO-LIST.aspx")

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
            objIndicatorInformation = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteIndicatorInformation(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchIndicatorInformation.aspx")

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
    ''' Permite poblar la grilla con la información historica
    ''' del indicador requerido
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadHistoricalRecords()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se pobla la grilla
            Me.gvRecords.DataSource = facade.getIndicatorInformationList(applicationCredentials, idmeasurementdatebyindicator:=Me.IdMeasurementDateByIndicator, idindicator:=Me.IdIndicator)
            Me.gvRecords.DataBind()

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
    ''' Metodo que permite cargar la información de la descripción y la meta para el indicador requerido.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformationIndicator()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim indicatorEntity As IndicatorEntity

        Try

            'Se pobla la grilla
            indicatorEntity = facade.loadIndicator(applicationCredentials, idIndicator:=Me.IdIndicator)
            Me.txtdescription.Text = indicatorEntity.description
            Me.txtgoal.Text = indicatorEntity.goal

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar recursos
            facade = Nothing
            indicatorEntity = Nothing
        End Try

    End Sub

#End Region

    
End Class
