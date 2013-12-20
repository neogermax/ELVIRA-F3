Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data

Partial Class FormulationAndAdoption_addproyectchargemasive
    Inherits System.Web.UI.Page

#Region "propiedades"
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
        Me.vali.Visible = False
        Me.linkcrono.Visible = False

        ' cargar el titulo
        Session("lblTitle") = "INGRESAR CRONOGRAMA."

        loadCombos()
        Me.ddlproyect.Items.Insert(0, New ListItem("Seleccione...", "-1"))
        Me.ddlproyect.SelectedValue = "-1"
    End Sub
    ''' <summary>
    ''' boton que ejecuta la carga para validacion del archivo excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Btnuppcharge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnuppcharge.Click

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim menzaje As String
        Dim path As String = ""
        Dim idproject As Integer

        If HFid.Value = "" Then

            Me.Lbltitle.Text = "No ha seleccionado proyecto "
            Exit Sub

        Else
            idproject = Me.HFid.Value
            
        End If

        Dim coneccion As OleDbConnection = New OleDbConnection("")
        Me.Lbltitle.Text = Me.HFtitle.Value


        Try

            If (FileUpload1.HasFile) Then
                'CAPTURAMOS EL NOMBRE DEL ARCHIVO
                Dim fileName = System.IO.Path.GetFileName(Me.FileUpload1.FileName)
                Me.Lblarchivo.Text = fileName
                'CATURAMOS EL TAMAÑO
                Dim fileSize As Integer = FileUpload1.PostedFile.ContentLength
                If (fileSize < 1100000) Then
                    'CAPTURAMOS LA EXTENCION DEL ARCHIVO
                    Dim extension As String = System.IO.Path.GetExtension(fileName)
                    If (extension = ".xls") Then
                        'Almacenarlo en upload
                        path = System.IO.Path.Combine(Server.MapPath("~/upload"), fileName)
                        Me.FileUpload1.SaveAs(path)
                        'Ejecutar proceso
                        Dim oconn As OleDbConnection = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("~/upload/" & fileName) & "; Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";")
                        coneccion = oconn
                        'Despues de conectarse al archivo excel seleccionamos los datos de la hoja por el nombre
                        Dim ocmd As New OleDbCommand("select * from [CRONOGRAMA$]", oconn)

                        oconn.Open()

                        Dim odr As OleDbDataReader = ocmd.ExecuteReader()

                        'utilizamos el bulkcopy para cargar la tabla de la base de datos
                        Dim copia As New SqlBulkCopy(System.Configuration.ConfigurationManager.ConnectionStrings("FSC_eProjectConnectionStringuploadmasive").ConnectionString)
                        'llamamos la tabla destino en la base de datos
                        copia.DestinationTableName = "TemporaryActivities"
                        'copiamos los datos en la tabla
                        copia.WriteToServer(odr)

                        sql = New StringBuilder

                        'validamos si existe actividades para el proyecto seleccionado
                        sql.Append("exec Value_exists_activity  " & idproject)
                        Dim v_exist_activity = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                        If v_exist_activity = 1 Then

                            sql = New StringBuilder
                            'hacer query para averiguar si hay filas completas con nulls
                            sql.Append("delete from TemporaryActivities where  Cod_project is null  and Cod_activity is null and Activity is null and Cod_subactivity is null and subactivity is null and Subactivity_previous is null and Nit_Actors is null and Actors is null and  responsible is null and Star_date is null and End_date is null    ")
                            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                            Me.Lblmsginfo.Text = "Desea remplazar las actividades existentes ó agregar nuevas actividades? "
                            Me.Lblmsginfored.Text = ""
                            Me.containerSuccess.Visible = "true"
                            'HABILITA EL MODULO DE PREGUNTA USUARIO MUESTRA LOS BOTONES BORRAR O AGREGAR
                            Me.vali.Visible = "true"
                            oconn.Close()
                            'Eliminar el libro al salir
                            System.IO.File.Delete(path)
                            Exit Sub
                        Else

                            sql = New StringBuilder
                            'hacer query para averiguar si hay filas completas con nulls
                            sql.Append("delete from TemporaryActivities where  Cod_project is null  and Cod_activity is null and Activity is null and Cod_subactivity is null and subactivity is null and Subactivity_previous is null and Nit_Actors is null and Actors is null and  responsible is null and Star_date is null and End_date is null    ")
                            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                            'INSTANCIAMOS DE NUEVO PARA DAR NUEVA SENTENCIA
                            sql = New StringBuilder


                            'validar y ejecutar la carga
                            sql.Append("exec load_data_validation " & idproject)
                            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)


                            sql = New StringBuilder

                            sql.Append("select valuesresult from resultsmensaje  where id = 1 ")
                            menzaje = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                            MENSAJES(menzaje)

                            'INSTANCIAMOS DE NUEVO PARA DAR NUEVA SENTENCIA
                            sql = New StringBuilder
                            'borrar la tabla temporal
                            sql.Append("delete TemporaryActivities")
                            GattacaApplication.RunSQL(applicationCredentials, sql.ToString)


                            oconn.Close()
                            'Eliminar el libro al salir
                            System.IO.File.Delete(path)


                        End If

                    Else

                        Me.Lblmsginfored.Text = "El proceso de carga ha fallado. Por favor compruebe que el archivo sea un archivo de excel (.xls)."
                        Me.Lblmsginfo.Text = ""
                        Me.containerSuccess.Visible = "true"

                    End If
                Else

                    Me.Lblmsginfored.Text = "El proceso de carga ha fallado. Por favor verifique el tamaño del acchivo no supere de 1 (MB). "
                    Me.Lblmsginfo.Text = ""
                    Me.containerSuccess.Visible = "true"

                End If

            Else

                Me.Lblmsginfored.Text = "no se ha seleccionado ningun archivo."
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"

            End If

        Catch ex As Exception

            Me.Lblmsginfored.Text = " Se produjo un error en la conexión con el servidor!  " & "(" & ex.Message & ")"
            Me.Lblmsginfo.Text = ""
            Me.containerSuccess.Visible = "true"
            'Eliminar el libro al salir
            coneccion.Close()
            System.IO.File.Delete(path)

            sql = New StringBuilder
            'borrar la tabla temporal
            sql.Append("delete from TemporaryActivities")
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString)

        End Try

    End Sub
    ''' <summary>
    ''' BONTON QUE LLAMA EL FORMATO ALOJADO EN LA BASE DE DATOS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Btnlinkdownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnlinkdownload.Click
        Response.Redirect("~/XLSCARGE/CCronograma.xls")
    End Sub

    ''' <summary>
    ''' BOTON QUE GUARDA LAS DEMAS ACTIVIDADES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Btnaddcharge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnaddcharge.Click

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim idproject As Integer
        idproject = Me.HFid.Value

        Me.vali.Visible = "true"

        Try

            'validar y ejecutar la carga
            sql.Append("exec load_data_validation " & idproject)
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            sql = New StringBuilder

            sql.Append("select valuesresult from resultsmensaje  where id = 1 ")
            Dim menzaje = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            MENSAJES(menzaje)
            Me.vali.Visible = "false"


        Catch ex As Exception

            Me.Lblmsginfored.Text = " Se produjo un error en la conexión con el servidor, no se agregaron mas datos  "
            Me.Lblmsginfo.Text = ""
            Me.containerSuccess.Visible = "true"
            'Eliminar el libro al salir

            sql = New StringBuilder
            'borrar la tabla temporal
            sql.Append("delete from TemporaryActivities")
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString)
            Me.vali.Visible = "false"


        End Try


    End Sub
    ''' <summary>
    ''' funcion para eliminar datos ya existentes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Btndeleteandcharge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btndeleteandcharge.Click

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim idproject As Integer
        idproject = Me.HFid.Value

        Me.vali.Visible = "true"

        Try
            sql = New StringBuilder

            'sentencia para eliminar las actividades 
            sql.Append("exec delete_load_charge_masive " & idproject)
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            sql = New StringBuilder

            'validar y ejecutar la carga
            sql.Append("exec load_data_validation " & idproject)
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            sql = New StringBuilder

            sql.Append("select valuesresult from resultsmensaje  where id = 1 ")
            Dim menzaje = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

            MENSAJES(menzaje)

            Me.vali.Visible = "false"


        Catch ex As Exception

            Me.Lblmsginfored.Text = " Se produjo un error en la conexión con el servidor, no se borraron los datos deseados  "
            Me.Lblmsginfo.Text = ""
            Me.containerSuccess.Visible = "true"
            'Eliminar el libro al salir

            sql = New StringBuilder
            'borrar la tabla temporal
            sql.Append("delete from TemporaryActivities")
            GattacaApplication.RunSQL(applicationCredentials, sql.ToString)
            Me.vali.Visible = "false"

        End Try

    End Sub

