Imports System.Collections.Generic
Imports Gattaca.Application.Credentials


Partial Class FormulationAndAdoption_projectForumPanel
    Inherits System.Web.UI.Page

#Region "Eventos"

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

        'Definir los objetos
        Dim idForum As String
        Dim pagsize As Integer = 2
        Dim currentPag As Integer = 0
        Dim replyList As List(Of ReplyEntity)

        If Not Page.IsPostBack Then

            ' cargar el titulo
            Session("lblTitle") = "Panel del foro del proyecto"

            ' datos de la busqueda
            ViewState("field") = ""
            ViewState("value") = ""

            idForum = Request.QueryString("id")

            dlForum.DataSource = search(idForum)
            dlForum.DataBind()

            replyList = searchReply(idForum)

            If (pagsize > replyList.Count) Then
                pagsize = replyList.Count
                dlReply.ShowFooter = False
            End If

            Session("pagSize") = pagsize
            Session("currentPag") = currentPag
            dlReply.DataSource = replyList.GetRange(0, pagsize)
            dlReply.DataBind()

        End If

    End Sub

    Protected Sub dlForum_ItemCommand(ByVal sender As Object, ByVal e As DataListCommandEventArgs) Handles dlForum.ItemCommand
        If (e.CommandName.Equals("editForum")) Then
            'Editar Foro
            Response.Redirect("addForum.aspx?op=edit&id=" & e.CommandArgument)
        End If
        If (e.CommandName.Equals("addReply")) Then
            'Editar Foro
            Response.Redirect("addReply.aspx?op=add&idf=" & e.CommandArgument)
        End If
    End Sub

    Protected Sub dlReply_ItemCommand(ByVal sender As Object, ByVal e As DataListCommandEventArgs) Handles dlReply.ItemCommand
        'Definir los objetos
        Dim pagsize As Integer = Integer.Parse(Session("pagSize"))
        Dim currentPag As Integer = Integer.Parse(Session("currentPag"))
        Dim idForum As String = Request.QueryString("id")
        Dim replyList As List(Of ReplyEntity) = searchReply(idForum)
        'Paginación

        Select Case e.CommandName
            Case "next"
                currentPag += 1
                If currentPag * pagsize >= replyList.Count Then
                    currentPag -= 1
                Else
                    If (pagsize * currentPag + pagsize) >= replyList.Count Then
                        dlReply.DataSource = replyList.GetRange(pagsize * currentPag, replyList.Count - (pagsize * currentPag))
                    Else
                        dlReply.DataSource = replyList.GetRange(pagsize * currentPag, pagsize)
                    End If
                    Session("currentPag") = currentPag
                    dlReply.DataBind()
                    upReply.Update()
                End If
            Case "previous"
                currentPag -= 1
                If (currentPag) < 0 Then
                    currentPag = 0
                Else
                    dlReply.DataSource = replyList.GetRange(pagsize * currentPag, pagsize)
                    dlReply.DataBind()
                    upReply.Update()
                End If
                Session("currentPag") = currentPag
            Case Else
                'Editar Respuesta
                Response.Redirect("addReply.aspx" & e.CommandArgument)
        End Select
        
        
    End Sub

    Protected Sub dlForum_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlForum.ItemDataBound

        ' Definir los objetos
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'Comprobar que sea un item valido
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Retornar el item solicitado
            Dim enabledLabel As Label = CType(e.Item.FindControl("EnabledLabel"), Label)

            If (enabledLabel.Text = "True") Then
                enabledLabel.Text = "Habilitado"
            End If
            If (enabledLabel.Text = "False") Then
                enabledLabel.Text = "Deshabilitado"
            End If

            ' Retornar el item solicitado
            Dim hlattatchment As HyperLink = CType(e.Item.FindControl("hlattatchment"), HyperLink)
            If Not hlattatchment.Text.Equals("") Then
                hlattatchment.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "\" & hlattatchment.Text
            End If

            ' Retornar el item solicitado
            Dim btnEditForum As Button = CType(e.Item.FindControl("btnEditForum"), Button)
            Dim lblIdUser As Label = CType(e.Item.FindControl("lblIdUser"), Label)

            ' Asignar permisos de edición según usuario
            If Not lblIdUser.Text.Equals(applicationCredentials.UserID.ToString) Then
                btnEditForum.Visible = False
            End If

        End If

    End Sub

    Protected Sub dlReply_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlReply.ItemDataBound

        ' Definir los objetos
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        If e.Item.ItemType = ListItemType.Item Or _
             e.Item.ItemType = ListItemType.AlternatingItem Then


            ' Retrieve the Label control in the current DataListItem.
            Dim btnEditReply As Button = _
                CType(e.Item.FindControl("btnEditReply"), Button)

            ' Retornar el item solicitado
            Dim hlreplyattatchment As HyperLink = CType(e.Item.FindControl("hlreplyattatchment"), HyperLink)
            If Not hlreplyattatchment.Text.Equals("") Then
                hlreplyattatchment.NavigateUrl = PublicFunction.getSettingValue("documentPath") & "\" & hlreplyattatchment.Text
            End If

            ' Asignar permisos de edición según usuario
            If Not btnEditReply.CommandName.Equals(applicationCredentials.UserID.ToString) Then
                btnEditReply.Visible = False
            End If

        End If

    End Sub

#End Region


#Region "Metodos"

    Public Function search(ByVal idForum As String) As List(Of ForumEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        search = New List(Of ForumEntity)

        Try
            ' buscar
            search = facade.getForumList(applicationCredentials, id:=idForum.ToString)

        Catch ex As Exception
            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar los objetos
            facade = Nothing
            ID = Nothing
        End Try

    End Function

    Public Function searchReply(ByVal idForum As Integer) As List(Of ReplyEntity)

        ' definirlos objetos
        Dim facade As New Facade
        Dim id As String = ""
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        searchReply = New List(Of ReplyEntity)

        Try
            ' buscar
            searchReply = facade.getReplyList(applicationCredentials, idforum:=idForum, order:="updatedate DESC")

        Catch ex As Exception
            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally
            ' liberar los objetos
            facade = Nothing
            id = Nothing
        End Try

    End Function

#End Region

End Class
