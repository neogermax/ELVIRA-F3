Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProgramComponentDALC

    ' contantes
    Const MODULENAME As String = "ProgramComponentDALC"


    ''' <summary> 
    ''' Verifica si ya existe el código
    ''' Devuelve Verdadero si existe algún registro con el mismo código y diferente id(opcional)
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verifyCode(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
                                ByVal code As String, _
                                Optional ByVal id As String = "") As Boolean

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try

            ' Evitar que se repitan registros con el mismo Codigo
            If id.Equals("") Then

                'Se usa antes de ingresar un nuevo registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM ProgramComponent WHERE Code = '" & code & "'")

            Else
                'Se usa antes de actualizar un registro
                sql.AppendLine("SELECT COUNT(Code) AS cont FROM ProgramComponent WHERE Code = '" & code & "' AND id <> '" & id & "'")

            End If

            ' ejecutar la consulta
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString())

            If dtData.Rows.Count > 0 Then

                If CLng(dtData.Rows(0)(0)) = 0 Then

                    ' retornar que no existe
                    verifyCode = False

                Else

                    ' retornar que existe
                    verifyCode = True

                End If

            End If

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "verifyCode")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al verificar el código de ENTERPRISE. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try
    End Function

    ''' <summary> 
    ''' Registar un nuevo ProgramComponent
    ''' </summary>
    ''' <param name="ProgramComponent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal ProgramComponent As ProgramComponentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO ProgramComponent(" & _
             "code," & _
             "name," & _
             "description," & _
             "idProgram," & _
             "idresponsible," & _
             "enabled," & _
             "iduser," & _
             "createdate" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & ProgramComponent.code & "',")
            sql.AppendLine("'" & ProgramComponent.name & "',")
            sql.AppendLine("'" & ProgramComponent.description & "',")
            sql.AppendLine("'" & ProgramComponent.idProgram & "',")
            sql.AppendLine("'" & ProgramComponent.idresponsible & "',")
            sql.AppendLine("'" & ProgramComponent.enabled & "',")
            sql.AppendLine("'" & ProgramComponent.iduser & "',")
            sql.AppendLine("'" & ProgramComponent.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "')")

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
            Throw New Exception("Error al insertar el ProgramComponent. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un ProgramComponent por el Id
    ''' </summary>
    ''' <param name="idProgramComponent"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponent As Integer) As ProgramComponentEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProgramComponent As New ProgramComponentEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT ProgramComponent.*, ApplicationUser.Name AS userName ")
            sql.Append(" FROM ProgramComponent INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON ProgramComponent.IdUser = ApplicationUser.ID ")
            sql.Append(" WHERE ProgramComponent.Id = " & idProgramComponent)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProgramComponent.id = data.Rows(0)("id")
                objProgramComponent.code = data.Rows(0)("code")
                objProgramComponent.name = data.Rows(0)("name")
                objProgramComponent.description = data.Rows(0)("description")
                objProgramComponent.idProgram = data.Rows(0)("idProgram")
                objProgramComponent.idresponsible = data.Rows(0)("idresponsible")
                objProgramComponent.enabled = data.Rows(0)("enabled")
                objProgramComponent.iduser = data.Rows(0)("iduser")
                objProgramComponent.createdate = data.Rows(0)("createdate")
                objProgramComponent.USERNAME = data.Rows(0)("userName")

            End If

            ' retornar el objeto
            Return objProgramComponent

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un ProgramComponent. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProgramComponent = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="code"></param>
    ''' <param name="name"></param>
    ''' <param name="description"></param>
    ''' <param name="idProgram"></param>
    ''' <param name="idresponsible"></param>
    ''' <param name="enabled"></param>
    ''' <param name="enabledtext"></param>
    ''' <param name="iduser"></param>
    ''' <param name="createdate"></param>
    ''' <returns>un objeto de tipo List(Of ProgramComponentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
                                Optional ByVal idlike As String = "", _
        Optional ByVal code As String = "", _
        Optional ByVal name As String = "", _
        Optional ByVal description As String = "", _
        Optional ByVal idProgram As String = "", _
        Optional ByVal Programname As String = "", _
        Optional ByVal idresponsible As String = "", _
        Optional ByVal responsiblename As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal enabledtext As String = "", _
        Optional ByVal iduser As String = "", _
        Optional ByVal username As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal order As String = "") As List(Of ProgramComponentEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProgramComponent As ProgramComponentEntity
        Dim ProgramComponentList As New List(Of ProgramComponentEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT spa.*, apu.Name AS userName, mac.Name AS ProgramName, ")
            sql.Append(" (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser WHERE (spa.IdResponsible = ID)) AS responsibleName ")
            sql.Append(" FROM ProgramComponent AS spa INNER JOIN ")
            sql.Append(" " & dbSecurityName & ".dbo.ApplicationUser AS apu ON spa.IdUser = apu.ID INNER JOIN ")
            sql.Append(" Program AS mac ON spa.IdProgram = mac.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " spa.id = '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idlike.Equals("") Then

                sql.Append(where & " spa.id like '%" & idlike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not code.Equals("") Then

                sql.Append(where & " spa.code like '%" & code & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not name.Equals("") Then

                sql.Append(where & " spa.name like '%" & name & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not description.Equals("") Then

                sql.Append(where & " spa.description like '%" & description & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idProgram.Equals("") Then

                '                Dim arraycomponente As String()
                '               Dim t_componente As Integer
                '              Dim contador As Integer = 0

                ' arraycomponente = idProgram.Split(New [Char]() {","c})

                'ASIGNAMOS EL TAMAÑO 
                't_componente = arraycomponente.Length

                sql.Append(where & " spa.IdProgram IN (")

                ' For index_com As Integer = 0 To t_componente


                'If t_componente = contador Then

                sql.Append("'" & idProgram & "'")

                'Else

                ' sql.Append("'" & arraycomponente(contador) & "',")

                ' End If
                '   contador = contador + 1
                '' Next

                sql.Append(")")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not Programname.Equals("") Then

                sql.Append(where & " mac.Name like '%" & Programname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idresponsible.Equals("") Then

                sql.Append(where & " spa.IdResponsible = '" & idresponsible & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not responsiblename.Equals("") Then

                sql.Append(where & " (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser WHERE (spa.IdResponsible = ID)) like '%" & responsiblename & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " spa.enabled = '" & enabled & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabledtext.Equals("") Then

                sql.Append(where & " spa.enabled IN ")
                sql.Append(" (SELECT Value FROM (SELECT 'habilitado' AS Estate, 1 AS Value ")
                sql.Append(" UNION SELECT 'deshabilitado' AS Estate, 0 AS Value) Temp ")
                sql.Append(" WHERE Estate LIKE '%" & enabledtext & "%') ")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not iduser.Equals("") Then

                sql.Append(where & " spa.IdUser = '" & iduser & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not username.Equals("") Then

                sql.Append(where & " apu.Name like '%" & username & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, spa.createdate, 103) like '%" & createdate & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "username"
                        sql.Append(" ORDER BY apu.Name ")
                    Case "Programname"
                        sql.Append(" ORDER BY mac.Name ")
                    Case "responsiblename"
                        sql.Append(" ORDER BY (SELECT Name FROM " & dbSecurityName & ".dbo.ApplicationUser WHERE (spa.IdResponsible = ID)) ")
                    Case Else
                        sql.Append(" ORDER BY spa." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProgramComponent = New ProgramComponentEntity

                ' cargar el valor del campo
                objProgramComponent.id = row("id")
                objProgramComponent.code = row("code")
                objProgramComponent.name = row("name")
                objProgramComponent.description = row("description")
                objProgramComponent.idProgram = row("idProgram")
                objProgramComponent.idresponsible = row("idresponsible")
                objProgramComponent.enabled = row("enabled")
                objProgramComponent.iduser = row("iduser")
                objProgramComponent.createdate = row("createdate")
                objProgramComponent.USERNAME = row("userName")
                objProgramComponent.ProgramNAME = row("ProgramName")
                objProgramComponent.RESPONSIBLENAME = row("responsibleName")

                ' agregar a la lista
                ProgramComponentList.Add(objProgramComponent)

            Next

            ' retornar el objeto
            getList = ProgramComponentList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de ProgramComponent. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProgramComponent = Nothing
            ProgramComponentList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo ProgramComponent
    ''' </summary>
    ''' <param name="ProgramComponent"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal ProgramComponent As ProgramComponentEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update ProgramComponent SET")
            sql.AppendLine(" code = '" & ProgramComponent.code & "',")
            sql.AppendLine(" name = '" & ProgramComponent.name & "',")
            sql.AppendLine(" description = '" & ProgramComponent.description & "',")
            sql.AppendLine(" idProgram = '" & ProgramComponent.idProgram & "',")
            sql.AppendLine(" idresponsible = '" & ProgramComponent.idresponsible & "',")
            sql.AppendLine(" enabled = '" & ProgramComponent.enabled & "',")
            sql.AppendLine(" iduser = '" & ProgramComponent.iduser & "',")
            sql.AppendLine(" createdate = '" & ProgramComponent.createdate.ToString("yyyy/MM/dd HH:mm:ss") & "'")
            sql.AppendLine("WHERE id = " & ProgramComponent.id)

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
            Throw New Exception("Error al modificar el ProgramComponent. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el ProgramComponent de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProgramComponent As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from ProgramComponent ")
            SQL.AppendLine(" where id = '" & idProgramComponent & "' ")

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
            Throw New Exception("Error al elimiar el ProgramComponent. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Metodo creado por Jose Olmes Torres Abril 28 de 2010
    ''' Carga una lista de Componentes del Programa segun una linea estrategica dado
    ''' </summary>
    ''' <param name="objApplicationCredentials">Credenciales del usuario</param>   
    ''' <param name="idStrategicLine">identificador de la linea estrategica</param>   
    ''' <returns>un objeto de tipo List(Of ProgramComponentEntity)</returns>
    ''' <remarks></remarks>
    Public Function getListByStrategicLine(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        ByVal idStrategicLine As String) As List(Of ProgramComponentEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProgramComponent As ProgramComponentEntity
        Dim ProgramComponentList As New List(Of ProgramComponentEntity)
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT ProgramComponent.id, ProgramComponent.Name, ProgramComponent.Code")
            sql.Append(" FROM  ProgramComponent ")
            sql.Append(" INNER JOIN Program ON ProgramComponent.IdProgram = Program.Id")
            sql.Append(" INNER JOIN StrategicLine  ON Program.IdStrategicLine = StrategicLine.Id")
            sql.Append(" WHERE StrategicLine.Id = " & idStrategicLine & " AND ProgramComponent.Enabled=1")
            sql.Append(" GROUP BY ProgramComponent.id, ProgramComponent.Name, ProgramComponent.Code")
            sql.Append(" ORDER BY ProgramComponent.Name")

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objProgramComponent = New ProgramComponentEntity

                ' cargar el valor del campo
                objProgramComponent.id = row("id")
                objProgramComponent.name = row("name")
                objProgramComponent.code = row("code")

                ' agregar a la lista
                ProgramComponentList.Add(objProgramComponent)

            Next

            ' retornar el objeto
            getListByStrategicLine = ProgramComponentList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de StrategicActivity. ")

        Finally
            ' liberando recursos
            sql = Nothing
            objProgramComponent = Nothing
            ProgramComponentList = Nothing
            data = Nothing

        End Try

    End Function


End Class
