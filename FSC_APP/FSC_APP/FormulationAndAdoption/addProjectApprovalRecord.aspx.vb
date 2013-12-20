Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addProjectApprovalRecord
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

            ' cargar los combos
            loadCombos()

            Me.ddlidproject.Items.Insert(0, New ListItem("Seleccione...", "-1"))
            Me.ddlidproject.SelectedValue = "-1"

           
            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "APROBACIÓN DE UNA IDEA."

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
                    Me.rfvid.Visible = False
                    Me.lblcode.Visible = False
                    Me.txtcode.Visible = False
                    Dim idProcessInstance As String = String.Empty
                    Dim idActivityInstance As String = String.Empty
                    Dim IdEntryData As String = String.Empty
                    Me.ddlapproved.SelectedValue = 1

                    ' cargar los valores del BPM
                    idProcessInstance = Request.QueryString("idProcessInstance")
                    idActivityInstance = Request.QueryString("idActivityInstance")
                    IdEntryData = Request.QueryString("IdEntryData")

                    ' verificar si viene desde el BPM
                    If idProcessInstance IsNot Nothing Then

                        Me.lblBPMMessage.Visible = True
                        Me.rblCondition.Visible = True
                        Me.btnCancel.Visible = False

                        ' cargar la lista de condiciones de la actividad
                        Dim conditions As Array = GattacaApplication.getConditions(applicationCredentials, idActivityInstance)

                        For Each condition As ListItem In conditions

                            ' cargar la lista de condiciones para la actividad
                            Me.rblCondition.Items.Add(New ListItem(condition.Text, condition.Value))

                        Next

                        ' seleccionar el primero
                        Me.rblCondition.SelectedIndex = 0

                        ' cargar el proyecto
                        Me.ddlidproject.SelectedValue = IdEntryData
                        Me.ddlidproject.Enabled = False

                        ' cargar el mensaje
                        Me.lblBPMMessage.Text = PublicFunction.getSettingValue("BPM.Condition.Message")

                    End If

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR EL REGISTRO DE APROBACIÓN DE UN PROYECTO."

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
                    Me.lblcode.Enabled = False
                    Me.txtcode.Enabled = False
                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objProjectApprovalRecord As New ProjectApprovalRecordEntity

                    Try
                        ' cargar el registro referenciado
                        objProjectApprovalRecord = facade.loadProjectApprovalRecord(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objProjectApprovalRecord.id
                        Me.ddlidproject.SelectedValue = objProjectApprovalRecord.idproject
                        Me.txtcode.Text = objProjectApprovalRecord.code
                        Me.txtcomments.Text = objProjectApprovalRecord.comments
                        If objProjectApprovalRecord.approvaldate <> defaultDate Then
                            Me.txtapprovaldate.Text = objProjectApprovalRecord.approvaldate
                        Else
                            Me.txtapprovaldate.Text = ""
                        End If
                        Me.txtactnumber.Text = objProjectApprovalRecord.actnumber
                        Me.txtapprovedvalue.Text = objProjectApprovalRecord.approvedvalue.ToString("#,###")
                        Me.ddlapproved.SelectedValue = objProjectApprovalRecord.approved
                        Me.ddlenabled.SelectedValue = objProjectApprovalRecord.enabled
                        Me.txtiduser.Text = objProjectApprovalRecord.USERNAME
                        Me.txtcreatedate.Text = objProjectApprovalRecord.createdate

                        ' cargar y habilitar el archivo anexo
                        Me.hlattachment.NavigateUrl = PublicFunction.getSettingValue("documentPath") _
                                                        & "\" & objProjectApprovalRecord.attachment
                        Me.hlattachment.Text = objProjectApprovalRecord.attachment
                        Me.hlattachment.Visible = True

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objProjectApprovalRecord = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click
        Dim salvar As Integer = 0
        Dim objProjectApprovalRecord As New ProjectApprovalRecordEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty
        Me.Label7.Text = ""
        Me.Label9.Text = ""


        If Me.btnAddData.Text = "Confirmar aprobación" Then
            salvar = 1
    
        Else

            Me.btnAddData.Text = "Confirmar aprobación"
            Me.Txtline.Text = Me.HDline.Value
            Me.Txtnameidea.Text = Me.HDidea.Value
            Me.Txtprogram.Text = Me.HDprogram.Value
            Me.txtapprovedvalue.Text = Me.HDvalue.Value
            Me.lblsaveinformation.Text = "Está seguro que desea aprobar la idea?, tenga en cuenta que una vez aprobada NO podrá ser modificada.  "
            Me.containerSuccess.Visible = "True"
            Dim valorcampo As Long = 0

            If validarcamposnum() = 1 Then
                Me.Txtaportcontra.Text = ""
                Me.TxtaportFSC.Text = ""
                Me.txtapprovedvalue.Text = ""
                Exit Sub
            Else
                Me.lblHelpapprovedvalue.Text = ""
            End If
           
            
            ' Me.lblsaveinformation.ForeColor = Drawing.Color.Green

            'Subir el archivo
            Dim LOADFILES As String = PublicFunction.LoadFile(Request)
            Me.filename.Value = LOADFILES
            Me.Lbltitlearchivo.Text = LOADFILES

        End If

        If salvar = 1 Then

            ' definir los objetos
            Dim facade As New Facade
           
            Me.Txtline.Text = Me.HDline.Value
            Me.Txtnameidea.Text = Me.HDidea.Value
            Me.Txtprogram.Text = Me.HDprogram.Value

            'Post-verificación de código
            If Not verifyCode() Then
                Return
            End If

            Try
                ' cargar los valores registrados por el usuario
                objProjectApprovalRecord.idproject = Me.ddlidproject.SelectedValue
                objProjectApprovalRecord.code = Me.txtcode.Text

                'TODO: 21 campos nuesvos para la validacion de aprobacion idea
                '12-06-2013 german rodriguez 

                objProjectApprovalRecord.codeapprovedidea = Me.txtcodeapproved.Text
                objProjectApprovalRecord.Ididea = Me.ddlidproject.SelectedValue

                objProjectApprovalRecord.comments = Me.txtcomments.Text
                objProjectApprovalRecord.approvaldate = IIf((Me.txtapprovaldate.Text = ""), Nothing, Me.txtapprovaldate.Text)
                objProjectApprovalRecord.actnumber = clean_vbCrLf(Me.txtactnumber.Text)

                'TODO: 21 campos nuesvos para la validacion de aprobacion idea
                '24-08-2013 german rodriguez 

                objProjectApprovalRecord.approvedvalue = PublicFunction.ConvertStringToDouble(Me.HDvalue.Value)
                objProjectApprovalRecord.aportOtros = Me.Txtaportcontra.Text
                objProjectApprovalRecord.aportFSC = Me.TxtaportFSC.Text

                objProjectApprovalRecord.approved = Me.ddlapproved.SelectedValue
                objProjectApprovalRecord.enabled = Me.ddlenabled.SelectedValue
                objProjectApprovalRecord.iduser = applicationCredentials.UserID
                objProjectApprovalRecord.createdate = Now

                objProjectApprovalRecord.attachment = Me.filename.Value

                ' cargar los valores del BPM
                idProcessInstance = Request.QueryString("idProcessInstance")
                idActivityInstance = Request.QueryString("idActivityInstance")

                If idProcessInstance IsNot Nothing Then

                    objProjectApprovalRecord.IdProcessInstance = idProcessInstance
                    objProjectApprovalRecord.IdActivityInstance = idActivityInstance

                End If

                ' almacenar la entidad
                objProjectApprovalRecord.id = facade.addProjectApprovalRecord(applicationCredentials, objProjectApprovalRecord)

                If idProcessInstance IsNot Nothing Then

                    ' finalizar la actividad actual
                    GattacaApplication.endActivityInstance(applicationCredentials, idProcessInstance, idActivityInstance, _
                                                           Me.rblCondition.SelectedValue, "Se ha registrado la aprobacion del proyecto", _
                                                           "", "", "", "")
                    ' ir a la pagina de lista de tareas
                    Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

                Else

                    ' ir al administrador
                    Me.lblsaveinformation.Text = "La idea se aprobó exitosamente!  "
                    Me.containerSuccess.Visible = "True"
                    ' Me.lblsaveinformation.ForeColor = Drawing.Color.Green
                    Me.btnAddData.Visible = False
                    Me.btnCancel.Visible = False
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
                objProjectApprovalRecord = Nothing
                facade = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("/fsc/ResearchAndDevelopment/searchIdea.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objProjectApprovalRecord As New ProjectApprovalRecordEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Post-verificación de código
        If Not verifyCode() Then
            Return
        End If

        ' cargar el registro referenciado
        objProjectApprovalRecord = facade.loadProjectApprovalRecord(applicationCredentials, Request.QueryString("Id"))

        Try
            ' cargar los datos           
            objProjectApprovalRecord.idproject = Me.ddlidproject.SelectedValue
            objProjectApprovalRecord.code = Me.txtcode.Text
            objProjectApprovalRecord.comments = Me.txtcomments.Text
            'TODO: 21 campos nuesvos para la validacion de aprobacion idea
            '12-06-2013 german rodriguez 

            objProjectApprovalRecord.codeapprovedidea = Me.txtcodeapproved.text
            objProjectApprovalRecord.Ididea = Me.ddlidproject.SelectedValue

            objProjectApprovalRecord.approvaldate = IIf((Me.txtapprovaldate.Text = ""), Nothing, Me.txtapprovaldate.Text)
            objProjectApprovalRecord.actnumber = Me.txtactnumber.Text
            objProjectApprovalRecord.approvedvalue = PublicFunction.ConvertStringToDouble(Me.txtapprovedvalue.Text)            
            objProjectApprovalRecord.approved = Me.ddlapproved.SelectedValue
            objProjectApprovalRecord.enabled = Me.ddlenabled.SelectedValue

            ' modificar el registro
            facade.updateProjectApprovalRecord(applicationCredentials, objProjectApprovalRecord)

            ' ir al administrador
            Response.Redirect("searchProjectApprovalRecord.aspx")

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
            objProjectApprovalRecord = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteProjectApprovalRecord(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchProjectApprovalRecord.aspx")

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

    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idClosedState As String = ""

        Try
            'Se consulta el código correspondiente a la fase de "Evaluación y Cierre"
            'idClosedState = ConfigurationManager.AppSettings("IdClosedState").ToString()

            ' cargar la lista de los tipos




            If (Request.QueryString("op").Equals("add")) Then

                Me.ddlidproject.DataSource = facade.getProjectListNotInPhaseapproval(applicationCredentials, idphase:=idClosedState, enabled:="1", order:="Code", isLastVersion:="1")
            Else
                Me.ddlidproject.DataSource = facade.getProjectListNotInPhaseapproval(applicationCredentials, idphase:=idClosedState, order:="Code", isLastVersion:="1")
            End If

            Me.ddlidproject.DataValueField = "code"
            Me.ddlidproject.DataTextField = "name"
            Me.ddlidproject.DataBind()

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

            If facade.verifyProjectApprovalRecordCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
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

#Region "funciones"

    ''' <summary>
    ''' TODO: 100 funcion que revisa los enter en el guardar y editar idea
    ''' autor: German Rodriguez 16/10/2013 MGgroup
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)

        Return result

    End Function
    ' TODO: 100 funcion que revisa los enter en el guardar y editar idea
    ' Autor: german Rodriguez MG group
    ' cierre de cambio



    Private Function validarcamposnum()

        Dim shwich As Integer = 0
        Dim valorcampo As Long = 0
        Dim val1 = Replace(Me.Txtaportcontra.Text, ".", "")
        Dim val2 = Replace(Me.TxtaportFSC.Text, ".", "")

        Try
            valorcampo = Convert.ToInt64(val1) + Convert.ToInt64(val2)

        Catch ex As Exception
            Me.Label7.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
            Me.Label9.Text = "El valor ingresado supera el tamaño máximo permitido ($99.999.999.999). Por favor ingrese un menor valor."
            Me.Txtaportcontra.Text = ""
            Me.TxtaportFSC.Text = ""
            Me.txtapprovedvalue.Text = ""
            Me.containerSuccess.Visible = "false"
            Me.btnAddData.Text = "Aprobar Idea"
            shwich = 1
        End Try

        Return shwich

    End Function

#End Region



End Class
