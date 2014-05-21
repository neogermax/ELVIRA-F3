Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ThirdByProjectDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ThirdByProject
    ''' </summary>
    ''' <param name="ThirdByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
						ByVal ThirdByProject As ThirdByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            '"ActorName," & _
            '"actions," & _
            '"experiences," & _
            sql.AppendLine("INSERT INTO ThirdByProject(" & _
             "idproject," & _
             "idThird, " & _
             "Type," & _
             "Vrmoney," & _
             "VrSpecies," & _
             "FSCorCounterpartContribution," & _
             "Name," & _
             "Contact," & _
             "Documents," & _
             "Phone," & _
             "Email," & _
             "generatesflow," & _
             "CreateDate" & _
             ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ThirdByProject.idproject & "',")
            sql.AppendLine("'" & ThirdByProject.idthird & "',")
            sql.AppendLine("'" & ThirdByProject.type & "',")
            sql.AppendLine("'" & ThirdByProject.Vrmoney & "',")
            sql.AppendLine("'" & ThirdByProject.VrSpecies & "',")
            sql.AppendLine("'" & ThirdByProject.FSCorCounterpartContribution & "',")
            sql.AppendLine("'" & ThirdByProject.Name & "',")
            sql.AppendLine("'" & ThirdByProject.contact & "',")
            sql.AppendLine("'" & ThirdByProject.Documents & "',")
            sql.AppendLine("'" & ThirdByProject.Phone & "',")
            sql.AppendLine("'" & ThirdByProject.Email & "',")
            sql.AppendLine("'" & ThirdByProject.EstadoFlujos & "',")
            sql.AppendLine("'" & ThirdByProject.CreateDate.ToString("yyyy/MM/dd HH:mm:ss") & "')")


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
            Throw New Exception("Error al insertar el ThirdByProject. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function
   
    ''' <summary>
    ''' Cargar un ThirdByProject por el Id
    ''' </summary>
    ''' <param name="idThirdByProject"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal idThirdByProject As Integer) As ThirdByProjectEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objThirdByProject As New ThirdByProjectEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT ThirdByProject.Id, ThirdByProject.IdProject,  ")
            sql.Append("    ThirdByProject.Actions, ThirdByProject.Experiences, ThirdByProject.Type, ThirdByProject.ActorName AS thirdname ")
            sql.Append(" FROM ThirdByProject  ")
            ' sql.Append("    Third ON ThirdByProject.IdThird = Third.Id ")
            sql.Append(" WHERE ThirdByProject.Id = " & idThirdByProject)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objThirdByProject.id = data.Rows(0)("id")
                objThirdByProject.idproject = data.Rows(0)("idproject")
                objThirdByProject.idthird = data.Rows(0)("idthird")
                'objThirdByProject.actions = data.Rows(0)("actions")
                'objThirdByProject.experiences = data.Rows(0)("experiences")
                objThirdByProject.type = data.Rows(0)("type")
                'objThirdByProject.THIRDNAME = data.Rows(0)("thirdname")

            End If

            ' retornar el objeto
            Return objThirdByProject

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ThirdByProject. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objThirdByProject = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idproject"></param>
    ''' <param name="idthird"></param>
    ''' <param name="actions"></param>
    ''' <param name="experiences"></param>
    ''' <param name="type"></param>
    ''' <returns>un objeto de tipo List(Of ThirdByProjectEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList( ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
								Optional ByVal id As String = "", _
								Optional ByVal idproject As String = "", _
								Optional ByVal idthird As String = "", _
								Optional ByVal actions As String = "", _
								Optional ByVal experiences As String = "", _
								Optional ByVal type As String = "", _
								Optional order as string = "") As List(Of ThirdByProjectEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objThirdByProject As ThirdByProjectEntity
        Dim ThirdByProjectList As New List(Of ThirdByProjectEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            'TODO:35 modificacion de query 
            'Autor: german rodriguez 19/07/2013
            'descripcion: modificacion de query de proyecto para actores por cambio de modelo de datos

            ' construir la sentencia
            sql.Append(" select TP.Id,tp.IdThird, t.Name as thirdname, tp.Type, t.contact,t.documents, t.phone, t.email, TP.IdProject   ")
            sql.Append(" FROM ThirdByProject tp  ")
            sql.Append(" inner join Project p on p.id = tp.IdProject ")
            sql.Append(" inner join third t on t.Id= tp.IdThird ")

            'TODO:35 modificacion de query 
            'Autor: german rodriguez 19/07/2013
            'cierre de cambio


            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idproject.Equals("") Then

                sql.Append(where & " idproject = '" & idproject & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idthird.Equals("") Then

                sql.Append(where & " idthird = '" & idthird & "'")
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
                objThirdByProject = New ThirdByProjectEntity

                ' cargar el valor del campo
                objThirdByProject.id = row("id")
                objThirdByProject.idproject = row("idproject")
                objThirdByProject.idthird = row("idthird")
                objThirdByProject.type = row("type")
                objThirdByProject.THIRDNAME = row("thirdname")
                objThirdByProject.THIRD.contact = IIf(Not IsDBNull(row("contact")), row("contact"), "")
                objThirdByProject.THIRD.documents = IIf(Not IsDBNull(row("documents")), row("documents"), "")
                objThirdByProject.THIRD.phone = IIf(Not IsDBNull(row("phone")), row("phone"), "")
                objThirdByProject.THIRD.email = IIf(Not IsDBNull(row("email")), row("email"), "")



                'objThirdByProject.actions = row("actions")
                'objThirdByProject.experiences = row("experiences")
                
                ' agregar a la lista
                ThirdByProjectList.Add(objThirdByProject)

            Next

            ' retornar el objeto
            getList = ThirdByProjectList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ThirdByProject. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objThirdByProject = Nothing
            ThirdByProjectList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Modificar un objeto de tipo ThirdByProject
    ''' </summary>
    ''' <param name="ThirdByProject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
							ByVal ThirdByProject As ThirdByProjectEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ThirdByProject SET")
            sql.AppendLine(" idThird = '" & ThirdByProject.idproject & "',")
            sql.AppendLine(" idproject = '" & ThirdByProject.idproject & "',")
            'sql.AppendLine(" ActorName = '" & ThirdByProject.THIRDNAME & "',")
            'SQL.AppendLine(" actions = '" & ThirdByProject.actions & "',")           
            'SQL.AppendLine(" experiences = '" & ThirdByProject.experiences & "',")           
            'sql.AppendLine(" type = '" & ThirdByProject.type & "'")
            SQL.AppendLine("WHERE id = " & ThirdByProject.id)

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
            Throw New Exception("Error al modificar el ThirdByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
    
    ''' <summary>
    ''' Borra el ThirdByProject de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByProject As Integer, _
       Optional ByVal idProject As Integer = 0) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ThirdByProject ")
            If idProject = 0 Then
                SQL.AppendLine(" where id = '" & idThirdByProject & "' ")
            Else
                SQL.AppendLine(" where idProject = '" & idProject & "' ")
            End If

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
            Throw New Exception("Error al elimiar el ThirdByProject. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
           

End Class
