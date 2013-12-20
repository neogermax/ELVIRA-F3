Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO


Partial Class actas_acta_star
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
            Me.warning.Visible = False
            Dim idproject As Integer = Request.QueryString("id")
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
            Dim facade As New Facade
            If validaracta() = 1 Then
                Exit Sub
            Else
                buscardatos()
            End If

        End If

    End Sub

    Protected Sub Btnexport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexport.Click

        Dim objProceedings As Proceedings = New Proceedings()
        Dim idproject As Integer = Request.QueryString("cid")
        Me.warning.Visible = False

        objProceedings.Idproject = idproject                                                    'enviamos el id de projecto seleccionado

        objProceedings.DirectorioActas = Server.MapPath("~")
        objProceedings.OperatorName = Me.Txtnameoperator.Text                                  'enviamos el nombre del operador
        objProceedings.NumberAgreementContract = Me.Txtnumbercontract.Text                     'enviamos el numero del contrato
        objProceedings.ObjectOfTheAgreementContract = Me.Txtobjectcontract.Text                'enviamos el objetivo del contrato 
        objProceedings.TitleName = Me.Txttitledinamic.Text
        objProceedings.AccompanimentProfessionalNameInitiative = Me.Txtnameprofiniciative.Text 'enviamos el nombre del acompañante
        objProceedings.SupervisorName = Me.Txtnamesupervisor.Text                              'enviamos el nombre del supervisor
        objProceedings.ContractAgreementValue = Me.Txtvaluescontract.Text
        objProceedings.InLyrics = Me.Txtvaluesletters.Text                                     'enviamos el valor del contrato en letras
        objProceedings.aportvaluesFSC = Me.TxtaportFSCefect.Text
        objProceedings.aportespecieFSC = Me.TxtaporFSCesp.Text
        objProceedings.aporvaluesother = Me.Txtaportotrosefect.Text
        objProceedings.aportespecieother = Me.Txtaportotrosesp.Text
        objProceedings.suscripdateofContractAgreement = Me.Txtfechasuscript.Text
        objProceedings.StartDateOfContractAgreement = Me.Txtdatestartcontract.Text
        objProceedings.CompletionDateContractAgreement = Me.Txtdateendcontract.Text
        objProceedings.DurationOfContract = Me.Txtdurationcontract.Text                        'enviamos la duracion del contrato
        objProceedings.InformationOnTheLegal = Me.Txtsoportsjuridique.Text                     'enviamos el soporte juridico
        objProceedings.aseguradoraname = Me.Txtaseguradora.Text
        objProceedings.PolicyDetailsList = Me.HFtabladat.Value                                 'enviamos la lista de polizas aliadas al contrato
        objProceedings.OperatingPartnerContractAgreement = Me.Txtejecucion.Text
        objProceedings.Comments = Me.Txtobservaciones.Text                                     'enviamos las observaciones
        objProceedings.cargo1 = Me.TextBox1.Text
        objProceedings.cargo2 = Me.TextBox3.Text
        objProceedings.cargo3 = Me.TextBox5.Text
        objProceedings.cargo4 = Me.TextBox7.Text
        objProceedings.cargo5 = Me.TextBox9.Text
        objProceedings.cargo6 = Me.TextBox11.Text

        objProceedings.name1 = Me.TextBox2.Text
        objProceedings.name2 = Me.TextBox4.Text
        objProceedings.name3 = Me.TextBox6.Text
        objProceedings.name4 = Me.TextBox8.Text
        objProceedings.name5 = Me.TextBox10.Text
        objProceedings.name6 = Me.TextBox12.Text
        objProceedings.dateend = Me.txtFinishDate.Text


        objProceedings.dinamicnameSO = Me.ddlTypeThird.SelectedValue
        objProceedings.dinamicnumberCC = Me.ddlTypeoF.SelectedValue

        objProceedings.dinamictp = "Información general del " & objProceedings.dinamicnumberCC
        objProceedings.dinamicobjectCC = "Objeto del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicsuperviser = "Nombre del Supervisor(a) del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicvalues = "Valor del contrato en " & objProceedings.dinamicnumberCC & " $:"
        objProceedings.dinamicaportefect = "Aporte del " & objProceedings.dinamicnameSO & " en Efectivo:"
        objProceedings.dinamicaportefespe = "Aporte del " & objProceedings.dinamicnameSO & " en Especie:"
        objProceedings.dinamicdatesuscrip = "Fecha de suscripción del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicduration = "Duración del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicdatestart = "Fecha de Inicio del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicdateend = "Fecha de Terminación del " & objProceedings.dinamicnumberCC & ":"
        objProceedings.dinamicoperating = "Información de la ejecución del " & objProceedings.dinamicnumberCC

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim datapoliza As DataTable

        sql = New StringBuilder

        sql.Append("select p.numero_poliza, pd.Concepto, p.fecha_exp,p.fecha_ven from contractrequest cr   ")
        sql.Append("inner join poliza p on p.id = cr.PolizaId  ")
        sql.Append("inner join PolizaDetails pd on pd.Id_Poliza = p.id   ")
        sql.Append("where idproject = " & idproject)

        ' ejecutar la intruccion
        datapoliza = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)
        Dim stringhtml, t1, t2, t3, t4 As String

        Dim conta As Integer = 0
        Dim arrayfechas As String()

        Dim datosd = Me.HFdatetrim.Value
        If datosd <> "" Then

            datosd = Left(datosd, Len(datosd) - 1)
            datosd = Replace(datosd, "__", "_", 1)
            arrayfechas = datosd.Split(New [Char]() {"_"c})
        Else
            datosd = "vacio"
            arrayfechas = datosd.Split(New [Char]() {"_"c})
        End If

        If datapoliza.Rows.Count > 0 Then

            For Each itemDataTable As DataRow In datapoliza.Rows

                If IsDBNull(datapoliza.Rows(conta)("numero_poliza")) = False Then
                    t1 = datapoliza.Rows(conta)("numero_poliza")
                End If
                If IsDBNull(datapoliza.Rows(conta)("Concepto")) = False Then
                    t2 = datapoliza.Rows(conta)("Concepto")
                End If
                If IsDBNull(datapoliza.Rows(conta)("fecha_exp")) = False Then
                    t3 = datapoliza.Rows(conta)("fecha_exp")
                End If

                If arrayfechas(0) = "vacio" Then
                    Me.warning.Visible = True
                    Me.Lblwarning.Text = "Las fechas de las polizas tienen que ser diligenciadas!"
                    Exit Sub
                Else
                    t4 = arrayfechas(conta)
                End If

                stringhtml &= " <tr align=""center""><td style=""border: solid 1px #000000; "">" & t1 & "</td><td style=""border: solid 1px #000000; "">" & t2 & "</td><td colspan=""2"" style=""border: solid 1px #000000; "">" & t3 & "</td><td colspan=""2"" style=""border: solid 1px #000000; "">" & t4 & "</td></tr>  "

                conta = conta + 1
            Next
        Else
            stringhtml = ""
        End If

        objProceedings.PolicyDetailsList = stringhtml

        Dim nameexport = objProceedings.ExportProceedingsStart()
        savelog(nameexport)
        Response.Redirect(nameexport)

    End Sub

    Protected Sub Btnexportvalidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexportvalidate.Click

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim proceeding_logs_start As proceeding_logsEntity = New proceeding_logsEntity()
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()

        Dim sql As New StringBuilder
        Dim dataexport As DataTable
        Dim idproject As Integer = Request.QueryString("cid")
        Dim exportacta As String = ""

        sql.Append("select FileName from Proceeding_Logs where  tipo_Acta_id = 1 and  Project_Id = " & idproject)
        dataexport = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If IsDBNull(dataexport.Rows(0)("FileName")) = False Then
            exportacta = dataexport.Rows(0)("FileName")
        End If

        proceeding_logs_start.project_id = idproject
        proceeding_logs_start.Tipo_acta_id = 1
        proceeding_logs_start.iduser = applicationCredentials.UserID
        proceeding_logs_start.createdate = Now
        proceeding_logs_start.file_name = exportacta

        objProceedingsadd.add(applicationCredentials, proceeding_logs_start)

        Response.Redirect(exportacta)

    End Sub

