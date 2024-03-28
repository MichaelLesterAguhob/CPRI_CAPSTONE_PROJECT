
Imports MySql.Data.MySqlClient

Public Class PrintThesisClearance
    Dim dt As New DataTable
    Dim dt2 As New DataTable
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
            'dgvData.DataSource = dt
            'dgvData.Refresh()
        End Using

        Dim query2 As String = "SELECT 
                                    co_authors_name
                                FROM
                                    co_authors
                                WHERE
                                    co_authors_id = 202490841
                                LIMIT 8 OFFSET 0;
                            "
        Using cmd2 As New MySqlCommand(query2, con)
            Dim adptr2 As New MySqlDataAdapter(cmd2)
            adptr2.Fill(dt2)
            'dgvData.DataSource = dt
            'dgvData.Refresh()
        End Using

        con.Close()

        Dim print_clearance As New PrintClearance
        print_clearance.Database.Tables("authors").SetDataSource(dt)
        print_clearance.Database.Tables("co_authors").SetDataSource(dt2)
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.ReportSource = print_clearance
    End Sub

    Private Sub Preview_Click(sender As Object, e As EventArgs) Handles Preview.Click

    End Sub
End Class