Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SubActivityDALC

    ' contantes
    Const MODULENAME As String = "SubActivityDALC"

    ''' <summary> 
    ''' Verifica si ya existe el Número que es el usado como Código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyNumber(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal number As String, _
                                Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(number) AS cont FROM SubActivity WHERE isLastVersion = 1 AND number = '" & number & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(number) AS cont FROM SubActivity WHERE isLastVersion = 1 AND number = '" & number & "' AND id <> '" & id & "'")

            End If

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyNumber = False

                Else

                    ' retornar que existe
                    verifyNumber = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el número de SubActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo SubActivity
    ''' </summary>
    ''' <param name="SubActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SubActivity As SubActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim defaultDate As New DateTime

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO SubActivity(" & _
             "idactivity," & _
             "type," & _
             "number," & _
             "name," & _
             "description," & _
             "idresponsible," & _
             "begindate," & _
             "enddate," & _
             "totalcost," & _
             "duration," & _
             "fsccontribution," & _
             "ofcontribution," & _
             "attachment," & _
             "criticalpath," & _
             "requiresapproval," & _
             "enabled," & _
             "iduser," & _
             "createdate," & _
             "idphase," & _
             "idKey," & _
             "isLastVersion" & _
            ")")

            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SubActivity.idactivity & "',")
            sql.AppendLine("'" & SubActivity.type & "',")
            sql.AppendLine("'" & SubActivity.number & "',")
            sql.AppendLine("'" & SubActivity.name & "',")
            sql.AppendLine("'" & SubActivity.description & "',")
            sql.AppendLine("'" & SubActivity.idresponsible & "',")
            If (SubActivity.begindate = defaultDate) Then
                sql.AppendLine(" NULL, ")
            Else
                sql.AppendLine("'" & SubActivity.begindate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            If (SubActivity.enddate = defaultDate) Then
                sql.AppendLine(" NULL, ")
            Else
                sql.AppendLine("'" & SubActivity.enddate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            End If
            sql.AppendLine("'" & SubActivity.totalcost.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubActivity.duration & "',")
            sql.AppendLine("'" & SubActivity.fsccontribution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubActivity.ofcontribution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubActivity.attachment & "',")
            sql.AppendLine("'" & SubActivity.criticalpath & "',")
            sql.AppendLine("'" & SubActivity.requiresapproval & "',")
            sql.AppendLine("'" & SubActivity.enabled & "',")
            sql.AppendLine("'" & SubActivity.iduser & "',")
            sql.AppendLine("'" & SubActivity.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & SubActivity.idphase & "',")
            sql.AppendLine("'" & SubActivity.idKey & "',")
            sql.AppendLine("'" & SubActivity.isLastVersion & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            If SubActivity.idKey = 0 Then

                ' limpiar el sql
                sql.Remove(0, sql.Length)

                ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
                sql.AppendLine("Update SubActivity SET")
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
            Throw New Exception("Error al insertar el SubActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SubActivity por el Id
    ''' </summary>
    ''' <param name="idSubActivity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivity As Integer, _
       Optional ByVal consultLastVersion As Boolean = True) As SubActivityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSubActivity As New SubActivityEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SubActivity.Id, SubActivity.IdActivity, SubActivity.Type, SubActivity.Number, " & _
                       "        SubActivity.Name, SubActivity.Description, SubActivity.IdResponsible, SubActivity.BeginDate, " & _
                       "        SubActivity.EndDate, SubActivity.TotalCost, SubActivity.Duration, " & _
                       "        SubActivity.FSCContribution, SubActivity.OFContribution, SubActivity.Attachment, " & _
                       "        SubActivity.CriticalPath, SubActivity.RequiresApproval, SubActivity.Enabled," & _
                       "        SubActivity.IdUser, SubActivity.CreateDate, SubActivity.idKey, SubActivity.isLastVersion, " & _
                       "        (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 WHERE (SubActivity.IdResponsible = ID)) AS responsiblename, " & _
                       "        Activity.Title AS activitytitle, Component.Name AS componentname, " & _
                       "        Objective.Name AS objectivename, Project.Name AS projectname, ApplicationUser.Name AS username " & _
                       " FROM SubActivity INNER JOIN " & _
                       "    Activity ON SubActivity.IdActivity =Activity.idkey and Activity.IsLastVersion='1'  INNER JOIN " & _
                       "    Component ON Activity.IdComponent = Component.idkey and Component.IsLastVersion='1'INNER JOIN " & _
                       "    Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN " & _
                       "    Project ON Objective.IdProject = Project.idKey and Project.IsLastVersion='1' INNER JOIN " & _
                       "    " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivity.IdUser = ApplicationUser.ID ")

            'Se verifica si se desea consultar la última versión de la subactividad requerida.
            If (consultLastVersion) Then
                sql.Append(" WHERE SubActivity.idkey = " & idSubActivity & " and SubActivity.IsLastVersion='1'")
            Else
                sql.Append(" WHERE SubActivity.id = " & idSubActivity)
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objSubActivity.id = data.Rows(0)("id")
                objSubActivity.idactivity = data.Rows(0)("idactivity")
                objSubActivity.type = data.Rows(0)("type")
                objSubActivity.number = data.Rows(0)("number")
                objSubActivity.name = data.Rows(0)("name")
                objSubActivity.description = data.Rows(0)("description")
                objSubActivity.idresponsible = data.Rows(0)("idresponsible")
                objSubActivity.begindate = IIf(Not IsDBNull(data.Rows(0)("begindate")), data.Rows(0)("begindate"), Nothing)
                objSubActivity.enddate = IIf(Not IsDBNull(data.Rows(0)("enddate")), data.Rows(0)("enddate"), Nothing)
                objSubActivity.totalcost = data.Rows(0)("totalcost")
                objSubActivity.duration = data.Rows(0)("duration")
                objSubActivity.fsccontribution = data.Rows(0)("fsccontribution")
                objSubActivity.ofcontribution = data.Rows(0)("ofcontribution")
                objSubActivity.attachment = IIf(Not IsDBNull(data.Rows(0)("attachment")), data.Rows(0)("attachment"), "")
                objSubActivity.criticalpath = data.Rows(0)("criticalpath")
                objSubActivity.requiresapproval = data.Rows(0)("requiresapproval")
                objSubActivity.enabled = data.Rows(0)("enabled")
                objSubActivity.iduser = data.Rows(0)("iduser")
                objSubActivity.createdate = data.Rows(0)("createdate")
                objSubActivity.USERNAME = IIf(Not IsDBNull(data.Rows(0)("userName")), data.Rows(0)("userName"), "")
                objSubActivity.ACTIVITYTITLE = IIf(Not IsDBNull(data.Rows(0)("activitytitle")), data.Rows(0)("activitytitle"), "")
                objSubActivity.COMPONENTNAME = IIf(Not IsDBNull(data.Rows(0)("componentname")), data.Rows(0)("componentname"), "")
                objSubActivity.OBJECTIVENAME = IIf(Not IsDBNull(data.Rows(0)("objectivename")), data.Rows(0)("objectivename"), "")
                objSubActivity.PROJECTNAME = IIf(Not IsDBNull(data.Rows(0)("projectname")), data.Rows(0)("projectname"), "")
                objSubActivity.RESPONSIBLENAME = IIf(Not IsDBNull(data.Rows(0)("responsiblename")), data.Rows(0)("responsiblename"), "")
                objSubActivity.idKey = IIf(IsDBNull(data.Rows(0)("idKey")), 0, data.Rows(0)("idKey"))
                objSubActivity.isLastVersion = IIf(IsDBNull(data.Rows(0)("isLastVersion")), False, data.Rows(0)("isLastVersion"))

            End If

            ' retornar el objeto
            Return objSubActivity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SubActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSubActivity = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idactivity"></param>
    ''' <param name="activitytitle"></param>
    ''' <param name="type"></param>
    ''' <param name="number"></param>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <param name="idresponsible"></param>
    ''' <param name="responsiblename"></param>
    ''' <param name="begindate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="totalcost"></param>
    ''' <param name="duration"></param>
    ''' <param name="fsccontribution"></param>
    ''' <param name="ofcontribution"></param>
    ''' <param name="attachment"></param>
    ''' <param name="criticalpath"></param>
    ''' <param name="requiresapproval"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <param name="componentname"></param>
    ''' <param name="objectivename"></param>
    ''' <param name="projectname"></param>
    ''' <returns>un objeto de tipo List(Of SubActivityEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idactivity As String = "", _
        Optional ByVal activitytitle As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal typetext As String = "", _
        Optional ByVal number As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal begindate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal totalcost As String = "", _
        Optional ByVal duration As String = "", _
        Optional ByVal fsccontribution As String = "", _
        Optional ByVal ofcontribution As String = "", _
        Optional ByVal attachment As String = "", _
        Optional ByVal criticalpath As String = "", _
        Optional ByVal requiresapproval As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal componentname As String = "", _
        Optional ByVal objectivename As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal order As String = "", _
        Optional ByVal idKey As String = "", _
        Optional ByVal isLastVersion As String = "", _
         Optional ByVal idProject As String = "") As List(Of SubActivityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSubActivity As SubActivityEntity
        Dim SubActivityList As New List(Of SubActivityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SubActivity.Id, SubActivity.IdActivity, SubActivity.Type, SubActivity.Number, " & _
                       "        SubActivity.Name, SubActivity.Description, SubActivity.IdResponsible, SubActivity.BeginDate, " & _
                       "        SubActivity.EndDate, SubActivity.TotalCost, SubActivity.Duration, " & _
                       "        SubActivity.FSCContribution, SubActivity.OFContribution, SubActivity.Attachment, " & _
                       "        SubActivity.CriticalPath, SubActivity.RequiresApproval, SubActivity.Enabled," & _
                       "        SubActivity.IdUser, SubActivity.CreateDate, SubActivity.idKey, SubActivity.isLastVersion, " & _
                       "        (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 WHERE (SubActivity.IdResponsible = ID)) AS responsiblename, " & _
                       "        Activity.Title AS activitytitle, Component.Name AS componentname, " & _
                       "        Objective.Name AS objectivename, Project.Name AS projectname, ApplicationUser.Name AS username " & _
                       " FROM SubActivity INNER JOIN " & _
                       "    Activity ON SubActivity.IdActivity = Activity.idkey and Activity.IsLastVersion='1'  INNER JOIN " & _
                       "    Component ON Activity.IdComponent = Component.idkey and Component.IsLastVersion='1' INNER JOIN " & _
                       "    Objective ON Component.IdObjective = Objective.idkey and Objective.IsLastVersion='1' INNER JOIN " & _
                       "    Project ON Objective.IdProject = Project.idKey and Project.IsLastVersion='1' INNER JOIN " & _
                       "    " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SubActivity.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " SubActivity.id = '" & id & "'")
                where = " AND "

            End If

            If Not idlike.Equals("") Then

                sql.Append(where & " SubActivity.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idactivity.Equals("") Then

                sql.Append(where & " SubActivity.idactivity = '" & idactivity & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not activitytitle.Equals("") Then

                sql.Append(where & " Activity.Title like '%" & activitytitle & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not type.Equals("") Then

                sql.Append(where & " SubActivity.type = '" & type & "'")
                where = " AND "

            End If

            'verificar si hay entrada de datos para el campo
            If Not typetext.Equals("") Then

                sql.Append(where & " SubActivity.type like '%" & typetext & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not number.Equals("") Then

                sql.Append(where & " SubActivity.number like '%" & number & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " SubActivity.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " SubActivity.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idresponsible.Equals("") Then

                sql.Append(where & " SubActivity.responsible = '" & idresponsible & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not responsiblename.Equals("") Then

                sql.Append(where & " (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser AS ApplicationUser_1 WHERE (SubActivity.IdResponsible = ID)) like '%" & responsiblename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not begindate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivity.begindate, 103) like '%" & begindate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivity.enddate, 103) like '%" & enddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not totalcost.Equals("") Then

                sql.Append(where & " SubActivity.totalcost like '%" & totalcost & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not duration.Equals("") Then

                sql.Append(where & " SubActivity.duration like '%" & duration & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not fsccontribution.Equals("") Then

                sql.Append(where & " SubActivity.fsccontribution like '%" & fsccontribution & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ofcontribution.Equals("") Then

                sql.Append(where & " SubActivity.ofcontribution like '%" & ofcontribution & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachment.Equals("") Then

                sql.Append(where & " SubActivity.attachment like '%" & attachment & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not criticalpath.Equals("") Then

                sql.Append(where & " SubActivity.criticalpath = '" & criticalpath & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not requiresapproval.Equals("") Then

                sql.Append(where & " SubActivity.requiresapproval = '" & requiresapproval & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " SubActivity.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " SubActivity.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " SubActivity.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SubActivity.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not componentname.Equals("") Then

                sql.Append(where & " Component.Name like '%" & componentname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not objectivename.Equals("") Then

                sql.Append(where & " Objective.Name like '%" & objectivename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idKey.Equals("") Then

                sql.Append(where & "  SubActivity.idKey = '" & idKey & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not isLastVersion.Equals("") Then

                sql.Append(where & "   SubActivity.isLastVersion = '" & isLastVersion & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProject.Equals("") Then

                sql.Append(where & "  Objective.IdProject = '" & idProject & "'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY username ")
                    Case "activitytitle"
                        sql.Append(" ORDER BY activitytitle ")
                    Case "responsiblename"
                        sql.Append(" ORDER BY responsiblename ")
                    Case "componentname"
                        sql.Append(" ORDER BY componentname ")
                    Case "objectivename"
                        sql.Append(" ORDER BY objectivename ")
                    Case "projectname"
                        sql.Append(" ORDER BY projectname ")
                    Case Else
                        sql.Append(" ORDER BY SubActivity." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSubActivity = New SubActivityEntity

                ' cargar el valor del campo
                objSubActivity.id = row("id")
                objSubActivity.idactivity = row("idactivity")
                objSubActivity.type = row("type")
                objSubActivity.number = row("number")
                objSubActivity.name = row("name")
                objSubActivity.description = row("description")
                objSubActivity.idresponsible = row("idresponsible")
                objSubActivity.begindate = IIf(Not IsDBNull(row("begindate")), row("begindate"), Nothing)
                objSubActivity.enddate = IIf(Not IsDBNull(row("enddate")), row("enddate"), Nothing)
                objSubActivity.totalcost = row("totalcost")
                objSubActivity.duration = row("duration")
                objSubActivity.fsccontribution = row("fsccontribution")
                objSubActivity.ofcontribution = row("ofcontribution")
                objSubActivity.attachment = IIf(Not IsDBNull(row("attachment")), row("attachment"), "")
                objSubActivity.criticalpath = row("criticalpath")
                objSubActivity.requiresapproval = row("requiresapproval")
                objSubActivity.enabled = row("enabled")
                objSubActivity.iduser = row("iduser")
                objSubActivity.createdate = row("createdate")
                objSubActivity.USERNAME = IIf(Not IsDBNull(row("userName")), row("userName"), "")
                objSubActivity.ACTIVITYTITLE = IIf(Not IsDBNull(row("activitytitle")), row("activitytitle"), "")
                objSubActivity.COMPONENTNAME = IIf(Not IsDBNull(row("componentname")), row("componentname"), "")
                objSubActivity.OBJECTIVENAME = IIf(Not IsDBNull(row("objectivename")), row("objectivename"), "")
                objSubActivity.PROJECTNAME = IIf(Not IsDBNull(row("projectname")), row("projectname"), "")
                objSubActivity.RESPONSIBLENAME = IIf(Not IsDBNull(row("responsiblename")), row("responsiblename"), "")
                objSubActivity.idKey = IIf(IsDBNull(row("idKey")), 0, row("idKey"))
                objSubActivity.isLastVersion = IIf(IsDBNull(row("isLastVersion")), False, row("isLastVersion"))

                ' agregar a la lista
                SubActivityList.Add(objSubActivity)

            Next

            ' retornar el objeto
            getList = SubActivityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SubActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSubActivity = Nothing
            SubActivityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SubActivity
    ''' </summary>
    ''' <param name="SubActivity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SubActivity As SubActivityEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim defaultDate As New DateTime

        Try
            ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            sql.AppendLine("Update SubActivity SET")
            sql.AppendLine(" isLastVersion = 0")
            sql.AppendLine("WHERE id = " & SubActivity.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' insertar el nuevo registro
            add(objApplicationCredentials, SubActivity)

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
            Throw New Exception("Error al modificar el SubActivity. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SubActivity de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSubActivity As Integer, _
        ByVal idKey As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from SubActivity ")
            SQL.AppendLine(" where id = '" & idSubActivity & "' ")
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
            Throw New Exception("Error al elimiar el SubActivity. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
