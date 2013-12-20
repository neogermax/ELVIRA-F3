Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class VisibilityLevelDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo VisibilityLevel
    ''' </summary>
    ''' <param name="VisibilityLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal VisibilityLevel As VisibilityLevelEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO VisibilityLevel(" & _
             "id," & _ 
             "code," & _ 
             "name," & _ 
             "iduser," & _ 
             "enabled," & _ 
             "createdate," & _ 
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & VisibilityLevel.id & "',")
            sql.AppendLine("'" & VisibilityLevel.code & "',")
            sql.AppendLine("'" & VisibilityLevel.name & "',")
            sql.AppendLine("'" & VisibilityLevel.iduser & "',")
            sql.AppendLine("'" & VisibilityLevel.enabled & "',")
            sql.AppendLine("'" & VisibilityLevel.createdate & "',")

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
            Throw New Exception("Error al insertar el VisibilityLevel. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un VisibilityLevel por el Id
    ''' </summary>
    ''' <param name="idVisibilityLevel"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idVisibilityLevel As Integer) As VisibilityLevelEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objVisibilityLevel As New VisibilityLevelEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM VisibilityLevel ")
            sql.Append(" WHERE Id = " & idVisibilityLevel)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objVisibilityLevel.id = data.Rows(0)("id")
				objVisibilityLevel.code = data.Rows(0)("code")
				objVisibilityLevel.name = data.Rows(0)("name")
				objVisibilityLevel.iduser = data.Rows(0)("iduser")
				objVisibilityLevel.enabled = data.Rows(0)("enabled")
				objVisibilityLevel.createdate = data.Rows(0)("createdate")

            End If

            ' retornar el objeto
            Return objVisibilityLevel

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un VisibilityLevel. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objVisibilityLevel = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="iduser"></param>
    ''' <param name="enabled"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of VisibilityLevelEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal code As String = "", _
								Optional ByVal name As String = "", _
								Optional ByVal iduser As String = "", _
								Optional ByVal enabled As String = "", _
								Optional ByVal createdate As String = "", _
								Optional order as string = "") As List(Of VisibilityLevelEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objVisibilityLevel As VisibilityLevelEntity
        Dim VisibilityLevelList As New List(Of VisibilityLevelEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")


        Try
            ' construir la sentencia
            sql.Append("SELECT VisibilityLevel.ID, Company.Code, Company.Name, Company.Enabled ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.Company as Company INNER JOIN ")
            sql.Append("    VisibilityLevel on VisibilityLevel.Code=Company.Code ")
            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " VisibilityLevel.id like '%" & id & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " Company.code like '%" & code & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " Company.name like '%" & name & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            'If Not iduser.Equals("") Then

            '    SQL.Append(where & " iduser like '%" & iduser & "%'")
            '    where = " AND "

            'End If
             
            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Company.enabled like '%" & enabled & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            'If Not createdate.Equals("") Then

            '    SQL.Append(where & " createdate like '%" & createdate & "%'")
            '    where = " AND "

            'End If
             
            If Not order.Equals(String.Empty) Then
            
				' ordernar
				SQL.Append(" ORDER BY " & order)
            
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objVisibilityLevel = New VisibilityLevelEntity

				' cargar el valor del campo
				objVisibilityLevel.id = row("id")
				objVisibilityLevel.code = row("code")
				objVisibilityLevel.name = row("name")
                'objVisibilityLevel.iduser = row("iduser")
				objVisibilityLevel.enabled = row("enabled")
                'objVisibilityLevel.createdate = row("createdate")

                ' agregar a la lista
                VisibilityLevelList.Add(objVisibilityLevel)

            Next

            ' retornar el objeto
            getList = VisibilityLevelList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de VisibilityLevel. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objVisibilityLevel = Nothing
            VisibilityLevelList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo VisibilityLevel
    ''' </summary>
    ''' <param name="VisibilityLevel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal VisibilityLevel As VisibilityLevelEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update VisibilityLevel SET")
            SQL.AppendLine(" id = '" & VisibilityLevel.id & "',")           
            SQL.AppendLine(" code = '" & VisibilityLevel.code & "',")           
            SQL.AppendLine(" name = '" & VisibilityLevel.name & "',")           
            SQL.AppendLine(" iduser = '" & VisibilityLevel.iduser & "',")           
            SQL.AppendLine(" enabled = '" & VisibilityLevel.enabled & "',")           
            SQL.AppendLine(" createdate = '" & VisibilityLevel.createdate & "',")           
            SQL.AppendLine("WHERE id = " & VisibilityLevel.id)

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
            Throw New Exception("Error al modificar el VisibilityLevel. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el VisibilityLevel de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idVisibilityLevel As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from VisibilityLevel ")
            SQL.AppendLine(" where id = '" & idVisibilityLevel & "' ")

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
            Throw New Exception("Error al elimiar el VisibilityLevel. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
