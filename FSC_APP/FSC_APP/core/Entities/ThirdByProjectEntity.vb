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

    Private _Vrmoney As String
    Private _VrSpecies As String
    Private _FSCorCounterpartContribution As String

    Private _Name As String
    Private _contact As String
    Private _Documents As String
    Private _Phone As String
    Private _Email As String
    Private _CreateDate As DateTime
    Private _EstadoFlujos As String



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

    Public Property type() As String
        Get
            Return Me._type
        End Get
        Set(ByVal value As String)
            Me._type = value
        End Set
    End Property
    Public Property Vrmoney() As String
        Get
            Return Me._Vrmoney
        End Get
        Set(ByVal value As String)
            Me._Vrmoney = value
        End Set
    End Property
    Public Property VrSpecies() As String
        Get
            Return Me._VrSpecies
        End Get
        Set(ByVal value As String)
            Me._VrSpecies = value
        End Set
    End Property
    Public Property FSCorCounterpartContribution() As String
        Get
            Return Me._FSCorCounterpartContribution
        End Get
        Set(ByVal value As String)
            Me._FSCorCounterpartContribution = value
        End Set
    End Property
    'cambios de face 3
    Public Property Name() As String
        Get
            Return Me._Name
        End Get
        Set(ByVal value As String)
            Me._Name = value
        End Set
    End Property
    Public Property contact() As String
        Get
            Return Me._contact
        End Get
        Set(ByVal value As String)
            Me._contact = value
        End Set
    End Property
    Public Property Documents() As String
        Get
            Return Me._Documents
        End Get
        Set(ByVal value As String)
            Me._Documents = value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return Me._Phone
        End Get
        Set(ByVal value As String)
            Me._Phone = value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return Me._Email
        End Get
        Set(ByVal value As String)
            Me._Email = value
        End Set
    End Property
    Public Property CreateDate() As DateTime
        Get
            Return Me._CreateDate
        End Get
        Set(ByVal value As DateTime)
            Me._CreateDate = value
        End Set
    End Property
    Public Property EstadoFlujos() As String
        Get
            Return Me._EstadoFlujos
        End Get
        Set(ByVal value As String)
            Me._EstadoFlujos = value
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
