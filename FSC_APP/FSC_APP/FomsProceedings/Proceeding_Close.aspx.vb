Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO
Imports PostMail

Partial Class FomsProceedings_Proceeding_Close
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

        If Not Page.IsPostBack Then

            ' obtener los parametos
            Me.tablevalide.Visible = False
            Dim idproject As Integer = Request.QueryString("id")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim facade As New Facade
            'If validarpreactas() = 1 Then
            '    Exit Sub
            'End If

            If validaracta() = 1 Then
                Exit Sub
            Else
                buscardatos()
                Session("validateurlcompromise") = "T"

            End If

        End If

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim objproceeding As Proceedings = New Proceedings()

        'Cargar propiedades del objeto
        objproceeding.TypeThird = Me.ddlTypeThird.Text
        objproceeding.CloseTypeoF = Me.ddlTypeoF.Text
        objproceeding.ThirdName = Me.txtThirdName.Text
        objproceeding.ContractNumberClose = Me.txtContractNumber.Text
        objproceeding.InitialDate = Me.txtInitialDate.Text
        objproceeding.FinalDate = Me.txtfinalDate.Text
        objproceeding.ContractObjectClose = Me.taContractObject.InnerText
        objproceeding.InitialValue = Me.txtInitialValue.Text
        objproceeding.LettersClose = Me.txtLetters.Text

        If Me.rbAdition1.Checked = True Then
            objproceeding.Adition = "SI"
        Else
            objproceeding.Adition = "NO"
        End If

        objproceeding.NumberAdition = Me.txtNumberAdition.Text
        objproceeding.InLetters = Me.txtInLetters.Text
        objproceeding.AdditionDateClose = Me.txtAdditionDate.Text
        objproceeding.AditionValue = Me.txtAditionValue.Text
        objproceeding.VigencyExtend = Me.txtVigencyExtend.Text
        objproceeding.ContractFinalValueClose = Me.txtContractFinalValue.Text
        objproceeding.ContractFinalValueInLetters = Me.txtContractFinalValueInLetters.Text
        objproceeding.Fullfillment = Me.taFullfillment.InnerText
        objproceeding.Observations = Me.taObservations.InnerText
        objproceeding.Weakness = Me.taWeakness.InnerText
        objproceeding.Oportunities = Me.taOportunities.InnerText
        objproceeding.Strongest = Me.taStrongest.InnerText
        objproceeding.Learnings = Me.taLearnings.InnerText
        objproceeding.FinishDate = Me.txtFinishDate.Text
        objproceeding.PartnerName1 = Me.txtPartnerName1.Text
        objproceeding.FSCName1 = Me.txtFSCName1.Text
        objproceeding.PartnerName2 = Me.txtPartnerName2.Text
        objproceeding.FSCName2 = Me.txtFSCName2.Text
        objproceeding.PartnerName3 = Me.txtPartnerName3.Text
        objproceeding.FSCName3 = Me.txtFSCName3.Text

        'Mapear labels
        objproceeding.lblDates = "Fechas del " & objproceeding.CloseTypeoF & ":"
        objproceeding.lblObjectContract = "Objeto del " & objproceeding.CloseTypeoF & ":"
        objproceeding.lblContractAditions = "Adiciones al " & objproceeding.CloseTypeoF & ":"
        objproceeding.lblContractInitialValue = "Valor Inicial del " & objproceeding.CloseTypeoF
        objproceeding.lblFinalValue = "Valor Final del " & objproceeding.CloseTypeoF
        objproceeding.lblWeakness = Me.lblWeakness.Text
        objproceeding.lblOportunity = Me.lblOportunity.Text
        objproceeding.lblStrong = Me.lblStrong.Text
        objproceeding.lblLearning = Me.lblLearning.Text
        objproceeding.lblOperator = objproceeding.TypeThird

        objproceeding.Idproject = Request.QueryString("cid")
        objproceeding.DirectorioActas = Server.MapPath("~")

        'Generar archivo a exportar
        Dim nameexport = objproceeding.StartExportClose()
        savelog(nameexport)
        Response.Redirect(nameexport)

    End Sub

    Protected Sub Btnexportvalidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexportvalidate.Click

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim proceeding_logs_close As proceeding_logsEntity = New proceeding_logsEntity()
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()

        Dim sql As New StringBuilder
        Dim dataexport As DataTable
        Dim idproject As Integer = Request.QueryString("cid")
        Dim exportacta As String = ""

        sql.Append("select FileName from Proceeding_Logs where  tipo_Acta_id = 3 and  Project_Id = " & idproject)
        dataexport = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If IsDBNull(dataexport.Rows(0)("FileName")) = False Then
            exportacta = dataexport.Rows(0)("FileName")
        End If

        proceeding_logs_close.project_id = idproject
        proceeding_logs_close.Tipo_acta_id = 3
        proceeding_logs_close.iduser = applicationCredentials.UserID
        proceeding_logs_close.createdate = Now
        proceeding_logs_close.file_name = exportacta

        objProceedingsadd.add(applicationCredentials, proceeding_logs_close)

        Response.Redirect(exportacta)

    End Sub
   
#End Region

