Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization
Imports System.Data
Imports PostMail
Imports FSC_APP.PostMail

Partial Class addContractRequest
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

            ' cargar los combos
            loadCombos()

            'preseleccionar gerencia y ocultar controles que no se utilizan
            Me.ddlManagement.SelectedIndex = Me.ddlManagement.Items.IndexOf(ddlManagement.Items.FindByText("GE1"))
            Me.ddlManagement.Visible = False
            Me.lblidmanagement.Visible = False
            Me.ddlEnabled.Visible = False
            Me.lblenabled.Visible = False
            Me.ddlSupervisor.Items.Insert(0, New ListItem("Seleccione...", "-1"))
            Me.ddlSupervisor.SelectedValue = "-1"

            'DropDownList_Listado.SelectedValue = DropDownList.Items.FindByValue(ValorSeleccionado).Value
            'boton exportar
            Me.btnExport.Visible = False

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "GENERAR CONTRATO"

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblrequestnumber.Visible = False
                    Me.txtrequestnumber.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    Me.btnCancel.Visible = False
                    Me.lblSuccess.Visible = False
                    Me.btnFinishContract.Visible = False
                    'Me.Label6.Visible = False
                    'Me.txtContractorName.Visible = False

                    'Se crea la lista de los contratistas personas juridicas
                    Session("contractorLegalEntityByContractRequestList") = New List(Of ContractorLegalEntityByContractRequestEntity)

                    'Se crea la lista de los contratistas personas naturales
                    Session("contractorNaturalPersonByContractRequestList") = New List(Of ContractorNaturalPersonByContractRequestEntity)

                    'Se crea la lista de los pagos de la solicitud de contrato actual
                    Session("paymentsListByContractRequestList") = New List(Of PaymentsListByContractRequestEntity)

                    'Se crea la variable de session que almacena la lista de conceptos de la poliza
                    Session("PolizaDetailsList") = New List(Of PolizaDetailsEntity)

                    'Se crea la variable de session que almacena la lista de polizas
                    Session("PolizaList") = New List(Of PolizaEntity)

                    'ocultar campo de solo lectura numero de proyecto
                    Me.txtProject.Visible = False

                    'Generar elemento por defecto de los DropDownlist
                    'Me.ddlActor.Items.Insert(0, New ListItem("Seleccione...", "-1"))
                    'Me.ddlActor.SelectedValue = "-1"
                    Me.ddlProject.Items.Insert(0, New ListItem("Seleccione...", "-1"))
                    Me.ddlProject.SelectedValue = "-1"
                    Me.ddlContractNature.Items.Insert(0, New ListItem("Seleccione...", "-1"))
                    Me.ddlContractNature.SelectedValue = "-1"
                    Me.ddlEnabled.Items.Insert(0, New ListItem("Seleccione...", "-1"))
                    Me.ddlEnabled.SelectedValue = "-1"
                    Me.ddlConfidential.Items.Insert(0, New ListItem("Seleccione...", "-1"))
                    Me.ddlConfidential.SelectedValue = "-1"
                    

                Case "edit", "show"

                    If op = "show" Then
                        Me.ddlSupervisor.Visible = False
                        Me.btnAddSupervisor.Visible = False
                    End If

                    If Request.QueryString("successSave") <> Nothing Then
                        Me.lblsaveinformation.Text = "Contrato guardado parcialmente!" & Chr(13) & "Para culminar el proceso, puede hacer click en el botón " & Chr(34) & "Finalizar proceso de contratación" & Chr(34) & "."
                        ' Me.lblsaveinformation.ForeColor = Drawing.Color.Green
                        Me.containerSuccess.Visible = True
                        'Me.TabContainer1.ActiveTabIndex = 1
                    End If

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblrequestnumber.Enabled = False
                    Me.txtrequestnumber.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False

                    'bloquear el dropdown de proyecto
                    Me.ddlProject.Visible = False
                    Me.ddlProject.Enabled = False
                    'Mostrar campo de solo lectura numero de proyecto
                    Me.txtProject.Visible = True
                    Me.txtProject.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objContractRequest As New ContractRequestEntity
                    Dim objPoliza As New PolizaEntity
                    Dim objPolizaDetails As List(Of PolizaDetailsEntity) = New List(Of PolizaDetailsEntity)()
                    Dim objproject As New ProjectEntity


                    Try
                        ' cargar el registro referenciado
                        objContractRequest = facade.loadContractRequest(applicationCredentials, Request.QueryString("ID"))

                        ' mostrar los valores
                        Me.txtrequestnumber.Text = objContractRequest.requestnumber
                        'Se verfica si el valor seleccionado por defecto es igual al valor almacenado
                        If Not (Me.ddlManagement.SelectedValue = objContractRequest.idmanagement.ToString()) Then
                            Me.ddlManagement.SelectedValue = objContractRequest.idmanagement
                            'Se llama al metodo que permite poblar el combo de proyectos
                            Me.LoadDropDownListProjectByManagement()
                        End If
                        Me.ddlProject.SelectedValue = objContractRequest.idproject
                        Me.ddlContractNature.SelectedValue = objContractRequest.idcontractnature
                        Me.txtcontractnumberadjusted.Text = objContractRequest.contractnumberadjusted
                        Me.ddlEnabled.SelectedValue = objContractRequest.enabled

                        'Obtener el grupo del usuario para validar
                        Dim grupouser As String
                        grupouser = Session("mMenu")
                        grupouser = Replace(grupouser, "_", "")

                        'validar si el contrato esta finalizado
                        If objContractRequest.enabled = True Or grupouser = 36 Then
                            ' cargar el titulo
                            Session("lblTitle") = "CONSULTAR SOLICITUD DE CONTRATO"
                            Me.btnCancel.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnSave.Visible = False
                            Me.btnFinishContract.Visible = False
                            Me.addConcept.Visible = False
                            Me.btnCancel.Enabled = False
                            Me.btnDelete.Enabled = False
                            Me.btnSave.Enabled = False
                            Me.btnFinishContract.Enabled = False
                            Me.addConcept.Enabled = False
                            gvPolizaConcept.Columns(0).Visible = False
                        End If

                        Me.txtiduser.Text = objContractRequest.USERNAME
                        Me.txtcreatedate.Text = objContractRequest.createdate.ToString("yyyy/MM/dd hh:mm:ss tt")
                        'Me.txtcreatedate.Text = objContractRequest.createdate

                        objproject = facade.loadProject(applicationCredentials, objContractRequest.idproject)

                        'Se cargan los campos nuevos del formulario
                        Me.txtProject.Text = objContractRequest.idproject & "_" & objContractRequest.PROJECTNAME

                        'Verificar si el contrato es otro si
                        If objproject.Typeapproval = 2 Then
                            Me.txtProject.Text = Me.txtProject.Text & " (Otro si)"
                        End If

                        Me.lblProjectNumber.Text = objContractRequest.idproject & "_" & objContractRequest.PROJECTNAME
                        Me.HFProject.Value = objContractRequest.idproject
                        If (objContractRequest.suscriptdate <> "12:00:00 AM") Then Me.txtSubscriptionDate.Text = objContractRequest.suscriptdate.ToString("yyyy/MM/dd")
                        If (objContractRequest.startdate <> "12:00:00 AM") Then Me.txtInitialDate.Text = objContractRequest.startdate.ToString("yyyy/MM/dd")
                        If (objContractRequest.LiquidationDate <> "12:00:00 AM") Then Me.txtLiquidationDate.Text = objContractRequest.LiquidationDate.ToString("yyyy/MM/dd")
                        Me.txtContractDuration.Text = objContractRequest.monthduration
                        'Me.txtSupervisor.Text = objContractRequest.supervisor
                        Me.ddlConfidential.SelectedValue = objContractRequest.confidential
                        Me.chkSignedContract.Checked = objContractRequest.signedcontract
                        Me.txtObs.Text = objContractRequest.notes

                        'cargar los datos de terceros
                        Session("contractorLegalEntityByContractRequestList") = objContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST
                        ' Se actualiza la informacion de la grilla
                        'Me.gvContractorLegalEntity.DataSource = objContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST
                        'Me.gvContractorLegalEntity.DataBind()

                        'cargar los datos de poliza
                        objPoliza = facade.loadPoliza(applicationCredentials, objContractRequest.polizaid)

                        If (objPoliza.fecha_exp <> "12:00:00 AM") Then Me.txtExpeditionDate.Text = objPoliza.fecha_exp
                        'Me.txtFinishdate.Text = objPoliza.fecha_ven
                        Me.txtPolizaNumber.Text = objPoliza.numero_poliza
                        Me.txtPolizaConsec.Text = objPoliza.aseguradora

                        If Me.txtExpeditionDate.Text <> "" Or Me.txtPolizaNumber.Text <> "" Or Me.txtPolizaConsec.Text <> "" Then
                            Me.HFPolRequired.Value = 1
                        End If

                        'cargar los detalles de la poliza
                        objPolizaDetails = facade.loadPolizaDetails(applicationCredentials, objPoliza.id)

                        Session("contractorLegalEntityByContractRequestList") = objPolizaDetails
                        Me.gvPolizaConcept.DataSource = objPolizaDetails
                        Me.gvPolizaConcept.DataBind()

                        'Se carga la lista de contratistas personas jurídicas

                        ''Se carga la lista de contratistas personas naturales
                        'Session("contractorNaturalPersonByContractRequestList") = objContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST
                        '' Se actualiza la informacion de la grilla
                        'Me.gvContractorNaturalPerson.DataSource = objContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST
                        'Me.gvContractorNaturalPerson.DataBind()

                        'Se carga la información corespondiente a Objeto y valor
                        'With objContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST
                        '    'Se veirifica que almenos los 2 primeros campos hayan sido llenados
                        '    Me.txtSubjectContract.Text = .subjectcontract
                        '    Me.txtProductsOrDeliverables.Text = .productsordeliverables
                        '    Me.txtContractValue.Text = .contractvalue.ToString("#,###")
                        '    Me.txtContributionAmount.Text = .contributionamount.ToString("#,###")
                        '    Me.txtFeesConsultantByInstitution.Text = .feesconsultantbyinstitution.ToString("#,###")
                        '    Me.txtTotalFeesIntegralConsultant.Text = .totalfeesintegralconsultant.ToString("#,###")
                        '    Me.txtContributionAmountRecipientInstitution.Text = .contributionamountrecipientinstitution.ToString("#,###")
                        '    Me.ddlCurrency.SelectedValue = .idcurrency
                        'End With

                        ''Se carga la lista de pagos de la solicitud de contrato actual
                        'Session("paymentsListByContractRequestList") = objContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST
                        '' Se actualiza la informacion de la grilla
                        'Me.gvPaymentsList.DataSource = objContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST
                        'Me.gvPaymentsList.DataBind()

                        'Se carga la información corespondiente a los datos del contrato de la solicitud actual
                        'With objContractRequest.CONTRACTDATABYCONTRACTREQUEST
                        '    If (.startdate > CDate("1900/01/01")) Then Me.txtStartDate.Text = .startdate.ToString("yyyy/MM/dd")
                        '    If (.enddate > CDate("1900/01/01")) Then Me.txtEndDate.Text = .enddate.ToString("yyyy/MM/dd")
                        '    Me.txtBudgetValidity.Text = .budgetvalidity
                        '    Me.txtContactData.Text = .contactdata
                        '    Me.txtEmail.Text = .email
                        '    Me.txtTelephone.Text = .telephone
                        'End With

                        'Se carga la información corespondiente a las observaciones de la solicitud actual
                        'With objContractRequest.COMMENTSBYCONTRACTREQUEST
                        '    Me.txtAdditionalComments.Text = .additionalcomments
                        '    Me.chkStartActRequires.Checked = .startactrequires
                        '    If (.datenoticeexpiration > CDate("1900/01/01")) Then Me.txtDateNoticeExpiration.Text = .datenoticeexpiration.ToString("yyyy/MM/dd")
                        '    Me.txtContractNumber.Text = .contractnumber
                        '    Me.txtPurchaseOrder.Text = .purchaseorder
                        'End With

                        If op.Equals("show") Then

                            ' cargar el titulo
                            Session("lblTitle") = "MOSTRAR CONTRATO"

                            ' ocultar los botones para realizar modificaciones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                        Else

                            ' cargar el titulo
                            Session("lblTitle") = "EDITAR CONTRATO"

                        End If

                        'validar si el contrato esta finalizado
                        If objContractRequest.enabled = True Then
                            ' cargar el titulo
                            Session("lblTitle") = "CONSULTAR CONTRATO"
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
                        objContractRequest = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objContractRequest As New ContractRequestEntity
        Dim objpoliza As New PolizaEntity
        Dim objpolizadetails As New PolizaDetailsEntity
        Dim requestNumber As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Me.txtEndingDate.Text = Me.HFEndDate.Value

        'validar listas
        If Me.ddlProject.SelectedItem.Text = "Seleccione..." Then
            Me.lblHelpidproject.Text = "*"
            Me.lblHelpidproject.ForeColor = Drawing.Color.Red
            'Me.TabContainer1.ActiveTabIndex = 0
            Exit Sub
        Else
            Me.lblHelpidproject.Text = ""
        End If

        If Me.txtcontractnumberadjusted.Text = "" Then
            Me.lblHelpcontractnumberadjusted.Text = "Debe escribir un número de contrato"
            Me.lblHelpcontractnumberadjusted.ForeColor = Drawing.Color.Red
            Exit Sub
        Else
            Me.lblHelpcontractnumberadjusted.Text = ""
        End If

        If Me.PolizaRequired.Checked = True Then

            If Me.txtPolizaConsec.Text = "" Or Me.txtPolizaNumber.Text = "" Or Me.txtExpeditionDate.Text = "" Then

                Me.lblinformation.Text = "Falta ingresar datos de la poliza. Por favor verifique."
                Exit Sub

            End If

        End If

        'If Me.ddlConfidential.SelectedItem.Text = "Seleccione..." Then
        '    Me.lblNfoConfidential.Text = "*"
        '    Me.lblNfoConfidential.ForeColor = Drawing.Color.Red
        '    Me.TabContainer1.ActiveTabIndex = 0
        '    Exit Sub
        'Else
        '    Me.lblNfoConfidential.Text = ""
        'End If

        Try
            ' cargar los valores registrados por el usuario
            objContractRequest.idmanagement = IIf(Me.ddlManagement.SelectedValue.Length > 0, Me.ddlManagement.SelectedValue, 0)
            objContractRequest.idproject = IIf(Me.ddlProject.SelectedValue.Length > 0, Me.ddlProject.SelectedValue, 0)
            objContractRequest.idcontractnature = IIf(Me.ddlContractNature.SelectedValue.Length > 0, Me.ddlContractNature.SelectedValue, 0)
            objContractRequest.contractnumberadjusted = IIf(Me.txtcontractnumberadjusted.Text <> "", Me.txtcontractnumberadjusted.Text, "")
            'agregar a la consulta los campos nuevos del formulario
            If Me.txtInitialDate.Text <> "" Then
                objContractRequest.startdate = Convert.ToDateTime(Me.txtInitialDate.Text)
            Else
                objContractRequest.startdate = Nothing 'Convert.ToDateTime("2000-01-01")
            End If

            If Me.txtSubscriptionDate.Text <> "" Then
                objContractRequest.suscriptdate = Convert.ToDateTime(Me.txtSubscriptionDate.Text)
            Else
                objContractRequest.suscriptdate = Nothing 'Convert.ToDateTime("2000-01-01")
            End If

            If Me.txtContractDuration.Text <> "" Then
                objContractRequest.monthduration = Convert.ToDecimal(Me.txtContractDuration.Text)
            Else
                objContractRequest.monthduration = Nothing
            End If

            If Me.txtLiquidationDate.Text <> "" Then
                objContractRequest.LiquidationDate = Convert.ToDateTime(Me.txtLiquidationDate.Text)
            Else
                objContractRequest.LiquidationDate = Nothing
            End If

            'objContractRequest.supervisor = IIf(Me.txtSupervisor.Text <> "", Convert.ToString(Me.txtSupervisor.Text), "")
            objContractRequest.confidential = IIf(Me.ddlConfidential.SelectedValue.Length > 0, Me.ddlConfidential.SelectedValue, -1)
            objContractRequest.signedcontract = Me.chkSignedContract.Checked
            objContractRequest.notes = Convert.ToString(Me.txtObs.Text)

            objContractRequest.idprocessinstance = 0
            objContractRequest.idactivityinstance = 0
            objContractRequest.enabled = False
            objContractRequest.iduser = applicationCredentials.UserID
            objContractRequest.createdate = Now

            'Se almacena la información de los contratistas personas jurídicas
            objContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST = DirectCast(Session("contractorLegalEntityByContractRequestList"), List(Of ContractorLegalEntityByContractRequestEntity))

            If Me.txtExpeditionDate.Text = "" Then
            Else
                'Se almacena informacion la poliza
                If Me.txtExpeditionDate.Text <> "" Then
                    objpoliza.fecha_exp = Convert.ToDateTime(Me.txtExpeditionDate.Text)
                Else
                    objpoliza.fecha_exp = Nothing
                End If


                objpoliza.numero_poliza = Me.txtPolizaNumber.Text
                objpoliza.aseguradora = Me.txtPolizaConsec.Text
                objpoliza.fecha_ven = Nothing 'Convert.ToDateTime("2000-01-01")
            End If

            'Se almacena la información de los contratistas personas naturales
            'objContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST = DirectCast(Session("contractorNaturalPersonByContractRequestList"), List(Of ContractorNaturalPersonByContractRequestEntity))

            'Se almacena la información correspondiente a Objeto y valor de la solicitud actual
            'With objContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST
            '    .subjectcontract = Me.txtSubjectContract.Text
            '    .productsordeliverables = Me.txtProductsOrDeliverables.Text
            '    If (Me.txtContractValue.Text.Length > 0) Then .contractvalue = PublicFunction.ConvertStringToDouble(Me.txtContractValue.Text)
            '    If (Me.txtContributionAmount.Text.Length > 0) Then .contributionamount = PublicFunction.ConvertStringToDouble(Me.txtContributionAmount.Text)
            '    If (Me.txtFeesConsultantByInstitution.Text.Length > 0) Then .feesconsultantbyinstitution = PublicFunction.ConvertStringToDouble(Me.txtFeesConsultantByInstitution.Text)
            '    If (Me.txtTotalFeesIntegralConsultant.Text.Length > 0) Then .totalfeesintegralconsultant = PublicFunction.ConvertStringToDouble(Me.txtTotalFeesIntegralConsultant.Text)
            '    If (Me.txtContributionAmountRecipientInstitution.Text.Length > 0) Then .contributionamountrecipientinstitution = PublicFunction.ConvertStringToDouble(Me.txtContributionAmountRecipientInstitution.Text)
            '    .idcurrency = Me.ddlCurrency.SelectedValue
            'End With

            'Se almacena la información de la lista de pagos de la solicitud de contrato actual
            'objContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST = DirectCast(Session("paymentsListByContractRequestList"), List(Of PaymentsListByContractRequestEntity))

            'Se almacena la información correspondiente a los datos del contrato
            'With objContractRequest.CONTRACTDATABYCONTRACTREQUEST
            '    .contractduration = Me.txtContractDuration.Text
            '    If (Me.txtStartDate.Text.Length > 0) Then .startdate = Me.txtStartDate.Text
            '    If (Me.txtEndDate.Text.Length > 0) Then .enddate = Me.txtEndDate.Text
            '    .supervisor = Me.txtSupervisor.Text
            '    .budgetvalidity = Me.txtBudgetValidity.Text
            '    .contactdata = Me.txtContactData.Text
            '    .email = Me.txtEmail.Text
            '    .telephone = Me.txtTelephone.Text
            'End With

            'Se almacena la información correspondiente a las observaciones de la solicitud de contrato actual
            'With objContractRequest.COMMENTSBYCONTRACTREQUEST

            '    .additionalcomments = Me.txtAdditionalComments.Text
            '    .startactrequires = Me.chkStartActRequires.Checked
            '    If (Me.txtDateNoticeExpiration.Text.Length > 0) Then .datenoticeexpiration = Me.txtDateNoticeExpiration.Text
            '    .contractnumber = Me.txtContractNumber.Text
            '    .purchaseorder = Me.txtPurchaseOrder.Text

            'End With

            ' almacenar la entidad
            objContractRequest.requestnumber = facade.addContractRequest(applicationCredentials, objContractRequest)

            If Me.PolizaRequired.Checked = False Then
            Else
                'capturar id del contrato y agregar poliza
                objpoliza.contrato_id = objContractRequest.requestnumber
                objpoliza.id = facade.addPoliza(applicationCredentials, objpoliza)

                'Se agregan los conceptos de la poliza
                facade.addPolizaDetails(applicationCredentials, DirectCast(Session("PolizaDetailsList"), List(Of PolizaDetailsEntity)), objpoliza.id)

                'actualizar idpoliza en contratacion
                facade.updatePolizaId(applicationCredentials, objpoliza.id, objContractRequest.requestnumber)
            End If


            ' crear el proceso en el BPM
            'objContractRequest.idprocessinstance = GattacaApplication.createProcessInstance(applicationCredentials, PublicFunction.getSettingValue("BPM.ProcessCase.PR06"), _
            '                                                                     "WebForm", "ContractRequestEntity", objContractRequest.requestnumber, 0)

            '' Iniciarlo
            'objContractRequest.idactivityinstance = GattacaApplication.startProcessInstance(applicationCredentials, objContractRequest.idprocessinstance, _
            '                                                                     PublicFunction.getSettingValue("BPM.ProcessCase.PR06"), _
            '                                                                     "WebForm", "ContractRequestEntity", objContractRequest.requestnumber, 0)
            ' actualizar la Idea
            facade.updateContractRequest(applicationCredentials, objContractRequest)

            ' cerrar esta pagina
            ScriptManager.RegisterStartupScript(Me, GetType(String), "close", "<script>window.close();</script>", False)

            'mandar mensaje y controlar botones
            'HttpContext.Current.ApplicationInstance.CompleteRequest()
            Me.lblSuccess.Text = "Contrato guardado satisfactoriamente!"
            Me.lblSuccess.Visible = True
            'Me.btnExport.Visible = True
            Me.btnAddData.Visible = False
            'Me.btnAddContractorLegalEntity.Visible = False
            Me.addConcept.Visible = False
            'Me.txtEndDate.Text = Me.HFEndDate.Value

            requestNumber = objContractRequest.requestnumber

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada
            System.Threading.Thread.ResetAbort()
            Return
        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            objContractRequest = Nothing
            facade = Nothing

        End Try

        If requestNumber <> "" Then
            redirectToEdit(requestNumber)
        End If

    End Sub

    Protected Sub redirectToEdit(ByVal data As String)
        Response.Redirect("/Engagement/addContractRequest.aspx?successSave=1&op=edit&id=" & data)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchContractRequest.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal Finished As String = "") Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objContractRequest As New ContractRequestEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objpoliza As New PolizaEntity
        Dim objpolizadetails As New PolizaDetailsEntity
        Dim requestNumber As String = ""

        ' cargar el registro referenciado
        objContractRequest = facade.loadContractRequest(applicationCredentials, Request.QueryString("ID"))

        'Pasar en el Hidden field el value del tab
        Me.HFActivetab.Value = 1

        Try
            ' cargar los datos
            objContractRequest.idmanagement = IIf(Me.ddlManagement.SelectedValue.Length > 0, Me.ddlManagement.SelectedValue, 0)
            objContractRequest.idproject = Me.HFProject.Value 'IIf(Me.ddlProject.SelectedValue.Length > 0, Me.ddlProject.SelectedValue, 0)
            objContractRequest.idcontractnature = IIf(Me.ddlContractNature.SelectedValue.Length > 0, Me.ddlContractNature.SelectedValue, 0)
            objContractRequest.contractnumberadjusted = Me.txtcontractnumberadjusted.Text
            objContractRequest.idprocessinstance = 0
            objContractRequest.idactivityinstance = 0

            'agregar campos nuevos
            objContractRequest.signedcontract = Me.chkSignedContract.Checked

            If Me.txtInitialDate.Text <> "" Then
                objContractRequest.startdate = Convert.ToDateTime(Me.txtInitialDate.Text)
            Else
                objContractRequest.startdate = Convert.ToDateTime("2000-01-01")
            End If

            If Me.txtSubscriptionDate.Text <> "" Then
                objContractRequest.suscriptdate = Convert.ToDateTime(Me.txtSubscriptionDate.Text)
            Else
                objContractRequest.suscriptdate = Convert.ToDateTime("2000-01-01")
            End If

            If Me.txtLiquidationDate.Text <> "" Then
                objContractRequest.LiquidationDate = Convert.ToDateTime(Me.txtLiquidationDate.Text)
            Else
                objContractRequest.suscriptdate = Convert.ToDateTime("2000-01-01")
            End If

            objContractRequest.confidential = IIf(Me.ddlConfidential.SelectedValue.Length > 0, Me.ddlConfidential.SelectedValue, 0)

            If Me.txtContractDuration.Text <> "" Then
                objContractRequest.monthduration = Convert.ToDecimal(Me.txtContractDuration.Text)
            End If

            'objContractRequest.supervisor = IIf(Me.txtSupervisor.Text <> "", Convert.ToString(Me.txtSupervisor.Text), "")
            objContractRequest.notes = Convert.ToString(Me.txtObs.Text)

            If Me.txtExpeditionDate.Text = "" Then
            Else
                'Se almacena informacion la poliza
                objpoliza.fecha_exp = Me.txtExpeditionDate.Text
                objpoliza.numero_poliza = Me.txtPolizaNumber.Text
                objpoliza.aseguradora = Me.txtPolizaConsec.Text
            End If

            If Me.PolizaRequired.Checked = False Then
            Else
                'capturar id del contrato y agregar poliza
                objpoliza.contrato_id = objContractRequest.requestnumber
                objpoliza.id = facade.addPoliza(applicationCredentials, objpoliza)

                'Se agregan los conceptos de la poliza
                facade.addPolizaDetails(applicationCredentials, DirectCast(Session("PolizaDetailsList"), List(Of PolizaDetailsEntity)), objpoliza.id)

                'actualizar idpoliza en contratacion
                facade.updatePolizaId(applicationCredentials, objpoliza.id, objContractRequest.requestnumber)
            End If

            'objContractRequest.enabled = Me.ddlEnabled.SelectedValue

            'Se almacena la información de los contratistas personas jurídicas
            'objContractRequest.CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST = DirectCast(Session("contractorLegalEntityByContractRequestList"), List(Of ContractorLegalEntityByContractRequestEntity))

            'Se almacena la información de los contratistas personas naturales
            'objContractRequest.CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST = DirectCast(Session("contractorNaturalPersonByContractRequestList"), List(Of ContractorNaturalPersonByContractRequestEntity))

            'Se almacena la información correspondiente a Objeto y valor de la solicitud actual
            'With objContractRequest.SUBJECTANDVALUEBYCONTRACTREQUEST
            '    .subjectcontract = Me.txtSubjectContract.Text
            '    .productsordeliverables = Me.txtProductsOrDeliverables.Text
            '    .contractvalue = PublicFunction.ConvertStringToDouble(IIf(Me.txtContractValue.Text.Length > 0, Me.txtContractValue.Text, "0"))
            '    .contributionamount = PublicFunction.ConvertStringToDouble(IIf(Me.txtContributionAmount.Text.Length > 0, Me.txtContributionAmount.Text, "0"))
            '    .feesconsultantbyinstitution = PublicFunction.ConvertStringToDouble(IIf(Me.txtFeesConsultantByInstitution.Text.Length > 0, Me.txtFeesConsultantByInstitution.Text, "0"))
            '    .totalfeesintegralconsultant = PublicFunction.ConvertStringToDouble(IIf(Me.txtTotalFeesIntegralConsultant.Text.Length > 0, Me.txtTotalFeesIntegralConsultant.Text, "0"))
            '    .contributionamountrecipientinstitution = PublicFunction.ConvertStringToDouble(IIf(Me.txtContributionAmountRecipientInstitution.Text.Length > 0, Me.txtContributionAmountRecipientInstitution.Text, "0"))
            '    .idcurrency = Me.ddlCurrency.SelectedValue
            'End With

            'Se almacena la información de la lista de pagos de la solicitud de contrato actual
            'objContractRequest.PAYMENTSLISTBYCONTRACTREQUESTLIST = DirectCast(Session("paymentsListByContractRequestList"), List(Of PaymentsListByContractRequestEntity))

            'Se almacena la información correspondiente a los datos del contrato
            'With objContractRequest.CONTRACTDATABYCONTRACTREQUEST
            '    .contractduration = Me.txtContractDuration.Text
            '    If (Me.txtStartDate.Text.Length > 0) Then
            '        .startdate = Me.txtStartDate.Text
            '    Else
            '        .startdate = Nothing
            '    End If
            '    If (Me.txtEndDate.Text.Length > 0) Then
            '        .enddate = Me.txtEndDate.Text
            '    Else
            '        .enddate = Nothing
            '    End If
            '    .supervisor = Me.txtSupervisor.Text
            '    .budgetvalidity = Me.txtBudgetValidity.Text
            '    .contactdata = Me.txtContactData.Text
            '    .email = Me.txtEmail.Text
            '    .telephone = Me.txtTelephone.Text
            'End With

            'Se verifica la finalización del contrato
            If Finished = "True" Then
                objContractRequest.enabled = "True"
            End If

            'Se almacena la información correspondiente a las observaciones de la solicitud de contrato actual
            'With objContractRequest.COMMENTSBYCONTRACTREQUEST

            '    .additionalcomments = Me.txtAdditionalComments.Text
            '    .startactrequires = Me.chkStartActRequires.Checked
            '    If (Me.txtDateNoticeExpiration.Text.Length > 0) Then
            '        .datenoticeexpiration = Me.txtDateNoticeExpiration.Text
            '    Else
            '        .datenoticeexpiration = Nothing
            '    End If
            '    .contractnumber = Me.txtContractNumber.Text
            '    .purchaseorder = Me.txtPurchaseOrder.Text

            'End With

            ' modificar el registro
            facade.updateContractRequest(applicationCredentials, objContractRequest)

            If Finished = "True" Then
                'Actualizar el proyecto a estado contratado.
                facade.finishproject(applicationCredentials, HFProject.Value)

                'Enviar correo
                Dim correo As PostMail_SndMail = New PostMail_SndMail()
                Dim asunto As String
                Dim mensajecorreo As String
                Dim destinatarios As String = ""
                Dim destinatarios2 As String = ""
                Dim destinatarios3 As String = ""
                Dim sql As New StringBuilder
                Dim data As DataTable
                Dim primero As Integer = 0

                'consultar los usuarios de seguimiento
                sql.Append("use FSC_eSecurity select idapplicationuser from UsersByGroup where idusergroup = 28")
                data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                If data.Rows.Count > 0 Then

                    For Each itemdatatable As DataRow In data.Rows
                        primero = primero + 1

                        If primero = 1 Then
                            destinatarios = itemdatatable("idapplicationuser") & " and enabled = 'T' "
                        Else
                            destinatarios = destinatarios & " or id=" & itemdatatable("idapplicationuser") & " and enabled = 'T' "
                        End If

                    Next

                End If

                'reiniciar las variables
                sql = New StringBuilder
                primero = 0

                'consultar el id del lider del proyecto
                sql.Append("use FSC_eProject select iduser from project where project.id = " & objContractRequest.idproject)
                data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                If data.Rows.Count > 0 Then
                    destinatarios2 = destinatarios2 & " or id=" & data(0)("iduser")
                End If

                'reiniciar las variables
                sql = New StringBuilder
                primero = 0

                'consultar admins
                sql.Append("select user_id from usersbymailgroup where usersbymailgroup.mailgroup = 2 or usersbymailgroup.mailgroup = 1")
                data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                If data.Rows.Count > 0 Then

                    For Each itemDataTable As DataRow In data.Rows

                        destinatarios3 = destinatarios3 & " or id=" & itemDataTable("user_id")

                    Next

                End If

                'reiniciar las variables
                sql = New StringBuilder
                primero = 0

                'consultar los emails
                sql.Append("use FSC_eSecurity select email from ApplicationUser where id =" & destinatarios)
                sql.Append(" " & destinatarios2.ToString)
                sql.Append(" " & destinatarios3.ToString)
                data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                If data.Rows.Count > 0 Then

                    For Each itemDataTable As DataRow In data.Rows

                        primero = primero + 1

                        If primero = 1 Then
                            destinatarios = itemDataTable("email")
                        Else
                            destinatarios = destinatarios & "," & itemDataTable("email")
                        End If

                    Next

                End If

                asunto = "Generación de Acta de Inicio del " & Me.ddlContractNature.SelectedItem.ToString & " - " & Me.lblProjectNumber.Text

                mensajecorreo = "Hola"
                mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "El proceso de contratación del " & Me.ddlContractNature.SelectedItem.ToString & " - " & Me.lblProjectNumber.Text & " finalizó con éxito. Puede realizar comité de inicio."
                mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "Cordialmente,"
                mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "ELVIRA"
                mensajecorreo = mensajecorreo & Chr(13) & "EvaLuación y Valoración de la InveRsión Articulada"
                mensajecorreo = mensajecorreo & Chr(13) & "Fundación Saldarriaga Concha"

                correo.SendMail(destinatarios, asunto, mensajecorreo)


            End If

            ' ir al administrador
            'Response.Redirect("searchContractRequest.aspx")
            Response.Redirect("/Engagement/searchContractRequest.aspx?successFinish=1")

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
            objContractRequest = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteContractRequest(applicationCredentials, Request.QueryString("requestnumber"))

            ' ir al administrador
            Response.Redirect("searchContractRequest.aspx")

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

    Protected Sub ddlManagement_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlManagement.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de proyectos
        Me.LoadDropDownListProjectByManagement()
        'Me.TabContainer1.ActiveTabIndex = 0

    End Sub

    'Protected Sub btnAddContractorLegalEntity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddContractorLegalEntity.Click

    '    'Definir los objetos
    '    Dim contractorLegalEntityByContractRequestList As List(Of ContractorLegalEntityByContractRequestEntity)
    '    Dim contractorLegalEntityByContractRequest As New ContractorLegalEntityByContractRequestEntity

    '    Me.txtEndingDate.Text = Me.HFEndDate.Value

    '    'Se inicializan controles
    '    Me.lblHelpNit.Text = ""

    '    'Se carga la lista de la session
    '    contractorLegalEntityByContractRequestList = DirectCast(Session("contractorLegalEntityByContractRequestList"),  _
    '    List(Of ContractorLegalEntityByContractRequestEntity))

    '    'Se poblan los datos del contratista
    '    With contractorLegalEntityByContractRequest
    '        .entitynamedescription = Me.HFtextactor.Value
    '        .nit = Me.txtNit.Text
    '        .legalrepresentative = Me.txtLegalRepresentative.Text
    '        '.contractorname = Me.txtContractorName.Text
    '        .identificationnumber = Me.txtIdentificationNumber.Text
    '    End With

    '    If Not (contractorLegalEntityByContractRequestList.Exists(Function(unContractorLegalEntityByContractRequest) _
    '        unContractorLegalEntityByContractRequest.nit = contractorLegalEntityByContractRequest.nit)) Then
    '        ' agregarlos
    '        contractorLegalEntityByContractRequestList.Add(contractorLegalEntityByContractRequest)

    '        ' mostrar
    '        Me.gvContractorLegalEntity.DataSource = contractorLegalEntityByContractRequestList
    '        Me.gvContractorLegalEntity.DataBind()

    '        'Se llama al metodo que permite limpiar los controles de la pestaña actual
    '        Me.CleanContractorLegalEntity()

    '    Else

    '        'Se muestra el mensaje notificando el error
    '        Me.lblHelpNit.ForeColor = Drawing.Color.Red
    '        Me.lblHelpNit.Text = "No se pueden ingresar dos contratistas con el mismo Nit."
    '        Me.txtNit.Focus()

    '    End If

    'End Sub

    'Protected Sub gvContractorLegalEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvContractorLegalEntity.SelectedIndexChanged

    '    ' definir los objetos
    '    Dim contractorLegalEntityByContractRequestList As List(Of ContractorLegalEntityByContractRequestEntity)
    '    Dim index As Integer = 0

    '    ' cargar la lista de la session
    '    contractorLegalEntityByContractRequestList = DirectCast(Session("contractorLegalEntityByContractRequestList"),  _
    '    List(Of ContractorLegalEntityByContractRequestEntity))

    '    ' remover el seleccionado
    '    contractorLegalEntityByContractRequestList.RemoveAt(Me.gvContractorLegalEntity.SelectedIndex)

    '    ' mostrar
    '    Me.gvContractorLegalEntity.DataSource = contractorLegalEntityByContractRequestList
    '    Me.gvContractorLegalEntity.DataBind()

    '    'Se selecciona la pestama de ubicaciones por idea
    '    Me.TabContainer1.ActiveTabIndex = 1

    'End Sub

    'Protected Sub btnAddContractorNaturalPerson_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddContractorNaturalPerson.Click

    '    'Definir los objetos
    '    Dim contractorNaturalPersonByContractRequestList As List(Of ContractorNaturalPersonByContractRequestEntity)
    '    Dim contractorNaturalPersonByContractRequest As New ContractorNaturalPersonByContractRequestEntity

    '    'Se inicializan controles
    '    Me.lblHelpNitContractorNaturalPerson.Text = ""

    '    'Se carga la lista de la session
    '    contractorNaturalPersonByContractRequestList = DirectCast(Session("contractorNaturalPersonByContractRequestList"),  _
    '    List(Of ContractorNaturalPersonByContractRequestEntity))

    '    'Se poblan los datos del contratista
    '    With contractorNaturalPersonByContractRequest
    '        .nit = Me.txtNitContractorNaturalPerson.Text
    '        .contractorname = Me.txtContractorNameContractorNaturalPerson.Text
    '    End With

    '    If Not (contractorNaturalPersonByContractRequestList.Exists(Function(uncontractorNaturalPersonByContractRequest) _
    '        uncontractorNaturalPersonByContractRequest.nit = contractorNaturalPersonByContractRequest.nit)) Then

    '        ' agregarlos
    '        contractorNaturalPersonByContractRequestList.Add(contractorNaturalPersonByContractRequest)

    '        ' mostrar
    '        Me.gvContractorNaturalPerson.DataSource = contractorNaturalPersonByContractRequestList
    '        Me.gvContractorNaturalPerson.DataBind()

    '        'Se llama al metodo que permite limpiar los controles de la pestaña actual
    '        Me.CleanContractorNaturalPerson()

    '    Else

    '        'Se muestra el mensaje notificando el error
    '        Me.lblHelpNitContractorNaturalPerson.ForeColor = Drawing.Color.Red
    '        Me.lblHelpNitContractorNaturalPerson.Text = "No se pueden ingresar dos contratistas con el mismo Nit."
    '        Me.txtNitContractorNaturalPerson.Focus()

    '    End If

    'End Sub

    'Protected Sub gvContractorNaturalPerson_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvContractorNaturalPerson.SelectedIndexChanged

    '    ' definir los objetos
    '    Dim contractorNaturalPersonByContractRequestList As List(Of ContractorNaturalPersonByContractRequestEntity)
    '    Dim index As Integer = 0

    '    ' cargar la lista de la session
    '    contractorNaturalPersonByContractRequestList = DirectCast(Session("contractorNaturalPersonByContractRequestList"),  _
    '    List(Of ContractorNaturalPersonByContractRequestEntity))

    '    ' remover el seleccionado
    '    contractorNaturalPersonByContractRequestList.RemoveAt(Me.gvContractorNaturalPerson.SelectedIndex)

    '    ' mostrar
    '    Me.gvContractorNaturalPerson.DataSource = contractorNaturalPersonByContractRequestList
    '    Me.gvContractorNaturalPerson.DataBind()

    '    'Se selecciona la pestama de ubicaciones por idea
    '    Me.TabContainer1.ActiveTabIndex = 2

    'End Sub

    'Protected Sub btnAddPaymentsList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPaymentsList.Click

    '    'Definir los objetos
    '    Dim paymentsListByContractRequestList As List(Of PaymentsListByContractRequestEntity)
    '    Dim paymentsListByContractRequest As New PaymentsListByContractRequestEntity

    '    'Se inicializan controles
    '    Me.lblHelpValue.Text = ""

    '    'Se carga la lista de la session
    '    paymentsListByContractRequestList = DirectCast(Session("paymentsListByContractRequestList"),  _
    '    List(Of PaymentsListByContractRequestEntity))

    '    'Se poblan los datos de la lista de pagos
    '    With paymentsListByContractRequest
    '        If (Me.txtValue.Text.Length > 0) Then .value = PublicFunction.ConvertStringToDouble(Me.txtValue.Text)
    '        If (Me.txtPercentage.Text.Length > 0) Then .percentage = Convert.ToDouble(Me.txtPercentage.Text, CultureInfo.CurrentCulture)
    '        .datePaymentsList = Me.txtdate.Text
    '    End With

    '    If Not (paymentsListByContractRequestList.Exists(Function(unPaymentsListByContractRequest) _
    '        unPaymentsListByContractRequest.value = paymentsListByContractRequest.value _
    '        AndAlso unPaymentsListByContractRequest.datePaymentsList = paymentsListByContractRequest.datePaymentsList)) Then
    '        ' agregarlos
    '        paymentsListByContractRequestList.Add(paymentsListByContractRequest)

    '        ' mostrar
    '        Me.gvPaymentsList.DataSource = paymentsListByContractRequestList
    '        Me.gvPaymentsList.DataBind()

    '        'Se llama al metodo que permite limpiar los controles de la pestaña actual
    '        Me.CleanPaymentsList()

    '    Else

    '        'Se muestra el mensaje notificando el error
    '        Me.lblHelpValue.ForeColor = Drawing.Color.Red
    '        Me.lblHelpValue.Text = "No se pueden ingresar dos pagos con el mismo valor y la misma fecha."
    '        Me.txtValue.Focus()

    '    End If

    'End Sub

    'Protected Sub gvPaymentsList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPaymentsList.SelectedIndexChanged

    '    ' definir los objetos
    '    Dim paymentsListByContractRequestList As List(Of PaymentsListByContractRequestEntity)
    '    Dim index As Integer = 0

    '    ' cargar la lista de la session
    '    paymentsListByContractRequestList = DirectCast(Session("paymentsListByContractRequestList"),  _
    '    List(Of PaymentsListByContractRequestEntity))

    '    ' remover el seleccionado
    '    paymentsListByContractRequestList.RemoveAt(Me.gvPaymentsList.SelectedIndex)

    '    ' mostrar
    '    Me.gvPaymentsList.DataSource = paymentsListByContractRequestList
    '    Me.gvPaymentsList.DataBind()

    '    'Se selecciona la pestama de ubicaciones por idea
    '    Me.TabContainer1.ActiveTabIndex = 4

    'End Sub

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
            'Se agregan las rutinas para poblar el combo de Gerencias
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlManagement.DataSource = facade.getManagementList(applicationCredentials, enabled:="1", order:="Code")
            Else
                Me.ddlManagement.DataSource = facade.getManagementList(applicationCredentials, order:="Code")
            End If
            Me.ddlManagement.DataValueField = "Id"
            Me.ddlManagement.DataTextField = "Code"
            Me.ddlManagement.DataBind()

            'TODO: Traer proyectos en el combo proyecto aprobado
            'autor y fecha: Pedro Cruz 29/Jul/13
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlProject.DataSource = facade.getProjectListcontract(applicationCredentials)
            Else
                Me.ddlProject.DataSource = facade.getProjectListcontract(applicationCredentials)
            End If

            Me.ddlProject.DataValueField = "Id"
            Me.ddlProject.DataTextField = "code"
            Me.ddlProject.DataBind()

            'Traer lista de supervisores

            Me.ddlSupervisor.DataSource = facade.getNaturalThird(applicationCredentials)
            Me.ddlSupervisor.DataValueField = "Id"
            Me.ddlSupervisor.DataTextField = "Name"
            Me.ddlSupervisor.DataBind()

            ''Se llama la metodo que permite cargar el combo de terceros

            'Dim valor As String = Convert.ToString(Me.tipopersona.SelectedItem)

            'If valor = "Persona natural" Then
            '    valor = 1
            'Else
            '    valor = 0
            'End If

            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlActor.DataSource = facade.loadPersonas(applicationCredentials, Convert.ToInt32(valor))
            'Else
            '    Me.ddlActor.DataSource = facade.loadPersonas(applicationCredentials, Convert.ToInt32(valor))
            'End If

            'Me.ddlActor.DataValueField = "Id"
            'Me.ddlActor.DataTextField = "name"
            'Me.ddlActor.DataBind()

            ' Me.LoadDropDownListThird(facade, applicationCredentials)

            'Se llama al metodo que permite poblar el combo de proyectos
            'Me.LoadDropDownListProjectByManagement()

            'Se pobla el combo de tipos de moneda
            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlCurrency.DataSource = facade.getCurrencyList(applicationCredentials, enabled:="T", order:="Code")
            'Else
            '    Me.ddlCurrency.DataSource = facade.getCurrencyList(applicationCredentials, order:="Code")
            'End If
            'Me.ddlCurrency.DataValueField = "Id"
            'Me.ddlCurrency.DataTextField = "Code"
            'Me.ddlCurrency.DataBind()

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
    ''' Metodo que permite poblar el combo de proyectos por gerencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListProjectByManagement()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idClosedState As String = ""

        Try
            'Se consulta el código correspondiente a la fase de "Evaluación y Cierre"
            idClosedState = ConfigurationManager.AppSettings("IdClosedState").ToString()

            'Se pobla el combo con los proyectos que pertenecen a una gerencia determinada y que adicionalmente estan en una fase diferente a la de "Evaluación y Cierre"
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlProject.DataSource = facade.getProjectByManagementList(applicationCredentials, idManagement:=Me.ddlManagement.SelectedValue, enabled:="1", order:="Code", idphase:=idClosedState)
            Else
                Me.ddlProject.DataSource = facade.getProjectByManagementList(applicationCredentials, idManagement:=Me.ddlManagement.SelectedValue, order:="Code", idphase:=idClosedState)
            End If
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

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
    ''' Permite realizar la  limpieza de los controles de la pestaña de contratista persona jurídica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanContractorLegalEntity()

        ''Me.txtEntityNameDescription.Text = ""
        'Me.txtNit.Text = ""
        'Me.txtLegalRepresentative.Text = ""
        'Me.txtContractorName.Text = ""
        'Me.txtIdentificationNumber.Text = ""
        'Me.tipopersona.SelectedValue = 0
        'Me.txtEntityNameDescription.Focus()

    End Sub

    ''' <summary>
    ''' Permite realizar la  limpieza de los controles de la pestaña de contratista persona natural
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanContractorNaturalPerson()

        'Me.txtNitContractorNaturalPerson.Text = ""
        'Me.txtContractorNameContractorNaturalPerson.Text = ""
        'Me.txtNitContractorNaturalPerson.Focus()

    End Sub

    ''' <summary>
    ''' Permite realizar la  limpieza de los controles de la pestaña de lista de pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanPaymentsList()

        'Me.txtValue.Text = ""
        'Me.txtPercentage.Text = ""
        'Me.txtdate.Text = ""
        'Me.txtValue.Focus()

    End Sub

