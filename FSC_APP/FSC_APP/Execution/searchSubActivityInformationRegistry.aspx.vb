Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchSubActivityInformationRegistry
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
            Session("lblTitle") = "BUSCAR Registro de Información de actividad."

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
        Dim list As List(Of SubActivityInformationRegistryEntity)

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

        Dim defaulDate As New DateTime

        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(4).Text.Equals(defaulDate.ToString) Then
                e.Row.Cells(4).Text = ""
            Else
                e.Row.Cells(4).Text = e.Row.Cells(4).Text
            End If

            If e.Row.Cells(5).Text.Equals(defaulDate.ToString) Then
                e.Row.Cells(5).Text = ""
            Else
                e.Row.Cells(5).Text = e.Row.Cells(5).Text
            End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of SubActivityInformationRegistryEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idsubactivity As String = ""
        Dim subactivityname As String = ""
        Dim description As String = ""
        Dim begindate As String = ""
        Dim enddate As String = ""
        Dim comments As String = ""
        Dim attachment As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        search = New List(Of SubActivityInformationRegistryEntity)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "subactivityname" : subactivityname = CStr(Me.txtSearch.Text)
            Case "description" : description = CStr(Me.txtSearch.Text)
            Case "begindate" : begindate = CStr(Me.txtSearch.Text)
            Case "enddate" : enddate = CStr(Me.txtSearch.Text)
            Case "comments" : comments = CStr(Me.txtSearch.Text)
            Case "attachment" : attachment = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getSubActivityInformationRegistryList(applicationCredentials, _
             id, _
             idlike, _
             idsubactivity, _
             subactivityname, _
             description, _
             begindate, _
             enddate, _
             comments, _
             attachment, _
             iduser, _
             username, _
             createdate, _
             enabled, _
             enabledtext, _
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
            idsubactivity = Nothing
            description = Nothing
            begindate = Nothing
            enddate = Nothing
            comments = Nothing
            attachment = Nothing
            iduser = Nothing
            createdate = Nothing
            enabled = Nothing

        End Try

    End Function

#End Region

End Class
