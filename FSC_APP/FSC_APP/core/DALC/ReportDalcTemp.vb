Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ReportDalcTemp
    Const MODULENAME As String = "ReportDALC"

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
            sql.Append(" SELECT i.Description, i.Goal, i.GreenValue, i.YellowValue, i.RedValue, i.Id as IdIndicator")
            sql.Append(" FROM Indicator i")
            sql.Append(" WHERE i.LevelIndicator = '" & IndicatorType & "' AND i.IdEntities = " & IdStrategicLine & "")

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
            Throw New Exception("Error al cargar los indicadores de un Linea Estrategica o estrategia ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la consulta del reporte de Inventario de ideas
    ''' </summary>
    ''' <param name="startDateRecord">Fecha de registro inicial</param>
    ''' <param name="endDateRecord">Fecha de registro final</param>
    ''' <param name="startDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha fin</param>
    ''' <param name="idDepto">Identificador del departamento</param>
    ''' <param name="idsCities">Identificadores de las ciudades</param>
    ''' <param name="idStrategicLine">Identificador de la Linea Estrategica</param>
    ''' <param name="idsProgramComponents">Identificadortes de las Componentes del Programa</param>
    ''' <param name="source">fuente</param>
    ''' <param name="startCost">Costo inicial</param>
    ''' <param name="endCost">Costo final</param>
    ''' <param name="state">Estado</param>
    ''' <remarks></remarks>
    Public Function loadReportIdeaInventory(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal startDateRecord As String, _
        ByVal endDateRecord As String, _
        ByVal startDate As String, _
        ByVal endDate As String, _
        ByVal idDepto As String, _
        ByVal idsCities As String, _
        ByVal idStrategicLine As String, _
        ByVal idsProgramComponents As String, _
        ByVal source As String, _
        ByVal startCost As String, _
        ByVal endCost As String, _
        ByVal state As String _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim Table As String

        Try

            '' construir la sentencia
            'sql.AppendLine(" SELECT Entity.Name, Description, Goal, GreenValue, YellowValue, RedValue, measurementDate,   ")

            'sql.AppendLine(" WHERE LevelIndicator = '" & IndicatorType & "'")

            '' verificar si existen fechas en el filtro
            'If Not BeginDate.Equals(String.Empty) And Not endDate.Equals(String.Empty) Then

            '    sql.AppendLine(" 	AND MeasurementDate BETWEEN '" & CDate(BeginDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")

            'End If

            '' verificar si existen usuario
            'If Not idUser.Equals(String.Empty) Then

            '    sql.AppendLine(" 	AND Indicator.idUser = " & idUser)

            'End If

            sql.AppendLine(" ORDER By MeasurementDate DESC  ")

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
    ''' Consulta la lista de actividades estrategicas por estrategia.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getStrategicActivityGantt(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                              ByVal idStrategy As String) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' contruir el sql
            sql.AppendLine(" SELECT StrategicActivity.Id AS Id, StrategicActivity.code as Code, StrategicActivity.Name AS Name, ")
            sql.AppendLine(" 		convert(varchar(10), begindate, 103) As StartDate, convert(varchar(10), enddate, 103) As EndDate, ")
            sql.AppendLine(" 		0 as tarPorcentaje, ApplicationUser.Name as Resource  ")
            sql.AppendLine(" FROM StrategicActivity ")
            sql.AppendLine(" 	INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON StrategicActivity.IdResponsible = ApplicationUser.Id ")

            If Not idStrategy.Equals(String.Empty) Then

                ' cargar los de la estrategia
                sql.AppendLine(" WHERE StrategicActivity.IdStrategy = " & idStrategy)

            End If

            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

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
    '''  Consulta los estados de una convocatoria
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idProject"></param>
    ''' <param name="Code"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getStateSummoning(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                                     Optional ByVal idStrategicLine As String = "", _
                                                     Optional ByVal idProject As String = "", _
                                                     Optional ByVal Code As String = "", _
                                                     Optional ByVal StartDate As String = "", _
                                                     Optional ByVal EndDate As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' contruir el sql
            sql.AppendLine(" SELECT DISTINCT Summoning.Id, Summoning.Code, Summoning.Name, Summoning.Description,  ")
            sql.AppendLine(" convert(varchar(10), Summoning.BeginDate, 103) As BeginDate, convert(varchar(10),  Summoning.EndDate, 103) As EndDate, ")
            sql.AppendLine(" StrategicLine.Name AS StrategicLine, Project.Name AS Project, Project.idkey as Projectidkey, Project.Code as ProjectCode, StrategicLine.Id as StrategicLineId, StrategicLine.Code as StrategicLineCode,  ")
            sql.AppendLine(" (SELECT COUNT(*) AS Expr1 FROM Proposal WHERE (IdSummoning = Summoning.Id)) AS ProposalNumber, ")
            sql.AppendLine(" (SELECT  COUNT(*) AS Expr1  FROM  Proposal AS Proposal_1  WHERE  (IdSummoning = Summoning.Id) AND (Result = 'Aprobado')) AS ApprovalProposal ")
            sql.AppendLine(" FROM ProgramComponentByProject INNER JOIN ")
            sql.AppendLine(" Summoning INNER JOIN  Project ON Summoning.IdProject = Project.idkey and Project.IsLastVersion='1' ON ProgramComponentByProject.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.AppendLine(" Program INNER JOIN StrategicLine ON Program.IdStrategicLine = StrategicLine.Id INNER JOIN ")
            sql.AppendLine(" ProgramComponent ON Program.Id = ProgramComponent.IdProgram ON  ")
            sql.AppendLine(" ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then

                sql.Append(where & " StrategicLine.id ='" & idStrategicLine & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then

                sql.Append(where & " Project.idkey ='" & idProject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not Code.Equals("") Then

                sql.Append(where & " Summoning.Code like '%" & Code & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & " Summoning.BeginDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & " Summoning.BeginDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " Summoning.BeginDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If


            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStateSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar getStateSummoning ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    '''  Consulta las propuestas hechas
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idProject"></param>
    ''' <param name="Code"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProposalList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                                     Optional ByVal idStrategicLine As String = "", _
                                                     Optional ByVal idProject As String = "", _
                                                     Optional ByVal Code As String = "", _
                                                     Optional ByVal StartDate As String = "", _
                                                     Optional ByVal EndDate As String = "", _
                                                      Optional ByVal OperatorName As String = "", _
                                                       Optional ByVal State As String = "", _
                                                        Optional ByVal idProposal As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' contruir el sql
            sql.AppendLine(" SELECT DISTINCT Summoning.Id, Summoning.Code, Summoning.Name, Summoning.Description,  ")
            sql.AppendLine(" convert(varchar(10), Summoning.BeginDate, 103) As BeginDate, convert(varchar(10),  Summoning.EndDate, 103) As EndDate, ")
            sql.AppendLine(" StrategicLine.Name AS StrategicLine, Project.Name AS Project, Project.idkey as Projectidkey, Project.Code as ProjectCode, StrategicLine.Id as StrategicLineId, StrategicLine.Code as StrategicLineCode, ")
            sql.AppendLine(" Proposal.Operator, Proposal.Result, Proposal.ProjectName, Proposal.TotalValue, Proposal.InputFSC, Proposal.InputOtherSources, ")
            sql.AppendLine(" Proposal.BriefProjectDescription, Proposal.OperatorNit, Proposal.Target, Proposal.TargetPopulation, Proposal.ExpectedResults, Proposal.Id AS IdProposal ")
            sql.AppendLine(" FROM ProgramComponentByProject INNER JOIN ")
            sql.AppendLine(" Summoning INNER JOIN  Project ON Summoning.IdProject = Project.idkey and Project.IsLastVersion='1' ON ProgramComponentByProject.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.AppendLine(" Program INNER JOIN StrategicLine ON Program.IdStrategicLine = StrategicLine.Id INNER JOIN ")
            sql.AppendLine(" ProgramComponent ON Program.Id = ProgramComponent.IdProgram ON  ")
            sql.AppendLine(" ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id INNER JOIN ")
            sql.AppendLine("  Proposal ON Summoning.Id = Proposal.IdSummoning ")
            ' verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then

                sql.Append(where & " StrategicLine.id ='" & idStrategicLine & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then

                sql.Append(where & " Project.idkey ='" & idProject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not Code.Equals("") Then

                sql.Append(where & " Summoning.Code like '%" & Code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not OperatorName.Equals("") Then

                sql.Append(where & " Proposal.Operator like '%" & OperatorName & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not State.Equals("") Then

                sql.Append(where & " Proposal.Result = '" & State & "'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & " Summoning.BeginDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & " Summoning.BeginDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " Summoning.BeginDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProposal.Equals("") Then

                sql.Append(where & " Proposal.Id ='" & idProposal & "'")
                where = " AND "

            End If


            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProposalList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar getProposalList ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Consulta las ciudades que tiene una propuesta
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProposalLocation(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                        Optional ByVal idProposal As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' contruir el sql
            sql.AppendLine(" SELECT Proposal.Id, City.Name , Depto.Name AS Depto ")
            sql.AppendLine(" FROM Proposal INNER JOIN ")
            sql.AppendLine("   LocationByProposal ON Proposal.Id = LocationByProposal.IdProposal INNER JOIN ")
            sql.AppendLine(" " & dbSecurityName & ".dbo.City City ON LocationByProposal.IdCity = City.ID INNER JOIN ")
            sql.AppendLine("  " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID ")
       

            ' verificar si hay entrada de datos para el campo
            If Not idProposal.Equals("") Then

                sql.Append(where & " Proposal.Id ='" & idProposal & "'")
                where = " AND "

            End If


            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProposalLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar getProposalLocation ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Consulta la lista de aprendizajes,  ajustes y logros
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="UserName"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLearningList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                        Optional ByVal idProject As String = "", _
                                        Optional ByVal UserName As String = "", _
                                         Optional ByVal StartDate As String = "", _
                                        Optional ByVal EndDate As String = "", _
                                         Optional ByVal State As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' contruir el sql
            sql.AppendLine(" SELECT Project.Name as Project, convert(varchar(10), Execution.CreateDate, 103) AS CreateDate , ApplicationUser.Name AS USERNAME, ")
            sql.AppendLine(" Case '" + State + "' when '1' then Execution.Learning  when '2' then Execution.Adjust ")
            sql.AppendLine("  when '3' then Execution.achievements when '4' then  Execution.QualitativeIndicators End  AS State  ")
            sql.AppendLine(" FROM Execution INNER JOIN ")
            sql.AppendLine("   " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Execution.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.AppendLine(" Project ON Execution.IdProject = Project.idkey and Project.isLastVersion='1' ")



            ' verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then

                sql.Append(where & " Project.idkey ='" & idProject & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not UserName.Equals("") Then

                sql.Append(where & "ApplicationUser.Name like '%" & UserName & "%'")
                where = " AND "

            End If


            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  Execution.CreateDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  Execution.CreateDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & "  Execution.CreateDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If


            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getLearningList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar   getLearningList ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function



    ''' <summary>
    ''' Consulta la lista de testimonios
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idCity"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="ProjectRole"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getTestimonyList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                        Optional ByVal idProject As String = "", _
                                        Optional ByVal idCity As String = "", _
                                        Optional ByVal idDepto As String = "", _
                                         Optional ByVal StartDate As String = "", _
                                        Optional ByVal EndDate As String = "", _
                                         Optional ByVal ProjectRole As String = "", _
                                           Optional ByVal IdTestimony As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' contruir el sql
            sql.AppendLine(" SELECT  Project.Name AS Project,  convert(varchar(10), Execution.CreateDate, 103) AS CreateDate, Testimony.ProjectRole, Depto.Name AS Depto, City.Name AS City,  ")
            sql.AppendLine(" Testimony.Name AS NameTestimony, Testimony.Age, Testimony.Id AS IdTestimony, Testimony.Sex, ")
            sql.AppendLine("  Testimony.Phone, Testimony.Email, Testimony.Description  ")
            sql.AppendLine(" FROM Testimony INNER JOIN ")
            sql.AppendLine("   Execution ON Testimony.IdExecution = Execution.Id INNER JOIN ")
            sql.AppendLine(" Project ON Execution.IdProject = Project.idkey and Project.isLastVersion='1' INNER JOIN ")
            sql.AppendLine(" " & dbSecurityName & ".dbo.City City ON Testimony.IdCity = City.ID INNER JOIN ")
            sql.AppendLine(" " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then

                sql.Append(where & " Project.idkey ='" & idProject & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not ProjectRole.Equals("") Then

                sql.Append(where & "Testimony.ProjectRole like '%" & ProjectRole & "%'")
                where = " AND "

            End If


            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  Execution.CreateDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  Execution.CreateDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & "  Execution.CreateDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idCity.Equals("") Then

                sql.Append(where & "Testimony.IdCity= '" & idCity & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idDepto.Equals("") Then

                sql.Append(where & "Depto.ID= '" & idDepto & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not IdTestimony.Equals("") Then

                sql.Append(where & "Testimony.Id= '" & IdTestimony & "'")
                where = " AND "

            End If


            ' ejecutar la intruccion
            Return GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getTestimonyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar   getTestimonyList ")

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Consulta los bancos de documentos 
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idDocumentType"></param>
    ''' <param name="UserName"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportAttachmentsDocuments(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idProject As String = "", _
        Optional ByVal idDocumentType As String = "", _
          Optional ByVal UserName As String = "", _
          Optional ByVal StartDate As String = "", _
           Optional ByVal EndDate As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            ' construir la sentencia
            sql.AppendLine(" SELECT Distinct Project.Name AS Project, Documents.Title, DocumentsByEntity.EntityName, Documents.Description, ")
            sql.AppendLine(" ApplicationUser.Name AS UserName, convert(varchar(10), Documents.CreateDate, 103) AS CreateDate , VisibilityLevel.Name AS VisibilityLevel, Documents.AttachFile ")
            sql.AppendLine(" From Project RIGHT OUTER JOIN ")
            sql.AppendLine("  DocumentsByEntity INNER JOIN ")
            sql.AppendLine("  Documents ON DocumentsByEntity.IdDocuments = Documents.Id INNER JOIN ")
            sql.AppendLine("   " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Documents.IdUser = ApplicationUser.ID INNER JOIN ")
            sql.AppendLine("  VisibilityLevel ON Documents.IdVisibilityLevel = VisibilityLevel.Id ON Project.idKey = DocumentsByEntity.IdnEntity and  Project.IslastVersion=1 and DocumentsByEntity.EntityName='ProjectEntity' ")

            'Verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then
                sql.Append(where & " Project.Idkey = '" & idProject & "' and DocumentsByEntity.EntityName='ProjectEntity'  ")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If Not idDocumentType.Equals("") Then
                sql.Append(where & " Documents.IdDocumentType = '" & idDocumentType & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not UserName.Equals("") Then
                sql.Append(where & " ApplicationUser.Name like '%" & UserName & "%'")
                where = " AND "
            End If




            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  Documents.CreateDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  Documents.CreateDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & "  Documents.CreateDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportAttachmentsDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de banco de datos.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Consulta los  documentos de una subactividad
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="UserName"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportProducts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idProject As String = "", _
         Optional ByVal UserName As String = "", _
          Optional ByVal StartDate As String = "", _
           Optional ByVal EndDate As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            ' construir la sentencia
            sql.AppendLine(" SELECT Project.Name AS Project, Documents.Title, DocumentsByEntity.EntityName, Documents.Description,  ")
            sql.AppendLine(" ApplicationUser.Name AS UserName, Documents.AttachFile, convert(varchar(10), SubActivityInformationRegistry.CreateDate, 103) AS CreateDate ")
            sql.AppendLine(" FROM DocumentsByEntity INNER JOIN ")
            sql.AppendLine("  Documents ON DocumentsByEntity.IdDocuments = Documents.Id INNER JOIN ")
            sql.AppendLine(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser INNER JOIN ")
            sql.AppendLine(" SubActivityInformationRegistry INNER JOIN ")
            sql.AppendLine(" Project INNER JOIN ")
            sql.AppendLine(" Objective ON Project.idkey = Objective.IdProject and Project.isLastVersion='1' INNER JOIN ")
            sql.AppendLine(" Component ON Objective.idkey = Component.IdObjective and Objective.isLastVersion='1' INNER JOIN ")
            sql.AppendLine(" Activity ON Component.idkey = Activity.IdComponent and Component.isLastVersion='1' INNER JOIN ")
            sql.AppendLine("  SubActivity ON Activity.idkey = SubActivity.IdActivity and Activity.isLastVersion='1' ON SubActivityInformationRegistry.IdSubActivity = SubActivity.idkey and SubActivity.isLastVersion='1' ON   ")
            sql.AppendLine(" ApplicationUser.ID = SubActivityInformationRegistry.IdUser ON DocumentsByEntity.IdnEntity = SubActivityInformationRegistry.Id ")

            sql.Append(where & " DocumentsByEntity.EntityName='SubActivityInformationRegistryEntity' ")
            where = " AND "

            'Verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then
                sql.Append(where & " Project.Idkey = '" & idProject & "' ")
                where = " AND "

            End If

           

            'Verificar si hay entrada de datos para el campo
            If Not UserName.Equals("") Then
                sql.Append(where & " ApplicationUser.Name like '%" & UserName & "%'")
                where = " AND "
            End If




            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  SubActivityInformationRegistry.CreateDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  SubActivityInformationRegistry.CreateDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & "  SubActivityInformationRegistry.CreateDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProducts")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de productos.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function


    ''' <summary>
    ''' Retorna los proyectos con buenas practicas
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idDepto"></param>
    ''' <param name="idCity"></param>
    ''' <param name="UserName"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportGoodPractice(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "", _
          Optional ByVal UserName As String = "", _
          Optional ByVal StartDate As String = "", _
           Optional ByVal EndDate As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            ' construir la sentencia
            sql.AppendLine(" SELECT Distinct   Project.Name AS Project, StrategicLine.Name AS StrategicLine, Project.Objective, t.Depto , t.City,  ")
            sql.AppendLine(" convert(varchar(10),  Project.BeginDate, 103) AS BeginDate, convert(varchar(10), CloseRegistry.ClosingDate, 103) AS  ClosingDate, Project.TotalCost, Project.FSCContribution ")
            sql.AppendLine(" FROM Program INNER JOIN ")
            sql.AppendLine(" StrategicLine ON Program.IdStrategicLine = StrategicLine.Id INNER JOIN ")
            sql.AppendLine(" ProgramComponent ON Program.Id = ProgramComponent.IdProgram INNER JOIN ")
            sql.AppendLine(" ProgramComponentByProject ON ProgramComponent.Id = ProgramComponentByProject.IdProgramComponent INNER JOIN ")
            sql.AppendLine(" Project ON ProgramComponentByProject.IdProject = Project.idkey and Project.isLastVersion='1'  INNER JOIN ")
            sql.AppendLine("  CloseRegistry ON Project.IdKey = CloseRegistry.IdProject and Project.isLastVersion='1' LEFT OUTER JOIN ")
            sql.AppendLine("  (Select Project.Id,(select Top 1  Depto.Name AS Depto from  ProjectLocation INNER JOIN ")
            sql.AppendLine(" " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.ID INNER JOIN  " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID")
            sql.AppendLine(" where  ProjectLocation.IdProject=Project.idkey  and Project.isLastVersion='1' ")
            ' verificar si hay entrada de datos para el campo
            If Not idDepto.Equals("") Then
                sql.Append(" and Depto.Name = '" & idDepto & "'")
            End If
            sql.AppendLine("  ) as Depto,  (select Top 1  City.Name AS City from  ProjectLocation INNER JOIN " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.ID INNER JOIN  ")
            sql.AppendLine("    " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID  ")
            sql.AppendLine("   where  ProjectLocation.IdProject=Project.idkey  and Project.isLastVersion='1' ")
            ' verificar si hay entrada de datos para el campo
            If Not idCity.Equals("") Then
                sql.Append(" and City.Name  = '" & idCity & "'")
            End If
            sql.AppendLine("   ) as City from Project) as  t   ")
            sql.AppendLine("    on t.Id=Project.idkey   and Project.isLastVersion='1' LEFT OUTER JOIN  ")
            sql.AppendLine(" OperatorByProject ON Project.idkey = dbo.OperatorByProject.IdProject and Project.isLastVersion='1' LEFT OUTER JOIN ")
            sql.AppendLine(" Third ON OperatorByProject.IdOperator = Third.Id ")
            sql.Append(where & " CloseRegistry.GoodPractice = '1'  ")
            where = " AND "


            'Verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then
                sql.Append(where & " StrategicLine.Id = '" & idStrategicLine & "'  ")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If Not idCity.Equals("") Then
                sql.Append(where & " t.City = '" & idCity & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not idDepto.Equals("") Then
                sql.Append(where & " t.Depto = '" & idDepto & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not UserName.Equals("") Then
                sql.Append(where & " Third.Name like '%" & UserName & "%'")
                where = " AND "
            End If




            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  CloseRegistry.ClosingDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  CloseRegistry.ClosingDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CloseRegistry.ClosingDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportGoodPractice")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de buenas practicas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Retorna los datos del operadot
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="idDepto"></param>
    ''' <param name="idCity"></param>
    ''' <param name="UserName"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="StartDate0"></param>
    ''' <param name="EndDate0"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportOperatorList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal idProject As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal idDepto As String = "", _
        Optional ByVal idCity As String = "", _
          Optional ByVal UserName As String = "", _
          Optional ByVal StartDate As String = "", _
           Optional ByVal EndDate As String = "", _
        Optional ByVal StartDate0 As String = "", _
           Optional ByVal EndDate0 As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            ' construir la sentencia
            sql.AppendLine(" SELECT DISTINCT City.Name AS City, Depto.Name AS Depto, Project.Name AS Project, Third.Name AS Operator, Third.Id ")
            sql.AppendLine(" FROM CloseRegistry RIGHT OUTER JOIN ")
            sql.AppendLine(" Project ON CloseRegistry.IdProject = Project.idkey and Project.isLastVersion='1' LEFT OUTER JOIN ")
            sql.AppendLine("  Program INNER JOIN ")
            sql.AppendLine("  StrategicLine ON Program.IdStrategicLine = StrategicLine.Id INNER JOIN ")
            sql.AppendLine(" ProgramComponent ON Program.Id = ProgramComponent.IdProgram INNER JOIN ")
            sql.AppendLine("  ProgramComponentByProject ON ProgramComponent.Id = ProgramComponentByProject.IdProgramComponent ON  ")
            sql.AppendLine("  Project.idkey = ProgramComponentByProject.IdProject and Project.isLastVersion='1' LEFT OUTER JOIN ")
            sql.AppendLine("   " & dbSecurityName & ".dbo.City City INNER JOIN ")
            sql.AppendLine(" ProjectLocation ON City.ID = ProjectLocation.IdCity INNER JOIN ")
            sql.AppendLine("  " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID ON Project.idkey = ProjectLocation.IdProject and Project.isLastVersion='1' LEFT OUTER JOIN ")
            sql.AppendLine("  OperatorByProject INNER JOIN ")
            sql.AppendLine("  Third ON OperatorByProject.IdOperator = Third.Id ON Project.idkey = OperatorByProject.IdProject and Project.isLastVersion='1' ")



            'Verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then
                sql.Append(where & " StrategicLine.Id = '" & idStrategicLine & "'  ")
                where = " AND "

            End If


            'Verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then
                sql.Append(where & " Project.idKey = '" & idProject & "' and Project.isLastVersion='1'  ")
                where = " AND "

            End If



            'Verificar si hay entrada de datos para el campo
            If Not idCity.Equals("") Then
                sql.Append(where & " City.ID = '" & idCity & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not idDepto.Equals("") Then
                sql.Append(where & " Depto.ID = '" & idDepto & "'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not UserName.Equals("") Then
                sql.Append(where & " Third.Name like '%" & UserName & "%'")
                where = " AND "
            End If




            'Verificar si hay entrada de datos para el campo
            If StartDate.Length > 0 OrElse EndDate.Length > 0 Then

                If StartDate.Length > 0 AndAlso EndDate.Length > 0 Then
                    sql.AppendLine(where & "  Project.BeginDate BETWEEN '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate.Length > 0 Then
                    sql.Append(where & "  Project.BeginDate >= '" & CDate(StartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " Project.BeginDate <= '" & CDate(EndDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If StartDate0.Length > 0 OrElse EndDate0.Length > 0 Then

                If StartDate0.Length > 0 AndAlso EndDate0.Length > 0 Then
                    sql.AppendLine(where & "  CloseRegistry.ClosingDate BETWEEN '" & CDate(StartDate0).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(EndDate0).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf StartDate0.Length > 0 Then
                    sql.Append(where & "  CloseRegistry.ClosingDate >= '" & CDate(StartDate0).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CloseRegistry.ClosingDate <= '" & CDate(EndDate0).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportOperatorList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de Lista de operadores.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function



    ''' <summary>
    ''' Carga las estadisticas del sitio.
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportStatistics(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT (SELECT COUNT(Id) AS Idea FROM Idea) AS Idea, ")
            sql.AppendLine("(SELECT COUNT(ID) AS Expr1 FROM FSC_BPM.dbo.ActivityInstance WHERE (IDCauseTransition = 1114)) as ApprovalIdea, ")
            sql.AppendLine(" (SELECT COUNT(ID) AS Expr1 FROM FSC_BPM.dbo.ActivityInstance WHERE (IDCauseTransition = 1128)) as ApprovalProject, ")
            sql.AppendLine(" (Select Count(IdKey) From (SELECT DISTINCT ProjectPhase.name, Project.Name AS Expr1,Project.idKey,ProjectPhase.id ")
            sql.AppendLine(" FROM Project INNER JOIN ProjectPhase ON Project.idkey = ProjectPhase.id and ProjectPhase.id=1 and Project.isLastVersion='1' ) as tabla) as Formulation,")
            sql.AppendLine(" (Select Count(IdKey) From (SELECT DISTINCT ProjectPhase.name, Project.Name AS Expr1,Project.idKey,ProjectPhase.id ")
            sql.AppendLine(" FROM Project INNER JOIN ProjectPhase ON Project.idkey = ProjectPhase.id and ProjectPhase.id=2 and Project.isLastVersion='1') as tabla) as Planeation,")
            sql.AppendLine(" (Select Count(IdKey) From (SELECT DISTINCT ProjectPhase.name, Project.Name AS Expr1,Project.idKey,ProjectPhase.id ")
            sql.AppendLine(" FROM Project INNER JOIN ProjectPhase ON Project.idkey = ProjectPhase.id and ProjectPhase.id=3 and Project.isLastVersion='1' ) as tabla) as Execution,")
            sql.AppendLine(" (Select Count(IdKey) From (SELECT DISTINCT ProjectPhase.name, Project.Name AS Expr1,Project.idKey,ProjectPhase.id ")
            sql.AppendLine(" FROM Project INNER JOIN ProjectPhase ON Project.idkey = ProjectPhase.id and ProjectPhase.id=4 and Project.isLastVersion='1') as tabla) as Evaluation,")
            sql.AppendLine(" (Select Count(IdKey) From (SELECT DISTINCT ProjectPhase.name, Project.Name AS Expr1,Project.idKey,ProjectPhase.id ")
            sql.AppendLine(" FROM Project INNER JOIN ProjectPhase ON Project.idkey = ProjectPhase.id and ProjectPhase.id=5 and Project.isLastVersion='1' ) as tabla) as Closing")



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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStatistics")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de estadisticas.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Retorna los resultados de una encuesta
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="Code"></param>
    ''' <param name="Inquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportInquest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal idProject As String = "", _
        Optional ByVal Code As String = "", _
        Optional ByVal Inquest As String = "") As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine(" SELECT Inquest.Name AS InquestName, QuestionsByInquestContent.QuestionText, AnswersByQuestion.Answer, Inquest.Id AS IdInquest,  ")
            sql.AppendLine(" InquestContent.Id AS IdInquestContent, QuestionsByInquestContent.Id AS IdQuestionByInquestContent,  ")
            sql.AppendLine("  AnswersByQuestion.Id AS IdAnswerByQuestion, Project.Code AS ProjectCode, Project.Name AS ProjectName, Project.idkey AS ProjectId ")
            sql.AppendLine(" FROM Inquest INNER JOIN")
            sql.AppendLine("  InquestContent ON Inquest.Id = InquestContent.IdInquest INNER JOIN ")
            sql.AppendLine(" QuestionsByInquestContent ON InquestContent.Id = QuestionsByInquestContent.IdInquestContent INNER JOIN ")
            sql.AppendLine("  AnswersByResolvedInquest ON QuestionsByInquestContent.Id = AnswersByResolvedInquest.IdQuestionsByInquestContent INNER JOIN  ")
            sql.AppendLine("  AnswersByQuestion ON QuestionsByInquestContent.Id = AnswersByQuestion.IdQuestionsByInquestContent AND  ")
            sql.AppendLine("   AnswersByResolvedInquest.IdAnswersByQuestion = AnswersByQuestion.Id INNER JOIN ")
            sql.AppendLine("     Project ON Inquest.IdProject = Project.idkey and Project.isLastVersion='1' ")
           


    

            'Verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then
                Sql.Append(where & " Project.idKey = '" & idProject & "' and Project.isLastVersion='1'  ")
                where = " AND "

            End If


            'Verificar si hay entrada de datos para el campo
            If Not Inquest.Equals("") Then
                sql.Append(where & " Inquest.Name like '%" & Inquest & "%'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If Not Code.Equals("") Then
                sql.Append(where & " Project.Code like '%" & Code & "%'")
                where = " AND "
            End If




            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, Sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte los graficos de una encuesta.")

        Finally
            ' liberando recursos
            Sql = Nothing
            data = Nothing

        End Try

    End Function


End Class
