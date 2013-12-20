Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractorNaturalPersonByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractorNaturalPersonByContractRequest
    ''' </summary>
    ''' <param name="ContractorNaturalPersonByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractorNaturalPersonByContractRequest As ContractorNaturalPersonByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractorNaturalPersonByContractRequest(" & _
             "idcontractrequest," & _
             "nit," & _
             "contractorname" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ContractorNaturalPersonByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & ContractorNaturalPersonByContractRequest.nit & "',")
            sql.AppendLine("'" & ContractorNaturalPersonByContractRequest.contractorname & "')")

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
            Throw New Exception("Error al insertar la lista de contratistas personas naturales de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractorNaturalPersonByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractorNaturalPersonByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorNaturalPersonByContractRequest As Integer) As ContractorNaturalPersonByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractorNaturalPersonByContractRequest As New ContractorNaturalPersonByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractorNaturalPersonByContractRequest ")
            sql.Append(" WHERE Id = " & idContractorNaturalPersonByContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractorNaturalPersonByContractRequest.id = data.Rows(0)("id")
                objContractorNaturalPersonByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
                objContractorNaturalPersonByContractRequest.nit = data.Rows(0)("nit")
                objContractorNaturalPersonByContractRequest.contractorname = data.Rows(0)("contractorname")

            End If

            ' retornar el objeto
            Return objContractorNaturalPersonByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Contratista persona natural. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractorNaturalPersonByContractRequest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="nit"></param>
    ''' <param name="contractorname"></param>
    ''' <returns>un objeto de tipo List(Of ContractorNaturalPersonByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal nit As String = "", _
        Optional ByVal contractorname As String = "", _
        Optional ByVal order As String = "") As List(Of ContractorNaturalPersonByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractorNaturalPersonByContractRequest As ContractorNaturalPersonByContractRequestEntity
        Dim ContractorNaturalPersonByContractRequestList As New List(Of ContractorNaturalPersonByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractorNaturalPersonByContractRequest ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcontractrequest.Equals("") Then

                sql.Append(where & " idcontractrequest = '" & idcontractrequest & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not nit.Equals("") Then

                sql.Append(where & " nit like '%" & nit & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractorname.Equals("") Then

                sql.Append(where & " contractorname like '%" & contractorname & "%'")
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
                objContractorNaturalPersonByContractRequest = New ContractorNaturalPersonByContractRequestEntity

                ' cargar el valor del campo
                objContractorNaturalPersonByContractRequest.id = row("id")
                objContractorNaturalPersonByContractRequest.idcontractrequest = row("idcontractrequest")
                objContractorNaturalPersonByContractRequest.nit = row("nit")
                objContractorNaturalPersonByContractRequest.contractorname = row("contractorname")

                ' agregar a la lista
                ContractorNaturalPersonByContractRequestList.Add(objContractorNaturalPersonByContractRequest)

            Next

            ' retornar el objeto
            getList = ContractorNaturalPersonByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de contratistas personas naturales de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractorNaturalPersonByContractRequest = Nothing
            ContractorNaturalPersonByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractorNaturalPersonByContractRequest
    ''' </summary>
    ''' <param name="ContractorNaturalPersonByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractorNaturalPersonByContractRequest As ContractorNaturalPersonByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractorNaturalPersonByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & ContractorNaturalPersonByContractRequest.idcontractrequest & "',")
            sql.AppendLine(" nit = '" & ContractorNaturalPersonByContractRequest.nit & "',")
            sql.AppendLine(" contractorname = '" & ContractorNaturalPersonByContractRequest.contractorname & "'")
            sql.AppendLine("WHERE id = " & ContractorNaturalPersonByContractRequest.id)

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
            Throw New Exception("Error al modificar la lista de contratistas personas naturales de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractorNaturalPersonByContractRequest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorNaturalPersonByContractRequest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractorNaturalPersonByContractRequest ")
            SQL.AppendLine(" where id = '" & idContractorNaturalPersonByContractRequest & "' ")

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
            Throw New Exception("Error al elimiar la lista de contratistas personas naturales de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de los contratistas personas naturales de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractorNaturalPersonByContractRequest ")
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
            Throw New Exception("Error al eliminar la lista de contratistas personas naturales de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Sub


End Class
