Imports Microsoft.VisualBasic

Public Class UsersByGroupEntity
#Region "Campos"

    ' definicion de campos
    Private _IDApplicationUser As Integer
    Private _code As String

#End Region

#Region "Propiedades"
    Public Property IDApplicationUser() As Integer
        Get
            Return _IDApplicationUser
        End Get
        Set(ByVal Value As Integer)
            _IDApplicationUser = Value
        End Set
    End Property

    Public Property code() As String
        Get
            Return _code
        End Get
        Set(ByVal Value As String)
            _code = Value
        End Set
    End Property
#End Region
End Class
