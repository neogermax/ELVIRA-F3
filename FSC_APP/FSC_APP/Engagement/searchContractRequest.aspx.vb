Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient

Partial Class searchContractRequest
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
        Dim codeUser As DataTable = getCodeGroup(codigogrupo, applicationCredentials)
        codigogrupo = codeUser(0)(0).ToString()

        'ocultar el organizador
        'Me.lblSort.Text = ""
        'Me.ddlSort.Visible = False

        'validar que el grupo es administrador o seguimiento
        If (codigogrupo = "Seg y Eval") Or (codigogrupo = "ADMIN") Then
            Me.export.Visible = True
        Else
            Me.export.Visible = False
        End If

        If Not Page.IsPostBack Then

            'Mostrar mensaje finalizacion
            If Request.QueryString("successFinish") <> Nothing Then
                Me.lblsaveinformation.Text = "El proceso se culminó exitosamente!"
                Me.lblsaveinformation.ForeColor = Drawing.Color.Green
                Me.containerSuccess.Visible = True
            End If

            ' cargar el titulo
            Session("lblTitle") = "BUSCAR CONTRATO."

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
        Dim list As List(Of ContractRequestEntity)

        ' cargar la busqueda
        list = search()

        ' cargar los datos
        Me.gvData.DataSource = list
        Me.gvData.DataBind()
        Me.gvDataexp.DataSource = list
        Me.gvDataexp.DataBind()

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
                e.Row.Cells(7).Text = "Proceso Finalizado"
            Else
                e.Row.Cells(7).Text = "Pendiente finalizar proceso"
            End If

            'Naturaleza de contratacion (4)
            Select Case e.Row.Cells(4).Text

                Case 1
                    e.Row.Cells(4).Text = "Contrato"
                Case 2
                    e.Row.Cells(4).Text = "Convenio"
                Case 3
                    e.Row.Cells(4).Text = "Orden de prestación de servicios"
                Case 4
                    e.Row.Cells(4).Text = "Orden de compraventa"
                Case 5
                    e.Row.Cells(4).Text = "Otro si"
                Case 6
                    e.Row.Cells(4).Text = "Otros"
                Case Else
                    e.Row.Cells(4).Text = ""

            End Select

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

    Public Function search() As List(Of ContractRequestEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim requestnumber As String = ""
        Dim requestnumberlike As String = ""
        Dim idmanagement As String = ""
        Dim managementname As String = ""
        Dim idproject As String = ""
        Dim projectname As String = ""
        Dim idcontractnature As String = ""
        Dim contractnaturename As String = ""
        Dim contractnumberadjusted As String = ""
        Dim enabled As String = ""
        Dim enabledtext As String = ""
        Dim iduser As String = ""
        Dim username As String = ""
        Dim createdate As String = ""
        Dim filterfinish As String = CStr(Me.ddlFinished.Text)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener el valor de la busqueda
        Select Case CStr(ViewState("field"))

            Case "requestnumber" : requestnumber = CStr(Me.txtSearch.Text)
            Case "requestnumberlike" : requestnumberlike = CStr(Me.txtSearch.Text)
            Case "idmanagement" : idmanagement = CStr(Me.txtSearch.Text)
            Case "managementname" : managementname = CStr(Me.txtSearch.Text)
            Case "idproject" : idproject = CStr(Me.txtSearch.Text)
            Case "projectname" : projectname = CStr(Me.txtSearch.Text)
            Case "idcontractnature" : idcontractnature = CStr(Me.txtSearch.Text)
            Case "contractnaturename" : contractnaturename = CStr(Me.txtSearch.Text)
            Case "contractnumberadjusted" : contractnumberadjusted = CStr(Me.txtSearch.Text)
            Case "enabled" : enabled = CStr(Me.txtSearch.Text)
            Case "enabledtext" : enabledtext = CStr(Me.txtSearch.Text)
            Case "iduser" : iduser = CStr(Me.txtSearch.Text)
            Case "username" : username = CStr(Me.txtSearch.Text)
            Case "createdate" : createdate = CStr(Me.txtSearch.Text)
            Case "filterfinish" : filterfinish = CStr(Me.ddlFinished.Text)

        End Select

        Try
            ' buscar
            search = facade.getContractRequestList(applicationCredentials, _
             requestnumber, _
             requestnumberlike, _
             idmanagement, _
             managementname, _
             idproject, _
             projectname, _
             idcontractnature, _
             contractnaturename, _
             contractnumberadjusted, _
             enabled, _
             enabledtext, _
             iduser, _
             username, _
             createdate, _
             filterfinish)

        Catch ex As Exception
            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar los objetos
            facade = Nothing
            requestnumber = Nothing
            idmanagement = Nothing
            managementname = Nothing
            idproject = Nothing
            projectname = Nothing
            idcontractnature = Nothing
            contractnaturename = Nothing
            contractnumberadjusted = Nothing
            enabled = Nothing
            enabledtext = Nothing
            iduser = Nothing
            createdate = Nothing

        End Try

    End Function

#End Region

End Class
