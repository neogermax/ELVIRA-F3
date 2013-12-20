Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchExecution
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
            Session("lblTitle") = "BUSCAR REGISTRO DE EJECUCIÓN."

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
        Dim list As List(Of ExecutionEntity)

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

            If e.Row.Cells(7).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(7).Text = "Habilitado"

            Else

                e.Row.Cells(7).Text = "Deshabilitado"

            End If



        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ExecutionEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim qualitativeindicators As String = ""
        Dim learning As String = ""
        Dim adjust As String = ""
        Dim achievements As String = ""
        Dim enable As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim testimonyname As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)

            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "qualitativeindicators" : qualitativeindicators = CStr(Me.txtSearch.Text)
            Case "learning" : learning = CStr(Me.txtSearch.Text)
            Case "testimonyname" : testimonyname = CStr(Me.txtSearch.Text)
            Case "enable" : enabledtext = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getExecutionList(applicationCredentials, _
             id, _
             idlike, _
             idproject, _
             projectname, _
             qualitativeindicators, _
             learning, _
             adjust, _
             achievements, _
             testimonyname, _
             enable, _
             enabledtext, _
             iduser, _
             createdate, _
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
			qualitativeindicators = Nothing
			learning = Nothing
			adjust = Nothing
			achievements = Nothing
			enable = Nothing
			iduser = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

  

End Class
