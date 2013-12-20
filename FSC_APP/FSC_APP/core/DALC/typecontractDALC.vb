'TODO:TABLA DALC TIPO DE CONTRATACION NUEVA PARA COMBO BOX DESDE BASE DE DATOS
'AUTOR:GERMAN RODRIGUEZ 20/07/2013

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class typecontractDALC

    ' contantes
    Const MODULENAME As String = "typecontractDALC"

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="CONTRACT"></param>
    ''' <returns>un objeto de tipo List(Of typecontractEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal id As String = "", _
       Optional ByVal CONTRACT As String = "", _
       Optional ByVal order As String = "") As List(Of typecontractEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objtypecontract As typecontractEntity
        Dim typecontractEntityList As New List(Of typecontractEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia

            sql.Append(" SELECT TYPECONTRACT.ID, TYPECONTRACT.CONTRACT FROM TYPECONTRACT ")
            sql.Append(" order by (ID) ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objtypecontract = New typecontractEntity

                ' cargar el valor del campo
                objtypecontract.id = row("ID")
                objtypecontract.contract = row("CONTRACT")

                ' agregar a la lista
                typecontractEntityList.Add(objtypecontract)

            Next

            ' retornar el objeto
            getList = typecontractEntityList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de typecontract. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objtypecontract = Nothing
            typecontractEntityList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function


    ''' <summary> 
    ''' Registar un nuevo typecontract
    ''' </summary>
    ''' <param name="typecontract"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal typecontract As typecontractEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO typecontract(" & _
             "contract" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & typecontract.contract & "')")
           
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
            Throw New Exception("Error al insertar el typecontract. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el typecontract de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idTtypecontract As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from typecontract")
            SQL.AppendLine(" where id = '" & idTtypecontract & "' ")

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
            Throw New Exception("Error al elimiar el idTtypecontract. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function



End Class
