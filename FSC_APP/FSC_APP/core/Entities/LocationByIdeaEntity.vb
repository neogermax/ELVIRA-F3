Imports Microsoft.VisualBasic

Public Class LocationByIdeaEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _ididea As Integer
    Private _depto As DeptoEntity
    Private _city As CityEntity

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
    Public Property ididea() As Integer
        Get
            Return Me._ididea
        End Get
        Set(ByVal value As Integer)
            Me._ididea = value
        End Set
    End Property
    Public Property DEPTO() As DeptoEntity
        Get
            Return Me._depto
        End Get
        Set(ByVal value As DeptoEntity)
            Me._depto = value
        End Set
    End Property
    Public Property CITY() As CityEntity
        Get
            Return Me._city
        End Get
        Set(ByVal value As CityEntity)
            Me._city = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        Me._depto = New DeptoEntity()
        Me._city = New CityEntity()
    End Sub

#End Region

End Class
