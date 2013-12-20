' TODO: 45 MAPEO DE TABLA SUBACTIVIDADESBYACTORES
' AUTOR: GERMAN RODRIGUEZ 30/07/13

Imports Microsoft.VisualBasic

Public Class SubactivityByThirdEntity

#Region "campos"

    Private _id As Integer
    Private _idSubActivity As Integer
    Private _idThird As Integer

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
    Public Property idSubactivity() As Integer
        Get
            Return Me._idSubActivity
        End Get
        Set(ByVal value As Integer)
            Me._idSubActivity = value
        End Set
    End Property

#End Region
End Class
