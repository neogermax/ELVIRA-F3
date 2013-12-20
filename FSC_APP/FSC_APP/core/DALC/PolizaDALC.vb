Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class PolizaDALC

    'constantes
    Const MODULENAME As String = "ProducerDALC"
    ''' <summary>
    ''' Agregar poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="Poliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Poliza As PolizaEntity) As Long

        'definiendo objetos
        Dim sql As New StringBuilder
        Dim dtdata As DataTable

        'TODO: Agregar poliza
        'Autor: Pedro Cruz

        Try
            'generar el query
            sql.AppendLine("INSERT INTO Poliza (numero_poliza, aseguradora, contrato_id")

            'validar fecha
            If Poliza.fecha_exp <> "#12:00:00 AM#" Then
                sql.AppendLine(", fecha_exp) ")
            Else
                sql.AppendLine(") ")
            End If


            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Poliza.numero_poliza & "',")
            sql.AppendLine("'" & Poliza.aseguradora & "',")
            sql.AppendLine("'" & Poliza.contrato_id & "' ")

            'validar fecha
            If Poliza.fecha_exp <> "#12:00:00 AM#" Then
                sql.AppendLine(", convert(datetime, '" & Replace(Poliza.fecha_exp, "/", "-") & "',103)) ")
            Else
                sql.AppendLine(")")
            End If

            'sql.AppendLine("convert(datetime, '" & Replace(Poliza.fecha_ven, "/", "-") & "',103)) ")

            'instruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'Ejecutar el query
            dtdata = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            ' finalizar la transaccion
            CtxSetComplete()

            ' retornar
            Return num

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'mostrar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'sube el error de nivel
            Throw New Exception("Error al insertar la poliza. " & ex.Message)

        Finally
            'liberando recursos
            sql = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Cargar una poliza de la tabla
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="idPoliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPoliza As Integer) As PolizaEntity

        'definiendo objetos
        Dim sql As New StringBuilder
        Dim objPoliza As New PolizaEntity
        Dim data As DataTable

        'TODO: Cargar poliza
        'Autor: Pedro Cruz

        'obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            'crear el query
            sql.Append("SELECT * from poliza ")
            sql.Append("WHERE poliza.ID = " & idPoliza)

            'ejecutar
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                'cargar datos
                objPoliza.id = data.Rows(0)("id")
                objPoliza.numero_poliza = data.Rows(0)("numero_poliza")
                ' captura aseguradora
                If IsDBNull(data.Rows(0)("aseguradora")) = False Then
                    objPoliza.aseguradora = data.Rows(0)("aseguradora")
                Else
                    objPoliza.aseguradora = Nothing
                End If
                objPoliza.contrato_id = data.Rows(0)("contrato_id")
                objPoliza.fecha_exp = data.Rows(0)("fecha_exp")
                'objPoliza.fecha_ven = data.Rows(0)("fecha_ven")

            End If

            'retornar el objeto
            Return objPoliza

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al cargar una poliza. " & ex.Message)

        Finally

            'liberando recursos
            sql = Nothing
            data = Nothing
            objPoliza = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Actualizar una poliza
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="Poliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Poliza As PolizaEntity) As Long

        'definiendo los objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Update Poliza SET")
            sql.AppendLine(" numero_poliza = '" & Poliza.numero_poliza & "',")
            sql.AppendLine(" aseguradora = '" & Poliza.aseguradora & "',")
            sql.AppendLine(" contrato_id = '" & Poliza.contrato_id & "',")

            sql.AppendLine(" WHERE id = " & Poliza.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al modificar la poliza. " & ex.Message)

        Finally

            'liberar recursos
            sql = Nothing

        End Try

    End Function

    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal idPoliza As Integer) As Long

        'definiendo los objetos
        Dim sql As New StringBuilder

        Try
            'construir la sentencia
            sql.AppendLine("Delete from poliza ")
            sql.AppendLine("Where id = '" & idPoliza & "' ")

            'ejecutar la instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As SqlClient.SqlException

            'Se verifica si el error es por integridad referencial
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
                Throw New Exception("Error al eliminar la poliza. " & ex.Message)

            End If

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'Subir el error de nivel
            Throw New Exception("Error al eliminar la poliza. " & ex.Message)

        Finally

            'liberando recursos
            sql = Nothing

        End Try

    End Function

End Class
