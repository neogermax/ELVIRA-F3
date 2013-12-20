
Imports Microsoft.VisualBasic
Imports Gattaca.Application.ExceptionManager
Imports Gattaca.Entity.eSecurity

Public Class SecurityFacade

    ' defini el nombre
    Const MODULENAME As String = "SecurityFacade"

    'Public Sub doLogin(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                   ByVal User As String, _
    '                   ByVal Pw As String, _
    '                    ByVal AuthenticationMode As Gattaca.Entity.eSecurity.AuthenticationMode, _
    '                    ByVal session As System.Web.SessionState.HttpSessionState, _
    '                    ByVal request As System.Web.HttpRequest)

    '    ' definir los objetos
    '    Dim objSecurity As New Gattaca.Facade.eSecurity.eSecurity
    '    Dim objUser As New ApplicationUserEntity
    '    Dim superUser As Long = 1
    '    Dim menu As String = ""
    '    Dim SessionID As String = ""
    '    Dim userGroups As List(Of Gattaca.Entity.eSecurity.UserGroupEntity)
    '    Dim sGroup As String = ""

    '    Try
    '        ' autenticar con el directorio activo
    '        'objUser = objSecurity.UserLogin(objApplicationCredentials, User, Pw, AuthenticationMode)
    '        objUser.ID = 3
    '        objUser.Name = "Norma Sanchez"
    '        objUser.LastLogin = Now


    '        'cargar las credenciales del usuario a la session
    '        session("ApplicationCredentials") = PublicFunction.buildApplicationCredentials(objApplicationCredentials.ClientName, _
    '                                                                                            request, objUser.ID, session.SessionID)
    '        session("ApplicationUserEntity") = objUser

    '        ' cargar el titulo
    '        session("lblTitle") = PublicFunction.getSettingValue("defaultTitle")

    '        ' cargar los grupos del usuario
    '        'userGroups = objSecurity.GetUserGroupsbyUser(objApplicationCredentials, objUser.ID)

    '        'For Each UserGroup As Gattaca.Entity.eSecurity.UserGroupEntity In userGroups
    '        '    ' concatenar los grupos
    '        '    sGroup = sGroup & UserGroup.ID

    '        'Next

    '        ' habilitar mostrar el menu
    '        session("showMenu") = True

    '        ' guardar la informacion del grupo
    '        session("UserGroup") = "123457" 'sGroup

    '    Catch ex As Exception

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, "", MODULENAME, "doLogin")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        'mostrando el error
    '        Throw ex

    '    Finally

    '        ' liberar recursos
    '        objSecurity = Nothing
    '        objUser = Nothing
    '        superUser = Nothing
    '        menu = Nothing

    '    End Try

    'End Sub

    Public Sub doLogin(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                   ByVal User As String, _
                   ByVal Pw As String, _
                    ByVal AuthenticationMode As Gattaca.Entity.eSecurity.AuthenticationMode, _
                    ByVal session As System.Web.SessionState.HttpSessionState, _
                    ByVal request As System.Web.HttpRequest)

        ' definir los objetos
        Dim objSecurity As New Gattaca.Facade.eSecurity.eSecurity
        Dim objUser As ApplicationUserEntity
        Dim superUser As Long = 1
        Dim menu As String = ""
        Dim SessionID As String = ""
        Dim sGroup As String = ""
        Dim facade As New Facade
        Dim objUserGroupDALC As UserGroupDALC = New UserGroupDALC()
        Dim UserByGroupList As New List(Of UsersByGroupEntity)

        Try
            ' autenticar con el directorio activo
            objUser = objSecurity.UserLogin(objApplicationCredentials, User, Pw, AuthenticationMode)

            'cargar las credenciales del usuario a la session
            session("ApplicationCredentials") = PublicFunction.buildApplicationCredentials(objApplicationCredentials.ClientName, _
                                                                                                request, objUser.ID, session.SessionID)
            session("ApplicationUserEntity") = objUser

            ' cargar el titulo
            session("lblTitle") = PublicFunction.getSettingValue("defaultTitle")

            ' obteniendo la lista de los usuarios
            UserByGroupList = objUserGroupDALC.getUserGroups(objApplicationCredentials)

            For Each userGroup As UsersByGroupEntity In UserByGroupList
                ' concatenar los grupos
                sGroup = sGroup & "_" & userGroup.code

            Next

            ' cargar el menu del usuario
            session("mMenu") = sGroup

        Catch ex As Exception

            ' publicar el error
            GattacaApplication.Publish(ex, "", MODULENAME, "doLogin")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'mostrando el error
            Throw ex

        Finally

            ' liberar recursos
            objSecurity = Nothing
            objUser = Nothing
            superUser = Nothing
            menu = Nothing

        End Try

    End Sub

End Class
