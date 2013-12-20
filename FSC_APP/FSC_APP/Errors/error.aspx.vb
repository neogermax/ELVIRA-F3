Option Strict On

Partial Class Errors_error
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

        If Session("sError") IsNot Nothing Then

            'mostrando el error
            Me.lblMessage.Text = CStr(Session("sError"))

        End If

    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ' variable
        Dim url As String = CStr(Session("sUrl"))

        'verificar si viene vacia
        If url Is Nothing Or url.Equals(String.Empty) Then

            ' ir a la pagina origen
            url = "~/security/login.aspx"

        End If

        ' ir a la pagina
        Response.Redirect(url)

    End Sub

End Class
