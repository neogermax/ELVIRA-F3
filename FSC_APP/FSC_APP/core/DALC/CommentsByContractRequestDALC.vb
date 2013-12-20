Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class CommentsByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo CommentsByContractRequest
    ''' </summary>
    ''' <param name="CommentsByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal CommentsByContractRequest As CommentsByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO CommentsByContractRequest(" & _
             "idcontractrequest," & _
             "additionalcomments," & _
             "startactrequires,")
            If (CommentsByContractRequest.datenoticeexpiration > CDate("1900/01/01")) Then sql.AppendLine("datenoticeexpiration,")
            sql.AppendLine("contractnumber," & _
             "purchaseorder" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & CommentsByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & CommentsByContractRequest.additionalcomments & "',")
            sql.AppendLine("'" & CommentsByContractRequest.startactrequires & "',")
            If (CommentsByContractRequest.datenoticeexpiration > CDate("1900/01/01")) _
            Then sql.AppendLine("'" & CommentsByContractRequest.datenoticeexpiration.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & CommentsByContractRequest.contractnumber & "',")
            sql.AppendLine("'" & CommentsByContractRequest.purchaseorder & "')")

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
            Throw New Exception("Error al insertar las observaciones de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un CommentsByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As CommentsByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objCommentsByContractRequest As New CommentsByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM CommentsByContractRequest ")
            sql.Append(" WHERE IdContractRequest = " & idContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objCommentsByContractRequest.id = data.Rows(0)("id")
                objCommentsByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
                objCommentsByContractRequest.additionalcomments = data.Rows(0)("additionalcomments")
                objCommentsByContractRequest.startactrequires = data.Rows(0)("startactrequires")
                If Not (IsDBNull(data.Rows(0)("datenoticeexpiration"))) Then _
                objCommentsByContractRequest.datenoticeexpiration = data.Rows(0)("datenoticeexpiration")
                objCommentsByContractRequest.contractnumber = data.Rows(0)("contractnumber")
                objCommentsByContractRequest.purchaseorder = data.Rows(0)("purchaseorder")

            End If

            ' retornar el objeto
            Return objCommentsByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objCommentsByContractRequest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="additionalcomments"></param>
    ''' <param name="startactrequires"></param>
    ''' <param name="datenoticeexpiration"></param>
    ''' <param name="contractnumber"></param>
    ''' <param name="purchaseorder"></param>
    ''' <returns>un objeto de tipo List(Of CommentsByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal additionalcomments As String = "", _
        Optional ByVal startactrequires As String = "", _
        Optional ByVal datenoticeexpiration As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal purchaseorder As String = "", _
        Optional ByVal order As String = "") As List(Of CommentsByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objCommentsByContractRequest As CommentsByContractRequestEntity
        Dim CommentsByContractRequestList As New List(Of CommentsByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM CommentsByContractRequest ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idcontractrequest.Equals("") Then

                sql.Append(where & " idcontractrequest like '%" & idcontractrequest & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not additionalcomments.Equals("") Then

                sql.Append(where & " additionalcomments like '%" & additionalcomments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startactrequires.Equals("") Then

                sql.Append(where & " startactrequires like '%" & startactrequires & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not datenoticeexpiration.Equals("") Then

                sql.Append(where & " datenoticeexpiration like '%" & datenoticeexpiration & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractnumber.Equals("") Then

                sql.Append(where & " contractnumber like '%" & contractnumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not purchaseorder.Equals("") Then

                sql.Append(where & " purchaseorder like '%" & purchaseorder & "%'")
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
                objCommentsByContractRequest = New CommentsByContractRequestEntity

                ' cargar el valor del campo
                objCommentsByContractRequest.id = row("id")
                objCommentsByContractRequest.idcontractrequest = row("idcontractrequest")
                objCommentsByContractRequest.additionalcomments = row("additionalcomments")
                objCommentsByContractRequest.startactrequires = row("startactrequires")
                objCommentsByContractRequest.datenoticeexpiration = row("datenoticeexpiration")
                objCommentsByContractRequest.contractnumber = row("contractnumber")
                objCommentsByContractRequest.purchaseorder = row("purchaseorder")

                ' agregar a la lista
                CommentsByContractRequestList.Add(objCommentsByContractRequest)

            Next

            ' retornar el objeto
            getList = CommentsByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar las observaciones de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objCommentsByContractRequest = Nothing
            CommentsByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo CommentsByContractRequest
    ''' </summary>
    ''' <param name="CommentsByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal CommentsByContractRequest As CommentsByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update CommentsByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & CommentsByContractRequest.idcontractrequest & "',")
            sql.AppendLine(" additionalcomments = '" & CommentsByContractRequest.additionalcomments & "',")
            sql.AppendLine(" startactrequires = '" & CommentsByContractRequest.startactrequires & "',")
            If (CommentsByContractRequest.datenoticeexpiration > CDate("1900/01/01")) Then
                sql.AppendLine(" datenoticeexpiration = '" & CommentsByContractRequest.datenoticeexpiration.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine(" datenoticeexpiration = NULL" & ",")
            End If
            sql.AppendLine(" contractnumber = '" & CommentsByContractRequest.contractnumber & "',")
            sql.AppendLine(" purchaseorder = '" & CommentsByContractRequest.purchaseorder & "'")
            sql.AppendLine("WHERE id = " & CommentsByContractRequest.id)

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
            Throw New Exception("Error al modificar las observaciones de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el CommentsByContractRequest de una forma
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from CommentsByContractRequest ")
            SQL.AppendLine(" where IdContractRequest = '" & idContractRequest & "' ")

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
            Throw New Exception("Error al elimiar las observaciones de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
