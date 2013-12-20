Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class Report_reportIdeaInventory
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

            ' cargar los combos
            Me.LoadCombos()

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")

            ' de acuerdo a la opcion
            Select Case op

                Case "report"

                    'Se botienen los filtros seleccionados en la página.
                    Me.GetFiltersReport()

                    'Se llama al metodo que pobla la grilla
                    Me.LoadGridView()

                    'Se llama al metodo que recarga el reporte
                    Me.LoadReport()

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
                Me.LoadReport()

            End If
        End If

        ' poner el titulo
        Session("lblTitle") = "Reporte inventario de ideas"

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargar los datos
        Me.LoadGridView()

    End Sub

    Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged

        'Se llama al metodo que permite crear cargar el combo de ciudades
        Me.LoadDropDownCities()

    End Sub

    Protected Sub bt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt.Click

        'Se asignan los filtro seleccionados por el usuario
        Me.SetFiltersReport()

        ' cargar el reporte
        Me.LoadGridView()

        'Se borra el reporte
        Me.crvReport.ReportSource = Nothing

        'Se cola el valor de la bandera del reporte en falso
        Me.ReportExist = False

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Permite almacenar los filtros asignados por el usuario.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFiltersReport()

        Dim filterList As New List(Of String)

        'Se verifica si el campo Fecha Registro Inicial tiene algun valor
        If (Me.txtDateStartReg.Text.Length > 0) Then
            filterList.Add("txtDateStartReg;" & Me.txtDateStartReg.Text)
        End If

        'Se verifica si el campo Fecha Registro Final tiene algun valor
        If (Me.txtDateEndReg.Text.Length > 0) Then
            filterList.Add("txtDateEndReg;" & Me.txtDateEndReg.Text)
        End If

        'Se verifica si el campo Nombre Idea tiene algun valor
        If (Me.txtIdeaName.Text.Length > 0) Then
            filterList.Add("txtIdeaName;" & Me.txtIdeaName.Text)
        End If

        'Se verifica si el campo Linea Estrategica tiene algun valor
        If (Me.ddlStrategicLines.SelectedValue.Length > 0) Then
            filterList.Add("ddlStrategicLines;" & Me.ddlStrategicLines.SelectedValue)
        End If

        'Se verifica si el campo Fuente tiene algun valor
        If (Me.ddlSource.SelectedValue.Length > 0) Then
            filterList.Add("ddlSource;" & Me.ddlSource.SelectedValue)
        End If

        'Se verifica si el campo Departamento tiene algun valor
        If (Me.ddlDepto.SelectedValue.Length > 0) Then
            filterList.Add("ddlDepto;" & Me.ddlDepto.SelectedValue)
        End If

        'Se verifica si el campo Municípios tiene algun valor
        If (Me.ddlCities.SelectedValue.Length > 0) Then
            filterList.Add("ddlCities;" & Me.ddlCities.SelectedValue)
        End If

        'Se verifica si el campo Costo Inicial tiene algun valor
        If (Me.txtStartCost.Text.Length > 0) Then
            filterList.Add("txtStartCost;" & Me.txtStartCost.Text)
        End If

        'Se verifica si el campo Costo Final tiene algun valor
        If (Me.txtEndCost.Text.Length > 0) Then
            filterList.Add("txtEndCost;" & Me.txtEndCost.Text)
        End If

        'Se verifica si el campo Estado tiene algun valor
        If (Me.ddlState.SelectedValue.Length > 0) Then
            filterList.Add("ddlState;" & Me.ddlState.SelectedValue)
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

                    Case "txtDateStartReg"
                        Me.txtDateStartReg.Text = vector(1)
                    Case "txtDateEndReg"
                        Me.txtDateEndReg.Text = vector(1)
                    Case "txtIdeaName"
                        Me.txtIdeaName.Text = vector(1)
                    Case "ddlStrategicLines"
                        Me.ddlStrategicLines.SelectedValue = vector(1)
                    Case "ddlSource"
                        Me.ddlSource.SelectedValue = vector(1)
                    Case "ddlDepto"
                        Me.ddlDepto.SelectedValue = vector(1)
                    Case "ddlCities"
                        Me.ddlCities.SelectedValue = vector(1)
                    Case "txtStartCost"
                        Me.txtStartCost.Text = vector(1)
                    Case "txtEndCost"
                        Me.txtEndCost.Text = vector(1)
                    Case "ddlState"
                        Me.ddlState.SelectedValue = vector(1)

                End Select

            Next

        End If

    End Sub

    ''' <summary>
    ''' Permite poblar la grilla de Ideas con la información requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadGridView()

        'Declaracion de objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dt As New DataTable()
        Dim dtAux As New DataTable()
        Dim idsCities As String = ""
        Dim idsActivities As String = ""
        Dim startCost As String = ""
        Dim endCost As String = ""

        Try
            'Se recupera el valor de los cosots
            If (Me.txtStartCost.Text.Length > 0) Then startCost = PublicFunction.ConvertStringToDouble(Me.txtStartCost.Text).ToString()
            If (Me.txtEndCost.Text.Length > 0) Then endCost = PublicFunction.ConvertStringToDouble(Me.txtEndCost.Text).ToString()

            'Se recupera el resultado de la consulta
            dt = facade.loadReportIdeaInventory(applicationCredentials, "", Me.txtDateStartReg.Text, Me.txtDateEndReg.Text, Me.txtIdeaName.Text, _
             Me.ddlStrategicLines.SelectedValue, Me.ddlSource.SelectedValue, ddlDepto.SelectedValue, Me.ddlCities.SelectedValue, _
              startCost, endCost, Me.ddlState.SelectedValue)

            'Se pobla la tabla que alamacena la información de ubicaciones por idea
            dtAux = facade.loadReportLocationsByIdea(applicationCredentials, ddlDepto.SelectedValue, Me.ddlCities.SelectedValue)

            'Se pobla la grilla
            Me.gvData.DataSource = dt
            Me.gvData.DataBind()

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            dt = Nothing
            facade = Nothing
        End Try


    End Sub

    ''' <summary>
    ''' Permite poblar las diferentes listas del formulario actual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadCombos()

        ' definir los objetos
        Dim objFacade As New Facade()
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)

        Try

            ' cargar los usuarios registrados
            Me.ddlDepto.DataSource = objFacade.getDeptoList(applicationCredentials, enabled:="T", order:="Depto.Code")
            Me.ddlDepto.DataValueField = "Id"
            Me.ddlDepto.DataTextField = "Code"
            Me.ddlDepto.DataBind()

            ' agregar la opcion de todos
            Me.ddlDepto.Items.Add(New ListItem("Todos", ""))
            Me.ddlDepto.SelectedValue = ""

            'Se llama al metodo que pobla la lista de ciudades
            Me.LoadDropDownCities()

            'Se pobla el combo de Linea Estrategica
            Me.ddlStrategicLines.DataSource = objFacade.getStrategicLineList(applicationCredentials, enabled:="1", order:="Code")
            Me.ddlStrategicLines.DataValueField = "Id"
            Me.ddlStrategicLines.DataTextField = "Code"
            Me.ddlStrategicLines.DataBind()

            ' agregar la opcion de todos
            Me.ddlStrategicLines.Items.Add(New ListItem("Todos", ""))

            ' seleccionar
            Me.ddlStrategicLines.SelectedValue = ""

            'Se llama al metodo que pobla la lista de Componentes del Programa
            Me.LoadListProgramComponents()

            'Se agrega la opcion de todos para el combo de fuente
            Me.ddlSource.Items.Add(New ListItem("Todos", ""))
            Me.ddlSource.SelectedValue = ""

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            objFacade = Nothing

        End Try
    End Sub

    ''' <summary>
    ''' Permite cargar la lista de municipios segun un depto seleccionado.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDropDownCities()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)

        Try

            'Se carga la lista de los municipos
            If (Me.ddlDepto.SelectedValue.Length > 0) Then

                Me.ddlCities.DataSource = facade.getCityList(applicationCredentials, iddepto:=Me.ddlDepto.SelectedValue, enabled:="T", order:="City.Code")

            Else

                Me.ddlCities.DataSource = facade.getCityList(applicationCredentials, enabled:="T", order:="City.Code")

            End If

            Me.ddlCities.DataValueField = "Id"
            Me.ddlCities.DataTextField = "Code"
            Me.ddlCities.DataBind()

            ' agregar la opcion de todos
            Me.ddlCities.Items.Add(New ListItem("Todos", ""))
            'Seleccionar
            Me.ddlCities.SelectedValue = ""

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
    ''' Metodo que permite cargar la lista de Componentes del Programa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListProgramComponents()


    End Sub

    ''' <summary>
    ''' Permite cargar el reporte de inventarios por idea
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadReport()

        'Declaracion de objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dt As New DataTable()
        Dim dtLocations As New DataTable()
        Dim dtAttachments As New DataTable()
        Dim dtThirds As New DataTable()
        Dim idsCities As String = ""
        Dim idsActivities As String = ""
        Dim startCost As String = ""
        Dim endCost As String = ""

        Try
            'Se recupera el resultado de la consulta
            dt = facade.loadReportIdeaInventory(applicationCredentials, idIdea:=Request.QueryString("id"))

            'Se pobla la tabla que carga la lista de archivos adjuntos por idea
            dtAttachments = facade.loadReportAttachmentsByIdea(applicationCredentials, idEntity:=Request.QueryString("id"), entityName:=GetType(IdeaEntity).ToString())

            'Se pobla la tabla que alamacena la información de ubicaciones por idea
            dtLocations = facade.loadReportLocationsByIdea(applicationCredentials, idIdea:=Request.QueryString("id"))

            'Se pobla la tabla que carga la lista de terceros por idea
            dtThirds = facade.loadReportThirdsByIdea(applicationCredentials, idIdea:=Request.QueryString("id"))

            'Se configuran los objetos
            Dim rd As New ReportDocument
            Dim ds As New DataSet()
            ds.DataSetName = "dsRptIdeaInventory.xsd"

            'Se Adjunta las tablas requeridas
            ds.Tables.Add(dt.Copy())
            ds.Tables(0).TableName = "vReportIdeaInventory"
            ds.Tables.Add(dtAttachments.Copy())
            ds.Tables(1).TableName = "vReportAttachmentsByIdea"
            ds.Tables.Add(dtLocations.Copy())
            ds.Tables(2).TableName = "vReportLocationsByIdea"
            ds.Tables.Add(dtThirds.Copy())
            ds.Tables(3).TableName = "vReportThirdByIdea"

            'Se carga el reporte
            rd.Load(Server.MapPath("IdeaInventory.rpt"))
            rd.SetDataSource(ds)
            Me.crvReport.ReportSource = rd

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            dt = Nothing
            facade = Nothing
        End Try

    End Sub

#End Region

End Class
