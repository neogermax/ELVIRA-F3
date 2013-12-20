Imports Microsoft.VisualBasic

Public Class ProjectApprovalDeinitive

#Region "Campos"

    Private _id As Integer
    Private _idproyect As Integer
    Private _code As String
    Private _approvaldate As DateTime
    Private _actnumber As String
    Private _approvedvalue As Integer
    Private _codeapprovedidproject As String
    Private _iduser As Integer
    Private _createdate As DateTime

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

    Public Property idproyect() As Integer
        Get
            Return Me._idproyect
        End Get
        Set(ByVal value As Integer)
            Me._idproyect = value
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

    Public Property approvaldate() As DateTime
        Get
            Return Me._approvaldate
        End Get
        Set(ByVal value As DateTime)
            Me._approvaldate = value
        End Set
    End Property

    Public Property actnumber() As String
        Get
            Return Me._actnumber
        End Get
        Set(ByVal value As String)
            Me._actnumber = value
        End Set
    End Property

    Public Property approvedvalue() As Integer
        Get
            Return Me._approvedvalue
        End Get
        Set(ByVal value As Integer)
            Me._approvedvalue = value
        End Set
    End Property

    Public Property codeapprovedidproject() As String
        Get
            Return Me._codeapprovedidproject
        End Get
        Set(ByVal value As String)
            Me._codeapprovedidproject = value
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

#End Region

End Class
