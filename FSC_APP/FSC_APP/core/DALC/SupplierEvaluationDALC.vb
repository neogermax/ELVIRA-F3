Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SupplierEvaluationDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo SupplierEvaluation
    ''' </summary>
    ''' <param name="SupplierEvaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal SupplierEvaluation As SupplierEvaluationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO SupplierEvaluation(" & _
             "idsupplier," & _
             "contractnumber," & _
             "contractstartdate," & _
             "contractenddate," & _
             "contractsubject," & _
             "contractvalue," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SupplierEvaluation.idsupplier & "',")
            sql.AppendLine("'" & SupplierEvaluation.contractnumber & "',")
            sql.AppendLine("'" & SupplierEvaluation.contractstartdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & SupplierEvaluation.contractenddate.ToString("yyyyMMdd") & "',")
            sql.AppendLine("'" & SupplierEvaluation.contractsubject & "',")
            sql.AppendLine("'" & SupplierEvaluation.contractvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierEvaluation.iduser & "',")
            sql.AppendLine("'" & SupplierEvaluation.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el SupplierEvaluation. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un SupplierEvaluation por el Id
    ''' </summary>
    ''' <param name="idSupplierEvaluation"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSupplierEvaluation As Integer) As SupplierEvaluationEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSupplierEvaluation As New SupplierEvaluationEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SupplierEvaluation.*, ApplicationUser.Name AS ApplicationUserName ")
            sql.Append(" FROM SupplierEvaluation ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SupplierEvaluation.IdUser = ApplicationUser.Id ")
            sql.Append(" WHERE SupplierEvaluation.Id = " & idSupplierEvaluation)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

				' cargar los datos
				objSupplierEvaluation.id = data.Rows(0)("id")
				objSupplierEvaluation.idsupplier = data.Rows(0)("idsupplier")
				objSupplierEvaluation.contractnumber = data.Rows(0)("contractnumber")
				objSupplierEvaluation.contractstartdate = data.Rows(0)("contractstartdate")
				objSupplierEvaluation.contractenddate = data.Rows(0)("contractenddate")
				objSupplierEvaluation.contractsubject = data.Rows(0)("contractsubject")
				objSupplierEvaluation.contractvalue = data.Rows(0)("contractvalue")
				objSupplierEvaluation.iduser = data.Rows(0)("iduser")
                objSupplierEvaluation.createdate = data.Rows(0)("createdate")
                objSupplierEvaluation.USERNAME = data.Rows(0)("ApplicationUserName")

            End If

            ' retornar el objeto
            Return objSupplierEvaluation

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SupplierEvaluation. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSupplierEvaluation = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idsupplier"></param>
    ''' <param name="suppliername"></param>
    ''' <param name="contractnumber"></param>
    ''' <param name="contractstartdate"></param>
    ''' <param name="contractenddate"></param>
    ''' <param name="contractsubject"></param>
    ''' <param name="contractvalue"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of SupplierEvaluationEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idsupplier As String = "", _
        Optional ByVal suppliername As String = "", _
        Optional ByVal contractnumber As String = "", _
        Optional ByVal contractstartdate As String = "", _
        Optional ByVal contractenddate As String = "", _
        Optional ByVal contractsubject As String = "", _
        Optional ByVal contractvalue As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of SupplierEvaluationEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSupplierEvaluation As SupplierEvaluationEntity
        Dim SupplierEvaluationList As New List(Of SupplierEvaluationEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT SupplierEvaluation.*, ApplicationUser.Name AS ApplicationUserName, Third.Name as ThirdName ")
            sql.Append(" FROM SupplierEvaluation ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON SupplierEvaluation.IdUser = ApplicationUser.Id ")
            sql.Append(" INNER JOIN Third ON SupplierEvaluation.IdSupplier = Third.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " SupplierEvaluation.id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idsupplier.Equals("") Then

                sql.Append(where & " SupplierEvaluation.idsupplier = '" & idsupplier & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not suppliername.Equals("") Then

                sql.Append(where & " Third.Name like '%" & suppliername & "%'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not contractnumber.Equals("") Then

                sql.Append(where & " SupplierEvaluation.contractnumber like '%" & contractnumber & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractstartdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SupplierEvaluation.contractstartdate, 103) like '%" & contractstartdate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractenddate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SupplierEvaluation.contractenddate, 103) like '%" & contractenddate & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractsubject.Equals("") Then

                sql.Append(where & " SupplierEvaluation.contractsubject like '%" & contractsubject & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractvalue.Equals("") Then

                sql.Append(where & " SupplierEvaluation.contractvalue like '%" & contractvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " SupplierEvaluation.iduser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, SupplierEvaluation.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.Name ")
                    Case "suppliername"
                        sql.Append(" ORDER BY Third.Name ")
                    Case Else
                        ' ordernar
                        sql.Append(" ORDER BY SupplierEvaluation." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSupplierEvaluation = New SupplierEvaluationEntity

                ' cargar el valor del campo
                objSupplierEvaluation.id = row("id")
                objSupplierEvaluation.idsupplier = row("idsupplier")
                objSupplierEvaluation.contractnumber = row("contractnumber")
                objSupplierEvaluation.contractstartdate = row("contractstartdate")
                objSupplierEvaluation.contractenddate = row("contractenddate")
                objSupplierEvaluation.contractsubject = row("contractsubject")
                objSupplierEvaluation.contractvalue = row("contractvalue")
                objSupplierEvaluation.iduser = row("iduser")
                objSupplierEvaluation.createdate = row("createdate")
                objSupplierEvaluation.USERNAME = row("ApplicationUserName")
                objSupplierEvaluation.SUPPLIERNAME = row("ThirdName")

                ' agregar a la lista
                SupplierEvaluationList.Add(objSupplierEvaluation)

            Next

            ' retornar el objeto
            getList = SupplierEvaluationList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SupplierEvaluation. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objSupplierEvaluation = Nothing
            SupplierEvaluationList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo SupplierEvaluation
    ''' </summary>
    ''' <param name="SupplierEvaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal SupplierEvaluation As SupplierEvaluationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update SupplierEvaluation SET")
            sql.AppendLine(" idsupplier = '" & SupplierEvaluation.idsupplier & "',")
            SQL.AppendLine(" contractnumber = '" & SupplierEvaluation.contractnumber & "',")           
            sql.AppendLine(" contractstartdate = '" & SupplierEvaluation.contractstartdate.ToString("yyyyMMdd") & "',")
            sql.AppendLine(" contractenddate = '" & SupplierEvaluation.contractenddate.ToString("yyyyMMdd") & "',")
            SQL.AppendLine(" contractsubject = '" & SupplierEvaluation.contractsubject & "',")           
            sql.AppendLine(" contractvalue = '" & SupplierEvaluation.contractvalue.ToString().Replace(",", ".") & "',")
            SQL.AppendLine(" iduser = '" & SupplierEvaluation.iduser & "',")           
            sql.AppendLine(" createdate = '" & SupplierEvaluation.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            SQL.AppendLine("WHERE id = " & SupplierEvaluation.id)

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
            Throw New Exception("Error al modificar el SupplierEvaluation. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el SupplierEvaluation de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idSupplierEvaluation As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from SupplierEvaluation ")
            SQL.AppendLine(" where id = '" & idSupplierEvaluation & "' ")

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
            Throw New Exception("Error al elimiar el SupplierEvaluation. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
