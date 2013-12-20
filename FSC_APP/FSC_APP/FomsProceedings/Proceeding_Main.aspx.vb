Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO

Partial Class FomsProceedings_Proceeding_Main
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

        Try

            Dim valido As Boolean

            Select Case Request.QueryString("tp")

                Case "str" 'Inicio
                    valido = True
                    'Titulo
                    Session("lblTitle") = "GENERACION DE ACTAS DE INICIO"

                Case "trc" 'Seguimiento
                    valido = True
                    'Titulo
                    Session("lblTitle") = "GENERACION DE ACTAS DE SEGUIMIENTO"

                Case "cls" 'Cierre
                    valido = True
                    'Titulo
                    Session("lblTitle") = "GENERACION DE ACTAS DE CIERRE"
                Case Else
                    valido = False

            End Select

            If Not Page.IsPostBack Then

                If valido = False Then
                    Me.dvProyecto.Visible = False
                    Me.dvRedir.Visible = True
                Else
                    Me.dvProyecto.Visible = True
                    Me.dvRedir.Visible = False
                End If

                loadCombos()
            End If

        Catch ex As Exception

            Throw New Exception("Error al cargar la lista de proyectos." & ex.Message)

        End Try

    End Sub

#End Region

#Region "Métodos"

    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim facade As New Facade

        Me.LoadDropDownListproject(facade, applicationCredentials)

    End Sub

    Private Sub LoadDropDownListproject(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        Me.ddlprojectsvalidate.DataSource = getprojectactainicioList(applicationCredentials)
        Me.ddlprojectsvalidate.DataValueField = "Id"
        Me.ddlprojectsvalidate.DataTextField = "Name"
        Me.ddlprojectsvalidate.DataBind()

    End Sub


    Public Function getprojectactainicioList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectEntity)

        Dim sql As New StringBuilder
        Dim objproject As ProjectEntity
        Dim projectList As New List(Of ProjectEntity)
        Dim data As DataTable

        Try
            ' construir la sentencia

            sql.Append("select distinct p.id, p.name as nombre, CONVERT(varchar, p.id) + '_'  + p.Name as name  from Project p ")
            sql.Append("inner join ContractRequest cr on cr.IdProject = p.id where cr.Finished = 1    ")
            sql.Append("order by p.id desc , nombre,name   ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objproject = New ProjectEntity

                ' cargar el valor del campo
                objproject.id = row("id")
                objproject.name = row("name")

                ' agregar a la lista
                projectList.Add(objproject)

            Next

            ' retornar el objeto
            getprojectactainicioList = projectList


        Catch ex As Exception

            Throw New Exception("Error al cargar la lista de Proyectos. " & ex.Message)

        Finally
            ' liberando recursos
            objproject = Nothing

        End Try

    End Function


#End Region

    Protected Sub btnRedir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedir.Click
        Response.Redirect("/NewMenu/Menu.aspx")
    End Sub

    Protected Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click

        Dim ruta As String
        Dim suf As String

        If Me.ddlprojectsvalidate.SelectedValue > 0 Then
            'agregar sufijo
            suf = "?cid=" & Me.ddlprojectsvalidate.SelectedValue

            'redireccionar al acta correspondiente
            Select Case Request.QueryString("tp")

                Case "str" 'Inicio

                    ruta = "/FomsProceedings/Proceedings_stars.aspx" & suf

                Case "trc" 'Seguimiento

                    ruta = "/FomsProceedings/Proceeding_tracing.aspx" & suf

                Case "cls" 'Cierre

                    ruta = "/FomsProceedings/Proceeding_close.aspx" & suf

                Case Else
                    ruta = "/NewMenu/Menu.aspx"
            End Select

            Response.Redirect(ruta)

        End If

    End Sub
End Class
