Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class MitigationByRiskDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ComponentByRisk
    ''' </summary>
    ''' <param name="MitigationtByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal MitigationtByRisk As MitigationByRiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO MitigationByRisk(" & _
             "idrisk," & _
             "idmitigation" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & MitigationtByRisk.idrisk & "',")
            sql.AppendLine("'" & MitigationtByRisk.idmitigation & "')")

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
            Throw New Exception("Error al insertar el MitigetionByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ComponentByRisk por el Id
    ''' </summary>
    ''' <param name="idMitigationByRisk"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigationByRisk As Integer) As MitigationByRiskEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objMitigationByRisk As New MitigationByRiskEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT MitigationByRisk.Id, MitigationByRisk.IdRisk, MitigationByRisk.IdMitigation, " & _
                       "        Mitigation.Name AS mitigationname, Risk.Name AS riskname, Risk.Code " & _
                       " FROM MitigationByRisk INNER JOIN " & _
                       "      Mitigation ON MitigationByRisk.IdMitigation = Mitigation.id   INNER JOIN " & _
                       "      Risk ON MitigationByRisk.IdRisk = Risk.id  " & _
                       " WHERE MitigationByRisk.Id = " & idMitigationByRisk)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objMitigationByRisk.id = data.Rows(0)("id")
                objMitigationByRisk.idrisk = data.Rows(0)("idrisk")
                objMitigationByRisk.idmitigation = data.Rows(0)("idmitigation")
                objMitigationByRisk.RISKNAME = data.Rows(0)("riskname")
                objMitigationByRisk.MITIGATIONNAME = data.Rows(0)("mitigationname")
                objMitigationByRisk.CODE = data.Rows(0)("Code")

            End If

            ' retornar el objeto
            Return objMitigationByRisk

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un MitigationByRisk. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objMitigationByRisk = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idrisk"></param>
    ''' <param name="riskname"></param>
    ''' <param name="idmitigation"></param>
    ''' <param name="mitigationname"></param>
    ''' <returns>un objeto de tipo List(Of ComponentByRiskEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idrisk As String = "", _
        Optional ByVal riskname As String = "", _
        Optional ByVal idmitigation As String = "", _
       Optional ByVal mitigationname As String = "", _
        Optional ByVal order As String = "") As List(Of MitigationByRiskEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objMitigationByRisk As MitigationByRiskEntity
        Dim MitigationByRiskList As New List(Of MitigationByRiskEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT MitigationByRisk.Id, MitigationByRisk.IdRisk, MitigationByRisk.IdMitigation, " & _
                       "        Mitigation.Name AS mitigationname, Risk.Name AS riskname, Risk.Code " & _
                       " FROM MitigationByRisk INNER JOIN " & _
                       "      Mitigation ON MitigationByRisk.IdMitigation =Mitigation.id INNER JOIN " & _
                       "      Risk ON MitigationByRisk.IdRisk =Risk.id   ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " MitigationByRisk.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " MitigationByRisk.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idrisk.Equals("") Then

                sql.Append(where & " MitigationByRisk.idrisk = '" & idrisk & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not riskname.Equals("") Then

                sql.Append(where & " Risk.Name like '%" & riskname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idmitigation.Equals("") Then

                sql.Append(where & " MitigationByRisk.idmitigation = '" & idmitigation & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not mitigationname.Equals("") Then

                sql.Append(where & " Mitigation.Name like '%" & mitigationname & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "riskname"
                        sql.Append(" ORDER BY Risk.Name ")
                    Case "mitigationname"
                        sql.Append(" ORDER BY mitigationname ")
                    Case Else
                        sql.Append(" ORDER BY MitigationByRisk." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objMitigationByRisk = New MitigationByRiskEntity

                ' cargar el valor del campo
                objMitigationByRisk.id = row("id")
                objMitigationByRisk.idrisk = row("idrisk")
                objMitigationByRisk.idmitigation = row("idmitigation")
                objMitigationByRisk.RISKNAME = row("riskname")
                objMitigationByRisk.MITIGATIONNAME = row("mitigationname")
                objMitigationByRisk.CODE = row("Code")

                ' agregar a la lista
                MitigationByRiskList.Add(objMitigationByRisk)

            Next

            ' retornar el objeto
            getList = MitigationByRiskList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de MitigationByRisk. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objMitigationByRisk = Nothing
            MitigationByRiskList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ComponentByRisk
    ''' </summary>
    ''' <param name="MitigationByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal MitigationByRisk As MitigationByRiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update MitigationByRisk SET")
            sql.AppendLine(" idrisk = '" & MitigationByRisk.idrisk & "',")
            sql.AppendLine(" idmitigation = '" & MitigationByRisk.idmitigation & "'")
            sql.AppendLine("WHERE id = " & MitigationByRisk.id)

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
            Throw New Exception("Error al modificar el MitigationByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ComponentByRisk de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idMitigationByRisk As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from MitigationByRisk ")
            SQL.AppendLine(" where idmitigation = '" & idMitigationByRisk & "' ")

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
            Throw New Exception("Error al elimiar el MitigationByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
