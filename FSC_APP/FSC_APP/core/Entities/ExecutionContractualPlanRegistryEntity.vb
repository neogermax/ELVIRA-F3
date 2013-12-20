Imports Microsoft.VisualBasic

Public Class ExecutionContractualPlanRegistryEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _ExecutionContractualPlanRegistryEntityDetails As List(Of ExecutionContractualPlanRegistryDetailsEntity)
    Private _username As String

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
    Public Property username() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property ExecutionContractualPlanRegistryEntityDetails() As List(Of ExecutionContractualPlanRegistryDetailsEntity)
        Get
            Return Me._ExecutionContractualPlanRegistryEntityDetails
        End Get
        Set(ByVal value As List(Of ExecutionContractualPlanRegistryDetailsEntity))
            Me._ExecutionContractualPlanRegistryEntityDetails = value
        End Set
    End Property

#End Region

End Class
