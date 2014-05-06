Imports Microsoft.VisualBasic

Public Class ProposalEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idsummoning As Integer
    Private _operator As String
    Private _operatornit As String
    Private _projectname As String
    Private _target As String
    Private _targetpopulation As String
    Private _expectedresults As String
    Private _totalvalue As Decimal
    Private _inputfsc As Double
    Private _inputothersources As Double
    Private _briefprojectdescription As String
    Private _score As Double
    Private _result As String
    Private _responsiblereview As Integer
    Private _reviewdate As DateTime
    Private _enabled As Boolean
    Private _createdate As DateTime
    Private _iduser As Integer
    Private _username As String
    'Atributos adicionales
    Private _responsiblereviewname As String
    Private _summoningName As String
    Private _deptoName As String
    Private _cityName As String
    Private _documentList As List(Of DocumentsEntity)
    Private _documentsByEntityList As List(Of DocumentsByEntityEntity)
    Private _locationsList As List(Of LocationByProposalEntity)

    Private _idProcessInstance As Integer
    Private _idActivityInstance As Integer

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
    Public Property idsummoning() As Integer
        Get
            Return Me._idsummoning
        End Get
        Set(ByVal value As Integer)
            Me._idsummoning = value
        End Set
    End Property
    Public Property nameOperator() As String
        Get
            Return Me._operator
        End Get
        Set(ByVal value As String)
            Me._operator = value
        End Set
    End Property
    Public Property operatornit() As String
        Get
            Return Me._operatornit
        End Get
        Set(ByVal value As String)
            Me._operatornit = value
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
    Public Property target() As String
        Get
            Return Me._target
        End Get
        Set(ByVal value As String)
            Me._target = value
        End Set
    End Property
    Public Property targetpopulation() As String
        Get
            Return Me._targetpopulation
        End Get
        Set(ByVal value As String)
            Me._targetpopulation = value
        End Set
    End Property
    Public Property expectedresults() As String
        Get
            Return Me._expectedresults
        End Get
        Set(ByVal value As String)
            Me._expectedresults = value
        End Set
    End Property
    Public Property totalvalue() As Decimal
        Get
            Return Me._totalvalue
        End Get
        Set(ByVal value As Decimal)
            Me._totalvalue = value
        End Set
    End Property
    Public Property inputfsc() As Double
        Get
            Return Me._inputfsc
        End Get
        Set(ByVal value As Double)
            Me._inputfsc = value
        End Set
    End Property
    Public Property inputothersources() As Double
        Get
            Return Me._inputothersources
        End Get
        Set(ByVal value As Double)
            Me._inputothersources = value
        End Set
    End Property
    Public Property briefprojectdescription() As String
        Get
            Return Me._briefprojectdescription
        End Get
        Set(ByVal value As String)
            Me._briefprojectdescription = value
        End Set
    End Property
    Public Property score() As Double
        Get
            Return Me._score
        End Get
        Set(ByVal value As Double)
            Me._score = value
        End Set
    End Property
    Public Property result() As String
        Get
            Return Me._result
        End Get
        Set(ByVal value As String)
            Me._result = value
        End Set
    End Property
    Public Property responsiblereview() As Integer
        Get
            Return Me._responsiblereview
        End Get
        Set(ByVal value As Integer)
            Me._responsiblereview = value
        End Set
    End Property
    Public Property reviewdate() As DateTime
        Get
            Return Me._reviewdate
        End Get
        Set(ByVal value As DateTime)
            Me._reviewdate = value
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
    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
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

    Public Property RESPONSIBLEREVIEWNAME() As String
        Get
            Return Me._responsiblereviewname
        End Get
        Set(ByVal value As String)
            Me._responsiblereviewname = value
        End Set
    End Property
    Public Property SUMMONINGNAME() As String
        Get
            Return Me._summoningName
        End Get
        Set(ByVal value As String)
            Me._summoningName = value
        End Set
    End Property
    Public Property DEPTONAME() As String
        Get
            Return Me._deptoName
        End Get
        Set(ByVal value As String)
            Me._deptoName = value
        End Set
    End Property
    Public Property CITYNAME() As String
        Get
            Return Me._cityName
        End Get
        Set(ByVal value As String)
            Me._cityName = value
        End Set
    End Property
    Public Property DOCUMENTLIST() As List(Of DocumentsEntity)
        Get
            Return Me._documentList
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Me._documentList = value
        End Set
    End Property
    Public Property DOCUMENTSBYENTITYLIST() As List(Of DocumentsByEntityEntity)
        Get
            Return Me._documentsByEntityList
        End Get
        Set(ByVal value As List(Of DocumentsByEntityEntity))
            Me._documentsByEntityList = value
        End Set
    End Property
    Public Property LOCATIONSLIST() As List(Of LocationByProposalEntity)
        Get
            Return Me._locationsList
        End Get
        Set(ByVal value As List(Of LocationByProposalEntity))
            Me._locationsList = value
        End Set
    End Property

    Public Property IdProcessInstance() As Integer
        Get
            Return Me._idProcessInstance
        End Get
        Set(ByVal value As Integer)
            Me._idProcessInstance = value
        End Set
    End Property
    Public Property IdActivityInstance() As Integer
        Get
            Return Me._idActivityInstance
        End Get
        Set(ByVal value As Integer)
            Me._idActivityInstance = value
        End Set
    End Property

#End Region

End Class
