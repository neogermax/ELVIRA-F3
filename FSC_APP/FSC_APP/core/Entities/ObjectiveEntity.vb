Imports Microsoft.VisualBasic

Public Class ObjectiveEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _idproject As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    ' para manejo de versiones
    Private _idKey As Integer
    Private _isLastVersion As Boolean

    'para el manejo de fases
    Private _idphase As Integer

    ' para mostrar en las listas
    Private _username As String
    Private _projectname As String

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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
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

#End Region

End Class
