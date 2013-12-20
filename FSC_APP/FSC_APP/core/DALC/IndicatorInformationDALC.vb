Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class IndicatorInformationDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo IndicatorInformation
    ''' </summary>
    ''' <param name="IndicatorInformation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal IndicatorInformation As IndicatorInformationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO IndicatorInformation(" & _
             "idmeasurementdatebyindicator," & _
             "idindicator," & _
             "description," & _
             "goal," & _
             "value," & _
             "comments," & _
              "MeasureDate," & _
             "registrationdate," & _
             "iduser" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & IndicatorInformation.idmeasurementdatebyindicator & "',")
            sql.AppendLine("'" & IndicatorInformation.idindicator & "',")
            sql.AppendLine("'" & IndicatorInformation.description & "',")
            sql.AppendLine("'" & IndicatorInformation.goal & "',")
            sql.AppendLine("'" & IndicatorInformation.value.Replace(",", ".") & "',")
            sql.AppendLine("'" & IndicatorInformation.comments & "',")
            sql.AppendLine("'" & IndicatorInformation.measuredate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & IndicatorInformation.registrationdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & IndicatorInformation.iduser & "')")

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
            Throw New Exception("Error al insertar la información del indicador." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un IndicatorInformation por el Id
    ''' </summary>
    ''' <param name="idIndicatorInformation"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorInformation As Integer) As IndicatorInformationEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objIndicatorInformation As New IndicatorInformationEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM IndicatorInformation ")
            sql.Append(" WHERE Id = " & idIndicatorInformation)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objIndicatorInformation.id = data.Rows(0)("id")
                objIndicatorInformation.idmeasurementdatebyindicator = data.Rows(0)("idmeasurementdatebyindicator")
                objIndicatorInformation.idindicator = data.Rows(0)("idindicator")
                objIndicatorInformation.description = data.Rows(0)("description")
                objIndicatorInformation.goal = data.Rows(0)("goal")
                objIndicatorInformation.value = data.Rows(0)("value")
                objIndicatorInformation.comments = data.Rows(0)("comments")
                objIndicatorInformation.measuredate = data.Rows(0)("measuredate")
                objIndicatorInformation.registrationdate = data.Rows(0)("registrationdate")
                objIndicatorInformation.iduser = data.Rows(0)("iduser")

            End If

            ' retornar el objeto
            Return objIndicatorInformation

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la información del indicador.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objIndicatorInformation = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idmeasurementdatebyindicator"></param>
    ''' <param name="idindicator"></param>
    ''' <param name="description"></param>
    ''' <param name="goal"></param>
    ''' <param name="value"></param>
    ''' <param name="comments"></param>
    ''' <param name="registrationdate"></param>
    ''' <param name="iduser"></param>
    ''' <returns>un objeto de tipo List(Of IndicatorInformationEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idmeasurementdatebyindicator As String = "", _
        Optional ByVal idindicator As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal goal As String = "", _
        Optional ByVal value As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal registrationdate As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal order As String = "") As List(Of IndicatorInformationEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objIndicatorInformation As IndicatorInformationEntity
        Dim IndicatorInformationList As New List(Of IndicatorInformationEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM IndicatorInformation ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idmeasurementdatebyindicator.Equals("") Then

                sql.Append(where & " idmeasurementdatebyindicator like '%" & idmeasurementdatebyindicator & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idindicator.Equals("") Then

                sql.Append(where & " idindicator like '%" & idindicator & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not goal.Equals("") Then

                sql.Append(where & " goal like '%" & goal & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not value.Equals("") Then

                sql.Append(where & " value like '%" & value & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not registrationdate.Equals("") Then

                sql.Append(where & " registrationdate like '%" & registrationdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " iduser like '%" & iduser & "%'")
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
                objIndicatorInformation = New IndicatorInformationEntity

                ' cargar el valor del campo
                objIndicatorInformation.id = row("id")
                objIndicatorInformation.idmeasurementdatebyindicator = row("idmeasurementdatebyindicator")
                objIndicatorInformation.idindicator = row("idindicator")
                objIndicatorInformation.description = row("description")
                objIndicatorInformation.goal = row("goal")
                objIndicatorInformation.value = row("value")
                objIndicatorInformation.comments = row("comments")
                objIndicatorInformation.measuredate = row("measuredate")
                objIndicatorInformation.registrationdate = row("registrationdate")
                objIndicatorInformation.iduser = row("iduser")

                ' agregar a la lista
                IndicatorInformationList.Add(objIndicatorInformation)

            Next

            ' retornar el objeto
            getList = IndicatorInformationList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de informaciónes del indicador.")

        Finally
            ' liberando recursos
            sql = Nothing
            objIndicatorInformation = Nothing
            IndicatorInformationList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo IndicatorInformation
    ''' </summary>
    ''' <param name="IndicatorInformation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal IndicatorInformation As IndicatorInformationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update IndicatorInformation SET")
            sql.AppendLine(" idmeasurementdatebyindicator = '" & IndicatorInformation.idmeasurementdatebyindicator & "',")
            sql.AppendLine(" idindicator = '" & IndicatorInformation.idindicator & "',")
            sql.AppendLine(" description = '" & IndicatorInformation.description & "',")
            sql.AppendLine(" goal = '" & IndicatorInformation.goal & "',")
            sql.AppendLine(" value = '" & IndicatorInformation.value.Replace(",", ".") & "',")
            sql.AppendLine(" comments = '" & IndicatorInformation.comments & "',")
            sql.AppendLine(" MeasureDate = '" & IndicatorInformation.measuredate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine(" registrationdate = '" & IndicatorInformation.registrationdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine(" iduser = '" & IndicatorInformation.iduser & "'")
            sql.AppendLine(" WHERE id = " & IndicatorInformation.id)

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
            Throw New Exception("Error al modificar la información del indicador." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el IndicatorInformation de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIndicatorInformation As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from IndicatorInformation ")
            SQL.AppendLine(" where id = '" & idIndicatorInformation & "' ")

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
            Throw New Exception("Error al elimiar la información del indicador." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
