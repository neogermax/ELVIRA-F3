Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addSummoning
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

        If Not Page.IsPostBack Then

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            loadCombos()
            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVA CONVOCATORIA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Visible = False
                    Me.txtid.Visible = False
                    Me.lblcreatedate.Visible = False
                    Me.txtcreatedate.Visible = False
                    Me.lbliduser.Visible = False
                    Me.txtiduser.Visible = False
                    
                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA CONVOCATORIA"

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = True
                    Me.btnDelete.Visible = True
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False
                    Me.lblid.Enabled = False
                    Me.txtid.Enabled = False
                    Me.lblcreatedate.Enabled = False
                    Me.txtcreatedate.Enabled = False
                    Me.lbliduser.Enabled = False
                    Me.txtiduser.Enabled = False
                    ' definir los objetos
                    Dim facade As New Facade
					Dim objSummoning As New SummoningEntity

                    Try
                        ' cargar el registro referenciado
                        objSummoning = facade.loadSummoning(applicationCredentials, Request.QueryString("id"))

                        ' mostrar los valores
                        Me.txtid.Text = objSummoning.id
                        Me.txtcode.Text = objSummoning.code
                        Me.txtname.Text = objSummoning.name
                        Me.txtdescription.Text = objSummoning.description
                        Me.ddlidproject.SelectedValue = objSummoning.idproject
                        Me.txtbegindate.Text = objSummoning.begindate.ToString("yyyy/MM/dd")
                        Me.txtenddate.Text = objSummoning.enddate.ToString("yyyy/MM/dd")
                        Me.ddlenabled.SelectedValue = objSummoning.enabled
                        Me.txtiduser.Text = objSummoning.USERNAME
                        Me.txtcreatedate.Text = objSummoning.createdate.ToString("yyyy/MM/dd")

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objSummoning = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
		Dim facade As New Facade
        Dim objSummoning As New SummoningEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los valores registrados por el usuario
            objSummoning.code = Me.txtcode.Text
            objSummoning.name = Me.txtname.Text
            objSummoning.description = Me.txtdescription.Text
            objSummoning.idproject = Me.ddlidproject.SelectedValue
            objSummoning.begindate = Me.txtbegindate.Text
            objSummoning.enddate = Me.txtenddate.Text
            objSummoning.enabled = Me.ddlenabled.SelectedValue
            objSummoning.iduser = applicationCredentials.UserID
            objSummoning.createdate = Date.Now

            ' almacenar la entidad
            objSummoning.id = facade.addSummoning(applicationCredentials, objSummoning)


            ' mensaje de confirmación
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Confirmación", "<script type='text/javascript'> alert('La convocatoria se guardo con éxito'); </script>", False)
            Alert("La convocatoria se guardo con éxito", Me)
            ' ir al administrador
            '  Response.Redirect("searchSummoning.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            objSummoning = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchSummoning.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
		Dim objSummoning As New SummoningEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        'se cargo el archivo referenciado
        objSummoning = facade.loadSummoning(applicationCredentials, Request.QueryString("Id"))
        Try
            ' cargar los datos
           
            objSummoning.code = Me.txtcode.Text
            objSummoning.name = Me.txtname.Text
            objSummoning.description = Me.txtdescription.Text
            objSummoning.idproject = Me.ddlidproject.SelectedValue
            objSummoning.begindate = Me.txtbegindate.Text
            objSummoning.enddate = Me.txtenddate.Text
            objSummoning.enabled = Me.ddlenabled.SelectedValue
          

            ' modificar el registro
           facade.updateSummoning(applicationCredentials, objSummoning)

            ' ir al administrador
            Response.Redirect("searchSummoning.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
			facade = Nothing
            objSummoning = Nothing

        End Try

    End Sub
    
    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteSummoning(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchSummoning.aspx")

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub

    
   Protected Sub btnCancelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelDelete.Click

        ' ocultar algunos botones
        Me.btnAddData.Visible = False
        Me.btnSave.Visible = True
        Me.btnDelete.Visible = True
        Me.btnCancelDelete.Visible = False
        Me.btnConfirmDelete.Visible = False
        Me.lblDelete.Visible = False
        Me.btnCancel.Visible = True

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' ocultar algunos botones
        Me.btnSave.Visible = False
        Me.btnDelete.Visible = False
        Me.btnConfirmDelete.Visible = True
        Me.btnCancel.Visible = False
        Me.btnCancelDelete.Visible = True
        Me.lblDelete.Visible = True

    End Sub
    Protected Sub txtcode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcode.TextChanged
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' obtener los parametos
        Dim op As String = Request.QueryString("op")

        Try

            If facade.verifySummoningCode(applicationCredentials, Me.txtcode.Text, Me.txtid.Text) Then
                lblHelpcode.Text = "Este número ya existe, por favor cámbielo"
                rfvcode.IsValid = 0
            Else
                lblHelpcode.Text = ""
                rfvcode.IsValid = 1
            End If

        Catch oex As Threading.ThreadAbortException
            ' no hacer nada

        Catch ex As Exception

            ' ir a error
            Session("sError") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub
#End Region

#Region "Metodos"
    Public Sub Alert(ByVal msg As String, ByRef P As Page)
        Dim strScript As String
        strScript = "<script language=javascript> alert('" + msg + ".');  location.href='searchSummoning.aspx';</script>"
        P.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", strScript)
    End Sub
  




    ''' <summary>
    ''' Cargar los datos de las listas
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks></remarks>
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar la lista de los tipos
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", enabled:="1", order:="Code")
            Else
                Me.ddlidproject.DataSource = facade.getProjectList(applicationCredentials, isLastVersion:="1", order:="Code")
            End If
            Me.ddlidproject.DataValueField = "IdKey"
            Me.ddlidproject.DataTextField = "Code"
            Me.ddlidproject.DataBind()

        Catch ex As Exception

            'mostrando el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        Finally

            ' liberar recursos
            facade = Nothing

        End Try

    End Sub
#End Region

   
End Class
