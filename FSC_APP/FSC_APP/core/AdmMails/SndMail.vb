Imports Microsoft.VisualBasic

Namespace PostMail

    Public Class PostMail_SndMail

        Public Function SendMail(ByVal Destination As String, ByVal Subject As String, ByVal Content As String, Optional ByVal adjunto As String = "") As String

            Dim correo As New System.Net.Mail.MailMessage

            'Generar las partes del correo
            correo.From = New System.Net.Mail.MailAddress("elvira@saldarriagaconcha.org")
            correo.To.Add(Destination)
            correo.Subject = Subject
            correo.Body = Content
            correo.IsBodyHtml = False
            correo.Priority = System.Net.Mail.MailPriority.Normal
            If adjunto <> "" Then
                Dim attachement As New System.Net.Mail.Attachment(adjunto)
                correo.Attachments.Add(attachement)
            End If

            Try

                'Declarar el servidor SMTP
                Dim smtp As New System.Net.Mail.SmtpClient
                smtp.Host = "smtp.gmail.com"
                smtp.Port = 587
                smtp.EnableSsl = True

                'Declarar credenciales de acceso
                smtp.Credentials = New System.Net.NetworkCredential("elvira@saldarriagaconcha.org", "Saldarriaga2013")

                'Enviar el mensaje
                'smtp.Send(correo)

                Return "Ok"
            Catch ex As Exception
                Return "Error: " & ex.Message
            End Try

        End Function

    End Class

End Namespace