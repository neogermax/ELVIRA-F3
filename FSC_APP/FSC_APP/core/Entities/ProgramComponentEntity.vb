Imports Microsoft.VisualBasic

Public Class ProgramComponentEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _description As String
    Private _idProgram As Integer
    Private _idresponsible As Integer
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _username As String
    Private _Programname As String
    Private _responsiblename As String

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
    Public Property description() As String
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
        End Set
    End Property
    Public Property idProgram() As Integer
        Get
            Return Me._idProgram
        End Get
        Set(ByVal value As Integer)
            Me._idProgram = value
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

    Public Property ProgramNAME() As String
        Get
            Return Me._Programname
        End Get
        Set(ByVal value As String)
            Me._Programname = value
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

#End Region

End Class
