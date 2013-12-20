Imports Microsoft.VisualBasic

Public Class ProjectLocationEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _idcity As Integer

    Private _iddepto As Integer
    Private _deptoname As String
    Private _cityname As String

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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property idcity() As Integer
        Get
            Return Me._idcity
        End Get
        Set(ByVal value As Integer)
            Me._idcity = value
        End Set
    End Property
    Public Property IDDEPTO() As Integer
        Get
            Return Me._iddepto
        End Get
        Set(ByVal value As Integer)
            Me._iddepto = value
        End Set
    End Property
    Public Property DEPTONAME() As String
        Get
            Return Me._deptoname
        End Get
        Set(ByVal value As String)
            Me._deptoname = value
        End Set
    End Property
    Public Property CITYNAME() As String
        Get
            Return Me._cityname
        End Get
        Set(ByVal value As String)
            Me._cityname = value
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
