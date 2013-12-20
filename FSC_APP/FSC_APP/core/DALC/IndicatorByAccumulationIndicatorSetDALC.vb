Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class IndicatorByAccumulationIndicatorSetDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo IndicatorByAccumulationIndicatorSet
    ''' </summary>
    ''' <param name="IndicatorByAccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal IndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO IndicatorByAccumulationIndicatorSet(" & _
            "idaccumulationindicatorset," & _
             "idindicator" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & IndicatorByAccumulationIndicatorSet.idaccumulationindicatorset & "',")
            sql.AppendLine("'" & IndicatorByAccumulationIndicatorSet.idindicator & "')")

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
            Throw New Exception("Error al insertar el IndicatorByAccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un IndicatorByAccumulationIndicatorSet por el Id
    ''' </summary>
    ''' <param name="idIndicatorByAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idIndicatorByAccumulationIndicatorSet As Integer) As IndicatorByAccumulationIndicatorSetEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objIndicatorByAccumulationIndicatorSet As New IndicatorByAccumulationIndicatorSetEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT IndicatorByAccumulationIndicatorSet.*, Indicator.Code as indicatorcode ")
            sql.Append(" FROM IndicatorByAccumulationIndicatorSet INNER JOIN ")
            sql.Append(" 	Indicator ON IndicatorByAccumulationIndicatorSet.IdIndicator = Indicator.Id ")
            sql.Append(" WHERE IndicatorByAccumulationIndicatorSet.Id = " & idIndicatorByAccumulationIndicatorSet)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objIndicatorByAccumulationIndicatorSet.id = data.Rows(0)("id")
				objIndicatorByAccumulationIndicatorSet.idaccumulationindicatorset = data.Rows(0)("idaccumulationindicatorset")
                objIndicatorByAccumulationIndicatorSet.idindicator = data.Rows(0)("idindicator")
                objIndicatorByAccumulationIndicatorSet.INDICATORCODE = data.Rows(0)("indicatorcode")

            End If

            ' retornar el objeto
            Return objIndicatorByAccumulationIndicatorSet

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objIndicatorByAccumulationIndicatorSet = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idaccumulationindicatorset"></param>
    ''' <param name="idindicator"></param>
    ''' <param name="indicatorcode"></param>
    ''' <returns>un objeto de tipo List(Of IndicatorByAccumulationIndicatorSetEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idaccumulationindicatorset As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal indicatorcode As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorByAccumulationIndicatorSetEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity
        Dim IndicatorByAccumulationIndicatorSetList As New List(Of IndicatorByAccumulationIndicatorSetEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT IndicatorByAccumulationIndicatorSet.*, Indicator.Code as indicatorcode ")
            sql.Append(" FROM IndicatorByAccumulationIndicatorSet INNER JOIN ")
            sql.Append(" 	Indicator ON IndicatorByAccumulationIndicatorSet.IdIndicator = Indicator.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idaccumulationindicatorset.Equals("") Then

                sql.Append(where & " idaccumulationindicatorset = '" & idaccumulationindicatorset & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idindicator.Equals("") Then

                sql.Append(where & " idindicator = '" & idindicator & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not indicatorcode.Equals("") Then

                sql.Append(where & " indicatorcode like '%" & indicatorcode & "%'")
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
                objIndicatorByAccumulationIndicatorSet = New IndicatorByAccumulationIndicatorSetEntity

                ' cargar el valor del campo
                objIndicatorByAccumulationIndicatorSet.id = row("id")
                objIndicatorByAccumulationIndicatorSet.idaccumulationindicatorset = row("idaccumulationindicatorset")
                objIndicatorByAccumulationIndicatorSet.idindicator = row("idindicator")
                objIndicatorByAccumulationIndicatorSet.INDICATORCODE = row("indicatorcode")

                ' agregar a la lista
                IndicatorByAccumulationIndicatorSetList.Add(objIndicatorByAccumulationIndicatorSet)

            Next

            ' retornar el objeto
            getList = IndicatorByAccumulationIndicatorSetList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de IndicatorByAccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objIndicatorByAccumulationIndicatorSet = Nothing
            IndicatorByAccumulationIndicatorSetList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo IndicatorByAccumulationIndicatorSet
    ''' </summary>
    ''' <param name="IndicatorByAccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal IndicatorByAccumulationIndicatorSet As IndicatorByAccumulationIndicatorSetEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update IndicatorByAccumulationIndicatorSet SET")
            SQL.AppendLine(" idaccumulationindicatorset = '" & IndicatorByAccumulationIndicatorSet.idaccumulationindicatorset & "',")           
            sql.AppendLine(" idindicator = '" & IndicatorByAccumulationIndicatorSet.idindicator & "'")
            SQL.AppendLine("WHERE id = " & IndicatorByAccumulationIndicatorSet.id)

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
            Throw New Exception("Error al modificar el IndicatorByAccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el IndicatorByAccumulationIndicatorSet de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorByAccumulationIndicatorSet As Integer, _
                            Optional ByVal idindicator As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from IndicatorByAccumulationIndicatorSet ")
            If idindicator = 0 Then
                SQL.AppendLine(" where id = '" & idIndicatorByAccumulationIndicatorSet & "' ")
            Else
                SQL.AppendLine(" where idindicator = '" & idindicator & "' ")
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
            Throw New Exception("Error al elimiar el IndicatorByAccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
