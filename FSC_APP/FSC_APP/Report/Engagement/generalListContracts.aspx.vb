Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine

Partial Class Report_Engagement_GeneralListContracts
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve el valor que indica si la pagina presenta un reporte
    ''' </summary>
    ''' <value>valor booleano</value>
    ''' <returns>valor booleano</returns>
    ''' <remarks></remarks>
    Private Property ReportExist() As Boolean
        Get
            Return DirectCast(Session("reportExist"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Session("reportExist") = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna o devuelve el valor del combo de departamentos
    ''' </summary>
    ''' <value>identificador del departamento</value>
    ''' <returns>identificador del departamento</returns>
    ''' <remarks></remarks>
    Private Property FilterList() As List(Of String)
        Get
            Return DirectCast(Session("filterListReportIdeaInventory"), List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            Session("filterListReportIdeaInventory") = value
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
            ' cargar el titulo
            Session("lblTitle") = "Listado general de contratos."

            'Se llama al metodo que pobla los combos
            loadCombos()

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")

            ' de acuerdo a la opcion
            Select Case op

                Case "report"

                    'Se botienen los filtros seleccionados en la página.
                    Me.GetFiltersReport()

                    'Se llama al metodo que pobla la grilla
                    Me.LoadGridView()

                    ' actualizar los datos
                    Me.upData.Update()

                    'Se llama al metodo que recarga el reporte
                    Me.loadReport()

                    'Se indica que se ha cargado un reporte en la página
                    Me.ReportExist = True

                Case Else

                    'Se inicializan los controles requeridos
                    Me.ReportExist = False

            End Select

        Else

            'Se verifica si existe al gun freporte en la página
            If (Me.ReportExist) Then

                'Se llama al metodo que recarga el reporte
                Me.loadReport()

            End If

        End If

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargar los datos
        Me.LoadGridView()

    End Sub

    Protected Sub ddlManagement_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlManagement.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de Lineas estrategicas
        Me.LoadDropDownListStrategicLine()

    End Sub

    Protected Sub ddlStrategicLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrategicLine.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de proyectos
        Me.LoadDropDownListProjectByStrategicLine()

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        'Se asignan los filtro seleccionados por el usuario
        Me.SetFiltersReport()

        'Se llama al metodo que pobla la grila de contratos
        Me.LoadGridView()

        ' actualizar los datos
        Me.upData.Update()

        'Se borra el reporte
        Me.crvGeneralListContracts.ReportSource = Nothing
        Me.upReport.Update()

        'Se cola el valor de la bandera del reporte en falso
        Me.ReportExist = False

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite almacenar los filtros asignados por el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFiltersReport()

        Dim filterList As New List(Of String)

        'Se verifica si el campo Gerencia tiene algun valor
        If (Me.ddlManagement.SelectedValue.Length > 0) Then
            filterList.Add("ddlManagement;" & Me.ddlManagement.SelectedValue)
        End If

        'Se verifica si el campo Linea Estrategica tiene algun valor
        If (Me.ddlStrategicLine.SelectedValue.Length > 0) Then
            filterList.Add("ddlStrategicLine;" & Me.ddlStrategicLine.SelectedValue)
        End If

        'Se verifica si el campo Proyecto tiene algun valor
        If (Me.ddlProject.SelectedValue.Length > 0) Then
            filterList.Add("ddlProject;" & Me.ddlProject.SelectedValue)
        End If

        'Se verifica si el campo Estado del contrato tiene algun valor
        If (Me.ddlContractState.SelectedValue.Length > 0) Then
            filterList.Add("ddlContractState;" & Me.ddlContractState.SelectedValue)
        End If

        'Se verifica si el campo Vigencia presuppuestal tiene algun valor
        If (Me.txtEffectiveBudget.Text.Length > 0) Then
            filterList.Add("txtEffectiveBudget;" & Me.txtEffectiveBudget.Text)
        End If

        'Se verifica si el campo Nombre contratista tiene algun valor
        If (Me.txtContractorName.Text.Length > 0) Then
            filterList.Add("txtContractorName;" & Me.txtContractorName.Text)
        End If

        'Se verifica si el campo Supervisor tiene algun valor
        If (Me.txtSupervisor.Text.Length > 0) Then
            filterList.Add("txtSupervisor;" & Me.txtSupervisor.Text)
        End If

        'Se agrega la lista a la variable de session requerida
        Me.FilterList = filterList

    End Sub

    ''' <summary>
    ''' Permite asignar los valores de los filtros almacenados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetFiltersReport()

        'Se verifica que exiat una lista
        If Not (Me.FilterList Is Nothing) Then

            'Se recorren los elementos de la lista
            For Each filter As String In Me.FilterList
                Dim vector() As String
                vector = filter.Split(";")

                'Se buscan los controles que contienen un filtro
                Select Case vector(0)

                    Case "ddlManagement"
                        Me.ddlManagement.SelectedValue = vector(1)
                    Case "ddlStrategicLine"
                        Me.ddlStrategicLine.SelectedValue = vector(1)
                    Case "ddlProject"
                        Me.ddlProject.SelectedValue = vector(1)
                    Case "ddlContractState"
                        Me.ddlContractState.SelectedValue = vector(1)
                    Case "txtEffectiveBudget"
                        Me.txtEffectiveBudget.Text = vector(1)
                    Case "txtContractorName"
                        Me.txtContractorName.Text = vector(1)
                    Case "txtSupervisor"
                        Me.txtSupervisor.Text = vector(1)                    
                End Select

            Next

        End If

    End Sub

    ''' <summary>
    ''' Metodo encargado de cargar la consulta que permite poblar la grilla 
    ''' con el listado general de contratos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadGridView()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myManagement As String = ""
        Dim myStrategicLine As String = ""
        Dim myProject As String = ""
        Dim myContractState As String = ""
        Dim myEffectiveBudget As String = ""
        Dim myContractorName As String = ""
        Dim mySupervisor As String = ""

        Try
            'Se recuperan los valores de los filtros
            'myManagement = Request.Form("ctl00$cphPrincipal$ddlManagement")
            myManagement = Me.ddlManagement.SelectedValue
            myStrategicLine = Me.ddlStrategicLine.SelectedValue
            myProject = Me.ddlProject.SelectedValue
            myContractState = Me.ddlContractState.SelectedValue
            myEffectiveBudget = Me.txtEffectiveBudget.Text
            myContractorName = Me.txtContractorName.Text
            mySupervisor = Me.txtSupervisor.Text

            'Se llama al metodo que consulta la información requerida
            Me.gvData.DataSource = facade.loadReportGeneralListContracts(applicationCredentials, "", "", myManagement, myStrategicLine, myProject, myContractState, myEffectiveBudget, myContractorName, mySupervisor)
            Me.gvData.DataBind()

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            ReportDataSet = Nothing
            reportDoc = Nothing
            facade = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Metodo encargado de cargar el reporte requerido
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadReport()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim reportDoc As New ReportDocument
        Dim ReportDataSet As New DataSet
        Dim myContractNumber As String = ""
        Dim myRequestNumber As String = ""

        Try
            'Se recuperan los valores de los filtros
            myContractNumber = Request.QueryString("contractNumber")
            myRequestNumber = Request.QueryString("requestNumber")

            'Se Adjunta las tablas requeridas
            ReportDataSet.Tables.Add(facade.loadReportGeneralListContracts(applicationCredentials, contractNumber:=myContractNumber).Copy())
            ReportDataSet.DataSetName = "dsRptGeneralListContracts.xsd"
            ReportDataSet.Tables(0).TableName = "vReportGeneralListContracts"

            'Se adjunta la tabla con los datos de los contratistas
            ReportDataSet.Tables.Add(facade.loadReportContractorsByGeneralListContracts(applicationCredentials, myRequestNumber).Copy())
            ReportDataSet.Tables(1).TableName = "vReportContractorByGeneralListContracts"

            'Se adjunta la tabla con los datos de la lista de pagos
            ReportDataSet.Tables.Add(facade.loadReportPaymentsListByGeneralListContracts(applicationCredentials, myRequestNumber).Copy())
            ReportDataSet.Tables(2).TableName = "vReportPaymentsListByGeneralListContracts"

            'Se carga el reporte
            reportDoc.Load(Server.MapPath("GeneralListContracts.rpt"))
            reportDoc.SetDataSource(ReportDataSet)
            Me.crvGeneralListContracts.ReportSource = reportDoc

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            ReportDataSet = Nothing
            reportDoc = Nothing
            facade = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Permite poblar los combos de lformulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se agregan las rutinas para poblar el combo de Gerencias
            Me.ddlManagement.DataSource = facade.getManagementList(applicationCredentials, enabled:="1", order:="Code")
            Me.ddlManagement.DataValueField = "Id"
            Me.ddlManagement.DataTextField = "Code"
            Me.ddlManagement.DataBind()
            'Se agrega el item todos
            Me.ddlManagement.Items.Add(New ListItem("Todos", ""))
            Me.ddlManagement.SelectedValue = ""

            'Se llama al metodo que permite poblar el combo de Lineas Estrategicas
            Me.LoadDropDownListStrategicLine()

            'Se llama al metodo que permite poblar el combo de proyectos
            Me.LoadDropDownListProjectByStrategicLine()

            'Se pobla el combo de estados del contrado
            Me.ddlContractState.Items.Add(New ListItem("Abierto", "Abierto"))
            Me.ddlContractState.Items.Add(New ListItem("Cerrado", "Cerrado"))
            Me.ddlContractState.Items.Add(New ListItem("Todos", ""))
            Me.ddlContractState.SelectedValue = ""

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
    ''' Metodo que permite poblar el combo de Lineas Estrategicas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownListStrategicLine()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            'Se pobla el combo
            Me.ddlStrategicLine.DataSource = facade.getStrategicLineList(applicationCredentials, idmanagment:=Me.ddlManagement.SelectedValue, enabled:="1", order:="Code")
            Me.ddlStrategicLine.DataValueField = "Id"
            Me.ddlStrategicLine.DataTextField = "Code"
            Me.ddlStrategicLine.DataBind()

            'Se agrega el item todos
            Me.ddlStrategicLine.Items.Add(New ListItem("Todos", ""))
            Me.ddlStrategicLine.SelectedValue = ""

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
    Private Sub LoadDropDownListProjectByStrategicLine()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se limpia el combo de proyectos
            Me.ddlProject.Items.Clear()

            'Se pobla el combo
            Me.ddlProject.DataSource = facade.getProjectListByStrategicLine(applicationCredentials, idStrategicLine:=Me.ddlStrategicLine.SelectedValue, enabled:="1", order:="Code")
            Me.ddlProject.DataValueField = "idkey"
            Me.ddlProject.DataTextField = "Code"
            Me.ddlProject.DataBind()

            'Se agrega el item todos
            Me.ddlProject.Items.Add(New ListItem("Todos", ""))
            Me.ddlProject.SelectedValue = ""

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
