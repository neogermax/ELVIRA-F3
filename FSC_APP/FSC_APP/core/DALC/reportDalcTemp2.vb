
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class reportDalcTemp2

    Const MODULENAME As String = "ReportDALC"

    ''' <summary>
    ''' Carga la consulta del reporte de Inventario de ideas
    ''' </summary>
    ''' <param name="startDateRecord">Fecha de registro inicial</param>
    ''' <param name="endDateRecord">Fecha de registro final</param>
    ''' <param name="startDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha fin</param>
    ''' <param name="idDepto">Identificador del departamento</param>
    ''' <param name="idCity">Identificadores de las ciudades</param>
    ''' <param name="idStrategicLine">Identificador de la linea estrategica</param>
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
        ByVal idCity As String, _
        ByVal idStrategicLine As String, _
        ByVal idsProgramComponents As String, _
        ByVal source As String, _
        ByVal startCost As String, _
        ByVal endCost As String, _
        Optional ByVal state As String = "" _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportIdeaInventory")

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
            If startDate.Length > 0 OrElse endDate.Length > 0 Then

                If startDate.Length > 0 AndAlso endDate.Length > 0 Then
                    sql.AppendLine(where & " StartDate BETWEEN '" & CDate(startDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startDate.Length > 0 Then
                    sql.Append(where & " StartDate >= '" & CDate(startDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " StartDate <= '" & CDate(endDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idDepto.Length > 0 Then

                sql.Append(where & " IdIdea IN( SELECT DISTINCT IdIdea FROM vReportLocationsByIdea WHERE IdDepto = " & idDepto & ")")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idCity.Length > 0 Then

                sql.Append(where & " IdIdea IN( SELECT DISTINCT IdIdea FROM vReportLocationsByIdea WHERE IdCity = " & idCity & ")")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idStrategicLine.Length > 0 Then

                sql.Append(where & " IdStrategicLine = " & idStrategicLine)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If idsProgramComponents.Length > 0 Then

                sql.Append(where & " IdProgramComponent IN (" & idsProgramComponents & ")")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If source.Length > 0 Then

                sql.Append(where & " Source = '" & source & "'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startCost.Length > 0 OrElse endCost.Length > 0 Then

                If startCost.Length > 0 Then
                    sql.Append(where & " Cost >= " & startCost.Replace(",", "."))
                Else
                    sql.Append(where & " Cost <= " & endCost.Replace(",", "."))
                End If

                where = " AND "

            ElseIf startCost.Length > 0 AndAlso endCost.Length > 0 Then

                sql.AppendLine(where & " Cost BETWEEN " & startCost.Replace(",", ".") & " AND " & endCost.Replace(",", "."))
                where = " AND "

            End If

            'Se agrega el ordenamiento para la consulta
            sql.AppendLine(" ORDER BY IdIdea ")

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
        ByVal idDepto As String, _
        ByVal idCity As String _
    ) As DataTable

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT * FROM vReportLocationsByIdea")

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
    ''' Carga la consulta del reporte de mapa de actores
    ''' </summary>
    ''' <param name="IdTird">Identificador del tercero</param>
    ''' <param name="StartCreateDate">Fecha de creación inicial</param> 
    ''' <param name="EndCreateDate">Fecha de creación final</param> 
    ''' <param name="Actions">Acciones</param> 
    ''' <param name="Experiences">Experiencias</param> 
    ''' <param name="Type">Tipo</param> 
    ''' <remarks></remarks>
    Public Function loadReportActorsMap(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal IdTird As String, _
        ByVal StartCreateDate As String, _
        ByVal EndCreateDate As String, _
        ByVal Actions As String, _
        ByVal Experiences As String, _
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
            If Actions.Length > 0 Then

                sql.Append(where & " Actions Like '%" & Actions & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If Experiences.Length > 0 Then

                sql.Append(where & " Experiences Like '%" & Experiences & "%'")
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


    ''' <summary>
    ''' Este reporte muestra la consulta detallada del negocio
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="startDateRecord"></param>
    ''' <param name="startendDateRecord"></param>
    ''' <param name="startDate"></param>
    ''' <param name="startendDate"></param>
    ''' <param name="endDate"></param>
    ''' <param name="endstartDate"></param>
    ''' <param name="IdActivity"></param>
    ''' <param name="sLaboralSitiuation"></param>
    ''' <param name="sKindofClient"></param>
    ''' <param name="sIdentificacion"></param>
    ''' <param name="sRadicado"></param>
    ''' <param name="sNombreSolicitante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportBBVA(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal startDateRecord As String, _
         ByVal startendDateRecord As String, _
        ByVal startDate As String, _
        ByVal startendDate As String, _
        ByVal endDate As String, _
          ByVal endstartDate As String, _
         ByVal IdActivity As String, _
         ByVal sLaboralSitiuation As String, _
          ByVal sKindofClient As String, _
          ByVal sIdentificacion As String, _
         ByVal sRadicado As String, _
          ByVal sNombreSolicitante As String) As DataTable
   

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT  Formulario, FormularioInstancia,Table1.StartTime,IdProceso, [Número de identificación], [Nombre del cliente], ")
            sql.AppendLine(" case When UPPER([Nombre del Gestor]) <> 'GESTOR VIRTUAL BBVA' THEN 'Gestor Comercial' END as Canal, ")
            sql.AppendLine(" [Nombre del Gestor], [Tipo de producto], [Tipo de subproducto], [Tipo de cliente],[Valor solicitado], ")
            sql.AppendLine(" Activity.name AS IDActivity, Case when ActivityInstance.Status=248 then 'Finalizada' else case when ActivityInstance.Status=2 then 'En Atención' else 'Sin iniciar' end end as Status,  ActivityInstance.StartTime as InicioEtapa ,  ")
            sql.AppendLine("  ActivityInstance.EndTime as CierreEtapa    , Isnull(cast(Participant.name  as nvarchar),'sin asignar') as Asingadoa ")
            sql.AppendLine(" FROM BBVA_eWorkFlow.dbo.TraceableActivity TraceableActivity  INNER JOIN ")
            sql.AppendLine(" BBVA_eWorkFlow.dbo.Activity Activity ON TraceableActivity.IDActivity = Activity.ID INNER JOIN ")
            sql.AppendLine(" BBVA_eWorkFlow.dbo.ActivityInstance ActivityInstance ON Activity.ID = ActivityInstance.IDActivity  LEFT OUTER JOIN ")
            sql.AppendLine("  BBVA_eWorkFlow.dbo.Participant Participant on ActivityInstance.IDParticipant=Participant.Id INNER JOIN	 ")
            sql.AppendLine(" (Select Formulario, FormularioInstancia,StartTime,IdProceso, [Número de identificación], [Nombre del cliente], ")
            sql.AppendLine(" [Nombre del Gestor], [Tipo de producto],(Select Nombre From   BBVA_eFormBuilder.dbo.Producto where Id=[Tipo de subproducto]) as [Tipo de subproducto], [Tipo de cliente],[Valor solicitado],  [Situación Laboral] ")
            sql.AppendLine(" FROM ")
            sql.AppendLine(" (SELECT   FormInstance.IdForm as Formulario,  FormInstance.id AS FormularioInstancia,ProcessInstance.StartTime,  ")
            sql.AppendLine(" ProcessInstance.Id as IdProceso,  FieldByFormInstance.value as valor, FormInstance.idForm, Field.text as Texto, FormInstance.EntryData, ProcessInstance.ID ")
            sql.AppendLine(" FROM  BBVA_eFormBuilder.dbo.Field AS  Field INNER JOIN ")
            sql.AppendLine(" BBVA_eFormBuilder.dbo.FieldByFormInstance AS FieldByFormInstance ON Field.id = FieldByFormInstance.idField INNER JOIN")
            sql.AppendLine(" BBVA_eFormBuilder.dbo.FormInstance AS FormInstance ON FieldByFormInstance.idFormInstance = FormInstance.id  Inner Join")
            sql.AppendLine(" BBVA_eWorkFlow.dbo.ProcessInstance ProcessInstance on ProcessInstance.EntryData = FormInstance.IdForm  and Cast( ProcessInstance.IdEntryData as int )= FormInstance.id ")
            sql.AppendLine(" WHERE     (FormInstance.IdForm = 88)) t")
            sql.AppendLine(" PIVOT ( MAX(valor) FOR Texto IN ( [Número de identificación], [Nombre del cliente], [Nombre del Gestor], [Tipo de producto], [Tipo de subproducto], [Tipo de cliente], [Valor solicitado],  [Situación Laboral] ) ) AS PVT")
            sql.AppendLine(" ) as table1 on ActivityInstance.IDProcessInstance=Table1.IdProceso ")
            sql.Append(where & " (Activity.ActivityType = '4' OR Activity.ActivityType = '0')")
            where = " AND "


            'Verificar si hay entrada de datos para el campo
            If startDateRecord.Length > 0 OrElse startendDateRecord.Length > 0 Then

                If startDateRecord.Length > 0 AndAlso startendDateRecord.Length > 0 Then
                    sql.AppendLine(where & " CreateTime BETWEEN '" & CDate(startDateRecord).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(startendDateRecord).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startDateRecord.Length > 0 Then
                    sql.Append(where & " CreateTime >= '" & CDate(startDateRecord).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " CreateTime <= '" & CDate(startendDateRecord).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If startDate.Length > 0 OrElse startendDate.Length > 0 Then

                If startDate.Length > 0 AndAlso startendDate.Length > 0 Then
                    sql.AppendLine(where & "  ActivityInstance.StartTime BETWEEN '" & CDate(startDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(startendDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startDate.Length > 0 Then
                    sql.Append(where & "  ActivityInstance.StartTime >= '" & CDate(startDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & "  ActivityInstance.StartTime <= '" & CDate(startendDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If


            'Verificar si hay entrada de datos para el campo
            If endDate.Length > 0 OrElse endstartDate.Length > 0 Then

                If endDate.Length > 0 AndAlso endstartDate.Length > 0 Then
                    sql.AppendLine(where & " ActivityInstance.EndTime BETWEEN '" & CDate(endDate).ToString("yyyy/MM/dd 00:00:00") & "' AND '" & CDate(endstartDate).ToString("yyyy/MM/dd 23:59:59") & "'  ")
                ElseIf startDate.Length > 0 Then
                    sql.Append(where & " ActivityInstance.EndTime >= '" & CDate(endDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                Else
                    sql.Append(where & " ActivityInstance.EndTime <= '" & CDate(endstartDate).ToString("yyyy/MM/dd 00:00:00") & "'")
                End If
                where = " AND "

            End If



            'Verificar si hay entrada de datos para el campo
            If sLaboralSitiuation <> "Todos" Then
                sql.Append(where & "  [Situación Laboral] Like '%" & sLaboralSitiuation & "%'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If sKindofClient <> "Todos" Then
                sql.Append(where & " [Tipo de cliente] Like '%" & sKindofClient & "%'")
                where = " AND "
            End If

            'Verificar si hay entrada de datos para el campo
            If IdActivity <> "Todos" And IdActivity.Length > 0 Then
                Dim sListActivities As String = loadReportBBVAListActivity(objApplicationCredentials, IdActivity)
                sql.Append(where & " ( Activity.ID IN( " & sListActivities & "))")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If sIdentificacion.Length > 0 Then

                sql.Append(where & " [Número de identificación] Like '%" & sIdentificacion & "%'")
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If sRadicado.Length > 0 Then

                sql.Append(where & "  IdProceso = " & sRadicado)
                where = " AND "

            End If

            'Verificar si hay entrada de datos para el campo
            If sNombreSolicitante.Length > 0 Then

                sql.Append(where & "  [Nombre del cliente] Like  '%" & sNombreSolicitante & "%'")
                where = " AND "

            End If
            ''Se agrega el ordenamiento para la consulta
            sql.AppendLine(" Order by FormularioInstancia DESC ")

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
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBBVA")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte detallado del negocio.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga las actividades del la tabla vertical
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportBBVAActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable


        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT Vertical.ID, Vertical.Name ")
            sql.AppendLine(" FROM   BBVA_eWorkFlow.dbo.Vertical Vertical ")
          

            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' retornar el objeto
            Return data

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBBVAActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las actividades.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Consulta que carga la lista de actividades asociada a una actividad de la lista VerticalByActivity
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="sIdActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportBBVAListActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal sIdActivity As String) As String


        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim sIn As String = ""
        Dim sComodin As String = ""

        Try

            ' construir la sentencia
            sql.AppendLine("SELECT VerticalByActivity.IdActivity ")
            sql.AppendLine(" FROM   BBVA_eWorkFlow.dbo.Vertical Vertical INNER JOIN ")
            sql.AppendLine("    BBVA_eWorkFlow.dbo.VerticalByActivity  VerticalByActivity on Vertical.Id=VerticalByActivity.IdVertical ")
            sql.AppendLine("   where Vertical.Id=" & sIdActivity)
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each Row As DataRow In data.Rows
                sIn = sIn + sComodin + Row("IdActivity").ToString()
                sComodin = ","
            Next


            ' retornar el objeto
            Return sIn

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBBVAListActivity")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de actividades.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing

        End Try

    End Function

End Class
