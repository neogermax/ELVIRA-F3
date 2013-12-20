Imports Microsoft.VisualBasic

Public Class SubjectAndValueByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _subjectcontract As String
    Private _productsordeliverables As String
    Private _contractvalue As Double
    Private _contributionamount As Double
    Private _feesconsultantbyinstitution As Double
    Private _totalfeesintegralconsultant As Double
    Private _contributionamountrecipientinstitution As Double
    Private _idcurrency As Integer

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
    Public Property subjectcontract() As String
        Get
            Return Me._subjectcontract
        End Get
        Set(ByVal value As String)
            Me._subjectcontract = value
        End Set
    End Property
    Public Property productsordeliverables() As String
        Get
            Return Me._productsordeliverables
        End Get
        Set(ByVal value As String)
            Me._productsordeliverables = value
        End Set
    End Property
    Public Property contractvalue() As Double
        Get
            Return Me._contractvalue
        End Get
        Set(ByVal value As Double)
            Me._contractvalue = value
        End Set
    End Property
    Public Property contributionamount() As Double
        Get
            Return Me._contributionamount
        End Get
        Set(ByVal value As Double)
            Me._contributionamount = value
        End Set
    End Property
    Public Property feesconsultantbyinstitution() As Double
        Get
            Return Me._feesconsultantbyinstitution
        End Get
        Set(ByVal value As Double)
            Me._feesconsultantbyinstitution = value
        End Set
    End Property
    Public Property totalfeesintegralconsultant() As Double
        Get
            Return Me._totalfeesintegralconsultant
        End Get
        Set(ByVal value As Double)
            Me._totalfeesintegralconsultant = value
        End Set
    End Property
    Public Property contributionamountrecipientinstitution() As Double
        Get
            Return Me._contributionamountrecipientinstitution
        End Get
        Set(ByVal value As Double)
            Me._contributionamountrecipientinstitution = value
        End Set
    End Property
    Public Property idcurrency() As Integer
        Get
            Return Me._idcurrency
        End Get
        Set(ByVal value As Integer)
            Me._idcurrency = value
        End Set
    End Property

#End Region

End Class
