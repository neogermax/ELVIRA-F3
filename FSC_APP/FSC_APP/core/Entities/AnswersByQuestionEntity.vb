Imports Microsoft.VisualBasic

Public Class AnswersByQuestionEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idquestionsbyinquestcontent As Integer
    Private _idinquestcontent As Integer
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
    Public Property idquestionsbyinquestcontent() As Integer
        Get
            Return Me._idquestionsbyinquestcontent
        End Get
        Set(ByVal value As Integer)
            Me._idquestionsbyinquestcontent = value
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
    Public Property answer() As String
        Get
            Return Me._answer
        End Get
        Set(ByVal value As String)
            Me._answer = value
        End Set
    End Property

#End Region

End Class
