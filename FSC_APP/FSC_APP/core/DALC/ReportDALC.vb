Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ReportDALC
    ' contantes
    Const MODULENAME As String = "ReportDALC"

    ''' <summary>
    ''' Consulta los responsables de los indicadores
    ''' </summary>
    ''' <remarks></remarks>
    Public Function loadIndicatorUsers(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            sql.Append(" SELECT DISTINCT a.ID, a.Name, a.Code ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.ApplicationUser a, Indicator I ")
            sql.Append(" WHERE a.ID = I.IdUser ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadIndicatorUsers ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    '''Carga la consulta del reporte de plan general
    ''' </summary>
    ''' <param name="sYear">El año que quiere filtrar</param>
    ''' <remarks></remarks>
    Public Function loadReportGeneralPlan(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal sYear As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
           
            'sql.Append(" SELECT Perspective.NAME  as Perspective, Perspective.Id as IdPerspective , StrategicObjective.Name as StrategicObjective,")
            'sql.Append(" StrategicObjective.id  as IdStrategicObjective   , Indicator.Description,Indicator.Id as IdIndicator, Indicator.Goal, Indicator.GreenValue, Indicator.YellowValue, Indicator.RedValue, EntityDetail.Name as StrategyStrategicLine,Cast(EntityDetail.Id as nvarchar(10)) +  EntityDetail.Type as IdStrategyStrategicLine, EntityDetail.Type, EntityDetail.Id as IdStrategyStrategicLine1 ")
            'sql.Append("    FROM Perspective ")
            'sql.Append(" INNER JOIN StrategicObjective ON Perspective.id = StrategicObjective.idPerspective ")
            'sql.Append(" INNER JOIN Indicator ON StrategicObjective.id = Indicator.identities  ")
            'sql.Append(" INNER JOIN (SELECT Id, Name, idStrategicObjective, 'Estrategia' as TYPE FROM Strategy UNION ")
            'sql.Append(" SELECT Id, Name, idStrategicObjective, 'Linea Estrategica' as TYPE FROM StrategicLine  ) EntityDetail ")
            'sql.Append(" ON StrategicObjective.Id = EntityDetail.idStrategicObjective ")
            'sql.Append(" Where StrategicObjective.Year=  " & sYear)
            'sql.Append(" Order by Perspective.Code, StrategicObjective.Name, Indicator.Description, EntityDetail.Name ")



            sql.Append(" SELECT Perspective.NAME  as Perspective, Perspective.Id as IdPerspective , StrategicObjective.Name as StrategicObjective,")
            sql.Append(" StrategicObjective.id  as IdStrategicObjective   , '' as Description, ''  as IdIndicator, '' as Goal, '' as GreenValue, '' as YellowValue, '' as RedValue, EntityDetail.Name as StrategyStrategicLine,Cast(EntityDetail.Id as nvarchar(10)) +  EntityDetail.Type as IdStrategyStrategicLine, EntityDetail.Type, EntityDetail.Id as IdStrategyStrategicLine1 ")
            sql.Append("    FROM Perspective ")
            sql.Append(" INNER JOIN StrategicObjective ON Perspective.id = StrategicObjective.idPerspective ")
            sql.Append(" INNER JOIN (SELECT Id, Name, idStrategicObjective, 'Estrategia' as TYPE FROM Strategy UNION ")
            sql.Append(" SELECT Id, Name, idStrategicObjective, 'Linea Estrategica' as TYPE FROM StrategicLine  ) EntityDetail ")
            sql.Append(" ON StrategicObjective.Id = EntityDetail.idStrategicObjective ")
            sql.Append(" Where StrategicObjective.Year=  " & sYear)
            sql.Append(" Order by Perspective.Code, StrategicObjective.Name,  EntityDetail.Name ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)



            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportGeneralPlan")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportGeneralPlan ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <returns>un objeto de tipo List(Of STRATEGICOBJECTIVEEntity)</returns>
    ''' <remarks></remarks>
    Public Function getListYear(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of STRATEGICOBJECTIVEEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSTRATEGICOBJECTIVE As STRATEGICOBJECTIVEEntity
        Dim STRATEGICOBJECTIVEList As New List(Of STRATEGICOBJECTIVEEntity)
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
       

        Try
            ' construir la sentencia
            sql.Append("SELECT   Distinct  Year FROM  StrategicObjective order by Year Desc")




            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSTRATEGICOBJECTIVE = New STRATEGICOBJECTIVEEntity

                ' cargar el valor del campo

                objSTRATEGICOBJECTIVE.year = row("year")
                ' agregar a la lista
                STRATEGICOBJECTIVEList.Add(objSTRATEGICOBJECTIVE)

            Next

            ' retornar el objeto
            getListYear = STRATEGICOBJECTIVEList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getListYear")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de STRATEGICOBJECTIVE. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSTRATEGICOBJECTIVE = Nothing
            STRATEGICOBJECTIVEList = Nothing
            data = Nothing


        End Try

    End Function

    ''' <summary>
    '''Carga la consulta del reporte del detalle de la estrategia
    ''' </summary>
    ''' <param name="sIdStrategy">El id de la estrategia a buscar</param>
    ''' <remarks></remarks>
    Public Function loadReportStrategyDetail(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal sIdStrategy As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT Strategy.Id AS IdStrategy, Strategy.Name AS StrategyName, Strategy.Code AS StrategyCode, StrategicActivity.Name AS StrategicActivityName, StrategicActivity.Id as IdStrategicActivity, ")
            sql.Append(" StrategicActivity.Description AS ActivityDescription, StrategicActivity.BeginDate, StrategicActivity.EstimatedValue, Indicator.Id as IdIndicator, ")
            sql.Append("Indicator.Description AS Description, Indicator.Goal, Indicator.GreenValue, Indicator.YellowValue,Indicator.RedValue")
            sql.Append(" FROM Strategy LEFT JOIN  StrategicActivity ON Strategy.Id = StrategicActivity.IdStrategy INNER JOIN")
            sql.Append("  Indicator ON Strategy.Id = Indicator.IdEntities AND Indicator.LevelIndicator = '2.2'")
            ' verificar si hay entrada de datos para el campo

            If Not sIdStrategy.Equals("") Then

                sql.Append(where & " Strategy.Id='" & sIdStrategy & "'")
                where = " AND "

            End If

            sql.AppendLine(" ORDER By Strategy.Name ASC, StrategicActivity.Name ASC  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)



            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportGeneralPlan")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportGeneralPlan ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    '''Carga las mediciones de los indicadores
    ''' </summary>
    ''' <param name="sIdIndicator">El id del indicador</param>
    ''' <remarks></remarks>
    Public Function loadMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal sIdIndicator As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        Try
            ' construir la sentencia
            sql.Append(" SELECT     IdIndicator, measurementDate, measure, measuretype ")
            sql.Append(" FROM   MeasurementDateByIndicator ")
            If Not sIdIndicator.Equals("") Then
                sql.Append(where & "  (IdIndicator=" & sIdIndicator & ")")
            End If

            sql.AppendLine(" ORDER By measurementDate Desc  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)



            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadMeasurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadMeasurementDateByIndicator")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    '''Carga la consulta del reporte del detalle de la Linea Estrategica
    ''' </summary>
    ''' <param name="sIdStrategicLine">El id de la linea Estrategica a buscar</param>
    ''' <remarks></remarks>
    Public Function loadReportStrategicLineDetail(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal sIdStrategicLine As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
          
            sql.Append(" SELECT p.Name AS StrategicLineName, p.Id as IdStrategicLine, p.Code as StrategicLineCode, ma.Name AS ProgramName, sa.Name AS ProgramComponentName, sa.Description AS ProgramComponentDescription ")
            sql.Append(" FROM StrategicLine as p INNER JOIN Program as ma ON p.Id = ma.IdStrategicLine INNER JOIN ProgramComponent as sa ON ma.Id = sa.IdProgram ")

            If Not sIdStrategicLine.Equals("") Then
                sql.Append(where & "  p.Id = '" & sIdStrategicLine & "'")
                where = " AND "

            End If

            sql.AppendLine(" ORDER By p.Name ASC,ma.Name ASC, sa.Name ASC  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)



            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportStrategicLineDatail ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de Inventario de Indicadores
    ''' </summary>
    ''' <param name="IndicatorType">Tipo de indicador</param>
    ''' <param name="BeginDate">Fecha de medición inicial</param>
    ''' <param name="EndDate">Fecha de medición final</param>
    ''' <remarks></remarks>
    Public Function loadReportIndicatorInventory(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                                    ByVal IndicatorType As String, _
                                                    ByVal BeginDate As String, _
                                                    ByVal EndDate As String, _
                                                    ByVal idUser As String) As DataTable
        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim Table As String

        Try
            Select Case IndicatorType

                Case "2" : Table = "Program"
                Case "1.1" : Table = "StrategicLine"
                Case "1.2" : Table = "Strategy"

            End Select

            'If IndicatorType = "1" Then
            '    Table = "StrategicObjective"
            'Else
            '    Table = "Program"
            'End If

            ' construir la sentencia
            sql.AppendLine(" SELECT Distinct Entity.Name, Description, Goal, GreenValue, YellowValue, RedValue, Indicator.Id, Indicator.LevelIndicator,   ")
            sql.AppendLine(" 	CASE Indicator.LevelIndicator      ")
            sql.AppendLine(" 		WHEN '3' THEN 'Tercer Nivel - Proyecto'   ")
            sql.AppendLine(" 		WHEN '1.1' THEN 'Primer Nivel - Linea Estrategica'    ")
            sql.AppendLine(" 		WHEN '1.2' THEN 'Primer Nivel - Estrategia'      ")
            sql.AppendLine(" 		WHEN '2' THEN 'Segundo Nivel - Programa' END AS IndicatorType   ")
            sql.AppendLine(" FROM Indicator ")
            sql.AppendLine(" 	LEFT OUTER JOIN " & Table & " Entity ON Indicator.IdEntities = Entity.Id ")
            sql.AppendLine(" 	LEFT OUTER JOIN MeasurementDateByIndicator  ON Indicator.Id = MeasurementDateByIndicator.IdIndicator ")
            sql.AppendLine(" WHERE LevelIndicator = '" & IndicatorType & "'")

            ' verificar si existen fechas en el filtro
            If Not BeginDate.Equals(String.Empty) And Not EndDate.Equals(String.Empty) Then

                sql.AppendLine(" 	AND MeasurementDate BETWEEN '" & CDate(BeginDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")

            End If

            ' verificar si existen usuario
            If Not idUser.Equals(String.Empty) Then

                sql.AppendLine(" 	AND Indicator.idUser = " & idUser)

            End If

            sql.AppendLine(" ORDER By Indicator.LevelIndicator ASC  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportStrategicLineDatail ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    Public Function loadIndicators(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal IdStrategicLine As Integer, ByVal IdStrategy As Integer) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim IndicatorType As String

        Try
            If IdStrategicLine = -1 Then
                IndicatorType = "1.2"
            Else
                IndicatorType = "1.1"
            End If
            ' construir la sentencia
            sql.Append(" SELECT i.Description, i.Goal, i.GreenValue, i.YellowValue, i.RedValue, m.measurementDate, ")
            sql.Append(" FROM Indicator i, MeasurementDateByIndicator m ")
            sql.Append(" WHERE i.LevelIndicator = '" & IndicatorType & "' AND i.IdEntities = " & IdStrategicLine & " AND i.Id = m.IdIndicator ORDER BY m.measurementDate DESC ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicators")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los indicadores de una Linea estrategica o estrategia ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información básica del proyecto
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Function loadReportBasicProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As String, _
    ByVal code As String, _
    ByVal year As Integer, _
    ByVal idProjectPhase As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " AND "
        Dim idProjectList As String = ""
        Dim isConsultByPhase As Boolean

        Try
            'Se verifica si viene algun valor de fase y de proyecto
            If (idProjectPhase.Length > 0 AndAlso idProject.Length > 0) Then

                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                isConsultByPhase = True

            ElseIf (idProjectPhase.Length > 0) Then

                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                isConsultByPhase = True

            End If

            If isConsultByPhase Then
                'Se consruye la instrucción de consulta
                sql.Append(" SELECT * FROM vReportBasicProjectData WHERE idProject IN (" & idProjectList & ")")
            Else
                'Se consruye la instrucción de consulta
                sql.Append(" SELECT * FROM vReportBasicProjectData WHERE isLastVersion=1")
            End If

            If idProject.Length > 0 Then
                'Se verifica si es una consulta por fase
                If Not isConsultByPhase Then
                    sql.Append(where & " idkey = " & idProject & " and isLastVersion=1")
                End If
                where = " AND "
            End If

            If code.Length > 0 Then
                sql.Append(where & " Code = '" & code & "' ")
                where = " AND "
            End If

            If year <> 0 Then
                sql.Append(where & " YEAR(CreateDate) = " & year & " ")
                where = " AND "
            End If

            ' ejecutar la intruccion y retornar el objeto
            loadReportBasicProject = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta del reporte datos básicos del proyecto.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar las ultimas versiones de los proyectos
    ''' que pertenecen a una fase determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject">identificador del proyecto</param>
    ''' <param name="idProjectPhase">identificador de la fase</param>
    ''' <returns>una cadena con la lista de los IdProject de los proyectos requeridos</returns>
    ''' <remarks></remarks>
    Private Function loadConsultIdProjectsListByPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As String, ByVal idProjectPhase As String) As String

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        Dim idProjectList As String = ""
        Dim data As DataTable

        'Se construye la intraucción requerida
        sql.Append("SELECT vReportBasicProjectData.IdKey,Subconsult.maxIdProject")
        sql.Append(" FROM vReportBasicProjectData INNER JOIN")
        sql.Append("(SELECT IdKey, (Select Max(Id) FROM Project WHERE Project.IdKey = vReportBasicProjectData.IdKey")
        If idProjectPhase.Length > 0 Then
            sql.Append(" AND  IdPhase='" & idProjectPhase & "'")
        End If
        sql.Append(") AS maxIdProject FROM vReportBasicProjectData ) AS Subconsult")
        sql.Append(" ON vReportBasicProjectData.IdKey = Subconsult.IdKey")

        'Se verifica si viene algun valor para el campo
        If idProject.Length > 0 AndAlso idProject <> "0" Then
            sql.Append(where & " vReportBasicProjectData.idkey = " & idProject)
            where = " AND "
        End If

        'Se verifica si viene algun valor para el campo
        If idProjectPhase.Length > 0 Then
            sql.Append(where & "IdPhase='" & idProjectPhase & "'")
            where = " AND "
        End If

        sql.Append(" GROUP BY vReportBasicProjectData.IdKey,Subconsult.maxIdProject ")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        For Each row As DataRow In data.Rows
            ' cargar el valor del campo
            idProjectList &= row("maxIdProject") & ","
        Next

        'Se ajusta la lista de idProjects
        If idProjectList.Length > 0 Then
            idProjectList = idProjectList.Substring(0, idProjectList.Length - 1)
        Else
            idProjectList = "0"
        End If

        ' finalizar la transaccion
        CtxSetComplete()

        Return idProjectList

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de ubicaciones por proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del ususuario</param>   
    ''' <param name="idDepto">Identificador del departamento</param>
    ''' <param name="idCity">Identificadores de las ciudades</param>
    ''' <remarks></remarks>
    Public Function loadReportLocationsByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idProject As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportLocationsByProject")

            'Verificar si hay entrada de datos para el campo
            If idProject.Length > 0 Then

                sql.Append(where & " IdProject = " & idProject)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then

                sql.Append(where & " IdDepto = " & idDepto)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then

                sql.Append(where & " IdCity = " & idCity)
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de ubicaciones por proyecto.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de fuentes por proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del ususuario</param>   
    ''' <param name="idProject">Identificador del proyecto</param>   
    ''' <remarks></remarks>
    Public Function loadReportSourceByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idProject As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportSourceByProject")

            'Verificar si hay entrada de datos para el campo
            If idProject.Length > 0 Then

                sql.Append(where & " IdProject = " & idProject)
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de ubicaciones por proyecto.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta la matriz de indicadores
    ''' Consulta las fechas de medición de los indicadores segun año y/o nombre de proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="typeIndicator"></param>
    ''' <param name="year"></param>
    ''' <remarks></remarks>
    Public Function MatrixIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idproject As Integer, _
    ByVal typeIndicator As Integer, _
    ByVal year As Integer, _
    ByVal idProjectPhase As String) As DataSet

        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        MatrixIndicator = New DataSet("dsFormulation")

        Try
            sql.Append(" SELECT (SELECT CASE WHEN Indicator.Type = 1 THEN 'Beneficiarios' WHEN Indicator.Type = 2 THEN 'Capacidad instalada' WHEN Indicator.Type = 3 THEN 'Gestión del conocimiento' ")
            sql.Append(" 	END AS Expr1) AS Type, Indicator.Description, Indicator.Goal, Indicator.GreenValue, Indicator.YellowValue, ")
            sql.Append(" 	Indicator.RedValue, Indicator.SourceVerification, Indicator.Assumptions, Project.Id AS ProjectId, ")
            sql.Append(" 	Project.Name AS projectName, Indicator.Id ")
            sql.Append(" FROM Indicator INNER JOIN ")
            sql.Append(" 	Project ON Indicator.IdEntities = Project.idkey")
            sql.Append(" WHERE (Indicator.LevelIndicator = '3') ")
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idproject, idProjectPhase)
                sql.Append(" AND (Project.Id IN (" & idProjectList & "))")
            Else
                sql.Append(" AND (Project.idKey = " & idproject & " and Project.IsLastVersion='1') ")
            End If
            If typeIndicator <> 0 Then
                sql.Append(" AND (Indicator.Type = " & typeIndicator & ") ")
            End If
            sql.Append(" ORDER BY projectName ")

            ' ejecutar la intruccion y adicionar la tabla al dataset
            MatrixIndicator.Tables.Add(GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString).Copy)
            MatrixIndicator.Tables(0).TableName = "MatrixIndicator"

            sql.Remove(0, sql.Length)
            sql.Append(" SELECT MeasurementDateByIndicator.measurementDate, MeasurementDateByIndicator.IdIndicator, MeasurementDateByIndicator.measure, MeasurementDateByIndicator.measuretype, project.Id AS projectId ")
            sql.Append(" FROM MeasurementDateByIndicator INNER JOIN ")
            sql.Append(" 	Indicator ON MeasurementDateByIndicator.IdIndicator = Indicator.Id INNER JOIN ")
            sql.Append(" 	Project ON Indicator.IdEntities = Project.idkey")
            sql.Append(" WHERE (Indicator.LevelIndicator = '3') ")
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idproject, idProjectPhase)
                sql.Append(" AND (Project.Id IN (" & idProjectList & "))")
            Else
                sql.Append(" AND Project.idkey = " & idproject & " and Project.IsLastVersion='1'")
            End If

            If year <> 0 Then
                sql.Append(" AND YEAR(MeasurementDateByIndicator.measurementDate) = '" & year & "'")
            End If

            ' ejecutar la intruccion y adicionar la tabla al dataset
            MatrixIndicator.Tables.Add(GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString).Copy)
            MatrixIndicator.Tables(1).TableName = "MeasurementDateByIndicator"

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "MatrixIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar MatrixIndicator ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información del plan de ejecución
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <param name="code"></param>
    ''' <param name="year"></param>
    ''' <remarks></remarks>
    Public Function loadReportExecutionPlan(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As Integer, _
    ByVal code As String, _
    ByVal year As Integer, _
    ByVal idProjectPhase As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        Dim where As String = "WHERE"
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            sql.Append(" SELECT Component.Id AS IdComponent, Component.Name AS ComponentName, Activity.Title AS ActivityName,   ")
            sql.Append(" 	   Activity.Id AS IdActivity, SubActivity.Name AS SubActivityName, SubActivity.Id AS IdSubActivity,   ")
            sql.Append(" 	  ApplicationUser.Name AS Responsible, SubActivity.BeginDate, SubActivity.EndDate, SubActivity.Duration, SubActivity.TotalCost,  ")
            sql.Append(" 	 SubActivity.FSCContribution, SubActivity.OFContribution ")
            sql.Append(" 	FROM         Activity LEFT OUTER JOIN  " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser  ")
            sql.Append(" 	INNER JOIN  SubActivity ON ApplicationUser.ID = SubActivity.IdResponsible ON SubActivity.IdActivity= Activity.idkey and Activity.IsLastVersion='1'  ")
            sql.Append(" RIGHT OUTER JOIN  Objective INNER JOIN  Component ON Component.IdObjective=Objective.idkey and Objective.IsLastVersion='1'  ")
            sql.Append(" 	INNER JOIN   Project ON Objective.IdProject = Project.idkey")
            If (idProjectPhase.Length = 0) Then
                sql.Append(" and Project.IsLastVersion='1'")
            End If
            sql.Append(" ON Activity.IdComponent = Component.idkey and Component.IsLastVersion='1' ")

            sql.Append(where & " Component.IsLastVersion='1'  and  Activity.IsLastVersion='1' and SubActivity.islastversion='1' ")
            where = " AND "



            'Se verifica si llega un filtro por la fase
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                sql.Append(where & " Project.Id IN (" & idProjectList & ")")
                where = "OR"
            Else
                If idProject <> 0 Then
                    sql.Append(where & " Project.idkey = " & idProject)
                    where = "OR"
                End If
            End If

            If code <> "" Then
                sql.Append(where & " Project.Code = '" & code & "' ")
                where = "OR"
            End If

            If year <> 0 Then
                sql.Append(where & " YEAR(Project.CreateDate) = " & year & " ")
                where = "OR"
            End If

            sql.AppendLine(" ORDER BY ComponentName, ActivityName, SubActivityName  ")

            ' ejecutar la intruccion y retornar el objeto
            loadReportExecutionPlan = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadIndicatorUsers ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta la matriz de riesgos por proyecto y año de comienzo del proyecto
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <param name="year"></param>
    ''' <remarks></remarks>
    Public Function RiskMatrix(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As Integer, _
    ByVal year As Integer, _
    ByVal idProjectPhase As String) As DataSet

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        RiskMatrix = New DataSet("dsFormulation")
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            'Construyendo la sentencia
            sql.Append(" SELECT DISTINCT Risk.Id, Risk.Code, Risk.Name, Risk.Description, Risk.WhatCanHappen, ")
            sql.Append(" (SELECT CASE WHEN Risk.RiskImpact = 1 THEN 'Alto' WHEN Risk.RiskImpact = 2 THEN 'Medio' WHEN Risk.RiskImpact = 3 THEN 'Bajo' END AS IMPACT) as RiskImpact, ")
            sql.Append(" (SELECT CASE WHEN Risk.OcurrenceProbability = 1 THEN 'Alto' WHEN Risk.OcurrenceProbability = 2 THEN 'Medio' WHEN Risk.OcurrenceProbability = 3 THEN 'Bajo' END AS OCURRENCE) as OcurrenceProbability ")
            sql.Append(" FROM Risk INNER JOIN ")
            sql.Append(" ComponentByRisk ON ComponentByRisk.IdRisk=Risk.Id INNER JOIN  ")
            sql.Append(" Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN  ")
            sql.Append(" Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN  ")
            sql.Append(" Project ON Objective.IdProject = Project.idkey ")
            If (idProjectPhase.Length = 0) Then
                sql.Append(" and Project.IsLastVersion='1'")
            End If

            'Se verifica si llega un filtro por la fase
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                sql.Append(" WHERE Project.Id IN (" & idProjectList & ")")
            Else
                sql.Append(" WHERE Project.idkey = " & idProject & " and Project.IsLastVersion='1' ")
            End If

            'sql.Append(" AND Risk.isLastVersion = 1 ")

            If year <> 0 Then
                sql.Append(" AND YEAR(Project.BeginDate) = " & year & " ")
            End If

            sql.Append(" ORDER BY Risk.Name  ")

            ' ejecutar la intruccion y retornar el objeto
            RiskMatrix.Tables.Add(GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString).Copy)
            RiskMatrix.Tables(0).TableName = "Risk"


            'Construyendo la sentencia
            sql.Remove(0, sql.Length)
            sql.Append(" SELECT DISTINCT ComponentByRisk.IdRisk, ComponentByRisk.IdComponent, Component.Name as ComponentName ")
            sql.Append(" FROM ComponentByRisk INNER JOIN ")
            sql.Append(" Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN ")
            sql.Append(" Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN ")
            sql.Append(" Project ON   Objective.IdProject=Project.idKey ")
            If (idProjectPhase.Length = 0) Then
                sql.Append(" and Project.IsLastVersion='1'")
            End If

            'Se verifica si llega un filtro por la fase
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                sql.Append(" WHERE Project.Id IN (" & idProjectList & ")")
            Else
                sql.Append(" WHERE Project.idkey = " & idProject & " and Project.IsLastVersion='1' ")
            End If

            If year <> 0 Then
                sql.Append(" AND YEAR(Project.BeginDate) = " & year & " ")
            End If

            sql.Append(" ORDER BY ComponentName  ")

            ' ejecutar la intruccion y retornar el objeto
            RiskMatrix.Tables.Add(GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString).Copy)
            RiskMatrix.Tables(1).TableName = "ComponentByRisk"

            'Construyendo la sentencia
            sql.Remove(0, sql.Length)
            sql.Append(" SELECT DISTINCT Mitigation.Code,  Mitigation.Name as Description, MitigationByRisk.IdRisk,  ")
            sql.Append(" (SELECT CASE WHEN Mitigation.ImpactOnRisk = 1 THEN 'Alto' WHEN Mitigation.ImpactOnRisk = 2 THEN 'Medio' WHEN Mitigation.ImpactOnRisk = 3 THEN 'Bajo' END AS IMPACT) as ImpactOnRisk, ")
            sql.Append(" 	ApplicationUser.Name AS ResponsibleName ")
            sql.Append(" FROM Mitigation INNER JOIN ")
            sql.Append(" 	  MitigationByRisk ON Mitigation.Id = MitigationByRisk.IdMitigation INNER JOIN ")
            sql.Append(" 	 Risk ON MitigationByRisk.IdRisk = Risk.Id INNER JOIN ")
            sql.Append(" 	ComponentByRisk ON ComponentByRisk.IdRisk=Risk.Id INNER JOIN ")
            sql.Append(" 	Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	Objective ON Component.IdObjective =Objective.idkey and Objective.IsLastVersion='1' INNER JOIN ")
            sql.Append(" 	Project ON Objective.IdProject = Project.idKey ")
            If (idProjectPhase.Length = 0) Then
                sql.Append(" and Project.IsLastVersion='1' ")
            End If
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Mitigation.IdResponsible = ApplicationUser.ID ")
            'Se verifica si llega un filtro por la fase
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                sql.Append(" WHERE Project.Id IN (" & idProjectList & ")")
            Else
                sql.Append(" WHERE Project.idkey = " & idProject & " and Project.IsLastVersion='1' ")
            End If

            'sql.Append(" AND Mitigation.isLastVersion = 1 ")

            If year <> 0 Then
                sql.Append(" AND YEAR(Project.BeginDate) = " & year & " ")
            End If

            sql.Append(" ORDER BY Mitigation.Name  ")

            ' ejecutar la intruccion y retornar el objeto
            RiskMatrix.Tables.Add(GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString).Copy)
            RiskMatrix.Tables(2).TableName = "MitigationByRisk"

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "RiskMatrix")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar RiskMatrix ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta Las actividades de un proyecto
    ''' </summary>
    ''' <param name="idComponent"></param>
    ''' <remarks></remarks>
    Public Function loadReportProjectChronogram(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idComponent As Integer, _
    ByVal idProjectPhase As String, _
    ByVal idProject As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        Dim where As String = "WHERE"
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            sql.Append(" SELECT distinct Project.Name as PName, Project.idkey as IdProject, Project.Code as ProjectCode, Component.idkey AS IdComponent, Component.Name AS ComponentName, Component.Code AS ComponentCode, Activity.Title AS ActivityName,   ")
            sql.Append(" 	   Activity.Id AS IdActivity, SubActivity.Name AS SubActivityName, SubActivity.idKey AS IdSubActivity, convert(varchar(10), SubActivity.BeginDate, 103) as BeginDate,   ")
            sql.Append(" 	    convert(varchar(10), SubActivity.EndDate, 103) as EndDate, SubActivity.Duration, SubActivity.TotalCost, SubActivity.FSCContribution, SubActivity.OFContribution, 	0 as tarPorcentaje, SubActivity.reponsible AS Responsible,Project.Name + '_'+ convert(varchar,Project.id) as ProjectName  ")
            'sql.Append(" 	    convert(varchar(10), SubActivity.EndDate, 103) as EndDate, SubActivity.Duration, SubActivity.TotalCost, SubActivity.FSCContribution, SubActivity.OFContribution, 	0 as tarPorcentaje, ApplicationUser.ID AS Responsible  ")
            sql.Append(" FROM Activity INNER JOIN  Objective INNER JOIN ")
            sql.Append(" 	Component ON  Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN ")
            sql.Append("  Project ON Objective.IdProject = Project.idKey ")
            If (idProjectPhase.Length = 0) Then
                sql.Append(" and Project.IsLastVersion='1'")
            End If
            sql.Append(" ON Activity.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN ")
            sql.Append("  SubActivity ON  SubActivity.IdActivity=Activity.idkey and Activity.IsLastVersion='1' and SubActivity.isLastVersion='1' ")
            'TODO: 51 MODIFICACION DE QUERY PARA EL NUEVO RESPONSABLE
            'AUTOR GERMAN RODRIGUEZ
            ' sql.Append(" INNER JOIN   " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivity.IdResponsible = ApplicationUser.ID ")

            'Se verifica si llega un filtro por la fase
            'If (idProjectPhase.Length > 0) Then
            '    'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
            '    idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, "", idProjectPhase)
            '    sql.Append(where & " Project.Id IN (" & idProjectList & ")")
            '    where = " AND "
            'End If

            'If idComponent <> 0 Then
            '    sql.Append(where & " Component.idkey = " & idComponent & " and Component.IsLastVersion='1' ")
            '    where = " AND "
            'End If

            If idProject <> 0 Then
                sql.Append(where & " Project.idkey = " & idProject & " and Project.IsLastVersion='1' ")
                where = " AND "
            End If
            sql.AppendLine(" ORDER BY ComponentName, ActivityName, SubActivityName, BeginDate ")

            ' ejecutar la intruccion y retornar el objeto
            loadReportProjectChronogram = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportProjectChronogram ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta las actividades ejecutadas
    ''' </summary>
    ''' <param name="idSubActivity"></param>
    ''' <remarks></remarks>
    Public Function loadReportSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idSubActivity As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = "WHERE"
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            sql.Append(" SELECT  ApplicationUser_1.Name AS Responsible,   convert(varchar(10),  SubActivityInformationRegistry.EndDate, 103) as EndDate,")
            sql.Append(" 	     Convert(nvarchar(10),SubActivityInformationRegistry.Id) + Convert(nvarchar(10),SubActivityInformationRegistry.IdSubActivity) as IdInformationRegistry ,  convert(varchar(10),  SubActivityInformationRegistry.BeginDate, 103) as BeginDate   ")
            sql.Append(" FROM   SubActivityInformationRegistry INNER JOIN ")
            sql.Append(" 	   " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 ON SubActivityInformationRegistry.IdUser = ApplicationUser_1.ID ")



            If Not idSubActivity.Equals("") Then
                sql.Append(where & " SubActivityInformationRegistry.IdSubActivity = '" & idSubActivity & "'")
                where = "AND"
            End If

            ' ejecutar la intruccion y retornar el objeto
            loadReportSubActivityInformationRegistry = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar loadReportSubActivityInformationRegistry")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite poblar la informacion para la consulta general de proyectos
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="projectName"></param>
    ''' <param name="idDepto"></param>
    ''' <param name="idCity"></param>
    ''' <param name="targetPopulationName"></param>
    ''' <param name="idOperator"></param>
    ''' <param name="startTotalValue"></param>
    ''' <param name="endTotalValue"></param>
    ''' <param name="startContributionValue"></param>
    ''' <param name="endContributionValue"></param>
    ''' <param name="effectivebudget"></param>
    ''' <param name="state"></param>
    ''' <param name="startClosingDate"></param>
    ''' <param name="endClosingDate"></param>
    ''' <returns>retorna una tabla con los resultados encontrados</returns>
    ''' <remarks></remarks>
    Public Function loadConsultGeneralProjects(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal projectName As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "", _
        Optional ByVal targetPopulationName As String = "", _
        Optional ByVal idOperator As String = "", _
        Optional ByVal startTotalValue As String = "", _
        Optional ByVal endTotalValue As String = "", _
        Optional ByVal startContributionValue As String = "", _
        Optional ByVal endContributionValue As String = "", _
        Optional ByVal effectivebudget As String = "", _
        Optional ByVal state As String = "", _
        Optional ByVal startClosingDate As String = "", _
        Optional ByVal endClosingDate As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            ' construir la sentencia
            sql.AppendLine("SELECT Project.*, CloseRegistry.ClosingDate, CProgam.IdStrategicLine,")
            sql.AppendLine(" (SELECT StrategicLine.Name FROM StrategicLine WHERE StrategicLine.Id = CProgam.IdStrategicLine) as StrategicLineName, COperator.IdOperator, tabla1.IdDepto,")
            sql.AppendLine(" (SELECT Depto.Name FROM " & dbSecurityName & ".dbo.Depto Depto WHERE Depto.Id = tabla1.IdDepto) as DeptoName, tabla1.IdCity,")
            sql.AppendLine(" (SELECT City.Name FROM " & dbSecurityName & ".dbo.City City WHERE City.Id = tabla1.IdCity) as CityName")
            sql.AppendLine(" FROM Project INNER JOIN")
            sql.AppendLine(" (Select Project.Id, ")
            sql.AppendLine(" (SELECT TOP 1 City.IdDepto FROM " & dbSecurityName & ".dbo.City City")
            sql.AppendLine(" INNER JOIN  ProjectLocation ON City.Id = ProjectLocation.IdCity")
            sql.AppendLine(" WHERE ProjectLocation.IdProject = Project.idKey")
            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then sql.Append(" AND City.IdDepto = " & idDepto)
            sql.AppendLine(" ) as IdDepto,")
            sql.AppendLine(" (SELECT TOP 1 ProjectLocation.IdCity FROM ProjectLocation")
            sql.AppendLine(" INNER JOIN " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.Id")
            sql.AppendLine(" WHERE ProjectLocation.IdProject = Project.idKey")
            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then sql.Append(" AND ProjectLocation.IdCity = " & idCity)
            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then sql.Append(" AND City.IdDepto = " & idDepto)
            sql.AppendLine(" ) as IdCity")
            sql.AppendLine(" FROM Project) AS tabla1 ")
            sql.AppendLine(" ON tabla1.Id= Project.idKey and Project.IsLastVersion='1'")
            sql.AppendLine(" LEFT OUTER JOIN")
            sql.AppendLine(" (SELECT Project.Id,")
            sql.AppendLine(" (SELECT TOP 1 StrategicLine.Id FROM StrategicLine")
            sql.AppendLine(" INNER JOIN Program ON StrategicLine.Id = Program.IdStrategicLine")
            sql.AppendLine(" INNER JOIN ProgramComponent ON Program.Id = ProgramComponent.IdProgram")
            sql.AppendLine(" INNER JOIN ProgramComponentByProject ON ProgramComponent.Id = ProgramComponentByProject.IdProgramComponent")
            sql.AppendLine(" WHERE ProgramComponentByProject.IdProject = Project.idKey")
            'Verificar si hay entrada de datos para el campo
            If idStrategicLine.Length > 0 Then sql.Append(" AND StrategicLine.Id = " & idStrategicLine)
            sql.AppendLine(" ) AS IdStrategicLine  FROM Project) AS CProgam")
            sql.AppendLine(" ON CProgam.Id = Project.idKey and Project.IsLastVersion='1'")
            sql.AppendLine(" LEFT OUTER JOIN")
            sql.AppendLine(" (SELECT Project.Id,")
            sql.AppendLine(" (SELECT TOP 1 OperatorByProject.IdOperator FROM OperatorByProject")
            sql.AppendLine(" WHERE OperatorByProject.IdProject = Project.idKey")
            'Verificar si hay entrada de datos para el campo
            If idOperator.Length > 0 Then sql.Append(" AND OperatorByProject.IdOperator = " & idOperator)
            sql.AppendLine(") AS IdOperator FROM Project) AS COperator")
            sql.AppendLine(" ON  COperator.Id = Project.idKey and Project.IsLastVersion='1'")
            sql.Append(" LEFT OUTER JOIN CloseRegistry ON  CloseRegistry.IdProject=Project.idKey and Project.IsLastVersion='1'")

            'Verificar si hay entrada de datos para el campo
            If idStrategicLine.Length > 0 Then

                sql.Append(where & " CProgam.IdStrategicLine = '" & idStrategicLine & "'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If projectName.Length > 0 Then

                sql.Append(where & " Project.Name Like '%" & projectName & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then

                sql.Append(where & " tabla1.IdDepto = " & idDepto)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then

                sql.Append(where & " tabla1.IdCity = " & idCity)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If targetPopulationName.Length > 0 Then

                sql.Append(where & " Project.Population Like '%" & targetPopulationName & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startTotalValue.Length > 0 AndAlso endTotalValue.Length > 0 Then

                sql.AppendLine(where & " Project.TotalCost BETWEEN " & startTotalValue.Replace(",", ".") & " AND " & endTotalValue.Replace(",", "."))
                where = " AND "

            ElseIf startTotalValue.Length > 0 OrElse endTotalValue.Length > 0 Then

                If startTotalValue.Length > 0 Then
                    sql.Append(where & " Project.TotalCost >= " & startTotalValue.Replace(",", "."))
                Else
                    sql.Append(where & " Project.TotalCost <= " & endTotalValue.Replace(",", "."))
                End If

                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startContributionValue.Length > 0 AndAlso endContributionValue.Length > 0 Then

                sql.AppendLine(where & " Project.FSCContribution BETWEEN " & startContributionValue.Replace(",", ".") & " AND " & endContributionValue.Replace(",", "."))
                where = " AND "

            ElseIf startContributionValue.Length > 0 OrElse endContributionValue.Length > 0 Then

                If startContributionValue.Length > 0 Then
                    sql.Append(where & " Project.FSCContribution >= " & startContributionValue.Replace(",", "."))
                Else
                    sql.Append(where & " Project.FSCContribution <= " & endContributionValue.Replace(",", "."))
                End If

                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If effectivebudget.Length > 0 Then

                sql.Append(where & " Project.EffectiveBudget = " & effectivebudget)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idOperator.Length > 0 Then

                sql.Append(where & " COperator.IdOperator = " & idOperator)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startClosingDate.Length > 0 OrElse endClosingDate.Length > 0 Then

                If startClosingDate.Length > 0 AndAlso endClosingDate.Length > 0 Then
                    sql.AppendLine(where & " CloseRegistry.ClosingDate BETWEEN '" & CDate(startClosingDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endClosingDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startClosingDate.Length > 0 Then
                    sql.Append(where & " CloseRegistry.ClosingDate >= '" & CDate(startClosingDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CloseRegistry.ClosingDate <= '" & CDate(endClosingDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            'Se agrega el ordenamiento para la consulta
            sql.AppendLine(" ORDER BY Project.Name ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta general de proyectos.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información del plan de contratación
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Function loadReportRecruitmentPlan(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As String, _
    ByVal idProjectPhase As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        Dim where As String = " WHERE "

        Try
            sql.Append(" SELECT * FROM vReportRecruitmentPlan")

            'Se verifica si llega algun filtro por la fase del proyecto
            If (idProjectPhase.Length > 0) Then
                'Se llama al metodo que permite consultar las ultimas versiones de un proyecto en una fase determinada
                idProjectList = Me.loadConsultIdProjectsListByPhase(objApplicationCredentials, idProject, idProjectPhase)
                sql.Append(where & " IdProject IN (" & idProjectList & ")")
            Else
                If idProject.Length > 0 Then
                    sql.Append(where & " IdProject = " & idProject & " ")
                End If
            End If

            sql.AppendLine(" ORDER BY ProjectName ")

            ' ejecutar la intruccion y retornar el objeto
            loadReportRecruitmentPlan = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte del plan de contratación.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información de las solicitudes de contrato que cumplen con los filtros requeridos
    ''' </summary>
    ''' <param name="idManagement"></param>
    ''' <param name="idProject"></param>
    ''' <param name="dateStartRequest"></param>
    ''' <param name="dateEndRequest"></param>
    ''' <param name="contractorName"></param>
    ''' <remarks></remarks>
    Public Function loadConsultGeneralContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idManagement As String = "", _
    Optional ByVal idProject As String = "", _
    Optional ByVal dateStartRequest As String = "", _
    Optional ByVal dateEndRequest As String = "", _
    Optional ByVal contractorName As String = "" _
    ) As String

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim idContractRequestList As String = ""

        Try
            sql.Append(" SELECT RequestNumber FROM vReportContractsRequest")
            sql.Append(" LEFT OUTER JOIN vReportContractorByContractRequest")
            sql.Append(" ON vReportContractorByContractRequest.IdContractRequest = vReportContractsRequest.RequestNumber ")

            If idManagement.Length > 0 Then
                sql.Append(where & " IdManagement = " & idManagement)
                where = " AND "
            End If

            If idProject.Length > 0 Then
                sql.Append(where & " IdProject = " & idProject)
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If dateStartRequest.Length > 0 OrElse dateEndRequest.Length > 0 Then

                If dateStartRequest.Length > 0 AndAlso dateEndRequest.Length > 0 Then
                    sql.AppendLine(where & " CreateDate BETWEEN '" & CDate(dateStartRequest).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(dateEndRequest).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf dateStartRequest.Length > 0 Then
                    sql.Append(where & " CreateDate >= '" & CDate(dateStartRequest).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CreateDate <= '" & CDate(dateEndRequest).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            If contractorName.Length > 0 Then
                sql.Append(where & " ContractorName like '%" & contractorName & "%'")
                where = " AND "
            End If

            sql.Append("GROUP BY RequestNumber")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                idContractRequestList &= row("RequestNumber") & ","

            Next

            'Se refina la lista de solicitudes de contrato
            If (idContractRequestList.Length > 0) Then idContractRequestList = idContractRequestList.Substring(0, idContractRequestList.Length - 1)

            ' retornar el objeto
            Return idContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte de las solicitudes de contrato.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información de las solicitudes del contrato
    ''' </summary>
    ''' <param name="idContractRequestList"></param>
    ''' <param name="idManagement"></param>
    ''' <param name="idProject"></param>
    ''' <param name="dateStartRequest"></param>
    ''' <param name="dateEndRequest"></param>
    ''' <remarks></remarks>
    Public Function loadReportContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequestList As String = "", _
    Optional ByVal idManagement As String = "", _
    Optional ByVal idProject As String = "", _
    Optional ByVal dateStartRequest As String = "", _
    Optional ByVal dateEndRequest As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            sql.Append(" SELECT * FROM vReportContractsRequest")

            If idContractRequestList.Length > 0 Then
                sql.Append(where & " RequestNumber IN (" & idContractRequestList & ")")
                where = " AND "
            End If

            If idManagement.Length > 0 Then
                sql.Append(where & " IdManagement = " & idManagement)
                where = " AND "
            End If

            If idProject.Length > 0 Then
                sql.Append(where & " IdProject = " & idProject)
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If dateStartRequest.Length > 0 OrElse dateEndRequest.Length > 0 Then

                If dateStartRequest.Length > 0 AndAlso dateEndRequest.Length > 0 Then
                    sql.AppendLine(where & " CreateDate BETWEEN '" & CDate(dateStartRequest).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(dateEndRequest).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf dateStartRequest.Length > 0 Then
                    sql.Append(where & " CreateDate >= '" & CDate(dateStartRequest).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CreateDate <= '" & CDate(dateEndRequest).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            sql.AppendLine(" ORDER BY ProjectName ")

            ' ejecutar la intruccion y retornar el objeto
            loadReportContractRequest = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte de las solicitudes de contrato.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información de los contratistas por cada solicitud de contrato hecha
    ''' </summary>
    ''' <param name="idContractRequestList"></param>
    ''' <remarks></remarks>
    Public Function loadReportContractorsNameByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequestList As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            sql.Append("SELECT * FROM vReportContractorByContractRequest")

            If idContractRequestList.Length > 0 Then
                sql.Append(where & " IdContractRequest IN (" & idContractRequestList & ")")
                where = " AND "
            End If

            sql.AppendLine(" ORDER BY contractorName ")

            'Ejecutar la intruccion y retornar el objeto
            loadReportContractorsNameByContractRequest = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte de las solicitudes de contrato.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información del listado general de contratos
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idContractRequest"></param>
    ''' <param name="contractNumber"></param>
    ''' <param name="idManagement"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idProject"></param>
    ''' <param name="contractState"></param>
    ''' <param name="effectiveBudget"></param>
    ''' <param name="contractorName"></param>
    ''' <param name="supervisor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportGeneralListContracts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequest As String = "", _
    Optional ByVal contractNumber As String = "", _
    Optional ByVal idManagement As String = "", _
    Optional ByVal idStrategicLine As String = "", _
    Optional ByVal idProject As String = "", _
    Optional ByVal contractState As String = "", _
    Optional ByVal effectiveBudget As String = "", _
    Optional ByVal contractorName As String = "", _
    Optional ByVal supervisor As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            'Se construye la instrucción de consulta
            sql.Append("SELECT vReportGeneralListContracts.*, Contractors.ContractorName FROM vReportGeneralListContracts")
            sql.Append(" INNER JOIN")
            sql.Append(" (SELECT vReportGeneralListContracts.RequestNumber, ")
            sql.Append(" (SELECT TOP 1 vReportContractorByGeneralListContracts.ContractorName  FROM  vReportContractorByGeneralListContracts")
            sql.Append(" WHERE vReportContractorByGeneralListContracts.IdContractRequest = vReportGeneralListContracts.RequestNumber")
            If contractorName <> Nothing AndAlso contractorName.Length > 0 Then
                sql.Append(" AND vReportContractorByGeneralListContracts.ContractorName LIKE '%" & contractorName & "%'")
            End If
            sql.Append(") AS ContractorName FROM vReportGeneralListContracts) AS Contractors")
            sql.Append(" ON  vReportGeneralListContracts.RequestNumber = Contractors.RequestNumber")

            If idContractRequest <> Nothing AndAlso idContractRequest.Length > 0 Then
                sql.Append(where & " IdContractRequest = '" & idContractRequest & "'")
                where = " AND "
            End If

            If contractNumber <> Nothing AndAlso contractNumber.Length > 0 Then
                sql.Append(where & " ContractNumber = '" & contractNumber & "'")
                where = " AND "
            End If

            If idManagement <> Nothing AndAlso idManagement.Length > 0 Then
                sql.Append(where & " IdManagement = '" & idManagement & "'")
                where = " AND "
            End If

            If idStrategicLine <> Nothing AndAlso idStrategicLine.Length > 0 Then
                sql.Append(where & " IdStrategicLine = '" & idStrategicLine & "'")
                where = " AND "
            End If

            If idProject <> Nothing AndAlso idProject.Length > 0 Then
                sql.Append(where & " IdProject = '" & idProject & "'")
                where = " AND "
            End If

            If contractState <> Nothing AndAlso contractState.Length > 0 Then
                sql.Append(where & " ContractState LIKE '%" & contractState & "%'")
                where = " AND "
            End If

            If effectiveBudget <> Nothing AndAlso effectiveBudget.Length > 0 Then
                sql.Append(where & " EffectiveBudget LIKE '%" & effectiveBudget & "%'")
                where = " AND "
            End If

            If contractorName <> Nothing AndAlso contractorName.Length > 0 Then
                sql.Append(where & " Contractors.ContractorName LIKE '%" & contractorName & "%'")
                where = " AND "
            End If

            If supervisor <> Nothing AndAlso supervisor.Length > 0 Then
                sql.Append(where & " Supervisor LIKE '%" & supervisor & "%'")
                where = " AND "
            End If

            'Se asigna el ordenamiento
            sql.AppendLine(" ORDER BY ProjectName ")

            'Ejecutar la intruccion y retornar el objeto
            loadReportGeneralListContracts = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte general de contratos.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información de los contratistas por cada contrato
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadReportContractorsByGeneralListContracts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequest As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            sql.Append("SELECT * FROM vReportContractorByGeneralListContracts")

            If idContractRequest.Length > 0 Then
                sql.Append(where & " IdContractRequest = '" & idContractRequest & "'")
                where = " AND "
            End If

            sql.AppendLine(" ORDER BY ContractorName ")

            'Ejecutar la intruccion y retornar el objeto
            loadReportContractorsByGeneralListContracts = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los contratistas para la ficha técnica del contrato.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información de la lista de pagos realizada por cada contrato.
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadReportPaymentsListByGeneralListContracts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequest As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            sql.Append("SELECT * FROM vReportPaymentsListByGeneralListContracts")

            If idContractRequest.Length > 0 Then
                sql.Append(where & " IdContractRequest = '" & idContractRequest & "'")
                where = " AND "
            End If

            sql.AppendLine(" ORDER BY Date ")

            'Ejecutar la intruccion y retornar el objeto
            loadReportPaymentsListByGeneralListContracts = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de pagos para la ficha técnica del contrato.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta información del listado de proyectos cerrados
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportCLosedProjectList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idStrategicLine As String = "", _
    Optional ByVal idProject As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "

        Try
            'Se construye la instrucción de consulta
            sql.Append("SELECT StrategicLine.Id AS IdStrategicLine, StrategicLine.Name AS StrategicLineName, Project.Idkey AS IdKeyProject, Project.Name AS ProjectName,")
            sql.Append(" Project.BeginDate, CloseRegistry.ClosingDate, Project.TotalCost, CloseRegistry.GoodPractice ")
            sql.Append(" FROM Project INNER JOIN CloseRegistry ON CloseRegistry.IdProject=Project.IdKey AND Project.IslastVersion='1'")
            sql.Append(" LEFT OUTER JOIN  ProgramComponentByProject ON Project.IdKey = ProgramComponentByProject.IdProject AND Project.IslastVersion='1'")
            sql.Append(" LEFT OUTER JOIN  ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id")
            sql.Append(" LEFT OUTER JOIN  Program ON ProgramComponent.IdProgram = Program.Id")
            sql.Append(" LEFT OUTER JOIN  StrategicLine ON Program.IdStrategicLine = StrategicLine.Id")

            'Se verifica si viene algun valor 
            If idStrategicLine <> Nothing AndAlso idStrategicLine.Length > 0 Then
                sql.Append(where & " StrategicLine.Id = '" & idStrategicLine & "'")
                where = " AND "
            End If

            If idProject <> Nothing AndAlso idProject.Length > 0 Then
                sql.Append(where & " Project.IdKey = '" & idProject & "' AND Project.IslastVersion='1'")
                where = " AND "
            End If

            'Se asigna el agrupamiento
            sql.Append(" GROUP BY StrategicLine.Id, StrategicLine.Name, Project.Idkey, Project.Name, Project.BeginDate, CloseRegistry.ClosingDate,")
            sql.Append(" Project.TotalCost, CloseRegistry.GoodPractice")

            'Se asigna el ordenamiento
            sql.AppendLine(" ORDER BY ProjectName ")

            'Ejecutar la intruccion y retornar el objeto
            loadReportCLosedProjectList = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta listado de proyectos cerrados.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta de reporte de registro de proyectos cerrados
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="IdProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal IdProject As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.AppendLine("SELECT Project.IdKey, Project.Name, Project.Objective, CloseRegistry.ClosingDate, CloseRegistry.Weakness,")
            sql.AppendLine(" CloseRegistry.Opportunity, CloseRegistry.Strengths, CloseRegistry.LearningForNewProjects, CloseRegistry.GoodPractice ")
            sql.AppendLine(" FROM CloseRegistry INNER JOIN ")
            sql.AppendLine(" Project ON CloseRegistry.IdProject = Project.idkey and Project.IsLastVersion='1' ")

            'Verificar si hay entrada de datos para el campo
            If IdProject.Length > 0 Then

                sql.Append(where & " Project.IsLastVersion='1' AND Project.idKey = '" & IdProject & "'")
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte registro de proyectos cerrados.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta de reporte de operadores por proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="IdProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadOperatorsByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal IdProject As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.AppendLine("SELECT OperatorByProject.IdProject, OperatorByProject.IdOperator, Third.Name")
            sql.AppendLine(" FROM OperatorByProject")
            sql.AppendLine(" INNER JOIN Third ON OperatorByProject.IdOperator = Third.Id")

            'Verificar si hay entrada de datos para el campo
            If IdProject.Length > 0 Then

                sql.Append(where & " OperatorByProject.IdProject = '" & IdProject & "'")
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de operadores por proyecto.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta la información requerida para el reporte lista de actividades
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idComponent"></param>
    ''' <param name="idResponsible"></param>
    ''' <param name="idState"></param>
    ''' <param name="endDateIni"></param>
    ''' <param name="endDateEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportListActivities(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As String, _
    ByVal idComponent As String, _
    ByVal idResponsible As String, _
    ByVal idState As String, _
    ByVal endDateIni As String, _
    ByVal endDateEnd As String _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim idProjectList As String = ""
        Dim where As String = " WHERE "

        Try
            sql.Append(" SELECT * FROM vReportActivities")

            'Se verifica si viene un valor para el campo
            If idProject.Length > 0 Then
                sql.Append(where & " idKey = '" & idProject & "'")
                where = " AND "
            End If

            'Se verifica si viene un valor para el campo
            If idComponent.Length > 0 Then
                sql.Append(where & " IdComponent = '" & idComponent & "'")
                where = " AND "
            End If

            'Se verifica si viene un valor para el campo
            If idResponsible.Length > 0 Then
                sql.Append(where & " IdResponsible = '" & idResponsible & "'")
                where = " AND "
            End If

            'Se verifica si viene un valor para el campo
            If idResponsible.Length > 0 Then
                sql.Append(where & " IdResponsible = '" & idResponsible & "'")
                where = " AND "
            End If

            'Se verifica si viene un valor para el campo
            If idState.Length > 0 Then
                sql.Append(where & " NameState = '" & idState & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If endDateIni.Length > 0 OrElse endDateEnd.Length > 0 Then

                If endDateIni.Length > 0 AndAlso endDateEnd.Length > 0 Then
                    sql.AppendLine(where & " ScheduledEndDate BETWEEN '" & CDate(endDateIni).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endDateEnd).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf endDateIni.Length > 0 Then
                    sql.Append(where & " ScheduledEndDate >= '" & CDate(endDateIni).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " ScheduledEndDate <= '" & CDate(endDateEnd).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If
            sql.Append(" ORDER BY NameProject, NameComponent, EndDate ASC, ScheduledEndDate ASC ")

            ' ejecutar la intruccion y retornar el objeto
            loadReportListActivities = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte lista de actividades.")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function


#Region "Research and Development"

    ''' <summary>
    ''' Carga la consulta del reporte de Inventario de ideas
    ''' </summary>
    ''' <param name="startDateRecord">Fecha de registro inicial</param>
    ''' <param name="endDateRecord">Fecha de registro final</param>
    ''' <param name="startDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha fin</param>
    ''' <param name="idDepto">Identificador del departamento</param>
    ''' <param name="idCity">Identificadores de las ciudades</param>
    ''' <param name="idStrategicLine">Identificador de la Linea Estrategica</param>
    ''' <param name="idsProgramComponents">Identificadortes de las Componentes del Programa</param>
    ''' <param name="source">fuente</param>
    ''' <param name="startCost">Costo inicial</param>
    ''' <param name="endCost">Costo final</param>
    ''' <param name="state">Estado</param>
    ''' <remarks></remarks>
    Public Function loadReportIdeaInventory(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idIdea As String = "", _
        Optional ByVal startDateRecord As String = "", _
        Optional ByVal endDateRecord As String = "", _
        Optional ByVal ideaName As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "", _
        Optional ByVal startCost As String = "", _
        Optional ByVal endCost As String = "", _
        Optional ByVal state As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.AppendLine("SELECT vReportIdeaInventory.*, tabla1.IdDepto,")
            sql.AppendLine(" (SELECT Depto.Name FROM " & dbSecurityName & ".dbo.Depto Depto WHERE Depto.Id = tabla1.IdDepto) as DptoName, tabla1.IdCity,")
            sql.AppendLine(" (SELECT City.Name FROM " & dbSecurityName & ".dbo.City City WHERE City.Id = tabla1.IdCity) as CityName")
            sql.AppendLine(" FROM vReportIdeaInventory")
            sql.AppendLine(" INNER JOIN")
            sql.AppendLine(" (Select Idea.Id, ")
            sql.AppendLine(" (SELECT TOP 1 LocationByIdea.IdDepto FROM LocationByIdea WHERE LocationByIdea.IdIdea = Idea.Id")
            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then sql.Append(" AND LocationByIdea.IdDepto = " & idDepto)
            sql.AppendLine(" ) as IdDepto,")
            sql.AppendLine(" (SELECT TOP 1 LocationByIdea.IdCity FROM LocationByIdea WHERE LocationByIdea.IdIdea = Idea.Id")
            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then sql.Append(" AND LocationByIdea.IdCity = " & idCity)
            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then sql.Append(" AND LocationByIdea.IdDepto = " & idDepto)
            sql.AppendLine(" ) as IdCity")
            sql.AppendLine(" FROM Idea) AS tabla1")
            sql.AppendLine(" ON vReportIdeaInventory.IdIdea = tabla1.Id")

            'Verificar si hay entrada de datos para el campo
            If idIdea.Length > 0 Then

                sql.Append(where & " IdIdea = '" & idIdea & "'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startDateRecord.Length > 0 OrElse endDateRecord.Length > 0 Then

                If startDateRecord.Length > 0 AndAlso endDateRecord.Length > 0 Then
                    sql.AppendLine(where & " CreateDate BETWEEN '" & CDate(startDateRecord).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endDateRecord).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startDateRecord.Length > 0 Then
                    sql.Append(where & " CreateDate >= '" & CDate(startDateRecord).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CreateDate <= '" & CDate(endDateRecord).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If ideaName.Length > 0 Then

                sql.Append(where & " Name like '%" & ideaName & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idStrategicLine.Length > 0 Then

                sql.Append(where & " IdStrategicLine = " & idStrategicLine)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If source.Length > 0 Then

                sql.Append(where & " Source = '" & source & "'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then

                sql.Append(where & " tabla1.IdDepto = " & idDepto)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then

                sql.Append(where & " tabla1.IdCity = " & idCity)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startCost.Length > 0 AndAlso endCost.Length > 0 Then

                sql.AppendLine(where & " Cost BETWEEN " & startCost.Replace(",", ".") & " AND " & endCost.Replace(",", "."))
                where = " AND "

            ElseIf startCost.Length > 0 OrElse endCost.Length > 0 Then

                If startCost.Length > 0 Then
                    sql.Append(where & " Cost >= " & startCost.Replace(",", "."))
                Else
                    sql.Append(where & " Cost <= " & endCost.Replace(",", "."))
                End If

                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If state.Length > 0 Then

                'Se realiza la compración del campo
                sql.Append(where & " State = '" & state & "'")
                where = " AND "

            End If

            'Se agrega el ordenamiento para la consulta
            sql.AppendLine(" ORDER BY Name ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de inventario de ideas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de ubicaciones por idea
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del ususuario</param>   
    ''' <param name="idDepto">Identificador del departamento</param>
    ''' <param name="idCity">Identificadores de las ciudades</param>
    ''' <remarks></remarks>
    Public Function loadReportLocationsByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idIdea As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportLocationsByIdea")

            'Verificar si hay entrada de datos para el campo
            If idIdea.Length > 0 Then

                sql.Append(where & " IdIdea = " & idIdea)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then

                sql.Append(where & " IdDepto = " & idDepto)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then

                sql.Append(where & " IdCity = " & idCity)
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de inventario de ideas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de los documentos adjuntos por entidad
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del ususuario</param>   
    ''' <param name="idEntity">Identificador de la entidad</param>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <remarks></remarks>
    Public Function loadReportAttachmentsByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idEntity As String = "", _
        Optional ByVal entityName As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportAttachmentsByIdea")

            'Verificar si hay entrada de datos para el campo
            If idEntity.Length > 0 Then

                sql.Append(where & " IdIdea = " & idEntity)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If entityName.Length > 0 Then

                sql.Append(where & " EntityName = '" & entityName & "'")
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de inventario de ideas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de los terceros por idea
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del ususuario</param>   
    ''' <param name="idIdea">Identificador de la Idea</param>
    ''' <remarks></remarks>
    Public Function loadReportThirdsByIdea(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idIdea As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportThirdByIdea")

            'Verificar si hay entrada de datos para el campo
            If idIdea.Length > 0 Then

                sql.Append(where & " IdIdea = " & idIdea)
                where = " AND "

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de inventario de ideas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de mapa de actores
    ''' </summary>
    ''' <param name="IdTird">Identificador del tercero</param>
    ''' <param name="StartCreateDate">Fecha de creación inicial</param> 
    ''' <param name="EndCreateDate">Fecha de creación final</param> 
    ''' <param name="Type">Tipo</param> 
    ''' <remarks></remarks>
    Public Function loadReportActorsMap(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal IdTird As String, _
        ByVal StartCreateDate As String, _
        ByVal EndCreateDate As String, _
        ByVal Type As String _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportActorsMap")

            'Verificar si hay entrada de datos para el campo
            If IdTird.Length > 0 Then

                sql.Append(where & " IdThird = " & IdTird)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If StartCreateDate.Length > 0 OrElse EndCreateDate.Length > 0 Then

                If StartCreateDate.Length > 0 AndAlso EndCreateDate.Length > 0 Then
                    sql.AppendLine(where & " CreateDate BETWEEN '" & CDate(StartCreateDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndCreateDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartCreateDate.Length > 0 Then
                    sql.Append(where & " CreateDate >= '" & CDate(StartCreateDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CreateDate <= '" & CDate(EndCreateDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If



            'Verificar si hay entrada de datos para el campo
            If Type.Length > 0 Then

                sql.Append(where & " Type = '" & Type & "'")
                where = " AND "

            End If

            'Se agrega el ordenamiento para la consulta
            sql.AppendLine(" ORDER BY Id ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de mapa de actores.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

#End Region

End Class
