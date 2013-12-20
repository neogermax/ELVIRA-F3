Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ComponentByRiskDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ComponentByRisk
    ''' </summary>
    ''' <param name="ComponentByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal ComponentByRisk As ComponentByRiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ComponentByRisk(" & _
             "idrisk," & _
             "idcomponent" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ComponentByRisk.idrisk & "',")
            sql.AppendLine("'" & ComponentByRisk.idcomponent & "')")

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
            Throw New Exception("Error al insertar el ComponentByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un ComponentByRisk por el Id
    ''' </summary>
    ''' <param name="idComponentByRisk"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idComponentByRisk As Integer) As ComponentByRiskEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objComponentByRisk As New ComponentByRiskEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT ComponentByRisk.Id, ComponentByRisk.IdRisk, ComponentByRisk.IdComponent, " & _
                       "        Component.Name AS componentname, Risk.Name AS riskname, Component.Code " & _
                       " FROM ComponentByRisk INNER JOIN " & _
                       "      Component ON ComponentByRisk.IdComponent = Component.idkey and Component.IsLastVersion='1'  INNER JOIN " & _
                       "      Risk ON ComponentByRisk.IdRisk = Risk.id " & _
                       " WHERE ComponentByRisk.Id = " & idComponentByRisk)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objComponentByRisk.id = data.Rows(0)("id")
				objComponentByRisk.idrisk = data.Rows(0)("idrisk")
                objComponentByRisk.idcomponent = data.Rows(0)("idcomponent")
                objComponentByRisk.RISKNAME = data.Rows(0)("riskname")
                objComponentByRisk.COMPONENTNAME = data.Rows(0)("componentname")
                objComponentByRisk.CODE = data.Rows(0)("Code")

            End If

            ' retornar el objeto
            Return objComponentByRisk

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ComponentByRisk. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objComponentByRisk = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idrisk"></param>
    ''' <param name="riskname"></param>
    ''' <param name="idcomponent"></param>
    ''' <param name="componentname"></param>
    ''' <returns>un objeto de tipo List(Of ComponentByRiskEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
                                Optional ByVal idlike As String = "", _
								Optional ByVal idrisk As String = "", _
                                Optional ByVal riskname As String = "", _
								Optional ByVal idcomponent As String = "", _
                                Optional ByVal componentname As String = "", _
								Optional order as string = "") As List(Of ComponentByRiskEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objComponentByRisk As ComponentByRiskEntity
        Dim ComponentByRiskList As New List(Of ComponentByRiskEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            sql.Append(" SELECT ComponentByRisk.Id, ComponentByRisk.IdRisk, ComponentByRisk.IdComponent, " & _
                       "        Component.Name AS componentname, Risk.Name AS riskname, Component.Code " & _
                       " FROM ComponentByRisk INNER JOIN " & _
                       "      Component ON ComponentByRisk.IdComponent =Component.idkey and Component.IsLastVersion='1' INNER JOIN " & _
                       "      Risk ON ComponentByRisk.IdRisk =Risk.id  ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " ComponentByRisk.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " ComponentByRisk.id like '%" & idlike & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idrisk.Equals("") Then

                sql.Append(where & " ComponentByRisk.idrisk = '" & idrisk & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not riskname.Equals("") Then

                sql.Append(where & " Risk.Name like '%" & riskname & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idcomponent.Equals("") Then

                sql.Append(where & " ComponentByRisk.idcomponent = '" & idcomponent & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not componentname.Equals("") Then

                sql.Append(where & " Component.Name like '%" & componentname & "%'")
                where = " AND "

            End If
             
            If Not order.Equals(String.Empty) Then
            
                ' ordernar
                Select Case order
                    Case "riskname"
                        sql.Append(" ORDER BY riskname ")
                    Case "componentname"
                        sql.Append(" ORDER BY componentname ")
                    Case Else
                        sql.Append(" ORDER BY ComponentByRisk." & order)
                End Select
            
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objComponentByRisk = New ComponentByRiskEntity

				' cargar el valor del campo
				objComponentByRisk.id = row("id")
				objComponentByRisk.idrisk = row("idrisk")
                objComponentByRisk.idcomponent = row("idcomponent")
                objComponentByRisk.RISKNAME = row("riskname")
                objComponentByRisk.COMPONENTNAME = row("componentname")
                objComponentByRisk.CODE = row("Code")

                ' agregar a la lista
                ComponentByRiskList.Add(objComponentByRisk)

            Next

            ' retornar el objeto
            getList = ComponentByRiskList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ComponentByRisk. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objComponentByRisk = Nothing
            ComponentByRiskList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo ComponentByRisk
    ''' </summary>
    ''' <param name="ComponentByRisk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal ComponentByRisk As ComponentByRiskEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ComponentByRisk SET")
            SQL.AppendLine(" idrisk = '" & ComponentByRisk.idrisk & "',")           
            sql.AppendLine(" idcomponent = '" & ComponentByRisk.idcomponent & "'")
            SQL.AppendLine("WHERE id = " & ComponentByRisk.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, SQL.ToString)

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el ComponentByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el ComponentByRisk de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idComponentByRisk As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ComponentByRisk ")
            SQL.AppendLine(" where id = '" & idComponentByRisk & "' ")

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
            Throw New Exception("Error al elimiar el ComponentByRisk. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
