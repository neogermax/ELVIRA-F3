Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections.Generic
Imports System.Data
Imports Gattaca.Application.ExceptionManager
Imports Gattaca.Application.Credentials

Public Class ReportFacadeTemp
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
        ByVal sIdStrategy As String) As DataTable

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
        ByVal sIdStrategicLine As String) As DataTable
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
            Throw New Exception("Error al cargar  el reporte detalle Linea Estrategica ")

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
            Throw New Exception("Error al cargar  indicadores de un Linea estrategica o estrategia ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    '''' <summary>
    '''' Consulta la información básica del proyecto
    '''' </summary>
    '''' <param name="idProject"></param>
    '''' <remarks></remarks>
    'Public Function loadReportBasicProjectDataMain(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '                                     ByVal idProject As Integer) As DataTable
    '    Dim Report As New ReportDALC
    '    Try

    '        ' retornar el objeto
    '        loadReportBasicProjectDataMain = Report.loadReportBasicProjectDataMain(objApplicationCredentials, idProject)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBasicProjectDataMain")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al cargar el reporte de datos básicos del proyecto ")

    '    End Try

    'End Function
#Region "OperationalPlanning"
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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            getStateSummoning = oReport.getStateSummoning(objApplicationCredentials, idStrategicLine, idProject, Code, StartDate, EndDate)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getStateSummoning")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte Estado de la convocatoria ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            getProposalList = oReport.getProposalList(objApplicationCredentials, idStrategicLine, idProject, Code, StartDate, EndDate, OperatorName, State, idProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProposalList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte la lista de propuestas ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            getProposalLocation = oReport.getProposalLocation(objApplicationCredentials, idProposal)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProposalLocation")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  las ciudades de una propuesta ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Consulta los testimonios en una lista
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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            getLearningList = oReport.getLearningList(objApplicationCredentials, idProject, UserName, StartDate, EndDate, State)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getLearningList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Aprendizajes, ajustes y logros ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            getTestimonyList = oReport.getTestimonyList(objApplicationCredentials, idProject, idCity, idDepto, StartDate, EndDate, ProjectRole, IdTestimony)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getTestimonyList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de testimonios")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportAttachmentsDocuments = oReport.loadReportAttachmentsDocuments(objApplicationCredentials, idProject, idDocumentType, UserName, StartDate, EndDate)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportAttachmentsDocuments")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el banco de documentos ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportProducts = oReport.loadReportProducts(objApplicationCredentials, idProject, UserName, StartDate, EndDate)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportProducts")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los productos ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportGoodPractice = oReport.loadReportGoodPractice(objApplicationCredentials, idStrategicLine, idDepto, idCity, UserName, StartDate, EndDate)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportGoodPractice")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las buenas practicas ")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportOperatorList = oReport.loadReportOperatorList(objApplicationCredentials, idProject, idStrategicLine, idDepto, idCity, UserName, StartDate, EndDate, StartDate0, EndDate0)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportOperatorList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de lista de operadores ")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Carga las estadisticas del sitio.
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function loadReportStatistics(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportStatistics = oReport.loadReportStatistics(objApplicationCredentials)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportStatistics")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de estadisticas")

        Finally
            ' liberando recursos
            oReport = Nothing

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
        Dim oReport As New ReportDalcTemp
        Try

            ' retornar el objeto
            loadReportInquest = oReport.loadReportInquest(objApplicationCredentials, idProject, Code, Inquest)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportInquest")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el reporte de grafico de encuestas")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function
#End Region

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
    ''' <param name="idStrategicLine">Identificador de la linea Estrategica</param>
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

        Dim oReport As New reportDalcTemp2
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportIdeaInventory = oReport.loadReportIdeaInventory(objApplicationCredentials, startDateRecord, endDateRecord, _
                startDate, endDate, idDepto, idCity, idStrategicLine, idsProgramComponents, source, startCost, endCost)

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
        ByVal idDepto As String, _
        ByVal idCity As String _
    ) As DataTable

        Dim oReport As New reportDalcTemp2
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportLocationsByIdea = oReport.loadReportLocationsByIdea(objApplicationCredentials, idDepto, idCity)

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

        Dim oReport As New reportDalcTemp2
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportActorsMap = oReport.loadReportActorsMap(objApplicationCredentials, IdTird, StartCreateDate, EndCreateDate, Actions, Experiences, Type)

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
#Region "Report BBVA"
    
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

        Dim oReport As New reportDalcTemp2
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportBBVA = oReport.loadReportBBVA(objApplicationCredentials, startDateRecord, startendDateRecord, startDate, startendDate, endDate, endstartDate, IdActivity, sLaboralSitiuation, sKindofClient, sIdentificacion, sRadicado, sNombreSolicitante)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadReportBBVA")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar  el reporte mapa de actores.")

        Finally
            ' liberando recursos
            oReport = Nothing

        End Try
    End Function



    Public Function loadReportBBVAActivity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As DataTable

        Dim oReport As New reportDalcTemp2
        Try

            'Se llama al metodo del dal que permite realizar la consulta requerida
            loadReportBBVAActivity = oReport.loadReportBBVAActivity(objApplicationCredentials)

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
            oReport = Nothing

        End Try
    End Function
#End Region

End Class