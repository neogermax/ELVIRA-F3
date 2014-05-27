Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Text.RegularExpressions.Regex

Public Class ProjectDALC

    ' contantes
    Const MODULENAME As String = "ProjectDALC"

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
            sql.AppendLine("Select Id from " & dbBPMName & ".dbo.ProcessInstance ProcessInstance ")
            sql.AppendLine(" where EntryData='" & EntryData & "' and IDEntryData='" & idEntryData & "'and Status='" & Status & "' and ")
            'sql.AppendLine(" idprocess= (select case idPhase when 1 then 8 when 2 then 9 end from project where idkey ='" & idEntryData & "' and islastversion='1' ) ")
            sql.AppendLine(" (idprocess= 9) ")
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
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Project WHERE isLastVersion = 1 AND Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM Project WHERE isLastVersion = 1 AND Code = '" & code & "' AND id <> '" & id & "'")

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
            Throw New Exception("Error al verificar el código de ENTERPRISE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo Project
    ''' </summary>
    ''' <param name="Project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Project As ProjectEntity) As Long
        Dim numid As Integer
        ' definiendo los objtos
        Dim sqlMaxIdProject As New StringBuilder
        Dim sqlCode As New StringBuilder
        Dim sql As New StringBuilder

        Dim dtData As DataTable
        Dim dtDataCode As DataTable
        Dim dtDataMax As DataTable
        Dim fecha As New DateTime
        Dim code As String

        Try
            'sqlMaxIdProject.AppendLine("select MAX(Project.Id) from Project")
            'dtDataMax = GattacaApplication.RunSQLRDT(objApplicationCredentials, sqlMaxIdProject.ToString)

            ''obtener la idea y su nombre
            'sqlCode.AppendLine("SELECT Idea.Id, Idea.name FROM Idea JOIN ProjectApprovalRecord par ON (Idea.Id=par.IdIdea) WHERE Idea.Id = " & Project.ididea)
            ''sqlCode.AppendLine(" " & dtDataMax.Rows(0)(0) & ")  GROUP BY Idea.Id, Idea.name")
            'dtDataCode = GattacaApplication.RunSQLRDT(objApplicationCredentials, sqlCode.ToString)
            'If dtDataCode.Rows.Count > 0 Then

            '    ' cargar los datos
            '    code = dtDataCode.Rows(0)("Id")
            '    code = code & "-" & dtDataCode.Rows(0)("name")

            'End If
            'numid = Convert.ToInt32(dtDataMax.Rows(0)(0)) + 1
            'code = code & "-" & numid
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Project(" & _
             "ididea," & _
             "code," & _
             "name," & _
             "objective," & _
             "antecedent," & _
             "justification," & _
             "begindate," & _
             "duration," & _
             "zonedescription," & _
             "population," & _
             "strategicdescription," & _
             "ResultsKnowledgeManagement," & _
             "ResultsInstalledCapacity," & _
             "results," & _
             "source," & _
             "purpose," & _
             "totalcost," & _
             "fsccontribution," & _
             "counterpartvalue," & _
             "effectivebudget," & _
             "attachment," & _
             "idphase," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "idKey," & _
             "isLastVersion," & _
             "IdProcessInstance, " & _
             "Idtypecontract, " & _
             "Typeapproval, " & _
             "Mother, " & _
             "OtherResults, " & _
             "obligationsoftheparties, " & _
             "BudgetRoute, " & _
             "RisksIdentified, " & _
             "RiskMitigation, " & _
             "ideaappliesIVA, " & _
             "days, " & _
             "Project_derivados, " & _
             "completiondate " & _
             ")")

            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Project.ididea & "',")
            sql.AppendLine("'" & Project.code & "',")
            sql.AppendLine("'" & Project.name & "',")
            sql.AppendLine("'" & Project.objective & "',")
            sql.AppendLine("'" & Project.antecedent & "',")
            sql.AppendLine("'" & Project.justification & "',")

            If (Project.begindate <> fecha) Then

                sql.AppendLine("'" & Project.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")

            Else

                sql.AppendLine(" NULL, ")

            End If

            sql.AppendLine("'" & Project.duration & "',")
            sql.AppendLine("'" & Project.zonedescription & "',")
            sql.AppendLine("'" & Project.population & "',")
            sql.AppendLine("'" & Project.strategicdescription & "',")
            sql.AppendLine("'" & Project.ResultsKnowledgeManagement & "',")
            sql.AppendLine("'" & Project.ResultsInstalledCapacity & "',")
            sql.AppendLine("'" & Project.results & "',")
            sql.AppendLine("'" & Project.source & "',")
            sql.AppendLine("'" & Project.purpose & "',")
            sql.AppendLine("'" & Project.totalcost.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Project.fsccontribution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Project.counterpartvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Project.effectivebudget & "',")
            sql.AppendLine("'" & Project.attachment & "',")
            sql.AppendLine("'" & Project.idphase & "',")
            sql.AppendLine("'" & Project.enabled & "',")
            sql.AppendLine("'" & Project.iduser & "',")
            sql.AppendLine("'" & Project.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & Project.idKey & "',")
            sql.AppendLine("'" & Project.isLastVersion & "',")
            sql.AppendLine("'" & Project.IdProcessInstance & "' , ")
            'sql.AppendLine("'" & Project.IdActivityInstance & "')")
            sql.AppendLine("'" & Project.idtypecontract & "' , ")
            sql.AppendLine("'" & Project.Typeapproval & "' , ")
            sql.AppendLine("'" & Project.mother & "' , ")
            sql.AppendLine("'" & Project.OthersResults & "' , ")
            sql.AppendLine("'" & Project.Obligaciones & "' , ")
            sql.AppendLine("'" & Project.presupuestal & "' , ")
            sql.AppendLine("'" & Project.riesgos & "' , ")
            sql.AppendLine("'" & Project.mitigacion & "' , ")
            sql.AppendLine("'" & Project.iva & "' , ")
            sql.AppendLine("'" & Project.dia & "' , ")
            sql.AppendLine("'" & Project.Project_derivados & "' , ")
            sql.AppendLine("'" & Project.completiondate.ToString("yyyy/MM/dd HH:mm:ss") & "' ) ")
            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            If Project.idKey = 0 Then

                ' limpiar el sql
                sql.Remove(0, sql.Length)

                ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
                sql.AppendLine("Update Project SET")
                sql.AppendLine(" idKey = '" & num & "',")
                sql.AppendLine(" isLastVersion = 1")
                sql.AppendLine("WHERE id = " & num)

                'Ejecutar la Instruccion
                GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            End If

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
            Throw New Exception("Error al insertar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing
            sqlCode = Nothing
            dtDataCode = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Project por el Id
    ''' </summary>
    ''' <param name="idProject"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal idProject As Integer, _
                            Optional ByVal consultLastVersion As Boolean = True) As ProjectEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProject As New ProjectEntity
        Dim objSourceByProjectDALC As New SourceByProjectDALC
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        Dim objForumDALC As New ForumDALC
        Dim objExplanatoryDALC As ExplanatoryDALC

        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Project.*, isnull(Idea.Name,'') AS ideaname, isnull(ApplicationUser.Name,'') AS username, ")
            sql.Append(" 	  (SELECT TOP (1) isnull(Program.Name,'') ")
            sql.Append(" 		FROM ProgramComponentByProject LEFT OUTER JOIN ")
            sql.Append(" 			ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
            sql.Append(" 			Program ON ProgramComponent.IdProgram = Program.Id ")
            sql.Append(" 		WHERE (ProgramComponentByProject.IdProject = Project.idkey and Project.IsLastVersion='1')) AS Programname, ")
            sql.Append(" 	  (SELECT TOP (1) isnull(StrategicLine.Name,'') ")
            sql.Append(" 		FROM ProgramComponentByProject AS ProgramComponentByProject_1 LEFT OUTER JOIN ")
            sql.Append(" 			ProgramComponent AS ProgramComponent_1 ON ProgramComponentByProject_1.IdProgramComponent = ProgramComponent_1.Id LEFT OUTER JOIN ")
            sql.Append(" 			Program AS Program_1 ON ProgramComponent_1.IdProgram = Program_1.Id LEFT OUTER JOIN ")
            sql.Append(" 			StrategicLine ON Program_1.IdStrategicLine = StrategicLine.Id ")
            sql.Append(" 		WHERE (ProgramComponentByProject_1.IdProject = Project.idkey and Project.IsLastVersion='1')) AS StrategicLinename ")
            sql.Append(" FROM Project LEFT OUTER JOIN ")
            sql.Append(" 	Idea ON Project.IdIdea = Idea.Id LEFT OUTER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Project.IdUser = ApplicationUser.ID ")

            'If (consultLastVersion) Then
            '    sql.Append(" WHERE Project.IsLastVersion='1' And Project.idKey = " & idProject)
            'Else
            sql.Append(" WHERE Project.id = " & idProject)
            'End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)




            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProject.id = data.Rows(0)("id")
                objProject.ididea = data.Rows(0)("IdIdea")
                objProject.code = data.Rows(0)("code")
                objProject.name = data.Rows(0)("name")
                objProject.objective = data.Rows(0)("objective")
                objProject.antecedent = data.Rows(0)("antecedent")
                objProject.justification = data.Rows(0)("justification")
                objProject.begindate = IIf(Not IsDBNull(data.Rows(0)("begindate")), data.Rows(0)("begindate"), Nothing)
                objProject.duration = data.Rows(0)("duration")
                objProject.zonedescription = data.Rows(0)("zonedescription")
                objProject.population = data.Rows(0)("population")
                objProject.ResultsKnowledgeManagement = IIf(Not IsDBNull(data.Rows(0)("ResultsKnowledgeManagement")), data.Rows(0)("ResultsKnowledgeManagement"), "")
                objProject.ResultsInstalledCapacity = IIf(Not IsDBNull(data.Rows(0)("ResultsInstalledCapacity")), data.Rows(0)("ResultsInstalledCapacity"), "")
                objProject.strategicdescription = data.Rows(0)("strategicdescription")
                objProject.results = data.Rows(0)("results")
                objProject.source = data.Rows(0)("source")
                objProject.purpose = data.Rows(0)("purpose")
                objProject.totalcost = data.Rows(0)("totalcost")
                objProject.fsccontribution = data.Rows(0)("fsccontribution")
                objProject.counterpartvalue = data.Rows(0)("counterpartvalue")
                objProject.effectivebudget = data.Rows(0)("effectivebudget")
                objProject.attachment = IIf(Not IsDBNull(data.Rows(0)("attachment")), data.Rows(0)("attachment"), "")
                objProject.idphase = data.Rows(0)("idphase")
                objProject.enabled = data.Rows(0)("enabled")
                objProject.iduser = data.Rows(0)("iduser")
                objProject.createdate = data.Rows(0)("createdate")
                objProject.USERNAME = data.Rows(0)("username")
                objProject.IDEANAME = data.Rows(0)("ideaname")
                objProject.StrategicLineNAME = IIf(Not IsDBNull(data.Rows(0)("StrategicLinename")), data.Rows(0)("StrategicLinename"), "")
                objProject.ProgramNAME = IIf(Not IsDBNull(data.Rows(0)("Programname")), data.Rows(0)("Programname"), "")
                objProject.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objProject.sourceByProjectList = objSourceByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                objProject.projectlocationlist = objProjectLocationDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                'objProject.thirdbyprojectlist = objThirdByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                objProject.operatorbyprojectlist = objOperatorByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                objProject.ProgramComponentbyprojectlist = objProgramComponentByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                objProject.forumlist = objForumDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                objProject.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))
                objProject.IdProcessInstance = data.Rows(0)("IdProcessInstance")
                objProject.Typeapproval = data.Rows(0)("Typeapproval")

                objProject.editablemoney = data.Rows(0)("editablemoney")
                objProject.editabletime = data.Rows(0)("editabletime")
                objProject.editableresults = data.Rows(0)("editableresults")
                'objProject.IdActivityInstance = data.Rows(0)("IdActivityInstance")

            End If

            ' retornar el objeto
            Return objProject

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Project. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProject = Nothing
            objProjectLocationDALC = Nothing
            objThirdByProjectDALC = Nothing
            objOperatorByProjectDALC = Nothing
            objProgramComponentByProjectDALC = Nothing
            objForumDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="ididea"></param>
    ''' <param name="ideaname"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="objective"></param>
    ''' <param name="antecedent"></param>
    ''' <param name="justification"></param>
    ''' <param name="begindate"></param>
    ''' <param name="duration"></param>
    ''' <param name="zonedescription"></param>
    ''' <param name="population"></param>
    ''' <param name="strategicdescription"></param>
    ''' <param name="results"></param>
    ''' <param name="source"></param>
    ''' <param name="purpose"></param>
    ''' <param name="totalcost"></param>
    ''' <param name="fsccontribution"></param>
    ''' <param name="counterpartvalue"></param>
    ''' <param name="effectivebudget"></param>
    ''' <param name="attachment"></param>
    ''' <param name="idphase"></param>
    ''' <param name="phasename"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="idStrategicLine"></param>
    ''' <param name="StrategicLinename"></param>
    ''' <param name="idProgram"></param>
    ''' <param name="Programname"></param>
    ''' <param name="idProgramComponent"></param>
    ''' <param name="ProgramComponentname"></param>
    ''' <param name="currentactivityname"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ProjectEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal ideaname As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal objective As String = "", _
        Optional ByVal antecedent As String = "", _
        Optional ByVal justification As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal zonedescription As String = "", _
        Optional ByVal population As String = "", _
        Optional ByVal strategicdescription As String = "", _
        Optional ByVal results As String = "", _
        Optional ByVal source As String = "", _
        Optional ByVal purpose As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal fsccontribution As String = "", _
        Optional ByVal counterpartvalue As String = "", _
        Optional ByVal effectivebudget As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal idphase As String = "", _
        Optional ByVal phasename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal StrategicLinename As String = "", _
        Optional ByVal idProgram As String = "", _
        Optional ByVal Programname As String = "", _
        Optional ByVal idProgramComponent As String = "", _
        Optional ByVal ProgramComponentname As String = "", _
        Optional ByVal currentactivityname As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "", _
        Optional ByVal fromindicadores As Integer = 0) As List(Of ProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        Dim objForumDALC As New ForumDALC
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try

            'Averiguar si la consulta viene de indicadores
            If fromindicadores = 0 Then
                ' construir la sentencia
                sql.Append(" SELECT Project.*, isnull(Idea.Name,'') AS ideaname, isnull(ApplicationUser.Name,'') AS username, ")
                sql.Append(" 	  (SELECT TOP (1) isnull(Program.Name,'') ")
                sql.Append(" 		FROM ProgramComponentByProject INNER JOIN ")
                sql.Append(" 			ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
                sql.Append(" 			Program ON ProgramComponent.IdProgram = Program.Id ")
                sql.Append(" 		WHERE (ProgramComponentByProject.IdProject = Project.idkey)) AS Programname, ")
                sql.Append(" 	  (SELECT TOP (1) isnull(StrategicLine.Name,'') ")
                sql.Append(" 		FROM ProgramComponentByProject AS ProgramComponentByProject_1 LEFT OUTER JOIN ")
                sql.Append(" 			ProgramComponent AS ProgramComponent_1 ON ProgramComponentByProject_1.IdProgramComponent = ProgramComponent_1.Id LEFT OUTER JOIN ")
                sql.Append(" 			Program AS Program_1 ON ProgramComponent_1.IdProgram = Program_1.Id LEFT OUTER JOIN ")
                sql.Append(" 			StrategicLine ON Program_1.IdStrategicLine = StrategicLine.Id ")
                sql.Append(" 		WHERE (ProgramComponentByProject_1.IdProject = Project.idkey)) AS StrategicLinename ")
                sql.Append(" FROM Project INNER JOIN Idea ON Project.IdIdea = Idea.Id LEFT OUTER JOIN ")
                sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Project.IdUser = ApplicationUser.ID  ")



                ' verificar si hay entrada de datos para el campo
                If Not id.Equals("") Then

                    sql.Append(where & " Project.id = '" & id & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idlike.Equals("") Then

                    sql.Append(where & " Project.id like '%" & idlike & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not ididea.Equals("") Then

                    sql.Append(where & " Project.ididea = '" & ididea & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not ideaname.Equals("") Then

                    sql.Append(where & " Idea.Name like '%" & ideaname & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not code.Equals("") Then

                    sql.Append(where & " Project.code like '%" & code & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not name.Equals("") Then

                    sql.Append(where & " Project.name like '%" & name & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not objective.Equals("") Then

                    sql.Append(where & " Project.objective like '%" & objective & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not antecedent.Equals("") Then

                    sql.Append(where & " Project.antecedent like '%" & antecedent & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not justification.Equals("") Then

                    sql.Append(where & " Project.justification like '%" & justification & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not begindate.Equals("") Then

                    sql.Append(where & " CONVERT(NVARCHAR, Project.begindate, 103) like '%" & begindate & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not duration.Equals("") Then

                    sql.Append(where & " Project.duration like '%" & duration & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not zonedescription.Equals("") Then

                    sql.Append(where & " Project.zonedescription like '%" & zonedescription & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not population.Equals("") Then

                    sql.Append(where & " Project.population like '%" & population & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not strategicdescription.Equals("") Then

                    sql.Append(where & " Project.strategicdescription like '%" & strategicdescription & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not results.Equals("") Then

                    sql.Append(where & " Project.results like '%" & results & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not source.Equals("") Then

                    sql.Append(where & " Project.source like '%" & source & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not purpose.Equals("") Then

                    sql.Append(where & " Project.purpose like '%" & purpose & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not totalcost.Equals("") Then

                    sql.Append(where & " Project.totalcost like '%" & totalcost & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not fsccontribution.Equals("") Then

                    sql.Append(where & " Project.fsccontribution like '%" & fsccontribution & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not counterpartvalue.Equals("") Then

                    sql.Append(where & " Project.counterpartvalue like '%" & counterpartvalue & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not effectivebudget.Equals("") Then

                    sql.Append(where & " Project.effectivebudget like '%" & effectivebudget & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not attachment.Equals("") Then

                    sql.Append(where & " Project.attachment like '%" & attachment & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idphase.Equals("") Then

                    sql.Append(where & " Project.idphase = '" & idphase & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not phasename.Equals("") Then

                    sql.Append(where & " Project.idphase IN ")
                    sql.Append(" (SELECT Value FROM (SELECT 'Formulación' AS Impact, 1 AS Value ")
                    sql.Append(" UNION SELECT 'Formulacion' AS Impact, 1 AS Value ")
                    sql.Append(" UNION SELECT 'Planeación' AS Impact, 2 AS Value ")
                    sql.Append(" UNION SELECT 'Planeacion' AS Impact, 2 AS Value ")
                    sql.Append(" UNION SELECT 'Ejecución' AS Impact, 3 AS Value ")
                    sql.Append(" UNION SELECT 'Ejecucion' AS Impact, 3 AS Value ")
                    sql.Append(" UNION SELECT 'Evaluación' AS Impact, 4 AS Value ")
                    sql.Append(" UNION SELECT 'Evaluacion' AS Impact, 4 AS Value) Temp ")
                    sql.Append(" WHERE Impact LIKE '%" & phasename & "%') ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not enabled.Equals("") Then

                    sql.Append(where & " Project.enabled = '" & enabled & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not enabledtext.Equals("") Then

                    sql.Append(where & " Project.enabled IN ")
                    sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                    sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                    sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not iduser.Equals("") Then

                    sql.Append(where & " Project.IdUser = '" & iduser & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not username.Equals("") Then

                    sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idStrategicLine.Equals("") Then

                    sql.Append(where & " Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT ProgramComponentByProject.IdProject ")
                    sql.Append(" FROM ProgramComponentByProject LEFT OUTER JOIN ")
                    sql.Append("	ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
                    sql.Append("	Program ON ProgramComponent.IdProgram = Program.Id LEFT OUTER JOIN ")
                    sql.Append("	StrategicLine ON Program.IdStrategicLine = StrategicLine.Id ")
                    sql.Append(" WHERE (StrategicLine.Id = '" & idStrategicLine & "')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not StrategicLinename.Equals("") Then

                    sql.Append(where & "  Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT ProgramComponentByProject.IdProject ")
                    sql.Append(" FROM ProgramComponentByProject LEFT OUTER JOIN ")
                    sql.Append("	ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
                    sql.Append("	Program ON ProgramComponent.IdProgram = Program.Id LEFT OUTER JOIN ")
                    sql.Append("	StrategicLine ON Program.IdStrategicLine = StrategicLine.Id ")
                    sql.Append(" WHERE (StrategicLine.Name like '%" & StrategicLinename & "%')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idProgram.Equals("") Then

                    sql.Append(where & " Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT ProgramComponentByProject.IdProject ")
                    sql.Append("  FROM ProgramComponentByProject LEFT OUTER JOIN ")
                    sql.Append(" 		ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
                    sql.Append(" 		Program ON ProgramComponent.IdProgram = Program.Id ")
                    sql.Append("  WHERE (Program.Id = '" & idProgram & "')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not Programname.Equals("") Then

                    sql.Append(where & " Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT ProgramComponentByProject.IdProject ")
                    sql.Append("  FROM ProgramComponentByProject LEFT OUTER JOIN ")
                    sql.Append(" 		ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id LEFT OUTER JOIN ")
                    sql.Append(" 		Program ON ProgramComponent.IdProgram = Program.Id ")
                    sql.Append("  WHERE (Program.Name like '%" & Programname & "%')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idProgramComponent.Equals("") Then

                    sql.Append(where & " Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT IdProject ")
                    sql.Append("  FROM ProgramComponentByProject ")
                    sql.Append("  WHERE (IdProject = Project.idkey and Project.IsLastVersion='1') AND (IdProgramComponent = '" & idProgramComponent & "')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not ProgramComponentname.Equals("") Then

                    sql.Append(where & " Project.IsLastVersion='1' and Project.idkey IN ")
                    sql.Append(" (SELECT ProgramComponentByProject.IdProject ")
                    sql.Append(" FROM ProgramComponentByProject LEFT OUTER JOIN ")
                    sql.Append(" ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id ")
                    sql.Append(" WHERE (ProgramComponent.Name like  '%" & ProgramComponentname & "%')) ")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not createdate.Equals("") Then

                    sql.Append(where & " CONVERT(NVARCHAR, Project.createdate, 103) like '%" & createdate & "%'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                If Not idKey.Equals("") Then

                    sql.Append(where & "  Project.idKey = '" & idKey & "'")
                    where = " AND "

                End If

                ' verificar si hay entrada de datos para el campo
                'If Not isLastVersion.Equals("") Then

                '    sql.Append(where & "   Project.isLastVersion = '" & isLastVersion & "'")
                '    where = " AND "

                'End If
                Dim swhch_orderby As Integer = 0

                If Not order.Equals(String.Empty) Then

                    ' ordernar
                    Select Case order
                        Case "username"
                            sql.Append(" ORDER BY username ")
                        Case "ideaname"
                            sql.Append(" ORDER BY ideaname ")
                        Case Else
                            'sql.Append(" ORDER BY Project." & order)
                            sql.Append(" ORDER BY Project.CreateDate DESC")
                            swhch_orderby = 1
                    End Select
                End If

                If swhch_orderby = 0 Then
                    'ordenar los poryecto por ultimos generados
                    'autor:German rodriguez 1/11/2013 
                    sql.Append(" ORDER BY Project.CreateDate DESC")
                End If

            Else

                'Sentencia para lista de Indicadores
                'autor: pedro cruz 31/10/2013
                sql.Append("SELECT [dbo].[Project].[IdIdea], Project.idKey, [dbo].[Project].[Id] , [dbo].[Project].[Name], Project.isLastVersion, left((cast([dbo].[Project].[IdIdea] as nvarchar) + '_' + cast([dbo].[Project].[Id] as nvarchar) + ' - ' + [dbo].[Project].[Name]),200) as code FROM [dbo].[Project]")
                sql.Append("INNER JOIN [dbo].[Idea] ON [dbo].[Project].[IdIdea] = [dbo].[Idea].[Id]")
                sql.Append("where Project.isLastVersion=1 and Project.Enabled=1")
                sql.Append("ORDER BY [dbo].[Project].[Id] DESC")
            End If


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity

                ' cargar el valor del campo

                If fromindicadores = 0 Then
                    objProject.id = row("id")
                    objProject.code = row("code")
                    objProject.name = row("name")
                    objProject.objective = row("objective")
                    objProject.antecedent = row("antecedent")
                    objProject.justification = row("justification")
                    objProject.begindate = IIf(Not IsDBNull(row("begindate")), row("begindate"), Nothing)
                    objProject.duration = row("duration")
                    objProject.zonedescription = row("zonedescription")
                    objProject.population = row("population")
                    objProject.strategicdescription = row("strategicdescription")
                    objProject.results = row("results")
                    objProject.source = row("source")
                    objProject.purpose = row("purpose")
                    objProject.totalcost = row("totalcost")
                    objProject.fsccontribution = row("fsccontribution")
                    objProject.counterpartvalue = row("counterpartvalue")
                    objProject.effectivebudget = row("effectivebudget")
                    objProject.attachment = IIf(Not IsDBNull(row("attachment")), row("attachment"), "")
                    objProject.idphase = row("idphase")
                    objProject.enabled = row("enabled")
                    objProject.iduser = row("iduser")
                    objProject.createdate = row("createdate")
                    objProject.USERNAME = row("username")
                    objProject.IDEANAME = row("ideaname")
                    objProject.StrategicLineNAME = IIf(Not IsDBNull(row("StrategicLinename")), row("StrategicLinename"), "")
                    objProject.ProgramNAME = IIf(Not IsDBNull(row("Programname")), row("Programname"), "")
                    objProject.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                    objProject.projectlocationlist = objProjectLocationDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                    'objProject.thirdbyprojectlist = objThirdByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                    objProject.operatorbyprojectlist = objOperatorByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                    objProject.ProgramComponentbyprojectlist = objProgramComponentByProjectDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                    objProject.forumlist = objForumDALC.getList(objApplicationCredentials, idproject:=objProject.idKey)
                    objProject.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))
                    objProject.IdProcessInstance = row("IdProcessInstance")
                    'objProject.IdActivityInstance = row("IdActivityInstance")
                    objProject.completiondate = row("completiondate")
                Else
                    'cuando la consulta viene de Indicadores
                    objProject.id = row("id")
                    objProject.code = row("code")
                    objProject.name = row("name")
                    objProject.idKey = row("idkey")
                End If

                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getList = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project. - " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objProject = Nothing
            ProjectList = Nothing
            data = Nothing
            where = Nothing
            objProjectLocationDALC = Nothing
            objThirdByProjectDALC = Nothing
            objOperatorByProjectDALC = Nothing
            objProgramComponentByProjectDALC = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Carga la lista de proyectos
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getListToDropDownList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim objProjectLocationDALC As New ProjectLocationDALC
        Dim objThirdByProjectDALC As New ThirdByProjectDALC
        Dim objOperatorByProjectDALC As New OperatorByProjectDALC
        Dim objProgramComponentByProjectDALC As New ProgramComponentByProjectDALC
        Dim objForumDALC As New ForumDALC
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Project.idkey, Project.Name ")
            sql.Append(" FROM Project INNER JOIN Idea ON Project.IdIdea = Idea.Id LEFT OUTER JOIN ")
            sql.Append(dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Project.IdUser = ApplicationUser.ID  ")

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Project.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   Project.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case "ideaname"
                        sql.Append(" ORDER BY ideaname ")
                    Case Else
                        sql.Append(" ORDER BY Project." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity

                ' cargar el valor del campo
                objProject.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                objProject.name = row("name")

                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getListToDropDownList = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project. - " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objProject = Nothing
            ProjectList = Nothing
            data = Nothing
            where = Nothing
            objProjectLocationDALC = Nothing
            objThirdByProjectDALC = Nothing
            objOperatorByProjectDALC = Nothing
            objProgramComponentByProjectDALC = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Permite consultar todos los proyectos que no esten en una fase deerminada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getListNotInPhase(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idphase As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of ProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")

        Try
            'construir la sentencia
            sql.Append(" SELECT Project.id, idkey, Project.code, Project.name, Project.enabled, Project.idphase, Project.isLastVersion ")
            sql.Append(" FROM Project INNER JOIN ")
            sql.Append(" 	Idea ON Project.IdIdea = Idea.Id LEFT OUTER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Project.IdUser = ApplicationUser.ID  ")


            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Project.enabled = '" & enabled & "'")
                where = " AND "

            End If

            'verificar si hay entrada de datos para el campo
            If Not idphase.Equals("") Then

                sql.Append(where & " Project.idphase <> '" & idphase & "'")
                where = " AND "

            End If

            '  verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   Project.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            'Ordenar
            If Not order.Equals(String.Empty) Then

                sql.Append(" ORDER BY Project." & order)

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity

                ' cargar el valor del campo
                objProject.id = row("id")
                objProject.idKey = row("idkey")
                objProject.code = row("code")
                objProject.name = row("name")
                objProject.enabled = row("enabled")
                objProject.idphase = row("idphase")

                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getListNotInPhase = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objProject = Nothing
            ProjectList = Nothing
            data = Nothing
            where = Nothing
        End Try

    End Function


    ''' <summary>
    ''' Permite consultar todos los proyectos que no esten en una fase deerminada
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getListNotInPhaseapprovalrecord(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idphase As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of IdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder

        ' TODO: 29 instancias nuevas para la idea
        ' 04/06/13 german rodriguez MGgroup

        Dim objidea As IdeaEntity
        Dim idealist As New List(Of IdeaEntity)

        ' TODO: 29 instancias nuevas para la idea
        ' 04/06/13 german rodriguez MGgroup
        ' cierre de cambio

        Dim data As DataTable
        Dim where As String = " WHERE "

        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")

        Try

            'construir la sentencia

            ' TODO: 30 instancias nuevas para la idea
            ' 04/06/13 german rodriguez MGgroup


            sql.Append(" select distinct I.Code,I.Name,I.createDate, I.Code+'_'+I.Name as 'name_code' from Idea i ")
            sql.Append(" left JOIN ProjectApprovalRecord par on par.Ididea = i.Id ")
            sql.Append(" where PAR.Ididea is null and i.Typeapproval = 3 ")
            If Not order.Equals(String.Empty) Then

                sql.Append(" ORDER BY I.createDate  DESC")

            End If

            ' TODO: 30 instancias nuevas para la idea
            ' 04/06/13 german rodriguez MGgroup
            ' cierre de cambio

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objidea = New IdeaEntity

                ' TODO: 31 instancias nuevas para la idea
                ' 04/06/13 german rodriguez MGgroup

                objidea.code = row("code")
                objidea.name = row("name_code")

                ' TODO: 31 instancias nuevas para la idea
                ' 04/06/13 german rodriguez MGgroup
                ' cierre de cambio

                ' agregar a la lista

                idealist.Add(objidea)
                'ProjectList.Add(objProject)

            Next

            ' retornar el objeto

            getListNotInPhaseapprovalrecord = idealist


            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objidea = Nothing
            idealist = Nothing
            'objProject = Nothing
            'ProjectList = Nothing
            data = Nothing
            where = Nothing
        End Try

    End Function


    ''' <summary>
    ''' Permite consultar TOdAS LAS IDEA APROVADAS PARA EL OTRO SI
    ''' TODO: 36 CREACION DE FUNCION PARA OTRO SI
    ''' AUTOR:GERMAN RODRIGUEZ 14/06/2013 MGgroup
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idphase"></param>
    ''' <param name="enabled"></param>
    ''' <param name="order"></param>
    ''' <param name="isLastVersion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Function getListIdeaAprobada(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idphase As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal isLastVersion As String = "") As List(Of IdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder

        Dim objidea As IdeaEntity
        Dim idealist As New List(Of IdeaEntity)

        Dim data As DataTable
        Dim where As String = " WHERE "

        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")

        Try

            'construir la sentencia
            sql.Append("select  Idea.Id, Idea.Code,Idea.Name,par.codeapprovedidea,  Idea.Code+'_'+Idea.Name as 'name_code'")
            sql.Append("FROM Idea INNER JOIN ProjectApprovalRecord par ON idea.Id = par.Ididea ORDER BY par.CreateDate DESC")


            If Not order.Equals(String.Empty) Then

                sql.Append(" ORDER BY par.CreateDate ASC ")

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objidea = New IdeaEntity

                ' cargar el valor del campo
                objidea.id = row("id")
                objidea.code = row("code")
                objidea.name = row("name_code")

                ' agregar a la lista

                idealist.Add(objidea)

            Next

            ' retornar el objeto
            getListIdeaAprobada = idealist

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Project." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            objidea = Nothing
            idealist = Nothing
            data = Nothing
            where = Nothing
        End Try

    End Function

    ' TODO: 36 CREACION DE FUNCION PARA OTRO SI
    ' AUTOR:GERMAN RODRIGUEZ 14/06/2013 MGgroup
    ' cierre de cambio


    ''' <summary>
    ''' Modificar un objeto de tipo Project
    ''' </summary>
    ''' <param name="Project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal Project As ProjectEntity, _
                            Optional ByVal idPhase As Integer = 0) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try

            ' insertar el nuevo registro - Queda como la ultima version activa - 
            add(objApplicationCredentials, Project)

            ' Deshabilitar el registro actual, de tal manera que quede como historial
            sql.AppendLine("Update Project SET")
            sql.AppendLine(" isLastVersion = 0")
            sql.AppendLine("WHERE id = " & Project.id)

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
            Throw New Exception("Error al modificar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Project de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProject As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Project ")
            SQL.AppendLine(" where id = '" & idProject & "' ")
            SQL.AppendLine("    OR idKey = '" & idKey & "' ")

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al eliminar el Project. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar una lista de proyectos sin filtrado, usado para el panel general de proyectos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getListNoFilter(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As List(Of ProjectEntity)

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProject As New ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT TOP (100) PERCENT Project.Id, Project.IdKey, Project.IdIdea, Project.Code, Project.Name, Project.Objective, Project.Antecedent,  ")
            sql.Append("                       Project.Justification, Project.BeginDate, Project.Duration, Project.ZoneDescription, Project.Population,  ")
            sql.Append("                       Project.StrategicDescription, Project.Results, Project.Source, Project.Purpose, Project.TotalCost, Project.FSCContribution,  ")
            sql.Append("                       Project.CounterpartValue, Project.EffectiveBudget, Project.Attachment, Project.IdPhase, Project.Enabled, Project.IdUser,  ")
            sql.Append("                       Project.CreateDate, Project.IdProcessInstance, Project.IdActivityInstance , Idea.Name AS ideaname, ApplicationUser.Name AS username, ")
            sql.Append("                           (SELECT  Top 1 Program.Name ")
            sql.Append("                             FROM          ProgramComponentByProject INNER JOIN ")
            sql.Append("                                                    ProgramComponent ON ProgramComponentByProject.IdProgramComponent = ProgramComponent.Id INNER JOIN ")
            sql.Append("                                                    Program ON ProgramComponent.IdProgram = Program.Id ")
            sql.Append("                             WHERE      (ProgramComponentByProject.IdProject = Project.idkey and Project.IsLastVersion='1')) AS Programname, ")
            sql.Append("                           (SELECT Top 1 StrategicLine.Name ")
            sql.Append("                             FROM          ProgramComponentByProject AS ProgramComponentByProject_1 INNER JOIN ")
            sql.Append("                                                    ProgramComponent AS ProgramComponent_1 ON ProgramComponentByProject_1.IdProgramComponent = ProgramComponent_1.Id INNER JOIN ")
            sql.Append("                                                    Program AS Program_1 ON ProgramComponent_1.IdProgram = Program_1.Id INNER JOIN ")
            sql.Append("                                                    StrategicLine ON Program_1.IdStrategicLine = StrategicLine.Id ")
            sql.Append("                             WHERE      (ProgramComponentByProject_1.IdProject = Project.idkey and Project.IsLastVersion='1')) AS StrategicLinename, ")
            sql.Append("                           (SELECT Top 1  StrategicObjective.Name ")
            sql.Append("                             FROM          ProgramComponentByProject AS ProgramComponentByProject_1 INNER JOIN ")
            sql.Append("                                                    ProgramComponent AS ProgramComponent_1 ON ProgramComponentByProject_1.IdProgramComponent = ProgramComponent_1.Id INNER JOIN ")
            sql.Append("                                                    Program AS Program_1 ON ProgramComponent_1.IdProgram = Program_1.Id INNER JOIN ")
            sql.Append("                                                    StrategicLine AS StrategicLine_1 ON Program_1.IdStrategicLine = StrategicLine_1.Id INNER JOIN ")
            sql.Append("                                                    StrategicObjective ON StrategicLine_1.IdStrategicObjective = StrategicObjective.Id ")
            sql.Append("                             WHERE      (ProgramComponentByProject_1.IdProject = Project.idkey and Project.IsLastVersion='1')) AS strategicobjectivename ")
            sql.Append(" FROM Project INNER JOIN Idea ON Project.IdIdea = Idea.Id INNER JOIN ")
            sql.Append(" 	" & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Project.IdUser = ApplicationUser.ID  ")
            sql.Append(" 	Where Project.IsLastVersion='1'  ")
            sql.Append(" ORDER BY Project.Name ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity

                ' cargar el valor del campo
                objProject.id = row("id")
                objProject.idKey = row("idkey")
                objProject.code = row("code")
                objProject.name = row("name")
                objProject.objective = row("objective")
                objProject.antecedent = row("antecedent")
                objProject.justification = row("justification")
                objProject.begindate = IIf(Not IsDBNull(row("begindate")), row("begindate"), Nothing)
                objProject.duration = row("duration")
                objProject.zonedescription = row("zonedescription")
                objProject.population = row("population")
                objProject.strategicdescription = row("strategicdescription")
                objProject.results = row("results")
                objProject.source = row("source")
                objProject.purpose = row("purpose")
                objProject.totalcost = row("totalcost")
                objProject.fsccontribution = row("fsccontribution")
                objProject.counterpartvalue = row("counterpartvalue")
                objProject.effectivebudget = row("effectivebudget")
                objProject.attachment = IIf(Not IsDBNull(row("attachment")), row("attachment"), "")
                objProject.idphase = row("idphase")
                objProject.enabled = row("enabled")
                objProject.iduser = row("iduser")
                objProject.createdate = row("createdate")
                objProject.USERNAME = row("username")
                objProject.IDEANAME = row("ideaname")
                objProject.StrategicLineNAME = IIf(Not IsDBNull(row("StrategicLinename")), row("StrategicLinename"), "")
                objProject.ProgramNAME = IIf(Not IsDBNull(row("Programname")), row("Programname"), "")
                objProject.STRATEGICOBJECTIVENAME = IIf(Not IsDBNull(row("strategicobjectivename")), row("strategicobjectivename"), "")
                objProject.IdProcessInstance = row("IdProcessInstance")
                objProject.IdActivityInstance = row("IdActivityInstance")

                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getListNoFilter = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getListNoFilter")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de  Project. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProject = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar la información de los proyectos
    ''' segun un linea Estrategica determinado
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idStrategicLine">identificador de la linea Estrategica</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">orden de los campos</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getListByStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idStrategicLine As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "") As List(Of ProjectEntity)

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable
        Dim where As String = " AND "

        Try
            ' construir la sentencia
            sql.Append("SELECT Project.idKey, Project.Id, Project.Name, Project.Code")
            sql.Append(" FROM Project")
            sql.Append(" LEFT OUTER JOIN ProgramComponentByProject ON ProgramComponentByProject.IdProject = Project.idkey")
            sql.Append(" LEFT OUTER JOIN ProgramComponent ON ProgramComponent.Id = ProgramComponentByProject.IdProgramComponent")
            sql.Append(" LEFT OUTER JOIN Program ON Program.Id = ProgramComponent.IdProgram")
            sql.Append(" LEFT OUTER JOIN StrategicLine ON StrategicLine.Id = Program.IdStrategicLine")
            sql.Append(" WHERE Project.IsLastVersion='1'")

            ' verificar si hay entrada de datos para el campo
            If Not idStrategicLine.Equals("") Then

                sql.Append(where & " StrategicLine.Id = '" & idStrategicLine & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Project.enabled = '" & enabled & "'")
                where = " AND "

            End If

            'Se anexa la informacion del agrupamiento
            sql.Append(" GROUP BY Project.Id, Project.Name, Project.Code, Project.idKey")

            If Not order.Equals(String.Empty) Then

                ' ordernar
                sql.Append(" ORDER BY Project." & order)

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProject = New ProjectEntity

                ' cargar el valor del campo
                objProject.id = row("id")
                objProject.name = row("name")
                objProject.code = row("code")
                objProject.idKey = row("idkey")

                ' agregar a la lista
                ProjectList.Add(objProject)

            Next

            ' retornar el objeto
            getListByStrategicLine = ProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Proyectos. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProject = Nothing
            ProjectList = Nothing
            data = Nothing

        End Try
    End Function

    ''' <summary>
    ''' Cambia la fase del proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangePhases(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                            ByVal idProject As Integer, _
                            ByVal idPhase As Integer) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim listObjective As List(Of ObjectiveEntity)
        Dim listComponent As List(Of ComponentEntity)
        Dim listActivity As List(Of ActivityEntity)
        Dim listSubactivity As List(Of SubActivityEntity)
        Dim listRisk As New List(Of RiskEntity)
        Dim listMitigation As New List(Of MitigationEntity)
        Dim ObjectiveDalc As New ObjectiveDALC
        Dim ComponentDalc As New ComponentDALC
        Dim ActivityDalc As New ActivityDALC
        Dim SubactivityDalc As New SubActivityDALC
        Dim RiskDalc As New RiskDALC
        Dim MitigationDalc As New MitigationDALC
        Try
            ' consultamos la lista de objetivos para un proyecto
            listObjective = ObjectiveDalc.getList(objApplicationCredentials, _
                                                 idproject:=idProject, isLastVersion:="1")


            ' cambiamos la fase del objetivo y se genera un nueva versión
            For Each mylistObjective As ObjectiveEntity In listObjective
                mylistObjective.idphase = idPhase
                ObjectiveDalc.update(objApplicationCredentials, mylistObjective)
            Next

            ' consultamos la lista de componentes para un proyecto
            listComponent = ComponentDalc.getList(objApplicationCredentials, idproject:=idProject, isLastVersion:="1")

            ' cambiamos la fase del componente y se genera un nueva versión
            For Each mylistComponent As ComponentEntity In listComponent
                mylistComponent.idphase = idPhase
                ComponentDalc.update(objApplicationCredentials, mylistComponent)
            Next

            ' consultamos la lista de actividades para un proyecto
            listActivity = ActivityDalc.getList(objApplicationCredentials, idproject:=idProject, isLastVersion:="1")


            ' cambiamos la fase de actividades y se genera un nueva versión
            For Each mylistActivity As ActivityEntity In listActivity
                mylistActivity.idphase = idPhase
                ActivityDalc.update(objApplicationCredentials, mylistActivity)
            Next

            ' consultamos la lista de subactividades para un proyecto
            listSubactivity = SubactivityDalc.getList(objApplicationCredentials, idProject:=idProject, isLastVersion:="1")


            ' cambiamos la fase de subactividades y se genera un nueva versión
            For Each mylistSubactivity As SubActivityEntity In listSubactivity
                mylistSubactivity.idphase = idPhase
                SubactivityDalc.update(objApplicationCredentials, mylistSubactivity)
            Next


            '' consultamos la lista de Riesgos para un proyecto
            'listRisk = RiskDalc.getList(objApplicationCredentials, idproject:=idProject, isLastVersion:="1")


            '' cambiamos la fase de los riesgos y se genera un nueva versión
            'For Each mylistRisk As RiskEntity In listRisk
            '    mylistRisk.idphase = idPhase
            '    RiskDalc.update(objApplicationCredentials, mylistRisk)
            'Next

            ' consultamos la lista de mitigación para un proyecto
            'listMitigation = MitigationDalc.getList(objApplicationCredentials, idproject:=idProject, isLastVersion:="1")


            '' cambiamos la fase de la mitigación y se genera un nueva versión
            'For Each mylistMitigation As MitigationEntity In listMitigation
            '    mylistMitigation.idphase = idPhase
            '    MitigationDalc.update(objApplicationCredentials, mylistMitigation)
            'Next






            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Project. ")

        Finally
            ' liberando recursos
            listActivity = Nothing
            ActivityDalc = Nothing
            listSubactivity = Nothing
            SubactivityDalc = Nothing
            listObjective = Nothing
            ObjectiveDalc = Nothing
            listComponent = Nothing
            ComponentDalc = Nothing
            listRisk = Nothing
            RiskDalc = Nothing
            listMitigation = Nothing
            MitigationDalc = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Cargar la lista de proyectos para exportarla a XML
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function exportList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim xml As New StringBuilder
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("  SELECT  Project.Name as Nombre, StrategicLine.Name as Programa,  ")
            sql.Append(" 			isnull(Third.Name, 'No Registrado') AS Operadores,  ")
            sql.Append(" 			ProjectPhase.Name as EstadoProyecto,  ")
            sql.Append(" 			isnull(Depto.Name, 'No Registrado') AS Departamento, ")
            sql.Append(" 			isnull(City.Name, 'No Registrado') AS Municipio ")
            sql.Append("            FROM Project  ")
            sql.Append("            	INNER JOIN ProjectPhase ON Project.IdPhase = ProjectPhase.id  ")
            sql.Append("            	INNER JOIN (SELECT DISTINCT ProgramComponentByProject.idProject, StrategicLine.Name  ")
            sql.Append("            				FROM ProgramComponent  ")
            sql.Append("            					INNER JOIN Program ON ProgramComponent.IdProgram = Program.Id  ")
            sql.Append("            					INNER JOIN StrategicLine ON Program.IdStrategicLine = StrategicLine.Id  ")
            sql.Append("            					INNER JOIN ProgramComponentByProject ON ProgramComponent.Id = ProgramComponentByProject.IdProgramComponent)   ")
            sql.Append("            				StrategicLine ON Project.Id = StrategicLine.idProject  ")
            sql.Append("            	LEFT OUTER JOIN OperatorByProject on Project.id = OperatorByProject.idproject ")
            sql.Append(" 				LEFT OUTER JOIN Third ON OperatorByProject.IdOperator = Third.Id ")
            sql.Append("            	LEFT OUTER JOIN ProjectLocation on Project.id = ProjectLocation.idproject ")
            sql.Append(" 				LEFT OUTER JOIN " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.Id ")
            sql.Append(" 				LEFT OUTER JOIN " & dbSecurityName & ".dbo.Depto Depto ON City.IdDepto = Depto.Id ")
            sql.Append(" FOR XML RAW ('Proyecto') , ELEMENTS  ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' iniciar el xml
            xml.Append("<Proyectos>")

            For Each row As DataRow In data.Rows

                ' concatenar la respuesta
                xml.Append(row(0))

            Next

            ' fianlizar el xml
            xml.Append("</Proyectos>")

            ' retornar el objeto
            exportList = xml.ToString

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "exportList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Projectos. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            xml = Nothing

        End Try

    End Function
    ''' <summary>
    ''' TODO: Funcion que consulta contra la base de datos la lista de proyectos aprobados
    ''' Autor: Pedro Cruz
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getListcontract(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal id As String = "", _
    Optional ByVal code As String = "" _
     ) As List(Of ProjectEntity)

        Dim sql As New StringBuilder
        Dim objProject As ProjectEntity
        Dim ProjectList As New List(Of ProjectEntity)
        Dim data As DataTable

        Try

            sql.Append("select a.id, a.Name, a.Typeapproval, ")
            sql.Append("convert(varchar,a.id) + '_' + a.Name + case when a.Typeapproval=2 then ' (Otro si)' else '' end as codigo, ")
            sql.Append("b.IdProject ")
            sql.Append("from Project a ")
            sql.Append("full outer join ContractRequest b ")
            sql.Append("on a.id = b.idproject ")
            sql.Append("where (a.id is null ")
            sql.Append("or b.IdProject is null) ")
            sql.Append("or a.id in( ")
            sql.Append("select IdProject from Project ")
            sql.Append("inner join contractrequest ")
            sql.Append("on project.id = contractrequest.idproject ")
            sql.Append("where(Project.Typeapproval = 2) ")
            sql.Append("group by IdProject ")
            sql.Append(") ")
            sql.Append("group by a.id, a.Name, a.Typeapproval, b.IdProject ")
            sql.Append("order by id desc ")

            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                objProject = New ProjectEntity

                objProject.id = row("id")
                objProject.code = row("codigo")

                ProjectList.Add(objProject)

            Next

            getListcontract = ProjectList

            CtxSetComplete()

        Catch ex As Exception

        End Try

    End Function

    Public Function finalUpdate(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idproject As Integer) As String

        Dim sql As New StringBuilder

        Try

            sql.Append("update Project set Typeapproval = 1 where id = " & idproject)
            GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)
            CtxSetComplete()

        Catch ex As Exception

        End Try

    End Function

    ''' <summary>
    ''' Cambia campos del proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateFields(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
              ByVal Project As ProjectEntity, _
                            Optional ByVal idPhase As Integer = 0) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim sql2 As New StringBuilder
        Try

            ' insercion de los campos 
            sql.Append("Update Project SET Typeapproval = " & Project.Typeapproval)
            sql.Append(", CounterpartValue=" & Project.counterpartvalue & ", Results = '" & Project.results)
            sql.Append("', Duration = '" & Project.duration)
            sql.Append("', editablemoney = '" & Project.editablemoney)
            sql.Append("', editableresults = '" & Project.editableresults)
            sql.Append("', editabletime = '" & Project.editabletime)
            sql.Append("', completiondate = '" & Project.completiondate)
            sql.Append("'  WHERE id = " & Project.id)

            'Ejecutar la Instruccion de insercion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)


            ' Deshabilitar el registro actual, de tal manera que quede como historial
            sql2.AppendLine("Update Project SET")
            sql2.AppendLine(" isLastVersion = 0")
            sql2.AppendLine("WHERE id = " & Project.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql2.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            sql2 = Nothing

        End Try


    End Function


    Public Function UpdateFromContract(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Project As ProjectEntity, ByVal ProjectId As Long) As Long

        Dim sql As New StringBuilder

        Try

            'construir la sentencia
            sql.AppendLine("Update Project SET")
            sql.AppendLine(" BeginDate = '" & Project.begindate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine(" completiondate = '" & Project.Enddate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE Id = " & ProjectId)

            'Ejecutar la Instrucción
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'Finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            CtxSetAbort()

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "UpdateFromContract")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            Throw New Exception("Error al actualizar el proyecto. - " & ex.Message)

        Finally

            sql = Nothing

        End Try


    End Function

    ''' <summary>
    ''' Edita campos del proyecto
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idProject"></param>
    ''' <param name="idPhase"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateNoApprovals(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
              ByVal Project As ProjectEntity, _
                            Optional ByVal idPhase As Integer = 0) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim sql2 As New StringBuilder
        Try

            ' insercion de los campos 
            sql.Append("Update Project SET Typeapproval = " & Project.Typeapproval)
            sql.Append(", CounterpartValue=" & Project.counterpartvalue & ", Results = '" & Project.results)
            sql.Append("', Duration = '" & Project.duration)
            sql.Append("', editablemoney = '" & Project.editablemoney)
            sql.Append("', editableresults = '" & Project.editableresults)
            sql.Append("', editabletime = '" & Project.editabletime)
            sql.Append("', name= '" & Project.name)
            sql.Append("', objective= '" & Project.objective)
            sql.Append("', antecedent= '" & Project.antecedent)
            sql.Append("', justification= '" & Project.justification)
            sql.Append("', begindate = '" & Project.begindate.ToString("yyyy/MM/dd HH:mm:ss"))
            sql.Append("', zonedescription= '" & Project.zonedescription)
            sql.Append("', population= '" & Project.population)
            sql.Append("',  strategicdescription= '" & Project.strategicdescription)
            sql.Append("', ResultsKnowledgeManagement= '" & Project.ResultsKnowledgeManagement)
            sql.Append("', ResultsInstalledCapacity= '" & Project.ResultsInstalledCapacity)

            sql.Append("', source= '" & Project.source)
            sql.Append("', purpose= '" & Project.purpose)
            sql.Append("', totalcost= '" & Project.totalcost)
            sql.Append("', fsccontribution= '" & Project.fsccontribution)

            sql.Append("', effectivebudget= '" & Project.effectivebudget)
            sql.Append("', attachment= '" & Project.attachment)
            sql.Append("', idphase= '" & Project.idphase)
            sql.Append("', enabled= '" & Project.enabled)
            'sql.Append("', iduser= '" & Project.iduser)
            'sql.Append("', createdate= '" & Project.createdate.ToString("yyyy/MM/dd HH:mm:ss"))
            sql.Append("', idKey= '" & Project.idKey)
            sql.Append("', isLastVersion= '" & Project.isLastVersion)
            sql.Append("', IdProcessInstance= ' " & Project.IdProcessInstance)
            sql.Append("', completiondate= ' " & Project.completiondate.ToString("yyyy/MM/dd HH:mm:ss"))

            sql.Append("'  WHERE id = " & Project.id)



            'Ejecutar la Instruccion de insercion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)


            ' Deshabilitar el registro actual, de tal manera que quede como historial
            sql2.AppendLine("Update Project SET")
            sql2.AppendLine(" isLastVersion = 0")
            sql2.AppendLine("WHERE id = " & Project.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql2.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "updateNoApprovals")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            sql2 = Nothing

        End Try


    End Function


End Class
