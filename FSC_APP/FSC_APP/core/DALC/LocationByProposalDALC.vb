Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class LocationByProposalDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo LocationByProposal
    ''' </summary>
    ''' <param name="LocationByProposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal LocationByProposal As LocationByProposalEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO LocationByProposal(" & _
             "idproposal," & _
             "iddepto," & _
             "idcity" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & LocationByProposal.idproposal & "',")
            sql.AppendLine("'" & LocationByProposal.DEPTO.id & "',")
            sql.AppendLine("'" & LocationByProposal.CITY.id & "')")

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
            Throw New Exception("Error al insertar la ubicación por propuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un LocationByProposal por el Id
    ''' </summary>
    ''' <param name="idLocationByProposal"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idLocationByProposal As Integer) As LocationByProposalEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objLocationByProposal As New LocationByProposalEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM LocationByProposal ")
            sql.Append(" WHERE Id = " & idLocationByProposal)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objLocationByProposal.id = data.Rows(0)("id")
				objLocationByProposal.idproposal = data.Rows(0)("idproposal")
                objLocationByProposal.DEPTO.id = data.Rows(0)("iddepto")
                objLocationByProposal.CITY.id = data.Rows(0)("idcity")

            End If

            ' retornar el objeto
            Return objLocationByProposal

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la ubicación por propuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objLocationByProposal = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproposal"></param>
    ''' <param name="iddepto"></param>
    ''' <param name="idcity"></param>
    ''' <returns>un objeto de tipo List(Of LocationByProposalEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idproposal As String = "", _
								Optional ByVal iddepto As String = "", _
								Optional ByVal idcity As String = "", _
								Optional order as string = "") As List(Of LocationByProposalEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objLocationByProposal As LocationByProposalEntity
        Dim LocationByProposalList As New List(Of LocationByProposalEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT LocationByProposal.*, Depto.Name AS DeptoName, City.Name AS CityName ")
            sql.Append(" FROM LocationByProposal ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.Depto Depto ON  LocationByProposal.IdDepto = Depto.Id ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.City City ON  LocationByProposal.IdCity = City.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                SQL.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idproposal.Equals("") Then

                SQL.Append(where & " idproposal like '%" & idproposal & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not iddepto.Equals("") Then

                SQL.Append(where & " iddepto like '%" & iddepto & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idcity.Equals("") Then

                SQL.Append(where & " idcity like '%" & idcity & "%'")
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
                objLocationByProposal = New LocationByProposalEntity

				' cargar el valor del campo
				objLocationByProposal.id = row("id")
				objLocationByProposal.idproposal = row("idproposal")
                objLocationByProposal.DEPTO.id = row("iddepto")
                objLocationByProposal.DEPTO.name = row("DeptoName")
                objLocationByProposal.CITY.id = row("idcity")
                objLocationByProposal.CITY.name = row("CityName")
                ' agregar a la lista
                LocationByProposalList.Add(objLocationByProposal)

            Next

            ' retornar el objeto
            getList = LocationByProposalList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista ubicaciones por propuesta.")

        Finally
            ' liberando recursos
            SQL = Nothing
            objLocationByProposal = Nothing
            LocationByProposalList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo LocationByProposal
    ''' </summary>
    ''' <param name="LocationByProposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal LocationByProposal As LocationByProposalEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update LocationByProposal SET")
            sql.AppendLine(" idproposal = '" & LocationByProposal.idproposal & "',")
            sql.AppendLine(" iddepto = '" & LocationByProposal.DEPTO.id & "',")
            sql.AppendLine(" idcity = '" & LocationByProposal.CITY.id & "'")
            sql.AppendLine(" WHERE id = " & LocationByProposal.id)

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
            Throw New Exception("Error al modificar la ubicación por propuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el LocationByProposal de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idLocationByProposal As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from LocationByProposal ")
            SQL.AppendLine(" where id = '" & idLocationByProposal & "' ")

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
            Throw New Exception("Error al eliminar la ubicación por propuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de las ubicaciones por propuesta
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idProposal">identificador de la propuesta</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idProposal As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from LocationByProposal ")
            SQL.AppendLine(" where IdProposal = '" & idProposal & "' ")

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
            Throw New Exception("Error al elimiar la ubicación por prouesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           
End Class
