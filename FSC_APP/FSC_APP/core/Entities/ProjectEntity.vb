Imports Microsoft.VisualBasic

Public Class ProjectEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _ididea As Integer
    Private _code As String
    Private _name As String
    Private _objective As String
    Private _antecedent As String
    Private _justification As String
    Private _begindate As DateTime
    Private _duration As String
    Private _zonedescription As String
    Private _population As String
    Private _strategicdescription As String
    Private _results As String
    Private _fsccontribution As Double
    Private _counterpartvalue As Double
    Private _effectivebudget As String
    Private _attachment As String
    Private _idphase As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _source As String
    Private _purpose As String
    Private _totalcost As Double

    Private _resultsknowledgemanagement As String
    Private _resultsinstalledcapacity As String
    Private _typeapproval As String
    ' campos bandera para activar o desactivar los campos de valor, tiempo y alcance
    Private _editablemoney As Char
    Private _editabletime As Char
    Private _editableresults As Char
    Private _completiondate As DateTime






    Private _sourceByProjectList As List(Of SourceByProjectEntity)
    Private _projectlocationlist As List(Of ProjectLocationEntity)
    Private _thirdbyprojectlist As List(Of ThirdByProjectEntity)
    Private _operatorbyprojectlist As List(Of OperatorByProjectEntity)
    Private _ProgramComponentbyprojectlist As List(Of ProgramComponentByProjectEntity)
    Private _forumlist As List(Of ForumEntity)
    'LISTA FLUJO DE PAGOS
    Private _paymentflowByProjectList As List(Of PaymentFlowEntity)

    'PARA ACLARATORIOS
    Private _explanatoryEntityList As List(Of ExplanatoryEntity)

    'para lista de documentos
    Private _documentList As List(Of DocumentsEntity)
    Private _documentsByEntityList As List(Of DocumentsByEntityEntity)

    ' para manejo de versiones
    Private _idKey As Integer
    Private _isLastVersion As Boolean

    Private _username As String
    Private _ideaname As String
    Private _Programname As String
    Private _StrategicLinename As String
    Private _currentactivityname As String
    Private _strategicobjectivename As String

    Private _idProcessInstance As Integer
    Private _idActivityInstance As Integer

#End Region

