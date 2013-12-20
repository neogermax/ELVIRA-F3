Imports Microsoft.VisualBasic

Public Class StrategicActivityEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _description As String
    Private _idstrategy As Integer
    Private _begindate As DateTime
    Private _enddate As DateTime
    Private _estimatedvalue As String
    Private _idresponsible As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _username As String
    Private _strategyname As String
    Private _responsiblename As String

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
    Public Property code() As String
        Get
            Return Me._code
        End Get
        Set(ByVal value As String)
            Me._code = value
        End Set
    End Property
    Public Property name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property
    Public Property description() As String
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
        End Set
    End Property
    Public Property idstrategy() As Integer
        Get
            Return Me._idstrategy
        End Get
        Set(ByVal value As Integer)
            Me._idstrategy = value
        End Set
    End Property
    Public Property begindate() As DateTime
        Get
            Return Me._begindate
        End Get
        Set(ByVal value As DateTime)
            Me._begindate = value
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
    Public Property estimatedvalue() As String
        Get
            Return Me._estimatedvalue
        End Get
        Set(ByVal value As String)
            Me._estimatedvalue = value
        End Set
    End Property
    Public Property idresponsible() As Integer
        Get
            Return Me._idresponsible
        End Get
        Set(ByVal value As Integer)
            Me._idresponsible = value
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

    Public Property STRATEGYNAME() As String
        Get
            Return Me._strategyname
        End Get
        Set(ByVal value As String)
            Me._strategyname = value
        End Set
    End Property

    Public Property RESPONSIBLENAME() As String
        Get
            Return Me._responsiblename
        End Get
        Set(ByVal value As String)
            Me._responsiblename = value
        End Set
    End Property

#End Region

End Class
