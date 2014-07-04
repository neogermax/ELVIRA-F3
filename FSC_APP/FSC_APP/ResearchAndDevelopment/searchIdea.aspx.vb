Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient

Partial Class searchIdea
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

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        Dim codigogrupo As String = Session("mMenu")
        codigogrupo = codigogrupo.Replace("_", "")
        Dim codeUser As DataTable = getCodeGroup(codigogrupo, ApplicationCredentials)
        codigogrupo = codeUser(0)(0).ToString()


        'validar que el grupo es administrador o seguimiento
        If (codigogrupo = "Seg y Eval") Or (codigogrupo = "ADMIN") Then
            Me.export.Visible = True
        Else
            Me.export.Visible = False
        End If

        If Not Page.IsPostBack Then


            ' cargar el titulo
            Session("lblTitle") = "BUSCAR UNA IDEA."

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
        Dim list As List(Of IdeaEntity)

        ' cargar la busqueda
        list = search()

        
        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()
        Me.gvDataexp.DataSource = list
        Me.gvDataexp.DataBind()


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

            If e.Row.Cells(7).Text.ToUpper.Equals("TRUE") Then
                e.Row.Cells(7).Text = "SI"
            Else
                e.Row.Cells(7).Text = "NO"
            End If

            If e.Row.Cells(12).Text.ToUpper.Equals("TRUE") Then
                e.Row.Cells(12).Text = "Habilitado"
            Else
                e.Row.Cells(12).Text = "Deshabilitado"
            End If
        End If
    End Sub

#End Region

#Region "Metodos"
    Public Function getCodeGroup(ByVal idGroup As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Try
            'consulta del codigo del grupo
            sql.Append(" USE [FSC_eSecurity] ")
            sql.Append("SELECT ug.Code, ug.Name FROM UserGroup ug ")
            sql.Append("WHERE ug.id =" & idGroup)
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Return data
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function search() As List(Of IdeaEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim code As String = ""
        Dim name As String = ""
        Dim objective As String = ""
        Dim startdate As String = ""
        Dim duration As String = ""
        Dim areadescription As String = ""
        Dim population As String = ""
        Dim cost As String = ""
        Dim strategydescription As String = ""
        Dim results As String = ""
        Dim source As String = ""
        Dim idsummoning As String = ""
        Dim startprocess As String = ""
        Dim startprocesstext As String = ""
        Dim createdate As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim StrategicLineName As String = ""
        Dim ProgramName As String = ""
        Dim ProgramComponentName As String = ""
        Dim filterstatus As String = CStr(Me.ddlFinished.SelectedValue)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : id = CStr(Me.txtSearch.Text)
            Case "idlike" : idlike = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "name" : name = CStr(Me.txtSearch.Text)
            Case "objective" : objective = CStr(Me.txtSearch.Text)
            Case "startdate" : startdate = CStr(Me.txtSearch.Text)
            Case "duration" : duration = CStr(Me.txtSearch.Text)
            Case "areadescription" : areadescription = CStr(Me.txtSearch.Text)
            Case "population" : population = CStr(Me.txtSearch.Text)
            Case "cost" : cost = CStr(Me.txtSearch.Text)
            Case "strategydescription" : strategydescription = CStr(Me.txtSearch.Text)
            Case "results" : results = CStr(Me.txtSearch.Text)
            Case "source" : source = CStr(Me.txtSearch.Text)
            Case "idsummoning" : idsummoning = CStr(Me.txtSearch.Text)
            Case "startprocess" : startprocess = CStr(Me.txtSearch.Text)
            Case "startprocesstext" : startprocesstext = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "enabled" : enabled = CStr(Me.txtSearch.Text)
            Case "enabledtext" : enabledtext = CStr(Me.txtSearch.Text)
            Case "StrategicLineName" : StrategicLineName = CStr(Me.txtSearch.Text)
            Case "ProgramName" : ProgramName = CStr(Me.txtSearch.Text)
            Case "ProgramComponentName" : ProgramComponentName = CStr(Me.txtSearch.Text)
            Case "filterstatus" : filterstatus = CStr(Me.ddlFinished.SelectedValue)

        End Select

        Try
            ' buscar
            search = facade.getIdeaList(applicationCredentials, _
             id, _
             idlike, _
             code, _
             name, _
             objective, _
             startdate, _
             duration, _
             areadescription, _
             population, _
             cost, _
             strategydescription, _
             results, _
             source, _
             idsummoning, _
             startprocess, _
             startprocesstext, _
             createdate, _
             iduser, _
             username, _
             enabled, _
             enabledtext, _
             StrategicLineName, _
             ProgramName, _
             ProgramComponentName, _
             filterstatus)

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
            name = Nothing
            objective = Nothing
            startdate = Nothing
            duration = Nothing
            areadescription = Nothing
            population = Nothing
            cost = Nothing
            strategydescription = Nothing
            results = Nothing
            source = Nothing
            idsummoning = Nothing
            startprocess = Nothing
            startprocesstext = Nothing
            createdate = Nothing
            iduser = Nothing
            username = Nothing
            enabled = Nothing
            enabledtext = Nothing
            StrategicLineName = Nothing
            ProgramName = Nothing
            ProgramComponentName = Nothing

        End Try

    End Function


#End Region

End Class
