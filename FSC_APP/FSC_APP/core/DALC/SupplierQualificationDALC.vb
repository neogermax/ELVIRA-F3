Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SupplierQualificationDALC

    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary> 
    ''' Registar un nuevo SupplierQualification
    ''' </summary>
    ''' <param name="SupplierQualification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal SupplierQualification As SupplierQualificationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO SupplierQualification(" & _
             "idsupplierevaluation," & _
             "contractsubject," & _
             "contractualobligations," & _
             "definedgoals," & _
             "agreeddeadlines," & _
             "totalitydeliveredproducts," & _
             "requestsmadebyfsc," & _
             "deliveryproductsservices," & _
             "reporting," & _
             "productquality," & _
             "reportsquality," & _
             "accompanimentquality," & _
             "attentioncomplaintsclaims," & _
             "returnedproducts," & _
             "productvalueadded," & _
             "accompanimentvalueadded," & _
             "reportsvalueadded," & _
             "projectplaneacion," & _
             "methodologyimplemented," & _
             "developmentprojectorganization," & _
             "jointactivities," & _
             "projectcontrol," & _
             "servicestaffcompetence," & _
             "suppliercompetence," & _
             "informationconfidentiality," & _
             "compliancepercentage," & _
             "opportunitypercentage," & _
             "qualitypercentage," & _
             "addedvaluepercentage," & _
             "methodologypercentage," & _
             "servicestaffcompetencepercentage," & _
             "confidentialitypercentage" & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SupplierQualification.idsupplierevaluation & "',")
            sql.AppendLine("'" & SupplierQualification.contractsubject & "',")
            sql.AppendLine("'" & SupplierQualification.contractualobligations & "',")
            sql.AppendLine("'" & SupplierQualification.definedgoals & "',")
            sql.AppendLine("'" & SupplierQualification.agreeddeadlines & "',")
            sql.AppendLine("'" & SupplierQualification.totalitydeliveredproducts & "',")
            sql.AppendLine("'" & SupplierQualification.requestsmadebyfsc & "',")
            sql.AppendLine("'" & SupplierQualification.deliveryproductsservices & "',")
            sql.AppendLine("'" & SupplierQualification.reporting & "',")
            sql.AppendLine("'" & SupplierQualification.productquality & "',")
            sql.AppendLine("'" & SupplierQualification.reportsquality & "',")
            sql.AppendLine("'" & SupplierQualification.accompanimentquality & "',")
            sql.AppendLine("'" & SupplierQualification.attentioncomplaintsclaims & "',")
            sql.AppendLine("'" & SupplierQualification.returnedproducts & "',")
            sql.AppendLine("'" & SupplierQualification.productvalueadded & "',")
            sql.AppendLine("'" & SupplierQualification.accompanimentvalueadded & "',")
            sql.AppendLine("'" & SupplierQualification.reportsvalueadded & "',")
            sql.AppendLine("'" & SupplierQualification.projectplaneacion & "',")
            sql.AppendLine("'" & SupplierQualification.methodologyimplemented & "',")
            sql.AppendLine("'" & SupplierQualification.developmentprojectorganization & "',")
            sql.AppendLine("'" & SupplierQualification.jointactivities & "',")
            sql.AppendLine("'" & SupplierQualification.projectcontrol & "',")
            sql.AppendLine("'" & SupplierQualification.servicestaffcompetence & "',")
            sql.AppendLine("'" & SupplierQualification.suppliercompetence & "',")
            sql.AppendLine("'" & SupplierQualification.informationconfidentiality & "',")
            sql.AppendLine("'" & SupplierQualification.compliancepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.opportunitypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.qualitypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.addedvaluepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.methodologypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.servicestaffcompetencepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine("'" & SupplierQualification.confidentialitypercentage.ToString().Replace(",", ".") & "')")

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
            Throw New Exception("Error al insertar el SupplierQualification. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar un SupplierQualification por el Id
    ''' </summary>
    ''' <param name="idSupplierQualification"></param>
    ''' <remarks></remarks>
    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idSupplierEvaluation As Integer) As SupplierQualificationEntity

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objSupplierQualification As New SupplierQualificationEntity
        Dim data As DataTable

        Try
            ' construir la sentencia
            sql.Append(" SELECT * ")
            sql.Append(" FROM SupplierQualification ")
            sql.Append(" WHERE idsupplierevaluation = " & idSupplierEvaluation)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                ' cargar los datos
                objSupplierQualification.id = data.Rows(0)("id")
                objSupplierQualification.idsupplierevaluation = data.Rows(0)("idsupplierevaluation")
                objSupplierQualification.contractsubject = data.Rows(0)("contractsubject")
                objSupplierQualification.contractualobligations = data.Rows(0)("contractualobligations")
                objSupplierQualification.definedgoals = data.Rows(0)("definedgoals")
                objSupplierQualification.agreeddeadlines = data.Rows(0)("agreeddeadlines")
                objSupplierQualification.totalitydeliveredproducts = data.Rows(0)("totalitydeliveredproducts")
                objSupplierQualification.requestsmadebyfsc = data.Rows(0)("requestsmadebyfsc")
                objSupplierQualification.deliveryproductsservices = data.Rows(0)("deliveryproductsservices")
                objSupplierQualification.reporting = data.Rows(0)("reporting")
                objSupplierQualification.productquality = data.Rows(0)("productquality")
                objSupplierQualification.reportsquality = data.Rows(0)("reportsquality")
                objSupplierQualification.accompanimentquality = data.Rows(0)("accompanimentquality")
                objSupplierQualification.attentioncomplaintsclaims = data.Rows(0)("attentioncomplaintsclaims")
                objSupplierQualification.returnedproducts = data.Rows(0)("returnedproducts")
                objSupplierQualification.productvalueadded = data.Rows(0)("productvalueadded")
                objSupplierQualification.accompanimentvalueadded = data.Rows(0)("accompanimentvalueadded")
                objSupplierQualification.reportsvalueadded = data.Rows(0)("reportsvalueadded")
                objSupplierQualification.projectplaneacion = data.Rows(0)("projectplaneacion")
                objSupplierQualification.methodologyimplemented = data.Rows(0)("methodologyimplemented")
                objSupplierQualification.developmentprojectorganization = data.Rows(0)("developmentprojectorganization")
                objSupplierQualification.jointactivities = data.Rows(0)("jointactivities")
                objSupplierQualification.projectcontrol = data.Rows(0)("projectcontrol")
                objSupplierQualification.servicestaffcompetence = data.Rows(0)("servicestaffcompetence")
                objSupplierQualification.suppliercompetence = data.Rows(0)("suppliercompetence")
                objSupplierQualification.informationconfidentiality = data.Rows(0)("informationconfidentiality")
                objSupplierQualification.compliancepercentage = data.Rows(0)("compliancepercentage")
                objSupplierQualification.opportunitypercentage = data.Rows(0)("opportunitypercentage")
                objSupplierQualification.qualitypercentage = data.Rows(0)("qualitypercentage")
                objSupplierQualification.addedvaluepercentage = data.Rows(0)("addedvaluepercentage")
                objSupplierQualification.methodologypercentage = data.Rows(0)("methodologypercentage")
                objSupplierQualification.servicestaffcompetencepercentage = data.Rows(0)("servicestaffcompetencepercentage")
                objSupplierQualification.confidentialitypercentage = data.Rows(0)("confidentialitypercentage")

            End If

            ' retornar el objeto
            Return objSupplierQualification

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un SupplierQualification. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objSupplierQualification = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Cargar lista de capitulos de una forma
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idsupplierevaluation"></param>
    ''' <param name="contractsubject"></param>
    ''' <param name="contractualobligations"></param>
    ''' <param name="definedgoals"></param>
    ''' <param name="agreeddeadlines"></param>
    ''' <param name="totalitydeliveredproducts"></param>
    ''' <param name="requestsmadebyfsc"></param>
    ''' <param name="deliveryproductsservices"></param>
    ''' <param name="reporting"></param>
    ''' <param name="productquality"></param>
    ''' <param name="reportsquality"></param>
    ''' <param name="accompanimentquality"></param>
    ''' <param name="attentioncomplaintsclaims"></param>
    ''' <param name="returnedproducts"></param>
    ''' <param name="productvalueadded"></param>
    ''' <param name="accompanimentvalueadded"></param>
    ''' <param name="reportsvalueadded"></param>
    ''' <param name="projectplaneacion"></param>
    ''' <param name="methodologyimplemented"></param>
    ''' <param name="developmentprojectorganization"></param>
    ''' <param name="jointactivities"></param>
    ''' <param name="projectcontrol"></param>
    ''' <param name="servicestaffcompetence"></param>
    ''' <param name="suppliercompetence"></param>
    ''' <param name="informationconfidentiality"></param>
    ''' <param name="compliancepercentage"></param>
    ''' <param name="opportunitypercentage"></param>
    ''' <param name="qualitypercentage"></param>
    ''' <param name="addedvaluepercentage"></param>
    ''' <param name="methodologypercentage"></param>
    ''' <param name="servicestaffcompetencepercentage"></param>
    ''' <param name="confidentialitypercentage"></param>
    ''' <returns>un objeto de tipo List(Of SupplierQualificationEntity)</returns>
    ''' <remarks></remarks>
    Public Function getList(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
        Optional ByVal id As String = "", _
        Optional ByVal idsupplierevaluation As String = "", _
        Optional ByVal contractsubject As String = "", _
        Optional ByVal contractualobligations As String = "", _
        Optional ByVal definedgoals As String = "", _
        Optional ByVal agreeddeadlines As String = "", _
        Optional ByVal totalitydeliveredproducts As String = "", _
        Optional ByVal requestsmadebyfsc As String = "", _
        Optional ByVal deliveryproductsservices As String = "", _
        Optional ByVal reporting As String = "", _
        Optional ByVal productquality As String = "", _
        Optional ByVal reportsquality As String = "", _
        Optional ByVal accompanimentquality As String = "", _
        Optional ByVal attentioncomplaintsclaims As String = "", _
        Optional ByVal returnedproducts As String = "", _
        Optional ByVal productvalueadded As String = "", _
        Optional ByVal accompanimentvalueadded As String = "", _
        Optional ByVal reportsvalueadded As String = "", _
        Optional ByVal projectplaneacion As String = "", _
        Optional ByVal methodologyimplemented As String = "", _
        Optional ByVal developmentprojectorganization As String = "", _
        Optional ByVal jointactivities As String = "", _
        Optional ByVal projectcontrol As String = "", _
        Optional ByVal servicestaffcompetence As String = "", _
        Optional ByVal suppliercompetence As String = "", _
        Optional ByVal informationconfidentiality As String = "", _
        Optional ByVal compliancepercentage As String = "", _
        Optional ByVal opportunitypercentage As String = "", _
        Optional ByVal qualitypercentage As String = "", _
        Optional ByVal addedvaluepercentage As String = "", _
        Optional ByVal methodologypercentage As String = "", _
        Optional ByVal servicestaffcompetencepercentage As String = "", _
        Optional ByVal confidentialitypercentage As String = "", _
        Optional ByVal order As String = "") As List(Of SupplierQualificationEntity)

        ' definiendo los objetos
        Dim sql As New StringBuilder
        Dim objSupplierQualification As SupplierQualificationEntity
        Dim SupplierQualificationList As New List(Of SupplierQualificationEntity)
        Dim data As DataTable
        Dim where As String = " WHERE "

        Try
            ' construir la sentencia
            SQL.Append(" SELECT * ")
            SQL.Append(" FROM SupplierQualification ")

            ' verificar si hay entrada de datos para el campo
            If Not id.Equals("") Then

                SQL.Append(where & " id like '%" & id & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not idsupplierevaluation.Equals("") Then

                SQL.Append(where & " idsupplierevaluation like '%" & idsupplierevaluation & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractsubject.Equals("") Then

                SQL.Append(where & " contractsubject like '%" & contractsubject & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not contractualobligations.Equals("") Then

                SQL.Append(where & " contractualobligations like '%" & contractualobligations & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not definedgoals.Equals("") Then

                SQL.Append(where & " definedgoals like '%" & definedgoals & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not agreeddeadlines.Equals("") Then

                SQL.Append(where & " agreeddeadlines like '%" & agreeddeadlines & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not totalitydeliveredproducts.Equals("") Then

                SQL.Append(where & " totalitydeliveredproducts like '%" & totalitydeliveredproducts & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not requestsmadebyfsc.Equals("") Then

                SQL.Append(where & " requestsmadebyfsc like '%" & requestsmadebyfsc & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not deliveryproductsservices.Equals("") Then

                SQL.Append(where & " deliveryproductsservices like '%" & deliveryproductsservices & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not reporting.Equals("") Then

                SQL.Append(where & " reporting like '%" & reporting & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not productquality.Equals("") Then

                SQL.Append(where & " productquality like '%" & productquality & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not reportsquality.Equals("") Then

                SQL.Append(where & " reportsquality like '%" & reportsquality & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not accompanimentquality.Equals("") Then

                SQL.Append(where & " accompanimentquality like '%" & accompanimentquality & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not attentioncomplaintsclaims.Equals("") Then

                SQL.Append(where & " attentioncomplaintsclaims like '%" & attentioncomplaintsclaims & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not returnedproducts.Equals("") Then

                SQL.Append(where & " returnedproducts like '%" & returnedproducts & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not productvalueadded.Equals("") Then

                SQL.Append(where & " productvalueadded like '%" & productvalueadded & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not accompanimentvalueadded.Equals("") Then

                SQL.Append(where & " accompanimentvalueadded like '%" & accompanimentvalueadded & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not reportsvalueadded.Equals("") Then

                SQL.Append(where & " reportsvalueadded like '%" & reportsvalueadded & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectplaneacion.Equals("") Then

                SQL.Append(where & " projectplaneacion like '%" & projectplaneacion & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not methodologyimplemented.Equals("") Then

                SQL.Append(where & " methodologyimplemented like '%" & methodologyimplemented & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not developmentprojectorganization.Equals("") Then

                SQL.Append(where & " developmentprojectorganization like '%" & developmentprojectorganization & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not jointactivities.Equals("") Then

                SQL.Append(where & " jointactivities like '%" & jointactivities & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not projectcontrol.Equals("") Then

                SQL.Append(where & " projectcontrol like '%" & projectcontrol & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not servicestaffcompetence.Equals("") Then

                SQL.Append(where & " servicestaffcompetence like '%" & servicestaffcompetence & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not suppliercompetence.Equals("") Then

                SQL.Append(where & " suppliercompetence like '%" & suppliercompetence & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not informationconfidentiality.Equals("") Then

                SQL.Append(where & " informationconfidentiality like '%" & informationconfidentiality & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not compliancepercentage.Equals("") Then

                SQL.Append(where & " compliancepercentage like '%" & compliancepercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not opportunitypercentage.Equals("") Then

                SQL.Append(where & " opportunitypercentage like '%" & opportunitypercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not qualitypercentage.Equals("") Then

                SQL.Append(where & " qualitypercentage like '%" & qualitypercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not addedvaluepercentage.Equals("") Then

                SQL.Append(where & " addedvaluepercentage like '%" & addedvaluepercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not methodologypercentage.Equals("") Then

                SQL.Append(where & " methodologypercentage like '%" & methodologypercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not servicestaffcompetencepercentage.Equals("") Then

                SQL.Append(where & " servicestaffcompetencepercentage like '%" & servicestaffcompetencepercentage & "%'")
                where = " AND "

            End If

            ' verificar si hay entrada de datos para el campo
            If Not confidentialitypercentage.Equals("") Then

                SQL.Append(where & " confidentialitypercentage like '%" & confidentialitypercentage & "%'")
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
                objSupplierQualification = New SupplierQualificationEntity

                ' cargar el valor del campo
                objSupplierQualification.id = row("id")
                objSupplierQualification.idsupplierevaluation = row("idsupplierevaluation")
                objSupplierQualification.contractsubject = row("contractsubject")
                objSupplierQualification.contractualobligations = row("contractualobligations")
                objSupplierQualification.definedgoals = row("definedgoals")
                objSupplierQualification.agreeddeadlines = row("agreeddeadlines")
                objSupplierQualification.totalitydeliveredproducts = row("totalitydeliveredproducts")
                objSupplierQualification.requestsmadebyfsc = row("requestsmadebyfsc")
                objSupplierQualification.deliveryproductsservices = row("deliveryproductsservices")
                objSupplierQualification.reporting = row("reporting")
                objSupplierQualification.productquality = row("productquality")
                objSupplierQualification.reportsquality = row("reportsquality")
                objSupplierQualification.accompanimentquality = row("accompanimentquality")
                objSupplierQualification.attentioncomplaintsclaims = row("attentioncomplaintsclaims")
                objSupplierQualification.returnedproducts = row("returnedproducts")
                objSupplierQualification.productvalueadded = row("productvalueadded")
                objSupplierQualification.accompanimentvalueadded = row("accompanimentvalueadded")
                objSupplierQualification.reportsvalueadded = row("reportsvalueadded")
                objSupplierQualification.projectplaneacion = row("projectplaneacion")
                objSupplierQualification.methodologyimplemented = row("methodologyimplemented")
                objSupplierQualification.developmentprojectorganization = row("developmentprojectorganization")
                objSupplierQualification.jointactivities = row("jointactivities")
                objSupplierQualification.projectcontrol = row("projectcontrol")
                objSupplierQualification.servicestaffcompetence = row("servicestaffcompetence")
                objSupplierQualification.suppliercompetence = row("suppliercompetence")
                objSupplierQualification.informationconfidentiality = row("informationconfidentiality")
                objSupplierQualification.compliancepercentage = row("compliancepercentage")
                objSupplierQualification.opportunitypercentage = row("opportunitypercentage")
                objSupplierQualification.qualitypercentage = row("qualitypercentage")
                objSupplierQualification.addedvaluepercentage = row("addedvaluepercentage")
                objSupplierQualification.methodologypercentage = row("methodologypercentage")
                objSupplierQualification.servicestaffcompetencepercentage = row("servicestaffcompetencepercentage")
                objSupplierQualification.confidentialitypercentage = row("confidentialitypercentage")

                ' agregar a la lista
                SupplierQualificationList.Add(objSupplierQualification)

            Next

            ' retornar el objeto
            getList = SupplierQualificationList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "getList")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar la lista de SupplierQualification. ")

        Finally
            ' liberando recursos
            SQL = Nothing
            objSupplierQualification = Nothing
            SupplierQualificationList = Nothing
            data = Nothing
            where = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Modificar un objeto de tipo SupplierQualification
    ''' </summary>
    ''' <param name="SupplierQualification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function update(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal SupplierQualification As SupplierQualificationEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder

        Try
            ' construir la sentencia
            sql.AppendLine("Update SupplierQualification SET")
            sql.AppendLine(" idsupplierevaluation = '" & SupplierQualification.idsupplierevaluation & "',")
            sql.AppendLine(" contractsubject = '" & SupplierQualification.contractsubject & "',")
            sql.AppendLine(" contractualobligations = '" & SupplierQualification.contractualobligations & "',")
            sql.AppendLine(" definedgoals = '" & SupplierQualification.definedgoals & "',")
            sql.AppendLine(" agreeddeadlines = '" & SupplierQualification.agreeddeadlines & "',")
            sql.AppendLine(" totalitydeliveredproducts = '" & SupplierQualification.totalitydeliveredproducts & "',")
            sql.AppendLine(" requestsmadebyfsc = '" & SupplierQualification.requestsmadebyfsc & "',")
            sql.AppendLine(" deliveryproductsservices = '" & SupplierQualification.deliveryproductsservices & "',")
            sql.AppendLine(" reporting = '" & SupplierQualification.reporting & "',")
            sql.AppendLine(" productquality = '" & SupplierQualification.productquality & "',")
            sql.AppendLine(" reportsquality = '" & SupplierQualification.reportsquality & "',")
            sql.AppendLine(" accompanimentquality = '" & SupplierQualification.accompanimentquality & "',")
            sql.AppendLine(" attentioncomplaintsclaims = '" & SupplierQualification.attentioncomplaintsclaims & "',")
            sql.AppendLine(" returnedproducts = '" & SupplierQualification.returnedproducts & "',")
            sql.AppendLine(" productvalueadded = '" & SupplierQualification.productvalueadded & "',")
            sql.AppendLine(" accompanimentvalueadded = '" & SupplierQualification.accompanimentvalueadded & "',")
            sql.AppendLine(" reportsvalueadded = '" & SupplierQualification.reportsvalueadded & "',")
            sql.AppendLine(" projectplaneacion = '" & SupplierQualification.projectplaneacion & "',")
            sql.AppendLine(" methodologyimplemented = '" & SupplierQualification.methodologyimplemented & "',")
            sql.AppendLine(" developmentprojectorganization = '" & SupplierQualification.developmentprojectorganization & "',")
            sql.AppendLine(" jointactivities = '" & SupplierQualification.jointactivities & "',")
            sql.AppendLine(" projectcontrol = '" & SupplierQualification.projectcontrol & "',")
            sql.AppendLine(" servicestaffcompetence = '" & SupplierQualification.servicestaffcompetence & "',")
            sql.AppendLine(" suppliercompetence = '" & SupplierQualification.suppliercompetence & "',")
            sql.AppendLine(" informationconfidentiality = '" & SupplierQualification.informationconfidentiality & "',")
            sql.AppendLine(" compliancepercentage = '" & SupplierQualification.compliancepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" opportunitypercentage = '" & SupplierQualification.opportunitypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" qualitypercentage = '" & SupplierQualification.qualitypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" addedvaluepercentage = '" & SupplierQualification.addedvaluepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" methodologypercentage = '" & SupplierQualification.methodologypercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" servicestaffcompetencepercentage = '" & SupplierQualification.servicestaffcompetencepercentage.ToString().Replace(",", ".") & "',")
            sql.AppendLine(" confidentialitypercentage = '" & SupplierQualification.confidentialitypercentage.ToString().Replace(",", ".") & "'")
            sql.AppendLine("WHERE id = " & SupplierQualification.id)

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
            Throw New Exception("Error al modificar el SupplierQualification. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing

        End Try

    End Function

    ''' <summary>
    ''' Borra el SupplierQualification de una forma
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
            SQL.AppendLine(" Delete from SupplierQualification ")
            SQL.AppendLine(" where IdSupplierEvaluation = '" & idSupplierEvaluation & "' ")

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
            Throw New Exception("Error al elimiar el SupplierQualification. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

End Class
