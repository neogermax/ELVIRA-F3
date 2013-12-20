Imports Microsoft.VisualBasic

Public Class AddresseeEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _name As String
    Private _email As String
    Private _idusergroup As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    'Atributos adicionales
    Private _username As String
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
    Public Property name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return Me._email
        End Get
        Set(ByVal value As String)
            Me._email = value
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
