Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization

Partial Class addSupplierEvaluation
    Inherits System.Web.UI.Page

#Region "Atributos"

    'Variables declaradas para recuperar el valor del porcentaje
    'de cada grupo de calificaciones
    Private porcentajeCumplimiento As Single = 0
    Private porcentajeOportunidad As Single = 0
    Private porcentajeCalidad As Single = 0
    Private porcentajeValorAgregado As Single = 0
    Private porcentajeMetodologia As Single = 0
    Private porcentajeCompetencia As Single = 0
    Private porcentajeConfidencialidad As Single = 0

    'Variables declaradas para recuperar el valor de la ponderacion
    'de cada grupo de calificaciones
    Private ponderacionCumplimiento As Double = 0
    Private ponderacionOportunidad As Double = 0
    Private ponderacionCalidad As Double = 0
    Private ponderacionValorAgregado As Double = 0
    Private ponderacionMetodologia As Double = 0
    Private ponderacionCompetencia As Double = 0
    Private ponderacionConfidencialidad As Double = 0

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

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA EVALUACIÓN DE PROVEEDOR."

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

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA EVALUACIÓN DE PROVEEDOR."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.txtid.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.txtcreatedate.Enabled = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objSupplierEvaluation As New SupplierEvaluationEntity

                    Try
                        ' cargar el registro referenciado
                        objSupplierEvaluation = facade.loadSupplierEvaluation(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objSupplierEvaluation.id
                        Me.ddlThird.SelectedValue = objSupplierEvaluation.idsupplier
                        Me.txtcontractnumber.Text = objSupplierEvaluation.contractnumber
                        Me.txtcontractstartdate.Text = objSupplierEvaluation.contractstartdate.ToString("yyyy/MM/dd")
                        Me.txtcontractenddate.Text = objSupplierEvaluation.contractenddate.ToString("yyyy/MM/dd")
                        Me.txtcontractsubject.Text = objSupplierEvaluation.contractsubject
                        Me.txtcontractvalue.Text = objSupplierEvaluation.contractvalue.ToString("#,###")
                        Me.txtiduser.Text = objSupplierEvaluation.USERNAME
                        Me.txtcreatedate.Text = objSupplierEvaluation.createdate

                        'Se carga la información de la calificación del proveedor evaluado
                        'Se agregan los datos de la calificacion del proveedor
                        With objSupplierEvaluation.SUPPLIERQUALIFICATION
                            Me.ddlContractSubject.SelectedValue = .contractsubject
                            Me.ddlContractualObligations.SelectedValue = .contractualobligations
                            Me.ddlDefinedGoals.SelectedValue = .definedgoals
                            Me.ddlAgreedDeadlines.SelectedValue = .agreeddeadlines
                            Me.ddlTotalityDeliveredProducts.SelectedValue = .totalitydeliveredproducts
                            Me.ddlRequestsMadeByFSC.SelectedValue = .requestsmadebyfsc
                            Me.ddlDeliveryProductsServices.SelectedValue = .deliveryproductsservices
                            Me.ddlReporting.SelectedValue = .reporting
                            Me.ddlProductQuality.SelectedValue = .productquality
                            Me.ddlReportsQuality.SelectedValue = .reportsquality
                            Me.ddlAccompanimentQuality.SelectedValue = .accompanimentquality
                            Me.ddlAttentionComplaintsClaims.SelectedValue = .attentioncomplaintsclaims
                            Me.ddlReturnedProducts.SelectedValue = .returnedproducts
                            Me.ddlProductValueAdded.SelectedValue = .productvalueadded
                            Me.ddlAccompanimentValueAdded.SelectedValue = .accompanimentvalueadded
                            Me.ddlReportsValueAdded.SelectedValue = .reportsvalueadded
                            Me.ddlProjectPlaneacion.SelectedValue = .projectplaneacion
                            Me.ddlMethodologyImplemented.SelectedValue = .methodologyimplemented
                            Me.ddlDevelopmentProjectOrganization.SelectedValue = .developmentprojectorganization
                            Me.ddlJointActivities.SelectedValue = .jointactivities
                            Me.ddlProjectControl.SelectedValue = .projectcontrol
                            Me.ddlServiceStaffCompetence.SelectedValue = .servicestaffcompetence
                            Me.ddlSupplierCompetence.SelectedValue = .suppliercompetence
                            Me.ddlInformationConfidentiality.SelectedValue = .informationconfidentiality
                            'Se recuperan los valores de los porcentajes
                            Me.txtPorcentajeCumplimiento.Text = .compliancepercentage.ToString()
                            Me.txtPorcentajeOportunidad.Text = .opportunitypercentage.ToString()
                            Me.txtPorcentajeCalidad.Text = .qualitypercentage.ToString()
                            Me.txtPorcentajeValorAgregado.Text = .addedvaluepercentage.ToString()
                            Me.txtPorcentajeMetodologia.Text = .methodologypercentage.ToString()
                            Me.txtPorcentajeCompetenciaPersonal.Text = .servicestaffcompetencepercentage.ToString()
                            Me.txtPorcentajeConfidencialidad.Text = .confidentialitypercentage.ToString()

                        End With

                        'Se llama al metodo que permite calcular los valores de resumen 
                        'de los diferentes grupos de calificaciones
                        Me.CalculateSumaryGrups()

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objSupplierEvaluation = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0

        Else
            'Se inicializa el control de mensajes de error
            Me.lblErrorMessage.Text = ""
        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objSupplierEvaluation As New SupplierEvaluationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objSupplierEvaluation.idsupplier = Me.ddlThird.SelectedValue
            objSupplierEvaluation.contractnumber = Me.txtcontractnumber.Text
            objSupplierEvaluation.contractstartdate = Me.txtcontractstartdate.Text
            objSupplierEvaluation.contractenddate = Me.txtcontractenddate.Text
            objSupplierEvaluation.contractsubject = Me.txtcontractsubject.Text
            If (Me.txtcontractvalue.Text.Length > 0) Then objSupplierEvaluation.contractvalue = Convert.ToDouble(Me.txtcontractvalue.Text, CultureInfo.CurrentCulture)
            objSupplierEvaluation.iduser = applicationCredentials.UserID
            objSupplierEvaluation.createdate = Now()

            'Se agregan los datos de la calificacion del proveedor
            With objSupplierEvaluation.SUPPLIERQUALIFICATION
                .contractsubject = Me.ddlContractSubject.SelectedValue
                .contractualobligations = Me.ddlContractualObligations.SelectedValue
                .definedgoals = Me.ddlDefinedGoals.SelectedValue
                .agreeddeadlines = Me.ddlAgreedDeadlines.SelectedValue
                .totalitydeliveredproducts = Me.ddlTotalityDeliveredProducts.SelectedValue
                .requestsmadebyfsc = Me.ddlRequestsMadeByFSC.SelectedValue
                .deliveryproductsservices = Me.ddlDeliveryProductsServices.SelectedValue
                .reporting = Me.ddlReporting.SelectedValue
                .productquality = Me.ddlProductQuality.SelectedValue
                .reportsquality = Me.ddlReportsQuality.SelectedValue
                .accompanimentquality = Me.ddlAccompanimentQuality.SelectedValue
                .attentioncomplaintsclaims = Me.ddlAttentionComplaintsClaims.SelectedValue
                .returnedproducts = Me.ddlReturnedProducts.SelectedValue
                .productvalueadded = Me.ddlProductValueAdded.SelectedValue
                .accompanimentvalueadded = Me.ddlAccompanimentValueAdded.SelectedValue
                .reportsvalueadded = Me.ddlReportsValueAdded.SelectedValue
                .projectplaneacion = Me.ddlProjectPlaneacion.SelectedValue
                .methodologyimplemented = Me.ddlMethodologyImplemented.SelectedValue
                .developmentprojectorganization = Me.ddlDevelopmentProjectOrganization.SelectedValue
                .jointactivities = Me.ddlJointActivities.SelectedValue
                .projectcontrol = Me.ddlProjectControl.SelectedValue
                .servicestaffcompetence = Me.ddlServiceStaffCompetence.SelectedValue
                .suppliercompetence = Me.ddlSupplierCompetence.SelectedValue
                .informationconfidentiality = Me.ddlInformationConfidentiality.SelectedValue
                'Se almacenan los valores de los porcentajes
                If (Me.txtPorcentajeCumplimiento.Text.Length > 0) Then .compliancepercentage = Convert.ToDouble(Me.txtPorcentajeCumplimiento.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeOportunidad.Text.Length > 0) Then .opportunitypercentage = Convert.ToDouble(Me.txtPorcentajeOportunidad.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeCalidad.Text.Length > 0) Then .qualitypercentage = Convert.ToDouble(Me.txtPorcentajeCalidad.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeValorAgregado.Text.Length > 0) Then .addedvaluepercentage = Convert.ToDouble(Me.txtPorcentajeValorAgregado.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeMetodologia.Text.Length > 0) Then .methodologypercentage = Convert.ToDouble(Me.txtPorcentajeMetodologia.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeCompetenciaPersonal.Text.Length > 0) Then .servicestaffcompetencepercentage = Convert.ToDouble(Me.txtPorcentajeCompetenciaPersonal.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeConfidencialidad.Text.Length > 0) Then .confidentialitypercentage = Convert.ToDouble(Me.txtPorcentajeConfidencialidad.Text, CultureInfo.CurrentCulture)
            End With

            ' almacenar la entidad
            objSupplierEvaluation.id = facade.addSupplierEvaluation(applicationCredentials, objSupplierEvaluation)

            ' ir al administrador
            Response.Redirect("searchSupplierEvaluation.aspx")

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
            objSupplierEvaluation = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchSupplierEvaluation.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objSupplierEvaluation As New SupplierEvaluationEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' cargar el registro referenciado
        objSupplierEvaluation = facade.loadSupplierEvaluation(applicationCredentials, Request.QueryString("id"))

        Try
            ' cargar los datos
            objSupplierEvaluation.idsupplier = Me.ddlThird.SelectedValue
            objSupplierEvaluation.contractnumber = Me.txtcontractnumber.Text
            objSupplierEvaluation.contractstartdate = Me.txtcontractstartdate.Text
            objSupplierEvaluation.contractenddate = Me.txtcontractenddate.Text
            objSupplierEvaluation.contractsubject = Me.txtcontractsubject.Text
            If (Me.txtcontractvalue.Text.Length > 0) Then objSupplierEvaluation.contractvalue = Convert.ToDouble(Me.txtcontractvalue.Text, CultureInfo.CurrentCulture)

            'Se agregan los datos de la calificacion del proveedor
            With objSupplierEvaluation.SUPPLIERQUALIFICATION
                .contractsubject = Me.ddlContractSubject.SelectedValue
                .contractualobligations = Me.ddlContractualObligations.SelectedValue
                .definedgoals = Me.ddlDefinedGoals.SelectedValue
                .agreeddeadlines = Me.ddlAgreedDeadlines.SelectedValue
                .totalitydeliveredproducts = Me.ddlTotalityDeliveredProducts.SelectedValue
                .requestsmadebyfsc = Me.ddlRequestsMadeByFSC.SelectedValue
                .deliveryproductsservices = Me.ddlDeliveryProductsServices.SelectedValue
                .reporting = Me.ddlReporting.SelectedValue
                .productquality = Me.ddlProductQuality.SelectedValue
                .reportsquality = Me.ddlReportsQuality.SelectedValue
                .accompanimentquality = Me.ddlAccompanimentQuality.SelectedValue
                .attentioncomplaintsclaims = Me.ddlAttentionComplaintsClaims.SelectedValue
                .returnedproducts = Me.ddlReturnedProducts.SelectedValue
                .productvalueadded = Me.ddlProductValueAdded.SelectedValue
                .accompanimentvalueadded = Me.ddlAccompanimentValueAdded.SelectedValue
                .reportsvalueadded = Me.ddlReportsValueAdded.SelectedValue
                .projectplaneacion = Me.ddlProjectPlaneacion.SelectedValue
                .methodologyimplemented = Me.ddlMethodologyImplemented.SelectedValue
                .developmentprojectorganization = Me.ddlDevelopmentProjectOrganization.SelectedValue
                .jointactivities = Me.ddlJointActivities.SelectedValue
                .projectcontrol = Me.ddlProjectControl.SelectedValue
                .servicestaffcompetence = Me.ddlServiceStaffCompetence.SelectedValue
                .suppliercompetence = Me.ddlSupplierCompetence.SelectedValue
                .informationconfidentiality = Me.ddlInformationConfidentiality.SelectedValue
                'Se almacenan los valores de los porcentajes
                If (Me.txtPorcentajeCumplimiento.Text.Length > 0) Then .compliancepercentage = Convert.ToDouble(Me.txtPorcentajeCumplimiento.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeOportunidad.Text.Length > 0) Then .opportunitypercentage = Convert.ToDouble(Me.txtPorcentajeOportunidad.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeCalidad.Text.Length > 0) Then .qualitypercentage = Convert.ToDouble(Me.txtPorcentajeCalidad.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeValorAgregado.Text.Length > 0) Then .addedvaluepercentage = Convert.ToDouble(Me.txtPorcentajeValorAgregado.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeMetodologia.Text.Length > 0) Then .methodologypercentage = Convert.ToDouble(Me.txtPorcentajeMetodologia.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeCompetenciaPersonal.Text.Length > 0) Then .servicestaffcompetencepercentage = Convert.ToDouble(Me.txtPorcentajeCompetenciaPersonal.Text, CultureInfo.CurrentCulture)
                If (Me.txtPorcentajeConfidencialidad.Text.Length > 0) Then .confidentialitypercentage = Convert.ToDouble(Me.txtPorcentajeConfidencialidad.Text, CultureInfo.CurrentCulture)

            End With

            ' modificar el registro
            facade.updateSupplierEvaluation(applicationCredentials, objSupplierEvaluation)

            ' ir al administrador
            Response.Redirect("searchSupplierEvaluation.aspx")

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
            objSupplierEvaluation = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteSupplierEvaluation(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchSupplierEvaluation.aspx")

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

    Protected Sub bntCalQualification_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntCalQualification.Click

        'Se llama al metodo que permite calcular los valores de resumen 
        'de los diferentes grupos de calificaciones
        Me.CalculateSumaryGrups()

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

            'Se pobla el combo de terceros
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlThird.DataSource = facade.getThirdList(applicationCredentials, enabled:="1", order:="Code")
            Else
                Me.ddlThird.DataSource = facade.getThirdList(applicationCredentials, order:="Code")
            End If
            Me.ddlThird.DataValueField = "Id"
            Me.ddlThird.DataTextField = "Code"
            Me.ddlThird.DataBind()

            If Not (Request.QueryString("IdThird") Is Nothing) Then
                ddlThird.SelectedValue = Request.QueryString("IdThird")
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

    ''' <summary>
    ''' Este metodo permite calcular los valores de resumen para cada uno de los grupos e calificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSumaryGrups()

        Try
            'Se llama al metodo que permite calcular los resultados del grupo Cumplimiento
            Me.CalculateSummaryCompliance()

            'Se llama al metodo que permite calcular los resultados del grupo Oportunidad
            Me.CalculateSummaryOpportunity()

            'Se llama al metodo que permite calcular los resultados del grupo Calidad
            Me.CalculateSummaryQuality()

            'Se llama al metodo que permite calcular los resultados del grupo Valor agregado
            Me.CalculateSummaryValueAdded()

            'Se llama al metodo que permite calcular los resultados del grupo Metodologia
            Me.CalculateSummaryMethodology()

            'Se llama al metodo que permite calcular los resultados del grupo Competencia personal prestador del servicio
            Me.CalculateSummaryServiceStaffCompetence()

            'Se llama al metodo que permite calcular los resultados del grupo Confidencialidad
            Me.CalculateSummaryConfidentiality()

            'Se llama al metodo que calcula los resultados finales
            Me.CalculteSummaryFinal()

        Catch ex As Exception
            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()
        End Try

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Cumplimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryCompliance()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlContractSubject.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlContractSubject.SelectedValue)
        End If

        If Not (Me.ddlContractualObligations.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlContractualObligations.SelectedValue)
        End If

        If Not (Me.ddlDefinedGoals.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlDefinedGoals.SelectedValue)
        End If

        If Not (Me.ddlAgreedDeadlines.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlAgreedDeadlines.SelectedValue)
        End If

        If Not (Me.ddlTotalityDeliveredProducts.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlTotalityDeliveredProducts.SelectedValue)
        End If

        If Not (Me.ddlRequestsMadeByFSC.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlRequestsMadeByFSC.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeCumplimiento.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionCumplimiento.Text = miCalificacion.ToString()
            Me.porcentajeCumplimiento = (CDbl(Me.txtPorcentajeCumplimiento.Text) / 100)
            Me.ponderacionCumplimiento = (miCalificacion * Me.porcentajeCumplimiento)
            Me.ponderacionCumplimiento = Math.Round(Me.ponderacionCumplimiento, 2)
            Me.lblPonderacionCumplimiento.Text = Me.ponderacionCumplimiento.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Oportunidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryOpportunity()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlDeliveryProductsServices.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlDeliveryProductsServices.SelectedValue)
        End If

        If Not (Me.ddlReporting.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlReporting.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeOportunidad.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionOportunidad.Text = miCalificacion.ToString()
            Me.porcentajeOportunidad = (CDbl(Me.txtPorcentajeOportunidad.Text) / 100)
            Me.ponderacionOportunidad = (miCalificacion * Me.porcentajeOportunidad)
            Me.ponderacionOportunidad = Math.Round(Me.ponderacionOportunidad, 2)
            Me.lblPonderacionOportunidad.Text = Me.ponderacionOportunidad.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Calidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryQuality()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlProductQuality.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlProductQuality.SelectedValue)
        End If

        If Not (Me.ddlReportsQuality.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlReportsQuality.SelectedValue)
        End If

        If Not (Me.ddlAccompanimentQuality.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlAccompanimentQuality.SelectedValue)
        End If

        If Not (Me.ddlAttentionComplaintsClaims.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlAttentionComplaintsClaims.SelectedValue)
        End If

        If Not (Me.ddlReturnedProducts.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlReturnedProducts.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeCalidad.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionCalidad.Text = miCalificacion.ToString()
            Me.porcentajeCalidad = (CDbl(Me.txtPorcentajeCalidad.Text) / 100)
            Me.ponderacionCalidad = (miCalificacion * Me.porcentajeCalidad)
            Me.ponderacionCalidad = Math.Round(Me.ponderacionCalidad, 2)
            Me.lblPonderacionCalidad.Text = Me.ponderacionCalidad.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Valor agregado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryValueAdded()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlProductValueAdded.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlProductValueAdded.SelectedValue)
        End If

        If Not (Me.ddlAccompanimentValueAdded.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlAccompanimentValueAdded.SelectedValue)
        End If

        If Not (Me.ddlReportsValueAdded.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlReportsValueAdded.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeValorAgregado.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionValorAgregado.Text = miCalificacion.ToString()
            Me.porcentajeValorAgregado = (CDbl(Me.txtPorcentajeValorAgregado.Text) / 100)
            Me.ponderacionValorAgregado = (miCalificacion * Me.porcentajeValorAgregado)
            Me.ponderacionValorAgregado = Math.Round(Me.ponderacionValorAgregado, 2)
            Me.lblPonderacionValorAgregado.Text = Me.ponderacionValorAgregado.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Metodologia
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryMethodology()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlProjectPlaneacion.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlProjectPlaneacion.SelectedValue)
        End If

        If Not (Me.ddlMethodologyImplemented.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlMethodologyImplemented.SelectedValue)
        End If

        If Not (Me.ddlDevelopmentProjectOrganization.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlDevelopmentProjectOrganization.SelectedValue)
        End If

        If Not (Me.ddlJointActivities.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlJointActivities.SelectedValue)
        End If

        If Not (Me.ddlProjectControl.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlProjectControl.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeMetodologia.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionMetodologia.Text = miCalificacion.ToString()
            Me.porcentajeMetodologia = (CDbl(Me.txtPorcentajeMetodologia.Text) / 100)
            Me.ponderacionMetodologia = (miCalificacion * Me.porcentajeMetodologia)
            Me.ponderacionMetodologia = Math.Round(Me.ponderacionMetodologia, 2)
            Me.lblPonderacionMetodologia.Text = Me.ponderacionMetodologia.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Competencia personal prestador del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryServiceStaffCompetence()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlServiceStaffCompetence.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlServiceStaffCompetence.SelectedValue)
        End If

        If Not (Me.ddlSupplierCompetence.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlSupplierCompetence.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeCompetenciaPersonal.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionCompetenciaPersonal.Text = miCalificacion.ToString()
            Me.porcentajeCompetencia = (CDbl(Me.txtPorcentajeCompetenciaPersonal.Text) / 100)
            Me.ponderacionCompetencia = (miCalificacion * Me.porcentajeCompetencia)
            Me.ponderacionCompetencia = Math.Round(Me.ponderacionCompetencia, 2)
            Me.lblPonderacionCompetenciaPersonal.Text = Me.ponderacionCompetencia.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Calcula los valores de resumen para el grupo de Confidencialidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateSummaryConfidentiality()
        Dim miContadorItems As Integer = 0
        Dim miTotalPuntaje As Integer = 0
        Dim miCalificacion As Double = 0

        'Se recuperan los valores de los combos del grupo actual
        If Not (Me.ddlInformationConfidentiality.SelectedValue.Equals("0")) Then
            miContadorItems += 1
            miTotalPuntaje += CDbl(Me.ddlInformationConfidentiality.SelectedValue)
        End If

        'Se calculan el resto de totales siempre y cuando el total del puntaje sea mayor a cero
        If (miTotalPuntaje > 0) Then
            Me.lblTotalPuntajeConfidencialidad.Text = miTotalPuntaje.ToString()
            miCalificacion = (miTotalPuntaje / miContadorItems)
            miCalificacion = Math.Round(miCalificacion, 2)
            Me.lblCalificacionConfidencialidad.Text = miCalificacion.ToString()
            Me.porcentajeConfidencialidad = (CDbl(Me.txtPorcentajeConfidencialidad.Text) / 100)
            Me.ponderacionConfidencialidad = (miCalificacion * Me.porcentajeConfidencialidad)
            Me.ponderacionConfidencialidad = Math.Round(Me.ponderacionConfidencialidad, 2)
            Me.lblPonderacionConfidencialidad.Text = Me.ponderacionConfidencialidad.ToString()
        End If

    End Sub

    ''' <summary>
    ''' Permite calcular los totales finales de la calificación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculteSummaryFinal()
        Dim miValorPorcentajeTotal As Double = 0
        Dim miValorCalificacionTotal As Double = 0
        Dim miValorEnLetras As String = ""

        'Se calcula el valor del porcentaje final
        miValorPorcentajeTotal = (Me.porcentajeCumplimiento + Me.porcentajeOportunidad + Me.porcentajeCalidad _
        + Me.porcentajeValorAgregado + Me.porcentajeMetodologia + Me.porcentajeCompetencia + Me.porcentajeConfidencialidad)
        miValorPorcentajeTotal = Math.Round(miValorPorcentajeTotal, 2)

        'Se valida que el valor del porcentaje total este entre los rangos permitidos
        If (miValorPorcentajeTotal > 1D) Then
            Me.lblErrorMessage.Text = "La suma total de los porcentajes no puede dar un valor superior a 100 (cien). Por favor verifique."
        Else
            'Se almacenan los valores de las ponderaciones de cada grupo de calificaciones
            Me.lblTotalPorcentajeCumplimiento.Text = Me.ponderacionCumplimiento.ToString()
            Me.lblTotalPorcentajeOprtunidad.Text = Me.ponderacionOportunidad.ToString()
            Me.lblTotalPorcentajeCalidad.Text = Me.ponderacionCalidad.ToString()
            Me.lblTotalPorcentajeValorAgregado.Text = Me.ponderacionValorAgregado.ToString()
            Me.lblTotalPorcentajeMetodologia.Text = Me.ponderacionMetodologia.ToString()
            Me.lblTotalPorcentajeCompetenciaPrestadorServicio.Text = Me.ponderacionCompetencia.ToString()
            Me.lblTotalPorcentajeConfidencialidad.Text = Me.ponderacionConfidencialidad.ToString()

            'Se calcula el valor de la calificación total
            miValorCalificacionTotal = (miValorPorcentajeTotal + Me.ponderacionCumplimiento + Me.ponderacionOportunidad _
            + Me.ponderacionCalidad + Me.ponderacionValorAgregado + Me.ponderacionMetodologia + Me.ponderacionCompetencia _
            + Me.ponderacionConfidencialidad)
            miValorCalificacionTotal = Math.Round(miValorCalificacionTotal, 2)
            Me.lblCalificacionTotal.Text = miValorCalificacionTotal.ToString()

            'Se calcula el valor de la calificación  en letras
            If (miValorCalificacionTotal < 2.9D) Then
                Me.lblCalificacionLetras.Text = "DEFICIENTE"
            ElseIf (miValorCalificacionTotal >= 3D AndAlso miValorCalificacionTotal <= 3.9D) Then
                Me.lblCalificacionLetras.Text = "ACEPTABLE"
            ElseIf (miValorCalificacionTotal >= 4D AndAlso miValorCalificacionTotal <= 4.7D) Then
                Me.lblCalificacionLetras.Text = "BUENO"
            ElseIf (miValorCalificacionTotal >= 4.8D AndAlso miValorCalificacionTotal <= 5D) Then
                Me.lblCalificacionLetras.Text = "EXCELENTE"
            End If
        End If

    End Sub

#End Region

End Class
