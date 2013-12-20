Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO

Partial Class Report_Engagement_ReportContractRequestactas
    Inherits System.Web.UI.Page

#Region "Eventos"

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

        'Se crea la variable de session que almacena la lista de logs proccedings
        Session("proceedingList") = New List(Of proceeding_logsEntity)

        ' cargar el titulo
        Session("lblTitle") = "Reportes Actas Proyectos"

    End Sub

    Protected Sub btnviewsactas_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnviewsactas.Click

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim data2 As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)



        Dim proceedingList As List(Of proceeding_logsEntity) = New List(Of proceeding_logsEntity)()

        ' cargarla de la session
        proceedingList = DirectCast(Session("proceedingList"), List(Of proceeding_logsEntity))

        Dim idproyect As Integer
        'consulta de los datos de actores por id

        idproyect = Me.HDreportact.Value

        ' ejecutar la intruccion
        sql.Append("select  pl.FileName as ruta, ta.ActaName, REPLACE(pl.FileName,'/Proceedings/','') as FileName,pl.Create_Date,au.Name,Acta_id from Proceeding_Logs pl  ")
        sql.Append("INNER JOIN TypeActas ta on ta.id = Tipo_Acta_id                            ")
        sql.Append("INNER JOIN FSC_eSecurity.dbo.ApplicationUser au on pl.User_id =au.ID  ")
        sql.Append("where pl.Project_Id = " & idproyect & " and Tipo_Acta_id = 2")
        sql.Append("group by pl.FileName,ta.ActaName,pl.Create_Date,au.Name,Acta_id   ")
        sql.Append("ORDER BY pl.Create_Date DESC  ")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        If data.Rows.Count > 0 Then

            For Each row As DataRow In data.Rows

                Dim objproceeding As proceeding_logsEntity = New proceeding_logsEntity()

                If IsDBNull(row("ruta")) = False Then
                    objproceeding.name_ruta = row("ruta")
                End If

                If IsDBNull(row("Acta_id")) = False Then
                    objproceeding.acta_id = row("Acta_id")
                End If

                If IsDBNull(row("ActaName")) = False Then
                    objproceeding.name_acta = row("ActaName")
                End If

                If IsDBNull(row("filename")) = False Then
                    objproceeding.file_name = row("filename")
                End If

                If IsDBNull(row("Create_Date")) = False Then
                    objproceeding.createdate = row("Create_Date")
                End If

                If IsDBNull(row("Name")) = False Then
                    objproceeding.name_user = row("Name")
                End If
                ' agregarlos
                proceedingList.Add(objproceeding)
            Next

        End If


        sql = New StringBuilder

        sql.Append("select  pl.FileName as ruta, ta.ActaName, REPLACE(pl.FileName,'/Proceedings/','') as FileName,pl.Create_Date,au.Name,Acta_id from Proceeding_Logs pl  ")
        sql.Append("INNER JOIN TypeActas ta on ta.id = Tipo_Acta_id                            ")
        sql.Append("INNER JOIN FSC_eSecurity.dbo.ApplicationUser au on pl.User_id =au.ID  ")
        sql.Append("where pl.Project_Id = " & idproyect & " and Acta_id = 1  and Tipo_Acta_id <> 2")
        sql.Append("group by pl.FileName,ta.ActaName,pl.Create_Date,au.Name,Acta_id   ")
        sql.Append("ORDER BY pl.Create_Date DESC  ")

        ' ejecutar la intruccion
        data2 = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)



        If data2.Rows.Count > 0 Then

            For Each row As DataRow In data2.Rows

                Dim objproceeding As proceeding_logsEntity = New proceeding_logsEntity()

                If IsDBNull(row("ruta")) = False Then
                    objproceeding.name_ruta = row("ruta")
                End If

                If IsDBNull(row("Acta_id")) = False Then
                    objproceeding.acta_id = row("Acta_id")
                End If

                If IsDBNull(row("ActaName")) = False Then
                    objproceeding.name_acta = row("ActaName")
                End If

                If IsDBNull(row("filename")) = False Then
                    objproceeding.file_name = row("filename")
                End If

                If IsDBNull(row("Create_Date")) = False Then
                    objproceeding.createdate = row("Create_Date")
                End If

                If IsDBNull(row("Name")) = False Then
                    objproceeding.name_user = row("Name")
                End If
                ' agregarlos
                proceedingList.Add(objproceeding)

            Next

        End If


        Dim objDataTable As DataTable = New DataTable()

        objDataTable.Columns.Add("ruta")
        objDataTable.Columns.Add("Actaname")
        objDataTable.Columns.Add("id_acta")
        objDataTable.Columns.Add("FileName")
        objDataTable.Columns.Add("Create_Date")
        objDataTable.Columns.Add("Name")

        For Each itemDataTable As proceeding_logsEntity In proceedingList
            objDataTable.Rows.Add(itemDataTable.name_ruta, itemDataTable.name_acta, itemDataTable.acta_id, itemDataTable.file_name, itemDataTable.createdate, itemDataTable.name_user)
        Next

        If objDataTable.Rows.Count > 0 Then

            Me.containerSuccess.Visible = "false"

            Me.gvactas.DataSource = objDataTable
            Me.gvactas.DataBind()

        Else

            Me.gvactas.DataSource = ""
            Me.gvactas.DataBind()

            Me.lblsaveinformation.Text = "Este proyecto no tiene actas generadas hasta el momento.  "
            Me.containerSuccess.Visible = "True"

        End If


    End Sub

#End Region

#Region "Métodos"

    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim facade As New Facade

        Me.LoadDropDownListproject(facade, applicationCredentials)

    End Sub

    Private Sub LoadDropDownListproject(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        Me.ddlproyect.DataSource = getprojectactainicioList(applicationCredentials)
        Me.ddlproyect.DataValueField = "Id"
        Me.ddlproyect.DataTextField = "Name"
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

            sql.Append("select distinct p.id, p.name as nombre, CONVERT(varchar, p.id) + '_'  + p.Name as name  from Project p ")
            sql.Append("inner join ContractRequest cr on cr.IdProject = p.id where  cr.SignedContract ='True'  ")
            sql.Append("order by p.id desc , nombre,name   ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objproject = New ProjectEntity

                ' cargar el valor del campo
                objproject.id = row("id")
                objproject.name = row("name")

                ' agregar a la lista
                projectList.Add(objproject)

            Next

            ' retornar el objeto
            getprojectactainicioList = projectList


        Catch ex As Exception

            Throw New Exception("Error al cargar la lista de Proyectos. " & ex.Message)

        Finally
            ' liberando recursos
            objproject = Nothing

        End Try

    End Function

    Protected Sub guardarid(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.HDreportact.Value = Me.ddlproyect.SelectedValue
    End Sub

#End Region

  

   
End Class
