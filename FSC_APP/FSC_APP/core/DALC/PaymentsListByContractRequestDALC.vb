Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class PaymentsListByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo PaymentsListByContractRequest
    ''' </summary>
    ''' <param name="PaymentsListByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO PaymentsListByContractRequest(" & _
             "idcontractrequest," & _
             "value," & _
             "percentage," & _
             "date" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & PaymentsListByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & PaymentsListByContractRequest.value.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & PaymentsListByContractRequest.percentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & PaymentsListByContractRequest.datePaymentsList.ToString("yyyyMMdd") & "')")

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
            Throw New Exception("Error al insertar la lista de pagos de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un PaymentsListByContractRequest por el Id
    ''' </summary>
    ''' <param name="idPaymentsListByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idPaymentsListByContractRequest As Integer) As PaymentsListByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objPaymentsListByContractRequest As New PaymentsListByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM PaymentsListByContractRequest ")
            sql.Append(" WHERE Id = " & idPaymentsListByContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objPaymentsListByContractRequest.id = data.Rows(0)("id")
				objPaymentsListByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
				objPaymentsListByContractRequest.value = data.Rows(0)("value")
				objPaymentsListByContractRequest.percentage = data.Rows(0)("percentage")
                objPaymentsListByContractRequest.datePaymentsList = data.Rows(0)("date")

            End If

            ' retornar el objeto
            Return objPaymentsListByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objPaymentsListByContractRequest = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="value"></param>
    ''' <param name="percentage"></param>
    ''' <param name="datePaymentsList"></param>
    ''' <returns>un objeto de tipo List(Of PaymentsListByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal percentage As String = "", _
        Optional ByVal datePaymentsList As String = "", _
        Optional ByVal order As String = "") As List(Of PaymentsListByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objPaymentsListByContractRequest As PaymentsListByContractRequestEntity
        Dim PaymentsListByContractRequestList As New List(Of PaymentsListByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM PaymentsListByContractRequest ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcontractrequest.Equals("") Then

                sql.Append(where & " idcontractrequest = '" & idcontractrequest & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not value.Equals("") Then

                sql.Append(where & " value like '%" & value & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not percentage.Equals("") Then

                sql.Append(where & " percentage like '%" & percentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not datePaymentsList.Equals("") Then

                sql.Append(where & " date like '%" & datePaymentsList & "%'")
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
                objPaymentsListByContractRequest = New PaymentsListByContractRequestEntity

                ' cargar el valor del campo
                objPaymentsListByContractRequest.id = row("id")
                objPaymentsListByContractRequest.idcontractrequest = row("idcontractrequest")
                objPaymentsListByContractRequest.value = row("value")
                objPaymentsListByContractRequest.percentage = row("percentage")
                objPaymentsListByContractRequest.datePaymentsList = row("date")
                objPaymentsListByContractRequest.finalPaymentDate = IIf(IsDBNull(row("finalPaymentDate")), Nothing, row("finalPaymentDate"))
                If Not IsDBNull(row("finalPaymentValue")) Then
                    objPaymentsListByContractRequest.finalPaymentValue = row("finalPaymentValue")
                End If

                ' agregar a la lista
                PaymentsListByContractRequestList.Add(objPaymentsListByContractRequest)

            Next

            ' retornar el objeto
            getList = PaymentsListByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de pagos de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objPaymentsListByContractRequest = Nothing
            PaymentsListByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo PaymentsListByContractRequest
    ''' </summary>
    ''' <param name="PaymentsListByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update PaymentsListByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & PaymentsListByContractRequest.idcontractrequest & "',")
            sql.AppendLine(" value = '" & PaymentsListByContractRequest.value.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" percentage = '" & PaymentsListByContractRequest.percentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" date = '" & PaymentsListByContractRequest.datePaymentsList.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" WHERE id = " & PaymentsListByContractRequest.id)

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
            Throw New Exception("Error al modificar la lista de pagos de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el PaymentsListByContractRequest de una forma
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPaymentsListByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idPaymentsListByContractRequest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from PaymentsListByContractRequest ")
            SQL.AppendLine(" where id = '" & idPaymentsListByContractRequest & "' ")

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
            Throw New Exception("Error al eliminar la lista de pagos de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de los pagos de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from PaymentsListByContractRequest ")
            SQL.AppendLine(" where IdContractRequest = '" & idRequestNumber & "' ")

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
            Throw New Exception("Error al eliminar la lista de pagos de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Sub

    ''' <summary>
    ''' Metodo que permite actualziar lal ista de pagos desde
    ''' la ejecución del contrato
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario</param>
    ''' <param name="PaymentsListByContractRequest">objeto de tipo PaymentsListByContractRequestEntity</param>
    ''' <remarks></remarks>
    Public Sub updateByContractExecution(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal PaymentsListByContractRequest As PaymentsListByContractRequestEntity)

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update PaymentsListByContractRequest SET")
            If (PaymentsListByContractRequest.finalPaymentDate > CDate("1900/01/01")) Then
                sql.AppendLine(" FinalPaymentDate = '" & PaymentsListByContractRequest.finalPaymentDate.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine(" FinalPaymentDate = NULL, ")
            End If        
            sql.AppendLine(" FinalPaymentValue = '" & PaymentsListByContractRequest.finalPaymentValue.ToString().Replace(",", ".") & "'")

            sql.AppendLine(" WHERE id = " & PaymentsListByContractRequest.id)

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
            Throw New Exception("Error al modificar la lista de pagos de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Sub

End Class
