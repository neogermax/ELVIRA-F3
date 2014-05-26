Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class AlertsDALC

    'Constantes
    Const MODULENAME As String = "AlertsDALC"

    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Alert As AlertsEntity) As Long

        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            'Construir query
            sql.AppendLine("INSERT INTO Alerts(")
            sql.AppendLine("Name, Users, Groups, Days, Subject, Message) ")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Alert.name & "', ")
            sql.AppendLine("'" & Alert.users & "', ")
            sql.AppendLine("'" & Alert.groups & "', ")
            sql.AppendLine("'" & Alert.days & "', ")
            sql.AppendLine("'" & Alert.subject & "', ")
            sql.AppendLine("'" & Alert.message & "'")
            sql.AppendLine(")")
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            CtxSetComplete()

            Return num

        Catch ex As Exception

            CtxSetAbort()

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            Throw New Exception("Error al insertar la Alerta. " & ex.Message)

        Finally
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal AlertId As Long) As AlertsEntity

        Dim sql As New StringBuilder
        Dim objAlert As New AlertsEntity
        Dim data As DataTable
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            sql.AppendLine("SELECT * FROM Alerts ")
            sql.AppendLine("WHERE Alert_id = " & AlertId)

            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then
                objAlert.days = data.Rows(0)("days")
                objAlert.groups = data.Rows(0)("groups")
                objAlert.message = data.Rows(0)("message")
                objAlert.name = data.Rows(0)("name")
                objAlert.subject = data.Rows(0)("subject")
                objAlert.users = data.Rows(0)("users")
            End If

            Return objAlert

            CtxSetComplete()

        Catch ex As Exception

            CtxSetAbort()
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy"

            Throw New Exception("Error al cargar una Alerta. " & ex.Message)

        Finally

            sql = Nothing
            data = Nothing
            objAlert = Nothing

        End Try

    End Function

    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
            Optional ByVal name As String = "", _
            Optional ByVal subject As String = "") As List(Of AlertsEntity)

        Dim sql As New StringBuilder
        Dim objAlert As AlertsEntity
        Dim AlertList As New List(Of AlertsEntity)
        Dim data As DataTable
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            sql.AppendLine("SELECT * from Alerts ")

            If Not name.Equals("") Then
                sql.AppendLine("WHERE ")
                sql.AppendLine("Alerts.name like '%" & name & "%'")
            End If

            If Not subject.Equals("") Then
                sql.AppendLine("WHERE ")
                sql.AppendLine("Alerts.subject like '%" & subject & "%'")
            End If

            sql.AppendLine("ORDER BY Alerts.name DESC ")

            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                objAlert = New AlertsEntity
                objAlert.days = IIf(Not IsDBNull(row("days")), row("days"), "0")
                objAlert.groups = IIf(Not IsDBNull(row("groups")), row("groups"), Nothing)
                objAlert.message = IIf(Not IsDBNull(row("message")), row("message"), Nothing)
                objAlert.name = IIf(Not IsDBNull(row("name")), row("name"), Nothing)
                objAlert.subject = IIf(Not IsDBNull(row("subject")), row("subject"), Nothing)
                objAlert.users = IIf(Not IsDBNull(row("users")), row("users"), Nothing)

                AlertList.Add(objAlert)

            Next

            getList = AlertList

            CtxSetComplete()

        Catch ex As Exception

            CtxSetAbort()
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getlist")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            Throw New Exception("Error al cargar la lista de Alertas. " & ex.Message)

        Finally

            sql = Nothing
            objAlert = Nothing
            AlertList = Nothing
            data = Nothing

        End Try

    End Function

    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal Alert As AlertsEntity) As Long

        Dim sql As New StringBuilder

        Try
            sql.AppendLine("Update Alerts SET ")
            sql.AppendLine("days = " & Alert.days & "', ")
            sql.AppendLine("groups = " & Alert.groups & "', ")
            sql.AppendLine("message = " & Alert.message & "', ")
            sql.AppendLine("name = " & Alert.name & "', ")
            sql.AppendLine("subject = " & Alert.subject & "', ")
            sql.AppendLine("users = " & Alert.users & "' ")
            sql.AppendLine("WHERE alert_id = " & Alert.alert_id)

            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)
            CtxSetComplete()

        Catch ex As Exception

            CtxSetAbort()

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            Throw New Exception("Error al actualizar la Alerta. " & ex.Message)

        Finally

            sql = Nothing

        End Try

    End Function


    Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal AlertId As Long) As Long

        Dim sql As New StringBuilder

        Try
            sql.AppendLine("DELETE from Alerts ")
            sql.AppendLine("where alert_id = " & AlertId & "'")

            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)
            CtxSetComplete()

        Catch ex As Exception

            CtxSetAbort()

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "delete")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            Throw New Exception("Error al eliminar la alerta. " & ex.Message)

        Finally

            sql = Nothing

        End Try

    End Function


End Class