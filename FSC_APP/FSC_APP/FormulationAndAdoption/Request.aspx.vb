''' <summary>
''' TODO: Form Request create by Juan Camilo Martinez Morales
''' Date: 14/05/2014
''' </summary>
''' <remarks></remarks>
Partial Public Class Request
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
    ''' <summary>
    ''' Event Page Load for this page
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">Event</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("idproject") <> Nothing Then

            End If
        Catch ex As Exception
            Response.Write("<script type='text/javascript'>alert('Ocurrio un error inesperado!');</script>")
        End Try
    End Sub

End Class