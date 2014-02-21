Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient


Partial Class FormulationAndAdoption_ajaxChargeTextfieldProject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim idIdea As Integer
        'trae el jquery para hacer todo por debajo del servidor
        action = Request.QueryString("action").ToString()

        Select Case action
            Case "getIdeaProject"
                'convierte la variable y llama funcion para la validacion de la idea
                'Dim va As Integer = Request.QueryString("id")
                idIdea = Convert.ToInt32(Request.QueryString("id").ToString())
                searchIdea(idIdea, applicationCredentials)

            Case "getListActors"
                Dim va As Integer = Request.QueryString("id")
                buscardatethird(va, applicationCredentials)

            Case "getListComponentPrograms"
                Dim va As Integer = Request.QueryString("id")
                searchComponentsProgram(va, applicationCredentials)

            Case "calculafechas"
                Dim fecha As Date
                Dim duracion As String
                If (Request.QueryString("fecha").ToString() <> "") Then
                    'fecha = Convert.ToDateTime(Request.QueryString("fecha").ToString())
                    fecha = Request.QueryString("fecha").ToString()
                    duracion = Request.QueryString("duration").ToString()

                    calculafechas(fecha, duracion)
                End If

            Case Else
        End Select
    End Sub

    Public Sub searchIdea(ByVal id As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials)
        Dim sql As New StringBuilder
        Dim sqlTotalFSC As New StringBuilder
        Dim sqlTotalNoFSC As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim objSqlCommandTotal As New SqlCommand

        Dim data As DataTable
        Dim dataTotalFSC As DataTable
        Dim dataTotalNoFSC As DataTable
        Dim lineEstrategic As String = ""
        Dim programa As String = ""
        Dim Objective As String = ""
        Dim Justification As String = ""
        Dim AreaDescription As String = ""
        Dim ResultsBenef As String = ""
        Dim ResultsKnowledgeManagement As String = ""
        Dim ResultsInstalledCapacity As String = ""
        Dim StartDate As String = ""
        Dim Duration As String = ""
        Dim Population As String = ""
        Dim VrmoneyTotal As Long = 0
        Dim VrSpeciesTotal As Long = 0
        Dim total As Long = 0
        Dim totalNoFsc As Long = 0
        Dim fuente As String = ""
        Dim habilitado As Boolean
        Dim ApprovedValue As Long = 0

       
        sql.Append("select  TOP 1 sl.Name, p.Name as programa, i.Objective, i.Justification, i.AreaDescription, i.Results, ")
        sql.Append("i.ResultsKnowledgeManagement, i.ResultsInstalledCapacity, i.StartDate, i.Duration, ")
        sql.Append(" i.Population, ")
        sql.Append(" i.Source, i.Enabled, par.ApprovedValue, par.aportFSC, par.aportOtros  ")
        sql.Append(" from Idea i join ProgramComponentByIdea pci on (pci.IdIdea=i.Id) ")
        sql.Append(" join ProgramComponent pc on (pc.Id=pci.IdProgramComponent)")
        sql.Append(" join Program p on (p.Id=pc.IdProgram) ")
        sql.Append(" join StrategicLine sl on (sl.Id=p.IdStrategicLine) ")
        sql.Append(" left join ThirdByIdea tbi on (tbi.IdIdea=i.Id) ")
        sql.Append(" left join ProjectApprovalRecord par on (par.Ididea=i.Id) ")
        sql.Append(" WHERE i.Id =" & id)

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)
        If data.Rows.Count > 0 Then
            Dim objResult As String = "{"
            objResult &= " ""Name"":"""

            If IsDBNull(data.Rows(0)("Name")) = False Then
                lineEstrategic = data.Rows(0)("Name")
                lineEstrategic = lineEstrategic.Replace("""", "\""")
            End If
            objResult &= lineEstrategic

            objResult &= """, ""programa"": """
            If IsDBNull(data.Rows(0)("programa")) = False Then
                programa = data.Rows(0)("programa")
            End If
            objResult &= programa
            ' obtiene el objetivo de IDEA 
            objResult &= """, ""Objective"": """
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

            ' obtiene fecha de inicio
            objResult &= """, ""StartDate"": """
            If IsDBNull(data.Rows(0)("StartDate")) = False Then
                StartDate = data.Rows(0)("StartDate")
            End If
            Dim dateFormated As Date = StartDate
            objResult &= dateFormated.ToString("yyyy/MM/dd")

            ' obtiene duration
            objResult &= """, ""Duration"": """
            If IsDBNull(data.Rows(0)("Duration")) = False Then
                Duration = data.Rows(0)("Duration")
            End If
            objResult &= Duration

            'calculo fecha finalizacion
            Dim arrdias() As String
            Dim decimas As String
            Dim dias As Double
            Dim meses As Double

            'Cambiar puntos por comas
            Duration = Replace(Duration, ".", ",", 1)

            'Calcular los dias

            arrdias = Split(Duration, ",", , CompareMethod.Text)

            If UBound(arrdias) > 0 Then
                decimas = "0," & arrdias(1)
                dias = CInt(decimas * 30)
                meses = CInt(arrdias(0))
            Else
                meses = Duration
                dias = 0
            End If

            Dim fechafinal As Date
            'calcular la fecha final
            fechafinal = CDate(dateFormated)
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

            finalizacion = fechaok.ToString("yyyy/MM/dd")
            objResult &= """, ""finalizacion"": """ & finalizacion


            ' obtiene Poblacion
            objResult &= """, ""Population"": """
            If IsDBNull(data.Rows(0)("Population")) = False Then
                Population = data.Rows(0)("Population")
            End If
            objResult &= Population
            ' devuelve valor total
            'If IsDBNull(data.Rows(0)("vrmoney_total")) = False Then
            'VrmoneyTotal = data.Rows(0)("vrmoney_total")
            'End If
            'If IsDBNull(data.Rows(0)("vrspecies_total")) = False Then
            'VrSpeciesTotal = data.Rows(0)("vrspecies_total")
            'End If

            'total = VrmoneyTotal + VrSpeciesTotal


            ' obtiene total de FSC
            objResult &= """, ""total"": """

            If IsDBNull(data.Rows(0)("aportFSC")) = False Then
                total = data.Rows(0)("aportFSC")
            End If
            objResult &= total

            ' obtiene total de LOS NO FSC 
            objResult &= """, ""totalNoFsc"": """
            If IsDBNull(data.Rows(0)("aportOtros")) = False Then

                totalNoFsc = data.Rows(0)("aportOtros")
            End If
            objResult &= totalNoFsc

            'obtiene fuente
            objResult &= """, ""fuente"": """
            If IsDBNull(data.Rows(0)("Source")) = False Then
                fuente = data.Rows(0)("Source")
            End If
            objResult &= fuente

            ' obtiene si esta habilitado o no
            objResult &= """, ""enabled"": """
            If IsDBNull(data.Rows(0)("Enabled")) = False Then
                habilitado = data.Rows(0)("Enabled")

            End If
            If habilitado = True Then

                objResult &= "habilitado"
            Else
                objResult &= "deshabilitado"
            End If

            ' obtiene valor aprobado en dinero
            objResult &= """, ""ApprovedValue"": """
            If IsDBNull(data.Rows(0)("ApprovedValue")) = False Then
                ApprovedValue = data.Rows(0)("ApprovedValue")
            End If
            objResult &= ApprovedValue


            objResult &= """}"



            Response.Write(objResult)
        End If

    End Sub

    '  funcion que busca los datos de terceros y los convierte en una tabla///////////////////////////////////////////////////////////////////////////////////

    Public Function buscardatethird(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable


        'consulta de los datos de actores por id
        sql.Append("select t.Id, t.Name,ti.type, t.contact, ti.Vrmoney, ti.VrSpecies, ti.FSCorCounterpartContribution from Third t   ")
        sql.Append("inner join ThirdByIdea ti on ti.IdThird= t.Id             ")
        sql.Append("inner join Idea i on i.Id = ti.IdIdea                     ")

        sql.Append("where i.Id = " & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String
        html = "<table  style=""font-family: 'Calibri';"" border=1 cellspacing=0 cellpadding=2 bordercolor=""666633"" >"
        html &= " <tr>"
        html &= " <td colspan=""7"">"
        html &= " <h2 align=center> Actores </h2>"
        html &= " </tr>"
        html &= " </td>"
        html &= " <tr align=""center""><td style=""width: 0px"">Id</td><td style=""width: 200px"">Actor</td><td style=""width: 200px"">Tipo</td><td style=""width: 200px"">Contacto</td><td style=""width: 131px"">VALOR EFECTIVO</td><td style=""width: 131px"">VALOR ESPECIE</td><td style=""width: 131px"">TOTAL</td></tr>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 0px"" >{0}</td><td style=""width: 200px"">{1}</td><td style=""width: 200px"">{2}</td><td style=""width: 131px"">{3}</td><td style=""width: 131px"">{4}</td><td style=""width: 131px"">{5}</td><td style=""width: 131px"">{6}</td></tr>", itemDataTable(0), itemDataTable(1), itemDataTable(2), itemDataTable(3), itemDataTable(4), itemDataTable(5), itemDataTable(6))
        Next
        html &= " </table>"

        Response.Write(html)
    End Function

    ' funcion que busca los componentes para idea aprobada

    Public Function searchComponentsProgram(ByVal ididea As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable


        'consulta de los datos de componentes programa por id
        sql.Append("SELECT pc.Code FROM ProgramComponentByIdea pci JOIN ProgramComponent pc  ")
        sql.Append(" ON(pc.Id=pci.IdProgramcomponent)")
        sql.Append("WHERE pci.IdIdea =" & ididea)

        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String
        html = "<table  style=""font-family: 'Calibri';"" border=1 cellspacing=0 cellpadding=2 bordercolor=""666633"" >"
        html &= " <tr>"
        html &= " <td colspan=""6"">"
        html &= " <h2 align=center> Componentes de programa </h2>"
        html &= " </tr>"
        html &= " </td>"
        html &= " <tr align=""center""><td style=""width: 200px"">COMPONENTES DE PROGRAMA</td></tr>"
        For Each itemDataTable As DataRow In data.Rows
            html &= String.Format(" <tr align=""center""><td style=""width: 500px"" >{0}</td></tr>", itemDataTable(0))
        Next
        html &= " </table>"




        Response.Write(html)
    End Function

    ' funcion que calcula fecha final segun duracion en meses

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



End Class
