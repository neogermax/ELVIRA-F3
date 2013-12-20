
Partial Class NewMenu_Menu
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            'Page.Theme = "GattacaAdmin"

        End If

        Me.hfUGr.Value = Session("mMenu")

        Session("mMenu") = Replace(Me.hfUGr.Value, "_", "")

    End Sub
End Class
