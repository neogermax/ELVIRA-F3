Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchIndicator
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
            Session("lblTitle") = "BUSCAR INDICADOR."

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
        Dim list As List(Of IndicatorEntity)

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

            If e.Row.Cells(9).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(9).Text = "Habilitado"

            Else

                e.Row.Cells(9).Text = "Deshabilitado"

            End If

            If e.Row.Cells(5).Text.ToUpper.Trim.Equals("1") Then

                e.Row.Cells(5).Text = "Beneficiarios"

            ElseIf e.Row.Cells(5).Text.ToUpper.Trim.Equals("2") Then

                e.Row.Cells(5).Text = "Capacidad instalada"

            ElseIf e.Row.Cells(5).Text.ToUpper.Trim.Equals("3") Then

                e.Row.Cells(5).Text = "Gestión del conocimiento"

            End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of IndicatorEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim code As String = ""
        Dim description As String = ""
        Dim type As String = ""
        Dim goal As String = ""
        Dim greenvalue As String = ""
        Dim yellowvalue As String = ""
        Dim redvalue As String = ""
        Dim assumptions As String = ""
        Dim sourceverification As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim entityname As String = ""
        Dim levelname As String = ""
        Dim levelindicator As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "description" : description = CStr(Me.txtSearch.Text)
            Case "type" : type = CStr(Me.txtSearch.Text)
            Case "goal" : goal = CStr(Me.txtSearch.Text)
            Case "greenvalue" : greenvalue = CStr(Me.txtSearch.Text)
            Case "yellowvalue" : yellowvalue = CStr(Me.txtSearch.Text)
            Case "redvalue" : redvalue = CStr(Me.txtSearch.Text)
            Case "assumptions" : assumptions = CStr(Me.txtSearch.Text)
            Case "sourceverification" : sourceverification = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "entityname" : entityname = CStr(Me.txtSearch.Text)
            Case "levelname" : levelname = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getIndicatorList(applicationCredentials, _
             id, _
             idlike, _
             code, _
             description, _
             type, _
             goal, _
             greenvalue, _
             yellowvalue, _
             redvalue, _
             assumptions, _
             sourceverification, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             entityname, _
            levelname, _
            levelindicator)

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
            description = Nothing
            type = Nothing
            goal = Nothing
            greenvalue = Nothing
            yellowvalue = Nothing
            redvalue = Nothing
            assumptions = Nothing
            sourceverification = Nothing
            enabled = Nothing
            enabledtext = Nothing
            iduser = Nothing
            username = Nothing
            createdate = Nothing
            entityname = Nothing
            levelname = Nothing
            levelindicator = Nothing

        End Try

    End Function

#End Region

End Class
