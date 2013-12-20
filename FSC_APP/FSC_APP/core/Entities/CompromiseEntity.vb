Imports Microsoft.VisualBasic

Public Class CompromiseEntity

#Region "campos"
    Private _id As Integer
    Private _idproject As Integer
    Private _liabilities As String
    Private _responsible As String
    Private _tracingdate As DateTime
    Private _email As String
    Private _proceeding_log_id As Integer

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
    Public Property liabilities() As String
        Get
            Return Me._liabilities
        End Get
        Set(ByVal value As String)
            Me._liabilities = value
        End Set
    End Property
    Public Property responsible() As String
        Get
            Return Me._responsible
        End Get
        Set(ByVal value As String)
            Me._responsible = value
        End Set
    End Property
    Public Property tracingdate() As DateTime
        Get
            Return Me._tracingdate
        End Get
        Set(ByVal value As DateTime)
            Me._tracingdate = value
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
    Public Property proceeding_log_id() As Integer
        Get
            Return Me._proceeding_log_id
        End Get
        Set(ByVal value As Integer)
            Me._proceeding_log_id = value
        End Set
    End Property

#End Region
   
End Class
