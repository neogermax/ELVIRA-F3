Imports Microsoft.VisualBasic

Public Class ContractorLegalEntityByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _entitynamedescription As String
    Private _nit As String
    Private _legalrepresentative As String
    Private _contractorname As String
    Private _identificationnumber As String

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
    Public Property entitynamedescription() As String
        Get
            Return Me._entitynamedescription
        End Get
        Set(ByVal value As String)
            Me._entitynamedescription = value
        End Set
    End Property
    Public Property nit() As String
        Get
            Return Me._nit
        End Get
        Set(ByVal value As String)
            Me._nit = value
        End Set
    End Property
    Public Property legalrepresentative() As String
        Get
            Return Me._legalrepresentative
        End Get
        Set(ByVal value As String)
            Me._legalrepresentative = value
        End Set
    End Property
    Public Property contractorname() As String
        Get
            Return Me._contractorname
        End Get
        Set(ByVal value As String)
            Me._contractorname = value
        End Set
    End Property
    Public Property identificationnumber() As String
        Get
            Return Me._identificationnumber
        End Get
        Set(ByVal value As String)
            Me._identificationnumber = value
        End Set
    End Property

#End Region

End Class
