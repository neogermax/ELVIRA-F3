Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchActivity
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
            Session("lblTitle") = "BUSCAR ACTIVIDAD."

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
        Dim list As List(Of ActivityEntity)

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

            If e.Row.Cells(7).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(7).Text = "SI"

            Else

                e.Row.Cells(7).Text = "NO"

            End If


        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ActivityEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim number As String = ""
        Dim title As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim idobjective As String = ""
        Dim objectivename As String = ""
        Dim idcomponent As String = ""
        Dim componentname As String = ""
        Dim description As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim criticalpathtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "title" : title = CStr(Me.txtSearch.Text)
            Case "number" : number = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "objectivename" : objectivename = CStr(Me.txtSearch.Text)
            Case "componentname" : componentname = CStr(Me.txtSearch.Text)
            Case "criticalpath" : criticalpathtext = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getActivityList(applicationCredentials, _
             id, _
             idlike, _
             number, _
             title, _
             idcomponent, _
             componentname, _
              idproject, _
             projectname, _
             idobjective, _
             objectivename, _
             description, _
             enabled, _
              enabledtext, _
              criticalpathtext, _
             iduser, _
             username, _
             createdate, _
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
            idlike = Nothing
			number = Nothing
			title = Nothing
            idcomponent = Nothing
            componentname = Nothing
            idproject = Nothing
            projectname = Nothing
            idobjective = Nothing
            objectivename = Nothing
			description = Nothing
            enabled = Nothing
            enabledtext = Nothing
            criticalpathtext = Nothing
            iduser = Nothing
            username = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

End Class
