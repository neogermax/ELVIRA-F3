Option Strict On

Imports Microsoft.VisualBasic

<Serializable()> _
Public Class Setting

#Region "Campos"

    Private _id As Integer
    Private _parameterName As String
    Private _parameterValue As String
    Private _idModule As Integer

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

    Public Property parameterName() As String
        Get
            Return Me._parameterName
        End Get
        Set(ByVal value As String)
            Me._parameterName = value
        End Set
    End Property

    Public Property parameterValue() As String
        Get
            Return Me._parameterValue
        End Get
        Set(ByVal value As String)
            Me._parameterValue = value
        End Set
    End Property

    Public Property idModule() As Integer
        Get
            Return Me._idModule
        End Get
        Set(ByVal value As Integer)
            Me._idModule = value
        End Set
    End Property

#End Region

End Class