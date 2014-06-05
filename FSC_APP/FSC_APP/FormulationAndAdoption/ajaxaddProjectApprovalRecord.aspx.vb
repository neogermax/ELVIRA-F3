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
        Dim code As Integer
        Dim name, campos_nuevos As String
        Dim id_b As Integer
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)



        'trae el jquery para hacer todo por debajo del servidor
        action = Request.QueryString("action").ToString()


        Select Case action

            Case "validarideas"
                'convierte la variable y llama funcion para la validacion de la idea
                code = Convert.ToInt64(Request.QueryString("code").ToString())

                Dim objResult As String = "{"

                objResult &= " ""mensaje"": """

                If ValideLocationIdea(code) = 1 Then
                    objResult &= " ciudades,"
                End If

                If ValidetrirIdea(code) = 1 Then
                    objResult &= " Actores,"
                End If

                If ValideValueIdea(code) = 1 Then
                    objResult &= " valores,"
                End If


                objResult = objResult.Trim(",")
                objResult &= """"

                If ValideLocationIdea(code) = 0 And ValidetrirIdea(code) = 0 And ValideValueIdea(code) = 0 And campos_nuevos = 0 Then
                    name = Request.QueryString("name")
                    objResult &= String.Format(", ""noaprobacion"": ""{0}""", CreateCodeIdea(code, name))
                End If

                objResult &= "}"

                Response.Write(objResult)

            Case "buscar"
                'convierte la variable y llama funcion para la validacion de la idea
                id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                buscardateidea(id_b, applicationCredentials, Request.QueryString("id"))

            Case "buscaractores"

                id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))


            Case "validar_campos_new"
                id_b = Convert.ToInt64(Request.QueryString("code").ToString())
                validar_campos_new(id_b)

            Case Else
               
        End Select



    End Sub

    Public Function validar_campos_new(ByVal codigo As Integer) As Integer

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim Data_campos, Data_flujos As DataTable

        Dim shiwch_val As Integer = 0

        Dim result_str As String = "Falta por diligenciar :"

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append("SELECT I.obligationsoftheparties,i.BudgetRoute,i.RiskMitigation,i.RisksIdentified FROM  Idea AS I where i.id =" & codigo)

        Data_campos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)


        sql = New StringBuilder

        sql.Append("select dcf.N_pago from Detailedcashflows dcf where dcf.IdIdea=" & codigo)

        Data_flujos = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        If Data_campos.Rows.Count > 0 Then

         
            If IsDBNull(Data_campos.Rows(0)("obligationsoftheparties")) = False Then
            Else
                result_str &= " obligaciones de las partes"
            End If

            If IsDBNull(Data_campos.Rows(0)("BudgetRoute")) = False Then
            Else
                If result_str = "Falta por diligenciar :" Then
                    result_str &= " ruta!"
                Else
                    result_str &= ", ruta"
                End If

            End If

            If IsDBNull(Data_campos.Rows(0)("RiskMitigation")) = False Then
            Else
                If result_str = "Falta por diligenciar :" Then
                    result_str &= " Mitigación de riesgos!"
                Else
                    result_str &= ",  Mitigación de riesgos"
                End If


            End If

            If IsDBNull(Data_campos.Rows(0)("RisksIdentified")) = False Then
            Else
                If result_str = "Falta por diligenciar :" Then
                    result_str &= " riesgos identificados!"
                Else
                    result_str &= ",  riesgos identificados"
                End If
            End If

        End If

        If Data_flujos.Rows.Count > 0 Then

        Else
            If result_str = "Falta por diligenciar :" Then

                result_str &= " flujos de pagos!"
            Else
                result_str &= ", flujos de pagos"
            End If

        End If

        If result_str <> "Falta por diligenciar :" Then
            result_str &= "!"
        End If

        Response.Write(result_str)

    End Function



    'funcion que valua las ciudades y trae los parametros
    Public Function ValideLocationIdea(ByVal codigo As Integer) As Integer

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion de ciudades
        sql.Append("exec ValueLocationIdea " & codigo)
        Dim data = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)




        If data = 1 Then
            Return 1
        Else
            Return 0
        End If


    End Function

    'funcion que valua los terceros y trae los parametros
    Public Function ValidetrirIdea(ByVal codigo As Integer) As Integer

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        'Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion de terceros
        sql.Append("exec ValueThirIdea " & codigo)
        Dim data = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data = 1 Then
            Return 1
        Else
            Return 0
        End If


    End Function

    'funcion que valua los valores y trae los parametros
    Public Function ValideValueIdea(ByVal codigo As Integer) As Integer

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        'Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion de terceros
        sql.Append("exec ValueValueIdea " & codigo)
        Dim data = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        If data = 1 Then
            Return 1
        Else
            Return 0
        End If


    End Function

    'funcion que crea el codigo de aprobación
    Public Function CreateCodeIdea(ByVal codigo As String, ByVal nombre As String) As String
        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion de ciudades
        sql.Append("exec maximeidaprovalidea")

        Dim data = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Dim code_idea As String

        code_idea = nombre + "_" + Convert.ToString(data)

        Return code_idea

    End Function



    Public Function buscardateidea(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThird As Integer) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim datatableactores As DataTable

        Dim name As String = ""
        Dim line As String = ""
        Dim program As String = ""
        Dim value As String = ""
        Dim value2 As String = ""
        Dim fscformat As String = ""
        Dim otrosformat As String = ""
        Dim html As String = ""

        sql.Append("select convert(bigint,REPLACE(ti.FSCorCounterpartContribution,'.','')) from Idea i  ")
        sql.Append("inner join ThirdByIdea ti on i.Id = ti.IdIdea  ")
        sql.Append(" inner join Third t on t.Id= ti.IdThird ")
        sql.Append("where (t.code = '8600383389' And ti.IdIdea = " & ididea & ")")

        Dim FSCval = GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

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

        Dim otrosval = GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)


        If otrosval = 0 Then
            otrosformat = "0"
        Else
            otrosformat = Format(Convert.ToInt64(otrosval), "#,###.##")
        End If

        sql = New StringBuilder
        'consulta de los datos de actores por id
        sql.Append("select distinct i.Id,i.Name,l.Code,p.Code as programa,i.Cost  from idea i  ")
        sql.Append("inner join ProgramComponentByIdea pi on (i.Id = pi.IdIdea)         ")
        sql.Append("inner join ProgramComponent pc on (pi.IdProgramComponent = pc.Id)  ")
        sql.Append("inner join Program p on (pc.IdProgram = p.Id)                      ")
        sql.Append("inner join StrategicLine l on (p.IdStrategicLine = l.Id)           ")

        sql.Append("where i.Id = " & ididea)
        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Dim objResult As String = "{"

            objResult &= " ""name"": """

            If IsDBNull(data.Rows(0)("Name")) = False Then
                name = data.Rows(0)("Name")
                name = name.Replace("""", "\""")
            End If

            objResult &= name
            objResult &= " "", ""line"": """

            If IsDBNull(data.Rows(0)("Code")) = False Then
                line = data.Rows(0)("Code")
            End If

            objResult &= line

            objResult &= " "", ""value"": """

            If IsDBNull(data.Rows(0)("cost")) = False Then
                value = data.Rows(0)("cost")
                value2 = Format(Convert.ToInt64(value), "#,###.##")
            End If

            objResult &= value2
            objResult &= """, ""program"": """

            If IsDBNull(data.Rows(0)("programa")) = False Then
                program = data.Rows(0)("programa")
            End If

            objResult &= program

            objResult &= """, ""FSC"": """
            objResult &= fscformat

            objResult &= """, ""otro"": """
            objResult &= otrosformat

            'objResult &= """, ""html"": """
            'objResult &= html

            objResult &= """}"

            Response.Write(objResult)

        Else
            Dim objResult As String = "0"
            Response.Write(objResult)
        End If


    End Function

    Public Function buscardatethird(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
           ByVal idThird As Integer) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        'consulta de los datos de actores por id
        sql.Append("select t.Name,t.contact,ti.Type,ti.VrSpecies,ti.Vrmoney ,ti.FSCorCounterpartContribution  from Third t       ")
        sql.Append("inner join ThirdByIdea ti on ti.IdThird= t.Id             ")
        sql.Append("inner join Idea i on i.Id = ti.IdIdea                     ")

        sql.Append("where i.Id = " & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String
        html = "<table id=""T_Actors"" style=""width: 100%;"" border=""1"" cellspacing=""1"" cellpadding=""1""><thead><tr align=""center""><th style=""width: 200px"">Actor</th><th style=""width: 200px"">Contacto</th><th style=""width: 131px"">Tipo</th><th style=""width: 131px"">Vr Especie</th><th style=""width: 131px"">Vr Dinero</th><th style=""width: 131px"">Valor Total</th></tr></thead><tbody>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 200px"">{0}</td><td style=""width: 200px"">{1}</td><td style=""width: 131px"">{2}</td><td style=""width: 131px"">{3}</td><td style=""width: 131px"">{4}</td><td style=""width: 131px"">{5}</td></tr>", itemDataTable(0), itemDataTable(1), itemDataTable(2), itemDataTable(3), itemDataTable(4), itemDataTable(5))
        Next
        html &= "</tbody></table>"

        Response.Write(html)

        
    End Function







End Class



