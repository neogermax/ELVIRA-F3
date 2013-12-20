'TODO:39 TABLA IDENTITY TIPO DE CONTRATACION NUEVA PARA COMBO BOX DESDE BASE DE DATOS
'AUTOR:GERMAN RODRIGUEZ 20/07/2013
Imports Microsoft.VisualBasic

Public Class typecontractEntity

#Region "Campos"
    'campos
    Private _id As Integer
    Private _contract As String

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
    Public Property contract() As String
        Get
            Return Me._contract
        End Get
        Set(ByVal value As String)
            Me._contract = value
        End Set
    End Property

#End Region


#Region "Constructor"

    Public Sub New()

    End Sub

#End Region


End Class
