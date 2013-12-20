Imports Microsoft.VisualBasic

Public Class PolizaDetailsEntity

#Region "Campos"

    Private _id As Integer
    Private _id_poliza As Integer
    Private _concepto As String
    Private _aseguradora As String


    Private _contractpolizadetailslist As List(Of PolizaDetailsEntity)


#End Region

#Region "Propiedades"

    Public Property Id() As Integer
        Get
            Return Me._Id
        End Get
        Set(ByVal value As Integer)
            Me._Id = value
        End Set
    End Property

    Public Property Id_Poliza() As Integer
        Get
            Return Me._Id_Poliza
        End Get
        Set(ByVal value As Integer)
            Me._Id_Poliza = value
        End Set
    End Property

    Public Property Concepto() As String
        Get
            Return Me._Concepto
        End Get
        Set(ByVal value As String)
            Me._Concepto = value
        End Set
    End Property

    Public Property aseguradora() As String
        Get
            Return Me._aseguradora
        End Get
        Set(ByVal value As String)
            Me._aseguradora = value
        End Set
    End Property


    Public Property CONTRACTPOLIZADETAILSLIST() As List(Of PolizaDetailsEntity)
        Get
            Return Me._contractpolizadetailslist
        End Get
        Set(ByVal value As List(Of PolizaDetailsEntity))
            Me._contractpolizadetailslist = value
        End Set
    End Property

#End Region

End Class
