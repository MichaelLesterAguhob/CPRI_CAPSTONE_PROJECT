
Imports MySql.Data.MySqlClient

Public Class Form1

    Shared date_time As DateTime = DateTime.Now

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        TxtDate.Text = date_time.Date.ToString("MM-dd-yyyy")
        Timer1.Start()
        LoadAllDisplayData()

        If loggedin <= 0 Then
            Me.Close()
            Dim crt_lgn As New CreateLoginAccount
            crt_lgn.Show()
        Else
            If account_type_loggedin = "admin" Then
                LblWelcome.Text = "WELCOME ADMIN | " & account_loggedin
            Else
                LblWelcome.Text = "WELCOME STAFF | " & account_loggedin
            End If
        End If
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
        Dim rrm As New ResearchRepoManager(Me, "")
        rrm.Show()
    End Sub

    Private Sub ResearchCourseFacilitatorAndAdviserMonitoringStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResearchCourseFacilitatorAndAdviserMonitoringStatusToolStripMenuItem.Click
        Dim rcfgams As New ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
        rcfgams.Show()
    End Sub

    Private Sub BorrowingAndReturningToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub BorrowingAndReturningToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BorrowingAndReturningToolStripMenuItem1.Click
        Dim open_borrowing_retruning As New BorrowingAndReturning(Me, "")
        open_borrowing_retruning.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim open_borrowing_retruning As New BorrowingAndReturning(Me, "")
        open_borrowing_retruning.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rrm As New ResearchRepoManager(Me, "")
        rrm.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim rcfgams As New ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
        rcfgams.Show()
    End Sub


    '// LOAD ALL DATA TO BE DISPLAYED
    Public Sub LoadAllDisplayData()
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

            Dim date_today As Date = Date.Today.ToString("MM-dd-yyyy")

            'GET RETURNED BOOKS
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM returned_books WHERE returned_date=@rd", con)
                cmd.Parameters.AddWithValue("@rd", Date.Today.ToString("MM-dd-yyyy"))
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblReturnedBooks.Text = rows.ToString()
                Else
                    LblReturnedBooks.Text = "0"
                End If
            End Using

            'GET DUE TODAY BOOKS
            Using cmd As New MySqlCommand("SELECT COUNT(*) FROM borrowed_books WHERE due_date=@dd AND is_cancel='NO' AND is_returned='NO' AND is_overdue='NO'", con)
                cmd.Parameters.AddWithValue("@dd", Date.Today.ToString("MM-dd-yyyy"))
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblDueToday.Text = rows.ToString()
                Else
                    LblDueToday.Text = "0"
                End If
            End Using

            'GET published books
            Using cmd As New MySqlCommand(
                "SELECT 
                    ifnull(COUNT(*),0) AS published_books
                FROM 
                    scholarly_works
                INNER JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id", con)
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    LblPub.Text = rows.ToString()
                Else
                    LblPub.Text = "0"
                End If
            End Using

            'GET published books
            Using cmd As New MySqlCommand(
                "SELECT 
                    ifnull(COUNT(*),0) AS published_books
                FROM 
                    scholarly_works
                LEFT JOIN published_details
                    ON published_details.published_id = scholarly_works.sw_id
                WHERE published_details.published_id IS NULL
                ", con)
                Dim rows As Integer = 0
                rows = Convert.ToInt32(cmd.ExecuteScalar())
                If rows > 0 Then
                    lblUnpub.Text = rows.ToString()
                Else
                    lblUnpub.Text = "0"
                End If
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occured while loading display data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub LblOverdues_Click(sender As Object, e As EventArgs) Handles LblOverdues.Click
        Dim br As New BorrowingAndReturning(Me, "overdues")
        br.Show()
    End Sub

    Private Sub LblBorrowedBooks_Click(sender As Object, e As EventArgs) Handles LblBorrowedBooks.Click
        Dim br As New BorrowingAndReturning(Me, "borrowed")
        br.Show()
    End Sub

    Private Sub LblReturnedBooks_Click(sender As Object, e As EventArgs) Handles LblReturnedBooks.Click
        Dim br As New BorrowingAndReturning(Me, "returned")
        br.Show()
    End Sub

    Private Sub LblDueToday_Click(sender As Object, e As EventArgs) Handles LblDueToday.Click
        Dim br As New BorrowingAndReturning(Me, "borrowed")
        br.Show()
    End Sub

    Private Sub lblUnpub_Click(sender As Object, e As EventArgs) Handles lblUnpub.Click
        Dim br As New ResearchRepoManager(Me, "Unpublished")
        br.Show()
    End Sub

    Private Sub LblPub_Click(sender As Object, e As EventArgs) Handles LblPub.Click
        Dim br As New ResearchRepoManager(Me, "Published")
        br.Show()
    End Sub


End Class
