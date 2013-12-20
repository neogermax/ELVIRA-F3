Imports Microsoft.VisualBasic

Public Class InquestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _idproject As Integer
    Private _projectphase As String
    Private _idusergroup As Integer
    Private _enabled As Boolean
    Private _createdate As DateTime

    'Atributos adicionales
    Private _username As String
    Private _projectname As String
    Private _usergroupname As String

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
    Public Property projectphase() As String
        Get
            Return Me._projectphase
        End Get
        Set(ByVal value As String)
            Me._projectphase = value
        End Set
    End Property
    Public Property idusergroup() As Integer
        Get
            Return Me._idusergroup
        End Get
        Set(ByVal value As Integer)
            Me._idusergroup = value
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
    Public Property USERGROUPNAME() As String
        Get
            Return Me._usergroupname
        End Get
        Set(ByVal value As String)
            Me._usergroupname = value
        End Set
    End Property

#End Region

End Class
