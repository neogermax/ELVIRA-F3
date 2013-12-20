Imports Microsoft.VisualBasic

Public Class MitigationByRiskEntity
#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idrisk As Integer
    Private _idMitigation As Integer
    Private _Code As String

    Private _riskname As String
    Private _mitigationname As String

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
    Public Property idmitigation() As Integer
        Get
            Return Me._idMitigation
        End Get
        Set(ByVal value As Integer)
            Me._idMitigation = value
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

    Public Property MITIGATIONNAME() As String
        Get
            Return Me._mitigationname
        End Get
        Set(ByVal value As String)
            Me._mitigationname = value
        End Set
    End Property


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
