Imports Microsoft.VisualBasic

Public Class SupervisorByContractRequestEntity

#Region "Campos"

    Private _id As Integer
    Private _third_id As Integer
    Private _contractrequest_id As Integer

#End Region

#Region "Propiedades"

    Public ReadOnly Property Id() As Integer
        Get
            Return Me._id
        End Get
    End Property

    Public Property Third_Id() As Integer
        Get
            Return Me._third_id
        End Get
        Set(ByVal value As Integer)
            Me._third_id = value
        End Set
    End Property

    Public Property ContractRequest_Id() As Integer
        Get
            Return Me._contractrequest_id
        End Get
        Set(ByVal value As Integer)
            Me._contractrequest_id = value
        End Set
    End Property

#End Region

End Class