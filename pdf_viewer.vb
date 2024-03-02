
Imports MySql.Data.MySqlClient
Imports PdfiumViewer
Imports System.IO

Public Class pdf_viewer

    Private Sub pdf_viewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.Close()
        Try
            con.Open()
            Dim pdf_data As Byte()

            Dim query As String = "SELECT file_data FROM sw_abstract WHERE abstract_id=@PdfId"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@PdfId", 1234)
                pdf_data = DirectCast(cmd.ExecuteScalar(), Byte())

            End Using

            Dim ms As New MemoryStream(pdf_data)
            Dim pdf_document As PdfDocument = PdfDocument.Load(ms)
            PdfViewer1.Document = pdf_document
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        Finally
            con.Close()
        End Try
    End Sub



End Class
