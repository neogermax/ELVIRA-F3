Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class CityDALC

    ' contantes
    Const MODULENAME As String = "CityDALC"

    ''' <summary>
    ''' Cargar un City por el Id
    ''' </summary>
    ''' <param name="idCity"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idCity As Integer) As CityEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objCity As New CityEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.City City ")
            sql.Append(" WHERE Id = " & idCity)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objCity.id = data.Rows(0)("id")
				objCity.code = data.Rows(0)("code")
				objCity.name = data.Rows(0)("name")
				objCity.iddepto = data.Rows(0)("iddepto")
				objCity.enabled = data.Rows(0)("enabled")

            End If

            ' retornar el objeto
            Return objCity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un City. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objCity = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="iddepto"></param>
    ''' <param name="enabled"></param>
    ''' <returns>un objeto de tipo List(Of CityEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal code As String = "", _
								Optional ByVal name As String = "", _
								Optional ByVal iddepto As String = "", _
								Optional ByVal enabled As String = "", _
								Optional order as string = "") As List(Of CityEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objCity As CityEntity
        Dim CityList As New List(Of CityEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            SQL.Append(" SELECT * ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.City City")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                SQL.Append(where & " code like '%" & code & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                SQL.Append(where & " name like '%" & name & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not iddepto.Equals("") Then

                sql.Append(where & " iddepto = '" & iddepto & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                SQL.Append(where & " enabled like '%" & enabled & "%'")
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
                objCity = New CityEntity

				' cargar el valor del campo
				objCity.id = row("id")
				objCity.code = row("code")
				objCity.name = row("name")
				objCity.iddepto = row("iddepto")
				objCity.enabled = row("enabled")

                ' agregar a la lista
                CityList.Add(objCity)

            Next

            ' retornar el objeto
            getList = CityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de City. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objCity = Nothing
            CityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
           

End Class
