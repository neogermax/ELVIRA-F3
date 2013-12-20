Imports Microsoft.VisualBasic

Public Class ExecutionEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _projectname As String
    Private _qualitativeindicators As String
    Private _learning As String
    Private _adjust As String
    Private _achievements As String
    Private _enable As Boolean
    Private _iduser As Integer
    Private _username As String
    Private _testimonyname As String
    Private _observation As String
    Private _createdate As DateTime
    Private _TestimonyList As List(Of TestimonyEntity)
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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
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
    Public Property qualitativeindicators() As String
        Get
            Return Me._qualitativeindicators
        End Get
        Set(ByVal value As String)
            Me._qualitativeindicators = value
        End Set
    End Property
    Public Property observation() As String
        Get
            Return Me._observation
        End Get
        Set(ByVal value As String)
            Me._observation = value
        End Set
    End Property
    Public Property learning() As String
        Get
            Return Me._learning
        End Get
        Set(ByVal value As String)
            Me._learning = value
        End Set
    End Property
    Public Property adjust() As String
        Get
            Return Me._adjust
        End Get
        Set(ByVal value As String)
            Me._adjust = value
        End Set
    End Property
    Public Property achievements() As String
        Get
            Return Me._achievements
        End Get
        Set(ByVal value As String)
            Me._achievements = value
        End Set
    End Property
    Public Property enable() As Boolean
        Get
            Return Me._enable
        End Get
        Set(ByVal value As Boolean)
            Me._enable = value
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
    Public Property createdate() As DateTime
        Get
            Return Me._createdate
        End Get
        Set(ByVal value As DateTime)
            Me._createdate = value
        End Set
    End Property


    Public Property TESTIMONYLIST() As List(Of TestimonyEntity)
        Get
            Return Me._TestimonyList
        End Get
        Set(ByVal value As List(Of TestimonyEntity))
            Me._TestimonyList = value
        End Set
    End Property


    Public Property TESTIMONYNAME() As String
        Get
            Return Me._testimonyname
        End Get
        Set(ByVal value As String)
            Me._testimonyname = value
        End Set
    End Property

    Public Property DOCUMENTSBYEXECUTIONLIST() As List(Of DocumentsByEntityEntity)
        Get
            Return Me._documentsByEntityList
        End Get
        Set(ByVal value As List(Of DocumentsByEntityEntity))
            Me._documentsByEntityList = value
        End Set
    End Property

    Public Property DOCUMENTLIST() As List(Of DocumentsEntity)
        Get
            Return Me._documentList
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Me._documentList = value
        End Set
    End Property

#End Region

End Class