#Region "metodos"

    Private Sub savelog(ByVal NAME As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim proceeding_logs_close As proceeding_logsEntity = New proceeding_logsEntity()
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()
        Dim idproject As Integer = Request.QueryString("cid")

        proceeding_logs_close.project_id = idproject
        proceeding_logs_close.Tipo_acta_id = 3
        proceeding_logs_close.iduser = applicationCredentials.UserID
        proceeding_logs_close.createdate = Now
        proceeding_logs_close.file_name = NAME

        objProceedingsadd.add(applicationCredentials, proceeding_logs_close)

    End Sub

    Private Function validaracta() As Integer

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idproject As Integer = Request.QueryString("cid")
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim validecreate As Integer
        Dim name As String = ""

        validecreate = 0

        Session("proyectcomrpomise") = idproject

        sql.AppendLine("select project_id from Proceeding_Logs where Project_Id = " & idproject & " and tipo_Acta_id = 3 ")
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Me.tablevalide.Visible = True
            Me.Acta_cierre.Visible = False
            sql = New StringBuilder
            sql.Append("select name from project where id= " & idproject)
            Dim nameproyect = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If IsDBNull(nameproyect.Rows(0)("name")) = False Then
                name = nameproyect.Rows(0)("name")
            End If

            Me.Lblinformationvalide.Text = " El acta de cierre del proyecto " & idproject & "_" & name & " ya fue generada; si desea consultarla, haga click en el siguiente botón"

            validecreate = 1
        End If

        Return validecreate

    End Function

    Private Function validarpreactas() As Integer

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idproject As Integer = Request.QueryString("cid")
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim data2 As DataTable
        Dim validepreactas As Integer
        Dim name As String = ""

        validepreactas = 0
        Dim VALIDEINICIO = 0

        sql.Append("select name from project where id= " & idproject)
        Dim nameproyect = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If IsDBNull(nameproyect.Rows(0)("name")) = False Then
            name = nameproyect.Rows(0)("name")
        End If

        sql = New StringBuilder

        sql.AppendLine("select project_id from Proceeding_Logs where Project_Id = " & idproject & " and Acta_id = 1")
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count = 0 Then

            Me.tablevalide.Visible = True
            Me.Acta_cierre.Visible = False
            Me.Btnexportvalidate.Visible = False

            Me.Lblinformationvalide.Text = " El proyecto " & idproject & "_" & name & " no se puede crear el acta de cierre, porque no se le ha creado un acta de inicio"
            VALIDEINICIO = 1
            validepreactas = 1
        End If

        If VALIDEINICIO = 0 Then

            sql = New StringBuilder

            sql.AppendLine("select project_id from Proceeding_Logs where Project_Id = " & idproject & " and Acta_id = 2")
            data2 = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If data2.Rows.Count = 0 Then

                Me.tablevalide.Visible = True
                Me.Acta_cierre.Visible = False
                Me.Btnexportvalidate.Visible = False

                Me.Lblinformationvalide.Text = " El proyecto " & idproject & "_" & name & " no se puede crear el acta de cierre, porque no se le ha creado un acta de seguimiento"
                validepreactas = 1
            End If

        End If

        Return validepreactas

    End Function

    Public Sub buscardatos()

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim datacontrato As DataTable
        Dim dataproyecto As DataTable

        Dim idproject As Integer = Request.QueryString("cid")

        Try
            'query que traer los actores que van aliados al proyecto
            sql.Append("select t.Name from ThirdByProject tp       ")
            sql.Append("inner join Project p on  p.id = tp.IdProject         ")
            sql.Append("inner join Third t on t.Id= tp.IdThird     ")
            sql.Append("where p.id = " & idproject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


            If data.Rows.Count > 0 Then

                Dim objResult As String = ""

                For Each itemDataTable As DataRow In data.Rows

                    objResult &= itemDataTable(0) & "-"

                Next
                Me.txtThirdName.Text = objResult
            End If

            sql = New StringBuilder
            'query que traer el numero de contrato del proyecto seleccionado
            sql.Append("select ContractNumberAdjusted from ContractRequest where IdProject = " & idproject)

            ' ejecutar la intruccion
            datacontrato = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If datacontrato.Rows.Count > 0 Then

                'no de contrato
                If IsDBNull(datacontrato.Rows(0)("ContractNumberAdjusted")) = False Then
                    Me.txtContractNumber.Text = datacontrato.Rows(0)("ContractNumberAdjusted")
                End If

            End If

            sql = New StringBuilder
            sql.Append("select Objective, TotalCost from Project where Id = " & idproject)

            ' ejecutar la intruccion
            dataproyecto = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If dataproyecto.Rows.Count > 0 Then
                Dim datosformat As String
                'objetive
                If IsDBNull(dataproyecto.Rows(0)("Objective")) = False Then
                    Me.taContractObject.Value = dataproyecto.Rows(0)("Objective")
                End If
                'total
                If IsDBNull(dataproyecto.Rows(0)("TotalCost")) = False Then
                    datosformat = dataproyecto.Rows(0)("TotalCost")
                    Me.txtInitialValue.Text = Format(Convert.ToInt32(datosformat), "#,###.##")
                End If
            End If

        Catch ex As Exception

        End Try


    End Sub

#End Region


  
   
   
End Class
