Imports Microsoft.VisualBasic

Public Class ThirdByProjectEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _idthird As Integer
    'Private _actions As String
    'Private _experiences As String
    Private _type As String
    Private _third As ThirdEntity
    Private _thirdname As String
    'Private _typetext As String

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
    Public Property idthird() As Integer
        Get
            Return Me._idthird
        End Get
        Set(ByVal value As Integer)
            Me._idthird = value
        End Set
    End Property
    'Public Property actions() As String
    '    Get
    '        Return Me._actions
    '    End Get
    '    Set(ByVal value As String)
    '        Me._actions = value
    '    End Set
    'End Property
    'Public Property experiences() As String
    '    Get
    '        Return Me._experiences
    '    End Get
    '    Set(ByVal value As String)
    '        Me._experiences = value
    '    End Set
    'End Property
    Public Property type() As String
        Get
            Return Me._type
        End Get
        Set(ByVal value As String)
            Me._type = value
        End Set
    End Property
    Public Property THIRDNAME() As String
        Get
            Return Me._thirdname
        End Get
        Set(ByVal value As String)
            Me._thirdname = value
        End Set
    End Property
    'Public ReadOnly Property TYPETEXT() As String
    '    Get
    '        TYPETEXT = ""
    '        Select Case Me._type
    '            Case 1
    '                TYPETEXT = "Operador"
    '            Case 2
    '                TYPETEXT = "Socio"
    '            Case 3
    '                TYPETEXT = "Otros actores"
    '            Case Else
    '                TYPETEXT = "No definido"
    '        End Select
    '    End Get
    'End Property

    Public Property THIRD() As ThirdEntity
        Get
            Return Me._third
        End Get
        Set(ByVal value As ThirdEntity)
            Me._third = value
        End Set
    End Property

#End Region
#Region "Constructor"

    'se desabilitan campos eliminados GR

    Public Sub New()
        '   ' Se crea la instancia del objeto third
        Me._third = New ThirdEntity()
    End Sub

#End Region


End Class
