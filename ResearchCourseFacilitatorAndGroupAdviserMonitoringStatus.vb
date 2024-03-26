Public Class ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
    Private Sub BtnAddRCFGARecord_Click(sender As Object, e As EventArgs) Handles BtnAddRCFGARecord.Click
        Dim open_add_form As New AddResearchCourseFacilitatorGroupAdviserRecord
        open_add_form.Show()
    End Sub
End Class