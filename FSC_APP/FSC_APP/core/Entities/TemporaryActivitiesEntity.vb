'TODO:50 TABLA IDENTITY ACTIVIDADES TEMPORAL  PARA REVISAR LOS DATOS DE LA CARGA MASIVA  EN LA BASE DE DATOS
'AUTOR:GERMAN RODRIGUEZ 23/07/2013
Imports Microsoft.VisualBasic

Public Class TemporaryActivitiesEntity

#Region "Campos"
    ' campos
    Private _Cod_project As String
    Private _Cod_activity As String
    Private _Activity As String
    Private _Cod_subactivity As String
    Private _subactivity As String
    Private _Subactivity_previous As String
    Private _Nit_Actors As String
    Private _Actors As String
    Private _responsible As String
    Private _Star_date As String
    Private _End_date As String
#End Region

#Region "propiedades"

    Public Property Cod_project() As String
        Get
            Return Me._Cod_project
        End Get
        Set(ByVal value As String)
            Me._Cod_project = value
        End Set
    End Property


    Public Property Cod_activity() As String
        Get
            Return Me._Cod_activity
        End Get
        Set(ByVal value As String)
            Me._Cod_activity = value
        End Set
    End Property

    Public Property Activity() As String
        Get
            Return Me._Activity
        End Get
        Set(ByVal value As String)
            Me._Activity = value
        End Set
    End Property

    Public Property Cod_subactivity() As String
        Get
            Return Me._Cod_subactivity
        End Get
        Set(ByVal value As String)
            Me._Cod_subactivity = value
        End Set
    End Property

    Public Property subactivity() As String
        Get
            Return Me._subactivity
        End Get
        Set(ByVal value As String)
            Me._subactivity = value
        End Set
    End Property

    Public Property Subactivity_previous() As String
        Get
            Return Me._Subactivity_previous
        End Get
        Set(ByVal value As String)
            Me._Subactivity_previous = value
        End Set
    End Property

    Public Property Nit_Actors() As String
        Get
            Return Me._Nit_Actors
        End Get
        Set(ByVal value As String)
            Me._Nit_Actors = value
        End Set
    End Property

    Public Property Actors() As String
        Get
            Return Me._Actors
        End Get
        Set(ByVal value As String)
            Me._Actors = value
        End Set
    End Property

    Public Property responsible() As String
        Get
            Return Me._responsible
        End Get
        Set(ByVal value As String)
            Me._responsible = value
        End Set
    End Property

    Public Property Star_date() As String
        Get
            Return Me._Star_date
        End Get
        Set(ByVal value As String)
            Me._Star_date = value
        End Set
    End Property

    Public Property End_date() As String
        Get
            Return Me._End_date
        End Get
        Set(ByVal value As String)
            Me._End_date = value
        End Set
    End Property

#End Region

End Class
