Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ThirdByIdeaDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo ThirdByIdea
    ''' </summary>
    ''' <param name="ThirdByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ThirdByIdea As ThirdByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            'TODO: 18 MODIFICACION DE MODELO POR ACTORES
            'AUTOR: GERMAN RODRIGUEZ 19/06/2013 MGgroup

            ' construir la sentencia
            sql.AppendLine("INSERT INTO ThirdByIdea(" & _
             "ididea," & _
            "idthird," & _
            "type," & _
            "Vrmoney," & _
            "VrSpecies," & _
            "FSCorCounterpartContribution," & _
            "Name," & _
            "Contact," & _
            "Documents," & _
            "Phone," & _
            "Email," & _
            "CreateDate" & _
             ")")

            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ThirdByIdea.ididea & "',")
            sql.AppendLine("'" & ThirdByIdea.idthird & "',")
            sql.AppendLine("'" & ThirdByIdea.type & "',")
            sql.AppendLine("'" & ThirdByIdea.Vrmoney & "',")
            sql.AppendLine("'" & ThirdByIdea.VrSpecies & "',")
            sql.AppendLine("'" & ThirdByIdea.FSCorCounterpartContribution & "',")
            sql.AppendLine("'" & ThirdByIdea.Name & "',")
            sql.AppendLine("'" & ThirdByIdea.contact & "',")
            sql.AppendLine("'" & ThirdByIdea.Documents & "',")
            sql.AppendLine("'" & ThirdByIdea.Phone & "',")
            sql.AppendLine("'" & ThirdByIdea.Email & "',")
            sql.AppendLine("'" & ThirdByIdea.CreateDate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

            'TODO: 18 MODIFICACION DE MODELO POR ACTORES
            'AUTOR: GERMAN RODRIGUEZ 19/06/2013 MGgroup
            'cierre de cambio

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
            Throw New Exception("Error al insertar el ThirdByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ThirdByIdea por el Id
    ''' </summary>
    ''' <param name="idThirdByIdea"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByIdea As Integer) As ThirdByIdeaEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objThirdByIdea As New ThirdByIdeaEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM ThirdByIdea ")
            sql.Append(" WHERE Id = " & idThirdByIdea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objThirdByIdea.id = data.Rows(0)("id")
                objThirdByIdea.ididea = data.Rows(0)("ididea")
                objThirdByIdea.ididea = data.Rows(0)("idthird")
                objThirdByIdea.type = data.Rows(0)("type")
                objThirdByIdea.Vrmoney = data.Rows(0)("Vrmoney")
                objThirdByIdea.VrSpecies = data.Rows(0)("VrSpecies")
                objThirdByIdea.FSCorCounterpartContribution = data.Rows(0)("FSCorCounterpartContribution")


            End If

            ' retornar el objeto
            Return objThirdByIdea

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ThirdByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objThirdByIdea = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="ididea"></param>
    ''' <param name="idthird"></param>
    ''' <param name="type"></param>
    ''' <returns>un objeto de tipo List(Of ThirdByIdeaEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal ididea As String = "", _
        Optional ByVal idthird As String = "", _
        Optional ByVal type As String = "", _
        Optional ByVal Vrmoney As String = "", _
        Optional ByVal VrSpecies As String = "", _
        Optional ByVal FSCorCounterpartContribution As String = "", _
        Optional ByVal order As String = "") As List(Of ThirdByIdeaEntity)



        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objThirdByIdea As ThirdByIdeaEntity
        Dim ThirdByIdeaList As New List(Of ThirdByIdeaEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            'TODO: 19 modificacion de query por actualizacion de actores
            'AUTOR:German Rodriguez 05/07/2013 MGgroup
            sql.Append(" SELECT ThirdByIdea.Id,ThirdByIdea.IdIdea,ThirdByIdea.IdThird, ThirdByIdea.Type,ThirdByIdea.VrSpecies,ThirdByIdea.Vrmoney,ThirdByIdea.FSCorCounterpartContribution,Third.Id,Third.Code,ThirdByIdea.Name,ThirdByIdea.contact,ThirdByIdea.documents,ThirdByIdea.phone,ThirdByIdea.email,third.Enabled,Third.IdUser,third.CreateDate FROM ThirdByIdea  ")
            sql.Append(" INNER JOIN Third ON ThirdByIdea.idthird = Third.Id ")

            'TODO: 19 modificacion de query por actualizacion de actores
            'AUTOR:German Rodriguez 05/07/2013 MGgroup
            'cierre de cambio

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not ididea.Equals("") Then

                sql.Append(where & " ididea = '" & ididea & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idthird.Equals("") Then

                sql.Append(where & " idthird = '" & idthird & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not type.Equals("") Then

                sql.Append(where & " type like '%" & type & "%'")
                where = " AND "

            End If

            If Not Vrmoney.Equals("") Then

                sql.Append(where & " Idea.Vrmoney like '%" & Vrmoney.Trim() & "%'")
                where = " AND "

            End If

            If Not VrSpecies.Equals("") Then

                sql.Append(where & " Idea.VrSpecies like '%" & VrSpecies.Trim() & "%'")
                where = " AND "

            End If

            If Not FSCorCounterpartContribution.Equals("") Then

                sql.Append(where & " Idea.FSCorCounterpartContribution like '%" & FSCorCounterpartContribution.Trim() & "%'")
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
                objThirdByIdea = New ThirdByIdeaEntity

                ' cargar el valor del campo
                objThirdByIdea.id = row("id")
                objThirdByIdea.ididea = row("ididea")
                objThirdByIdea.idthird = row("idthird")
                objThirdByIdea.THIRD.id = row("idthird")
                objThirdByIdea.Name = row("name")
                objThirdByIdea.contact = IIf(Not IsDBNull(row("contact")), row("contact"), "")
                objThirdByIdea.Documents = IIf(Not IsDBNull(row("documents")), row("documents"), "")
                objThirdByIdea.Phone = IIf(Not IsDBNull(row("phone")), row("phone"), "")
                objThirdByIdea.Email = IIf(Not IsDBNull(row("email")), row("email"), "")
                objThirdByIdea.type = row("type")
                objThirdByIdea.Vrmoney = IIf(Not IsDBNull(row("Vrmoney")), row("Vrmoney"), 0)
                objThirdByIdea.VrSpecies = IIf(Not IsDBNull(row("VrSpecies")), row("VrSpecies"), 0)
                objThirdByIdea.FSCorCounterpartContribution = IIf(Not IsDBNull(row("FSCorCounterpartContribution")), row("FSCorCounterpartContribution"), 0)

                ' agregar a la lista
                ThirdByIdeaList.Add(objThirdByIdea)

            Next

            ' retornar el objeto
            getList = ThirdByIdeaList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ThirdByIdea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objThirdByIdea = Nothing
            ThirdByIdeaList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ThirdByIdea
    ''' </summary>
    ''' <param name="ThirdByIdea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ThirdByIdea As ThirdByIdeaEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ThirdByIdea SET")
            sql.AppendLine(" id = '" & ThirdByIdea.id & "',")
            sql.AppendLine(" ididea = '" & ThirdByIdea.ididea & "',")
            sql.AppendLine(" idthird = '" & ThirdByIdea.idthird & "',")
            sql.AppendLine(" type = '" & ThirdByIdea.type & "',")
            sql.AppendLine(" Vrmoney = " & ThirdByIdea.Vrmoney.ToString().Replace(",", ".") & ",") 'campo nuevo fase II german rodriguez
            sql.AppendLine(" VrSpecies = " & ThirdByIdea.VrSpecies.ToString().Replace(",", ".") & ",") 'campo nuevo fase II german rodriguez
            sql.AppendLine(" FSCorCounterpartContribution = " & ThirdByIdea.FSCorCounterpartContribution.ToString().Replace(",", ".")) 'campo nuevo fase II german rodriguez


            sql.AppendLine("WHERE id = " & ThirdByIdea.id)

            'Ejecutar la Instruccion
            GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

           
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "update")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al modificar el ThirdByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ThirdByIdea de una forma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idThirdByIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ThirdByIdea ")
            SQL.AppendLine(" where id = '" & idThirdByIdea & "' ")

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
            Throw New Exception("Error al elimiar el ThirdByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra los terceros por idea de una diea determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idIdea">identificador de la idea</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ThirdByIdea ")
            SQL.AppendLine(" where IdIdea = '" & idIdea & "' ")

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
            Throw New Exception("Error al elimiar el ThirdByIdea. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
