Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections.Generic
Imports System.Data
Imports Gattaca.Application.ExceptionManager
Imports Gattaca.Application.Credentials

Public Class ReportFacade
    ' defini el nombre
    Const MODULENAME As String = "ReportFacade"

    ''' <summary>
    ''' Consulta los responsables de los indicadores
    ''' </summary>
    ''' <remarks></remarks>
    Public Function loadIndicatorUsers(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadIndicatorUsers = oReport.loadIndicatorUsers(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicatorUsers")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  los usuarios de los indicadores")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Carga la lista de los años que hay en la trabla strategicobjective
    ''' </summary>
    ''' <returns>un objeto de tipo List(Of STRATEGICOBJECTIVEEntity )</returns>
    ''' <remarks></remarks>
    Public Function getListYear(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of STRATEGICOBJECTIVEEntity)

        ' definir los objetos
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            getListYear = oReport.getListYear(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getListYear")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de PERSPECTIVE. ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try

    End Function

    ''' <summary>
    '''Carga la consulta del reporte de plan general
    ''' </summary>
    ''' <param name="sYear">El año que quiere filtrar</param>
    ''' <remarks></remarks>
    Public Function loadReportGeneralPlan(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal sYear As String) As DataTable

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportGeneralPlan = oReport.loadReportGeneralPlan(objApplicationCredentials, sYear)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportGeneralPlan")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte plan general ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    '''Carga la consulta del reporte del detalle de la estrategia
    ''' </summary>
    ''' <param name="sIdStrategy">El id de la estrategia a buscar</param>
    ''' <remarks></remarks>
    Public Function loadReportStrategyDetail(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal sIdStrategy As String = "") As DataTable

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportStrategyDetail = oReport.loadReportStrategyDetail(objApplicationCredentials, sIdStrategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategyDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte detalle estrategia ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Carga la consulta del reporte del detalle de una linea estrategica
    ''' </summary>
    ''' <param name="sIdStrategicLine">El id de una linea estrategica a buscar</param>
    ''' <remarks></remarks>
    Public Function loadReportStrategicLineDetail(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal sIdStrategicLine As String = "") As DataTable
        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportStrategicLineDetail = oReport.loadReportStrategicLineDetail(objApplicationCredentials, sIdStrategicLine)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStrategicLineDatail")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte detalle de la Linea Estrategica ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        ByVal iduser As String) As DataTable


        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportIndicatorInventory = oReport.loadReportIndicatorInventory(objApplicationCredentials, IndicatorType, BeginDate, EndDate, iduser)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte inventario de indicadores ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    Public Function loadIndicators(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal IdStrategicLine As Integer, ByVal IdStrategy As Integer) As DataTable
        Dim oReport As New ReportDalcTemp

        Try

            ' retornar el objeto
            loadIndicators = oReport.loadIndicators(objApplicationCredentials, IdStrategicLine, IdStrategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadIndicators")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  indicadores de una linea estrategica o estrategia ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta la información básica del proyecto
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Function loadReportBasicProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idProject As String, _
    ByVal code As String, _
    ByVal year As Integer, _
    ByVal idProjectPhase As String) As DataTable

        Dim Report As New ReportDALC
        Try

            ' retornar el objeto
            loadReportBasicProject = Report.loadReportBasicProject(objApplicationCredentials, idProject, code, year, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBasicProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de datos básicos del proyecto ")

        End Try

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportLocationsByProject = oReport.loadReportLocationsByProject(objApplicationCredentials, idProject, idDepto, idCity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte de ubicaciones por proyecto.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportSourceByProject = oReport.loadReportSourceByProject(objApplicationCredentials, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte de fuentes por proyecto.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim Report As New ReportDALC
        Try

            ' retornar el objeto
            MatrixIndicator = Report.MatrixIndicator(objApplicationCredentials, idproject, typeIndicator, year, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "MatrixIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de matriz de indicadores ")

        End Try

    End Function

    ''' <summary>
    '''Carga las mediciones de los indicadores
    ''' </summary>
    ''' <param name="sIdIndicator">El id del indicador</param>
    ''' <remarks></remarks>
    Public Function loadMeasurementDateByIndicator(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal sIdIndicator As String) As DataTable
        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadMeasurementDateByIndicator = oReport.loadMeasurementDateByIndicator(objApplicationCredentials, sIdIndicator)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadMesurementDateByIndicator")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  las fechas de medición ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta la lista de actividades estrategicas por estrategia.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getStrategicActivityGantt(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                              ByVal idStrategy As String) As DataTable

        ' definiendo los objtos
        Dim report As New ReportDalcTemp

        Try
            ' ejecutar la intruccion
            Return report.getStrategicActivityGantt(objApplicationCredentials, idStrategy)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStrategicActivityGantt")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de actividades de la estrategia ")

        Finally
            ' liberando recursos
            report = Nothing

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

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportExecutionPlan = oReport.loadReportExecutionPlan(objApplicationCredentials, idProject, code, year, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportExecutionPlan")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte del plan de ejecución ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim report As New ReportDALC

        Try
            ' ejecutar la intruccion
            Return report.RiskMatrix(objApplicationCredentials, idProject, year, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "RiskMatrix")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la matriz de riesgos ")

        Finally
            ' liberando recursos
            report = Nothing

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

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportProjectChronogram = oReport.loadReportProjectChronogram(objApplicationCredentials, idComponent, idProjectPhase, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte cronograma del proyecto ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta las actividades ejecutadas
    ''' </summary>
    ''' <param name="idSubActivity"></param>
    ''' <remarks></remarks>
    Public Function loadReportSubActivityInformationRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idSubActivity As String) As DataTable


        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadReportSubActivityInformationRegistry = oReport.loadReportSubActivityInformationRegistry(objApplicationCredentials, idSubActivity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportSubActivityInformationRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte Subactividades vs Ejecución ")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            ' retornar el objeto
            loadConsultGeneralProjects = oReport.loadConsultGeneralProjects(objApplicationCredentials, idStrategicLine, projectName, _
                idDepto, idCity, targetPopulationName, idOperator, startTotalValue, endTotalValue, startContributionValue, _
                endContributionValue, effectivebudget, state, startClosingDate, endClosingDate)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta general de proyectos.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadReportRecruitmentPlan = oReport.loadReportRecruitmentPlan(objApplicationCredentials, idProject, idProjectPhase)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte del plan de contratación.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try
            ' retornar el objeto
            loadConsultGeneralContractRequest = oReport.loadConsultGeneralContractRequest(objApplicationCredentials, _
                 idManagement, idProject, dateStartRequest, dateEndRequest, contractorName)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte de solicitudes de contrato.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try
            ' retornar el objeto
            loadReportContractRequest = oReport.loadReportContractRequest(objApplicationCredentials, idContractRequestList, _
                idManagement, idProject, dateStartRequest, dateEndRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte de solicitudes de contrato.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta información de los contratistas por cada solicitud de contrato hecha
    ''' </summary>
    ''' <param name="idContractRequestList"></param>
    ''' <remarks></remarks>
    Public Function loadReportContractorsNameByContractRequest(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequestList As String = "") As DataTable

        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadReportContractorsNameByContractRequest = oReport.loadReportContractorsNameByContractRequest(objApplicationCredentials, idContractRequestList)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta de los contratistas para el reporte de solicitudes de contrato.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadReportGeneralListContracts = oReport.loadReportGeneralListContracts(objApplicationCredentials, idContractRequest, _
                contractNumber, idManagement, idStrategicLine, idProject, contractState, effectiveBudget, contractorName, supervisor)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta general de contratos.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta información de los contratistas por cada contrato
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadReportContractorsByGeneralListContracts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequest As String = "") As DataTable

        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadReportContractorsByGeneralListContracts = oReport.loadReportContractorsByGeneralListContracts(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los contratistas para la ficha técnica del contrato.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta información de la lista de pagos realizada por cada contrato.
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function loadReportPaymentsListByGeneralListContracts(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal idContractRequest As String = "") As DataTable


        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadReportPaymentsListByGeneralListContracts = oReport.loadReportPaymentsListByGeneralListContracts(objApplicationCredentials, idContractRequest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de pagos para la ficha técnica del contrato.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta de reporte de registro de proyectos cerrados
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="IdProject"></param>
    ''' <param name=""></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadCloseRegistry(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal IdProject As String) As DataTable
        'Definición de variables
        Dim oReport As New ReportDALC

        Try

            ' retornar el objeto
            loadCloseRegistry = oReport.loadCloseRegistry(objApplicationCredentials, IdProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadCloseRegistry")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta de registro de proyectos cerrados.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try
            ' retornar el objeto
            loadOperatorsByProject = oReport.loadOperatorsByProject(objApplicationCredentials, IdProject)

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
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try
            ' retornar el objeto
            loadReportListActivities = oReport.loadReportListActivities(objApplicationCredentials, idProject, idComponent, idResponsible, idState, endDateIni, endDateEnd)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta para el reporte lista de actividades.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        'Definición de variables
        Dim oReport As New ReportDALC

        Try
            ' retornar el objeto
            loadReportCLosedProjectList = oReport.loadReportCLosedProjectList(objApplicationCredentials, idStrategicLine, idProject)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProjectChronogram")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la consulta listado de proyectos cerrados.")

        Finally
            ' liberando recursos
            oReport = Nothing

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
    ''' <param name="idStrategicLine">Identificador de una Linea Estrategica</param>
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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportIdeaInventory = oReport.loadReportIdeaInventory(objApplicationCredentials, idIdea, startDateRecord, _
                endDateRecord, ideaName, idStrategicLine, source, idDepto, idCity, startCost, endCost, state)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte inventario de ideas.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportLocationsByIdea = oReport.loadReportLocationsByIdea(objApplicationCredentials, idIdea, idDepto, idCity)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte de ubicaciones por idea.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportAttachmentsByIdea = oReport.loadReportAttachmentsByIdea(objApplicationCredentials, idEntity, entityName)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte de adjuntos por idea.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportThirdsByIdea = oReport.loadReportThirdsByIdea(objApplicationCredentials, idIdea)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte de adjuntos por idea.")

        Finally
            ' liberando recursos
            oReport = Nothing

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

        Dim oReport As New ReportDALC
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportActorsMap = oReport.loadReportActorsMap(objApplicationCredentials, IdTird, StartCreateDate, EndCreateDate, Type)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportIndicatorInventory")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte mapa de actores.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

#End Region


#Region "Project"

    ''' <summary>
    ''' Cargar la lista de proyectos para exportarla a XML
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function exportList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        ' definir el objeto
        Dim project As New ProjectDALC

        Try

            ' retornar el objeto
            exportList = project.exportList(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "exportList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Projectos. ")

        Finally
            ' liberando recursos
            project = Nothing

        End Try

    End Function


#End Region

End Class