'TODO:15 SE CREA FORMULARIO DE TRANSACCION CON EL SERVIDOR PARA APROBACION IDEA
'5/06/13 GERMAN RODRIGUEZ

Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Class FormulationAndAdoption_ajaxaddProjectApprovalRecord
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim S_id_project, S_id_idea, S_date, S_N_acta, S_aport_others, S_aport_fsc, S_aport_total, S_approval As String
        Dim id_b As Integer
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        If Request.Form("action") <> Nothing Then

            Dim option_proyecto = Request.Form("action")

            Select Case option_proyecto

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

                Case "charge_idea_approval"
                    charge_idea_approval()

                Case "buscar"
                    'convierte la variable y llama funcion para la validacion de la idea
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    buscardateidea(id_b)

                Case "buscaractores"
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    buscardatethird(id_b)

                Case "validar_modulo_completo"
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    validar_date_project(id_b)

                Case "project_mother"
                    id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                    Save_project_mother(id_b)

                Case Else

            End Select

        End If

    End Sub

    Protected Function CREATE_Approval_Project(ByVal id_project As String, ByVal id_idea As String, ByVal date_approval As String, ByVal N_acta As String, ByVal aport_others As String, ByVal aport_fsc As String, ByVal aport_total As String, ByVal approval As String)

        Dim facade As New Facade
        Dim objIdeaApproval As New IdeaApprovalRecordEntity

        Dim ApplicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim Id_idea_v = id_idea.Split(New [Char]() {"_"c})

        'id del proyecto
        objIdeaApproval.idproject = id_project
        'fecha de aprobacion
        objIdeaApproval.approvaldate = date_approval
        'numero del acto
        objIdeaApproval.actnumber = N_acta
        'aportes de otros
        objIdeaApproval.aportOtros = aport_others
        'aportes de la FSC
        objIdeaApproval.aportFSC = aport_fsc
        'total aporte
        objIdeaApproval.approvedvalue = aport_total
        'comite de aprobacion
        objIdeaApproval.approved = approval
        'usuario registrado
        objIdeaApproval.iduser = ApplicationCredentials.UserID
        'fecha del sistema
        objIdeaApproval.createdate = Now

        'datos quemados
        objIdeaApproval.code = ""
        objIdeaApproval.codeapprovedidea = ""
        objIdeaApproval.Ididea = Id_idea_v(0)
        objIdeaApproval.comments = ""
        objIdeaApproval.enabled = 1
        objIdeaApproval.attachment = ""

        facade.addIdeaApprovalRecord(ApplicationCredentials, objIdeaApproval)

        update_project_approval(id_project)

        Dim Result As String

        If objIdeaApproval.idproject <> 0 Then

            Result = "La Idea se aprobó exitosamente!"
            Response.Write(Result)

        Else

            Result = "Se perdio la conexion al guardar los datos de la aprobación"
            Response.Write(Result)

        End If


    End Function

    Protected Function update_project_approval(ByVal idproject As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder

        sql.Append(" update idea set Typeapproval = 1 where id = " & idproject)
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

        sql.Append(" select l.Code from ProgramComponentByidea pi ")
        sql.Append(" inner join ProgramComponent pc on (pi.IdProgramComponent = pc.Id) ")
        sql.Append(" inner join Program pr on (pc.IdProgram = pr.Id) ")
        sql.Append(" inner join StrategicLine l on (pr.IdStrategicLine = l.Id) ")
        sql.Append(" where pi.IdIdea = " & id_project)
        Data_linea = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos nuevos
        sql = New StringBuilder

        sql.Append(" SELECT i.obligationsoftheparties,i.BudgetRoute,i.RiskMitigation,i.RisksIdentified FROM Idea AS i ")
        sql.Append(" where i.id = " & id_project)
        Data_campos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos ubicaciones
        sql = New StringBuilder

        sql.Append("select iddepto from LocationByIdea where IdIdea = " & id_project)
        Data_ubicacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos actores
        sql = New StringBuilder

        sql.Append("select IdThird from ThirdByIdea where IdIdea  = " & id_project)
        Data_actores = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        '---------------------------------------- consulta campos flujos
        sql = New StringBuilder

        sql.Append("select N_pagos from Paymentflow where IdIdea = " & id_project)
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

    Protected Function charge_idea_approval()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        ' construir la sentencia


        sql.Append(" select distinct I.Code,I.Name,I.createDate, I.Code+'_'+I.Name as 'name_code' from Idea i ")
        sql.Append(" left JOIN ProjectApprovalRecord par on par.Ididea = i.Id ")
        sql.Append(" where PAR.Ididea is null and i.Typeapproval = 3 ")

        sql.Append(" ORDER BY I.createDate  DESC")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim htmlresults As String = "<option>Seleccione...</option>"

        For Each row As DataRow In data.Rows

            htmlresults &= String.Format("<option value='{0}'>{1}</option>", row(0).ToString(), row(3).ToString())

        Next

        ' retornar el objeto
        Response.Write(htmlresults)

    End Function

    Public Function buscardateidea(ByVal ididea As Integer)

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

        sql.Append("select convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.','')) from Idea i  ")
        sql.Append("inner join ThirdByIdea ti on i.Id = ti.IdIdea  ")
        sql.Append(" inner join Third t on t.Id= ti.IdThird ")
        sql.Append("where (t.code = '8600383389' And ti.IdIdea = " & ididea & ")")

        Dim FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If FSCval = 0 Then
            fscformat = "0"
        Else
            fscformat = Format(Convert.ToInt64(FSCval), "#,###.##")
        End If

        sql = New StringBuilder
        sql.Append("select sum(convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.',''))) from Idea i   ")
        sql.Append("inner join ThirdByIdea ti on i.Id=ti.IdIdea  ")
        sql.Append(" inner join Third t on t.Id= ti.IdThird ")
        sql.Append("where(t.code <> '8600383389' And ti.IdIdea = " & ididea & ")")

        Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)


        If otrosval = 0 Then
            otrosformat = "0"
        Else
            otrosformat = Format(Convert.ToInt64(otrosval), "#,###.##")
        End If

        sql = New StringBuilder
        'consulta de los datos de actores por id
        sql.Append(" select distinct i.Id, i.Name, l.Code, p.Code as programa, i.Cost, p.CreateDate, au.Name as user_fsc  from idea i  ")
        sql.Append(" inner join ProgramComponentByIdea pi on (i.Id = pi.IdIdea)         ")
        sql.Append(" inner join ProgramComponent pc on (pi.IdProgramComponent = pc.Id)  ")
        sql.Append(" inner join Program p on (pc.IdProgram = p.Id)                      ")
        sql.Append(" inner join StrategicLine l on (p.IdStrategicLine = l.Id)           ")
        sql.Append(" inner join FSC_eSecurity.dbo.ApplicationUser au on i.IdUser= au.ID ")

        sql.Append("where i.Id = " & ididea)
        ' ejecutar la intruccion
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

            If IsDBNull(data.Rows(0)("cost")) = False Then
                value = data.Rows(0)("cost")
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

    Public Function buscardatethird(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'consulta de los datos de actores por id
        sql.Append("select t.Name,t.contact,ti.Type,ti.VrSpecies,ti.Vrmoney ,ti.FSCorCounterpartContribution  from Third t       ")
        sql.Append("inner join ThirdByIdea ti on ti.IdThird= t.Id             ")
        sql.Append("inner join Idea i on i.Id = ti.IdIdea                     ")

        sql.Append("where i.Id = " & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String
        html = "<table id=""T_Actors"" style=""width: 100%;"" border=""1"" cellspacing=""1"" cellpadding=""1""><thead><tr align=""center""><th style=""width: 200px"">Actor</th><th style=""width: 200px"">Contacto</th><th style=""width: 131px"">Tipo</th><th style=""width: 131px"">Vr Especie</th><th style=""width: 131px"">Vr Dinero</th><th style=""width: 131px"">Valor Total</th></tr></thead><tbody>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 200px"">{0}</td><td style=""width: 200px"">{1}</td><td style=""width: 131px"">{2}</td><td style=""width: 131px"">{3}</td><td style=""width: 131px"">{4}</td><td style=""width: 131px"">{5}</td></tr>", itemDataTable(0), itemDataTable(1), itemDataTable(2), itemDataTable(3), itemDataTable(4), itemDataTable(5))
        Next
        html &= "</tbody></table>"

        Response.Write(html)

    End Function

    ''' <summary>
    ''' funcion crear proyecto madre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save_project_mother(ByVal ididea As String)


        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'capturamos el usuario q aprueba la idea
        Dim user As String = applicationCredentials.UserID

        'capturar el id de la idea para consultas
        'Dim ididea As String = Me.ddlidproject.SelectedValue

        Dim dtData, Data_component, Data_ubicacion, Data_actor As DataTable
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        'consultamos el futuro id del proyecto
        sql.Append(" select MAX(id)+ 1 from Project ")
        Dim idproject = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder

        'insertamos los datos del proyecto madre
        sql.Append("insert into Project (IdIdea, code, Name, Objective, ZoneDescription, Justification, Results,  ResultsInstalledCapacity, ResultsKnowledgeManagement, OtherResults, obligationsoftheparties,  BudgetRoute, RisksIdentified, RiskMitigation, BeginDate,  Duration,  days, completiondate, Population, Mother, ideaappliesIVA, EffectiveBudget, IdPhase,  Enabled, IdUser, isLastVersion, IdProcessInstance, IdActivityInstance, Typeapproval, idkey, Project_derivados, CreateDate) ")
        sql.Append(" select i.ID, convert(varchar,i.ID)+'_'+i.name+'_','Proyecto_M_'+ i.name,i.Objective, i.AreaDescription, i.Justification, i.Results, i.ResultsInstalledCapacity, i.ResultsKnowledgeManagement, i.OtherResults, i.obligationsoftheparties, i.BudgetRoute, i.RisksIdentified, i.RiskMitigation, i.StartDate, i.Duration, i.days, i.Enddate, i.Population, 1, i.ideaappliesIVA,'2014', 1, 1,'" & user & "',0,0,0,1," & Convert.ToString(idproject) & "," & Convert.ToString(idproject) & ", GETDATE() from Idea i ")
        sql.Append(" where i.id = " & ididea)

        sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

        'obtener el id
        dtData = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        ' id creado
        Dim idEntity As Long = CLng(dtData.Rows(0)("Id"))

        ' finalizar la transaccion
        CtxSetComplete()

        sql = New StringBuilder

        'actualzamos los id de proyecto
        sql.Append("update Project set code = code +'" & Convert.ToString(idEntity) & "', idKey = '" & Convert.ToString(idEntity) & "', Project_derivados= '" & Convert.ToString(idEntity) & "' , source = '', purpose = '', TotalCost=0, FSCContribution=0, CounterpartValue=0, Attachment='',Antecedent='', StrategicDescription=''  where id = " & idEntity)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        'consultamos los id de la tabla componentes de la idea seleccionada
        sql.Append("select id, IdProgramComponent from ProgramComponentByIdea where IdIdea = " & ididea)
        Data_component = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'validamos si hay componentes
        If Data_component.Rows.Count > 0 Then

            'rrecorremos la consulta de componentes en idea
            For Each row As DataRow In Data_component.Rows
                Dim id_component = row(0).ToString()

                sql = New StringBuilder
                'hacemos el insert en la tabla componentbyproyect de los datos estraidos de la idea
                sql.Append(" insert into ProgramComponentByProject (IdProject,IdProgramComponent) ")
                sql.Append("select '" & idEntity & "', IdProgramComponent from  ProgramComponentByIdea where id=" & id_component)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

            Next

        End If

        sql = New StringBuilder

        'consultamos los id de la tabla ubicaciones de la idea seleccionada
        sql.Append("select id from LocationByIdea where IdIdea = " & ididea)
        Data_ubicacion = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If Data_ubicacion.Rows.Count > 0 Then

            For Each row As DataRow In Data_ubicacion.Rows

                Dim id_ubicacion = row(0).ToString()

                sql = New StringBuilder
                'hacemos el insert en la tabla proyeclocation de los datos estraidos de la idea
                sql.Append(" insert into ProjectLocation (IdProject,IdCity,iddepto) ")
                sql.Append("select '" & idEntity & "',IdCity,IdDepto from LocationByIdea where id = " & id_ubicacion)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString)


            Next

        End If

        sql = New StringBuilder

        'consultamos los id de la tabla ubicaciones de la idea seleccionada
        sql.Append("select id from thirdbyidea where ididea =" & ididea)
        Data_actor = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If Data_actor.Rows.Count > 0 Then

            For Each row As DataRow In Data_actor.Rows

                Dim id_actor = row(0).ToString()

                sql = New StringBuilder
                'hacemos el insert en la tabla proyeclocation de los datos estraidos de la idea
                sql.Append(" insert into ThirdByProject(IdProject,IdThird,Type,Vrmoney,VrSpecies,FSCorCounterpartContribution,Name,Contact,Documents,Phone,Email,CreateDate,generatesflow) ")
                sql.Append("select '" & idEntity & "',ti.IdThird,ti.Type,ti.Vrmoney,ti.VrSpecies,ti.FSCorCounterpartContribution,ti.Name,ti.Contact,ti.Documents,ti.Phone,ti.Email,GETDATE(),ti.generatesflow from ThirdByIdea ti where id = " & id_actor)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

            Next

        End If

        sql = New StringBuilder

        'actualizamos el campo proyecto de la tabla paymentflow y le damos el atributo madre a los flujos de pago
        sql.Append("update Paymentflow set idproject = '" & idEntity & "',mother = 1 where ididea =" & ididea)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)


        sql = New StringBuilder

        'actualizamos el campo proyecto de la tabla paymentflow y le damos el atributo madre a los flujos de pago
        sql.Append("update Detailedcashflows set idproject = '" & idEntity & "',mother = 1 where ididea =" & ididea)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)



    End Function


End Class



