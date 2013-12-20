Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class ProposalDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo Proposal
    ''' </summary>
    ''' <param name="Proposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal Proposal As ProposalEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Proposal(" & _
             "idsummoning," & _
             "operator," & _
             "operatornit," & _
             "projectname," & _
             "target," & _
             "targetpopulation," & _
             "expectedresults," & _
             "totalvalue," & _
             "inputfsc," & _
             "inputothersources," & _
             "briefprojectdescription," & _
             "score," & _
             "result," & _
             "responsiblereview," & _
             "reviewdate," & _
             "enabled," & _
             "createdate," & _
             "IdProcessInstance," & _
             "IdActivityInstance," & _
             "IdUser" & _
            ")")
            sql.AppendLine("VALUES (")
            'Se verifica si el campo convocatoria tiene un valor válido
            If (Proposal.idsummoning = 0) Then
                sql.AppendLine("NULL,")
            Else
                sql.AppendLine("'" & Proposal.idsummoning & "',")
            End If
            sql.AppendLine("'" & Proposal.nameOperator & "',")
            sql.AppendLine("'" & Proposal.operatornit & "',")
            sql.AppendLine("'" & Proposal.projectname & "',")
            sql.AppendLine("'" & Proposal.target & "',")
            sql.AppendLine("'" & Proposal.targetpopulation & "',")
            sql.AppendLine("'" & Proposal.expectedresults & "',")
            sql.AppendLine("'" & Proposal.totalvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Proposal.inputfsc.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Proposal.inputothersources.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Proposal.briefprojectdescription & "',")
            sql.AppendLine("'" & Proposal.score.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & Proposal.result & "',")
            sql.AppendLine("'" & Proposal.responsiblereview & "',")
            If (Proposal.reviewdate > CDate("1900/01/01")) Then
                sql.AppendLine("'" & Proposal.reviewdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            Else
                sql.AppendLine("NULL,")
            End If
            sql.AppendLine("'" & Proposal.enabled & "',")
            sql.AppendLine("'" & Proposal.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine("'" & Proposal.IdProcessInstance & "',")
            sql.AppendLine("'" & Proposal.IdActivityInstance & "',")
            sql.AppendLine("'" & Proposal.iduser & "')")
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
            Throw New Exception("Error al insertar la propuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un Proposal por el Id
    ''' </summary>
    ''' <param name="idProposal"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProposal As Integer) As ProposalEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objProposal As New ProposalEntity
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT Proposal.* , ApplicationUser.Name AS ApplicationUserName, p.Name AS UserName ")
            sql.Append(" FROM Proposal ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser  ON Proposal.responsiblereview = ApplicationUser.Id  ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser as p ON Proposal.IdUser=p.Id ")
            sql.Append(" WHERE Proposal.Id = " & idProposal)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objProposal.id = data.Rows(0)("id")
                If Not (IsDBNull(data.Rows(0)("idsummoning"))) Then objProposal.idsummoning = data.Rows(0)("idsummoning")
                objProposal.nameOperator = data.Rows(0)("operator")
                objProposal.operatornit = data.Rows(0)("operatornit")
                objProposal.projectname = data.Rows(0)("projectname")
                objProposal.target = data.Rows(0)("target")
                objProposal.targetpopulation = data.Rows(0)("targetpopulation")
                objProposal.expectedresults = data.Rows(0)("expectedresults")
                objProposal.totalvalue = data.Rows(0)("totalvalue")
                objProposal.inputfsc = data.Rows(0)("inputfsc")
                objProposal.inputothersources = data.Rows(0)("inputothersources")
                objProposal.briefprojectdescription = data.Rows(0)("briefprojectdescription")
                objProposal.score = data.Rows(0)("score")
                objProposal.result = data.Rows(0)("result")
                objProposal.responsiblereview = data.Rows(0)("responsiblereview")
                If Not (IsDBNull(data.Rows(0)("reviewdate"))) Then objProposal.reviewdate = data.Rows(0)("reviewdate")
                objProposal.enabled = data.Rows(0)("enabled")
                objProposal.createdate = data.Rows(0)("createdate")
                If Not (IsDBNull(data.Rows(0)("ApplicationUserName"))) Then objProposal.RESPONSIBLEREVIEWNAME = data.Rows(0)("ApplicationUserName")
                objProposal.IdProcessInstance = data.Rows(0)("IdProcessInstance")
                objProposal.IdActivityInstance = data.Rows(0)("IdActivityInstance")
                objProposal.iduser = data.Rows(0)("IdUser")
                objProposal.USERNAME = data.Rows(0)("UserName")


            End If

            ' retornar el objeto
            Return objProposal

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar una propuesta.")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objProposal = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idLike"></param>
    ''' <param name="idsummoning"></param>
    ''' <param name="summoningName"></param>
    ''' <param name="nameOperator"></param>
    ''' <param name="operatornit"></param>
    ''' <param name="projectname"></param>
    ''' <param name="target"></param>
    ''' <param name="targetpopulation"></param>
    ''' <param name="expectedresults"></param>
    ''' <param name="totalvalue"></param>
    ''' <param name="inputfsc"></param>
    ''' <param name="inputothersources"></param>
    ''' <param name="briefprojectdescription"></param>
    ''' <param name="score"></param>
    ''' <param name="result"></param>
    ''' <param name="responsiblereview"></param>
    ''' <param name="reviewdate"></param>
    ''' <param name="enabled"></param>
    ''' <param name="createdate"></param>
    ''' <param name="deptoName"></param>
    ''' <param name="cityName"></param>
    ''' <returns>un objeto de tipo List(Of ProposalEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idLike As String = "", _
        Optional ByVal idsummoning As String = "", _
        Optional ByVal summoningName As String = "", _
        Optional ByVal nameOperator As String = "", _
        Optional ByVal operatornit As String = "", _
        Optional ByVal projectname As String = "", _
        Optional ByVal target As String = "", _
        Optional ByVal targetpopulation As String = "", _
        Optional ByVal expectedresults As String = "", _
        Optional ByVal totalvalue As String = "", _
        Optional ByVal inputfsc As String = "", _
        Optional ByVal inputothersources As String = "", _
        Optional ByVal briefprojectdescription As String = "", _
        Optional ByVal score As String = "", _
        Optional ByVal result As String = "", _
        Optional ByVal responsiblereview As String = "", _
        Optional ByVal reviewdate As String = "", _
        Optional ByVal enabled As String = "", _
        Optional ByVal createdate As String = "", _
        Optional ByVal deptoName As String = "", _
        Optional ByVal cityName As String = "", _
        Optional ByVal order As String = "") As List(Of ProposalEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objProposal As ProposalEntity
        Dim ProposalList As New List(Of ProposalEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")
        Try
            ' construir la sentencia
            sql.Append(" SELECT Proposal.*, Summoning.Name  AS  summoningName, Depto.Name AS deptoName, City.Name AS cityName, ApplicationUser.Name AS USERNAME, ApplicationUser.Id AS IdUser")
            sql.Append(" FROM Proposal ")
            sql.Append(" INNER JOIN " & dbSecurityName & ".dbo.ApplicationUser ApplicationUser ON Proposal.IdUser=ApplicationUser.Id ")
            sql.Append(" LEFT OUTER JOIN Summoning ON Proposal.IdSummoning = Summoning.Id  ")
            sql.Append(" LEFT OUTER JOIN LocationByProposal ON Proposal.Id = LocationByProposal.IdProposal ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.Depto Depto ON LocationByProposal.IdDepto = Depto.Id  ")
            sql.Append(" LEFT OUTER JOIN " & dbSecurityName & ".dbo.City City  ON LocationByProposal.IdCity = City.Id ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                sql.Append(where & " Proposal.id = '" & id & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idLike.Equals("") Then

                sql.Append(where & " Proposal.id like '%" & idLike & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idsummoning.Equals("") Then

                sql.Append(where & " Proposal.idsummoning = '" & idsummoning & "'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not summoningName.Equals("") Then

                sql.Append(where & " Summoning.Name like '%" & summoningName & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not nameOperator.Equals("") Then

                sql.Append(where & " Proposal.operator like '%" & nameOperator & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not operatornit.Equals("") Then

                sql.Append(where & " Proposal.operatornit like '%" & operatornit & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectname.Equals("") Then

                sql.Append(where & " Proposal.projectname like '%" & projectname & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not target.Equals("") Then

                sql.Append(where & " Proposal.target like '%" & target & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not targetpopulation.Equals("") Then

                sql.Append(where & " Proposal.targetpopulation like '%" & targetpopulation & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not expectedresults.Equals("") Then

                sql.Append(where & " Proposal.expectedresults like '%" & expectedresults & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not totalvalue.Equals("") Then

                sql.Append(where & " Proposal.totalvalue like '%" & totalvalue & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not inputfsc.Equals("") Then

                sql.Append(where & " Proposal.inputfsc like '%" & inputfsc & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not inputothersources.Equals("") Then

                sql.Append(where & " Proposal.inputothersources like '%" & inputothersources & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not briefprojectdescription.Equals("") Then

                sql.Append(where & " Proposal.briefprojectdescription like '%" & briefprojectdescription & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not score.Equals("") Then

                sql.Append(where & " Proposal.score like '%" & score & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not result.Equals("") Then

                sql.Append(where & " Proposal.result like '%" & result & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not responsiblereview.Equals("") Then

                sql.Append(where & " Proposal.responsiblereview like '%" & responsiblereview & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not reviewdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Proposal.reviewdate, 103) like '%" & reviewdate.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not enabled.Equals("") Then

                sql.Append(where & " Proposal.enabled like '%" & enabled & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not createdate.Equals("") Then

                sql.Append(where & " CONVERT(NVARCHAR, Proposal.createdate, 103) like '%" & createdate.Trim() & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not deptoName.Equals("") Then

                sql.Append(where & " Depto.Name like '%" & deptoName & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not cityName.Equals("") Then

                sql.Append(where & " City.Name like '%" & cityName & "%'")
                where = " AND "

            End If

            If Not order.Equals(String.Empty) Then

                ' ordernar
                Select Case order
                    Case "summoningName"
                        sql.Append(" ORDER BY Summoning.Name ")
                    Case "deptoName"
                        sql.Append(" ORDER BY Depto.Name ")
                    Case "cityName"
                        sql.Append(" ORDER BY City.Name ")
                    Case Else
                        sql.Append(" ORDER BY Proposal." & order)
                End Select

            End If

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            Dim miListaIds As String = ""

            For Each row As DataRow In data.Rows

                'Se valida que el Id ya no haya sido agregado
                If Not (miListaIds.Contains(row("id").ToString())) Then

                    ' cargar los datos
                    objProposal = New ProposalEntity

                    ' cargar el valor del campo
                    objProposal.id = row("id")
                    If Not (IsDBNull(row("idsummoning"))) Then objProposal.idsummoning = row("idsummoning")
                    objProposal.nameOperator = row("operator")
                    objProposal.operatornit = row("operatornit")
                    objProposal.projectname = row("projectname")
                    objProposal.target = row("target")
                    objProposal.targetpopulation = row("targetpopulation")
                    objProposal.expectedresults = row("expectedresults")
                    objProposal.totalvalue = row("totalvalue")
                    objProposal.inputfsc = row("inputfsc")
                    objProposal.inputothersources = row("inputothersources")
                    objProposal.briefprojectdescription = row("briefprojectdescription")
                    objProposal.score = row("score")
                    objProposal.result = row("result")
                    objProposal.responsiblereview = row("responsiblereview")
                    If Not (IsDBNull(row("reviewdate"))) Then objProposal.reviewdate = row("reviewdate")
                    objProposal.enabled = row("enabled")
                    objProposal.createdate = row("createdate")
                    If Not (IsDBNull(row("summoningName"))) Then objProposal.SUMMONINGNAME = row("summoningName")
                    If Not (IsDBNull(row("deptoName"))) Then objProposal.DEPTONAME = row("deptoName")
                    If Not (IsDBNull(row("cityName"))) Then objProposal.CITYNAME = row("cityName")
                    objProposal.IdProcessInstance = row("IdProcessInstance")
                    objProposal.IdActivityInstance = row("IdActivityInstance")
                    objProposal.USERNAME = row("UserName")
                    objProposal.iduser = row("IdUser")
                    ' agregar a la lista
                    ProposalList.Add(objProposal)

                End If

                'Se agrega el Id a la lista
                miListaIds &= row("id").ToString() & ","

            Next

            ' retornar el objeto
            getList = ProposalList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de propuestas.")

        Finally
            ' liberando recursos
            sql = Nothing
            objProposal = Nothing
            ProposalList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo Proposal
    ''' </summary>
    ''' <param name="Proposal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal Proposal As ProposalEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update Proposal SET")
            'Se verifica si el campo convocatoria tiene un valor válido
            If (Proposal.idsummoning = 0) Then
                sql.AppendLine(" idsummoning = NULL,")
            Else
                sql.AppendLine(" idsummoning = '" & Proposal.idsummoning & "',")
            End If
            sql.AppendLine(" operator = '" & Proposal.nameOperator & "',")
            sql.AppendLine(" operatornit = '" & Proposal.operatornit & "',")
            sql.AppendLine(" projectname = '" & Proposal.projectname & "',")
            sql.AppendLine(" target = '" & Proposal.target & "',")
            sql.AppendLine(" targetpopulation = '" & Proposal.targetpopulation & "',")
            sql.AppendLine(" expectedresults = '" & Proposal.expectedresults & "',")
            sql.AppendLine(" totalvalue = '" & Proposal.totalvalue.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" inputfsc = '" & Proposal.inputfsc.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" inputothersources = '" & Proposal.inputothersources.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" briefprojectdescription = '" & Proposal.briefprojectdescription & "',")
            sql.AppendLine(" score = '" & Proposal.score.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" result = '" & Proposal.result & "',")
            sql.AppendLine(" responsiblereview = '" & Proposal.responsiblereview & "',")
            If (Proposal.reviewdate > CDate("1900/01/01")) Then
                sql.AppendLine(" reviewdate = '" & Proposal.reviewdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            Else
                sql.AppendLine("reviewdate = NULL,")
            End If
            sql.AppendLine(" enabled = '" & Proposal.enabled & "',")
            sql.AppendLine(" createdate = '" & Proposal.createdate.ToString("yyyyMMdd HH:mm:ss") & "',")
            sql.AppendLine(" IdProcessInstance = '" & Proposal.IdProcessInstance & "',")
            sql.AppendLine(" IdActivityInstance = '" & Proposal.IdActivityInstance & "'")
            sql.AppendLine(" WHERE id = " & Proposal.id)

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
            Throw New Exception("Error al modificar la propuesta." & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el Proposal de una forma
    ''' </summary>
    ''' <param name="Chapter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idProposal As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Proposal ")
            SQL.AppendLine(" where id = '" & idProposal & "' ")

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
            Throw New Exception("Error al elimiar la propuesta." & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function


End Class
