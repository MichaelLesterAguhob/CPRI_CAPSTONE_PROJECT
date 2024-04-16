Imports ZXing
Imports ZXing.QrCode
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports MySql.Data.MySqlClient
Public Class QrCodeGenerator

    Dim qrCodeBitmap As Bitmap
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get the text entered by the user
        Dim textToEncode As String = TextBox1.Text.Trim()

        ' Check if the input text is not empty
        If Not String.IsNullOrEmpty(textToEncode) Then
            ' Generate QR code bitmap
            qrCodeBitmap = GenerateQRCodeBitmap(textToEncode)

            ' Display the QR code bitmap in PictureBox
            PictureBox1.Image = qrCodeBitmap

            ' Save the QR code bitmap as PNG in the database
            SaveQRCodeToDatabase(textToEncode, qrCodeBitmap)
        Else
            MessageBox.Show("Please enter text to generate a QR code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function GenerateQRCodeBitmap(text As String) As Bitmap
        ' Configure QR code writer
        Dim qrWriter As New BarcodeWriter()
        qrWriter.Format = BarcodeFormat.QR_CODE
        Dim qrCodeOptions As New QrCodeEncodingOptions() With {
            .DisableECI = True,
            .CharacterSet = "UTF-8",
            .Width = PictureBox1.Width,
            .Height = PictureBox1.Height
        }
        qrWriter.Options = qrCodeOptions

        ' Generate QR code bitmap
        Dim qrCodeBitmap As Bitmap = qrWriter.Write(text)
        Return qrCodeBitmap
    End Function

    Private Sub SaveQRCodeToDatabase(text As String, qrCodeBitmap As Bitmap)
        Try
            ' Convert QR code bitmap to byte array (PNG format)
            Dim imageBytes As Byte() = ImageToByteArray(qrCodeBitmap)

            ' Connect to the database
            Using conn As New SqlConnection("")
                conn.Open()

                ' Prepare SQL command to insert the QR code image bytes into the database
                Dim sql As String = "INSERT INTO QRCodeImages (Text, ImageBytes) VALUES (@Text, @ImageBytes)"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Text", text)
                    cmd.Parameters.AddWithValue("@ImageBytes", imageBytes)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("QR Code saved to database successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving QR Code to database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ImageToByteArray(image As Image) As Byte()
        Dim ms As New MemoryStream()
        image.Save(ms, ImageFormat.Png)
        Return ms.ToArray()
    End Function


    '// SENDING TO EMAIL
    Private Sub SendQrCode()
        Try
            Dim imageBytes As Byte() = ImageToByteArray(qrCodeBitmap)

            Dim smtp_server As New SmtpClient
            smtp_server.UseDefaultCredentials = False
            smtp_server.Credentials = New Net.NetworkCredential("cdsga.cpri@gmail.com", "xmuc gwab jeua dxss")
            smtp_server.Port = 587
            smtp_server.EnableSsl = True
            smtp_server.Host = "smtp.gmail.com"

            Dim email As New MailMessage
            email = New MailMessage()
            email.From = New MailAddress("cdsga.cpri@gmail.com")
            email.To.Add(TxtEmail.Text.Trim)
            email.Subject = "Your CPRI | QR CODE ID"
            email.Body = "<span style='font-size: 25px; color: maroon;'>" & "CDSGA-CPRI" & "</span> <br><br>" & "Here's your QR code ID : " & "<br> <span style='font-size: 35px;'>" & "image here" & "</span>"
            email.IsBodyHtml = True

            ' Attach the QR code image as an attachment
            email.Attachments.Add(New Attachment(New MemoryStream(imageBytes), "QRCode.png"))

            smtp_server.Send(email)

            MessageBox.Show("Qr Code ID sent", "Reset Code sent", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred while sending reset code", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SendQrCode()
    End Sub
End Class
