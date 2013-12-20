Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProjectLocationDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ProjectLocation
    ''' </summary>
    ''' <param name="ProjectLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal ProjectLocation As ProjectLocationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ProjectLocation(" & _
             "idproject, " & _
             "idcity, " & _
             "iddepto, " & _
             "deptoname, " & _
             "cityname " & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ProjectLocation.idproject & "',")
            sql.AppendLine("'" & ProjectLocation.idcity & "',")
            sql.AppendLine("'" & ProjectLocation.IDDEPTO & "',")
            sql.AppendLine("'" & ProjectLocation.DEPTONAME & "',")
            sql.AppendLine("'" & ProjectLocation.CITYNAME & "')")



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
            Throw New Exception("Error al insertar el ProjectLocation. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un ProjectLocation por el Id
    ''' </summary>
    ''' <param name="idProjectLocation"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idProjectLocation As Integer) As ProjectLocationEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProjectLocation As New ProjectLocationEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ProjectLocation.Id, ProjectLocation.IdProject, ProjectLocation.IdCity, City.Name AS citiyname, ")
            sql.Append("    Depto.ID AS iddepto, Depto.Name AS deptoname ")
            sql.Append(" FROM ProjectLocation INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.ID INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID ")
            sql.Append(" WHERE ProjectLocation.Id = " & idProjectLocation)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProjectLocation.id = data.Rows(0)("id")
                objProjectLocation.idproject = data.Rows(0)("idproject")
                objProjectLocation.idcity = data.Rows(0)("idcity")
                objProjectLocation.CITYNAME = data.Rows(0)("citiyname")
                objProjectLocation.IDDEPTO = data.Rows(0)("iddepto")
                objProjectLocation.DEPTONAME = data.Rows(0)("deptoname")

            End If

            ' retornar el objeto
            Return objProjectLocation

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProjectLocation. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProjectLocation = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproject"></param>
    ''' <param name="idcity"></param>
    ''' <returns>un objeto de tipo List(Of ProjectLocationEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idproject As String = "", _
								Optional ByVal idcity As String = "", _
								Optional order as string = "") As List(Of ProjectLocationEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProjectLocation As ProjectLocationEntity
        Dim ProjectLocationList As New List(Of ProjectLocationEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ProjectLocation.Id, ProjectLocation.IdProject, ProjectLocation.IdCity, City.Name AS citiyname, ")
            sql.Append("    Depto.ID AS iddepto, Depto.Name AS deptoname ")
            sql.Append(" FROM ProjectLocation INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.City City ON ProjectLocation.IdCity = City.ID INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " idproject = '" & idproject & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idcity.Equals("") Then

                sql.Append(where & " idcity = '" & idcity & "'")
                where = " AND "

            End If
             
            If Not order.Equals(String.Empty) Then
            
				' ordernar
				SQL.Append(" ORDER BY " & order)
            
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProjectLocation = New ProjectLocationEntity

				' cargar el valor del campo
				objProjectLocation.id = row("id")
				objProjectLocation.idproject = row("idproject")
                objProjectLocation.idcity = row("idcity")
                objProjectLocation.CITYNAME = row("citiyname")
                objProjectLocation.IDDEPTO = row("iddepto")
                objProjectLocation.DEPTONAME = row("deptoname")

                ' agregar a la lista
                ProjectLocationList.Add(objProjectLocation)

            Next

            ' retornar el objeto
            getList = ProjectLocationList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProjectLocation. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objProjectLocation = Nothing
            ProjectLocationList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo ProjectLocation
    ''' </summary>
    ''' <param name="ProjectLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal ProjectLocation As ProjectLocationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ProjectLocation SET")
            SQL.AppendLine(" idproject = '" & ProjectLocation.idproject & "',")           
            sql.AppendLine(" idcity = '" & ProjectLocation.idcity & "'")
            SQL.AppendLine("WHERE id = " & ProjectLocation.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el ProjectLocation. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el ProjectLocation de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProjectLocation As Integer, _
       Optional ByVal idProject As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProjectLocation ")
            If idProject = 0 Then
                SQL.AppendLine(" where id = '" & idProjectLocation & "' ")
            Else
                SQL.AppendLine(" where idProject = '" & idProject & "' ")
            End If

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
            Throw New Exception("Error al elimiar el ProjectLocation. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
