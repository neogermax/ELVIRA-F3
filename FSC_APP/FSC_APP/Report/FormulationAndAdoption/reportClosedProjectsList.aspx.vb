Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data

Partial Class Report_FormulationAndAdoption_reportClosedProjectsList
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

            ' cargar el titulo
            Session("lblTitle") = "Listado proyectos cerrados."

            'Se llama al metodo que pobla los combos
            loadCombos()

        End If

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargar los datos
        Me.LoadGridView()

    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound
        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(5).Text.ToUpper().Equals("TRUE") Then
                e.Row.Cells(5).Text = "SI"
            Else
                e.Row.Cells(5).Text = "NO"
            End If

        End If
    End Sub

    Protected Sub ddlStrategicLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrategicLine.SelectedIndexChanged

        'Se llama al metodo que permite poblar el combo de proyectos
        Me.LoadDropDownListProjectByStrategicLine()

    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click

        'Se llama al metodo que pobla la grila de contratos
        Me.LoadGridView()

        ' actualizar los datos
        Me.upData.Update()

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo encargado de cargar la consulta que permite poblar la grilla 
    ''' con el listado de proyectos cerrados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadGridView()

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim myStrategicLine As String = ""
        Dim myProject As String = ""

        Try
            'Se recuperan los valores de los filtros
            myStrategicLine = Me.ddlStrategicLine.SelectedValue
            myProject = Me.ddlProject.SelectedValue

            'Se llama al metodo que consulta la información requerida
            Me.gvData.DataSource = facade.loadReportCLosedProjectList(applicationCredentials, myStrategicLine, myProject)
            Me.gvData.DataBind()

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
    ''' Permite poblar los combos de lformulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadCombos()
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se llama al metodo que permite poblar el combo de Lineas Estrategicas
            Me.LoadDropDownListStrategicLine()

            'Se llama al metodo que permite poblar el combo de proyectos
            Me.LoadDropDownListProjectByStrategicLine()

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
            Me.ddlStrategicLine.DataSource = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="Code")
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
