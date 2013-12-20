Imports Microsoft.VisualBasic

Public Class ResolvedInquestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idinquestcontent As Integer
    Private _comments As String
    Private _iduser As Integer
    Private _createdate As DateTime

    'Atributos adicionales
    Private _userName As String
    Private _answersByResolvedInquestList As List(Of AnswersByResolvedInquestEntity)

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
    Public Property comments() As String
        Get
            Return Me._comments
        End Get
        Set(ByVal value As String)
            Me._comments = value
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
            Return Me._userName
        End Get
        Set(ByVal value As String)
            Me._userName = value
        End Set
    End Property
    Public Property ANSWERSBYRESOLVEDINQUESTLIST() As List(Of AnswersByResolvedInquestEntity)
        Get
            Return Me._answersByResolvedInquestList
        End Get
        Set(ByVal value As List(Of AnswersByResolvedInquestEntity))
            Me._answersByResolvedInquestList = value
        End Set
    End Property

#End Region

End Class
