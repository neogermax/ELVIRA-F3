Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AccumulationIndicatorSetDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal code As String, _
                                Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM AccumulationIndicatorSet WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM AccumulationIndicatorSet WHERE Code = '" & code & "' AND id <> '" & id & "'")

            End If

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
            Throw New Exception("Error al verificar el código de ENTERPRISE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo AccumulationIndicatorSet
    ''' </summary>
    ''' <param name="AccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal AccumulationIndicatorSet As AccumulationIndicatorSetEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO AccumulationIndicatorSet(" & _
             "idindicator," & _
             "code," & _
             "description," & _
             "name," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & AccumulationIndicatorSet.idindicator & "',")
            sql.AppendLine("'" & AccumulationIndicatorSet.code & "',")
            sql.AppendLine("'" & AccumulationIndicatorSet.description & "',")
            sql.AppendLine("'" & AccumulationIndicatorSet.name & "',")
            sql.AppendLine("'" & AccumulationIndicatorSet.iduser & "',")
            sql.AppendLine("'" & AccumulationIndicatorSet.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el AccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un AccumulationIndicatorSet por el Id
    ''' </summary>
    ''' <param name="idAccumulationIndicatorSet"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAccumulationIndicatorSet As Integer, _
        Optional ByVal idindicator As Integer = 0) As AccumulationIndicatorSetEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objAccumulationIndicatorSet As New AccumulationIndicatorSetEntity
        Dim objIndicatorByAccumulationIndicatorSetListDALC As New IndicatorByAccumulationIndicatorSetDALC
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT accumulationIndicatorSet.Id, accumulationIndicatorSet.IdIndicator, accumulationIndicatorSet.Code, accumulationIndicatorSet.Name,  ")
            sql.Append(" 	accumulationIndicatorSet.IdUser, accumulationIndicatorSet.CreateDate, accumulationIndicatorSet.description, ")
            sql.Append("	Indicator.Code AS indicatorcode, ApplicationUser.Name AS username  ")
            sql.Append(" FROM accumulationIndicatorSet INNER JOIN ")
            sql.Append(" 	Indicator ON accumulationIndicatorSet.IdIndicator = Indicator.Id INNER JOIN ")
            sql.Append("    " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON accumulationIndicatorSet.IdUser = ApplicationUser.ID ")
            If idindicator = 0 Then
                sql.Append(" WHERE accumulationIndicatorSet.Id = " & idAccumulationIndicatorSet)
            Else
                sql.Append(" WHERE accumulationIndicatorSet.IdIndicator = " & idindicator)
            End If


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objAccumulationIndicatorSet.id = data.Rows(0)("id")
                objAccumulationIndicatorSet.idindicator = data.Rows(0)("idindicator")
                objAccumulationIndicatorSet.code = data.Rows(0)("code")
                objAccumulationIndicatorSet.description = data.Rows(0)("description")
                objAccumulationIndicatorSet.name = data.Rows(0)("name")
                objAccumulationIndicatorSet.iduser = data.Rows(0)("iduser")
                objAccumulationIndicatorSet.createdate = data.Rows(0)("createdate")
                objAccumulationIndicatorSet.USERNAME = data.Rows(0)("username")
                objAccumulationIndicatorSet.INDICATORCODE = data.Rows(0)("indicatorcode")
                objAccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST = objIndicatorByAccumulationIndicatorSetListDALC.getList(objApplicationCredentials, idaccumulationindicatorset:=objAccumulationIndicatorSet.id, order:="indicatorcode")

            End If

            ' retornar el objeto
            Return objAccumulationIndicatorSet

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objAccumulationIndicatorSet = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idindicator"></param>
    ''' <param name="indicatorcode"></param>
    ''' <param name="code"></param>
    ''' <param name="description"></param>
    ''' <param name="name"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of AccumulationIndicatorSetEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal indicatorcode As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of AccumulationIndicatorSetEntity)
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objAccumulationIndicatorSet As AccumulationIndicatorSetEntity
        Dim AccumulationIndicatorSetList As New List(Of AccumulationIndicatorSetEntity)
        Dim objIndicatorByAccumulationIndicatorSetListDALC As New IndicatorByAccumulationIndicatorSetDALC
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT accumulationIndicatorSet.Id, accumulationIndicatorSet.IdIndicator, accumulationIndicatorSet.Code, accumulationIndicatorSet.Name,  ")
            sql.Append(" 	accumulationIndicatorSet.IdUser, accumulationIndicatorSet.CreateDate, accumulationIndicatorSet.description, ")
            sql.Append("	Indicator.Code AS indicatorcode, ApplicationUser.Name AS username  ")
            sql.Append(" FROM accumulationIndicatorSet INNER JOIN ")
            sql.Append(" 	Indicator ON accumulationIndicatorSet.IdIndicator = Indicator.Id INNER JOIN ")
            sql.Append("     " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON accumulationIndicatorSet.IdUser = ApplicationUser.ID ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idindicator.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.idindicator = '" & idindicator & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not indicatorcode.Equals("") Then

                sql.Append(where & " Indicator.Code like '%" & indicatorcode & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " accumulationIndicatorSet.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(nvarchar, accumulationIndicatorSet.createdate, 103) like '%" & createdate & "%'")
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
                objAccumulationIndicatorSet = New AccumulationIndicatorSetEntity

                ' cargar el valor del campo
                objAccumulationIndicatorSet.id = row("id")
                objAccumulationIndicatorSet.idindicator = row("idindicator")
                objAccumulationIndicatorSet.code = row("code")
                objAccumulationIndicatorSet.name = row("name")
                objAccumulationIndicatorSet.iduser = row("iduser")
                objAccumulationIndicatorSet.createdate = row("createdate")
                objAccumulationIndicatorSet.USERNAME = row("username")
                objAccumulationIndicatorSet.INDICATORCODE = row("indicatorcode")
                objAccumulationIndicatorSet.INDICATORBYACCUMULATIONINDICATORSETLIST = objIndicatorByAccumulationIndicatorSetListDALC.getList(objApplicationCredentials, idaccumulationindicatorset:=objAccumulationIndicatorSet.id, order:="indicatorcode")

                ' agregar a la lista
                AccumulationIndicatorSetList.Add(objAccumulationIndicatorSet)

            Next

            ' retornar el objeto
            getList = AccumulationIndicatorSetList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de AccumulationIndicatorSet. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objAccumulationIndicatorSet = Nothing
            AccumulationIndicatorSetList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo AccumulationIndicatorSet
    ''' </summary>
    ''' <param name="AccumulationIndicatorSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal AccumulationIndicatorSet As AccumulationIndicatorSetEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update AccumulationIndicatorSet SET")
            sql.AppendLine(" idindicator = '" & AccumulationIndicatorSet.idindicator & "',")
            sql.AppendLine(" code = '" & AccumulationIndicatorSet.code & "',")
            sql.AppendLine(" name = '" & AccumulationIndicatorSet.name & "',")
            sql.AppendLine(" iduser = '" & AccumulationIndicatorSet.iduser & "',")
            sql.AppendLine(" createdate = '" & AccumulationIndicatorSet.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & AccumulationIndicatorSet.id)

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
            Throw New Exception("Error al modificar el AccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el AccumulationIndicatorSet de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idAccumulationIndicatorSet As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from AccumulationIndicatorSet ")
            SQL.AppendLine(" where id = '" & idAccumulationIndicatorSet & "' ")

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
            Throw New Exception("Error al elimiar el AccumulationIndicatorSet. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
