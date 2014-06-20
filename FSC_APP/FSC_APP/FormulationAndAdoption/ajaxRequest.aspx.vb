Imports System.Web.Script.Serialization
Imports FSC_DAO.model
Imports System.Linq
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
End Class