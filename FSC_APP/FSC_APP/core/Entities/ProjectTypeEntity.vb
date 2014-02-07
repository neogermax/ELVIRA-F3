Imports Microsoft.VisualBasic

Public Class ProjectTypeEntity

#Region "Campos"

    Private _id As Integer
    Private _Project_Type As String

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
    Public Property Project_Type() As String
        Get
            Return Me._Project_Type
        End Get
        Set(ByVal value As String)
            Me._Project_Type = value
        End Set
    End Property

#End Region

End Class
