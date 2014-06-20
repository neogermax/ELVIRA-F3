Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Runtime.Serialization.Json
Imports Newtonsoft.Json



Partial Public Class AjaxAddProject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion, dia As String
        Dim c_estado_proceso, s_id, S_estado, type_i_p, idprogram_list, S_ididea, S_strCode, S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_mitigacion, S_riesgos, S_presupuestal, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles As String
        Dim estado_proceso, ideditar, id_lineStrategic, id_depto, idprogram, idpopulation, Countarchivo As Integer

        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files
        Dim DocumentsTmpList As New List(Of DocumentstmpEntity)

        Session("locationByIdeaList") = New List(Of LocationByIdeaEntity)
        Session("thirdByIdeaList") = New List(Of ThirdByIdeaEntity)
        Session("paymentFlowList") = New List(Of PaymentFlowEntity)
        Session("DetailedcashflowsList") = New List(Of DetailedcashflowsEntity)

        Session("projectLocationList") = New List(Of ProjectLocationEntity)
        Session("thirdByProjectList") = New List(Of ThirdByProjectEntity)


        If Request.Files.Count() > 0 Then

            'Se recorre la lista de archivos cargados al servidor
            For i As Integer = 0 To files.Count - 1

                Dim file As HttpPostedFile = files(i)

                If file.ContentLength > 0 Then

                    strFileName = file.FileName.Split("\".ToCharArray)

                    ' dar nombre al anexo
                    fileName = strFileName(strFileName.Length - 1)

                    ' determinanado la ruta destino
                    Dim sFullPath As String = HttpContext.Current.Server.MapPath(PublicFunction.getSettingValue("documentPath")) & "\temp\" & fileName

                    'Subiendo el archivo al server
                    file.SaveAs(sFullPath)

                    'Se instancia un objeto de tipo documento y se pobla con la info. reuqerida.
                    Dim objDocument As New DocumentstmpEntity()
                    objDocument.namefile = fileName

                    'Se agrega el objeto de tipo documento a la lista de documentos
                    DocumentsTmpList.Add(objDocument)
                    Session("DocumentsTmp") = DocumentsTmpList

                    Response.Write(fileName)
                End If

            Next

            Exit Sub

        End If

        'trae el jquery para hacer todo por debajo del servidor
        If Request.Form("action") <> Nothing Then

            Dim option_proyecto = Request.Form("action")

            Select Case option_proyecto

                Case "save"

                    'cambio de metodologia
                    S_linea_estrategica = Request.Form("linea_estrategica").ToString()
                    S_programa = Request.Form("programa").ToString()
                    S_nombre = Request.Form("nombre").ToString
                    S_justificacion = Request.Form("justificacion").ToString
                    S_objetivo = Request.Form("objetivo").ToString
                    S_objetivo_esp = Request.Form("objetivo_esp").ToString
                    S_Resultados_Benef = Request.Form("Resultados_Benef").ToString
                    S_Resultados_Ges_c = Request.Form("Resultados_Ges_c").ToString
                    S_Resultados_Cap_i = Request.Form("Resultados_Cap_i").ToString
                    S_Resultados_otros_resul = Request.Form("Resultados_otros_resul").ToString
                    S_Fecha_inicio = Request.Form("Fecha_inicio").ToString
                    S_mes = Request.Form("mes").ToString
                    S_dia = Request.Form("dia").ToString
                    S_Fecha_fin = Request.Form("Fecha_fin").ToString
                    S_Población = Request.Form("Población").ToString
                    S_contratacion = Request.Form("contratacion").ToString
                    S_riesgos = Request.Form("riesgo").ToString
                    S_mitigacion = Request.Form("mitigacion").ToString
                    S_presupuestal = Request.Form("presupuestal").ToString
                    S_cost = Request.Form("cost").ToString
                    S_iva = Request.Form("iva").ToString
                    S_obligaciones = Request.Form("obligaciones").ToString
                    S_listubicaciones = Request.Form("listubicaciones").ToString
                    S_listactors = Request.Form("listactores").ToString
                    S_listcomponentes = Request.Form("listcomponentes").ToString
                    S_listflujos = Request.Form("listflujos").ToString
                    S_listdetallesflujos = Request.Form("listdetallesflujos").ToString
                    S_listfiles = Request.Form("listfiles").ToString
                    S_ididea = Request.Form("ididea").ToString
                    S_strCode = Request.Form("str_code").ToString
                    S_estado = Request.Form("aproval_project").ToString

                    save_PROYECTO(S_ididea, S_strCode, S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles, S_estado) '


                Case "edit"

                    'cambio de metodologia
                    S_linea_estrategica = Request.Form("linea_estrategica").ToString()
                    S_programa = Request.Form("programa").ToString()
                    S_nombre = Request.Form("nombre").ToString
                    S_justificacion = Request.Form("justificacion").ToString
                    S_objetivo = Request.Form("objetivo").ToString
                    S_objetivo_esp = Request.Form("objetivo_esp").ToString
                    S_Resultados_Benef = Request.Form("Resultados_Benef").ToString
                    S_Resultados_Ges_c = Request.Form("Resultados_Ges_c").ToString
                    S_Resultados_Cap_i = Request.Form("Resultados_Cap_i").ToString
                    S_Resultados_otros_resul = Request.Form("Resultados_otros_resul").ToString
                    S_Fecha_inicio = Request.Form("Fecha_inicio").ToString
                    S_mes = Request.Form("mes").ToString
                    S_dia = Request.Form("dia").ToString
                    S_Fecha_fin = Request.Form("Fecha_fin").ToString
                    S_Población = Request.Form("Población").ToString
                    S_contratacion = Request.Form("contratacion").ToString
                    S_riesgos = Request.Form("riesgo").ToString
                    S_mitigacion = Request.Form("mitigacion").ToString
                    S_presupuestal = Request.Form("presupuestal").ToString
                    S_cost = Request.Form("cost").ToString
                    S_iva = Request.Form("iva").ToString
                    S_obligaciones = Request.Form("obligaciones").ToString
                    S_listubicaciones = Request.Form("listubicaciones").ToString
                    S_listactors = Request.Form("listactores").ToString
                    S_listcomponentes = Request.Form("listcomponentes").ToString
                    S_listflujos = Request.Form("listflujos").ToString
                    S_listdetallesflujos = Request.Form("listdetallesflujos").ToString
                    S_listfiles = Request.Form("listfiles").ToString
                    S_ididea = Request.Form("ididea").ToString
                    S_strCode = Request.Form("str_code").ToString
                    S_estado = Request.Form("aproval_project").ToString
                    s_id = Request.Form("code").ToString

                    edit_PROYECTO(s_id, S_ididea, S_strCode, S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles, S_estado) '

                Case Else

            End Select

        Else


            action = Request.QueryString("action").ToString()
            Select Case action

                '----------------- modulo componentes------------------------------------------------------------
                Case "C_program"
                    id_lineStrategic = Convert.ToInt32(Request.QueryString("idlinestrategic").ToString)
                    charge_program(id_lineStrategic)

                Case "list_program"
                    id_lineStrategic = Convert.ToInt32(Request.QueryString("idlinestrategic").ToString)
                    charge_list_program(id_lineStrategic)

                Case "C_component"
                    idprogram_list = Request.QueryString("idprogram").ToString
                    estado_proceso = Request.QueryString("estado_proceso").ToString
                    ideditar = Convert.ToInt32(Request.QueryString("id").ToString)

                    charge_component(idprogram_list, estado_proceso, ideditar)

                Case "View_line_strategic"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_line_Strategic(ideditar)

                Case "View_line_strategic_project"

                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_line_Strategic_project(ideditar)

                Case "View_program"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    estado_proceso = Request.QueryString("estado_proceso").ToString
                    searh_Program(ideditar, estado_proceso)

                Case "View_component"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_component(ideditar)

                Case "View_component_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_component_project(ideditar)
                    '----------------- modulo descripcion-------------------------------------------------------

                Case "calculafechas"
                    fecha = Convert.ToDateTime(Request.QueryString("fecha").ToString())
                    duracion = Request.QueryString("duracion").ToString()
                    dia = Request.QueryString("dias").ToString()
                    calculafechas(fecha, duracion, dia)

                Case "C_population"

                    idpopulation = Convert.ToInt32(Request.QueryString("idpopulation").ToString)
                    Charge_population(idpopulation)

                Case "Cpopulation_view"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_c_population(ideditar)

                Case "Ctypcontract_view"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_c_typecontract(ideditar)

                Case "C_ideas_aprobada"
                    charge_idea_aproval()

                Case "C_type_aproval"
                    type_i_p = Request.QueryString("type").ToString
                    charge_typeAproval(type_i_p)

                Case "C_charge_others"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    charge_others_droplist(ideditar)
                    '----------------- modulo ubicacion-------------------------------------------------------
                Case "C_munip"
                    id_depto = Convert.ToInt32(Request.QueryString("iddepto").ToString)
                    Charge_munip(id_depto)

                Case "View_ubicacion_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_location_array(ideditar)

                Case "view_ubicacion_proyect"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_location_project(ideditar)

                    '----------------- modulo actores-------------------------------------------------------
                Case "buscar"
                    'convierte la variable y llama funcion para la validacion de la idea
                    id_b = Convert.ToInt32(Request.QueryString("id").ToString())
                    buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))

                Case "View_actores_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_actores_array(ideditar)

                Case "View_actores_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_actores_project(ideditar)

                    '----------------- modulo flujos-------------------------------------------------------
                Case "View_flujos_p_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_flujos_array(ideditar)

                Case "View_flujos_p_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_flujos_project(ideditar)

                Case "View_flujos_actors_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_actors_flujos_array(ideditar)

                Case "View_flujos_actors_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_actors_flujos_project(ideditar)

                Case "View_detalle_flujo_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_detalles_array(ideditar)

                Case "View_detalle_flujo_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_detalles_project(ideditar)
                    '----------------- modulo anexos-------------------------------------------------------

                Case "View_anexos_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_document_anexos_array(ideditar)

                Case "View_anexos_project"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    searh_document_anexos_project(ideditar)
                    '----------------- tareas generales-------------------------------------------------------
                Case "load_combos"
                    type_i_p = Request.QueryString("type").ToString
                    load_combos(type_i_p)

                Case "getIdeaProject_inf_p"
                    ideditar = Convert.ToInt32(Request.QueryString("id").ToString)
                    searchIdea_inf_p(ideditar, applicationCredentials)

                Case "traer_valores_madre"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    c_estado_proceso = Request.QueryString("estado").ToString
                    SearchProject_values_mother(ideditar, c_estado_proceso)

                Case "borrar_archivos"
                    borrar_archivos()

                Case "copiar_archivos"
                    copiar_archivos()

                Case "aprobacion_proyecto"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    validar_aprobacion_proyecto(ideditar)

                Case "proyecto_madre"
                    ideditar = Convert.ToInt32(Request.QueryString("idproject").ToString)
                    validar_proyecto_madre(ideditar)

                Case Else


            End Select

        End If

    End Sub

    Protected Function load_combos(ByVal type As String)

        Dim facade As New Facade

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim Data_line, Data_typecontrato, Data_typo_project, Data_depto, Data_Actor, Data_Approval As DataTable

        sql.Append(" SELECT ID, Code as descripcion FROM StrategicLine AS pro where id <> 32 order by descripcion")
        Data_line = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" SELECT TYPECONTRACT.ID, TYPECONTRACT.CONTRACT as descripcion  FROM TYPECONTRACT  order by (ID)")
        Data_typecontrato = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" select pt.ID, pt.Project_Type as descripcion  from  ProjectType pt  order by pt.Project_Type asc")
        Data_typo_project = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" SELECT ID, Name as descripcion FROM FSC_eSecurity.dbo.Depto  order by descripcion ")
        Data_depto = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" select ID, Name as descripcion from Third order by descripcion ")
        Data_Actor = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        If type = "P" Then

            sql.Append(" select ID, estados as descripcion from Type_aproval_project ")
            sql.Append(" where aplica_idea ='s' ")
            sql.Append(" order by estados asc")

        Else

            sql.Append(" select ID,estados as descripcion from Type_aproval_project ")
            sql.Append(" order by estados asc")

        End If

        Data_Approval = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim Json_line = JsonConvert.SerializeObject(Data_line)
        Dim Json_typecontrato = JsonConvert.SerializeObject(Data_typecontrato)
        Dim Json_typo_project = JsonConvert.SerializeObject(Data_typo_project)
        Dim Json_depto = JsonConvert.SerializeObject(Data_depto)
        Dim Json_Actor = JsonConvert.SerializeObject(Data_Actor)
        Dim Json_Approval = JsonConvert.SerializeObject(Data_Approval)

        Dim objCatalogSerialize = String.Format("[{0},{1},{2},{3},{4},{5}]", Json_line, Json_typecontrato, Json_typo_project, Json_depto, Json_Actor, Json_Approval)

        Response.Write(objCatalogSerialize)

    End Function

    Protected Function validar_proyecto_madre(ByVal id_project As Integer)

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim result As Integer


        sql.Append("select Mother from Project  where id =" & id_project)
        Dim idproyectoprovado = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If idproyectoprovado <> 0 Then

            result = 1

        Else

            result = 0

        End If

        Response.Write(result)
    End Function

    Protected Function validar_aprobacion_proyecto(ByVal id_project As Integer)

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim result As Integer


        sql.Append("SELECT Idproject FROM  ProjectApprovalRecord WHERE Idproject=" & id_project)
        Dim idproyectoprovado = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If idproyectoprovado <> 0 Then

            result = 1

        Else

            result = 0

        End If

        Response.Write(result)
    End Function

    Protected Function SearchProject_values_mother(ByVal ididea As String, ByVal estado As String)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objResult, completiondate, BeginDate As String

        If estado = "crear" Then
            'consulta para saber el proyecto madre del derivado seleccionado
            sql.Append(" select distinct p.Project_derivados from  Project p where p.ididea = " & ididea)
        Else
            'consulta para saber el proyecto madre del derivado seleccionado
            sql.Append(" select distinct p.Project_derivados from  Project p where p.id = " & ididea)
        End If

        Dim id_proyect_mother = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder

        'consulta para saber el total del proyecto madre
        sql.Append(" select sum(CONVERT(INT,replace(tp.Vrmoney,'.',''))) AS TOTAL_VALOR from ThirdByProject tp where tp.IdProject = " & id_proyect_mother)
        Dim total_value_mother = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder

        'validamos si viene de edicion o creacion de proyecto
        If estado = "crear" Then
            'consulta para saber la suma del proyectos derivados
            sql.Append("select sum (convert(int, valortotal)) as total_derivados from Paymentflow where idproject in (select p.id  from Project p where  p.Mother=0 and p.Project_derivados = " & id_proyect_mother & ")")
        Else
            'consulta para saber la suma del proyectos derivados
            sql.Append("select sum (convert(int, valortotal)) as total_derivados from Paymentflow where idproject in (select p.id  from Project p where  p.Mother=0 and p.Project_derivados = " & id_proyect_mother & " and p.id <> " & ididea & ")")
        End If

        Dim ressiduo_valor_mother = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Dim disponible As String

        disponible = Convert.ToInt32(total_value_mother) - Convert.ToInt32(ressiduo_valor_mother)

        sql = New StringBuilder

        sql.Append("select distinct  p.BeginDate, p.completiondate from  Project p where p.id = " & id_proyect_mother)
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        objResult &= "{"

        objResult &= """total_value_mother"": """
        objResult &= total_value_mother

        objResult &= """, ""ressiduo_valor_mother"": """
        objResult &= disponible

        If data.Rows.Count > 0 Then

            objResult &= """, ""BeginDate"": """

            If IsDBNull(data.Rows(0)("BeginDate")) = False Then
                BeginDate = data.Rows(0)("BeginDate")
            End If
            Dim dateFormated_START As Date = BeginDate
            objResult &= dateFormated_START.ToString("yyyy/MM/dd")

            objResult &= """, ""completiondate"": """

            If IsDBNull(data.Rows(0)("completiondate")) = False Then
                completiondate = data.Rows(0)("completiondate")
            End If
            Dim dateFormated_END As Date = completiondate
            objResult &= dateFormated_END.ToString("yyyy/MM/dd")

        End If

        objResult &= """}"

        Response.Write(objResult)

    End Function

    Protected Function charge_others_droplist(ByVal idproject As String)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim Population, Typeapproval, Idtypecontract, ideaappliesIVA As String

        sql.Append(" select Population, Typeapproval, Idtypecontract, ideaappliesIVA from Project where id = " & idproject)
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Dim objResult As String = "{"

            ' obtiene el objetivo de IDEA 
            objResult &= " ""Population"": """
            If IsDBNull(data.Rows(0)("Population")) = False Then
                Population = data.Rows(0)("Population")
            End If
            objResult &= Population

            ' obtiene la justificacion de IDEA
            objResult &= """, ""Typeapproval"": """
            If IsDBNull(data.Rows(0)("Typeapproval")) = False Then
                Typeapproval = data.Rows(0)("Typeapproval")
            End If
            objResult &= Typeapproval

            objResult &= """, ""Idtypecontract"": """
            If IsDBNull(data.Rows(0)("Idtypecontract")) = False Then
                Idtypecontract = data.Rows(0)("Idtypecontract")
            End If
            objResult &= Idtypecontract

            objResult &= """, ""ideaappliesIVA"": """
            If IsDBNull(data.Rows(0)("ideaappliesIVA")) = False Then
                ideaappliesIVA = data.Rows(0)("ideaappliesIVA")
            End If
            objResult &= ideaappliesIVA

            'objResult &= """, ""ididea"": """
            'If IsDBNull(data.Rows(0)("ididea")) = False Then
            '    ididea = data.Rows(0)("ididea")
            'End If
            'objResult &= ididea

            objResult &= """}"

            Response.Write(objResult)

        End If

    End Function

    Protected Function charge_typeAproval(ByVal type As String)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        If type = "I" Then

            sql.Append(" select id,estados from Type_aproval_project ")
            sql.Append(" where aplica_idea ='s' ")
            sql.Append(" order by estados asc")

        Else

            sql.Append(" select id,estados from Type_aproval_project ")
            sql.Append(" order by estados asc")

        End If

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String = "<option>Seleccione...</option>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)

    End Function

    Protected Function searh_document_anexos_project(ByVal idproject As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_anexos As DataTable
        Dim idfile, filename, Description As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select d.id, d.AttachFile,d.Description, d.id_document from DocumentsByEntity de ")
        sql.Append(" inner join Documents d on d.Id =de.IdDocuments ")
        sql.Append(" where  de.EntityName ='ProjectEntity' and de.IdnEntity=" & idproject)

        data_anexos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim valuar_anexo As Integer = 1
        Dim objResult As String = ""

        If data_anexos.Rows.Count > 0 Then

            For Each row As DataRow In data_anexos.Rows
                '{ "idfile": idfile, "filename": filename, "Description": description }

                objResult &= "{"

                objResult &= """idfile"": """
                idfile = row(3).ToString

                If idfile = "" Then
                    idfile = row(0).ToString
                End If

                idfile = Replace(idfile, " ", "")

                objResult &= idfile

                objResult &= """, ""filename"": """
                filename = row(1).ToString

                objResult &= filename

                objResult &= """, ""Description"": """
                Description = row(2).ToString

                objResult &= Description

                If valuar_anexo = data_anexos.Rows.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_anexo = valuar_anexo + 1

            Next

        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_document_anexos_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_anexos As DataTable
        Dim idfile, filename, Description As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select d.id, d.AttachFile,d.Description, d.id_document from DocumentsByEntity de ")
        sql.Append(" inner join Documents d on d.Id =de.IdDocuments ")
        sql.Append(" where  de.EntityName ='IdeaEntity' and de.IdnEntity=" & ididea)

        data_anexos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim valuar_anexo As Integer = 1
        Dim objResult As String = ""

        If data_anexos.Rows.Count > 0 Then

            For Each row As DataRow In data_anexos.Rows
                '{ "idfile": idfile, "filename": filename, "Description": description }

                objResult &= "{"

                objResult &= """idfile"": """
                idfile = row(3).ToString

                If idfile = "" Then
                    idfile = row(0).ToString
                End If

                idfile = Replace(idfile, " ", "")

                objResult &= idfile

                objResult &= """, ""filename"": """
                filename = row(1).ToString

                objResult &= filename

                objResult &= """, ""Description"": """
                Description = row(2).ToString

                objResult &= Description

                If valuar_anexo = data_anexos.Rows.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_anexo = valuar_anexo + 1

            Next

        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_detalles_project(ByVal idproject As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim idpago, idaportante, Aportante, desembolso As String

        sql.Append(" select dcf.N_pago, dcf.IdAportante, dcf.Aportante, dcf.Desembolso  from Detailedcashflows dcf where dcf.Idproject = " & idproject)

        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim valuar_detalle As Integer = 1
        Dim objResult As String = ""

        For Each row As DataRow In data.Rows

            objResult &= "{"

            objResult &= """idpago"": """
            idpago = row(0).ToString()

            objResult &= idpago

            objResult &= """, ""idaportante"": """
            idaportante = row(1).ToString()

            objResult &= idaportante

            objResult &= """, ""Aportante"": """
            Aportante = row(2).ToString()

            objResult &= Aportante

            objResult &= """, ""desembolso"": """
            desembolso = row(3).ToString()
            desembolso = desembolso.Replace(" ", "")

            objResult &= desembolso


            If valuar_detalle = data.Rows.Count Then

                objResult &= """}"

            Else
                objResult &= """}|"

            End If

            valuar_detalle = valuar_detalle + 1

        Next

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_detalles_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim idpago, idaportante, Aportante, desembolso As String

        sql.Append(" select dcf.N_pago, dcf.IdAportante, dcf.Aportante, dcf.Desembolso  from Detailedcashflows dcf where dcf.IdIdea = " & ididea)

        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim valuar_detalle As Integer = 1
        Dim objResult As String = ""

        For Each row As DataRow In data.Rows

            objResult &= "{"

            objResult &= """idpago"": """
            idpago = row(0).ToString()

            objResult &= idpago

            objResult &= """, ""idaportante"": """
            idaportante = row(1).ToString()

            objResult &= idaportante

            objResult &= """, ""Aportante"": """
            Aportante = row(2).ToString()

            objResult &= Aportante

            objResult &= """, ""desembolso"": """
            desembolso = row(3).ToString()
            desembolso = desembolso.Replace(" ", "")

            objResult &= desembolso


            If valuar_detalle = data.Rows.Count Then

                objResult &= """}"

            Else
                objResult &= """}|"

            End If

            valuar_detalle = valuar_detalle + 1

        Next

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_actors_flujos_project(ByVal idproject As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_actors_flujos As DataTable

        Dim thirdbyproject As New ThirdByProjectDALC
        Dim data_listactores As List(Of ThirdByProjectEntity)

        Dim actorsVal, actorsName, tipoactors, contact, cedula, telefono, email, diner, especie, total, estado_flujo As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        data_listactores = thirdbyproject.getList(applicationCredentials, , idproject, , , , , )


        Dim valuar_actor As Integer = 1
        Dim objResult As String = ""

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                estado_flujo = row.EstadoFlujos


                If estado_flujo = "s" Then

                    objResult &= "{"

                    objResult &= """actorsVal"": """
                    actorsVal = row.idthird

                    objResult &= actorsVal

                    objResult &= """, ""actorsName"": """
                    actorsName = row.Name

                    objResult &= actorsName

                    objResult &= """, ""tipoactors"": """
                    tipoactors = row.type

                    objResult &= tipoactors

                    objResult &= """, ""contact"": """
                    contact = row.contact

                    objResult &= contact

                    objResult &= """, ""cedula"": """
                    cedula = row.Documents

                    objResult &= cedula

                    objResult &= """, ""telefono"": """
                    telefono = row.Phone

                    objResult &= telefono

                    objResult &= """, ""email"": """
                    email = row.Email

                    objResult &= email

                    objResult &= """, ""diner"": """
                    diner = row.Vrmoney

                    objResult &= diner

                    objResult &= """, ""especie"": """
                    especie = row.VrSpecies

                    objResult &= especie

                    objResult &= """, ""total"": """
                    total = row.FSCorCounterpartContribution

                    objResult &= total

                    objResult &= """, ""estado_flujo"": """

                    objResult &= estado_flujo

                    If valuar_actor = data_listactores.Count Then

                        objResult &= """}"

                    Else
                        objResult &= """}|"

                    End If

                    valuar_actor = valuar_actor + 1


                End If
            Next
        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_actors_flujos_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_actors_flujos As DataTable

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim data_listactores As List(Of ThirdByIdeaEntity)

        Dim actorsVal, actorsName, tipoactors, contact, cedula, telefono, email, diner, especie, total, estado_flujo As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        Dim valuar_actor As Integer = 1
        Dim objResult As String = ""

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                estado_flujo = row.EstadoFlujos


                If estado_flujo = "s" Then

                    objResult &= "{"

                    objResult &= """actorsVal"": """
                    actorsVal = row.idthird

                    objResult &= actorsVal

                    objResult &= """, ""actorsName"": """
                    actorsName = row.Name

                    objResult &= actorsName

                    objResult &= """, ""tipoactors"": """
                    tipoactors = row.type

                    objResult &= tipoactors

                    objResult &= """, ""contact"": """
                    contact = row.contact

                    objResult &= contact

                    objResult &= """, ""cedula"": """
                    cedula = row.Documents

                    objResult &= cedula

                    objResult &= """, ""telefono"": """
                    telefono = row.Phone

                    objResult &= telefono

                    objResult &= """, ""email"": """
                    email = row.Email

                    objResult &= email

                    objResult &= """, ""diner"": """
                    diner = row.Vrmoney

                    objResult &= diner

                    objResult &= """, ""especie"": """
                    especie = row.VrSpecies

                    objResult &= especie

                    objResult &= """, ""total"": """
                    total = row.FSCorCounterpartContribution

                    objResult &= total

                    objResult &= """, ""estado_flujo"": """

                    objResult &= estado_flujo

                    If valuar_actor = data_listactores.Count Then

                        objResult &= """}"

                    Else
                        objResult &= """}|"

                    End If

                    valuar_actor = valuar_actor + 1


                End If
            Next
        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_flujos_project(ByVal idproject As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim flujopagos As New PaymentFlowDALC()
        Dim objflujos As PaymentFlowEntity
        Dim data_listpagos As New List(Of PaymentFlowEntity)

        Dim N_pago, fecha_pago, porcentaje, entrega, tflujos As String

        Dim valuar_flujo As Integer = 1
        Dim objResult As String

        data_listpagos = flujopagos.getFlowPayment("p", idproject, applicationCredentials)

        If data_listpagos.Count > 0 Then

            For Each row In data_listpagos

                objResult &= "{"

                objResult &= """N_pago"": """
                N_pago = row.N_pagos

                N_pago = Replace(N_pago, " ", "")
                objResult &= N_pago

                objResult &= """, ""fecha_pago"": """
                fecha_pago = row.fecha

                objResult &= fecha_pago

                objResult &= """, ""porcentaje"": """
                porcentaje = row.porcentaje

                objResult &= porcentaje

                objResult &= """, ""entrega"": """
                entrega = row.entregable

                objResult &= entrega

                objResult &= """, ""tflujos"": """
                tflujos = row.valorparcial
                tflujos = tflujos.Replace(" ", "")
                tflujos = Format(Convert.ToInt64(tflujos), "#,###.##")
                objResult &= tflujos

                If valuar_flujo = data_listpagos.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_flujo = valuar_flujo + 1

            Next
        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)
    End Function

    Protected Function searh_flujos_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim flujopagos As New PaymentFlowDALC()
        Dim objflujos As PaymentFlowEntity
        Dim data_listpagos As New List(Of PaymentFlowEntity)

        Dim N_pago, fecha_pago, porcentaje, entrega, tflujos As String

        Dim valuar_flujo As Integer = 1
        Dim objResult As String

        data_listpagos = flujopagos.getFlowPayment("i", ididea, applicationCredentials)

        If data_listpagos.Count > 0 Then

            For Each row In data_listpagos

                objResult &= "{"

                objResult &= """N_pago"": """
                N_pago = row.N_pagos

                N_pago = Replace(N_pago, " ", "")
                objResult &= N_pago

                objResult &= """, ""fecha_pago"": """
                fecha_pago = row.fecha

                objResult &= fecha_pago

                objResult &= """, ""porcentaje"": """
                porcentaje = row.porcentaje

                objResult &= porcentaje

                objResult &= """, ""entrega"": """
                entrega = row.entregable

                objResult &= entrega

                objResult &= """, ""tflujos"": """
                tflujos = row.valorparcial
                tflujos = tflujos.Replace(" ", "")
                tflujos = Format(Convert.ToInt64(tflujos), "#,###.##")
                objResult &= tflujos

                If valuar_flujo = data_listpagos.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_flujo = valuar_flujo + 1

            Next
        End If

        If objResult = "" Then

            objResult = "vacio"

        End If

        Response.Write(objResult)
    End Function

    Protected Sub searchIdea_inf_p(ByVal id As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials)

        Dim sql As New StringBuilder

        Dim data As DataTable
        Dim dataTotalFSC As DataTable
        Dim dataTotalNoFSC As DataTable
        Dim Objective As String = ""
        Dim Justification As String = ""
        Dim AreaDescription As String = ""
        Dim ResultsBenef As String = ""
        Dim ResultsKnowledgeManagement As String = ""
        Dim ResultsInstalledCapacity As String = ""
        Dim StartDate As String = ""
        Dim Duration As String = ""
        Dim OtherResults As String = ""
        Dim days As String = ""
        Dim ideaappliesIVA As String = ""
        Dim obligationsoftheparties As String = ""
        Dim RiskMitigation As String = ""
        Dim RisksIdentified As String = ""
        Dim BudgetRoute As String = ""
        Dim Population As String = ""
        Dim Idtypecontract As String = ""

        sql.Append("select  i.Objective, i.Justification, i.AreaDescription,  i.Results, i.ResultsKnowledgeManagement, i.ResultsInstalledCapacity, i.OtherResults , i.startdate, i.Duration, i.days, i.ideaappliesIVA, i.obligationsoftheparties, i.RiskMitigation, i.RisksIdentified, i.BudgetRoute, i.Population, i.Idtypecontract  from idea i where i.id= " & id)

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Dim objResult As String = "{"

            ' obtiene el objetivo de IDEA 
            objResult &= " ""Objective"": """
            If IsDBNull(data.Rows(0)("Objective")) = False Then
                Objective = data.Rows(0)("Objective")
                Objective = Objective.Replace("""", "\""")
            End If
            objResult &= Objective

            ' obtiene la justificacion de IDEA
            objResult &= """, ""Justification"": """
            If IsDBNull(data.Rows(0)("Justification")) = False Then
                Justification = data.Rows(0)("Justification")
                Justification = Justification.Replace("""", "\""")
            End If
            objResult &= Justification

            ' obtiene area descripcion en el campo equivale a objetivos especificos
            objResult &= """, ""AreaDescription"": """
            If IsDBNull(data.Rows(0)("AreaDescription")) = False Then
                AreaDescription = data.Rows(0)("AreaDescription")
                AreaDescription = AreaDescription.Replace("""", "\""")

            End If
            objResult &= AreaDescription

            ' obtiene resultados beneficiarios que equivale a results de la tabla
            objResult &= """, ""Results"": """
            If IsDBNull(data.Rows(0)("Results")) = False Then
                ResultsBenef = data.Rows(0)("Results")
                ResultsBenef = ResultsBenef.Replace("""", "\""")
            End If
            objResult &= ResultsBenef

            ' obtiene resultados gestion del conocimiento
            objResult &= """, ""ResultsKnowledgeManagement"": """
            If IsDBNull(data.Rows(0)("ResultsKnowledgeManagement")) = False Then
                ResultsKnowledgeManagement = data.Rows(0)("ResultsKnowledgeManagement")
                ResultsKnowledgeManagement = ResultsKnowledgeManagement.Replace("""", "\""")
            End If
            objResult &= ResultsKnowledgeManagement

            ' obtiene resultados de la capacidad instalada
            objResult &= """, ""ResultsInstalledCapacity"": """
            If IsDBNull(data.Rows(0)("ResultsInstalledCapacity")) = False Then
                ResultsInstalledCapacity = data.Rows(0)("ResultsInstalledCapacity")
                ResultsInstalledCapacity = ResultsInstalledCapacity.Replace("""", "\""")
            End If
            objResult &= ResultsInstalledCapacity

            objResult &= """, ""OtherResults"": """
            If IsDBNull(data.Rows(0)("OtherResults")) = False Then
                OtherResults = data.Rows(0)("OtherResults")
                OtherResults = OtherResults.Replace("""", "\""")
            End If
            objResult &= OtherResults

            ' obtiene fecha de inicio
            objResult &= """, ""StartDate"": """
            If IsDBNull(data.Rows(0)("StartDate")) = False Then
                StartDate = data.Rows(0)("StartDate")
            End If
            Dim dateFormated As Date = StartDate
            objResult &= dateFormated.ToString("yyyy/MM/dd")

            ' obtiene meses
            objResult &= """, ""Duration"": """
            If IsDBNull(data.Rows(0)("Duration")) = False Then
                Duration = data.Rows(0)("Duration")
            End If
            objResult &= Duration

            ' obtiene dias
            objResult &= """, ""days"": """
            If IsDBNull(data.Rows(0)("days")) = False Then
                days = data.Rows(0)("days")
            End If
            objResult &= days

            ' obtiene si tiene iva la idea
            objResult &= """, ""ideaappliesIVA"": """
            If IsDBNull(data.Rows(0)("ideaappliesIVA")) = False Then
                ideaappliesIVA = data.Rows(0)("ideaappliesIVA")
            End If
            objResult &= ideaappliesIVA

            ' obtiene si tiene obligaciones
            objResult &= """, ""obligationsoftheparties"": """
            If IsDBNull(data.Rows(0)("obligationsoftheparties")) = False Then
                obligationsoftheparties = data.Rows(0)("obligationsoftheparties")
            End If
            objResult &= obligationsoftheparties

            ' obtiene si tiene mitigaciones
            objResult &= """, ""RiskMitigation"": """
            If IsDBNull(data.Rows(0)("RiskMitigation")) = False Then
                RiskMitigation = data.Rows(0)("RiskMitigation")
            End If
            objResult &= RiskMitigation

            'obtiene si tiene riesgos
            objResult &= """, ""RisksIdentified"": """
            If IsDBNull(data.Rows(0)("RisksIdentified")) = False Then
                RisksIdentified = data.Rows(0)("RisksIdentified")
            End If
            objResult &= RisksIdentified

            'obtiene si tiene riesgos
            objResult &= """, ""BudgetRoute"": """
            If IsDBNull(data.Rows(0)("BudgetRoute")) = False Then
                BudgetRoute = data.Rows(0)("BudgetRoute")
            End If
            objResult &= BudgetRoute

            'obtiene si tiene poblacion
            objResult &= """, ""Population"": """
            If IsDBNull(data.Rows(0)("Population")) = False Then
                Population = data.Rows(0)("Population")
            End If
            objResult &= Population

            'obtiene si tiene tipo de contrato
            objResult &= """, ""Idtypecontract"": """
            If IsDBNull(data.Rows(0)("Idtypecontract")) = False Then
                Idtypecontract = data.Rows(0)("Idtypecontract")
            End If
            objResult &= Idtypecontract

            objResult &= """}"

            Response.Write(objResult)

        End If

    End Sub

    Protected Function searh_c_typecontract(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim populationvalue As String = ""


        sql.Append(" select i.Idtypecontract from  Idea i where i.id =" & ididea)

        Dim data_c_population = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_c_population = 0 Then
            populationvalue = "0"
        Else
            populationvalue = data_c_population
        End If

        Response.Write(populationvalue)

    End Function

    Protected Function searh_c_population(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim populationvalue As String = ""


        sql.Append(" select i.population from  Idea i where i.id =" & ididea)

        Dim data_c_population = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_c_population = 0 Then
            populationvalue = "0"
        Else
            populationvalue = data_c_population
        End If

        Response.Write(populationvalue)

    End Function

    ''' <summary>
    ''' funcion que carga el combo de tipo de poblacion segun la selección del tipo de proyecto
    ''' Autor: German Rodriguez MGgroup
    ''' 28-01-2014
    ''' </summary>
    ''' <param name="id_population"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function Charge_population(ByVal id_population As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select p.ID, p.pupulation  from  population p ")
        sql.Append(" where p.id_proyect_type = " & id_population)
        sql.Append(" order by p.id_proyect_type asc ")

        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String = "<option>Seleccione...</option>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)

    End Function

    Protected Function charge_idea_aproval()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append("select  Idea.Id, Idea.Code,Idea.Name,par.codeapprovedidea,  Idea.Code+'_'+Idea.Name as 'name_code' ")
        sql.Append("FROM Idea INNER JOIN IdeaApprovalRecord par ON idea.Id = par.Ididea ORDER BY par.CreateDate DESC ")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String = "<option>Seleccione...</option>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(4).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)


    End Function

    Protected Function calculafechas(ByVal fecha As DateTime, ByVal duracion As String, ByVal dias_ope As String) As String

        Dim objResult As String

        Try

            Dim arrdias() As String
            Dim decimas As String
            Dim dias As Double
            Dim meses As Double

            'Cambiar puntos por comas
            duracion = Replace(duracion, ".", ",", 1)

            'Calcular los dias
            arrdias = Split(duracion, ",", , CompareMethod.Text)

            If UBound(arrdias) > 0 Then
                decimas = "0," & arrdias(1)
                dias = CInt(decimas * 30)
                meses = CInt(arrdias(0))
            Else
                meses = duracion
                If dias_ope = "" Then
                    dias = 0
                Else
                    dias = dias_ope
                End If

            End If

            Dim fechafinal As Date
            'calcular la fecha final
            fechafinal = CDate(fecha)
            Dim tipointervalo As DateInterval
            tipointervalo = DateInterval.Day

            'Agregar los meses a la fecha
            Dim finalizacionpre As String = DateAdd(DateInterval.Month, meses, fechafinal)
            finalizacionpre = CDate(finalizacionpre)

            'Agregar los días a la fecha
            Dim finalizacion As String = DateAdd("d", dias, finalizacionpre)
            finalizacion = CDate(finalizacion)

            Dim quitadia As String = DateAdd("d", -1, finalizacion)

            Dim fechaok As DateTime = quitadia

            objResult = fechaok.ToString("yyyy/MM/dd")

        Catch ex As Exception

            objResult = ""

        End Try

        Response.Write(objResult)

    End Function

    Protected Function searh_actores_project(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim thirdbyproject As New ThirdByProjectDALC
        Dim objactores As ThirdByProjectEntity
        Dim data_listactores As List(Of ThirdByProjectEntity)
        Dim actorsVal, actorsName, tipoactors, contact, cedula, telefono, email, diner, especie, total, estado_flujo As String

        Dim htmlactores As String

        data_listactores = thirdbyproject.getList(applicationCredentials, , ididea, , , , , )

        Dim valuar_actor As Integer = 1
        Dim objResult As String

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                objResult &= "{"

                objResult &= """actorsVal"": """
                actorsVal = row.idthird

                objResult &= actorsVal

                objResult &= """, ""actorsName"": """
                actorsName = row.Name

                objResult &= actorsName

                objResult &= """, ""tipoactors"": """
                tipoactors = row.type

                objResult &= tipoactors

                objResult &= """, ""contact"": """
                contact = row.contact

                objResult &= contact

                objResult &= """, ""cedula"": """
                cedula = row.Documents

                objResult &= cedula

                objResult &= """, ""telefono"": """
                telefono = row.Phone

                objResult &= telefono

                objResult &= """, ""email"": """
                email = row.Email

                objResult &= email

                objResult &= """, ""diner"": """
                diner = row.Vrmoney

                objResult &= diner

                objResult &= """, ""especie"": """
                especie = row.VrSpecies

                objResult &= especie

                objResult &= """, ""total"": """
                total = row.FSCorCounterpartContribution

                objResult &= total

                objResult &= """, ""estado_flujo"": """
                estado_flujo = row.EstadoFlujos

                objResult &= estado_flujo

                If valuar_actor = data_listactores.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_actor = valuar_actor + 1
            Next

        End If

        Response.Write(objResult)


    End Function

    Protected Function searh_actores_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim objactores As ThirdByIdeaEntity
        Dim data_listactores As List(Of ThirdByIdeaEntity)
        Dim actorsVal, actorsName, tipoactors, contact, cedula, telefono, email, diner, especie, total, estado_flujo As String

        Dim htmlactores As String

        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        Dim valuar_actor As Integer = 1
        Dim objResult As String

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                objResult &= "{"

                objResult &= """actorsVal"": """
                actorsVal = row.idthird

                objResult &= actorsVal

                objResult &= """, ""actorsName"": """
                actorsName = row.Name

                objResult &= actorsName

                objResult &= """, ""tipoactors"": """
                tipoactors = row.type

                objResult &= tipoactors

                objResult &= """, ""contact"": """
                contact = row.contact

                objResult &= contact

                objResult &= """, ""cedula"": """
                cedula = row.Documents

                objResult &= cedula

                objResult &= """, ""telefono"": """
                telefono = row.Phone

                objResult &= telefono

                objResult &= """, ""email"": """
                email = row.Email

                objResult &= email

                objResult &= """, ""diner"": """
                diner = row.Vrmoney

                objResult &= diner

                objResult &= """, ""especie"": """
                especie = row.VrSpecies

                objResult &= especie

                objResult &= """, ""total"": """
                total = row.FSCorCounterpartContribution

                objResult &= total

                objResult &= """, ""estado_flujo"": """
                estado_flujo = row.EstadoFlujos

                objResult &= estado_flujo

                If valuar_actor = data_listactores.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_actor = valuar_actor + 1
            Next

        End If

        Response.Write(objResult)

    End Function

    Protected Function buscardatethird(ByVal bybal As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal idThird As Integer) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim contact As String = ""
        Dim documents As String = ""
        Dim phone As String = ""
        Dim email As String = ""
        Dim name As String = ""
        Dim idt As String = ""

        'consulta de los datos de actores por id
        sql.Append("SELECT Id,Code,Name,contact,documents,phone,email,Actions,Experiences,Enabled,IdUser,CreateDate  FROM Third ")
        sql.Append("where Id = " & idThird)
        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Dim objResult As String = "{"

            objResult &= " ""idthird"": """

            If IsDBNull(data.Rows(0)("id")) = False Then
                idt = data.Rows(0)("id")
            End If

            objResult &= idt
            objResult &= " "", ""name"": """

            If IsDBNull(data.Rows(0)("name")) = False Then
                name = data.Rows(0)("name")
            End If

            objResult &= name
            objResult &= " "", ""contact"": """

            If IsDBNull(data.Rows(0)("contact")) = False Then
                contact = data.Rows(0)("contact")
            End If

            objResult &= contact
            objResult &= """, ""documents"": """

            If IsDBNull(data.Rows(0)("documents")) = False Then
                documents = data.Rows(0)("documents")
            End If
            objResult &= documents
            objResult &= """, ""phone"": """

            If IsDBNull(data.Rows(0)("phone")) = False Then
                phone = data.Rows(0)("phone")
            End If

            objResult &= phone
            objResult &= """, ""email"": """

            If IsDBNull(data.Rows(0)("email")) = False Then
                email = data.Rows(0)("email")
            End If

            objResult &= email

            objResult &= """}"

            Response.Write(objResult)
        End If


    End Function

    ''' <summary>
    ''' funcion que carga el combo de tipo de componente de programa
    ''' Autor: German Rodriguez MGgroup
    ''' 12-01-2014
    ''' </summary>
    ''' <param name="idprogram"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function charge_component(ByVal idprogram As String, ByVal estado_proceso As Integer, ByVal ididea As Integer)

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim Data_programcomponent As List(Of ProgramComponentEntity)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim component_value As DataTable
        Dim id_component As String

        sql.Append("select IdProgramComponent from ProgramComponentByIdea  where IdIdea=" & ididea)
        component_value = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim htmlresults As String = ""
        Dim id, code As String
        Dim s_html As Integer = 0

        Data_programcomponent = facade.getProgramComponentList(applicationCredentials, idProgram:=idprogram, enabled:="1", order:="Code")

        For Each row In Data_programcomponent
            ' cargar el valor del campo
            id = row.id
            code = row.code

            If estado_proceso = 1 Then

                For Each row_comp As DataRow In component_value.Rows

                    id_component = row_comp(0).ToString()
                    If id = id_component Then
                        ' swichear en ves de crear el ul
                        s_html = 1
                    End If

                Next

                If s_html = 0 Then
                    htmlresults &= "<li id= add" + id + " class='seleccione'>" + code + "</li>"
                Else
                    s_html = 0
                End If

            Else
                htmlresults &= "<li id= add" + id + " class='seleccione'>" + code + "</li>"

            End If

        Next

        Response.Write(htmlresults)

    End Function

    Protected Function charge_list_program(ByVal idLinestrategic As Integer)
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim program_data As List(Of ProgramEntity)

        Dim htmlresults As String
        Dim id, code As String

        program_data = facade.getProgramList(applicationCredentials, idStrategicLine:=idLinestrategic, enabled:="1", order:="Code")

        For Each row In program_data
            ' cargar el valor del campo
            id = row.id
            code = row.code
            htmlresults &= "<li id= 'add" & id & "' class='seleccione_program'>" & code & "</li>"
        Next
        Response.Write(htmlresults)

    End Function

    Protected Function searh_component_project(ByVal idproject As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim component_value As DataTable

        Dim htmlcomponente As String = ""
        sql.Append(" SELECT  pc.Id ,pc.Code FROM ProgramComponentByProject pcp ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pcp.idProgramComponent = Pc.Id ")
        sql.Append(" INNER JOIN Program p ON Pc.IdProgram = P.Id ")
        sql.Append(" where pcp.IdProject = " & idproject)

        component_value = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If component_value.Rows.Count > 0 Then

            For Each row As DataRow In component_value.Rows
                htmlcomponente &= "<li id= 'selectadd" + row(0).ToString() + "' class='des_seleccionar'>" + row(1).ToString() + "</li>"
            Next

        End If

        Response.Write(htmlcomponente)
    End Function

    Protected Function searh_component(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim component_value As DataTable

        Dim htmlcomponente As String = ""
        sql.Append(" SELECT  pc.Id ,pc.Code FROM ProgramComponentByIdea pci ")
        sql.Append(" INNER JOIN ProgramComponent pc  ON pci.idProgramComponent = Pc.Id  ")
        sql.Append(" INNER JOIN Program p ON Pc.IdProgram = P.Id  ")
        sql.Append(" where pci.IdIdea = " & ididea)

        component_value = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If component_value.Rows.Count > 0 Then

            For Each row As DataRow In component_value.Rows
                htmlcomponente &= "<li id= 'selectadd" + row(0).ToString() + "' class='des_seleccionar'>" + row(1).ToString() + "</li>"
            Next

        End If

        Response.Write(htmlcomponente)

    End Function

    Protected Function searh_Program(ByVal ididea As Integer, ByVal estado_proceso As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim program_value As String = ""

        If estado_proceso = 0 Then

            sql.Append(" select top 1(pc.IdProgram) from ProgramComponentByIdea pci ")
            sql.Append(" INNER JOIN ProgramComponent pc ON pci.idProgramComponent = pc.Id ")
            sql.Append(" where pci.IdIdea = " & ididea)

        Else

            sql.Append(" select top 1(pc.IdProgram) from ProgramComponentByProject pcp ")
            sql.Append(" INNER JOIN ProgramComponent pc ON pcp.idProgramComponent = pc.Id ")
            sql.Append(" where pcp.IdProject = " & ididea)

        End If

        Dim data_program = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_program = 0 Then
            program_value = "0"
        Else
            program_value = data_program
        End If

        Response.Write(program_value)


    End Function

    Protected Function searh_line_Strategic_project(ByVal idproject As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim linevalue As String = ""

        sql.Append(" select TOP 1(SL.Id) from ProgramComponentByProject pcp  ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pcp.idProgramComponent = pc.Id ")
        sql.Append(" INNER JOIN Program p ON Pc.IdProgram = P.Id ")
        sql.Append(" INNER JOIN StrategicLine SL ON SL.Id = P.IdStrategicLine ")
        sql.Append(" where pcp.IdProject = " & idproject)

        Dim data_lineStrategig = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_lineStrategig = 0 Then
            linevalue = "0"
        Else
            linevalue = data_lineStrategig
        End If

        Response.Write(linevalue)
    End Function

    Protected Function searh_line_Strategic(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim linevalue As String = ""

        sql.Append(" select TOP 1(SL.Id) from ProgramComponentByIdea pci ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pci.idProgramComponent = pc.Id ")
        sql.Append(" INNER JOIN Program p ON Pc.IdProgram = P.Id ")
        sql.Append(" INNER JOIN StrategicLine SL ON SL.Id = P.IdStrategicLine ")
        sql.Append(" where pci.IdIdea = " & ididea)

        Dim data_lineStrategig = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_lineStrategig = 0 Then
            linevalue = "0"
        Else
            linevalue = data_lineStrategig
        End If

        Response.Write(linevalue)

    End Function

    ''' <summary>
    ''' funcion que carga el combo de programa precedido por la linea estrategicas seleccionada
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <param name="idLinestrategic"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function charge_program(ByVal idLinestrategic As Integer)
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim program_data As List(Of ProgramEntity)

        Dim htmlresults As String = "<option>Seleccione...</option>"
        Dim id, code As String

        program_data = facade.getProgramList(applicationCredentials, idStrategicLine:=idLinestrategic, enabled:="1", order:="Code")

        For Each row In program_data
            ' cargar el valor del campo
            id = row.id
            code = row.code
            htmlresults &= String.Format("<option value='{0}'>{1}</option>", id, code)

        Next
        Response.Write(htmlresults)

    End Function

    ''' <summary>
    ''' funcion que carga el combo de municipios precedido por el departamento seleccionada
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <param name="iddepto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function Charge_munip(ByVal iddepto As Integer)
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim data_munip As List(Of CityEntity)

        Dim htmlresults As String = "<option>Seleccione...</option>"
        Dim id, name As String

        data_munip = facade.getCityList(applicationCredentials, iddepto:=iddepto, enabled:="T", order:="City.Code")

        For Each row In data_munip
            ' cargar el valor del campo
            id = row.id
            name = row.name
            htmlresults &= String.Format("<option value='{0}'>{1}</option>", id, name)

        Next
        Response.Write(htmlresults)

    End Function

    Protected Function searh_location_project(ByVal idproject As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim LocationByproject As New ProjectLocationDALC
        Dim objlocation As ProjectLocationEntity
        Dim data_listlocation As List(Of ProjectLocationEntity)
        Dim DeptoVal, CityVal, CityName, DeptoName As String

        Dim objResult As String

        data_listlocation = LocationByproject.getList(applicationCredentials, , idproject, , )

        Dim valuar_ubi As Integer = 1

        If data_listlocation.Count > 0 Then

            For Each row In data_listlocation

                objResult &= "{"

                objResult &= """DeptoVal"": """
                DeptoVal = row.IDDEPTO
                objResult &= DeptoVal

                objResult &= """, ""DeptoName"": """
                DeptoName = row.DEPTONAME

                objResult &= DeptoName

                objResult &= """, ""CityVal"": """
                CityVal = row.idcity

                objResult &= CityVal

                objResult &= """, ""CityName"": """
                CityName = row.CITYNAME

                objResult &= CityName

                If valuar_ubi = data_listlocation.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_ubi = valuar_ubi + 1
            Next

        End If

        Response.Write(objResult)

    End Function

    Protected Function searh_location_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim LocationByIdea As New LocationByIdeaDALC
        Dim objlocation As LocationByIdeaEntity
        Dim data_listlocation As List(Of LocationByIdeaEntity)
        Dim DeptoVal, CityVal, CityName, DeptoName As String

        Dim objResult As String


        data_listlocation = LocationByIdea.getList(applicationCredentials, , ididea, , , )

        Dim valuar_ubi As Integer = 1

        If data_listlocation.Count > 0 Then


            For Each row In data_listlocation

                objResult &= "{"

                objResult &= """DeptoVal"": """
                DeptoVal = row.DEPTO.id
                objResult &= DeptoVal

                objResult &= """, ""DeptoName"": """
                DeptoName = row.DEPTO.name

                objResult &= DeptoName

                objResult &= """, ""CityVal"": """
                CityVal = row.CITY.id

                objResult &= CityVal

                objResult &= """, ""CityName"": """
                CityName = row.CITY.name

                objResult &= CityName

                If valuar_ubi = data_listlocation.Count Then

                    objResult &= """}"

                Else
                    objResult &= """}|"

                End If

                valuar_ubi = valuar_ubi + 1
            Next


        End If
        Response.Write(objResult)


    End Function

    Protected Function edit_PROYECTO(ByVal id As String, ByVal ididea As String, ByVal str_code As String, ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal otros_resul As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String, ByVal list_flujos As String, ByVal list_detalles_flujos As String, ByVal list_files As String, ByVal estado As String) '

        Dim facade As New Facade
        Dim objProject As New ProjectEntity
        Dim sOldFile As String = String.Empty

        Dim ProgramComponentByProjectList As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)

        '        Session("projectLocationList") = New List(Of ProjectLocationEntity)
        '       Session("thirdByProjectList") = New List(Of ThirdByProjectEntity)

        Dim arrayubicacion, arrayactor, arraycomponente, arrayflujos, arraydetallesflujos As String()
        Dim deptovalexist, Cityvalexist As Integer
        Dim desembolsoexist, Aportanteexist, idaportanteexist, N_pagodetexist, estados_flujosexist, N_pagoexist, fecha_pagoexist, porcentajeexist, entregaexist, tflujosexist, existidprogram, existactorsVal, existactorsName, existtipoactors, existcontact, existcedula, existtelefono, existemail, existdiner, existespecie, existtotal As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            list_ubicacion = Replace(list_ubicacion, "{", " ", 1)
            list_ubicacion = Replace(list_ubicacion, "}", " ", 1)
            list_ubicacion = Replace(list_ubicacion, """", " ", 1)
            'convertimos el string en un array de datos
            arrayubicacion = list_ubicacion.Split(New [Char]() {","c})

            list_actor = Replace(list_actor, "{", " ", 1)
            list_actor = Replace(list_actor, "}", " ", 1)
            list_actor = Replace(list_actor, """", " ", 1)
            'convertimos el string en un array de datos
            arrayactor = list_actor.Split(New [Char]() {","c})

            list_flujos = Replace(list_flujos, "{", " ", 1)
            list_flujos = Replace(list_flujos, "}", " ", 1)
            list_flujos = Replace(list_flujos, """", " ", 1)
            'convertimos el string en un array de datos
            arrayflujos = list_flujos.Split(New [Char]() {","c})

            list_detalles_flujos = Replace(list_detalles_flujos, "{", " ", 1)
            list_detalles_flujos = Replace(list_detalles_flujos, "}", " ", 1)
            list_detalles_flujos = Replace(list_detalles_flujos, """", " ", 1)
            'convertimos el string en un array de datos
            arraydetallesflujos = list_detalles_flujos.Split(New [Char]() {","c})

            list_componentes = Replace(list_componentes, "/", "*", 1)
            list_componentes = Replace(list_componentes, "_ *", "*", 1)
            ''convertimos el string en un array de datos
            arraycomponente = list_componentes.Split(New [Char]() {"*"c})

            Dim contador As Integer = 0
            Dim contadoractor As Integer = 0
            Dim contadorcomp As Integer = 0
            Dim contadorflu As Integer = 0
            Dim contadordetflu As Integer = 0

            'recorremos los componentes seleccionados
            For Each row In arraycomponente

                'istanciamos el objeto componente
                Dim myProgramComponentByproject As New ProgramComponentByProjectEntity


                If IsNumeric(arraycomponente(contadorcomp)) Then

                    myProgramComponentByproject.idProgramComponent = arraycomponente(contadorcomp)
                    ProgramComponentByProjectList.Add(myProgramComponentByproject)

                End If

                contadorcomp = contadorcomp + 1
            Next

            '----------------------------------------------------ubicaciones------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aubicacion As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aubicacion = arrayubicacion.Length

            'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
            For index_ubi As Integer = 0 To t_Aubicacion

                Dim objListProyectLocationsList As List(Of ProjectLocationEntity) = New List(Of ProjectLocationEntity)
                Dim objProjectLocation As ProjectLocationEntity = New ProjectLocationEntity()
                objListProyectLocationsList = (DirectCast(Session("projectLocationList"), List(Of ProjectLocationEntity)))

                Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
                Dim objCityEntity As CityEntity = New CityEntity()

                'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                deptovalexist = InStr(arrayubicacion(contador), "DeptoVal")
                Cityvalexist = InStr(arrayubicacion(contador + 2), "CityVal")

                'separamos el valor de campo
                deptovalexist = Replace(arrayubicacion(contador), " DeptoVal : ", " ", 1)
                Cityvalexist = Replace(arrayubicacion(contador + 2), "CityVal : ", " ", 1)

                'asignamos al objeto
                objProjectLocation.IDDEPTO = deptovalexist
                objProjectLocation.idcity = Cityvalexist

                'cargamos al list
                objListProyectLocationsList.Add(objProjectLocation)

                index_ubi = index_ubi + 4
                contador = contador + 4

                If contador <> index_ubi Then
                    index_ubi = contador
                End If

            Next

            '----------------------------------------------------actores------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aactor As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aactor = arrayactor.Length

            'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
            For index_act As Integer = 0 To t_Aactor

                Dim ThirdByProjectList As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()
                Dim ThirdByProject As ThirdByProjectEntity = New ThirdByProjectEntity()
                ThirdByProjectList = DirectCast(Session("thirdByProjectList"), List(Of ThirdByProjectEntity))

                'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                existactorsVal = InStr(arrayactor(contadoractor), "actorsVal") 'y
                existactorsName = InStr(arrayactor(contadoractor + 1), "actorsName") 'y
                existtipoactors = InStr(arrayactor(contadoractor + 2), "tipoactors")
                existcontact = InStr(arrayactor(contadoractor + 3), "contact") 'y
                existcedula = InStr(arrayactor(contadoractor + 4), "cedula") 'y
                existtelefono = InStr(arrayactor(contadoractor + 5), "telefono") 'y
                existemail = InStr(arrayactor(contadoractor + 6), "email") 'y
                existdiner = InStr(arrayactor(contadoractor + 7), "diner")
                existespecie = InStr(arrayactor(contadoractor + 8), "especie")
                existtotal = InStr(arrayactor(contadoractor + 9), "total")
                estados_flujosexist = InStr(arrayactor(contadoractor + 10), "estado_flujo")

                'separamos el valor de campo
                existactorsVal = Replace(arrayactor(contadoractor), " actorsVal : ", " ", 1)
                existactorsName = Replace(arrayactor(contadoractor + 1), "actorsName : ", " ", 1)
                existtipoactors = Replace(arrayactor(contadoractor + 2), "tipoactors : ", " ", 1)
                existcontact = Replace(arrayactor(contadoractor + 3), "contact : ", " ", 1)
                existcedula = Replace(arrayactor(contadoractor + 4), "cedula : ", " ", 1)
                existtelefono = Replace(arrayactor(contadoractor + 5), "telefono : ", " ", 1)
                existemail = Replace(arrayactor(contadoractor + 6), "email : ", " ", 1)
                existdiner = Replace(arrayactor(contadoractor + 7), "diner : ", " ", 1)
                existespecie = Replace(arrayactor(contadoractor + 8), "especie : ", " ", 1)
                existtotal = Replace(arrayactor(contadoractor + 9), "total : ", " ", 1)
                estados_flujosexist = Replace(arrayactor(contadoractor + 10), "estado_flujo : ", " ", 1)
                estados_flujosexist = estados_flujosexist.Replace(" ", "")
                'asignamos al objeto
                ThirdByProject.idthird = existactorsVal
                ThirdByProject.THIRD.name = existactorsName
                ThirdByProject.Name = existactorsName
                ThirdByProject.type = existtipoactors
                ThirdByProject.THIRD.contact = existcontact
                ThirdByProject.contact = existcontact
                ThirdByProject.THIRD.documents = existcedula
                ThirdByProject.Documents = existcedula
                ThirdByProject.THIRD.phone = existtelefono
                ThirdByProject.Phone = existtelefono
                ThirdByProject.THIRD.email = existemail
                ThirdByProject.Email = existemail
                ThirdByProject.Vrmoney = existdiner
                ThirdByProject.VrSpecies = existespecie
                ThirdByProject.FSCorCounterpartContribution = existtotal
                ThirdByProject.EstadoFlujos = estados_flujosexist
                ThirdByProject.CreateDate = Now

                'cargamos al list
                ThirdByProjectList.Add(ThirdByProject)

                contadoractor = contadoractor + 11
                index_act = index_act + 11

                If contadoractor <> index_act Then
                    index_act = contadoractor
                End If

            Next

            '----------------------------------------------------flujos------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aflujo As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aflujo = arrayflujos.Length


            If arrayflujos(0) = "vacio_ojo" Then

            Else

                'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
                For index_flu As Integer = 0 To t_Aflujo

                    Dim objpaymentFlow As PaymentFlowEntity = New PaymentFlowEntity()
                    Dim PaymentFlowList As List(Of PaymentFlowEntity)
                    PaymentFlowList = DirectCast(Session("paymentFlowList"), List(Of PaymentFlowEntity))

                    'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                    N_pagoexist = InStr(arrayflujos(contadorflu), "N_pago")
                    fecha_pagoexist = InStr(arrayflujos(contadorflu + 1), "fecha_pago")
                    porcentajeexist = InStr(arrayflujos(contadorflu + 2), "porcentaje")
                    entregaexist = InStr(arrayflujos(contadorflu + 3), "entrega")
                    tflujosexist = InStr(arrayflujos(contadorflu + 4), "tflujos")

                    'separamos el valor de campo
                    N_pagoexist = Replace(arrayflujos(contadorflu), " N_pago : ", " ", 1)
                    fecha_pagoexist = Replace(arrayflujos(contadorflu + 1), " fecha_pago : ", " ", 1)
                    porcentajeexist = Replace(arrayflujos(contadorflu + 2), " porcentaje : ", " ", 1)
                    porcentajeexist = porcentajeexist.Replace("%", "")
                    entregaexist = Replace(arrayflujos(contadorflu + 3), " entrega : ", " ", 1)
                    entregaexist = entregaexist.Replace("¬", ",")
                    tflujosexist = Replace(arrayflujos(contadorflu + 4), " tflujos : ", " ", 1)
                    tflujosexist = tflujosexist.Replace(".", "")
                    'asignamos al objeto
                    objpaymentFlow.N_pagos = N_pagoexist
                    objpaymentFlow.fecha = Convert.ToDateTime(fecha_pagoexist)
                    objpaymentFlow.porcentaje = porcentajeexist
                    objpaymentFlow.entregable = entregaexist
                    objpaymentFlow.valortotal = Convert.ToInt32(tflujosexist)
                    objpaymentFlow.valorparcial = Convert.ToInt32(tflujosexist)
                    objpaymentFlow.ididea = 0
                    objpaymentFlow.mother = 0
                    'cargamos al list
                    PaymentFlowList.Add(objpaymentFlow)

                    contadorflu = contadorflu + 5
                    index_flu = index_flu + 5

                    If contadorflu <> index_flu Then
                        index_flu = contadorflu
                    End If

                Next
            End If

            '----------------------------------------------------detallesflujos------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aflujodetalle As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aflujodetalle = arraydetallesflujos.Length

            If arraydetallesflujos(0) = "vacio_ojo" Then

            Else

                'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
                For index_fludet As Integer = 0 To t_Aflujodetalle

                    Dim objDetalleflujo As DetailedcashflowsEntity = New DetailedcashflowsEntity()
                    Dim listDetalleflujo As List(Of DetailedcashflowsEntity)
                    listDetalleflujo = (DirectCast(Session("DetailedcashflowsList"), List(Of DetailedcashflowsEntity)))

                    'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                    N_pagodetexist = InStr(arraydetallesflujos(contadordetflu), "idpago")
                    idaportanteexist = InStr(arraydetallesflujos(contadordetflu + 1), "idaportante")
                    Aportanteexist = InStr(arraydetallesflujos(contadordetflu + 2), "Aportante")
                    desembolsoexist = InStr(arraydetallesflujos(contadordetflu + 3), "desembolso")

                    'separamos el valor de campo
                    N_pagodetexist = Replace(arraydetallesflujos(contadordetflu), " idpago : ", " ", 1)
                    idaportanteexist = Replace(arraydetallesflujos(contadordetflu + 1), " idaportante : ", " ", 1)
                    Aportanteexist = Replace(arraydetallesflujos(contadordetflu + 2), " Aportante : ", " ", 1)
                    desembolsoexist = Replace(arraydetallesflujos(contadordetflu + 3), " desembolso : ", " ", 1)
                    desembolsoexist = desembolsoexist.Replace(".", "")

                    'asignamos al objeto
                    objDetalleflujo.N_pago = Convert.ToInt32(N_pagodetexist)
                    objDetalleflujo.IdAportante = Convert.ToInt32(idaportanteexist)
                    objDetalleflujo.Aportante = Aportanteexist
                    objDetalleflujo.Desembolso = desembolsoexist
                    objDetalleflujo.IdIdea = 0
                    objDetalleflujo.mother = 0

                    'cargamos al list
                    listDetalleflujo.Add(objDetalleflujo)

                    contadordetflu = contadordetflu + 4
                    index_fludet = index_fludet + 4

                    If contadordetflu <> index_fludet Then
                        index_fludet = contadordetflu
                    End If

                Next

            End If


            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objProject.ProgramComponentbyprojectlist = ProgramComponentByProjectList


            'buscamos el id del proyecto madre
            Dim idproyect_mother = search_id_project_mother(ididea)
            'lo asignamos al proyecto derivado
            objProject.Project_derivados = idproyect_mother

            objProject.id = id
            objProject.code = str_code

            objProject.ididea = ididea
            objProject.name = clean_vbCrLf(name)
            objProject.objective = clean_vbCrLf(objetive)
            objProject.begindate = fecha_i
            objProject.duration = mes
            objProject.zonedescription = clean_vbCrLf(obj_esp)
            objProject.population = poblacion
            objProject.totalcost = Convert.ToInt32(cost)
            objProject.results = clean_vbCrLf(resul_bef)
            objProject.createdate = Now
            objProject.iduser = applicationCredentials.UserID
            objProject.justification = clean_vbCrLf(justify)
            objProject.ResultsKnowledgeManagement = clean_vbCrLf(resul_ges_c)
            objProject.ResultsInstalledCapacity = clean_vbCrLf(resul_cap_i)
            objProject.OthersResults = clean_vbCrLf(otros_resul)
            objProject.idtypecontract = contratacion
            objProject.Typeapproval = estado
            objProject.Obligaciones = obligaciones
            objProject.mitigacion = mitigacion
            objProject.riesgos = riesgos
            objProject.presupuestal = presupuestal
            objProject.dia = dia
            objProject.iva = iva
            objProject.mother = 0

            objProject.completiondate = Convert.ToDateTime(fecha_f)

            objProject.effectivebudget = ""
            objProject.idphase = "1"
            objProject.strategicdescription = ""
            objProject.attachment = ""
            objProject.purpose = ""
            objProject.source = ""

            'Se garega la lista de ubicaciones agregada
            objProject.projectlocationlist = DirectCast(Session("projectLocationList"), List(Of ProjectLocationEntity))
            'objIdea.LOCATIONBYIDEALIST = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

            'Se agrega la lista de terceros agregada
            objProject.thirdbyprojectlist = DirectCast(Session("thirdByProjectList"), List(Of ThirdByProjectEntity))
            'objIdea.THIRDBYIDEALIST = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

            'Se agrega la lista de FLUJOS DE PAGOS
            objProject.paymentflowByProjectList = DirectCast(Session("paymentFlowList"), List(Of PaymentFlowEntity))

            'Se agrega la lista de  detalles de FLUJOS DE PAGOS
            objProject.DetailedcashflowsbyProjectList = DirectCast(Session("DetailedcashflowsList"), List(Of DetailedcashflowsEntity))


            facade.updateProject(applicationCredentials, objProject, sOldFile)

            delete_documents(objProject.id)
            save_document_PROYECTO(list_files, objProject.id)

            Dim Result As String

            If objProject.id <> 0 Then

                Result = "Proyecto se modifico correctamente!"
                Response.Write(Result)

            Else

                Result = "Se perdio la conexion al guardar los datos del Proyecto"
                Response.Write(Result)

            End If


        Catch ex As Exception

        End Try


    End Function

    Protected Function delete_documents(ByVal id_project As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim dtData, dtDatadoc As DataTable

        sql.Append(" delete Documents where id in (select iddocuments from DocumentsByEntity where EntityName = 'ProjectEntity' and  IdnEntity = " & id_project & ")")
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" delete DocumentsByEntity where EntityName = 'ProjectEntity' and IdnEntity = " & id_project)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

    End Function

    Protected Function save_PROYECTO(ByVal ididea As String, ByVal str_code As String, ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal otros_resul As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String, ByVal list_flujos As String, ByVal list_detalles_flujos As String, ByVal list_files As String, ByVal estado As String) '

        Dim facade As New Facade
        Dim objProject As New ProjectEntity

        Dim ProgramComponentByProjectList As List(Of ProgramComponentByProjectEntity) = New List(Of ProgramComponentByProjectEntity)

        '        Session("projectLocationList") = New List(Of ProjectLocationEntity)
        '       Session("thirdByProjectList") = New List(Of ThirdByProjectEntity)

        Dim arrayubicacion, arrayactor, arraycomponente, arrayflujos, arraydetallesflujos As String()
        Dim deptovalexist, Cityvalexist As Integer
        Dim desembolsoexist, Aportanteexist, idaportanteexist, N_pagodetexist, estados_flujosexist, N_pagoexist, fecha_pagoexist, porcentajeexist, entregaexist, tflujosexist, existidprogram, existactorsVal, existactorsName, existtipoactors, existcontact, existcedula, existtelefono, existemail, existdiner, existespecie, existtotal As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try

            list_ubicacion = Replace(list_ubicacion, "{", " ", 1)
            list_ubicacion = Replace(list_ubicacion, "}", " ", 1)
            list_ubicacion = Replace(list_ubicacion, """", " ", 1)
            'convertimos el string en un array de datos
            arrayubicacion = list_ubicacion.Split(New [Char]() {","c})

            list_actor = Replace(list_actor, "{", " ", 1)
            list_actor = Replace(list_actor, "}", " ", 1)
            list_actor = Replace(list_actor, """", " ", 1)
            'convertimos el string en un array de datos
            arrayactor = list_actor.Split(New [Char]() {","c})

            list_flujos = Replace(list_flujos, "{", " ", 1)
            list_flujos = Replace(list_flujos, "}", " ", 1)
            list_flujos = Replace(list_flujos, """", " ", 1)
            'convertimos el string en un array de datos
            arrayflujos = list_flujos.Split(New [Char]() {","c})

            list_detalles_flujos = Replace(list_detalles_flujos, "{", " ", 1)
            list_detalles_flujos = Replace(list_detalles_flujos, "}", " ", 1)
            list_detalles_flujos = Replace(list_detalles_flujos, """", " ", 1)
            'convertimos el string en un array de datos
            arraydetallesflujos = list_detalles_flujos.Split(New [Char]() {","c})

            list_componentes = Replace(list_componentes, "/", "*", 1)
            list_componentes = Replace(list_componentes, "_ *", "*", 1)
            ''convertimos el string en un array de datos
            arraycomponente = list_componentes.Split(New [Char]() {"*"c})

            Dim contador As Integer = 0
            Dim contadoractor As Integer = 0
            Dim contadorcomp As Integer = 0
            Dim contadorflu As Integer = 0
            Dim contadordetflu As Integer = 0

            'recorremos los componentes seleccionados
            For Each row In arraycomponente

                'istanciamos el objeto componente
                Dim myProgramComponentByproject As New ProgramComponentByProjectEntity

                If IsNumeric(arraycomponente(contadorcomp)) Then

                    myProgramComponentByproject.idProgramComponent = arraycomponente(contadorcomp)
                    ProgramComponentByProjectList.Add(myProgramComponentByproject)

                End If

                contadorcomp = contadorcomp + 1
            Next

            '----------------------------------------------------ubicaciones------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aubicacion As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aubicacion = arrayubicacion.Length

            'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
            For index_ubi As Integer = 0 To t_Aubicacion

                Dim objListProyectLocationsList As List(Of ProjectLocationEntity) = New List(Of ProjectLocationEntity)
                Dim objProjectLocation As ProjectLocationEntity = New ProjectLocationEntity()
                objListProyectLocationsList = (DirectCast(Session("projectLocationList"), List(Of ProjectLocationEntity)))

                Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
                Dim objCityEntity As CityEntity = New CityEntity()

                'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                deptovalexist = InStr(arrayubicacion(contador), "DeptoVal")
                Cityvalexist = InStr(arrayubicacion(contador + 2), "CityVal")

                'separamos el valor de campo
                deptovalexist = Replace(arrayubicacion(contador), " DeptoVal : ", " ", 1)
                Cityvalexist = Replace(arrayubicacion(contador + 2), "CityVal : ", " ", 1)

                'asignamos al objeto
                objProjectLocation.IDDEPTO = deptovalexist
                objProjectLocation.idcity = Cityvalexist

                'cargamos al list
                objListProyectLocationsList.Add(objProjectLocation)

                index_ubi = index_ubi + 4
                contador = contador + 4

                If contador <> index_ubi Then
                    index_ubi = contador
                End If

            Next

            '----------------------------------------------------actores------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aactor As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aactor = arrayactor.Length

            'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
            For index_act As Integer = 0 To t_Aactor

                Dim ThirdByProjectList As List(Of ThirdByProjectEntity) = New List(Of ThirdByProjectEntity)()
                Dim ThirdByProject As ThirdByProjectEntity = New ThirdByProjectEntity()
                ThirdByProjectList = DirectCast(Session("thirdByProjectList"), List(Of ThirdByProjectEntity))

                'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                existactorsVal = InStr(arrayactor(contadoractor), "actorsVal") 'y
                existactorsName = InStr(arrayactor(contadoractor + 1), "actorsName") 'y
                existtipoactors = InStr(arrayactor(contadoractor + 2), "tipoactors")
                existcontact = InStr(arrayactor(contadoractor + 3), "contact") 'y
                existcedula = InStr(arrayactor(contadoractor + 4), "cedula") 'y
                existtelefono = InStr(arrayactor(contadoractor + 5), "telefono") 'y
                existemail = InStr(arrayactor(contadoractor + 6), "email") 'y
                existdiner = InStr(arrayactor(contadoractor + 7), "diner")
                existespecie = InStr(arrayactor(contadoractor + 8), "especie")
                existtotal = InStr(arrayactor(contadoractor + 9), "total")
                estados_flujosexist = InStr(arrayactor(contadoractor + 10), "estado_flujo")

                'separamos el valor de campo
                existactorsVal = Replace(arrayactor(contadoractor), " actorsVal : ", " ", 1)
                existactorsName = Replace(arrayactor(contadoractor + 1), "actorsName : ", " ", 1)
                existtipoactors = Replace(arrayactor(contadoractor + 2), "tipoactors : ", " ", 1)
                existcontact = Replace(arrayactor(contadoractor + 3), "contact : ", " ", 1)
                existcedula = Replace(arrayactor(contadoractor + 4), "cedula : ", " ", 1)
                existtelefono = Replace(arrayactor(contadoractor + 5), "telefono : ", " ", 1)
                existemail = Replace(arrayactor(contadoractor + 6), "email : ", " ", 1)
                existdiner = Replace(arrayactor(contadoractor + 7), "diner : ", " ", 1)
                existespecie = Replace(arrayactor(contadoractor + 8), "especie : ", " ", 1)
                existtotal = Replace(arrayactor(contadoractor + 9), "total : ", " ", 1)
                estados_flujosexist = Replace(arrayactor(contadoractor + 10), "estado_flujo : ", " ", 1)
                estados_flujosexist = estados_flujosexist.Replace(" ", "")
                'asignamos al objeto
                ThirdByProject.idthird = existactorsVal
                ThirdByProject.THIRD.name = existactorsName
                ThirdByProject.Name = existactorsName
                ThirdByProject.type = existtipoactors
                ThirdByProject.THIRD.contact = existcontact
                ThirdByProject.contact = existcontact
                ThirdByProject.THIRD.documents = existcedula
                ThirdByProject.Documents = existcedula
                ThirdByProject.THIRD.phone = existtelefono
                ThirdByProject.Phone = existtelefono
                ThirdByProject.THIRD.email = existemail
                ThirdByProject.Email = existemail
                ThirdByProject.Vrmoney = existdiner
                ThirdByProject.VrSpecies = existespecie
                ThirdByProject.FSCorCounterpartContribution = existtotal
                ThirdByProject.EstadoFlujos = estados_flujosexist
                ThirdByProject.CreateDate = Now

                'cargamos al list
                ThirdByProjectList.Add(ThirdByProject)

                contadoractor = contadoractor + 11
                index_act = index_act + 11

                If contadoractor <> index_act Then
                    index_act = contadoractor
                End If

            Next

            '----------------------------------------------------flujos------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aflujo As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aflujo = arrayflujos.Length


            If arrayflujos(0) = "vacio_ojo" Then

            Else

                'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
                For index_flu As Integer = 0 To t_Aflujo

                    Dim objpaymentFlow As PaymentFlowEntity = New PaymentFlowEntity()
                    Dim PaymentFlowList As List(Of PaymentFlowEntity)
                    PaymentFlowList = DirectCast(Session("paymentFlowList"), List(Of PaymentFlowEntity))

                    'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                    N_pagoexist = InStr(arrayflujos(contadorflu), "N_pago")
                    fecha_pagoexist = InStr(arrayflujos(contadorflu + 1), "fecha_pago")
                    porcentajeexist = InStr(arrayflujos(contadorflu + 2), "porcentaje")
                    entregaexist = InStr(arrayflujos(contadorflu + 3), "entrega")
                    tflujosexist = InStr(arrayflujos(contadorflu + 4), "tflujos")

                    'separamos el valor de campo
                    N_pagoexist = Replace(arrayflujos(contadorflu), " N_pago : ", " ", 1)
                    fecha_pagoexist = Replace(arrayflujos(contadorflu + 1), " fecha_pago : ", " ", 1)
                    porcentajeexist = Replace(arrayflujos(contadorflu + 2), " porcentaje : ", " ", 1)
                    porcentajeexist = porcentajeexist.Replace("%", "")
                    entregaexist = Replace(arrayflujos(contadorflu + 3), " entrega : ", " ", 1)
                    entregaexist = entregaexist.Replace("¬", ",")
                    tflujosexist = Replace(arrayflujos(contadorflu + 4), " tflujos : ", " ", 1)
                    tflujosexist = tflujosexist.Replace(".", "")
                    'asignamos al objeto
                    objpaymentFlow.N_pagos = N_pagoexist
                    objpaymentFlow.fecha = Convert.ToDateTime(fecha_pagoexist)
                    objpaymentFlow.porcentaje = porcentajeexist
                    objpaymentFlow.entregable = entregaexist
                    objpaymentFlow.valortotal = Convert.ToInt32(tflujosexist)
                    objpaymentFlow.valorparcial = Convert.ToInt32(tflujosexist)
                    objpaymentFlow.ididea = 0
                    objpaymentFlow.mother = 0
                    'cargamos al list
                    PaymentFlowList.Add(objpaymentFlow)

                    contadorflu = contadorflu + 5
                    index_flu = index_flu + 5

                    If contadorflu <> index_flu Then
                        index_flu = contadorflu
                    End If

                Next
            End If

            '----------------------------------------------------detallesflujos------------------------------------------------------------------------
            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_Aflujodetalle As Integer

            'ASIGNAMOS EL TAMAÑO 
            t_Aflujodetalle = arraydetallesflujos.Length

            If arraydetallesflujos(0) = "vacio_ojo" Then

            Else

                'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
                For index_fludet As Integer = 0 To t_Aflujodetalle

                    Dim objDetalleflujo As DetailedcashflowsEntity = New DetailedcashflowsEntity()
                    Dim listDetalleflujo As List(Of DetailedcashflowsEntity)
                    listDetalleflujo = (DirectCast(Session("DetailedcashflowsList"), List(Of DetailedcashflowsEntity)))

                    'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                    N_pagodetexist = InStr(arraydetallesflujos(contadordetflu), "idpago")
                    idaportanteexist = InStr(arraydetallesflujos(contadordetflu + 1), "idaportante")
                    Aportanteexist = InStr(arraydetallesflujos(contadordetflu + 2), "Aportante")
                    desembolsoexist = InStr(arraydetallesflujos(contadordetflu + 3), "desembolso")

                    'separamos el valor de campo
                    N_pagodetexist = Replace(arraydetallesflujos(contadordetflu), " idpago : ", " ", 1)
                    idaportanteexist = Replace(arraydetallesflujos(contadordetflu + 1), " idaportante : ", " ", 1)
                    Aportanteexist = Replace(arraydetallesflujos(contadordetflu + 2), " Aportante : ", " ", 1)
                    desembolsoexist = Replace(arraydetallesflujos(contadordetflu + 3), " desembolso : ", " ", 1)
                    desembolsoexist = desembolsoexist.Replace(".", "")

                    'asignamos al objeto
                    objDetalleflujo.N_pago = Convert.ToInt32(N_pagodetexist)
                    objDetalleflujo.IdAportante = Convert.ToInt32(idaportanteexist)
                    objDetalleflujo.Aportante = Aportanteexist
                    objDetalleflujo.Desembolso = desembolsoexist
                    objDetalleflujo.IdIdea = 0
                    objDetalleflujo.mother = 0

                    'cargamos al list
                    listDetalleflujo.Add(objDetalleflujo)

                    contadordetflu = contadordetflu + 4
                    index_fludet = index_fludet + 4

                    If contadordetflu <> index_fludet Then
                        index_fludet = contadordetflu
                    End If

                Next

            End If


            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objProject.ProgramComponentbyprojectlist = ProgramComponentByProjectList

            'buscamos el id del proyecto madre
            Dim idproyect_mother = search_id_project_mother(ididea)
            'lo asignamos al proyecto derivado
            objProject.Project_derivados = idproyect_mother

            objProject.code = str_code

            objProject.ididea = ididea
            objProject.name = clean_vbCrLf(name)
            objProject.objective = clean_vbCrLf(objetive)
            objProject.begindate = fecha_i
            objProject.duration = mes
            objProject.zonedescription = clean_vbCrLf(obj_esp)
            objProject.population = poblacion
            objProject.totalcost = Convert.ToInt32(cost)
            objProject.results = clean_vbCrLf(resul_bef)
            objProject.createdate = Now
            objProject.iduser = applicationCredentials.UserID
            objProject.justification = clean_vbCrLf(justify)
            objProject.ResultsKnowledgeManagement = clean_vbCrLf(resul_ges_c)
            objProject.ResultsInstalledCapacity = clean_vbCrLf(resul_cap_i)
            objProject.OthersResults = clean_vbCrLf(otros_resul)
            objProject.idtypecontract = contratacion
            objProject.Typeapproval = estado
            objProject.Obligaciones = obligaciones
            objProject.mitigacion = mitigacion
            objProject.riesgos = riesgos
            objProject.presupuestal = presupuestal
            objProject.dia = dia
            objProject.iva = iva
            objProject.mother = 0

            objProject.completiondate = Convert.ToDateTime(fecha_f)

            objProject.effectivebudget = ""
            objProject.idphase = "1"
            objProject.strategicdescription = ""
            objProject.attachment = ""
            objProject.purpose = ""
            objProject.source = ""

            'Se garega la lista de ubicaciones agregada
            objProject.projectlocationlist = DirectCast(Session("projectLocationList"), List(Of ProjectLocationEntity))
            'objIdea.LOCATIONBYIDEALIST = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

            'Se agrega la lista de terceros agregada
            objProject.thirdbyprojectlist = DirectCast(Session("thirdByProjectList"), List(Of ThirdByProjectEntity))
            'objIdea.THIRDBYIDEALIST = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

            'Se agrega la lista de FLUJOS DE PAGOS
            objProject.paymentflowByProjectList = DirectCast(Session("paymentFlowList"), List(Of PaymentFlowEntity))

            'Se agrega la lista de  detalles de FLUJOS DE PAGOS
            objProject.DetailedcashflowsbyProjectList = DirectCast(Session("DetailedcashflowsList"), List(Of DetailedcashflowsEntity))

            'almacenar la entidad
            objProject.id = facade.addProject(applicationCredentials, objProject)

            update_proyect(objProject.id, str_code)

            save_document_PROYECTO(list_files, objProject.id)


            Dim Result As String

            If objProject.id <> 0 Then

                Result = "Proyecto se guardó correctamente!"
                Response.Write(Result)

            Else

                Result = "Se perdio la conexion al guardar los datos del Proyecto"
                Response.Write(Result)

            End If

        Catch ex As Exception

            Dim Result As String
            Result = "Error inesperado por favor consulte el administrador - " & ex.Message
            Response.Write(Result)

        End Try


    End Function

    Private Function search_id_project_mother(ByVal ididea As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        sql.Append(" select id from project where Mother = 1 and IdIdea = " & ididea)

        Dim id_mother = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Return id_mother

    End Function

    Private Function update_proyect(ByVal idproyect As String, ByVal code_ini As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim code_act = code_ini & "_" & idproyect

        sql.Append(" update project set code = '" & code_act & "' where id =" & idproyect)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)


    End Function

    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)
        Dim comillas As String

        Return result

    End Function

    Protected Function save_document_PROYECTO(ByVal list_file As String, ByVal idPROYECTO As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ArrayFile As String()

        list_file = Replace(list_file, "{", " ", 1)
        list_file = Replace(list_file, "}", " ", 1)
        list_file = Replace(list_file, """", " ", 1)
        'convertimos el string en un array de datos
        ArrayFile = list_file.Split(New [Char]() {","c})

        If ArrayFile(0) <> "vacio_ojo" Then

            'ISTANCIAMOS LA VARIABLE DEL TAMAÑO DEL ARRAY
            Dim t_file As Integer
            Dim contador As Integer = 0

            Dim idfileexist, filenameexist, Descriptionexist As String

            'ASIGNAMOS EL TAMAÑO 
            t_file = ArrayFile.Length

            'RECORREMOS LA CANTIDAD DE VECES ASIGNADAS
            For index_ubi As Integer = 0 To t_file

                Dim objDocument As DocumentsEntity = New DocumentsEntity()
                Dim objDocumentbyEntity As DocumentsByEntityEntity = New DocumentsByEntityEntity()

                'VERIDFICAMOS Q EXISTAN LOS CAMPOS SOLICITADOS
                idfileexist = InStr(ArrayFile(contador), "idfile")
                filenameexist = InStr(ArrayFile(contador + 1), "filename")
                Descriptionexist = InStr(ArrayFile(contador + 2), "Description")

                'separamos el valor de campo
                idfileexist = Replace(ArrayFile(contador), " idfile :", " ", 1)
                filenameexist = Replace(ArrayFile(contador + 1), "filename : ", "", 1)
                Descriptionexist = Replace(ArrayFile(contador + 2), "Description : ", " ", 1)
                Descriptionexist = Descriptionexist.Replace("¬", ",")

                'asignamos al objeto
                objDocument.title = filenameexist
                objDocument.description = Descriptionexist
                objDocument.ideditedfor = 0
                objDocument.iddocumenttype = 0
                objDocument.idvisibilitylevel = 0
                objDocument.createdate = Now
                objDocument.iduser = applicationCredentials.UserID
                objDocument.attachfile = filenameexist
                objDocument.enabled = 1
                objDocument.Id_Entity_Zone = "ProjectEntity_" & idPROYECTO
                objDocument.Id_document = Convert.ToInt32(idfileexist)

                'cargamos al list
                Dim sql As New StringBuilder
                Dim dtData, dtDatadoc As DataTable

                ' construir la sentencia
                sql.AppendLine("INSERT INTO Documents(title,description,ideditedfor,idvisibilitylevel,iddocumenttype,createdate,iduser,attachfile,enabled,Id_Entity_Zone,Id_document ) ")
                sql.AppendLine(" VALUES (")
                sql.AppendLine("'" & objDocument.title & "',")
                sql.AppendLine("'" & objDocument.description & "',")
                sql.AppendLine("'" & objDocument.ideditedfor & "',")
                sql.AppendLine("'" & objDocument.idvisibilitylevel & "',")
                sql.AppendLine("'" & objDocument.iddocumenttype & "',")
                sql.AppendLine("'" & objDocument.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
                sql.AppendLine("'" & objDocument.iduser & "',")
                sql.AppendLine("'" & objDocument.attachfile & "',")
                sql.AppendLine("'" & objDocument.enabled & "',")
                sql.AppendLine("'" & objDocument.Id_Entity_Zone & "',")
                sql.AppendLine("'" & objDocument.Id_document & "')")

                ' intruccion para obtener el registro insertado
                sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

                'obtener el id
                dtData = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                ' id creado
                Dim idEntity As Long = CLng(dtData.Rows(0)("Id"))

                ' finalizar la transaccion
                CtxSetComplete()

                'asignamos al objeto
                objDocumentbyEntity.iddocuments = idEntity
                objDocumentbyEntity.idnentity = idPROYECTO
                objDocumentbyEntity.entityname = "ProjectEntity"

                sql = New StringBuilder

                ' construir la sentencia
                sql.AppendLine("INSERT INTO DocumentsByEntity( iddocuments,idnentity,entityName) ")
                sql.AppendLine("VALUES (")
                sql.AppendLine("'" & objDocumentbyEntity.iddocuments & "',")
                sql.AppendLine("'" & objDocumentbyEntity.idnentity & "',")
                sql.AppendLine("'" & objDocumentbyEntity.entityname & "')")

                ' intruccion para obtener el registro insertado
                sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

                'obtener el id
                dtDatadoc = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

                ' id creado
                Dim num As Long = CLng(dtDatadoc.Rows(0)("Id"))

                ' finalizar la transaccion
                CtxSetComplete()

                index_ubi = index_ubi + 3
                contador = contador + 3

            Next

        End If

    End Function

    Protected Function borrar_archivos()
        'Dim startinfo As New ProcessStartInfo("C:\Gattaca_pruebas\WebSiteFSC\ELVIRA-F3\FSC_APP\FSC_APP\bats\BORRAR_ARC.bat")
        Dim startinfo As New ProcessStartInfo(Server.MapPath("\bats\BORRAR_ARC.bat"))
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startinfo)
    End Function

    Protected Function copiar_archivos()
        ' Dim startinfo As New ProcessStartInfo("C:\Gattaca_pruebas\WebSiteFSC\ELVIRA-F3\FSC_APP\FSC_APP\bats\COPIAR_ARC.bat")
        Dim startinfo As New ProcessStartInfo(Server.MapPath("\bats\COPIAR_ARC.bat"))
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startinfo)
    End Function


End Class