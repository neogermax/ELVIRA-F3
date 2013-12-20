Imports Microsoft.VisualBasic

Public Class QuestionsByInquestContentEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idinquestcontent As Integer
    Private _questiontext As String
    Private _questiontype As String

    'Atributos adicionales
    Private _answersList As List(Of AnswersByQuestionEntity)

#End Region

#Region "Propiedades"

    Public Property id() As Integer
        Get
            Return Me._id
        End Get
        Set(ByVal value As Integer)
            Me._id = value
        End Set
    End Property
    Public Property idinquestcontent() As Integer
        Get
            Return Me._idinquestcontent
        End Get
        Set(ByVal value As Integer)
            Me._idinquestcontent = value
        End Set
    End Property
    Public Property questiontext() As String
        Get
            Return Me._questiontext
        End Get
        Set(ByVal value As String)
            Me._questiontext = value
        End Set
    End Property
    Public Property questiontype() As String
        Get
            Return Me._questiontype
        End Get
        Set(ByVal value As String)
            Me._questiontype = value
        End Set
    End Property

    Public Property ANSWERSLIST() As List(Of AnswersByQuestionEntity)
        Get
            Return Me._answersList
        End Get
        Set(ByVal value As List(Of AnswersByQuestionEntity))
            Me._answersList = value
        End Set
    End Property

#End Region

End Class
