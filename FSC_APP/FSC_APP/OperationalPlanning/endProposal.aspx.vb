Option Strict On

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization

Partial Class OperationalPlanning_endProposal
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
            Session("lblTitle") = "FIANLIZAR PROCESO DE APROBACION DE UNA PROPUESTA."

            Me.lblMessage.Text = "Por Favor Haga click en 'Continuar' para finalizar 'PR04-Proceso de Revision de Propuesas' y crear 'PR01-Proceso de Aprobacion de Ideas'."

        End If

    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        ' definir los objetos
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idProcessInstance As String = String.Empty
        Dim idActivityInstance As String = String.Empty
        Dim idEntryData As String = String.Empty
        Dim proposal As New ProposalEntity
        Dim facade As New Facade
        Dim objIdea As New IdeaEntity

        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")
        idEntryData = Request.QueryString("id")

        ' cargar la lista de condiciones de la actividad
        Dim conditions As Array = GattacaApplication.getConditions(applicationCredentials, idActivityInstance)

        For Each condition As ListItem In conditions

            ' solo obtener la primera condicion y finalizar
            ' finalizar la actividad actual
            GattacaApplication.endActivityInstance(applicationCredentials, CLng(idProcessInstance), idActivityInstance, _
                                                   condition.Value, "Se ha finalizado el proceso de Aprobacion de una Propuesta", _
                                                   "", "", "", "")
            ' salirse del ciclo
            Exit For

        Next

        ' cargar los datos de la idea
        proposal = facade.loadProposal(applicationCredentials, CInt(idEntryData))

        ' cargar los datos de la idea que van a hacer parte del proyecto
        copyProporsalToIdea(proposal, objIdea)

        ' crear el nuevo proyecto
        objIdea.id = CInt(facade.addIdea(applicationCredentials, objIdea))

        ' crear el proceso de aporbacion de proyecto
        objIdea.IdProcessInstance = CInt(GattacaApplication.createProcessInstance(applicationCredentials, _
                                                                                  PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                                "WebForm", "IdeaEntity", objIdea.id.ToString, "0"))
        ' Iniciarlo
        objIdea.IdActivityInstance = CInt(GattacaApplication.startProcessInstance(applicationCredentials, objIdea.IdProcessInstance, _
                                                                             PublicFunction.getSettingValue("BPM.ProcessCase.PR01"), _
                                                                             "WebForm", "IdeaEntity", objIdea.id.ToString, "0"))
        ' actualizar la idea
        facade.updateIdea(applicationCredentials, objIdea)

        ' ir a la pagina de lista de tareas
        Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Copiar toda la informacion de la idea que va a pertenecer al proyecto
    ''' </summary>
    ''' <param name="idea"></param>
    ''' <param name="proposal"></param>
    ''' <remarks></remarks>
    Sub copyProporsalToIdea(ByVal proposal As ProposalEntity, _
                            ByVal idea As IdeaEntity)

        ' definir los objetos
        Dim locationByIdeaList As New List(Of LocationByIdeaEntity)
        Dim thirdByIdeaList As New List(Of ThirdByIdeaEntity)
        Dim ProgramComponentByIdeaList As New List(Of ProgramComponentByIdeaEntity)

        ' copiar los datos
        idea.name = proposal.projectname
        idea.objective = proposal.target
        idea.population = proposal.targetpopulation
        idea.name = proposal.projectname
        idea.results = proposal.result
        idea.cost = proposal.totalvalue
        idea.createdate = Now
        idea.enabled = True
        idea.startdate = Now

        ' cargar los datos de las ubicaciones
        For Each location As LocationByProposalEntity In proposal.LOCATIONSLIST

            Dim LocationByIdea As New LocationByIdeaEntity

            LocationByIdea.DEPTO.id = location.DEPTO.id
            LocationByIdea.CITY.id = location.CITY.id

            ' agregar a lA LISTA
            locationByIdeaList.Add(LocationByIdea)

        Next

        ' atualiar las listas
        idea.LOCATIONBYIDEALIST = locationByIdeaList
        idea.THIRDBYIDEALIST = thirdByIdeaList
        idea.ProgramComponentBYIDEALIST = ProgramComponentByIdeaList

    End Sub

#End Region

End Class
