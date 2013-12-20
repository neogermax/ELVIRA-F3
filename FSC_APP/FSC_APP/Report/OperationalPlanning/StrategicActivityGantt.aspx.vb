Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data

Partial Class Report_FormulationAndAdoption_StrategicActivityGantt
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
            loadCombos()

            ' cargar el titulo
            Session("lblTitle") = "GANTT DE ACTIVIDADES DE ESTRATEGIA"

            ' limpiar el gantt
            cleanGantt()

        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        ' definir los objetos
        Dim dt As DataTable
        Dim facade As New ReportFacade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lita de actividades
            dt = facade.getStrategicActivityGantt(applicationCredentials, Me.ddlidstrategy.SelectedValue)

            'Generar archivo tareas.xml en el servidor
            Dim xwriter As New XmlTextWriter(Server.MapPath("tareas.xml"), Nothing)

            'Atributos de identación para el archivo xml
            xwriter.Formatting = Formatting.Indented
            xwriter.Indentation = 4

            'De acuerdo a la documentación del jsGantt 1.2 http://www.jsgantt.com/
            xwriter.WriteStartElement("project")

            'recorrer la lista de actividades retornadas
            For Each registro As DataRow In dt.Rows

                xwriter.WriteStartElement("task")

                xwriter.WriteStartElement("pID")
                xwriter.WriteString(registro("Id").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pName")
                xwriter.WriteString(registro("Name").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pStart")
                xwriter.WriteString(registro("StartDate").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pEnd")
                xwriter.WriteString(registro("EndDate").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pColor")
                xwriter.WriteString("ff00ff")
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pCaption")
                xwriter.WriteString(registro("Name").ToString())
                xwriter.WriteEndElement()

                xwriter.WriteStartElement("pRes")
                xwriter.WriteString(registro("Resource").ToString())
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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            Me.ddlidstrategy.DataSource = facade.getStrategyList(applicationCredentials, order:="code")
            Me.ddlidstrategy.DataValueField = "Id"
            Me.ddlidstrategy.DataTextField = "Code"
            Me.ddlidstrategy.DataBind()

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
        Dim xwriter As New XmlTextWriter(Server.MapPath("tareas.xml"), Nothing)

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

#End Region

End Class
