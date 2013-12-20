Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class FSCServices
    Inherits System.Web.Services.WebService

    ' contantes
    Const MODULENAME As String = "FSCServices"

    <WebMethod()> _
    Public Function getProject() As String

        ' definir los obejtos
        Dim xml As String = String.Empty
        Dim objApplicationCredentials As ApplicationCredentials = PublicFunction.buildApplicationCredentials()
        Dim report As New ReportFacade
        Dim dt As DataTable

        Try

            ' cargar la lista de proyectos
            xml = report.exportList(objApplicationCredentials)

        Catch ex As Exception

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getProject")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al insertar el Destinatario." & ex.Message)

        End Try

        ' retornar la lista de proyectos
        getProject = xml

    End Function

End Class
