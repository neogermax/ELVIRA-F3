Imports Microsoft.VisualBasic

Public Class ForumEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _subject As String
    Private _message As String
    Private _attachment As String
    Private _updateddate As DateTime
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _replylist As List(Of ReplyEntity)

    Private _username As String
    Private _projectname As String
    Private _lastreplyusername As String
    Private _replycount As Integer
    Private _lastreplycreatedate As DateTime


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
    Public Property subject() As String
        Get
            Return Me._subject
        End Get
        Set(ByVal value As String)
            Me._subject = value
        End Set
    End Property
    Public Property message() As String
        Get
            Return Me._message
        End Get
        Set(ByVal value As String)
            Me._message = value
        End Set
    End Property
    Public Property attachment() As String
        Get
            Return Me._attachment
        End Get
        Set(ByVal value As String)
            Me._attachment = value
        End Set
    End Property
    Public Property updateddate() As DateTime
        Get
            Return Me._updateddate
        End Get
        Set(ByVal value As DateTime)
            Me._updateddate = value
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
    Public Property REPLYLIST() As List(Of ReplyEntity)
        Get
            Return Me._replylist
        End Get
        Set(ByVal value As List(Of ReplyEntity))
            Me._replylist = value
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
    Public Property LASTREPLYUSERNAME() As String
        Get
            Return Me._lastreplyusername
        End Get
        Set(ByVal value As String)
            Me._lastreplyusername = value
        End Set
    End Property
    Public Property REPLYCOUNT() As Integer
        Get
            Return Me._replycount
        End Get
        Set(ByVal value As Integer)
            Me._replycount = value
        End Set
    End Property
    Public Property LASTREPLYCREATEDATE() As DateTime
        Get
            Return Me._lastreplycreatedate
        End Get
        Set(ByVal value As DateTime)
            Me._lastreplycreatedate = value
        End Set
    End Property

#End Region

End Class
