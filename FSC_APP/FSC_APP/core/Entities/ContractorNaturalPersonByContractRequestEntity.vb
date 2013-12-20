Imports Microsoft.VisualBasic

Public Class ContractorNaturalPersonByContractRequestEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idcontractrequest As Integer
    Private _nit As String
    Private _contractorname As String

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
    Public Property nit() As String
        Get
            Return Me._nit
        End Get
        Set(ByVal value As String)
            Me._nit = value
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

#End Region

End Class