#End Region

    ''' <summary>
    ''' Metodo que permite cargar el combo de terceros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListThird(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)
        'Se pobla el combo


    End Sub

    Protected Sub addConcept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addConcept.Click

        Dim PolizaDetailsList As List(Of PolizaDetailsEntity)
        Dim PolizaDetails As New PolizaDetailsEntity
        Dim PolizaList As List(Of PolizaEntity) = New List(Of PolizaEntity)()
        Dim Poliza As PolizaEntity = New PolizaEntity

        Me.txtEndingDate.Text = Me.HFEndDate.Value

        'Pasar en el Hidden field el value del tab
        Me.HFActivetab.Value = 3

        'controlar que este digitado un numero de contrato
        If Not IsNumeric(Me.txtcontractnumberadjusted.Text) Then
            Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
            Me.lblAddPolizaNfo.Text = "El numero del contrato no es un número. O no se ha diligenciado."
            Exit Sub
        Else
            Me.lblAddPolizaNfo.Text = ""
        End If

        'controlar que este digitado un numero de poliza
        'If Not IsNumeric(Me.txtPolizaNumber.Text) Then
        '    Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
        '    Me.lblAddPolizaNfo.Text = "El numero de la poliza no es un número. O no se ha diligenciado."
        '    Exit Sub
        'Else
        '    Me.lblAddPolizaNfo.Text = ""
        'End If

        'controlar que este digitado el aseguradora de la poliza
        'If Not IsNumeric(Me.txtPolizaConsec.Text) Then
        '    Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
        '    Me.lblAddPolizaNfo.Text = "El numero del aseguradora la poliza no es un número. O no se ha diligenciado."
        '    Exit Sub
        'Else
        '    Me.lblAddPolizaNfo.Text = ""
        'End If

        'controlar que se haya escrito un concepto
        If Me.TextBox1.Text = "" Then
            Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
            Me.lblAddPolizaNfo.Text = "No se ha escrito el concepto."
            Exit Sub
        Else
            Me.lblAddPolizaNfo.Text = ""
        End If

        'verificar que se diligencien ambas fechas en el concepto
        If Me.txtInitDatePoliza.Text = "" Or Me.txtFinishDatePoliza.Text = "" Then
            Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
            Me.lblAddPolizaNfo.Text = "Por favor diligencie las fechas de vigencia de la póliza."
            Exit Sub
        Else
            Me.lblAddPolizaNfo.Text = ""
        End If

        'verificar que la finalización sea mayor o igual que el inicio
        If Me.txtInitDatePoliza.Text > Me.txtFinishDatePoliza.Text Then
            Me.lblAddPolizaNfo.ForeColor = Drawing.Color.Red
            Me.lblAddPolizaNfo.Text = "La fecha de fin de vigencia no debe se inferior a la fecha de inicio de la vigencia."
            Exit Sub
        Else
            Me.lblAddPolizaNfo.Text = ""
        End If

        Try

            PolizaDetailsList = DirectCast(Session("PolizaDetailsList"), List(Of PolizaDetailsEntity))
            PolizaList = DirectCast(Session("PolizaList"), List(Of PolizaEntity))

            'poliza
            Poliza.numero_poliza = Me.txtPolizaNumber.Text
            Poliza.aseguradora = Me.txtPolizaConsec.Text
            Poliza.contrato_id = Me.txtcontractnumberadjusted.Text

            PolizaList.Add(Poliza)

            'datos poliza
            'PolizaDetails.Id_Poliza =
            PolizaDetails.Concepto = Me.TextBox1.Text
            PolizaDetails.aseguradora = Me.txtPolizaConsec.Text
            PolizaDetails.inivig = Me.txtInitDatePoliza.Text
            PolizaDetails.finvig = Me.txtFinishDatePoliza.Text

            If Not PolizaDetailsList.Exists(Function(inipoliza) inipoliza.Concepto = PolizaDetails.Concepto) Then

                PolizaDetailsList.Add(PolizaDetails)

                Me.gvPolizaConcept.DataSource = PolizaDetailsList
                Me.gvPolizaConcept.DataBind()

                Me.TextBox1.Text = ""
                Me.txtInitDatePoliza.Text = ""
                Me.txtFinishDatePoliza.Text = ""

            End If

            'For Each itemDataTable As PolizaDetailsEntity In PolizaDetailsList
            ' Next


        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        End Try


    End Sub

    Protected Sub gvPolizaConcept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPolizaConcept.SelectedIndexChanged

        Dim PolizaDetailsList As List(Of PolizaDetailsEntity)
        Dim index As Integer = 0

        PolizaDetailsList = DirectCast(Session("polizadetailsList"), List(Of PolizaDetailsEntity))

        PolizaDetailsList.RemoveAt(Me.gvPolizaConcept.SelectedIndex)

        Me.gvPolizaConcept.DataSource = PolizaDetailsList
        Me.gvPolizaConcept.DataBind()


    End Sub

    Protected Sub Export_Terms_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export_Terms.Click

        Dim objProceeding_ReferenceTerms As Proceedings = New Proceedings()

        Dim DATAPROJECT As DataTable
        Dim DATAIDEA As DataTable
        Dim DATALOCATION As DataTable
        Dim DATAACTORS As DataTable
        Dim pagos As DataTable
        Dim V_id_proyecto As String



        Me.lblExportTerms.ForeColor = Drawing.Color.Green
        Me.lblExportTerms.Text = "Términos generados!"


        Try
            'validar si viene de agregar o editar
            If Me.HF_ID_Project.Value = "" Then
                V_id_proyecto = Me.HFProject.Value
            Else
                V_id_proyecto = Me.HF_ID_Project.Value
            End If

            Dim sql As New StringBuilder
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            sql.Append("select Name,Objective,Justification,ZoneDescription,BeginDate,Population,Duration,TotalCost,Results,FSCContribution,CounterpartValue,code,ResultsKnowledgeManagement,ResultsInstalledCapacity from Project where id = " & V_id_proyecto)
            DATAPROJECT = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            sql.Append("select distinct i.Id,tcc.Contract,l.Code,p.Code as programa from idea i    ")
            sql.Append("inner join ProgramComponentByIdea pi on (i.Id = pi.IdIdea)         ")
            sql.Append("inner join ProgramComponent pc on (pi.IdProgramComponent = pc.Id)  ")
            sql.Append("inner join Program p on (pc.IdProgram = p.Id)       ")
            sql.Append("INNER JOIN Project PJ ON (PJ.IdIdea= I.Id)              ")
            sql.Append("inner join StrategicLine l on (p.IdStrategicLine = l.Id)  ")
            sql.Append("inner join TypeContract tcc on  (tcc.Id=i.Idtypecontract)              ")
            sql.Append("where PJ.Id =" & V_id_proyecto)

            DATAIDEA = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            sql.Append("select deptoname,cityname from ProjectLocation  where IdProject=" & V_id_proyecto)
            DATALOCATION = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder


            sql.Append("select t.Name,t.contact,tp.Type,T.email,T.phone,T.documents from Third t    ")
            sql.Append("inner join ThirdByProject tp on tp.IdThird= t.Id              ")
            sql.Append("INNER JOIN Project P ON P.Id= tp.IdProject                  ")
            sql.Append("where tp.IdProject = " & V_id_proyecto)


            DATAACTORS = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            sql.Append("select valorparcial, porcentaje,entregable,fecha from Paymentflow where idproject= " & V_id_proyecto)
            pagos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


            'id de proyecto
            objProceeding_ReferenceTerms.id_idea_proyecto_t = V_id_proyecto
            'ruta
            objProceeding_ReferenceTerms.DirectorioActas = Server.MapPath("~")

            'datos de la tabla idea
            If DATAIDEA.Rows.Count > 0 Then

                'linea estrategica
                If IsDBNull(DATAIDEA.Rows(0)("Code")) = False Then
                    objProceeding_ReferenceTerms.Linea_Estrategica_t = DATAIDEA.Rows(0)("Code")
                End If
                'programa
                If IsDBNull(DATAIDEA.Rows(0)("programa")) = False Then
                    objProceeding_ReferenceTerms.Programa_t = DATAIDEA.Rows(0)("programa")
                End If
                'tipo de contraTO
                If IsDBNull(DATAIDEA.Rows(0)("contract")) = False Then
                    objProceeding_ReferenceTerms.modalidad_contratos_t = DATAIDEA.Rows(0)("contract")
                End If

            End If

            'datos dela tabla proyecto
            If DATAPROJECT.Rows.Count > 0 Then
                'nombre
                If IsDBNull(DATAPROJECT.Rows(0)("name")) = False Then
                    objProceeding_ReferenceTerms.name_idea_proyecto_t = DATAPROJECT.Rows(0)("name")
                End If
                'poblacion
                If IsDBNull(DATAPROJECT.Rows(0)("Population")) = False Then
                    Select Case DATAPROJECT.Rows(0)("Population")
                        Case 1
                            objProceeding_ReferenceTerms.people_benefactor_t = "Personas con Discapacidad"
                        Case 2
                            objProceeding_ReferenceTerms.people_benefactor_t = "Personas Mayores"
                        Case 3
                            objProceeding_ReferenceTerms.people_benefactor_t = "Personas con discapacidad y personas"
                        Case 4
                            objProceeding_ReferenceTerms.people_benefactor_t = "Otras"
                        Case Else
                            objProceeding_ReferenceTerms.people_benefactor_t = ""
                    End Select
                End If
                'duracion
                If IsDBNull(DATAPROJECT.Rows(0)("Duration")) = False Then
                    objProceeding_ReferenceTerms.duration_t = DATAPROJECT.Rows(0)("Duration")
                End If
                'fecha
                If IsDBNull(DATAPROJECT.Rows(0)("BeginDate")) = False Then
                    objProceeding_ReferenceTerms.date_inicial_t = DATAPROJECT.Rows(0)("BeginDate")
                End If
                'justificacion
                If IsDBNull(DATAPROJECT.Rows(0)("Justification")) = False Then
                    objProceeding_ReferenceTerms.justificacion_t = DATAPROJECT.Rows(0)("Justification")
                    If objProceeding_ReferenceTerms.justificacion_t = "" Then
                        objProceeding_ReferenceTerms.justificacion_t = "No aplica"
                    End If
                End If
                'objetivo
                If IsDBNull(DATAPROJECT.Rows(0)("Objective")) = False Then
                    objProceeding_ReferenceTerms.objetive_t = DATAPROJECT.Rows(0)("Objective")
                End If
                'objetivos especificos
                If IsDBNull(DATAPROJECT.Rows(0)("ZoneDescription")) = False Then
                    objProceeding_ReferenceTerms.objetive_esp_t = DATAPROJECT.Rows(0)("ZoneDescription")
                End If
                'resultados beneficiarios
                If IsDBNull(DATAPROJECT.Rows(0)("Results")) = False Then
                    objProceeding_ReferenceTerms.result_be_t = DATAPROJECT.Rows(0)("Results")
                    If objProceeding_ReferenceTerms.result_be_t = "" Then
                        objProceeding_ReferenceTerms.result_be_t = "No aplica"
                    End If
                End If
                ' valor contato
                If IsDBNull(DATAPROJECT.Rows(0)("TotalCost")) = False Then
                    Dim vt6 = DATAPROJECT.Rows(0)("TotalCost")
                    objProceeding_ReferenceTerms.values_total_t = Format(Convert.ToDecimal(vt6), "#,###.##")
                    If objProceeding_ReferenceTerms.values_total_t = "" Then
                        objProceeding_ReferenceTerms.values_total_t = 0
                    End If
                End If
                ' aporte socio
                If IsDBNull(DATAPROJECT.Rows(0)("CounterpartValue")) = False Then
                    Dim vtsocio = DATAPROJECT.Rows(0)("CounterpartValue")
                    objProceeding_ReferenceTerms.aport_socios_t = Format(Convert.ToDecimal(vtsocio), "#,###.##")
                    If objProceeding_ReferenceTerms.aport_socios_t = "" Then
                        objProceeding_ReferenceTerms.aport_socios_t = 0
                    End If
                End If
                ' aporte FSC
                If IsDBNull(DATAPROJECT.Rows(0)("FSCContribution")) = False Then
                    Dim vtfsc = DATAPROJECT.Rows(0)("FSCContribution")
                    objProceeding_ReferenceTerms.aport_FSC_t = Format(Convert.ToDecimal(vtfsc), "#,###.##")
                    If objProceeding_ReferenceTerms.aport_FSC_t = "" Then
                        objProceeding_ReferenceTerms.aport_FSC_t = 0
                    End If
                End If

                If IsDBNull(DATAPROJECT.Rows(0)("code")) = False Then
                    objProceeding_ReferenceTerms.code_idea_proyecto_t = DATAPROJECT.Rows(0)("code")
                End If

                If IsDBNull(DATAPROJECT.Rows(0)("ResultsKnowledgeManagement")) = False Then
                    objProceeding_ReferenceTerms.result_gest_t = DATAPROJECT.Rows(0)("ResultsKnowledgeManagement")
                    If objProceeding_ReferenceTerms.result_gest_t = "" Then
                        objProceeding_ReferenceTerms.result_gest_t = "No aplica"
                    End If
                End If

                If IsDBNull(DATAPROJECT.Rows(0)("ResultsInstalledCapacity")) = False Then
                    objProceeding_ReferenceTerms.capacidad_ins_t = DATAPROJECT.Rows(0)("ResultsInstalledCapacity")
                    If objProceeding_ReferenceTerms.capacidad_ins_t = "" Then
                        objProceeding_ReferenceTerms.capacidad_ins_t = "No aplica"
                    End If
                End If

            End If

            'datos de la tabla proyecto by localizacion
            If DATALOCATION.Rows.Count > 0 Then
                Dim depto As String = ""
                Dim temporal As String = ""
                Dim munip As String = ""
                Dim cont As Integer = 0

                For Each Item In DATALOCATION.Rows

                    ' captura departamento
                    If IsDBNull(DATALOCATION.Rows(cont)("deptoname")) = False Then
                        depto = DATALOCATION.Rows(cont)("deptoname")
                    End If
                    ' captura municipio
                    If IsDBNull(DATALOCATION.Rows(cont)("cityname")) = False Then
                        munip = DATALOCATION.Rows(cont)("cityname")
                    End If
                    'concatenamos los datos capturados
                    temporal = temporal & "//" & depto & "," & munip
                    depto = ""
                    munip = ""
                    cont = cont + 1
                Next
                objProceeding_ReferenceTerms.location_t = temporal
            End If

            'datos de actores
            If DATAACTORS.Rows.Count > 0 Then

                Dim nombre As String = ""
                Dim contacto As String = ""
                Dim type As String = ""
                Dim email As String = ""
                Dim tel As String = ""
                Dim documento As String = ""
                Dim cont2 As Integer = 0

                For Each Item In DATAACTORS.Rows

                    ' captura nombre
                    If IsDBNull(DATAACTORS.Rows(cont2)("name")) = False Then
                        nombre = DATAACTORS.Rows(cont2)("name")
                    End If
                    ' captura contacto
                    If IsDBNull(DATAACTORS.Rows(cont2)("contact")) = False Then
                        contacto = DATAACTORS.Rows(cont2)("contact")
                    End If
                    ' captura tipo
                    If IsDBNull(DATAACTORS.Rows(cont2)("type")) = False Then
                        type = DATAACTORS.Rows(cont2)("type")
                    End If
                    ' captura email
                    If IsDBNull(DATAACTORS.Rows(cont2)("email")) = False Then
                        email = DATAACTORS.Rows(cont2)("email")
                    End If
                    ' captura telefono
                    If IsDBNull(DATAACTORS.Rows(cont2)("phone")) = False Then
                        tel = DATAACTORS.Rows(cont2)("phone")
                    End If
                    ' captura documento
                    If IsDBNull(DATAACTORS.Rows(cont2)("documents")) = False Then
                        documento = DATAACTORS.Rows(cont2)("documents")
                    End If
                    'concatenamos los datos capturados
                    objProceeding_ReferenceTerms.actors_t &= "<tr><td style=""width: 16%;"">" & nombre & "</td><td style=""width: 16%;"">" & type & "</td><td style=""width: 16%;"">" & contacto & "</td><td style=""width: 16%;"">" & documento & "</td><td style=""width: 16%;"">" & tel & "</td><td style=""width: 16%;"">" & email & "</tr>"
                    cont2 = cont2 + 1
                Next

            End If

            Dim celdapago As Integer = 0
            Dim cont3 As Integer = 0
            If pagos.Rows.Count > 0 Then

                For Each pagoitem In pagos.Rows

                    If IsDBNull(pagos.Rows(celdapago)("valorparcial")) = False Then
                        objProceeding_ReferenceTerms.AdditionValue = pagos.Rows(celdapago)("valorparcial")
                    End If
                    If IsDBNull(pagos.Rows(celdapago)("porcentaje")) = False Then
                        objProceeding_ReferenceTerms.AdditionValueLetters = pagos.Rows(celdapago)("porcentaje")
                    End If
                    If IsDBNull(pagos.Rows(celdapago)("entregable")) = False Then
                        objProceeding_ReferenceTerms.Adition = pagos.Rows(celdapago)("entregable")
                    End If
                    If IsDBNull(pagos.Rows(celdapago)("fecha")) = False Then
                        objProceeding_ReferenceTerms.AdditionDate = pagos.Rows(celdapago)("fecha")
                    End If

                    cont3 = celdapago + 1

                    objProceeding_ReferenceTerms.flujos_t &= "<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong> Desembolso " & cont3 & "  </strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToDecimal(objProceeding_ReferenceTerms.AdditionValue), "#,###.##") & "</td><td style=""width: 5%;"">" & objProceeding_ReferenceTerms.AdditionValueLetters.ToString() & "</td><td style=""width: 16%;""></td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.Adition.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.AdditionDate.ToString() & "</td></tr><tr>"

                    celdapago = celdapago + 1

                Next

                sql = New StringBuilder

                sql.Append("select sum(valorparcial) from Paymentflow where idproject =" & V_id_proyecto)
                Dim valtpagos = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                objProceeding_ReferenceTerms.tflujos_t = "<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Total</strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToInt32(valtpagos), "#,###.##") & "</td><td style=""width: 5%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>100%</strong></span></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td></tr><tr>"
            End If

            Response.Redirect(objProceeding_ReferenceTerms.ExportReferenceTerms())


        Catch ex As Exception

            ''mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        End Try


    End Sub

    Protected Sub btnFinishContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinishContract.Click

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objProject As New ProjectEntity
        Dim sOldFile As String = String.Empty

        'Validacion de campos

        If Me.txtSubscriptionDate.Text = "" Then
            Me.lblInfoSubscriptionDate.ForeColor = Drawing.Color.Red
            Me.lblInfoSubscriptionDate.Text = "Debe diligenciar la fecha de Suscripción"
            Exit Sub
        Else
            Me.lblInfoSubscriptionDate.Text = ""
        End If

        If Me.txtInitialDate.Text = "" Then
            Me.lblinformation.ForeColor = Drawing.Color.Red
            Me.lblinformation.Text = "Debe diligenciar la fecha de Inicio"
            Exit Sub
        Else
            Me.lblinformation.Text = ""
        End If

        If Me.txtContractDuration.Text = "" Or Me.txtContractDuration.Text = 0 Then
            Me.lblNfoEndingdate.Visible = True
            Me.lblNfoEndingdate.ForeColor = Drawing.Color.Red
            Me.lblNfoEndingdate.Text = "Debe diligenciar la duración del contrato"
            Exit Sub
        Else
            Me.lblNfoEndingdate.Text = ""
        End If

        If chkSignedContract.Checked = False Then
            Me.lblNfoSigned.ForeColor = Drawing.Color.Red
            Me.lblNfoSigned.Text = "El contrato debe estar firmado para poder finalizar el proceso"
            Exit Sub
        Else
            Me.lblNfoSigned.Text = ""
        End If

        If Me.btnFinishContract.Text = "Finalizar proceso de contratación" Then
            Me.btnFinishContract.Text = "Confirmar"
            'Me.lblDelete.Text = "Revise la información ingresada, una vez presione el botón confirmar, esta NO podrá se modificada."
            'Me.lblDelete.Visible = True
        Else
            Me.btnFinishContract.Text = "Finalizar proceso de contratación"
            'Me.lblDelete.Text = ""
            'Proceso de actualizacion
            Try
                Call btnSave_Click(sender, e, "True")

            Catch ex As Exception

            End Try

            'Final


        End If

    End Sub


    Protected Sub btnAddSupervisor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSupervisor.Click

        'Definir la pestaña activa para el update
        Me.HFActivetab.Value = 1

        'validar que se haya realizado una selección
        If Me.ddlSupervisor.SelectedItem.Text = "Seleccione..." Then
            Me.lblAddSupervisor.ForeColor = Drawing.Color.Red
            Me.lblAddSupervisor.Text = "Por favor seleccione un Actor."
            Exit Sub
        End If

        'limpiar el mensaje de error
        Me.lblAddSupervisor.Text = ""



    End Sub
End Class
