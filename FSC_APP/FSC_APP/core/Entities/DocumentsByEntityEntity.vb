Imports Microsoft.VisualBasic

Public Class DocumentsByEntityEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _iddocuments As Integer
    Private _idnentity As Integer
    Private _entityname As String

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
    Public Property iddocuments() As Integer
        Get
            Return Me._iddocuments
        End Get
        Set(ByVal value As Integer)
            Me._iddocuments = value
        End Set
    End Property
    Public Property idnentity() As Integer
        Get
            Return Me._idnentity
        End Get
        Set(ByVal value As Integer)
            Me._idnentity = value
        End Set
    End Property
    Public Property entityname() As String
        Get
            Return Me._entityname
        End Get
        Set(ByVal value As String)
            Me._entityname = value
        End Set
    End Property

#End Region

End Class
