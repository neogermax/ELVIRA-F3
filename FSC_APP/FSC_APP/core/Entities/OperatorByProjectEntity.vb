Imports Microsoft.VisualBasic

Public Class OperatorByProjectEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _idoperator As Integer

    Private _operatorname As String

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
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property idoperator() As Integer
        Get
            Return Me._idoperator
        End Get
        Set(ByVal value As Integer)
            Me._idoperator = value
        End Set
    End Property
    Public Property OPERATORNAME() As String
        Get
            Return Me._operatorname
        End Get
        Set(ByVal value As String)
            Me._operatorname = value
        End Set
    End Property
#End Region

End Class
