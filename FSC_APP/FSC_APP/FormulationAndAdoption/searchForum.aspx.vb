Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchForum
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

        Dim projectName As String = ""

        If Not Page.IsPostBack Then
            
            ' cargar el titulo
            Session("lblTitle") = "FORO DEL PROYECTO."

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""

            projectName = Request.QueryString("prjn")
            If Not projectName Is Nothing Then
                Me.txtSearch.Text = projectName
                btnSearch_Click(sender, e)
            End If

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' datos de la busqueda
        ViewState("field") = Me.rblSearch.SelectedValue
        ViewState("value") = Me.txtSearch.Text

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = 0

        ' definir los objetos
        Dim list As List(Of ForumEntity)

        ' cargar la busqueda
        list = search()

        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()

        If list.Count > 0 Then

            ' mostrar la busqueda
            Me.lblSubTitle.Text = "Resultados de la búsqueda."

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
        Dim Fecha As New DateTime
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(8).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(8).Text = "Habilitado"

            Else

                e.Row.Cells(8).Text = "Deshabilitado"

            End If

            If e.Row.Cells(6).Text = Fecha.ToString() Then
                e.Row.Cells(6).Text = ""
            End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ForumEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim subject As String = ""
        Dim message As String = ""
        Dim attachment As String = ""
        Dim updateddate As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim replycount As String = ""
        Dim lastreplyusername As String = ""
        Dim lastreplycreatedate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "subject" : subject = CStr(Me.txtSearch.Text)
            Case "message" : message = CStr(Me.txtSearch.Text)
            Case "attachment" : attachment = CStr(Me.txtSearch.Text)
            Case "updateddate" : updateddate = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "replycount" : replycount = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "lastreplycreatedate" : lastreplycreatedate = CStr(Me.txtSearch.Text)
            Case "lastreplyusername" : lastreplyusername = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getForumList(applicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             subject, _
             message, _
             attachment, _
             updateddate, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             replycount, _
             lastreplyusername, _
             lastreplycreatedate, _
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
			subject = Nothing
			message = Nothing
			attachment = Nothing
			updateddate = Nothing
			enabled = Nothing
			iduser = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

End Class
