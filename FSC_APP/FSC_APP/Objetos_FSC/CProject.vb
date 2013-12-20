Public Class CProject


#Region "propiedades publicas y privadas"

    Private _Id As Integer
    Private _IdIdea As Integer
    Private _Code As String
    Private _Name As String
    Private _Objective As String
    Private _Antecedent As String
    Private _Justification As String
    Private _BeginDate As Date
    Private _Duration As String
    Private _ZoneDescription As String
    Private _Population As String
    Private _StrategicDescription As String
    Private _Results As String
    Private _Source As String
    Private _Purpose As String
    Private _TotalCost As Decimal
    Private _FSCContribution As Decimal
    Private _CounterpartValue As Decimal
    Private _EffectiveBudget As Integer
    Private _Attachment As String
    Private _IdPhase As Integer
    Private _Enabled As Integer
    Private _IdUser As Integer
    Private _CreateDate As Date
    Private _idKey As Integer
    Private _isLastVersion As Integer
    Private _IdProcessInstance As Integer
    Private _IdActivityInstance As Integer
    Private _Typeapproval As String
    Private _editablemoney As String
    Private _editabletime As String
    Private _completiondate As Date
    Private _ResultsKnowledgeManagement As String
    Private _ResultsInstalledCapacity As String


    Public ReadOnly Property id() As Integer
        Get
            Return _Id
        End Get
    End Property
    Public Property ididea() As Integer
        Get
            Return _IdIdea
        End Get
        Set(ByVal value As Integer)
            _IdIdea = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public Property Objective() As String
        Get
            Return _Objective
        End Get
        Set(ByVal value As String)
            _Objective = value
        End Set
    End Property
    Public Property Antecedent() As String
        Get
            Return _Antecedent
        End Get
        Set(ByVal value As String)
            _Antecedent = value
        End Set
    End Property
    Public Property Justification() As String
        Get
            Return _Justification
        End Get
        Set(ByVal value As String)
            _Justification = value
        End Set
    End Property
    Public Property BeginDate() As Date
        Get
            Return _BeginDate
        End Get
        Set(ByVal value As Date)
            _BeginDate = value
        End Set
    End Property
    Public Property Duration() As String
        Get
            Return _Duration
        End Get
        Set(ByVal value As String)
            _Duration = value
        End Set
    End Property
    Public Property ZoneDescription() As String
        Get
            Return _ZoneDescription
        End Get
        Set(ByVal value As String)
            _ZoneDescription = value
        End Set
    End Property
    Public Property Population() As String
        Get
            Return _Population
        End Get
        Set(ByVal value As String)
            _Population = value
        End Set
    End Property
    Public Property StrategicDescription() As String
        Get
            Return _StrategicDescription
        End Get
        Set(ByVal value As String)
            _StrategicDescription = value
        End Set
    End Property
    Public Property Results() As String
        Get
            Return _Results
        End Get
        Set(ByVal value As String)
            _Results = value
        End Set
    End Property
    Public Property Source() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property
    Public Property Purpose() As String
        Get
            Return _Purpose
        End Get
        Set(ByVal value As String)
            _Purpose = value
        End Set
    End Property
    Public Property TotalCost() As Decimal
        Get
            Return _TotalCost
        End Get
        Set(ByVal value As Decimal)
            _TotalCost = value
        End Set
    End Property
    Public Property FSCContribution() As Decimal
        Get
            Return _FSCContribution
        End Get
        Set(ByVal value As Decimal)
            _FSCContribution = value
        End Set
    End Property
    Public Property CounterpartValue() As Decimal
        Get
            Return _CounterpartValue
        End Get
        Set(ByVal value As Decimal)
            _CounterpartValue = value
        End Set
    End Property
    Public Property EffectiveBudget() As Integer
        Get
            Return _EffectiveBudget
        End Get
        Set(ByVal value As Integer)
            _EffectiveBudget = value
        End Set
    End Property
    Public Property Attachment() As String
        Get
            Return _Attachment
        End Get
        Set(ByVal value As String)
            _Attachment = value
        End Set
    End Property
    Public Property IdPhase() As Integer
        Get
            Return _IdPhase
        End Get
        Set(ByVal value As Integer)
            _IdPhase = value
        End Set
    End Property
    Public Property IdUser() As Integer
        Get
            Return _IdUser
        End Get
        Set(ByVal value As Integer)
            _IdUser = value
        End Set
    End Property
    Public Property CreateDate() As Date
        Get
            Return _CreateDate
        End Get
        Set(ByVal value As Date)
            _CreateDate = value
        End Set
    End Property
    Public Property idKey() As Integer
        Get
            Return _idKey
        End Get
        Set(ByVal value As Integer)
            _idKey = value
        End Set
    End Property
    Public Property isLastVersion() As Integer
        Get
            Return _isLastVersion
        End Get
        Set(ByVal value As Integer)
            _isLastVersion = value
        End Set
    End Property
    Public Property IdActivityInstance() As Integer
        Get
            Return _IdActivityInstance
        End Get
        Set(ByVal value As Integer)
            _IdActivityInstance = value
        End Set
    End Property
    Public Property Typeapproval() As String
        Get
            Return _Typeapproval
        End Get
        Set(ByVal value As String)
            _Typeapproval = value
        End Set
    End Property
    Public Property editablemoney() As String
        Get
            Return _editablemoney
        End Get
        Set(ByVal value As String)
            _editablemoney = value
        End Set
    End Property
    Public Property editabletime() As String
        Get
            Return _editabletime
        End Get
        Set(ByVal value As String)
            _editabletime = value
        End Set
    End Property
    Public Property ResultsKnowledgeManagement() As String
        Get
            Return _ResultsKnowledgeManagement
        End Get
        Set(ByVal value As String)
            _ResultsKnowledgeManagement = value
        End Set
    End Property
    Public Property ResultsInstalledCapacity() As String
        Get
            Return _ResultsInstalledCapacity
        End Get
        Set(ByVal value As String)
            _ResultsInstalledCapacity = value
        End Set
    End Property

#End Region

End Class
