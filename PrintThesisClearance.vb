
Imports MySql.Data.MySqlClient

Public Class PrintThesisClearance
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim dt3 As New DataTable
    Dim ds As New DataSet

    Private Sub PrintThesisClearance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConOpen()
        Dim query As String = "SELECT 
                                    authors_name
                                FROM
                                    authors
                                WHERE
                                    authors_id = 202490841
                                    
                            "
        Using cmd As New MySqlCommand(query, con)
            Dim adptr As New MySqlDataAdapter(cmd)
            adptr.Fill(dt)
        End Using



        Dim query2 As String = "SELECT * FROM co_authors WHERE co_authors_id = 202490841"
        Using cmd2 As New MySqlCommand(query2, con)
            Dim adptr2 As New MySqlDataAdapter(cmd2)
            adptr2.Fill(dt2)
        End Using


        Dim dtFormattedCoAuthors As New DataTable
            dtFormattedCoAuthors.Columns.Add("FirstColumn", GetType(String))
            dtFormattedCoAuthors.Columns.Add("SecondColumn", GetType(String))

        ' Iterate over the co-authors and populate the formatted DataTable
        Dim rowCount As Integer = Math.Ceiling(dt2.Rows.Count / 2)
        For i As Integer = 0 To rowCount - 1
            Dim firstCoAuthor As String = If(i < dt2.Rows.Count, dt2.Rows(i)("co_authors_name").ToString(), "")
            Dim secondCoAuthor As String = If(i + rowCount < dt2.Rows.Count, dt2.Rows(i + rowCount)("co_authors_name").ToString(), "")
            dtFormattedCoAuthors.Rows.Add(firstCoAuthor, secondCoAuthor)
        Next

        con.Close()

        Dim print_clearance As New PrintClearance
        print_clearance.Database.Tables("authors").SetDataSource(dt)
        print_clearance.Database.Tables("FormattedCoAuthors").SetDataSource(dtFormattedCoAuthors)
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.ReportSource = print_clearance
    End Sub
End Class