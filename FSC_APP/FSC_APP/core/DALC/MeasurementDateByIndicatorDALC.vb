Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class MeasurementDateByIndicatorDALC

    ' contantes
    Const MODULENAME As String = "MeasurementDateByIndicatorDALC"

    ''' <summary> 
    ''' Registar un nuevo MeasurementDateByIndicator
    ''' </summary>
    ''' <param name="MeasurementDateByIndicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal MeasurementDateByIndicator As MeasurementDateByIndicatorEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO MeasurementDateByIndicator(" & _
             "idindicator," & _
             "measurementdate," & _
             "measure," & _
             "measuretype" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & MeasurementDateByIndicator.idindicator & "',")
            sql.AppendLine("'" & MeasurementDateByIndicator.measurementdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine("'" & MeasurementDateByIndicator.measure & "',")
            sql.AppendLine("'" & MeasurementDateByIndicator.measuretype & "')")

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
            Throw New Exception("Error al insertar el MeasurementDateByIndicator. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un MeasurementDateByIndicator por el Id
    ''' </summary>
    ''' <param name="idMeasurementDateByIndicator"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idMeasurementDateByIndicator As Integer) As MeasurementDateByIndicatorEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objMeasurementDateByIndicator As New MeasurementDateByIndicatorEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM MeasurementDateByIndicator ")
            sql.Append(" WHERE Id = " & idMeasurementDateByIndicator)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objMeasurementDateByIndicator.id = data.Rows(0)("id")
				objMeasurementDateByIndicator.idindicator = data.Rows(0)("idindicator")
				objMeasurementDateByIndicator.measurementdate = data.Rows(0)("measurementdate")
                objMeasurementDateByIndicator.measure = data.Rows(0)("measure")
                objMeasurementDateByIndicator.measuretype = data.Rows(0)("measuretype")

            End If

            ' retornar el objeto
            Return objMeasurementDateByIndicator

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objMeasurementDateByIndicator = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idindicator"></param>
    ''' <param name="measurementdate"></param>
    ''' <returns>un objeto de tipo List(Of MeasurementDateByIndicatorEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idindicator As String = "", _
								Optional ByVal measurementdate As String = "", _
								Optional order as string = "") As List(Of MeasurementDateByIndicatorEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objMeasurementDateByIndicator As MeasurementDateByIndicatorEntity
        Dim MeasurementDateByIndicatorList As New List(Of MeasurementDateByIndicatorEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            SQL.Append(" SELECT * ")
            SQL.Append(" FROM MeasurementDateByIndicator ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idindicator.Equals("") Then

                sql.Append(where & " idindicator = '" & idindicator & "'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not measurementdate.Equals("") Then

                sql.Append(where & " measurementdate = '" & measurementdate & "'")
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
                objMeasurementDateByIndicator = New MeasurementDateByIndicatorEntity

				' cargar el valor del campo
				objMeasurementDateByIndicator.id = row("id")
                objMeasurementDateByIndicator.idindicator = row("idindicator")
				objMeasurementDateByIndicator.measurementdate = row("measurementdate")
                objMeasurementDateByIndicator.measure = IIf(Not IsDBNull(row("measure")), row("measure"), "")
                objMeasurementDateByIndicator.measuretype = IIf(Not IsDBNull(row("measuretype")), row("measuretype"), "")
                ' agregar a la lista
                MeasurementDateByIndicatorList.Add(objMeasurementDateByIndicator)

            Next

            ' retornar el objeto
            getList = MeasurementDateByIndicatorList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de MeasurementDateByIndicator. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objMeasurementDateByIndicator = Nothing
            MeasurementDateByIndicatorList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo MeasurementDateByIndicator
    ''' </summary>
    ''' <param name="MeasurementDateByIndicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal MeasurementDateByIndicator As MeasurementDateByIndicatorEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update MeasurementDateByIndicator SET")
            SQL.AppendLine(" idindicator = '" & MeasurementDateByIndicator.idindicator & "',")           
            sql.AppendLine(" measurementdate = '" & MeasurementDateByIndicator.measurementdate.ToString("yyyy/MM/dd HH:mm:ss") & "',")
            sql.AppendLine(" measure = '" & MeasurementDateByIndicator.measure & "',")
            sql.AppendLine(" measuretype = '" & MeasurementDateByIndicator.measuretype & "'")
            sql.AppendLine(" WHERE id = " & MeasurementDateByIndicator.id)

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
            Throw New Exception("Error al modificar el MeasurementDateByIndicator. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el MeasurementDateByIndicator de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMeasurementDateByIndicator As Integer, Optional ByVal idIndicator As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            If (idIndicator = 0) Then
                SQL.AppendLine(" Delete from MeasurementDateByIndicator ")
                SQL.AppendLine(" where id = '" & idMeasurementDateByIndicator & "' ")
            Else
                SQL.AppendLine(" Delete from MeasurementDateByIndicator ")
                SQL.AppendLine(" where idIndicator = '" & idIndicator & "' ")
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
            Throw New Exception("Error al elimiar el MeasurementDateByIndicator. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
