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
Imports Newtonsoft.Json

Partial Public Class GeneralPlanning_AjaxAddThird
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim id_third, code, S_Type_people, S_Type_document, S_Document, S_Name, S_Legal_representative, S_L_rep_doc, S_Sex, S_Phone, S_Address, S_Email, S_Contact As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        If Request.Form("action") <> Nothing Then

            Dim option_proyecto = Request.Form("action")

            Select option_proyecto

                Case "save"

                    S_Type_people = Request.Form("Type_people").ToString()
                    S_Type_document = Request.Form("Type_document").ToString()
                    S_Document = Request.Form("Document").ToString()
                    S_Name = Request.Form("Name").ToString()
                    S_Legal_representative = Request.Form("Legal_representative").ToString()
                    S_L_rep_doc = Request.Form("L_rep_doc").ToString()
                    S_Sex = Request.Form("Sex").ToString()
                    S_Phone = Request.Form("Phone").ToString()
                    S_Address = Request.Form("Address").ToString()
                    S_Email = Request.Form("Email").ToString()
                    S_Contact = Request.Form("Contact").ToString()
                    CREATE_actors(S_Type_people, S_Type_document, S_Document, S_Name, S_Legal_representative, S_L_rep_doc, S_Sex, S_Phone, S_Address, S_Email, S_Contact)

            End Select

        Else
            'trae el jquery para hacer todo por debajo del servidor
            action = Request.QueryString("action").ToString()

            Select Case action

                Case "load_combos"
                    load_combos()

                Case "verifico"
                    code = Request.QueryString("nit").ToString()
                    verificarnit(code)

                Case "Charge_combos"
                    id_third = Request.QueryString("Id_third").ToString()
                    Charge_combos(id_third)

                Case Else

            End Select

        End If
       
    End Sub

    Protected Function Charge_combos(ByVal third As String)

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        sql.Append(" select d.ID from FSC_eSecurity.dbo.document d ")
        sql.Append(" inner join Third t on t.tipodocumento =d.Id ")
        sql.Append(" where t.id = " & third)
        ' ejecutar la intruccion
        Dim Data_document = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        'reiniciamos la variable de consulte
        sql = New StringBuilder

        sql.Append(" select p.ID, p.descripcion from FSC_eSecurity.dbo.people p ")
        sql.Append(" inner join Third t on t.PersonaNatural =p.Id ")
        sql.Append(" where t.id = " & third)

        ' ejecutar la intruccion
        Dim Data_people = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        'reiniciamos la variable de consulte
        sql = New StringBuilder

        sql.Append("  ")
        sql.Append("  ")
        sql.Append("  ")

        ' ejecutar la intruccion
        Dim Data_sex = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)


    End Function

    Protected Function load_combos()

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand
        Dim data_document, Data_people, Data_sex As DataTable
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        '---------------- region de querys--------------------------------
        sql.Append(" select ID, descripcion from FSC_eSecurity.dbo.document  ORDER BY ID ")
        ' ejecutar la intruccion
        data_document = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'reiniciamos la variable de consulte
        sql = New StringBuilder

        sql.Append(" select ID, descripcion from FSC_eSecurity.dbo.people ORDER BY ID ")
        ' ejecutar la intruccion
        Data_people = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)

        'reiniciamos la variable de consulte
        sql = New StringBuilder

        sql.Append("select ID, descripcion from FSC_eSecurity.dbo.sex  ORDER BY ID")
        ' ejecutar la intruccion
        Data_sex = GattacaApplication.RunSQLRDT(applicationCredentials, sql.ToString)
        '---------------- end region de querys--------------------------------

        Dim Json_people = JsonConvert.SerializeObject(Data_people)
        Dim Json_document = JsonConvert.SerializeObject(data_document)
        Dim Json_sex = JsonConvert.SerializeObject(Data_sex)


        Dim objCatalogSerialize = String.Format("[{0},{1},{2}]", Json_people, Json_document, Json_sex)

        Response.Write(objCatalogSerialize)

    End Function

    Protected Function CREATE_actors(ByVal Type_people As String, ByVal Type_document As String, ByVal Document As String, ByVal Name As String, ByVal Legal_representative As String, ByVal L_rep_doc As String, ByVal Sex As String, ByVal Phone As String, ByVal Address As String, ByVal Email As String, ByVal Contact As String)

        Dim facade As New Facade
        Dim objThird As New ThirdEntity
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim V_NIT As String

        'validamos si viene vacio el nit
        If Document = "" Then
            'llamamos funcion que genera el nit por defecto
            V_NIT = validate_NIT_provicional(Document)
            'validamos si es el primer nitpor defecto asignamos al objeto corespondiente
            If V_NIT = "" Then
                objThird.code = "NIT_D01"
            Else
                objThird.code = V_NIT
            End If
        Else
            objThird.code = Document
        End If

        objThird.name = clean_vbCrLf(Name)
        objThird.phone = clean_vbCrLf(Phone)
        objThird.email = clean_vbCrLf(Email)
        objThird.enabled = 1
        objThird.iduser = applicationCredentials.UserID
        objThird.PersonaNatural = Type_people
        objThird.representantelegal = clean_vbCrLf(Legal_representative)
        objThird.createdate = Now
        objThird.tipodocumento = Type_document
        objThird.docrepresentante = clean_vbCrLf(L_rep_doc)
        objThird.direccion = clean_vbCrLf(Address)
        objThird.documents = Document


        If Type_people = 2 Then
            objThird.contact = clean_vbCrLf(Contact)
        Else
            objThird.contact = clean_vbCrLf(Name)
        End If

       
        If Sex = "No Aplica" Then
            objThird.sex = 0
        Else
            objThird.sex = Sex
        End If

        objThird.id = facade.addThird(applicationCredentials, objThird)

        Dim result As Integer


        If objThird.id <> 0 Then
            result = 1
        Else
            result = 0
        End If

        Response.Write(result)

    End Function

    Protected Function verificarnit(ByVal codigo As String)

        Dim facade As New Facade
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)
        Dim Result As String

        If facade.verifyThirdCode(applicationCredentials, codigo) Then

            Result = 1
        Else
            Result = 0
        End If

        Response.Write(Result)
    End Function

    Protected Function validate_NIT_provicional(ByVal nit As String) As String

        Dim sql As New StringBuilder
        Dim objSqlCommand As New SqlCommand

        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)


        'consulta y validacion NIT por default
        sql.Append("select max(CAST(substring(code,6,15) as int)) + 1  as numeros from Third where Code like  '%NIT_D%' ")
        Dim data_nit = GattacaApplication.RunSQL(applicationCredentials, sql.ToString(), 174, Nothing, CommandType.Text, "DB1", "FSC", True)

        Dim NIT_Default As String

        NIT_Default = "NIT_D" + Convert.ToString(data_nit)

        Return NIT_Default
    End Function

    Private Function clean_vbCrLf(ByVal text As String)

        Dim pattern As String = vbCrLf
        Dim replacement As String = " "
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(text, replacement)

        Return result

    End Function

End Class