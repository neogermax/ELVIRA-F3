
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ir a la pagina de login
        Response.Redirect("~/security/login.aspx")

    End Sub

End Class
