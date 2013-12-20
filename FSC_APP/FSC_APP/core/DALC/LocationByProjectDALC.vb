Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class LocationByProjectDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo LocationByProject
    ''' </summary>
    ''' <param name="LocationByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal LocationByProject As LocationByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO LocationByProject(" & _
             "idproject," & _
             "iddepto," & _
             "idcity" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & LocationByProject.idproject & "',")
            sql.AppendLine("'" & LocationByProject.DEPTO.id & "',")
            sql.AppendLine("'" & LocationByProject.CITY.id & "')")

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
       ByVal idLocationByProject As Integer) As LocationByProjectEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objLocationByProject As New LocationByProjectEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM LocationByProject ")
            sql.Append(" WHERE Id = " & idLocationByProject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objLocationByProject.id = data.Rows(0)("id")
                objLocationByProject.idproject = data.Rows(0)("idproject")
                objLocationByProject.DEPTO.id = data.Rows(0)("iddepto")
                objLocationByProject.CITY.id = data.Rows(0)("idcity")

            End If

            ' retornar el objeto
            Return objLocationByProject

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
            objLocationByProject = Nothing

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
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idproject As String = "", _
        Optional ByVal iddepto As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal order As String = "") As List(Of LocationByProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objLocationByProject As LocationByProjectEntity
        Dim LocationByProjectList As New List(Of LocationByProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT LocationByProject.*, Depto.Name AS DeptoName, City.Name AS CityName ")
            sql.Append(" FROM LocationByProject ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.Depto Depto ON  LocationByProject.iddepto = Depto.Id ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.City City ON  LocationByProject.idcity = City.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " ididea like '%" & idproject & "%'")
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
                sql.Append(" ORDER BY " & order)

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objLocationByProject = New LocationByProjectEntity

                ' cargar el valor del campo
                objLocationByProject.id = row("id")
                objLocationByProject.idproject = row("ididea")
                objLocationByProject.DEPTO.id = row("iddepto")
                objLocationByProject.DEPTO.name = row("DeptoName")
                objLocationByProject.CITY.id = row("idcity")
                objLocationByProject.CITY.name = row("CityName")

                ' agregar a la lista
                LocationByProjectList.Add(objLocationByProject)

            Next

            ' retornar el objeto
            getList = LocationByProjectList

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
            objLocationByProject = Nothing
            LocationByProjectList = Nothing
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
    'Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '   ByVal LocationByIdea As LocationByIdeaEntity) As Long

    '    ' definiendo los objtos
    '    Dim sql As New StringBuilder

    '    Try
    '        ' construir la sentencia
    '        sql.AppendLine("Update LocationByIdea SET")
    '        sql.AppendLine(" id = '" & LocationByIdea.id & "',")
    '        sql.AppendLine(" ididea = '" & LocationByIdea.ididea & "',")
    '        sql.AppendLine(" iddepto = '" & LocationByIdea.DEPTO.id & "',")
    '        sql.AppendLine(" idcity = '" & LocationByIdea.CITY.id & "',")
    '        sql.AppendLine("WHERE id = " & LocationByIdea.id)

    '        'Ejecutar la Instruccion
    '        GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al modificar el LocationByIdea. " & ex.Message)

    '    Finally
    '        ' liberando recursos
    '        sql = Nothing

    '    End Try

    'End Function

    ''' <summary>
    ''' Borra el LocationByIdea de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    '   ByVal idLocationByIdea As Integer) As Long

    '    ' definiendo los objtos
    '    Dim SQL As New StringBuilder

    '    Try
    '        ' construir la sentencia
    '        SQL.AppendLine(" Delete from LocationByIdea ")
    '        SQL.AppendLine(" where id = '" & idLocationByIdea & "' ")

    '        'Ejecutar la Instruccion
    '        GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al elimiar el LocationByIdea. " & ex.Message)

    '    Finally
    '        ' liberando recursos
    '        SQL = Nothing

    '    End Try

    'End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de las ubicaciones por idea
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idIdea As Integer) As Long

    '    ' definiendo los objtos
    '    Dim SQL As New StringBuilder

    '    Try
    '        ' construir la sentencia
    '        SQL.AppendLine(" Delete from LocationByIdea ")
    '        SQL.AppendLine(" where IdIdea = '" & idIdea & "' ")

    '        'Ejecutar la Instruccion
    '        GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

    '        ' finalizar la transaccion
    '        CtxSetComplete()

    '    Catch ex As Exception

    '        ' cancelar la transaccion
    '        CtxSetAbort()

    '        ' publicar el error
    '        GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
    '        ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

    '        ' subir el error de nivel
    '        Throw New Exception("Error al elimiar el LocationByIdea. " & ex.Message)

    '    Finally
    '        ' liberando recursos
    '        SQL = Nothing

    '    End Try

    'End Function

End Class
