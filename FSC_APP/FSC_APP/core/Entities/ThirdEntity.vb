Imports Microsoft.VisualBasic

Public Class ThirdEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String

    'TODO:33 CAMBIOS DE TABLA TERCEROS 
    'AUTOR:GERMAN RODRIGUEZ 2/07/2013 MGgroup
    'descripcion: POR FSC FASE II 1/2

    Private _contact As String
    Private _documents As String
    Private _phone As String
    Private _email As String

    'TODO:33 CAMBIOS DE TABLA TERCEROS 
    'AUTOR:GERMAN RODRIGUEZ 2/07/2013 MGgroup
    'cierre de cambios

    
    Private _actions As String
    Private _experiences As String
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime

    Private _username As String

    'TODO:34 CAMBIOS DE TABLA TERCEROS
    'AUTOR:Autor: Pedro Cruz MGgroup
    'descripcion: POR FSC FASE II 1/2

    Private _personanatural As Integer
    Private _representantelegal As String

    'TODO:34 CAMBIOS DE TABLA TERCEROS
    'AUTOR:Autor: Pedro Cruz MGgroup
    'cierre de cambios 

    Private _tipodocumento As String
    Private _docrepresentante As String
    Private _direccion As String
    Private _sex As String

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
    Public Property contact() As String
        Get
            Return Me._contact
        End Get
        Set(ByVal value As String)
            Me._contact = value
        End Set
    End Property

    Public Property documents() As String
        Get
            Return Me._documents
        End Get
        Set(ByVal value As String)
            Me._documents = value
        End Set
    End Property
    Public Property phone() As String
        Get
            Return Me._phone
        End Get
        Set(ByVal value As String)
            Me._phone = value
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
    Public Property actions() As String
        Get
            Return Me._actions
        End Get
        Set(ByVal value As String)
            Me._actions = value
        End Set
    End Property
    Public Property experiences() As String
        Get
            Return Me._experiences
        End Get
        Set(ByVal value As String)
            Me._experiences = value
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

    Public Property PersonaNatural() As Integer
        Get
            Return Me._personanatural
        End Get
        Set(ByVal value As Integer)
            Me._personanatural = value
        End Set
    End Property
    Public Property representantelegal() As String
        Get
            Return Me._representantelegal
        End Get
        Set(ByVal value As String)
            Me._representantelegal = value
        End Set
    End Property
    Public Property tipodocumento() As String
        Get
            Return Me._tipodocumento
        End Get
        Set(ByVal value As String)
            Me._tipodocumento = value
        End Set
    End Property
    Public Property docrepresentante() As String
        Get
            Return Me._docrepresentante
        End Get
        Set(ByVal value As String)
            Me._docrepresentante = value
        End Set
    End Property
    Public Property direccion() As String
        Get
            Return Me._direccion
        End Get
        Set(ByVal value As String)
            Me._direccion = value
        End Set
    End Property
    Public Property sex() As String
        Get
            Return Me._sex
        End Get
        Set(ByVal value As String)
            Me._sex = value
        End Set
    End Property


#End Region


End Class
