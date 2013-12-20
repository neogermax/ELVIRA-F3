Imports Microsoft.VisualBasic

Public Class SubActivityInformationRegistryEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idsubactivity As Integer
    Private _description As String
    Private _begindate As DateTime
    Private _enddate As DateTime
    Private _comments As String
    Private _attachment As String
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _state As String
    Private _observation As String
    Private _indicator As String
    Private _documentList As List(Of DocumentsEntity)
    Private _documentsByEntityList As List(Of DocumentsByEntityEntity)
    Private _subactivityname As String
    Private _username As String

    Private _idProcessInstance As Integer
    Private _idActivityInstance As Integer

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
    Public Property idsubactivity() As Integer
        Get
            Return Me._idsubactivity
        End Get
        Set(ByVal value As Integer)
            Me._idsubactivity = value
        End Set
    End Property
    Public Property description() As String
        Get
            Return Me._description
        End Get
        Set(ByVal value As String)
            Me._description = value
        End Set
    End Property
    Public Property begindate() As DateTime
        Get
            Return Me._begindate
        End Get
        Set(ByVal value As DateTime)
            Me._begindate = value
        End Set
    End Property
    Public Property enddate() As DateTime
        Get
            Return Me._enddate
        End Get
        Set(ByVal value As DateTime)
            Me._enddate = value
        End Set
    End Property
    Public Property comments() As String
        Get
            Return Me._comments
        End Get
        Set(ByVal value As String)
            Me._comments = value
        End Set
    End Property

    Public Property observation() As String
        Get
            Return Me._observation
        End Get
        Set(ByVal value As String)
            Me._observation = value
        End Set
    End Property
    Public Property attachment() As String
        Get
            Return Me._attachment
        End Get
        Set(ByVal value As String)
            Me._attachment = value
        End Set
    End Property
    Public Property indicator() As String
        Get
            Return Me._indicator
        End Get
        Set(ByVal value As String)
            Me._indicator = value
        End Set
    End Property
    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
        End Set
    End Property
    Public Property createdate() As DateTime
        Get
            Return Me._createdate
        End Get
        Set(ByVal value As DateTime)
            Me._createdate = value
        End Set
    End Property
    Public Property state() As String
        Get
            Return Me._state
        End Get
        Set(ByVal value As String)
            Me._state = value
        End Set
    End Property
    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property SUBACTIVITYNAME() As String
        Get
            Return Me._subactivityname
        End Get
        Set(ByVal value As String)
            Me._subactivityname = value
        End Set
    End Property
    Public ReadOnly Property STATETEXT() As String
        Get
            STATETEXT = ""
            Select Case Me.state
                Case 1
                    STATETEXT = "Pendiente"
                Case 2
                    STATETEXT = "Vencida"
                Case 3
                    STATETEXT = "Cumplida"
                Case 4
                    STATETEXT = "Cancelada"
            End Select
        End Get
    End Property
    Public Property DOCUMENTLIST() As List(Of DocumentsEntity)
        Get
            Return Me._documentList
        End Get
        Set(ByVal value As List(Of DocumentsEntity))
            Me._documentList = value
        End Set
    End Property
    Public Property DOCUMENTSBYSAIRELIST() As List(Of DocumentsByEntityEntity)
        Get
            Return Me._documentsByEntityList
        End Get
        Set(ByVal value As List(Of DocumentsByEntityEntity))
            Me._documentsByEntityList = value
        End Set
    End Property

    Public Property IdProcessInstance() As Integer
        Get
            Return Me._idProcessInstance
        End Get
        Set(ByVal value As Integer)
            Me._idProcessInstance = value
        End Set
    End Property
    Public Property IdActivityInstance() As Integer
        Get
            Return Me._idActivityInstance
        End Get
        Set(ByVal value As Integer)
            Me._idActivityInstance = value
        End Set
    End Property

#End Region

End Class
