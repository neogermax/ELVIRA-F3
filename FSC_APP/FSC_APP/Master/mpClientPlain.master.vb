
Partial Class Master_mpClientPlain
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        ' definir los objetos
        Dim tag As HtmlMeta = New HtmlMeta()

        ' tag para refrescar la pagina
        tag.HttpEquiv = "REFRESH"
        tag.Name = "SessionTimeout"
        tag.Content = (Session.Timeout * 60) - 5 & "; URL=" & ResolveUrl("~/Security/logout.aspx")

        ' agregarlo
        Page.Header.Controls.Add(tag)

    End Sub

End Class

