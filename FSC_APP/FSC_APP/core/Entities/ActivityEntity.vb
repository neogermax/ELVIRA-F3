Imports Microsoft.VisualBasic

Public Class ActivityEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _number As String
    Private _title As String
    Private _idcomponent As Integer
    Private _description As String
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _idproject As Integer
    Private _idobjective As Integer
    Private _criticalpath As Boolean
    Private _username As String
    Private _componentname As String
    Private _projectname As String
    Private _objectivename As String
    Private _ObjectiveByActivityList As List(Of ObjectiveByActivityEntity)

    ' para manejo de versiones
    Private _idKey As Integer
    Private _isLastVersion As Boolean

    'para el manejo de fases
    Private _idphase As Integer

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
    Public Property number() As String
        Get
            Return Me._number
        End Get
        Set(ByVal value As String)
            Me._number = value
        End Set
    End Property
    Public Property title() As String
        Get
            Return Me._title
        End Get
        Set(ByVal value As String)
            Me._title = value
        End Set
    End Property
    Public Property idcomponent() As Integer
        Get
            Return Me._idcomponent
        End Get
        Set(ByVal value As Integer)
            Me._idcomponent = value
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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property idobjective() As Integer
        Get
            Return Me._idobjective
        End Get
        Set(ByVal value As Integer)
            Me._idobjective = value
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
   
    Public Property COMPONENTNAME() As String
        Get
            Return Me._componentname
        End Get
        Set(ByVal value As String)
            Me._componentname = value
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

    Public Property OBJECTIVENAME() As String
        Get
            Return Me._objectivename
        End Get
        Set(ByVal value As String)
            Me._objectivename = value
        End Set
    End Property
    Public Property CRITICALPATH() As Boolean
        Get
            Return Me._criticalpath
        End Get
        Set(ByVal value As Boolean)
            Me._criticalpath = value
        End Set
    End Property


    Public Property OBJECTIVEBYACTIVITYLIST() As List(Of ObjectiveByActivityEntity)
        Get
            Return Me._ObjectiveByActivityList
        End Get
        Set(ByVal value As List(Of ObjectiveByActivityEntity))
            Me._ObjectiveByActivityList = value
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

#End Region

End Class
