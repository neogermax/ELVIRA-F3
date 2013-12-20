Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports System.Data
Imports System.Data.SqlClient
Imports Gattaca.Application.ExceptionManager
Imports System.IO
Partial Class Report_proceedings_Reportcompromise
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
            'se ocultan campos de informacion
            Me.containerSuccess.Visible = False
            Me.containerSuccess2.Visible = False
            Me.tablacomrpomise.Visible = False

            'funcion que carga el combo de proyecto
            loadCombos()

            Me.ddlproyect.Items.Insert(0, New ListItem("Seleccione...", "-1"))
            Me.ddlproyect.SelectedValue = "-1"

            'se carga titulo
            Session("lblTitle") = "REPORTE COMPROMISOS ACTAS DE SEGUIMIENTO"

            'se descargan las seciones
            Dim validateurl = Session("validateurlcompromise")
            Dim idproyect = Session("proyectcomrpomise")

            'se valida la sesion si es por menu o por acta de cierre
            If validateurl = "T" Then

                Me.fec1.Visible = False
                Me.fec2.Visible = False
                Session("validateurlcompromise") = "N"

            End If

            'se carga el combo si la secion tiene datos
            If idproyect <> Nothing Or idproyect <> 0 Then
                Me.ddlproyect.SelectedValue = idproyect
                Me.ddlproyect.Enabled = False
                Me.HDreportact.Value = idproyect
                Session("proyectcomrpomise") = 0
            End If



            'Se crea la variable de session que almacena la lista de logs proccedings
            Session("proceedingList") = New List(Of proceeding_logsEntity)

        Else

            If Me.HDreportact.Value = "" Then
                Me.ddlproyect.SelectedValue = "-1"
            Else
                Me.ddlproyect.SelectedValue = Me.HDreportact.Value
            End If


            'se ocultan campos de informacion
            Me.containerSuccess.Visible = False
            Me.containerSuccess2.Visible = False
            Me.tablacomrpomise.Visible = False

        End If



    End Sub

    Protected Sub Btnvisulizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnvisulizar.Click

        'se inicializan variables
        Dim idproyect As Integer
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim datanull As DataTable
        Dim fecha1 As Date
        Dim fecha2 As Date
        Dim stringhtml, num, responsable, compromiso, f_inicial As String
        Dim stringhtmlnull, num2, responsable2, compromiso2, f_inicial2, f_final As String
        Dim t1 As Integer = 0
        Dim t2 As Integer = 0


        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        'se ocultan campos de informacion
        Me.containerSuccess.Visible = False
        Me.containerSuccess2.Visible = False

        'se valida la informacion del campo 
        If Me.HDreportact.Value = "" Then
            idproyect = 0
        Else
            idproyect = Me.HDreportact.Value
        End If

        'se valida la informacion del campo
        If Me.txtStartingDate.Text = "" Then
            fecha1 = "01/12/9999"
        Else
            fecha1 = Me.txtStartingDate.Text
        End If

        'se valida la informacion del campo
        If Me.txtEndingDate.Text = "" Then
            fecha2 = "01/12/9999"
        Else
            fecha2 = Me.txtEndingDate.Text
        End If


        'se valida la informacion de los campos fecha
        If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text <> "" Then
            'se valida las fechas
            If fecha1 > fecha2 Then
                Me.containerSuccess.Visible = True
                Me.Lblmsginfo.Text = "la fecha inicial debe ser menor a la fecha final"
                Me.txtStartingDate.Text = ""
                Me.txtEndingDate.Text = ""
                Exit Sub
            End If

        End If

        'se valida la informacion de combo proyecto
        If idproyect = 0 Then
            Me.containerSuccess.Visible = True
            Me.Lblmsginfo.Text = "Debe seleccionar un proyecto por favor"
            Me.txtStartingDate.Text = ""
            Me.txtEndingDate.Text = ""
            Exit Sub
        Else

            'se abilita tabla de informacion
            Me.tablacomrpomise.Visible = True

            'se inicializa query
            sql.Append("select  id,responsible,liabilities,tracingdate from Compromise      ")
            sql.Append("where IdUser is null and idproject = " & idproyect)

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text <> "" Then
                Dim remfecha1 = Format(fecha1, "yyyy/MM/dd")
                Dim ffecha1 = Replace(remfecha1, "/", "-")

                Dim remfecha2 = Format(fecha2, "yyyy/MM/dd")
                Dim ffecha2 = Replace(remfecha2, "/", "-")

                sql.Append(" AND tracingdate >  '" & ffecha1 & "'  AND tracingdate < '" & ffecha2 & "'")
            End If

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text = "" And Me.txtEndingDate.Text <> "" Then
                Dim remfecha2 = Format(fecha2, "yyyy/MM/dd")
                Dim ffecha2 = Replace(remfecha2, "/", "-")

                sql.Append("AND tracingdate = '" & ffecha2 & "'")
            End If

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text = "" Then
                Dim remfecha1 = Format(fecha1, "yyyy/MM/dd")
                Dim ffecha1 = Replace(remfecha1, "/", "-")

                sql.Append("AND tracingdate = '" & ffecha1 & "'")
            End If

            'se ejecuta la sentencia segun el query diseñado
            data = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

            'query para segunda tabla
            sql = New StringBuilder
            'se inicializa query
            sql.Append("select id,responsible,liabilities,tracingdate,Enddate from Compromise      ")
            sql.Append("where IdUser is not null and idproject = " & idproyect)

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text <> "" Then
                Dim remfecha1 = Format(fecha1, "yyyy/MM/dd")
                Dim ffecha1 = Replace(remfecha1, "/", "-")

                Dim remfecha2 = Format(fecha2, "yyyy/MM/dd")
                Dim ffecha2 = Replace(remfecha2, "/", "-")

                sql.Append(" AND tracingdate >  '" & ffecha1 & "'  AND tracingdate < '" & ffecha2 & "'")
            End If

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text = "" And Me.txtEndingDate.Text <> "" Then
                Dim remfecha2 = Format(fecha2, "yyyy/MM/dd")
                Dim ffecha2 = Replace(remfecha2, "/", "-")

                sql.Append("AND tracingdate = '" & ffecha2 & "'")
            End If

            'se valida la informacion de los campos fecha
            If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text = "" Then
                Dim remfecha1 = Format(fecha1, "yyyy/MM/dd")
                Dim ffecha1 = Replace(remfecha1, "/", "-")

                sql.Append("AND tracingdate = '" & ffecha1 & "'")
            End If

            'se ejecuta la sentencia segun el query diseñado
            datanull = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


            Dim conta As Integer = 0

            'se valida la consulta
            If data.Rows.Count > 0 Then
                'se recorre la consulta
                For Each row As DataRow In data.Rows

                    Dim idname As String = "txtfechafin"
                    Dim idpos As String = "txtidpos"


                    If IsDBNull(row("id")) = False Then
                        num = row("id")
                    End If

                    If IsDBNull(row("responsible")) = False Then
                        responsable = row("responsible")
                    End If

                    If IsDBNull(row("liabilities")) = False Then
                        compromiso = row("liabilities")
                    End If

                    If IsDBNull(row("tracingdate")) = False Then
                        f_inicial = row("tracingdate")
                    End If

                    idname = idname & conta
                    idpos = idpos & conta
                    ' genera la tabla con la informacion de la consulta deseada
                    stringhtml &= "<tr><td style=""text-align: center; width: 5%; display:none; border: solid 1px #000000;""><label id=" & idpos & ">" & num & "</label></td><td style=""text-align: center; width: 25%; border: solid 1px #000000;"">" & responsable & "</td><td style=""text-align: center; width: 40%; border: solid 1px #000000;"">" & compromiso & "</td><td style=""text-align: center; width: 15%; border: solid 1px #000000;"">" & f_inicial & "</td><td style=""text-align: center; width: 15%; border: solid 1px #000000;""><input type=""date"" size=""50"" class=""validarfechas"" id=""" & idname & """  name=""" & idname & """></td></tr>"

                    conta = conta + 1
                Next

            Else
                ' genera la tabla vacia si no encuentra datos
                stringhtml = ""
                t1 = 1

            End If


            'se valida la consulta
            If datanull.Rows.Count > 0 Then
                'se recorre la consulta
                For Each row As DataRow In datanull.Rows

                    If IsDBNull(row("id")) = False Then
                        num2 = row("id")
                    End If

                    If IsDBNull(row("responsible")) = False Then
                        responsable2 = row("responsible")
                    End If

                    If IsDBNull(row("liabilities")) = False Then
                        compromiso2 = row("liabilities")
                    End If

                    If IsDBNull(row("tracingdate")) = False Then
                        f_inicial2 = row("tracingdate")
                    End If

                    If IsDBNull(row("Enddate")) = False Then
                        f_final = row("Enddate")
                    End If

                    ' genera la tabla con la informacion de la consulta deseada
                    stringhtmlnull &= "<tr><td style=""text-align: center; width: 5%; display:none; border: solid 1px #000000;"">" & num2 & "</td><td style=""text-align: center; width: 25%; border: solid 1px #000000;"">" & responsable2 & "</td><td style=""text-align: center; width: 40%; border: solid 1px #000000;"">" & compromiso2 & "</td><td style=""text-align: center; width: 15%; border: solid 1px #000000;"">" & f_inicial2 & "</td><td style=""text-align: center; width: 15%; border: solid 1px #000000;"">" & f_final & "</td></tr>"


                Next

            Else
                ' genera la tabla vacia si no encuentra datos
                stringhtmlnull = ""
                t2 = 1
            End If


            If t1 = 1 And t2 = 1 Then
                'abilita menzaje
                Me.containerSuccess.Visible = True

                'valida campo que tiene datos
                If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text = "" Then

                    Me.Lblmsginfo.Text = "la fecha " & fecha1 & " no tiene compromisos"
                    Me.tablacomrpomise.Visible = False
                    Me.txtEndingDate.Enabled = True
                    Me.txtStartingDate.Enabled = True

                End If

                If Me.txtStartingDate.Text = "" And Me.txtEndingDate.Text <> "" Then

                    Me.Lblmsginfo.Text = "la fecha " & fecha2 & " no tiene compromisos"
                    Me.tablacomrpomise.Visible = False
                    Me.txtEndingDate.Enabled = True
                    Me.txtStartingDate.Enabled = True

                End If

                If Me.txtStartingDate.Text <> "" And Me.txtEndingDate.Text <> "" Then
                    Me.Lblmsginfo.Text = "la fechas comprendidas entre el " & fecha1 & " y el " & fecha2 & " no tiene compromisos"
                    Me.tablacomrpomise.Visible = False
                    Me.txtEndingDate.Enabled = True
                    Me.txtStartingDate.Enabled = True

                End If

            Else



                Me.comrpomise.InnerHtml = stringhtml
                Me.comnull.InnerHtml = stringhtmlnull
                Me.txtEndingDate.Enabled = False
                Me.txtStartingDate.Enabled = False

            End If



        End If

    End Sub

    Protected Sub Btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnguardar.Click

        'se inicializan variables
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim arrayfechas As String()
        Dim arrayid As String()
        Dim sql As New StringBuilder
        Me.containerSuccess2.Visible = False

        'cargan datos de objetos dinamicos
        Dim datosd = Me.HFdatetrim.Value
        Dim datosid = Me.HFidtrim.Value

        'valida campos vacios
        If datosd <> "" Then
            'quitamos sobrantes
            datosd = Left(datosd, Len(datosd) - 1)
            'remplazamos separdores
            datosd = Replace(datosd, "__", "_", 1)
            'convertimos el string en un array de datos
            arrayfechas = datosd.Split(New [Char]() {"_"c})
        Else
            datosd = "vacio"
            arrayfechas = datosd.Split(New [Char]() {"_"c})
        End If

        'valida campos vacios
        If datosid <> "" Then
            'quitamos sobrantes
            datosid = Left(datosid, Len(datosid) - 1)
            'remplazamos separdores
            datosid = Replace(datosid, "__", "_", 1)
            'convertimos el string en un array de datos
            arrayid = datosid.Split(New [Char]() {"_"c})
        Else
            'cargamos datos para validacion vacia
            datosid = "vacio"
            'convertimos el string en un array de datos
            arrayid = datosid.Split(New [Char]() {"_"c})
        End If

        'validamos que  el array contenga datos
        If arrayfechas(0) = "vacio" Then
            Me.containerSuccess.Visible = True
            Me.Lblmsginfo.Text = "Debe diligenciar almenos una fecha por favor!"
            Me.txtEndingDate.Enabled = True
            Me.txtStartingDate.Enabled = True

            Exit Sub
        Else
            Dim conta As Integer = 0
            'recorremos el array para actualizar la fechas de finalizacion
            For Each row In arrayfechas

                sql = New StringBuilder
                sql.Append("update Compromise set Enddate = '" & arrayfechas(conta) & "', IdUser = " & applicationCredentials.UserID & ", DateMod = GETDATE() where id = " & arrayid(conta))
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)
                conta = conta + 1

            Next

            Me.containerSuccess.Visible = True
            Me.Lblmsginfo3.Text = "Se modificó con exito él (los) compromiso(s) seleccionado(s)"
            Me.Lblmsginfo2.Text = ""
            Me.Lblmsginfo.Text = ""

        End If


    End Sub

#End Region

#Region "metodos"

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
            sql.Append("inner join Proceeding_Logs pl on p.id = pl.Project_Id      ")
            sql.Append("inner join ContractRequest cr on cr.IdProject = p.id where  pl.Tipo_Acta_id = 2   ")
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

#End Region


End Class
