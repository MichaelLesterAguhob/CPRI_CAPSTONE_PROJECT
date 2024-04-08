
Imports MySql.Data.MySqlClient

Public Class Form1

    Shared date_time As DateTime = DateTime.Now

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        Timer1.Start()
        LoadAllDisplayData()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Close all child forms before closing Form1
        For Each childForm In Me.MdiChildren
            childForm.Close()
        Next
        Application.Exit() ' Close the entire application
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TxtTime.Text = TimeOfDay.ToString("h:mm:ss tt")
    End Sub

    Private Sub ResearchRepositoryManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResearchRepositoryManagerToolStripMenuItem.Click
        Dim rrm As New ResearchRepoManager
        rrm.Show()
    End Sub

    Private Sub ResearchCourseFacilitatorAndAdviserMonitoringStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResearchCourseFacilitatorAndAdviserMonitoringStatusToolStripMenuItem.Click
        Dim rcfgams As New ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
        rcfgams.Show()
    End Sub

    Private Sub BorrowingAndReturningToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BorrowingAndReturningToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BorrowingAndReturningToolStripMenuItem1.Click
        Dim open_borrowing_retruning As New BorrowingAndReturning
        open_borrowing_retruning.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim open_borrowing_retruning As New BorrowingAndReturning
        open_borrowing_retruning.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rrm As New ResearchRepoManager
        rrm.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim rcfgams As New ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
        rcfgams.Show()
    End Sub


    '// LOAD ALL DATA TO BE DISPLAYED
    Private Sub LoadAllDisplayData()
        con.Close()
        Try
            con.Open()
            'GET ALL OVERDUES
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM overdues WHERE status='NOT RETURNED'", con)
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblOverdues.Text = rows.ToString()
                Else
                    LblOverdues.Text = "0"
                End If
            End Using

            'GET ALL BOOROWED BOOKS
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM borrowed_books WHERE is_cancel='NO' AND is_returned='NO' ", con)
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblBorrowedBooks.Text = rows.ToString()
                Else
                    LblBorrowedBooks.Text = "0"
                End If
            End Using

            'GET RETURNED BOOKS
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM returned_books WHERE is_cancel='NO' AND is_returned='NO' ", con)
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblBorrowedBooks.Text = rows.ToString()
                Else
                    LblBorrowedBooks.Text = "0"
                End If
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occured while loading display data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub
End Class
