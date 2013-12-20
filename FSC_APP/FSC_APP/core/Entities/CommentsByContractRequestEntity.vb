Imports Microsoft.VisualBasic

Public Class CommentsByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _additionalcomments As String
    Private _startactrequires As Boolean
    Private _datenoticeexpiration As Date
    Private _contractnumber As String
    Private _purchaseorder As String

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
    Public Property idcontractrequest() As Integer
        Get
            Return Me._idcontractrequest
        End Get
        Set(ByVal value As Integer)
            Me._idcontractrequest = value
        End Set
    End Property
    Public Property additionalcomments() As String
        Get
            Return Me._additionalcomments
        End Get
        Set(ByVal value As String)
            Me._additionalcomments = value
        End Set
    End Property
    Public Property startactrequires() As Boolean
        Get
            Return Me._startactrequires
        End Get
        Set(ByVal value As Boolean)
            Me._startactrequires = value
        End Set
    End Property
    Public Property datenoticeexpiration() As Date
        Get
            Return Me._datenoticeexpiration
        End Get
        Set(ByVal value As Date)
            Me._datenoticeexpiration = value
        End Set
    End Property
    Public Property contractnumber() As String
        Get
            Return Me._contractnumber
        End Get
        Set(ByVal value As String)
            Me._contractnumber = value
        End Set
    End Property
    Public Property purchaseorder() As String
        Get
            Return Me._purchaseorder
        End Get
        Set(ByVal value As String)
            Me._purchaseorder = value
        End Set
    End Property

#End Region

End Class
