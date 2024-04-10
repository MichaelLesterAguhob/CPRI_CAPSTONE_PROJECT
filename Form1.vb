
Imports MySql.Data.MySqlClient

Public Class Form1

    Shared ReadOnly date_time As DateTime = DateTime.Now

    Private Sub CheckActiveLogin()
        If loggedin <= 0 Then
            Me.Close()
            Dim crt_lgn As New CreateLoginAccount
            crt_lgn.Show()
        Else
            If account_type_loggedin = "admin" Then
                LblWelcome.Text = "WELCOME ADMIN | " & account_loggedin.ToUpper()
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        TxtDateNow.Text = date_time.Date.ToString("MM-dd-yyyy")
        Timer1.Start()
        LoadAllDisplayData()
        ' CheckActiveLogin()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Close all child forms before closing Form1
        ' For Each childForm In Me.MdiChildren
        ' childForm.Close()
        'Next
        'Application.Exit() ' Close the entire application
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TxtTimeNow.Text = TimeOfDay.ToString("h:mm:ss tt")
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

    Private Sub LblUnpub_Click(sender As Object, e As EventArgs) Handles lblUnpub.Click
        Dim br As New ResearchRepoManager(Me, "Unpublished")
        br.Show()
    End Sub

    Private Sub LblPub_Click(sender As Object, e As EventArgs) Handles LblPub.Click
        Dim br As New ResearchRepoManager(Me, "Published")
        br.Show()
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        loggedin = 0
        account_loggedin = ""
        account_type_loggedin = ""
        CheckActiveLogin()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub LogOut_Click(sender As Object, e As EventArgs) Handles LogOut.Click
        loggedin = 0
        account_loggedin = ""
        account_type_loggedin = ""
        CheckActiveLogin()
    End Sub


    ReadOnly myFont1 As New Font("Microsoft Sans Serif", 14, FontStyle.Regular)
    ReadOnly myFont01 As New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)

    ReadOnly myFont2 As New Font("Segoe UI", 12, FontStyle.Regular)
    ReadOnly myFont02 As New Font("Segoe UI", 9, FontStyle.Regular)

    '// display data
    Private Sub LblPub_MouseEnter(sender As Object, e As EventArgs) Handles LblPub.MouseEnter
        LblPub.ImageAlign = ContentAlignment.TopRight
        Label12.Font = myFont1
    End Sub

    Private Sub LblPub_MouseLeave(sender As Object, e As EventArgs) Handles LblPub.MouseLeave
        LblPub.ImageAlign = ContentAlignment.MiddleRight
        Label12.Font = myFont01
    End Sub

    '//
    Private Sub LblunPub_MouseEnter(sender As Object, e As EventArgs) Handles lblUnpub.MouseEnter
        lblUnpub.ImageAlign = ContentAlignment.TopRight
        Label13.Font = myFont1
    End Sub

    Private Sub LblunPub_MouseLeave(sender As Object, e As EventArgs) Handles lblUnpub.MouseLeave
        lblUnpub.ImageAlign = ContentAlignment.MiddleRight
        Label13.Font = myFont01
    End Sub

    '//
    Private Sub LblOverdues_MouseEnter(sender As Object, e As EventArgs) Handles LblOverdues.MouseEnter
        LblOverdues.ImageAlign = ContentAlignment.TopRight
        Label10.Font = myFont1
    End Sub

    Private Sub LblOverdues_MouseLeave(sender As Object, e As EventArgs) Handles LblOverdues.MouseLeave
        LblOverdues.ImageAlign = ContentAlignment.MiddleRight
        Label10.Font = myFont01
    End Sub

    '//
    Private Sub LblDueToday_MouseEnter(sender As Object, e As EventArgs) Handles LblDueToday.MouseEnter
        LblDueToday.ImageAlign = ContentAlignment.TopRight
        Label3.Font = myFont1
    End Sub

    Private Sub LblDueToday_MouseLeave(sender As Object, e As EventArgs) Handles LblDueToday.MouseLeave
        LblDueToday.ImageAlign = ContentAlignment.MiddleRight
        Label3.Font = myFont01
    End Sub

    '//
    Private Sub LblBorrowedBooks_MouseEnter(sender As Object, e As EventArgs) Handles LblBorrowedBooks.MouseEnter
        LblBorrowedBooks.ImageAlign = ContentAlignment.TopRight
        Label11.Font = myFont1
    End Sub

    Private Sub LblBorrowedBooks_MouseLeave(sender As Object, e As EventArgs) Handles LblBorrowedBooks.MouseLeave
        LblBorrowedBooks.ImageAlign = ContentAlignment.MiddleRight
        Label11.Font = myFont01
    End Sub

    '//
    Private Sub LblReturnedBooks_MouseEnter(sender As Object, e As EventArgs) Handles LblReturnedBooks.MouseEnter
        LblReturnedBooks.ImageAlign = ContentAlignment.TopRight
        Label14.Font = myFont1
    End Sub
    Private Sub LblReturnedBooks_MouseLeave(sender As Object, e As EventArgs) Handles LblReturnedBooks.MouseLeave
        LblReturnedBooks.ImageAlign = ContentAlignment.MiddleRight
        Label14.Font = myFont01
    End Sub

    '//nav button
    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.ImageAlign = ContentAlignment.TopLeft
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.ImageAlign = ContentAlignment.MiddleLeft
    End Sub

    '//
    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.ImageAlign = ContentAlignment.TopLeft
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.ImageAlign = ContentAlignment.MiddleLeft
    End Sub

    '//
    Private Sub Button3_MouseEnter(sender As Object, e As EventArgs) Handles Button3.MouseEnter
        Button3.ImageAlign = ContentAlignment.TopLeft
    End Sub

    Private Sub Button3_MouseLeave(sender As Object, e As EventArgs) Handles Button3.MouseLeave
        Button3.ImageAlign = ContentAlignment.MiddleLeft
    End Sub

    '//logout
    Private Sub LogOut_MouseEnter(sender As Object, e As EventArgs) Handles LogOut.MouseEnter
        LogOut.ImageAlign = ContentAlignment.MiddleRight
        LogOut.TextAlign = ContentAlignment.MiddleLeft
    End Sub

    Private Sub LogOut_MouseLeave(sender As Object, e As EventArgs) Handles LogOut.MouseLeave
        LogOut.ImageAlign = ContentAlignment.MiddleLeft
        LogOut.TextAlign = ContentAlignment.MiddleRight
    End Sub

    '//
    Private Sub MenuToolStripMenuItem1_MouseEnter(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem1.MouseEnter
        MenuToolStripMenuItem1.ImageAlign = ContentAlignment.TopLeft
        MenuToolStripMenuItem1.Font = myFont2

    End Sub

    Private Sub MenuToolStripMenuItem1_MouseLeave(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem1.MouseLeave
        MenuToolStripMenuItem1.ImageAlign = ContentAlignment.MiddleLeft
        MenuToolStripMenuItem1.Font = myFont02
    End Sub

    Private Sub ReportsToolStripMenuItem_MouseEnter(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.MouseEnter
        ReportsToolStripMenuItem.ImageAlign = ContentAlignment.TopLeft
        ReportsToolStripMenuItem.Font = myFont2

    End Sub

    Private Sub ReportsToolStripMenuItem_MouseLeave(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.MouseLeave
        ReportsToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft
        ReportsToolStripMenuItem.Font = myFont02
    End Sub

    Private Sub MenuToolStripMenuItem_MouseEnter(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.MouseEnter
        MenuToolStripMenuItem.ImageAlign = ContentAlignment.TopLeft
        MenuToolStripMenuItem.Font = myFont2

    End Sub

    Private Sub MenuToolStripMenuItem_MouseLeave(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.MouseLeave
        MenuToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft
        MenuToolStripMenuItem.Font = myFont02
    End Sub


End Class
