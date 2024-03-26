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
End Class
