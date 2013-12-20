Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchProjectApprovalRecord
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
            Session("lblTitle") = "BUSCAR REGISTRO DE APROBACIÓN DE IDEA."

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
        Dim list As List(Of ProjectApprovalRecordEntity)

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


        Dim defaultDate As New DateTime
        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(10).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(10).Text = "Habilitado"

            Else

                e.Row.Cells(10).Text = "Deshabilitado"

            End If

            'If DateTime.Parse(e.Row.Cells(6).Text) = defaultDate Then
            '    e.Row.Cells(6).Text = ""
            'End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ProjectApprovalRecordEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim code As String = ""
        Dim comments As String = ""
        Dim attachment As String = ""
        Dim approvaldate As String = ""
        Dim actnumber As String = ""
        Dim approvedvalue As String = ""
        Dim approved As String = ""
        Dim approvedtext As String = ""
        Dim codeapprovedidea As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "comments" : comments = CStr(Me.txtSearch.Text)
            Case "attachment" : attachment = CStr(Me.txtSearch.Text)
            Case "approvaldate" : approvaldate = CStr(Me.txtSearch.Text)
            Case "actnumber" : actnumber = CStr(Me.txtSearch.Text)
            Case "approvedvalue" : approvedvalue = CStr(Me.txtSearch.Text)
            Case "approvedtext" : approvedtext = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getProjectApprovalRecordList(applicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             code, _
             comments, _
             attachment, _
             approvaldate, _
             actnumber, _
             approvedvalue, _
             approved, _
             approvedtext, _
             codeapprovedidea, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate)

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
            idproject = Nothing
            projectname = Nothing
            code = Nothing
            comments = Nothing
            attachment = Nothing
            approvaldate = Nothing
            actnumber = Nothing
            approvedvalue = Nothing
            approved = Nothing
            approvedtext = Nothing
            enabled = Nothing
            enabledtext = Nothing
            iduser = Nothing
            username = Nothing
            createdate = Nothing

        End Try

    End Function

#End Region

End Class
