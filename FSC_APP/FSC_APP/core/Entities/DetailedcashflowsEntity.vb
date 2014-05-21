Imports Microsoft.VisualBasic

Public Class DetailedcashflowsEntity

#Region "Campos"

    Private _id As Integer
    Private _IdIdea As Integer
    Private _IdProject As Integer
    Private _N_pago As Integer
    Private _IdAportante As Integer
    Private _Aportante As String
    Private _Desembolso As String
    Private _mother As Integer


#End Region

#Region "propiedades"

    Public Property id() As Integer
        Get
            Return Me._id
        End Get
        Set(ByVal value As Integer)
            Me._id = value
        End Set
    End Property
    Public Property IdIdea() As Integer
        Get
            Return Me._IdIdea
        End Get
        Set(ByVal value As Integer)
            Me._IdIdea = value
        End Set
    End Property
    Public Property IdProject() As Integer
        Get
            Return Me._IdProject
        End Get
        Set(ByVal value As Integer)
            Me._IdProject = value
        End Set
    End Property
    Public Property N_pago() As Integer
        Get
            Return Me._N_pago
        End Get
        Set(ByVal value As Integer)
            Me._N_pago = value
        End Set
    End Property
    Public Property IdAportante() As Integer
        Get
            Return Me._IdAportante
        End Get
        Set(ByVal value As Integer)
            Me._IdAportante = value
        End Set
    End Property
    Public Property Aportante() As String
        Get
            Return Me._Aportante
        End Get
        Set(ByVal value As String)
            Me._Aportante = value
        End Set
    End Property
    Public Property Desembolso() As String
        Get
            Return Me._Desembolso
        End Get
        Set(ByVal value As String)
            Me._Desembolso = value
        End Set
    End Property
    Public Property mother() As Integer
        Get
            Return Me._mother
        End Get
        Set(ByVal value As Integer)
            Me._mother = value
        End Set
    End Property

#End Region


End Class