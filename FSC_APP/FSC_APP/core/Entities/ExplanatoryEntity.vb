Imports Microsoft.VisualBasic

Public Class ExplanatoryEntity

#Region "campos"
    'campos
    Private _id As Integer
    Private _observation As String
    Private _fecha As DateTime
    Private _idproject As Integer

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
    Public Property observation() As String
        Get
            Return Me._observation
        End Get
        Set(ByVal value As String)
            Me._observation = value
        End Set
    End Property
    Public Property fecha() As DateTime
        Get
            Return Me._fecha
        End Get
        Set(ByVal value As DateTime)
            Me._fecha = value
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
    

#End Region


End Class
