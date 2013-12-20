Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ContractExecutionDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ContractExecution
    ''' </summary>
    ''' <param name="ContractExecution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ContractExecution As ContractExecutionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ContractExecution(" & _
             "idcontractrequest," & _
             "startdate," & _
             "paymentdate," & _
             "contractnumber," & _
             "ordernumber," & _
             "closingcomments," & _
             "closingdate," & _
             "finalpaymentdate," & _
             "value," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ContractExecution.idcontractrequest & "',")
            sql.AppendLine("'" & ContractExecution.startdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & ContractExecution.paymentdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & ContractExecution.contractnumber & "',")
            sql.AppendLine("'" & ContractExecution.ordernumber & "',")
            sql.AppendLine("'" & ContractExecution.closingcomments & "',")
            sql.AppendLine("'" & ContractExecution.closingdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & ContractExecution.finalpaymentdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & ContractExecution.value.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & ContractExecution.iduser & "',")
            sql.AppendLine("'" & ContractExecution.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(ContractExecution.idcontractrequest)

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
            Throw New Exception("Error al insertar la Ejecución de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ContractExecution por el Id
    ''' </summary>
    ''' <param name="idContractExecution"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractExecution As Integer) As ContractExecutionEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objContractExecution As New ContractExecutionEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ContractExecution.*, ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM ContractExecution ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = ContractExecution.IdUser  ")
            sql.Append(" WHERE IdContractRequest = " & idContractExecution)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objContractExecution.idcontractrequest = data.Rows(0)("idcontractrequest")
                objContractExecution.startdate = data.Rows(0)("startdate")
                objContractExecution.paymentdate = data.Rows(0)("paymentdate")
                objContractExecution.contractnumber = data.Rows(0)("contractnumber")
                objContractExecution.ordernumber = data.Rows(0)("ordernumber")
                objContractExecution.closingcomments = data.Rows(0)("closingcomments")
                objContractExecution.closingdate = data.Rows(0)("closingdate")
                objContractExecution.finalpaymentdate = data.Rows(0)("finalpaymentdate")
                objContractExecution.value = data.Rows(0)("value")
                objContractExecution.iduser = data.Rows(0)("iduser")
                objContractExecution.createdate = data.Rows(0)("createdate")
                objContractExecution.USERNAME = data.Rows(0)("ApplicationUserName")
                objContractExecution.IDCONTRACTREQUESTOLD = objContractExecution.idcontractrequest

            End If

            ' retornar el objeto
            Return objContractExecution

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una Ejecución de contrato. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractExecution = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="startdate"></param>
    ''' <param name="paymentdate"></param>
    ''' <param name="contractnumber"></param>
    ''' <param name="ordernumber"></param>
    ''' <param name="closingcomments"></param>
    ''' <param name="closingdate"></param>
    ''' <param name="finalpaymentdate"></param>
    ''' <param name="value"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ContractExecutionEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal idcontractrequest As String = "", _
        Optional ByVal startdate As String = "", _
        Optional ByVal paymentdate As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal ordernumber As String = "", _
        Optional ByVal closingcomments As String = "", _
        Optional ByVal closingdate As String = "", _
        Optional ByVal finalpaymentdate As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ContractExecutionEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objContractExecution As ContractExecutionEntity
        Dim ContractExecutionList As New List(Of ContractExecutionEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ContractExecution.*, ApplicationUser.Name AS ApplicationUserName ")
            sql.Append(" FROM ContractExecution ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ApplicationUser.Id = ContractExecution.IdUser  ")

            ' verificar si hay entrada de datos para el campo
            If Not idcontractrequest.Equals("") Then

                sql.Append(where & " idcontractrequest like '%" & idcontractrequest & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not startdate.Equals("") Then

                sql.Append(where & " ContractExecution.startdate like '%" & startdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not paymentdate.Equals("") Then

                sql.Append(where & " ContractExecution.paymentdate like '%" & paymentdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractnumber.Equals("") Then

                sql.Append(where & " ContractExecution.contractnumber like '%" & contractnumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ordernumber.Equals("") Then

                sql.Append(where & " ContractExecution.ordernumber like '%" & ordernumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not closingcomments.Equals("") Then

                sql.Append(where & " ContractExecution.closingcomments like '%" & closingcomments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not closingdate.Equals("") Then

                sql.Append(where & " ContractExecution.closingdate like '%" & closingdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not finalpaymentdate.Equals("") Then

                sql.Append(where & " ContractExecution.finalpaymentdate like '%" & finalpaymentdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not value.Equals("") Then

                sql.Append(where & " ContractExecution.value like '%" & value & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " ContractExecution.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " ContractExecution.createdate like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case Else
                        sql.Append(" ORDER BY ContractExecution." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objContractExecution = New ContractExecutionEntity

                ' cargar el valor del campo
                objContractExecution.idcontractrequest = row("idcontractrequest")
                objContractExecution.startdate = row("startdate")
                objContractExecution.paymentdate = row("paymentdate")
                objContractExecution.contractnumber = row("contractnumber")
                objContractExecution.ordernumber = row("ordernumber")
                objContractExecution.closingcomments = row("closingcomments")
                objContractExecution.closingdate = row("closingdate")
                objContractExecution.finalpaymentdate = row("finalpaymentdate")
                objContractExecution.value = row("value")
                objContractExecution.iduser = row("iduser")
                objContractExecution.createdate = row("createdate")
                objContractExecution.USERNAME = row("ApplicationUserName")

                ' agregar a la lista
                ContractExecutionList.Add(objContractExecution)

            Next

            ' retornar el objeto
            getList = ContractExecutionList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista las Ejecuciones de contrato. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objContractExecution = Nothing
            ContractExecutionList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ContractExecution
    ''' </summary>
    ''' <param name="ContractExecution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ContractExecution As ContractExecutionEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ContractExecution SET")
            sql.AppendLine(" idcontractrequest = '" & ContractExecution.idcontractrequest & "',")
            sql.AppendLine(" startdate = '" & ContractExecution.startdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" paymentdate = '" & ContractExecution.paymentdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" contractnumber = '" & ContractExecution.contractnumber & "',")
            sql.AppendLine(" ordernumber = '" & ContractExecution.ordernumber & "',")
            sql.AppendLine(" closingcomments = '" & ContractExecution.closingcomments & "',")
            sql.AppendLine(" closingdate = '" & ContractExecution.closingdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" finalpaymentdate = '" & ContractExecution.finalpaymentdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" value = '" & ContractExecution.value.ToString().Replace(",", ".") & "'")
            sql.AppendLine(" WHERE idcontractrequest = " & ContractExecution.IDCONTRACTREQUESTOLD)

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
            Throw New Exception("Error al modificar la Ejecución de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ContractExecution de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractExecution As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ContractExecution ")
            SQL.AppendLine(" where idcontractrequest = '" & idContractExecution & "' ")

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
            Throw New Exception("Error al elimiar la Ejecución de contrato. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Metodo que permite poblar una lista de solicitudes de contrato
    ''' </summary>
    ''' <param name="objApplicationCredentials">credenciales del usuario actual</param>
    ''' <param name="enabled">estado de los registros</param>
    ''' <returns>retorna una lista de solicitudes de contrato</returns>
    ''' <remarks></remarks>
    Public Function getContractRequestList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    Optional ByVal enabled As String = "") As List(Of ContractRequestEntity)

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim contractRequestList As New List(Of ContractRequestEntity)
        Dim objContractRequest As ContractRequestEntity
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT RequestNumber ")
            sql.Append(" FROM ContractRequest ")
            sql.Append(" LEFT JOIN ContractExecution ")
            sql.Append(" ON ContractRequest.RequestNumber = ContractExecution.IdContractRequest ")

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " ContractRequest.enabled = '" & enabled & "'")
                where = " AND "

            End If

            'Se agrega la condicion final
            sql.Append(where & " ContractExecution.IdContractRequest IS NULL ")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objContractRequest = New ContractRequestEntity

                ' cargar el valor del campo
                objContractRequest.requestnumber = row("RequestNumber")

                ' agregar a la lista
                contractRequestList.Add(objContractRequest)

            Next

            ' retornar el objeto
            getContractRequestList = contractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de solicirudes de contrato. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objContractRequest = Nothing
            contractRequestList = Nothing

        End Try

    End Function

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="IdContractRequest">Identificador que se desea verificar</param>
    ''' <returns>Verdadero si existe algún registro, falso en caso contrario</returns>
    ''' <remarks></remarks>
    Public Function verifyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
    ByVal IdContractRequest As String) As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            'Se usa antes de ingresar un nuevo registro
            sql.AppendLine("SELECT COUNT(IdContractRequest) AS cont FROM ContractExecution WHERE IdContractRequest = '" & IdContractRequest & "'")

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyCode = False

                Else

                    ' retornar que existe
                    verifyCode = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el Identificador de la Ejecución de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function


End Class
