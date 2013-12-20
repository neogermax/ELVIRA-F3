Imports Microsoft.VisualBasic

Public Class PolizaEntity

#Region "Campos"

    Private _id As Integer
    Private _numero_poliza As String
    Private _aseguradora As String
    Private _contrato_id As Integer
    Private _fecha_exp As DateTime
    Private _fecha_ven As DateTime

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

    Public Property numero_poliza() As String
        Get
            Return Me._numero_poliza
        End Get
        Set(ByVal value As String)
            Me._numero_poliza = value
        End Set
    End Property

    Public Property aseguradora() As String
        Get
            Return Me._aseguradora
        End Get
        Set(ByVal value As String)
            Me._aseguradora = value
        End Set
    End Property

    Public Property contrato_id() As Integer
        Get
            Return Me._contrato_id
        End Get
        Set(ByVal value As Integer)
            Me._contrato_id = value
        End Set
    End Property

    Public Property fecha_exp() As DateTime
        Get
            Return Me._fecha_exp
        End Get
        Set(ByVal value As DateTime)
            Me._fecha_exp = value
        End Set
    End Property

    Public Property fecha_ven() As DateTime
        Get
            Return Me._fecha_ven
        End Get
        Set(ByVal value As DateTime)
            Me._fecha_ven = value
        End Set
    End Property

#End Region

End Class
