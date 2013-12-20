Imports Microsoft.VisualBasic


Public Class listDetail

#Region "Campos"

    Public _id As Integer
    Public _name As String

#End Region

#Region "Funciones"

    Public Overloads Function Equals(ByVal other As listDetail) As Boolean
        Equals = False
        If other._id = Me._id And other._name = Me._name Then
            Equals = True
        End If
    End Function

#End Region

End Class

Public Class SubActivityMainPanelEntity


#Region "Campos"

    ' campos
    Private _id As Integer
    Private _idReal As Integer
    Private _strategicobjective As List(Of listDetail)
    Private _StrategicLine As List(Of listDetail)
    Private _strategy As List(Of listDetail)
    Private _idproject As Integer
    Private _projectname As String
    Private _projectPhase As Integer
    Private _idcomponent As Integer
    Private _componentname As String
    Private _name As String
    Private _Attachment As String
    Private _type As Integer
    Private _state As Integer
    Private _begindate As DateTime
    Private _enddate As DateTime
    Private _iduser As Integer
    Private _username As String
    Private _approval As Boolean
    Private _measuramentDateByIndicator As List(Of MeasurementDateByIndicatorEntity)

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


    Public Property idReal() As Integer
        Get
            Return Me._idReal
        End Get
        Set(ByVal value As Integer)
            Me._idReal = value
        End Set
    End Property

    Public Property strategicobjective() As List(Of listDetail)
        Get
            Return Me._strategicobjective
        End Get
        Set(ByVal value As List(Of listDetail))
            Me._strategicobjective = value
        End Set
    End Property
    Public ReadOnly Property strategicobjectiveText() As String
        Get
            strategicobjectiveText = ""
            For Each detail As listDetail In Me._strategicobjective
                strategicobjectiveText += detail._name + "<br />"
            Next
        End Get
    End Property
    Public Property StrategicLine() As List(Of listDetail)
        Get
            Return Me._StrategicLine
        End Get
        Set(ByVal value As List(Of listDetail))
            Me._StrategicLine = value
        End Set
    End Property
    Public ReadOnly Property StrategicLineText() As String
        Get
            StrategicLineText = ""
            For Each detail As listDetail In Me._StrategicLine
                StrategicLineText += detail._name + "<br />"
            Next
        End Get
    End Property
    Public Property strategy() As List(Of listDetail)
        Get
            Return Me._strategy
        End Get
        Set(ByVal value As List(Of listDetail))
            Me._strategy = value
        End Set
    End Property
    Public ReadOnly Property strategyText() As String
        Get
            strategyText = ""
            For Each detail As listDetail In Me._strategy
                strategyText += detail._name + "<br />"
            Next
        End Get
    End Property
    Public Property idproject() As Integer
        Get
            Return Me._idproject
        End Get
        Set(ByVal value As Integer)
            Me._idproject = value
        End Set
    End Property
    Public Property projectname() As String
        Get
            Return Me._projectname
        End Get
        Set(ByVal value As String)
            Me._projectname = value
        End Set
    End Property

    Public Property attachment() As String
        Get
            Return Me._Attachment
        End Get
        Set(ByVal value As String)
            Me._Attachment = value
        End Set
    End Property
    Public Property projectphase() As Integer
        Get
            Return Me._projectPhase
        End Get
        Set(ByVal value As Integer)
            Me._projectPhase = value
        End Set
    End Property
    Public ReadOnly Property projectphaseText() As String
        Get
            projectphaseText = ""
            Select Case Me._projectPhase
                Case 1
                    projectphaseText = "Formulación"
                Case 2
                    projectphaseText = "Aprobación"
                Case 3
                    projectphaseText = "Ejecución"
                Case 4
                    projectphaseText = "Evaluación"
            End Select
        End Get
    End Property
    Public Property idcomponent() As Integer
        Get
            Return Me._idcomponent
        End Get
        Set(ByVal value As Integer)
            Me._idcomponent = value
        End Set
    End Property
    Public Property componentname() As String
        Get
            Return Me._componentname
        End Get
        Set(ByVal value As String)
            Me._componentname = value
        End Set
    End Property
    Public Property name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property
    Public Property type() As Integer
        Get
            Return Me._type
        End Get
        Set(ByVal value As Integer)
            Me._type = value
        End Set
    End Property
    Public ReadOnly Property typeText() As String
        Get
            typeText = ""
            Select Case Me._type
                Case 1
                    typeText = "Actividad"
                Case 2
                    typeText = "Indicador"
                Case 3
                    typeText = "Encuesta"
            End Select
        End Get
    End Property
    Public Property state() As String
        Get
            Return Me._state
        End Get
        Set(ByVal value As String)
            Me._state = value
        End Set
    End Property
    Public ReadOnly Property stateText() As String
        Get
            stateText = ""
            Select Case Me._state
                Case 1
                    stateText = "Pendiente"
                Case 2
                    stateText = "Vencida"
                Case 3
                    stateText = "Cumplida"
                Case 4
                    stateText = "Cancelado"
            End Select
        End Get
    End Property
    Public Property begindate() As DateTime
        Get
            Return Me._begindate
        End Get
        Set(ByVal value As DateTime)
            Me._begindate = value
        End Set
    End Property
    Public ReadOnly Property begindateText() As String
        Get
            Return "<a href='addSubActivityInformationRegistry.aspx?op=add&idsubactivity=" & Me._id & "'>" & Me._begindate.ToString("dd/MM/yyyy") & "</a>"
        End Get
    End Property
    Public Property enddate() As DateTime
        Get
            Return Me._enddate.ToString("dd/MM/yyyy")
        End Get
        Set(ByVal value As DateTime)
            Me._enddate = value
        End Set
    End Property
    Public ReadOnly Property enddateText() As String
        Get
            Return Me._enddate.ToString("dd/MM/yyyy")
        End Get
    End Property
    Public Property iduser() As Integer
        Get
            Return Me._iduser
        End Get
        Set(ByVal value As Integer)
            Me._iduser = value
        End Set
    End Property
    Public Property username() As String
        Get
            Return Me._username
        End Get
        Set(ByVal value As String)
            Me._username = value
        End Set
    End Property
    Public Property approval() As Boolean
        Get
            Return Me._approval
        End Get
        Set(ByVal value As Boolean)
            Me._approval = value
        End Set
    End Property
    Public ReadOnly Property approvalText() As String
        Get
            approvalText = ""
            If Me._approval Then
                approvalText = "Si"
            Else
                approvalText = "No"
            End If
        End Get
    End Property
    Public Property measuramentDateByIndicator() As List(Of MeasurementDateByIndicatorEntity)
        Get
            Return Me._measuramentDateByIndicator
        End Get
        Set(ByVal value As List(Of MeasurementDateByIndicatorEntity))
            Me._measuramentDateByIndicator = value
        End Set
    End Property
    Public ReadOnly Property measuramentDateByIndicatorText() As String
        Get
            measuramentDateByIndicatorText = ""
            For Each mdbi As MeasurementDateByIndicatorEntity In Me._measuramentDateByIndicator
                measuramentDateByIndicatorText += "<a href='addIndicatorInformation.aspx?op=add&IdMeasurementDateByIndicator=" & mdbi.id & "&IdIndicator=" & mdbi.idindicator & "'>" & mdbi.measurementdate.ToString("dd/MM/yyyy") & "</a><br />"
            Next
        End Get
    End Property

