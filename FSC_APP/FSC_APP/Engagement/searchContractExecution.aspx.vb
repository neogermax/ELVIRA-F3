Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchContractExecution
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
            Session("lblTitle") = "BUSCAR EJECUCIÓN DE CONTRATO."

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
        Dim list As List(Of ContractExecutionEntity)

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

    Public Function search() As List(Of ContractExecutionEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim idcontractrequest As String = ""
        Dim startdate As String = ""
        Dim paymentdate As String = ""
        Dim contractnumber As String = ""
        Dim ordernumber As String = ""
        Dim closingcomments As String = ""
        Dim closingdate As String = ""
        Dim finalpaymentdate As String = ""
        Dim value As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "idcontractrequest" : idcontractrequest = CStr(Me.txtSearch.Text)
            Case "startdate" : startdate = CStr(Me.txtSearch.Text)
            Case "paymentdate" : paymentdate = CStr(Me.txtSearch.Text)
            Case "contractnumber" : contractnumber = CStr(Me.txtSearch.Text)
            Case "ordernumber" : ordernumber = CStr(Me.txtSearch.Text)
            Case "closingcomments" : closingcomments = CStr(Me.txtSearch.Text)
            Case "closingdate" : closingdate = CStr(Me.txtSearch.Text)
            Case "finalpaymentdate" : finalpaymentdate = CStr(Me.txtSearch.Text)
            Case "value" : value = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getContractExecutionList(applicationCredentials, _
             idcontractrequest, _
             startdate, _
             paymentdate, _
             contractnumber, _
             ordernumber, _
             closingcomments, _
             closingdate, _
             finalpaymentdate, _
             value, _
             iduser, _
             username, _
             createdate, _
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
			idcontractrequest = Nothing
			startdate = Nothing
			paymentdate = Nothing
			contractnumber = Nothing
			ordernumber = Nothing
			closingcomments = Nothing
			closingdate = Nothing
			finalpaymentdate = Nothing
			value = Nothing
            iduser = Nothing
            username = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region

End Class
