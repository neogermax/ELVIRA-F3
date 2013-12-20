Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchCloseRegistry
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
            Session("lblTitle") = "BUSCAR REGISTRO DE CIERRES."

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' datos de la busqueda
        ViewState("field") = Me.rblSearch.SelectedValue
        ViewState("value") = Me.txtSearch.Text

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = 0

        ' definir los objetos
        Dim list As List(Of CloseRegistryEntity)

        ' cargar la busqueda
        list = search()

        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()

        If list.Count > 0 Then

            ' mostrar la busqueda
            Me.lblSubTitle.Text = "Resultados de la B�squeda."

        Else
            ' mostrar la busqueda
            Me.lblSubTitle.Text = "La b�squeda no produjo resultados."

        End If

        ' actualizar los datos
        Me.upData.Update()
        Me.upSubTitle.Update()

    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = e.NewPageIndex

        ' cargar los datos
        Me.gvData.DataSource = search()
        Me.gvData.DataBind()

        ' actualizar los datos
        Me.upData.Update()

    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound

        Dim defaultDate As New DateTime

        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(3).Text.ToString.Equals(defaultDate.ToString) Then

                e.Row.Cells(3).Text = ""

            End If

            If e.Row.Cells(5).Text.ToString.Equals(defaultDate.ToString) Then

                e.Row.Cells(5).Text = ""

            End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of CloseRegistryEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim closingdate As String = ""
        Dim weakness As String = ""
        Dim opportunity As String = ""
        Dim strengths As String = ""
        Dim learningfornewprojects As String = ""
        Dim goodpractice As String = ""
        Dim goodpracticetext As String = ""
        Dim registrationdate As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "closingdate" : closingdate = CStr(Me.txtSearch.Text)
            Case "goodpractice" : goodpracticetext = CStr(Me.txtSearch.Text)
            Case "registrationdate" : registrationdate = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getCloseRegistryList(applicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             closingdate, _
             weakness, _
             opportunity, _
             strengths, _
             learningfornewprojects, _
             goodpractice, _
             goodpracticetext, _
             registrationdate, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
            Me.ddlSort.SelectedValue)

        Catch ex As Exception
            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar los objetos
            facade = Nothing
			id = Nothing
			idproject = Nothing
			closingdate = Nothing
			weakness = Nothing
			opportunity = Nothing
			strengths = Nothing
			learningfornewprojects = Nothing
			goodpractice = Nothing
			registrationdate = Nothing
			enabled = Nothing
			iduser = Nothing

        End Try

    End Function

#End Region

End Class
