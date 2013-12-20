Imports Microsoft.VisualBasic

Public Class ReplyEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idforum As Integer
    Private _iduser As Integer
    Private _subject As String
    Private _attachment As String
    Private _updatedate As DateTime
    Private _createdate As DateTime

    Private _forumsubject As String
    Private _username As String

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
    Public Property idforum() As Integer
        Get
            Return Me._idforum
        End Get
        Set(ByVal value As Integer)
            Me._idforum = value
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
    Public Property subject() As String
        Get
            Return Me._subject
        End Get
        Set(ByVal value As String)
            Me._subject = value
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
    Public Property updatedate() As DateTime
        Get
            Return Me._updatedate
        End Get
        Set(ByVal value As DateTime)
            Me._updatedate = value
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
    Public Property FORUMSUBJECT() As String
        Get
            Return Me._forumsubject
        End Get
        Set(ByVal value As String)
            Me._forumsubject = value
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

#End Region

End Class
