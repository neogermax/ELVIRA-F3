Option Strict On

Imports Gattaca.Application.Credentials
Imports System.Collections.Generic
Imports Gattaca.Application.ExceptionManager
Imports System.Xml

Partial Class Security_login
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'verificar si ya se cargaron los setthings
        If Application("settings") Is Nothing Then

            ' definir los objetos
            Dim doc As XmlDocument
            Dim settingDALC As New SettingDALC
            Dim applicationCredentials As ApplicationCredentials
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

        ' quemar el tema por defecto
        Me.Page.Theme = "GattacaAdmin"

    End Sub

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogIn.Click

        ' definir los objetos
        Dim applicationCredentials As ApplicationCredentials
        Dim superUser As Long = 1
        Dim clientName As String = Me.ddlClient.SelectedValue
        Dim LOGON_USER As String = Me.txtUser.Text
        Dim facade As New SecurityFacade

        Try

            ' contruir el credentials
            applicationCredentials = PublicFunction.buildApplicationCredentials(clientName, Request, superUser, Session.SessionID)

            ' hacer login
            facade.doLogin(applicationCredentials, LOGON_USER, Me.txtPw.Text, _
                            Gattaca.Entity.eSecurity.AuthenticationMode.SecurityOnly, Session, Request)

            ' habilitar elmenu
            Session("showMenu") = True

            ' ir a la pagina
            Response.Redirect("~/NewMenu/Menu.aspx")


        Catch oEx As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            'mostrando el error
            Session("sError") = ex.Message
            Session("sUrl") = "" 'Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            applicationCredentials = Nothing
            superUser = Nothing
            clientName = Nothing
            LOGON_USER = Nothing

        End Try

    End Sub

#End Region

End Class
