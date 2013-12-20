Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addExecutionContractualPlanRegistry
    Inherits System.Web.UI.Page

#Region "Eventos "

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
            Dim defaultDate As New DateTime

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
            Session("ExecutionContractualPlanRegistryDetailsList") = objExecutionContractualPlanRegistryDetailsList

            'Cargar combos
            loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "Registrar Plan de Ejecución Contractual."

                    ' ocultar algunos botones
                    Me.lblid.Visible = False
                    Me.lbliduser.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.lblDelete.Visible = False
                    Me.txtid.Visible = False
                    Me.txtiduser.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.btnAddData.Visible = True
                    Me.btnAdd.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "Editar un Registro del Plan de Ejecución Contractual."

                    ' ocultar algunos botones
                    Me.txtid.Enabled = False
                    Me.txtiduser.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lblDelete.Visible = False
                    Me.btnAddData.Visible = True
                    Me.btnAdd.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objExecutionContractualPlanRegistry As New ExecutionContractualPlanRegistryEntity

                    Try
                        ' cargar el registro referenciado
                        objExecutionContractualPlanRegistry = facade.loadExecutionContractualPlanRegistry(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objExecutionContractualPlanRegistry.id
                        Me.txtiduser.Text = objExecutionContractualPlanRegistry.username
                        Me.txtcreatedate.Text = objExecutionContractualPlanRegistry.createdate

                        ' cargar la lista de detalles
                        objExecutionContractualPlanRegistryDetailsList = facade.getExecutionContractualPlanRegistryDetailsList(applicationCredentials, idexecutioncontractualplanregistry:=objExecutionContractualPlanRegistry.id)
                        Session("ExecutionContractualPlanRegistryDetailsList") = objExecutionContractualPlanRegistryDetailsList
                        ' actualizar paneles y mostrar datos
                        gvData.DataSource = objExecutionContractualPlanRegistryDetailsList
                        gvData.DataBind()
                        upAddData.Update()

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objExecutionContractualPlanRegistry = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim objExecutionContractualPlanRegistryDetails As New ExecutionContractualPlanRegistryDetailsEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
        objExecutionContractualPlanRegistryDetailsList = Session("ExecutionContractualPlanRegistryDetailsList")

        Try
            ' cargar los valores registrados por el usuario
            objExecutionContractualPlanRegistryDetails.idproject = Me.ddlidproject.SelectedValue
            objExecutionContractualPlanRegistryDetails.projectname = Me.ddlidproject.SelectedItem.Text
            objExecutionContractualPlanRegistryDetails.concept = Me.txtconcept.Text
            objExecutionContractualPlanRegistryDetails.contractType = Me.ddlContractType.SelectedValue
            objExecutionContractualPlanRegistryDetails.contractTypeName = Me.ddlContractType.SelectedItem.Text
            objExecutionContractualPlanRegistryDetails.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            If Me.txtengagementdate.Text <> "" Then
                objExecutionContractualPlanRegistryDetails.engagementdate = Me.txtengagementdate.Text
            End If
            objExecutionContractualPlanRegistryDetails.comments = Me.txtcomments.Text
            objExecutionContractualPlanRegistryDetails.createdate = Now

            ' almacenar la entidad
            objExecutionContractualPlanRegistryDetailsList.Add(objExecutionContractualPlanRegistryDetails)
            Session("ExecutionContractualPlanRegistryList") = objExecutionContractualPlanRegistryDetailsList

            ' actualizar paneles y mostrar datos
            txtconcept.Text = ""
            ddlContractType.SelectedIndex = 0
            txttotalcost.Text = ""
            txtengagementdate.Text = ""
            Me.txtcomments.Text = ""

            gvData.DataSource = objExecutionContractualPlanRegistryDetailsList
            gvData.DataBind()
            upAddData.Update()

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
            objExecutionContractualPlanRegistryDetails = Nothing

        End Try

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        lblMessage.Text = ""
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objExecutionContractualPlanRegistry As New ExecutionContractualPlanRegistryEntity
        Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)

        ' cargar los datos
        objExecutionContractualPlanRegistry.iduser = applicationCredentials.UserID
        objExecutionContractualPlanRegistry.createdate = Now
        objExecutionContractualPlanRegistryDetailsList = Session("ExecutionContractualPlanRegistryDetailsList")
        objExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails = objExecutionContractualPlanRegistryDetailsList

        If objExecutionContractualPlanRegistryDetailsList.Count > 0 Then

            ' Guardar el registro
            facade.addExecutionContractualPlanRegistry(applicationCredentials, objExecutionContractualPlanRegistry)

            Try

                ' ir al administrador
                Response.Redirect("searchExecutionContractualPlanRegistry.aspx")

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
        Else

            'MsgBox("Debe tener por lo menos un detalle para guardar", MsgBoxStyle.OkOnly, "BPO FSC")
            lblMessage.Text = "Debe tener por lo menos un detalle para guardar"

        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objExecutionContractualPlanRegistry As New ExecutionContractualPlanRegistryEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
        objExecutionContractualPlanRegistryDetailsList = Session("ExecutionContractualPlanRegistryDetailsList")

        ' cargar el registro referenciado
        objExecutionContractualPlanRegistry = facade.loadExecutionContractualPlanRegistry(applicationCredentials, Request.QueryString("Id"))

        If objExecutionContractualPlanRegistryDetailsList.Count > 0 Then

            Try
                ' cargar los datos
                objExecutionContractualPlanRegistry.ExecutionContractualPlanRegistryEntityDetails = objExecutionContractualPlanRegistryDetailsList

                ' modificar el registro
                facade.updateExecutionContractualPlanRegistry(applicationCredentials, objExecutionContractualPlanRegistry)

                ' ir al administrador
                Response.Redirect("searchExecutionContractualPlanRegistry.aspx")

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
                objExecutionContractualPlanRegistry = Nothing

            End Try

        Else

            ' MsgBox("Debe tener por lo menos un detalle para guardar", MsgBoxStyle.OkOnly, "BPO FSC")
            lblMessage.Text = "Debe tener por lo menos un detalle para guardar"

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchExecutionContractualPlanRegistry.aspx")

    End Sub

    Protected Sub gvData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvData.SelectedIndexChanged

        ' Cargar la lista
        Dim objExecutionContractualPlanRegistryDetailsList As List(Of ExecutionContractualPlanRegistryDetailsEntity) = Session("ExecutionContractualPlanRegistryDetailsList")

        ' Quitar el registro de la lista
        objExecutionContractualPlanRegistryDetailsList.RemoveAt(Me.gvData.SelectedIndex)

        'Asignar de nuevo a la variable de sesión
        Session("ExecutionContractualPlanRegistryDetailsList") = objExecutionContractualPlanRegistryDetailsList

        ' mostrar los datos en la grilla
        Me.gvData.DataSource = objExecutionContractualPlanRegistryDetailsList
        Me.gvData.DataBind()
        Me.upAddData.Update()

    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound

        Dim defaultDate As New DateTime

        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If defaultDate.ToString.Equals(e.Row.Cells(4).Text) Then
                e.Row.Cells(4).Text = ""
            Else
                e.Row.Cells(4).Text = e.Row.Cells(4).Text
            End If

        End If
    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteExecutionContractualPlanRegistryDetails(applicationCredentials, 0, Request.QueryString("Id"))
            facade.deleteExecutionContractualPlanRegistry(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchExecutionContractualPlanRegistry.aspx")

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

    Protected Sub gvData_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvData.RowEditing
        Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
        Dim defaulDate As New DateTime
        Dim ECPD As New ExecutionContractualPlanRegistryDetailsEntity
        objExecutionContractualPlanRegistryDetailsList = Session("ExecutionContractualPlanRegistryDetailsList")
        ECPD = objExecutionContractualPlanRegistryDetailsList(e.NewEditIndex)
        Me.ddlidproject.SelectedValue = ECPD.idproject
        Me.txtconcept.Text = ECPD.concept
        Me.ddlContractType.SelectedValue = ECPD.contractType
        Me.txttotalcost.Text = ECPD.totalcost.ToString("#,###")
        If ECPD.engagementdate <> defaulDate Then
            Me.txtengagementdate.Text = ECPD.engagementdate
        Else
            Me.txtengagementdate.Text = ""
        End If
        Me.txtcomments.Text = ECPD.comments
        ViewState("indexDetailList") = e.NewEditIndex
        ViewState("editDetailList") = True
        Me.btnEditData.Visible = True
        Me.btnCancelEditData.Visible = True
        Me.btnAddData.Visible = False
    End Sub

    Protected Sub btnCancelEditData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelEditData.Click
        ViewState("editDetailList") = False
        Me.btnEditData.Visible = False
        Me.btnCancelEditData.Visible = False
        Me.btnAddData.Visible = True
        txtconcept.Text = ""
        ddlContractType.SelectedIndex = 0
        txttotalcost.Text = ""
        txtengagementdate.Text = ""
        Me.txtcomments.Text = ""
    End Sub

    Protected Sub btnEditData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditData.Click
        Dim objExecutionContractualPlanRegistryDetailsList As New List(Of ExecutionContractualPlanRegistryDetailsEntity)
        Dim ECPD As New ExecutionContractualPlanRegistryDetailsEntity
        objExecutionContractualPlanRegistryDetailsList = Session("ExecutionContractualPlanRegistryDetailsList")
        If ViewState("editDetailList") = True Then
            ' cargar los valores registrados por el usuario
            ECPD.idproject = Me.ddlidproject.SelectedValue
            ECPD.projectname = Me.ddlidproject.SelectedItem.Text
            ECPD.concept = Me.txtconcept.Text
            ECPD.contractType = Me.ddlContractType.SelectedValue
            ECPD.contractTypeName = Me.ddlContractType.SelectedItem.Text
            ECPD.totalcost = PublicFunction.ConvertStringToDouble(Me.txttotalcost.Text)
            If Me.txtengagementdate.Text <> "" Then
                ECPD.engagementdate = Me.txtengagementdate.Text
            End If
            ECPD.comments = Me.txtcomments.Text
            ECPD.createdate = Now

            ' almacenar la entidad
            objExecutionContractualPlanRegistryDetailsList(ViewState("indexDetailList")) = ECPD
            Session("ExecutionContractualPlanRegistryList") = objExecutionContractualPlanRegistryDetailsList
            Me.btnEditData.Visible = False
            Me.btnCancelEditData.Visible = False
            Me.btnAddData.Visible = True
            txtconcept.Text = ""
            ddlContractType.SelectedIndex = 0
            txttotalcost.Text = ""
            txtengagementdate.Text = ""
            Me.txtcomments.Text = ""
            gvData.DataSource = objExecutionContractualPlanRegistryDetailsList
            gvData.DataBind()
            upAddData.Update()
        End If
    End Sub

#End Region

#Region "Metodos"

    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idClosedState As String = ""

        Try
            'Se consulta el código correspondiente a la fase de "Evaluación y Cierre"
            idClosedState = ConfigurationManager.AppSettings("IdClosedState").ToString()

            ' cargar la lista de los proyectos
            Me.ddlidproject.DataSource = facade.getProjectListNotInPhase(applicationCredentials, idphase:=idClosedState, enabled:="1", order:="Code", isLastVersion:="1")
            Me.ddlidproject.DataValueField = "idkey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()

            ' cargar la lista de los tipos de contrato
            Me.ddlContractType.DataSource = facade.getContractTypeList(applicationCredentials, enabled:="1", order:="Name")
            Me.ddlContractType.DataValueField = "Id"
            Me.ddlContractType.DataTextField = "Name"
            Me.ddlContractType.DataBind()

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
