Imports Microsoft.VisualBasic

Public Class ContractRequestEntity

#Region "Campos"

    ' campos
    Private _requestnumber As Integer
    Private _idmanagement As Integer
    Private _idproject As Integer
    Private _idcontractnature As Integer
    Private _contractnumberadjusted As String
    Private _idprocessinstance As Integer
    Private _idactivityinstance As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _polizaid As Integer
    Private _signedcontract As Boolean
    Private _startdate As DateTime
    Private _suscriptdate As DateTime
    Private _confidential As Integer
    Private _monthduration As Decimal
    Private _supervisor As String
    Private _notes As String

    'Atributos adicionales
    Private _liquidationdate As String
    Private _finishfilter As String
    Private _username As String
    Private _managementname As String
    Private _projectname As String
    Private _contractorLegalEntityByContractRequestList As List(Of ContractorLegalEntityByContractRequestEntity)
    Private _contractorNaturalPersonByContractRequestList As List(Of ContractorNaturalPersonByContractRequestEntity)
    Private _subjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity
    Private _paymentsListByContractRequestList As List(Of PaymentsListByContractRequestEntity)
    Private _contractDataByContractRequest As ContractDataByContractRequestEntity
    Private _commentsByContractRequest As CommentsByContractRequestEntity
    Private _externalcontract As Boolean
   
#End Region

#Region "Propiedades"

    Public Property requestnumber() As Integer
        Get
            Return Me._requestnumber
        End Get
        Set(ByVal value As Integer)
            Me._requestnumber = value
        End Set
    End Property
    Public Property idmanagement() As Integer
        Get
            Return Me._idmanagement
        End Get
        Set(ByVal value As Integer)
            Me._idmanagement = value
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
    Public Property idcontractnature() As Integer
        Get
            Return Me._idcontractnature
        End Get
        Set(ByVal value As Integer)
            Me._idcontractnature = value
        End Set
    End Property
    Public Property contractnumberadjusted() As String
        Get
            Return Me._contractnumberadjusted
        End Get
        Set(ByVal value As String)
            Me._contractnumberadjusted = value
        End Set
    End Property
    Public Property idprocessinstance() As Integer
        Get
            Return Me._idprocessinstance
        End Get
        Set(ByVal value As Integer)
            Me._idprocessinstance = value
        End Set
    End Property
    Public Property idactivityinstance() As Integer
        Get
            Return Me._idactivityinstance
        End Get
        Set(ByVal value As Integer)
            Me._idactivityinstance = value
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
    Public Property MANAGEMENTNAME() As String
        Get
            Return Me._managementname
        End Get
        Set(ByVal value As String)
            Me._managementname = value
        End Set
    End Property
    Public Property PROJECTNAME() As String
        Get
            Return Me._projectname
        End Get
        Set(ByVal value As String)
            Me._projectname = value
        End Set
    End Property
    Public Property CONTRACTORLEGALENTITYBYCONTRACTREQUESTLIST() As List(Of ContractorLegalEntityByContractRequestEntity)
        Get
            Return Me._contractorLegalEntityByContractRequestList
        End Get
        Set(ByVal value As List(Of ContractorLegalEntityByContractRequestEntity))
            Me._contractorLegalEntityByContractRequestList = value
        End Set
    End Property
    Public Property CONTRACTORNATURALPERSONBYCONTRACTREQUESTLIST() As List(Of ContractorNaturalPersonByContractRequestEntity)
        Get
            Return Me._contractorNaturalPersonByContractRequestList
        End Get
        Set(ByVal value As List(Of ContractorNaturalPersonByContractRequestEntity))
            Me._contractorNaturalPersonByContractRequestList = value
        End Set
    End Property
    Public Property SUBJECTANDVALUEBYCONTRACTREQUEST() As SubjectAndValueByContractRequestEntity
        Get
            Return Me._subjectAndValueByContractRequest
        End Get
        Set(ByVal value As SubjectAndValueByContractRequestEntity)
            Me._subjectAndValueByContractRequest = value
        End Set
    End Property
    Public Property PAYMENTSLISTBYCONTRACTREQUESTLIST() As List(Of PaymentsListByContractRequestEntity)
        Get
            Return Me._paymentsListByContractRequestList
        End Get
        Set(ByVal value As List(Of PaymentsListByContractRequestEntity))
            Me._paymentsListByContractRequestList = value
        End Set
    End Property
    Public Property CONTRACTDATABYCONTRACTREQUEST() As ContractDataByContractRequestEntity
        Get
            Return Me._contractDataByContractRequest
        End Get
        Set(ByVal value As ContractDataByContractRequestEntity)
            Me._contractDataByContractRequest = value
        End Set
    End Property
    Public Property COMMENTSBYCONTRACTREQUEST() As CommentsByContractRequestEntity
        Get
            Return Me._commentsByContractRequest
        End Get
        Set(ByVal value As CommentsByContractRequestEntity)
            Me._commentsByContractRequest = value
        End Set
    End Property

    'Campos nuevos de contratacion

    Public Property polizaid() As Integer
        Get
            Return Me._polizaid
        End Get
        Set(ByVal value As Integer)
            Me._polizaid = value
        End Set
    End Property

    Public Property signedcontract() As Boolean
        Get
            Return Me._signedcontract
        End Get
        Set(ByVal value As Boolean)
            Me._signedcontract = value
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

    Public Property suscriptdate() As DateTime
        Get
            Return Me._suscriptdate
        End Get
        Set(ByVal value As DateTime)
            Me._suscriptdate = value
        End Set
    End Property

    Public Property LiquidationDate() As DateTime
        Get
            Return Me._LiquidationDate
        End Get
        Set(ByVal value As DateTime)
            Me._LiquidationDate = value
        End Set
    End Property


    Public Property confidential() As Integer
        Get
            Return Me._confidential
        End Get
        Set(ByVal value As Integer)
            Me._confidential = value
        End Set
    End Property

    Public Property monthduration() As Decimal
        Get
            Return Me._monthduration
        End Get
        Set(ByVal value As Decimal)
            Me._monthduration = value
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

    Public Property notes() As String
        Get
            Return Me._notes
        End Get
        Set(ByVal value As String)
            Me._notes = value
        End Set
    End Property

    Public Property finishfilter() As String
        Get
            Return Me._finishfilter
        End Get
        Set(ByVal value As String)
            Me._finishfilter = value
        End Set
    End Property

    Public Property ExternalContract() As Boolean
        Get
            Return Me._externalcontract
        End Get
        Set(ByVal value As Boolean)
            Me._externalcontract = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()

        'Se inicializan los objetos requeridos
        Me._subjectAndValueByContractRequest = New SubjectAndValueByContractRequestEntity()
        Me._contractDataByContractRequest = New ContractDataByContractRequestEntity()
        Me._commentsByContractRequest = New CommentsByContractRequestEntity()

    End Sub

#End Region

End Class
