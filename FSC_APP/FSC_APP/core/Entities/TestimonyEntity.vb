Imports Microsoft.VisualBasic

Public Class TestimonyEntity
#Region "Campos"

    ' campos
    Private _id As Integer
    Private _name As String
    Private _age As String
    Private _sex As String
    Private _phone As String
    Private _description As String
    Private _email As String
    Private _projectrole As String
    Private _departamento As String

    'Atributos agregados
    Private _idexecution As Integer
    Private _idcity As Integer
    Private _documentList As List(Of DocumentsEntity)
    Private _documentsByEntityList As List(Of DocumentsByEntityEntity)
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
    Public Property idexecution() As Integer
        Get
            Return Me._idexecution
        End Get
        Set(ByVal value As Integer)
            Me._idexecution = value
        End Set
    End Property
    Public Property idcity() As Integer
        Get
            Return Me._idcity
        End Get
        Set(ByVal value As Integer)
            Me._idcity = value
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


    Public Property age() As String
        Get
            Return Me._age
        End Get
        Set(ByVal value As String)
            Me._age = value
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

    Public Property DEPARTAMENTO() As String
        Get
            Return Me._departamento
        End Get
        Set(ByVal value As String)
            Me._departamento = value
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


    Public Property description() As String
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
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

    Public Property projectrole() As String
        Get
            Return Me._projectrole
        End Get
        Set(ByVal value As String)
            Me._projectrole = value
        End Set
    End Property






#End Region
End Class
