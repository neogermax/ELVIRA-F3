Imports System.Xml
Imports System.Collections.Generic
Imports Gattaca.Application.Credentials
Imports Gattaca.Entity.eSecurity
Imports Gattaca.Interfaces.eSecurity
Imports Gattaca.Application.ExceptionManager
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class AjaxReportContractual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim action As String
        Dim applicationCredentials As ApplicationCredentials = DirectCast(Session("ApplicationCredentials"), ApplicationCredentials)

        Try
            action = Request.Form("action").ToString()

            Select Case action

                Case "loadreport"
                    cargarreporte(ApplicationCredentials)
                Case Else

            End Select

        Catch ex As Exception
            Dim merror As String
            merror = ex.Message
        End Try

    End Sub

    Public Function cargarreporte(ByVal objApplicationCredentials As Gattaca.Application.Credentials.ApplicationCredentials) As String

        Dim sql As New StringBuilder
        Dim data As DataTable
        Dim reporte As String = ""
        Dim estado As String
        Dim centrado As String

        sql.AppendLine("select idKey, Name, typeapproval, finished, sum(inicio) as inicio, sum(seguimiento) as seguimiento, sum(cierre) as cierre from ")
        sql.AppendLine("(select idKey, Name, typeapproval, finished, completiondate, [1] Inicio, [2] Seguimiento, [3] Cierre from ")
        sql.AppendLine("( ")
        sql.AppendLine("select idKey, Name, typeapproval, completiondate, Proceeding_Logs.Tipo_Acta_id, ContractRequest.Finished, max(Proceeding_Logs.Create_Date) as Create_Date from project ")
        sql.AppendLine("left join ContractRequest on Project.idKey = ContractRequest.IdProject ")
        sql.AppendLine("left join Proceeding_Logs on Project.idKey = Proceeding_Logs.Project_Id ")
        sql.AppendLine("group by idKey,Name, Typeapproval, completiondate, Proceeding_Logs.Tipo_Acta_id, ContractRequest.Finished ")
        sql.AppendLine(") as acta ")
        sql.AppendLine("pivot(count(acta.tipo_acta_id) for acta.tipo_acta_id in([1],[2],[3]))pvt) as resultados ")
        sql.AppendLine("group by idKey, Name, Typeapproval, Finished ")
        sql.AppendLine("order by idkey desc ")

        data = GattacaApplication.RunSQLRDT(objApplicationCredentials, sql.ToString)

        'Abrir tags de la tabla
        reporte &= "<table id=" & Chr(34) & "tblReport" & Chr(34) & "><tbody>" & vbCrLf

        'Generar los encabezados de la tabla
        reporte &= "<thead><tr><th style='width: 5%;'>Id</th><th style='width: 45%;'>Nombre</th><th style='width: 5%;'>Estado</th><th style='width: 5%;'>Contratación</th><th style='width: 5%;'>Acta inicio</th><th style='width: 5%;'>Acta seguimiento</th><th style='width: 5%;'>Acta cierre</th></tr></thead>" & vbCrLf

        Try

            'verificar si hay datos
            If data.Rows.Count > 0 Then

                For Each item In data.Rows
                    reporte &= "<tr>"
                    'Id del proyecto
                    reporte &= "<td>" & item("idKey") & "</td>"
                    'Nombre del proyecto truncado a 25 caracteres
                    reporte &= "<td>" & Left(item("Name"), 150) & "</td>"
                    'Estado del proyecto

                    If IsDBNull(item("typeapproval")) Then
                        estado = "X"
                    Else
                        estado = item("typeapproval")
                    End If

                    centrado = "<td align=" & Chr(34) & "center" & Chr(34) & " valign=" & Chr(34) & "middle" & Chr(34)

                    Select Case estado

                        Case 1
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_ok.png" & Chr(34) & " alt=" & Chr(34) & "Contrato" & Chr(34) & "></td>"

                        Case 2
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & ">Otro si</td>"

                        Case 3
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#FFC7CE" & Chr(34) & ">Aclaratorio</td>"

                        Case 4
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#FFC7CE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_no.png" & Chr(34) & " alt=" & Chr(34) & "No aprobado" & Chr(34) & "></td>"

                        Case Else

                    End Select

                    'Reiniciar variable
                    estado = ""

                    'Estado contratación
                    If IsDBNull(item("finished")) Then
                        estado = "X"
                    Else
                        estado = item("finished")
                    End If

                    Select Case estado
                        Case "False"
                            reporte &= "<td></td>"
                        Case "True"
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_ok.png" & Chr(34) & " alt=" & Chr(34) & "Finalizado" & Chr(34) & "></td>"
                        Case Else
                            reporte &= "<td></td>"
                    End Select

                    'Reiniciar variable
                    estado = ""

                    'Acta de inicio
                    If IsDBNull(item("inicio")) Then
                        estado = "X"
                    Else
                        estado = item("inicio")
                    End If

                    Select Case estado
                        Case 0
                            reporte &= "<td></td>"
                        Case 1
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_ok.png" & Chr(34) & " alt=" & Chr(34) & "Acta de inicio" & Chr(34) & "></td>"
                        Case Else
                            reporte &= "<td></td>"
                    End Select

                    'Reiniciar variable
                    estado = ""

                    'Acta de seguimiento
                    If IsDBNull(item("seguimiento")) Then
                        estado = "X"
                    Else
                        estado = item("seguimiento")
                    End If

                    Select Case estado
                        Case 0
                            reporte &= "<td></td>"
                        Case 1
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_ok.png" & Chr(34) & " alt=" & Chr(34) & "Acta de seguimiento" & Chr(34) & "></td>"
                        Case Else
                            reporte &= "<td></td>"
                    End Select

                    'Reiniciar variable
                    estado = ""

                    'Acta de seguimiento
                    If IsDBNull(item("cierre")) Then
                        estado = "X"
                    Else
                        estado = item("cierre")
                    End If

                    Select Case estado
                        Case 0
                            reporte &= "<td></td>"
                        Case 1
                            reporte &= centrado & " bgcolor=" & Chr(34) & "#C6EFCE" & Chr(34) & "><img src=" & Chr(34) & "../images/stat_ok.png" & Chr(34) & " alt=" & Chr(34) & "Acta de cierre" & Chr(34) & "></td>"
                        Case Else
                            reporte &= "<td></td>"
                    End Select

                    'Cerrar tag de fila
                    reporte &= "</tr>" & vbCrLf
                Next

            End If

            'Cerrar tags de la tabla
            reporte &= vbCrLf & "</tbody></table>"

            reporte = Replace(reporte, "t d>", "td>")

            Response.Write(reporte)

        Catch ex As Exception
            Dim merror As String
            merror = ex.Message

        End Try

    End Function

End Class