#End Region


#Region "metodos"

    Private Sub MENSAJES(ByRef IDMENSAJE As Integer)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idproject As Integer = Me.HFid.Value

        Me.Lblmsginfo.Text = ""

        Select Case IDMENSAJE

            Case 0
                Me.Lblmsginfored.Text = "Upps! lo sentimos problemas de conexión con el servidor! "
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"

            Case 1
                Me.Lblmsginfored.Text = "El archivo tiene campos vacios, verifique por favor (campos no obligatorios actividad predecesora y responsable) "
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"

            Case 2
                Me.Lblmsginfored.Text = "Tiene mas de un COD_PROYECTO para ingresar, verifique por favor!  "
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"

            Case 3
                Me.Lblmsginfored.Text = "El COD_PROYECTO es diferente al que selecciono,  verifique por favor! "
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"

            Case 4
                sql.Append("update project set Enabled=1, isLastVersion=1, IdProcessInstance=1  where id = " & idproject)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("select MAX(DISTINCT IdComponent) from activity  where idproyect= " & idproject)
                Dim AB = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("delete SubactivityByThird WHERE IdSubActivity IN (select ID from SubActivity  where IdActivity in (select ID from activity  where idproyect=" & idproject & " and IdComponent <> " & AB & ")) ")
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("delete SubActivity where IdActivity in (select ID from activity  where idproyect = " & idproject & " and IdComponent <> " & AB & ")")
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("delete from activity  where idproyect= " & idproject & " and IdComponent <> " & AB)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("SELECT MAX(DISTINCT ID) FROM Objective WHERE IdProject= " & idproject)
                Dim OB = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append("DELETE Component  WHERE IdObjective IN (SELECT ID FROM OBJECTIVE where IdProject= " & idproject & " and ID <> " & OB & ")")
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                sql = New StringBuilder

                sql.Append(" DELETE OBJECTIVE WHERE ID <>" & OB & " AND IdProject = " & idproject)
                GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

                Me.Lblmsginfo.Text = "Carga de cronograma exitosa!"
                Me.Lblmsginfored.Text = ""
                Me.containerSuccess.Visible = "true"
                Me.linkcrono.Visible = True

            Case 5
                Me.Lblmsginfored.Text = "Se produjo un error en la carga de cronograma! "
                Me.Lblmsginfo.Text = ""
                Me.containerSuccess.Visible = "true"
        End Select

    End Sub
    Public Sub loadCombos(Optional ByVal type As Boolean = True)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim facade As New Facade

        Me.LoadDropDownListproject(facade, applicationCredentials)

    End Sub

    Private Sub LoadDropDownListproject(ByVal facade As Facade, ByVal applicationCredentials As ApplicationCredentials)

        Me.ddlproyect.DataSource = getprojectactainicioList(applicationCredentials)
        Me.ddlproyect.DataValueField = "Id"
        Me.ddlproyect.DataTextField = "code"
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

            sql.Append(" select id, convert(varchar(50),IdIdea) + '_' + Name + '_' + convert(varchar(50),id)  as code from  project  where Typeapproval <> 4  ")
            sql.Append(" order by (CreateDate) DESC  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objproject = New ProjectEntity

                ' cargar el valor del campo
                objproject.id = row("id")
                objproject.code = row("code")

                ' agregar a la lista
                projectList.Add(objproject)

            Next

            ' retornar el objeto
            getprojectactainicioList = projectList


        Catch ex As Exception

            Throw New Exception("Error al cargar la lista de Third. ")

        Finally
            ' liberando recursos
            objproject = Nothing

        End Try

    End Function


#End Region

End Class