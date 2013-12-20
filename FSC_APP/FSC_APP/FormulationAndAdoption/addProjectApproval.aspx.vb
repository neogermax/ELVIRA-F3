Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data

Partial Class FormulationAndAdoption_addProjectApproval
    Inherits System.Web.UI.Page

#Region "propiedades"
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Session("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Session("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadCombos()
        Me.ddlproyect.Items.Insert(0, New ListItem("Seleccione...", "-1"))
        Me.ddlproyect.SelectedValue = "-1"

    End Sub
#End Region

#Region "metodos"


    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim facade As New Facade

        Me.LoadDropDownListproject(facade, applicationCredentials)

    End Sub

    Private Sub LoadDropDownListproject(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        Me.ddlproyect.DataSource = getprojectactainicioList(applicationCredentials)
        Me.ddlproyect.DataValueField = "Id"
        Me.ddlproyect.DataTextField = "code"
        Me.ddlproyect.DataBind()

    End Sub


    Public Function getprojectactainicioList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectEntity)

        Dim sql As New StringBuilder
        Dim objproject As ProjectEntity
        Dim projectList As New List(Of ProjectEntity)
        Dim data As DataTable

        Try
            ' construir la sentencia

            sql.Append(" select id, convert(varchar(50),IdIdea) + '_' + Name + '_' + convert(varchar(50),id)  as code from  project  where Typeapproval <> 1  ")
            sql.Append(" order by (CreateDate) DESC  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objproject = New ProjectEntity

                ' cargar el valor del campo
                objproject.id = row("id")
                objproject.code = row("code")

                ' agregar a la lista
                projectList.Add(objproject)

            Next

            ' retornar el objeto
            getprojectactainicioList = projectList


        Catch ex As Exception

            Throw New Exception("Error al cargar la lista de proyecto. ")

        Finally
            ' liberando recursos
            objproject = Nothing

        End Try

    End Function


#End Region


End Class
