Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ResolvedInquestDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ResolvedInquest
    ''' </summary>
    ''' <param name="ResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ResolvedInquest As ResolvedInquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ResolvedInquest(" & _
             "idinquestcontent," & _
             "comments," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ResolvedInquest.idinquestcontent & "',")
            sql.AppendLine("'" & ResolvedInquest.comments & "',")
            sql.AppendLine("'" & ResolvedInquest.iduser & "',")
            sql.AppendLine("'" & ResolvedInquest.createdate.ToString("yyyyMMdd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar la encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ResolvedInquest por el Id
    ''' </summary>
    ''' <param name="idResolvedInquest"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idResolvedInquest As Integer) As ResolvedInquestEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objResolvedInquest As New ResolvedInquestEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ResolvedInquest ")
            sql.Append(" WHERE ResolvedInquest.Id = " & idResolvedInquest)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objResolvedInquest.id = data.Rows(0)("id")
                objResolvedInquest.idinquestcontent = data.Rows(0)("idinquestcontent")
                objResolvedInquest.comments = data.Rows(0)("comments")
                objResolvedInquest.iduser = data.Rows(0)("iduser")
                objResolvedInquest.createdate = data.Rows(0)("createdate")

            End If

            ' retornar el objeto
            Return objResolvedInquest

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una encuesta resuelta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objResolvedInquest = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idlike"></param>
    ''' <param name="idinquestcontent"></param>
    ''' <param name="comments"></param>
    ''' <param name="iduser"></param>
    ''' <param name="username"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ResolvedInquestEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idlike As String = "", _
        Optional ByVal idinquestcontent As String = "", _
        Optional ByVal idlikeinquestcontent As String = "", _
        Optional ByVal comments As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ResolvedInquestEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objResolvedInquest As ResolvedInquestEntity
        Dim ResolvedInquestList As New List(Of ResolvedInquestEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT ResolvedInquest.*, ApplicationUser.Name AS ApplicationUserName")
            sql.Append(" FROM ResolvedInquest ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser  ON ApplicationUser.Id = ResolvedInquest.IdUser ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " ResolvedInquest.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " ResolvedInquest.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idinquestcontent.Equals("") Then

                sql.Append(where & " ResolvedInquest.idinquestcontent = '" & idinquestcontent & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlikeinquestcontent.Equals("") Then

                sql.Append(where & " ResolvedInquest.idinquestcontent like '%" & idlikeinquestcontent & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not comments.Equals("") Then

                sql.Append(where & " ResolvedInquest.comments like '%" & comments & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " ResolvedInquest.iduser like '%" & iduser & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " ApplicationUser.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, ResolvedInquest.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY ApplicationUser.name ")
                    Case Else
                        sql.Append(" ORDER BY ResolvedInquest." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objResolvedInquest = New ResolvedInquestEntity

                ' cargar el valor del campo
                objResolvedInquest.id = row("id")
                objResolvedInquest.idinquestcontent = row("idinquestcontent")
                objResolvedInquest.comments = row("comments")
                objResolvedInquest.iduser = row("iduser")
                objResolvedInquest.createdate = row("createdate")
                objResolvedInquest.USERNAME = row("ApplicationUserName")

                ' agregar a la lista
                ResolvedInquestList.Add(objResolvedInquest)

            Next

            ' retornar el objeto
            getList = ResolvedInquestList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de encuestas resuelta.")

        Finally
            ' liberando recursos
            sql = Nothing
            objResolvedInquest = Nothing
            ResolvedInquestList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ResolvedInquest
    ''' </summary>
    ''' <param name="ResolvedInquest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ResolvedInquest As ResolvedInquestEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ResolvedInquest SET")
            sql.AppendLine(" idinquestcontent = '" & ResolvedInquest.idinquestcontent & "',")
            sql.AppendLine(" comments = '" & ResolvedInquest.comments & "',")
            sql.AppendLine(" iduser = '" & ResolvedInquest.iduser & "',")
            sql.AppendLine(" createdate = '" & ResolvedInquest.createdate.ToString("yyyyMMdd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE id = " & ResolvedInquest.id)

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
            Throw New Exception("Error al modificar la encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ResolvedInquest de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idResolvedInquest As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ResolvedInquest ")
            SQL.AppendLine(" where id = '" & idResolvedInquest & "' ")

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
            Throw New Exception("Error al elimiar la encuesta resuelta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
