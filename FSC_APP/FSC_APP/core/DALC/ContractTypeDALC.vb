Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractTypeDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractType
    ''' </summary>
    ''' <param name="ContractType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractType As ContractTypeEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractType(" & _
             "name," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ContractType.name & "',")
            sql.AppendLine("'" & ContractType.enabled & "',")
            sql.AppendLine("'" & ContractType.iduser & "',")
            sql.AppendLine("'" & ContractType.createdate.ToString("yyymmdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el tipo de contrato." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractType por el Id
    ''' </summary>
    ''' <param name="idContractType"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractType As Integer) As ContractTypeEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractType As New ContractTypeEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractType ")
            sql.Append(" WHERE Id = " & idContractType)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractType.id = data.Rows(0)("id")
                objContractType.name = data.Rows(0)("name")
                objContractType.enabled = data.Rows(0)("enabled")
                objContractType.iduser = data.Rows(0)("iduser")
                objContractType.createdate = data.Rows(0)("createdate")

            End If

            ' retornar el objeto
            Return objContractType

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un tipo de contrato.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractType = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="name"></param>
    ''' <param name="enabled"></param>
    ''' <param name="iduser"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ContractTypeEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ContractTypeEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractType As ContractTypeEntity
        Dim ContractTypeList As New List(Of ContractTypeEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractType ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " enabled like '%" & enabled & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " createdate like '%" & createdate & "%'")
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
                objContractType = New ContractTypeEntity

                ' cargar el valor del campo
                objContractType.id = row("id")
                objContractType.name = row("name")
                objContractType.enabled = row("enabled")
                objContractType.iduser = row("iduser")
                objContractType.createdate = row("createdate")

                ' agregar a la lista
                ContractTypeList.Add(objContractType)

            Next

            ' retornar el objeto
            getList = ContractTypeList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de tipos de contrato.")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractType = Nothing
            ContractTypeList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractType
    ''' </summary>
    ''' <param name="ContractType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractType As ContractTypeEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractType SET")
            sql.AppendLine(" name = '" & ContractType.name & "',")
            sql.AppendLine(" enabled = '" & ContractType.enabled & "'")
            sql.AppendLine(" WHERE id = " & ContractType.id)

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
            Throw New Exception("Error al modificar el tipo de contrato." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractType de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractType As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractType ")
            SQL.AppendLine(" where id = '" & idContractType & "' ")

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
            Throw New Exception("Error al elimiar el tipo de contrato." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
