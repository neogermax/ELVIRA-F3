Option Strict On

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization

Partial Class FormulationAndAdoption_changeProjectState
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
            Session("lblTitle") = "CAMBIAR ESTADO DEL PROYECTO A PLANEACION OPERATIVA."

            Me.lblMessage.Text = "Por Favor Haga click en 'Continuar' para finalizar 'PR02-Proceso de Formulación y Aprobación de Proyecto' y crear 'PR03-Proceso de Aprobacion de Planeacion Operativa'."

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
                                                   condition.Value, "Se ha finalizado el proceso de Formulacion y Aprobacion", _
                                                   "", "", "", "")
            ' salirse del ciclo
            Exit For

        Next

        ' cargar los datos del proyecto
        project = facade.loadProject(applicationCredentials, CInt(idEntryData))

        ' cambiar el proyecto a fase de planeacion operativa
        project.idphase = 2

        ' crear el proceso de aporbacion de proyecto
        project.IdProcessInstance = CInt(GattacaApplication.createProcessInstance(applicationCredentials, _
                                                                                  PublicFunction.getSettingValue("BPM.ProcessCase.PR03"), _
                                                                                "WebForm", "ProjectEntity", project.idKey.ToString, "0"))
        ' Iniciarlo
        project.IdActivityInstance = CInt(GattacaApplication.startProcessInstance(applicationCredentials, project.IdProcessInstance, _
                                                                             PublicFunction.getSettingValue("BPM.ProcessCase.PR03"), _
                                                                             "WebForm", "ProjectEntity", project.idKey.ToString, "0"))
        ' actualizar la Idea
        facade.updateProject(applicationCredentials, project, "")

        'Cambia las fases a todos los elementos del proyecto
        facade.ChangePhases(applicationCredentials, project.idKey, project.idphase)

        ' ir a la pagina de lista de tareas
        Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

    End Sub

#End Region

End Class
