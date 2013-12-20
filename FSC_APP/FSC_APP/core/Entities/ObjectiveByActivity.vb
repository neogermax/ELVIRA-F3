Imports Microsoft.VisualBasic

Public Class ObjectiveByActivityEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idactivity As Integer
    Private _idobjective As Integer

    'Atributos agregados
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
    Public Property idactivity() As Integer
        Get
            Return Me._idactivity
        End Get
        Set(ByVal value As Integer)
            Me._idactivity = value
        End Set
    End Property
    Public Property idobjective() As Integer
        Get
            Return Me._idobjective
        End Get
        Set(ByVal value As Integer)
            Me._idobjective = value
        End Set
    End Property

    Public Property IDPROJECT() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property

 

#End Region

End Class

