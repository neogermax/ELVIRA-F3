Imports Microsoft.VisualBasic

Public Class CurrencyEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _code As String
    Private _name As String
    Private _priceprefix As String
    Private _enabled As String

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
    Public Property priceprefix() As String
        Get
            Return Me._priceprefix
        End Get
        Set(ByVal value As String)
            Me._priceprefix = value
        End Set
    End Property
    Public Property enabled() As String
        Get
            Return Me._enabled
        End Get
        Set(ByVal value As String)
            Me._enabled = value
        End Set
    End Property

#End Region

End Class
