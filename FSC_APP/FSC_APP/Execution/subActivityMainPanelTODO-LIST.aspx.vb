Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class Execution_subActivityMainPanelTODO_LIST
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
            Session("lblTitle") = "LISTADO DE SUBACTIVIDADES PENDIENTES"

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
        Dim list As List(Of SubActivityMainPanelEntity)

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
        Dim defaultDate As New DateTime

        If e.Row.RowType = DataControlRowType.DataRow Then

            'Si es de tipo 2 o es indicador, mostrar las fechas de medición
            If e.Row.Cells(15).Text.ToUpper.Equals("2") Then

                e.Row.Cells(11).Text = e.Row.Cells(16).Text
                e.Row.Cells(12).Text = ""
                e.Row.Cells(14).Text = ""
                e.Row.Cells(6).Text = ""

            End If

            If e.Row.Cells(12).ToString.Equals(defaultDate.ToString) Then
                e.Row.Cells(12).Text = ""
            End If


            Dim objHyperlink As HyperLink
            Dim miEntidad As SubActivityMainPanelEntity = e.Row.DataItem
            objHyperlink = e.Row.Cells(8).Controls(0)
            If (miEntidad.attachment.Length > 0) Then
                objHyperlink.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "/" & miEntidad.attachment
                objHyperlink.Target = "_blank"
            End If

        End If

    End Sub

    Protected Sub gvData_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvData.DataBinding
        gvData.Columns(15).Visible = True
        gvData.Columns(16).Visible = True
    End Sub

    Protected Sub gvData_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvData.DataBound
        gvData.Columns(15).Visible = False
        gvData.Columns(16).Visible = False
    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of SubActivityMainPanelEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim idstrategicobjective As String = ""
        Dim strategicobjectivename As String = ""
        Dim idStrategicLine As String = ""
        Dim StrategicLinename As String = ""
        Dim idstrategy As String = ""
        Dim strategyname As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim projectPhase As String = ""
        Dim projectPhaseText As String = ""
        Dim idcomponent As String = ""
        Dim componentname As String = ""
        Dim name As String = ""
        Dim type As String = ""
        Dim typetext As String = ""
        Dim state As String = ""
        Dim statetext As String = ""
        Dim begindate As String = ""
        Dim enddate As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim approval As String = ""
        Dim approvalText As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : id = CStr(Me.txtSearch.Text)
            Case "strategicobjective" : strategicobjectivename = CStr(Me.txtSearch.Text)
            Case "StrategicLine" : StrategicLinename = CStr(Me.txtSearch.Text)
            Case "strategy" : strategyname = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "projectphase" : projectPhaseText = CStr(Me.txtSearch.Text)
            Case "componentname" : componentname = CStr(Me.txtSearch.Text)
            Case "name" : name = CStr(Me.txtSearch.Text)
            Case "type" : typetext = CStr(Me.txtSearch.Text)
            Case "state" : statetext = CStr(Me.txtSearch.Text)
            Case "begindate" : begindate = CStr(Me.txtSearch.Text)
            Case "enddate" : enddate = CStr(Me.txtSearch.Text)
            Case "approval" : approvalText = CStr(Me.txtSearch.Text)

        End Select

        'iduser = applicationCredentials.UserID

        Try
            ' buscar
            search = facade.getSubActivityMainPanelList(applicationCredentials, _
            id, _
            idlike, _
            idstrategicobjective, _
            strategicobjectivename, _
            idStrategicLine, _
            StrategicLinename, _
            idstrategy, _
            strategyname, _
            idproject, _
            projectname, _
            projectPhase, _
            projectPhaseText, _
            idcomponent, _
            componentname, _
            name, _
            type, _
            typetext, _
            state, _
            statetext, _
            begindate, _
            enddate, _
            iduser, _
            username, _
            approval, _
            approvalText, _
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
            idstrategicobjective = Nothing
            strategicobjectivename = Nothing
            idStrategicLine = Nothing
            StrategicLinename = Nothing
            idstrategy = Nothing
            strategyname = Nothing
            idproject = Nothing
            projectname = Nothing
            projectPhase = Nothing
            projectPhaseText = Nothing
            idcomponent = Nothing
            componentname = Nothing
            name = Nothing
            type = Nothing
            typetext = Nothing
            state = Nothing
            statetext = Nothing
            begindate = Nothing
            enddate = Nothing
            iduser = Nothing
            username = Nothing
            approval = Nothing
            approvalText = Nothing

        End Try

    End Function

#End Region
   
End Class
