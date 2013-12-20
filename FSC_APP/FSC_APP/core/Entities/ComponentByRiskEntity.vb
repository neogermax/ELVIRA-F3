Imports Microsoft.VisualBasic

Public Class ComponentByRiskEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idrisk As Integer
    Private _idcomponent As Integer
    Private _Code As String

    Private _riskname As String
    Private _componentname As String

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
    Public Property idrisk() As Integer
        Get
            Return Me._idrisk
        End Get
        Set(ByVal value As Integer)
            Me._idrisk = value
        End Set
    End Property
    Public Property idcomponent() As Integer
        Get
            Return Me._idcomponent
        End Get
        Set(ByVal value As Integer)
            Me._idcomponent = value
        End Set
    End Property
    Public Property RISKNAME() As String
        Get
            Return Me._riskname
        End Get
        Set(ByVal value As String)
            Me._riskname = value
        End Set
    End Property

    Public Property COMPONENTNAME() As String
        Get
            Return Me._componentname
        End Get
        Set(ByVal value As String)
            Me._componentname = value
        End Set
    End Property

    Public Overloads Function Equals(ByVal other As ComponentByRiskEntity) As Boolean
        If Me._id = other._id And Me._idrisk = other._idrisk And Me._idcomponent = other._idcomponent Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Property CODE() As String
        Get
            Return Me._Code
        End Get
        Set(ByVal value As String)
            Me._Code = value
        End Set

    End Property

#End Region

End Class
