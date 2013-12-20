Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager


Public Class SubActivityMainPanelDALC

    ' constantes
    Const MODULENAME As String = "SubActivityMainPanelDALC"

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idstrategicobjective"></param>
    ''' <param name="strategicobjectivename"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="StrategicLinename"></param>
    ''' <param name="idstrategy"></param>
    ''' <param name="strategyname"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="projectPhase"></param>
    ''' <param name="projectPhaseText"></param>
    ''' <param name="idcomponent"></param>
    ''' <param name="componentname"></param>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="typetext"></param>
    ''' <param name="state"></param>
    ''' <param name="statetext"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="approval"></param>
    ''' <param name="approvalText"></param>
    ''' <param name="order"></param>
    ''' <returns>un objeto de tipo List(Of MANAGEMENTEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal StrategicLinename As String = "", _
        Optional ByVal idstrategy As String = "", _
        Optional ByVal strategyname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal projectPhase As String = "", _
        Optional ByVal projectPhaseText As String = "", _
        Optional ByVal idcomponent As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal state As String = "", _
        Optional ByVal statetext As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal approval As String = "", _
        Optional ByVal approvalText As String = "", _
        Optional ByVal order As String = "") As List(Of SubActivityMainPanelEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSubActivityMainPanel As SubActivityMainPanelEntity
        Dim SubActivityMainPanelList As New List(Of SubActivityMainPanelEntity)
        Dim data As DataTable
        Dim data1 As DataTable
        Dim strategicobjectiveList As List(Of listDetail)
        Dim StrategicLineList As List(Of listDetail)
        Dim strategyList As List(Of listDetail)
        Dim measuramentDateList As List(Of MeasurementDateByIndicatorEntity)
        Dim measuramentDateByIndicatorList As List(Of MeasurementDateByIndicatorEntity)
        Dim indicatorList As New List(Of Integer)
        Dim idIndicator As Integer
        Dim indicatorString As String
        Dim beforeDays As Integer = 15
        Dim repeat As Boolean
        Dim idi As Integer = Nothing
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia para traer los indicadores
            sql.AppendLine(" SELECT DISTINCT SubActivity.idkey AS ID ,  SubActivity.id as IdReal,  SubActivity.IdActivity, SubActivity.Type, SubActivity.Number, SubActivity.Name, SubActivity.Description,  ")
            sql.AppendLine(" 	SubActivity.IdResponsible, SubActivity.BeginDate, SubActivity.EndDate, SubActivity.TotalCost, SubActivity.Duration,  ")
            sql.AppendLine(" 	SubActivity.FSCContribution, SubActivity.OFContribution, SubActivity.Attachment, SubActivity.CriticalPath,  ")
            sql.AppendLine(" 	SubActivity.RequiresApproval, SubActivity.Enabled, SubActivity.IdUser, SubActivity.CreateDate,  ")
            sql.AppendLine(" 	SubActivityInformationRegistry.State, Res.Name AS userName, Activity.idkey AS ActivityId, Activity.Title AS ActivityTitle,  ")
            sql.AppendLine(" 	Component.idkey AS ComponentId, Component.Name AS ComponentName, Objective.idkey AS ObjectiveId,  ")
            sql.AppendLine(" 	Objective.Name AS ObjectiveName, Project.idkey AS ProjectId, Project.Name AS ProjectName, Project.IdPhase AS ProjectPhase ")
            sql.AppendLine(" FROM SubActivityInformationRegistry RIGHT OUTER JOIN ")
            sql.AppendLine(" 	SubActivity INNER JOIN ")
            sql.AppendLine(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivity.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.AppendLine(" 	" & dbSecurityName & ".dbo.ApplicationUser as Res ON SubActivity.IdResponsible = Res.ID INNER JOIN ")
            sql.AppendLine(" 	Activity ON SubActivity.IdActivity = Activity.idkey and Activity.IsLastVersion='1' INNER JOIN ")
            sql.AppendLine(" 	Component ON Activity.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN ")
            sql.AppendLine(" 	Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN ")
            sql.AppendLine(" 	Project ON Objective.IdProject = Project.idKey and Project.IsLastVersion='1' ON SubActivityInformationRegistry.IdSubActivity = SubActivity.idkey and SubActivity.IsLastVersion='1' ")
            sql.AppendLine(" WHERE (SubActivity.Enabled = 1) and SubActivity.IsLastVersion='1'  and  SubActivityInformationRegistry.Id is null  ")

            where = "AND"

            Integer.TryParse(id, idi)

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.AppendLine(where & " (SubActivity.id = '" & idi & "' ) ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.AppendLine(where & " (SubActivity.idkey = '" & idi & "' ) ")
                where = " AND "

            End If



            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.AppendLine(where & " (SubActivity.idkey LIKE '%" & idlike & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.AppendLine(where & " (Project.idkey = '" & idproject & "') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.AppendLine(where & " (Project.Name like '%" & projectname & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectPhase.Equals("") Then

                sql.AppendLine(where & " (Project.IdPhase = '" & projectPhase & "') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectPhaseText.Equals("") Then

                sql.AppendLine(where & " (Project.IdPhase IN ")
                sql.AppendLine(" (SELECT Value FROM (SELECT 'Formulación' AS Impact, 1 AS Value ")
                sql.AppendLine(" UNION SELECT 'Formulacion' AS Impact, 1 AS Value ")
                sql.AppendLine(" UNION SELECT 'Planeación' AS Impact, 2 AS Value ")
                sql.AppendLine(" UNION SELECT 'Planeacion' AS Impact, 2 AS Value ")
                sql.AppendLine(" UNION SELECT 'Ejecución' AS Impact, 3 AS Value ")
                sql.AppendLine(" UNION SELECT 'Ejecucion' AS Impact, 3 AS Value ")
                sql.AppendLine(" UNION SELECT 'Evaluación' AS Impact, 4 AS Value ")
                sql.AppendLine(" UNION SELECT 'Evaluacion' AS Impact, 4 AS Value) Temp ")
                sql.AppendLine(" WHERE (Impact LIKE '%" & projectPhaseText & "%')) ) ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " (SubActivity.Name like '%" & name & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " (SubActivity.IdResponsible= '" & iduser & "') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " (Res.Name like '%" & username & "%') ")
                where = " AND "

            End If


            '' verificar si hay entrada de datos para el campo
            'If begindate.Equals("") And enddate.Equals("") Then

            '    sql.AppendLine(where & "  ( (SubActivity.BeginDate BETWEEN '" & Today.AddDays(-beforeDays).ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "') ")
            '    sql.AppendLine(" OR (SubActivity.EndDate BETWEEN '" & Today.ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "')  ) ")
            '    where = " AND "

            'Else
            ' verificar si hay entrada de datos para el campo
            ' verificar si hay entrada de datos para el campo
            'If begindate.Equals("") And enddate.Equals("") Then

            '    sql.AppendLine(where & "   (SubActivity.BeginDate <='" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "') ")
            '    'sql.AppendLine(" OR (SubActivity.EndDate BETWEEN '" & Today.ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "')  ) ")
            '    where = " AND "

            'Else
            'verificar si hay entrada de datos para el campo


            If Not begindate.Equals("") Then

                sql.AppendLine(where & " (CONVERT(NVARCHAR, SubActivity.begindate, 103) like '%" & begindate & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.AppendLine(where & " (CONVERT(NVARCHAR, SubActivity.enddate, 103) like '%" & enddate & "%') ")
                where = " AND "

            End If

            'End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSubActivityMainPanel = New SubActivityMainPanelEntity

                ' cargar el valor del campo
                objSubActivityMainPanel.id = row("Id")
                objSubActivityMainPanel.idReal = row("IdReal")
                objSubActivityMainPanel.idproject = row("ProjectId")
                objSubActivityMainPanel.projectname = row("ProjectName")
                objSubActivityMainPanel.projectphase = row("ProjectPhase")
                objSubActivityMainPanel.idcomponent = row("ComponentId")
                objSubActivityMainPanel.componentname = row("ComponentName")
                objSubActivityMainPanel.name = row("name")
                objSubActivityMainPanel.attachment = row("attachment")
                objSubActivityMainPanel.type = 1
                objSubActivityMainPanel.state = IIf(Not IsDBNull(row("State")), row("State"), 1)
                objSubActivityMainPanel.begindate = IIf(Not IsDBNull(row("begindate")), row("begindate"), Nothing)
                objSubActivityMainPanel.enddate = IIf(Not IsDBNull(row("enddate")), row("enddate"), Nothing)
                objSubActivityMainPanel.iduser = row("iduser")
                objSubActivityMainPanel.username = row("userName")
                objSubActivityMainPanel.approval = IIf(Not IsDBNull(row("RequiresApproval")), row("RequiresApproval"), Nothing)

                'Agregar al objeto su lista de Objetivos estrategicos, Lineas estrategicas, Estrategias
                data1 = listDetails(objApplicationCredentials, objSubActivityMainPanel.idproject)

                strategicobjectiveList = New List(Of listDetail)
                StrategicLineList = New List(Of listDetail)
                strategyList = New List(Of listDetail)

                For Each data1Row As DataRow In data1.Rows

                    If Not IsDBNull(data1Row("IdStrategicObjective")) Then
                        Dim strategicobjective As New listDetail
                        strategicobjective._id = data1Row("IdStrategicObjective")
                        strategicobjective._name = data1Row("StrategicObjectiveName")


                        'Objetivos estrategicos correspondientes al indicador
                        repeat = False
                        For Each it As listDetail In strategicobjectiveList
                            If it.Equals(strategicobjective) Then
                                repeat = True
                            End If
                        Next
                        If Not repeat Then
                            strategicobjectiveList.Add(strategicobjective)
                        End If

                    End If

                    If Not IsDBNull(data1Row("IdStrategicLine")) Then
                        Dim StrategicLine As New listDetail
                        StrategicLine._id = data1Row("IdStrategicLine")
                        StrategicLine._name = data1Row("StrategicLineName")

                        'Linieas estrategicas correspondientes al indicador
                        repeat = False
                        For Each it As listDetail In StrategicLineList
                            If it.Equals(StrategicLine) Then
                                repeat = True
                            End If
                        Next
                        If Not repeat Then
                            StrategicLineList.Add(StrategicLine)
                        End If

                    End If

                    If Not IsDBNull(data1Row("IdStrategy")) Then
                        Dim strategy As New listDetail
                        strategy._id = data1Row("IdStrategy")
                        strategy._name = data1Row("StrategyName")

                        'Estrategias correspondientes al indicador
                        repeat = False
                        For Each it As listDetail In strategyList
                            If it.Equals(strategy) Then
                                repeat = True
                            End If
                        Next
                        If Not repeat Then
                            strategyList.Add(strategy)
                        End If

                    End If

                Next

                objSubActivityMainPanel.strategicobjective = strategicobjectiveList
                objSubActivityMainPanel.StrategicLine = StrategicLineList
                objSubActivityMainPanel.strategy = strategyList
                objSubActivityMainPanel.measuramentDateByIndicator = New List(Of MeasurementDateByIndicatorEntity)

                ' agregar a la lista
                SubActivityMainPanelList.Add(objSubActivityMainPanel)

            Next

            'Construir la secuencia para obtener los indicadores de la cola de tareas proximas
            sql.Remove(0, sql.Length)

            ' 1. Obtener las fechas de medición no registradas dentro del rango de fechas
            sql.Append(" SELECT MeasurementDateByIndicator.Id, MeasurementDateByIndicator.IdIndicator, ")
            sql.Append(" 	MeasurementDateByIndicator.measurementDate ")
            sql.Append(" FROM MeasurementDateByIndicator INNER JOIN ")
            sql.Append(" 	Indicator ON MeasurementDateByIndicator.IdIndicator = Indicator.Id ")
            sql.Append(" WHERE (Indicator.LevelIndicator = '3') AND (Indicator.Enabled = 1) AND ")

            where = "AND"

            ' 1.1 Verificar rango de fechas
            If begindate.Equals("") Then
                sql.Append(" (MeasurementDateByIndicator.measurementDate <= '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "') AND ")
                'sql.Append(" (MeasurementDateByIndicator.measurementDate BETWEEN '" & Today.ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "') AND ")

            Else

                sql.Append(" (CONVERT(NVARCHAR, MeasurementDateByIndicator.measurementDate,  103) LIKE  '%" & begindate & "%') AND ")

            End If
            sql.Append(" 	  ((SELECT COUNT(Id) AS already ")
            sql.Append(" 	    FROM IndicatorInformation ")
            sql.Append(" 	    WHERE (MeasurementDateByIndicator.Id = IdMeasurementDateByIndicator)) = 0) ")
            sql.Append(" ORDER BY MeasurementDateByIndicator.IdIndicator, MeasurementDateByIndicator.measurementDate ")


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)
            measuramentDateList = New List(Of MeasurementDateByIndicatorEntity)

            If data.Rows.Count > 0 Then

                '2. Obtener los datos de los indicadores sin repetir, y guardar las fechas obtenidas
                For Each row As DataRow In data.Rows

                    idIndicator = row("IdIndicator")
                    If Not indicatorList.Contains(idIndicator) Then
                        indicatorList.Add(idIndicator)
                    End If

                    Dim mdbi As New MeasurementDateByIndicatorEntity
                    mdbi.id = row("Id")
                    mdbi.idindicator = row("IdIndicator")
                    mdbi.measurementdate = row("measurementDate")
                    measuramentDateList.Add(mdbi)

                Next

                '3. Completar los datos para cada indicador
                indicatorString = ""
                For Each idIndicator In indicatorList
                    indicatorString += idIndicator & ","
                Next
                If Not indicatorString.Equals("") Then
                    indicatorString = indicatorString.Remove(indicatorString.Length - 1)
                End If

                sql.Remove(0, sql.Length)
                sql.Append(" SELECT DISTINCT Indicator.Id, Indicator.Description, Indicator.IdUser, Project.Id AS ProjectId, Project.Name AS projectName, ")
                sql.Append(" Project.IdPhase AS projectPhase, ApplicationUser.Name AS userName ")
                sql.Append(" FROM Indicator AS Indicator INNER JOIN ")
                sql.Append(" 	Project ON Indicator.IdEntities = Project.idKey and Project.IsLastVersion='1' INNER JOIN ")
                sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Indicator.IdUser = ApplicationUser.ID ")
                sql.Append(" WHERE Indicator.Id IN (" & indicatorString & ") ")

                where = "AND"

                Integer.TryParse(id, idi)

                ' verificar si hay entrada de datos para el campo
                If Not id.Equals("") Then

                    sql.Append(where & " Indicator.Id = '" & idi & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idproject.Equals("") Then

                    sql.Append(where & " Project.Id = '" & idproject & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not projectname.Equals("") Then

                    sql.Append(where & " Project.Name like '%" & projectname & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not projectPhase.Equals("") Then

                    sql.Append(where & " Project.IdPhase = '" & projectPhase & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not projectPhaseText.Equals("") Then

                    sql.Append(where & " Project.IdPhase IN ")
                    sql.Append(" (SELECT Value FROM (SELECT 'Formulación' AS Impact, 1 AS Value ")
                    sql.Append(" UNION SELECT 'Formulacion' AS Impact, 1 AS Value ")
                    sql.Append(" UNION SELECT 'Planeación' AS Impact, 2 AS Value ")
                    sql.Append(" UNION SELECT 'Planeacion' AS Impact, 2 AS Value ")
                    sql.Append(" UNION SELECT 'Ejecución' AS Impact, 3 AS Value ")
                    sql.Append(" UNION SELECT 'Ejecucion' AS Impact, 3 AS Value ")
                    sql.Append(" UNION SELECT 'Evaluación' AS Impact, 4 AS Value ")
                    sql.Append(" UNION SELECT 'Evaluacion' AS Impact, 4 AS Value) Temp ")
                    sql.Append(" WHERE Impact LIKE '%" & projectPhaseText & "%') ")
                    where = " AND "

                End If


                ' verificar si hay entrada de datos para el campo
                If Not name.Equals("") Then

                    sql.Append(where & " Indicator.Description like '%" & name & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not iduser.Equals("") Then

                    sql.Append(where & " ( Indicator.IdResponsable= '" & iduser & "') ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                'If Not iduser.Equals("") Then

                '    sql.Append(where & " Indicator.IdUser = '" & iduser & "'")
                '    where = " AND "

                'End If

                ' verificar si hay entrada de datos para el campo
                If Not username.Equals("") Then

                    sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                    where = " AND "

                End If

                ' ejecutar la intruccion
                data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

                For Each row As DataRow In data.Rows

                    ' cargar los datos
                    objSubActivityMainPanel = New SubActivityMainPanelEntity

                    ' cargar el valor del campo
                    objSubActivityMainPanel.id = row("Id")
                    objSubActivityMainPanel.idReal = (row("Id"))
                    objSubActivityMainPanel.idproject = row("ProjectId")
                    objSubActivityMainPanel.projectname = row("ProjectName")
                    objSubActivityMainPanel.projectphase = row("ProjectPhase")
                    objSubActivityMainPanel.name = row("Description")
                    objSubActivityMainPanel.type = 2
                    objSubActivityMainPanel.iduser = row("iduser")
                    objSubActivityMainPanel.username = row("userName")
                    objSubActivityMainPanel.state = 1
                    objSubActivityMainPanel.idcomponent = 0
                    objSubActivityMainPanel.componentname = ""
                    objSubActivityMainPanel.attachment = ""

                    'Agregar al objeto su lista de Objetivos estrategicos, Lineas Estretegicas, Estrategias
                    data1 = listDetails(objApplicationCredentials, objSubActivityMainPanel.idproject)

                    strategicobjectiveList = New List(Of listDetail)
                    StrategicLineList = New List(Of listDetail)
                    strategyList = New List(Of listDetail)

                    For Each data1Row As DataRow In data1.Rows

                        If Not IsDBNull(data1Row("IdStrategicObjective")) Then
                            Dim strategicobjective As New listDetail
                            strategicobjective._id = data1Row("IdStrategicObjective")
                            strategicobjective._name = data1Row("StrategicObjectiveName")

                            'Objetivos estrategicos correspondientes al indicador
                            repeat = False
                            For Each it As listDetail In strategicobjectiveList
                                If it.Equals(strategicobjective) Then
                                    repeat = True
                                End If
                            Next
                            If Not repeat Then
                                strategicobjectiveList.Add(strategicobjective)
                            End If

                        End If

                        If Not IsDBNull(data1Row("IdStrategicLine")) Then
                            Dim StrategicLine As New listDetail
                            StrategicLine._id = data1Row("IdStrategicLine")
                            StrategicLine._name = data1Row("StrategicLineName")

                            'Lineas estrategicas correspondientes al indicador
                            repeat = False
                            For Each it As listDetail In StrategicLineList
                                If it.Equals(StrategicLine) Then
                                    repeat = True
                                End If
                            Next
                            If Not repeat Then
                                StrategicLineList.Add(StrategicLine)
                            End If

                        End If

                        If Not IsDBNull(data1Row("IdStrategy")) Then
                            Dim strategy As New listDetail
                            strategy._id = data1Row("IdStrategy")
                            strategy._name = data1Row("StrategyName")

                            'Estrategias correspondientes al indicador
                            repeat = False
                            For Each it As listDetail In strategyList
                                If it.Equals(strategy) Then
                                    repeat = True
                                End If
                            Next
                            If Not repeat Then
                                strategyList.Add(strategy)
                            End If

                        End If

                    Next

                    measuramentDateByIndicatorList = New List(Of MeasurementDateByIndicatorEntity)
                    'Asignar las fechas para registrar

                    For Each mdbi As MeasurementDateByIndicatorEntity In measuramentDateList

                        If mdbi.idindicator = objSubActivityMainPanel.id Then
                            Dim measuramentDateByIndicator As New MeasurementDateByIndicatorEntity
                            measuramentDateByIndicator.id = mdbi.id
                            measuramentDateByIndicator.idindicator = mdbi.idindicator
                            measuramentDateByIndicator.measurementdate = mdbi.measurementdate
                            measuramentDateByIndicatorList.Add(measuramentDateByIndicator)
                        End If

                    Next

                    objSubActivityMainPanel.strategicobjective = strategicobjectiveList
                    objSubActivityMainPanel.StrategicLine = StrategicLineList
                    objSubActivityMainPanel.strategy = strategyList
                    objSubActivityMainPanel.measuramentDateByIndicator = measuramentDateByIndicatorList

                    ' agregar a la lista
                    SubActivityMainPanelList.Add(objSubActivityMainPanel)

                Next

            End If

            'Se llama al metodo que permite consultar las actividades de las estrategias de la cola de tareas próxima.
            Me.loadStrategicActivities(objApplicationCredentials, SubActivityMainPanelList, beforeDays, _
            id, idlike, idstrategicobjective, strategicobjectivename, idstrategy, strategyname, projectname, _
            projectPhaseText, name, type, typetext, state, statetext, begindate, enddate, iduser, username)

            'Filtrar por Objetivo estrategico, Lineas estrategicas, estrategia, tipo y/o estado
            If Not idstrategicobjective.Equals("") Or Not strategicobjectivename.Equals("") Or Not idStrategicLine.Equals("") Or _
            Not StrategicLinename.Equals("") Or Not idstrategy.Equals("") Or Not strategyname.Equals("") Or Not type.Equals("") Or _
            Not typetext.Equals("") Or Not state.Equals("") Or Not statetext.Equals("") Or _
            Not approval.Equals("") Or Not approvalText.Equals("") Or Not idcomponent.Equals("") Or _
            Not componentname.Equals("") Then

                ' Lista auxiliar para guardar concordancias
                Dim sampList As New List(Of SubActivityMainPanelEntity)

                For Each samp As SubActivityMainPanelEntity In SubActivityMainPanelList

                    ' Se reusa la variable para indicar que un registro concuerda con la busqueda
                    repeat = False

                    ' verificar si hay entrada de datos para el campo
                    If Not idstrategicobjective.Equals("") Then
                        For Each ld As listDetail In samp.strategicobjective
                            If ld._id.ToString.Equals(idstrategicobjective) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not strategicobjectivename.Equals("") Then
                        For Each ld As listDetail In samp.strategicobjective
                            If ld._name.ToUpper.ToString.Contains(strategicobjectivename.ToUpper) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not idStrategicLine.Equals("") Then
                        For Each ld As listDetail In samp.StrategicLine
                            If ld._id.ToString.Equals(idStrategicLine) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not StrategicLinename.Equals("") Then
                        For Each ld As listDetail In samp.StrategicLine
                            If ld._name.ToUpper.ToString.Contains(StrategicLinename.ToUpper) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not idstrategy.Equals("") Then
                        For Each ld As listDetail In samp.strategy
                            If ld._id.ToString.Equals(idstrategy) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not strategyname.Equals("") Then
                        For Each ld As listDetail In samp.strategy
                            If ld._name.ToUpper.ToString.Contains(strategyname.ToUpper) Then
                                repeat = True
                            End If
                        Next
                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not type.Equals("") Then

                        If samp.type = type Then
                            repeat = True
                        End If

                    End If

                    ' verificar si hay entrada de datos para el campo
                    If Not typetext.Equals("") Then

                        If samp.typeText.ToUpper.Contains(typetext.ToUpper) Then
                            repeat = True
                        End If

                    End If

                    If Not state.Equals("") Then
                        If samp.state = state Then
                            repeat = True
                        End If
                    End If

                    If Not statetext.Equals("") Then
                        If samp.stateText.ToUpper.Contains(statetext.ToUpper) Then
                            repeat = True
                        End If
                    End If

                    If Not approval.Equals("") Then
                        If samp.approval = Boolean.Parse(approval) Then
                            repeat = True
                        End If
                    End If

                    If Not approvalText.Equals("") Then
                        If samp.approvalText.ToUpper.Contains(approvalText.ToUpper) Then
                            repeat = True
                        End If
                    End If

                    If Not idcomponent.Equals("") Then
                        If samp.idcomponent = idcomponent Then
                            repeat = True
                        End If
                    End If

                    If Not componentname.Equals("") Then
                        If samp.componentname.ToUpper.Contains(componentname.ToUpper) Then
                            repeat = True
                        End If
                    End If

                    If repeat Then
                        sampList.Add(samp)
                    End If

                Next

                SubActivityMainPanelList = sampList

            End If

            ' Ordenamiento
            Dim objSubActivityMainPanelComparer As New SubActivityMainPanelComparer
            objSubActivityMainPanelComparer.order = order
            SubActivityMainPanelList.Sort(objSubActivityMainPanelComparer)

            ' retornar el objeto
            getList = SubActivityMainPanelList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Panel General de SubActividades. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSubActivityMainPanel = Nothing
            SubActivityMainPanelList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Creado Por Jose Olmes Torres Jimenez, Junio 11/2010
    ''' Metodo que permite consultar las actividades de la estrategia de la lista de tareas pendientes
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="SubActivityMainPanelList"></param>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idstrategicobjective"></param>
    ''' <param name="strategicobjectivename"></param>
    ''' <param name="idstrategy"></param>
    ''' <param name="strategyname"></param>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="typetext"></param>
    ''' <param name="state"></param>
    ''' <param name="statetext"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <remarks></remarks>
    Private Sub loadStrategicActivities(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal SubActivityMainPanelList As List(Of SubActivityMainPanelEntity), _
        ByVal beforeDays As Integer, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idstrategicobjective As String = "", _
        Optional ByVal strategicobjectivename As String = "", _
        Optional ByVal idstrategy As String = "", _
        Optional ByVal strategyname As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal projectPhaseText As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal state As String = "", _
        Optional ByVal statetext As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "")

        'Se verifica si el ususario realizó algun filtro por proyecto o fase del proyecto,
        'en este caso no se realiza la consulta de actividades de la estrategia
        If projectname.Length = 0 AndAlso projectPhaseText.Length = 0 Then

            ' definiendo los objetos
            Dim objSubActivityMainPanel As SubActivityMainPanelEntity
            Dim StrategicLineList As List(Of listDetail)
            Dim strategicobjectiveList As List(Of listDetail)
            Dim strategyList As List(Of listDetail)
            Dim measuramentDateByIndicatorList As List(Of MeasurementDateByIndicatorEntity)
            Dim sql As New StringBuilder
            Dim data As DataTable
            Dim where As String = " WHERE "
            Dim idi As Integer = 0
            ' obtener el nombre de la base de datos de seguridad
            Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
            Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
            sql.Append("SELECT StrategicActivity.Id, StrategicActivity.Name, StrategicActivity.BeginDate,")
            sql.Append(" StrategicActivity.EndDate, StrategicActivity.IdUser, ApplicationUser.Name AS userName,")
            sql.Append(" Strategy.Id AS IdStrategy, Strategy.Name AS StrategyName,")
            sql.Append(" StrategicObjective.Id AS IdStrategicObjective, StrategicObjective.Name AS StrategicObjectiveName")
            sql.Append(" FROM StrategicActivity ")
            sql.Append(" INNER JOIN Strategy ON StrategicActivity.IdStrategy = Strategy.Id")
            sql.Append(" INNER JOIN StrategicObjective ON Strategy.IdStrategicObjective = StrategicObjective.Id")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON StrategicActivity.IdUser = ApplicationUser.ID")

            'Se convierte el id a un valor entero
            Integer.TryParse(id, idi)

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " (StrategicActivity.Id = '" & idi & "') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " (StrategicActivity.Id LIKE '%" & idlike & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " (StrategicActivity.Name like '%" & name & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " (StrategicActivity.IdUser = '" & iduser & "') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " (ApplicationUser.Name like '%" & username & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If begindate.Equals("") And enddate.Equals("") Then

                sql.Append(where & "  ( (StrategicActivity.BeginDate BETWEEN '" & Today.ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "') ")
                sql.Append(" OR (StrategicActivity.EndDate BETWEEN '" & Today.ToString("yyyyMMdd HH:mm:ss") & "' AND '" & Today.AddDays(beforeDays).ToString("yyyyMMdd HH:mm:ss") & "')  ) ")
                where = " AND "

            Else

                ' verificar si hay entrada de datos para el campo
                If Not begindate.Equals("") Then

                    sql.Append(where & " (CONVERT(NVARCHAR, StrategicActivity.begindate, 103) like '%" & begindate & "%') ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not enddate.Equals("") Then

                    sql.Append(where & " (CONVERT(NVARCHAR, StrategicActivity.enddate, 103) like '%" & enddate & "%') ")
                    where = " AND "

                End If

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSubActivityMainPanel = New SubActivityMainPanelEntity

                ' cargar el valor del campo
                objSubActivityMainPanel.id = row("Id")
                objSubActivityMainPanel.name = row("Name")
                objSubActivityMainPanel.type = 1
                objSubActivityMainPanel.begindate = IIf(Not IsDBNull(row("BeginDate")), row("BeginDate"), Nothing)
                objSubActivityMainPanel.enddate = IIf(Not IsDBNull(row("EndDate")), row("EndDate"), Nothing)
                objSubActivityMainPanel.iduser = row("IdUser")
                objSubActivityMainPanel.username = row("userName")
                objSubActivityMainPanel.projectname = ""
                objSubActivityMainPanel.projectphase = 0
                objSubActivityMainPanel.componentname = ""

                'Se agrega la estrategia y el objetivo estratégico
                StrategicLineList = New List(Of listDetail)
                strategicobjectiveList = New List(Of listDetail)
                strategyList = New List(Of listDetail)
                measuramentDateByIndicatorList = New List(Of MeasurementDateByIndicatorEntity)

                Dim StrategicLine As New listDetail
                StrategicLine._id = 0
                StrategicLine._name = ""
                StrategicLineList.Add(StrategicLine)

                If Not IsDBNull(row("IdStrategicObjective")) Then
                    Dim strategicobjective As New listDetail
                    strategicobjective._id = row("IdStrategicObjective")
                    strategicobjective._name = row("StrategicObjectiveName")
                    strategicobjectiveList.Add(strategicobjective)
                End If

                If Not IsDBNull(row("IdStrategy")) Then
                    Dim strategy As New listDetail
                    strategy._id = row("IdStrategy")
                    strategy._name = row("StrategyName")
                    strategyList.Add(strategy)
                End If

                Dim measuramentDateByIndicator As New MeasurementDateByIndicatorEntity
                measuramentDateByIndicator.id = 0
                measuramentDateByIndicator.idindicator = 0
                measuramentDateByIndicator.measurementdate = Nothing
                measuramentDateByIndicatorList.Add(measuramentDateByIndicator)

                objSubActivityMainPanel.StrategicLine = StrategicLineList
                objSubActivityMainPanel.strategicobjective = strategicobjectiveList
                objSubActivityMainPanel.strategy = strategyList
                objSubActivityMainPanel.measuramentDateByIndicator = measuramentDateByIndicatorList

                ' agregar a la lista
                SubActivityMainPanelList.Add(objSubActivityMainPanel)

            Next

        End If

    End Sub

    Private Function listDetails(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal IdProject As Integer) As DataTable

        ' definiendo los objetos
        Dim sql As New StringBuilder
        listDetails = New DataTable

        Try

            'Construir la consulta
            sql.Append(" SELECT ProgramComponent.Id AS IdProgramComponent, ProgramComponent.Name AS ProgramComponentName, Program.Id AS IdProgram, ")
            sql.Append(" 	Program.Name AS ProgramName, StrategicLine.Id AS IdStrategicLine, StrategicLine.Name AS StrategicLineName, ")
            sql.Append(" 	StrategicObjective.Id AS IdStrategicObjective, StrategicObjective.Name AS StrategicObjectiveName, Strategy.Id AS IdStrategy, ")
            sql.Append(" 	Strategy.Name AS StrategyName, ProgramComponentByProject.IdProject AS ProjectId ")
            sql.Append(" FROM ProgramComponentByProject LEFT OUTER JOIN ")
            sql.Append(" 	ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
            sql.Append(" 	Program ON ProgramComponent.IdProgram = Program.Id LEFT OUTER JOIN ")
            sql.Append(" 	StrategicLine ON Program.IdStrategicLine = StrategicLine.Id LEFT OUTER JOIN ")
            sql.Append(" 	StrategicObjective ON StrategicLine.IdStrategicObjective = StrategicObjective.Id LEFT OUTER JOIN ")
            sql.Append(" 	Strategy ON StrategicObjective.Id = Strategy.IdStrategicObjective ")
            sql.Append(" WHERE (ProgramComponentByProject.IdProject =" & IdProject & ") ")

            ' ejecutar la intruccion
            listDetails = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception
            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar algunos detalles de la lista de Panel General de SubActividades. ")

        Finally
            ' liberando recursos
            sql = Nothing
        End Try

    End Function

End Class