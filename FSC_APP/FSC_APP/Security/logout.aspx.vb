Option Strict On

Imports Gattaca.Application.Credentials

Partial Class Security_logout
    Inherits System.Web.UI.Page

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

            ' verificar que este vigente la session
            'If Session("ApplicationCredentials") IsNot Nothing Then
            If Session.Count <> 0 Then

                ' definir los objetos
                Dim objSecurity As New Gattaca.Facade.eSecurity.eSecurity

                Try
                    'TODO: FIX
                    ' verificar las credenciales del usuario
                    'objSecurity.UserLogOut(CType(Session("ApplicationCredentials"), ApplicationCredentials), _
                    '                        CType(Session("ApplicationCredentials"), ApplicationCredentials).UserID)

                    ' cerrar la sesion
                    Session("ApplicationCredentials") = Nothing
                    Session.Abandon()

                Catch oEx As Threading.ThreadAbortException
                    ' no hacer nada

                Catch ex As Exception
                    'mostrando el error
                    Session("serror") = ex.Message
                    Response.Redirect("~/errors/error.aspx")
                    Response.End()

                Finally
                    ' liberar recursos
                    objSecurity = Nothing

                End Try

            End If

        End If

    End Sub

    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOut.Click
        ' ir al inicio
        Response.Redirect("~/security/login.aspx")

    End Sub

End Class
