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


Partial Class ResearchAndDevelopment_AjaxAddIdea
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion, dia As String
        Dim type_i_p, idprogram_list, S_type_aproval, S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_mitigacion, S_riesgos, S_presupuestal, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles As String
        Dim estado_proceso, ideditar, id_lineStrategic, id_depto, idprogram, idpopulation, Countarchivo As Integer

        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files
        Dim DocumentsTmpList As New List(Of DocumentstmpEntity)

        Session("locationByIdeaList") = New List(Of LocationByIdeaEntity)
        Session("thirdByIdeaList") = New List(Of ThirdByIdeaEntity)
        Session("paymentFlowList") = New List(Of PaymentFlowEntity)
        Session("DetailedcashflowsList") = New List(Of DetailedcashflowsEntity)

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
            Dim option_idea = Request.Form("action")

            Select Case option_idea

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
                    S_type_aproval = Request.Form("tipo_estado").ToString

                    save_IDEA(S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles, S_type_aproval) '

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
                    S_code = Request.Form("code").ToString
                    S_type_aproval = Request.Form("tipo_estado").ToString

                    edit_IDEA(S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles, S_type_aproval) '

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

                    If Request.QueryString("id") <> Nothing Then
                        ideditar = Convert.ToInt32(Request.QueryString("id").ToString)
                        charge_component(idprogram_list, estado_proceso, ideditar)
                    End If

                Case "View_line_strategic"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_line_Strategic(ideditar)

                Case "View_program"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_Program(ideditar)

                Case "View_component"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_component(ideditar)

                Case "View_componentes_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_component_array(ideditar)

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

                Case "Ctypaproval_view"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_c_typeaproval(ideditar)

                    '----------------- modulo ubicacion-------------------------------------------------------
                Case "C_munip"

                    id_depto = Convert.ToInt32(Request.QueryString("iddepto").ToString)
                    Charge_munip(id_depto)

                Case "View_ubicacion_array"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_location_array(ideditar)

                    '----------------- modulo actores-------------------------------------------------------
                Case "buscar"
                    'convierte la variable y llama funcion para la validacion de la idea
                    id_b = Convert.ToInt32(Request.QueryString("id").ToString())
                    buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))

                Case "View_actores_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_actores_array(ideditar)

                    '----------------- modulo flujos-------------------------------------------------------
                Case "View_flujos_p_array"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_flujos_array(ideditar)

                Case "View_flujos_actors_array"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_actors_flujos_array(ideditar)

                Case "View_detalle_flujo_array"

                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_detalles_array(ideditar)

                    '----------------- modulo anexos-------------------------------------------------------
                Case "View_anexos_array"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    searh_document_anexos_array(ideditar)

                    '----------------- tareas generales-------------------------------------------------------
                Case "load_combos"
                    type_i_p = Request.QueryString("type").ToString
                    load_combos(type_i_p)

                Case "Charge_combos_edit"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    charge_combos_edit(ideditar)

                Case "aprobacion_idea"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    validar_aprobacion_idea(ideditar)

                Case "load_idarchive"
                    ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                    load_id_archive(ideditar)

                Case "borrar_archivos"
                    borrar_archivos()

                Case "copiar_archivos"
                    copiar_archivos()

                Case Else

            End Select

        End If

    End Sub

    Protected Function charge_combos_edit(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim type_aproval_value As String = ""
        Dim typecontractvalue As String = ""
        Dim populationvalue As String = ""
        Dim program_value As String = ""
        Dim linevalue As String = ""
        
        sql.Append(" select i.typeapproval from idea i where i.id =" & ididea)
        Dim data_c_typeaproval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder
        sql.Append(" select i.Idtypecontract from  Idea i where i.id =" & ididea)
        Dim data_c_typecontract = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder
        sql.Append(" select i.population from Idea i where i.id =" & ididea)
        Dim data_c_population = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder
        sql.Append(" select top 1(pc.IdProgram) from ProgramComponentByIdea pci ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pci.idProgramComponent = pc.Id ")
        sql.Append(" where pci.IdIdea = " & ididea)
        Dim data_program = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        sql = New StringBuilder
        sql.Append(" select TOP 1(SL.Id) from ProgramComponentByIdea pci ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pci.idProgramComponent = pc.Id ")
        sql.Append(" INNER JOIN Program p ON Pc.IdProgram = P.Id ")
        sql.Append(" INNER JOIN StrategicLine SL ON SL.Id = P.IdStrategicLine ")
        sql.Append(" where pci.IdIdea = " & ididea)
        Dim data_lineStrategig = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        type_aproval_value = validate_date_consult(data_c_typeaproval)
        populationvalue = validate_date_consult(data_c_population)
        typecontractvalue = validate_date_consult(data_c_typecontract)
        program_value = validate_date_consult(data_program)
        linevalue = validate_date_consult(data_lineStrategig)

        Dim objCatalogSerialize = String.Format("[{0},{1},{2},{3},{4}]", linevalue, program_value, populationvalue, type_aproval_value, typecontractvalue)

        Response.Write(objCatalogSerialize)

    End Function

    Function validate_date_consult(ByVal date_consult As String)

        Dim value_string As String

        If date_consult = 0 Then
            value_string = "0"
        Else
            value_string = date_consult
        End If

        Return value_string

    End Function

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

        If type = "I" Then

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

    Protected Function searh_c_typeaproval(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim type_aproval_value As String = ""

        sql.Append(" select i.typeapproval from idea i where i.id =" & ididea)

        Dim data_c_typeaproval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_c_typeaproval = 0 Then
            type_aproval_value = "0"
        Else
            type_aproval_value = data_c_typeaproval
        End If

        Response.Write(type_aproval_value)

    End Function

    Protected Function searh_component_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim component_value As DataTable
        Dim id_component As String

        Dim objResult As String

        sql.Append("select IdProgramComponent from ProgramComponentByIdea  where IdIdea=" & ididea)

        component_value = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim valuar_ubi As Integer = 1

        If component_value.Rows.Count > 0 Then


            For Each row As DataRow In component_value.Rows

                objResult &= ""

                id_component = row(0).ToString()

                objResult &= id_component

                If valuar_ubi = component_value.Rows.Count Then

                    objResult &= ""

                Else
                    objResult &= ","

                End If

                valuar_ubi = valuar_ubi + 1

            Next

        End If

        Response.Write(objResult)


    End Function

    Protected Function load_id_archive(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_anexos As DataTable
        Dim idfile As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select max(d.id) as id, MAX(d.id_document) as iddocument from DocumentsByEntity de ")
        sql.Append(" inner join Documents d on d.Id =de.IdDocuments ")
        sql.Append(" where  de.EntityName ='IdeaEntity' and de.IdnEntity= " & ididea)

        data_anexos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data_anexos.Rows.Count > 0 Then

            If IsDBNull(data_anexos.Rows(0)("iddocument")) = False Then
                idfile = data_anexos.Rows(0)("iddocument")
            Else
                If IsDBNull(data_anexos.Rows(0)("id")) = False Then
                    idfile = data_anexos.Rows(0)("id")
                Else
                    idfile = 0
                End If

            End If

        End If
        Response.Write(idfile)

    End Function

    Protected Function searh_c_typecontract(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

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

    Protected Function validar_aprobacion_idea(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim result As Integer

        sql.Append("SELECT Ididea FROM  IdeaApprovalRecord WHERE Ididea=" & ididea)
        Dim idideaaprovada = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If idideaaprovada <> 0 Then

            result = 1

        Else

            result = 0

        End If

        Response.Write(result)
    End Function

    Protected Function searh_document_anexos_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_anexos As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim objListCFileView As New List(Of CFilesView)


        sql.Append(" select d.id, d.AttachFile,d.Description, d.id_document from DocumentsByEntity de ")
        sql.Append(" inner join Documents d on d.Id =de.IdDocuments ")
        sql.Append(" where  de.EntityName ='IdeaEntity' and de.IdnEntity=" & ididea)

        data_anexos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data_anexos.Rows.Count > 0 Then


            For Each row As DataRow In data_anexos.Rows

                Dim objCFileView As CFilesView = New CFilesView()

                objCFileView.idfile = row(3).ToString

                If objCFileView.idfile = "" Then
                    objCFileView.idfile = row(0).ToString
                End If

                objCFileView.filename = row(1).ToString
                objCFileView.Description = row(2).ToString

                objListCFileView.Add(objCFileView)

            Next

        End If


        Response.Write(JsonConvert.SerializeObject(objListCFileView.ToArray()))

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

    Protected Function searh_Program(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim program_value As String = ""

        sql.Append(" select top 1(pc.IdProgram) from ProgramComponentByIdea pci ")
        sql.Append(" INNER JOIN ProgramComponent pc ON pci.idProgramComponent = pc.Id ")
        sql.Append(" where pci.IdIdea = " & ididea)

        Dim data_program = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data_program = 0 Then
            program_value = "0"
        Else
            program_value = data_program
        End If

        Response.Write(program_value)


    End Function

    Protected Function searh_line_Strategic(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ProgramComponentByIdea As New ProgramComponentByIdeaDALC

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

    Protected Function searh_detalles_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_detalles As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim objListCDetailsFileView As New List(Of CDetailsPaymentFlowView)

        sql.Append(" select dcf.N_pago, dcf.IdAportante, dcf.Aportante, dcf.Desembolso  from Detailedcashflows dcf where dcf.IdIdea = " & ididea)

        data_detalles = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        For Each row As DataRow In data_detalles.Rows

            Dim Objdetails As CDetailsPaymentFlowView = New CDetailsPaymentFlowView()

            Objdetails.idpago = row(0).ToString()
            Objdetails.idaportante = row(1).ToString()
            Objdetails.Aportante = row(2).ToString()
            Objdetails.desembolso = row(3).ToString()

            objListCDetailsFileView.Add(Objdetails)

        Next

        Response.Write(JsonConvert.SerializeObject(objListCDetailsFileView.ToArray()))

    End Function

    Protected Function searh_actors_flujos_array(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_actors_flujos As DataTable

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim data_listactores As List(Of ThirdByIdeaEntity)
        Dim objListCActorsView As New List(Of CActorsView)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                Dim estado_flujo As String = row.EstadoFlujos

                If estado_flujo = "s" Then

                    Dim objCActorsView As CActorsView = New CActorsView()

                    objCActorsView.actorsVal = row.idthird
                    objCActorsView.actorsName = row.Name
                    objCActorsView.tipoactors = row.type
                    objCActorsView.contact = row.contact
                    objCActorsView.cedula = row.Documents
                    objCActorsView.telefono = row.Phone
                    objCActorsView.email = row.Email
                    objCActorsView.diner = row.Vrmoney
                    objCActorsView.especie = row.VrSpecies
                    objCActorsView.total = row.FSCorCounterpartContribution
                    objCActorsView.estado_flujo = row.EstadoFlujos

                    objListCActorsView.Add(objCActorsView)

                End If
            Next
        End If

        Response.Write(JsonConvert.SerializeObject(objListCActorsView.ToArray()))

    End Function

    Protected Function searh_flujos_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim flujopagos As New PaymentFlowDALC()
        Dim objflujos As PaymentFlowEntity
        Dim data_listpagos As New List(Of PaymentFlowEntity)
        Dim objListCPaymentFlowView As New List(Of CPaymentFlowView)

        data_listpagos = flujopagos.getFlowPayment("i", ididea, applicationCredentials)

        If data_listpagos.Count > 0 Then

            For Each row In data_listpagos
                Dim objCPaymentFlowView As CPaymentFlowView = New CPaymentFlowView()

                objCPaymentFlowView.N_pago = row.N_pagos
                objCPaymentFlowView.fecha_pago = row.fecha
                objCPaymentFlowView.porcentaje = row.porcentaje
                objCPaymentFlowView.entrega = row.entregable
                objCPaymentFlowView.tflujos = row.valortotal

                objListCPaymentFlowView.Add(objCPaymentFlowView)
            Next
        End If

        Response.Write(JsonConvert.SerializeObject(objListCPaymentFlowView.ToArray()))
    End Function

    Protected Function searh_actores_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim objactores As ThirdByIdeaEntity
        Dim data_listactores As List(Of ThirdByIdeaEntity)
        Dim objListCActorsView As New List(Of CActorsView)

        Dim htmlactores As String

        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        If data_listactores.Count > 0 Then

            For Each row In data_listactores

                Dim objCActorsView As CActorsView = New CActorsView()

                objCActorsView.actorsVal = row.idthird
                objCActorsView.actorsName = row.Name
                objCActorsView.tipoactors = row.type
                objCActorsView.contact = row.contact
                objCActorsView.cedula = row.Documents
                objCActorsView.telefono = row.Phone
                objCActorsView.email = row.Email
                objCActorsView.diner = row.Vrmoney
                objCActorsView.especie = row.VrSpecies
                objCActorsView.total = row.FSCorCounterpartContribution
                objCActorsView.estado_flujo = row.EstadoFlujos

                objListCActorsView.Add(objCActorsView)
            Next

        End If

        Response.Write(JsonConvert.SerializeObject(objListCActorsView.ToArray()))

    End Function

    Protected Function searh_location_array(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim LocationByIdea As New LocationByIdeaDALC
        Dim objlocation As LocationByIdeaEntity
        Dim data_listlocation As List(Of LocationByIdeaEntity)
        Dim objListCLocationView As New List(Of ClocationView)

        data_listlocation = LocationByIdea.getList(applicationCredentials, , ididea, , , )

        Dim valuar_ubi As Integer = 1

        If data_listlocation.Count > 0 Then

            For Each row In data_listlocation

                Dim objCLocationView As ClocationView = New ClocationView()

                objCLocationView.DeptoVal = row.DEPTO.id
                objCLocationView.CityVal = row.CITY.id
                objCLocationView.CityName = row.CITY.name
                objCLocationView.DeptoName = row.DEPTO.name

                objListCLocationView.Add(objCLocationView)

            Next

        End If

        Response.Write(JsonConvert.SerializeObject(objListCLocationView.ToArray()))

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

        Dim html As String = "<option>Seleccione...</opption>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)

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

        Dim htmlresults As String = "<option>Seleccione...</opption>"
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

        Dim htmlresults As String = "<option>Seleccione...</opption>"
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

    Protected Function save_document_IDEA(ByVal list_file As String, ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim ArrayFile As String()

        Dim NewListFiles = JsonConvert.DeserializeObject(Of List(Of CFilesView))(list_file)

        For Each item_file As CFilesView In NewListFiles

            Dim objDocument As DocumentsEntity = New DocumentsEntity()
            Dim objDocumentbyEntity As DocumentsByEntityEntity = New DocumentsByEntityEntity()

            objDocument.title = item_file.filename
            objDocument.description = item_file.Description
            objDocument.ideditedfor = 0
            objDocument.iddocumenttype = 0
            objDocument.idvisibilitylevel = 0
            objDocument.createdate = Now
            objDocument.iduser = applicationCredentials.UserID
            objDocument.attachfile = item_file.filename
            objDocument.enabled = 1
            objDocument.Id_Entity_Zone = "IdeaEntity_" & ididea
            objDocument.Id_document = Convert.ToInt32(item_file.idfile)

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
            objDocumentbyEntity.idnentity = ididea
            objDocumentbyEntity.entityname = "IdeaEntity"


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

        Next

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
    ' TODO: 16 funcion que verifica si la idea esta aprobada
    ' Autor: german Rodriguez MG group
    ' cierre de cambio

    ''' <summary>
    ''' TODO: 100 funcion que revisa los enter en el guardar y editar idea
    ''' autor: German Rodriguez 16/10/2013 MGgroup
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)
        Dim comillas As String

        Return result

    End Function

    Protected Function borrar_archivos()
        'Response.Write(Server.MapPath("\bats\BORRAR_ARC.bat"))
        Dim startinfo As New ProcessStartInfo(Server.MapPath("\bats\BORRAR_ARC.bat"))
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startinfo)
    End Function

    Protected Function copiar_archivos()
        'Dim startinfo As New ProcessStartInfo("C:\Gattaca_pruebas\WebSiteFSC\ELVIRA-F3\FSC_APP\FSC_APP\bats\COPIAR_ARC.bat")
        Dim startinfo As New ProcessStartInfo(Server.MapPath("\bats\COPIAR_ARC.bat"))
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startinfo)
    End Function

    ''' <summary>
    ''' funcion para guardar la idea
    ''' Autor: German Rodriguez MGgroup
    ''' 08-01-2014 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="line_strategic"></param>
    ''' <param name="program"></param>
    ''' <param name="name"></param>
    ''' <param name="justify"></param>
    ''' <param name="objetive"></param>
    ''' <param name="obj_esp"></param>
    ''' <param name="resul_bef"></param>
    ''' <param name="resul_ges_c"></param>
    ''' <param name="resul_cap_i"></param>
    ''' <param name="fecha_i"></param>
    ''' <param name="mes"></param>
    ''' <param name="dia"></param>
    ''' <param name="fecha_f"></param>
    ''' <param name="poblacion"></param>
    ''' <param name="contratacion"></param>
    ''' <param name="A_Mfsc"></param>
    ''' <param name="A_Efsc"></param>
    ''' <param name="A_Mcounter"></param>
    ''' <param name="A_Ecounter"></param>
    ''' <param name="cost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function save_IDEA(ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal otros_resul As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String, ByVal list_flujos As String, ByVal list_detalles_flujos As String, ByVal list_files As String, ByVal type_aproval As String) '

        Dim facade As New Facade
        Dim objIdea As New IdeaEntity
        Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim arrayubicacion, arrayactor, arraycomponente, arrayflujos, arraydetallesflujos As String()

        Try

            Dim NewListubicaciones = JsonConvert.DeserializeObject(Of List(Of ClocationView))(list_ubicacion)
            Dim NewListActors = JsonConvert.DeserializeObject(Of List(Of CActorsView))(list_actor)
            Dim NewListFlujos = JsonConvert.DeserializeObject(Of List(Of CPaymentFlowView))(list_flujos)
            Dim NewListDetailsFlujos = JsonConvert.DeserializeObject(Of List(Of CDetailsPaymentFlowView))(list_detalles_flujos)

            list_componentes = Replace(list_componentes, "/", "*", 1)
            list_componentes = Replace(list_componentes, "_ *", "*", 1)
            ''convertimos el string en un array de datos
            arraycomponente = list_componentes.Split(New [Char]() {"*"c})

            Dim contadorcomp As Integer = 0

            'recorremos los componentes seleccionados
            For Each row In arraycomponente

                'istanciamos el objeto componente
                Dim myProgramComponentByIdea As New ProgramComponentByIdeaEntity

                If IsNumeric(arraycomponente(contadorcomp)) Then

                    myProgramComponentByIdea.idProgramComponent = arraycomponente(contadorcomp)
                    myProgramComponentByIdeaList.Add(myProgramComponentByIdea)

                End If

                contadorcomp = contadorcomp + 1
            Next
            '----------------------------------------------------ubicaciones------------------------------------------------------------------------
            Dim LocationByIdeaList As List(Of LocationByIdeaEntity) = New List(Of LocationByIdeaEntity)()

            For Each item_location As ClocationView In NewListubicaciones

                Dim objlocationidea As New LocationByIdeaEntity
                Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
                Dim objCityEntity As CityEntity = New CityEntity()

                'asignamos al objeto
                objDeptoEntity.id = item_location.DeptoVal
                objlocationidea.DEPTO = objDeptoEntity
                objCityEntity.id = item_location.CityVal
                objlocationidea.CITY = objCityEntity

                'cargamos al list
                LocationByIdeaList.Add(objlocationidea)

            Next
            '----------------------------------------------------actores------------------------------------------------------------------------
            Dim thirdByIdeaList As List(Of ThirdByIdeaEntity) = New List(Of ThirdByIdeaEntity)()

            For Each item_third As CActorsView In NewListActors

                Dim thirdByIdea As ThirdByIdeaEntity = New ThirdByIdeaEntity()

                thirdByIdea.idthird = item_third.actorsVal
                thirdByIdea.THIRD.name = item_third.actorsName
                thirdByIdea.Name = item_third.actorsName
                thirdByIdea.type = item_third.tipoactors
                thirdByIdea.contact = item_third.contact
                thirdByIdea.Documents = item_third.cedula
                thirdByIdea.Phone = item_third.telefono
                thirdByIdea.Email = item_third.email
                thirdByIdea.Vrmoney = item_third.diner
                thirdByIdea.VrSpecies = item_third.especie
                thirdByIdea.FSCorCounterpartContribution = item_third.total
                thirdByIdea.EstadoFlujos = item_third.estado_flujo
                thirdByIdea.CreateDate = Now

                Update_Third_Date(thirdByIdea.idthird, thirdByIdea.contact, thirdByIdea.Documents, thirdByIdea.Phone, thirdByIdea.Email)

                'cargamos al list
                thirdByIdeaList.Add(thirdByIdea)

            Next
            '----------------------------------------------------flujos------------------------------------------------------------------------
            Dim PaymentFlowList As List(Of PaymentFlowEntity) = New List(Of PaymentFlowEntity)()

            For Each item_paymentflow As CPaymentFlowView In NewListFlujos

                Dim objpaymentFlow As PaymentFlowEntity = New PaymentFlowEntity()

                objpaymentFlow.N_pagos = item_paymentflow.N_pago
                objpaymentFlow.fecha = Convert.ToDateTime(item_paymentflow.fecha_pago)
                objpaymentFlow.porcentaje = item_paymentflow.porcentaje
                objpaymentFlow.entregable = item_paymentflow.entrega
                objpaymentFlow.valortotal = Convert.ToInt32(item_paymentflow.tflujos)
                objpaymentFlow.valorparcial = Convert.ToInt32(item_paymentflow.tflujos)
                objpaymentFlow.idproject = 0
                objpaymentFlow.mother = Nothing

                'cargamos al list
                PaymentFlowList.Add(objpaymentFlow)

            Next
            '----------------------------------------------------detallesflujos------------------------------------------------------------------------
            Dim DetailedcashflowsList As List(Of DetailedcashflowsEntity) = New List(Of DetailedcashflowsEntity)()

            For Each item_Detailedcashflow As CDetailsPaymentFlowView In NewListDetailsFlujos

                Dim objDetalleflujo As DetailedcashflowsEntity = New DetailedcashflowsEntity()

                'asignamos al objeto
                objDetalleflujo.N_pago = Convert.ToInt32(item_Detailedcashflow.idpago)
                objDetalleflujo.IdAportante = Convert.ToInt32(item_Detailedcashflow.idaportante)
                objDetalleflujo.Aportante = item_Detailedcashflow.Aportante
                objDetalleflujo.Desembolso = item_Detailedcashflow.desembolso
                objDetalleflujo.IdProject = 0
                objDetalleflujo.mother = Nothing

                'cargamos al list
                DetailedcashflowsList.Add(objDetalleflujo)

            Next

            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

            Dim codeidea = code

            objIdea.code = Replace(codeidea, vbCrLf, " ")

            objIdea.name = clean_vbCrLf(name)

            objIdea.objective = clean_vbCrLf(objetive)
            objIdea.startdate = fecha_i
            objIdea.duration = mes
            objIdea.areadescription = clean_vbCrLf(obj_esp)
            objIdea.population = poblacion
            objIdea.cost = Convert.ToInt32(cost)
            objIdea.results = clean_vbCrLf(resul_bef)
            objIdea.createdate = Now
            objIdea.iduser = applicationCredentials.UserID

            objIdea.justification = clean_vbCrLf(justify)
            objIdea.Enddate = Convert.ToDateTime(fecha_f)

            objIdea.ResultsKnowledgeManagement = clean_vbCrLf(resul_ges_c)
            objIdea.ResultsInstalledCapacity = clean_vbCrLf(resul_cap_i)
            objIdea.OthersResults = clean_vbCrLf(otros_resul)
            objIdea.idtypecontract = contratacion

            objIdea.Obligaciones = obligaciones
            objIdea.mitigacion = mitigacion
            objIdea.riesgos = riesgos
            objIdea.presupuestal = presupuestal
            objIdea.dia = dia
            objIdea.iva = iva
            objIdea.Typeapproval = 3

            'Se garega la lista de ubicaciones agregada
            objIdea.LOCATIONBYIDEALIST = LocationByIdeaList
            'Se agrega la lista de terceros agregada
            objIdea.THIRDBYIDEALIST = thirdByIdeaList
            'Se agrega la lista de FLUJOS DE PAGOS
            objIdea.paymentflowByProjectList = PaymentFlowList
            'Se agrega la lista de  detalles de FLUJOS DE PAGOS
            objIdea.DetailedcashflowsbyIdeaList = DetailedcashflowsList
            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

            objIdea.id = facade.addIdea(applicationCredentials, objIdea)


            save_document_IDEA(list_files, objIdea.id)

            Dim Result As String

            If objIdea.id <> 0 Then

                Result = "La idea se guardó correctamente !"
                Response.Write(Result)

            Else

                Result = "Se perdio la conexcion al guardar los datos del la Idea"
                Response.Write(Result)

            End If

        Catch ex As Exception

            Dim Result As String
            Result = "Error inesperado por favor consulte el administrador - " & ex.Message
            Response.Write(Result)

        End Try

    End Function

    Protected Function edit_IDEA(ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal otros_resul As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String, ByVal list_flujos As String, ByVal list_detalles_flujos As String, ByVal list_files As String, ByVal type_aproval As String)

        Dim facade As New Facade
        Dim objIdea As New IdeaEntity
        Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)

        Dim arrayubicacion, arrayactor, arraycomponente, arrayflujos, arraydetallesflujos As String()
        
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            Dim NewListubicaciones = JsonConvert.DeserializeObject(Of List(Of ClocationView))(list_ubicacion)
            Dim NewListActors = JsonConvert.DeserializeObject(Of List(Of CActorsView))(list_actor)
            Dim NewListFlujos = JsonConvert.DeserializeObject(Of List(Of CPaymentFlowView))(list_flujos)
            Dim NewListDetailsFlujos = JsonConvert.DeserializeObject(Of List(Of CDetailsPaymentFlowView))(list_detalles_flujos)

            list_componentes = Replace(list_componentes, "/", "*", 1)
            list_componentes = Replace(list_componentes, "_ *", "*", 1)
            ''convertimos el string en un array de datos
            arraycomponente = list_componentes.Split(New [Char]() {"*"c})

            Dim contadorcomp As Integer = 0

            'recorremos los componentes seleccionados
            For Each row In arraycomponente

                'istanciamos el objeto componente
                Dim myProgramComponentByIdea As New ProgramComponentByIdeaEntity

                If IsNumeric(arraycomponente(contadorcomp)) Then

                    myProgramComponentByIdea.idProgramComponent = arraycomponente(contadorcomp)
                    myProgramComponentByIdeaList.Add(myProgramComponentByIdea)

                End If

                contadorcomp = contadorcomp + 1
            Next
            '----------------------------------------------------ubicaciones------------------------------------------------------------------------
            Dim LocationByIdeaList As List(Of LocationByIdeaEntity) = New List(Of LocationByIdeaEntity)()

            For Each item_location As ClocationView In NewListubicaciones

                Dim objlocationidea As New LocationByIdeaEntity
                Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
                Dim objCityEntity As CityEntity = New CityEntity()

                'asignamos al objeto
                objDeptoEntity.id = item_location.DeptoVal
                objlocationidea.DEPTO = objDeptoEntity
                objCityEntity.id = item_location.CityVal
                objlocationidea.CITY = objCityEntity

                'cargamos al list
                LocationByIdeaList.Add(objlocationidea)

            Next
            '----------------------------------------------------actores------------------------------------------------------------------------
            Dim thirdByIdeaList As List(Of ThirdByIdeaEntity) = New List(Of ThirdByIdeaEntity)()

            For Each item_third As CActorsView In NewListActors

                Dim thirdByIdea As ThirdByIdeaEntity = New ThirdByIdeaEntity()

                thirdByIdea.idthird = item_third.actorsVal
                thirdByIdea.THIRD.name = item_third.actorsName
                thirdByIdea.Name = item_third.actorsName
                thirdByIdea.type = item_third.tipoactors
                thirdByIdea.contact = item_third.contact
                thirdByIdea.Documents = item_third.cedula
                thirdByIdea.Phone = item_third.telefono
                thirdByIdea.Email = item_third.email
                thirdByIdea.Vrmoney = item_third.diner
                thirdByIdea.VrSpecies = item_third.especie
                thirdByIdea.FSCorCounterpartContribution = item_third.total
                thirdByIdea.EstadoFlujos = item_third.estado_flujo
                thirdByIdea.CreateDate = Now

                Update_Third_Date(thirdByIdea.idthird, thirdByIdea.contact, thirdByIdea.Documents, thirdByIdea.Phone, thirdByIdea.Email)

                'cargamos al list
                thirdByIdeaList.Add(thirdByIdea)

            Next
            '----------------------------------------------------flujos------------------------------------------------------------------------
            Dim PaymentFlowList As List(Of PaymentFlowEntity) = New List(Of PaymentFlowEntity)()

            For Each item_paymentflow As CPaymentFlowView In NewListFlujos

                Dim objpaymentFlow As PaymentFlowEntity = New PaymentFlowEntity()

                objpaymentFlow.N_pagos = item_paymentflow.N_pago
                objpaymentFlow.fecha = Convert.ToDateTime(item_paymentflow.fecha_pago)
                objpaymentFlow.porcentaje = item_paymentflow.porcentaje
                objpaymentFlow.entregable = item_paymentflow.entrega
                objpaymentFlow.valortotal = Convert.ToInt32(item_paymentflow.tflujos)
                objpaymentFlow.valorparcial = Convert.ToInt32(item_paymentflow.tflujos)
                objpaymentFlow.idproject = 0
                objpaymentFlow.mother = Nothing

                'cargamos al list
                PaymentFlowList.Add(objpaymentFlow)

            Next
            '----------------------------------------------------detallesflujos------------------------------------------------------------------------
            Dim DetailedcashflowsList As List(Of DetailedcashflowsEntity) = New List(Of DetailedcashflowsEntity)()

            For Each item_Detailedcashflow As CDetailsPaymentFlowView In NewListDetailsFlujos

                Dim objDetalleflujo As DetailedcashflowsEntity = New DetailedcashflowsEntity()

                'asignamos al objeto
                objDetalleflujo.N_pago = Convert.ToInt32(item_Detailedcashflow.idpago)
                objDetalleflujo.IdAportante = Convert.ToInt32(item_Detailedcashflow.idaportante)
                objDetalleflujo.Aportante = item_Detailedcashflow.Aportante
                objDetalleflujo.Desembolso = item_Detailedcashflow.desembolso
                objDetalleflujo.IdProject = 0
                objDetalleflujo.mother = Nothing

                'cargamos al list
                DetailedcashflowsList.Add(objDetalleflujo)

            Next

            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

            objIdea.id = code

            objIdea.name = clean_vbCrLf(name)
            objIdea.objective = clean_vbCrLf(objetive)
            objIdea.startdate = fecha_i
            objIdea.duration = mes
            objIdea.areadescription = clean_vbCrLf(obj_esp)
            objIdea.population = poblacion
            objIdea.cost = Convert.ToInt32(cost)
            objIdea.results = clean_vbCrLf(resul_bef)
            objIdea.createdate = Now
            objIdea.iduser = applicationCredentials.UserID

            objIdea.justification = clean_vbCrLf(justify)
            objIdea.Enddate = Convert.ToDateTime(fecha_f)

            objIdea.ResultsKnowledgeManagement = clean_vbCrLf(resul_ges_c)
            objIdea.ResultsInstalledCapacity = clean_vbCrLf(resul_cap_i)
            objIdea.OthersResults = clean_vbCrLf(otros_resul)
            objIdea.idtypecontract = contratacion

            objIdea.Obligaciones = obligaciones
            objIdea.mitigacion = mitigacion
            objIdea.riesgos = riesgos
            objIdea.presupuestal = presupuestal
            objIdea.dia = dia
            objIdea.iva = iva
            objIdea.Typeapproval = type_aproval

            'Se garega la lista de ubicaciones agregada
            objIdea.LOCATIONBYIDEALIST = LocationByIdeaList
            'Se agrega la lista de terceros agregada
            objIdea.THIRDBYIDEALIST = thirdByIdeaList
            'Se agrega la lista de FLUJOS DE PAGOS
            objIdea.paymentflowByProjectList = PaymentFlowList
            'Se agrega la lista de  detalles de FLUJOS DE PAGOS
            objIdea.DetailedcashflowsbyIdeaList = DetailedcashflowsList
            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList


            facade.updateIdea(applicationCredentials, objIdea)

            delete_documents(objIdea.id)
            save_document_IDEA(list_files, objIdea.id)

            Dim Result As String

            If objIdea.id <> 0 Then

                Result = "La idea se modifico correctamente!"
                Response.Write(Result)

            Else

                Result = "Se perdio la conecxion al guardar los datos del la Idea"
                Response.Write(Result)

            End If

        Catch ex As Exception

        End Try

    End Function

    Protected Function Update_Third_Date(ByVal idthird As String, ByVal contact As String, ByVal Documents As String, ByVal Phone As String, ByVal Email As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand


        sql = New StringBuilder
        'guardar los datos del actor
        sql.Append(" update third set ")
        sql.Append(" contact='" & contact & "',")
        sql.Append(" documents='" & Documents & "',")
        sql.Append(" phone='" & Phone & "',")
        sql.Append(" email='" & Email & "'")
        sql.Append(" where ID = " & idthird)
        ' ejecutar la intruccion
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)


    End Function

    Protected Function delete_documents(ByVal id_idea As String)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim dtData, dtDatadoc As DataTable

        sql.Append(" delete Documents where id in (select iddocuments from DocumentsByEntity where EntityName = 'IdeaEntity' and  IdnEntity = " & id_idea & ")")
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

        sql = New StringBuilder

        sql.Append(" delete DocumentsByEntity where EntityName = 'IdeaEntity' and IdnEntity = " & id_idea)
        GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

    End Function

End Class

Class CPaymentFlowView
#Region "Properties public and private"

    Private _N_pago As String
    Public Property N_pago() As String
        Get
            Return _N_pago
        End Get
        Set(ByVal value As String)
            _N_pago = value
        End Set
    End Property

    Private _entrega As String
    Public Property entrega() As String
        Get
            Return _entrega
        End Get
        Set(ByVal value As String)
            _entrega = value
        End Set
    End Property

    Private _fecha_pago As String
    Public Property fecha_pago() As String
        Get
            Return _fecha_pago
        End Get
        Set(ByVal value As String)
            _fecha_pago = value
        End Set
    End Property

    Private _porcentaje As String
    Public Property porcentaje() As String
        Get
            Return _porcentaje
        End Get
        Set(ByVal value As String)
            _porcentaje = value
        End Set
    End Property

    Private _tflujos As String
    Public Property tflujos() As String
        Get
            Return _tflujos
        End Get
        Set(ByVal value As String)
            _tflujos = value
        End Set
    End Property
#End Region
End Class

Class CFilesView
#Region "Properties public and private"

    Private _idfile As String
    Public Property idfile() As String
        Get
            Return _idfile
        End Get
        Set(ByVal value As String)
            _idfile = value
        End Set
    End Property
    Private _filename As String
    Public Property filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property
    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

#End Region
End Class

Class CDetailsPaymentFlowView
#Region "Properties public and private"
    Private _idpago As String
    Public Property idpago() As String
        Get
            Return _idpago
        End Get
        Set(ByVal value As String)
            _idpago = value
        End Set
    End Property
    Private _idaportante As String
    Public Property idaportante() As String
        Get
            Return _idaportante
        End Get
        Set(ByVal value As String)
            _idaportante = value
        End Set
    End Property
    Private _Aportante As String
    Public Property Aportante() As String
        Get
            Return _Aportante
        End Get
        Set(ByVal value As String)
            _Aportante = value
        End Set
    End Property
    Private _desembolso As String
    Public Property desembolso() As String
        Get
            Return _desembolso
        End Get
        Set(ByVal value As String)
            _desembolso = value
        End Set
    End Property
#End Region
End Class
 
Class ClocationView
#Region "Properties public and private"
    Private _DeptoVal As String
    Public Property DeptoVal() As String
        Get
            Return _DeptoVal
        End Get
        Set(ByVal value As String)
            _DeptoVal = value
        End Set
    End Property
    Private _CityVal As String
    Public Property CityVal() As String
        Get
            Return _CityVal
        End Get
        Set(ByVal value As String)
            _CityVal = value
        End Set
    End Property
    Private _DeptoName As String
    Public Property DeptoName() As String
        Get
            Return _DeptoName
        End Get
        Set(ByVal value As String)
            _DeptoName = value
        End Set
    End Property
    Private _CityName As String
    Public Property CityName() As String
        Get
            Return _CityName
        End Get
        Set(ByVal value As String)
            _CityName = value
        End Set
    End Property
#End Region
End Class

Class CActorsView
#Region "Properties public and private"
    Private _actorsVal As String
    Public Property actorsVal() As String
        Get
            Return _actorsVal
        End Get
        Set(ByVal value As String)
            _actorsVal = value
        End Set
    End Property
    Private _actorsName As String
    Public Property actorsName() As String
        Get
            Return _actorsName
        End Get
        Set(ByVal value As String)
            _actorsName = value
        End Set
    End Property
    Private _tipoactors As String
    Public Property tipoactors() As String
        Get
            Return _tipoactors
        End Get
        Set(ByVal value As String)
            _tipoactors = value
        End Set
    End Property
    Private _contact As String
    Public Property contact() As String
        Get
            Return _contact
        End Get
        Set(ByVal value As String)
            _contact = value
        End Set
    End Property
    Private _cedula As String
    Public Property cedula() As String
        Get
            Return _cedula
        End Get
        Set(ByVal value As String)
            _cedula = value
        End Set
    End Property
    Private _telefono As String
    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal value As String)
            _telefono = value
        End Set
    End Property
    Private _email As String
    Public Property email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property
    Private _diner As String
    Public Property diner() As String
        Get
            Return _diner
        End Get
        Set(ByVal value As String)
            _diner = value
        End Set
    End Property
    Private _especie As String
    Public Property especie() As String
        Get
            Return _especie
        End Get
        Set(ByVal value As String)
            _especie = value
        End Set
    End Property
    Private _total As String
    Public Property total() As String
        Get
            Return _total
        End Get
        Set(ByVal value As String)
            _total = value
        End Set
    End Property
    Private _estado_flujo As String
    Public Property estado_flujo() As String
        Get
            Return _estado_flujo
        End Get
        Set(ByVal value As String)
            _estado_flujo = value
        End Set
    End Property
#End Region
End Class