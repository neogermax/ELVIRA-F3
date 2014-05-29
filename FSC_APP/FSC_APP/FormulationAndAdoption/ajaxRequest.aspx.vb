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
                Dim idProject As Integer = Convert.ToInt32(Request.Form("idProject"))
                Dim actionToResponse As String = Request.Form("action")
                'Method for action depend of the request transaction
                Select Case actionToResponse
                    Case "getInformationProject"
                        'Get the  project information relevant 
                        getInformationProject(idProject)
                        Exit Select
                    Case "loadThirdProject"
                        'Get thirs by project
                        getThirdsByProject(idProject)
                        Exit Select
                    Case "loadFlowProject"
                        'Get flows by project
                        getFlowsByProject(idProject)
                        Exit Select
                    Case "loadDetailsFlowsProject"
                        'Get flows by project
                        getDetailsFlowsByProject(idProject)
                        Exit Select
                    Case "saveInformationRerquest"
                        'Get flows by project
                        saveInformationRerquest()
                        Exit Select
                End Select
            End If

        Catch ex As Exception
            'Error Message
        End Try
    End Sub

    Protected Sub getInformationProject(ByVal idProject As Integer)
        Dim objCProject As FSC_DAO.model.CProject = New FSC_DAO.model.CProject()
        Dim objJavaScriptSerializer As JavaScriptSerializer = New JavaScriptSerializer()

        objCProject.id = idProject
        Dim objRequestObjCProject As Project = objCProject._selectProjectById()

        objRequestObjCProject.BeginDate = Convert.ToDateTime(objRequestObjCProject.BeginDate).ToShortDateString()

        'Response data for file javascript
        Response.Write(JsonConvert.SerializeObject(objRequestObjCProject))
    End Sub

    Protected Sub getThirdsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objThirds = From objThirdByProject In objFscDaoDataContext.ThirdByProject Where objThirdByProject.IdProject = idProject Select objThirdByProject

        Dim objSerializedObject = JsonConvert.SerializeObject(objThirds.ToArray())

        objSerializedObject = objSerializedObject.Replace("""", "\""")

        objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub getFlowsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objFlows = From objPaymentflow In objFscDaoDataContext.Paymentflow Where objPaymentflow.idproject = idProject Select objPaymentflow

        Dim objSerializedObject = JsonConvert.SerializeObject(objFlows.ToArray())

        objSerializedObject = objSerializedObject.Replace("""", "\""")
        objSerializedObject = objSerializedObject.Replace("\n", "\\n")
        objSerializedObject = objSerializedObject.Replace("\r", "\\r")

        objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub getDetailsFlowsByProject(ByVal idProject As Integer)
        Dim objFscDaoDataContext As FSC_DAO.model.fscdaoDataContext = New FSC_DAO.model.fscdaoDataContext()

        Dim objDetailsFlows = From objDetailedcashflows In objFscDaoDataContext.Detailedcashflows Where objDetailedcashflows.IdProject = idProject Select objDetailedcashflows

        Dim objSerializedObject = JsonConvert.SerializeObject(objDetailsFlows.ToArray())

        objSerializedObject = objSerializedObject.Replace("""", "\""")
        objSerializedObject = objSerializedObject.Replace("\n", "\\n")
        objSerializedObject = objSerializedObject.Replace("\r", "\\r")

        objSerializedObject = String.Format("{0}{1}{2}", """", objSerializedObject, """")

        Response.Write(objSerializedObject)

    End Sub

    Protected Sub saveInformationRerquest()
        Dim JSONProjectInformation = JsonConvert.DeserializeObject(Of FSC_DAO.model.Project)(Request.Form("projectInformation"))
        Dim JSONThirdsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.ThirdByProject))(Request.Form("thirdsInformation"))
        Dim JSONFlowsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.Paymentflow))(Request.Form("flowsInformation"))
        Dim JSONDetailsInformation = JsonConvert.DeserializeObject(Of List(Of FSC_DAO.model.Detailedcashflows))(Request.Form("detailsInformation"))

        Dim IdRequest As Integer = saveProjectInformation(JSONProjectInformation)
        saveThirdsInformation(JSONThirdsInformation, IdRequest)
        saveFlowsInformation(JSONFlowsInformation, IdRequest)

        Response.Write("ok")

    End Sub

    Protected Function saveProjectInformation(ByVal JSONProjectInformation As FSC_DAO.model.Project) As Integer
        Dim objCRequest As FSC_DAO.model.CRequest = New FSC_DAO.model.CRequest()

        objCRequest.setPropertiesFromProject(JSONProjectInformation)
        objCRequest.executeInsert()

        Return objCRequest.Id
    End Function

    Protected Sub saveThirdsInformation(ByVal JSONThirdsInformation As List(Of FSC_DAO.model.ThirdByProject), ByVal IdRequest As Integer)
        For Each item In JSONThirdsInformation
            Dim objCThirdByRequest As FSC_DAO.model.CThirdByRequest = New FSC_DAO.model.CThirdByRequest()
            objCThirdByRequest.IdRequest = IdRequest
            objCThirdByRequest.setPropertiesFromThirdByProject(item)
            objCThirdByRequest.executeInsert()
        Next
    End Sub

    Protected Sub saveFlowsInformation(ByVal JSONFlowsInformation As List(Of FSC_DAO.model.Paymentflow), ByVal IdRequest As Integer)
        For Each item In JSONFlowsInformation
            Dim objCPaymentFlow_Request As FSC_DAO.model.CPaymentFlow_Request = New FSC_DAO.model.CPaymentFlow_Request()
            objCPaymentFlow_Request.IdRequest = IdRequest
            objCPaymentFlow_Request.setPropertiesFromProject(item)
            objCPaymentFlow_Request.executeInsert()
        Next

    End Sub

End Class