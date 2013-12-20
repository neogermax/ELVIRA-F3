Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class overallProjectPanel
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
            Session("lblTitle") = "PANEL GENERAL DE PROYECTOS."

            ' definir los objetos
            Dim list As List(Of ProjectEntity)

            ' actualizar el valor de la paginacion
            Me.gvData.PageIndex = 0

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
        End If

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

    Protected Sub updatePhase(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvData.RowCommand

        Me.upSubTitle.Update()

        ' definirlos objetos
        Dim consultLastVersion As Boolean = True
        Dim idProject As Integer
        Dim idPhase As Integer
        Dim facade As New Facade
        Dim objProject As New ProjectEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim list As List(Of ProjectEntity)

        ' Rescatar el Id del project asociado
        Integer.TryParse(e.CommandArgument.ToString, idProject)

        ' Rescatar la fase y validar su pre-cambio
        Integer.TryParse(e.CommandName.ToString, idPhase)
        If (idPhase < 4 And idPhase <> 0) Then
            If (idProject <> 0) Then
                ' cargar el registro referenciado
                objProject = facade.loadProject(applicationCredentials, idProject, consultLastVersion)
                objProject.idphase = idPhase + 1
                objProject.createdate = Now
                objProject.iduser = applicationCredentials.UserID

                Try
                    ' cambiar en la bd

                    facade.updateProject(applicationCredentials, objProject, "", idPhase + 1)
                   
                    ' actualizar el valor de la paginacion
                    Me.gvData.PageIndex = 0

                    ' cargar la busqueda
                    list = search()

                    ' cargar los datos
                    Me.gvData.DataSource = list
                    Me.gvData.DataBind()

                    If list.Count > 0 Then

                        ' mostrar la busqueda
                        Me.lblSubTitle.Text = "Cambio realizado con éxito"

                    Else
                        ' mostrar la busqueda
                        Me.lblSubTitle.Text = "La búsqueda no produjo resultados."

                    End If

                Catch ex As Exception
                    ' ir a error
                    Session("sError") = ex.Message
                    Session("sUrl") = Request.UrlReferrer.PathAndQuery
                    Response.Redirect("~/errors/error.aspx")
                    Response.End()

                Finally
                    ' liberar los objetos
                    facade = Nothing

                End Try

            Else

                Me.lblSubTitle.Text = "Existen problemas al retornar el proyecto, intente de nuevo"

            End If
        Else

            Me.lblSubTitle.Text = "No se puede cambiar la fase, este proyecto se encuentra en su etapa de evaluación"

        End If

        ' actualizar los datos
        Me.upSubTitle.Update()
        Me.upData.Update()

    End Sub

#End Region

#Region "Metodos"

    Public Function search() As List(Of ProjectEntity)

        ' definirlos objetos
        search = New List(Of ProjectEntity)
        Dim facade As New Facade
        
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' buscar
            search = facade.getProjectListNoFilter(applicationCredentials)

        Catch ex As Exception
            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar los objetos
            facade = Nothing

        End Try

    End Function

#End Region

End Class
