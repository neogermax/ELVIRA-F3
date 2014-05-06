Imports Microsoft.VisualBasic

Public Class IdeaEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _objective As String
    Private _startdate As DateTime
    Private _duration As String
    Private _areadescription As String
    Private _population As String
    Private _cost As Decimal
    Private _fsccontribution As Double
    Private _counterpartvalue As Double
    Private _strategydescription As String
    Private _results As String
    Private _source As String
    Private _justification As String
    Private _idsummoning As Integer
    Private _startprocess As Boolean
    Private _createdate As DateTime
    Private _iduser As Integer
    Private _enabled As Boolean


    'Atributo adicional que almacena el nombre del usuario
    Private _username As String
    Private _documentList As List(Of DocumentsEntity)
    Private _documentsByEntityList As List(Of DocumentsByEntityEntity)
    Private _locationByIdeaList As List(Of LocationByIdeaEntity)
    Private _thirdByIdeaList As List(Of ThirdByIdeaEntity)
    Private _ProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity)
    Private _StrategicLineName As String
    Private _ProgramName As String
    Private _ProgramComponentName As String
    Private _idProcessInstance As Integer
    Private _idActivityInstance As Integer

    ' TODO: 21 ideaEntity se crean nuevos campos 
    ' Autor: German Rodriguez MGgroup
    ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II
    Private _Loadingobservations As String
    Private _ResultsKnowledgeManagement As String
    Private _ResultsInstalledCapacity As String
    Private _idtypecontract As Integer
    Private _Enddate As DateTime

    ' TODO: 21 ideaEntity se crean nuevos campos 
    ' Autor: German Rodriguez MGgroup
    ' cierre de modificación
    Private _OBLIGACIONES As String
    Private _IVA As Integer
    Private _riesgos As String
    Private _mitigacion As String
    Private _presupuestal As String
    Private _dia As String
    Private _OthersResults As String

    Private _paymentflowByProjectList As List(Of PaymentFlowEntity)
    Private _DetailedcashflowsbyIdeaList As List(Of DetailedcashflowsEntity)

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
    Public Property objective() As String
        Get
            Return Me._objective
        End Get
        Set(ByVal value As String)
            Me._objective = value
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
    Public Property duration() As String
        Get
            Return Me._duration
        End Get
        Set(ByVal value As String)
            Me._duration = value
        End Set
    End Property
    Public Property areadescription() As String
        Get
            Return Me._areadescription
        End Get
        Set(ByVal value As String)
            Me._areadescription = value
        End Set
    End Property
    Public Property population() As String
        Get
            Return Me._population
        End Get
        Set(ByVal value As String)
            Me._population = value
        End Set
    End Property
    Public Property cost() As Decimal
        Get
            Return Me._cost
        End Get
        Set(ByVal value As Decimal)
            Me._cost = value
        End Set
    End Property
    Public Property fsccontribution() As Double
        Get
            Return Me._fsccontribution
        End Get
        Set(ByVal value As Double)
            Me._fsccontribution = value
        End Set
    End Property
    Public Property counterpartvalue() As Double
        Get
            Return Me._counterpartvalue
        End Get
        Set(ByVal value As Double)
            Me._counterpartvalue = value
        End Set
    End Property
    Public Property strategydescription() As String
        Get
            Return Me._strategydescription
        End Get
        Set(ByVal value As String)
            Me._strategydescription = value
        End Set
    End Property
    Public Property results() As String
        Get
            Return Me._results
        End Get
        Set(ByVal value As String)
            Me._results = value
        End Set
    End Property
    Public Property source() As String
        Get
            Return Me._source
        End Get
        Set(ByVal value As String)
            Me._source = value
        End Set
    End Property
    Public Property justification() As String
        Get
            Return Me._justification
        End Get
        Set(ByVal value As String)
            Me._justification = value
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
    Public Property startprocess() As Boolean
        Get
            Return Me._startprocess
        End Get
        Set(ByVal value As Boolean)
            Me._startprocess = value
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
    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
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

    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
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
    Public Property DOCUMENTSBYIDEALIST() As List(Of DocumentsByEntityEntity)
        Get
            Return Me._documentsByEntityList
        End Get
        Set(ByVal value As List(Of DocumentsByEntityEntity))
            Me._documentsByEntityList = value
        End Set
    End Property
    Public Property LOCATIONBYIDEALIST() As List(Of LocationByIdeaEntity)
        Get
            Return Me._locationByIdeaList
        End Get
        Set(ByVal value As List(Of LocationByIdeaEntity))
            Me._locationByIdeaList = value
        End Set
    End Property
    Public Property THIRDBYIDEALIST() As List(Of ThirdByIdeaEntity)
        Get
            Return Me._thirdByIdeaList
        End Get
        Set(ByVal value As List(Of ThirdByIdeaEntity))
            Me._thirdByIdeaList = value
        End Set
    End Property
    Public Property ProgramComponentBYIDEALIST() As List(Of ProgramComponentByIdeaEntity)
        Get
            Return Me._ProgramComponentByIdeaList
        End Get
        Set(ByVal value As List(Of ProgramComponentByIdeaEntity))
            Me._ProgramComponentByIdeaList = value
        End Set
    End Property
    Public Property StrategicLineNAME() As String
        Get
            Return Me._StrategicLineName
        End Get
        Set(ByVal value As String)
            Me._StrategicLineName = value
        End Set
    End Property
    Public Property ProgramNAME() As String
        Get
            Return Me._ProgramName
        End Get
        Set(ByVal value As String)
            Me._ProgramName = value
        End Set
    End Property
    Public Property ProgramComponentNAME() As String
        Get
            Return Me._ProgramComponentName
        End Get
        Set(ByVal value As String)
            Me._ProgramComponentName = value
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
    ' TODO: 22 ideaEntity se crean nuevos campos
    ' Autor: German Rodriguez MGgroup
    ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II


    Public Property Loadingobservations() As String
        Get
            Return Me._Loadingobservations
        End Get
        Set(ByVal value As String)
            Me._Loadingobservations = value
        End Set
    End Property
    Public Property ResultsKnowledgeManagement() As String
        Get
            Return Me._ResultsKnowledgeManagement
        End Get
        Set(ByVal value As String)
            Me._ResultsKnowledgeManagement = value
        End Set
    End Property
    Public Property ResultsInstalledCapacity() As String
        Get
            Return Me._ResultsInstalledCapacity
        End Get
        Set(ByVal value As String)
            Me._ResultsInstalledCapacity = value
        End Set
    End Property
    Public Property idtypecontract() As Integer
        Get
            Return Me._idtypecontract
        End Get
        Set(ByVal value As Integer)
            Me._idtypecontract = value
        End Set
    End Property
    Public Property Enddate() As DateTime
        Get
            Return Me._Enddate
        End Get
        Set(ByVal value As DateTime)
            Me._Enddate = value
        End Set
    End Property

    ' TODO: 22 ideaEntity se crean nuevos campos
    ' Autor: German Rodriguez MGgroup
    ' cierre de modificacion
    Public Property Obligaciones() As String
        Get
            Return Me._OBLIGACIONES
        End Get
        Set(ByVal value As String)
            Me._OBLIGACIONES = value
        End Set
    End Property
    Public Property iva() As Integer
        Get
            Return Me._IVA
        End Get
        Set(ByVal value As Integer)
            Me._IVA = value
        End Set
    End Property
        Public Property riesgos() As String
        Get
            Return Me._riesgos
        End Get
        Set(ByVal value As String)
            Me._riesgos = value
        End Set
    End Property
    Public Property mitigacion() As String
        Get
            Return Me._mitigacion
        End Get
        Set(ByVal value As String)
            Me._mitigacion = value
        End Set
    End Property
    Public Property presupuestal() As String
        Get
            Return Me._presupuestal
        End Get
        Set(ByVal value As String)
            Me._presupuestal = value
        End Set
    End Property
    Public Property dia() As String
        Get
            Return Me._dia
        End Get
        Set(ByVal value As String)
            Me._dia = value
        End Set
    End Property
    Public Property paymentflowByProjectList() As List(Of PaymentFlowEntity)
        Get
            Return Me._paymentflowByProjectList
        End Get
        Set(ByVal value As List(Of PaymentFlowEntity))
            Me._paymentflowByProjectList = value
        End Set
    End Property
    Public Property DetailedcashflowsbyIdeaList() As List(Of DetailedcashflowsEntity)
        Get
            Return Me._DetailedcashflowsbyIdeaList
        End Get
        Set(ByVal value As List(Of DetailedcashflowsEntity))
            Me._DetailedcashflowsbyIdeaList = value
        End Set
    End Property
    Public Property OthersResults() As String
        Get
            Return Me._OthersResults
        End Get
        Set(ByVal value As String)
            Me._OthersResults = value
        End Set
    End Property



#End Region

#Region "Constructor"

    Public Sub New()

    End Sub

#End Region

End Class
