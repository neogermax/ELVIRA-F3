Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Partial Class Report_FormulationAndAdoption_reportProjectChronogram
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

            'Cargar combos
            loadComboProject()
            Me.loadComboProjectPhase()

            'Se verifica si viene un identificador de proyecto por la URL
            If Not (Request.QueryString("idProject") Is Nothing) Then
                Me.ddlProject.SelectedValue = Request.QueryString("idProject").ToString()

                'Se llama al metodo que pobla los componentes
                Me.loadComboComponent()

                'Se llama al metodo que pemrite crear el gantt para el proyecto requerido
                Me.createGantt()
            Else

                ' limpiar el gantt
                cleanGantt()

            End If

            ' cargar el titulo
            Session("lblTitle") = "Reporte cronograma del proyecto"

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'Se llama al metodo que pemrite crear el gantt para el proyecto requerido
        Me.createGantt()

    End Sub

    Protected Sub ddlProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
        loadComboComponent()
    End Sub

    'Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

    '    Response.Redirect("reportProjectChronogramPrint.aspx?idComponent=" & Me.ddlComponent.SelectedValue)

    'End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo que permite crear el gantt para un proyecto determinado
    ''' </summary>
    ''' <param name="idKeyProject"></param>
    ''' <remarks></remarks>
    Private Sub createGantt(Optional ByVal idKeyProject As String = "")

        ' definir los objetos
        Dim dt As DataTable
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idComponent As Integer = 0

        Try
            ' cargar la lita de actividades
            If Me.ddlComponent.SelectedValue.Length > 0 Then idComponent = Me.ddlComponent.SelectedValue
            dt = facade.loadReportProjectChronogram(applicationCredentials, idComponent, Me.ddlProjectPhase.SelectedValue, ddlProject.SelectedValue)

            'Generar archivo tareas.xml en el servidor
            Dim xwriter As New XmlTextWriter(Server.MapPath("Actividades.xml"), Nothing)

            'Atributos de identación para el archivo xml
            xwriter.Formatting = Formatting.Indented
            xwriter.Indentation = 4

            'De acuerdo a la documentación del jsGantt 1.2 http://www.jsgantt.com/
            xwriter.WriteStartElement("project")

            'recorrer la lista de actividades retornadas
            For Each registro As DataRow In dt.Rows

                xwriter.WriteStartElement("task")

                xwriter.WriteStartElement("pID")
                xwriter.WriteString(registro("IdSubActivity").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pName")
                xwriter.WriteString(registro("SubActivityName").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pStart")
                xwriter.WriteString(registro("BeginDate").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pEnd")
                xwriter.WriteString(registro("EndDate").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pColor")
                xwriter.WriteString("ff00ff")
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pCaption")
                xwriter.WriteString(registro("SubActivityName").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pRes")
                xwriter.WriteString(registro("Responsible").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteEndElement()

            Next

            ' escribir los elementos
            xwriter.WriteEndElement()

            'liberar
            xwriter.Flush()
            xwriter.Close()

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub


    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadComboProject(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable
        Dim dtProject As New DataTable
        Dim dvProject As New DataView
        Try
            ' cargar la lista de los tipos
            dt = facade.loadReportProjectChronogram(applicationCredentials, 0, Me.ddlProjectPhase.SelectedValue, 0)
            dvProject = dt.DefaultView
            dvProject.Sort = "IdProject desc"
            dtProject = dvProject.ToTable(True, "ProjectName", "IdProject")

            Me.ddlProject.DataSource = dtProject
            Me.ddlProject.DataValueField = "IdProject"
            Me.ddlProject.DataTextField = "ProjectName"
            Me.ddlProject.DataBind()
            Me.ddlProject.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    Public Sub cleanGantt()

        'Generar archivo tareas.xml en el servidor
        Dim xwriter As New XmlTextWriter(Server.MapPath("actividades.xml"), Nothing)

        'Atributos de identación para el archivo xml
        xwriter.Formatting = Formatting.Indented
        xwriter.Indentation = 4

        'De acuerdo a la documentación del jsGantt 1.2 http://www.jsgantt.com/
        xwriter.WriteStartElement("project")

        ' escribir los elementos
        xwriter.WriteEndElement()

        'liberar
        xwriter.Flush()
        xwriter.Close()

    End Sub

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadComboComponent(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim dt As New DataTable
        Dim dtComponent As New DataTable
        Dim dvComponent As New DataView
        Try
            ' cargar la lista de los tipos
            dt = facade.loadReportProjectChronogram(applicationCredentials, 0, Me.ddlProjectPhase.SelectedValue, ddlProject.SelectedValue)
            dvComponent = dt.DefaultView
            dvComponent.RowFilter = " IdProject='" + ddlProject.SelectedValue.ToString() + "'"
            dvComponent.Sort = "ComponentName ASC"
            dtComponent = dvComponent.ToTable(True, "ComponentCode", "IdComponent")

            Me.ddlComponent.DataSource = dtComponent
            Me.ddlComponent.DataValueField = "IdComponent"
            Me.ddlComponent.DataTextField = "ComponentCode"
            Me.ddlComponent.DataBind()
            Me.ddlComponent.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

            ' agregar la opcion de todos
            Me.ddlComponent.Items.Add(New ListItem("Todos los componentes", ""))
            Me.ddlComponent.SelectedValue = ""

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Permite poblar el combo de fases de proyecto
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub loadComboProjectPhase()

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se llama al metodo que permite poblar el combo de fases de proyecto
            ' cargar la lista de las fases de un proyecto
            Me.ddlProjectPhase.DataSource = facade.getProjectPhaseList(applicationCredentials, isenabled:="1", order:="name")
            Me.ddlProjectPhase.DataValueField = "id"
            Me.ddlProjectPhase.DataTextField = "name"
            Me.ddlProjectPhase.DataBind()

            'Se agrega el item todos
            Me.ddlProjectPhase.Items.Add(New ListItem("Todas", ""))
            Me.ddlProjectPhase.SelectedValue = ""

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

#End Region

End Class
