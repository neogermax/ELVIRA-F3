Option Strict On

Imports Gattaca.Application.Credentials
Imports System.Collections.Generic
Imports Gattaca.Application.ExceptionManager
Imports System.Xml
Imports Microsoft.VisualBasic
Imports Gattaca.Entity.eSecurity

Partial Class Security_applicationLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        Dim applicationCredentials As ApplicationCredentials

        'verificar si ya se cargaron los setthings
        If Application("settings") Is Nothing Then

            ' definir los objetos
            Dim doc As XmlDocument
            Dim settingDALC As New SettingDALC
            Dim superUser As Long = 1
            Dim clientName As String = ""

            ' cargar los datos del cliente
            doc = New XmlDocument()
            doc.Load(Server.MapPath("~/Include/Server/LicenseInformation.xml"))
            clientName = PublicFunction.getNode(doc, "Client").Attributes("Value").Value

            ' contruir el credentials
            applicationCredentials = PublicFunction.buildApplicationCredentials(clientName, Request, superUser, Session.SessionID)
            ViewState("applicationCredentials") = applicationCredentials

            ' obtener los parametros
            Application("settings") = settingDALC.getList(applicationCredentials)

        End If

        If HttpContext.Current.Application("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Application("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' verificar si vienen las credenciales correctas
        If Request.QueryString("ClientName") IsNot Nothing _
            AndAlso Request.QueryString("IdRelated") IsNot Nothing _
                AndAlso Request.QueryString("op") IsNot Nothing Then

            ' definir los objetos
            Dim superUser As Long = 1
            Dim clientName As String = ""
            Dim facade As New Facade
            Dim objSecurity As New Gattaca.Facade.eSecurity.eSecurity
            Dim objUser As New ApplicationUserEntity
            Dim applicationCredentials As ApplicationCredentials
            Dim userGroups As List(Of Gattaca.Entity.eSecurity.UserGroupEntity)
            Dim sGroup As String = ""
            Dim showRequest As Boolean = False

            'cargar las credenciales del usuario a la session
            applicationCredentials = PublicFunction.buildApplicationCredentials(Request.QueryString("ClientName").ToString, _
                                                                                            Request, _
                                                                                            CLng(Request.QueryString("IdRelated").ToString), _
                                                                                            Session.SessionID)
            ' cargar los datos del usuario
            objUser.ID = CInt(Request.QueryString("IdRelated").ToString)
            objUser.Name = facade.getUserName(applicationCredentials, CInt(objUser.ID))
            objUser.LastLogin = Now

            ' subirlos a session
            Session("ApplicationUserEntity") = objUser

            ' cargar los grupos del usuario
            userGroups = objSecurity.GetUserGroupsbyUser(applicationCredentials, objUser.ID)

            For Each UserGroup As Gattaca.Entity.eSecurity.UserGroupEntity In userGroups
                ' concatenar los grupos
                sGroup = sGroup & UserGroup.ID

            Next

            ' guardar la informacion del grupo
            Session("UserGroup") = "123457" 'sGroup

            ' guardar las credenciales del usuario
            Session("ApplicationCredentials") = applicationCredentials

            ' deshabilitar mostrar el menu
            Session("showMenu") = True

            ' Dim obj = objSecurity.GetUsers(applicationCredentials, "id", "1")

            ' verificar el tipo de funcionalidad. Si viene vacio, es para edicion, de lo contrario, es para mostrar simplemente
            If Request.QueryString("showRequest") IsNot Nothing Then

                ' mostrar la solicitud
                showRequest = True

            End If

            If Not showRequest Then

                Select Case Request.QueryString("op").ToString

                    Case "addIdea"

                        ' ir a crear una idea
                        Response.Redirect("~/ResearchAndDevelopment/addIdea.aspx?op=add")

                    Case "editIdea"

                        ' ir a crear una idea
                        Response.Redirect("~/ResearchAndDevelopment/addIdea.aspx?op=edit" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "endIdea"

                        ' ir a crear una idea
                        Response.Redirect("~/ResearchAndDevelopment/endIdea.aspx?" & _
                                          "IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "editProject"

                        ' ir a crear una idea
                        Response.Redirect("~/FormulationAndAdoption/addProject.aspx?op=edit" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "addProjectApprovalRecord"

                        ' ir a crear una idea
                        Response.Redirect("~/FormulationAndAdoption/addProjectApprovalRecord.aspx?op=add" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&IdEntryData=" & Request.QueryString("IDEntryData"))

                    Case "changeProjectStateToOperationalPlaning"

                        ' ir a crear una idea
                        Response.Redirect("~/FormulationAndAdoption/changeProjectState.aspx?" & _
                                          "IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&IdEntryData=" & Request.QueryString("IDEntryData"))

                    Case "changeProjectStateToExecution"

                        ' ir a crear una idea
                        Response.Redirect("~/OperationalPlanning/changeProjectState.aspx?" & _
                                          "IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&IdEntryData=" & Request.QueryString("IDEntryData"))

                    Case "addProposal"

                        ' no mostrar el menu
                        Session("showMenu") = False

                        ' ir a crear una idea
                        Response.Redirect("~/OperationalPlanning/addProposal.aspx?op=add" & _
                                          "&IdSummoning=" & Request.QueryString("IdSummoning"))

                    Case "editProposal"

                        ' ir a crear una idea
                        Response.Redirect("~/OperationalPlanning/addProposal.aspx?op=edit" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "endProposal"

                        ' ir a crear una idea
                        Response.Redirect("~/OperationalPlanning/endProposal.aspx?" & _
                                          "IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "editSubActivityInformationRegistry"

                        ' ir a crear una idea
                        Response.Redirect("~/Execution/addSubActivityInformationRegistry.aspx?op=edit" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "addContractRequest"

                        ' ir a crear una idea
                        Response.Redirect("~/Engagement/addContractRequest.aspx?op=add")


                End Select

            Else

                ' definir los objetos
                Dim EntryData As String = Request.QueryString("EntryData")

                ' solamente mostrar la solicitud
                Select Case EntryData

                    Case "IdeaEntity"

                        ' ir a crear una idea
                        Response.Redirect("~/ResearchAndDevelopment/addIdea.aspx?op=show" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                    Case "ProjectEntity"

                        ' ir a crear una idea
                        Response.Redirect("~/FormulationAndAdoption/addProject.aspx?op=show" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))
                    Case "ProposalEntity"

                        ' ir a mostrar una propuesta
                        Response.Redirect("~/OperationalPlanning/addProposal.aspx?op=show" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))
                    Case "ContractRequestEntity"

                        ' ir a mostrar una propuesta
                        Response.Redirect("~/Engagement/addContractRequest.aspx?op=show" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))
                    Case "SubActivityInformationRegistryEntity"

                        ' ir a mostrar una propuesta
                        Response.Redirect("~/Execution/addSubActivityInformationRegistry.aspx?op=show" & _
                                          "&IDProcessInstance=" & Request.QueryString("IDProcessInstance") & _
                                          "&IDActivityInstance=" & Request.QueryString("IDActivityInstance") & _
                                          "&ID=" & Request.QueryString("IDEntryData"))

                End Select

            End If

        Else

            ' ir a error
            Session("sError") = "Credenciales de Usuario Invalidas. Incidente Registrado."
            Session("sUrl") = Nothing
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        End If

    End Sub

End Class
