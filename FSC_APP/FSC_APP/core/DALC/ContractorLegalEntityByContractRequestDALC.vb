Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractorLegalEntityByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractorLegalEntityByContractRequest
    ''' </summary>
    ''' <param name="ContractorLegalEntityByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractorLegalEntityByContractRequest As ContractorLegalEntityByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractorLegalEntityByContractRequest(" & _
             "idcontractrequest," & _
             "entitynamedescription," & _
             "nit," & _
             "legalrepresentative," & _
             "contractorname," & _
             "identificationnumber" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.entitynamedescription & "',")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.nit & "',")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.legalrepresentative & "',")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.contractorname & "',")
            sql.AppendLine("'" & ContractorLegalEntityByContractRequest.identificationnumber & "')")

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
            Throw New Exception("Error al insertar la lista de contratistas personas jurídicas de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractorLegalEntityByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractorLegalEntityByContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorLegalEntityByContractRequest As Integer) As ContractorLegalEntityByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractorLegalEntityByContractRequest As New ContractorLegalEntityByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractorLegalEntityByContractRequest ")
            sql.Append(" WHERE Id = " & idContractorLegalEntityByContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractorLegalEntityByContractRequest.id = data.Rows(0)("id")
                objContractorLegalEntityByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
                objContractorLegalEntityByContractRequest.entitynamedescription = data.Rows(0)("entitynamedescription")
                objContractorLegalEntityByContractRequest.nit = data.Rows(0)("nit")
                objContractorLegalEntityByContractRequest.legalrepresentative = data.Rows(0)("legalrepresentative")
                objContractorLegalEntityByContractRequest.contractorname = data.Rows(0)("contractorname")
                objContractorLegalEntityByContractRequest.identificationnumber = data.Rows(0)("identificationnumber")

            End If

            ' retornar el objeto
            Return objContractorLegalEntityByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el contratista persona jurídica de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractorLegalEntityByContractRequest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="entitynamedescription"></param>
    ''' <param name="nit"></param>
    ''' <param name="legalrepresentative"></param>
    ''' <param name="contractorname"></param>
    ''' <param name="identificationnumber"></param>
    ''' <returns>un objeto de tipo List(Of ContractorLegalEntityByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal entitynamedescription As String = "", _
        Optional ByVal nit As String = "", _
        Optional ByVal legalrepresentative As String = "", _
        Optional ByVal contractorname As String = "", _
        Optional ByVal identificationnumber As String = "", _
        Optional ByVal order As String = "") As List(Of ContractorLegalEntityByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractorLegalEntityByContractRequest As ContractorLegalEntityByContractRequestEntity
        Dim ContractorLegalEntityByContractRequestList As New List(Of ContractorLegalEntityByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractorLegalEntityByContractRequest ")

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
            If Not entitynamedescription.Equals("") Then

                sql.Append(where & " entitynamedescription like '%" & entitynamedescription & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not nit.Equals("") Then

                sql.Append(where & " nit like '%" & nit & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not legalrepresentative.Equals("") Then

                sql.Append(where & " legalrepresentative like '%" & legalrepresentative & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractorname.Equals("") Then

                sql.Append(where & " contractorname like '%" & contractorname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not identificationnumber.Equals("") Then

                sql.Append(where & " identificationnumber like '%" & identificationnumber & "%'")
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
                objContractorLegalEntityByContractRequest = New ContractorLegalEntityByContractRequestEntity

                ' cargar el valor del campo
                objContractorLegalEntityByContractRequest.id = row("id")
                objContractorLegalEntityByContractRequest.idcontractrequest = row("idcontractrequest")
                objContractorLegalEntityByContractRequest.entitynamedescription = row("entitynamedescription")
                objContractorLegalEntityByContractRequest.nit = row("nit")
                objContractorLegalEntityByContractRequest.legalrepresentative = row("legalrepresentative")
                objContractorLegalEntityByContractRequest.contractorname = row("contractorname")
                objContractorLegalEntityByContractRequest.identificationnumber = row("identificationnumber")

                ' agregar a la lista
                ContractorLegalEntityByContractRequestList.Add(objContractorLegalEntityByContractRequest)

            Next

            ' retornar el objeto
            getList = ContractorLegalEntityByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de contratistas personas jurídicas de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractorLegalEntityByContractRequest = Nothing
            ContractorLegalEntityByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractorLegalEntityByContractRequest
    ''' </summary>
    ''' <param name="ContractorLegalEntityByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractorLegalEntityByContractRequest As ContractorLegalEntityByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractorLegalEntityByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & ContractorLegalEntityByContractRequest.idcontractrequest & "',")
            sql.AppendLine(" entitynamedescription = '" & ContractorLegalEntityByContractRequest.entitynamedescription & "',")
            sql.AppendLine(" nit = '" & ContractorLegalEntityByContractRequest.nit & "',")
            sql.AppendLine(" legalrepresentative = '" & ContractorLegalEntityByContractRequest.legalrepresentative & "',")
            sql.AppendLine(" contractorname = '" & ContractorLegalEntityByContractRequest.contractorname & "',")
            sql.AppendLine(" identificationnumber = '" & ContractorLegalEntityByContractRequest.identificationnumber & "'")
            sql.AppendLine("WHERE id = " & ContractorLegalEntityByContractRequest.id)

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
            Throw New Exception("Error al modificar la lista de contratistas personas jurídicas de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractorLegalEntityByContractRequest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractorLegalEntityByContractRequest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractorLegalEntityByContractRequest ")
            SQL.AppendLine(" where id = '" & idContractorLegalEntityByContractRequest & "' ")

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
            Throw New Exception("Error al elimiar la lista de contratistas personas jurídicas de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra todos los registros almacenados de los contratistas personas jurídicas de una solicitud determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="idRequestNumber">identificador de la soliciutd de contrato</param>
    ''' <remarks></remarks>
    Public Sub deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idRequestNumber As Integer)

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractorLegalEntityByContractRequest ")
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
            Throw New Exception("Error al eliminar la lista de contratistas personas jurídicas de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Sub

End Class
