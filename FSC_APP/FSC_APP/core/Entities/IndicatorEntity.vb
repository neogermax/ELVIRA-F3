Imports Microsoft.VisualBasic

Public Class IndicatorEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _levelindicator As String
    Private _identity As Integer
    Private _code As String
    Private _description As String
    Private _type As String
    Private _goal As String
    Private _greenvalue As String
    Private _yellowvalue As String
    Private _redvalue As String
    Private _assumptions As String
    Private _sourceverification As String
    Private _enabled As Boolean
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _idresponsable As Integer

    Private _dateList As List(Of MeasurementDateByIndicatorEntity)
    Private _entityname As String
    Private _username As String
    Private _responsablename As String
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
    Public Property code() As String
        Get
            Return Me._code
        End Get
        Set(ByVal value As String)
            Me._code = value
        End Set
    End Property
    Public Property levelindicator() As String
        Get
            Return Me._levelindicator
        End Get
        Set(ByVal value As String)
            Me._levelindicator = value
        End Set
    End Property
    Public Property identity() As Integer
        Get
            Return Me._identity
        End Get
        Set(ByVal value As Integer)
            Me._identity = value
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
    Public Property type() As String
        Get
            Return Me._type
        End Get
        Set(ByVal value As String)
            Me._type = value
        End Set
    End Property
    Public Property goal() As String
        Get
            Return Me._goal
        End Get
        Set(ByVal value As String)
            Me._goal = value
        End Set
    End Property
    Public Property greenvalue() As String
        Get
            Return Me._greenvalue
        End Get
        Set(ByVal value As String)
            Me._greenvalue = value
        End Set
    End Property
    Public Property yellowvalue() As String
        Get
            Return Me._yellowvalue
        End Get
        Set(ByVal value As String)
            Me._yellowvalue = value
        End Set
    End Property
    Public Property redvalue() As String
        Get
            Return Me._redvalue
        End Get
        Set(ByVal value As String)
            Me._redvalue = value
        End Set
    End Property
    Public Property assumptions() As String
        Get
            Return Me._assumptions
        End Get
        Set(ByVal value As String)
            Me._assumptions = value
        End Set
    End Property
    Public Property sourceverification() As String
        Get
            Return Me._sourceverification
        End Get
        Set(ByVal value As String)
            Me._sourceverification = value
        End Set
    End Property
    Public Property enabled() As Boolean
        Get
            Return Me._enabled
        End Get
        Set(ByVal value As Boolean)
            Me._enabled = value
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

    Public Property idresponsable() As Integer
        Get
            Return Me._idresponsable
        End Get
        Set(ByVal value As Integer)
            Me._idresponsable = value
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

    Public Property dateList() As List(Of MeasurementDateByIndicatorEntity)
        Get
            Return Me._dateList
        End Get
        Set(ByVal value As List(Of MeasurementDateByIndicatorEntity))
            Me._dateList = value
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

    Public Property RESPONSABLENAME() As String
        Get
            Return Me._responsablename
        End Get
        Set(ByVal value As String)
            Me._responsablename = value
        End Set
    End Property
    Public Property ENTITYNAME() As String
        Get
            Return Me._entityname
        End Get
        Set(ByVal value As String)
            Me._entityname = value
        End Set
    End Property
    Public ReadOnly Property TYPENAME() As String
        Get
            TYPENAME = ""
            Select Case Me.type
                Case 1
                    TYPENAME = "Beneficiarios"
                Case 2
                    TYPENAME = "Capacidad instalada"
                Case 3
                    TYPENAME = "Gestión del conocimiento"
            End Select
        End Get
    End Property
    Public ReadOnly Property LEVELNAME() As String
        Get
            LEVELNAME = ""
            Select Case Me.levelindicator
                Case "2"
                    LEVELNAME = "Segundo Nivel - Programa"
                Case "1.1"
                    LEVELNAME = "Primer Nivel - Linea Estrategica"
                Case "1.2"
                    LEVELNAME = "Primer Nivel - Estrategia"
                Case "3"
                    LEVELNAME = "Tercer Nivel - Proyecto"
            End Select
        End Get
    End Property
#End Region

End Class
