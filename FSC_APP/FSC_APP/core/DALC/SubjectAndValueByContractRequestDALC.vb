Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SubjectAndValueByContractRequestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo SubjectAndValueByContractRequest
    ''' </summary>
    ''' <param name="SubjectAndValueByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal SubjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO SubjectAndValueByContractRequest(" & _
             "idcontractrequest," & _
             "subjectcontract," & _
             "productsordeliverables," & _
             "contractvalue," & _
             "contributionamount," & _
             "feesconsultantbyinstitution," & _
             "totalfeesintegralconsultant," & _
             "contributionamountrecipientinstitution," & _
             "idcurrency" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.idcontractrequest & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.subjectcontract & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.productsordeliverables & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.contractvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.contributionamount.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.feesconsultantbyinstitution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.totalfeesintegralconsultant.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.contributionamountrecipientinstitution.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SubjectAndValueByContractRequest.idcurrency & "')")

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
            Throw New Exception("Error al insertar el Objeto y valor de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un SubjectAndValueByContractRequest por el Id
    ''' </summary>
    ''' <param name="idContractRequest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idContractRequest As Integer) As SubjectAndValueByContractRequestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSubjectAndValueByContractRequest As New SubjectAndValueByContractRequestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM SubjectAndValueByContractRequest ")
            sql.Append(" WHERE IdContractRequest = " & idContractRequest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objSubjectAndValueByContractRequest.id = data.Rows(0)("id")
                objSubjectAndValueByContractRequest.idcontractrequest = data.Rows(0)("idcontractrequest")
                objSubjectAndValueByContractRequest.subjectcontract = data.Rows(0)("subjectcontract")
                objSubjectAndValueByContractRequest.productsordeliverables = data.Rows(0)("productsordeliverables")
                objSubjectAndValueByContractRequest.contractvalue = data.Rows(0)("contractvalue")
                objSubjectAndValueByContractRequest.contributionamount = data.Rows(0)("contributionamount")
                objSubjectAndValueByContractRequest.feesconsultantbyinstitution = data.Rows(0)("feesconsultantbyinstitution")
                objSubjectAndValueByContractRequest.totalfeesintegralconsultant = data.Rows(0)("totalfeesintegralconsultant")
                objSubjectAndValueByContractRequest.contributionamountrecipientinstitution = data.Rows(0)("contributionamountrecipientinstitution")
                objSubjectAndValueByContractRequest.idcurrency = data.Rows(0)("idcurrency")

            End If

            ' retornar el objeto
            Return objSubjectAndValueByContractRequest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar el Objeto y valor de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSubjectAndValueByContractRequest = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idcontractrequest"></param>
    ''' <param name="subjectcontract"></param>
    ''' <param name="productsordeliverables"></param>
    ''' <param name="contractvalue"></param>
    ''' <param name="contributionamount"></param>
    ''' <param name="feesconsultantbyinstitution"></param>
    ''' <param name="totalfeesintegralconsultant"></param>
    ''' <param name="contributionamountrecipientinstitution"></param>
    ''' <param name="idcurrency"></param>
    ''' <returns>un objeto de tipo List(Of SubjectAndValueByContractRequestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idcontractrequest As String = "", _
								Optional ByVal subjectcontract As String = "", _
								Optional ByVal productsordeliverables As String = "", _
								Optional ByVal contractvalue As String = "", _
								Optional ByVal contributionamount As String = "", _
								Optional ByVal feesconsultantbyinstitution As String = "", _
								Optional ByVal totalfeesintegralconsultant As String = "", _
								Optional ByVal contributionamountrecipientinstitution As String = "", _
								Optional ByVal idcurrency As String = "", _
								Optional order as string = "") As List(Of SubjectAndValueByContractRequestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSubjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity
        Dim SubjectAndValueByContractRequestList As New List(Of SubjectAndValueByContractRequestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            SQL.Append(" SELECT * ")
            SQL.Append(" FROM SubjectAndValueByContractRequest ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                SQL.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idcontractrequest.Equals("") Then

                SQL.Append(where & " idcontractrequest like '%" & idcontractrequest & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not subjectcontract.Equals("") Then

                SQL.Append(where & " subjectcontract like '%" & subjectcontract & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not productsordeliverables.Equals("") Then

                SQL.Append(where & " productsordeliverables like '%" & productsordeliverables & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not contractvalue.Equals("") Then

                SQL.Append(where & " contractvalue like '%" & contractvalue & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not contributionamount.Equals("") Then

                SQL.Append(where & " contributionamount like '%" & contributionamount & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not feesconsultantbyinstitution.Equals("") Then

                SQL.Append(where & " feesconsultantbyinstitution like '%" & feesconsultantbyinstitution & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not totalfeesintegralconsultant.Equals("") Then

                SQL.Append(where & " totalfeesintegralconsultant like '%" & totalfeesintegralconsultant & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not contributionamountrecipientinstitution.Equals("") Then

                SQL.Append(where & " contributionamountrecipientinstitution like '%" & contributionamountrecipientinstitution & "%'")
                where = " AND "

            End If
             
            ' verificar si hay entrada de datos para el campo
            If Not idcurrency.Equals("") Then

                SQL.Append(where & " idcurrency like '%" & idcurrency & "%'")
                where = " AND "

            End If
             
            If Not order.Equals(String.Empty) Then
            
				' ordernar
				SQL.Append(" ORDER BY " & order)
            
            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objSubjectAndValueByContractRequest = New SubjectAndValueByContractRequestEntity

				' cargar el valor del campo
				objSubjectAndValueByContractRequest.id = row("id")
				objSubjectAndValueByContractRequest.idcontractrequest = row("idcontractrequest")
				objSubjectAndValueByContractRequest.subjectcontract = row("subjectcontract")
				objSubjectAndValueByContractRequest.productsordeliverables = row("productsordeliverables")
				objSubjectAndValueByContractRequest.contractvalue = row("contractvalue")
				objSubjectAndValueByContractRequest.contributionamount = row("contributionamount")
				objSubjectAndValueByContractRequest.feesconsultantbyinstitution = row("feesconsultantbyinstitution")
				objSubjectAndValueByContractRequest.totalfeesintegralconsultant = row("totalfeesintegralconsultant")
				objSubjectAndValueByContractRequest.contributionamountrecipientinstitution = row("contributionamountrecipientinstitution")
				objSubjectAndValueByContractRequest.idcurrency = row("idcurrency")

                ' agregar a la lista
                SubjectAndValueByContractRequestList.Add(objSubjectAndValueByContractRequest)

            Next

            ' retornar el objeto
            getList = SubjectAndValueByContractRequestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista el Objetos y valores de la solicitud de contrato actual. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objSubjectAndValueByContractRequest = Nothing
            SubjectAndValueByContractRequestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo SubjectAndValueByContractRequest
    ''' </summary>
    ''' <param name="SubjectAndValueByContractRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal SubjectAndValueByContractRequest As SubjectAndValueByContractRequestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine("Update SubjectAndValueByContractRequest SET")
            sql.AppendLine(" idcontractrequest = '" & SubjectAndValueByContractRequest.idcontractrequest & "',")
            SQL.AppendLine(" subjectcontract = '" & SubjectAndValueByContractRequest.subjectcontract & "',")           
            SQL.AppendLine(" productsordeliverables = '" & SubjectAndValueByContractRequest.productsordeliverables & "',")           
            sql.AppendLine(" contractvalue = '" & SubjectAndValueByContractRequest.contractvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" contributionamount = '" & SubjectAndValueByContractRequest.contributionamount.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" feesconsultantbyinstitution = '" & SubjectAndValueByContractRequest.feesconsultantbyinstitution.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" totalfeesintegralconsultant = '" & SubjectAndValueByContractRequest.totalfeesintegralconsultant.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" contributionamountrecipientinstitution = '" & SubjectAndValueByContractRequest.contributionamountrecipientinstitution.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" idcurrency = '" & SubjectAndValueByContractRequest.idcurrency & "'")
            SQL.AppendLine("WHERE id = " & SubjectAndValueByContractRequest.id)

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
            Throw New Exception("Error al modificar el Objeto y valor de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el SubjectAndValueByContractRequest de una forma
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
            SQL.AppendLine(" Delete from SubjectAndValueByContractRequest ")
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
            Throw New Exception("Error al eliminar el Objeto y valor de la solicitud de contrato actual. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
