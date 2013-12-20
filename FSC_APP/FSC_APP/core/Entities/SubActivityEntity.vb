Imports Microsoft.VisualBasic

Public Class SubActivityEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idactivity As Integer
    Private _type As Integer
    Private _number As String
    Private _name As String
    Private _description As String
    Private _idresponsible As Integer
    Private _begindate As DateTime
    Private _enddate As DateTime
    Private _totalcost As Double
    Private _duration As Integer
    Private _fsccontribution As Double
    Private _ofcontribution As Double
    Private _attachment As String
    Private _criticalpath As Boolean
    Private _requiresapproval As Boolean
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    ' para manejo de versiones
    Private _idKey As Integer
    Private _isLastVersion As Boolean

    'para el manejo de fases
    Private _idphase As Integer

    Private _username As String
    Private _responsiblename As String
    Private _activitytitle As String
    Private _componentname As String
    Private _objectivename As String
    Private _projectname As String

    'TODO: 50 CAMBIO PARA CREAR EL CAMPO RESPONSABLE
    'AUTOR:GERMNA RODRIGUEZ 7/08/2013
    Private _reponsible As String

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
    Public Property idactivity() As Integer
        Get
            Return Me._idactivity
        End Get
        Set(ByVal value As Integer)
            Me._idactivity = value
        End Set
    End Property
    Public Property type() As Integer
        Get
            Return Me._type
        End Get
        Set(ByVal value As Integer)
            Me._type = value
        End Set
    End Property
    Public Property number() As String
        Get
            Return Me._number
        End Get
        Set(ByVal value As String)
            Me._number = value
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
    Public Property idresponsible() As Integer
        Get
            Return Me._idresponsible
        End Get
        Set(ByVal value As Integer)
            Me._idresponsible = value
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
    Public Property totalcost() As Double
        Get
            Return Me._totalcost
        End Get
        Set(ByVal value As Double)
            Me._totalcost = value
        End Set
    End Property
    Public Property duration() As Integer
        Get
            Return Me._duration
        End Get
        Set(ByVal value As Integer)
            Me._duration = value
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
    Public Property ofcontribution() As Double
        Get
            Return Me._ofcontribution
        End Get
        Set(ByVal value As Double)
            Me._ofcontribution = value
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
    Public Property criticalpath() As Boolean
        Get
            Return Me._criticalpath
        End Get
        Set(ByVal value As Boolean)
            Me._criticalpath = value
        End Set
    End Property
    Public Property requiresapproval() As Boolean
        Get
            Return Me._requiresapproval
        End Get
        Set(ByVal value As Boolean)
            Me._requiresapproval = value
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
    Public Property RESPONSIBLENAME() As String
        Get
            Return Me._responsiblename
        End Get
        Set(ByVal value As String)
            Me._responsiblename = value
        End Set
    End Property
    Public Property ACTIVITYTITLE() As String
        Get
            Return Me._activitytitle
        End Get
        Set(ByVal value As String)
            Me._activitytitle = value
        End Set
    End Property

    Public Property COMPONENTNAME() As String
        Get
            Return Me._componentname
        End Get
        Set(ByVal value As String)
            Me._componentname = value
        End Set
    End Property

    Public Property OBJECTIVENAME() As String
        Get
            Return Me._objectivename
        End Get
        Set(ByVal value As String)
            Me._objectivename = value
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

    Public Property idphase() As Integer
        Get
            Return Me._idphase
        End Get
        Set(ByVal value As Integer)
            Me._idphase = value
        End Set
    End Property

    Public Property reponsible() As String
        Get
            Return Me._reponsible
        End Get
        Set(ByVal value As String)
            Me._reponsible = value
        End Set
    End Property

#End Region

End Class
