Imports Microsoft.VisualBasic

Public Class ProjectApprovalRecordEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _code As String
    Private _comments As String
    Private _attachment As String
    Private _approvaldate As DateTime
    Private _actnumber As String
    Private _approvedvalue As Double
    Private _approved As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _projectname As String
    Private _username As String
    Private _approvedtext As String

    Private _idProcessInstance As Integer
    Private _idActivityInstance As Integer

    'TODO:39 se agregan nuevos campos para la validacion de aprobacion idea
    '12/06/13 GERMAN RODRIGUEZ MGgroup

    Private _codeapprovedidea As String
    Private _Ididea As Integer

    'TODO:39 se agregan nuevos campos para la validacion de aprobacion idea
    '12/06/13 GERMAN RODRIGUEZ MGgroup
    'cierre de modificacion

    'TODO:40 se agregan nuevos campos para los aportes de aprobacion idea
    '24/08/13 GERMAN RODRIGUEZ MGgroup

    Private _aportFSC As Double
    Private _aportOtros As Double

    'TODO:40 se agregan nuevos campos para los aportes de aprobacion idea
    '24/08/13 GERMAN RODRIGUEZ MGgroup
    'cierre de modificacion

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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
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
    Public Property comments() As String
        Get
            Return Me._comments
        End Get
        Set(ByVal value As String)
            Me._comments = value
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
    Public Property approvaldate() As DateTime
        Get
            Return Me._approvaldate
        End Get
        Set(ByVal value As DateTime)
            Me._approvaldate = value
        End Set
    End Property
    Public Property actnumber() As String
        Get
            Return Me._actnumber
        End Get
        Set(ByVal value As String)
            Me._actnumber = value
        End Set
    End Property
    Public Property approvedvalue() As Double
        Get
            Return Me._approvedvalue
        End Get
        Set(ByVal value As Double)
            Me._approvedvalue = value
        End Set
    End Property
    Public Property approved() As Integer
        Get
            Return Me._approved
        End Get
        Set(ByVal value As Integer)
            Me._approved = value
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
    Public Property PROJECTNAME() As String
        Get
            Return Me._projectname
        End Get
        Set(ByVal value As String)
            Me._projectname = value
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
    Public ReadOnly Property APPROVEDTEXT() As String
        Get
            APPROVEDTEXT = ""
            Select Case Me._approved
                Case 0
                    APPROVEDTEXT = " Junta Directiva"
                Case 1
                    APPROVEDTEXT = "Comité de Contratación"
                Case 2
                    APPROVEDTEXT = "Pre Comité de Contratación"
            End Select
        End Get
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
    'TODO:41 se agregan nuevos campos para la validacion de aprobacion idea(propiedades)
    '12/06/13 GERMAN RODRIGUEZ MGgroup
    Public Property codeapprovedidea() As String
        Get
            Return Me._codeapprovedidea
        End Get
        Set(ByVal value As String)
            Me._codeapprovedidea = value
        End Set
    End Property
    Public Property Ididea() As Integer
        Get
            Return Me._Ididea
        End Get
        Set(ByVal value As Integer)
            Me._Ididea = value
        End Set
    End Property
    'TODO:41 se agregan nuevos campos para la validacion de aprobacion idea(propiedades)
    '12/06/13 GERMAN RODRIGUEZ MGgroup

    'TODO:42 se agregan nuevos campos para los aportes de aprobacion idea(propiedades)
    '12/06/13 GERMAN RODRIGUEZ MGgroup
    Public Property aportOtros() As Double
        Get
            Return Me._aportOtros
        End Get
        Set(ByVal value As Double)
            Me._aportOtros = value
        End Set
    End Property

    Public Property aportFSC() As Double
        Get
            Return Me._aportFSC
        End Get
        Set(ByVal value As Double)
            Me._aportFSC = value
        End Set
    End Property

    'TODO:42 se agregan nuevos campos para los aportes de aprobacion idea(propiedades)
    '12/06/13 GERMAN RODRIGUEZ MGgroup
    'cierre de cambios

#End Region

End Class
