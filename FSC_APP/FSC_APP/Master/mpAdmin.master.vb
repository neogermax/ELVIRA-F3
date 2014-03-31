Option Strict On

Partial Class Master_mpAdmin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        ' definir los objetos
        Dim tag As HtmlMeta = New HtmlMeta()

        ' tag para refrescar la pagina
        tag.HttpEquiv = "REFRESH"
        tag.Name = "SessionTimeout"
        tag.Content = (Session.Timeout * 120) - 5 & "; URL=" & ResolveUrl("~/Security/logout.aspx")

        ' agregarlo
        Page.Header.Controls.Add(tag)

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


      
        ' verificar que este vigente la session
        If Session Is Nothing Or Session.Count = 0 Then

            ' ir a pagina logout
            Response.Redirect("../security/LogOut.aspx")
            Response.End()

        End If

        'Ocultar div
        Me.divMenu.Visible = False

        If Session("pretty") IsNot Nothing Then


            Dim validar As String = Session("pretty").ToString

            If validar = "1" Then
                Me.ucUserData.Visible = False
                Session("pretty") = 0
            Else
                Me.ucUserData.Visible = True

            End If


        End If

        

        ' 
        If Session("lblTitle") IsNot Nothing Then

            ' cargar el titulo
            Me.lblTitle.Text = Session("lblTitle").ToString
            Me.Page.Title = Session("lblTitle").ToString

        End If

        If Not Page.IsPostBack Then


            ' desabilitar mostrar el menu
            If CBool(Session("showMenu")) Then

                ' cargar la ruta
                'Dim path As String = "~/Menu/mMenu123457.xml"
                Dim path As String = "~/Menu/mMenu" & Session("mMenu").ToString & ".xml"

                'indicarle al proveedor cual es el archivo xml del menu
                Me.xdsMenu.DataFile = Server.MapPath(path)
                Me.xdsMenu.XPath = "/*/*"

            End If

        End If

        ' aumentar el tiempo de espera de respuesta al lado del cliente
        Me.smPrincipal.AsyncPostBackTimeout = 1200

        ' cargar el scrip de refrescar la pantalla
        ScriptManager.RegisterStartupScript(Page, Me.GetType, "imageWait", GattacaApplication.getWaitImageScript, False)

    End Sub


End Class

