Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class IdeaDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary>
    ''' Verifica que la entidad no este aprovada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="EntryData"></param>
    ''' <param name="idEntryData"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyapprove(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal EntryData As String, _
     ByVal idEntryData As String, _
        ByVal Status As String) As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try


            'Se usa antes de ingresar un nuevo registro
            sql.AppendLine("Select Id from " & dbBPMName & ".dbo.ProcessInstance ProcessInstance")
            sql.AppendLine(" where EntryData='" & EntryData & "' and IDEntryData='" & idEntryData & "'and Status='" & Status & "'")

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then


                ' retornar que no existe
                verifyapprove = True

            Else

                ' retornar que existe
                verifyapprove = False

            End If



        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyapprove")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Idea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function


    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal code As String, _
    Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Idea WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Idea WHERE Code = '" & code & "' AND id <> '" & id & "'")

            End If

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyCode = False

                Else

                    ' retornar que existe
                    verifyCode = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de Idea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo Idea
    ''' </summary>
    ''' <param name="Idea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Idea As IdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        ' TODO: 17 ideadalc add se crean nuevos campos
        ' Autor: German Rodriguez MGgroup
        ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Idea(" & _
             "name," & _
             "objective," & _
             "startdate," & _
             "duration," & _
             "days," & _
             "areadescription," & _
             "population," & _
             "cost," & _
             "fsccontribution," & _
             "counterpartvalue," & _
             "strategydescription," & _
             "results," & _
             "source," & _
             "justification," & _
             "idsummoning," & _
             "startprocess," & _
             "createdate," & _
             "iduser," & _
             "enabled," & _
             "idProcessInstance," & _
             "idActivityInstance," & _
             "ResultsKnowledgeManagement," & _
             "ResultsInstalledCapacity," & _
             "OtherResults," & _
             "Loadingobservations," & _
             "idtypecontract," & _
             "Enddate," & _
             "obligationsoftheparties," & _
             "RiskMitigation," & _
             "RisksIdentified," & _
             "ideaappliesIVA," & _
             "BudgetRoute" & _
             ")")

            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Idea.name & "',")
            sql.AppendLine("'" & Idea.objective & "',")
            sql.AppendLine("'" & Idea.startdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & Idea.duration & "',")
            sql.AppendLine("'" & Idea.dia & "',")
            sql.AppendLine("'" & Idea.areadescription & "',")
            sql.AppendLine("'" & Idea.population & "',")
            sql.AppendLine("'" & Idea.cost.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Idea.fsccontribution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Idea.counterpartvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Idea.strategydescription & "',")
            sql.AppendLine("'" & Idea.results & "',")
            sql.AppendLine("'" & Idea.source & "',")
            sql.AppendLine("'" & Idea.justification & "',")
            sql.AppendLine("'" & Idea.idsummoning & "',")
            sql.AppendLine("'" & Idea.startprocess & "',")
            sql.AppendLine("'" & Idea.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & Idea.iduser & "',")
            sql.AppendLine("'" & Idea.enabled & "',")
            sql.AppendLine("'" & Idea.IdProcessInstance & "',")
            sql.AppendLine("'" & Idea.IdActivityInstance & "',")
            sql.AppendLine("'" & Idea.ResultsKnowledgeManagement & "',")
            sql.AppendLine("'" & Idea.ResultsInstalledCapacity & "',")
            sql.AppendLine("'" & Idea.OthersResults & "',")
            sql.AppendLine("'" & Idea.Loadingobservations & "',")
            sql.AppendLine("'" & Idea.idtypecontract & "',")
            sql.AppendLine("'" & Idea.Enddate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & Idea.Obligaciones & "',")
            sql.AppendLine("'" & Idea.riesgos & "',")
            sql.AppendLine("'" & Idea.mitigacion & "',")
            sql.AppendLine("'" & Idea.iva & "',")
            sql.AppendLine("'" & Idea.presupuestal & "')")

            ' TODO: 17 ideadalc add se crean nuevos campos
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))
            sql.Remove(0, sql.Length)
            'modifica el codigo con el mismo Id 
            sql.AppendLine("Update Idea SET")
            sql.AppendLine(" code = '" & num & "'")
            sql.AppendLine(" WHERE id = " & num)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

            ' retornar
            Return num

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al insertar el Idea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Idea por el Id
    ''' </summary>
    ''' <param name="idIdea"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As IdeaEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objIdea As New IdeaEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Idea.* , ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM Idea ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Idea.IdUser = ApplicationUser.Id  ")
            sql.Append(" WHERE Idea.Id = " & idIdea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then


                ' cargar los datos
                objIdea.id = data.Rows(0)("id")
                objIdea.code = data.Rows(0)("code")
                objIdea.name = data.Rows(0)("name")
                objIdea.objective = data.Rows(0)("objective")
                objIdea.startdate = data.Rows(0)("startdate")
                objIdea.duration = data.Rows(0)("duration")
                objIdea.areadescription = data.Rows(0)("areadescription")
                objIdea.population = data.Rows(0)("population")
                objIdea.cost = data.Rows(0)("cost")
                If Not (IsDBNull(data.Rows(0)("fsccontribution"))) Then objIdea.fsccontribution = data.Rows(0)("fsccontribution")
                If Not (IsDBNull(data.Rows(0)("counterpartvalue"))) Then objIdea.counterpartvalue = data.Rows(0)("counterpartvalue")
                objIdea.strategydescription = data.Rows(0)("strategydescription")
                objIdea.results = data.Rows(0)("results")
                objIdea.source = data.Rows(0)("source")
                If Not (IsDBNull(data.Rows(0)("justification"))) Then objIdea.justification = data.Rows(0)("justification")
                objIdea.idsummoning = data.Rows(0)("idsummoning")
                objIdea.startprocess = data.Rows(0)("startprocess")
                objIdea.createdate = data.Rows(0)("createdate")
                If Not (IsDBNull(data.Rows(0)("iduser"))) Then objIdea.iduser = data.Rows(0)("iduser")
                objIdea.enabled = data.Rows(0)("enabled")
                If Not (IsDBNull(data.Rows(0)("ApplicationUserName"))) Then objIdea.USERNAME = data.Rows(0)("ApplicationUserName")
                If Not (IsDBNull(data.Rows(0)("IdProcessInstance"))) Then objIdea.IdProcessInstance = data.Rows(0)("IdProcessInstance")
                If Not (IsDBNull(data.Rows(0)("IdActivityInstance"))) Then objIdea.IdActivityInstance = data.Rows(0)("IdActivityInstance")

                ' TODO: 23 ideadalc load  se crean nuevos campos
                ' Autor: German Rodriguez MGgroup
                ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

                If Not (IsDBNull(data.Rows(0)("ResultsKnowledgeManagement"))) Then objIdea.ResultsKnowledgeManagement = data.Rows(0)("ResultsKnowledgeManagement")
                If Not (IsDBNull(data.Rows(0)("ResultsInstalledCapacity"))) Then objIdea.ResultsInstalledCapacity = data.Rows(0)("ResultsInstalledCapacity")
                If Not (IsDBNull(data.Rows(0)("Loadingobservations"))) Then objIdea.Loadingobservations = data.Rows(0)("Loadingobservations")
                If Not (IsDBNull(data.Rows(0)("idtypecontract"))) Then objIdea.idtypecontract = data.Rows(0)("idtypecontract")

                If Not (IsDBNull(data.Rows(0)("RiskMitigation"))) Then objIdea.mitigacion = data.Rows(0)("RiskMitigation")
                If Not (IsDBNull(data.Rows(0)("RisksIdentified"))) Then objIdea.riesgos = data.Rows(0)("RisksIdentified")
                If Not (IsDBNull(data.Rows(0)("obligationsoftheparties"))) Then objIdea.Obligaciones = data.Rows(0)("obligationsoftheparties")
                If Not (IsDBNull(data.Rows(0)("BudgetRoute"))) Then objIdea.presupuestal = data.Rows(0)("BudgetRoute")
                If Not (IsDBNull(data.Rows(0)("days"))) Then objIdea.dia = data.Rows(0)("days")
                If Not (IsDBNull(data.Rows(0)("OtherResults"))) Then objIdea.OthersResults = data.Rows(0)("OtherResults")
                If Not (IsDBNull(data.Rows(0)("ideaappliesIVA"))) Then objIdea.iva = data.Rows(0)("ideaappliesIVA")


                ' TODO: 23 ideadalc load  se crean nuevos campos
                ' Autor: German Rodriguez MGgroup
                ' cierre de cambio



            End If

            ' retornar el objeto
            Return objIdea

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Idea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objIdea = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="objective"></param>
    ''' <param name="startdate"></param>
    ''' <param name="duration"></param>
    ''' <param name="areadescription"></param>
    ''' <param name="population"></param>
    ''' <param name="cost"></param>
    ''' <param name="strategydescription"></param>
    ''' <param name="results"></param>
    ''' <param name="source"></param>
    ''' <param name="idsummoning"></param>
    ''' <param name="startprocess"></param>
    ''' <param name="startprocesstext"></param>
    ''' <param name="createdate"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <returns>un objeto de tipo List(Of IdeaEntity)</returns>
    ''' <remarks></remarks>
    ''' TODO: 24 ideadalc getlist se crean nuevos campos
    ''' Autor: German Rodriguez MGgroup
    ''' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal objective As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal areadescription As String = "", _
        Optional ByVal population As String = "", _
        Optional ByVal cost As String = "", _
        Optional ByVal strategydescription As String = "", _
        Optional ByVal results As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal idsummoning As String = "", _
        Optional ByVal startprocess As String = "", _
        Optional ByVal startprocesstext As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal StrategicLineName As String = "", _
        Optional ByVal ProgramName As String = "", _
        Optional ByVal ProgramComponentName As String = "", _
        Optional ByVal Loadingobservations As String = "", _
        Optional ByVal ResultsKnowledgeManagement As String = "", _
        Optional ByVal ResultsInstalledCapacity As String = "", _
        Optional ByVal idtypecontract As String = "", _
        Optional ByVal order As String = "") As List(Of IdeaEntity)

        ' TODO: 24 ideadalc getlist se crean nuevos campos
        ' Autor: German Rodriguez MGgroup
        'cierre de cambios

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIdea As IdeaEntity
        Dim IdeaList As New List(Of IdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim where1 As String = " WHERE "
        Dim where2 As String = " WHERE "
        Dim where3 As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("  SELECT Idea.*, ProgramComponentName,  ProgramName, StrategicLineName, ApplicationUser.Name AS ApplicationUserName ")
            sql.Append(" FROM Idea ")
            sql.Append(" Left join  (Select l.Id,")
            sql.Append(" (Select Top 1 SA.Name AS ProgramComponentName  ")
            sql.Append(" FROM ProgramComponentByIdea")
            sql.Append(" LEFT JOIN ProgramComponent SA ON ProgramComponentByIdea.IdProgramComponent = SA.Id  ")
            sql.Append(" LEFT JOIN Program MA ON SA.IdProgram = MA.Id LEFT JOIN StrategicLine ON MA.IdStrategicLine = StrategicLine.Id   ")
            sql.Append(where1 & " l.Id=ProgramComponentByIdea.IdIdea ")
            where1 = " AND "
            ' verificar si hay entrada de datos para el campo linea estrategica
            If Not StrategicLineName.Equals("") Then
                sql.Append(where1 & " StrategicLine.Name like '%" & StrategicLineName.Trim() & "%'")
                where1 = " AND "
            End If
            ' verificar si hay entrada de datos para el campo Programa
            If Not ProgramName.Equals("") Then
                sql.Append(where1 & " MA.Name like '%" & ProgramName.Trim() & "%'")
                where1 = " AND "
            End If

            ' verificar si hay entrada de datos para el campo Componente del Programa
            If Not ProgramComponentName.Equals("") Then
                sql.Append(where1 & " SA.Name like '%" & ProgramComponentName.Trim() & "%'")
                where1 = " AND "
            End If
            sql.Append(")as ProgramComponentName,   ")
            sql.Append(" (Select Top 1  MA.Name AS ProgramName  ")
            sql.Append(" FROM ProgramComponentByIdea")
            sql.Append(" LEFT JOIN ProgramComponent SA ON ProgramComponentByIdea.IdProgramComponent = SA.Id  ")
            sql.Append(" LEFT JOIN Program MA ON SA.IdProgram = MA.Id LEFT JOIN StrategicLine ON MA.IdStrategicLine = StrategicLine.Id   ")
            sql.Append(where2 & " l.Id=ProgramComponentByIdea.IdIdea ")
            where2 = " AND "
            ' verificar si hay entrada de datos para el campo Linea estrategica
            If Not StrategicLineName.Equals("") Then
                sql.Append(where2 & " StrategicLine.Name like '%" & StrategicLineName.Trim() & "%'")
                where2 = " AND "
            End If
            ' verificar si hay entrada de datos para el campo Programa
            If Not ProgramName.Equals("") Then
                sql.Append(where2 & " MA.Name like '%" & ProgramName.Trim() & "%'")
                where2 = " AND "
            End If

            ' verificar si hay entrada de datos para el campo Componente del Programa
            If Not ProgramComponentName.Equals("") Then
                sql.Append(where & " SA.Name like '%" & ProgramComponentName.Trim() & "%'")
                where2 = " AND "
            End If
            sql.Append(")as ProgramName,   ")
            sql.Append(" (Select Top 1  StrategicLine.Name AS StrategicLineName  ")
            sql.Append(" FROM ProgramComponentByIdea")
            sql.Append(" LEFT JOIN ProgramComponent SA ON ProgramComponentByIdea.IdProgramComponent = SA.Id  ")
            sql.Append(" LEFT JOIN Program MA ON SA.IdProgram = MA.Id LEFT JOIN StrategicLine ON MA.IdStrategicLine = StrategicLine.Id   ")
            sql.Append(where3 & " l.Id=ProgramComponentByIdea.IdIdea ")
            where3 = " AND "
            ' verificar si hay entrada de datos para el campo Linea Estrategica
            If Not StrategicLineName.Equals("") Then
                sql.Append(where3 & " StrategicLine.Name like '%" & StrategicLineName.Trim() & "%'")
                where3 = " AND "
            End If
            ' verificar si hay entrada de datos para el campo Programa
            If Not ProgramName.Equals("") Then
                sql.Append(where3 & " MA.Name like '%" & ProgramName.Trim() & "%'")
                where3 = " AND "
            End If

            ' verificar si hay entrada de datos para el campo Componente del Programa
            If Not ProgramComponentName.Equals("") Then
                sql.Append(where3 & " SA.Name like '%" & ProgramComponentName.Trim() & "%'")
                where3 = " AND "
            End If
            sql.Append(")as StrategicLineName   ")
            sql.Append(" from Idea as l)as t on Idea.Id = t.Id   ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Idea.IdUser = ApplicationUser.Id  ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Idea.id = '" & id.Trim() & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Idea.id like '%" & idlike.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Idea.code like '%" & code.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Idea.name like '%" & name.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not objective.Equals("") Then

                sql.Append(where & " Idea.objective like '%" & objective.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Idea.startdate, 103) like '%" & startdate.Trim() & "%'")
                where = " AND "
            End If

            ' verificar si hay entrada de datos para el campo
            If Not duration.Equals("") Then

                sql.Append(where & " Idea.duration like '%" & duration.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not areadescription.Equals("") Then

                sql.Append(where & " Idea.areadescription like '%" & areadescription.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not population.Equals("") Then

                sql.Append(where & " Idea.population like '%" & population.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not cost.Equals("") Then

                sql.Append(where & " Idea.cost like '%" & cost.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not strategydescription.Equals("") Then

                sql.Append(where & " Idea.strategydescription like '%" & strategydescription.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not results.Equals("") Then

                sql.Append(where & " Idea.results like '%" & results.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not source.Equals("") Then

                sql.Append(where & " Idea.source like '%" & source.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idsummoning.Equals("") Then

                sql.Append(where & " Idea.idsummoning like '%" & idsummoning.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startprocess.Equals("") Then

                sql.Append(where & " Idea.startprocess like '%" & startprocess.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startprocesstext.Equals("") Then

                sql.Append(where & " Idea.startprocess IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'si' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'no' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & startprocesstext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Idea.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Idea.iduser = '" & iduser.Trim() & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Idea.enabled like '%" & enabled.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Idea.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo Linea Estrategica
            If Not StrategicLineName.Equals("") Then
                sql.Append(where & "  StrategicLineName like '%" & StrategicLineName.Trim() & "%'")
                where = " AND "
            End If

            ' verificar si hay entrada de datos para el campo programa
            If Not ProgramName.Equals("") Then
                sql.Append(where & " ProgramName like '%" & ProgramName.Trim() & "%'")
                where = " AND "
            End If

            ' verificar si hay entrada de datos para el campo Componente del Programa
            If Not ProgramComponentName.Equals("") Then
                sql.Append(where & " ProgramComponentName like '%" & ProgramComponentName.Trim() & "%'")
                where = " AND "
            End If


            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "StrategicLineName"
                        sql.Append(" ORDER BY  StrategicLineName ")
                    Case "ProgramName"
                        sql.Append(" ORDER BY ProgramName ")
                    Case "ProgramComponentName"
                        sql.Append(" ORDER BY ProgramComponentName ")
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case Else
                        sql.Append(" ORDER BY Idea." & order)
                End Select

            End If

            ' TODO:25 ideadalc getlist cambio de orden en el buscar 
            ' Autor: German Rodriguez MGgroup
            ' decripciòn: cambio de orden en el buscar de la idea

            sql.Append(" ORDER BY Idea.createDate desc")

            ' TODO:25 ideadalc getlist cambio de orden en el buscar 
            ' Autor: German Rodriguez MGgroup
            ' cierre de cambio


            ' verificar si hay entrada de datos para el campo
            If Not ResultsKnowledgeManagement.Equals("") Then

                sql.Append(where & " Idea.ResultsKnowledgeManagement like '%" & ResultsKnowledgeManagement.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ResultsInstalledCapacity.Equals("") Then

                sql.Append(where & " Idea.ResultsInstalledCapacity like '%" & ResultsInstalledCapacity.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idtypecontract.Equals("") Then

                sql.Append(where & " Idea.idtypecontract like '%" & idtypecontract.Trim() & "%'")
                where = " AND "

            End If


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objIdea = New IdeaEntity

                ' cargar el valor del campo
                objIdea.id = row("id")
                objIdea.code = row("code")
                objIdea.name = row("name")
                objIdea.objective = row("objective")
                objIdea.startdate = row("startdate")
                objIdea.duration = row("duration")
                objIdea.areadescription = row("areadescription")
                objIdea.population = row("population")
                objIdea.cost = row("cost")
                objIdea.fsccontribution = IIf(Not IsDBNull(row("fsccontribution")), row("fsccontribution"), 0)
                objIdea.counterpartvalue = IIf(Not IsDBNull(row("counterpartvalue")), row("counterpartvalue"), 0)
                objIdea.strategydescription = row("strategydescription")
                objIdea.results = row("results")
                objIdea.source = row("source")
                objIdea.justification = IIf(Not IsDBNull(row("justification")), row("justification"), "")
                objIdea.idsummoning = row("idsummoning")
                objIdea.startprocess = row("startprocess")
                objIdea.createdate = row("createdate")
                objIdea.iduser = row("iduser")
                objIdea.enabled = row("enabled")
                objIdea.StrategicLineNAME = IIf(Not IsDBNull(row("StrategicLineName")), row("StrategicLineName"), "")
                objIdea.ProgramComponentNAME = IIf(Not IsDBNull(row("ProgramComponentName")), row("ProgramComponentName"), "")
                objIdea.ProgramNAME = IIf(Not IsDBNull(row("ProgramName")), row("ProgramName"), "")
                objIdea.USERNAME = IIf(Not IsDBNull(row("ApplicationUserName")), row("ApplicationUserName"), "")

                ' TODO: 26  ideadalc getlist  se crean nuevos campos
                ' Autor: German Rodriguez MGgroup
                ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

                objIdea.ResultsInstalledCapacity = IIf(Not IsDBNull(row("ResultsInstalledCapacity")), row("ResultsInstalledCapacity"), "")
                objIdea.ResultsKnowledgeManagement = IIf(Not IsDBNull(row("ResultsKnowledgeManagement")), row("ResultsKnowledgeManagement"), "")
                objIdea.Loadingobservations = IIf(Not IsDBNull(row("Loadingobservations")), row("Loadingobservations"), "")
                objIdea.idtypecontract = IIf(Not IsDBNull(row("idtypecontract")), row("idtypecontract"), "")
                objIdea.Enddate = IIf(Not IsDBNull(row("Enddate")), row("Enddate"), "")

                ' TODO: 26  ideadalc getlist  se crean nuevos campos
                ' Autor: German Rodriguez MGgroup
                ' cierre de cambio

                ' agregar a la lista
                IdeaList.Add(objIdea)

            Next

            ' retornar el objeto
            getList = IdeaList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Idea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objIdea = Nothing
            IdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    Public Function getListIdeaAprobada(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal id As String = "", _
       Optional ByVal idlike As String = "", _
       Optional ByVal code As String = "", _
       Optional ByVal name As String = "", _
       Optional ByVal objective As String = "", _
       Optional ByVal startdate As String = "", _
       Optional ByVal duration As String = "", _
       Optional ByVal areadescription As String = "", _
       Optional ByVal population As String = "", _
       Optional ByVal cost As String = "", _
       Optional ByVal strategydescription As String = "", _
       Optional ByVal results As String = "", _
       Optional ByVal source As String = "", _
       Optional ByVal idsummoning As String = "", _
       Optional ByVal startprocess As String = "", _
       Optional ByVal startprocesstext As String = "", _
       Optional ByVal createdate As String = "", _
       Optional ByVal iduser As String = "", _
       Optional ByVal username As String = "", _
       Optional ByVal enabled As String = "", _
       Optional ByVal enabledtext As String = "", _
       Optional ByVal StrategicLineName As String = "", _
       Optional ByVal ProgramName As String = "", _
       Optional ByVal ProgramComponentName As String = "", _
       Optional ByVal Loadingobservations As String = "", _
       Optional ByVal ResultsKnowledgeManagement As String = "", _
       Optional ByVal ResultsInstalledCapacity As String = "", _
       Optional ByVal idtypecontract As String = "", _
       Optional ByVal order As String = "") As List(Of IdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIdea As IdeaEntity
        Dim IdeaList As New List(Of IdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try

            'construir la sentencia
            sql.Append("select distinct Idea.Code,Idea.Name,par.codeapprovedidea,  Idea.Code+'_'+Idea.Name as 'name_code'")
            sql.Append("FROM Idea INNER JOIN ProjectApprovalRecord par ON idea.Id = par.Ididea")


            If Not order.Equals(String.Empty) Then

                sql.Append(" ORDER BY Idea.Name")

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objIdea = New IdeaEntity

                ' cargar el valor del campo
                objIdea.code = row("code")
                objIdea.name = row("name_code")

                ' agregar a la lista

                IdeaList.Add(objIdea)

            Next

            ' retornar el objeto
            getListIdeaAprobada = IdeaList

            ' finalizar la transaccion
            CtxSetComplete()


        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Idea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objIdea = Nothing
            IdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function



    ''' <summary>
    ''' Modificar un objeto de tipo Idea
    ''' </summary>
    ''' <param name="Idea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Idea As IdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Idea SET")
            sql.AppendLine(" name = '" & Idea.name & "',")
            sql.AppendLine(" objective = '" & Idea.objective & "',")
            sql.AppendLine(" startdate = '" & Idea.startdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" duration = '" & Idea.duration & "',")
            sql.AppendLine(" areadescription = '" & Idea.areadescription & "',")
            sql.AppendLine(" population = '" & Idea.population & "',")
            sql.AppendLine(" cost = " & Idea.cost.ToString().Replace(",", ".") & ",")
            sql.AppendLine(" fsccontribution = " & Idea.fsccontribution.ToString().Replace(",", ".") & ",")
            sql.AppendLine(" counterpartvalue = " & Idea.counterpartvalue.ToString().Replace(",", ".") & ",")
            sql.AppendLine(" strategydescription = '" & Idea.strategydescription & "',")
            sql.AppendLine(" results = '" & Idea.results & "',")
            sql.AppendLine(" source = '" & Idea.source & "',")
            sql.AppendLine(" justification = '" & Idea.justification & "',")
            sql.AppendLine(" idsummoning = '" & Idea.idsummoning & "',")
            sql.AppendLine(" startprocess = '" & Idea.startprocess & "',")
            sql.AppendLine(" enabled = '" & Idea.enabled & "',")
            sql.AppendLine(" idProcessInstance = '" & Idea.IdProcessInstance & "',")
            sql.AppendLine(" idActivityInstance = '" & Idea.IdActivityInstance & "',")

            ' TODO: 27 ideadalc update se crean nuevos campos
            ' Autor: German Rodriguez MGgroup
            ' decripciòn: se crean nuevos campos solicitador por el cliente FSC fase II

            sql.AppendLine(" ResultsInstalledCapacity = '" & Idea.ResultsInstalledCapacity & "',")   'campo nuevo fase II german rodriguez 
            sql.AppendLine(" ResultsKnowledgeManagement = '" & Idea.ResultsKnowledgeManagement & "',") 'campo nuevo fase II german rodriguez
            sql.AppendLine(" Loadingobservations = '" & Idea.Loadingobservations & "',") 'campo nuevo fase II german rodriguez
            sql.AppendLine(" idtypecontract = '" & Idea.idtypecontract & "'") 'campo nuevo fase II german rodriguez

            ' TODO: 27 ideadalc update se crean nuevos campos
            ' Autor: German Rodriguez MGgroup
            'cierre de cambio

            sql.AppendLine(" WHERE id = " & Idea.id)


            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Idea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Idea de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Idea ")
            SQL.AppendLine(" where id = '" & idIdea & "' ")

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As SqlClient.SqlException

            'Se verifica si el error es por integridad referencial
            If ex.Number = 547 Then

                ' cancelar la transaccion
                CtxSetAbort()

                'publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                ' subir el error de nivel
                Throw New Exception("Ha ocurrido un error al intentar eliminar este registro debido a una relación existente con un registro de Proyecto.", ex)

            Else

                ' cancelar la transaccion
                CtxSetAbort()

                ' publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                ' subir el error de nivel
                Throw New Exception("Error al elimiar la Idea. " & ex.Message)

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al elimiar la Idea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
