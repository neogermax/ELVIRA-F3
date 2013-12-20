Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ExecutionDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Execution
    ''' </summary>
    ''' <param name="Execution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal Execution As ExecutionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Execution(" & _
             "idproject," & _
             "learning," & _
             "adjust," & _
             "achievements," & _
             "enable," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Execution.idproject & "',")
            sql.AppendLine("'" & Execution.learning & "',")
            sql.AppendLine("'" & Execution.adjust & "',")
            sql.AppendLine("'" & Execution.achievements & "',")
            sql.AppendLine("'" & Execution.enable & "',")
            sql.AppendLine("'" & Execution.iduser & "',")
            sql.AppendLine("'" & Execution.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el Execution. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un Execution por el Id
    ''' </summary>
    ''' <param name="idExecution"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idExecution As Integer) As ExecutionEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objExecution As New ExecutionEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("SELECT  Execution.Id, Execution.IdProject, Execution.QualitativeIndicators, Execution.Learning, Execution.Adjust,  ")
            sql.Append(" Execution.achievements, Execution.Enable, Execution.IdUser, Execution.CreateDate, ")
            sql.Append(" Project.Name as projectName,   ApplicationUser.Name AS userName ")
            sql.Append(" FROM Execution INNER JOIN ")
            sql.Append("  Project ON Execution.IdProject = Project.idkey and Project.IsLastVersion='1' INNER JOIN ")
            sql.Append("  " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Execution.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE  Execution.Id = " & idExecution)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objExecution.id = data.Rows(0)("id")
				objExecution.idproject = data.Rows(0)("idproject")
				objExecution.qualitativeindicators = data.Rows(0)("qualitativeindicators")
				objExecution.learning = data.Rows(0)("learning")
				objExecution.adjust = data.Rows(0)("adjust")
				objExecution.achievements = data.Rows(0)("achievements")
				objExecution.enable = data.Rows(0)("enable")
				objExecution.iduser = data.Rows(0)("iduser")
                objExecution.createdate = data.Rows(0)("createdate")
                objExecution.USERNAME = data.Rows(0)("userName")
                objExecution.PROJECTNAME = data.Rows(0)("projectName")
            End If

            ' retornar el objeto
            Return objExecution

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Execution. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objExecution = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproject"></param>
    ''' <param name="qualitativeindicators"></param>
    ''' <param name="learning"></param>
    ''' <param name="adjust"></param>
    ''' <param name="achievements"></param>
    ''' <param name="enable"></param>
    ''' <param name="iduser"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ExecutionEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idproject As String = "", _
         Optional ByVal projectname As String = "", _
        Optional ByVal qualitativeindicators As String = "", _
        Optional ByVal learning As String = "", _
        Optional ByVal Adjust As String = "", _
           Optional ByVal achievements As String = "", _
        Optional ByVal TestimonyName As String = "", _
          Optional ByVal enable As String = "", _
           Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal order As String = "") As List(Of ExecutionEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objExecution As ExecutionEntity
        Dim ExecutionList As New List(Of ExecutionEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append("SELECT     Execution.Id, Execution.IdProject, Execution.QualitativeIndicators, Execution.Learning, Execution.Adjust,  ")
            sql.Append(" Execution.achievements, Execution.Enable, Execution.IdUser, Execution.CreateDate, ")
            sql.Append(" Project.Name as projectName,ISNULL( p.Name,'') as TestimonyName,   ApplicationUser.Name AS userName ")
            sql.Append(" FROM Execution INNER JOIN ")
            sql.Append("  Project ON Execution.IdProject = Project.idkey and Project.IsLastVersion='1' LEFT OUTER join ")
            sql.Append("  (Select Execution.Id, (  SELECT top 1 Testimony.Name FROM  Testimony where  Execution.Id=Testimony.IdExecution ")
            ' verificar si hay entrada de datos para el campo
            If Not TestimonyName.Equals("") Then
                sql.Append(" and  Testimony.Name like '%" & TestimonyName & "%'")
            End If
            sql.Append(" ) as name from Execution ) as p  on  p.Id=Execution.Id  INNER JOIN ")
            sql.Append("  " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Execution.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Execution.id = '" & id & "'")
                where = " AND "

            End If

            If Not idlike.Equals("") Then

                sql.Append(where & " Execution.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " Execution.idproject '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Project.Name like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not qualitativeindicators.Equals("") Then

                sql.Append(where & " Execution.qualitativeindicators like '%" & qualitativeindicators & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not learning.Equals("") Then

                sql.Append(where & " Execution.learning like '%" & learning & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not Adjust.Equals("") Then

                sql.Append(where & " Execution.adjust like '%" & Adjust & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not achievements.Equals("") Then

                sql.Append(where & " Execution.achievements like '%" & achievements & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not TestimonyName.Equals("") Then

                sql.Append(where & " p.Name like '%" & TestimonyName & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enable.Equals("") Then

                sql.Append(where & " Execution.enable = '" & enable & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " Execution.enable IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " Execution.iduser  '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & "  ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Execution.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY  ApplicationUser.Name  ")
                    Case "testimonyname"
                        sql.Append(" ORDER BY   p.Name  ")
                    Case Else
                        sql.Append(" ORDER BY Execution." & order)
                End Select
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objExecution = New ExecutionEntity

                ' cargar el valor del campo
                objExecution.id = row("id")
                objExecution.idproject = row("idproject")
                objExecution.PROJECTNAME = row("projectName")
                objExecution.qualitativeindicators = row("qualitativeindicators")
                objExecution.learning = row("learning")
                objExecution.adjust = row("adjust")
                objExecution.achievements = row("achievements")
                objExecution.enable = row("enable")
                objExecution.iduser = row("iduser")
                objExecution.USERNAME = row("userName")
                objExecution.createdate = row("createdate")
                objExecution.TESTIMONYNAME = row("testimonyName")

                ' agregar a la lista
                ExecutionList.Add(objExecution)

            Next

            ' retornar el objeto
            getList = ExecutionList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Execution. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objExecution = Nothing
            ExecutionList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Execution
    ''' </summary>
    ''' <param name="Execution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Execution As ExecutionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Execution SET")
            sql.AppendLine(" idproject = '" & Execution.idproject & "',")
            sql.AppendLine(" qualitativeindicators = '" & Execution.qualitativeindicators & "',")
            sql.AppendLine(" learning = '" & Execution.learning & "',")
            sql.AppendLine(" adjust = '" & Execution.adjust & "',")
            sql.AppendLine(" achievements = '" & Execution.achievements & "',")
            sql.AppendLine(" enable = '" & Execution.enable & "'")
            sql.AppendLine("WHERE id = " & Execution.id)

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
            Throw New Exception("Error al modificar el Execution. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Execution de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecution As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Execution ")
            SQL.AppendLine(" where id = '" & idExecution & "' ")

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
            Throw New Exception("Error al elimiar el Execution. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
