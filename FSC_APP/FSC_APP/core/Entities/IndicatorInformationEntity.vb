Imports Microsoft.VisualBasic

Public Class IndicatorInformationEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idmeasurementdatebyindicator As Integer
    Private _idindicator As Integer
    Private _description As String
    Private _goal As String
    Private _value As String
    Private _comments As String
    Private _registrationdate As DateTime
    Private _measuredate As DateTime
    Private _iduser As Integer

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
    Public Property idmeasurementdatebyindicator() As Integer
        Get
            Return Me._idmeasurementdatebyindicator
        End Get
        Set(ByVal value As Integer)
            Me._idmeasurementdatebyindicator = value
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
    Public Property description() As String
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
        End Set
    End Property
    Public Property goal() As String
        Get
            Return Me._goal
        End Get
        Set(ByVal value As String)
            Me._goal = value
        End Set
    End Property
    Public Property value() As String
        Get
            Return Me._value
        End Get
        Set(ByVal value As String)
            Me._value = value
        End Set
    End Property
    Public Property comments() As String
        Get
            Return Me._comments
        End Get
        Set(ByVal value As String)
            Me._comments = value
        End Set
    End Property
    Public Property registrationdate() As DateTime
        Get
            Return Me._registrationdate
        End Get
        Set(ByVal value As DateTime)
            Me._registrationdate = value
        End Set
    End Property
    Public Property measuredate() As DateTime
        Get
            Return Me._measuredate
        End Get
        Set(ByVal value As DateTime)
            Me._measuredate = value
        End Set
    End Property


    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
        End Set
    End Property

#End Region

End Class
