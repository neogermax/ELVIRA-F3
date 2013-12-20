Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class LocationByIdeaDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo LocationByIdea
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal LocationByIdea As LocationByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO LocationByIdea(" & _
             "ididea," & _
             "iddepto," & _
             "idcity" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & LocationByIdea.ididea & "',")
            sql.AppendLine("'" & LocationByIdea.DEPTO.id & "',")
            sql.AppendLine("'" & LocationByIdea.CITY.id & "')")

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
            Throw New Exception("Error al insertar el LocationByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un LocationByIdea por el Id
    ''' </summary>
    ''' <param name="idLocationByIdea"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idLocationByIdea As Integer) As LocationByIdeaEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objLocationByIdea As New LocationByIdeaEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM LocationByIdea ")
            sql.Append(" WHERE Id = " & idLocationByIdea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objLocationByIdea.id = data.Rows(0)("id")
				objLocationByIdea.ididea = data.Rows(0)("ididea")
                objLocationByIdea.DEPTO.id = data.Rows(0)("iddepto")
                objLocationByIdea.CITY.id = data.Rows(0)("idcity")

            End If

            ' retornar el objeto
            Return objLocationByIdea

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un LocationByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objLocationByIdea = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="ididea"></param>
    ''' <param name="iddepto"></param>
    ''' <param name="idcity"></param>
    ''' <returns>un objeto de tipo List(Of LocationByIdeaEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal ididea As String = "", _
								Optional ByVal iddepto As String = "", _
								Optional ByVal idcity As String = "", _
								Optional order as string = "") As List(Of LocationByIdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objLocationByIdea As LocationByIdeaEntity
        Dim LocationByIdeaList As New List(Of LocationByIdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT LocationByIdea.*, Depto.Name AS DeptoName, City.Name AS CityName ")
            sql.Append(" FROM LocationByIdea ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.Depto Depto ON  LocationByIdea.iddepto = Depto.Id ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.City City ON  LocationByIdea.idcity = City.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ididea.Equals("") Then

                sql.Append(where & " ididea like '%" & ididea & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iddepto.Equals("") Then

                sql.Append(where & " iddepto like '%" & iddepto & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcity.Equals("") Then

                sql.Append(where & " idcity like '%" & idcity & "%'")
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
                objLocationByIdea = New LocationByIdeaEntity

				' cargar el valor del campo
				objLocationByIdea.id = row("id")
				objLocationByIdea.ididea = row("ididea")
                objLocationByIdea.DEPTO.id = row("iddepto")
                objLocationByIdea.DEPTO.name = row("DeptoName")
                objLocationByIdea.CITY.id = row("idcity")
                objLocationByIdea.CITY.name = row("CityName")

                ' agregar a la lista
                LocationByIdeaList.Add(objLocationByIdea)

            Next

            ' retornar el objeto
            getList = LocationByIdeaList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de LocationByIdea. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objLocationByIdea = Nothing
            LocationByIdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ' creado por hernan gomez  MG group
    '' <summary>
    ''' traer un objeto lista de objetos de tipo LocationByIdea para ser guardados en projecto
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function getListByProject(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal iddepto As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal order As String = "") As List(Of LocationByIdeaEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objLocationByIdea As LocationByIdeaEntity
        Dim LocationByIdeaList As New List(Of LocationByIdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT LocationByIdea.*, Depto.Name AS DeptoName, City.Name AS CityName ")
            sql.Append(" FROM LocationByIdea ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.Depto Depto ON  LocationByIdea.iddepto = Depto.Id ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.City City ON  LocationByIdea.idcity = City.Id ")
            sql.Append(" WHERE LocationByIdea.IdIdea =" & id)
            
            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objLocationByIdea = New LocationByIdeaEntity

                ' cargar el valor del campo
                objLocationByIdea.id = row("id")
                objLocationByIdea.ididea = row("ididea")
                objLocationByIdea.DEPTO.id = row("iddepto")
                objLocationByIdea.DEPTO.name = row("DeptoName")
                objLocationByIdea.CITY.id = row("idcity")
                objLocationByIdea.CITY.name = row("CityName")

                ' agregar a la lista
                LocationByIdeaList.Add(objLocationByIdea)

            Next

            ' retornar el objeto
            getListByProject = LocationByIdeaList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de LocationByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objLocationByIdea = Nothing
            LocationByIdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Modificar un objeto de tipo LocationByIdea
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal LocationByIdea As LocationByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update LocationByIdea SET")
            SQL.AppendLine(" id = '" & LocationByIdea.id & "',")           
            SQL.AppendLine(" ididea = '" & LocationByIdea.ididea & "',")           
            sql.AppendLine(" iddepto = '" & LocationByIdea.DEPTO.id & "',")
            sql.AppendLine(" idcity = '" & LocationByIdea.CITY.id & "',")
            SQL.AppendLine("WHERE id = " & LocationByIdea.id)

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
            Throw New Exception("Error al modificar el LocationByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el LocationByIdea de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idLocationByIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from LocationByIdea ")
            SQL.AppendLine(" where id = '" & idLocationByIdea & "' ")

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
            Throw New Exception("Error al elimiar el LocationByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de las ubicaciones por idea
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from LocationByIdea ")
            SQL.AppendLine(" where IdIdea = '" & idIdea & "' ")

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
            Throw New Exception("Error al elimiar el LocationByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
