'TODO:15 SE CREA FORMULARIO DE TRANSACCION CON EL SERVIDOR PARA APROBACION  DE PROYECTOS
'4/06/14 GERMAN RODRIGUEZ

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class AjaxaddProjectApproval
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim S_id_project, S_id_idea, S_date, S_N_acta, S_aport_others, S_aport_fsc, S_aport_total, S_approval As String
        Dim id_b As Integer
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        If Request.Form("action") <> Nothing Then

            Dim option_proyecto = Request.Form("action")

            Select option_proyecto

                Case "save"

                    S_id_project = Request.Form("id_project").ToString()
                    S_id_idea = Request.Form("id_idea").ToString()
                    S_date = Request.Form("date").ToString()
                    S_N_acta = Request.Form("N_acta").ToString
                    S_aport_others = Request.Form("aport_others").ToString
                    S_aport_fsc = Request.Form("aport_fsc").ToString
                    S_aport_total = Request.Form("aport_total").ToString
                    S_approval = Request.Form("approval").ToString

                    CREATE_Approval_Project(S_id_project, S_id_idea, S_date, S_N_acta, S_aport_others, S_aport_fsc, S_aport_total, S_approval)
            End Select

        Else

            'trae el jquery para hacer todo por debajo del servidor
            action = Request.QueryString("action").ToString()

            Select Case action

                Case "charge_proyect_approval"
                    charge_project_approval()

                Case "buscar"
                    'convierte la variable y llama funcion para la validacion de la idea
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    buscar_date_project(id_b)

                Case "buscar_actores"

                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    buscar_date_third_project(id_b)

                Case "validar_modulo_completo"
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    validar_date_project(id_b)

                Case Else


            End Select

        End If

    End Sub

    Protected Function CREATE_Approval_Project(ByVal id_project As String, ByVal id_idea As String, ByVal date_approval As String, ByVal N_acta As String, ByVal aport_others As String, ByVal aport_fsc As String, ByVal aport_total As String, ByVal approval As String)

        Dim facade As New Facade
        Dim objProjectApproval As New ProjectApprovalRecordEntity

        Dim ApplicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim Id_idea_v = id_idea.Split(New [Char]() {"_"c})

        'id del proyecto
        objProjectApproval.idproject = id_project
        'fecha de aprobacion
        objProjectApproval.approvaldate = date_approval
        'numero del acto
        objProjectApproval.actnumber = N_acta
        'aportes de otros
        objProjectApproval.aportOtros = aport_others
        'aportes de la FSC
        objProjectApproval.aportFSC = aport_fsc
        'total aporte
        objProjectApproval.approvedvalue = aport_total
        'comite de aprobacion
        objProjectApproval.approved = approval
        'usuario registrado
        objProjectApproval.iduser = ApplicationCredentials.UserID
        'fecha del sistema
        objProjectApproval.createdate = Now

        'datos quemados
        objProjectApproval.code = ""
        objProjectApproval.codeapprovedidea = ""
        objProjectApproval.Ididea = Id_idea_v(0)
        objProjectApproval.comments = ""
        objProjectApproval.enabled = 1
        objProjectApproval.attachment = ""

        facade.addProjectApprovalRecord(ApplicationCredentials, objProjectApproval)

        update_project_approval(id_project)

        Dim Result As String

        If objProjectApproval.idproject <> 0 Then

            Result = "El Proyecto se aprobó exitosamente!"
            Response.Write(Result)

        Else

            Result = "Se perdio la conexion al guardar los datos de la aprobación"
            Response.Write(Result)

        End If


    End Function

    Protected Function update_project_approval(ByVal idproject As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
     
        sql.Append(" update project set Typeapproval = 1 where id = " & idproject)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

    End Function

    Protected Function validar_date_project(ByVal id_project As Integer) As Integer

        'ISTANCIAMOS VARIABLES
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim Data_linea, Data_campos, Data_ubicacion, Data_actores, Data_flujos As DataTable
        Dim v_linea, V_obliga, V_ruta, V_riesgos, V_mitig, V_ubica, V_actores, V_flujo As Integer
        Dim result_str As String

        Dim shiwch_val As Integer = 0

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'region de consultas 

        '---------------------------------------- consulta linea estrategica

        sql.Append(" select l.Code from ProgramComponentByProject pp ")
        sql.Append(" inner join ProgramComponent pc on (pp.IdProgramComponent = pc.Id) ")
        sql.Append(" inner join Program pr on (pc.IdProgram = pr.Id) ")
        sql.Append(" inner join StrategicLine l on (pr.IdStrategicLine = l.Id) ")
        sql.Append(" where pp.IdProject = " & id_project)
        Data_linea = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos nuevos
        sql = New StringBuilder

        sql.Append(" SELECT p.obligationsoftheparties,p.BudgetRoute,p.RiskMitigation,p.RisksIdentified FROM Project AS p ")
        sql.Append(" where p.id = " & id_project)
        Data_campos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos ubicaciones
        sql = New StringBuilder

        sql.Append("select iddepto from ProjectLocation where IdProject = " & id_project)
        Data_ubicacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos actores
        sql = New StringBuilder

        sql.Append("select IdThird from ThirdByProject where IdProject  = " & id_project)
        Data_actores = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos flujos
        sql = New StringBuilder

        sql.Append("select N_pagos from Paymentflow where IdProject = " & id_project)
        Data_flujos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        'region de VALIDAMOS CONSULTAS

        'VALIDAMOS LINEA
        If Data_linea.Rows.Count > 0 Then
            v_linea = 0
        Else
            v_linea = 1
        End If

        'VALIDAMOS LOS CAMPOS NUEVOS DE FASE_3
        If Data_campos.Rows.Count > 0 Then

            If IsDBNull(Data_campos.Rows(0)("obligationsoftheparties")) = False Then
                V_obliga = 0
            Else
                V_obliga = 1
            End If

            If IsDBNull(Data_campos.Rows(0)("BudgetRoute")) = False Then
                V_ruta = 0
            Else
                V_ruta = 1
            End If

            If IsDBNull(Data_campos.Rows(0)("RiskMitigation")) = False Then
                V_mitig = 0
            Else
                V_mitig = 1
            End If

            If IsDBNull(Data_campos.Rows(0)("RisksIdentified")) = False Then
                V_riesgos = 0
            Else
                V_riesgos = 1
            End If

        End If

        'VALIDAMOS UBICACIONES
        If Data_ubicacion.Rows.Count > 0 Then
            V_ubica = 0
        Else
            V_ubica = 1
        End If

        'VALIDAMOS ACTORES
        If Data_actores.Rows.Count > 0 Then
            V_actores = 0
        Else
            V_actores = 1
        End If

        'VALIDAMOS FLUJOS
        If Data_flujos.Rows.Count > 0 Then
            V_flujo = 0
        Else
            V_flujo = 1
        End If

        'VALIDAMOS LOS RESULTADOS DE CADA PESTAÑA DEL PROYECTO
        If v_linea = 0 And V_obliga = 0 And V_ruta = 0 And V_riesgos = 0 And V_mitig = 0 And V_ubica = 0 And V_actores = 0 And V_flujo = 0 Then
            result_str = "VACIO"
        Else

            result_str = "Falta por diligenciar :"

            If v_linea = 1 Then
                result_str &= " linea estrategica"
                shiwch_val = 1
            End If

            If V_obliga = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", Campo: obligaciones de las partes"
                Else
                    result_str &= " Campo: obligaciones de las partes"
                    shiwch_val = 1
                End If
            End If

            If V_ruta = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", Campo: ruta estrategica"
                Else
                    result_str &= " Campo: ruta estrategica"
                    shiwch_val = 1
                End If
            End If

            If V_riesgos = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", Campo: riesgos identificados"
                Else
                    result_str &= " Campo: riesgos identificados"
                    shiwch_val = 1
                End If
            End If

            If V_mitig = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", Campo: Mitigación de riesgos"
                Else
                    result_str &= " Campo: Mitigación de riesgos"
                    shiwch_val = 1
                End If
            End If

            If V_ubica = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", ubicaciones"
                Else
                    result_str &= " ubicaciones"
                    shiwch_val = 1
                End If
            End If

            If V_actores = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", actores"
                Else
                    result_str &= " actores"
                    shiwch_val = 1
                End If
            End If

            If V_flujo = 1 Then
                If shiwch_val = 1 Then
                    result_str &= ", flujos de pagos"
                Else
                    result_str &= " flujos de pagos"
                    shiwch_val = 1
                End If
            End If

            result_str &= "!"

        End If

        Response.Write(result_str)

    End Function

    Protected Function buscar_date_third_project(ByVal id_project As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        sql.Append(" select  tp.Name ,tp.contact,tp.Type,tp.VrSpecies,tp.Vrmoney ,tp.FSCorCounterpartContribution  from Third t ")
        sql.Append(" inner join ThirdByProject tp on tp.IdThird= t.Id ")
        sql.Append(" inner join Project p on p.Id = tp.IdProject ")
        sql.Append(" where p.Id = " & id_project)

        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String
        html = "<table id=""T_Actors"" style=""width: 100%;"" border=""1"" cellspacing=""1"" cellpadding=""1""><thead><tr align=""center""><th style=""width: 200px"">Actor</th><th style=""width: 200px"">Contacto</th><th style=""width: 131px"">Tipo</th><th style=""width: 131px"">Vr Especie</th><th style=""width: 131px"">Vr Dinero</th><th style=""width: 131px"">Valor Total</th></tr></thead><tbody>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 200px"">{0}</td><td style=""width: 200px"">{1}</td><td style=""width: 131px"">{2}</td><td style=""width: 131px"">{3}</td><td style=""width: 131px"">{4}</td><td style=""width: 131px"">{5}</td></tr>", itemDataTable(0), itemDataTable(1), itemDataTable(2), itemDataTable(3), itemDataTable(4), itemDataTable(5))
        Next
        html &= "</tbody></table>"

        Response.Write(html)


    End Function

    Protected Function buscar_date_project(ByVal id_project As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Dim name As String = ""
        Dim line As String = ""
        Dim program As String = ""
        Dim value As String = ""
        Dim value2 As String = ""
        Dim fscformat As String = ""
        Dim otrosformat As String = ""
        Dim user_apro As String = ""
        Dim CreateDate As String = ""

        Dim html As String = ""

        'consulta para seber el valor de la fsc
        sql.Append(" select convert(bigint,REPLACE(tp.FSCorCounterpartContribution,'.','')) from Project p ")
        sql.Append(" inner join ThirdByProject tp on p.Id = tp.IdProject ")
        sql.Append(" inner join Third t on t.Id= tp.IdThird ")
        sql.Append(" where (t.code = '8600383389' And tp.IdProject = " & id_project & " )")

        Dim FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If FSCval = 0 Then
            fscformat = "0"
        Else
            fscformat = Format(Convert.ToInt64(FSCval), "#,###.##")
        End If

        sql = New StringBuilder

        'consulta para seber el valor de los otros actores en total
        sql.Append(" select sum(convert(bigint,REPLACE(tp.FSCorCounterpartContribution,'.',''))) from Project p ")
        sql.Append(" inner join ThirdByProject tp on p.Id = tp.IdProject ")
        sql.Append(" inner join Third t on t.Id= tp.IdThird ")
        sql.Append(" where (t.code <> '8600383389' And tp.IdProject = " & id_project & " )")

        Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If otrosval = 0 Then
            otrosformat = "0"
        Else
            otrosformat = Format(Convert.ToInt64(otrosval), "#,###.##")
        End If

        sql = New StringBuilder

        sql.Append(" select distinct p.Id,p.Name,l.Code,pm.Code as programa, p.TotalCost, p.CreateDate, au.Name as user_fsc from project p ")
        sql.Append(" inner join ProgramComponentByProject pp on (p.Id = pp.IdProject) ")
        sql.Append(" inner join ProgramComponent pc on (pp.IdProgramComponent = pc.Id) ")
        sql.Append(" inner join Program pm on (pc.IdProgram = pm.Id) ")
        sql.Append(" inner join StrategicLine l on (pm.IdStrategicLine = l.Id) ")
        sql.Append(" inner join FSC_eSecurity.dbo.ApplicationUser au on p.IdUser= au.ID ")

        sql.Append("  where p.Id = " & id_project)

        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim objResult As String

        If data.Rows.Count > 0 Then

            objResult = "{"

            objResult &= " ""name"": """

            If IsDBNull(data.Rows(0)("Name")) = False Then
                name = data.Rows(0)("Name")
                name = name.Replace("""", "\""")
            Else
                name = ""
            End If

            objResult &= name
            objResult &= " "", ""line"": """

            If IsDBNull(data.Rows(0)("Code")) = False Then
                line = data.Rows(0)("Code")
            Else
                line = ""
            End If

            objResult &= line

            objResult &= " "", ""value"": """

            If IsDBNull(data.Rows(0)("TotalCost")) = False Then
                value = data.Rows(0)("TotalCost")
                value2 = Format(Convert.ToInt64(value), "#,###.##")
            Else
                value2 = ""
            End If

            objResult &= value2
            objResult &= """, ""program"": """

            If IsDBNull(data.Rows(0)("programa")) = False Then
                program = data.Rows(0)("programa")
            Else
                program = ""
            End If

            objResult &= program

            objResult &= """, ""CreateDate"": """

            If IsDBNull(data.Rows(0)("CreateDate")) = False Then
                CreateDate = data.Rows(0)("CreateDate")
            End If

            objResult &= CreateDate

            objResult &= """, ""user_fsc"": """

            If IsDBNull(data.Rows(0)("user_fsc")) = False Then
                user_apro = data.Rows(0)("user_fsc")
            End If

            objResult &= user_apro

            objResult &= """, ""FSC"": """
            objResult &= fscformat

            objResult &= """, ""otro"": """
            objResult &= otrosformat

            objResult &= """}"

            Response.Write(objResult)

        Else
            objResult = "0"
            Response.Write(objResult)
        End If

    End Function

    Protected Function charge_project_approval()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' construir la sentencia
        sql.Append(" select  id, convert(varchar,IdIdea) + '_' + Name + '_' + convert(varchar,Id) as code, CreateDate from Project where Typeapproval = 3  ")
        sql.Append(" order by (CreateDate) DESC  ")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim htmlresults As String = "<option>Seleccione...</option>"

        For Each row As DataRow In data.Rows

            htmlresults &= String.Format("<option value='{0}'>{1}</option>", row(0).ToString(), row(1).ToString())

        Next

        ' retornar el objeto
        Response.Write(htmlresults)

    End Function

End Class