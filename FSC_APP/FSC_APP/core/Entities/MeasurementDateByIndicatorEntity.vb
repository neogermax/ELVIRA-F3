Imports Microsoft.VisualBasic

Public Class MeasurementDateByIndicatorEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idindicator As Integer
    Private _measurementdate As DateTime
    Private _measure As String
    Private _measureType As String
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
    Public Property idindicator() As Integer
        Get
            Return Me._idindicator
        End Get
        Set(ByVal value As Integer)
            Me._idindicator = value
        End Set
    End Property

    Public Property measure() As String
        Get
            Return Me._measure
        End Get
        Set(ByVal value As String)
            Me._measure = value
        End Set
    End Property
    Public Property measuretype() As String
        Get
            Return Me._measureType
        End Get
        Set(ByVal value As String)
            Me._measureType = value
        End Set
    End Property
    Public Property measurementdate() As DateTime
        Get
            Return Me._measurementdate
        End Get
        Set(ByVal value As DateTime)
            Me._measurementdate = value
        End Set
    End Property
    Public Overloads Function Equals(ByVal other As MeasurementDateByIndicatorEntity) As Boolean
        If Me._id = other._id And Me._idindicator = other._idindicator And Me._measurementdate = other._measurementdate Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
