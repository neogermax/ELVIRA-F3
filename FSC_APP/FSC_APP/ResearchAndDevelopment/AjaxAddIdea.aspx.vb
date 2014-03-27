
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

Partial Class ResearchAndDevelopment_AjaxAddIdea
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion, dia As String
        Dim S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_mitigacion, S_riesgos, S_presupuestal, S_listcomponentes As String
        Dim id_lineStrategic, id_depto, idprogram, idpopulation, Countarchivo As Integer

        Dim strFileName() As String
        Dim fileName As String = String.Empty
        Dim files As HttpFileCollection = Request.Files
        Dim DocumentsTmpList As New List(Of DocumentstmpEntity)

        Session("locationByIdeaList") = New List(Of LocationByIdeaEntity)

        
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
        action = Request.QueryString("action").ToString()
        Select Case action
            Case "buscar"
                'convierte la variable y llama funcion para la validacion de la idea
                id_b = Convert.ToInt32(Request.QueryString("id").ToString())

                buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))
            Case "calculafechas"

                fecha = Convert.ToDateTime(Request.QueryString("fecha").ToString())
                duracion = Request.QueryString("duracion").ToString()
                dia = Request.QueryString("dias").ToString()
                calculafechas(fecha, duracion, dia)

            Case "save"

                'S_code = Request.QueryString("code").ToString
                S_linea_estrategica = Request.QueryString("linea_estrategica").ToString
                S_programa = Request.QueryString("programa").ToString
                S_nombre = Request.QueryString("nombre").ToString
                S_justificacion = Request.QueryString("justificacion").ToString
                S_objetivo = Request.QueryString("objetivo").ToString
                S_objetivo_esp = Request.QueryString("objetivo_esp").ToString
                S_Resultados_Benef = Request.QueryString("Resultados_Benef").ToString
                S_Resultados_Ges_c = Request.QueryString("Resultados_Ges_c").ToString
                S_Resultados_Cap_i = Request.QueryString("Resultados_Cap_i").ToString
                S_Fecha_inicio = Request.QueryString("Fecha_inicio").ToString
                S_mes = Request.QueryString("mes").ToString
                S_dia = Request.QueryString("dia").ToString
                S_Fecha_fin = Request.QueryString("Fecha_fin").ToString
                S_Población = Request.QueryString("Población").ToString
                S_contratacion = Request.QueryString("contratacion").ToString
                S_riesgos = Request.QueryString("riesgo").ToString
                S_mitigacion = Request.QueryString("mitigacion").ToString
                S_presupuestal = Request.QueryString("presupuestal").ToString
                S_cost = Request.QueryString("cost").ToString
                S_iva = Request.QueryString("iva").ToString
                S_obligaciones = Request.QueryString("obligaciones").ToString
                S_listubicaciones = Request.QueryString("listubicaciones").ToString
                S_listactors = Request.QueryString("listactores").ToString
                S_listcomponentes = Request.QueryString("listcomponentes").ToString

                save_IDEA(S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes) '

            Case "C_linestrategic"

                Charge_Lstrategic()

            Case "C_program"

                id_lineStrategic = Convert.ToInt32(Request.QueryString("idlinestrategic").ToString)
                charge_program(id_lineStrategic)

            Case "C_deptos"

                Charge_deptos()

            Case "C_munip"

                id_depto = Convert.ToInt32(Request.QueryString("iddepto").ToString)
                Charge_munip(id_depto)

            Case "C_Actors"

                Charge_actors()

            Case "C_typecontract"

                Charge_typeContract()

            Case "C_component"

                idprogram = Convert.ToInt32(Request.QueryString("idprogram").ToString)
                charge_component(idprogram)

            Case "C_type_project"

                Charge_project_type()

            Case "C_population"

                idpopulation = Convert.ToInt32(Request.QueryString("idpopulation").ToString)
                Charge_population(idpopulation)

            Case "Export"

                S_linea_estrategica = Request.QueryString("linea_estrategica").ToString
                S_programa = Request.QueryString("programa").ToString
                S_nombre = Request.QueryString("nombre").ToString
                S_justificacion = Request.QueryString("justificacion").ToString
                S_objetivo = Request.QueryString("objetivo").ToString
                S_objetivo_esp = Request.QueryString("objetivo_esp").ToString
                S_Resultados_Benef = Request.QueryString("Resultados_Benef").ToString
                S_Resultados_Ges_c = Request.QueryString("Resultados_Ges_c").ToString
                S_Resultados_Cap_i = Request.QueryString("Resultados_Cap_i").ToString
                S_Fecha_inicio = Request.QueryString("Fecha_inicio").ToString
                S_mes = Request.QueryString("mes").ToString
                S_dia = Request.QueryString("dia").ToString
                S_Fecha_fin = Request.QueryString("Fecha_fin").ToString
                S_Población = Request.QueryString("Población").ToString
                S_contratacion = Request.QueryString("contratacion").ToString
                S_riesgos = Request.QueryString("riesgo").ToString
                S_mitigacion = Request.QueryString("mitigacion").ToString
                S_presupuestal = Request.QueryString("presupuestal").ToString
                S_cost = Request.QueryString("cost").ToString
                S_iva = Request.QueryString("iva").ToString
                S_obligaciones = Request.QueryString("obligaciones").ToString
                S_listubicaciones = Request.QueryString("listubicaciones").ToString
                S_listactors = Request.QueryString("listactores").ToString
                S_listcomponentes = Request.QueryString("listcomponentes").ToString

                Export_Idea(S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_riesgos, S_mitigacion, S_presupuestal, S_cost, S_obligaciones, S_iva, S_listubicaciones, S_listactors, S_listcomponentes) '

            Case Else

        End Select


    End Sub


    Public Function Export_Idea(ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String)

        Dim objlocationidea As New LocationByIdeaEntity

        Dim locationByIdeaList As List(Of LocationByIdeaEntity)
        Dim thirdByIdeaList As List(Of ThirdByIdeaEntity)

        Dim arrayubicacion, arrayactor, arraycomponente As String()
        Dim deptovalexist, Cityvalexist, existidprogram, existactorsVal, existactorsName, existtipoactors, existcontact, existcedula, existtelefono, existemail, existdiner, existespecie, existtotal As String



        Try

            locationByIdeaList = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))
            thirdByIdeaList = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))


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


            Dim contador As Integer = 0
            Dim contadoractor As Integer = 0
            Dim contadorcomp As Integer = 0

            Dim swicth As Integer = 0
            Dim swicth_actor As Integer = 0

            Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
            Dim objCityEntity As CityEntity = New CityEntity()
            Dim thirdByIdea As ThirdByIdeaEntity = New ThirdByIdeaEntity()



            Dim sql As New StringBuilder
            Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

            Dim ddlc, ddls, ddlp, just, objet, objetesp, resulb, resulgc, resulci, fech, dura, people, vt1, vt2, vt3, fuent, est, datanex, dept, munip, actor, action, vt4, vt5, vt6, active As String
            Dim FSCval As String = ""
            Dim ididea As Integer

            If code = "" Then
                sql.Append("select MAX(id) from idea")
                ididea = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)
            Else
                ididea = code
            End If

            sql = New StringBuilder
            sql.Append("select convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.','')) from Idea i  ")
            sql.Append("inner join ThirdByIdea ti on i.Id = ti.IdIdea  ")
            sql.Append(" inner join Third t on t.Id= ti.IdThird ")
            sql.Append("where (t.code = '8600383389' And ti.IdIdea = " & ididea & ")")

            FSCval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            If FSCval = 0 Then
                vt5 = "0"
            Else
                vt5 = Format(Convert.ToInt64(FSCval), "#,###.##")
            End If
            sql = New StringBuilder
            sql.Append("select sum(convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.',''))) from Idea i   ")
            sql.Append("inner join ThirdByIdea ti on i.Id=ti.IdIdea  ")
            sql.Append(" inner join Third t on t.Id= ti.IdThird ")
            sql.Append("where(t.code <> '8600383389' And ti.IdIdea = " & ididea & ")")


            Dim otrosval = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            If otrosval = 0 Then
                vt4 = "0"
            Else
                vt4 = Format(Convert.ToInt64(otrosval), "#,###.##")
            End If

            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=IdeaExport.doc")
            Response.Charset = "UTF8Encoding"
            Response.ContentType = "application/vnd.ms-word "

            ddls = line_strategic
            ddlp = program
            ddlc = contratacion

            just = justify
            objet = objetive
            objetesp = obj_esp
            resulb = resul_bef
            resulgc = resul_ges_c
            resulci = resul_cap_i
            fech = fecha_i
            dura = fecha_f
            people = poblacion
            vt3 = cost
            vt6 = cost

            If objetesp = "" Then
                objetesp = "No Aplica"
            End If
            If resulb = "" Then
                resulb = "No Aplica"
            End If
            If resulgc = "" Then
                resulgc = "No Aplica"
            End If
            If resulci = "" Then
                resulci = "No Aplica"
            End If
            If dura = "" Then
                dura = "No Aplica"
            End If
            If datanex = "" Then
                datanex = "No Aplica"
            End If

            If ddlc = "Seleccione...." Then
                ddlc = ""
            End If

            Response.Output.WriteLine("<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><table  style=""font-family: 'Times New Roman';"" Width=""100%"">")

            Response.Output.WriteLine("<body><p style=""text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">T&Eacute;RMINOS DE REFERENCIA</span></strong></p><p><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span></p>")
            Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 20%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>L&iacute;nea Estrat&eacute;gica:</strong></span></td><td>" & ddls.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Programa:</strong></span></td><td>" & ddlp.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Nombre de la Idea:</strong></span></td><td>" & name.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Localizaci&oacute;n Geogr&aacute;fica:</strong></span></td><td>")

            Dim lbldepto, lblcity As String

            For Each row In arrayubicacion

                deptovalexist = InStr(arrayubicacion(contador), "DeptoName")
                Cityvalexist = InStr(arrayubicacion(contador), "CityName")

                If deptovalexist > 0 Then

                    deptovalexist = Replace(arrayubicacion(contador), "DeptoName : ", " ", 1)
                    lbldepto = deptovalexist

                End If

                If Cityvalexist > 0 Then

                    Cityvalexist = Replace(arrayubicacion(contador), "CityName : ", " ", 1)
                    lblcity = Cityvalexist
                    swicth = 1

                End If

                If swicth = 1 Then
                    Response.Output.WriteLine(lbldepto.ToString() & ", " & lblcity.ToString() & " // ")
                    swicth = 0
                End If

                contador = contador + 1

            Next


            Response.Output.WriteLine("</td></tr><tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Poblaci&oacute;n Beneficiaria:</strong></span></td><td>" & people.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Duraci&oacute;n en meses:</strong></span></td><td>" & dura.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha de Inicio:</strong></span></td><td>" & fech.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor Total:</strong></span></td><td>" & vt3.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Modalidad de contrataci&oacute;n:</strong></span></span></td><td>" & ddlc.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 50%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>No de Idea:</strong></span></td><td>" & ididea.ToString() & "." & "</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actores:</strong></span></p>")
            Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Actor</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tipo</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contacto</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Documento de Identidad</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tel&eacute;fono</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Correo electr&oacute;nico</strong></span></td></tr>")

            Dim lblname, labelcontact, labeldocument, labeltype, labelphone, labelemail As String

            For Each row In arrayactor

                existactorsName = InStr(arrayactor(contadoractor), "actorsName") 'y
                existtipoactors = InStr(arrayactor(contadoractor), "tipoactors")
                existcontact = InStr(arrayactor(contadoractor), "contact") 'y
                existcedula = InStr(arrayactor(contadoractor), "cedula") 'y
                existtelefono = InStr(arrayactor(contadoractor), "telefono") 'y
                existemail = InStr(arrayactor(contadoractor), "email") 'y

                
                If existactorsName > 0 Then

                    existactorsName = Replace(arrayactor(contadoractor), "actorsName : ", " ", 1)
                    lblname = existactorsName

                End If

                If existtipoactors > 0 Then

                    existtipoactors = Replace(arrayactor(contadoractor), "tipoactors : ", " ", 1)
                    labeltype = existtipoactors

                End If

                If existcontact > 0 Then

                    existcontact = Replace(arrayactor(contadoractor), "contact : ", " ", 1)
                    labelcontact = existcontact

                End If

                If existcedula > 0 Then

                    existcedula = Replace(arrayactor(contadoractor), "cedula : ", " ", 1)
                    labeldocument = existcedula

                End If

                If existtelefono > 0 Then

                    existtelefono = Replace(arrayactor(contadoractor), "telefono : ", " ", 1)
                    labelphone = existtelefono

                End If

                If existemail > 0 Then

                    existemail = Replace(arrayactor(contadoractor), "email : ", " ", 1)
                    labelemail = existemail
                    swicth_actor = 1

                End If


                If lblname = "Total" Then
                    Exit For
                Else

                    If swicth_actor = 1 Then
                        Response.Output.WriteLine("<tr><td style=""width: 16%;"">" & lblname.ToString() & "</td><td style=""width: 16%;"">" & labeltype.ToString() & "</td><td style=""width: 16%;"">" & labelcontact.ToString() & "</td><td style=""width: 16%;"">" & labeldocument.ToString() & "</td><td style=""width: 16%;"">" & labelphone.ToString() & "</td><td style=""width: 16%;"">" & labelemail.ToString() & "</tr>")
                        swicth_actor = 0
                    End If

                End If

                contadoractor = contadoractor + 1

            Next




            Response.Output.WriteLine("</tbody></table><p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">JUSTIFICAC&Iacute;ON:</span></strong></p>")
            Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""67"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & just.ToString() & "</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVO:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""71"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objet.ToString() & "</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">OBJETIVOS ESPEC&Iacute;FICOS:</span></strong></p><table border=""0"" cellpadding=""1"" cellspacing=""1"" height=""82"" style=""width: 100%;"" width=""100%""><tbody><tr><td style=""text-align: justify;"">" & objetesp.ToString() & "</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">RESULTADOS ESPERADOS:</span></strong></p>")
            Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
            Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Beneficiarios:</span></strong></td><td style=""text-align: justify;"">" & resulb.ToString() & "</td></tr><tr>")
            Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Gesti&oacute;n del conocimiento*:</span></strong></td><td style=""text-align: justify;"">" & resulgc.ToString() & "</td></tr><tr>")
            Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 20%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Capacidad instalada:</span></strong></td><td style=""text-align: justify;"">" & resulci.ToString() & "</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">PRESUPUESTO GENERAL:</span></strong></p>")
            Response.Output.WriteLine("<table border=""0"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr>")
            Response.Output.WriteLine("<td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Valor Total del contrato:</span></strong></td><td>" & vt6.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de los Socios (Efectivo y Especie):</span></strong></td><td>" & vt4.ToString() & "</td></tr>")
            Response.Output.WriteLine("<tr><td style=""width: 5%; text-align: right;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">&gt;</span></strong></td><td style=""width: 50%;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">Aporte de la FSC (Efectivo y Especie):</span></strong></td><td>" & vt5.ToString() & "</td></tr></tbody></table>")

            Response.Output.WriteLine("<span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span><span _fck_bookmark=""1"" style=""display: none;"">&nbsp;</span>")

            Response.Output.WriteLine("<p><u><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">CRONOGRAMA DE PAGOS</span></strong></u></p>")
            Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" height=""125"" width=""100%""><tbody><tr><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Valor</strong></span></td><td style=""width: 5%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>%</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Origen de los Recursos</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Contraentrega</strong></span></td><td style=""width: 16%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Fecha</strong></span></td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Primer Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Segundo Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Tercer y &Uacute;ltimo Desembolso</strong></span></td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 5%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td><td style=""width: 16%;"">&nbsp;</td></tr><tr><td style=""width: 16%; text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">TOTAL</span></strong></td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 5%; text-align: center;""><strong><span style=""font-family:Times New Roman,helvetica,sans-serif;"">100%</span></strong></td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;"">&nbsp;</td><td style=""width: 16%; text-align: center;"">&nbsp;</td></tr></tbody></table>")
            Response.Output.WriteLine("<p>&nbsp;</p><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong><u>IDENTIFICACI&Oacute;N DE RIESGOS</u></strong></span></p><p>&nbsp;</p>")
            Response.Output.WriteLine("<table border=""1"" cellpadding=""1"" cellspacing=""1"" style=""width: 100%;""><tbody><tr><td style=""width: 50%; text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Riesgo identificado</strong></span></td><td style=""text-align: center;""><span style=""font-family:Times New Roman,helvetica,sans-serif;""><strong>Acci&oacute;n de mitigaci&oacute;n</strong></span></td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr><tr><td style=""width: 50%;"">&nbsp;</td><td>&nbsp;</td></tr></tbody></table>")
            Response.Output.WriteLine("<p><strong>*Nota:&nbsp; </strong>En la Fundaci&oacute;n Saldarriaga Concha promovemos la cultura de racionalizaci&oacute;n en el uso del papel, por lo que se solicita informar a nuestros operadores que solo debe enviar el <strong>informe final </strong>impreso<strong>.</strong></p>")

            Response.Flush()
            Response.End()


        Catch ex As Exception

        End Try
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
    ''' funcion que carga el combo de tipo de componente de programa
    ''' Autor: German Rodriguez MGgroup
    ''' 12-01-2014
    ''' </summary>
    ''' <param name="idprogram"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function charge_component(ByVal idprogram As Integer)

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
    Public Function save_IDEA(ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal riesgos As String, ByVal mitigacion As String, ByVal presupuestal As String, ByVal cost As String, ByVal obligaciones As String, ByVal iva As String, ByVal list_ubicacion As String, ByVal list_actor As String, ByVal list_componentes As String) '

        Dim facade As New Facade
        Dim objIdea As New IdeaEntity
        Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)
        Dim objlocationidea As New LocationByIdeaEntity

        Dim locationByIdeaList As List(Of LocationByIdeaEntity)
        Dim thirdByIdeaList As List(Of ThirdByIdeaEntity)

        Dim arrayubicacion, arrayactor, arraycomponente As String()
        Dim deptovalexist, Cityvalexist As Integer
        Dim existidprogram, existactorsVal, existactorsName, existtipoactors, existcontact, existcedula, existtelefono, existemail, existdiner, existespecie, existtotal As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try


            locationByIdeaList = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))
            thirdByIdeaList = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))


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

            list_componentes = Replace(list_componentes, "/", "*", 1)
            list_componentes = Replace(list_componentes, "_ *", "*", 1)
            'list_componentes = Replace(list_componentes, "}", " ", 1)
            'list_componentes = Replace(list_componentes, """", " ", 1)
            ''convertimos el string en un array de datos
            arraycomponente = list_componentes.Split(New [Char]() {"*"c})

            Dim contador As Integer = 0
            Dim contadoractor As Integer = 0
            Dim contadorcomp As Integer = 0

            Dim swicth As Integer = 0
            Dim swicthactor As Integer = 0

            Dim objDeptoEntity As DeptoEntity = New DeptoEntity()
            Dim objCityEntity As CityEntity = New CityEntity()
            Dim thirdByIdea As ThirdByIdeaEntity = New ThirdByIdeaEntity()

            Dim myProgramComponentByIdea As New ProgramComponentByIdeaEntity


            For Each row In arraycomponente

                If IsNumeric(arraycomponente(contadorcomp)) Then

                    myProgramComponentByIdea.idProgramComponent = arraycomponente(contadorcomp)
                    myProgramComponentByIdeaList.Add(myProgramComponentByIdea)

                    Dim f As Integer = 1

                Else
                    Dim f As Integer = 0
                End If

                contadorcomp = contadorcomp + 1
            Next


            For Each row In arrayubicacion

                deptovalexist = InStr(arrayubicacion(contador), "DeptoVal")
                Cityvalexist = InStr(arrayubicacion(contador), "CityVal")

                If deptovalexist > 0 Then

                    deptovalexist = Replace(arrayubicacion(contador), " DeptoVal : ", " ", 1)
                    objDeptoEntity.id = deptovalexist
                    objlocationidea.DEPTO = objDeptoEntity

                End If

                If Cityvalexist > 0 Then

                    Cityvalexist = Replace(arrayubicacion(contador), "CityVal : ", " ", 1)
                    objCityEntity.id = Cityvalexist
                    objlocationidea.CITY = objCityEntity
                    swicth = 1

                End If

                If swicth = 1 Then
                    locationByIdeaList.Add(objlocationidea)
                    swicth = 0
                End If

                contador = contador + 1

            Next


            For Each row In arrayactor

                existactorsVal = InStr(arrayactor(contadoractor), "actorsVal") 'y
                existactorsName = InStr(arrayactor(contadoractor), "actorsName") 'y
                existtipoactors = InStr(arrayactor(contadoractor), "tipoactors")
                existcontact = InStr(arrayactor(contadoractor), "contact") 'y
                existcedula = InStr(arrayactor(contadoractor), "cedula") 'y
                existtelefono = InStr(arrayactor(contadoractor), "telefono") 'y
                existemail = InStr(arrayactor(contadoractor), "email") 'y
                existdiner = InStr(arrayactor(contadoractor), "diner")
                existespecie = InStr(arrayactor(contadoractor), "especie")
                existtotal = InStr(arrayactor(contadoractor), "total")

                thirdByIdea.CreateDate = Now

                If existactorsVal > 0 Then

                    existactorsVal = Replace(arrayactor(contadoractor), " actorsVal : ", " ", 1)
                    thirdByIdea.idthird = existactorsVal

                End If

                If existactorsName > 0 Then

                    existactorsName = Replace(arrayactor(contadoractor), "actorsName : ", " ", 1)
                    thirdByIdea.THIRD.name = existactorsName
                    thirdByIdea.Name = existactorsName

                End If

                If existtipoactors > 0 Then

                    existtipoactors = Replace(arrayactor(contadoractor), "tipoactors : ", " ", 1)
                    thirdByIdea.type = existtipoactors

                End If

                If existcontact > 0 Then

                    existcontact = Replace(arrayactor(contadoractor), "contact : ", " ", 1)
                    thirdByIdea.THIRD.contact = existcontact
                    thirdByIdea.contact = existcontact

                End If

                If existcedula > 0 Then

                    existcedula = Replace(arrayactor(contadoractor), "cedula : ", " ", 1)
                    thirdByIdea.THIRD.documents = existcedula
                    thirdByIdea.Documents = existcedula

                End If

                If existtelefono > 0 Then

                    existtelefono = Replace(arrayactor(contadoractor), "telefono : ", " ", 1)
                    thirdByIdea.THIRD.phone = existtelefono
                    thirdByIdea.Phone = existtelefono

                End If

                If existemail > 0 Then

                    existemail = Replace(arrayactor(contadoractor), "email : ", " ", 1)
                    thirdByIdea.THIRD.email = existemail
                    thirdByIdea.Email = existemail

                End If

                If existdiner > 0 Then

                    existdiner = Replace(arrayactor(contadoractor), "diner : ", " ", 1)
                    thirdByIdea.Vrmoney = existdiner

                End If

                If existespecie > 0 Then

                    existespecie = Replace(arrayactor(contadoractor), "especie : ", " ", 1)
                    thirdByIdea.VrSpecies = existespecie

                End If

                If existtotal > 0 Then

                    existtotal = Replace(arrayactor(contadoractor), "total : ", " ", 1)
                    thirdByIdea.FSCorCounterpartContribution = existtotal
                    swicthactor = 1

                End If


                If swicthactor = 1 Then
                    thirdByIdeaList.Add(thirdByIdea)
                    swicthactor = 0
                End If

                contadoractor = contadoractor + 1

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
            objIdea.cost = PublicFunction.ConvertStringToDouble(cost)
            objIdea.results = clean_vbCrLf(resul_bef)
            objIdea.createdate = Now
            objIdea.iduser = applicationCredentials.UserID

            objIdea.justification = clean_vbCrLf(justify)
            objIdea.Enddate = Convert.ToDateTime(fecha_f)

            ' TODO: 4  addidea campos nuevos
            ' Autor: German Rodriguez MGgroup
            ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

            objIdea.ResultsKnowledgeManagement = clean_vbCrLf(resul_ges_c)
            objIdea.ResultsInstalledCapacity = clean_vbCrLf(resul_cap_i)
            objIdea.idtypecontract = contratacion

            objIdea.Obligaciones = obligaciones
            objIdea.mitigacion = mitigacion
            objIdea.riesgos = riesgos
            objIdea.presupuestal = presupuestal

            If iva = "on" Then
                objIdea.iva = True
            Else
                objIdea.iva = False
            End If

            ''objIdea.Loadingobservations = clean_vbCrLf(Me.txtobser.Text)

            ' TODO: 4  addidea campos nuevos
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio

            'Se garega la lista de ubicaciones agregada
            objIdea.LOCATIONBYIDEALIST = DirectCast(Session("locationByIdeaList"), List(Of LocationByIdeaEntity))

            'Se agrega la lista de terceros agregada
            objIdea.THIRDBYIDEALIST = DirectCast(Session("thirdByIdeaList"), List(Of ThirdByIdeaEntity))

            'Se almacena en el objeto idea la lista de Componentes del Programa obtenida
            objIdea.ProgramComponentBYIDEALIST = myProgramComponentByIdeaList

            objIdea.id = facade.addIdea(applicationCredentials, objIdea)





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
            Result = "Error inesperado por favor consulte el administrador"
            Response.Write(Result)

        End Try


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

   


End Class


