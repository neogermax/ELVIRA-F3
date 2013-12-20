Imports Microsoft.VisualBasic

Public Class InquestContentEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idinquest As Integer
    Private _code As String
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    'Atributos adicionales
    Private _username As String
    Private _InquestName As String
    Private _InquestEnabled As Boolean
    Private _questionsList As List(Of QuestionsByInquestContentEntity)
    Private _questionText As String
    Private _questionType As String
    Private _answer As String
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
    Public Property idinquest() As Integer
        Get
            Return Me._idinquest
        End Get
        Set(ByVal value As Integer)
            Me._idinquest = value
        End Set
    End Property
    Public Property code() As String
        Get
            Return Me._code
        End Get
        Set(ByVal value As String)
            Me._code = value
        End Set
    End Property
    Public Property enabled() As Boolean
        Get
            Return Me._enabled
        End Get
        Set(ByVal value As Boolean)
            Me._enabled = value
        End Set
    End Property
    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
        End Set
    End Property
    Public Property createdate() As DateTime
        Get
            Return Me._createdate
        End Get
        Set(ByVal value As DateTime)
            Me._createdate = value
        End Set
    End Property

    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property INQUESTNAME() As String
        Get
            Return Me._InquestName
        End Get
        Set(ByVal value As String)
            Me._InquestName = value
        End Set
    End Property
    Public Property INQUESTENABLED() As Boolean
        Get
            Return Me._InquestEnabled
        End Get
        Set(ByVal value As Boolean)
            Me._InquestEnabled = value
        End Set
    End Property
    Public Property QUESTIONSLIST() As List(Of QuestionsByInquestContentEntity)
        Get
            Return Me._questionsList
        End Get
        Set(ByVal value As List(Of QuestionsByInquestContentEntity))
            Me._questionsList = value
        End Set
    End Property
    Public Property QUESTIONTEXT() As String
        Get
            Return Me._questionText
        End Get
        Set(ByVal value As String)
            Me._questionText = value
        End Set
    End Property
    Public Property QUESTIONTYPE() As String
        Get
            Return Me._questionType
        End Get
        Set(ByVal value As String)
            Me._questionType = value
        End Set
    End Property
    Public Property ANSWER() As String
        Get
            Return Me._answer
        End Get
        Set(ByVal value As String)
            Me._answer = value
        End Set
    End Property

#End Region

End Class
