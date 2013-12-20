Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class DeptoDALC

    ' contantes
    Const MODULENAME As String = "DeptoDALC"

    
   
    ''' <summary>
    ''' Cargar un Depto por el Id
    ''' </summary>
    ''' <param name="idDepto"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idDepto As Integer) As DeptoEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objDepto As New DeptoEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.Depto Depto ")
            sql.Append(" WHERE Id = " & idDepto)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objDepto.id = data.Rows(0)("id")
				objDepto.code = data.Rows(0)("code")
				objDepto.name = data.Rows(0)("name")
				objDepto.idcountry = data.Rows(0)("idcountry")
				objDepto.enabled = data.Rows(0)("enabled")

            End If

            ' retornar el objeto
            Return objDepto

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Depto. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objDepto = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="idcountry"></param>
    ''' <param name="enabled"></param>
    ''' <returns>un objeto de tipo List(Of DeptoEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal code As String = "", _
								Optional ByVal name As String = "", _
								Optional ByVal idcountry As String = "", _
								Optional ByVal enabled As String = "", _
								Optional order as string = "") As List(Of DeptoEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objDepto As DeptoEntity
        Dim DeptoList As New List(Of DeptoEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            SQL.Append(" SELECT * ")
            sql.Append(" FROM " & dbSecurityName & ".dbo.Depto Depto")

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
            If Not idcountry.Equals("") Then

                sql.Append(where & " idcountry = '" & idcountry & "'")
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
                objDepto = New DeptoEntity

				' cargar el valor del campo
				objDepto.id = row("id")
				objDepto.code = row("code")
				objDepto.name = row("name")
				objDepto.idcountry = row("idcountry")
				objDepto.enabled = row("enabled")

                ' agregar a la lista
                DeptoList.Add(objDepto)

            Next

            ' retornar el objeto
            getList = DeptoList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Depto. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objDepto = Nothing
            DeptoList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

End Class
