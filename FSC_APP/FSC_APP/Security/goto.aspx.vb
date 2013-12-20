Option Strict On
Option Explicit On

Imports Gattaca.Application.Credentials

Partial Class Process_gotoWorkFlow
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' verificar que este vigente la session
        If Session Is Nothing Or Session.Count = 0 Then

            ' ir a pagina logout
            Response.Redirect("~/security/LogOut.aspx")
            Response.End()

        End If

        Dim appCred As ApplicationCredentials = CType(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim url As String = ""
        Dim op As String = Request.QueryString("op")

        ' cargar los parametros del usuario y el cliente
        url = "?IdUser=" & appCred.UserID.ToString & "&op=" & op & "&Client=" & appCred.ClientName

        Select Case op

            Case "ADMIN"
                url = PublicFunction.getSettingValue("BPM.Login") & url

            Case "SEC"
                url = PublicFunction.getSettingValue("SECURITY.Login") & url

            Case "TL"
                url = PublicFunction.getSettingValue("BPM.Login") & url

        End Select

        ' ir a la pagina
        Response.Redirect(url)

    End Sub

End Class
