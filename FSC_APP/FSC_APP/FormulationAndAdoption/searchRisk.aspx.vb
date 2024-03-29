Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchRisk
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
        Dim idproject As String = ""
        If Not Page.IsPostBack Then
            
            ' cargar el titulo
            Session("lblTitle") = "BUSCAR RIESGO."

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""
            idproject = Request.QueryString("idproject")
            If Not idproject Is Nothing Or rblSearch.SelectedValue = "" Then
                ViewState("field") = "idproject"
                ViewState("value") = idproject
                rblSearch.ClearSelection()
                btnSearch_Click(sender, e)
            End If

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' datos de la busqueda
        If rblSearch.SelectedValue <> "" Then
            ViewState("field") = Me.rblSearch.SelectedValue
            ViewState("value") = Me.txtSearch.Text
        End If
        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = 0

        ' definir los objetos
        Dim list As List(Of RiskEntity)

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

        ' verificar si es de tipo row
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(7).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(7).Text = "Habilitado"

            Else

                e.Row.Cells(7).Text = "Deshabilitado"

            End If

            Select Case (Integer.Parse(e.Row.Cells(6).Text))
                Case 1 : e.Row.Cells(6).Text = "Alto"
                Case 2 : e.Row.Cells(6).Text = "Medio"
                Case 3 : e.Row.Cells(6).Text = "Bajo"
            End Select

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of RiskEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim code As String = ""
        Dim name As String = ""
        Dim description As String = ""
        Dim whatcanhappen As String = ""
        Dim riskimpact As String = ""
        Dim ocurrenceprobability As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim idcomponent As String = ""
        Dim componentname As String = ""
        Dim idproject As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "name" : name = CStr(Me.txtSearch.Text)
            Case "description" : description = CStr(Me.txtSearch.Text)
            Case "riskimpact" : riskimpact = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "componentname" : componentname = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "idproject" : idproject = CStr(ViewState("value"))
        End Select

        Try
            ' buscar
            search = facade.getRiskList(applicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             description, _
             whatcanhappen, _
             riskimpact, _
             ocurrenceprobability, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             idcomponent, _
             componentname, _
             idproject, _
            Me.ddlSort.SelectedValue, _
            isLastVersion:=1)

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
			code = Nothing
			name = Nothing
			whatcanhappen = Nothing
			riskimpact = Nothing
			ocurrenceprobability = Nothing
			enabled = Nothing
			iduser = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

End Class
