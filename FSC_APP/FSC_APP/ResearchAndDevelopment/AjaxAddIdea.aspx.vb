
Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Class ResearchAndDevelopment_AjaxAddIdea
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion As String
        Dim S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost As String
        Dim id_lineStrategic, id_depto As Integer
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

                calculafechas(fecha, duracion)
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
                S_A_Mfsc = Request.QueryString("A_Mfsc").ToString
                S_A_Efsc = Request.QueryString("A_Efsc").ToString
                S_A_Mcounter = Request.QueryString("A_Mcounter").ToString
                S_A_Ecounter = Request.QueryString("A_Ecounter").ToString
                S_cost = Request.QueryString("cost").ToString

                save_IDEA(S_code, S_linea_estrategica, S_programa, S_nombre, S_justificacion, S_objetivo, S_objetivo_esp, S_Resultados_Benef, S_Resultados_Ges_c, S_Resultados_Cap_i, S_Fecha_inicio, S_mes, S_dia, S_Fecha_fin, S_Población, S_contratacion, S_A_Mfsc, S_A_Efsc, S_A_Mcounter, S_A_Ecounter, S_cost)

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

 
            Case Else

        End Select

    End Sub

  

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

        data_typecontect = Facade.gettypecontract(applicationCredentials, order:="id")

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
    Public Function save_IDEA(ByVal code As String, ByVal line_strategic As String, ByVal program As String, ByVal name As String, ByVal justify As String, ByVal objetive As String, ByVal obj_esp As String, ByVal resul_bef As String, ByVal resul_ges_c As String, ByVal resul_cap_i As String, ByVal fecha_i As String, ByVal mes As String, ByVal dia As String, ByVal fecha_f As String, ByVal poblacion As String, ByVal contratacion As String, ByVal A_Mfsc As String, ByVal A_Efsc As String, ByVal A_Mcounter As String, ByVal A_Ecounter As String, ByVal cost As String)

        Dim facade As New Facade
        Dim objIdea As New IdeaEntity
        Dim myProgramComponentByIdeaList As List(Of ProgramComponentByIdeaEntity) = New List(Of ProgramComponentByIdeaEntity)
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
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
            'objIdea.source = Me.ddlSource.SelectedValue
            'objIdea.idsummoning = Me.ddlSummoning.SelectedValue
            'objIdea.startprocess = Me.chkStartProcess.Checked
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


    Public Function calculafechas(ByVal fecha As DateTime, ByVal duracion As String) As String

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
                dias = 0
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

        Return result

    End Function

End Class


