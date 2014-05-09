Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager

Public Class SupervisorByContractDALC

    'Constantes
    Const MODULENAME As String = "SupervisorByContractDALC"

    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal SupervisorByContractReq As SupervisorByContractRequestEntity) As Long

        'Definiendo los objetos
        Dim sql As New StringBuilder
        Dim dtData As DataTable

        Try
            sql.AppendLine("INSERT INTO SupervisorbyContractReq(")
            sql.AppendLine("Third_Id, Contractrequest_Id) ")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & SupervisorByContractReq.Third_Id & "',")
            sql.AppendLine("'" & SupervisorByContractReq.ContractRequest_Id & "'")
            sql.AppendLine(")")

            'instruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            'id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            'finalizar la transaccion
            CtxSetComplete()

            'retorar
            Return num

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "add")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            'subir el error de nivel
            Throw New Exception("Error al insertar un Supervisor al contrato. " & ex.Message)

        Finally
            'liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    Public Function load(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, ByVal contractrequest As Integer) As SupervisorByContractRequestEntity

        'definiendo objetos
        Dim sql As New StringBuilder
        Dim objSupervisorByContract As New SupervisorByContractRequestEntity
        Dim data As DataTable
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try

            'construir la sentencia
            sql.Append("SELECT * from SupervisorbyContractReq")
            sql.Append(" WHERE ContractRequest_Id = " & contractrequest)

            'ejecutar el script
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            If data.Rows.Count > 0 Then

                'cargar los datos
                objSupervisorByContract.ContractRequest_Id = data.Rows(0)("ContractRequest_Id")
                objSupervisorByContract.Third_Id = data.Rows(0)("Third_Id")

            End If

            'retornar el objeto
            Return objSupervisorByContract

            'finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            'cancelar la transaccion
            CtxSetAbort()

            'publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "load")

            'subir el error de nivel
            Throw New Exception("Error al cargar un Supervisor. - " & ex.Message)

        Finally

            sql = Nothing
            data = Nothing
            objSupervisorByContract = Nothing

        End Try

    End Function

End Class
