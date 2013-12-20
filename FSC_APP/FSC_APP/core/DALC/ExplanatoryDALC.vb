
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager


Public Class ExplanatoryDALC
    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ACLARATORIO
    ''' </summary>
    ''' <param name="explanatory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Explanatory As ExplanatoryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Explanatory (" & _
             "observation," & _
             "fecha," & _
             "idproject" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Explanatory.observation & "',")
            sql.AppendLine("'" & Explanatory.fecha.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & Explanatory.idproject & "')")

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
            Throw New Exception("Error al insertar el aclaratorio. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Modificar un objeto de tipo Aclaratorio
    ''' </summary>
    ''' <param name="Explanatory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Explanatory As ExplanatoryEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update LocationByIdea SET")
            sql.AppendLine(" observation = '" & Explanatory.observation & "',")
            sql.AppendLine(" fecha = '" & Explanatory.fecha & "',")
            sql.AppendLine(" idproject = '" & Explanatory.idproject & "',")
            sql.AppendLine("WHERE id = " & Explanatory.id)

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
            Throw New Exception("Error al modificar el Aclaratorio. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Borra el Explanatory de una forma
    ''' </summary>
    ''' <param name="Aclaratorio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExplanatory As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Explanatory ")
            SQL.AppendLine(" where idproject = '" & idExplanatory & "' ")

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
            Throw New Exception("Error al elimiar el LocationByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Cargar un LocationByIdea por el Id
    ''' </summary>
    ''' <param name="idLocationByIdea"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExplanatory As Integer) As ExplanatoryEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objExplanatoryEntity As New ExplanatoryEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM Explanatory ")
            sql.Append(" WHERE Id = " & idExplanatory)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objExplanatoryEntity.id = data.Rows(0)("id")
                objExplanatoryEntity.observation = data.Rows(0)("observation")
                objExplanatoryEntity.fecha = data.Rows(0)("fecha")
                objExplanatoryEntity.idproject = data.Rows(0)("idproject")

            End If

            ' retornar el objeto
            Return objExplanatoryEntity

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un LocationByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objExplanatoryEntity = Nothing

        End Try

    End Function

    Public Function getListExplanatory(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       Optional ByVal id As String = "", _
       Optional ByVal observation As String = "", _
       Optional ByVal fecha As String = "", _
       Optional ByVal idproject As String = "", _
       Optional ByVal order As String = "") As List(Of ExplanatoryEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objExplanatoryEntity As ExplanatoryEntity
        Dim ExplanatoryList As New List(Of ExplanatoryEntity)
        Dim data As DataTable

        ' obtener el nombre de la base de datos de seguridad
        'Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        'Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Explanatory.id, Explanatory.observation, Explanatory.fecha ")
            sql.Append(" FROM Explanatory ")
            
            sql.Append(" WHERE Explanatory.idProject =" & idproject)


            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objExplanatoryEntity = New ExplanatoryEntity

                ' cargar el valor del campo
                objExplanatoryEntity.id = row("id")
                objExplanatoryEntity.observation = row("observation")
                objExplanatoryEntity.fecha = row("fecha")
                

                ' agregar a la lista
                ExplanatoryList.Add(objExplanatoryEntity)

            Next

            ' retornar el objeto
            getListExplanatory = ExplanatoryList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de LocationByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objExplanatoryEntity = Nothing
            ExplanatoryList = Nothing
            data = Nothing


        End Try

    End Function




End Class
