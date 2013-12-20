Imports Microsoft.VisualBasic

Public Class MenuEntity

#Region "Campos"

    Private _Id As Integer
    Private _TextField As String
    Private _URL As String
    Private _Enabled As String
    Private _SortOrden As Integer

#End Region

#Region "Propiedades"

    Public Property iId() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property sTextField() As String
        Get
            Return _TextField
        End Get
        Set(ByVal Value As String)
            _TextField = Value
        End Set
    End Property

    Public Property sURL() As String
        Get
            Return _URL
        End Get
        Set(ByVal Value As String)
            _URL = Value
        End Set
    End Property

    Public Property iSortOrden() As Integer
        Get
            Return _SortOrden
        End Get
        Set(ByVal Value As Integer)
            _SortOrden = Value
        End Set
    End Property

    Public Property sEnabled() As String
        Get
            Return _Enabled
        End Get
        Set(ByVal Value As String)
            _Enabled = Value
        End Set
    End Property

#End Region

End Class