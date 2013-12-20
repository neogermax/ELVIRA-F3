Imports Microsoft.VisualBasic

Public Class DocumentsEntity

#Region "Campos"

    ' campos
    Private _id As Integer
    Private _title As String
    Private _description As String
    Private _ideditedfor As Integer
    Private _idvisibilitylevel As Integer
    Private _iddocumenttype As Integer
    Private _createdate As DateTime
    Private _iduser As Integer
    Private _attachfile As String
    Private _enabled As Boolean

    'Atributos adicionales que almacenan informacion relacionada con el objeto actual
    Private _isNew As Boolean
    Private _isModified As Boolean
    Private _isDeleted As Boolean
    Private _attachFileOld As String
    Private _username As String
    Private _editedforname As String
    Private _visibilitylevelname As String
    Private _documenttypename As String
    Private _entityName As String
    Private _idnEntity As Integer
    Private _documentsByEntityId As Integer
    Private _projectName As String

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

    Public Property idnEntity() As Integer
        Get
            Return Me._idnEntity
        End Get
        Set(ByVal value As Integer)
            Me._idnEntity = value
        End Set
    End Property

    Public Property documentByEntityId() As Integer
        Get
            Return Me._documentsByEntityId
        End Get
        Set(ByVal value As Integer)
            Me._documentsByEntityId = value
        End Set
    End Property

    Public Property title() As String
        Get
            Return Me._title
        End Get
        Set(ByVal value As String)
            Me._title = value
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
    Public Property ideditedfor() As Integer
        Get
            Return Me._ideditedfor
        End Get
        Set(ByVal value As Integer)
            Me._ideditedfor = value
        End Set
    End Property
    Public Property idvisibilitylevel() As Integer
        Get
            Return Me._idvisibilitylevel
        End Get
        Set(ByVal value As Integer)
            Me._idvisibilitylevel = value
        End Set
    End Property
    Public Property iddocumenttype() As Integer
        Get
            Return Me._iddocumenttype
        End Get
        Set(ByVal value As Integer)
            Me._iddocumenttype = value
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


    Public Property ProjectName() As String
        Get
            Return Me._projectName
        End Get
        Set(ByVal value As String)
            Me._projectName = value
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
    Public Property attachfile() As String
        Get
            Return Me._attachfile
        End Get
        Set(ByVal value As String)
            Me._attachfile = value
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

    Public Property ISNEW() As Boolean
        Get
            Return Me._isNew
        End Get
        Set(ByVal value As Boolean)
            Me._isNew = value
        End Set
    End Property

    Public Property ISMODIFIED() As Boolean
        Get
            Return Me._isModified
        End Get
        Set(ByVal value As Boolean)
            Me._isModified = value
        End Set
    End Property

    Public Property ISDELETED() As Boolean
        Get
            Return Me._isDeleted
        End Get
        Set(ByVal value As Boolean)
            Me._isDeleted = value
        End Set
    End Property

    Public Property ATTACHFILEOLD() As String
        Get
            Return Me._attachFileOld
        End Get
        Set(ByVal value As String)
            Me._attachFileOld = value
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

    Public Property EDITEDFORNAME() As String
        Get
            Return Me._editedforname
        End Get
        Set(ByVal value As String)
            Me._editedforname = value
        End Set
    End Property

    Public Property VISIBILITYLEVELNAME() As String
        Get
            Return Me._visibilitylevelname
        End Get
        Set(ByVal value As String)
            Me._visibilitylevelname = value
        End Set
    End Property

    Public Property DOCUMENTTYPENAME() As String
        Get
            Return Me._documenttypename
        End Get
        Set(ByVal value As String)
            Me._documenttypename = value
        End Set
    End Property

    Public Property ENTITYNAME() As String
        Get
            Return Me._entityName
        End Get
        Set(ByVal value As String)
            Me._entityName = value
        End Set
    End Property

#End Region

End Class
