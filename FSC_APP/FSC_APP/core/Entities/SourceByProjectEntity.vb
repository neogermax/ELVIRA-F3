Imports Microsoft.VisualBasic

Public Class SourceByProjectEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idsource As Integer
    Private _idproject As Integer

    'Atributos adicionales
    Private _sourceName As String

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
    Public Property idsource() As Integer
        Get
            Return Me._idsource
        End Get
        Set(ByVal value As Integer)
            Me._idsource = value
        End Set
    End Property
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property

    'Propiedaes adicionales
    Public Property SOURCENAME() As String
        Get
            Return Me._sourceName
        End Get
        Set(ByVal value As String)
            Me._sourceName = value
        End Set
    End Property

#End Region

End Class
