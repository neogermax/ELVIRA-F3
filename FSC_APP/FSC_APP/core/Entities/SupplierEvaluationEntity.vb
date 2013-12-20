Imports Microsoft.VisualBasic

Public Class SupplierEvaluationEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idsupplier As Integer
    Private _contractnumber As String
    Private _contractstartdate As DateTime
    Private _contractenddate As DateTime
    Private _contractsubject As String
    Private _contractvalue As Double
    Private _iduser As Integer
    Private _createdate As DateTime

    'Atributo adicional que almacena el nombre del usuario
    Private _username As String
    Private _suppliername As String
    Private _supplierQualification As SupplierQualificationEntity


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
    Public Property idsupplier() As Integer
        Get
            Return Me._idsupplier
        End Get
        Set(ByVal value As Integer)
            Me._idsupplier = value
        End Set
    End Property
    Public Property contractnumber() As String
        Get
            Return Me._contractnumber
        End Get
        Set(ByVal value As String)
            Me._contractnumber = value
        End Set
    End Property
    Public Property contractstartdate() As DateTime
        Get
            Return Me._contractstartdate
        End Get
        Set(ByVal value As DateTime)
            Me._contractstartdate = value
        End Set
    End Property
    Public Property contractenddate() As DateTime
        Get
            Return Me._contractenddate
        End Get
        Set(ByVal value As DateTime)
            Me._contractenddate = value
        End Set
    End Property
    Public Property contractsubject() As String
        Get
            Return Me._contractsubject
        End Get
        Set(ByVal value As String)
            Me._contractsubject = value
        End Set
    End Property
    Public Property contractvalue() As Double
        Get
            Return Me._contractvalue
        End Get
        Set(ByVal value As Double)
            Me._contractvalue = value
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

    Public Property USERNAME() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property SUPPLIERNAME() As String
        Get
            Return Me._suppliername
        End Get
        Set(ByVal value As String)
            Me._suppliername = value
        End Set
    End Property
    Public Property SUPPLIERQUALIFICATION() As SupplierQualificationEntity
        Get
            Return Me._supplierQualification
        End Get
        Set(ByVal value As SupplierQualificationEntity)
            Me._supplierQualification = value
        End Set
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        'Se crea una instancia de la clase calificacion del proveedor
        Me._supplierQualification = New SupplierQualificationEntity()
    End Sub

#End Region

End Class
