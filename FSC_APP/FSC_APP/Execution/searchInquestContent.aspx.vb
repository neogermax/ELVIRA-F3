Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchInquestContent
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
            Session("lblTitle") = "BUSCAR CONTENIDO DE ENCUESTA."

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""

            'Se selecciona el primer filtro por defecto
            Me.rblSearch.SelectedIndex = 0

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' datos de la busqueda
        ViewState("field") = Me.rblSearch.SelectedValue
        ViewState("value") = Me.txtSearch.Text

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = 0

        ' definir los objetos
        Dim list As List(Of InquestContentEntity)

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

    Public Function search() As List(Of InquestContentEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idinquest As String = ""
        Dim code As String = ""
        Dim enabled As String = ""
        Dim enabledText As String = ""
        Dim iduser As String = ""
        Dim userName As String = ""
        Dim createdate As String = ""
        Dim questionText As String = ""
        Dim questionType As String = ""
        Dim answer As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : id = CStr(Me.txtSearch.Text)
            Case "idlike" : idlike = CStr(Me.txtSearch.Text)
            Case "idinquest" : idinquest = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "enabled" : enabled = CStr(Me.txtSearch.Text)
            Case "enabledtext" : enabledText = CStr(Me.txtSearch.Text)
            Case "username" : userName = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "questionText" : questionText = CStr(Me.txtSearch.Text)
            Case "questionType" : questionType = CStr(Me.txtSearch.Text)
            Case "answer" : answer = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getInquestContentList(applicationCredentials, _
             id, _
             idlike, _
             idinquest, _
             code, _
             enabled, _
             enabledText, _
             iduser, _
             userName, _
             createdate, _
             questionText, _
             questionType, _
             answer, _
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
			idinquest = Nothing
			code = Nothing
            enabled = Nothing
            enabledText = Nothing
            iduser = Nothing
            userName = Nothing
            createdate = Nothing
            questionText = Nothing
            questionType = Nothing
            answer = Nothing

        End Try

    End Function

#End Region

End Class
