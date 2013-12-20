Imports Microsoft.VisualBasic

Public Class ExecutionContractualPlanRegistryDetailsEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idexecutioncontractualplanregistry As Integer
    Private _idproject As Integer
    Private _concept As String
    Private _contractType As Integer
    Private _totalcost As Double
    Private _engagementdate As DateTime
    Private _comments As String
    Private _createdate As DateTime

    Private _projectname As String
    Private _typeText As String
    Private _contractTypeName As String

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
    Public Property idexecutioncontractualplanregistry() As Integer
        Get
            Return Me._idexecutioncontractualplanregistry
        End Get
        Set(ByVal value As Integer)
            Me._idexecutioncontractualplanregistry = value
        End Set
    End Property
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property concept() As String
        Get
            Return Me._concept
        End Get
        Set(ByVal value As String)
            Me._concept = value
        End Set
    End Property
    Public Property contractType() As Integer
        Get
            Return Me._contractType
        End Get
        Set(ByVal value As Integer)
            Me._contractType = value
        End Set
    End Property
    Public Property totalcost() As Double
        Get
            Return Me._totalcost
        End Get
        Set(ByVal value As Double)
            Me._totalcost = value
        End Set
    End Property
    Public Property engagementdate() As DateTime
        Get
            Return Me._engagementdate
        End Get
        Set(ByVal value As DateTime)
            Me._engagementdate = value
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
    Public Property createdate() As DateTime
        Get
            Return Me._createdate
        End Get
        Set(ByVal value As DateTime)
            Me._createdate = value
        End Set
    End Property
    Public Property projectname() As String
        Get
            Return Me._projectname
        End Get
        Set(ByVal value As String)
            Me._projectname = value
        End Set
    End Property
    Public ReadOnly Property typetext() As String
        Get
            typetext = Me._contractType
        End Get
    End Property
    Public Property contractTypeName() As String
        Get
            Return Me._contractTypeName
        End Get
        Set(ByVal value As String)
            Me._contractTypeName = value
        End Set
    End Property

#End Region

End Class
