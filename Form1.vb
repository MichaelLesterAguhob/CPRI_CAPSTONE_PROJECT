Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
End Class
