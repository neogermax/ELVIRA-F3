Imports Microsoft.VisualBasic

Public Class IndicatorByAccumulationIndicatorSetEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idaccumulationindicatorset As Integer
    Private _idindicator As Integer

    Private _indicatorcode As String

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
    Public Property idaccumulationindicatorset() As Integer
        Get
            Return Me._idaccumulationindicatorset
        End Get
        Set(ByVal value As Integer)
            Me._idaccumulationindicatorset = value
        End Set
    End Property
    Public Property idindicator() As Integer
        Get
            Return Me._idindicator
        End Get
        Set(ByVal value As Integer)
            Me._idindicator = value
        End Set
    End Property
    Public Property INDICATORCODE() As String
        Get
            Return Me._indicatorcode
        End Get
        Set(ByVal value As String)
            Me._indicatorcode = value
        End Set
    End Property

#End Region

End Class
