Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager


Public Class TestimonyDALC
    ' contantes
    Const MODULENAME As String = "TestimonyDALC"

    ''' <summary> 
    ''' Registar un nuevo Testimonio
    ''' </summary>
    ''' <param name="Testimony"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Testimony As TestimonyEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Testimony(" & _
             "idexecution," & _
             "name," & _
             "age," & _
             "sex," & _
             "phone," & _
             "idcity," & _
             "description," & _
             "email," & _
             "projectrole" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & Testimony.idexecution & "',")
            sql.AppendLine("'" & Testimony.name & "',")
            sql.AppendLine("'" & Testimony.age & "',")
            sql.AppendLine("'" & Testimony.sex & "',")
            sql.AppendLine("'" & Testimony.phone & "',")
            sql.AppendLine("'" & Testimony.idcity & "',")
            sql.AppendLine("'" & Testimony.description & "',")
            sql.AppendLine("'" & Testimony.email & "',")
            sql.AppendLine("'" & Testimony.projectrole & "')")

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
            Throw New Exception("Error al insertar el Testimony. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ThirdByIdea por el Id
    ''' </summary>
    ''' <param name="idTestimony"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idTestimony As Integer) As TestimonyEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objTestimony As New TestimonyEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM Testimony ")
            sql.Append(" WHERE Id = " & idTestimony)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objTestimony.id = data.Rows(0)("id")
                objTestimony.age = data.Rows(0)("age")
                objTestimony.description = data.Rows(0)("description")
                objTestimony.email = data.Rows(0)("email")
                objTestimony.idcity = data.Rows(0)("idcity")
                objTestimony.idexecution = data.Rows(0)("idexecution")
                objTestimony.sex = data.Rows(0)("sex")
                objTestimony.name = data.Rows(0)("name")
                objTestimony.phone = data.Rows(0)("phone")
                objTestimony.projectrole = data.Rows(0)("projectrole")

            End If

            ' retornar el objeto
            Return objTestimony

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Testimony. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objTestimony = Nothing

        End Try

    End Function
    ''' <summary>
    ''' Retorna los testimonios de una ejecución
    ''' </summary>
    ''' <param name="objApplicationCredentials"></param>
    ''' <param name="id"></param>
    ''' <param name="idexecution"></param>
    ''' <param name="name"></param>
    ''' <param name="age"></param>
    ''' <param name="sex"></param>
    ''' <param name="phone"></param>
    ''' <param name="idcity"></param>
    ''' <param name="description"></param>
    ''' <param name="email"></param>
    ''' <param name="projectrole"></param>
    ''' <param name="order"></param>
    ''' <returns>n objeto de tipo List(Of TestimonyList)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idexecution As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal age As String = "", _
        Optional ByVal sex As String = "", _
        Optional ByVal phone As String = "", _
        Optional ByVal idcity As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal email As String = "", _
        Optional ByVal projectrole As String = "", _
        Optional ByVal order As String = "") As List(Of TestimonyEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objTestimony As TestimonyEntity
        Dim TestimonyList As New List(Of TestimonyEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Testimony.Id, Testimony.IdExecution, Testimony.Name, Testimony.Age, Testimony.Sex, Testimony.Phone,")
            sql.Append("   Testimony.IdCity, Testimony.Description, Testimony.Email, Testimony.ProjectRole, Depto.Name AS Departamento ")
            sql.Append(" FROM Testimony INNER JOIN  " & dbSecurityName & ".dbo.City City ON Testimony.IdCity = City.ID INNER JOIN " & dbSecurityName & ".dbo.Depto Depto ON City.IDDepto = Depto.ID")


            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " id  = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " name like  '%" & name & "%")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not age.Equals("") Then

                sql.Append(where & " age like '%" & age & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not sex.Equals("") Then

                sql.Append(where & " sex like '%" & sex & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectrole.Equals("") Then

                sql.Append(where & " projectrole like '%" & projectrole & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not email.Equals("") Then

                sql.Append(where & "  email like '%" & email & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idexecution.Equals("") Then

                sql.Append(where & "  idexecution= '" & idexecution & "'")
                where = " AND "

            End If


            ' verificar si hay entrada de datos para el campo
            If Not idcity.Equals("") Then

                sql.Append(where & " idcity like '%" & idcity & "%'")
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
                objTestimony = New TestimonyEntity

                ' cargar el valor del campo
                objTestimony.id = row("id")
                objTestimony.age = row("age")
                objTestimony.description = row("description")
                objTestimony.email = row("email")
                objTestimony.idcity = row("idcity")
                objTestimony.idexecution = row("idexecution")
                objTestimony.sex = row("sex")
                objTestimony.name = row("name")
                objTestimony.phone = row("phone")
                objTestimony.projectrole = row("projectrole")
                objTestimony.DEPARTAMENTO = row("Departamento")
                ' agregar a la lista
                TestimonyList.Add(objTestimony)

            Next

            ' retornar el objeto
            getList = TestimonyList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de Testimony. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objTestimony = Nothing
            TestimonyList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Testimony
    ''' </summary>
    ''' <param name="Testimony"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Testimony As TestimonyEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Testimony SET")
            sql.AppendLine(" idexecution = '" & Testimony.idexecution & "',")
            sql.AppendLine(" name = '" & Testimony.name & "',")
            sql.AppendLine(" age = '" & Testimony.age & "',")
            sql.AppendLine(" sex = '" & Testimony.sex & "',")
            sql.AppendLine(" phone = '" & Testimony.phone & "',")
            sql.AppendLine(" idcity = '" & Testimony.idcity & "',")
            sql.AppendLine(" description = '" & Testimony.description & "',")
            sql.AppendLine(" email = '" & Testimony.email & "',")
            sql.AppendLine(" projecttole = '" & Testimony.projectrole & "' ")
            sql.AppendLine("WHERE id = " & Testimony.id)

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
            Throw New Exception("Error al modificar el Testimony. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ThirdByIdea de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idTestimony As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Testimony ")
            SQL.AppendLine(" where id = '" & idTestimony & "' ")

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
            Throw New Exception("Error al elimiar el Testimony. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra los testimonios por ejecución  determinada
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>
    ''' <param name="idExecution">identificador del registro de ejecución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteAll(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idExecution As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Testimony ")
            SQL.AppendLine(" where IdExecution = '" & idExecution & "' ")

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
            Throw New Exception("Error al elimiar el Testimony. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function
End Class
