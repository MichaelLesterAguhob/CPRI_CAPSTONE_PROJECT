Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class ViewWorks
    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()
    Dim abstract_file_extension As String

    Private Sub DgvSwData_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DgvSwData.CellPainting

        If e.RowIndex = -1 AndAlso e.ColumnIndex > -1 Then

            e.Graphics.FillRectangle(New SolidBrush(DgvSwData.DefaultCellStyle.BackColor), e.CellBounds)

            Dim headerFont As Font = DgvSwData.ColumnHeadersDefaultCellStyle.Font
            If headerFont Is Nothing Then
                headerFont = DgvSwData.DefaultCellStyle.Font
            End If
            Dim headerBrush As New SolidBrush(DgvSwData.ColumnHeadersDefaultCellStyle.ForeColor)
            Dim headerRect As New RectangleF(e.CellBounds.X, e.CellBounds.Y + 2, e.CellBounds.Width, e.CellBounds.Height - 5)
            e.Graphics.DrawString(DgvSwData.Columns(e.ColumnIndex).HeaderText, headerFont, headerBrush, headerRect)

            e.Handled = True
        End If
    End Sub

    Private Sub ViewWorks_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ConOpen()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error occurred in view works load", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        LoadWorkData()
        DgvSwData.ClearSelection()
    End Sub

    Dim stat As String
    Dim pub_pre As String
    Dim pub_pre_id As String
    Private Sub LoadWorkData()
        con.Close()
        Try

            con.Open()
            Using cmd As New MySqlCommand("
                                SELECT scholarly_works.*, 
                                      authors.* 
                                FROM 
                                    scholarly_works 
                                INNER JOIN authors
                                    ON authors.authors_id=scholarly_works.sw_id
                                WHERE sw_id=@to_view_id
                                ", con)

                cmd.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        TxtViewedId.Text = to_view_work_id
                        TxtStat.Text = reader("status_ongoing_completed").ToString()
                        stat = reader("status_ongoing_completed").ToString()
                        stat = reader("status_ongoing_completed").ToString()
                        TxtSc.Text = reader("school_year").ToString()
                        TxtDateCompleted.Text = reader("date_completed").ToString()
                        TxtSem.Text = reader("semester").ToString()
                        TxtRsrchAgenda.Text = reader("research_agenda").ToString()
                        TxtRsrchTitle.Text = reader("title").ToString()
                        TxtAuth.Text = reader("authors_name").ToString()
                        TxtDegProg.Text = reader("degree_program").ToString()
                        TxtAthrRole.Text = reader("role").ToString()

                        pub_pre = reader("published").ToString()
                        If pub_pre = "Published" Then
                            pub_pre = "published"
                        Else
                            pub_pre = reader("presented").ToString()
                            If pub_pre = "Presented" Then
                                pub_pre = "presented"
                            End If
                        End If
                    End If
                End If
                reader.Close()
            End Using


            Using cmd2 As New MySqlCommand("SELECT co_authors_name, degree_program, role FROM co_authors WHERE co_authors_id=@to_view_id", con)
                cmd2.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                Dim adptr As New MySqlDataAdapter(cmd2)
                Dim dt As New DataTable
                adptr.Fill(dt)

                DgvSwData.DataSource = dt
                DgvSwData.Refresh()
            End Using

            If pub_pre = "published" Then
                Using cmd3 As New MySqlCommand("SELECT * FROM published_details WHERE published_id=@to_view_id", con)
                    cmd3.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                    Dim reader2 As MySqlDataReader = cmd3.ExecuteReader()
                    If reader2.HasRows Then
                        If reader2.Read() Then

                            PanelPublished.Visible = True
                            PanelPresented.Visible = True
                            TxtPublishedLvl.Text = reader2("level").ToString()
                            TxtAcad.Text = reader2("academic_journal").ToString()
                            TxtVol.Text = reader2("volume_no").ToString()
                            TxtIssNo.Text = reader2("issue_no").ToString()
                            TxtPr.Text = reader2("page_range").ToString()
                            TxtDatePublished.Text = reader2("date_published").ToString()
                            TxtDoiUrl.Text = reader2("doi_url").ToString()

                        End If
                    End If
                    reader2.Close()
                End Using
            ElseIf pub_pre = "presented" Then
                Using cmd3 As New MySqlCommand("SELECT * FROM presented_details WHERE presented_id=@to_view_id", con)
                    cmd3.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                    Dim reader2 As MySqlDataReader = cmd3.ExecuteReader()
                    If reader2.HasRows Then
                        If reader2.Read() Then

                            PanelPublished.Visible = False
                            PanelPresented.Visible = True
                            TxtPresentedLvl.Text = reader2("level").ToString()
                            TxtRcn.Text = reader2("research_conference_name").ToString()
                            TxtPresentedDate.Text = reader2("date_presented").ToString()
                            TxtPlace.Text = reader2("place_presentation").ToString()

                        End If
                    End If
                    reader2.Close()
                End Using
            Else
                PanelPublished.Visible = True
                PanelPresented.Visible = True
            End If

            Using cmd2 As New MySqlCommand("SELECT file_data FROM sw_abstract WHERE abstract_id=@to_view_id", con)
                cmd2.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                Dim reader2 As MySqlDataReader = cmd2.ExecuteReader()
                If reader2.HasRows Then
                    If reader2.Read() Then

                        abstract_file_data = reader2("file_data")

                        Dim pdfContent As String = ExtractTextFromPdfBytes(abstract_file_data)
                        TextBox1.Text = pdfContent
                    End If
                End If
                reader2.Close()
            End Using

            If stat = "Completed" Then

                Using cmd2 As New MySqlCommand("SELECT * FROM status_completed_info WHERE stat_completed_id=@to_view_id", con)
                    cmd2.Parameters.AddWithValue("@to_view_id", to_view_work_id)
                    Dim reader2 As MySqlDataReader = cmd2.ExecuteReader()
                    If reader2.HasRows Then
                        If reader2.Read() Then

                            Dim is_all_submitted As Integer

                            If reader2("soft_copy_sbmttd_date").ToString <> "NO" Then
                                LblSoft.Visible = True
                                is_all_submitted += 1

                            End If
                            If reader2("hard_copy_sbmttd_date").ToString <> "NO" Then
                                LblHard.Visible = True
                                is_all_submitted += 1

                            End If
                            If reader2("dgi_sbmttd_date").ToString <> "NO" Then
                                LblDgi.Visible = True
                                is_all_submitted += 1

                            End If
                            If reader2("rga_ef_sbmttd_date").ToString <> "NO" Then
                                LblRga.Visible = True
                                is_all_submitted += 1

                            End If

                            If is_all_submitted = 4 Then
                                BtnPrint.Enabled = True
                                BtnPrint.BackColor = Color.LimeGreen
                            Else
                                BtnPrint.Enabled = False
                                BtnPrint.BackColor = Color.LightGray
                            End If

                            Dim pdfContent As String = ExtractTextFromPdfBytes(abstract_file_data)
                            TextBox1.Text = pdfContent
                        End If
                    End If
                    reader2.Close()
                End Using

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error occurred in view works load", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Public Function ExtractTextFromPdfBytes(pdfBytes As Byte()) As String
        Dim text As New StringBuilder()

        Using memoryStream As New MemoryStream(pdfBytes)
            Using reader As New PdfReader(memoryStream)
                For page As Integer = 1 To reader.NumberOfPages
                    Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                    Dim currentText As String = PdfTextExtractor.GetTextFromPage(reader, page, strategy)
                    text.Append(currentText)
                Next
            End Using
        End Using

        Return text.ToString()
    End Function

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        print_clearance_id = Convert.ToInt64(TxtViewedId.Text.Trim)
        Dim print_clearance As New PrintThesisClearance
        print_clearance.Show()

    End Sub
End Class