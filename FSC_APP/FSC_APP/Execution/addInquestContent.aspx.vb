Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addInquestContent
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve una lista de objetos de tipo QuestionsByInquestContentEntity
    ''' </summary>
    ''' <value>una lista de objetos de tipo QuestionsByInquestContentEntity</value>
    ''' <returns>una lista de objetos de tipo QuestionsByInquestContentEntity</returns>
    ''' <remarks></remarks>
    Private Property QuestionsList() As List(Of QuestionsByInquestContentEntity)
        Get
            Return DirectCast(Session("questionsList"), List(Of QuestionsByInquestContentEntity))
        End Get
        Set(ByVal value As List(Of QuestionsByInquestContentEntity))
            Session("questionsList") = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna o devuelve el valor del indice de la pregunta a la cual
    ''' se le desean asignar las respectivas respuestas.
    ''' </summary>
    ''' <value>indice que se desea asignar</value>
    ''' <returns>indice asignado</returns>
    ''' <remarks></remarks>
    Private Property IndexSelectedQuestion() As Integer
        Get
            Return DirectCast(Session("indexSelectedQuestion"), Integer)
        End Get
        Set(ByVal value As Integer)
            Session("indexSelectedQuestion") = value
        End Set
    End Property

#End Region

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

            ' cargar los combos
            Me.loadCombos()

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UN NUEVO CONTENIDO DE ENCUESTA."

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

                    ' crear la lista de preguntas
                    Me.QuestionsList = New List(Of QuestionsByInquestContentEntity)

                Case "edit"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UN CONTENIIDO DE ENCUESTA."

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
                    Dim objInquestContent As New InquestContentEntity

                    Try
                        ' cargar el registro referenciado
                        objInquestContent = facade.loadInquestContent(applicationCredentials, Request.QueryString("id"))

                        'Se pobla la lista de preguntas de la encuesta actual.
                        Me.QuestionsList = objInquestContent.QUESTIONSLIST
                        Me.gvQuestions.DataSource = objInquestContent.QUESTIONSLIST
                        Me.gvQuestions.DataBind()

                        ' mostrar los valores
                        Me.txtid.Text = objInquestContent.id
                        Me.ddlInquest.SelectedValue = objInquestContent.idinquest
                        Me.txtcode.Text = objInquestContent.code
                        Me.ddlenabled.SelectedValue = objInquestContent.enabled
                        Me.txtiduser.Text = objInquestContent.USERNAME
                        Me.txtcreatedate.Text = objInquestContent.createdate

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objInquestContent = Nothing

                    End Try

            End Select

            'Se selecciona la pestaña inicial
            Me.TabContainer1.ActiveTabIndex = 0

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        If Page.IsValid Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objInquestContent As New InquestContentEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Try
                ' cargar los valores registrados por el usuario
                objInquestContent.idinquest = Me.ddlInquest.SelectedValue
                objInquestContent.code = Me.txtcode.Text
                objInquestContent.enabled = Me.ddlenabled.SelectedValue
                objInquestContent.iduser = applicationCredentials.UserID
                objInquestContent.createdate = Now()

                'Se almacena la lista de preguntas de la encuesta actual.
                objInquestContent.QUESTIONSLIST = Me.QuestionsList

                ' almacenar la entidad
                objInquestContent.id = facade.addInquestContent(applicationCredentials, objInquestContent)

                ' ir al administrador
                Response.Redirect("searchInquestContent.aspx")

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
                objInquestContent = Nothing
                facade = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        ' ir al administrador
        Response.Redirect("searchInquestContent.aspx")

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Page.IsValid Then

            ' definir los objetos
            Dim facade As New Facade
            Dim objInquestContent As New InquestContentEntity
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' cargar el registro referenciado
            objInquestContent = facade.loadInquestContent(applicationCredentials, Request.QueryString("id"))

            Try
                ' cargar los datos
                objInquestContent.idinquest = Me.ddlInquest.SelectedValue
                objInquestContent.code = Me.txtcode.Text
                objInquestContent.enabled = Me.ddlenabled.SelectedValue

                'Se almacena la lista de preguntas de la encuesta actual.
                objInquestContent.QUESTIONSLIST = Me.QuestionsList

                ' modificar el registro
                facade.updateInquestContent(applicationCredentials, objInquestContent)

                ' ir al administrador
                Response.Redirect("searchInquestContent.aspx")

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
                objInquestContent = Nothing

            End Try

        End If

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteInquestContent(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchInquestContent.aspx")

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

    Protected Sub cvCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvCode.ServerValidate
        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            'Se veirifica el tipo de documento
            If facade.verifyInquestContentCode(applicationCredentials, args.Value, Me.txtid.Text) Then
                args.IsValid = False
                Me.txtcode.Focus()
            Else
                args.IsValid = True
                Me.ddlenabled.Focus()
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

    Protected Sub btnAddQuestion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddQuestion.Click

        ' definir los objetos
        Dim questionsList As List(Of QuestionsByInquestContentEntity)
        Dim question As New QuestionsByInquestContentEntity

        ' cargarla de la session
        If (Me.QuestionsList Is Nothing) Then Me.QuestionsList = New List(Of QuestionsByInquestContentEntity)
        questionsList = Me.QuestionsList

        'Se pobla la información de la pregunta
        question.questiontext = Me.txtQuestionText.Text
        question.questiontype = Me.ddlQuestionType.SelectedValue

        If Not (questionsList.Exists(Function(aQuestion) aQuestion.questiontext = question.questiontext AndAlso _
            aQuestion.questiontype = question.questiontype)) Then

            ' agregarlos
            questionsList.Add(question)

        End If

        ' mostrar
        Me.gvQuestions.DataSource = questionsList
        Me.gvQuestions.DataBind()

        'Se llama al método que permite limpiar los controles de la pestaña de respuestas
        Me.ClearAnswersControls()

        'Se limpia y enfoca la caja de texto.
        Me.txtQuestionText.Text = ""
        Me.txtQuestionText.Focus()

    End Sub

    Protected Sub gvQuestions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvQuestions.SelectedIndexChanged

        ' definir los objetos
        Dim questionsList As List(Of QuestionsByInquestContentEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        questionsList = Me.QuestionsList

        ' remover el seleccionado
        questionsList.RemoveAt(Me.gvQuestions.SelectedIndex)

        ' mostrar
        Me.gvQuestions.DataSource = questionsList
        Me.gvQuestions.DataBind()

        'Se llama al método que permite limpiar los controles de la pestaña de respuestas
        Me.ClearAnswersControls()

        'Se activa la pestaña actual
        Me.TabContainer1.ActiveTabIndex = 1

    End Sub

    Protected Sub gvQuestions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuestions.RowDataBound

        Dim objLinkButton As LinkButton
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim miEntidad As QuestionsByInquestContentEntity = e.Row.DataItem
            objLinkButton = e.Row.Cells(3).Controls(1)
            If (miEntidad.questiontype.Equals("Respuesta Texto")) Then
                objLinkButton.Enabled = False
            End If
        End If

    End Sub

    Protected Sub btnAddAnswer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAnswer.Click

        ' definir los objetos
        Dim answer As New AnswersByQuestionEntity

        ' cargarla de la session
        If (Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST Is Nothing) Then
            Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST = New List(Of AnswersByQuestionEntity)
        End If

        'Se pobla la información de la pregunta
        answer.answer = Me.txtAnswerText.Text

        If Not (Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST.Exists(Function(aAnswer) aAnswer.answer = answer.answer)) Then
            ' agregarlos
            Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST.Add(answer)
        End If

        ' mostrar
        Me.gvAnswers.DataSource = Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST
        Me.gvAnswers.DataBind()

        'Se limpia y enfoca la caja de texto.
        Me.txtAnswerText.Text = ""
        Me.txtAnswerText.Focus()

    End Sub

    Protected Sub gvAnswers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAnswers.SelectedIndexChanged

        ' remover el seleccionado
        Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST.RemoveAt(Me.gvAnswers.SelectedIndex)

        ' mostrar
        Me.gvAnswers.DataSource = Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST
        Me.gvAnswers.DataBind()

    End Sub

    Protected Sub lnkAnswers_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Se almacena el indice de la fila seleccionada
        Dim lnkButton As LinkButton = DirectCast(sender, LinkButton)
        Dim fieldCell As DataControlFieldCell = lnkButton.Parent
        Dim gridViewRow As GridViewRow = fieldCell.Parent
        Me.IndexSelectedQuestion = gridViewRow.DataItemIndex

        'Se selecciona la pestaña de las preguntas y se activa la tabla que contiene la información.
        Me.TabContainer1.ActiveTabIndex = 2
        Me.tableAnswers.Enabled = True

        'Se asigna el texto de la pregunta seleccionada
        Me.txtSelectedQuestion.Text = DirectCast(gridViewRow.Cells(1).Controls(0), DataBoundLiteralControl).Text.Trim()

        'Se actualiza la grilla con las respuestas de la pregunta seleccionada
        If Not (Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST Is Nothing) Then
            Me.gvAnswers.DataSource = Me.QuestionsList(Me.IndexSelectedQuestion).ANSWERSLIST
        Else
            'Se limpia la grilla
            Me.gvAnswers.DataSource = Nothing
        End If

        Me.gvAnswers.DataBind()

        'Se enfoca el control de la respuesta
        Me.txtAnswerText.Focus()

    End Sub

#End Region

#Region "Metodos"

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

            'Se llama al metodo que permite cargar el combo de encuestas
            If (Request.QueryString("op").Equals("add")) Then
                Me.ddlInquest.DataSource = facade.getInquestList(applicationCredentials, enabled:="1", order:="Code")
            Else
                Me.ddlInquest.DataSource = facade.getInquestList(applicationCredentials, order:="Code")
            End If

            Me.ddlInquest.DataValueField = "Id"
            Me.ddlInquest.DataTextField = "Code"
            Me.ddlInquest.DataBind()

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

    ''' <summary>
    ''' Permite limpiar los controles de la pestaña de respuestas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAnswersControls()

        Me.txtSelectedQuestion.Text = ""
        Me.txtAnswerText.Text = ""
        Me.gvAnswers.DataSource = Nothing
        Me.gvAnswers.DataBind()
        Me.tableAnswers.Enabled = False

    End Sub

#End Region

End Class
