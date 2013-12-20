Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addContractExecution
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve el Identificador
    ''' de la ejecución de contrato original
    ''' </summary>
    ''' <value>valor a asignar</value>
    ''' <returns>el identificador requerido</returns>
    ''' <remarks></remarks>
    Private Property IdContractRequestOriginal() As String
        Get
            Return ViewState("idContractRequestOriginal").ToString()
        End Get
        Set(ByVal value As String)
            ViewState("idContractRequestOriginal") = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna o devuelve la cantidad de registros de la lista 
    ''' de pagos para la ejecucion de contrato actual.
    ''' </summary>
    ''' <value>valor a asignar</value>
    ''' <returns>el contador requerido</returns>
    ''' <remarks></remarks>
    Private Property ContadorRegistrosListaPagos() As Integer
        Get
            Return CInt(ViewState("contadorRegistrosListaPagos"))
        End Get
        Set(ByVal value As Integer)
            ViewState("contadorRegistrosListaPagos") = value
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
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar los combos
            loadCombos()

            'Se llama al metodo que permite poblar la grilla de la lista de pagos
            Me.loadGridView()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVO EJECUCIÓN DE CONTRATO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA EJECUCIÓN DE CONTRATO."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.ddlcontractrequest.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objContractExecution As New ContractExecutionEntity

                    Try
                        ' cargar el registro referenciado
                        objContractExecution = facade.loadContractExecution(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.ddlcontractrequest.SelectedValue = objContractExecution.idcontractrequest
                        Me.txtstartdate.Text = objContractExecution.startdate.ToString("yyyy/MM/dd")
                        Me.txtpaymentdate.Text = objContractExecution.paymentdate.ToString("yyyy/MM/dd")
                        Me.txtcontractnumber.Text = objContractExecution.contractnumber
                        Me.txtordernumber.Text = objContractExecution.ordernumber
                        Me.txtclosingcomments.Text = objContractExecution.closingcomments
                        Me.txtclosingdate.Text = objContractExecution.closingdate.ToString("yyyy/MM/dd")
                        Me.txtfinalpaymentdate.Text = objContractExecution.finalpaymentdate.ToString("yyyy/MM/dd")
                        Me.txtvalue.Text = objContractExecution.value.ToString("#,###")
                        Me.txtiduser.Text = objContractExecution.USERNAME
                        Me.txtcreatedate.Text = objContractExecution.createdate.ToString("yyyy/MM/dd HH:mm:ss")

                        'Se almacena la información de la solicitud de contrato original
                        Me.IdContractRequestOriginal = objContractExecution.IDCONTRACTREQUESTOLD

                        ' Se actualiza la informacion de la grilla de la lista de pagos
                        Me.ContadorRegistrosListaPagos = objContractExecution.PAYMENTSLIST.Count
                        Me.gvPaymentsList.DataSource = objContractExecution.PAYMENTSLIST
                        Me.gvPaymentsList.DataBind()

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objContractExecution = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        If Page.IsValid Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objContractExecution As New ContractExecutionEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Try
                'Se veifica si existe una lista de pagos
                If (Me.ContadorRegistrosListaPagos > 0) Then

                    ' cargar los valores registrados por el usuario
                    objContractExecution.idcontractrequest = Me.ddlcontractrequest.SelectedValue
                    objContractExecution.startdate = Me.txtstartdate.Text
                    objContractExecution.paymentdate = Me.txtpaymentdate.Text
                    objContractExecution.contractnumber = Me.txtcontractnumber.Text
                    objContractExecution.ordernumber = Me.txtordernumber.Text
                    objContractExecution.closingcomments = Me.txtclosingcomments.Text
                    objContractExecution.closingdate = Me.txtclosingdate.Text
                    objContractExecution.finalpaymentdate = Me.txtfinalpaymentdate.Text
                    objContractExecution.value = PublicFunction.ConvertStringToDouble(Me.txtvalue.Text)
                    objContractExecution.iduser = applicationCredentials.UserID
                    objContractExecution.createdate = Now

                    'Se llama al metodo que permite recuperar la lista de pagos de la ejecución de contrato actual
                    objContractExecution.PAYMENTSLIST = Me.recoveryPaymentsList()

                    ' almacenar la entidad
                    objContractExecution.idcontractrequest = facade.addContractExecution(applicationCredentials, objContractExecution)

                    ' ir al administrador
                    Response.Redirect("searchContractExecution.aspx")

                Else

                    Me.lbMessage.Text = "La solicitud de contrato actual no contiene una lista de pagos asociada, por favor verifique."

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
                objContractExecution = Nothing
                facade = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchContractExecution.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Page.IsValid Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objContractExecution As New ContractExecutionEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar el registro referenciado
            objContractExecution = facade.loadContractExecution(applicationCredentials, Request.QueryString("id"))

            Try
                'Se veifica si existe una lista de pagos
                If (Me.ContadorRegistrosListaPagos > 0) Then

                    ' cargar los datos
                    objContractExecution.idcontractrequest = Me.ddlcontractrequest.SelectedValue
                    objContractExecution.startdate = Me.txtstartdate.Text
                    objContractExecution.paymentdate = Me.txtpaymentdate.Text
                    objContractExecution.contractnumber = Me.txtcontractnumber.Text
                    objContractExecution.ordernumber = Me.txtordernumber.Text
                    objContractExecution.closingcomments = Me.txtclosingcomments.Text
                    objContractExecution.closingdate = Me.txtclosingdate.Text
                    objContractExecution.finalpaymentdate = Me.txtfinalpaymentdate.Text
                    objContractExecution.value = PublicFunction.ConvertStringToDouble(Me.txtvalue.Text)

                    'Se llama al metodo que permite recuperar la lista de pagos de la ejecución de contrato actual
                    objContractExecution.PAYMENTSLIST = Me.recoveryPaymentsList()

                    ' modificar el registro
                    facade.updateContractExecution(applicationCredentials, objContractExecution)

                    ' ir al administrador
                    Response.Redirect("searchContractExecution.aspx")

                Else

                    Me.lbMessage.Text = "La solicitud de contrato actual no contiene una lista de pagos asociada, por favor verifique."

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
                objContractExecution = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteContractExecution(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchContractExecution.aspx")

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
        Me.btnCancelDelete.Visible = False
        Me.btnConfirmDelete.Visible = False
        Me.lblDelete.Visible = False
        Me.btnCancel.Visible = True

    End Sub

    Protected Sub cvContractrequest_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvContractrequest.ServerValidate
        ' definir los objetos
        Dim facade As Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim hacerValidacion As Boolean = False

        Try

            'Se verifica si la opción actual es la opción adicionar
            If (Request.QueryString("op").Equals("add")) Then

                hacerValidacion = True

            Else

                'Se verifica si el identificador de la solicitud actual,
                'es diferente del identificador inicialmente cargado.
                If (args.Value <> Me.IdContractRequestOriginal) Then

                    hacerValidacion = True

                Else

                    hacerValidacion = False
                    args.IsValid = True

                End If

            End If

            'Se veirifica el identificador de la ejecución de contrato
            If (hacerValidacion) Then

                facade = New Facade()

                If facade.verifyContractExecutionCode(applicationCredentials, args.Value) Then
                    args.IsValid = False
                    Me.ddlcontractrequest.Focus()
                Else
                    args.IsValid = True
                End If

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

    Protected Sub ddlcontractrequest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcontractrequest.SelectedIndexChanged

        'Se llama al metodo que permite poblar la grilla con la
        'lista de pagos de la solicitud de contrato seleccionada
        Me.loadGridView()

    End Sub

    Protected Sub gvPaymentsList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPaymentsList.RowDataBound
        'verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim mitxtDate As TextBox = DirectCast(e.Row.Cells(4).FindControl("txtFinalPaymentDate"), TextBox)
            If Not (CDate(mitxtDate.Text) > CDate("1900/01/01")) Then
                mitxtDate.Text = ""
            End If
        End If
    End Sub

    Protected Sub lbMessage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbMessage.Click

        Response.Redirect("addContractRequest.aspx?op=edit&requestnumber=" & Me.ddlcontractrequest.SelectedValue)

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo que permite recuperar la lista de pagos
    ''' para la ejecución de contrato actual
    ''' </summary>
    ''' <returns>retorna la lista de pagos en caso de existir o Nothing en caso contrario</returns>
    ''' <remarks></remarks>
    Private Function recoveryPaymentsList() As List(Of PaymentsListByContractRequestEntity)

        'Se declaran los objetos
        Dim paymentsList As New List(Of PaymentsListByContractRequestEntity)

        'Se recorre la lista de pagos
        If (Me.ContadorRegistrosListaPagos > 0) Then

            For miContador As Integer = 0 To Me.ContadorRegistrosListaPagos - 1
                Dim objPaymentList As New PaymentsListByContractRequestEntity

                'Se recuperan los valores requeridos
                objPaymentList.id = DirectCast(Me.gvPaymentsList.Rows(miContador).Cells(0).Controls(0), DataBoundLiteralControl).Text.Trim()
                Dim miTextBoxDate As TextBox = DirectCast(Me.gvPaymentsList.Rows(miContador).Cells(4).FindControl("txtFinalPaymentDate"), TextBox)
                If (miTextBoxDate.Text.Length > 0) Then objPaymentList.finalPaymentDate = miTextBoxDate.Text
                Dim miTextBoxValue As TextBox = DirectCast(Me.gvPaymentsList.Rows(miContador).Cells(5).FindControl("txtFinalPaymentValue"), TextBox)
                objPaymentList.finalPaymentValue = PublicFunction.ConvertStringToDouble(miTextBoxValue.Text)
                'Se agrega el objeto a la lista de pagos
                paymentsList.Add(objPaymentList)
            Next

            'Se retorna la lista encontrada
            Return paymentsList

        Else

            Return Nothing

        End If

    End Function

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

            'Se agregan las rutinas para poblar la lista de solicitudes de contrato
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlcontractrequest.DataSource = facade.getContractExecutionContractRequestList(applicationCredentials, enabled:="1")
            Else
                Me.ddlcontractrequest.DataSource = facade.getContractRequestList(applicationCredentials, requestnumber:=Request.QueryString("id"))
            End If
            Me.ddlcontractrequest.DataValueField = "RequestNumber"
            Me.ddlcontractrequest.DataTextField = "RequestNumber"
            Me.ddlcontractrequest.DataBind()

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
    ''' Permite poblar la grilla con los datos requeridos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadGridView()

        ' definir los objetos
        Dim facade As New Facade
        Dim objContractExecution As New ContractExecutionEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se verifica que exista una solicitud de contrato
            If (Me.ddlcontractrequest.SelectedValue.Length > 0) Then

                'Se llama al metodo que permite consultar la lista de pagos de la solicitud de contrato actual
                objContractExecution.PAYMENTSLIST = facade.getPaymentsListByContractRequestList(applicationCredentials, _
                    idcontractrequest:=Me.ddlcontractrequest.SelectedValue)

                'Se almacena en la bolsa de estado la cantidad de registros de la lista de pagos
                'para la ejecución de contrato actual.
                Me.ContadorRegistrosListaPagos = objContractExecution.PAYMENTSLIST.Count

                ' Se actualiza la informacion de la grilla de la lista de pagos
                Me.gvPaymentsList.DataSource = objContractExecution.PAYMENTSLIST
                Me.gvPaymentsList.DataBind()

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
