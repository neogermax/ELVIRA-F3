Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class DocumentsDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Documents
    ''' </summary>
    ''' <param name="Documents"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Documents As DocumentsEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Documents(" & _
             "title," & _
             "description," & _
             "ideditedfor," & _
             "idvisibilitylevel," & _
             "iddocumenttype," & _
             "createdate," & _
             "iduser," & _
             "attachfile," & _
             "enabled" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Documents.title & "',")
            sql.AppendLine("'" & Documents.description & "',")
            sql.AppendLine("'" & Documents.ideditedfor & "',")
            sql.AppendLine("'" & Documents.idvisibilitylevel & "',")
            sql.AppendLine("'" & Documents.iddocumenttype & "',")
            sql.AppendLine("'" & Documents.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & Documents.iduser & "',")
            sql.AppendLine("'" & Documents.attachfile & "',")
            sql.AppendLine("'" & Documents.enabled & "')")

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
            Throw New Exception("Error al insertar el Documents. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Documents por el Id
    ''' </summary>
    ''' <param name="idDocuments"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocuments As Integer) As DocumentsEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objDocuments As New DocumentsEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Documents.*, ApplicationUser.Name AS ApplicationUserName, DocumentsByEntity.EntityName, DocumentsByEntity.IdnEntity, DocumentsByEntity.Id as IdDocumentsByEntity ")
            sql.Append(" FROM Documents ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Documents.IdUser = ApplicationUser.Id INNER JOIN ")
            sql.Append(" DocumentsByEntity on DocumentsByEntity.IdDocuments=Documents.Id ")
            sql.Append(" WHERE Documents.Id = " & idDocuments)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objDocuments.id = data.Rows(0)("id")
                objDocuments.title = data.Rows(0)("title")
                objDocuments.description = data.Rows(0)("description")
                objDocuments.ideditedfor = data.Rows(0)("ideditedfor")
                objDocuments.idvisibilitylevel = data.Rows(0)("idvisibilitylevel")
                objDocuments.iddocumenttype = data.Rows(0)("iddocumenttype")
                objDocuments.createdate = data.Rows(0)("createdate")
                objDocuments.iduser = data.Rows(0)("iduser")
                objDocuments.attachfile = data.Rows(0)("attachfile")
                objDocuments.enabled = data.Rows(0)("enabled")
                objDocuments.USERNAME = data.Rows(0)("ApplicationUserName")
                objDocuments.ENTITYNAME = data.Rows(0)("EntityName")
                objDocuments.idnEntity = data.Rows(0)("IdnEntity")
                objDocuments.documentByEntityId = data.Rows(0)("IdDocumentsByEntity")

            End If

            ' retornar el objeto
            Return objDocuments

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Documents. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objDocuments = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="title"></param>
    ''' <param name="description"></param>
    ''' <param name="ideditedfor"></param>
    ''' <param name="editedforname"></param>''' 
    ''' <param name="idvisibilitylevel"></param>
    ''' <param name="visibilitylevelname"></param>
    ''' <param name="iddocumenttype"></param>
    ''' <param name="documenttypename"></param>
    ''' <param name="createdate"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="attachfile"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="entityName"></param>
    ''' <returns>un objeto de tipo List(Of DocumentsEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal title As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal ideditedfor As String = "", _
        Optional ByVal editedforname As String = "", _
        Optional ByVal idvisibilitylevel As String = "", _
        Optional ByVal visibilitylevelname As String = "", _
        Optional ByVal iddocumenttype As String = "", _
        Optional ByVal documenttypename As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal attachfile As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal entityName As String = "", _
        Optional ByVal ProjectName As String = "", _
        Optional ByVal order As String = "") As List(Of DocumentsEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objDocuments As DocumentsEntity
        Dim DocumentsList As New List(Of DocumentsEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Documents.*, ApplicationUser.Name AS ApplicationUserName, VisibilityLevel.Name AS VisibilityLevelName," & _
                       "DocumentType.Name AS DocumentTypeName, Entities.Equivalence AS EntityName, Project.Name as ProjectName, Project.Code  as CodeName ")
            sql.Append(" FROM Documents ")
            sql.Append(" LEFT JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Documents.IdUser = ApplicationUser.Id ")
            sql.Append(" LEFT JOIN  VisibilityLevel ON Documents.IdVisibilityLevel = VisibilityLevel.Id ")
            sql.Append(" LEFT JOIN DocumentType ON Documents.IdDocumentType = DocumentType.Id ")
            sql.Append(" LEFT JOIN DocumentsByEntity ON Documents.Id = DocumentsByEntity.IdDocuments ")
            sql.Append(" LEFT JOIN Entities ON DocumentsByEntity.EntityName = Entities.EntityName ")

            sql.Append(" LEFT JOIN Project ON Project.idKey = DocumentsByEntity.IdnEntity and  Project.IslastVersion=1 and  DocumentsByEntity.EntityName='ProjectEntity' ")

            sql.Append(where & "(SELECT    Count( ApplicationUser.ID) ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.Company Company INNER JOIN ")
            sql.Append("   " & dbSecurityName & ".dbo.UserGroupsByCompany  UserGroupsByCompany ON Company.ID = UserGroupsByCompany.IDCompany INNER JOIN ")
            sql.Append("  " & dbSecurityName & ".dbo.UserGroup UserGroup  ON UserGroupsByCompany.IDUserGroup = UserGroup.id INNER JOIN ")
            sql.Append("  " & dbSecurityName & ".dbo.UsersByGroup UsersByGroup ON UserGroup.id = UsersByGroup.IDUserGroup INNER JOIN ")
            sql.Append("   " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON UsersByGroup.IDApplicationUser = ApplicationUser.ID ")
            sql.Append(" WHERE     (Company.Enabled = 'T') AND (ApplicationUser.ID =" & objApplicationCredentials.UserID & ") AND (Company.Code = VisibilityLevel.Code))>=1")
            where = " AND "






            ' verificar si hay entrada de datos para el campo
            If Not ProjectName.Equals("") Then

                sql.Append(where & " Project.Name like '%" & ProjectName & "%'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Documents.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " Documents.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not title.Equals("") Then

                sql.Append(where & " Documents.title like '%" & title & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " Documents.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ideditedfor.Equals("") Then

                sql.Append(where & " Documents.IdEditedFor = '" & ideditedfor & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not editedforname.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & editedforname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idvisibilitylevel.Equals("") Then

                sql.Append(where & " Documents.IdVisibilityLevel = '" & idvisibilitylevel & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not visibilitylevelname.Equals("") Then

                sql.Append(where & " VisibilityLevel.Name like '%" & visibilitylevelname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iddocumenttype.Equals("") Then

                sql.Append(where & " Documents.IdDocumentType = '" & iddocumenttype & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not documenttypename.Equals("") Then

                sql.Append(where & " DocumentType.Name like '%" & documenttypename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Documents.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Documents.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attachfile.Equals("") Then

                sql.Append(where & " Documents.attachfile like '%" & attachfile & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Documents.enabled like '%" & enabled & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Documents.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext.Trim() & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not entityName.Equals("") Then

                sql.Append(where & " Entities.Equivalence like '%" & entityName & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "editedforname"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case "visibilitylevelname"
                        sql.Append(" ORDER BY VisibilityLevel.Name ")
                    Case "documenttypename"
                        sql.Append(" ORDER BY DocumentType.Name ")
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case "entityName"
                        sql.Append(" ORDER BY Entities.Equivalence ")
                    Case Else
                        sql.Append(" ORDER BY Documents." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objDocuments = New DocumentsEntity

                ' cargar el valor del campo
                objDocuments.id = row("id")
                objDocuments.title = row("title")
                objDocuments.description = row("description")
                objDocuments.ideditedfor = row("ideditedfor")
                objDocuments.idvisibilitylevel = row("idvisibilitylevel")
                objDocuments.iddocumenttype = row("iddocumenttype")
                objDocuments.createdate = row("createdate")
                objDocuments.iduser = row("iduser")
                objDocuments.attachfile = row("attachfile")
                objDocuments.enabled = row("enabled")
                objDocuments.USERNAME = IIf(IsDBNull(row("ApplicationUserName")), "", row("ApplicationUserName"))
                objDocuments.EDITEDFORNAME = IIf(IsDBNull(row("ApplicationUserName")), "", row("ApplicationUserName"))
                objDocuments.VISIBILITYLEVELNAME = IIf(IsDBNull(row("VisibilityLevelName")), "", row("VisibilityLevelName"))
                objDocuments.DOCUMENTTYPENAME = IIf(IsDBNull(row("DocumentTypeName")), "", row("DocumentTypeName"))
                objDocuments.ENTITYNAME = IIf(IsDBNull(row("EntityName")), "", row("EntityName"))
                objDocuments.ProjectName = IIf(IsDBNull(row("ProjectName")), "", row("ProjectName"))

                ' agregar a la lista
                DocumentsList.Add(objDocuments)

            Next

            ' retornar el objeto
            getList = DocumentsList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Documents. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objDocuments = Nothing
            DocumentsList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Documents
    ''' </summary>
    ''' <param name="Documents"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Documents As DocumentsEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Documents SET")
            sql.AppendLine(" title = '" & Documents.title & "',")
            sql.AppendLine(" description = '" & Documents.description & "',")
            sql.AppendLine(" ideditedfor = '" & Documents.ideditedfor & "',")
            sql.AppendLine(" idvisibilitylevel = '" & Documents.idvisibilitylevel & "',")
            sql.AppendLine(" iddocumenttype = '" & Documents.iddocumenttype & "',")
            sql.AppendLine(" attachfile = '" & Documents.attachfile & "',")
            sql.AppendLine(" enabled = '" & Documents.enabled & "'")
            sql.AppendLine(" WHERE id = " & Documents.id)

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
            Throw New Exception("Error al modificar el Documents. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Documents de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idDocuments As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Documents ")
            SQL.AppendLine(" where id = '" & idDocuments & "' ")

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
            Throw New Exception("Error al elimiar el Documents. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Permite consultar una lista de documentos anexos a una entidad determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="idsDocuments">una lista separa por comas de los ids de los documentos requeridos</param>
    ''' <returns>una lista de documentos</returns>
    ''' <remarks></remarks>
    Public Function getListByEntity(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
         ByVal idsDocuments As String) As List(Of DocumentsEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objDocuments As DocumentsEntity
        Dim DocumentsList As New List(Of DocumentsEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Documents.*, ApplicationUser.Name AS ApplicationUserName, VisibilityLevel.Name AS VisibilityLevelName, DocumentType.Name AS DocumentTypeName ")
            sql.Append(" FROM Documents ")
            sql.Append(" LEFT JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Documents.IdUser = ApplicationUser.Id ")
            sql.Append(" LEFT JOIN " & dbSecurityName & ".dbo.Company VisibilityLevel ON Documents.IdVisibilityLevel = VisibilityLevel.Id ")
            sql.Append(" LEFT JOIN DocumentType ON Documents.IdDocumentType = DocumentType.Id ")
            sql.Append(" WHERE Documents.id IN (" & idsDocuments & ")  ")
            'sql.Append(" (SELECT    Count( ApplicationUser.ID) ")
            'sql.Append(" FROM " & dbSecurityName & ".dbo.Company Company INNER JOIN ")
            'sql.Append("   " & dbSecurityName & ".dbo.UserGroupsByCompany  UserGroupsByCompany ON Company.ID = UserGroupsByCompany.IDCompany INNER JOIN ")
            'sql.Append("  " & dbSecurityName & ".dbo.UserGroup UserGroup  ON UserGroupsByCompany.IDUserGroup = UserGroup.id INNER JOIN ")
            'sql.Append("  " & dbSecurityName & ".dbo.UsersByGroup UsersByGroup ON UserGroup.id = UsersByGroup.IDUserGroup INNER JOIN ")
            'sql.Append("   " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON UsersByGroup.IDApplicationUser = ApplicationUser.ID ")
            'sql.Append(" WHERE     (Company.Enabled = 'T') AND (ApplicationUser.ID =" & objApplicationCredentials.UserID & ") AND (Company.Code = VisibilityLevel.Code))>=1")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objDocuments = New DocumentsEntity

                ' cargar el valor del campo
                objDocuments.id = row("id")
                objDocuments.title = row("title")
                objDocuments.description = row("description")
                objDocuments.ideditedfor = row("ideditedfor")
                objDocuments.idvisibilitylevel = row("idvisibilitylevel")
                objDocuments.iddocumenttype = row("iddocumenttype")
                objDocuments.createdate = row("createdate")
                objDocuments.iduser = row("iduser")
                objDocuments.attachfile = row("attachfile")
                objDocuments.enabled = row("enabled")
                objDocuments.USERNAME = IIf(IsDBNull(row("ApplicationUserName")), "", row("ApplicationUserName"))
                objDocuments.EDITEDFORNAME = IIf(IsDBNull(row("ApplicationUserName")), "", row("ApplicationUserName"))
                objDocuments.VISIBILITYLEVELNAME = IIf(IsDBNull(row("VisibilityLevelName")), "", row("VisibilityLevelName"))
                objDocuments.DOCUMENTTYPENAME = IIf(IsDBNull(row("DocumentTypeName")), "", row("DocumentTypeName"))

                ' agregar a la lista
                DocumentsList.Add(objDocuments)

            Next

            ' retornar el objeto
            getListByEntity = DocumentsList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Documents. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objDocuments = Nothing
            DocumentsList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function


End Class