#End Region

#Region "metodos"

    Private Sub savelog(ByVal NAME As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim proceeding_logs_start As proceeding_logsEntity = New proceeding_logsEntity()
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()
        Dim idproject As Integer = Request.QueryString("cid")

        proceeding_logs_start.project_id = idproject
        proceeding_logs_start.Tipo_acta_id = 1
        proceeding_logs_start.iduser = applicationCredentials.UserID
        proceeding_logs_start.createdate = Now
        proceeding_logs_start.file_name = NAME

        objProceedingsadd.add(applicationCredentials, proceeding_logs_start)

    End Sub
    Private Function validaracta() As Integer

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idproject As Integer = Request.QueryString("cid")
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim validecreate As Integer
        Dim name As String = ""

        validecreate = 0

        sql.AppendLine("select project_id from Proceeding_Logs where Project_Id = " & idproject & " and  Tipo_Acta_id = 1 ")
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Me.tablevalide.Visible = True
            Me.Acta_Inicio.Visible = False
            sql = New StringBuilder
            sql.Append("select name from project where id= " & idproject)
            Dim nameproyect = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If IsDBNull(nameproyect.Rows(0)("name")) = False Then
                name = nameproyect.Rows(0)("name")
            End If

            Me.Lblinformationvalide.Text = " El acta de inicio del proyecto " & idproject & "_" & name & " ya fue generada; si desea consultarla, haga click en el siguiente botón"

            validecreate = 1
        End If

        Return validecreate

    End Function



    Public Sub buscardatos()
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim datacontrato As DataTable
        Dim dataproyecto As DataTable
        Dim datapoliza As DataTable
        Dim asegura As DataTable
        Dim idproject As Integer = Request.QueryString("cid")


        Try

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
                Me.Txtnameoperator.Text = objResult
            End If

            sql = New StringBuilder
            sql.Append("select ContractNumberAdjusted, SuscriptDate, StartDate, MonthDuration from ContractRequest where IdProject = " & idproject)

            ' ejecutar la intruccion
            datacontrato = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If datacontrato.Rows.Count > 0 Then

                'no de contrato
                If IsDBNull(datacontrato.Rows(0)("ContractNumberAdjusted")) = False Then
                    Me.Txtnumbercontract.Text = datacontrato.Rows(0)("ContractNumberAdjusted")
                End If
                'fecha de suscripcion
                If IsDBNull(datacontrato.Rows(0)("SuscriptDate")) = False Then
                    Dim fesuscrip = datacontrato.Rows(0)("SuscriptDate")
                    Me.Txtfechasuscript.Text = Format(fesuscrip, "yyyy/MM/dd")
                End If
                'fecha de inicio
                If IsDBNull(datacontrato.Rows(0)("StartDate")) = False Then
                    Dim stard = datacontrato.Rows(0)("StartDate")
                    Me.Txtdatestartcontract.Text = Format(stard, "yyyy/MM/dd")
                End If
                'duracion
                If IsDBNull(datacontrato.Rows(0)("MonthDuration")) = False Then
                    Me.Txtdurationcontract.Text = datacontrato.Rows(0)("MonthDuration")
                End If
            End If

            sql = New StringBuilder
            sql.Append("select Objective,TotalCost from Project where Id = " & idproject)

            ' ejecutar la intruccion
            dataproyecto = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If dataproyecto.Rows.Count > 0 Then
                Dim datosformat As String
                'objetive
                If IsDBNull(dataproyecto.Rows(0)("Objective")) = False Then
                    Me.Txtobjectcontract.Text = dataproyecto.Rows(0)("Objective")
                End If
                'total
                If IsDBNull(dataproyecto.Rows(0)("TotalCost")) = False Then
                    datosformat = dataproyecto.Rows(0)("TotalCost")
                    Me.Txtvaluescontract.Text = Format(Convert.ToInt32(datosformat), "#,###.##")
                End If

            End If

            sql = New StringBuilder

            sql.Append("select p.numero_poliza, pd.Concepto, p.fecha_exp,p.fecha_ven from contractrequest cr   ")
            sql.Append("inner join poliza p on p.id = cr.PolizaId  ")
            sql.Append("inner join PolizaDetails pd on pd.Id_Poliza = p.id   ")
            sql.Append("where idproject = " & idproject)

            ' ejecutar la intruccion
            datapoliza = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)
            Dim stringhtml, t1, t2, t3 As String
            Dim conta As Integer = 0

            Me.HFcontdinamic.Value = datapoliza.Rows.Count

            If datapoliza.Rows.Count > 0 Then


                For Each itemDataTable As DataRow In datapoliza.Rows

                    Dim idname As String = "txtfechafin"

                    If IsDBNull(datapoliza.Rows(conta)("numero_poliza")) = False Then
                        t1 = datapoliza.Rows(conta)("numero_poliza")
                    End If
                    If IsDBNull(datapoliza.Rows(conta)("Concepto")) = False Then
                        t2 = datapoliza.Rows(conta)("Concepto")
                    End If
                    If IsDBNull(datapoliza.Rows(conta)("fecha_exp")) = False Then
                        t3 = datapoliza.Rows(conta)("fecha_exp")
                    End If

                    idname = idname & conta

                    stringhtml &= " <tr align=""center""><td style=""border: solid 1px #000000; "">" & t1 & "</td><td style=""border: solid 1px #000000; "">" & t2 & "</td><td colspan=""2"" style=""border: solid 1px #000000; "">" & t3 & "</td><td colspan=""2"" style=""border: solid 1px #000000; ""><input type=""date"" size=""50"" class=""validarfechas"" id=""" & idname & """  name=""" & idname & """></td></tr>  "

                    conta = conta + 1
                Next
            Else
                stringhtml = ""
            End If

            Me.tablapoliza.InnerHtml = stringhtml

            sql = New StringBuilder

            sql.Append("select p.aseguradora  from contractrequest cr inner join poliza p on p.id = cr.PolizaId  where idproject = " & idproject)
            asegura = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If asegura.Rows.Count > 0 Then
                If IsDBNull(asegura.Rows(0)("aseguradora")) = False Then
                    Me.Txtaseguradora.Text = asegura.Rows(0)("aseguradora")
                End If

            End If


        Catch ex As Exception

        End Try

    End Sub


#End Region


End Class
