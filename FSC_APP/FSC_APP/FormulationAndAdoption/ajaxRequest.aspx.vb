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


End Class