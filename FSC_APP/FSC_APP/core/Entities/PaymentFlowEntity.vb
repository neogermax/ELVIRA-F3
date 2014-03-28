Imports Microsoft.VisualBasic

Public Class PaymentFlowEntity
    ' se crea una entidad para flujos d pago
    ' autor Hernan Alonso Gomez
#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idproject As Integer
    Private _fecha As DateTime
    Private _porcentaje As Decimal
    Private _entregable As String
    Private _valorparcial As Decimal
    Private _ididea As Integer
    Private _valortotal As Decimal
    Private _N_pagos As String
#End Region

#Region "Constructor"

    'se desabilitan campos eliminados GR

    

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

    Public Property fecha() As DateTime
        Get
            Return Me._fecha
        End Get
        Set(ByVal value As DateTime)
            Me._fecha = value
        End Set
    End Property

    
    Public Property porcentaje() As Decimal
        Get
            Return Me._porcentaje
        End Get
        Set(ByVal value As Decimal)
            Me._porcentaje = value
        End Set
    End Property
    Public Property entregable() As String
        Get
            Return Me._entregable
        End Get
        Set(ByVal value As String)
            Me._entregable = value
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
    Public Property valorparcial() As Decimal
        Get
            Return Me._valorparcial
        End Get
        Set(ByVal value As Decimal)
            Me._valorparcial = value
        End Set
    End Property

    Public Property valortotal() As Decimal
        Get
            Return Me._valortotal
        End Get
        Set(ByVal value As Decimal)
            Me._valortotal = value
        End Set
    End Property
    Public Property N_pagos() As String
        Get
            Return Me._N_pagos
        End Get
        Set(ByVal value As String)
            Me._N_pagos = value
        End Set
    End Property


#End Region

End Class
