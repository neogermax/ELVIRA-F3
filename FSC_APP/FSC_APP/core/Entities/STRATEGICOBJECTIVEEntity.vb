Imports Microsoft.VisualBasic

Public Class STRATEGICOBJECTIVEEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _year As Integer
    Private _idperspective As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _username As String
    Private _perspectivename As String

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
    Public Property year() As Integer
        Get
            Return Me._year
        End Get
        Set(ByVal value As Integer)
            Me._year = value
        End Set
    End Property
    Public Property idperspective() As Integer
        Get
            Return Me._idperspective
        End Get
        Set(ByVal value As Integer)
            Me._idperspective = value
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

    Public Property PERSPECTIVENAME() As String
        Get
            Return Me._perspectivename
        End Get
        Set(ByVal value As String)
            Me._perspectivename = value
        End Set
    End Property

#End Region

End Class
