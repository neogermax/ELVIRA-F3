Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Partial Class addIdea
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

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim facade As New Facade

            ' cargar los combos
            'loadCombos()
            '  Me.ddlactors.Items.Insert(0, New ListItem("Seleccione...", "-1"))
            ' Me.ddlactors.SelectedValue = "-1"


            'verificamos que la idea no este aprobada para que no se pueda modificar

            If Not op.Equals("show") And facade.verifyapprove(applicationCredentials, "IdeaEntity", Request.QueryString("id"), "248") Then
                op = "show"
            End If

            ' de acuerdo a la opcion
            Select Case op
                Case "export"
                    Export_Idea()

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "NUEVA IDEA."

                    ' ocultar algunos botones
                    'Me.btnAddData.Visible = True
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
                    ' Me.btnRefresh.Visible = False
                    Me.btnCancel.Visible = False
                    Me.txtcode.Visible = False
                    Me.lblcode.Visible = False
                    Me.btnexportword.Visible = False
                    ' crear la lista de ubucaciones
                    Session("locationByIdeaList") = New List(Of LocationByIdeaEntity)

                    'Se crea la variable de session que almacena la lista de terceros por idea
                    Session("thirdByIdeaList") = New List(Of ThirdByIdeaEntity)

                Case "edit", "show"

                    ' ocultar algunos botones
                    'Me.btnAddData.Visible = False
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.txtcode.Enabled = False
                    Me.lblcode.Enabled = False
                    Me.btnCancel.Visible = False
                    ' definir los objetos
                    Dim objIdea As New IdeaEntity

                    Try

                        ' cargar el registro referenciado
                        objIdea = facade.loadIdea(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objIdea.id
                        Me.txtcode.Text = objIdea.code
                        Me.txtname.Text = objIdea.name
                        Me.txtobjective.Text = objIdea.objective
                        Me.txtstartdate.Text = objIdea.startdate.ToString("yyyy/MM/dd")
                        Me.txtduration.Text = objIdea.duration
                        Me.txtareadescription.Text = objIdea.areadescription
                        ' Me.ddlPupulation.SelectedValue = objIdea.population
                        Me.txtcost.Text = objIdea.cost.ToString("#,###")
                        ''  Me.txtfsccontribution.Text = objIdea.fsccontribution.ToString("#,###")
                        ''  Me.txtcounterpartvalue.Text = objIdea.counterpartvalue.ToString("#,###")
                        Me.txtjustification.Text = objIdea.justification
                        Me.txtstrategydescription.Text = objIdea.strategydescription
                        Me.txtresults.Text = objIdea.results
                        Me.ddlSource.SelectedValue = objIdea.source
                        Me.ddlSummoning.SelectedValue = objIdea.idsummoning


                        ' TODO: 1  addidea page load
                        ' Autor: German Rodriguez MGgroup
                        ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

                        Me.txtresulgc.Text = objIdea.ResultsKnowledgeManagement
                        Me.txtresulci.Text = objIdea.ResultsInstalledCapacity
                        Me.txtobser.Text = objIdea.Loadingobservations
                        ' Me.ddlmodcontract.SelectedValue = objIdea.idtypecontract


                        ' TODO: 1  addidea page load
                        ' Autor: German Rodriguez
                        ' cierre de cambio
                        Me.Txtaccionmitig.Text = objIdea.mitigacion
                        Me.Txtriesgos.Text = objIdea.riesgos
                        Me.Txtroutepresupuestal.Text = objIdea.presupuestal
                        Me.Txtobligationsoftheparties.Text = objIdea.Obligaciones
                        Me.Txtday.Text = objIdea.dia
                        Me.Txtothersresults.Text = objIdea.OthersResults
                        Me.RBnList_iva.SelectedValue = objIdea.iva
                        Me.HDiva.Value = objIdea.iva

                        Dim idProcessInstance As String = Request.QueryString("idProcessInstance")

                        ' verificar si el objeto cargado ya tiene un proceso iniciado
                        If objIdea.IdProcessInstance = 0 Then

                            ' habilitarlo
                            Me.chkStartProcess.Enabled = True

                        Else

                            ' desabilitarlo
                            Me.chkStartProcess.Enabled = False

                        End If

                        Me.chkStartProcess.Checked = objIdea.startprocess
                        Me.txtcreatedate.Text = objIdea.createdate
                        Me.txtiduser.Text = objIdea.USERNAME
                        Me.ddlenabled.SelectedValue = objIdea.enabled

                        '     If (objIdea.DOCUMENTLIST Is Nothing OrElse objIdea.DOCUMENTLIST.Count = 0) Then Me.btnRefresh.Visible = False

                        'Se carga la lista de documentos adjuntos
                        'Se almacena la lista en una variable de sesion.
                        Me.DocumentsList = objIdea.DOCUMENTLIST
                        'Me.gvDocuments.DataSource = objIdea.DOCUMENTLIST
                        'Me.gvDocuments.DataBind()

                        'Se carga la lista de ubicaciones por idea de la base de datos
                        Session("locationByIdeaList") = objIdea.LOCATIONBYIDEALIST

                        ' Se actualiza la informacion de la grilla
                        Me.gvLocations.DataSource = objIdea.LOCATIONBYIDEALIST
                        Me.gvLocations.DataBind()

                        'Se carga la lista de ubicaciones por idea de la base de datos
                        Session("thirdByIdeaList") = objIdea.THIRDBYIDEALIST

                        ' TODO: 2  creacion de objeto tabla para recorre el grid
                        ' Autor: German Rodriguez MGgroup
                        ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

                        'Dim objDataTable As DataTable = New DataTable()

                        'objDataTable.Columns.Add("idthird")
                        'objDataTable.Columns.Add("name")
                        'objDataTable.Columns.Add("type")
                        'objDataTable.Columns.Add("contact")
                        'objDataTable.Columns.Add("documents")
                        'objDataTable.Columns.Add("phone")
                        'objDataTable.Columns.Add("email")
                        'objDataTable.Columns.Add("Vrmoney")
                        'objDataTable.Columns.Add("VrSpecies")
                        'objDataTable.Columns.Add("FSCorCounterpartContribution")

                        ''For Each itemDataTable As ThirdByIdeaEntity In Session("thirdByIdeaList")
                        ''    objDataTable.Rows.Add(itemDataTable.idthird, itemDataTable.THIRD.name, itemDataTable.type, itemDataTable.THIRD.contact, itemDataTable.THIRD.documents, itemDataTable.THIRD.phone, itemDataTable.THIRD.email, itemDataTable.Vrmoney, itemDataTable.VrSpecies, itemDataTable.FSCorCounterpartContribution)
                        ''Next

                        'objDataTable.Rows.Add("", "Total", "", "", "", "", "", 0, 0, 0)

                        ' TODO: 2  creacion de objeto tabla para recorre el grid
                        ' Autor: German Rodriguez
                        ' cierre de cambio


                        ' Se actualiza la informacion de la grilla
                        'Me.gvThirds.DataSource = objDataTable
                        'Me.gvThirds.DataBind()

                        ' TODO: 3 VALIDACION DE GRID DE ACTOREWS PARA LA SUMATORIA
                        ' AUTOR:GERMAN RODRIGUEZ 08/07/2013 MGgroup

                        For Each itemRow As GridViewRow In Me.gvThirds.Rows


                            Dim omoney, tmoney, oespes, tespes, ofsc, tfsc As Double
                            Dim lbltmoney = CType(itemRow.Cells(7).FindControl("lblMoney"), Label).Text
                            Dim lbltmoneystr As String = Replace(lbltmoney, ".", "")
                            Dim lblespecie = CType(itemRow.Cells(8).FindControl("lblespecies"), Label).Text
                            Dim lblespeciestr As String = Replace(lblespecie, ".", "")
                            Dim lblfsc = CType(itemRow.Cells(9).FindControl("lbltolfsc"), Label).Text
                            Dim lblfscstr As String = Replace(lblfsc, ".", "")

                            If Me.Txtsub1.Text = "" Then
                                Me.Txtsub1.Text = lbltmoneystr
                            Else
                                tmoney = Me.Txtsub1.Text
                                Dim tmoneystr As String = Replace(tmoney, ".", "")
                                omoney = Convert.ToInt64(tmoneystr)
                                tmoney = omoney + Convert.ToInt64(lbltmoneystr)
                                Txtsub1.Text = Format(Convert.ToInt64(tmoney), "#,###.##")

                            End If

                            If Me.Textsub2.Text = "" Then
                                Me.Textsub2.Text = lblespeciestr
                            Else

                                oespes = Me.Textsub2.Text
                                Dim especiesstr As String = Replace(oespes, ".", "")
                                oespes = Convert.ToInt64(especiesstr)
                                tespes = oespes + Convert.ToInt64(lblespeciestr)
                                Textsub2.Text = Format(Convert.ToInt64(tespes), "#,###.##")
                            End If

                            If Me.Txtsub3.Text = "" Then
                                Me.Txtsub3.Text = lblfscstr
                            Else
                                ofsc = Me.Txtsub3.Text
                                Dim fscstr As String = Replace(ofsc, ".", "")
                                ofsc = Convert.ToInt64(fscstr)
                                tfsc = ofsc + Convert.ToInt64(lblfscstr)
                                Txtsub3.Text = Format(Convert.ToInt64(tfsc), "#,###.##")
                            End If

                        Next

                        'Dim filamod As Integer = Me.gvThirds.Rows.Count
                        'filamod = filamod - 1

                        'CType(Me.gvThirds.Rows(filamod).Cells(7).FindControl("lblMoney"), Label).Text = Me.Txtsub1.Text
                        'CType(Me.gvThirds.Rows(filamod).Cells(8).FindControl("lblespecies"), Label).Text = Me.Textsub2.Text
                        'CType(Me.gvThirds.Rows(filamod).Cells(9).FindControl("lbltolfsc"), Label).Text = Me.Txtsub3.Text

                        'Me.gvThirds.Rows(filamod).Cells(0).Controls(0).Visible = False

                        ' TODO: 3 VALIDACION DE GRID DE ACTOREWS PARA LA SUMATORIA
                        ' AUTOR:GERMAN RODRIGUEZ 08/07/2013 MGgroup
                        ' cierre de cambio

                        'Se llama al metodo que permite actulizar la lista de las activiades especificas almacenadas en la base de datos
                        Me.LoadProgramComponents(objIdea)

                        If op.Equals("show") Then

                            ' cargar el titulo
                            Session("lblTitle") = "MOSTRAR INFORMACION DE UNA IDEA."

                            ' ocultar los botones para realizar modificaciones
                            Me.btnSave.Visible = False
                            Me.btnDelete.Visible = False
                            Me.btnCancel.Visible = False

                        Else

                            ' cargar el titulo
                            Session("lblTitle") = "EDITAR UNA IDEA."

                        End If

                        ' se llama al metodo de verificacion de idea
                        verificaraprobacion()


                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objIdea = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            '  Me.TabContainer1.ActiveTabIndex = 0

            'Se inicializan las variables de la página
            ViewState("ValidCode") = True
        Else
            If Me.txtid.Text = "" Then
                Session("lblTitle") = "NUEVA IDEA."
            Else
                Session("lblTitle") = "EDITAR UNA IDEA."
            End If

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objIdea As New IdeaEntity
        Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            ' cargar los valores registrados por el usuario
            Dim codeidea = Me.txtcode.Text
            objIdea.code = Replace(codeidea, vbCrLf, " ")

            objIdea.name = clean_vbCrLf(Me.txtname.Text)

            objIdea.objective = clean_vbCrLf(Me.txtobjective.Text)
            objIdea.startdate = Me.txtstartdate.Text
            objIdea.duration = Me.txtduration.Text
            objIdea.areadescription = clean_vbCrLf(Me.txtareadescription.Text)
            'objIdea.population = Me.ddlPupulation.SelectedValue
            objIdea.cost = PublicFunction.ConvertStringToDouble(Me.txtcost.Text)
            '' objIdea.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
            '' objIdea.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text)
            objIdea.strategydescription = clean_vbCrLf(Me.txtstrategydescription.Text)
            objIdea.results = clean_vbCrLf(Me.txtresults.Text)
            objIdea.source = Me.ddlSource.SelectedValue
            objIdea.idsummoning = Me.ddlSummoning.SelectedValue
            objIdea.startprocess = Me.chkStartProcess.Checked
            objIdea.createdate = Now
            objIdea.iduser = applicationCredentials.UserID
            objIdea.enabled = Me.ddlenabled.SelectedValue
            objIdea.justification = clean_vbCrLf(Me.txtjustification.Text)
            objIdea.Enddate = Me.Txtdatecierre.Text

            ' TODO: 4  addidea campos nuevos
            ' Autor: German Rodriguez MGgroup
            ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

            objIdea.ResultsKnowledgeManagement = clean_vbCrLf(Me.txtresulgc.Text)
            objIdea.ResultsInstalledCapacity = clean_vbCrLf(Me.txtresulci.Text)
            objIdea.Loadingobservations = clean_vbCrLf(Me.txtobser.Text)
            '  objIdea.idtypecontract = Me.ddlmodcontract.SelectedValue

            ' TODO: 4  addidea campos nuevos
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio


            'Se agrega la lista de documentos cargados en el servidor
            Me.LoadFiles(objIdea, applicationCredentials.UserID)

            'Se garega la lista de ubicaciones agregada
            objIdea.LOCATIONBYIDEALIST = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

            'Se agrega la lista de terceros agregada
            objIdea.THIRDBYIDEALIST = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

            ' TODO: 5  addidea cabio de forma de leer el grid de actores
            ' Autor: German Rodriguez MGgroup

            Dim countrowthird As Integer = gvThirds.Rows.Count

            countrowthird = countrowthird - 1
            Dim ii As Integer = 0

            For i = 1 To countrowthird


                Dim objThirdDALC As ThirdDALC = New ThirdDALC()
                Dim objThirdEntity As ThirdEntity = New ThirdEntity()
                Dim objThirdByideaEntity As ThirdByIdeaEntity = New ThirdByIdeaEntity()

                Dim labelidthird = CType(gvThirds.Rows(ii).Cells(1).FindControl("lblIdactor"), Label).Text
                Dim labelcontact = CType(gvThirds.Rows(ii).Cells(3).FindControl("lblIcontact"), Label).Text
                Dim labeldocument = CType(gvThirds.Rows(ii).Cells(4).FindControl("lblIdocument"), Label).Text
                Dim labelphone = CType(gvThirds.Rows(ii).Cells(5).FindControl("lblphone"), Label).Text
                Dim labelemail = CType(gvThirds.Rows(ii).Cells(6).FindControl("lblemail"), Label).Text

                objThirdEntity.id = labelidthird

                objThirdEntity.contact = labelcontact
                objThirdEntity.documents = labeldocument
                objThirdEntity.phone = labelphone
                objThirdEntity.email = labelemail
                objThirdDALC.update_add(applicationCredentials, objThirdEntity)

                ii = ii + 1

            Next

            ' TODO: 5  addidea cabio de forma de leer el grid de vactores
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio 


            'Se recorre la lista de Componentes del Programa seleccionadas
            For Each item As ListItem In Me.dlbActivity.SelectedItems.Items
                Dim myProgramComponentByIdea As New ProgramComponentByIdeaEntity
                myProgramComponentByIdea.idProgramComponent = item.Value
                myProgramComponentByIdeaList.Add(myProgramComponentByIdea)
            Next

            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

            ' almacenar la entidad
            objIdea.id = facade.addIdea(applicationCredentials, objIdea)

            If (objIdea.startprocess) Then


                ' crear el proceso en el BPM
                objIdea.IdProcessInstance = GattacaApplication.createProcessInstance(applicationCredentials, PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                                     "WebForm", "IdeaEntity", objIdea.id, 0)

                ' Iniciarlo
                objIdea.IdActivityInstance = GattacaApplication.startProcessInstance(applicationCredentials, objIdea.IdProcessInstance, _
                                                                                     PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                                     "WebForm", "IdeaEntity", objIdea.id, 0)
                ' actualizar la Idea
                facade.updateIdea(applicationCredentials, objIdea)
                ' ir a la pagina de lista de tareas
                Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))
            Else
                'va a la pagina de buscar
                Me.Txtdatecierre.Text = Me.HFdate.Value
                Me.lblsaveinformation.Text = "La idea se guardó correctamente !"
                'Me.lblsaveinformation.ForeColor = Drawing.Color.Green
                Me.containerSuccess.Visible = "true"
                Me.btnAddData.Visible = "false"
                Me.btnexportword.Visible = "True"

            End If


            ' cerrar esta pagina
            'ScriptManager.RegisterStartupScript(Me, GetType(String), "close", "<script>window.close();</script>", False)

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
            objIdea = Nothing
            facade = Nothing

        End Try

    End Sub

    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

    '    ' ir al administrador
    '    Response.Redirect("searchIdea.aspx")

    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (CBool(ViewState("ValidCode"))) Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objIdea As New IdeaEntity
            Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim idProcessInstance As String = String.Empty
            Dim idActivityInstance As String = String.Empty
            Dim url As String = ""

            ' cargar los valores del BPM
            idProcessInstance = Request.QueryString("idProcessInstance")
            idActivityInstance = Request.QueryString("idActivityInstance")

            ' cargar el registro referenciado
            objIdea = facade.loadIdea(applicationCredentials, Request.QueryString("id"))

            Try
                ' cargar los datos
                objIdea.code = Me.txtcode.Text
                objIdea.name = clean_vbCrLf(Me.txtname.Text)
                objIdea.objective = clean_vbCrLf(Me.txtobjective.Text)
                objIdea.startdate = Me.txtstartdate.Text
                objIdea.duration = Me.txtduration.Text
                objIdea.areadescription = clean_vbCrLf(Me.txtareadescription.Text)
                '   objIdea.population = Me.ddlPupulation.SelectedValue
                objIdea.cost = PublicFunction.ConvertStringToDouble(Me.txtcost.Text)
                '' objIdea.fsccontribution = PublicFunction.ConvertStringToDouble(Me.txtfsccontribution.Text)
                ''objIdea.counterpartvalue = PublicFunction.ConvertStringToDouble(Me.txtcounterpartvalue.Text)
                objIdea.strategydescription = clean_vbCrLf(Me.txtstrategydescription.Text)
                objIdea.results = clean_vbCrLf(Me.txtresults.Text)
                objIdea.source = Me.ddlSource.SelectedValue
                objIdea.justification = clean_vbCrLf(Me.txtjustification.Text)
                objIdea.idsummoning = Me.ddlSummoning.SelectedValue
                objIdea.startprocess = Me.chkStartProcess.Checked
                objIdea.enabled = Me.ddlenabled.SelectedValue
                objIdea.Enddate = Me.HFEndDate.Value

                ' TODO: 6 addidea save
                ' Autor: German Rodriguez MGgroup
                ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

                objIdea.ResultsKnowledgeManagement = clean_vbCrLf(Me.txtresulgc.Text)
                objIdea.ResultsInstalledCapacity = clean_vbCrLf(Me.txtresulci.Text)
                objIdea.Loadingobservations = clean_vbCrLf(Me.txtobser.Text)
                ' objIdea.idtypecontract = Me.ddlmodcontract.SelectedValue

                ' TODO: 6 addidea save
                ' Autor: German Rodriguez MGgroup
                ' cirre de cambio


                'Se recupera la lista de documentos de la variable de sesion correspondiente
                If Not (Me.DocumentsList Is Nothing) Then objIdea.DOCUMENTLIST = Me.DocumentsList

                'Se agrega la lista de documentos cargados en el servidor
                Me.LoadFiles(objIdea, applicationCredentials.UserID)

                'Se garega la lista de ubicaciones agregada
                objIdea.LOCATIONBYIDEALIST = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

                'Se agrega la lista de terceros agregada
                objIdea.THIRDBYIDEALIST = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))


                Dim countrowthird As Integer = gvThirds.Rows.Count

                countrowthird = countrowthird - 1
                Dim ii As Integer = 0

                For i = 1 To countrowthird



                    Dim objThirdDALC As ThirdDALC = New ThirdDALC()
                    Dim objThirdEntity As ThirdEntity = New ThirdEntity()

                    Dim labelidthird = CType(gvThirds.Rows(ii).Cells(1).FindControl("lblIdactor"), Label).Text
                    Dim labelcontact = CType(gvThirds.Rows(ii).Cells(3).FindControl("lblIcontact"), Label).Text
                    Dim labeldocument = CType(gvThirds.Rows(ii).Cells(4).FindControl("lblIdocument"), Label).Text
                    Dim labelphone = CType(gvThirds.Rows(ii).Cells(5).FindControl("lblphone"), Label).Text
                    Dim labelemail = CType(gvThirds.Rows(ii).Cells(6).FindControl("lblemail"), Label).Text


                    objThirdEntity.id = labelidthird
                    objThirdEntity.contact = labelcontact
                    objThirdEntity.documents = labeldocument
                    objThirdEntity.phone = labelphone
                    objThirdEntity.email = labelemail
                    objThirdDALC.update_add(applicationCredentials, objThirdEntity)
                    ii = ii + 1
                Next



                'Se recorre la lista de Componentes del Programa seleccionadas
                For Each item As ListItem In Me.dlbActivity.SelectedItems.Items

                    ' definir los obejtos
                    Dim myProgramComponentByIdea As New ProgramComponentByIdeaEntity

                    ' cargar los valores seleccionados
                    myProgramComponentByIdea.idProgramComponent = item.Value
                    myProgramComponentByIdeaList.Add(myProgramComponentByIdea)

                Next

                'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
                objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

                ' verificar si se debe finalizar la actividad
                If idProcessInstance IsNot Nothing Then

                    ' finalizar la actividad actual
                    GattacaApplication.endActivityInstance(applicationCredentials, idProcessInstance, idActivityInstance, _
                                                           PublicFunction.getSettingValue("BPM.Condition.PR01-CD002"), "Se ha modificado la idea", _
                                                           "", "", "", "")
                    ' ir a la pagina de lista de tareas
                    url = PublicFunction.getSettingValue("BPM.TaskList")

                    ' verificar si se debe crear el proyecto
                ElseIf objIdea.IdProcessInstance = 0 And objIdea.startprocess And Me.chkStartProcess.Enabled = True Then

                    ' crear el proceso en el BPM
                    objIdea.IdProcessInstance = GattacaApplication.createProcessInstance(applicationCredentials, PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                                         "WebForm", "IdeaEntity", objIdea.id, 0)

                    ' Iniciarlo
                    objIdea.IdActivityInstance = GattacaApplication.startProcessInstance(applicationCredentials, objIdea.IdProcessInstance, _
                                                                                         PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                                         "WebForm", "IdeaEntity", objIdea.id, 0)
                    ' ir a la pagina de lista de tareas
                    url = PublicFunction.getSettingValue("BPM.TaskList")

                Else

                    ' ir a la pagina de lista de tareas
                    Me.lblsaveinformation.Text = "La idea se modificó exitosamente !"
                    'Me.lblsaveinformation.ForeColor = Drawing.Color.Green
                    Me.containerSuccess.Visible = "true"

                    Me.btnSave.Visible = "false"
                    Me.Txtdatecierre.Text = Me.HFdate.Value
                    'Response.Write("<script>alert('idea modificada satisfactoriamente');")
                    'Response.Write("</script>")

                End If

                ' actualizar la Idea
                facade.updateIdea(applicationCredentials, objIdea)

                'ir al administrador
                'Response.Redirect(url)

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
                objIdea = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Eliminar el registro
            facade.deleteIdea(applicationCredentials, CInt(Request.QueryString("id")), Me.DocumentsList)

            ' ir al administrador
            Response.Redirect("searchIdea.aspx")

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

    'Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged
    '    'Se llama al metodo que permite crear cargar el combo de ciudades
    '    '    Me.LoadDropDownCities()

    'End Sub

    'Protected Sub ddlStrategicLines_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrategicLines.SelectedIndexChanged
    '    'Se llama al metodo que permite crear cargar la lista de Programa
    '    '  Me.LoadDropDownListPrograms()
    '    'Me.TabContainer1.ActiveTabIndex = 3
    '    'Se recorre la lista de Componentes del Programa seleccionadas
    '    Dim miItem As ListItem
    '    For Each item As ListItem In Me.dlbActivity.SelectedItems.Items
    '        miItem = Me.dlbActivity.AviableItems.Items.FindByValue(item.Value)
    '        ' cargar los valores seleccionados
    '        dlbActivity.AviableItems.Items.Remove(miItem)
    '    Next

    'End Sub

    'Protected Sub ddlPrograms_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrograms.SelectedIndexChanged
    '    'Se llama al metodo que permite crear cargar la lista de Componentes del Programa
    '    Me.LoadListProgramComponents()
    '    'Me.TabContainer1.ActiveTabIndex = 3
    '    'Se limpia la lista de Componentes del Programa
    '    'Me.dlbActivity.SelectedItems.Items.Clear()
    '    'Se recorre la lista de Componentes del Programa seleccionadas
    '    Dim miItem As ListItem
    '    For Each item As ListItem In Me.dlbActivity.SelectedItems.Items

    '        miItem = Me.dlbActivity.AviableItems.Items.FindByValue(item.Value)
    '        ' cargar los valores seleccionados
    '        dlbActivity.AviableItems.Items.Remove(miItem)
    '    Next
    'End Sub

    'Protected Sub btnAgregarubicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarubicacion.Click

    '    'Se verifica que los combos de seleccion tengan valores
    '    If (Me.ddlDepto.SelectedValue.Length > 0 AndAlso Me.ddlCity.SelectedValue.Length > 0) Then
    '        ' definir los objetos
    '        Dim locationByIdeaList As List(Of LocationByIdeaEntity)
    '        Dim locationByIdea As New LocationByIdeaEntity

    '        ' cargarla de la session
    '        locationByIdeaList = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

    '        locationByIdea.DEPTO.id = Me.ddlDepto.SelectedValue
    '        locationByIdea.DEPTO.name = Me.ddlDepto.SelectedItem.Text
    '        locationByIdea.CITY.id = Me.ddlCity.SelectedValue
    '        locationByIdea.CITY.name = Me.ddlCity.SelectedItem.Text

    '        If Not (locationByIdeaList.Exists(Function(unLocation) unLocation.CITY.id = locationByIdea.CITY.id)) Then
    '            ' agregarlos
    '            locationByIdeaList.Add(locationByIdea)

    '            ' mostrar
    '            Me.gvLocations.DataSource = locationByIdeaList
    '            Me.gvLocations.DataBind()
    '        End If
    '    End If
    '    Me.Txtdatecierre.Text = Me.HFdate.Value
    'End Sub

    Protected Sub btnAddThird_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddThird.Click

        ' definir los objetos
        Me.lblavertenactors.Text = ""
        Dim thirdByIdeaList As List(Of ThirdByIdeaEntity)
        Dim thirdByIdea As New ThirdByIdeaEntity
        Dim tmoneystr, tspeciesstr, tfscstr, tmoney, tspecies, tfsc, VAL1, VAL2, VAL3 As String
        Dim swhitch As Integer = 0
        tmoneystr = 0
        tspeciesstr = 0
        tfscstr = 0

        'contador para saber la cantidad de datos en el grid----------- German Rodriguez--- 
        Dim countgve As Integer = Me.gvThirds.Rows.Count
        Dim CAMBIO As Integer = 0

        If countgve = 0 Then
            countgve = 0
        Else
            countgve = countgve - 1
            Dim ir As Integer = 0
            For i = 1 To countgve
                Dim lbltmoney = CType(gvThirds.Rows(ir).Cells(1).FindControl("lblIdactor"), Label).Text
                Dim comparar As Integer = Me.HDIDTHIRD.Value
                If comparar = lbltmoney Then
                    Me.lblavertenactors.Text = "este actor ya fue ingresado"
                    CleanThird()
                    CAMBIO = 1
                    Exit For
                End If
                ir = ir + 1
            Next
        End If

        If CAMBIO = 0 Then
            ' cargarla de la session
            thirdByIdeaList = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

            thirdByIdea.idthird = Me.HDIDTHIRD.Value
            thirdByIdea.THIRD.name = Me.HDNAMETHIRD.Value
            thirdByIdea.THIRD.contact = Me.Txtcontact.Text
            thirdByIdea.THIRD.documents = Me.Txtcedulacont.Text
            thirdByIdea.THIRD.phone = Me.Txttelcont.Text
            thirdByIdea.THIRD.email = Me.Txtemail.Text

            Dim omoney, ospecies, ofsc As String

            If Me.Txtvrdiner.Text = "" Then
                thirdByIdea.Vrmoney = 0
                omoney = 0
            Else
                omoney = Me.Txtvrdiner.Text
                thirdByIdea.Vrmoney = Me.Txtvrdiner.Text
            End If

            If Me.Txtvresp.Text = "" Then
                thirdByIdea.VrSpecies = 0
                ospecies = 0
            Else
                thirdByIdea.VrSpecies = Me.Txtvresp.Text
                ospecies = Me.Txtvresp.Text
            End If

            If Me.Txtaportfscocomp.Text = "" Then
                thirdByIdea.FSCorCounterpartContribution = 0
                ofsc = 0
            Else
                Dim VALNUM = IsNumeric(Me.Txtaportfscocomp.Text)
                If VALNUM = False Then
                    thirdByIdea.FSCorCounterpartContribution = 0
                    ofsc = 0
                Else
                    thirdByIdea.FSCorCounterpartContribution = Me.Txtaportfscocomp.Text
                    ofsc = Me.Txtaportfscocomp.Text
                End If
            End If

            thirdByIdea.type = Me.ddlType.SelectedItem.Text
            Try

                'TODO: 7  VALIDACION DE SUMAS EN AGREGAR UN ACTOR DEL GRID
                'AUTOR: GERMAN RODRIGUEZ 03/06/201311 MGgroup

                'OPERACIONES DE SUMA GERMAN RODRIGUEZ
                If Me.Txtsub1.Text = "" Then
                    Me.Txtsub1.Text = omoney
                    VAL1 = Me.Txtsub1.Text
                Else
                    tmoney = Me.Txtsub1.Text
                    tmoneystr = Replace(tmoney, ".", "")
                    Dim omoneystr As String = Replace(omoney, ".", "")
                    tmoneystr = Convert.ToInt64(omoneystr) + Convert.ToInt64(tmoneystr)
                    Me.Txtsub1.Text = Format(Convert.ToInt64(tmoneystr), "#,###.##")
                    VAL1 = Format(Convert.ToInt64(tmoneystr), "#,###.##")
                End If

                'OPERACIONES DE SUMA GERMAN RODRIGUEZ
                If Me.Textsub2.Text = "" Then
                    Me.Textsub2.Text = ospecies
                    VAL2 = Me.Textsub2.Text
                Else
                    tspecies = Me.Textsub2.Text
                    tspeciesstr = Replace(tspecies, ".", "")
                    Dim ospeciesstr As String = Replace(ospecies, ".", "")
                    tspeciesstr = Convert.ToInt64(ospeciesstr) + Convert.ToInt64(tspeciesstr)
                    Me.Textsub2.Text = Format(Convert.ToUInt64(tspeciesstr), "#,###.##")
                    VAL2 = Format(Convert.ToUInt64(tspeciesstr), "#,###.##")
                End If

                'OPERACIONES DE SUMA GERMAN RODRIGUEZ
                If Me.Txtsub3.Text = "" Then
                    swhitch = 1
                    Dim vtrev = Convert.ToInt64(Replace(VAL1, ".", "")) + Convert.ToInt64(Replace(VAL2, ".", ""))
                    If Convert.ToInt64(vtrev) > 99999999999 Then
                        Me.lblavertenactors.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
                        Me.Txtvrdiner.Text = ""
                        Me.Txtvresp.Text = ""
                        Me.Txtaportfscocomp.Text = ""
                        Exit Sub
                    Else
                        Me.Txtsub3.Text = ofsc
                        VAL3 = Me.Txtsub3.Text
                        Me.txtcost.Text = VAL3
                    End If

                Else
                    tfsc = Me.Txtsub3.Text
                    tfscstr = Replace(tfsc, ".", "")
                    Dim ofscstr As String = Replace(ofsc, ".", "")
                    tfscstr = Convert.ToInt64(ofscstr) + Convert.ToInt64(tfscstr)
                    ' Me.Txtsub3.Text = tfscstr
                    Me.Txtsub3.Text = Format(Convert.ToInt64(tfscstr), "#,###.##")
                    Me.HDvaluestotal.Value = Format(Convert.ToInt64(tfscstr), "#,###.##")
                    VAL3 = Format(Convert.ToInt64(tfscstr), "#,###.##")
                    Me.txtcost.Text = Me.HDvaluestotal.Value
                End If

                ' agregarlos

                thirdByIdeaList.Add(thirdByIdea)

                Dim objDataTable As DataTable = New DataTable()

                objDataTable.Columns.Add("idthird")
                objDataTable.Columns.Add("name")
                objDataTable.Columns.Add("type")
                objDataTable.Columns.Add("contact")
                objDataTable.Columns.Add("documents")
                objDataTable.Columns.Add("phone")
                objDataTable.Columns.Add("email")
                objDataTable.Columns.Add("Vrmoney")
                objDataTable.Columns.Add("VrSpecies")
                objDataTable.Columns.Add("FSCorCounterpartContribution")


                For Each itemDataTable As ThirdByIdeaEntity In thirdByIdeaList
                    objDataTable.Rows.Add(itemDataTable.idthird, itemDataTable.THIRD.name, itemDataTable.type, itemDataTable.THIRD.contact, itemDataTable.THIRD.documents, itemDataTable.THIRD.phone, itemDataTable.THIRD.email, itemDataTable.Vrmoney, itemDataTable.VrSpecies, itemDataTable.FSCorCounterpartContribution)
                Next

                objDataTable.Rows.Add("", "Total", "", "", "", "", "", VAL1, VAL2, VAL3)

                ' TODO: 7  VALIDACION DE SUMAS EN AGREGAR UN ACTOR DEL GRID
                ' AUTOR: GERMAN RODRIGUEZ 03/06/201311 MGgroup
                ' cierre cambio

                ' mostrar
                Me.gvThirds.DataSource = objDataTable
                Me.gvThirds.DataBind()

                ' TODO: 8  VALIDACION DE SUMAS EN AGREGAR UN ACTOR DEL GRID
                ' AUTOR: GERMAN RODRIGUEZ 03/06/201311 MGgroup

                Dim filamod As Integer = Me.gvThirds.Rows.Count
                filamod = filamod - 1
                Me.gvThirds.Rows(filamod).Cells(0).Controls(0).Visible = False

                ' TODO: 8  VALIDACION DE SUMAS EN AGREGAR UN ACTOR DEL GRID
                ' AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup
                ' cierre cambio


                Me.CleanThird()

            Catch ex As Exception

                Me.lblavertenactors.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
                Me.Txtvrdiner.Text = ""
                Me.Txtvresp.Text = ""
                Me.Txtaportfscocomp.Text = ""

                If swhitch = 1 Then
                    Me.Txtsub1.Text = ""
                    Me.Textsub2.Text = ""
                End If

            End Try

        End If
        Me.Txtdatecierre.Text = Me.HFdate.Value
        ' Me.ddlactors.Focus()

    End Sub

    Protected Sub gvLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLocations.SelectedIndexChanged

        ' definir los objetos
        Dim locationList As List(Of LocationByIdeaEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        locationList = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

        ' remover el seleccionado
        locationList.RemoveAt(Me.gvLocations.SelectedIndex)

        ' mostrar
        Me.gvLocations.DataSource = locationList
        Me.gvLocations.DataBind()

        'Se selecciona la pestama de ubicaciones por idea
        ' Me.TabContainer1.ActiveTabIndex = 1

    End Sub

    Protected Sub gvThirds_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvThirds.SelectedIndexChanged
        ' definir los objetos
        Dim thirdByIdeaList As List(Of ThirdByIdeaEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        thirdByIdeaList = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

        'TODO: 9 VALIDACION DE RESTAS EN ELIMINAR ACTOR DEL GRID
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup

        Dim omoney, ospecies, ofsc As String
        Dim tmoney, tspecies, tfsc As String

        omoney = Me.Txtsub1.Text
        Dim opermoney As String = Replace(omoney, ".", "")
        ospecies = Me.Textsub2.Text
        Dim operespecie As String = Replace(ospecies, ".", "")
        ofsc = Me.Txtsub3.Text
        Dim operfsc As String = Replace(ofsc, ".", "")

        Dim labelMoney As Label = CType(gvThirds.Rows(Me.gvThirds.SelectedIndex).Cells(7).FindControl("lblMoney"), Label)
        Dim labelespecies As Label = CType(gvThirds.Rows(Me.gvThirds.SelectedIndex).Cells(8).FindControl("lblespecies"), Label)
        Dim labeltotal As Label = CType(gvThirds.Rows(Me.gvThirds.SelectedIndex).Cells(9).FindControl("lbltolfsc"), Label)

        tmoney = labelMoney.Text
        tspecies = labelespecies.Text
        tfsc = labeltotal.Text

        Dim lbltmoneystr As String = Replace(tmoney, ".", "")
        Dim lblespeciestr As String = Replace(tspecies, ".", "")
        Dim lblfscstr As String = Replace(tfsc, ".", "")

        'OPERACIONES DE RESTA GERMAN RODRIGUEZ
        tmoney = Convert.ToInt64(opermoney) - Convert.ToInt64(lbltmoneystr)
        Me.Txtsub1.Text = Format(Convert.ToInt64(tmoney), "#,###.##")


        'OPERACIONES DE RESTA GERMAN RODRIGUEZ
        tspecies = Convert.ToInt64(operespecie) - Convert.ToInt64(lblespeciestr)
        Me.Textsub2.Text = Format(Convert.ToInt64(tspecies), "#,###.##")

        'OPERACIONES DE RESTA GERMAN RODRIGUEZ
        tfsc = Convert.ToInt64(operfsc) - Convert.ToInt64(lblfscstr)
        Me.Txtsub3.Text = Format(Convert.ToInt64(tfsc), "#,###.##")
        Me.HDvaluestotal.Value = Format(Convert.ToInt64(tfsc), "#,###.##")
        Me.txtcost.Text = Me.HDvaluestotal.Value

        'TODO: 9 VALIDACION DE RESTAS EN ELIMINAR ACTOR DEL GRID
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup
        ' cierre de cambio


        ' remover el seleccionado
        thirdByIdeaList.RemoveAt(Me.gvThirds.SelectedIndex)

        'TODO: 10 cambio de forma de grid actores
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup

        Dim objDataTable As DataTable = New DataTable()

        objDataTable.Columns.Add("idthird")
        objDataTable.Columns.Add("name")
        objDataTable.Columns.Add("type")
        objDataTable.Columns.Add("contact")
        objDataTable.Columns.Add("documents")
        objDataTable.Columns.Add("phone")
        objDataTable.Columns.Add("email")
        objDataTable.Columns.Add("Vrmoney")
        objDataTable.Columns.Add("VrSpecies")
        objDataTable.Columns.Add("FSCorCounterpartContribution")


        For Each itemDataTable As ThirdByIdeaEntity In thirdByIdeaList
            objDataTable.Rows.Add(itemDataTable.idthird, itemDataTable.THIRD.name, itemDataTable.type, itemDataTable.THIRD.contact, itemDataTable.THIRD.documents, itemDataTable.THIRD.phone, itemDataTable.THIRD.email, itemDataTable.Vrmoney, itemDataTable.VrSpecies, itemDataTable.FSCorCounterpartContribution)
        Next

        objDataTable.Rows.Add("", "Total", "", "", "", "", "", Format(Convert.ToInt32(tmoney), "#,###.##"), Format(Convert.ToInt32(tspecies), "#,###.##"), Format(Convert.ToInt32(tfsc), "#,###.##"))

        'TODO: 10 cambio de forma de grid actores
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup
        'cierre de cambio

        ' mostrar
        Me.gvThirds.DataSource = objDataTable
        Me.gvThirds.DataBind()

        'TODO: 11 cambio de forma de grid actores
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup

        Dim filamod As Integer = Me.gvThirds.Rows.Count
        filamod = filamod - 1
        Me.gvThirds.Rows(filamod).Cells(0).Controls(0).Visible = False

        'TODO: 11 cambio de forma de grid actores
        'AUTOR: GERMAN RODRIGUEZ 03/06/2013 MGgroup
        'cierre de cambio


        'Se selecciona la pestama de terceros por idea

        ' Me.TabContainer1.ActiveTabIndex = 2
    End Sub

    'Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try

    '        If facade.verifyIdeaCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
    '            lblHelpcode.ForeColor = Drawing.Color.Red
    '            lblHelpcode.Text = "Este código ya existe, por favor cambielo"
    '            rfvcode.IsValid = 0
    '            ViewState("ValidCode") = False
    '            Me.txtcode.Focus()
    '        Else
    '            lblHelpcode.Text = ""
    '            rfvcode.IsValid = 1
    '            ViewState("ValidCode") = True
    '            Me.txtname.Focus()
    '        End If

    '        'Se asigna el foco a la primer pestaña
    '        ' Me.TabContainer1.ActiveTabIndex = 0

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

    'Protected Sub gvDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDocuments.RowDeleting

    '    'Se recupera la lista de documentos actual
    '    Dim documentsList As List(Of DocumentsEntity)
    '    documentsList = Me.DocumentsList

    '    'Se pone el estado de elminación al documento requerido
    '    documentsList(e.RowIndex).ISDELETED = True

    '    'Se oculta de la grilla el registro seleccionado
    '    Me.gvDocuments.Rows(e.RowIndex).Visible = False

    'End Sub

    'Protected Sub gvDocuments_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocuments.RowDataBound

    '    Dim objHyperlink As HyperLink
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim miEntidad As DocumentsEntity = e.Row.DataItem
    '        objHyperlink = e.Row.Cells(9).Controls(0)
    '        If (miEntidad.attachfile.Length > 0) Then
    '            objHyperlink.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "/" & miEntidad.attachfile
    '            objHyperlink.Target = "_blank"
    '        End If
    '    End If
    'End Sub

    'Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    '    'Se llama al metodo que cpnsulta la lista de documentos para el registro de idea actual
    '    Me.LoadDocumentsList()

    '    'Se actualiza la grilla.
    '    Me.gvDocuments.DataSource = Me.DocumentsList
    '    Me.gvDocuments.DataBind()

    'End Sub


    ''' <summary>
    ''' TODO:12 boton permite exportar a word 
    ''' AUTOR:german Rodriguez 15/06/2013 MGgroup
    ''' modificacion de formato 21/08/2013 MGgroup
    ''' </summary>
    ''' <remarks></remarks>
    'Protected Sub btnexportword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexportword.Click

    '    Dim sql As New StringBuilder
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


    '    Dim ddlc, ddls, ddlp, name, just, objet, objetesp, resulb, resulgc, resulci, fech, dura, people, vt1, vt2, vt3, fuent, est, datanex, dept, munip, actor, action, vt4, vt5, vt6, active As String
    '    Dim FSCval As String = ""
    '    Dim ididea As Integer

    '    If Me.txtid.Text = "" Then
    '        sql.Append("select MAX(id) from idea")
    '        ididea = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)
    '    Else
    '        ididea = Me.txtid.Text
    '    End If

    '    sql = New StringBuilder
    '    sql.Append("select convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.','')) from Idea i  ")
    '    sql.Append("inner join ThirdByIdea ti on i.Id = ti.IdIdea  ")
    '    sql.Append(" inner join Third t on t.Id= ti.IdThird ")
    '    sql.Append("where (t.code = '8600383389' And ti.IdIdea = " & ididea & ")")

    '    FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

    '    If FSCval = 0 Then
    '        vt5 = "0"
    '    Else
    '        vt5 = Format(Convert.ToInt64(FSCval), "#,###.##")
    '    End If
    '    sql = New StringBuilder
    '    sql.Append("select sum(convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.',''))) from Idea i   ")
    '    sql.Append("inner join ThirdByIdea ti on i.Id=ti.IdIdea  ")
    '    sql.Append(" inner join Third t on t.Id= ti.IdThird ")
    '    sql.Append("where(t.code <> '8600383389' And ti.IdIdea = " & ididea & ")")


    '    Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)




    '    If otrosval = 0 Then
    '        vt4 = "0"
    '    Else
    '        vt4 = Format(Convert.ToInt64(otrosval), "#,###.##")
    '    End If

    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.AddHeader("content-disposition", "attachment;filename=IdeaExport.doc")
    '    Response.Charset = "UTF8Encoding"
    '    Response.ContentType = "application/vnd.ms-word "

    '    ddls = Me.ddlStrategicLines.SelectedItem.Text
    '    ddlp = Me.ddlPrograms.SelectedItem.Text
    '    ddlc = Me.ddlmodcontract.SelectedItem.Text
    '    name = Me.txtname.Text
    '    just = Me.txtjustification.Text
    '    objet = Me.txtobjective.Text
    '    objetesp = Me.txtareadescription.Text
    '    resulb = Me.txtresults.Text
    '    resulgc = Me.txtresulgc.Text
    '    resulci = Me.txtresulci.Text
    '    fech = Me.txtstartdate.Text
    '    dura = Me.txtduration.Text
    '    people = Me.ddlPupulation.SelectedItem.Text
    '    ''  vt1 = Me.txtfsccontribution.Text
    '    ''vt2 = Me.txtcounterpartvalue.Text
    '    vt3 = Me.txtcost.Text
    '    fuent = Me.ddlSource.SelectedItem.Text
    '    est = Me.ddlenabled.DataTextField
    '    datanex = Me.txtobser.Text
    '    dept = Me.ddlDepto.DataTextField
    '    munip = Me.ddlCity.DataTextField
    '    actor = Me.ddlactors.SelectedItem.Text
    '    action = Me.txtActions.Text
    '    vt6 = Me.Txtsub3.Text
    '    active = Me.dlbActivity.ToString()

    '    If objetesp = "" Then
    '        objetesp = "No Aplica"
    '    End If
    '    If resulb = "" Then
    '        resulb = "No Aplica"
    '    End If
    '    If resulgc = "" Then
    '        resulgc = "No Aplica"
    '    End If
    '    If resulci = "" Then
    '        resulci = "No Aplica"
    '    End If
    '    If dura = "" Then
    '        dura = "No Aplica"
    '    End If
    '    If datanex = "" Then
    '        datanex = "No Aplica"
    '    End If

    '    If ddlc = "Seleccione...." Then
    '        ddlc = ""
    '    End If

    '    Response.Output.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%"">")

    '    Response.Output.WriteLine("<body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>")
    '    Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 20%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>" & ddls.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Programa:</strong></span></td><td>" & ddlp.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre de la Idea:</strong></span></td><td>" & name.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>")

    '    For Each itemRow As GridViewRow In Me.gvLocations.Rows

    '        Dim lbldepto = CType(itemRow.Cells(1).FindControl("lbldepto"), Label).Text
    '        Dim lblcity = CType(itemRow.Cells(2).FindControl("lblcity"), Label).Text
    '        Response.Output.WriteLine(lbldepto.ToString() & ", " & lblcity.ToString() & " // ")

    '    Next

    '    Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Poblaci&oacute;n Beneficiaria:</strong></span></td><td>" & people.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Duraci&oacute;n en meses:</strong></span></td><td>" & dura.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Inicio:</strong></span></td><td>" & fech.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>" & vt3.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>" & ddlc.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No de Idea:</strong></span></td><td>" & ididea.ToString() & "." & "</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>")
    '    Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Documento de Identidad</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr>")


    '    For Each itemRow As GridViewRow In Me.gvThirds.Rows


    '        Dim lblname = CType(itemRow.Cells(1).FindControl("lblIname"), Label).Text
    '        Dim labelcontact = CType(itemRow.Cells(3).FindControl("lblIcontact"), Label).Text
    '        Dim labeldocument = CType(itemRow.Cells(4).FindControl("lblIdocument"), Label).Text
    '        Dim labelphone = CType(itemRow.Cells(5).FindControl("lblphone"), Label).Text
    '        Dim labelemail = CType(itemRow.Cells(6).FindControl("lblemail"), Label).Text
    '        Dim labeltype = CType(itemRow.Cells(2).FindControl("lbltype"), Label).Text

    '        If lblname = "Total" Then
    '            Exit For
    '        Else
    '            Response.Output.WriteLine("<tr><td style=""width: 16%;"">" & lblname.ToString() & "</td><td style=""width: 16%;"">" & labeltype.ToString() & "</td><td style=""width: 16%;"">" & labelcontact.ToString() & "</td><td style=""width: 16%;"">" & labeldocument.ToString() & "</td><td style=""width: 16%;"">" & labelphone.ToString() & "</td><td style=""width: 16%;"">" & labelemail.ToString() & "</tr>")
    '        End If

    '    Next

    '    Response.Output.WriteLine("</tbody></table><p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">JUSTIFICAC&Iacute;ON:</span></strong></p>")
    '    Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & just.ToString() & "</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVO:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""71"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objet.ToString() & "</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVOS ESPEC&Iacute;FICOS:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""82"" style=""width: 100%;"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objetesp.ToString() & "</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">RESULTADOS ESPERADOS:</span></strong></p>")
    '    Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
    '    Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Beneficiarios:</span></strong></td><td style=""text-align: justify;"">" & resulb.ToString() & "</td></tr><tr>")
    '    Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Gesti&oacute;n del conocimiento*:</span></strong></td><td style=""text-align: justify;"">" & resulgc.ToString() & "</td></tr><tr>")
    '    Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Capacidad instalada:</span></strong></td><td style=""text-align: justify;"">" & resulci.ToString() & "</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">PRESUPUESTO GENERAL:</span></strong></p>")
    '    Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
    '    Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Valor Total del contrato:</span></strong></td><td>" & vt6.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de los Socios (Efectivo y Especie):</span></strong></td><td>" & vt4.ToString() & "</td></tr>")
    '    Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de la FSC (Efectivo y Especie):</span></strong></td><td>" & vt5.ToString() & "</td></tr></tbody></table>")

    '    Response.Output.WriteLine("<span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span>")

    '    Response.Output.WriteLine("<p><u><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">CRONOGRAMA DE PAGOS</span></strong></u></p>")
    '    Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" height=""125"" width=""100%""><tbody><tr><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor</strong></span></td><td style=""width: 5%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>%</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Origen de los Recursos</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contraentrega</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha</strong></span></td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Primer Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Segundo Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tercer y &Uacute;ltimo Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%; text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">TOTAL</span></strong></td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 5%; text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">100%</span></strong></td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;"">&nbsp;</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>")
    '    Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr></tbody></table>")
    '    Response.Output.WriteLine("<p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p>")

    '    Response.Flush()
    '    Response.End()


    'End Sub
    ' TODO:12 boton permite exportar a word 
    ' AUTOR:german Rodriguez 15/06/2013 MGgroup
    ' cierre de cambio

    Public Function Export_Idea()

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim Data_aprobacion, Data_component_group, Data_idea, Data_pagos, Data_pagos_detalles, Data_detalles_actores, Data_totales_actors, Data_componet, Data_others, Data_ubicacion As DataTable

        Dim arrayubicacion, arrayactor, arraycomponente As String()

        Dim aplica_iva, ruta, riesgos, mitigacion, obligaciones, dia, ddlc, ddls, ddlp, name, just, objet, objetesp, resulb, resulgc, resulci, resulotros, fech, dura, people, vt1, vt2, vt3, fuent, est, datanex, dept, munip, actor, action, vt4, vt5, vt6, active As String
        Dim MinutesApproval, ApprovedBy, DateofApproval As String

        Dim FSCval As String = ""
        Dim ididea, approval_idea As Integer

        Dim idideastr = Request.QueryString("id")

        If idideastr = "" Then
            sql.Append("select MAX(id) from idea")
            ididea = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)
        Else
            ididea = idideastr
        End If

        'consulta de estado de idea
        sql = New StringBuilder
        sql.Append(" select typeapproval from Idea where id = " & ididea)
        approval_idea = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If approval_idea = 1 Then

            sql = New StringBuilder
            sql.Append(" select ActNumber,ApprovalDate,Approved from IdeaApprovalRecord where Ididea = " & ididea)
            Data_aprobacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If Data_aprobacion.Rows.Count > 0 Then

                If IsDBNull(Data_aprobacion.Rows(0)("ActNumber")) = False Then
                    MinutesApproval = Data_aprobacion.Rows(0)("ActNumber")
                End If

                If IsDBNull(Data_aprobacion.Rows(0)("ApprovalDate")) = False Then
                    DateofApproval = Data_aprobacion.Rows(0)("ApprovalDate")
                End If

                If IsDBNull(Data_aprobacion.Rows(0)("Approved")) = False Then
                    ApprovedBy = Data_aprobacion.Rows(0)("Approved")

                    Select Case ApprovedBy
                        Case 1
                            ApprovedBy = "Comité de Contratación"
                        Case 0
                            ApprovedBy = "Junta Directiva"
                    End Select
                End If

            End If
        Else
            MinutesApproval = "No Aplica"
            DateofApproval = "No Aplica"
            ApprovedBy = "No Aplica"
        End If


        sql = New StringBuilder

        sql.Append("select convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.','')) from Idea i  ")
        sql.Append("inner join ThirdByIdea ti on i.Id = ti.IdIdea  ")
        sql.Append(" inner join Third t on t.Id= ti.IdThird ")
        sql.Append("where (t.code = '8600383389' And ti.IdIdea = " & ididea & ")")

        FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If FSCval = 0 Then
            vt5 = "0"
        Else
            vt5 = Format(Convert.ToInt64(FSCval), "#,###.##")
        End If
        sql = New StringBuilder
        sql.Append("select sum(convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.',''))) from Idea i   ")
        sql.Append("inner join ThirdByIdea ti on i.Id=ti.IdIdea  ")
        sql.Append(" inner join Third t on t.Id= ti.IdThird ")
        sql.Append("where(t.code <> '8600383389' And ti.IdIdea = " & ididea & ")")


        Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If otrosval = 0 Then
            vt4 = "0"
        Else
            vt4 = Format(Convert.ToInt64(otrosval), "#,###.##")
        End If

        sql = New StringBuilder
        sql.Append("select Name,Justification,Objective,AreaDescription,results,ResultsKnowledgeManagement,ResultsInstalledCapacity,OtherResults,StartDate,Duration,days,Cost,obligationsoftheparties,RiskMitigation,RisksIdentified,BudgetRoute,ideaappliesIVA from idea ")
        sql.Append(" where id = " & ididea)

        ' ejecutar la intruccion
        Data_idea = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        sql = New StringBuilder

        sql.Append(" select i.Id, p.Name as objetivo, l.Name as linea_estra from idea i ")
        sql.Append(" inner join ProgramComponentByIdea pci on pci.IdIdea= i.Id ")
        sql.Append(" inner join ProgramComponent pc on pc.Id = pci.IdProgramComponent ")
        sql.Append(" inner join Program p on p.Id = pc.IdProgram ")
        sql.Append(" inner join StrategicLine l on l.Id = p.IdStrategicLine ")

        sql.Append(" where i.id = " & ididea)

        Data_componet = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" select distinct p.Code as objetivo_estrategico from ProgramComponentByIdea pci ")
        sql.Append(" inner join ProgramComponent pc on pci.IdProgramComponent = pc.Id ")
        sql.Append(" inner join Program P ON P.Id = pc.IdProgram ")

        sql.Append(" where pci.IdIdea = " & ididea)

        Data_component_group = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder
        sql.Append(" select  tp.Contract, p.Pupulation  from Idea i ")
        sql.Append(" inner join typecontract tp on tp.id = i.Idtypecontract ")
        sql.Append(" inner join Population p on p.Id= i.Population ")

        sql.Append(" where i.id = " & ididea)

        Data_others = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append("select ti.Name, ti.Type, ti.Contact,ti.Documents,ti.Phone, ti.Email, ti.Vrmoney, ti.VrSpecies, ti.FSCorCounterpartContribution, ti.generatesflow  from ThirdByIdea ti where ti.IdIdea = " & ididea)

        Data_detalles_actores = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        sql = New StringBuilder
        sql.Append(" select dep.Name as dapartamento, c.Name as municipio from LocationByIdea li ")
        sql.Append(" inner join FSC_eSecurity.dbo.depto dep on dep.id = li.iddepto ")
        sql.Append(" inner join FSC_eSecurity.dbo.City c on c.ID = li.IdCity ")
        sql.Append(" where li.ididea = " & ididea)

        Data_ubicacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        If Data_componet.Rows.Count > 0 Then

            If IsDBNull(Data_componet.Rows(0)("linea_estra")) = False Then
                ddls = Data_componet.Rows(0)("linea_estra")
            Else
                ddls = ""
            End If

        End If

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

            If IsDBNull(Data_idea.Rows(0)("AreaDescription")) = False Then
                objetesp = Data_idea.Rows(0)("AreaDescription")
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

            If IsDBNull(Data_idea.Rows(0)("StartDate")) = False Then
                fech = Data_idea.Rows(0)("StartDate")
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

            If IsDBNull(Data_idea.Rows(0)("cost")) = False Then
                vt3 = Data_idea.Rows(0)("cost")
                vt6 = Data_idea.Rows(0)("cost")
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



        Dim contador As Integer = 0
        Dim contadoractor As Integer = 0
        Dim contadorcomp As Integer = 0

        Dim swicth As Integer = 0
        Dim swicth_actor As Integer = 0

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=IdeaExport.doc")
        Response.Charset = "UTF8Encoding"
        Response.ContentType = "application/vnd.ms-word "


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

        If resulotros = "" Then
            resulotros = "No Aplica"
        End If

        Response.Output.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%"">")
        Response.Output.WriteLine("<body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>")
        Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 20%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>" & ddls.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Objetivos Estrat&eacute;gicos:</strong></span></td><td>")

        Dim valuar_compo As Integer = Data_component_group.Rows.Count
        valuar_compo = valuar_compo - 1

        Dim celda_componente As Integer = 0

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

        Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre de la Idea:</strong></span></td><td>" & name.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>")

        Dim lbldepto, lblcity As String


        Dim valuar_ubi As Integer = Data_ubicacion.Rows.Count
        valuar_ubi = valuar_ubi - 1


        Dim celda_ubicacion As Integer = 0

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

        Dim fechafinal = calculafechas(Convert.ToDateTime(fech), dura, dia)

        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Finalizaci&oacute;n:</strong></span></td><td>" & Convert.ToString(fechafinal) & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>" & ddlc.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Ruta Presupuestal:</strong></span></span></td><td style=""text-align: justify;"">" & ruta.ToString() & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>" & Format(Convert.ToInt64(vt3), "#,###.##") & "</td></tr>")

        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No. de Acta de Aprobaci&oacute;n:</strong></span></td><td>" & MinutesApproval & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Aprobado Por:</strong></span></td><td>" & ApprovedBy & "</td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Aprobaci&oacute;n:</strong></span></td><td>" & DateofApproval & "</td></tr>")

        'validamos si aplica iva y lo traducimos
        If aplica_iva = 1 Then
            aplica_iva = "Si"
        Else
            aplica_iva = "No"
        End If

        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Aplica IVA:</strong></span></td><td> " & aplica_iva.ToString() & " </td></tr>")
        Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No. de Idea:</strong></span></td><td>" & ididea.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr>")

        Dim lblname, labelcontact, labeldocument, labeltype, labelphone, labelemail As String

        Dim celda_det_actors_dat As Integer = 0

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


        Dim celda_det_actors As Integer = 0

        Dim name_actor, V_Efectivo, V_Especie, V_total, T_efectivo, T_especies, T_total, flujos_gene As String

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

        sql = New StringBuilder

        sql.Append("select sum(cast(replace(Vrmoney,'.','') as int))as v_money, sum(cast(replace(VrSpecies,'.','') as int)) as v_especie,sum(cast(replace(FSCorCounterpartContribution,'.','') as int)) as V_total from ThirdByIdea where ididea =" & ididea)
        Data_totales_actors = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

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

        name_actor = ""
        Dim name_str As String
        Dim celdanombre As Integer = 0

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

        sql = New StringBuilder

        sql.Append("select N_pagos,valorparcial, porcentaje,entregable,fecha  from Paymentflow where ididea = " & ididea)
        Data_pagos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim celdapago As Integer = 0
        Dim celdadetalle As Integer = 0

        Dim valorp, porsent, entregp, fechap, np, detalles, aport, desem As String

        If Data_pagos.Rows.Count > 0 Then

            For Each pagoitem In Data_pagos.Rows

                If IsDBNull(Data_pagos.Rows(celdapago)("N_pagos")) = False Then
                    np = Data_pagos.Rows(celdapago)("N_pagos")

                    detalles = ""
                    celdadetalle = 0

                    sql = New StringBuilder

                    sql.Append("select n_pago, Aportante, Desembolso from Detailedcashflows where IdIdea = " & ididea & " and N_pago = " & np)
                    Data_pagos_detalles = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

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

            sql = New StringBuilder

            sql.Append("select sum(valorparcial) from Paymentflow where ididea =" & ididea)
            Dim valtpagos = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            Response.Output.WriteLine("<tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Total</strong></span></td><td style=""width: 16%;"">" & Format(Convert.ToInt64(valtpagos), "#,###.##") & "</td><td style=""width: 5%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>100%</strong></span></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td><td style=""width: 16%;""></td></tr><tr>")


        End If

        Response.Output.WriteLine("</tbody></table><p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>")
        Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%; text-align: justify;"">" & mitigacion.ToString() & "</td><td style=""text-align: justify;"">" & riesgos.ToString() & "</td></tr></tbody></table>")
        Response.Output.WriteLine("<p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p>")

        Response.Flush()
        Response.End()

        Try


        Catch ex As Exception

        End Try
    End Function

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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite cargar los archivos sleeccionados
    ''' </summary>
    ''' <param name="objIdea">objeto de tipo IdeaEntity</param>
    ''' <param name="userId">Identificador del usuario actual</param>
    ''' <remarks></remarks>
    Private Sub LoadFiles(ByVal objIdea As IdeaEntity, ByVal userId As Long)

        'Definiendo los objtetos
        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files

        'Se verifica que existan archivos por cargar
        If ((Not files Is Nothing) AndAlso (files.Count > 0)) Then

            'Se verifica la opción actual
            If (Request.QueryString("op").Equals("add")) Then

                'Se instancia la lista de documentos
                objIdea.DOCUMENTLIST = New List(Of DocumentsEntity)

            Else
                'Se recupera la lista de documentos de la variable de sesion
                If (Me.DocumentsList Is Nothing) Then
                    objIdea.DOCUMENTLIST = New List(Of DocumentsEntity)
                Else
                    objIdea.DOCUMENTLIST = Me.DocumentsList
                End If

            End If

            'Se recorre la lista de archivos cargados al servidor
            For i As Integer = 0 To files.Count - 1

                Dim file As HttpPostedFile = files(i)
                If (file.ContentLength > 0) Then

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
                    objIdea.DOCUMENTLIST.Add(objDocument)

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Metodo que permite cargar la lista de Componentes del Programa almacenadas en la base de datos
    ''' </summary>
    ''' <param name="unObjIdea">onjeto de tipo IdeaEntity</param>
    ''' <remarks></remarks>
    Private Sub LoadProgramComponents(ByVal unObjIdea As IdeaEntity)

        'Se verifica que existan valores en la lista de actividades esppecificas
        'If (unObjIdea.ProgramComponentBYIDEALIST.Count > 0) Then

        '    'Se verifica si el id del Linea Estrategica actual es igual al id del Linea Estrategica almacenado
        '    If (Me.ddlStrategicLines.SelectedValue <> unObjIdea.ProgramComponentBYIDEALIST(0).IDStrategicLine) Then
        '        'Se selecciona el item del Linea Estrategica
        '        Me.ddlStrategicLines.SelectedValue = unObjIdea.ProgramComponentBYIDEALIST(0).IDStrategicLine
        '        'Se carga la lista de Programs
        '        '          Me.LoadDropDownListPrograms()
        '    End If
        '    'Se selecciona el item de la Programa
        '    Me.ddlPrograms.SelectedValue = unObjIdea.ProgramComponentBYIDEALIST(0).IDProgram
        '    'Se limpia la lista de activiades especificas
        '    Me.dlbActivity.AviableItems.Items.Clear()
        '    'Se carga la lista de Componentes del Programa por idea de la base de datos
        '    Me.LoadListProgramComponents()
        '    'Se recorre la lista de actividades seleccionadas y almacenadas en la base de datos
        '    Dim miItem As ListItem
        '    For Each myProgramComponent As ProgramComponentByIdeaEntity In unObjIdea.ProgramComponentBYIDEALIST
        '        'Se elimina la Componente del Programa actual de la lista de disponibles.
        '        miItem = Me.dlbActivity.AviableItems.Items.FindByValue(myProgramComponent.idProgramComponent.ToString())
        '        Me.dlbActivity.AviableItems.Items.Remove(miItem)
        '        'Se agrega en la lista de seleccionadas
        '        ' If Not (miItem Is Nothing) Then Me.dlbActivity.SelectedItems.Items.Add(miItem)
        '        Me.dlbActivity.SelectedItems.Items.Add(New ListItem(myProgramComponent.CODE, myProgramComponent.idProgramComponent))
        '    Next
        'End If

    End Sub

    ''' <summary>
    ''' Permite realizar la  limpieza de los controles de la pestaña de terceros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanThird()
        'Me.ddlThird.SelectedIndex = 0
        'Me.txtActor.Text = ""
        Me.txtActions.Text = ""
        Me.Txtcontact.Text = ""
        Me.Txtcedulacont.Text = ""
        Me.Txttelcont.Text = ""
        Me.Txtemail.Text = ""
        Me.Txtvrdiner.Text = ""
        Me.Txtvresp.Text = ""
        Me.Txtaportfscocomp.Text = ""
        '  Me.ddlactors.SelectedValue = "-1"

        Me.ddlType.SelectedIndex = 0
        'Me.txtActor.Focus()
    End Sub
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
            'Se llama al metodo que permite cargar el combo de convocatoria
            Me.LoadDropDownListSummoning(facade, applicationCredentials)

            'Se llama al metodo que permite cargar el combo de departamentos
            '       Me.LoadDropDownDepto(facade, applicationCredentials)

            'Cargar la lista de los municipos
            '      Me.LoadDropDownCities()

            'TODO: 13 se habilita combo terceros 
            'Autor: german Rodriguez MGgroup

            'Se llama la metodo que permite cargar el combo de terceros
            '  Me.LoadDropDownListThird(facade, applicationCredentials)

            'TODO: 13 se habilita combo terceros 
            'Autor: german Rodriguez MGgroup
            'cierre de proyecto

            'Se llama al metodo que permite cargar el combo de Linea Estrategica
            '    Me.LoadDropDownListStrategicLines(facade, applicationCredentials)

            'Se llama al metodo que permite cargar el combo de Programas
            '      Me.LoadDropDownListPrograms()

            'Se llama al metodo que permite cargar la lista de Componentes del Programa
            Me.LoadListProgramComponents()

            ' TODO:14 Metodo que permite cargar el combo de tipo de contratos
            ' Autor: German Rodriguez MGgroup

            '            Me.LoadDropDownListtypecontract(facade, applicationCredentials)

            ' TODO:14 Metodo que permite cargar el combo de tipo de contratos
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio


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
    ''' Metodo que permite cargar el combo de convocatoria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListSummoning(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        'Se pobla el combo
        If (Request.QueryString("op").Equals("add")) Then
            Me.ddlSummoning.DataSource = facade.getSummoningList(applicationCredentials, enabled:="1", order:="code")
        Else
            Me.ddlSummoning.DataSource = facade.getSummoningList(applicationCredentials, order:="code")
        End If
        Me.ddlSummoning.DataValueField = "Id"
        Me.ddlSummoning.DataTextField = "Code"
        Me.ddlSummoning.DataBind()

        ' agregar la opcion No Aplica
        Me.ddlSummoning.Items.Add(New ListItem("No Aplica", "0"))

        ' seleccionar
        Me.ddlSummoning.SelectedValue = "0"


    End Sub

    ''' <summary>
    ''' Permite cargar la lista de departamentos.
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownDepto(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

    '    ' cargar la lista de los departamentos
    '    If (Request.QueryString("op").Equals("add")) Then
    '        Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, enabled:="T", order:="Depto.Code")
    '    Else
    '        Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, order:="Depto.Code")
    '    End If
    '    Me.ddlDepto.DataValueField = "Id"
    '    Me.ddlDepto.DataTextField = "Name"
    '    Me.ddlDepto.DataBind()

    'End Sub

    ''' <summary>
    ''' Permite cargar la lista de municipios segun un depto seleccionado.
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownCities()
    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try
    '        'Se carga la lista de los municipos
    '        If (Request.QueryString("op").Equals("add")) Then
    '            Me.ddlCity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, enabled:="T", order:="City.Code")
    '        Else
    '            Me.ddlCity.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, order:="City.Code")
    '        End If
    '        Me.ddlCity.DataValueField = "Id"
    '        Me.ddlCity.DataTextField = "Name"
    '        Me.ddlCity.DataBind()
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

    ''' <summary>
    ''' TODO:15 Metodo que permite cargar el combo de tipo de contratos
    ''' Autor: German Rodriguez MGgroup
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownListtypecontract(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)
    '    'Se pobla el combo
    '    If (Request.QueryString("op").Equals("add")) Then
    '        Me.ddlmodcontract.DataSource = facade.gettypecontract(applicationCredentials, order:="id")
    '    Else
    '        Me.ddlmodcontract.DataSource = facade.gettypecontract(applicationCredentials, order:="id")
    '    End If
    '    Me.ddlmodcontract.DataValueField = "Id"
    '    Me.ddlmodcontract.DataTextField = "Contract"
    '    Me.ddlmodcontract.DataBind()

    'End Sub
    ' TODO:15 Metodo que permite cargar el combo de tipo de contratos
    ' Autor: German Rodriguez MGgroup
    ' cierre de cambio

    ''' <summary>
    ''' Metodo que permite cargar el combo de terceros
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownListThird(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)
    '    'Se pobla el combo
    '    If (Request.QueryString("op").Equals("add")) Then
    '        Me.ddlactors.DataSource = facade.getThirdList(applicationCredentials, enabled:="1", order:="Code")
    '    Else
    '        Me.ddlactors.DataSource = facade.getThirdList(applicationCredentials, order:="Code")
    '    End If
    '    Me.ddlactors.DataValueField = "Id"
    '    Me.ddlactors.DataTextField = "Name"
    '    Me.ddlactors.DataBind()

    'End Sub

    ''' <summary>
    ''' Metodo que permite cargar el combo de Linea Estrategica
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownListStrategicLines(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

    '    'Se pobla el combo
    '    If (Request.QueryString("op").Equals("add")) Then
    '        Me.ddlStrategicLines.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="Code")
    '    Else
    '        Me.ddlStrategicLines.DataSource = facade.getStrategicLineList(applicationCredentials, order:="Code")
    '    End If
    '    Me.ddlStrategicLines.DataValueField = "Id"
    '    Me.ddlStrategicLines.DataTextField = "Code"
    '    Me.ddlStrategicLines.DataBind()

    'End Sub

    ''' <summary>
    ''' Metodo que permite cargar el combo de Programa
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub LoadDropDownListPrograms()

    '    ' definir los objetos
    '    Dim facade As New Facade
    '    Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

    '    Try
    '        'Se pobla el combo
    '        If (Request.QueryString("op").Equals("add")) Then
    '            Me.ddlPrograms.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlStrategicLines.SelectedValue, enabled:="1", order:="Code")
    '        Else
    '            Me.ddlPrograms.DataSource = facade.getProgramList(applicationCredentials, idStrategicLine:=Me.ddlStrategicLines.SelectedValue, order:="Code")
    '        End If
    '        Me.ddlPrograms.DataValueField = "Id"
    '        Me.ddlPrograms.DataTextField = "Code"
    '        Me.ddlPrograms.DataBind()

    '        'Se limpia la lista de Componentes del Programa
    '        Me.dlbActivity.AviableItems.Items.Clear()
    '        ' Me.dlbActivity.SelectedItems.Items.Clear()

    '        'Se llama al metodo que permite crear cargar la lista de Componentes del Programa
    '        Me.LoadListProgramComponents()

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

    ''' <summary>
    ''' Metodo que permite cargar la lista de Componentes del Programa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListProgramComponents()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se verifica que exista una Programa previamente seleccionada
            'If (Me.ddlPrograms.SelectedValue.Length > 0) Then
            '    'Se pobla el combo
            '    If (Request.QueryString("op").Equals("add")) Then
            '        Me.dlbActivity.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=Me.ddlPrograms.SelectedValue, enabled:="1", order:="Code")

            '    Else
            '        Me.dlbActivity.AviableItems.DataSource = facade.getProgramComponentList(applicationCredentials, idProgram:=Me.ddlPrograms.SelectedValue, order:="Code")
            '    End If

            '    Me.dlbActivity.AviableItems.DataValueField = "Id"
            '    Me.dlbActivity.AviableItems.DataTextField = "Code"
            '    Me.dlbActivity.AviableItems.DataBind()

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
    '''documento 1
    ''' <summary>
    ''' Permite actualizar la lista de archivos anexos a la idea actual
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocumentsList()

        'Definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se definen los objetos
            Dim documentsByEntityList As List(Of DocumentsByEntityEntity)

            'Se llama al metodo que permite consultar la lista de documentos para el registro de idea actual
            'Se carga la lista de documentos para el registro de idea actual
            documentsByEntityList = facade.getDocumentsByEntityList(applicationCredentials, idnentity:=Request.QueryString("id"), entityName:=GetType(IdeaEntity).ToString())

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

#End Region

#Region "funciones"

    ''' <summary>
    ''' TODO: 16 funcion que verifica si la idea esta aprobada
    ''' Autor: german Rodriguez MG group
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaraprobacion() As String
        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim ididea As Integer
        ididea = Me.txtid.Text
        sql.Append("SELECT Ididea FROM  ProjectApprovalRecord WHERE Ididea=" & ididea)
        Dim idideaaprovada = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If idideaaprovada <> 0 Then
            Me.lblsaveinformation.Text = "Esta Idea ya se encuentra aprobada y NO puede ser modificada!"
            Me.containerSuccess.Visible = "True"
            ' Me.lblsaveinformation.ForeColor = Drawing.Color.Green
            Me.btnDelete.Visible = "false"
            Me.btnSave.Visible = "false"
        End If

    End Function

    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)

        Return result

    End Function


#End Region

End Class