Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO
Imports FSC_APP.PostMail

Partial Class FomsProceedings_Proceeding_tracing
    Inherits System.Web.UI.Page

#Region "Eventos"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        If HttpContext.Current.Application("Theme") IsNot Nothing Then

            ' quemar el tema del cliente
            Page.Theme = HttpContext.Current.Application("Theme").ToString

        Else
            ' quemar el tema por defecto
            Page.Theme = "GattacaAdmin"

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            Me.containerSuccess3.Visible = False
            'Se crea la variable de session que almacena la lista de compromisos
            Session("compromise") = New List(Of CompromiseEntity)
            'precargar los datos del proyecto
            buscardatos()

        End If

    End Sub
    ''' <summary>
    ''' TODO: FUNCION PARA GENERAR EL ACTA DE SEGUIMIENTO EN EXCEL  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Btnexport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnexport.Click

        Dim objProcceding As Proceedings = New Proceedings()
        Dim objCompromiseDALC As New CompromiseDALC
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idproject As Integer = Request.QueryString("cid")
        'Prueba de letras
        'Me.txtValueInLetters.Text = UCase(Letras(Me.txtValue.Text))

        'Guardar Acta para capturar el ID
        Dim idacta = savelog("X")

        'Cargar propiedades del objeto
        objProcceding.ProceedCode = Me.HFacta.Value

        objProcceding.ProceedApprovalDate = Me.txtComitDate.Text
        objProcceding.PartnerOperatorName = Me.txtPartnerName.Text
        objProcceding.ContractNumber = Me.txtContractNumber.Text
        objProcceding.ContractStartDate = Me.txtStartingDate.Text
        objProcceding.ContractFinishDate = Me.txtEndingDate.Text
        objProcceding.ContractObject = Me.txtContractObjective.Text
        objProcceding.Assistant1 = Me.txtAssistant1.Text
        objProcceding.Assistant2 = Me.txtAssistant2.Text
        objProcceding.Assistant3 = Me.txtAssistant3.Text
        objProcceding.Assistant4 = Me.txtAssistant4.Text
        objProcceding.Assistant5 = Me.txtAssistant5.Text
        objProcceding.Assistant6 = Me.txtAssistant6.Text
        objProcceding.Assistant7 = Me.txtAssistant7.Text
        objProcceding.DayOrder1 = Me.taDayOrder.InnerText

        'Mapear labels
        objProcceding.lblTypeThird = "Nombre del " & Me.ddlTypeThird.Text & ":"
        objProcceding.lblDatesTrace = "Fechas del " & Me.ddlTypeoF.Text & ":"
        objProcceding.lblObject = "Objeto del " & Me.ddlTypeoF.Text & ":"
        objProcceding.lblValue = "Valor del " & Me.ddlTypeoF.Text & " $:"
        objProcceding.lblAdditions = "Adiciones al " & Me.ddlTypeoF.Text & ":"
        objProcceding.Label1 = "Valor final del " & Me.ddlTypeoF.Text & " en $:"
        objProcceding.lblProceedNumber = "Número del " & Me.ddlTypeoF.Text & ":"

        objProcceding.Idproject = Request.QueryString("cid")
        objProcceding.DirectorioActas = Server.MapPath("~")
        'GUARDAR COMPROMISOS

        'Se agrega la lista de terceros agregada
        objProcceding.COMPROMISELIST = DirectCast(Session("compromise"), List(Of CompromiseEntity))

        If objProcceding.COMPROMISELIST.Count > 0 Then
            'Por cada elemento de la lista
            For Each compromise As CompromiseEntity In objProcceding.COMPROMISELIST
                'Se llama al metodo que almacena la informacion COMPROMISOS EN LA BASE DE DATOS.
                objCompromiseDALC.add(applicationCredentials, compromise, idacta)
            Next
        End If

        Dim html As String = ""
        Dim labelCOMPROMISOS As String
        Dim labelREPONSABLE As String
        Dim labelTIEMPO As String

        For Each itemRow As GridViewRow In Me.gvcompromisos.Rows

            labelCOMPROMISOS = CType(itemRow.Cells(3).FindControl("lblliabilities"), Label).Text
            labelREPONSABLE = CType(itemRow.Cells(4).FindControl("lblresponsible"), Label).Text
            labelTIEMPO = CType(itemRow.Cells(5).FindControl("lbltracingdate"), Label).Text

            html &= " <tr><td style=""border: solid 1px; border-color: #000000;"" colspan=""2"" rowspan=""1"">" & labelCOMPROMISOS & "</td><td style=""border: solid 1px; border-color: #000000;"" colspan=""3"">" & labelREPONSABLE & "</td><td style=""border: solid 1px; border-color: #000000;"">" & labelTIEMPO & "</td></tr>"

        Next

        objProcceding.TABLECOMPROMISE = html

        'Generar archivo a exportar
        Dim nameexport = objProcceding.StartExportTrace()

        '***Prueba para emails de seguimiento***
        Dim correo As PostMail_SndMail = New PostMail_SndMail
        Dim asunto As String
        Dim mensajecorreo As String
        Dim destinatarios As String = ""
        Dim responsables As String = ""
        Dim objProject As New ProjectEntity
        Dim objContractRequest As New ContractRequestEntity
        Dim facade As New Facade

        'traer el objeto proyect
        objProject = facade.loadProject(applicationCredentials, Request.QueryString("cid"))

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim primero As Integer = 0
        Dim archivo As String = ""

        '>>>Consultar responsables en variable
        For Each itemRow As GridViewRow In Me.gvcompromisos.Rows
            primero = primero + 1
            If primero = 1 Then
                responsables = CType(itemRow.Cells(5).FindControl("lblemail"), Label).Text
            Else
                responsables = responsables & ", " & CType(itemRow.Cells(5).FindControl("lblemail"), Label).Text
            End If
        Next

        'consultar lider del proyecto
        sql.Append("select iduser from project where project.id = " & Request.QueryString("cid"))
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            destinatarios = data(0)("iduser")
        End If

        'reiniciar las variables
        sql = New StringBuilder
        primero = 0

        'consultar admin(yasmira)
        sql.Append("select user_id from usersbymailgroup where usersbymailgroup.mailgroup = 2")
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            destinatarios = destinatarios & " or id=" & data(0)("user_id")
        End If

        'reiniciar las variables
        sql = New StringBuilder
        primero = 0

        'buscar generador acta inicio
        sql.Append("select USER_ID from proceeding_logs where Project_Id =" & Request.QueryString("cid") & "and Tipo_Acta_id = 2")
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            destinatarios = destinatarios & " or id=" & data(0)("user_id")
        End If

        'reiniciar las variables
        sql = New StringBuilder
        primero = 0

        'consultar los emails
        sql.Append("use FSC_eSecurity select email from ApplicationUser where id =" & destinatarios)
        data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            For Each itemDataTable As DataRow In data.Rows

                primero = primero + 1

                If primero = 1 Then
                    destinatarios = itemDataTable("email")
                Else
                    destinatarios = destinatarios & "," & itemDataTable("email")
                End If

            Next

        End If

        If responsables <> "" Then
            destinatarios = destinatarios & ", " & responsables
        End If

        'definir la ruta del archivo a adjuntar
        archivo = Server.MapPath("~") & Replace(nameexport, "/", "\")

        asunto = "Compromisos pendientes del proyecto " & objProject.id & " - " & objProject.name

        mensajecorreo = "Hola,"
        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "Usted tiene tareas pendientes en el proyecto " & objProject.id & " - " & objProject.name & "; para más información consulte el documento adjunto."
        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "Cordialmente,"
        mensajecorreo = mensajecorreo & Chr(13) & Chr(13) & "ELVIRA"
        mensajecorreo = mensajecorreo & Chr(13) & "EvaLuación y Valoración de la InveRsión Articulada"
        mensajecorreo = mensajecorreo & Chr(13) & "Fundación Saldarriaga Concha"

        correo.SendMail(destinatarios, asunto, mensajecorreo, archivo)

        '***Fin prueba para emails de seguimiento***

        'actualizar el acta con el nombre del archivo
        updatelog(nameexport, idacta)

        Me.Btnexport.Visible = False
        Response.Redirect(nameexport)

    End Sub

    Protected Sub gvcompromisos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvcompromisos.SelectedIndexChanged

        ' definir los objetos
        Dim COMPROMISELIST As List(Of CompromiseEntity)
        Dim index As Integer = 0

        ' cargarla de la session
        COMPROMISELIST = DirectCast(Session("compromise"), List(Of CompromiseEntity))

        ' remover el seleccionado
        COMPROMISELIST.RemoveAt(Me.gvcompromisos.SelectedIndex)

        ' mostrar
        Me.gvcompromisos.DataSource = COMPROMISELIST
        Me.gvcompromisos.DataBind()


    End Sub


    Protected Sub Btnagregarcompromisos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnagregarcompromisos.Click


        Dim objCompromise As New CompromiseEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim compromiseList As List(Of CompromiseEntity)

        ' cargarla de la session
        compromiseList = DirectCast(Session("compromise"), List(Of CompromiseEntity))

        If (Me.txtLiabilities.Text <> "") And (Me.txtreponsible.Text <> "") And (Me.txtTracingDate.Text <> "") And (Me.txtemail.Text <> "") Then

            Me.containerSuccess3.Visible = False

            objCompromise.id = 1
            objCompromise.liabilities = Me.txtLiabilities.Text
            objCompromise.idproject = Request.QueryString("cid")
            objCompromise.responsible = Me.txtreponsible.Text
            objCompromise.tracingdate = Me.txtTracingDate.Text
            objCompromise.email = Me.txtemail.Text
            
            'agregarlos
            compromiseList.Add(objCompromise)

            Dim objDataTable As DataTable = New DataTable()

            objDataTable.Columns.Add("id")
            objDataTable.Columns.Add("idproject")
            objDataTable.Columns.Add("liabilities")
            objDataTable.Columns.Add("responsible")
            objDataTable.Columns.Add("tracingdate")
            objDataTable.Columns.Add("email")


            For Each itemDataTable As CompromiseEntity In compromiseList

                objDataTable.Rows.Add(itemDataTable.id, itemDataTable.idproject, itemDataTable.liabilities, itemDataTable.responsible, itemDataTable.tracingdate, itemDataTable.email)
            Next

            ' mostrar
            Me.gvcompromisos.DataSource = objDataTable
            Me.gvcompromisos.DataBind()
            Me.Cleancompromise()

        Else
            Me.containerSuccess3.Visible = True
            Me.Lblinformationvalide.Text = "Los campos compromisos, responsable, fecha de seguimiento y correo electronico son obligatorios para agregar un nuevo compromiso!"
            If Me.txtLiabilities.Text = "" Then
                Me.txtLiabilities.Focus()
                Exit Sub
            End If
            If Me.txtreponsible.Text = "" Then
                Me.txtreponsible.Focus()
                Exit Sub
            End If
            If Me.txtTracingDate.Text = "" Then
                Me.txtTracingDate.Focus()
                Exit Sub
            End If
            If Me.txtemail.Text = "" Then
                Me.txtemail.Focus()
                Exit Sub
            End If
        End If

    End Sub

#End Region

#Region "Metodos"

    Public Function savelog(ByVal NAME As String) As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim proceeding_logs_close As proceeding_logsEntity = New proceeding_logsEntity()
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()
        Dim idproject As Integer = Request.QueryString("cid")

        proceeding_logs_close.project_id = idproject
        proceeding_logs_close.Tipo_acta_id = 2
        proceeding_logs_close.iduser = applicationCredentials.UserID
        proceeding_logs_close.createdate = Now
        proceeding_logs_close.file_name = NAME

        Dim idacta = objProceedingsadd.add(applicationCredentials, proceeding_logs_close)

        Return idacta

    End Function

    Public Function updatelog(ByVal nombrearchivo As String, ByVal actaid As String) As String

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim objProceedingsadd As proceedings_logDALC = New proceedings_logDALC()

        Dim idacta = objProceedingsadd.update(applicationCredentials, nombrearchivo, actaid)

        Return idacta

    End Function

    Public Sub buscardatos()

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim datacontrato As DataTable
        Dim dataproyecto As DataTable
        Dim data2 As String


        Dim idproject As Integer = Request.QueryString("cid")


        Try

            sql.Append("select max(Acta_id) + 1 from Proceeding_Logs where Tipo_Acta_id = 2 and Project_Id = " & idproject)
            data2 = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            If data2 = 0 Then
                data2 = 1
            End If

            Me.HFacta.Value = data2


            sql = New StringBuilder
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
                Me.txtPartnerName.Text = objResult
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
            sql.Append("select Objective from Project where Id = " & idproject)

            ' ejecutar la intruccion
            dataproyecto = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            If dataproyecto.Rows.Count > 0 Then
                'objetive
                If IsDBNull(dataproyecto.Rows(0)("Objective")) = False Then
                    Me.txtContractObjective.Text = dataproyecto.Rows(0)("Objective")
                End If

            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub Cleancompromise()

        Me.txtLiabilities.Text = ""
        Me.txtreponsible.Text = ""
        Me.txtTracingDate.Text = ""
        Me.txtemail.Text = ""
        Me.txtLiabilities.Focus()

    End Sub


#End Region

#Region "Funciones"

    Public Function Letras(ByVal numero As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras, entero, dec, flag As String
        'inicializar
        entero = ""
        palabras = ""
        dec = ""
        numero = Replace(numero, ".", "")

        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "," Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 1, 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or _
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And _
                    Len(entero) <= 6) Then palabras = palabras & "mil "
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then
                Letras = palabras & "con " & dec
            Else
                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function

#End Region


End Class
