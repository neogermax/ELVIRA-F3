Imports Microsoft.VisualBasic

Public Class DocumentstmpEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _namefile As String
    

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


    Public Property namefile() As String
        Get
            Return Me._namefile
        End Get
        Set(ByVal value As String)
            Me._namefile = value
        End Set
    End Property
 
#End Region

End Class
