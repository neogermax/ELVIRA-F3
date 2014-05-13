Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Class Engagement_ajaxcontracrequest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim action As String
        Dim id_b As Integer
        Dim fecha As Date
        Dim duracion As String
        Dim contrato As String
        Dim proyecto As String
        Dim columna As String

        Try
            action = Request.QueryString("action").ToString()

            Select Case action
                Case "loadactors"
                    loadthirdddl(applicationCredentials, Request.QueryString("id"), Request.QueryString("tipopersona"))

                Case "buscar"
                    id_b = Convert.ToInt32(Request.QueryString("id").ToString())
                    buscardatethird(id_b, applicationCredentials, Request.QueryString("id"))

                Case "calculafechas"
                    fecha = Convert.ToDateTime(Request.QueryString("fecha").ToString())
                    duracion = Request.QueryString("duracion").ToString()
                    fecha = fecha.ToString("yyyy/MM/dd")
                    calculafechas(fecha, duracion)

                Case "validarcontrato"
                    contrato = Request.QueryString("contrato").ToString()
                    buscardatecontract(contrato, applicationCredentials)

                Case "getproject"
                    proyecto = Request.QueryString("proyectid").ToString()

                    If proyecto > 0 Then
                        columna = Request.QueryString("columna").ToString()
                        buscarproyecto(proyecto, columna, applicationCredentials)
                    End If

                Case "getsupervisor"
                    contrato = Request.QueryString("contract").ToString()
                    buscarsupervisor(contrato, applicationCredentials)
                Case Else
            End Select
        Catch ex As Exception

        End Try

        

    End Sub

    Public Function loadthirdddl(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
          ByVal idThird As Integer, ByVal persona As String) As String

        Dim thirdlist As New List(Of ThirdEntity)
        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable

        sql.Append(" select Third.id,Third.Name, Third.PersonaNatural from Third ")
        sql.Append(" where Third.PersonaNatural = " & persona)
        sql.Append(" order by (Name) ")

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim html As String = ""

        For Each row As DataRow In data.Rows
            html &= String.Format("<option value =""{0}"">{1}</option>", row(0).ToString(), row(1).ToString())
        Next

        Response.Write(html)

    End Function

    Public Function buscardatethird(ByVal bybal As Integer, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
     ByVal idThird As Integer) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim name As String = ""
        Dim code As String = ""
        Dim documents As String = ""
        Dim RepresentanteLegal As String = ""
        

        'consulta de los datos de actores por id
        sql.Append("SELECT name,Code,documents,RepresentanteLegal  FROM Third ")
        sql.Append("where Id = " & idThird)
        ' ejecutar la intruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then

            Dim objResult As String = "{"

            objResult &= " ""nit"": """

            If IsDBNull(data.Rows(0)("code")) = False Then
                code = data.Rows(0)("code")
            End If

            objResult &= code
            objResult &= " "", ""replegal"": """

            If IsDBNull(data.Rows(0)("RepresentanteLegal")) = False Then
                RepresentanteLegal = data.Rows(0)("RepresentanteLegal")
            End If

            objResult &= RepresentanteLegal

            objResult &= " "", ""name"": """

            If IsDBNull(data.Rows(0)("name")) = False Then
                name = data.Rows(0)("name")
            End If

            objResult &= name

            objResult &= " "", ""documents"": """

            If IsDBNull(data.Rows(0)("documents")) = False Then
                documents = data.Rows(0)("documents")
            End If
            objResult &= documents
           
            objResult &= """}"

            Response.Write(objResult)
        End If


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

    Public Function buscardatecontract(ByVal contrato As String, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim DATA As DataTable

        'consulta de los datos de actores por id
        sql.Append("select ContractNumberAdjusted from ContractRequest ")
        sql.Append("where ContractNumberAdjusted = '" & contrato & "'")

        DATA = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim objResult As String = ""

        If DATA.Rows.Count > 0 Then
            objResult = "OK"
        Else
            objResult = "NO"
        End If

        Response.Write(objResult)

    End Function

    Public Function buscarproyecto(ByVal idProyect As String, ByVal columna As String, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data As DataTable
        Dim fecha As Date

        'consultar los datos
        sql.Append("SELECT ")

        'seleccionar columna
        sql.Append(columna)

        sql.Append(" From Project WHERE id = " & idProyect)

        'ejecutar la instruccion
        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        If data.Rows.Count > 0 Then
            'Si hay fechas
            Dim objresult As String
            objresult = data.Rows(0)(columna)

            If InStr(objresult, "/", CompareMethod.Text) > 0 Then
                'Es fecha
                fecha = objresult
                objresult = fecha.ToString("yyyy/MM/dd")
            End If

            Response.Write(objresult)
        Else
            'No hay fechas
        End If

    End Function

    Public Function buscarsupervisor(ByVal contrato As String, ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim DATA As DataTable

        'consulta de los datos de actores por id
        sql.Append("SELECT s.Third_Id FROM SupervisorbyContractReq s ")
        sql.Append("inner join Third t on s.Third_Id = t.Id ")
        sql.Append("where(contractrequest_id = " & contrato & ")")

        DATA = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        Dim objResult As String = ""

        If DATA.Rows.Count > 0 Then
            objResult = "OK"
        Else
            objResult = "NO"
        End If

        Response.Write(objResult)

    End Function

End Class



