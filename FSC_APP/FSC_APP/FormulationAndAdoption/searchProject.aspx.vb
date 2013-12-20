Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient

Partial Class searchProject
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
        'obtener codigo del grupo
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
            Session("lblTitle") = "BUSCAR PROYECTOS."

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' datos de la busqueda
        ViewState("field") = Me.rblSearch.SelectedValue
        ViewState("value") = Me.txtSearch.Text

        ' actualizar el valor de la paginacion
        Me.gvData.PageIndex = 0

        ' definir los objetos
        Dim list As List(Of ProjectEntity)


        'obtener codigo del grupo
        Dim codigogrupo As String = Session("mMenu")
        codigogrupo = codigogrupo.Replace("_", "")
        Dim codeUser As DataTable = getCodeGroup(codigogrupo, applicationCredentials)
        codigogrupo = codeUser(0)(0).ToString()
        ' cargar la busqueda
        list = search()
        'validar que el grupo es administrador o seguimiento
        If (codigogrupo = "Seg y Eval") Or (codigogrupo = "ADMIN") Then
            Me.export.Visible = True
        Else
            Me.export.Visible = False
        End If

        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()
        Me.gvDataExport.DataSource = list
        Me.gvDataExport.DataBind()
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

            If e.Row.Cells(10).Text.ToUpper.Equals("TRUE") Then

                e.Row.Cells(10).Text = "Habilitado"

            Else

                e.Row.Cells(10).Text = "Deshabilitado"

            End If

        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ProjectEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim idlike As String = ""
        Dim ididea As String = ""
        Dim ideaname As String = ""
        Dim code As String = ""
        Dim name As String = ""
        Dim objective As String = ""
        Dim antecedent As String = ""
        Dim justification As String = ""
        Dim begindate As String = ""
        Dim duration As String = ""
        Dim zonedescription As String = ""
        Dim population As String = ""
        Dim strategicdescription As String = ""
        Dim results As String = ""
        Dim source As String = ""
        Dim purpose As String = ""
        Dim totalcost As String = ""
        Dim fsccontribution As String = ""
        Dim counterpartvalue As String = ""
        Dim effectivebudget As String = ""
        Dim attachment As String = ""
        Dim idphase As String = ""
        Dim phasename As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim idStrategicLine As String = ""
        Dim StrategicLinename As String = ""
        Dim idProgram As String = ""
        Dim Programname As String = ""
        Dim idProgramComponent As String = ""
        Dim ProgramComponentname As String = ""
        Dim currentactivityname As String = ""
        Dim createdate As String = ""
        Dim completiondate As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "id" : idlike = CStr(Me.txtSearch.Text)
            Case "ideaname" : ideaname = CStr(Me.txtSearch.Text)
            Case "code" : code = CStr(Me.txtSearch.Text)
            Case "name" : name = CStr(Me.txtSearch.Text)
            Case "StrategicLinename" : StrategicLinename = CStr(Me.txtSearch.Text)
            Case "Programname" : Programname = CStr(Me.txtSearch.Text)
            Case "ProgramComponentname" : ProgramComponentname = CStr(Me.txtSearch.Text)
            Case "currentactivityname" : currentactivityname = CStr(Me.txtSearch.Text)
            Case "phasename" : phasename = CStr(Me.txtSearch.Text)
            Case "enabled" : enabledtext = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text) ' se restaura linea eliminada para la busqueda por usuario GR
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)

        End Select

        Try
            ' buscar
            search = facade.getProjectList(applicationCredentials, _
            id, _
             idlike, _
             ididea, _
             ideaname, _
             code, _
             name, _
             objective, _
             antecedent, _
             justification, _
             begindate, _
             duration, _
             zonedescription, _
             population, _
             strategicdescription, _
             results, _
             source, _
             purpose, _
             totalcost, _
             fsccontribution, _
             counterpartvalue, _
             effectivebudget, _
             attachment, _
             idphase, _
             phasename, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             idStrategicLine, _
             StrategicLinename, _
             idProgram, _
             Programname, _
             idProgramComponent, _
             ProgramComponentname, _
             currentactivityname, _
             createdate, , _
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
			code = Nothing
			name = Nothing
			objective = Nothing
			antecedent = Nothing
			justification = Nothing
			begindate = Nothing
			duration = Nothing
			zonedescription = Nothing
			population = Nothing
			strategicdescription = Nothing
			results = Nothing
			source = Nothing
			purpose = Nothing
			totalcost = Nothing
			fsccontribution = Nothing
			counterpartvalue = Nothing
			effectivebudget = Nothing
			attachment = Nothing
			idphase = Nothing
			enabled = Nothing
			iduser = Nothing
			createdate = Nothing

        End Try

    End Function

#End Region
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

   
    
End Class
