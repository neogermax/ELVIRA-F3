Imports System.Web.Script.Serialization
Imports FSC_DAO.model
Imports System.Linq
Imports Gattaca.Application.Credentials
Imports System.Data.Linq
Imports Newtonsoft.Json
''' <summary>
''' TODO: Form ajaxRequest create by Juan Camilo Martinez Morales
''' Date: 16/05/2014
''' </summary>
''' <remarks></remarks>
Partial Public Class ajaxRequest
    Inherits System.Web.UI.Page

#Region "Properties public and private"
    Dim _existRequest As Boolean = False
    Dim _idProject As Integer = 0
    Dim _ContractNumber As String = "N/A"
    Dim _Settlement_Date As String = "N/A"
#End Region

    ''' <summary>
    ''' Event Page Load for this page
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">Event</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Count of keys sent for request ajax
            Dim countKeys As Integer = Request.Form.AllKeys.Length

            If countKeys > 0 Then
                _idProject = Convert.ToInt32(Request.Form("idProject"))

                Dim objCRequest As FSC_DAO.model.CRequest = New FSC_DAO.model.CRequest()
                _existRequest = objCRequest.existRequestForProject(_idProject)
                _ContractNumber = objCRequest.getNumberContractFromProject(_idProject)
                _Settlement_Date = objCRequest.getSettlement_DateFromProject(_idProject)

                If _existRequest Then
                    _idProject = objCRequest.getExistRequestIdForProject(_idProject)
                End If


                Dim actionToResponse As String = Request.Form("action")
                Dim proyecto As String
                'Method for action depend of the request transaction
                Select Case actionToResponse
                    Case "getInformationProject"
                        'Get the  project information relevant 
                        getInformationProject(_idProject)
                        Exit Select
                    Case "loadThirdProject"
                        'Get thirs by project
                        getThirdsByProject(_idProject)
                        Exit Select
                    Case "loadFlowProject"
                        'Get flows by project
                        getFlowsByProject(_idProject)
                        Exit Select
                    Case "loadDetailsFlowsProject"
                        'Get flows by project
                        getDetailsFlowsByProject(_idProject)
                        Exit Select
                    Case "saveInformationRerquest"
                        'Get flows by project
                        saveInformationRerquest()
                        Exit Select
                    Case "getRequestTypeForProject"
                        getRequestTypeForProject(_idProject)
                        Exit Select
                    Case "getLastContactForProjectByThird"
                        getLastContactForProjectByThird()
                        Exit Select
                    Case "ExportTerms"
                        proyecto = Request.Form("idProject")
                        ExportTerms(proyecto)
                        Exit Select
                End Select
            End If

        Catch ex As Exception
            'Error Message
        End Try
    End Sub

    Protected Sub getInformationProject(ByVal idProject As Integer)

        If Not _existRequest Then
            Dim objCProject As FSC_DAO.model.CProject = New FSC_DAO.model.CProject()
            objCProject.Id = idProject
            Dim objRequestObjCProject As Project = objCProject._selectProjectById()
            objRequestObjCProject.Id = 0
            'Response data for file javascript
            Dim JSONDone As String = String.Format("[{0},""{1}"", ""{2}""]", JsonConvert.SerializeObject(objRequestObjCProject).ToString(), _ContractNumber, _Settlement_Date)

            'Response data for file javascript
            Response.Write(JSONDone)
        Else
            Dim objCRequest As FSC_DAO.model.CRequest = New FSC_DAO.model.CRequest()
            objCRequest.IdProject = idProject
            Dim objRequestObjCRequest As FSC_DAO.model.Request = objCRequest.selectRequestByProject()

            Dim JSONDone As String = String.Format("[{0},""{1}"", ""{2}""]", JsonConvert.SerializeObject(objRequestObjCRequest).ToString(), _ContractNumber, _Settlement_Date)

            'Response data for file javascript
            Response.Write(JSONDone)
        End If

    End Sub

    Protected Sub getThirdsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objThirds = Nothing

        If Not _existRequest Then
            objThirds = (From objThirdByProject In objFscDaoDataContext.ThirdByProject Where objThirdByProject.IdProject = idProject Select objThirdByProject.Contact, objThirdByProject.Documents, objThirdByProject.Email, objThirdByProject.FSCorCounterpartContribution, objThirdByProject.Id, objThirdByProject.IdThird, objThirdByProject.Name, objThirdByProject.Phone, objThirdByProject.Type, objThirdByProject.VrSpecies, objThirdByProject.Vrmoney, objThirdByProject.generatesflow).ToArray()
        Else
            objThirds = (From objThirdByProject In objFscDaoDataContext.ThirdByRequest Where objThirdByProject.IdRequest = idProject Select objThirdByProject.Contact, objThirdByProject.Documents, objThirdByProject.Email, objThirdByProject.FSCorCounterpartContribution, objThirdByProject.Id, objThirdByProject.IdThird, objThirdByProject.Name, objThirdByProject.Phone, objThirdByProject.Type, objThirdByProject.VrSpecies, objThirdByProject.Vrmoney, objThirdByProject.generatesflow).ToArray()
        End If

        Dim objSerializedObject = JsonConvert.SerializeObject(objThirds)

        'objSerializedObject = objSerializedObject.Replace("""", "\""")

        'objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub getFlowsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objFlows = Nothing

        If Not _existRequest Then
            objFlows = (From objPaymentflow In objFscDaoDataContext.Paymentflow Where objPaymentflow.idproject = idProject Select objPaymentflow).ToArray()
        Else
            objFlows = (From objPaymentflow In objFscDaoDataContext.Paymentflow_Request Where objPaymentflow.IdRequest = idProject Select objPaymentflow).ToArray()
        End If


        Dim objSerializedObject = JsonConvert.SerializeObject(objFlows)

        'objSerializedObject = objSerializedObject.Replace("""", "\""")
        'objSerializedObject = objSerializedObject.Replace("\n", "\\n")
        'objSerializedObject = objSerializedObject.Replace("\r", "\\r")

        'objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub getRequestTypeForProject(ByVal idRequest As Integer)
        If _existRequest Then
            Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

            Dim objRerquestTypes = (From objRequest_RequestType In objFscDaoDataContext.Request_RequestType Where objRequest_RequestType.IdRequest = idRequest Select objRequest_RequestType.IdRequestType, objRequest_RequestType.IdRequestSubtype).ToArray()

            Dim objSerializedObject = JsonConvert.SerializeObject(objRerquestTypes)

            Response.Write(objSerializedObject)
        Else
            Response.Write("[]")
        End If
        

        'objSerializedObject = objSerializedObject.Replace("""", "\""")
        'objSerializedObject = objSerializedObject.Replace("\n", "\\n")
        'objSerializedObject = objSerializedObject.Replace("\r", "\\r")

        'objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")



    End Sub

    Protected Sub getDetailsFlowsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objDetailsFlows = Nothing
        If Not _existRequest Then
            objDetailsFlows = (From objDetailedcashflows In objFscDaoDataContext.Detailedcashflows Where objDetailedcashflows.IdProject = idProject Select objDetailedcashflows).ToArray()
        Else
            objDetailsFlows = (From objDetailedcashflows In objFscDaoDataContext.DetailedCashFlowsRequest Where objDetailedcashflows.IdRequest = idProject Select objDetailedcashflows).ToArray()
        End If

        Dim objSerializedObject = JsonConvert.SerializeObject(objDetailsFlows)

        'objSerializedObject = objSerializedObject.Replace("""", "\""")
        'objSerializedObject = objSerializedObject.Replace("\n", "\\n")
        'objSerializedObject = objSerializedObject.Replace("\r", "\\r")

        'objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub saveInformationRerquest()
        Dim JSONProjectInformation = JsonConvert.DeserializeObject(Of FSC_DAO.model.Project)(Request.Form("projectInformation"))
        Dim JSONThirdsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.ThirdByProject))(Request.Form("thirdsInformation"))
        Dim JSONFlowsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.Paymentflow))(Request.Form("flowsInformation"))
        Dim JSONDetailsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.Detailedcashflows))(Request.Form("detailsInformation"))
        Dim JSONTypeRequest = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.Request_RequestType))(Request.Form("InformationTypeRequest"))

        Dim IdRequest As Integer = saveProjectInformation(JSONProjectInformation)
        If IdRequest = 0 And _existRequest Then
            IdRequest = _idProject
        End If
        saveThirdsInformation(JSONThirdsInformation, IdRequest)
        saveFlowsInformation(JSONFlowsInformation, IdRequest)
        saveDetailsCashFlows(JSONDetailsInformation, IdRequest)
        saveTypeRequest(JSONTypeRequest, IdRequest)

        Dim existCession = From TypeRequest In JSONTypeRequest Where TypeRequest.IdRequestType = 4 Select TypeRequest


        If existCession.Count() > 0 Then
            updateCessionToThird(IdRequest, Request.Form("OldThird"), Request.Form("NewThird"))
        End If

        Response.Write("ok")

    End Sub

    Protected Function saveProjectInformation(ByVal JSONProjectInformation As FSC_DAO.model.Project) As Integer
        Dim objCRequest As FSC_DAO.model.CRequest = New FSC_DAO.model.CRequest()

        objCRequest.setPropertiesFromProject(JSONProjectInformation, Request.Form("other_request").ToString(), Request.Form("StartSuspensionDate").ToString(), Request.Form("EndSuspensionDate").ToString(), Convert.ToInt16(Request.Form("RestartType")), Request.Form("SettlementDate"), Request.Form("CompletitionDate"))

        If Not _existRequest Then
            objCRequest.IdProject = _idProject
            objCRequest.executeInsert()
        Else
            objCRequest.Id = _idProject
            objCRequest.executeUpdate()
        End If

        Return objCRequest.Id
    End Function

    Protected Sub saveThirdsInformation(ByVal JSONThirdsInformation As List(Of FSC_DAO.model.ThirdByProject), ByVal IdRequest As Integer)
        For Each item In JSONThirdsInformation
            Dim objCThirdByRequest As FSC_DAO.model.CThirdByRequest = New FSC_DAO.model.CThirdByRequest()
            objCThirdByRequest.IdRequest = IdRequest
            objCThirdByRequest.setPropertiesFromThirdByProject(item)

            If Not _existRequest Or item.Id = 0 Then
                objCThirdByRequest.executeInsert()
            Else
                objCThirdByRequest.Id = item.Id
                objCThirdByRequest.executeUpdate()
            End If
        Next
    End Sub

    Protected Sub saveFlowsInformation(ByVal JSONFlowsInformation As List(Of FSC_DAO.model.Paymentflow), ByVal IdRequest As Integer)
        For Each item In JSONFlowsInformation
            Dim objCPaymentFlow_Request As FSC_DAO.model.CPaymentFlow_Request = New FSC_DAO.model.CPaymentFlow_Request()
            objCPaymentFlow_Request.IdRequest = IdRequest
            objCPaymentFlow_Request.setPropertiesFromProject(item)

            If Not _existRequest Or item.id = 0 Then
                objCPaymentFlow_Request.executeInsert()
            Else
                objCPaymentFlow_Request.Id = item.id
                objCPaymentFlow_Request.executeUpdate()
            End If
        Next
    End Sub

    Protected Sub saveDetailsCashFlows(ByVal JSONFlowsInformation As List(Of FSC_DAO.model.Detailedcashflows), ByVal IdRequest As Integer)
        For Each item In JSONFlowsInformation
            Dim objCDetailsCashFlow As FSC_DAO.model.CDetailsCashFlow = New FSC_DAO.model.CDetailsCashFlow()
            objCDetailsCashFlow.IdRequest = IdRequest
            objCDetailsCashFlow.setPropertiesFromDetailedcashflows(item)

            If Not _existRequest Or item.Id = 0 Then
                objCDetailsCashFlow.executeInsert()
            Else
                objCDetailsCashFlow.Id = item.Id
                objCDetailsCashFlow.executeUpdate()
            End If
        Next
    End Sub

    Protected Sub saveTypeRequest(ByVal JSONTypeRequestInformation As List(Of FSC_DAO.model.Request_RequestType), ByVal IdRequest As Integer)

        Dim objCRequest_RequestTypeDelete As FSC_DAO.model.CRequest_RequestType = New FSC_DAO.model.CRequest_RequestType()
        objCRequest_RequestTypeDelete.IdRequest = _idProject
        objCRequest_RequestTypeDelete.executeDeleteAll()

        For Each item In JSONTypeRequestInformation
            Dim objCRequest_RequestType As FSC_DAO.model.CRequest_RequestType = New FSC_DAO.model.CRequest_RequestType()
            objCRequest_RequestType.setPropertiesFromJSON(item)
            objCRequest_RequestType.IdRequest = IdRequest
            objCRequest_RequestType.executeInsert()
        Next
    End Sub
    Protected Sub updateCessionToThird(ByVal IdRequest As Integer, ByVal OldThird As Integer, ByVal NewThird As Integer)
        Dim JSONThirdCession As FSC_DAO.model.ThirdByRequest = JsonConvert.DeserializeObject(Of FSC_DAO.model.ThirdByRequest)(Request.Form("JSONThirdCession"))

        Dim objDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objThirdByRequest = From TableThirdByRequest In objDataContext.ThirdByRequest Where TableThirdByRequest.IdThird = OldThird And TableThirdByRequest.IdRequest = IdRequest Select TableThirdByRequest

        For Each Item As FSC_DAO.model.ThirdByRequest In objThirdByRequest
            Item.IdThird = NewThird
            Item.Name = JSONThirdCession.Name
            Item.Contact = JSONThirdCession.contact
            Item.Documents = JSONThirdCession.documents
            Item.Phone = JSONThirdCession.phone
            Item.Email = JSONThirdCession.email

            objDataContext.SubmitChanges()

        Next

        Dim objDetailedCashFlowsRequest = From TableDetailedCashFlowsRequest In objDataContext.DetailedCashFlowsRequest Where TableDetailedCashFlowsRequest.IdAportante = OldThird And TableDetailedCashFlowsRequest.IdRequest = IdRequest Select TableDetailedCashFlowsRequest

        For Each ItemCashFlows As FSC_DAO.model.DetailedCashFlowsRequest In objDetailedCashFlowsRequest

            ItemCashFlows.IdAportante = NewThird
            ItemCashFlows.Aportante = JSONThirdCession.Name

            objDataContext.SubmitChanges()
        Next


    End Sub

    Protected Sub getLastContactForProjectByThird()
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()
        Dim idThirdRequest As Integer = Convert.ToInt32(Request.Form("idThird"))

        Dim objThirdByProjectJSON = From objThirdByProject In objFscDaoDataContext.ThirdByProject Where objThirdByProject.IdThird = idThirdRequest Order By objThirdByProject.Id Descending Select objThirdByProject.Contact, objThirdByProject.Documents, objThirdByProject.Email, objThirdByProject.FSCorCounterpartContribution, objThirdByProject.Id, objThirdByProject.IdThird, objThirdByProject.Name, objThirdByProject.Phone, objThirdByProject.Type

        If objThirdByProjectJSON.Count() > 0 Then
            Response.Write(JsonConvert.SerializeObject(objThirdByProjectJSON.First()))
        Else
            Response.Write("")
        End If

    End Sub

    Protected Sub ExportTerms(ByVal proyecto As String)

        Dim objRequest_ReferenceTerms As Proceeding_Request = New Proceeding_Request()
        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim DataTerms As DataTable
        Dim DataProject As DataTable
        Dim DataReq As DataTable
        Dim DataStrLin As DataTable
        Dim DataPDetail As DataTable
        Dim primero As Boolean = False
        Dim alcance As Boolean = False
        Dim suspension As Boolean = False
        Dim adicion As Boolean = False
        Dim cesion As Boolean = False
        Dim otros As Boolean = False

        Try
            proyecto = 775
            'Query inicial de otro si
            sql.AppendLine("select * from request where idproject = " & proyecto)
            DataTerms = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            'Query contrato, idea, proyecto
            sql.AppendLine("select P.id, CN.nature_name, CR.contractnumberadjusted, CR.SuscriptDate from Project P")
            sql.AppendLine("right join contractrequest CR on CR.idproject = P.id ")
            sql.AppendLine("right join contractnature CN on CN.contractnature_id = CR.IdContractNature ")
            sql.AppendLine("where P.id = " & proyecto)
            DataProject = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            'Query tipos de otrosi
            sql.AppendLine("select r.IdRequestType, rt.type from request_requesttype r  ")
            sql.AppendLine("right join RequestType rt ")
            sql.AppendLine("on r.idrequesttype = rt.id ")
            sql.AppendLine("where idrequest = " & DataTerms.Rows(0)("Id"))
            DataReq = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            'Query línea estrategica
            sql.AppendLine("select PC.Name from ProgramComponentByProject PCP ")
            sql.AppendLine("right join ProgramComponent PC on PCP.idprogramcomponent = PC.id ")
            sql.AppendLine("where PCP.IdProject = " & proyecto)
            DataStrLin = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            sql = New StringBuilder

            'Query datos de proyecto
            sql.AppendLine("select objective, zonedescription, begindate, completiondate, results, ResultsKnowledgeManagement, ResultsInstalledCapacity, OtherResults, obligationsoftheparties, RisksIdentified, RiskMitigation from Project")
            sql.AppendLine("where ID = " & proyecto)
            DataPDetail = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If DataTerms.Rows.Count > 0 Then



                'Diligenciar encabezado
                'Diligenciar parte inicial

                'Cargar lineas estrategicas
                If DataStrLin.Rows.Count > 0 Then

                    Dim lineas As New StringBuilder

                    For Each item In DataStrLin.Rows

                        If primero = False Then
                            lineas.Append(item("name"))
                            primero = True
                        Else
                            lineas.Append(" || " & item("name"))
                        End If

                    Next

                    objRequest_ReferenceTerms.strategic_line = Convert.ToString(lineas)

                End If

                primero = False

                'Cargar tipos de otro si 
                If DataReq.Rows.Count > 0 Then
                    Dim tipos As New StringBuilder

                    For Each item In DataReq.Rows

                        'Determinar modulos a diligenciar
                        Select Case item("IdRequestType")

                            Case 1
                                adicion = True

                            Case 2
                                suspension = True

                            Case 3
                                alcance = True

                            Case 4
                                cesion = True

                            Case 5
                                otros = True

                        End Select

                        If primero = False Then
                            tipos.Append(item("type"))
                            primero = True
                        Else
                            tipos.Append(", " & item("type"))
                        End If

                    Next

                    objRequest_ReferenceTerms.type_request = tipos.ToString()

                End If

                objRequest_ReferenceTerms.date_request = DataTerms.Rows(0)("createdate")
                objRequest_ReferenceTerms.number_request = DataTerms.Rows(0)("Id")

                'Traer datos de contratacion y proyecto.
                If DataProject.Rows.Count > 0 Then

                    If IsDBNull(DataProject.Rows(0)("nature_name")) = False Then
                        objRequest_ReferenceTerms.contract_nature = DataProject.Rows(0)("nature_name")
                    End If

                    If IsDBNull(DataProject.Rows(0)("contractnumberadjusted")) = False Then
                        objRequest_ReferenceTerms.contract_number = DataProject.Rows(0)("contractnumberadjusted")
                    End If

                    If IsDBNull(DataProject.Rows(0)("suscriptdate")) = False Then
                        objRequest_ReferenceTerms.subscription_year = DataProject.Rows(0)("suscriptdate")
                    End If

                End If
                'Diligenciar justificación
                If IsDBNull(DataTerms.Rows(0)("Justification")) = False Then
                    objRequest_ReferenceTerms.Justification = DataTerms.Rows(0)("Justification")
                End If

                Dim modulo As New StringBuilder

                'Diligenciar detalles 2-1 Alcance

                If alcance = True Then

                    modulo.Append("<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td>&nbsp;</td></tr><tr><td><strong><em><u>ALCANCE:</u></em></strong></td></tr><tr><td>&nbsp;</td></tr></tbody></table>")
                    modulo.Append("<table border=""1"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td colspan=""2"" rowspan=""1"" style=""text-align: center;""><strong>Información Inicial</strong></td><td style=""text-align: center;""><strong>Información a Modificar</strong></td></tr>")
                    modulo.Append("<tr><td style=""width: 20%;""><strong>Objetivo General:</strong></td><td style=""width: 30%;"">" & DataPDetail.Rows(0)("objective") & "</td><td style=""width: 30%;"">" & DataTerms.Rows(0)("objective") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Objetivos Específicos:</strong></td><td>" & DataPDetail.Rows(0)("zonedescription") & "</td><td>" & DataTerms.Rows(0)("zonedescription") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Resultados Beneficiarios:</strong></td><td>" & DataPDetail.Rows(0)("results") & "</td><td>" & DataTerms.Rows(0)("results") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Resultados Gestión Del Conocimiento:</strong></td><td>" & DataPDetail.Rows(0)("resultsknowledgemanagement") & "</td><td>" & DataTerms.Rows(0)("resultsknowledgemanagement") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Resultados Capacidad Instalada:</strong></td><td>" & DataPDetail.Rows(0)("resultsinstalledcapacity") & "</td><td>" & DataTerms.Rows(0)("resultsinstalledcapacity") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Resultados Otros: </strong></td><td>" & DataPDetail.Rows(0)("otherresults") & "</td><td>" & DataTerms.Rows(0)("otherresults") & "</td></tr>")
                    modulo.Append("<tr><td><strong>Obligaciones de las Partes:</strong></td><td>" & DataPDetail.Rows(0)("obligationsoftheparties") & "</td><td>" & DataTerms.Rows(0)("obligationsoftheparties") & "</td></tr>")
                    modulo.Append("</tbody></table>")

                    objRequest_ReferenceTerms.scope = modulo.ToString
                Else
                    objRequest_ReferenceTerms.scope = ""
                End If

                modulo = New StringBuilder

                'Diligenciar detalles 2-2 Suspension

                If suspension = True Then

                    modulo.Append("<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td>&nbsp;</td></tr><tr><td><strong><em><u>SUSPENSIÓN:</u></em></strong></td></tr><tr><td>&nbsp;</td></tr></tbody></table>")
                    modulo.Append("<table border=""1"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td colspan=""2"" style=""text-align: center;""><strong>Información Inicial</strong></td></tr><tr><td style=""width: 50%;""><strong>Fecha de Inicio:</strong></td><td>" & DataPDetail.Rows(0)("begindate") & "</td></tr><tr><td><strong>Fecha de Finalización:</strong></td><td>" & DataPDetail.Rows(0)("completiondate") & "</td></tr><tr><td><strong>Fecha de Liquidación (Proyecto Madre):</strong></td><td>Fechaliq</td></tr><tr><td colspan=""2"" style=""text-align: center;""><strong>Información a Modificar</strong></td></tr><tr><td><strong>Fecha de inicio de Suspensión:</strong></td><td>" & DataTerms.Rows(0)("startsuspension_date") & "</td></tr><tr><td><strong>Fecha fin de suspensión:</strong></td><td>" & DataTerms.Rows(0)("EndSuspension_date") & "</td></tr><tr><td><strong>Tipo de Reinicio:</strong></td><td>" & DataTerms.Rows(0)("restart_type") & "</td></tr></tbody></table>")
                    objRequest_ReferenceTerms.suspension = modulo.ToString

                Else

                    objRequest_ReferenceTerms.suspension = ""

                End If

                modulo = New StringBuilder

                'Diligenciar detalles 2-3 Adicion Prorroga Entregable

                If adicion = True Then

                End If

                modulo = New StringBuilder

                'Diligenciar detalles 2-4 Cesion

                If cesion = True Then

                End If

                modulo = New StringBuilder

                'Diligenciar detalles 2-5 Otros

                If otros = True Then
                    If IsDBNull(DataTerms.Rows(0)("other_request")) = False Then
                        modulo.Append("<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td></td></tr><tr><td><strong><em><u>OTROS</u></em></strong><strong>:</strong></td></tr><tr><td></td></tr><tr><td><strong>Modificación:</strong></td></tr><tr><td>" & DataTerms.Rows(0)("other_request") & "</td></tr></tbody></table>")
                        objRequest_ReferenceTerms.others = modulo.ToString
                    End If
                Else
                    objRequest_ReferenceTerms.others = ""
                End If

                modulo = New StringBuilder

                'Diligenciar riesgos 
                modulo.Append("<tbody><tr><td>&nbsp;</td></tr><tr><td><strong>3. IDENTIFICACIÓN DE RIESGOS</strong></td></tr><tr><td>&nbsp;</td></tr></tbody>")
                modulo.Append("<table border=""1"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td style=""text-align: center;""><strong>Riesgo identificado</strong></td><td style=""text-align: center;""><strong>Acción de mitigación</strong></td></tr><tr><td>" & DataPDetail.Rows(0)("RisksIdentified") & "</td><td>" & DataPDetail.Rows(0)("RiskMitigation") & "</td></tr></tbody></table></br>&nbsp;</br>&nbsp;</br>")
                modulo.Append("<table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%;""><tbody><tr><td><strong>*Nota:&nbsp;En</strong> la Fundación Saldarriaga Concha promovemos la cultura de racionalización en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso.</td></tr></tbody></table>")

                objRequest_ReferenceTerms.risks = modulo.ToString

                objRequest_ReferenceTerms.idProject = proyecto
                objRequest_ReferenceTerms.directorio_Actas = Server.MapPath("~")

                Dim archivo As String = objRequest_ReferenceTerms.ExportProceedingsStart

                Response.Write(archivo)

            End If

        Catch ex As Exception

            'Mostrar el error
            Session("serror") = ex.Message
            Session("sUrl") = Request.UrlReferrer.PathAndQuery
            Response.Redirect("~/errors/error.aspx")
            Response.End()

        End Try

    End Sub

End Class