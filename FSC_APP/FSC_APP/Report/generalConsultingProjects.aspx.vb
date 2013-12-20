Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data

Partial Class Report_generalConsultingProjects
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

            ' cargar los combos
            Me.loadCombos()

        End If

        ' poner el titulo
        Session("lblTitle") = "Consulta general proyectos"

    End Sub

    Protected Sub ddlDepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepto.SelectedIndexChanged

        'Se llama al metodo que permite crear cargar el combo de ciudades
        Me.LoadDropDownCities()

    End Sub

    Protected Sub bt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt.Click

        'Se pobla la grilla
        Me.LoadGridView()

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargar los datos
        Me.LoadGridView()

    End Sub

    Protected Sub lnkBtnView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Se almacena el indice de la fila seleccionada
        Dim lnkButton As LinkButton = DirectCast(sender, LinkButton)
        Dim fieldCell As DataControlFieldCell = lnkButton.Parent
        Dim gridViewRow As GridViewRow = fieldCell.Parent
        Dim ddlReport As DropDownList
        Dim myIdProject As String
        Dim myIndex As Integer

        myIndex = gridViewRow.RowIndex
        myIdProject = Me.gvData.Rows(myIndex).Cells(0).Text

        'Se captura el combo de la fila seleccionada
        ddlReport = DirectCast(gridViewRow.Cells(9).FindControl("ddlReport"), DropDownList)

        'Se verifica que se haya seleccionado un valor
        If (ddlReport.SelectedValue.Length > 0) Then
            'Se redirecciona al usuario a la pagina del resporte seleccionado
            'Response.Redirect(ddlReport.SelectedValue & "?idProject=" & myIdProject)
            Me.AbreVentana(ddlReport.SelectedValue & "?idProject=" & myIdProject)
        End If

    End Sub

    ''' <summary>
    ''' Permite abrir una nueva ventana del navegador con una url determinada
    ''' </summary>
    ''' <param name="ventana"></param>
    ''' <remarks></remarks>
    Private Sub AbreVentana(ByVal ventana As String)

        Dim Clientscript As String = "<script type='text/javascript'>window.open('" & ventana & "');</script>"

        If Not (Page.ClientScript.IsStartupScriptRegistered("WOpen")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "WOpen", Clientscript)
        End If

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite poblar los combos del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ano As Integer

        Try

            'Cargar los años para vigencia presupuestal
            For ano = 2000 To 2030
                ddleffectivebudget.Items.Add(ano.ToString)
            Next

            'Agregar la opcion de todos
            Me.ddleffectivebudget.Items.Add(New ListItem("Todas", ""))
            Me.ddleffectivebudget.SelectedValue = ""

            ' cargar el combo de Linea Estrategica
            Me.ddlStrategicLines.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="code")
            Me.ddlStrategicLines.DataValueField = "Id"
            Me.ddlStrategicLines.DataTextField = "Code"
            Me.ddlStrategicLines.DataBind()

            ' agregar la opcion de todos
            Me.ddlStrategicLines.Items.Add(New ListItem("Todos", ""))
            Me.ddlStrategicLines.SelectedValue = ""

            ' cargar los usuarios registrados
            Me.ddlDepto.DataSource = facade.getDeptoList(applicationCredentials, enabled:="T", order:="Depto.Code")
            Me.ddlDepto.DataValueField = "Id"
            Me.ddlDepto.DataTextField = "Code"
            Me.ddlDepto.DataBind()

            ' agregar la opcion de todos
            Me.ddlDepto.Items.Add(New ListItem("Todos", ""))
            Me.ddlDepto.SelectedValue = ""

            'Se llama al metodo que pobla la lista de ciudades
            Me.LoadDropDownCities()

            'Cargar el combo de Operadores
            Me.ddlOperator.DataSource = facade.getThirdList(applicationCredentials, enabled:="1", order:="code")
            Me.ddlOperator.DataValueField = "Id"
            Me.ddlOperator.DataTextField = "Code"
            Me.ddlOperator.DataBind()

            ' agregar la opcion de todos
            Me.ddlOperator.Items.Add(New ListItem("Todos", ""))
            Me.ddlOperator.SelectedValue = ""

            'Agregar la opcion de todos para el combo de poblacion objetivo
            Me.ddlTargetPopulation.Items.Add(New ListItem("Todos", ""))
            Me.ddlTargetPopulation.SelectedValue = ""

            'Agregar la opcion de todos para el combo de estado
            Me.ddlEstate.Items.Add(New ListItem("Todos", ""))
            Me.ddlEstate.SelectedValue = ""

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
    ''' Permite poblar la grilla de projectos con la información requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadGridView()

        'Declaracion de objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), Gattaca.Application.Credentials.ApplicationCredentials)
        Dim dt As New DataTable()
        Dim idsCities As String = ""
        Dim idsActivities As String = ""
        Dim startTotalValue As String = ""
        Dim endTotalValue As String = ""
        Dim startContributionValue As String = ""
        Dim endContributionValue As String = ""

        Try
            'Se recupera el valor de los costos
            startContributionValue = PublicFunction.ConvertStringToDouble(Me.txtStartTotalValue.Text).ToString()
            endTotalValue = PublicFunction.ConvertStringToDouble(Me.txtEndTotalValue.Text).ToString()
            startContributionValue = PublicFunction.ConvertStringToDouble(Me.txtStartContributionValue.Text).ToString()
            endContributionValue = PublicFunction.ConvertStringToDouble(Me.txtEndContributionValue.Text).ToString()

            'Se recupera el resultado de la consulta
            dt = facade.loadConsultGeneralProjects(applicationCredentials, Me.ddlStrategicLines.SelectedValue, Me.txtProjectName.Text, _
                 Me.ddlDepto.SelectedValue, Me.ddlCities.SelectedValue, Me.ddlTargetPopulation.SelectedValue, Me.ddlOperator.SelectedValue, _
                 Me.txtStartTotalValue.Text, Me.txtEndTotalValue.Text, Me.txtStartContributionValue.Text, Me.txtEndContributionValue.Text, _
                 Me.ddleffectivebudget.SelectedValue, Me.ddlEstate.SelectedValue, Me.txtStartClosingDate.Text, Me.txtEndClosingDate.Text)

            'Se pobla la grilla
            Me.gvData.DataSource = dt
            Me.gvData.DataBind()

            'Actualizar los datos
            'Me.upData.Update()

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
