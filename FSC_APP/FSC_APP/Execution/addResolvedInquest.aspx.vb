Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials

Partial Class addResolvedInquest
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Asigna o devuelve el valor del identificador del contenido de encuesta actual.
    ''' </summary>
    ''' <value>identificador del contenido de encuesta actual</value>
    ''' <returns>identificador del contenido de encuesta actual</returns>
    ''' <remarks></remarks>
    Private Property IdInquestContent() As Integer
        Get
            Return DirectCast(ViewState("IdInquestContent"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdInquestContent") = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna o devuelve el valor de la URL desde donde es llamada esta página
    ''' </summary>
    ''' <value>url origen</value>
    ''' <returns>url origen</returns>
    ''' <remarks></remarks>
    Private Property UrlOrigen() As String
        Get
            Return DirectCast(ViewState("urlOrigen"), String)
        End Get
        Set(ByVal value As String)
            ViewState("urlOrigen") = value
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

        'Se recupera el id del contenido de encuesta requerido.
        Me.IdInquestContent = Request.QueryString("idInquestContent")

        'Se llama al metodo que permite cargar dinamicamente la encuesta requerida.
        Me.CreateInquest()

        If Not Page.IsPostBack Then

            'Se almacena la url origen desde donde es llamada esta página
            Me.UrlOrigen = Request.UrlReferrer.AbsolutePath

            ' obtener los parametos
            Dim op As String = Request.QueryString("op")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            ' de acuerdo a la opcion
            Select Case op

                Case "add"

                    ' cargar el titulo
                    Session("lblTitle") = "AGREGAR UNA NUEVA ENCUESTA RESUELTA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = True
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False

                Case "view"

                    ' cargar el titulo
                    Session("lblTitle") = "EDITAR UNA ENCUESTA RESUELTA."

                    ' ocultar algunos botones
                    Me.btnAddData.Visible = False
                    Me.btnSave.Visible = False
                    Me.btnDelete.Visible = False
                    Me.btnCancelDelete.Visible = False
                    Me.btnConfirmDelete.Visible = False
                    Me.lblDelete.Visible = False

                    ' definir los objetos
                    Dim facade As New Facade
                    Dim objResolvedInquest As New ResolvedInquestEntity

                    Try
                        ' cargar el registro referenciado
                        objResolvedInquest = facade.loadResolvedInquest(applicationCredentials, Request.QueryString("id"))

                        'Se llama al metodo que permite cargar los resultados de la encuesta actual.
                        Me.loadInquest(objResolvedInquest)

                    Catch ex As Exception

                        ' ir a error
                        Session("sError") = ex.Message
                        Session("sUrl") = Request.UrlReferrer.PathAndQuery
                        Response.Redirect("~/errors/error.aspx")
                        Response.End()

                    Finally

                        ' liberar recursos
                        facade = Nothing
                        objResolvedInquest = Nothing

                    End Try

            End Select

        End If

    End Sub

    Protected Sub btnAddData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddData.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objResolvedInquest As New ResolvedInquestEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim myCounter As Integer = 1

        Try

            'Se declaran los objetos requeridos
            Dim myTextBox As New TextBox()
            Dim myRadioButtonList As New RadioButtonList()
            objResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST = New List(Of AnswersByResolvedInquestEntity)

            'Se recorren las respuestas registradas
            For Each row As TableRow In Me.tbQuestions.Rows

                'Se crea un objeto de tipo AnswersByResolvedInquestEntity
                Dim answersByResolvedInquest As New AnswersByResolvedInquestEntity()

                'Se verifica si la fila es par. (en las filas pares se encuentran las repsuestas)
                If (myCounter Mod 2 = 0) Then

                    'Se verifica si el control de la fila actual es una caja de texto
                    If (myTextBox.GetType.ToString() = row.Cells(1).Controls(0).GetType().ToString()) Then

                        'Se recupera el control de la segunda celda
                        myTextBox = row.Cells(1).Controls(0)

                        'Se pobla el objeto de tipo respuesta por encuesta resuelta
                        answersByResolvedInquest.idquestionsbyinquestcontent = myTextBox.ID
                        answersByResolvedInquest.answertext = myTextBox.Text

                    Else

                        'Se captura el control de tipo radioButtonList
                        myRadioButtonList = row.Cells(1).Controls(0)

                        'Se pobla el objeto de tipo respuesta por encuesta resuelta
                        answersByResolvedInquest.idquestionsbyinquestcontent = myRadioButtonList.ID
                        answersByResolvedInquest.idanswersbyquestion = myRadioButtonList.SelectedValue

                    End If

                    'Se agrega la respuesta a la lista de respuestas
                    objResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST.Add(answersByResolvedInquest)
                Else

                    'Se verifica si la fila actual tiene mas de una celda
                    If (row.Cells.Count > 1) Then

                        'Se verifica si la caja de texto actual es la caja de texto de los comentarios
                        If (row.Cells(1).Controls(0).ID = "txtComments") Then
                            'Se captura el control de tipo radioButtonList
                            myTextBox = row.Cells(1).Controls(0)

                            'Se almacenan los comentarios digitados
                            objResolvedInquest.comments = myTextBox.Text
                        End If

                    End If

                End If

                'Se incrementa el contador de filas
                myCounter += 1

            Next

            ' cargar los valores registrados por el usuario
            objResolvedInquest.idinquestcontent = Me.IdInquestContent
            objResolvedInquest.iduser = applicationCredentials.UserID
            objResolvedInquest.createdate = Now()

            ' almacenar la entidad
            objResolvedInquest.id = facade.addResolvedInquest(applicationCredentials, objResolvedInquest)

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
            objResolvedInquest = Nothing
            facade = Nothing

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If (Me.UrlOrigen.IndexOf("searchResolvedInquest") >= 0) Then

            ' ir al administrador
            Response.Redirect("searchResolvedInquest.aspx")

        Else

            ' ir al administrador
            Response.Redirect("searchInquestContent.aspx")

        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim objResolvedInquest As New ResolvedInquestEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' cargar los datos
            objResolvedInquest.idinquestcontent = ""
            objResolvedInquest.comments = "" 'Me.txtcomments.Text

            ' modificar el registro
            facade.updateResolvedInquest(applicationCredentials, objResolvedInquest)

            ' ir al administrador
            Response.Redirect("searchResolvedInquest.aspx")

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
            objResolvedInquest = Nothing

        End Try

    End Sub

    Protected Sub btnConfirmDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmDelete.Click

        ' definir los objetos
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            ' eliminar el registro
            facade.deleteResolvedInquest(applicationCredentials, Request.QueryString("Id"))

            ' ir al administrador
            Response.Redirect("searchResolvedInquest.aspx")

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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Permite crear dinamicamente una encuesta determinada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateInquest()

        'Declaración de objetos
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objInquestContent As New InquestContentEntity
        Dim facade As New Facade
        Dim myCounter As Integer = 1

        Try

            'Se carga el bojeto de tipo contenido encuesta
            objInquestContent = facade.loadInquestContent(applicationCredentials, Me.IdInquestContent)

            'Se verifica el estado de la encuesta actual
            If (objInquestContent.INQUESTENABLED) Then

                'Se verifica el estado del contenido de la encuesta
                If (objInquestContent.enabled) Then

                    'Se verifica que la encuesta cotenga lamenos una pregunta
                    If (objInquestContent.QUESTIONSLIST.Count > 0) Then

                        'Se coloca el titulo de la encuesta
                        Me.lblMessage.Text = objInquestContent.INQUESTNAME

                        Dim rowQuestion As TableRow
                        Dim cellQuestionText As TableCell

                        'Se recorren las preguntas del contenido de encuesta
                        For Each questionByInquestContent As QuestionsByInquestContentEntity In objInquestContent.QUESTIONSLIST

                            'Se agrega el numero y el texto de la pregunta actual
                            rowQuestion = New TableRow
                            cellQuestionText = New TableCell()
                            cellQuestionText.Text = myCounter.ToString() & ") " & questionByInquestContent.questiontext
                            cellQuestionText.ColumnSpan = 2

                            'Se agregan las celdas a la fila
                            rowQuestion.Cells.Add(cellQuestionText)

                            'Se agrega la fila a la tabla
                            Me.tbQuestions.Rows.Add(rowQuestion)

                            'Se crea la fila de la respuesta
                            Dim rowAnswer = New TableRow
                            Dim cellAnswerText As New TableCell()
                            cellAnswerText.Text = "Respuesta:"
                            Dim cellAnswerControl As New TableCell()

                            'Se agrega el control de respuesta
                            Dim myTextBox As TextBox
                            Dim myRadioButtonList As RadioButtonList

                            'Se verifica el tipo de pregunta
                            If (questionByInquestContent.questiontype = "Respuesta Texto") Then
                                myTextBox = New TextBox()
                                myTextBox.ID = questionByInquestContent.id
                                myTextBox.Width = 400
                                myTextBox.MaxLength = 255

                                'Se crea el respectivo validador de campo requerido.
                                Dim myRfv As New RequiredFieldValidator()
                                myRfv.ErrorMessage = "*"
                                myRfv.Display = ValidatorDisplay.Dynamic
                                myRfv.SetFocusOnError = True
                                myRfv.ControlToValidate = myTextBox.ID

                                'Se agrega el control a la celda
                                cellAnswerControl.Controls.Add(myTextBox)
                                cellAnswerControl.Controls.Add(myRfv)
                            Else
                                'Se instancia un control de tipo radio button list
                                myRadioButtonList = New RadioButtonList()
                                myRadioButtonList.ID = questionByInquestContent.id
                                'Se recorre la lista de posibles opciones de respuesta para la pregunta actual
                                For Each answerOption As AnswersByQuestionEntity In questionByInquestContent.ANSWERSLIST
                                    myRadioButtonList.Items.Add(New ListItem(answerOption.answer, answerOption.id))
                                Next

                                'Se selecciona la priemr opción
                                myRadioButtonList.SelectedIndex = 0

                                'Se crea el respectivo validador de campo requerido.
                                Dim myRfv As New RequiredFieldValidator()
                                myRfv.ErrorMessage = "*"
                                myRfv.Display = ValidatorDisplay.Dynamic
                                myRfv.SetFocusOnError = True
                                myRfv.ControlToValidate = myRadioButtonList.ID

                                'Se agrega el control a la celda
                                cellAnswerControl.Controls.Add(myRadioButtonList)
                                cellAnswerControl.Controls.Add(myRfv)
                            End If

                            'Se agregan las celdas a la fila de la respuesta
                            rowAnswer.Cells.Add(cellAnswerText)
                            rowAnswer.Cells.Add(cellAnswerControl)

                            'Se agrega la fila a la tabla
                            Me.tbQuestions.Rows.Add(rowAnswer)

                            'Se incremena el contador de preguntas
                            myCounter += 1

                        Next

                        'Se agrega la fila de los comentarios
                        Dim rowComments As New TableRow()
                        Dim cellCommentsLabel As New TableCell()
                        Dim cellCommentstextBox As New TableCell()

                        'Se crea un control etiqueta
                        Dim lblComments As New Label
                        lblComments.ID = "lblComments"
                        lblComments.Text = "Comentarios"
                        'Se agrega el control a la celda
                        cellCommentsLabel.Controls.Add(lblComments)

                        'Se crea un control textArea
                        Dim txtComments As New TextBox
                        txtComments.ID = "txtComments"
                        txtComments.Width = 400
                        txtComments.TextMode = TextBoxMode.MultiLine
                        txtComments.Attributes.Add("onkeypress", "return textboxAreaMaxNumber(this,255)")
                        'Se agrega el control a la celda
                        cellCommentstextBox.Controls.Add(txtComments)

                        'Se agregan las celdas a la fila de los comentarios
                        rowComments.Cells.Add(cellCommentsLabel)
                        rowComments.Cells.Add(cellCommentstextBox)

                        'Se agrega la fila a la tabla
                        Me.tbQuestions.Rows.Add(rowComments)

                    Else

                        'Se deshabilita el formulario actual
                        Me.lblMessage.Text = "NO HAY PREGUNTAS PARA LA ENCUESTA SELECCIONADA, POR FAVOR VERIFIQUE."
                        Me.lblMessage.ForeColor = Drawing.Color.Red
                        Me.btnAddData.Enabled = False

                    End If

                Else

                    'Se deshabilita el formulario actual
                    Me.lblMessage.Text = "EL CONTENIDO DE LA ENCUESTA SELECCIONADA SE ENCUENTRA DESHABILITADO, POR FAVOR VERIFIQUE."
                    Me.lblMessage.ForeColor = Drawing.Color.Red
                    Me.btnAddData.Enabled = False

                End If

            Else

                'Se deshabilita el formulario actual
                Me.lblMessage.Text = "LA ENCUESTA SELECCIONADA SE ENCUENTRA DESHABILITADA, POR FAVOR VERIFIQUE."
                Me.lblMessage.ForeColor = Drawing.Color.Red
                Me.btnAddData.Enabled = False

            End If

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

    End Sub

    ''' <summary>
    ''' Permite cargar los resultados de una encuesta determinada
    ''' </summary>
    ''' <param name="objResolvedInquest">objeto de tipo ResolvedInquestEntity</param>
    ''' <remarks></remarks>
    Private Sub loadInquest(ByVal objResolvedInquest As ResolvedInquestEntity)

        'Declaración de variables        
        Dim myTextBox As New TextBox()
        Dim myRadioButtonList As New RadioButtonList()
        Dim myCounter As Integer = 1

        'Se recorren las respuestas registradas
        For Each row As TableRow In Me.tbQuestions.Rows

            'Se crea un objeto de tipo AnswersByResolvedInquestEntity
            Dim answersByResolvedInquest As New AnswersByResolvedInquestEntity()

            'Se verifica si la fila es par. (en las filas pares se encuentran las repsuestas)
            If (myCounter Mod 2 = 0) Then

                'Se verifica si el control de la fila actual es una caja de texto
                If (myTextBox.GetType.ToString() = row.Cells(1).Controls(0).GetType().ToString()) Then

                    'Se recupera el control de la segunda celda
                    myTextBox = row.Cells(1).Controls(0)

                    'Se busca la respuesta que contenga el id de la pregunta requerida
                    answersByResolvedInquest = objResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST.Find(Function(aAnswer) aAnswer.idquestionsbyinquestcontent = myTextBox.ID)

                    'Se pobla el objeto de tipo respuesta por encuesta resuelta
                    myTextBox.Text = answersByResolvedInquest.answertext

                Else

                    'Se captura el control de tipo radioButtonList
                    myRadioButtonList = row.Cells(1).Controls(0)

                    'Se busca la respuesta que contenga el id de la pregunta requerida
                    answersByResolvedInquest = objResolvedInquest.ANSWERSBYRESOLVEDINQUESTLIST.Find(Function(aAnswer) aAnswer.idquestionsbyinquestcontent = myRadioButtonList.ID)

                    'Se pobla el objeto de tipo respuesta por encuesta resuelta
                    myRadioButtonList.SelectedValue = answersByResolvedInquest.idanswersbyquestion

                End If

            Else

                'Se verifica si la fila actual tiene mas de una celda
                If (row.Cells.Count > 1) Then

                    'Se verifica si la caja de texto actual es la caja de texto de los comentarios
                    If (row.Cells(1).Controls(0).ID = "txtComments") Then
                        'Se captura el control de tipo radioButtonList
                        myTextBox = row.Cells(1).Controls(0)

                        'Se almacenan los comentarios digitados
                        myTextBox.Text = objResolvedInquest.comments
                    End If

                End If

            End If

            'Se incrementa el contador de filas
            myCounter += 1

        Next

    End Sub

#End Region

End Class
