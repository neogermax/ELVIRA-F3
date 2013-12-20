Imports Microsoft.VisualBasic

Public Class CloseRegistryEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _closingdate As DateTime
    Private _weakness As String
    Private _opportunity As String
    Private _strengths As String
    Private _learningfornewprojects As String
    Private _goodpractice As Boolean
    Private _registrationdate As DateTime
    Private _enabled As Boolean
    Private _iduser As Integer

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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property closingdate() As DateTime
        Get
            Return Me._closingdate
        End Get
        Set(ByVal value As DateTime)
            Me._closingdate = value
        End Set
    End Property
    Public Property weakness() As String
        Get
            Return Me._weakness
        End Get
        Set(ByVal value As String)
            Me._weakness = value
        End Set
    End Property
    Public Property opportunity() As String
        Get
            Return Me._opportunity
        End Get
        Set(ByVal value As String)
            Me._opportunity = value
        End Set
    End Property
    Public Property strengths() As String
        Get
            Return Me._strengths
        End Get
        Set(ByVal value As String)
            Me._strengths = value
        End Set
    End Property
    Public Property learningfornewprojects() As String
        Get
            Return Me._learningfornewprojects
        End Get
        Set(ByVal value As String)
            Me._learningfornewprojects = value
        End Set
    End Property
    Public Property goodpractice() As Boolean
        Get
            Return Me._goodpractice
        End Get
        Set(ByVal value As Boolean)
            Me._goodpractice = value
        End Set
    End Property
    Public Property registrationdate() As DateTime
        Get
            Return Me._registrationdate
        End Get
        Set(ByVal value As DateTime)
            Me._registrationdate = value
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
    Public ReadOnly Property GOODPRACTICETEXT() As String
        Get
            GOODPRACTICETEXT = ""
            If Me._goodpractice Then
                GOODPRACTICETEXT = "Si"
            Else
                GOODPRACTICETEXT = "No"
            End If
        End Get
    End Property

    Public ReadOnly Property ENABLEDTEXT() As String
        Get
            ENABLEDTEXT = ""
            If Me._enabled Then
                ENABLEDTEXT = "Habilitado"
            Else
                ENABLEDTEXT = "Deshabilitado"
            End If
        End Get
    End Property

#End Region

End Class
