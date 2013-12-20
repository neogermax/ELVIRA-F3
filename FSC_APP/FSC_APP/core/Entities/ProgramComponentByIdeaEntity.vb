Imports Microsoft.VisualBasic

Public Class ProgramComponentByIdeaEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _ididea As Integer
    Private _idProgramComponent As Integer

    'Atributos agregados
    Private _idProgram As Integer
    Private _idStrategicLine As Integer
    Private _Code As String
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
    Public Property ididea() As Integer
        Get
            Return Me._ididea
        End Get
        Set(ByVal value As Integer)
            Me._ididea = value
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

    Public Property IDProgram() As Integer
        Get
            Return Me._idProgram
        End Get
        Set(ByVal value As Integer)
            Me._idProgram = value
        End Set
    End Property

    Public Property IDStrategicLine() As Integer
        Get
            Return Me._idStrategicLine
        End Get
        Set(ByVal value As Integer)
            Me._idStrategicLine = value
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
