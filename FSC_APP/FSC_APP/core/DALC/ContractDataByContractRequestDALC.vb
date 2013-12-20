Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractDataByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractDataByContractRequest
    ''' </summary>
    ''' <param name="ContractDataByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractDataByContractRequest As ContractDataByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractDataByContractRequest(" & _
             "idcontractrequest," & _
             "contractduration," & _
             "startdate," & _
             "enddate," & _
             "supervisor," & _
             "budgetvalidity," & _
             "contactdata," & _
             "email," & _
             "telephone" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ContractDataByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & ContractDataByContractRequest.contractduration & "',")
            If (ContractDataByContractRequest.startdate > CDate("1900/01/01")) Then
                sql.AppendLine("'" & ContractDataByContractRequest.startdate.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine("NULL,")
            End If
            If (ContractDataByContractRequest.enddate > CDate("1900/01/01")) Then
                sql.AppendLine("'" & ContractDataByContractRequest.enddate.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine("NULL,")
            End If
            sql.AppendLine("'" & ContractDataByContractRequest.supervisor & "',")
            sql.AppendLine("'" & ContractDataByContractRequest.budgetvalidity & "',")
            sql.AppendLine("'" & ContractDataByContractRequest.contactdata & "',")
            sql.AppendLine("'" & ContractDataByContractRequest.email & "',")
            sql.AppendLine("'" & ContractDataByContractRequest.telephone & "')")

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
            Throw New Exception("Error al insertar los datos del contrato de la solicitud actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractDataByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As ContractDataByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractDataByContractRequest As New ContractDataByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractDataByContractRequest ")
            sql.Append(" WHERE IdContractRequest = " & idContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractDataByContractRequest.id = data.Rows(0)("id")
                objContractDataByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
                objContractDataByContractRequest.contractduration = data.Rows(0)("contractduration")
                If Not (IsDBNull(data.Rows(0)("startdate"))) Then objContractDataByContractRequest.startdate = data.Rows(0)("startdate")
                If Not (IsDBNull(data.Rows(0)("enddate"))) Then objContractDataByContractRequest.enddate = data.Rows(0)("enddate")
                objContractDataByContractRequest.supervisor = data.Rows(0)("supervisor")
                objContractDataByContractRequest.budgetvalidity = data.Rows(0)("budgetvalidity")
                objContractDataByContractRequest.contactdata = data.Rows(0)("contactdata")
                objContractDataByContractRequest.email = data.Rows(0)("email")
                objContractDataByContractRequest.telephone = data.Rows(0)("telephone")

            End If

            ' retornar el objeto
            Return objContractDataByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los datos del contrato de la solicitud actual.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractDataByContractRequest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="contractduration"></param>
    ''' <param name="startdate"></param>
    ''' <param name="enddate"></param>
    ''' <param name="supervisor"></param>
    ''' <param name="budgetvalidity"></param>
    ''' <param name="contactdata"></param>
    ''' <param name="email"></param>
    ''' <param name="telephone"></param>
    ''' <returns>un objeto de tipo List(Of ContractDataByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal contractduration As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal enddate As String = "", _
        Optional ByVal supervisor As String = "", _
        Optional ByVal budgetvalidity As String = "", _
        Optional ByVal contactdata As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal telephone As String = "", _
        Optional ByVal order As String = "") As List(Of ContractDataByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractDataByContractRequest As ContractDataByContractRequestEntity
        Dim ContractDataByContractRequestList As New List(Of ContractDataByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ContractDataByContractRequest ")

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
            If Not contractduration.Equals("") Then

                sql.Append(where & " contractduration like '%" & contractduration & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startdate.Equals("") Then

                sql.Append(where & " startdate like '%" & startdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enddate.Equals("") Then

                sql.Append(where & " enddate like '%" & enddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not supervisor.Equals("") Then

                sql.Append(where & " supervisor like '%" & supervisor & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not budgetvalidity.Equals("") Then

                sql.Append(where & " budgetvalidity like '%" & budgetvalidity & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contactdata.Equals("") Then

                sql.Append(where & " contactdata like '%" & contactdata & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not email.Equals("") Then

                sql.Append(where & " email like '%" & email & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not telephone.Equals("") Then

                sql.Append(where & " telephone like '%" & telephone & "%'")
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
                objContractDataByContractRequest = New ContractDataByContractRequestEntity

                ' cargar el valor del campo
                objContractDataByContractRequest.id = row("id")
                objContractDataByContractRequest.idcontractrequest = row("idcontractrequest")
                objContractDataByContractRequest.contractduration = row("contractduration")
                objContractDataByContractRequest.startdate = row("startdate")
                objContractDataByContractRequest.enddate = row("enddate")
                objContractDataByContractRequest.supervisor = row("supervisor")
                objContractDataByContractRequest.budgetvalidity = row("budgetvalidity")
                objContractDataByContractRequest.contactdata = row("contactdata")
                objContractDataByContractRequest.email = row("email")
                objContractDataByContractRequest.telephone = row("telephone")

                ' agregar a la lista
                ContractDataByContractRequestList.Add(objContractDataByContractRequest)

            Next

            ' retornar el objeto
            getList = ContractDataByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar los datos del contrato de la solicitud actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractDataByContractRequest = Nothing
            ContractDataByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractDataByContractRequest
    ''' </summary>
    ''' <param name="ContractDataByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractDataByContractRequest As ContractDataByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractDataByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & ContractDataByContractRequest.idcontractrequest & "',")
            sql.AppendLine(" contractduration = '" & ContractDataByContractRequest.contractduration & "',")
            If (ContractDataByContractRequest.startdate > CDate("1900/01/01")) Then
                sql.AppendLine(" startdate = '" & ContractDataByContractRequest.startdate.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine(" startdate = NULL,")
            End If
            If (ContractDataByContractRequest.enddate > CDate("1900/01/01")) Then
                sql.AppendLine(" enddate = '" & ContractDataByContractRequest.enddate.ToString("yyyyMMdd") & "',")
            Else
                sql.AppendLine(" enddate = NULL,")
            End If
            sql.AppendLine(" supervisor = '" & ContractDataByContractRequest.supervisor & "',")
            sql.AppendLine(" budgetvalidity = '" & ContractDataByContractRequest.budgetvalidity & "',")
            sql.AppendLine(" contactdata = '" & ContractDataByContractRequest.contactdata & "',")
            sql.AppendLine(" email = '" & ContractDataByContractRequest.email & "',")
            sql.AppendLine(" telephone = '" & ContractDataByContractRequest.telephone & "'")
            sql.AppendLine("WHERE id = " & ContractDataByContractRequest.id)

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
            Throw New Exception("Error al modificar los datos del contrato de la solicitud actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractDataByContractRequest de una forma
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
            SQL.AppendLine(" Delete from ContractDataByContractRequest ")
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
            Throw New Exception("Error al elimiar los datos del contrato de la solicitud actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
