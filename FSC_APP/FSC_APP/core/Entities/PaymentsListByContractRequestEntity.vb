Imports Microsoft.VisualBasic

Public Class PaymentsListByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _value As Double
    Private _percentage As Double
    Private _datePaymentsList As DateTime
    Private _finalPaymentDate As DateTime
    Private _finalPaymentValue As Double

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
    Public Property idcontractrequest() As Integer
        Get
            Return Me._idcontractrequest
        End Get
        Set(ByVal value As Integer)
            Me._idcontractrequest = value
        End Set
    End Property
    Public Property value() As Double
        Get
            Return Me._value
        End Get
        Set(ByVal value As Double)
            Me._value = value
        End Set
    End Property
    Public Property percentage() As Double
        Get
            Return Me._percentage
        End Get
        Set(ByVal value As Double)
            Me._percentage = value
        End Set
    End Property
    Public Property datePaymentsList() As DateTime
        Get
            Return Me._datePaymentsList
        End Get
        Set(ByVal value As DateTime)
            Me._datePaymentsList = value
        End Set
    End Property
    Public Property finalPaymentDate() As DateTime
        Get
            Return Me._finalPaymentDate
        End Get
        Set(ByVal value As DateTime)
            Me._finalPaymentDate = value
        End Set
    End Property
    Public Property finalPaymentValue() As Double
        Get
            Return Me._finalPaymentValue
        End Get
        Set(ByVal value As Double)
            Me._finalPaymentValue = value
        End Set
    End Property

#End Region

End Class
