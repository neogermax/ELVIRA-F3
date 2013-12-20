Imports Microsoft.VisualBasic

Public Class proceeding_logsEntity

#Region "Campos"

    Private _id As Integer
    Private _project_id As Integer
    Private _acta_id As String
    Private _Tipo_acta_id As String
    Private _iduser As Integer
    Private _createdate As DateTime
    Private _file_name As String


    Private _name_acta As String
    Private _name_user As String
    Private _name_ruta As String


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
    Public Property project_id() As Integer
        Get
            Return Me._project_id
        End Get
        Set(ByVal value As Integer)
            Me._project_id = value
        End Set
    End Property
    Public Property acta_id() As String
        Get
            Return Me._acta_id
        End Get
        Set(ByVal value As String)
            Me._acta_id = value
        End Set
    End Property
    Public Property Tipo_acta_id() As String
        Get
            Return Me._Tipo_acta_id
        End Get
        Set(ByVal value As String)
            Me._Tipo_acta_id = value
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
    Public Property file_name() As String
        Get
            Return Me._file_name
        End Get
        Set(ByVal value As String)
            Me._file_name = value
        End Set
    End Property

    Public Property name_acta() As String
        Get
            Return Me._name_acta
        End Get
        Set(ByVal value As String)
            Me._name_acta = value
        End Set
    End Property

    Public Property name_user() As String
        Get
            Return Me._name_user
        End Get
        Set(ByVal value As String)
            Me._name_user = value
        End Set
    End Property

    Public Property name_ruta() As String
        Get
            Return Me._name_ruta
        End Get
        Set(ByVal value As String)
            Me._name_ruta = value
        End Set
    End Property


#End Region


End Class
