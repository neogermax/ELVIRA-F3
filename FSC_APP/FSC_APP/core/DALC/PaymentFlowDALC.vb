
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Application.Credentials
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data.SqlClient

Public Class PaymentFlowDALC
    ' contantes
    Const MODULENAME As String = "ProducerDALC"

    ''' <summary>
    ''' Cargar un pagolistado por el Id
    ''' </summary>
    ''' <param name="idIdea"></param>
    ''' <remarks></remarks>
    Public Function loadPaymentFlowListByApproval(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
       ByVal idIdea As Integer) As List(Of PaymentFlowEntity)

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim objPaymentFlowEntity As New PaymentFlowEntity
        Dim objPFList As New List(Of PaymentFlowEntity)
        Dim data As DataTable
        ' obtener el nombre de la base de datos de seguridad
        Dim dbSecurityName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBSecurity")
        Dim dbBPMName As String = GattacaApplication.GetDBName(objApplicationCredentials, "VBWorkFlow")

        Try
            ' construir la sentencia
            sql.Append(" SELECT pf.id, pf.idproject, pf.fecha, pf.porcentaje, pf.entregable, pf.ididea, pf.valortotal ")
            sql.Append(" FROM Paymentflow pf ")
            sql.Append(" WHERE pf.ididea = " & idIdea)

            ' ejecutar la intruccion
            data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            For Each row As DataRow In data.Rows

                ' cargar los datos
                objPaymentFlowEntity = New PaymentFlowEntity

                ' cargar el valor del campo
                objPaymentFlowEntity.id = row("id")
                objPaymentFlowEntity.idproject = row("idproject")
                objPaymentFlowEntity.fecha = row("fecha")
                objPaymentFlowEntity.porcentaje = row("porcentaje")
                objPaymentFlowEntity.entregable = row("entregable")
                objPaymentFlowEntity.ididea = row("ididea")
                objPaymentFlowEntity.ididea = row("valortotal")
                ' agregar a la lista

                objPFList.Add(objPaymentFlowEntity)

            Next

            ' retornar el objeto
            loadPaymentFlowListByApproval = objPFList

            ' finalizar la transaccion
            CtxSetComplete()

        Catch ex As Exception

            ' cancelar la transaccion
            CtxSetAbort()

            ' publicar el error
            GattacaApplication.Publish(ex, objApplicationCredentials.ClientName, MODULENAME, "loadPaymentFlowListByApproval")
            ExceptionPolicy.HandleException(ex, "GattacaStandardExceptionPolicy")

            ' subir el error de nivel
            Throw New Exception("Error al cargar un Idea. ")

        Finally
            ' liberando recursos
            sql = Nothing
            data = Nothing
            objPaymentFlowEntity = Nothing

        End Try

    End Function
    Public Function add(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal PaymentFlow As PaymentFlowEntity) As Long

        ' definiendo los objtos
        Dim sql As New StringBuilder
        Dim dtData As DataTable
        Dim fecha As New DateTime
        'Dim porcentaje = Convert.ToDecimal(PaymentFlow.porcentaje)
        Dim porcentaje = Replace(PaymentFlow.porcentaje, ",", ".")
        Dim valorparcial = Replace(PaymentFlow.valorparcial, ",", ".")
        Dim valortotal = Replace(PaymentFlow.valorparcial, ",", ".")
        Try
            ' construir la sentencia
            sql.AppendLine("INSERT INTO Paymentflow(" & _
             "idproject," & _
             "fecha," & _
             "porcentaje," & _
             "entregable," & _
             "ididea," & _
             "valortotal," & _
             "N_pagos," & _
             "valorparcial " & _
            ")")
            sql.AppendLine("VALUES (")
            sql.AppendLine("'" & PaymentFlow.idproject & "',")
            sql.AppendLine("'" & Convert.ToDateTime(PaymentFlow.fecha).ToString("yyyy-MM-dd") & "',")
            sql.AppendLine(" " & porcentaje & ",")
            sql.AppendLine("'" & PaymentFlow.entregable & "',")
            sql.AppendLine("'" & PaymentFlow.ididea & "',")
            sql.AppendLine("'" & valortotal & "',")
            sql.AppendLine("'" & PaymentFlow.N_pagos & "',")
            sql.AppendLine("'" & valorparcial & "') ")

            'sql.AppendLine("'" & Project.IdActivityInstance & "')")

            ' intruccion para obtener el registro insertado
            sql.AppendLine(" SELECT SCOPE_IDENTITY() AS Id")

            'obtener el id
            dtData = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            ' id creado
            Dim num As Long = CLng(dtData.Rows(0)("Id"))

            'If Project.idKey = 0 Then

            '    ' limpiar el sql
            '    sql.Remove(0, sql.Length)

            '    ' actualizar el id de la llave, y habilitarlo para que sirva en las busquedas
            '    sql.AppendLine("Update PaymentFlow SET")
            '    sql.AppendLine(" idKey = '" & num & "',")
            '    sql.AppendLine(" isLastVersion = 1")
            '    sql.AppendLine("WHERE id = " & num)

            '    'Ejecutar la Instruccion
            '    GattacaApplication.RunSQL(objApplicationCredentials, sql.ToString)

            'End If

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
            Throw New Exception("Error al insertar el Proyecto. " & ex.Message)

        Finally
            ' liberando recursos
            sql = Nothing
            dtData = Nothing

        End Try

    End Function

    Public Function delete(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials, _
      ByVal idproject As Integer) As Long

        ' definiendo los objtos
        Dim SQL As New StringBuilder

        Try
            ' construir la sentencia
            SQL.AppendLine(" Delete from Paymentflow ")
            SQL.AppendLine(" where idproject = '" & idproject & "' ")
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
            Throw New Exception("Error al eliminar el Project. " & ex.Message)

        Finally
            ' liberando recursos
            SQL = Nothing

        End Try

    End Function

    Public Function getFlowPayment(ByVal estado_i_p As String, ByVal Project_id As Integer, ByVal objApplicationCredentials As ApplicationCredentials) As List(Of PaymentFlowEntity)
        Try
            Dim sql As New StringBuilder
            Dim objSqlCommand As New SqlCommand
            Dim data As DataTable
            'listado de flujos de pago
            Dim objListPaymentFlow As List(Of PaymentFlowEntity) = New List(Of PaymentFlowEntity)()



            If estado_i_p = "i" Then

                'consulta de los datos de actores por id
                sql.Append(" select i.id, pf.* from  Idea  i ")
                sql.Append("inner join   Paymentflow pf on pf.ididea=i.id  ")

                sql.Append("where i.id = " & Project_id)

                ' ejecutar la intruccion
                data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)


            Else

                'consulta de los datos de actores por id
                sql.Append("select p.Id, pf.* from Project p ")
                sql.Append("inner join Paymentflow pf on pf.Idproject = p.Id ")

                sql.Append("where p.Id = " & Project_id)

                ' ejecutar la intruccion
                data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

            End If


            For Each item_flowEntity In data.Rows

                Dim objflowpaymentEntity As PaymentFlowEntity = New PaymentFlowEntity()
                objflowpaymentEntity.id = item_flowEntity("Id")
                objflowpaymentEntity.idproject = item_flowEntity("idproject")
                objflowpaymentEntity.fecha = item_flowEntity("fecha")
                objflowpaymentEntity.porcentaje = item_flowEntity("porcentaje")
                objflowpaymentEntity.entregable = item_flowEntity("entregable")
                objflowpaymentEntity.ididea = item_flowEntity("Ididea")
                objflowpaymentEntity.valorparcial = item_flowEntity("valorparcial")
                objflowpaymentEntity.valortotal = item_flowEntity("valortotal")

                objListPaymentFlow.Add(objflowpaymentEntity)
            Next
            Return objListPaymentFlow
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
