Imports Microsoft.VisualBasic

Public Class AlertsEntity

#Region "Campos"
    'Campos
    Private _alert_id As Integer
    Private _name As String
    Private _users As String
    Private _groups As String
    Private _days As Integer
    Private _subject As String
    Private _message As String

#End Region

#Region "Propiedades"

    Public ReadOnly Property alert_id() As Integer
        Get
            Return Me._alert_id
        End Get
    End Property

    Public Property name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property

    Public Property users() As String
        Get
            Return Me._users
        End Get
        Set(ByVal value As String)
            Me._users = value
        End Set
    End Property

    Public Property groups() As String
        Get
            Return Me._groups
        End Get
        Set(ByVal value As String)
            Me._groups = value
        End Set
    End Property

    Public Property days() As Integer
        Get
            Return Me._days
        End Get
        Set(ByVal value As Integer)
            Me._days = value
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

#End Region

End Class