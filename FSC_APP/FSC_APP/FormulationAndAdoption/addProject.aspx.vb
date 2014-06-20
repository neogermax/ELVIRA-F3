Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.HttpFileCollection
Imports System.Web.HttpPostedFile
Imports PostMail
Imports FSC_APP.PostMail


Partial Class addProject
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve una lista de objetos de tipo DocumentsEntity
    ''' </summary>
    ''' <value>una lista de objetos de tipo DocumentsEntity</value>
    ''' <returns>una lista de objetos de tipo DocumentsEntity</returns>
    ''' <remarks></remarks>
    Private Property DocumentsList() As List(Of DocumentsEntity)
        Get
            Return DirectCast(Session("documentsList"), List(Of DocumentsEntity))
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Session("documentsList") = value
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

            'por defecto los checkbox estan desactivados
            Me.checkvalor.Visible = False
            Me.checktiempo.Visible = False
            Me.checkalcance.Visible = False
            Me.lblmodifotrosi.Visible = False
            '    Me.lblmsjporcent.Text = ""
            Me.lblMessageValidacionNombre.Text = ""
            ''Me.btnRefresh.Visible = False
            'Me.lblFlowNfo.Text = "."
            'Me.lblFlowNfo.ForeColor = Drawing.Color.White

            If Me.HDswich_ubicacion.Value = "" Then
                Me.HDswich_ubicacion.Value = 0
            End If

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim consultLastVersion As Boolean = True
            If Not (Request.QueryString("consultLastVersion") Is Nothing) Then consultLastVersion = Request.QueryString("consultLastVersion")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim objProgramComponent As New ProgramComponentEntity
            Dim objProgram As New ProgramEntity
            Dim Item As New ListItem
            Dim defaultDate As New DateTime
            Dim facade As New Facade

            'obtener codigo del grupo
            Dim codigogrupo As String = Session("mMenu")
            codigogrupo = codigogrupo.Replace("_", "")
            Dim codeUser As DataTable = getCodeGroup(codigogrupo, applicationCredentials)
            codigogrupo = codeUser(0)(0).ToString()

            'Cargar los combos
            Select Case op
                Case "export"
                    Export_Project()

                Case "add"
                    loadCombos()
            End Select

            ' Asignar lista de fuentes de proyecto a variable de sesión
            Session("sourceByProjectList") = New List(Of SourceByProjectEntity)
            ' Asignar lista de ubicaciones de proyecto a variable de sesión
            Session("projectLocationList") = New List(Of ProjectLocationEntity)
            ' Asignar lista de terceros de proyecto a variable de sesión
            Session("thirdByProjectList") = New List(Of ThirdByProjectEntity)
            ' Asignar lista de operadores de proyecto a variable de sesión
            Session("operatorByProjectList") = New List(Of ThirdByProjectEntity)
            'se crea la variable de session que tiene la lista de los flujos de pago para un proyecto
            Session("paymentFlowList") = New List(Of PaymentFlowEntity)
            'variable de sesion lista de ubicaciones
            Session("ideaLocationList") = New List(Of LocationByIdeaEntity)
            'variable de session e los aclaratorios
            Session("explanatoryList") = New List(Of ExplanatoryEntity)
            'variable de session de los documentos temporales
            Session("DocumentsTmp") = New List(Of DocumentstmpEntity)
            Session("typeapprovalbegin") = 0
            Session("totporcentajes") = 0


            ' de acuerdo a la opcion
            Select Case op

                Case "add"
                    'Me.txtvalortotalflow.Enabled = True
                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO PROYECTO DERIVADO."

                    ddltipoaprobacion.Items.Add(New ListItem("No aprobado", "4"))
                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = False
                    'Me.btnDelete.Visible = False
                    ''Me.btnCancelDelete.Visible = False
                    'Me.btnConfirmDelete.Visible = False
                    'Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    Me.rfvid.Visible = False
                    'Me.linkcharge.Visible = False
                    '  'Me.btnRefresh.Visible = False
                    Me.txtcounterpartvalue.Text = "0"
                    'Me.tbpnAclaratorio.Visible = False
                    ''Me.txtstartdate.ReadOnly = True
                    ''Me.upoperatorbyproject.Visible = False
                    'validar para no mostrar terminos de referencia al inicio
                    Me.btntermsreference.Visible = False
                    Dim objProject As New ProjectEntity
                    Try
                        ' cargar y asignar la lista de ubicaciones del proyecto


                    Catch ex As Exception

                    End Try

                Case "edit", "show"

                    'Me.TabPanelCompProgramas.Visible = False
                    Me.btnSave.Visible = False
                    If (Request.QueryString("successSave") <> Nothing) Then
                        If Session("modificar") = 1 Then
                            '           lblstatesuccess.Text = "El proyecto se modificó exitosamente!"
                        End If
                        If Session("modificar") = 2 Then
                            '         lblstatesuccess.Text = "El proyecto se aprobó exitosamente!"
                        End If

                        '   lblstatesuccess.Visible = True
                        containerSuccess.Visible = True
                        Me.btnSave.Visible = False
                        'btnSave.Visible = False
                    End If

                    For i = 2012 To 2030
                        ddleffectivebudget.Items.Add(i.ToString)
                    Next
                    ' ocultar algunos botones
                    'Me.btnDelete.Visible = False
                    'Me.ddlididea.Visible = False
                    Me.lblididea.Visible = False
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.txtcode.Visible = True
                    Me.lblcode.Visible = True
                    Me.txtenabled.Visible = False

                    Me.lblenabled.Visible = False

                    Me.txtcode.ReadOnly = True
                    Me.btntermsreference.Visible = False


                    'deshabilitar campos de valores por defecto en la edicion
                    Me.txtfsccontribution.ReadOnly = True
                    Me.txtcounterpartvalue.ReadOnly = True

                    Me.ddleffectivebudget.Enabled = False

                    If (Request.QueryString("successSave") <> Nothing) Then
                        Me.btnSave.Visible = False
                    End If

                    ' definir los objetos
                    Dim objProject As New ProjectEntity
                    Dim objPaymentFlow As New PaymentFlowEntity
                    Dim objExplanatoryEntity As New ExplanatoryEntity
                    Dim objDt As New DataTable()


                    Try

                        ' cargar el registro referenciado
                        objProject = facade.loadProject(applicationCredentials, Request.QueryString("id"), consultLastVersion)
                        Session("typeapprovalbegin") = objProject.Typeapproval
                        '     Me.lblcomponentesprograma.InnerHtml = searchComponentsProgram(objProject.ididea, applicationCredentials)
                        ' mostrar los valores
                        objDt = getLinStratbusqueda(objProject.id, applicationCredentials)

                        If objDt.Rows.Count > 0 Then
                        End If
                        ' CONSULTAR SI EXISTE CUNA CONTRATACION  PARA ESTE PROYECTO
                        Dim haveContract As DataTable = getRecruitment(objProject.id, applicationCredentials)

                        Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                        ' combo para todos los tipos de aprobacion para lider
                        If codigogrupo = "Lider" Then

                            If ((objProject.Typeapproval <> 1) And (objProject.Typeapproval <> 2)) Then
                                '      ddltipoaprobacion.Items.Add(New ListItem("No aprobado", "4"))
                            End If
                            'SI TIPO APROBACION ES CONTRATO O ACLARATORIO U OTRO SI
                            If (objProject.Typeapproval = 1) Or (objProject.Typeapproval = 2) Or (objProject.Typeapproval = 3) Then

                                'ddltipoaprobacion.Items.Add(New ListItem("Contrato", "1"))
                                'ddltipoaprobacion.Items.Add(New ListItem("Otro si", "2"))
                                'ddltipoaprobacion.Items.Add(New ListItem("Aclaratorio", "3"))
                                'Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                            End If

                        End If
                        'si el grupo es lider:
                        If codigogrupo = "Seg y Eval" Then

                            'If objProject.Typeapproval = 1 Then
                            '    '      ddltipoaprobacion.Items.Add(New ListItem("Contrato", "1"))
                            '    Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                            'End If
                            'If objProject.Typeapproval = 2 Then
                            '    ddltipoaprobacion.Items.Add(New ListItem("Otro si", "2"))
                            '    Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                            'End If
                            'If objProject.Typeapproval = 3 Then
                            '    ddltipoaprobacion.Items.Add(New ListItem("Aclaratorio", "3"))
                            '    Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                            'End If
                            'If objProject.Typeapproval = 4 Then
                            '    ddltipoaprobacion.Items.Add(New ListItem("Contrato", "1"))
                            'End If
                            '      Me.txtaclaratorio.ReadOnly = True
                        End If

                        'If codigogrupo = "ADMIN" Then
                        '    ddltipoaprobacion.Items.Add(New ListItem("No aprobado", "4"))
                        '    ddltipoaprobacion.Items.Add(New ListItem("contrato", "1"))
                        '    ddltipoaprobacion.Items.Add(New ListItem("Otro si", "2"))
                        '    ddltipoaprobacion.Items.Add(New ListItem("Aclaratorio", "3"))
                        '    Me.ddltipoaprobacion.SelectedValue = objProject.Typeapproval
                        'End If

                        Dim duration As Double


                        Me.txtid.Text = objProject.id
                        'Me.ddlididea.SelectedValue = objProject.ididea
                        'Me.ddlididea.Enabled = False
                        Me.txtcode.Text = objProject.code
                        Session("idproject") = objProject.id
                        Me.txtname.Text = objProject.name
                        Session("nameproject") = objProject.id
                        Me.txtobjective.Text = objProject.objective
                        Me.txtantecedent.Text = objProject.antecedent
                        Me.txtjustification.Text = objProject.justification
                        Me.txtresulgc.Text = objProject.ResultsKnowledgeManagement
                        Me.txtresulci.Text = objProject.ResultsInstalledCapacity
                        Me.txtstartdate.text = objProject.begindate
                        Me.txtduration.Text = objProject.duration

                        Me.Txtothersresults.Text = objProject.OthersResults
                        Me.Txtobligationsoftheparties.Text = objProject.Obligaciones
                        Me.Txtroutepresupuestal.Text = objProject.presupuestal
                        Me.Txtriesgos.Text = objProject.riesgos
                        Me.Txtaccionmitig.Text = objProject.mitigacion
                        Me.Txtday.Text = objProject.dia
                        Me.RBnList_iva.SelectedValue = objProject.iva
                        Me.HDiva.value = objProject.iva

                        Me.txtareadescription.Text = objProject.zonedescription
                        ' Me.ddlpopulation.SelectedValue = objProject.population
                        Me.txtstrategicdescription.Text = objProject.strategicdescription
                        Me.txtresults.Text = objProject.results
                        Me.txtpurpose.Text = objProject.purpose
                        Me.txttotalcost.Text = objProject.totalcost.ToString("#,###")
                        Me.txtfsccontribution.Text = objProject.fsccontribution.ToString("#,###")

                        'If objProject.counterpartvalue.ToString("#,###") <> "" Then
                        '    Me.txtcounterpartvalue.Text = objProject.counterpartvalue.ToString("#,###")
                        'Else
                        '    Me.txtcounterpartvalue.Text = "0"
                        'End If
                        'If objProject.Typeapproval = 3 Then
                        '    '       tbpnAclaratorio.Visible = True
                        'End If


                        'Me.ddleffectivebudget.SelectedValue = objProject.effectivebudget
                        'Me.ddlidphase.SelectedValue = objProject.idphase
                        Me.ddlidphase.Enabled = False
                        'Me.ddlenabled.SelectedValue = objProject.enabled
                        Me.txtiduser.Text = objProject.USERNAME
                        Me.txtcreatedate.Text = objProject.createdate
                        Me.txttotalcost.Text = objProject.totalcost

                        'validar se tipo aprobado es contrato
                        'If objProject.Typeapproval = 1 Then
                        '    'Me.tbpnAclaratorio.Visible = False


                        'End If
                        ' If objProject.Typeapproval = 2 Then
                        'Me.tbpnAclaratorio.Visible = False

                        'Me.checkvalor.Visible = True
                        'Me.checktiempo.Visible = True
                        'Me.checkalcance.Visible = True
                        'Me.lblmodifotrosi.Visible = True

                        'If objProject.editablemoney = "S" Then

                        '    Me.checkvalor.Checked = True
                        '    Me.txtcounterpartvalue.ReadOnly = False
                        '    Me.txtfsccontribution.ReadOnly = False
                        'Else
                        '    Me.checkvalor.Checked = False
                        'End If
                        ''VALIDA SI EL GRUPO ES SEGUIMIENTO DEJA NO EDITABLE
                        ' LOS CHECKS DE VALOR , TIEMPO Y ALCANCE
                        'If codigogrupo = "Seg y Eval" Then
                        '    Me.checkvalor.Enabled = False
                        '    Me.checktiempo.Enabled = False
                        '    Me.checkalcance.Enabled = False

                        'End If

                        'If objProject.editabletime = "S" Then
                        '    Me.checktiempo.Checked = True
                        '    Me.txtduration.ReadOnly = False
                        '    'Me.txtstartdate.ReadOnly = False
                        'Else
                        '    Me.checktiempo.Checked = False
                        'End If

                        'If objProject.editableresults = "S" Then
                        '    Me.checkalcance.Checked = True
                        '    Me.txtresults.ReadOnly = False
                        '    Me.txtobjective.ReadOnly = False
                        '    Me.txtobjective.Enabled = True
                        '    Me.txtjustification.ReadOnly = False
                        '    Me.txtareadescription.Enabled = True
                        '    Me.txtareadescription.ReadOnly = False

                        '    'Me.TextResultGestConocimiento.ReadOnly = False
                        '    'Me.TextResCapacidInstal.ReadOnly = False
                        'Else
                        '    Me.checkalcance.Checked = False
                        'End If
                        'Me.txtfechapago.Enabled = False
                        'Me.txtporcentaje.Enabled = False
                        'Me.txtvalorpartial.Enabled = False
                        'Me.txtentregable.Enabled = False
                        ''Me.BtnAddPayment.Enabled = False
                        'ddlidoperator.Enabled = False
                        'Me.ddltipooperador.Enabled = False
                        ' Me.btnaddoperatorbyproject.Enabled = False
                        'Me.btnadanexo.Enabled = False
                        '  Me.btnAddProjectLocation.Enabled = False
                        ' End If

                        'If objProject.Typeapproval = 3 Or objProject.Typeapproval = 1 Then

                        'Me.txtresults.ReadOnly = True
                        'Me.txtobjective.ReadOnly = True
                        'Me.txtjustification.ReadOnly = True
                        '' Me.txtzonedescription.ReadOnly = True
                        'Me.TextResultGestConocimiento.ReadOnly = True
                        'Me.TextResCapacidInstal.ReadOnly = True
                        'Me.txtduration.ReadOnly = True
                        ''Me.txtstartdate.ReadOnly = True
                        'Me.txtstartdate.Enabled = False
                        'Me.txtcounterpartvalue.ReadOnly = True
                        'Me.txtfsccontribution.ReadOnly = True
                        'Me.txtfechapago.Enabled = False
                        'Me.txtporcentaje.Enabled = False
                        'Me.txtvalorpartial.Enabled = False
                        'Me.txtentregable.Enabled = False
                        ''Me.BtnAddPayment.Enabled = False
                        'Me.ddlidoperator.Enabled = False
                        'Me.ddltipooperador.Enabled = False
                        'Me.btnaddoperatorbyproject.Enabled = False
                        'Me.btnadanexo.Enabled = False
                        '.Enabled = False
                        ' Me.ddlpopulation.Enabled = False
                        ' Me.ddliddepto.Enabled = False
                        ' Me.ddlidcity.Enabled = False
                        ' Me.gvprojectLocation.Enabled = False
                        'Me.gvPaymentFlow.Enabled = False
                        'Me.gvoperatorbyproject.Enabled = False
                        'Me.gvDocuments.Enabled = False

                        'End If

                        'If objProject.Typeapproval = 2 Then
                        '    If objProject.editablemoney = "S" Then
                        '        Me.txtfechapago.Enabled = True
                        '        Me.txtporcentaje.Enabled = True
                        '        Me.txtvalorpartial.Enabled = True
                        '        Me.txtentregable.Enabled = True
                        '        'Me.BtnAddPayment.Enabled = True
                        '        'Me.gvPaymentFlow.Enabled = True
                        '    End If
                        'End If
                        '' si es un proyecto no aprobado
                        'If objProject.Typeapproval = 4 Then
                        '    'TODO: OJO HABILITACION DEL MODULO ELIMINAR EN PROYECTO
                        '    ''Me.btnDelete.Visible = True
                        '    'Me.tbpnAclaratorio.Visible = False

                        '    Me.ddleffectivebudget.Enabled = True
                        '    Me.ddlidphase.Enabled = True
                        '    ' carga combo de actores 
                        '    'Me.ddlidoperator.DataSource = facade.getThirdList(applicationCredentials, order:="code")
                        '    'Me.ddlidoperator.Items.Insert(0, New ListItem("Seleccione..", "-1"))
                        '    'Me.ddlidoperator.DataValueField = "Id"
                        '    'Me.ddlidoperator.DataTextField = "Name"
                        '    'Me.ddlidoperator.DataBind()
                        '    'Me.ddlidoperator.Items.Insert(0, New ListItem("Seleccione..", "-1"))
                        '    Me.txtname.ReadOnly = False
                        '    Me.txtresults.ReadOnly = False
                        '    Me.txtobjective.ReadOnly = False
                        '    Me.txtjustification.ReadOnly = False
                        '    'Me.txtzonedescription.ReadOnly = False
                        '    'Me.TextResultGestConocimiento.ReadOnly = False
                        '    'Me.TextResCapacidInstal.ReadOnly = False
                        '    Me.txtduration.ReadOnly = False
                        '    ''Me.txtstartdate.ReadOnly = True

                        '    duration = Convert.ToDouble(Me.txtduration.Text.Replace(".", ","))
                        '    '  Me.TextFinalizacion.Text = getDateFinalization(duration, Me.txtstartdate.Text)
                        '    Me.txtcounterpartvalue.ReadOnly = False
                        '    Me.txtfsccontribution.ReadOnly = False
                        'End If
                        'SI ES LIDER Y OTRO SI
                        'If objProject.Typeapproval = 2 And codigogrupo = "Lider" Then
                        '    Me.txtresults.ReadOnly = True
                        '    Me.txtobjective.ReadOnly = True
                        '    Me.txtjustification.ReadOnly = True
                        '    'Me.txtzonedescription.ReadOnly = True
                        '    'Me.TextResultGestConocimiento.ReadOnly = True
                        '    'Me.TextResCapacidInstal.ReadOnly = True
                        '    Me.txtduration.ReadOnly = True
                        '    'Me.txtstartdate.Enabled = False
                        '    duration = Convert.ToDouble(Me.txtduration.Text.Replace(".", ","))
                        '    ' Me.TextFinalizacion.Text = getDateFinalization(duration, 'Me.txtstartdate.Text)
                        '    Me.txtcounterpartvalue.ReadOnly = True
                        '    Me.txtfsccontribution.ReadOnly = True
                        'End If
                        'SI ES ADMIN ACTIVA TODO
                        'If codigogrupo = "ADMIN" Then
                        '    Me.txtduration.ReadOnly = False
                        '    Me.txtfsccontribution.ReadOnly = False
                        '    Me.txtcounterpartvalue.ReadOnly = False
                        '    Me.ddleffectivebudget.Enabled = True
                        '    Me.txtfechapago.Enabled = True
                        '    Me.txtporcentaje.Enabled = True
                        '    Me.txtvalorpartial.Enabled = True
                        '    Me.txtentregable.Enabled = True
                        '    'Me.BtnAddPayment.Enabled = True
                        '    'Me.gvPaymentFlow.Enabled = True


                        '    'Me.ddlidoperator.Enabled = True
                        '    'Me.ddltipooperador.Enabled = True
                        '    'Me.btnaddoperatorbyproject.Enabled = True
                        '    'Me.btnadanexo.Enabled = True
                        '    ''Me.btnAddProjectLocation.Enabled = True
                        '    'Me.ddlidoperator.DataSource = facade.getThirdList(applicationCredentials, order:="code")
                        '    'Me.ddlidoperator.Items.Insert(0, New ListItem("Seleccione..", "-1"))
                        '    'Me.ddlidoperator.DataValueField = "Id"
                        '    'Me.ddlidoperator.DataTextField = "Name"
                        '    'Me.ddlidoperator.DataBind()
                        '    'Me.ddlidoperator.Items.Insert(0, New ListItem("Seleccione..", "-1"))
                        'End If
                        'SI EL GRUPO ES JURIDICA
                        'If codigogrupo = "Juridica" Then
                        '    If objProject.Typeapproval = 4 Then
                        '        ddltipoaprobacion.Items.Add(New ListItem("No aprobado", "4"))
                        '    End If
                        '    If objProject.Typeapproval = 1 Then
                        '        ddltipoaprobacion.Items.Add(New ListItem("contrato", "1"))
                        '    End If
                        '    If objProject.Typeapproval = 2 Then
                        '        ddltipoaprobacion.Items.Add(New ListItem("Otro si", "2"))
                        '    End If
                        '    If objProject.Typeapproval = 3 Then
                        '        ddltipoaprobacion.Items.Add(New ListItem("Aclaratorio", "3"))
                        '    End If
                        '    Me.txtjustification.ReadOnly = True
                        '    Me.txtobjective.ReadOnly = True
                        '    'Me.txtzonedescription.ReadOnly = True
                        '    Me.txtresults.ReadOnly = True
                        '    'Me.TextResultGestConocimiento.ReadOnly = True
                        '    'Me.TextResCapacidInstal.ReadOnly = True
                        '    'Me.txtstartdate.Enabled = False
                        '    Me.txtduration.ReadOnly = True
                        '    'Me.ddlpopulation.Enabled = False
                        '    Me.txtfsccontribution.ReadOnly = True
                        '    Me.txtcounterpartvalue.ReadOnly = True

                        '    Me.checkvalor.Enabled = False
                        '    Me.checktiempo.Enabled = False
                        '    Me.checkalcance.Enabled = False

                        '    Me.btnSave.Visible = False
                        'End If

                        'Me.ddlidoperator.DataSource = facade.getThirdList(applicationCredentials, order:="code")
                        duration = Convert.ToDouble(Me.txtduration.Text.Replace(".", ","))
                        '  Me.TextFinalizacion.Text = getDateFinalization(duration, 'Me.txtstartdate.Text)
                        ' cargar y asignar la lista de fuentes del proyecto
                        ' Me.gvSourceByProject.DataSource = objProject.sourceByProjectList
                        ' Me.gvSourceByProject.DataBind()
                        '                        Session("sourceByProjectList") = objProject.sourceByProjectList

                        ' cargar y asignar la lista de ubicaciones del proyecto

                        'gvprojectLocation.DataSource = objProject.projectlocationlist
                        'gvprojectLocation.DataBind()
                        ' Session("projectLocationList") = objProject.projectlocationlist

                        'cargar la lista de las aclaratorios
                        'objProject.explanatoryEntityList = getExplanatoryByProject(objProject.id, applicationCredentials)
                        'GridViewAclaratorio.DataSource = objProject.explanatoryEntityList
                        'GridViewAclaratorio.DataBind()
                        'Session("explanatoryList") = objProject.explanatoryEntityList


                        ' cargar y asignar la lista de id actores del proyecto, operador 
                        'Session("operatorByProjectList") = objProject.thirdbyprojectlist

                        'Dim objDataTable As DataTable = New DataTable()

                        'objDataTable.Columns.Add("idthird")
                        'objDataTable.Columns.Add("name")
                        'objDataTable.Columns.Add("type")
                        'objDataTable.Columns.Add("contact")
                        'objDataTable.Columns.Add("documents")
                        'objDataTable.Columns.Add("phone")
                        'objDataTable.Columns.Add("email")
                        '' objDataTable.Columns.Add("Vrmoney")
                        '' objDataTable.Columns.Add("VrSpecies")
                        ''objDataTable.Columns.Add("FSCorCounterpartContribution")


                        'For Each itemDataTable As ThirdByProjectEntity In Session("operatorByProjectList")
                        '    objDataTable.Rows.Add(itemDataTable.idthird, itemDataTable.THIRDNAME, itemDataTable.type, itemDataTable.THIRD.contact, itemDataTable.THIRD.documents, itemDataTable.THIRD.phone, itemDataTable.THIRD.email)
                        'Next

                        'gvoperatorbyproject.DataSource = objDataTable
                        'gvoperatorbyproject.DataBind()

                        'objProject.thirdbyprojectlist = getThirdByProject(objProject.id, applicationCredentials)
                        'gvoperatorbyproject.DataSource = getThirdBySession(objProject.id, applicationCredentials)
                        'gvoperatorbyproject.DataBind()
                        ' Session("operatorByProjectList") = objProject.thirdbyprojectlist

                        'cargar la lista de laos flujos de pago
                        'objProject.paymentflowByProjectList = getFlowPayment(objProject.id, applicationCredentials)
                        'gvPaymentFlow.DataSource = objProject.paymentflowByProjectList
                        'gvPaymentFlow.DataBind()
                        Session("paymentFlowList") = objProject.paymentflowByProjectList
                        Dim valortotal As Decimal
                        'For Each pf As PaymentFlowEntity In objProject.paymentflowByProjectList
                        '    valortotal = valortotal + pf.valortotal

                        'Next
                        'Session("valortotalflow") = valortotal
                        'Dim totporcentajes As Double
                        'For Each pf As PaymentFlowEntity In objProject.paymentflowByProjectList
                        '    totporcentajes = totporcentajes + pf.porcentaje
                        'Next
                        'Session("totporcentajes") = totporcentajes
                        ' Me.lblmensajeexitoerror.Text = Session("totporcentajes")

                        'If Session("totporcentajes") < 100 Then
                        '    Me.txtvalortotalflow.Enabled = True

                        'Else
                        '    Me.txtvalortotalflow.Enabled = False
                        'End If

                        'Me.txtvalortotalflow.Text = valortotal
                        'If Session("valortotalflow") <> Nothing Then
                        '    Me.txtvalortotalflow.Text = Session("valortotalflow")
                        'End If
                        '' If (objProject.DOCUMENTLIST Is Nothing OrElse objProject.DOCUMENTLIST.Count = 0) Then 'Me.btnRefresh.Visible = False

                        'Se carga la lista de documentos adjuntos
                        'Se almacena la lista en una variable de sesion.
                        'Me.DocumentsList = objProject.DOCUMENTLIST
                        'Me.gvDocuments.DataSource = objProject.DOCUMENTLIST
                        'Me.gvDocuments.DataBind()

                        'Session("valortotalflow") = txtvalortotalflow.Text




                        ' cargar la lista de Componentes del Programa del proyecto

                        If Not (objProject.ProgramComponentbyprojectlist Is Nothing) Then
                            If (objProject.ProgramComponentbyprojectlist.Count > 0) Then
                                objProgramComponent = facade.loadProgramComponent(applicationCredentials, idProgramComponent:=objProject.ProgramComponentbyprojectlist.First.idProgramComponent)
                                objProgram = facade.loadProgram(applicationCredentials, objProgramComponent.idProgram)
                                'ddlidStrategicLine.SelectedValue = objProgram.idStrategicLine
                                'ddlidStrategicLine_SelectedIndexChanged(sender, e)
                                'ddlidProgram.SelectedValue = objProgram.id
                                'ddlidStrategicLine_SelectedIndexChanged(sender, e)
                                For Each objProgramComponentByProject As ProgramComponentByProjectEntity In objProject.ProgramComponentbyprojectlist
                                    'Item = Me.dlbProgramComponentByProject.AviableItems.Items.FindByValue(objProgramComponentByProject.idProgramComponent)
                                    'Me.dlbProgramComponentByProject.AviableItems.Items.Remove(Item)
                                    'If Not (Item Is Nothing) Then
                                    '    Me.dlbProgramComponentByProject.SelectedItems.Items.Add(Item)
                                    'End If
                                    'Me.dlbProgramComponentByProject.SelectedItems.Items.Add(New ListItem(objProgramComponentByProject.CODE, objProgramComponentByProject.idProgramComponent))
                                Next
                            End If

                        End If
                        ' cargar y habilitar el archivo anexo
                        Me.hlattachment.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objProject.attachment
                        Me.hlattachment.Text = objProject.attachment
                        Me.hlattachment.Visible = False

                        ' guardar
                        ViewState("idKey") = objProject.idKey

                        If op.Equals("show") Then

                            ' ocultar algunos botones
                            Me.btnSave.Visible = False
                            'Me.btnDelete.Visible = False
                            'Me.btnCancel.Visible = False

                            ' limpiar label
                            Me.lblVersion.Text = ""

                        Else

                            'Cargar las versiones anteriores
                            '   loadVersions(objProject.idKey)

                        End If

                        Dim idProcessInstance As String = String.Empty
                        Dim idActivityInstance As String = String.Empty

                        ' cargar los valores del BPM
                        idProcessInstance = Request.QueryString("idProcessInstance")
                        idActivityInstance = Request.QueryString("idActivityInstance")

                        ' verificar si viene desde el BPM
                        If idProcessInstance IsNot Nothing Then

                            'Me.lblBPMMessage.Visible = True
                            'Me.rblCondition.Visible = True
                            ''Me.btnDelete.Visible = False
                            ''Me.btnCancel.Visible = False

                            ' cargar la lista de condiciones de la actividad
                            Dim conditions As Array = GattacaApplication.getConditions(applicationCredentials, idActivityInstance)

                            For Each condition As ListItem In conditions

                                ' cargar la lista de condiciones para la actividad
                                '      Me.rblCondition.Items.Add(New ListItem(condition.Text, condition.Value))

                            Next

                            ' seleccionar el primero
                            'Me.rblCondition.SelectedIndex = 0

                            ' cargar el mensaje
                            ' Me.lblBPMMessage.Text = PublicFunction.getSettingValue("BPM.Condition.Message")

                        End If

                        If op.Equals("show") Then

                            ' cargar el titulo
                            Session("lblTitle") = "MOSTRAR INFORMACION DEL PROYECTO DERIVADO."

                            ' ocultar los botones para realizar modificaciones
                            Me.btnSave.Visible = False
                            'Me.btnDelete.Visible = False
                            'Me.btnCancel.Visible = False

                        Else
                            ' cargar el titulo
                            Session("lblTitle") = "MODIFICAR PROYECTO DERIVADO."

                            'Rutina agregada por Jose Olmes Torres - Julio 22 de 2010
                            'Se verifica si el identificador de la fase del proyecto es la fase de cerrado
                            Dim idClosedState As String = ConfigurationManager.AppSettings("IdClosedState")
                            If (objProject.idphase.ToString() = idClosedState) Then
                                'Se oculta el botón grabar y el botón eliminar
                                'Me.btnSave.Visible = False
                                'Me.btnDelete.Visible = False
                            End If
                        End If

                        'Verificar titulo aprobacion
                        If (Request.QueryString("apr") <> Nothing) Then
                            Session("lblTitle") = "APROBAR PROYECTO DERIVADO."
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
                        objProject = Nothing
                        objProgramComponent = Nothing
                        objProgram = Nothing
                        Item = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            'Me.TabContainer1.ActiveTabIndex = 0

        End If

    End Sub

    'Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocuments.RowDataBound

    '    Dim objHyperlink As HyperLink
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim miEntidad As DocumentsEntity = e.Row.DataItem
    '        objHyperlink = e.Row.Cells(6).Controls(0)
    '        If (miEntidad.attachfile.Length > 0) Then
    '            objHyperlink.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "/" & miEntidad.attachfile
    '            objHyperlink.Target = "_blank"
    '        End If
    '    End If
    'End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos

        Dim facade As New Facade
        Dim objProject As New ProjectEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByProjectList As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)
        If Me.HDswich_ubicacion.Value = "" Or Me.HDswich_ubicacion.Value = 0 Then
            Me.HDswich_ubicacion.Value = 0
        End If

        'Post-verificación de código
        'If Not verifyCode() Then
        'Return
        'End If

        Try
            'If lblmensajeexitoerror.Text = "100" And Me.txtname.Text.Trim().Length > 0 Then
            '    ' cargar los valores registrados por el usuario

            If validarcamposnum() = 1 Then
                Exit Sub
            Else
                '    Me.lblHelpfsccontribution.Text = ""
                '   Me.lblHelpcounterpartvalue.Text = ""
            End If

            '     objProject.ididea = Me.ddlididea.SelectedValue
            objProject.code = Me.txtcode.Text
            objProject.name = Me.txtname.Text
            objProject.objective = Me.txtobjective.Text
            objProject.enabled = 1
            objProject.antecedent = Me.txtantecedent.Text
            objProject.justification = Me.txtjustification.Text
            'objProject.zonedescription = Me.txtzonedescription.Text
            objProject.results = Me.txtresults.Text
            ' objProject.ResultsKnowledgeManagement = Me.TextResultGestConocimiento.Text
            'objProject.ResultsInstalledCapacity = Me.TextResCapacidInstal.Text
            'objProject.begindate = IIf(('Me.txtstartdate.Text = ""), Nothing, 'Me.txtstartdate.Text)
            objProject.duration = Me.txtduration.Text
            'objProject.population = Me.ddlpopulation.SelectedValue
            objProject.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            objProject.effectivebudget = Me.ddleffectivebudget.SelectedValue.ToString
            'el dato no se guarda en tabla idea
            objProject.strategicdescription = Me.txtstrategicdescription.Text
            objProject.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text)
            objProject.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
            'Subir el archivo
            'objProject.attachment = PublicFunction.LoadFile(Request)
            objProject.Typeapproval = Me.ddltipoaprobacion.SelectedValue
            objProject.idphase = Me.ddlidphase.SelectedValue



            '    objProject.purpose = Me.txtpurpose.Text
            '    objProject.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text.Replace(".", ""))
            '    objProject.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text.Replace(".", ""))

            '    'objProject.enabled = Me.ddlenabled.SelectedValue
            objProject.iduser = applicationCredentials.UserID

            '    objProject.createdate = Now
            '    objProject.editablemoney = "N"
            '    objProject.editabletime = "N"
            '    objProject.editableresults = "N"

            '    'Cargar la lista de fuentes por proyecto
            'objProject.sourceByProjectList = Session("sourceByProjectList")

            '    'Cargar la lista de ubicaciones ProjectLocation
            objProject.projectlocationlist = Session("projectLocationList")

            Dim objDataTable As DataTable = Session("projectLocationList")
            Dim objListProyectLocations As List(Of ProjectLocationEntity) = New List(Of ProjectLocationEntity)
            'For Each rowDataTable In objDataTable.Rows

            '        Dim objProjectLocation As ProjectLocationEntity = New ProjectLocationEntity()
            '        ' TODO: cambio de forma de xcaptura de variable por el grid
            '        'autor: german rodriguez 25/08/2013

            '        'objProjectLocation.idcity = Convert.ToInt32(rowDataTable(0))
            '        objProjectLocation.idcity = rowDataTable(0)
            '        objProjectLocation.DEPTONAME = rowDataTable(1)
            '        objProjectLocation.CITYNAME = rowDataTable(2)
            '        objListProyectLocations.Add(objProjectLocation)
            '    Next

            '    objProject.projectlocationlist = objListProyectLocations

            '    'Cargar la lista de terceros ThirdByProject

            '    Dim idea_id As Integer = Convert.ToInt32(Me.ddlididea.SelectedValue)

            'objProject.thirdbyprojectlist = getThirdByIdea(idea_id, applicationCredentials)

            '    objProject.ProgramComponentbyprojectlist = ProgramComponentByProjectList

            '    objProject.paymentflowByProjectList = Session("paymentFlowList")

            '    'almacenar en BD 1 aclaratorio
            '    '1° declaramos lista de objetos exploratoryEntity y se instancia
            '    'Dim objListExplanatory As List(Of ExplanatoryEntity) = New List(Of ExplanatoryEntity)

            '    'Dim objExplanatoryEntity As ExplanatoryEntity = New ExplanatoryEntity
            '    'objExplanatoryEntity.observation = Me.txtaclaratorio.Text
            '    'objExplanatoryEntity.fecha = Date.Now()
            '    'objListExplanatory.Add(objExplanatoryEntity)
            '    'objProject.explanatoryEntityList = objListExplanatory

            '    'Se agrega la lista de documentos cargados en el servidor
            '    Me.LoadFilesBySession(objProject, applicationCredentials.UserID)
            '    'GUARDAR FECHA DE FINALIZACION
            '    ' objProject.completiondate = TextFinalizacion.Text


            '    'almacenar la entidad
            '    objProject.id = facade.addProject(applicationCredentials, objProject)

            '    'agregar los componentes de la idea al proyecto
            '    Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
            '    Dim dt_componentsIdea As DataTable = queryComponentsProgram(idea_id, applicationCredentials)
            '    Dim objListComponentesProject As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)
            '    For Each rowDataTable In dt_componentsIdea.Rows

            '        Dim objProgramComponentByProjectEntity As ProgramComponentByProjectEntity = New ProgramComponentByProjectEntity()

            '        objProgramComponentByProjectEntity.idproject = objProject.id
            '        objProgramComponentByProjectEntity.idProgramComponent = rowDataTable(1)
            '        objProgramComponentByProjectDALC.add(applicationCredentials, objProgramComponentByProjectEntity)
            '        'objListComponentesProject.Add(objProgramComponentByProjectEntity)
            '    Next

            '    'objProject.ProgramComponentbyprojectlist = objListComponentesProject
            '    'objProgramComponentByProjectDALC.add()

            '    ' ir al administrador
            '    Session("modificar") = 0
            '    Response.Redirect("addProject.aspx?successSave=1&op=edit&id=" & objProject.id)
            'Else

            '    'If lblmensajeexitoerror.Text <> "100" Then
            '    '    'Me.lblmsjporcent.Text = "El total de pago debe ser igual al 100%"
            '    '    ' Me.TabContainer1.ActiveTabIndex = 0
            '    '    'Me.lblsaveinformation.Text = "El total de pago debe ser igual al 100%"
            '    '    Me.lblMessageValidacionNombre.Text = "El total de pago debe ser igual al 100%"
            '    '    'Me.Label16.Text = "El total de pago debe ser igual al 100%"
            '    '    '          Me.LabelErrorGeneral.Text = "El total de pago debe ser igual al 100%"

            '    'End If
            '    If Me.txtname.Text.Trim().Length = 0 Then
            '        Me.lblMessageValidacionNombre.Text = "El campo nombre esta vacio "
            '        Me.lblsaveinformation.Text = "El campo nombre esta vacio "
            '        Me.lblMessageValidacionNombre.Text = "El campo nombre esta vacio "
            '        ''Me.Label16.Text = "El campo nombre esta vacio "
            '        '     Me.LabelErrorGeneral.Text = "El campo nombre esta vacio "
            '    End If
            'End If
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
            objProject = Nothing
            facade = Nothing
            ProgramComponentByProjectList = Nothing

        End Try

    End Sub

    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    '    ' ir al administrador
    '    Response.Redirect("searchProject.aspx")

    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objProject As New ProjectEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByProjectList As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty
        Dim sOldFile As String = String.Empty
        Dim objExplanatoryDALC As New ExplanatoryDALC
        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")

        ''Post-verificación de código
        'If Not verifyCode() Then
        '    Return
        'End If

        'cargar el registro referenciado
        objProject = facade.loadProject(applicationCredentials, Request.QueryString("Id"))
        Dim objPaymentFlowList As New List(Of PaymentFlowEntity)()

        'Validar Alerta de Contrato
        Dim valoralerta As String = "0"
        If objProject.Typeapproval = 4 And Me.ddltipoaprobacion.SelectedValue = 1 Then
            valoralerta = "1"
        End If

        objPaymentFlowList = Session("paymentFlowList")
        Dim objPF As PaymentFlowEntity
        objPF = New PaymentFlowEntity
        Dim totPayments As Double
        For Each objPF In objPaymentFlowList

            totPayments = totPayments + Convert.ToDouble(objPF.porcentaje)
        Next
        Try
            If validarcamposnum() = 1 Then
                Exit Sub
            Else
                Me.lblHelpfsccontribution.Text = ""
                Me.lblHelpcounterpartvalue.Text = ""
            End If


            'If lblmensajeexitoerror.Text = "100" Or totPayments = 100 Then
            '    objProject.code = Me.txtcode.Text
            '    objProject.name = Me.txtname.Text
            '    objProject.objective = Me.txtobjective.Text
            '    objProject.antecedent = Me.txtantecedent.Text
            '    objProject.justification = Me.txtjustification.Text
            '    ' objProject.begindate = IIf(('Me.txtstartdate.Text = ""), Nothing, 'Me.txtstartdate.Text)
            '    objProject.duration = Me.txtduration.Text
            '    objProject.zonedescription = Me.txtzonedescription.Text
            '    ' objProject.population = Me.ddlpopulation.SelectedValue
            '    objProject.strategicdescription = Me.txtstrategicdescription.Text
            '    objProject.results = Me.txtresults.Text
            '    objProject.purpose = Me.txtpurpose.Text
            '    objProject.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            '    objProject.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
            '    objProject.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text)
            '    objProject.effectivebudget = Me.ddleffectivebudget.SelectedValue
            '    objProject.idphase = Me.ddlidphase.SelectedValue
            '    'objProject.enabled = Me.ddlenabled.SelectedValue
            '    objProject.iduser = applicationCredentials.UserID
            '    objProject.createdate = Now
            '    objProject.ResultsKnowledgeManagement = Me.TextResultGestConocimiento.Text
            '    objProject.ResultsInstalledCapacity = Me.TextResCapacidInstal.Text
            '    'objProject.begindate = IIf(('Me.txtstartdate.Text = ""), Nothing, 'Me.txtstartdate.Text)
            '    objProject.duration = Me.txtduration.Text


            '    'editar tipo de aprobacion
            '    objProject.Typeapproval = ddltipoaprobacion.SelectedValue
            '    'guardar valor
            '    objProject.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text)
            '    'Cargar el archivo

            '    'objProject.attachment = PublicFunction.LoadFile(Request)

            '    'Cargar la lista de fuentes por proyecto
            '    objProject.sourceByProjectList = Session("sourceByProjectList")


            '    'Cargar la lista de ubicaciones ProjectLocation
            '    Dim a = Session("projectLocationList").GetType


            '    If Not a.IsGenericType Then
            '        Dim objDataTable As DataTable = Session("projectLocationList")
            '        Session.Add("projectLocationList", objDataTable)
            '        Dim objListProyectLocations As List(Of ProjectLocationEntity) = New List(Of ProjectLocationEntity)
            '        For Each rowDataTable In objDataTable.Rows

            '            Dim objProjectLocation As ProjectLocationEntity = New ProjectLocationEntity()

            '            objProjectLocation.idcity = rowDataTable(0)
            '            objProjectLocation.DEPTONAME = rowDataTable(1)
            '            objProjectLocation.CITYNAME = rowDataTable(2)
            '            objListProyectLocations.Add(objProjectLocation)
            '        Next

            '        objProject.projectlocationlist = objListProyectLocations
            '    End If

            '    objProject.paymentflowByProjectList = Session("paymentFlowList")

            '    objProject.thirdbyprojectlist = DirectCast(Session("operatorByProjectList"), List(Of ThirdByProjectEntity))

            '    objProject.ProgramComponentbyprojectlist = ProgramComponentByProjectList
            '    sOldFile = hlattachment.Text
            '    ' si no se modifico el archivo
            '    If objProject.attachment.Equals(String.Empty) Then

            '        'cargar el anterior
            '        objProject.attachment = Me.hlattachment.Text

            '    End If

            '    ' guardar valores de  campos para poder editar o no valor, tiempo y results
            '    If checkvalor.Checked Then
            '        objProject.editablemoney = "S"
            '    Else
            '        objProject.editablemoney = "N"
            '    End If
            '    If checktiempo.Checked Then
            '        objProject.editabletime = "S"
            '    Else
            '        objProject.editabletime = "N"
            '    End If
            '    If checkalcance.Checked Then
            '        objProject.editableresults = "S"
            '    Else
            '        objProject.editableresults = "N"
            '    End If
            '    'CARGA FECHA DE FINALIZACION EN EL OBJETO DEL PROYECTO
            '    Dim duration As Double = Convert.ToDouble(Me.txtduration.Text.Replace(".", ","))
            '    ' objProject.completiondate = getDateFinalization(duration, 'Me.txtstartdate.Text)
            '    ' TODO: Arreglar

            '    'Validar Alerta de Contrato
            '    'Dim valoralerta As String = "0"
            '    'If objProject.Typeapproval = 4 And Me.ddltipoaprobacion.SelectedValue = 1 Then
            '    '    valoralerta = "1"
            '    'End If

            '    ' modificar el registro si el proyecto no esta aprobado
            '    If objProject.Typeapproval = 4 Or Me.ddltipoaprobacion.SelectedValue = "1" Or objProject.Typeapproval = 2 Then
            '        Me.LoadFilesBySession(objProject, applicationCredentials.UserID)
            '        facade.updateProject(applicationCredentials, objProject, sOldFile)

            '    Else

            '        'MODIFICAR CAMBIOS SEGUN ULTIMOS REQUERIMIENTOS
            '        Me.LoadFilesBySession(objProject, applicationCredentials.UserID)
            '        facade.updateProjectLastRequirements(applicationCredentials, objProject, sOldFile)

            '    End If
            '    'almacenar en BD 1 aclaratorio

            '    'Enviar Notificación
            '    If valoralerta = 1 Then
            '        Dim correo As PostMail_SndMail = New PostMail_SndMail()
            '        Dim asunto As String
            '        Dim mensajecorreo As String
            '        Dim destinatarios As String = ""

            '        Dim sql As New StringBuilder
            '        Dim data As DataTable
            '        Dim primero As Integer

            '        'consultar juridica y el usuario admin(Jose)
            '        sql.Append("select user_id from usersbymailgroup where usersbymailgroup.mailgroup = 1 or usersbymailgroup.mailgroup = 3")
            '        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            '        If data.Rows.Count > 0 Then

            '            For Each itemdatatable As DataRow In data.Rows
            '                primero = primero + 1

            '                If primero = 1 Then
            '                    destinatarios = itemdatatable("user_id")
            '                Else
            '                    destinatarios = destinatarios & " or id=" & itemdatatable("user_id")
            '                End If

            '            Next

            '        End If

            '        'reiniciar las variables
            '        sql = New StringBuilder
            '        primero = 0

            '        'consultar el id del lider del proyecto
            '        sql.Append("select iduser from project where project.id = " & objProject.id)
            '        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            '        If data.Rows.Count > 0 Then
            '            destinatarios = destinatarios & " or id=" & data(0)("iduser")
            '        End If

            '        'reiniciar las variables
            '        sql = New StringBuilder
            '        primero = 0

            '        'consultar los emails
            '        sql.Append("use FSC_eSecurity select email from ApplicationUser where id =" & destinatarios)
            '        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            '        If data.Rows.Count > 0 Then

            '            For Each itemDataTable As DataRow In data.Rows

            '                primero = primero + 1

            '                If primero = 1 Then
            '                    destinatarios = itemDataTable("email")
            '                Else
            '                    destinatarios = destinatarios & "," & itemDataTable("email")
            '                End If

            '            Next

            '        End If

            '        asunto = "Se aprobó al proyecto " & objProject.id & " - " & objProject.name

            '        mensajecorreo = "Hola,"
            '        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "El proyecto " & objProject.id & " - " & objProject.name & " fué aprobado; a partir de este momento, puede iniciar el proceso de contratación."
            '        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "Cordialmente,"
            '        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "ELVIRA"
            '        mensajecorreo = mensajecorreo & Chr(13) & "EvaLuación y Valoración de la InveRsión Articulada"
            '        mensajecorreo = mensajecorreo & Chr(13) & "Fundación Saldarriaga Concha"

            '        correo.SendMail(destinatarios, asunto, mensajecorreo)
            '    End If

            '    'se crea objeto Explanatory donde se guardan los datos
            '    'If Me.txtaclaratorio.Text <> "" Then
            '    '    Dim objExplanatoryEntity As ExplanatoryEntity = New ExplanatoryEntity
            '    '    objExplanatoryEntity.observation = Me.txtaclaratorio.Text
            '    '    objExplanatoryEntity.fecha = Date.Now()
            '    '    objExplanatoryEntity.idproject = objProject.id
            '    '    objExplanatoryDALC.add(applicationCredentials, objExplanatoryEntity)
            '    'End If

            '    If idProcessInstance IsNot Nothing Then

            '        ' finalizar la actividad actual
            '        GattacaApplication.endActivityInstance(applicationCredentials, idProcessInstance, idActivityInstance, _
            '                                               Me.rblCondition.SelectedValue, "Se ha modificado el proyecto", _
            '                                               "", "", "", "")
            '        ' cerrar la ventana
            '        ' ir a la pagina de lista de tareas
            '        Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

            '    Else
            '        'Me.lblsaveinformation.Text = "Se ha guardado proyecto satisfactoriamente"
            '        ' ir al administrador
            '        'Response.Redirect("searchProject.aspx")




            '    End If
            '    'Me.lblstatesuccess.Text = "El proyecto se edito correctamente"

            '    If Me.ddltipoaprobacion.Text = "1" And Session("typeapprovalbegin") = 4 Then
            '        'mensaje de que se aprobo
            '        Session("modificar") = 2
            '    Else
            '        'mensaje exito que se modifico
            '        Session("modificar") = 1
            '    End If
            '    Response.Redirect("addProject.aspx?successSave=1&op=edit&id=" & objProject.id)



            'Else

            '    'If lblmensajeexitoerror.Text <> "100" Then
            '    '    'Me.lblmsjporcent.Text = "El total de pago debe ser igual al 100%"
            '    '    '.ActiveTabIndex = 0
            '    '    'Me.lblsaveinformation.Text = "El total de pago debe ser igual al 100%"
            '    '    Me.lblMessageValidacionNombre.Text = "El total de pago debe ser igual al 100%"
            '    '    'Me.Label16.Text = "El total de pago debe ser igual al 100%"
            '    '    '     Me.LabelErrorGeneral.Text = "El total de pago debe ser igual al 100%"

            '    'End If
            '    If Me.txtname.Text.Trim().Length = 0 Then
            '        Me.lblMessageValidacionNombre.Text = "El campo nombre esta vacio "
            '        Me.lblsaveinformation.Text = "El campo nombre esta vacio "
            '        Me.lblMessageValidacionNombre.Text = "El campo nombre esta vacio "
            '        'Me.Label16.Text = "El campo nombre esta vacio "
            '        '          Me.LabelErrorGeneral.Text = "El campo nombre esta vacio "
            '    End If
            'End If

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
            objProject = Nothing
            ProgramComponentByProjectList = Nothing

        End Try

    End Sub

    'Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
    '    Dim contract As DataTable
    '    Dim idproyect = Request.QueryString("Id")

    '    Try
    '        Dim SQL As New StringBuilder

    '        'consulta para averiguar si el proyecto tiene contratos
    '        ' German rodriguez MGgroup
    '        SQL.AppendLine(" select idproject from ContractRequest ")
    '        SQL.AppendLine(" where idproject = " & idproyect)

    '        'Ejecutar la Instruccion
    '        contract = GattacaApplication.RunSQLRDT(applicationCredentials, SQL.ToString)

    '        'validamos la consusta de contratos
    '        ' German rodriguez MGgroup
    '        If contract.Rows.Count > 0 Then

    '            Me.containerSuccess.Visible = True
    '            '      Me.lblstatesuccess.Text = "El proyecto no se puede eliminar porque contiene contratos heredados! "
    '            Exit Sub
    '        Else

    '            ' eliminar el registro
    '            facade.deleteProject(applicationCredentials, Request.QueryString("Id"), ViewState("idKey"))

    '            ' ir al administrador
    '            Response.Redirect("searchProject.aspx")

    '        End If


    '    Catch oex As Threading.ThreadAbortException
    '        ' no hacer nada

    '    Catch ex As Exception

    '        ' ir a error
    '        Session("sError") = ex.Message
    '        Session("sUrl") = Request.UrlReferrer.PathAndQuery
    '        Response.Redirect("~/errors/error.aspx")
    '        Response.End()

    '    Finally

    '        ' liberar recursos
    '        facade = Nothing

    '    End Try

    'End Sub

    'Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDelete.Click

    '    ' ocultar algunos botones
    '    Me.btnAddData.Visible = False
    '    Me.btnSave.Visible = True
    '    'Me.btnDelete.Visible = True
    '    'Me.btnCancelDelete.Visible = False
    '    Me.btnConfirmDelete.Visible = False
    '    Me.lblDelete.Visible = False
    '    'Me.btnCancel.Visible = True

    'End Sub

    'Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    '    ' ocultar algunos botones
    '    Me.btnSave.Visible = False
    '    'Me.btnDelete.Visible = False
    '    ' Me.btnConfirmDelete.Visible = True
    '    'Me.btnCancel.Visible = False
    '    ''Me.btnCancelDelete.Visible = True
    '    'Me.lblDelete.Visible = True

    'End Sub

    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        'Verificar Código
        verifyCode()

        'Se selecciona la pestaña inicial
        ' Me.TabContainer1.ActiveTabIndex = 0
        Me.txtname.Focus()

    End Sub

    'Protected Sub btnAddSource_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSource.Click

    '    ' cargar la lista de fuentes
    '    Dim objSourceByProjectList As List(Of SourceByProjectEntity) = Session("sourceByProjectList")
    '    Dim objSourceByProject As New SourceByProjectEntity()
    '    Dim repeated As Boolean = 0
    '    'Me.lblSourceMessage.Text = ""

    '    ' guardar los datos de la fecha
    '    'objSourceByProject.idsource = Me.ddlSource.SelectedValue
    '    'objSourceByProject.SOURCENAME = Me.ddlSource.SelectedItem.Text

    '    For Each objsource As SourceByProjectEntity In objSourceByProjectList
    '        If (objsource.idsource = objSourceByProject.idsource) Then
    '            repeated = 1
    '            Exit For
    '        End If
    '    Next

    '    If Not repeated Then
    '        ' agregar la fecha a la lista
    '        objSourceByProjectList.Add(objSourceByProject)
    '    Else
    '        'Notificar al usuario
    '        '    Me.lblSourceMessage.Text = "Ya existe esta fuente, por favor verifique."
    '    End If

    '    ' mostrar los datos en la grilla
    '    'Me.gvSourceByProject.DataSource = objSourceByProjectList
    '    'Me.gvSourceByProject.DataBind()

    'End Sub


    'Protected Sub btnAddProjectLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProjectLocation.Click

    '    Dim objProjectLoactionList As DataTable = Session("projectLocationList")
    '    Dim objProjectLocation As New ProjectLocationEntity
    '    Dim repeated As Boolean = 0

    '    If Me.HDswich_ubicacion.Value = "" Or Me.HDswich_ubicacion.Value = 0 Then
    '        Me.HDswich_ubicacion.Value = 0
    '    End If


    '    ' guardar los datos de la fecha
    '    objProjectLocation.idcity = ddlidcity.SelectedValue
    '    objProjectLocation.CITYNAME = ddlidcity.SelectedItem.Text
    '    objProjectLocation.DEPTONAME = ddliddepto.SelectedItem.Text

    '    ' For Each objproloc In objProjectLoactionList.Rows
    '    'If objproloc(0) = Convert.ToInt32(objProjectLocation.idcity) Then
    '    'repeated = 1
    '    ' End If
    '    'Next

    '    'If Not repeated Then
    '    ' agregar la fecha a la lista
    '    objProjectLoactionList.Rows.Add(objProjectLocation.idcity, objProjectLocation.DEPTONAME, objProjectLocation.CITYNAME)
    '    lblHelpprojectLocation.Text = "Recuerde hacer click en guardar para efectuar los cambios"
    '    'Else
    '    ' lblHelpprojectLocation.Text = "Ya existe esta ubicación, Recuerde hacer click en guardar para efectuar los cambios"
    '    '  End If
    '    Session("projectLocationList") = objProjectLoactionList
    '    ' mostrar los datos en la grilla
    '    Me.gvprojectLocation.DataSource = objProjectLoactionList
    '    Me.gvprojectLocation.DataBind()
    'End Sub

    'Protected Sub btnaddthirdbyproject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddthirdbyproject.Click
    '    ' cargar la lista de fechas
    '    Dim objThirdByProjectList As List(Of ThirdByProjectEntity) = Session("thirdByProjectList")
    '    Dim objThirdByProject As New ThirdByProjectEntity
    '    Dim repeated As Boolean = 0

    '    ' guardar los datos de la fecha
    '    'objThirdByProject.idthird = ddlidthird.SelectedValue
    '    'objThirdByProject.actions = txtactions.Text
    '    'objThirdByProject.experiences = txtexperiences.Text
    '    'objThirdByProject.type = ddlType.SelectedValue
    '    'objThirdByProject.THIRDNAME = txtActor.Text

    '    'For Each objthpro As ThirdByProjectEntity In objThirdByProjectList
    '    '    If (objthpro.idthird = objThirdByProject.idthird) Then
    '    '        repeated = 1
    '    '    End If
    '    'Next

    '    ' If Not repeated Then
    '    ' agregar la fecha a la lista
    '    objThirdByProjectList.Add(objThirdByProject)
    '    Me.txtactions.Text = ""
    '    Me.txtexperiences.Text = ""
    '    Me.ddlType.SelectedIndex = 0
    '    txtActor.Text = ""
    '    lblHelpthirdbyproject.Text = "Recuerde hacer click en guardar para efectuar los cambios"

    '    'Else
    '    'lblHelpthirdbyproject.Text = "Ya existe este tercero, Recuerde hacer click en guardar para efectuar los cambios"
    '    'End If

    '    ' mostrar los datos en la grilla
    '    Me.gvthirdbyproject.DataSource = objThirdByProjectList
    '    Me.gvthirdbyproject.DataBind()
    'End Sub

    'Protected Sub btnaddoperatorbyproject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddoperatorbyproject.Click
    '    'credenciales
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    ' cargar la lista de actores operatorByProjectList = thirdByProjectList
    '    Dim objThirdByProjectList As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()
    '    Dim thirdlist As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()
    '    Dim objThirdByProject As ThirdByProjectEntity = New ThirdByProjectEntity()

    '    Dim sql As New StringBuilder
    '    Dim objSqlCommand As New SqlCommand
    '    Dim data As DataTable

    '    'contador para saber la cantidad de datos en el grid ----------- German Rodriguez--- 
    '    Dim countgve As Integer = Me.gvoperatorbyproject.Rows.Count

    '    Dim CAMBIO As Integer = 0


    '    If countgve = 0 Then
    '        countgve = 0
    '        lblHelpoperatorbyproject.Text = ""
    '    Else
    '        countgve = countgve - 1
    '        Dim ir As Integer = 0
    '        For i = 1 To countgve
    '            Dim lbltmoney = CType(gvoperatorbyproject.Rows(ir).Cells(1).FindControl("lblIdactor"), Label).Text
    '            Dim comparar As Integer = Me.HDIDTHIRD.Value
    '            If comparar = lbltmoney Then
    '                lblHelpoperatorbyproject.Text = "Este actor ya fue ingresado"
    '                CAMBIO = 1
    '                Exit For
    '            End If
    '            ir = ir + 1
    '        Next
    '    End If


    '    If CAMBIO = 0 Then

    '        objThirdByProjectList = DirectCast(Session("operatorByProjectList"), List(Of ThirdByProjectEntity))

    '        objThirdByProject.idthird = Me.HDIDTHIRD.Value
    '        objThirdByProject.THIRDNAME = Me.HDNAMETHIRD.Value
    '        ' guardar los datos del actor
    '        objThirdByProject.idthird = ddlidoperator.SelectedValue
    '        'capturar el typo de operador
    '        objThirdByProject.type = ddltipooperador.SelectedItem.Text
    '        'CONSULTA DE DATOS DEL ACTOR
    '        sql.Append("select t.contact,t.documents,t.phone,t.email from third t where t.Id = " & Me.HDIDTHIRD.Value)
    '        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)
    '        'VALIDAMOS LA CONSULTA Y LOS CARGAMOS
    '        If data.Rows.Count > 0 Then

    '            If IsDBNull(data.Rows(0)("contact")) = False Then
    '                objThirdByProject.THIRD.contact = data.Rows(0)("contact")
    '            End If

    '            If IsDBNull(data.Rows(0)("documents")) = False Then
    '                objThirdByProject.THIRD.documents = data.Rows(0)("documents")
    '            End If

    '            If IsDBNull(data.Rows(0)("phone")) = False Then
    '                objThirdByProject.THIRD.phone = data.Rows(0)("phone")
    '            End If

    '            If IsDBNull(data.Rows(0)("email")) = False Then
    '                objThirdByProject.THIRD.email = data.Rows(0)("email")
    '            End If
    '        Else

    '            objThirdByProject.THIRD.contact = ""
    '            objThirdByProject.THIRD.documents = ""
    '            objThirdByProject.THIRD.email = ""
    '            objThirdByProject.THIRD.phone = ""

    '        End If


    '        'AGREGAMOS A LA SECION
    '        objThirdByProjectList.Add(objThirdByProject)

    '        Dim objDataTableFin As DataTable = New DataTable()

    '        objDataTableFin.Columns.Add("idthird")
    '        objDataTableFin.Columns.Add("name")
    '        objDataTableFin.Columns.Add("type")
    '        objDataTableFin.Columns.Add("contact")
    '        objDataTableFin.Columns.Add("documents")
    '        objDataTableFin.Columns.Add("phone")
    '        objDataTableFin.Columns.Add("email")

    '        For Each itemDataTablefin As ThirdByProjectEntity In objThirdByProjectList
    '            objDataTableFin.Rows.Add(itemDataTablefin.idthird, itemDataTablefin.THIRDNAME, itemDataTablefin.type, itemDataTablefin.THIRD.contact, itemDataTablefin.THIRD.documents, itemDataTablefin.THIRD.phone, itemDataTablefin.THIRD.email)
    '        Next

    '        'CARGARMOS AL GRID
    '        Me.gvoperatorbyproject.DataSource = objDataTableFin
    '        Me.gvoperatorbyproject.DataBind()

    '    End If




    '    'Dim idproject As Integer

    '    'For Each obt As ThirdByProjectEntity In objThirdByProjectList
    '    '    idproject = obt.idproject
    '    '    Exit For
    '    'Next


    '    ' Session("idProject") = idproject
    '    'Dim objThirdByProjectList As List(Of ThirdByProjectEntity) = Session("operatorByProjectList")

    '    'For Each objopepro As ThirdByProjectEntity In objThirdByProjectList
    '    '    If (objopepro.id = objThirdByProject.idthird) Then
    '    '        repeated = 1
    '    '    End If
    '    'Next

    '    ''obtencion de los datos del actor segun id seleccionado del combo
    '    'Dim objDataTableThird As New DataTable()
    '    'objDataTableThird = getThirdById(objThirdByProject.idthird, applicationCredentials)
    '    ''Dim num As Integer = Me.insertThirdProject(applicationCredentials, objThirdByProject.idthird, idproject, objThirdByProject.type)
    '    ''obtencion de los datos de los terceros en Session
    '    'Dim objDataTableThirdSession As New DataTable()
    '    ''objDataTableThirdSession = getThirdBySession(idproject, applicationCredentials)

    '    'lblHelpoperatorbyproject.Text = "Ya existe este Operador, Recuerde hacer click en guardar para efectuar los cambios"



    '    'For Each rowDataTable In objDataTableThirdSession.Rows
    '    '    objDataTableFin.Rows.Add(rowDataTable(0), rowDataTable(1), rowDataTable(2), rowDataTable(3), rowDataTable(4), rowDataTable(5))
    '    'Next


    'End Sub

    'Protected Sub gvSourceByProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSourceByProject.SelectedIndexChanged
    '    Me.lblSourceMessage.Text = ""

    '    ' cargar la lista de ubicaciones
    '    Dim sourceByProjectList As List(Of SourceByProjectEntity) = Session("sourceByProjectList")

    '    ' eliminar la ubicación de la lista
    '    sourceByProjectList.RemoveAt(Me.gvSourceByProject.SelectedIndex)

    '    ' mostrar las ubicaciones en la grilla
    '    Me.gvSourceByProject.DataSource = sourceByProjectList
    '    Me.gvSourceByProject.DataBind()
    'End Sub

    'Protected Sub gvthirdbyproject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvthirdbyproject.SelectedIndexChanged
    '    ' cargar la lista de terceros
    '    Dim thirdByProjectList As List(Of ThirdByProjectEntity) = Session("thirdByProjectList")

    '    ' eliminar el tercero de la lista
    '    thirdByProjectList.RemoveAt(Me.gvthirdbyproject.SelectedIndex)

    '    ' mostrar los terceros en la grilla
    '    Me.gvthirdbyproject.DataSource = thirdByProjectList
    '    Me.gvthirdbyproject.DataBind()
    'End Sub

    'Protected Sub gvoperatorbyproject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvoperatorbyproject.SelectedIndexChanged
    '    ' cargar la lista de operadores
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Dim ThirdByProjectList As List(Of ThirdByProjectEntity)
    '    Dim index As Integer = 0

    '    ' cargarla de la session
    '    ThirdByProjectList = DirectCast(Session("operatorByProjectList"), List(Of ThirdByProjectEntity))


    '    'Dim idproj As Integer = Session("idProject")
    '    'Dim operatorByProjectList As DataTable = getThirdBySession(idproj, applicationCredentials)
    '    'Dim operatorTable As DataTable
    '    'Dim indiceEliminar As Integer = Me.gvoperatorbyproject.SelectedIndex
    '    'Dim cont As Integer = 0
    '    'Dim idThirdByProject As Integer
    '    ' eliminar el operador de la lista
    '    ' For Each rowDataTable In operatorByProjectList.Rows
    '    '   If indiceEliminar = cont Then
    '    '       idThirdByProject = rowDataTable(6)
    '    '       deleteThirdByProject(applicationCredentials, idThirdByProject)
    '    '   End If
    '    '   cont = cont + 1
    '    ' next

    '    '  ThirdByProjectList.RemoveAt(Me.gvoperatorbyproject.SelectedIndex)

    '    Dim objDataTableFin As DataTable = New DataTable()

    '    objDataTableFin.Columns.Add("idthird")
    '    objDataTableFin.Columns.Add("name")
    '    objDataTableFin.Columns.Add("type")
    '    objDataTableFin.Columns.Add("contact")
    '    objDataTableFin.Columns.Add("documents")
    '    objDataTableFin.Columns.Add("phone")
    '    objDataTableFin.Columns.Add("email")

    '    For Each itemDataTablefin As ThirdByProjectEntity In ThirdByProjectList
    '        objDataTableFin.Rows.Add(itemDataTablefin.idthird, itemDataTablefin.THIRDNAME, itemDataTablefin.type, itemDataTablefin.THIRD.contact, itemDataTablefin.THIRD.documents, itemDataTablefin.THIRD.phone, itemDataTablefin.THIRD.email)
    '    Next

    '    'Me.gvoperatorbyproject.DataSource = objDataTableFin
    '    'Me.gvoperatorbyproject.DataBind()


    '    ' mostrar los operadores en la grilla
    '    'Me.gvoperatorbyproject.DataSource = operatorByProjectList
    '    ' Me.gvoperatorbyproject.DataSource = getThirdBySession(idproj, applicationCredentials)
    '    'Me.gvoperatorbyproject.DataBind()
    '    'Session("operatorByProjectList") = getThirdBySession(idproj, applicationCredentials)
    'End Sub

    'Protected Sub ddliddepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddliddepto.SelectedIndexChanged
    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try

    '        ' cargar el combo de Ciudad
    '        Me.ddlidcity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddliddepto.SelectedValue, order:="code")
    '        Me.ddlidcity.DataValueField = "Id"
    '        Me.ddlidcity.DataTextField = "name"
    '        Me.ddlidcity.DataBind()

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

    'Protected Sub ddlidStrategicLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidStrategicLine.SelectedIndexChanged
    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try

    '        ' cargar el combo de programa
    '        If (Request.QueryString("op").Equals("add")) Then
    '            Me.ddlidProgram.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlidStrategicLine.SelectedValue, enabled:="1", order:="code")
    '        Else
    '            Me.ddlidProgram.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlidStrategicLine.SelectedValue, order:="code")
    '        End If
    '        Me.ddlidProgram.DataValueField = "Id"
    '        Me.ddlidProgram.DataTextField = "Code"
    '        Me.ddlidProgram.DataBind()

    '        'Cargar la lista doble de Componentes del Programa
    '        Me.dlbProgramComponentByProject.AviableItems.Items.Clear()
    '        ' Me.dlbProgramComponentByProject.SelectedItems.Items.Clear()
    '        If (Me.ddlidProgram.Items.Count > 0) Then
    '            If (Request.QueryString("op").Equals("add")) Then
    '                Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, enabled:="1", order:="code")
    '            Else
    '                Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, order:="code")
    '            End If
    '            Me.dlbProgramComponentByProject.AviableItems.DataValueField = "Id"
    '            Me.dlbProgramComponentByProject.AviableItems.DataTextField = "Code"
    '            Me.dlbProgramComponentByProject.AviableItems.DataBind()
    '        End If
    '        Dim miItem As ListItem
    '        For Each item As ListItem In Me.dlbProgramComponentByProject.SelectedItems.Items

    '            miItem = Me.dlbProgramComponentByProject.AviableItems.Items.FindByValue(item.Value)
    '            ' cargar los valores seleccionados
    '            dlbProgramComponentByProject.AviableItems.Items.Remove(miItem)
    '        Next

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

    'Protected Sub ddlidProgram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlidProgram.SelectedIndexChanged
    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try

    '        'Cargar la lista doble de Componentes del Programa
    '        Me.dlbProgramComponentByProject.AviableItems.Items.Clear()
    '        ' Me.dlbProgramComponentByProject.SelectedItems.Items.Clear()
    '        If (Me.ddlidProgram.Items.Count > 0) Then
    '            If (Request.QueryString("op").Equals("add")) Then
    '                Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, enabled:="1", order:="code")
    '            Else
    '                Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, order:="code")
    '            End If
    '            Me.dlbProgramComponentByProject.AviableItems.DataValueField = "Id"
    '            Me.dlbProgramComponentByProject.AviableItems.DataTextField = "Code"
    '            Me.dlbProgramComponentByProject.AviableItems.DataBind()
    '        End If
    '        Dim miItem As ListItem
    '        For Each item As ListItem In Me.dlbProgramComponentByProject.SelectedItems.Items

    '            miItem = Me.dlbProgramComponentByProject.AviableItems.Items.FindByValue(item.Value)
    '            ' cargar los valores seleccionados
    '            dlbProgramComponentByProject.AviableItems.Items.Remove(miItem)
    '        Next

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

#Region "Metodos"


    ''' <summary>
    ''' Permite cargar los archivos sleeccionados
    ''' </summary>
    ''' <param name="userId">Identificador del usuario actual</param>
    ''' <remarks></remarks>
    Private Sub LoadFiles(ByVal objProject As ProjectEntity, ByVal userId As Long)

        'Definiendo los objtetos
        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files

        'Se verifica que existan archivos por cargar
        If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objProject.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else
                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objProject.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objProject.DOCUMENTLIST = Me.DocumentsList
                End If

            End If

            'Se recorre la lista de archivos cargados al servidor
            For i As Integer = 0 To files.Count - 1

                Dim file As HttpPostedFile = files(i)

                If file.ContentLength > 0 Then

                    strFileName = file.FileName.Split("\".ToCharArray)

                    ' dar nombre al anexo
                    fileName = Now.ToString("yyyyMMddhhmmss") & "_" & strFileName(strFileName.Length - 1)

                    ' determinanado la ruta destino
                    Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\" & fileName

                    'Subiendo el archivo al server
                    file.SaveAs(sFullPath)

                    'Se instancia un objeto de tipo documento y se pobla con la info. reuqerida.
                    Dim objDocument As New DocumentsEntity()
                    objDocument.attachfile = fileName
                    objDocument.createdate = Now
                    objDocument.iduser = userId
                    objDocument.enabled = True
                    objDocument.ISNEW = True

                    'Se agrega el objeto de tipo documento a la lista de documentos
                    objProject.DOCUMENTLIST.Add(objDocument)



                End If

            Next

        End If

    End Sub

    Private Sub LoadFilesBySession(ByVal objProject As ProjectEntity, ByVal userId As Long)

        Dim DocumentsTmpList As New List(Of DocumentstmpEntity)

        'se traen los nombres de los archivos por session
        DocumentsTmpList = Session("DocumentsTmp")
        'Se verifica que existan archivos por cargar
        If (Not DocumentsTmpList Is Nothing) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objProject.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else
                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objProject.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objProject.DOCUMENTLIST = Me.DocumentsList
                End If

            End If

            'Se recorre la lista de archivos cargados al servidor
            For i As Integer = 0 To DocumentsTmpList.Count - 1

                Dim fileName As String = DocumentsTmpList(i).namefile



                'Se instancia un objeto de tipo documento y se pobla con la info. reuqerida.
                Dim objDocument As New DocumentsEntity()
                objDocument.attachfile = fileName
                objDocument.createdate = Now
                objDocument.iduser = userId
                objDocument.enabled = True
                objDocument.ISNEW = True

                'Se agrega el objeto de tipo documento a la lista de documentos
                objProject.DOCUMENTLIST.Add(objDocument)





            Next

        End If

    End Sub

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim i As Integer
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim ideaAprobada As New List(Of IdeaEntity)


        Try
            ' cargar el combo de Ideas
            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlididea.DataSource = facade.getIdeaListApproved(applicationCredentials, enabled:="1", order:="code")
            'Else
            '    Me.ddlididea.DataSource = facade.loadProject(applicationCredentials, Request.QueryString("id"))
            'End If
            'Me.ddlididea.DataValueField = "Id"
            'Me.ddlididea.DataTextField = "Name"

            'Me.ddlididea.DataBind()
            'Me.ddlididea.Items.Insert(0, New ListItem("Seleccione...", "-1"))
            ''ddlididea.Items.Add(New ListItem("Seleccione...", "-1"))
            'ddlididea.SelectedValue = "-1"

            'Dim fechainicio As Integer = Session("txtstartdate")

            'Cargar los años para vigencia presupuestal
            For i = 2011 To 2030
                ddleffectivebudget.Items.Add(i.ToString)
            Next
            ' cargar el combo de fuentes
            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlSource.DataSource = facade.getSourceList(applicationCredentials, enabled:="1", order:="name")
            'Else
            '    Me.ddlSource.DataSource = facade.getSourceList(applicationCredentials, order:="name")
            'End If
            'Me.ddlSource.DataValueField = "Id"
            'Me.ddlSource.DataTextField = "name"
            'Me.ddlSource.DataBind()


            ' cargar el combo de Departamentos
            'Me.ddliddepto.DataSource = facade.getDeptoList(applicationCredentials, idcountry:="7", order:="code")
            'Me.ddliddepto.DataValueField = "Id"
            'Me.ddliddepto.DataTextField = "Name"
            'Me.ddliddepto.DataBind()

            '' cargar el combo de Ciudad
            'Me.ddlidcity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddliddepto.SelectedValue, order:="code")
            'Me.ddlidcity.DataValueField = "Id"
            'Me.ddlidcity.DataTextField = "Name"
            'Me.ddlidcity.DataBind()

            ' cargar el combo de terceros
            'If (Request.QueryString("op").Equals("add")) Then
            '    Me.ddlidoperator.DataSource = facade.getThirdList(applicationCredentials, enabled:="1", order:="code")
            'Else
            '    Me.ddlidoperator.DataSource = facade.getThirdList(applicationCredentials, order:="code")
            'End If
            ''Me.ddlidthird.DataValueField = "Id"
            'Me.ddlidthird.DataTextField = "Name"
            'Me.ddlidthird.DataBind()

            ' cargar el combo de operadores
            'Me.ddlidoperator.DataValueField = "Id"
            'Me.ddlidoperator.DataTextField = "Name"
            'Me.ddlidoperator.DataBind()

            '' cargar el combo de Linea Estrategica
            'If (Request.QueryString("op").Equals("add")) Then
            '    'Me.ddlidStrategicLine.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="code")
            'Else
            '    'Me.ddlidStrategicLine.DataSource = facade.getStrategicLineList(applicationCredentials, order:="code")
            'End If
            ''Me.ddlidStrategicLine.DataValueField = "Id"
            'Me.ddlidStrategicLine.DataTextField = "Code"
            'Me.ddlidStrategicLine.DataBind()

            ' cargar el combo de programa
            'If (Request.QueryString("op").Equals("add")) Then
            '    ' Me.ddlidProgram.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlidStrategicLine.SelectedValue, enabled:="1", order:="code")
            'Else
            '    'Me.ddlidProgram.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlidStrategicLine.SelectedValue, order:="code")
            'End If
            ''Me.ddlidProgram.DataValueField = "Id"
            'Me.ddlidProgram.DataTextField = "Code"
            'Me.ddlidProgram.DataBind()

            'tipo aprobacion


            'Cargar la lista doble de Componentes del Programa
            'If (Me.ddlidProgram.Items.Count > 0) Then
            '    If (Request.QueryString("op").Equals("add")) Then
            '        Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, enabled:="1", order:="code")
            '    Else
            '        Me.dlbProgramComponentByProject.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=ddlidProgram.SelectedValue, order:="code")
            '    End If
            '    Me.dlbProgramComponentByProject.AviableItems.DataValueField = "Id"
            '    Me.dlbProgramComponentByProject.AviableItems.DataTextField = "Code"
            '    Me.dlbProgramComponentByProject.AviableItems.DataBind()
            'End If

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

            If facade.verifyProjectCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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

    Public Sub loadVersions(ByVal idKey As String)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim list As List(Of ProjectEntity)

        Try
            ' cargar la lista de versiones anteriores
            list = facade.getProjectList(applicationCredentials, idKey:=idKey, isLastVersion:=0)

            'Me.gvVersion.DataSource = list
            'Me.gvVersion.DataBind()

            If list.Count > 0 Then

                ' mensaje
                Me.lblVersion.Text = "Versiones Anteriores Registradas"

            Else

                ' mensaje
                Me.lblVersion.Text = "No Hay Versiones Anteriores Registradas"

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

    Protected Function getThirdByIdea(ByVal Idea_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As List(Of ThirdByProjectEntity)
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de terceros por proyectos de tipo Thirdbyproject
            Dim objListThirdByProject As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()

            'consulta de los datos de actores por id
            sql.Append("select t.Id , ti.Type from Third t ")
            sql.Append("inner join ThirdByIdea ti on ti.IdThird = t.Id ")
            sql.Append("inner join Idea i on i.Id = ti.IdIdea ")
            sql.Append("where i.Id = " & Idea_id)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each item_ThirdByProjectEntity In data.Rows
                Dim objThirdByProjectEntity As ThirdByProjectEntity = New ThirdByProjectEntity()
                objThirdByProjectEntity.idthird = item_ThirdByProjectEntity("Id")
                objThirdByProjectEntity.type = item_ThirdByProjectEntity("Type")
                objListThirdByProject.Add(objThirdByProjectEntity)
            Next
            Return objListThirdByProject
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'funcion que trae los actores por proyecto
    Protected Function getThirdByProject(ByVal Project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As List(Of ThirdByProjectEntity)
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de terceros por proyectos de tipo Thirdbyproject
            Dim objListThirdByProject As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()

            'consulta de los datos de actores por id
            sql.Append("select t.Id, t.Name, tp.type, p.Id as idproject from Third t ")
            sql.Append("inner join ThirdByProject tp on tp.IdThird = t.Id ")
            sql.Append("inner join Project p on p.Id = tp.IdProject ")
            sql.Append("where p.Id = " & Project_id)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each item_ThirdByProjectEntity In data.Rows
                Dim objThirdByProjectEntity As ThirdByProjectEntity = New ThirdByProjectEntity()
                objThirdByProjectEntity.idthird = item_ThirdByProjectEntity("Id")
                objThirdByProjectEntity.THIRDNAME = item_ThirdByProjectEntity("Name")
                objThirdByProjectEntity.type = item_ThirdByProjectEntity("Type")
                objThirdByProjectEntity.idproject = item_ThirdByProjectEntity("idproject")
                objListThirdByProject.Add(objThirdByProjectEntity)
            Next
            Return objListThirdByProject
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getExplanatoryByProject(ByVal project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As List(Of ExplanatoryEntity)
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de aclaratorios
            Dim objListExplanatory As List(Of ExplanatoryEntity) = New List(Of ExplanatoryEntity)()

            'consulta de los datos de aclaratorios
            sql.Append("select e.observation, e.fecha from Explanatory e ")
            sql.Append("where e.idproject = " & project_id)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each item_explanatoryEntity In data.Rows
                Dim objExplanatoryEntity As ExplanatoryEntity = New ExplanatoryEntity()
                objExplanatoryEntity.observation = item_explanatoryEntity("observation")
                objExplanatoryEntity.fecha = item_explanatoryEntity("fecha")
                objListExplanatory.Add(objExplanatoryEntity)
            Next
            Return objListExplanatory
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getFlowPayment(ByVal Project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As List(Of PaymentFlowEntity)
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de flujos de pago
            Dim objListPaymentFlow As List(Of PaymentFlowEntity) = New List(Of PaymentFlowEntity)()

            'consulta de los datos de actores por id
            sql.Append("select p.Id, pf.* from Project p ")
            sql.Append("inner join Paymentflow pf on pf.Idproject = p.Id ")

            sql.Append("where p.Id = " & Project_id)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each item_flowEntity In data.Rows
                Dim objflowpaymentEntity As PaymentFlowEntity = New PaymentFlowEntity()
                objflowpaymentEntity.id = item_flowEntity("Id")
                objflowpaymentEntity.idproject = item_flowEntity("idproject")
                objflowpaymentEntity.fecha = item_flowEntity("fecha")
                objflowpaymentEntity.porcentaje = item_flowEntity("porcentaje")
                objflowpaymentEntity.entregable = item_flowEntity("entregable")
                objflowpaymentEntity.ididea = item_flowEntity("Ididea")
                objflowpaymentEntity.valorparcial = item_flowEntity("valorparcial")
                objflowpaymentEntity.valortotal = item_flowEntity("valortotal")

                objListPaymentFlow.Add(objflowpaymentEntity)
            Next
            Return objListPaymentFlow
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getLinStrat(ByVal project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As DataTable
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            ' consulta de los datos de la idea para crear proyecto
            sql.Append("select  TOP 1 sl.Name, p.Name as programa, i.Objective, i.Justification, i.AreaDescription, i.Results, ")
            sql.Append("i.ResultsKnowledgeManagement, i.ResultsInstalledCapacity, i.StartDate, i.Duration, ")
            sql.Append(" i.Population, ")
            sql.Append(" i.Source, i.Enabled, par.ApprovedValue ")
            sql.Append(" from Idea i join ProgramComponentByIdea pci on (pci.IdIdea=i.Id) ")
            sql.Append(" join ProgramComponent pc on (pc.Id=pci.IdProgramComponent)")
            sql.Append(" join Program p on (p.Id=pc.IdProgram) ")
            sql.Append(" join StrategicLine sl on (sl.Id=p.IdStrategicLine) ")
            sql.Append(" left join ThirdByIdea tbi on (tbi.IdIdea=i.Id) ")
            sql.Append(" left join ProjectApprovalRecord par on (par.Ididea=i.Id) ")
            sql.Append(" WHERE i.Id =" & project_id)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getLinStratbusqueda(ByVal project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As DataTable
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            ' consulta de los datos de la idea para crear proyecto
            sql.Append("select  sl.Name, p.Name as programa, i.Objective, i.Justification, i.Results,i.ResultsKnowledgeManagement, i.ResultsInstalledCapacity, i.Duration,i.Population,i.Source, i.Enabled, par.ApprovedValue from Project i ")
            sql.Append(" join ProgramComponentByProject pci on (pci.IdProject=i.Id) ")
            sql.Append(" join ProgramComponent pc on (pc.Id=pci.IdProgramComponent)")
            sql.Append(" join Program p on (p.Id=pc.IdProgram) ")
            sql.Append(" join StrategicLine sl on (sl.Id=p.IdStrategicLine) ")
            sql.Append(" left join ThirdByIdea tbi on (tbi.IdIdea=i.Id) ")
            sql.Append(" left join ProjectApprovalRecord par on (par.Ididea=i.Id) ")
            sql.Append(" WHERE i.Id =" & project_id)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Protected Sub gvDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocuments.RowDeleting

    '    'Se recupera la lista de documentos actual
    '    Dim documentsList As List(Of DocumentsEntity)
    '    documentsList = Me.DocumentsList

    '    'Se pone el estado de elminación al documento requerido
    '    documentsList(e.RowIndex).ISDELETED = True

    '    'Se oculta de la grilla el registro seleccionado
    '    Me.gvDocuments.Rows(e.RowIndex).Visible = False

    'End Sub
    ''***********************************************************************************************************
    ''funcion para refrescar lista de documentos
    'Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    '    'Se llama al metodo que cpnsulta la lista de documentos para el registro de idea actual
    '    Me.LoadDocumentsList()

    '    'Se actualiza la grilla.
    '    Me.gvDocuments.DataSource = Me.DocumentsList
    '    Me.gvDocuments.DataBind()

    'End Sub

    ''' <summary>
    ''' Permite actualizar la lista de archivos anexos al proyecto actual
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocumentsList()

        'Definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se definen los objetos
            Dim documentsByEntityList As List(Of DocumentsByEntityEntity)

            'Se llama al metodo que permite consultar la lista de documentos para el registro del proyecto actual
            'Se carga la lista de documentos para el registro de proyecto actual
            documentsByEntityList = facade.getDocumentsByEntityList(applicationCredentials, idnentity:=Request.QueryString("id"), entityName:=GetType(ProjectEntity).ToString())

            'Se verifica que existam documentos anexos al registro actual
            If (Not documentsByEntityList Is Nothing AndAlso documentsByEntityList.Count > 0) Then
                'Se recorre la lista de identificadores de documentos agregados
                Dim idsDocuments As String = ""
                For Each documentByEntity As DocumentsByEntityEntity In documentsByEntityList
                    idsDocuments &= documentByEntity.iddocuments.ToString() & ","
                Next
                If (idsDocuments.Length > 0) Then idsDocuments = idsDocuments.Substring(0, idsDocuments.Length - 1)

                'Se carga la lista de documentos requeridos
                Me.DocumentsList = facade.getDocumentsListByEntity(applicationCredentials, idsDocuments)
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

    Private Function validarcamposnum()

        Dim shwich As Integer = 0
        Dim valorcampo As Long = 0
        Dim val1 = Replace(Me.txtfsccontribution.Text, ".", "")
        Dim val2 = Replace(Me.txtcounterpartvalue.Text, ".", "")

        Try
            valorcampo = Convert.ToInt64(val1) + Convert.ToInt64(val2)

        Catch ex As Exception
            Me.lblHelpfsccontribution.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
            Me.lblHelpcounterpartvalue.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
            Me.txtfsccontribution.Text = ""
            Me.txtcounterpartvalue.Text = ""
            Me.txttotalcost.Text = ""
            shwich = 1
        End Try

        Return shwich

    End Function

#End Region


    'Protected Sub BtnAddPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddPayment.Click
    '    Page.Validate("validat")
    '    If Page.IsValid Then
    '        'deshabilitar campo valor total cuando se comienza a agregar pagos
    '        If Session("totporcentajes") < 100 Then
    '            txtvalortotalflow.Enabled = False
    '        End If
    '        Dim porcentajeT As Integer
    '        Dim totalporcentajes As Double
    '        Me.lblmensajeexitoerror.Text = ""
    '        Me.lblmsjporcent.Text = ""
    '        Dim paymentflow As PaymentFlowEntity = New PaymentFlowEntity()
    '        Dim valorTotal As String = Session("valortotalflow")
    '        valorTotal = Me.txtvalortotalflow.Text.Replace(",", "")
    '        paymentflow.valortotal = valorTotal
    '        Session("valortotalflow") = valorTotal
    '        Dim PaymentFlowList As List(Of PaymentFlowEntity)
    '        Dim paymentflowValidat As PaymentFlowEntity = New PaymentFlowEntity()
    '        PaymentFlowList = DirectCast(Session("PaymentFlowList"), List(Of PaymentFlowEntity))
    '        'sumar totales porcentajes presentes en el Grid View para validar
    '        For Each paymentflowValidat In PaymentFlowList
    '            porcentajeT = porcentajeT + CDbl(paymentflowValidat.porcentaje.ToString)
    '        Next
    '        paymentflow.valortotal = valorTotal
    '        If Me.txtporcentaje.Text <> "" Then
    '            paymentflow.porcentaje = Me.txtporcentaje.Text.Replace(".", ",")

    '        End If
    '        paymentflow.entregable = Me.txtentregable.Text

    '        If Me.txtfechapago.Text <> "" Then
    '            paymentflow.fecha = Me.txtfechapago.Text
    '        End If

    '        Dim val_parc As Decimal = paymentflow.porcentaje * (valorTotal / 100)
    '        paymentflow.valorparcial = val_parc.ToString
    '        PaymentFlowList.Add(paymentflow)

    '        Dim objDataTable As DataTable = New DataTable()
    '        objDataTable.Columns.Add("id")
    '        objDataTable.Columns.Add("idproject")
    '        objDataTable.Columns.Add("fecha")
    '        objDataTable.Columns.Add("porcentaje")
    '        objDataTable.Columns.Add("entregable")
    '        objDataTable.Columns.Add("ididea")
    '        objDataTable.Columns.Add("valorparcial")

    '        'If lblmensajeexitoerror.Text = "" Then
    '        '    lblmensajeexitoerror.Text = "0"
    '        'End If

    '        If Convert.ToInt32(lblmensajeexitoerror.Text) <= 100 Then
    '            For Each itemDataTable As PaymentFlowEntity In Session("paymentFlowList")
    '                totalporcentajes = totalporcentajes + CDbl(itemDataTable.porcentaje.ToString)
    '            Next
    '            For Each itemDataTable As PaymentFlowEntity In Session("paymentFlowList")
    '                ' valida que el total de porcentajes no supere el 100 %
    '                'If totalporcentajes <= 100 Then
    '                objDataTable.Rows.Add(itemDataTable.id, itemDataTable.idproject, itemDataTable.fecha.ToShortDateString, itemDataTable.porcentaje, itemDataTable.entregable, itemDataTable.ididea, Format(itemDataTable.valorparcial, "Currency"))

    '                Me.lblmensajeexitoerror.Text = totalporcentajes.ToString()
    '                Session("totporcentajes") = Me.lblmensajeexitoerror.Text
    '                Me.LblFlujodePagoPorcentajeIzquierda.Text = "Porcentaje de pagos asignados: "
    '                Me.LblFlujodePagoPorcentajeDerecha.Text = "% sobre 100%"
    '                'End If

    '                If totalporcentajes > 100 Then
    '                    Me.lblExceed100.Text = "El porcentaje que intenta ingresar, supera el total aprobado."
    '                Else
    '                    Me.lblExceed100.Text = ""
    '                End If

    '            Next
    '            ' Se actualiza la informacion de la grilla
    '            'If totalporcentajes <= 100 Then
    '            Me.gvPaymentFlow.DataSource = objDataTable
    '            Me.gvPaymentFlow.DataBind()
    '            'Else
    '            'PaymentFlowList.Remove(paymentflow)
    '            'End If
    '        Else

    '        End If
    '        Session("paymentFlowList") = PaymentFlowList
    '    End If


    '    ' se recarga el valor total de los flujos
    '    'txtvalortotalflow.Text = Hdvtotalvalue.Value
    '    'TextFinalizacion.Text = HiddenFieldDateEnd.Value


    '    clearflowpayment()


    'End Sub

    Public Sub clearflowpayment()

        Me.txtentregable.Text = ""
        Me.txtfechapago.Text = ""

        Me.txtporcentaje.Text = ""

    End Sub

    'Protected Sub gvPaymentFlow_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPaymentFlow.SelectedIndexChanged
    '    ' definir los objetos
    '    Dim paymentFlowList As List(Of PaymentFlowEntity)
    '    Dim index As Integer = 0
    '    ' cargarla de la session
    '    paymentFlowList = DirectCast(Session("paymentFlowList"), List(Of PaymentFlowEntity))
    '    Dim totporcentajes As Double

    '    ' remover pago seleccionado
    '    paymentFlowList.RemoveAt(Me.gvPaymentFlow.SelectedIndex)

    '    ' mostrar
    '    Me.gvPaymentFlow.DataSource = paymentFlowList
    '    Me.gvPaymentFlow.DataBind()
    '    For Each pf As PaymentFlowEntity In paymentFlowList
    '        totporcentajes = totporcentajes + pf.porcentaje
    '    Next
    '    Session("totporcentajes") = totporcentajes
    '    Me.lblmensajeexitoerror.Text = Session("totporcentajes")
    '    If totporcentajes < 101 Then
    '        Me.lblExceed100.Text = ""
    '    End If

    '    If Session("totporcentajes") < 1 Then
    '        Me.txtvalortotalflow.Enabled = True

    '    Else
    '        Me.txtvalortotalflow.Enabled = False
    '    End If

    '    'Se selecciona la pestama de terceros por idea

    '    'Me.TabContainer1.ActiveTabIndex = 2
    'End Sub

    'Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged

    '    Dim consultLastVersion As Boolean = True
    '    Dim idea_id As Integer
    '    Dim swhich As Integer = Me.HDswich_ubicacion.Value
    '    Me.lblsaveinformation.Text = ""
    '    If Not (Request.QueryString("consultLastVersion") Is Nothing) Then consultLastVersion = Request.QueryString("consultLastVersion")
    '    If Me.hdfechainicio.Value <> "" Then
    '        'Me.txtstartdate.Text = Me.hdfechainicio.Value
    '    End If
    '    If Me.hdfechafinalizacion.Value <> "" Then
    '        '    Me.TextFinalizacion.Text = Me.hdfechafinalizacion.Value
    '    End If
    '    Dim objLocationByIdeaDALC As New LocationByIdeaDALC
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
    '    Dim objProject As New ProjectEntity
    '    ' crear clase data table para traer la linea estrategica y el programa de la idea
    '    Dim objDt As New DataTable()
    '    If Me.ddlididea.SelectedValue <> "" Then
    '        idea_id = Convert.ToInt32(Me.ddlididea.SelectedValue)
    '    End If
    '    If idea_id <> -1 Then

    '        objDt = getLinStrat(idea_id, applicationCredentials)

    '        If objDt.Rows.Count > 0 Then
    '            Me.txtlinestrat.Text = objDt.Rows(0)(0).ToString
    '            Me.txtprograma.Text = objDt.Rows(0)(1).ToString
    '            'Me.txtprograma.Text = objDt.Rows(0)(7).ToString
    '        End If
    '        'If (Me.TabPanel2.Visible = True And swhich = 0) Then
    '        '    Me.HDswich_ubicacion.Value = 1
    '        '    Dim objDatatable As DataTable = New DataTable("proyectLocationList")
    '        '    Dim objListLocationByIdea As List(Of LocationByIdeaEntity) = objLocationByIdeaDALC.getListByProject(applicationCredentials, idea_id, consultLastVersion)

    '        '    objDatatable.Columns.Add("IDCITY")
    '        '    objDatatable.Columns.Add("DEPTONAME")
    '        '    objDatatable.Columns.Add("CITYNAME")

    '        '    For Each itemLocation In objListLocationByIdea
    '        '        objDatatable.Rows.Add(itemLocation.CITY.id, itemLocation.DEPTO.name, itemLocation.CITY.name)
    '        '    Next

    '        '    Dim cantidadRegistros_DataTable As Integer = objDatatable.Rows.Count

    '        '    For Each itemGridView As GridViewRow In gvprojectLocation.Rows

    '        '        If cantidadRegistros_DataTable >= itemGridView.DataItemIndex + 1 Then

    '        '            Dim bytesDepartamento As Byte() = UTF8Encoding.Default.GetBytes(itemGridView.Cells(2).Text)
    '        '            Dim objDepartamento = Encoding.UTF8.GetString(bytesDepartamento)
    '        '            Dim bytesCiudad As Byte() = UTF8Encoding.Default.GetBytes(itemGridView.Cells(2).Text)
    '        '            Dim objCiudad = Encoding.UTF8.GetString(bytesCiudad)

    '        '            If Not objDatatable.Rows(itemGridView.DataItemIndex)(0) = gvprojectLocation.Rows(itemGridView.DataItemIndex).Cells(1).Text Then
    '        '                objDatatable.Rows.Add(itemGridView.Cells(1).Text, objDepartamento, objCiudad)
    '        '            End If

    '        '        Else
    '        '            objDatatable.Rows.Add(itemGridView.Cells(1).Text, Server.HtmlDecode(itemGridView.Cells(2).Text), Server.HtmlDecode(itemGridView.Cells(3).Text))
    '        '        End If


    '        '    Next

    '        '    Session.Add("projectLocationList", objDatatable)

    '        '    gvprojectLocation.DataSource = objDatatable
    '        '    gvprojectLocation.DataBind()


    '        'End If


    '    End If
    '    If HiddenFieldFsc.Value <> "" Then
    '        Me.txtvalortotalflow.Text = HiddenFieldFsc.Value
    '        Me.txtvalortotalflow.Text = Me.txtvalortotalflow.Text.Replace(".", ",")
    '    End If


    '    Session("valortotalflow") = Me.txtvalortotalflow.Text


    '    hdididea.Value = Me.ddlididea.SelectedValue
    '    Me.lblmensajeexitoerror.Text = Session("totporcentajes")
    '    Me.LblFlujodePagoPorcentajeIzquierda.Text = "Porcentaje de pagos asignados: "
    '    Me.LblFlujodePagoPorcentajeDerecha.Text = "% sobre 100%"
    'End Sub

    'Protected Sub ddltipoaprobacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltipoaprobacion.SelectedIndexChanged
    '    If Me.ddltipoaprobacion.SelectedValue.ToString = "3" Then
    '        tbpnAclaratorio.Visible = True
    '        Me.txtcounterpartvalue.ReadOnly = True
    '        Me.txtfsccontribution.ReadOnly = True
    '        Me.txtduration.ReadOnly = True
    '        'Me.txtstartdate.ReadOnly = True
    '        Me.txtresults.ReadOnly = True
    '        Me.txtobjective.ReadOnly = True
    '        Me.txtjustification.ReadOnly = True
    '        Me.txtzonedescription.ReadOnly = True
    '        Me.TextResultGestConocimiento.ReadOnly = True
    '        Me.TextResCapacidInstal.ReadOnly = True
    '    End If
    '    If Me.ddltipoaprobacion.SelectedValue.ToString = "2" Then
    '        tbpnAclaratorio.Visible = False
    '    End If
    '    'validar que si tipo de aprobacion es otro si se activen los checks
    '    If Me.ddltipoaprobacion.SelectedValue.ToString = "2" Then
    '        Me.checkvalor.Visible = True
    '        Me.checktiempo.Visible = True
    '        Me.checkalcance.Visible = True
    '        Me.lblmodifotrosi.Visible = True
    '    End If
    '    If Me.ddltipoaprobacion.SelectedValue.ToString = "3" Or Me.ddltipoaprobacion.SelectedValue.ToString = "1" Then
    '        Me.checkvalor.Visible = False
    '        Me.checktiempo.Visible = False
    '        Me.checkalcance.Visible = False
    '        Me.lblmodifotrosi.Visible = False
    '    End If





    'End Sub

    'Protected Sub gvprojectLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvprojectLocation.SelectedIndexChanged
    '    ' cargar la lista de ubicaciones
    '    Dim projectLocationList As List(Of ProjectLocationEntity) = Session("projectLocationList")

    '    ' eliminar la ubicación de la lista
    '    projectLocationList.RemoveAt(Me.gvprojectLocation.SelectedIndex)

    '    ' mostrar las ubicaciones en la grilla
    '    Me.gvprojectLocation.DataSource = projectLocationList
    '    Me.gvprojectLocation.DataBind()
    'End Sub

    'Protected Sub gvprojectLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvprojectLocation.SelectedIndexChanged
    '    Me.HDswich_ubicacion.Value = 1
    '    'definir los objetos
    '    Dim objProjectLocationList As DataTable = Session("projectLocationList")
    '    '     Dim objProjectLocation As New ProjectLocationEntity
    '    Dim repeated As Boolean = 0


    '    Dim index As Integer = 0
    '    '   Dim listIndex As List(Of Integer) = New List(Of Integer)()
    '    Try

    '        ' objProjectLocation.idcity = Me.gvprojectLocation.SelectedRow.Cells(1).Text
    '        ' Dim positionDataTableRow As Integer = 0
    '        ' For Each objproloc In objProjectLocationList.Rows

    '        ' If objproloc(0) = (objProjectLocation.idcity) Then
    '        '        repeated = 1
    '        '              listIndex.Add(positionDataTableRow)
    '        '            End If

    '        ' positionDataTableRow += 1
    '        '           Next

    '        '    If repeated Then
    '        '      For Each itemIndex As Integer In listIndex
    '        ' remover ubicacion seleccionada


    '        objProjectLocationList.Rows.RemoveAt(Me.gvprojectLocation.SelectedIndex)
    '        'Next

    '        ' mostrar
    '        Session("projectLocationList") = objProjectLocationList

    '        '  Me.gvprojectLocation.DataSource = Nothing
    '        '  Me.gvprojectLocation.DataBind()
    '        Me.gvprojectLocation.DataSource = objProjectLocationList
    '        Me.gvprojectLocation.DataBind()
    '        Me.lblmensajeexitoerror.Text = ""
    '        'End If

    '        Me.TabContainer1.ActiveTabIndex = 1

    '    Catch ex As Exception
    '        Me.lblmensajeexitoerror.Text = ex.Message
    '    End Try



    'End Sub

    Protected Sub btntermsreference_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntermsreference.Click

        Dim objProceeding_ReferenceTerms As Proceedings = New Proceedings()


        Dim ddlc, ddls, ddlp, name, just, objet, objetesp, resulb, resulgc, resulci, fech, dura, people, vt1, vt2, vt6 As String
        Dim FSCval As String = ""

        Dim dataidea As DataTable
        Dim dataactor As DataTable
        Dim pagos As DataTable
        Dim dataciudades As DataTable

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append("select distinct tcc.Contract from idea i    ")
        sql.Append("inner join ProgramComponentByIdea pi on (i.Id = pi.IdIdea)         ")
        sql.Append("inner join ProgramComponent pc on (pi.IdProgramComponent = pc.Id)  ")
        sql.Append("inner join Program p on (pc.IdProgram = p.Id)       ")
        sql.Append("INNER JOIN Project PJ ON (PJ.IdIdea= I.Id)              ")
        sql.Append("inner join StrategicLine l on (p.IdStrategicLine = l.Id)  ")
        sql.Append("inner join TypeContract tcc on  (tcc.Id=i.Idtypecontract)              ")
        sql.Append("where PJ.Id =" & Me.txtid.Text)

        dataidea = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If dataidea.Rows.Count > 0 Then
            If IsDBNull(dataidea.Rows(0)("contract")) = False Then
                objProceeding_ReferenceTerms.ArchivedRecord = dataidea.Rows(0)("contract")
            End If

        End If
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=ProyectExport.doc")
        Response.Charset = "UTF8Encoding"
        Response.ContentType = "application/vnd.ms-word "

        ' ddls = Me.txtlinestrat.Text
        'ddlp = Me.txtprograma.Text
        ddlc = objProceeding_ReferenceTerms.ArchivedRecord
        name = Me.txtname.Text
        just = Me.txtjustification.Text
        objet = Me.txtobjective.Text
        objetesp = Me.txtareadescription.Text
        resulb = Me.txtresults.Text
        resulgc = Me.txtresulgc.Text
        resulci = Me.txtresulci.Text
        '  fech = 'Me.txtstartdate.Text
        dura = Me.txtduration.Text
        'people = Me.ddlpopulation.SelectedItem.Text
        vt1 = Me.txtfsccontribution.Text
        vt2 = Me.txtcounterpartvalue.Text
        vt6 = Format(Convert.ToInt64(Me.txttotalcost.Text), "#,###.##")

        If objetesp = "" Then
            objetesp = "No aplica"
        End If
        If resulb = "" Then
            resulb = "No aplica"
        End If
        If resulgc = "" Then
            resulgc = "No aplica"
        End If
        If resulci = "" Then
            resulci = "No aplica"
        End If
        If dura = "" Then
            dura = "No aplica"
        End If
        If ddlc = "Seleccione...." Then
            ddlc = ""
        End If

        '------------------------------------------------------------
        Response.Output.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%"">")

        Response.Output.WriteLine("<body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>" & ddls.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Programa:</strong></span></td><td>" & ddlp.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre del proyecto :</strong></span></td><td>" & name.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No del Proyecto:</strong></span></td><td>" & Me.txtid.Text & "." & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>")


        sql = New StringBuilder

        sql.Append("select deptoname, cityname from ProjectLocation WHERE IdProject = " & Me.txtid.Text)
        dataciudades = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)
        Dim celda As Integer = 0
        If dataciudades.Rows.Count > 0 Then
            For Each ciudaditem In dataciudades.Rows


                If IsDBNull(dataciudades.Rows(celda)("deptoname")) = False Then
                    objProceeding_ReferenceTerms.location_t = dataciudades.Rows(celda)("deptoname")
                Else
                    objProceeding_ReferenceTerms.location_t = ""
                End If
                If IsDBNull(dataciudades.Rows(celda)("cityname")) = False Then
                    objProceeding_ReferenceTerms.VigencyExtend = dataciudades.Rows(celda)("cityname")
                Else
                    objProceeding_ReferenceTerms.VigencyExtend = ""
                End If

                Response.Output.WriteLine(objProceeding_ReferenceTerms.location_t.ToString() & ", " & objProceeding_ReferenceTerms.VigencyExtend.ToString() & " // ")
                celda = celda + 1

            Next

        End If

        Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Poblaci&oacute;n Beneficiaria:</strong></span></td><td>" & people.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Duraci&oacute;n en meses:</strong></span></td><td>" & dura.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Inicio:</strong></span></td><td>" & fech.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>" & vt6.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>" & ddlc.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre de la idea:</strong></span></td><td>" & Me.txtcode.Text & "</td></tr></tbody></table>")

        Response.Output.WriteLine("<p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Documento de Identidad</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr>")

        sql = New StringBuilder

        sql.Append("select t.Name,t.contact,tp.Type,T.email,T.phone,T.documents from Third t    ")
        sql.Append("inner join ThirdByProject tp on tp.IdThird= t.Id              ")
        sql.Append("INNER JOIN Project P ON P.Id= tp.IdProject                  ")
        sql.Append("where tp.IdProject = " & Me.txtid.Text)

        dataactor = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim celdaactor As Integer = 0
        If dataactor.Rows.Count > 0 Then

            For Each actoritem In dataactor.Rows

                If IsDBNull(dataactor.Rows(celdaactor)("name")) = False Then
                    objProceeding_ReferenceTerms.actors_t = dataactor.Rows(celdaactor)("name")
                End If
                ' captura contacto
                If IsDBNull(dataactor.Rows(celdaactor)("contact")) = False Then
                    objProceeding_ReferenceTerms.contact_t = dataactor.Rows(celdaactor)("contact")
                End If
                ' captura tipo
                If IsDBNull(dataactor.Rows(celdaactor)("type")) = False Then
                    objProceeding_ReferenceTerms.tipo_t = dataactor.Rows(celdaactor)("type")
                End If
                ' captura email
                If IsDBNull(dataactor.Rows(celdaactor)("email")) = False Then
                    objProceeding_ReferenceTerms.email_t = dataactor.Rows(celdaactor)("email")
                End If
                ' captura telefono
                If IsDBNull(dataactor.Rows(celdaactor)("phone")) = False Then
                    objProceeding_ReferenceTerms.phone_t = dataactor.Rows(celdaactor)("phone")
                End If
                ' captura documento
                If IsDBNull(dataactor.Rows(celdaactor)("documents")) = False Then
                    objProceeding_ReferenceTerms.document_t = dataactor.Rows(celdaactor)("documents")
                End If


                Response.Output.WriteLine("<tr><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.actors_t.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.tipo_t & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.contact_t.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.document_t.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.phone_t.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.email_t & "</tr>")

                celdaactor = celdaactor + 1
            Next

        End If

        Response.Output.WriteLine("</tbody></table><p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">JUSTIFICAC&Iacute;ON:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & just.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVO:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""71"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objet.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVOS ESPEC&Iacute;FICOS:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""82"" style=""width: 100%;"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objetesp.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">RESULTADOS ESPERADOS:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Beneficiarios:</span></strong></td><td style=""text-align: justify;"">" & resulb.ToString() & "</td></tr><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Gesti&oacute;n del conocimiento*:</span></strong></td><td style=""text-align: justify;"">" & resulgc.ToString() & "</td></tr><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Capacidad instalada:</span></strong></td><td style=""text-align: justify;"">" & resulci.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">PRESUPUESTO GENERAL:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Valor Total del contrato:</span></strong></td><td>" & vt6.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de los Socios (Efectivo y Especie):</span></strong></td><td>" & vt2.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de la FSC (Efectivo y Especie):</span></strong></td><td>" & vt1.ToString() & "</td></tr></tbody></table>")

        Response.Output.WriteLine("<span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span>")

        Response.Output.WriteLine("<p><u><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">CRONOGRAMA DE PAGOS</span></strong></u></p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" height=""125"" width=""100%""><tbody><tr><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor</strong></span></td><td style=""width: 5%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>%</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Origen de los Recursos</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contraentrega</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha</strong></span></td></tr>")

        sql = New StringBuilder

        sql.Append("select valorparcial, porcentaje,entregable,fecha from Paymentflow where idproject= " & Me.txtid.Text)
        pagos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        Dim celdapago As Integer = 0
        Dim cont As Integer = 0
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

                cont = celdapago + 1

                Response.Output.WriteLine("<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong> Desembolso " & cont & "  </strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToDecimal(objProceeding_ReferenceTerms.AdditionValue), "#,###.##") & "</td><td style=""width: 5%;"">" & objProceeding_ReferenceTerms.AdditionValueLetters.ToString() & "</td><td style=""width: 16%;""></td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.Adition.ToString() & "</td><td style=""width: 16%;"">" & objProceeding_ReferenceTerms.AdditionDate.ToString() & "</td></tr><tr>")

                celdapago = celdapago + 1

            Next

            sql = New StringBuilder

            sql.Append("select sum(valorparcial) from Paymentflow where idproject =" & Me.txtid.Text)
            Dim valtpagos = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            Response.Output.WriteLine("<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Total</strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToInt64(valtpagos), "#,###.##") & "</td><td style=""width: 5%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>100%</strong></span></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td></tr><tr>")

        End If

        Response.Output.WriteLine("</tbody></table><p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p>")

        Response.Flush()
        Response.End()

    End Sub

    Public Function Export_Project()

        Dim id_project As String

        Dim objProceeding_ReferenceTerms As Proceedings = New Proceedings()

        Dim ddlc, ddls, ddlp, name, just, objet, objetesp, resulb, resulgc, resulci, fech, dura, people, vt1, vt2, vt6 As String
        Dim aplica_iva, ruta, riesgos, mitigacion, obligaciones, dia, resulotros, vt3, fuent, est, datanex, dept, munip, actor, action, vt4, vt5, active As String

        Dim FSCval As String = ""

        Dim Data_component_group, Data_idea, Data_pagos, Data_pagos_detalles, Data_detalles_actores, Data_totales_actors, Data_componet, Data_others, Data_ubicacion As DataTable

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=ProyectExport.doc")
        Response.Charset = "UTF8Encoding"
        Response.ContentType = "application/vnd.ms-word "

        Dim id_proyect_str = Request.QueryString("id")

        'validamos el id del proyecto si es edicion o creacion
        If id_proyect_str = "" Then

            sql.Append("select MAX(id) from Project")
            id_project = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Else

            id_project = id_proyect_str

        End If

        '----------------------------- sentencia para traer todos los datos del proyecto de la pestaña descripcion del proyecto ------------------------------
        sql = New StringBuilder

        sql.Append(" select ididea ,Name,Justification,Objective,ZoneDescription,results,ResultsKnowledgeManagement,ResultsInstalledCapacity,OtherResults,BeginDate,Duration,days,TotalCost,obligationsoftheparties,RiskMitigation,RisksIdentified,BudgetRoute,ideaappliesIVA from Project")
        sql.Append(" where id = " & id_project)

        ' ejecutar la intruccion
        Data_idea = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'cargamos datos de la consulta a las variables
        If Data_idea.Rows.Count > 0 Then

            If IsDBNull(Data_idea.Rows(0)("Name")) = False Then
                name = Data_idea.Rows(0)("Name")
            Else
                name = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("Justification")) = False Then
                just = Data_idea.Rows(0)("Justification")
            Else
                just = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("Objective")) = False Then
                objet = Data_idea.Rows(0)("Objective")
            Else
                objet = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("ZoneDescription")) = False Then
                objetesp = Data_idea.Rows(0)("ZoneDescription")
            Else
                objetesp = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("results")) = False Then
                resulb = Data_idea.Rows(0)("results")
            Else
                resulb = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("ResultsKnowledgeManagement")) = False Then
                resulgc = Data_idea.Rows(0)("ResultsKnowledgeManagement")
            Else
                resulgc = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("ResultsInstalledCapacity")) = False Then
                resulci = Data_idea.Rows(0)("ResultsInstalledCapacity")
            Else
                resulci = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("OtherResults")) = False Then
                resulotros = Data_idea.Rows(0)("OtherResults")
            Else
                resulotros = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("BeginDate")) = False Then
                fech = Data_idea.Rows(0)("BeginDate")
            Else
                fech = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("Duration")) = False Then
                dura = Data_idea.Rows(0)("Duration")
            Else
                dura = 0
            End If

            If IsDBNull(Data_idea.Rows(0)("days")) = False Then
                dia = Data_idea.Rows(0)("days")
            Else
                dia = 0
            End If

            If IsDBNull(Data_idea.Rows(0)("TotalCost")) = False Then
                vt3 = Data_idea.Rows(0)("TotalCost")
                vt6 = Data_idea.Rows(0)("TotalCost")
            Else
                vt3 = 0
                vt6 = 0
            End If

            If IsDBNull(Data_idea.Rows(0)("obligationsoftheparties")) = False Then
                obligaciones = Data_idea.Rows(0)("obligationsoftheparties")
            Else
                obligaciones = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("RiskMitigation")) = False Then
                mitigacion = Data_idea.Rows(0)("RiskMitigation")
            Else
                mitigacion = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("RisksIdentified")) = False Then
                riesgos = Data_idea.Rows(0)("RisksIdentified")
            Else
                riesgos = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("BudgetRoute")) = False Then
                ruta = Data_idea.Rows(0)("BudgetRoute")
            Else
                ruta = ""
            End If

            If IsDBNull(Data_idea.Rows(0)("ideaappliesIVA")) = False Then
                aplica_iva = Data_idea.Rows(0)("ideaappliesIVA")
            Else
                aplica_iva = 0
            End If

        End If

        '----------------- consulta para saber el valor del aporte de la fsc---------------------------------------------------------------------

        sql = New StringBuilder

        sql.Append(" select convert(bigint,REPLACE(tp.FSCorCounterpartContribution,'.','')) from Project p  ")
        sql.Append(" inner join ThirdByProject tp on p.Id = tp.IdProject ")
        sql.Append(" inner join Third t on t.Id = tp.IdThird ")
        sql.Append(" where (t.code = '8600383389' And tp.IdProject =  " & id_project & ")")

        FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If FSCval = 0 Then
            vt5 = "0"
        Else
            vt5 = Format(Convert.ToInt64(FSCval), "#,###.##")
        End If

        '----------------- consulta para saber el valor del aporte de los otros actores en total------------------------------------------------------

        sql = New StringBuilder

        sql.Append(" select sum(convert(bigint,REPLACE(tp.FSCorCounterpartContribution,'.',''))) from Project p ")
        sql.Append(" inner join ThirdByProject tp on p.Id = tp.IdProject ")
        sql.Append(" inner join Third t on t.Id = tp.IdThird ")
        sql.Append(" where (t.code <> '8600383389' And tp.IdProject = " & id_project & ")")

        Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If otrosval = 0 Then
            vt4 = "0"
        Else
            vt4 = Format(Convert.ToInt64(otrosval), "#,###.##")
        End If

        '---------------------- consulta para saber la linea estrategica del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append(" select p.Id, pr.Name as objetivo, l.Name as linea_estra from Project p ")
        sql.Append(" inner join ProgramComponentByProject pcp on pcp.IdProject= p.Id ")
        sql.Append(" inner join ProgramComponent pc on pc.Id = pcp.IdProgramComponent ")
        sql.Append(" inner join Program pr on pr.Id = pc.IdProgram ")
        sql.Append(" inner join StrategicLine l on l.Id = pr.IdStrategicLine ")

        sql.Append(" where p.id = " & id_project)

        Data_componet = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'cargamos datos de la consulta a las variables
        If Data_componet.Rows.Count > 0 Then

            If IsDBNull(Data_componet.Rows(0)("linea_estra")) = False Then
                ddls = Data_componet.Rows(0)("linea_estra")
            Else
                ddls = ""
            End If

        End If

        '---------------------- consulta para saber los objetivos estrategica del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append(" select distinct p.Code as objetivo_estrategico from ProgramComponentByProject pcp  ")
        sql.Append(" inner join ProgramComponent pc on pcp.IdProgramComponent = pc.Id ")
        sql.Append(" inner join Program P ON P.Id = pc.IdProgram ")

        sql.Append(" where pcp.IdProject = " & id_project)

        Data_component_group = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------- consulta para saber el tipo de contrato y poblacion del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append(" select  tp.Contract, po.Pupulation  from Project p ")
        sql.Append(" inner join typecontract tp on tp.id = p.Idtypecontract ")
        sql.Append(" inner join Population po on po.Id= p.Population ")

        sql.Append(" where p.id = " & id_project)

        Data_others = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'cargamos datos de la consulta a las variables
        If Data_others.Rows.Count > 0 Then

            If IsDBNull(Data_others.Rows(0)("Contract")) = False Then
                ddlc = Data_others.Rows(0)("Contract")
            Else
                ddlc = ""
            End If

            If IsDBNull(Data_others.Rows(0)("Pupulation")) = False Then
                people = Data_others.Rows(0)("Pupulation")
            Else
                people = ""
            End If

        End If

        '---------------------- consulta para saber los actores del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append(" select tp.Name, tp.Type, tp.Contact,tp.Documents,tp.Phone, tp.Email, ")
        sql.Append(" tp.Vrmoney, tp.VrSpecies, tp.FSCorCounterpartContribution, tp.generatesflow ")
        sql.Append(" from ThirdByProject tp where tp.IdProject = " & id_project)

        Data_detalles_actores = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '--------------------- consulta para saber las ubicaciones del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append(" select dep.Name as dapartamento, c.Name as municipio from ProjectLocation pl ")
        sql.Append(" inner join FSC_eSecurity.dbo.depto dep on dep.id = pl.IdCity ")
        sql.Append(" inner join FSC_eSecurity.dbo.City c on c.ID = pl.iddepto ")
        sql.Append(" where pl.IdProject = " & id_project)

        Data_ubicacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'validamos los campos q vienen vacios
        If objetesp = "" Then
            objetesp = "No Aplica"
        End If
        If resulb = "" Then
            resulb = "No Aplica"
        End If
        If resulgc = "" Then
            resulgc = "No Aplica"
        End If
        If resulci = "" Then
            resulci = "No Aplica"
        End If
        If dura = "" Then
            dura = "No Aplica"
        End If
        If obligaciones = "" Then
            obligaciones = "No Aplica"
        End If
        If mitigacion = "" Then
            mitigacion = "No Aplica"
        End If
        If riesgos = "" Then
            riesgos = "No Aplica"
        End If
        If ruta = "" Then
            ruta = "No Aplica"
        End If

        '--------------------- consulta para saber los totales de los valores de los actores del proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append("select sum(cast(replace(Vrmoney,'.','') as int))as v_money, sum(cast(replace(VrSpecies,'.','') as int)) as v_especie,sum(cast(replace(FSCorCounterpartContribution,'.','') as int)) as V_total from ThirdByProject where IdProject = " & id_project)
        Data_totales_actors = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '--------------------- consulta para saber los detalles de los flujos de pagos generados en el proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append("select N_pagos,valorparcial, porcentaje,entregable,fecha  from Paymentflow where idproject = " & id_project)
        Data_pagos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '--------------------- consulta para saber el total de los flujos de pagos generados en el proyecto derivado --------------------------------

        sql = New StringBuilder

        sql.Append("select sum(valorparcial) from Paymentflow where idproject = " & id_project)
        Dim valtpagos = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)


        'construimos el documento html
        Response.Output.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%"">")

        Response.Output.WriteLine("<body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 20%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>" & ddls.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Objetivos Estrat&eacute;gicos:</strong></span></td><td>")

        'istanciamos valores iniciales
        Dim valuar_compo As Integer = Data_component_group.Rows.Count
        valuar_compo = valuar_compo - 1
        Dim celda_componente As Integer = 0

        'cargamos en variables de los componentes ingresados en el proyecto
        If Data_component_group.Rows.Count > 0 Then

            For Each det_componente In Data_component_group.Rows

                If IsDBNull(Data_component_group.Rows(celda_componente)("objetivo_estrategico")) = False Then
                    ddlp = Data_component_group.Rows(celda_componente)("objetivo_estrategico")
                End If

                If valuar_compo = celda_componente Then
                    Response.Output.WriteLine(ddlp)
                Else
                    Response.Output.WriteLine(ddlp & ", ")
                End If

                celda_componente = celda_componente + 1

            Next

        End If

        Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre del Proyecto:</strong></span></td><td>" & name.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>")

        'istanciamos valores iniciales
        Dim lbldepto, lblcity As String
        Dim valuar_ubi As Integer = Data_ubicacion.Rows.Count
        valuar_ubi = valuar_ubi - 1
        Dim celda_ubicacion As Integer = 0

        'cargamos en variables de las ubicaciones ingresadas en el proyecto
        If Data_ubicacion.Rows.Count > 0 Then

            For Each det_actor In Data_ubicacion.Rows

                If IsDBNull(Data_ubicacion.Rows(celda_ubicacion)("dapartamento")) = False Then
                    lbldepto = Data_ubicacion.Rows(celda_ubicacion)("dapartamento")
                End If

                If IsDBNull(Data_ubicacion.Rows(celda_ubicacion)("municipio")) = False Then
                    lblcity = Data_ubicacion.Rows(celda_ubicacion)("municipio")
                End If

                If valuar_ubi = celda_ubicacion Then
                    Response.Output.WriteLine(lbldepto.ToString() & ", " & lblcity.ToString())
                Else
                    Response.Output.WriteLine(lbldepto.ToString() & ", " & lblcity.ToString() & " || ")
                End If

                celda_ubicacion = celda_ubicacion + 1

            Next

        End If

        Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Poblaci&oacute;n Objetivo:</strong></span></td><td>" & people.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Inicio:</strong></span></td><td>" & fech.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Duraci&oacute;n en meses:</strong></span></td><td> Meses: " & dura.ToString() & " Dias: " & dia.ToString() & "</td></tr>")

        'utilizamos la funcion q nos calcula  la fecha final del proyecto
        Dim fechafinal = calculafechas(Convert.ToDateTime(fech), dura, dia)

        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Finalizaci&oacute;n:</strong></span></td><td>" & Convert.ToString(fechafinal) & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>" & ddlc.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Ruta Presupuestal:</strong></span></span></td><td>" & ruta.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>" & Format(Convert.ToInt64(vt3), "#,###.##") & "</td></tr>")

        'validamos si aplica iva y lo traducimos
        If aplica_iva = 1 Then
            aplica_iva = "Si"
        Else
            aplica_iva = "No"
        End If

        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Aplica IVA:</strong></span></td><td> " & aplica_iva.ToString() & " </td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No. de proyecto:</strong></span></td><td>" & id_project.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr>")

        'istanciamos valores iniciales
        Dim lblname, labelcontact, labeldocument, labeltype, labelphone, labelemail As String
        Dim celda_det_actors_dat As Integer = 0

        'cargamos en variables de los datos de los actores ingresados en el proyecto
        If Data_detalles_actores.Rows.Count > 0 Then

            For Each det_actor In Data_detalles_actores.Rows

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Name")) = False Then
                    lblname = Data_detalles_actores.Rows(celda_det_actors_dat)("Name")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Type")) = False Then
                    labeltype = Data_detalles_actores.Rows(celda_det_actors_dat)("Type")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Contact")) = False Then
                    labelcontact = Data_detalles_actores.Rows(celda_det_actors_dat)("Contact")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Documents")) = False Then
                    labeldocument = Data_detalles_actores.Rows(celda_det_actors_dat)("Documents")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Phone")) = False Then
                    labelphone = Data_detalles_actores.Rows(celda_det_actors_dat)("Phone")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors_dat)("Email")) = False Then
                    labelemail = Data_detalles_actores.Rows(celda_det_actors_dat)("Email")
                End If

                Response.Output.WriteLine("<tr><td style=""width: 16%;"">" & lblname.ToString() & "</td><td style=""width: 16%;"">" & labeltype.ToString() & "</td><td style=""width: 16%;"">" & labelcontact.ToString() & "</td><td style=""width: 16%;"">" & labelphone.ToString() & "</td><td style=""width: 16%;"">" & labelemail.ToString() & "</tr>")

                celda_det_actors_dat = celda_det_actors_dat + 1

            Next

        End If

        Response.Output.WriteLine("</tbody></table><p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">JUSTIFICAC&Iacute;ON:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & just.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVO GENERAL:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""71"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objet.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVOS ESPEC&Iacute;FICOS:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""82"" style=""width: 100%;"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objetesp.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">RESULTADOS ESPERADOS:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Beneficiarios:</span></strong></td><td style=""text-align: justify;"">" & resulb.ToString() & "</td></tr><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Gesti&oacute;n del conocimiento:</span></strong></td><td style=""text-align: justify;"">" & resulgc.ToString() & "</td></tr><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Capacidad instalada:</span></strong></td><td style=""text-align: justify;"">" & resulci.ToString() & "</td></tr><tr>")
        Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Otros:</span></strong></td><td style=""text-align: justify;"">" & resulotros.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("</tbody></table><p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBLIGACIONES DE LAS PARTES:</span></strong></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & obligaciones.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">PRESUPUESTO GENERAL:</span></strong></p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""text-align: center;""><b><span lang=""ES"" style=""font-size: 12pt; line-height: 115%; font-family: 'Times New Roman', serif;"">Actor</span></b></td><td style=""text-align: center;""><b><span lang=""ES"" style=""font-size: 12pt; line-height: 115%; font-family: 'Times New Roman', serif;"">Efectivo</span></b></td><td style=""text-align: center;""><b><span lang=""ES"" style=""font-size: 12pt; line-height: 115%; font-family: 'Times New Roman', serif;"">Especie</span></b></td><td style=""text-align: center;""><b><span lang=""ES"" style=""font-size: 12pt; line-height: 115%; font-family: 'Times New Roman', serif;"">Total Aporte</span></b></td></tr>")

        'istanciamos valores iniciales
        Dim celda_det_actors As Integer = 0
        Dim name_actor, V_Efectivo, V_Especie, V_total, T_efectivo, T_especies, T_total, flujos_gene As String

        'cargamos en variables de los valores de los actores ingresados en el proyecto
        If Data_detalles_actores.Rows.Count > 0 Then

            For Each det_actor In Data_detalles_actores.Rows

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors)("Name")) = False Then
                    name_actor = Data_detalles_actores.Rows(celda_det_actors)("Name")
                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors)("Vrmoney")) = False Then
                    V_Efectivo = Data_detalles_actores.Rows(celda_det_actors)("Vrmoney")

                    If V_Efectivo = "" Then
                        V_Efectivo = 0
                    End If

                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors)("VrSpecies")) = False Then
                    V_Especie = Data_detalles_actores.Rows(celda_det_actors)("VrSpecies")

                    If V_Especie = "" Then
                        V_Especie = 0
                    End If

                End If

                If IsDBNull(Data_detalles_actores.Rows(celda_det_actors)("FSCorCounterpartContribution")) = False Then
                    V_total = Data_detalles_actores.Rows(celda_det_actors)("FSCorCounterpartContribution")

                    If V_total = "" Then
                        V_total = 0
                    End If
                End If

                Response.Output.WriteLine("<tr><td>" & name_actor & "</td><td  style=""text-align: center;"">" & V_Efectivo & "</td><td  style=""text-align: center;"">" & V_Especie & "</td><td  style=""text-align: center;"">" & V_total & "</td></tr>")

                celda_det_actors = celda_det_actors + 1

            Next

        End If

        'cargamos en variables de totales de los actores ingresados en el proyecto
        If Data_totales_actors.Rows.Count > 0 Then

            If IsDBNull(Data_totales_actors.Rows(0)("v_money")) = False Then
                T_efectivo = Data_totales_actors.Rows(0)("v_money")
            End If

            If IsDBNull(Data_totales_actors.Rows(0)("v_especie")) = False Then
                T_especies = Data_totales_actors.Rows(0)("v_especie")
            End If

            If IsDBNull(Data_totales_actors.Rows(0)("V_total")) = False Then
                T_total = Data_totales_actors.Rows(0)("V_total")
            End If

        End If

        Response.Output.WriteLine("<tr><td style=""text-align: center;""><b><span lang=""ES"" style=""font-size: 12pt; line-height: 115%; font-family: 'Times New Roman', serif;"">Total</span></b></td><td style=""text-align: center;""> " & Format(Convert.ToInt64(T_efectivo), "#,###.##") & "</td><td style=""text-align: center;""> " & Format(Convert.ToInt64(T_especies), "#,###.##") & "</td><td style=""text-align: center;""> " & Format(Convert.ToInt64(T_total), "#,###.##") & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span>")
        Response.Output.WriteLine("<p><u><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">FLUJOS DE PAGOS</span></strong></u></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 20%;""><strong>*Aporte de:</strong></td><td>")

        'istanciamos valores iniciales
        name_actor = ""
        Dim name_str As String
        Dim celdanombre As Integer = 0

        ' cargamos en variables los nombres de actores ingresados en el proyecto que tienen flujos de pagos
        If Data_detalles_actores.Rows.Count > 0 Then

            Dim valuar As Integer = Data_detalles_actores.Rows.Count
            valuar = valuar - 1

            For Each Eachnombreitem In Data_detalles_actores.Rows
                name_actor = Data_detalles_actores.Rows(celdanombre)("Name")
                flujos_gene = Data_detalles_actores.Rows(celdanombre)("generatesflow")

                If flujos_gene = "s" Then

                    If IsDBNull(Data_detalles_actores.Rows(celdanombre)("Name")) = False Then
                        name_actor = Data_detalles_actores.Rows(celdanombre)("Name")
                    End If

                    If valuar = celdanombre Then
                        name_str &= name_actor
                    Else
                        name_str &= name_actor & ", "
                    End If

                End If

                celdanombre = celdanombre + 1
            Next

        End If

        Response.Output.WriteLine(name_str & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" height=""125"" width=""100%""><tbody><tr><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor</strong></span></td><td style=""width: 5%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>%</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Origen de los Recursos</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contraentrega</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha</strong></span></td></tr>")

        'istanciamos valores iniciales
        Dim celdapago As Integer = 0
        Dim celdadetalle As Integer = 0
        Dim valorp, porsent, entregp, fechap, np, detalles, aport, desem As String

        ' cargamos en variables los flujos pagos ingresados en el proyecto derivado
        If Data_pagos.Rows.Count > 0 Then

            For Each pagoitem In Data_pagos.Rows

                If IsDBNull(Data_pagos.Rows(celdapago)("N_pagos")) = False Then
                    np = Data_pagos.Rows(celdapago)("N_pagos")

                    detalles = ""
                    celdadetalle = 0

                    '-------- consulta para saber los detalles segun el numero de pago generados en el proyecto derivado --------------------------------
                    sql = New StringBuilder

                    sql.Append("select n_pago, Aportante, Desembolso from Detailedcashflows where Idproject = " & id_project & " and N_pago = " & np)
                    Data_pagos_detalles = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                    ' cargamos en variables los detalles de los flujos pagos ingresados en el proyecto derivado
                    If Data_pagos_detalles.Rows.Count > 0 Then

                        Dim valuar_det As Integer = Data_pagos_detalles.Rows.Count
                        valuar_det = valuar_det - 1

                        For Each detalleitem In Data_pagos_detalles.Rows

                            If IsDBNull(Data_pagos_detalles.Rows(celdadetalle)("Aportante")) = False Then
                                aport = Data_pagos_detalles.Rows(celdadetalle)("Aportante")
                            End If
                            If IsDBNull(Data_pagos_detalles.Rows(celdadetalle)("Desembolso")) = False Then
                                desem = Data_pagos_detalles.Rows(celdadetalle)("Desembolso")
                            End If

                            If valuar_det = celdadetalle Then

                                detalles &= aport & " valor: " & desem
                            Else

                                detalles &= aport & " valor: " & desem & " || "
                            End If

                            celdadetalle = celdadetalle + 1
                        Next
                    End If

                End If
                If IsDBNull(Data_pagos.Rows(celdapago)("valorparcial")) = False Then
                    valorp = Data_pagos.Rows(celdapago)("valorparcial")
                End If
                If IsDBNull(Data_pagos.Rows(celdapago)("porcentaje")) = False Then
                    porsent = Data_pagos.Rows(celdapago)("porcentaje")
                End If
                If IsDBNull(Data_pagos.Rows(celdapago)("entregable")) = False Then
                    entregp = Data_pagos.Rows(celdapago)("entregable")
                End If
                If IsDBNull(Data_pagos.Rows(celdapago)("fecha")) = False Then
                    fechap = Data_pagos.Rows(celdapago)("fecha")
                End If

                Response.Output.WriteLine("<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong> Desembolso " & np & "  </strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToDecimal(valorp), "#,###.##") & "</td><td style=""width: 5%;"">" & porsent.ToString() & "</td><td style=""width: 16%;"">" & detalles & "</td><td style=""width: 16%;"">" & entregp.ToString() & "</td><td style=""width: 16%;"">" & fechap.ToString() & "</td></tr><tr>")

                celdapago = celdapago + 1

            Next

            Response.Output.WriteLine("<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Total</strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToInt64(valtpagos), "#,###.##") & "</td><td style=""width: 5%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>100%</strong></span></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td></tr><tr>")

        End If

        Response.Output.WriteLine("</tbody></table><p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%;"">" & mitigacion.ToString() & "</td><td>" & riesgos.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p>")

        Response.Flush()
        Response.End()

    End Function


    Protected Function getDateFinalization(ByVal duracion As Double, ByVal inicio As String) As String

        Dim objResult As String

        Try

            Dim arrdias() As String
            Dim decimas As String
            Dim dias As Double

            'Cambiar puntos por comas
            duracion = Replace(duracion, ".", ",", 1)

            'Calcular los dias


            arrdias = Split(duracion, ",", , CompareMethod.Text)

            If UBound(arrdias) > 0 Then
                decimas = "0," & arrdias(1)
                dias = CInt(decimas * 30)
            Else
                dias = 0
            End If

            Dim fechafinal As Date
            'calcular la fecha final
            fechafinal = CDate(inicio)
            Dim tipointervalo As DateInterval
            tipointervalo = DateInterval.Day

            'Agregar los meses a la fecha
            Dim finalizacionpre As String = DateAdd(DateInterval.Month, CInt(duracion), fechafinal)
            finalizacionpre = CDate(finalizacionpre)

            'Agregar los días a la fecha
            Dim finalizacion As String = DateAdd("d", dias, finalizacionpre)
            finalizacion = CDate(finalizacion)
            Dim quitadia As String = DateAdd("d", -1, finalizacion)
            Dim fechaok As DateTime = quitadia

            objResult = fechaok.ToString("yyyy/MM/dd")

        Catch ex As Exception

            objResult = ""

        End Try

        Return objResult
    End Function

    ' funcion que calcula fecha final segun duracion en meses
    Public Function calculafechas(ByVal fecha As DateTime, ByVal duracion As String, ByVal dias_ope As String) As String

        Dim objResult As String

        Try

            Dim arrdias() As String
            Dim decimas As String
            Dim dias As Double
            Dim meses As Double

            'Cambiar puntos por comas
            duracion = Replace(duracion, ".", ",", 1)

            'Calcular los dias
            arrdias = Split(duracion, ",", , CompareMethod.Text)

            If UBound(arrdias) > 0 Then
                decimas = "0," & arrdias(1)
                dias = CInt(decimas * 30)
                meses = CInt(arrdias(0))
            Else
                meses = duracion
                If dias_ope = "" Then
                    dias = 0
                Else
                    dias = dias_ope
                End If

            End If

            Dim fechafinal As Date
            'calcular la fecha final
            fechafinal = CDate(fecha)
            Dim tipointervalo As DateInterval
            tipointervalo = DateInterval.Day

            'Agregar los meses a la fecha
            Dim finalizacionpre As String = DateAdd(DateInterval.Month, meses, fechafinal)
            finalizacionpre = CDate(finalizacionpre)

            'Agregar los días a la fecha
            Dim finalizacion As String = DateAdd("d", dias, finalizacionpre)
            finalizacion = CDate(finalizacion)

            Dim quitadia As String = DateAdd("d", -1, finalizacion)

            Dim fechaok As DateTime = quitadia

            objResult = fechaok.ToString("dd/MM/yyyy")

        Catch ex As Exception

            objResult = ""

        End Try

        Return objResult

    End Function

    'Protected Sub btnadanexo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadanexo.Click


    '    'Definiendo los objtetos
    '    Dim strFileName() As String
    '    Dim fileName As String = String.Empty
    '    Dim files As HttpFileCollection = Request.Files
    '    Dim DocumentsTmpList As New List(Of DocumentstmpEntity)

    '    'Se verifica que existan archivos por cargar
    '    If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

    '        'Se verifica la opción actual
    '        If (Request.QueryString("op").Equals("add")) Then

    '            'Se instancia la lista de documentos temporales


    '        Else
    '            'Se recupera la lista de documentos de la variable de sesion
    '            'If (Me.DocumentsList Is Nothing) Then
    '            '    Dim DocumentsTmpList As List(Of DocumentstmpEntity)
    '            'Else
    '            '    objProject.DOCUMENTLIST = Me.DocumentsList
    '            'End If

    '        End If

    '        'Se recorre la lista de archivos cargados al servidor
    '        For i As Integer = 0 To files.Count - 1

    '            Dim file As HttpPostedFile = files(i)

    '            If file.ContentLength > 0 Then

    '                strFileName = file.FileName.Split("\".ToCharArray)

    '                ' dar nombre al anexo
    '                fileName = Now.ToString("yyyyMMddhhmmss") & "_" & strFileName(strFileName.Length - 1)

    '                ' determinanado la ruta destino
    '                Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\" & fileName

    '                'Subiendo el archivo al server
    '                file.SaveAs(sFullPath)

    '                'Se instancia un objeto de tipo documento y se pobla con la info. reuqerida.
    '                Dim objDocument As New DocumentstmpEntity()
    '                objDocument.namefile = fileName


    '                'Se agrega el objeto de tipo documento a la lista de documentos
    '                DocumentsTmpList.Add(objDocument)
    '                Session("DocumentsTmp") = DocumentsTmpList



    '            End If

    '        Next

    '    End If


    'End Sub

    Protected Function getThirdById(ByVal id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As DataTable
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de terceros por proyectos de tipo Thirdbyproject
            Dim objListThirdByProject As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()

            'consulta de los datos de actores por id
            sql.Append("select top 1 t.name, tp.type, t.contact, t.documents, t.phone,  t.email, t.id from Third t ")
            sql.Append("inner join ThirdByProject tp on t.Id= tp.IdThird ")

            sql.Append("where t.Id = " & id)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getThirdBySession(ByVal idproject As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As DataTable
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable

            'consulta de los datos de actores por id del pproyecto
            sql.Append("select t.name , tp.type  , t.contact, t.documents, t.phone, t.email, tp.Id  from Third t ")
            sql.Append("inner join ThirdByProject tp on t.Id= tp.IdThird ")

            sql.Append("where tp.Idproject = " & idproject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function insertThirdProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
     ByVal idthird As Integer, ByVal idproject As Integer, ByVal type As String) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            sql.AppendLine("INSERT INTO ThirdByProject(" & _
             "idproject," & _
             "Type, " & _
             "idThird" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & idproject & "',")
            sql.AppendLine("'" & type & "',")
            sql.AppendLine("'" & idthird & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            ' finalizar la transaccion
            'CtxSetComplete()

            ' retornar
            Return num

        Catch ex As Exception

            ' cancelar la transaccion
            'CtxSetAbort()

            ' publicar el error
            'GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            'ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al insertar el ThirdByProject. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    Public Function searchComponentsProgram(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable


        'consulta de los datos de componentes programa por id
        sql.Append("SELECT pc.Code FROM ProgramComponentByIdea pci JOIN ProgramComponent pc  ")
        sql.Append(" ON(pc.Id=pci.IdProgramcomponent)")
        sql.Append("WHERE pci.IdIdea =" & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String
        html = "<table  style=""font-family: 'Calibri';"" border=1 cellspacing=0 cellpadding=2 bordercolor=""666633"" >"
        html &= " <tr>"
        html &= " <td colspan=""6"">"
        html &= " <h2 align=center> Componentes de programa </h2>"
        html &= " </tr>"
        html &= " </td>"
        html &= " <tr align=""center""><td style=""width: 200px"">COMPONENTES DE PROGRAMA</td></tr>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 500px"" >{0}</td></tr>", itemDataTable(0))
        Next
        html &= " </table>"




        Return html
    End Function

    Public Function queryComponentsProgram(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Try
            'consulta de los datos de componentes programa por id
            sql.Append("SELECT pc.Code, pc.id FROM ProgramComponentByIdea pci JOIN ProgramComponent pc  ")
            sql.Append(" ON(pc.Id=pci.IdProgramcomponent)")
            sql.Append("WHERE pci.IdIdea =" & ididea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function deleteThirdByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByProject As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ThirdByProject ")

            SQL.AppendLine(" where id = " & idThirdByProject & " ")


            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            'CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            'CtxSetAbort()

            ' publicar el error
            'GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            'ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al elimiar el ThirdByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    Public Function getCodeGroup(ByVal idGroup As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Try
            'consulta del codigo del grupo
            sql.Append(" USE [FSC_eSecurity] ")
            sql.Append("SELECT ug.Code, ug.Name FROM UserGroup ug ")
            sql.Append("WHERE ug.id =" & idGroup)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Protected Function getRecruitment(ByVal idproject As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As DataTable
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable

            'consulta de los datos de actores por id del pproyecto
            sql.Append("select IdProject from ContractRequest  ")
            sql.Append("where Idproject = " & idproject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class

