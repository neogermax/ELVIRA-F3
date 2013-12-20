Imports Microsoft.VisualBasic

Public Class ContractExecutionEntity

#Region "Campos"

    ' campos
    Private _idcontractrequest As Integer
    Private _startdate As DateTime
    Private _paymentdate As DateTime
    Private _contractnumber As String
    Private _ordernumber As String
    Private _closingcomments As String
    Private _closingdate As DateTime
    Private _finalpaymentdate As DateTime
    Private _value As Double
    Private _iduser As Integer
    Private _createdate As DateTime

    'Atributos adicionales
    Private _username As String
    Private _idcontractrequestOld As Integer
    Private _paymentsList As List(Of PaymentsListByContractRequestEntity)

#End Region

#Region "Propiedades"

    Public Property idcontractrequest() As Integer
        Get
            Return Me._idcontractrequest
        End Get
        Set(ByVal value As Integer)
            Me._idcontractrequest = value
        End Set
    End Property
    Public Property startdate() As DateTime
        Get
            Return Me._startdate
        End Get
        Set(ByVal value As DateTime)
            Me._startdate = value
        End Set
    End Property
    Public Property paymentdate() As DateTime
        Get
            Return Me._paymentdate
        End Get
        Set(ByVal value As DateTime)
            Me._paymentdate = value
        End Set
    End Property
    Public Property contractnumber() As String
        Get
            Return Me._contractnumber
        End Get
        Set(ByVal value As String)
            Me._contractnumber = value
        End Set
    End Property
    Public Property ordernumber() As String
        Get
            Return Me._ordernumber
        End Get
        Set(ByVal value As String)
            Me._ordernumber = value
        End Set
    End Property
    Public Property closingcomments() As String
        Get
            Return Me._closingcomments
        End Get
        Set(ByVal value As String)
            Me._closingcomments = value
        End Set
    End Property
    Public Property closingdate() As DateTime
        Get
            Return Me._closingdate
        End Get
        Set(ByVal value As DateTime)
            Me._closingdate = value
        End Set
    End Property
    Public Property finalpaymentdate() As DateTime
        Get
            Return Me._finalpaymentdate
        End Get
        Set(ByVal value As DateTime)
            Me._finalpaymentdate = value
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
    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
        End Set
    End Property
    Public Property createdate() As DateTime
        Get
            Return Me._createdate
        End Get
        Set(ByVal value As DateTime)
            Me._createdate = value
        End Set
    End Property

    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property PAYMENTSLIST() As List(Of PaymentsListByContractRequestEntity)
        Get
            Return Me._paymentsList
        End Get
        Set(ByVal value As List(Of PaymentsListByContractRequestEntity))
            Me._paymentsList = value
        End Set
    End Property
    Public Property IDCONTRACTREQUESTOLD() As Integer
        Get
            Return Me._idcontractrequestOld
        End Get
        Set(ByVal value As Integer)
            Me._idcontractrequestOld = value
        End Set
    End Property


#End Region

End Class