#End Region

End Class

Public Class SubActivityMainPanelComparer

    Implements IComparer(Of SubActivityMainPanelEntity)

    Private _order As String

    Public Property order() As String
        Get
            Return Me._order
        End Get
        Set(ByVal value As String)
            Me._order = value
        End Set
    End Property

    Public Function Compare(ByVal x As SubActivityMainPanelEntity, ByVal y As SubActivityMainPanelEntity) As Integer Implements System.Collections.Generic.IComparer(Of SubActivityMainPanelEntity).Compare

        Select Case Me._order
            Case "id"
                If x.id > y.id Then
                    Return 1
                ElseIf x.id < y.id Then
                    Return -1
                Else
                    Return 0
                End If
            Case "strategicobjective"
                Return String.Compare(x.strategicobjectiveText, y.strategicobjectiveText)
            Case "StrategicLine"
                Return String.Compare(x.StrategicLineText, y.StrategicLineText)
            Case "strategy"
                Return String.Compare(x.strategyText, y.strategyText)
            Case "project"
                Return String.Compare(x.projectname, y.projectname)
            Case "projectphase"
                Return String.Compare(x.projectphaseText, y.projectphaseText)
            Case "component"
                Return String.Compare(x.componentname, y.componentname)
            Case "name"
                Return String.Compare(x.name, y.name)
            Case "type"
                If x.type > y.type Then
                    Return 1
                ElseIf x.type < y.type Then
                    Return -1
                Else
                    Return 0
                End If
            Case "state"
                If x.state > y.state Then
                    Return 1
                ElseIf x.state < y.state Then
                    Return -1
                Else
                    Return 0
                End If
            Case "begindate"
                If x.begindate > y.begindate Then
                    Return 1
                ElseIf x.begindate < y.begindate Then
                    Return -1
                Else
                    Return 0
                End If
            Case "enddate"
                If x.enddate > y.enddate Then
                    Return 1
                ElseIf x.enddate < y.enddate Then
                    Return -1
                Else
                    Return 0
                End If
            Case "user"
                Return String.Compare(x.username, y.username)
            Case "approval"
                If y.approval Then
                    Return 1
                Else
                    Return 0
                End If
        End Select

    End Function

End Class
