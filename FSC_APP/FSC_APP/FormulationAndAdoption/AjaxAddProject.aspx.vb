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



Partial Public Class AjaxAddProject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion, dia As String
        Dim idprogram_list, S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Resultados_otros_resul, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_mitigacion, S_riesgos, S_presupuestal, S_listcomponentes, S_listflujos, S_listdetallesflujos, S_listfiles As String
        Dim ideditar, id_lineStrategic, id_depto, idprogram, idpopulation, Countarchivo As Integer

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


        action = Request.QueryString("action").ToString()
        Select Case action

            '----------------- modulo componentes------------------------------------------------------------
            Case "C_linestrategic"

                Charge_Lstrategic()

            Case "C_program"

                id_lineStrategic = Convert.ToInt32(Request.QueryString("idlinestrategic").ToString)
                charge_program(id_lineStrategic)

            Case "list_program"

                id_lineStrategic = Convert.ToInt32(Request.QueryString("idlinestrategic").ToString)
                charge_list_program(id_lineStrategic)

            Case "C_component"

                idprogram_list = Request.QueryString("idprogram").ToString
                charge_component(idprogram_list)

            Case "View_line_strategic"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_line_Strategic(ideditar)

            Case "View_program"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_Program(ideditar)

            Case "View_component"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_component(ideditar)
                '----------------- modulo descripcion-------------------------------------------------------

            Case "calculafechas"

                fecha = Convert.ToDateTime(Request.QueryString("fecha").ToString())
                duracion = Request.QueryString("duracion").ToString()
                dia = Request.QueryString("dias").ToString()
                calculafechas(fecha, duracion, dia)

            Case "C_typecontract"

                Charge_typeContract()

            Case "C_type_project"

                Charge_project_type()

            Case "C_population"

                idpopulation = Convert.ToInt32(Request.QueryString("idpopulation").ToString)
                Charge_population(idpopulation)

            Case "View_matriz_principal"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_matriz_p(ideditar)

            Case "Cpopulation_view"
                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_c_population(ideditar)

            Case "Ctypcontract_view"
                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_c_typecontract(ideditar)

            Case "C_ideas_aprobada"
                charge_idea_aproval()
                '----------------- modulo ubicacion-------------------------------------------------------
            Case "C_deptos"

                Charge_deptos()

            Case "C_munip"

                id_depto = Convert.ToInt32(Request.QueryString("iddepto").ToString)
                Charge_munip(id_depto)

            Case "View_ubicacion"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_location(ideditar)

            Case "View_ubicacion_array"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_location_array(ideditar)

                '----------------- modulo actores-------------------------------------------------------
            Case "buscar"
                'convierte la variable y llama funcion para la validacion de la idea
                id_b = Convert.ToInt32(Request.QueryString("id").ToString())
                buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))

            Case "C_Actors"

                Charge_actors()

            Case "View_actores"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_actores(ideditar)

            Case "View_actores_array"
                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_actores_array(ideditar)

                '----------------- modulo flujos-------------------------------------------------------
            Case "View_flujos_p"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_flujos(ideditar)

            Case "View_flujos_p_array"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_flujos_array(ideditar)

            Case "View_flujos_actors"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_actors_flujos(ideditar)

            Case "View_flujos_actors_array"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_actors_flujos_array(ideditar)

            Case "View_detalle_flujo_array"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_detalles_array(ideditar)

                '----------------- modulo anexos-------------------------------------------------------
            Case "View_anexos"

                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_document_anexos(ideditar)

            Case "View_anexos_array"
                ideditar = Convert.ToInt32(Request.QueryString("ididea").ToString)
                searh_document_anexos_array(ideditar)

                '----------------- tareas generales-------------------------------------------------------

            Case "getIdeaProject_inf_p"
                ideditar = Convert.ToInt32(Request.QueryString("id").ToString)
                searchIdea_inf_p(ideditar, applicationCredentials)

            Case Else


        End Select

    End Sub


    Public Function searh_document_anexos_array(ByVal ididea As Integer)

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

    Public Function searh_document_anexos(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_anexos As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select d.id, d.AttachFile,d.Description, d.id_document from DocumentsByEntity de ")
        sql.Append(" inner join Documents d on d.Id =de.IdDocuments ")
        sql.Append(" where  de.EntityName ='IdeaEntity' and de.IdnEntity=" & ididea)

        data_anexos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html_anexos As String
        Dim id_files As Integer
        Dim name_archive As String

        If data_anexos.Rows.Count > 0 Then

            html_anexos = "<table id=""T_files"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th style=""text-align: center;"">Archivo</th><th style=""text-align: center;"">Observaciones</th><th style=""text-align: center;"">Eliminar</th></tr></thead><tbody>"

            For Each row As DataRow In data_anexos.Rows
                If row(3).ToString() = "" Then
                    id_files = row(0).ToString()
                Else
                    id_files = row(3).ToString()
                End If

                name_archive = row(1).ToString()
                name_archive = name_archive.Replace(" ", "")

                name_archive = name_archive.Replace("_", " ")

                html_anexos &= "<tr id=""archivo" & id_files & """><td><a id=""linkarchives" & id_files & """ runat=""server"" href=""/FSC_APP/document/" & name_archive & """ target= ""_blank"" title=""link"">" & name_archive & "</a></td><td style=""text-align: left;"">" & row(2).ToString & "</td><td style=""text-align: center;""><input type =""button"" value= ""Eliminar"" onclick=""deletefile('" & id_files & "')""></input></td></tr>"
            Next
            html_anexos &= "</tbody></table>"
        Else
            html_anexos = "<table id=""T_files"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th style=""text-align: center;"">Archivo</th><th style=""text-align: center;"">Observaciones</th><th style=""text-align: center;"">Eliminar</th></tr></thead><tbody>"
            html_anexos &= "</tbody></table>"

        End If

        Response.Write(html_anexos)


    End Function


    Public Function searh_detalles_array(ByVal ididea As Integer)

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


    Public Function searh_actors_flujos_array(ByVal ididea As Integer)

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


                If estado_flujo = "  s  " Then

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

    Public Function searh_actors_flujos(ByVal ididea As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_actors_flujos As DataTable

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select ti.idthird, ti.name,ti.FSCorCounterpartContribution from ThirdByIdea ti ")
        sql.Append(" where ti.generatesflow ='  s' and  ti.IdIdea = " & ididea)

        data_actors_flujos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html_actors_flujo As String

        If data_actors_flujos.Rows.Count > 0 Then

            html_actors_flujo = "<table id=""T_Actorsflujos"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th width=""1""></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>"

            For Each row As DataRow In data_actors_flujos.Rows
                html_actors_flujo &= "<tr id=""flujo" & row(0).ToString() & """><td width=""1"" style=""color: #D3D6FF;font-size: 0.1em;"">" & row(0).ToString() & "</td><td>" & row(1).ToString() & "</td><td id= ""value" & row(0).ToString() & """ >" & row(2).ToString() & "</td><td><input id=""" & "txtinput" & row(0).ToString() & """ onkeyup=""formatvercionsuma(this)"" onchange=""formatvercionsuma(this)""  onblur=""sumar_flujos('" & row(0).ToString() & "')"""" onfocus=""restar_flujos('" & row(0).ToString() & "')""""></input></td><td id=""desenbolso" & row(0).ToString() & """>0</td></tr>"
            Next

            html_actors_flujo &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Total</td><td id=""tflujosing""></td><td id=""totalflujos"">0</td></td id=""tflujosdesen""><td></tr></tbody></table>"

        Else

            html_actors_flujo = "<table id=""T_Actorsflujos"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th width=""1""></th><th>Aportante</th><th>Valor total aporte</th><th>Valor por programar</th><th>Saldo por programar</th></tr></thead><tbody>"
            html_actors_flujo &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Total</td><td id=""tflujosing""></td><td id=""totalflujos"">0</td></td id=""tflujosdesen""><td></tr></tbody></table>"

        End If

        Response.Write(html_actors_flujo)

    End Function

    Public Function searh_flujos_array(ByVal ididea As Integer)

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

    Public Function searh_flujos(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim flujopagos As New PaymentFlowDALC()
        Dim objflujos As PaymentFlowEntity
        Dim data_listpagos As New List(Of PaymentFlowEntity)

        Dim npagos, vpar, entregable, porcent, fecha As String

        Dim htmlflujo As String

        data_listpagos = flujopagos.getFlowPayment("i", ididea, applicationCredentials)

        If data_listpagos.Count > 0 Then

            htmlflujo = "<table id=""T_flujos"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th style=""text-align: center;"">No pago</th><th style=""text-align: center;"">Fecha</th><th style=""text-align: center;"">Porcentaje</th><th style=""text-align: center;"">Entregable</th><th style=""text-align: center;"">Valor parcial</th><th style=""text-align: center;"">Editar/Eliminar</th><th style=""text-align: center;"" >Detalle</th></tr></thead><tbody>"

            For Each row In data_listpagos

                npagos = row.N_pagos
                npagos = Replace(npagos, " ", "")

                vpar = row.valorparcial
                vpar = Format(Convert.ToInt64(vpar), "#,###.##")
                entregable = row.entregable
                porcent = row.porcentaje
                fecha = row.fecha

                htmlflujo &= "<tr id='flow" & npagos & "' ><td>" & npagos & "</td><td>" & fecha & "</td><td>" & porcent & "</td><td>" & entregable & "</td><td>" & vpar & "</td><td><input type =""button"" value= ""Editar"" onclick=""editflujo('" & npagos & "','" & fecha & "','" & porcent & "','" & entregable & "','" & vpar & "')""></input><input type =""button"" value= ""Eliminar"" onclick=""eliminarflujo('" & npagos & "')""></input></td><td><input type =""button"" value= ""Detalle"" onclick=""traerdetalles('" & npagos & "',this)""></input></td></tr>"

            Next

            htmlflujo &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Porcentaje acumulado</td><td id=""porcentaje"">0 %</td><td>Total</td><td id=""totalflujospagos"">0</td><td></td><td></td></tr></tbody></table>"


        Else
            htmlflujo = "<table id=""T_flujos"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th style=""text-align: center;"">No pago</th><th style=""text-align: center;"">Fecha</th><th style=""text-align: center;"">Porcentaje</th><th style=""text-align: center;"">Entregable</th><th style=""text-align: center;"">Valor parcial</th><th style=""text-align: center;"">Editar/Eliminar</th><th style=""text-align: center;"" >Detalle</th></tr></thead><tbody>"
            htmlflujo &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Porcentaje acumulado</td><td id=""porcentaje"">0 %</td><td>Total</td><td id=""totalflujospagos"">0</td><td></td><td></td></tr></tbody></table>"

        End If

        Response.Write(htmlflujo)

    End Function


    Public Sub searchIdea_inf_p(ByVal id As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials)

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


    Public Function searh_c_typecontract(ByVal ididea As Integer)

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

    Public Function searh_c_population(ByVal ididea As Integer)

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


    Public Function searh_matriz_p(ByVal ididea As Integer)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim objactores As ThirdByIdeaEntity
        Dim data_listactores As List(Of ThirdByIdeaEntity)
        Dim name, vd, ve, vt, id As String

        Dim htmlactores As String

        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        If data_listactores.Count > 0 Then

            htmlactores = "<table id=""matriz"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%""><thead><tr><th width=""1""></th><th></th><th>Efectivo</th><th>Especie</th><th>Total</th></tr></thead><tbody>"

            For Each row In data_listactores

                id = row.idthird
                name = row.Name
                vd = row.Vrmoney
                ve = row.VrSpecies
                vt = row.FSCorCounterpartContribution

                htmlactores &= "<tr id= ""matriz" & id & """><td width=""1"" style=""color: #D3D6FF;font-size: 0.1em;"">" & id & "</td><td style=""text-align: left"">" & name & "</td><td>" & vd & "</td><td> " & ve & "</td><td> " & vt & " </td></tr>"


            Next

            htmlactores &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Valor Total</td><td id=""valueMoneytotal"">0</td><td id=""ValueEspeciestotal"">0</td><td id=""ValueCostotal"">0</td></tr></tbody></table>"
        Else

            htmlactores = "<table id=""matriz"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%""><thead><tr><th width=""1""></th><th></th><th>Efectivo</th><th>Especie</th><th>Total</th></tr></thead><tbody>"
            htmlactores &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Valor Total</td><td id=""valueMoneytotal"">0</td><td id=""ValueEspeciestotal"">0</td><td id=""ValueCostotal"">0</td></tr></tbody></table>"

        End If

        Response.Write(htmlactores)


    End Function


    ''' <summary>
    ''' funcion que carga el combo de tipo de poblacion segun la selección del tipo de proyecto
    ''' Autor: German Rodriguez MGgroup
    ''' 28-01-2014
    ''' </summary>
    ''' <param name="id_population"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_population(ByVal id_population As Integer)

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
    ''' funcion que carga el combo de tipo de proyectos
    ''' Autor: German Rodriguez MGgroup
    ''' 28-01-2014
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_project_type()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        sql.Append(" select pt.ID,pt.Project_Type  from  ProjectType pt ")
        sql.Append(" order by pt.Project_Type asc")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String = "<option>Seleccione...</opption>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)

    End Function


    ''' <summary>
    ''' funcion que carga el combo de tipo de contrato
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_typeContract()

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim data_typecontect As List(Of typecontractEntity)

        Dim htmlresults As String = ""
        Dim id, contract As String

        data_typecontect = facade.gettypecontract(applicationCredentials, order:="id")

        For Each row In data_typecontect
            ' cargar el valor del campo
            id = row.id
            contract = row.contract
            htmlresults &= String.Format("<option value='{0}'>{1}</option>", id, contract)

        Next
        Response.Write(htmlresults)


    End Function


    Public Function charge_idea_aproval()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append("select  Idea.Id, Idea.Code,Idea.Name,par.codeapprovedidea,  Idea.Code+'_'+Idea.Name as 'name_code' ")
        sql.Append("FROM Idea INNER JOIN ProjectApprovalRecord par ON idea.Id = par.Ididea ORDER BY par.CreateDate DESC ")

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        Dim html As String = "<option>Seleccione...</opption>"
        For Each row As DataRow In data.Rows
            html &= String.Format("<option value = ""{0}"">{1}</option>", row(0).ToString(), row(4).ToString())
        Next

        ' retornar el objeto
        Response.Write(html)


    End Function
    Public Function calculafechas(ByVal fecha As DateTime, ByVal duracion As String, ByVal dias_ope As String) As String

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


    Public Function searh_actores_array(ByVal ididea As Integer)

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

    Public Function searh_actores(ByVal ididea As Integer)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim thirdbyidea As New ThirdByIdeaDALC
        Dim objactores As ThirdByIdeaEntity
        Dim data_listactores As List(Of ThirdByIdeaEntity)
        Dim name, contacto, email, tel, documet, tipo, vd, ve, vt, id As String

        Dim htmlactores As String

        data_listactores = thirdbyidea.getList(applicationCredentials, , ididea, , , , , , )

        If data_listactores.Count > 0 Then

            htmlactores = "<table id=""T_Actors"" align=""center"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th width=""1""></th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Total</th><th>Eliminar</th></tr></thead><tbody>"

            For Each row In data_listactores

                id = row.idthird
                name = row.Name
                contacto = row.contact
                tipo = row.type
                email = row.Email
                tel = row.Phone
                documet = row.Documents
                vd = row.Vrmoney
                ve = row.VrSpecies
                vt = row.FSCorCounterpartContribution

                htmlactores &= "<tr id=""actor" & id & """ ><td width=""1"" style=""color: #D3D6FF;font-size: 0.1em;"">" & id & "</td><td>" & name & "</td><td>" & tipo & "</td><td>" & contacto & "</td><td>" & documet & "</td><td>" & tel & "</td><td>" & email & "</td><td>" & vd & "</td><td>" & ve & "</td><td>" & vt & "</td><td><input type =""button"" value= ""Eliminar"" onclick=""deleteActor('" & id & "')""></input></td></tr>"


            Next

            htmlactores &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id=""val1""></td><td id=""val2"">0</td><td id=""val3"">0</td><td></td></tr></tbody></table>"
        Else

            htmlactores = "<table id=""T_Actors"" align=""center"" border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><thead><tr><th width=""1""></th><th>Actores</th><th>Tipo</th><th>Contacto</th><th>Documento Identidad</th><th>Tel&eacute;fono</th><th>Correo electr&oacute;nico</th><th>Vr Dinero</th><th>Vr Especie</th><th>Vr Total</th><th>Eliminar</th></tr></thead><tbody>"
            htmlactores &= "<tr><td width=""1"" style=""color: #D3D6FF; font-size: 0.1em;"">1000</td><td>Total</td><td></td><td></td><td></td><td></td><td></td><td id=""val1"">0</td><td id=""val2"">0</td><td id=""val3"">0</td><td></td></tr></tbody></table>"

        End If

        Response.Write(htmlactores)


    End Function

    ''' <summary>
    ''' funcion que carga el combo de actores
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_actors()

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim data_actors As List(Of ThirdEntity)

        Dim htmlresults As String = "<option>Seleccione...</opption>"
        Dim id, name As String

        data_actors = facade.getThirdList(applicationCredentials, enabled:="1", order:="Code")


        For Each row In data_actors
            ' cargar el valor del campo
            id = row.id
            name = row.name
            htmlresults &= String.Format("<option value='{0}'>{1}</option>", id, name)

        Next
        Response.Write(htmlresults)
    End Function


    Public Function buscardatethird(ByVal bybal As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
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
    Public Function charge_component(ByVal idprogram As String)

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim Data_programcomponent As List(Of ProgramComponentEntity)

        Dim htmlresults As String = ""
        Dim id, code As String

        Data_programcomponent = facade.getProgramComponentList(applicationCredentials, idProgram:=idprogram, enabled:="1", order:="Code")

        For Each row In Data_programcomponent
            ' cargar el valor del campo
            id = row.id
            code = row.code
            htmlresults &= "<li id= add" + id + " class='seleccione'>" + code + "</li>"
        Next
        Response.Write(htmlresults)

    End Function

    Public Function charge_list_program(ByVal idLinestrategic As Integer)
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

    Public Function searh_component(ByVal ididea As Integer)

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
                htmlcomponente &= "<li id= 'add" + row(0).ToString() + "' class='seleccione'>" + row(1).ToString() + "</li>"
            Next

        End If

        Response.Write(htmlcomponente)



    End Function

    Public Function searh_Program(ByVal ididea As Integer)

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

    Public Function searh_line_Strategic(ByVal ididea As Integer)

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


    ''' <summary>
    ''' funcion que carga el combo de programa precedido por la linea estrategicas seleccionada
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <param name="idLinestrategic"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function charge_program(ByVal idLinestrategic As Integer)
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

    ''' <summary>
    ''' funcion que carga el combo de linea estrategica
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_Lstrategic()
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim line_data As List(Of StrategicLineEntity)

        Dim htmlresults As String = "<option>Seleccione...</opption>"
        Dim id, code As String

        line_data = facade.getStrategicLineList(applicationCredentials, enabled:="1", order:="Code")

        For Each row In line_data
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
    Public Function Charge_munip(ByVal iddepto As Integer)
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
    ''' funcion que carga el combo de departamentos
    ''' Autor: German Rodriguez MGgroup
    ''' 09-01-2014
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Charge_deptos()
        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim depto_data As List(Of DeptoEntity)

        Dim htmlresults As String = "<option>Seleccione...</opption>"
        Dim id, name As String

        depto_data = facade.getDeptoList(applicationCredentials, enabled:="T", order:="Depto.Code")

        For Each row In depto_data
            ' cargar el valor del campo
            id = row.id
            name = row.name
            htmlresults &= String.Format("<option value='{0}'>{1}</option>", id, name)

        Next
        Response.Write(htmlresults)

    End Function

    Public Function searh_location_array(ByVal ididea As Integer)

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
                DeptoVal = row.CITY.id

                objResult &= DeptoVal

                objResult &= """, ""DeptoName"": """
                DeptoName = row.DEPTO.name

                objResult &= DeptoName

                objResult &= """, ""CityVal"": """
                CityVal = row.DEPTO.id

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

    Public Function searh_location(ByVal ididea As Integer)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim LocationByIdea As New LocationByIdeaDALC
        Dim objlocation As LocationByIdeaEntity
        Dim data_listlocation As List(Of LocationByIdeaEntity)
        Dim idcity, iddepto, namecyty, namedepto As String

        Dim htmlubications As String

        data_listlocation = LocationByIdea.getList(applicationCredentials, , ididea, , , )

        If data_listlocation.Count > 0 Then

            htmlubications = "<table id=""T_location"" border=""2"" cellpadding=""2"" cellspacing=""2"" style=""width: 100%;""><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead><tbody>"

            For Each row In data_listlocation

                namecyty = row.CITY.name
                namedepto = row.DEPTO.name

                Dim strdelete As String = namecyty & "_" & namedepto

                htmlubications &= "<tr><td>" & namedepto & "</td><td>" & namecyty & "</td><td><input type =""button"" class= ""deleteUbicacion"" value= ""Eliminar"" onclick=""deleteUbicacion(""" & strdelete & """)"" ></input></td></tr>"


            Next
            htmlubications &= "</tbody></table>"

        Else
            htmlubications = "<table id=""T_location"" border=""2"" cellpadding=""2"" cellspacing=""2"" style=""width: 100%;""><thead><tr><th>Departamento</th><th>Ciudad</th><th>Eliminar</th></tr></thead><tbody>"
            htmlubications &= "<tr><td></td><td></td><td><input type =""button"" class= ""deleteUbicacion"" value= ""Eliminar"" onclick=""deleteUbicacion()"" ></input></td></tr>"

        End If
        Response.Write(htmlubications)


    End Function


End Class