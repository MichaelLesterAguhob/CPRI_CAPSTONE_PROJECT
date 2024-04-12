Imports ZXing
Imports ZXing.QrCode
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Data.SqlClient ' Import SQL Server-specific namespace

Public Class QrCodeGenerator

    Private connectionString As String = "Your_Connection_String_Here"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get the text entered by the user
        Dim textToEncode As String = TextBox1.Text.Trim()

        ' Check if the input text is not empty
        If Not String.IsNullOrEmpty(textToEncode) Then
            ' Generate QR code bitmap
            Dim qrCodeBitmap As Bitmap = GenerateQRCodeBitmap(textToEncode)

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
            Using conn As New SqlConnection(connectionString)
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

End Class
