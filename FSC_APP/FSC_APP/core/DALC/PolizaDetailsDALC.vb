Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class PolizaDetailsDALC

    'constantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary>
    ''' Agregar detalles a una poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="PolizaDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaDetails As PolizaDetailsEntity, ByVal idpol As Integer) As Long

        'definiendo objetos
        Dim sql As New StringBuilder

        'TODO: Agregar detalles de poliza
        'Autor: Pedro Cruz

        Try

            'generar el query
            sql.AppendLine("INSERT INTO PolizaDetails (Id_poliza, Concepto, aseguradora) ")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & idpol & "',")
            sql.AppendLine("'" & PolizaDetails.Concepto & "',")
            sql.AppendLine("'" & PolizaDetails.aseguradora & "')")

            'instruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'Ejecutar el query
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'Finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'mostrar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

        Finally
            'liberando recursos
            sql = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Cargar los detalles de una poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPolizaDetails"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPolizaDetails As Integer) As PolizaDetailsEntity

        'definiendo objetos
        Dim sql As New StringBuilder
        Dim objPolizaDetails As New PolizaDetailsEntity
        Dim data As DataTable

        'TODO: Cargar Detalles de poliza
        'Autor: Pedro Cruz

        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            'crear el query
            sql.Append("SELECT * from PolizaDetails ")
            sql.AppendLine("WHERE polizadetails.Id_Poliza = " & idPolizaDetails)

            'ejecutar
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                'cargar datos
                objPolizaDetails.Id = data.Rows(0)("id")
                objPolizaDetails.Id_Poliza = data.Rows(0)("Id_Poliza")
                objPolizaDetails.Concepto = data.Rows(0)("Concepto")
                objPolizaDetails.aseguradora = data.Rows(0)("aseguradora")

            End If

            'retornar el objeto
            Return objPolizaDetails

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el erro de nivel
            Throw New Exception("Error al cargar los detalles de la poliza. " & ex.Message)

        Finally

            'liberar recursos
            sql = Nothing
            data = Nothing
            objPolizaDetails = Nothing

        End Try

    End Function

    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaDetails As PolizaDetailsEntity) As Long

        'definiendo objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Update PolizaDetails SET")
            sql.AppendLine(" Id_poliza = '" & PolizaDetails.Id_Poliza & "',")
            sql.AppendLine(" Concepto = '" & PolizaDetails.aseguradora & "',")
            sql.AppendLine(" aseguradora = '" & PolizaDetails.aseguradora & "',")

            sql.AppendLine(" WHERE id= " & PolizaDetails.Id)

            'ejecutar la instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'Subir el error de nivel
            Throw New Exception("Error al modificar los detalles de la poliza. " & ex.Message)

        Finally

            'liberar recursos
            sql = Nothing

        End Try

    End Function

    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPolizaDetails As Integer) As Long

        'definiendo objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Delete from PolizaDetails ")
            sql.AppendLine("Where Id = '" & idPolizaDetails & "' ")

            'ejecutar la instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As SqlClient.SqlException

            'se verifica si el error es por integridad referencial
            If ex.Number = 547 Then

                'cancelar la transaccion
                CtxSetAbort()

                'publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                'Subir el error de nivel
                Throw New Exception("Ha ocurrido un error al intentar eliminar este registro, debido a una relación existente " & ex.Message)

            Else
                'cancelar la transaccion
                CtxSetAbort()

                'publicar el error
                GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
                ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

                'subir el error de nivel
                Throw New Exception("Error al eliminar los detalles de la poliza. " & ex.Message)

            End If

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattaxaStandardExceptionPolicy")

        Finally

            'liberando recursos
            sql = Nothing

        End Try

    End Function

    Public Function updatepoliza(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaId As Integer, ByVal RequestNumber As Integer) As Long

        'definiendo objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Update ContractRequest SET")
            sql.AppendLine(" PolizaId = '" & PolizaId & "',")

            sql.AppendLine(" WHERE RequestNumber= " & RequestNumber & "'")

            'ejecutar la instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'Subir el error de nivel
            Throw New Exception("Error al modificar los detalles de la poliza. " & ex.Message)

        Finally

            'liberar recursos
            sql = Nothing

        End Try

    End Function

    Public Function updatePolizaId(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal PolizaId As Integer, ByVal RequestNumber As Integer) As Long

        'definiendo objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Update ContractRequest SET")
            sql.AppendLine(" PolizaId = '" & PolizaId & "' ")

            sql.AppendLine(" WHERE RequestNumber= '" & RequestNumber & "'")

            'ejecutar la instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'Subir el error de nivel
            Throw New Exception("Error al actualizar los detalles de la poliza. " & ex.Message)

        Finally

            'liberar recursos
            sql = Nothing

        End Try

    End Function

    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal polizaid As Integer) As List(Of PolizaDetailsEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objPolizaDetails As PolizaDetailsEntity
        Dim PolizaDetailsList As New List(Of PolizaDetailsEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM PolizaDetails ")
            sql.Append(" WHERE Id_Poliza = " & polizaid)

            ' ejecutar la instruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objPolizaDetails = New PolizaDetailsEntity

                ' cargar el valor del campo
                'objPolizaDetails.Id = row("id")
                objPolizaDetails.Concepto = row("concepto")
                'objContractorLegalEntityByContractRequest.id = row("id")
                'objContractorLegalEntityByContractRequest.idcontractrequest = row("idcontractrequest")
                'objContractorLegalEntityByContractRequest.entitynamedescription = row("entitynamedescription")
                'objContractorLegalEntityByContractRequest.nit = row("nit")
                'objContractorLegalEntityByContractRequest.legalrepresentative = row("legalrepresentative")
                'objContractorLegalEntityByContractRequest.contractorname = row("contractorname")
                'objContractorLegalEntityByContractRequest.identificationnumber = row("identificationnumber")

                ' agregar a la lista
                PolizaDetailsList.Add(objPolizaDetails)

            Next

            ' retornar el objeto
            getList = PolizaDetailsList

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
            objPolizaDetails = Nothing
            PolizaDetailsList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

End Class
