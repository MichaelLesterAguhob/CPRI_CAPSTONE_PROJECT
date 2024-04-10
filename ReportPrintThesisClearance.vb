
Imports MySql.Data.MySqlClient

Public Class ReportPrintThesisClearance


    Private Sub PrintThesisClearance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim dt_query_works As New DataTable
            Dim dt As New DataTable
            Dim dt2 As New DataTable

            ConOpen()
            Dim query_works As String = "SELECT 
                                    *
                                FROM
                                    scholarly_works
                                WHERE
                                    sw_id = @id
                                    
                            "
            Using cmd_query_works As New MySqlCommand(query_works, con)
                cmd_query_works.Parameters.AddWithValue("@id", print_clearance_id)
                Dim adptr_query_works As New MySqlDataAdapter(cmd_query_works)
                adptr_query_works.Fill(dt_query_works)
            End Using

            '////
            Dim query As String = "SELECT 
                                    authors_name
                                FROM
                                    authors
                                WHERE
                                    authors_id = @id
                                    
                            "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", print_clearance_id)
                Dim adptr As New MySqlDataAdapter(cmd)
                adptr.Fill(dt)
            End Using


            '////
            Dim query2 As String = "SELECT * FROM co_authors WHERE co_authors_id = @id"
            Using cmd2 As New MySqlCommand(query2, con)
                cmd2.Parameters.AddWithValue("@id", print_clearance_id)
                Dim adptr2 As New MySqlDataAdapter(cmd2)
                adptr2.Fill(dt2)
            End Using


            Dim dtFormattedCoAuthors As New DataTable
            dtFormattedCoAuthors.Columns.Add("FirstColumn", GetType(String))
            dtFormattedCoAuthors.Columns.Add("SecondColumn", GetType(String))

            ' Iterate over the co-authors and populate the formatted DataTable
            Dim start_at_7 As Integer = 7
            Dim spacer As String = " "
            If dt2.Rows.Count > 4 Then
                Dim rowCount As Integer = Math.Ceiling(dt2.Rows.Count / 2)
                For i As Integer = 0 To rowCount - 1
                    Dim firstCoAuthor As String = If(i < dt2.Rows.Count, dt2.Rows(i)("co_authors_name").ToString(), "")
                    Dim secondCoAuthor As String = If(i + rowCount < dt2.Rows.Count, dt2.Rows(i + rowCount)("co_authors_name").ToString(), "")
                    dtFormattedCoAuthors.Rows.Add(firstCoAuthor, (spacer & start_at_7 & ". " & secondCoAuthor))
                    start_at_7 += 1
                    If start_at_7 >= 10 Then
                        spacer = ""
                    End If
                Next
            Else
                Dim rowCount As Integer = dt2.Rows.Count
                For i As Integer = 0 To rowCount - 1
                    Dim firstCoAuthor As String = If(i < dt2.Rows.Count, dt2.Rows(i)("co_authors_name").ToString(), "")
                    dtFormattedCoAuthors.Rows.Add(firstCoAuthor)

                Next
            End If


            con.Close()

            Dim print_clearance As New PrintClearance
            print_clearance.Database.Tables("authors").SetDataSource(dt)
            print_clearance.Database.Tables("scholarly_works").SetDataSource(dt_query_works)
            print_clearance.Database.Tables("FormattedCoAuthors").SetDataSource(dtFormattedCoAuthors)


            CrystalReportViewer1.ReportSource = Nothing
            CrystalReportViewer1.ReportSource = print_clearance
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed on Saving Whole File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub
End Class