Imports Microsoft.VisualBasic

Public Class ContractDataByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _contractduration As String
    Private _startdate As DateTime
    Private _enddate As DateTime
    Private _supervisor As String
    Private _budgetvalidity As String
    Private _contactdata As String
    Private _email As String
    Private _telephone As String

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
    Public Property contractduration() As String
        Get
            Return Me._contractduration
        End Get
        Set(ByVal value As String)
            Me._contractduration = value
        End Set
    End Property
    Public Property startdate() As DateTime
        Get
            Return Me._startdate
        End Get
        Set(ByVal value As DateTime)
            Me._startdate = value
        End Set
    End Property
    Public Property enddate() As DateTime
        Get
            Return Me._enddate
        End Get
        Set(ByVal value As DateTime)
            Me._enddate = value
        End Set
    End Property
    Public Property supervisor() As String
        Get
            Return Me._supervisor
        End Get
        Set(ByVal value As String)
            Me._supervisor = value
        End Set
    End Property
    Public Property budgetvalidity() As String
        Get
            Return Me._budgetvalidity
        End Get
        Set(ByVal value As String)
            Me._budgetvalidity = value
        End Set
    End Property
    Public Property contactdata() As String
        Get
            Return Me._contactdata
        End Get
        Set(ByVal value As String)
            Me._contactdata = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return Me._email
        End Get
        Set(ByVal value As String)
            Me._email = value
        End Set
    End Property
    Public Property telephone() As String
        Get
            Return Me._telephone
        End Get
        Set(ByVal value As String)
            Me._telephone = value
        End Set
    End Property

#End Region

End Class
