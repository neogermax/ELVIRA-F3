Imports Microsoft.VisualBasic

Public Class AnswersByResolvedInquestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idresolvedinquest As Integer
    Private _idquestionsbyinquestcontent As Integer
    Private _idanswersbyquestion As Integer
    Private _answertext As String

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
    Public Property idresolvedinquest() As Integer
        Get
            Return Me._idresolvedinquest
        End Get
        Set(ByVal value As Integer)
            Me._idresolvedinquest = value
        End Set
    End Property
    Public Property idquestionsbyinquestcontent() As Integer
        Get
            Return Me._idquestionsbyinquestcontent
        End Get
        Set(ByVal value As Integer)
            Me._idquestionsbyinquestcontent = value
        End Set
    End Property
    Public Property idanswersbyquestion() As Integer
        Get
            Return Me._idanswersbyquestion
        End Get
        Set(ByVal value As Integer)
            Me._idanswersbyquestion = value
        End Set
    End Property
    Public Property answertext() As String
        Get
            Return Me._answertext
        End Get
        Set(ByVal value As String)
            Me._answertext = value
        End Set
    End Property

#End Region

End Class
