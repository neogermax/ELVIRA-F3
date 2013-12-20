
Partial Class Master_mpError
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' verificar que este vigente la session
        If Session Is Nothing Or Session.Count = 0 Then

            ' ir a pagina logout
            Response.Redirect("../security/LogOut.aspx")
            Response.End()

        End If

    End Sub

End Class

