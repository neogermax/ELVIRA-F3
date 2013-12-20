Option Strict On

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization

Partial Class OperationalPlanning_changeProjectState
    Inherits System.Web.UI.Page

#Region "Eventos de la Pagina"

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

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar el titulo
            Session("lblTitle") = "CAMBIAR ESTADO DEL PROYECTO A EJECUCION."

            Me.lblMessage.Text = "Por Favor Haga click en 'Continuar' para finalizar 'PR03-Proceso de Aprobacion de Planeacion Operativa'  y crear ."

        End If

    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        ' definir los objetos
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty
        Dim idEntryData As String = String.Empty
        Dim project As New ProjectEntity
        Dim facade As New Facade

        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")
        idEntryData = Request.QueryString("IdEntryData")

        ' cargar la lista de condiciones de la actividad
        Dim conditions As Array = GattacaApplication.getConditions(applicationCredentials, idActivityInstance)

        For Each condition As ListItem In conditions

            ' solo obtener la primera condicion y finalizar
            ' finalizar la actividad actual
            GattacaApplication.endActivityInstance(applicationCredentials, CLng(idProcessInstance), idActivityInstance, _
                                                   condition.Value, "Se ha finalizado el proceso de Planeacion Operativa", _
                                                   "", "", "", "")
            ' salirse del ciclo
            Exit For

        Next

        ' cargar los datos del proyecto
        project = facade.loadProject(applicationCredentials, CInt(idEntryData))

        ' cambiar el proyecto a fase de ejecucion
        project.idphase = 3

        ' actualizar el proyecto
        facade.updateProject(applicationCredentials, project, "")

        'Cambia las fases a todos los elementos del proyecto
        facade.ChangePhases(applicationCredentials, project.idKey, project.idphase)

        ' ir a la pagina de lista de tareas
        Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

    End Sub

#End Region

End Class
