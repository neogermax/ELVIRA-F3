Imports Microsoft.VisualBasic

Public Class ProgramComponentByProjectEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _idProgramComponent As Integer
    Private _Code As String
    Private _ProgramComponentname As String

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
    Public Property idProgramComponent() As Integer
        Get
            Return Me._idProgramComponent
        End Get
        Set(ByVal value As Integer)
            Me._idProgramComponent = value
        End Set
    End Property
    Public Property ProgramComponentNAME() As String
        Get
            Return Me._ProgramComponentname
        End Get
        Set(ByVal value As String)
            Me._ProgramComponentname = value
        End Set

    End Property

    Public Property CODE() As String
        Get
            Return Me._Code
        End Get
        Set(ByVal value As String)
            Me._Code = value
        End Set

    End Property
#End Region

End Class
