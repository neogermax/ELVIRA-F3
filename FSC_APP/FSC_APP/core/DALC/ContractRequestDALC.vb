Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractRequest
    ''' </summary>
    ''' <param name="ContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractRequest As ContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractRequest(")
            If (ContractRequest.idmanagement > 0) Then sql.AppendLine("idmanagement,")
            If (ContractRequest.idproject > 0) Then sql.AppendLine("idproject,")
            If (ContractRequest.idcontractnature > 0) Then sql.AppendLine("idcontractnature,")
            sql.AppendLine("contractnumberadjusted," & _
            "idprocessinstance," & _
            "idactivityinstance," & _
            "finished," & _
            "iduser," & _
            "createdate,")

            If (ContractRequest.suscriptdate <> "12:00:00 a.m.") Then sql.AppendLine("suscriptdate,")
            sql.AppendLine("monthduration, ")

            If (ContractRequest.LiquidationDate <> "12:00:00 a.m.") Then sql.AppendLine("LiquidationDate,")

            If (ContractRequest.startdate <> "12:00:00 a.m.") Then sql.AppendLine("startdate,")

            sql.AppendLine("supervisor," & _
            "confidential," & _
            "signedcontract," & _
            "notes" & _
           ")")
            sql.AppendLine("VALUES (")
            If (ContractRequest.idmanagement > 0) Then sql.AppendLine("'" & ContractRequest.idmanagement & "',")
            If (ContractRequest.idproject > 0) Then sql.AppendLine("'" & ContractRequest.idproject & "',")
            If (ContractRequest.idcontractnature > 0) Then sql.AppendLine("'" & ContractRequest.idcontractnature & "',")
            sql.AppendLine("'" & ContractRequest.contractnumberadjusted & "',")
            sql.AppendLine("'" & ContractRequest.idprocessinstance & "',")
            sql.AppendLine("'" & ContractRequest.idactivityinstance & "',")
            sql.AppendLine("'" & ContractRequest.enabled & "',")
            sql.AppendLine("'" & ContractRequest.iduser & "',")
            sql.AppendLine("'" & ContractRequest.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")

            'agregar campos nuevos
            If (ContractRequest.suscriptdate <> "12:00:00 a.m.") Then sql.AppendLine("convert(datetime, '" & ContractRequest.suscriptdate & "',103), ")
            sql.AppendLine("'" & Replace(ContractRequest.monthduration, ",", ".") & "',")
            If (ContractRequest.startdate <> "12:00:00 a.m.") Then sql.AppendLine("convert(datetime, '" & ContractRequest.startdate & "',103), ")
            If (ContractRequest.LiquidationDate <> "12:00:00 a.m.") Then sql.AppendLine("convert(datetime, '" & ContractRequest.LiquidationDate & "',103), ")
            sql.AppendLine("'" & ContractRequest.supervisor & "',")
            sql.AppendLine("'" & ContractRequest.confidential & "',")
            sql.AppendLine("'" & ContractRequest.signedcontract & "',")
            sql.AppendLine("'" & ContractRequest.notes & "' ")
            sql.AppendLine(")")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

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
            Throw New Exception("Error al insertar la Solicitud de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractRequest por el Id
    ''' </summary>
    ''' <param name="requestNumber"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal requestNumber As Integer) As ContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractRequest As New ContractRequestEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ContractRequest.*, ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM ContractRequest ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = ContractRequest.IdUser ")
            sql.Append(" WHERE requestnumber = " & requestNumber)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractRequest.requestnumber = data.Rows(0)("requestnumber")
                objContractRequest.idmanagement = data.Rows(0)("idmanagement")
                objContractRequest.idproject = IIf(Not IsDBNull(data.Rows(0)("idproject")), data.Rows(0)("idproject"), "0")
                objContractRequest.idcontractnature = IIf(Not IsDBNull(data.Rows(0)("idcontractnature")), data.Rows(0)("idcontractnature"), 0)
                objContractRequest.contractnumberadjusted = data.Rows(0)("contractnumberadjusted")
                objContractRequest.idprocessinstance = data.Rows(0)("idprocessinstance")
                objContractRequest.idactivityinstance = data.Rows(0)("idactivityinstance")
                objContractRequest.enabled = data.Rows(0)("finished")
                objContractRequest.iduser = IIf(Not IsDBNull(data.Rows(0)("iduser")), data.Rows(0)("iduser"), "0")
                objContractRequest.createdate = data.Rows(0)("createdate")
                objContractRequest.USERNAME = IIf(Not IsDBNull(data.Rows(0)("ApplicationUserName")), data.Rows(0)("ApplicationUserName"), "")

                'agregar los controles nuevos en contratacion
                objContractRequest.polizaid = IIf(Not IsDBNull(data.Rows(0)("polizaid")), data.Rows(0)("polizaid"), "0")
                objContractRequest.suscriptdate = IIf(Not IsDBNull(data.Rows(0)("suscriptdate")), data.Rows(0)("suscriptdate"), Nothing)
                objContractRequest.startdate = IIf(Not IsDBNull(data.Rows(0)("startdate")), data.Rows(0)("startdate"), Nothing)
                objContractRequest.LiquidationDate = IIf(Not IsDBNull(data.Rows(0)("LiquidationDate")), data.Rows(0)("LiquidationDate"), Nothing)
                objContractRequest.monthduration = IIf(Not IsDBNull(data.Rows(0)("monthduration")), data.Rows(0)("monthduration"), "0")
                objContractRequest.supervisor = IIf(Not IsDBNull(data.Rows(0)("supervisor")), data.Rows(0)("supervisor"), "")
                objContractRequest.confidential = IIf(Not IsDBNull(data.Rows(0)("confidential")), data.Rows(0)("confidential"), "0")
                objContractRequest.signedcontract = IIf(Not IsDBNull(data.Rows(0)("signedcontract")), data.Rows(0)("signedcontract"), False)
                objContractRequest.notes = IIf(Not IsDBNull(data.Rows(0)("notes")), data.Rows(0)("notes"), "")

            End If

            'traer el nombre del proyecto
            sql = New StringBuilder()
            sql.Append("select name from project where id = " & objContractRequest.idproject)
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then
                objContractRequest.PROJECTNAME = data.Rows(0)("name")
            End If

            ' retornar el objeto
            Return objContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una Solicitud de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractRequest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="requestnumber"></param>
    ''' <param name="requestnumberlike"></param>
    ''' <param name="idmanagement"></param>
    ''' <param name="managementname"></param>
    ''' <param name="idproject"></param>
    ''' <param name="projectname"></param>
    ''' <param name="idcontractnature"></param>
    ''' <param name="contractnaturename"></param>
    ''' <param name="contractnumberadjusted"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal requestnumber As String = "", _
        Optional ByVal requestnumberlike As String = "", _
        Optional ByVal idmanagement As String = "", _
        Optional ByVal managementname As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal idcontractnature As String = "", _
        Optional ByVal contractnaturename As String = "", _
        Optional ByVal contractnumberadjusted As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal finishfilter As String = "", _
        Optional ByVal order As String = "") As List(Of ContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractRequest As ContractRequestEntity
        Dim ContractRequestList As New List(Of ContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ContractRequest.*,Management.Name AS ManagementName, Project.Name AS ProjectName, ApplicationUser.Name AS ApplicationUserName ")
            sql.Append(" FROM ContractRequest ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = ContractRequest.IdUser  ")
            sql.Append(" INNER JOIN Management ON Management.Id = ContractRequest.IdManagement ")
            sql.Append(" LEFT JOIN Project ON Project.id = ContractRequest.IdProject") 'and Project.isLastVersion='1'")

            ' verificar si hay entrada de datos para el campo
            If Not requestnumber.Equals("") Then

                sql.Append(where & " ContractRequest.requestnumber = '" & requestnumber & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not requestnumberlike.Equals("") Then

                sql.Append(where & " ContractRequest.requestnumber like '%" & requestnumberlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idmanagement.Equals("") Then

                sql.Append(where & " ContractRequest.idmanagement = '" & idmanagement & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not managementname.Equals("") Then

                sql.Append(where & " Management.Name like '%" & managementname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " ContractRequest.idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcontractnature.Equals("") Then

                'verificar tipo de contratacion

                Select Case UCase(idcontractnature)

                    Case "CONTRATO"
                        idcontractnature = 1
                    Case "CONVENIO"
                        idcontractnature = 2
                    Case "ORDEN DE PRESTACIÓN DE SERVICIOS"
                        idcontractnature = 3
                    Case "ORDEN DE COMPRAVENTA"
                        idcontractnature = 4
                    Case "OTRO SI"
                        idcontractnature = 5
                    Case Else
                        idcontractnature = 6

                End Select

                sql.Append(where & " ContractRequest.IdContractNature like '%" & idcontractnature & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractnumberadjusted.Equals("") Then

                sql.Append(where & " ContractRequest.contractnumberadjusted like '%" & contractnumberadjusted & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " ContractRequest.finished = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " ContractRequest.finished IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "
            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " ContractRequest.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ContractRequest.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            'verificar si hay filtro por estado
            If Not finishfilter.Equals("") Then

                Select finishfilter

                    'Case "all"
                    Case "proccess"
                        sql.Append(where & " ContractRequest.Finished = 'False'")
                        where = " AND "
                    Case "finished"
                        sql.Append(where & " ContractRequest.Finished = 'True'")
                        where = " AND "

                End Select
            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                sql.Append(" ORDER BY ContractRequest.RequestNumber DESC ")
                'Select Case order
                '    Case "managementname"
                '        sql.Append(" ORDER BY Management.name ")
                '    Case "projectname"
                '        sql.Append(" ORDER BY Project.name ")
                '    Case "username"
                '        sql.Append(" ORDER BY ApplicationUser.name ")
                '    Case Else
                '        sql.Append(" ORDER BY ContractRequest." & order)
                'End Select
            End If

            ' ordernar
            sql.Append(" ORDER BY ContractRequest.CreateDate DESC ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objContractRequest = New ContractRequestEntity

                ' cargar el valor del campo
                objContractRequest.requestnumber = IIf(Not IsDBNull(row("requestnumber")), row("requestnumber"), "0")
                objContractRequest.idmanagement = IIf(Not IsDBNull(row("idmanagement")), row("idmanagement"), "0")
                objContractRequest.idproject = IIf(Not IsDBNull(row("idproject")), row("idproject"), "0")
                objContractRequest.idcontractnature = IIf(Not IsDBNull(row("idcontractnature")), row("idcontractnature"), "0")
                objContractRequest.contractnumberadjusted = IIf(Not IsDBNull(row("contractnumberadjusted")), row("contractnumberadjusted"), "0")
                objContractRequest.enabled = IIf(Not IsDBNull(row("finished")), row("finished"), False)
                objContractRequest.iduser = row("iduser")
                objContractRequest.createdate = row("createdate")
                objContractRequest.MANAGEMENTNAME = IIf(Not IsDBNull(row("ManagementName")), row("ManagementName"), "")
                objContractRequest.PROJECTNAME = IIf(Not IsDBNull(row("ProjectName")), row("ProjectName"), "")
                objContractRequest.USERNAME = row("ApplicationUserName")

                ' agregar a la lista
                ContractRequestList.Add(objContractRequest)

            Next

            ' retornar el objeto
            getList = ContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ContractRequest. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractRequest = Nothing
            ContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractRequest
    ''' </summary>
    ''' <param name="ContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractRequest As ContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractRequest SET")
            sql.AppendLine(" idmanagement = " & IIf(ContractRequest.idmanagement > 0, ContractRequest.idmanagement, "NULL") & ",")
            sql.AppendLine(" idproject = " & IIf(ContractRequest.idproject > 0, ContractRequest.idproject, "NULL") & ",")
            sql.AppendLine(" idcontractnature = " & IIf(ContractRequest.idcontractnature > 0, ContractRequest.idcontractnature, "NULL") & ",")
            sql.AppendLine(" contractnumberadjusted = '" & ContractRequest.contractnumberadjusted & "',")
            sql.AppendLine(" idprocessinstance = '" & ContractRequest.idprocessinstance & "',")
            sql.AppendLine(" idactivityinstance = '" & ContractRequest.idactivityinstance & "',")
            sql.AppendLine(" finished = '" & ContractRequest.enabled & "',")
            sql.AppendLine(" iduser = '" & ContractRequest.iduser & "',")
            sql.AppendLine(" createdate = '" & ContractRequest.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            'campos nuevos
            'If (ContractRequest.suscriptdate <> "12:00:00 a.m.") Then sql.AppendLine(" suscriptdate = convert(datetime, " & ContractRequest.suscriptdate & ",103),")
            sql.AppendLine(" monthduration = '" & ContractRequest.monthduration & "',")
            sql.AppendLine(" notes = '" & ContractRequest.notes & "',")
            sql.AppendLine(" supervisor = '" & ContractRequest.supervisor & "',")
            sql.AppendLine(" confidential = '" & ContractRequest.confidential & "',")
            sql.AppendLine(" signedcontract = '" & ContractRequest.signedcontract & "'")

            sql.AppendLine("WHERE requestnumber = " & ContractRequest.requestnumber)

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
            Throw New Exception("Error al modificar el ContractRequest. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractRequest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal requestNumber As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractRequest ")
            SQL.AppendLine(" where requestnumber = '" & requestNumber & "' ")

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
            Throw New Exception("Error al elimiar el ContractRequest. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar la información de los proyectos
    ''' segun una gerencia determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idManagement">identificador de la gerencia</param>
    ''' <param name="enabled">estado del registro</param>
    ''' <param name="order">orden de los campos</param>
    ''' <returns>lista de objetos de tipo ProjectEntity</returns>
    ''' <remarks></remarks>
    Public Function getListByManagement(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idManagement As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idphase As String = "") As List(Of ProjectEntity)

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
            sql.Append(" LEFT OUTER JOIN Management ON Management.Id = StrategicLine.IdManagment")
            sql.Append(" WHERE Project.IsLastVersion='1'")

            ' verificar si hay entrada de datos para el campo
            If Not idManagement.Equals("") Then

                sql.Append(where & " Management.Id = '" & idManagement & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Project.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idphase.Equals("") Then

                sql.Append(where & " Project.idphase <> '" & idphase & "'")
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
            getListByManagement = ProjectList

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


End Class