#Region "Propiedades"

    Shared Function cualquier() As String
        Return "hola"
    End Function

    Public Function cualquier2() As String
        Return "hola"
    End Function

    Public Property id() As Integer
        Get
            Return Me._id
        End Get
        Set(ByVal value As Integer)
            Me._id = value
        End Set
    End Property
    Public Property ididea() As Integer
        Get
            Return Me._ididea
        End Get
        Set(ByVal value As Integer)
            Me._ididea = value
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
    Public Property antecedent() As String
        Get
            Return Me._antecedent
        End Get
        Set(ByVal value As String)
            Me._antecedent = value
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
    Public Property begindate() As DateTime
        Get
            Return Me._begindate
        End Get
        Set(ByVal value As DateTime)
            Me._begindate = value
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
    Public Property zonedescription() As String
        Get
            Return Me._zonedescription
        End Get
        Set(ByVal value As String)
            Me._zonedescription = value
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
    Public Property strategicdescription() As String
        Get
            Return Me._strategicdescription
        End Get
        Set(ByVal value As String)
            Me._strategicdescription = value
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
    Public Property purpose() As String
        Get
            Return Me._purpose
        End Get
        Set(ByVal value As String)
            Me._purpose = value
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
    Public Property effectivebudget() As String
        Get
            Return Me._effectivebudget
        End Get
        Set(ByVal value As String)
            Me._effectivebudget = value
        End Set
    End Property
    ' get y set campos editables del otro si
    Public Property editablemoney() As String
        Get
            Return Me._editablemoney
        End Get
        Set(ByVal value As String)
            Me._editablemoney = value
        End Set
    End Property

    Public Property editabletime() As String
        Get
            Return Me._editabletime
        End Get
        Set(ByVal value As String)
            Me._editabletime = value
        End Set
    End Property

    Public Property editableresults() As String
        Get
            Return Me._editableresults
        End Get
        Set(ByVal value As String)
            Me._editableresults = value
        End Set
    End Property
    Public Property attachment() As String
        Get
            Return Me._attachment
        End Get
        Set(ByVal value As String)
            Me._attachment = value
        End Set
    End Property
    Public Property idphase() As Integer
        Get
            Return Me._idphase
        End Get
        Set(ByVal value As Integer)
            Me._idphase = value
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
    Public Property sourceByProjectList() As List(Of SourceByProjectEntity)
        Get
            Return Me._sourceByProjectList
        End Get
        Set(ByVal value As List(Of SourceByProjectEntity))
            Me._sourceByProjectList = value
        End Set
    End Property
    Public Property projectlocationlist() As List(Of ProjectLocationEntity)
        Get
            Return Me._projectlocationlist
        End Get
        Set(ByVal value As List(Of ProjectLocationEntity))
            Me._projectlocationlist = value
        End Set
    End Property
    Public Property thirdbyprojectlist() As List(Of ThirdByProjectEntity)
        Get
            Return Me._thirdbyprojectlist
        End Get
        Set(ByVal value As List(Of ThirdByProjectEntity))
            Me._thirdbyprojectlist = value
        End Set
    End Property
    Public Property operatorbyprojectlist() As List(Of OperatorByProjectEntity)
        Get
            Return Me._operatorbyprojectlist
        End Get
        Set(ByVal value As List(Of OperatorByProjectEntity))
            Me._operatorbyprojectlist = value
        End Set
    End Property
    Public Property ProgramComponentbyprojectlist() As List(Of ProgramComponentByProjectEntity)
        Get
            Return Me._ProgramComponentbyprojectlist
        End Get
        Set(ByVal value As List(Of ProgramComponentByProjectEntity))
            Me._ProgramComponentbyprojectlist = value
        End Set
    End Property
    Public Property forumlist() As List(Of ForumEntity)
        Get
            Return Me._forumlist
        End Get
        Set(ByVal value As List(Of ForumEntity))
            Me._forumlist = value
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
    Public Property IDEANAME() As String
        Get
            Return Me._ideaname
        End Get
        Set(ByVal value As String)
            Me._ideaname = value
        End Set
    End Property
    Public Property ProgramNAME() As String
        Get
            Return Me._Programname
        End Get
        Set(ByVal value As String)
            Me._Programname = value
        End Set
    End Property
    Public Property StrategicLineNAME() As String
        Get
            Return Me._StrategicLinename
        End Get
        Set(ByVal value As String)
            Me._StrategicLinename = value
        End Set
    End Property
    Public Property CURRENTACTIVITYNAME() As String
        Get
            Return Me._currentactivityname
        End Get
        Set(ByVal value As String)
            Me._currentactivityname = value
        End Set
    End Property
    Public ReadOnly Property PHASENAME() As String
        Get
            PHASENAME = ""
            Select Case Me._idphase
                Case 1
                    PHASENAME = "Formulación"
                Case 2
                    PHASENAME = "Planeación"
                Case 3
                    PHASENAME = "Ejecución"
                Case 4
                    PHASENAME = "Evaluación"
            End Select
            Return PHASENAME
        End Get
    End Property
    Public ReadOnly Property ProgramComponentBYPROJECTLISTTEXT() As String
        Get
            Dim text = ""
            Dim objProgramComponentByProject As New ProgramComponentByProjectEntity
            For Each objProgramComponentByProject In Me._ProgramComponentbyprojectlist
                text += objProgramComponentByProject.ProgramComponentNAME & "<br />"
            Next
            Return text
        End Get
    End Property
    Public ReadOnly Property FORUMLISTTEXT() As String
        Get
            Dim text = ""
            Dim objForum As New ForumEntity
            For Each objForum In Me._forumlist
                text += objForum.subject & "&#09<a href='projectForumPanel.aspx?id=" & objForum.id & "'>Ir al foro</a> <br />"
            Next
            Return text
        End Get
    End Property
    Public Property STRATEGICOBJECTIVENAME() As String
        Get
            Return Me._strategicobjectivename
        End Get
        Set(ByVal value As String)
            Me._strategicobjectivename = value
        End Set
    End Property

    Public Property idKey() As Integer
        Get
            Return Me._idkey
        End Get
        Set(ByVal value As Integer)
            Me._idkey = value
        End Set
    End Property

    Public Property isLastVersion() As Boolean
        Get
            Return Me._isLastVersion
        End Get
        Set(ByVal value As Boolean)
            Me._isLastVersion = value
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
    Public Property ResultsKnowledgeManagement() As String
        Get
            Return Me._resultsknowledgemanagement
        End Get
        Set(ByVal value As String)
            Me._resultsknowledgemanagement = value
        End Set
    End Property
    Public Property ResultsInstalledCapacity() As String
        Get
            Return Me._resultsinstalledcapacity
        End Get
        Set(ByVal value As String)
            Me._resultsinstalledcapacity = value
        End Set
    End Property
    Public Property Typeapproval() As String
        Get
            Return Me._typeapproval
        End Get
        Set(ByVal value As String)
            Me._typeapproval = value
        End Set
    End Property

    Public Property completiondate() As DateTime
        Get
            Return Me._completiondate
        End Get
        Set(ByVal value As DateTime)
            Me._completiondate = value
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
    Public Property explanatoryEntityList() As List(Of ExplanatoryEntity)
        Get
            Return Me._explanatoryEntityList
        End Get
        Set(ByVal value As List(Of ExplanatoryEntity))
            Me._explanatoryEntityList = value
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

#End Region

End Class
