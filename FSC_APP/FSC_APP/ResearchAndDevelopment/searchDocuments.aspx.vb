Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class searchDocuments
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
            Session("lblTitle") = "BUSCAR DOCUMENTOS"

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
        Dim list As List(Of DocumentsEntity)

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

        Dim objHyperlink As HyperLink
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim miEntidad As DocumentsEntity = e.Row.DataItem
            objHyperlink = e.Row.Cells(9).Controls(0)
            If (miEntidad.attachfile.Length > 0) Then
                objHyperlink.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "/" & miEntidad.attachfile
                objHyperlink.Target = "_blank"
            End If
        End If
    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of DocumentsEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim title As String = ""
        Dim description As String = ""
        Dim ideditedfor As String = ""
        Dim editedforname As String = ""
        Dim idvisibilitylevel As String = ""
        Dim visibilitylevelname As String = ""
        Dim iddocumenttype As String = ""
        Dim documenttypename As String = ""
        Dim createdate As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim attachfile As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim entityName As String = ""
        Dim projectName As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : id = CStr(Me.txtSearch.Text)
            Case "idlike" : idlike = CStr(Me.txtSearch.Text)
            Case "title" : title = CStr(Me.txtSearch.Text)
            Case "description" : description = CStr(Me.txtSearch.Text)
            Case "ideditedfor" : ideditedfor = CStr(Me.txtSearch.Text)
            Case "editedforname" : editedforname = CStr(Me.txtSearch.Text)
            Case "idvisibilitylevel" : idvisibilitylevel = CStr(Me.txtSearch.Text)
            Case "visibilitylevelname" : visibilitylevelname = CStr(Me.txtSearch.Text)
            Case "iddocumenttype" : iddocumenttype = CStr(Me.txtSearch.Text)
            Case "documenttypename" : documenttypename = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "attachfile" : attachfile = CStr(Me.txtSearch.Text)
            Case "enabled" : enabled = CStr(Me.txtSearch.Text)
            Case "enabledtext" : enabledtext = CStr(Me.txtSearch.Text)
            Case "entityName" : entityName = CStr(Me.txtSearch.Text)
            Case "projectName" : projectName = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getDocumentsList(applicationCredentials, _
             id, _
             idlike, _
             title, _
             description, _
             ideditedfor, _
             editedforname, _
             idvisibilitylevel, _
             visibilitylevelname, _
             iddocumenttype, _
             documenttypename, _
             createdate, _
             iduser, _
             username, _
             attachfile, _
             enabled, _
             enabledtext, _
             entityName, _
             projectName, _
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
			title = Nothing
			description = Nothing
            ideditedfor = Nothing
            editedforname = Nothing
            idvisibilitylevel = Nothing
            visibilitylevelname = Nothing
            iddocumenttype = Nothing
            documenttypename = Nothing
			createdate = Nothing
            iduser = Nothing
            username = Nothing
			attachfile = Nothing
            enabled = Nothing
            enabledtext = Nothing
            entityName = Nothing

        End Try

    End Function

#End Region

End Class
