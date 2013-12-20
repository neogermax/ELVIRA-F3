Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchStrategicActivity
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
            Session("lblTitle") = "BUSCAR ACTIVIDAD DE LA ESTRATÉGIA."

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
        Dim list As List(Of StrategicActivityEntity)

        ' cargar la busqueda
        list = search()

        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()

        If list.Count > 0 Then

            ' mostrar la busqueda
            Me.lblSubTitle.Text = "Resultados de la Búsqueda."

        Else
            ' mostrar la busqueda
            Me.lblSubTitle.Text = "La búsqueda no produjo resultados."

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

        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(8).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(8).Text = "Habilitado"

            Else

                e.Row.Cells(8).Text = "Deshabilitado"

            End If


        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of StrategicActivityEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim code As String = ""
        Dim name As String = ""
        Dim description As String = ""
        Dim strategy As String = ""
        Dim strategyname As String = ""
        Dim begindate As String = ""
        Dim enddate As String = ""
        Dim estimatedvalue As String = ""
        Dim idresponsible As String = ""
        Dim responsiblename As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "name" : name = CStr(Me.txtSearch.Text)
            Case "description" : description = CStr(Me.txtSearch.Text)
            Case "strategy" : strategy = CStr(Me.txtSearch.Text)
            Case "strategyname" : strategyname = CStr(Me.txtSearch.Text)
            Case "begindate" : begindate = CStr(Me.txtSearch.Text)
            Case "enddate" : enddate = CStr(Me.txtSearch.Text)
            Case "estimatedvalue" : estimatedvalue = CStr(Me.txtSearch.Text)
            Case "idresponsible" : idresponsible = CStr(Me.txtSearch.Text)
            Case "responsiblename" : responsiblename = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getStrategicActivityList(applicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             strategy, _
             strategyname, _
             begindate, _
             enddate, _
             estimatedvalue, _
             idresponsible, _
             responsiblename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
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
            idlike = Nothing
			code = Nothing
			name = Nothing
			description = Nothing
			strategy = Nothing
			begindate = Nothing
			enddate = Nothing
			estimatedvalue = Nothing
			idresponsible = Nothing
			enabled = Nothing
			iduser = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

End Class
