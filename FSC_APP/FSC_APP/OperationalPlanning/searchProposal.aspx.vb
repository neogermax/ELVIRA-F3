Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchProposal
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
            Session("lblTitle") = "BUSCAR PROPUESTA."

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
        Dim list As List(Of ProposalEntity)

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

#End Region

#Region "Metodos"

    Public Function search() As List(Of ProposalEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idLike As String = ""
        Dim idsummoning As String = ""
        Dim summoningName As String = ""
        Dim nameOperator As String = ""
        Dim operatornit As String = ""
        Dim projectname As String = ""
        Dim target As String = ""
        Dim targetpopulation As String = ""
        Dim expectedresults As String = ""
        Dim totalvalue As String = ""
        Dim inputfsc As String = ""
        Dim inputothersources As String = ""
        Dim briefprojectdescription As String = ""
        Dim score As String = ""
        Dim result As String = ""
        Dim responsiblereview As String = ""
        Dim reviewdate As String = ""
        Dim enabled As String = ""
        Dim createdate As String = ""
        Dim deptoName As String = ""
        Dim cityName As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : id = CStr(Me.txtSearch.Text)
            Case "idLike" : idLike = CStr(Me.txtSearch.Text)
            Case "idsummoning" : idsummoning = CStr(Me.txtSearch.Text)
            Case "summoningName" : summoningName = CStr(Me.txtSearch.Text)
            Case "nameOperator" : nameOperator = CStr(Me.txtSearch.Text)
            Case "operatornit" : operatornit = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "target" : target = CStr(Me.txtSearch.Text)
            Case "targetpopulation" : targetpopulation = CStr(Me.txtSearch.Text)
            Case "expectedresults" : expectedresults = CStr(Me.txtSearch.Text)
            Case "totalvalue" : totalvalue = CStr(Me.txtSearch.Text)
            Case "inputfsc" : inputfsc = CStr(Me.txtSearch.Text)
            Case "inputothersources" : inputothersources = CStr(Me.txtSearch.Text)
            Case "briefprojectdescription" : briefprojectdescription = CStr(Me.txtSearch.Text)
            Case "score" : score = CStr(Me.txtSearch.Text)
            Case "result" : result = CStr(Me.txtSearch.Text)
            Case "responsiblereview" : responsiblereview = CStr(Me.txtSearch.Text)
            Case "reviewdate" : reviewdate = CStr(Me.txtSearch.Text)
            Case "enabled" : enabled = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "deptoName" : deptoName = CStr(Me.txtSearch.Text)
            Case "cityName" : cityName = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getProposalList(applicationCredentials, _
             id, _
             idLike, _
             idsummoning, _
             summoningName, _
             nameOperator, _
             operatornit, _
             projectname, _
             target, _
             targetpopulation, _
             expectedresults, _
             totalvalue, _
             inputfsc, _
             inputothersources, _
             briefprojectdescription, _
             score, _
             result, _
             responsiblereview, _
             reviewdate, _
             enabled, _
             createdate, _
             deptoName, _
             cityName, _
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
            idLike = Nothing
            idsummoning = Nothing
            summoningName = Nothing
            nameOperator = Nothing
            operatornit = Nothing
            projectname = Nothing
            target = Nothing
            targetpopulation = Nothing
            expectedresults = Nothing
            totalvalue = Nothing
            inputfsc = Nothing
            inputothersources = Nothing
            briefprojectdescription = Nothing
            score = Nothing
            result = Nothing
            responsiblereview = Nothing
            reviewdate = Nothing
            enabled = Nothing
            createdate = Nothing
            deptoName = Nothing
            cityName = Nothing

        End Try

    End Function

#End Region

End Class
