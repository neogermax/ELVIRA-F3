Option Strict On

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Globalization

Partial Class ResearchAndDevelopment_endIdea
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
            Session("lblTitle") = "FINALIZAR PROCESO DE APROBACIÓN DE IDEA."

            Me.lblMessage.Text = "Por Favor Haga click en 'Continuar' para finalizar 'PR01-Proceso de Aprobación de Ideas' y crear 'PR02-Proceso de Formulación y Aprobación de Proyecto'."

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
        Dim objIdea As New IdeaEntity

        ' cargar los valores del BPM
        idProcessInstance = Request.QueryString("idProcessInstance")
        idActivityInstance = Request.QueryString("idActivityInstance")
        idEntryData = Request.QueryString("id")

        ' finalizar la actividad actual
        GattacaApplication.endActivityInstance(applicationCredentials, CLng(idProcessInstance), idActivityInstance, _
        PublicFunction.getSettingValue("BPM.Condition.PR01-CD007"), "Se ha finalizado el proceso de Idea", _
        "", "", "", "")

        ' cargar los datos de la idea
        objIdea = facade.loadIdea(applicationCredentials, CInt(idEntryData))

        ' cargar los datos de la idea que van a hacer parte del proyecto
        copyIdeaToProject(objIdea, project)

        ' crear el nuevo proyecto
        project.id = CInt(facade.addProject(applicationCredentials, project))
        project.idKey = project.id
        ' crear el proceso de aporbacion de proyecto
        project.IdProcessInstance = CInt(GattacaApplication.createProcessInstance(applicationCredentials, _
                                                                                 PublicFunction.getSettingValue("BPM.ProcessCase.PR02"), _
                                                                              "WebForm", "ProjectEntity", project.id.ToString, "0"))
        ' Iniciarlo
        project.IdActivityInstance = CInt(GattacaApplication.startProcessInstance(applicationCredentials, project.IdProcessInstance, _
                                                                           PublicFunction.getSettingValue("BPM.ProcessCase.PR02"), _
                                                                          "WebForm", "ProjectEntity", project.id.ToString, "0"))
        ' actualizar la Idea
        facade.updateProject(applicationCredentials, project, "")

        'Cambia las fases a todos los elementos del proyecto
        facade.ChangePhases(applicationCredentials, project.idKey, project.idphase)

        ' ir a la pagina de lista de tareas
        Response.Redirect(PublicFunction.getSettingValue("BPM.TaskList"))

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Copiar toda la informacion de la idea que va a pertenecer al proyecto
    ''' </summary>
    ''' <param name="idea"></param>
    ''' <param name="project"></param>
    ''' <remarks></remarks>
    Sub copyIdeaToProject(ByVal idea As IdeaEntity, _
                        ByVal project As ProjectEntity)

        ' definir los objetos
        Dim thirdByProjectList As New List(Of ThirdByProjectEntity)
        Dim thirdByProject As ThirdByProjectEntity

        Dim ProgramComponentByProjectList As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)
        Dim objProgramComponentByProject As ProgramComponentByProjectEntity

        Dim objProjectLoactionList As New List(Of ProjectLocationEntity)
        Dim objProjectLocation As New ProjectLocationEntity

        ' cargar los datos al proyecto que pertences a la idea
        project.ididea = idea.id
        project.code = idea.code
        project.name = idea.name
        project.begindate = idea.startdate
        project.duration = idea.duration
        project.zonedescription = idea.areadescription
        project.totalcost = idea.cost
        project.fsccontribution = idea.fsccontribution
        project.counterpartvalue = idea.counterpartvalue
        project.strategicdescription = idea.strategydescription
        project.results = idea.results
        project.justification = idea.justification
        'project. = idea.startprocess
        project.createdate = Now

        ' Se crea el proyecto inicando en fase de "Formulacion y Aprobacion"
        project.idphase = 1
        project.isLastVersion = True
        project.enabled = True

        '-----------------   TERCEROS   -----------------------------
        ' cargar la lista de terceros de la idea y agregarselos al proyecto
        For Each thirdByIdea As ThirdByIdeaEntity In idea.THIRDBYIDEALIST

            ' crear el tercero
            thirdByProject = New ThirdByProjectEntity

            ' copiar los datos
            '  thirdByProject.idthird = thirdByIdea.THIRD.id
            'thirdByProject.type = CInt(thirdByIdea.type)
            'thirdByProject.THIRDNAME = thirdByIdea.THIRD.name
            'thirdByProject.experiences = thirdByIdea.experiences
            'thirdByProject.actions = thirdByIdea.actions
            thirdByProject.type = thirdByIdea.type
            ' agregarlo a la lista
            thirdByProjectList.Add(thirdByProject)

        Next

        ' agregarlo al proyecto
        project.thirdbyprojectlist = thirdByProjectList

        '-----------------   Actividades   -----------------------------
        ' cargar la lista de actividaes de la idea y agregarselos al proyecto
        For Each myProgramComponentByIdea As ProgramComponentByIdeaEntity In idea.ProgramComponentBYIDEALIST

            ' crear el tercero
            objProgramComponentByProject = New ProgramComponentByProjectEntity

            ' copiar los datos
            objProgramComponentByProject.idProgramComponent = myProgramComponentByIdea.idProgramComponent

            ' agregarlo a la lista
            ProgramComponentByProjectList.Add(objProgramComponentByProject)

        Next

        ' agregarlo al proyecto
        project.ProgramComponentbyprojectlist = ProgramComponentByProjectList

        '-----------------   Ubicaciones   -----------------------------
        ' cargar la lista de actividaes de la idea y agregarselos al proyecto
        For Each LocationByIdea As LocationByIdeaEntity In idea.LOCATIONBYIDEALIST

            ' crear el tercero
            objProjectLocation = New ProjectLocationEntity

            ' copiar los datos
            objProjectLocation.idcity = LocationByIdea.CITY.id
            objProjectLocation.IDDEPTO = LocationByIdea.DEPTO.id

            ' agregarlo a la lista
            objProjectLoactionList.Add(objProjectLocation)

        Next

        ' agregarlos a la lista
        project.projectlocationlist = objProjectLoactionList

        ' inicializar las listas
        project.sourceByProjectList = New List(Of SourceByProjectEntity)
        ' project.projectlocationlist = New List(Of ProjectLocationEntity)
        project.operatorbyprojectlist = New List(Of OperatorByProjectEntity)

    End Sub

#End Region


End Class
