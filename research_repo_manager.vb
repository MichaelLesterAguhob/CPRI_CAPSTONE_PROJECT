

Public Class ResearchRepoManager

    'MAIN FORM LOAD
    Private Sub ResearchRepoManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PnlFilter.Width = 0
        PnlFilter.Height = 0
    End Sub

    'OPENING AND CLOSING OF FILTER PANEL ====================================
    Dim open_close_filter As Integer = 0
    Private Sub BtnFilter_Click(sender As Object, e As EventArgs) Handles BtnFilter.Click
        BtnFilter.Enabled = FalseD
        If open_close_filter = 0 Then
            TmOpenFilter.Enabled = True
            open_close_filter = 1
        Else
            TmCloseFilter.Enabled = True
            open_close_filter = 0
        End If
    End Sub
    Private Sub TmOpenFilter_Tick(sender As Object, e As EventArgs) Handles TmOpenFilter.Tick
        If PnlFilter.Width >= 500 Then
            TmOpenFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width + 500
            PnlFilter.Height = PnlFilter.Height + 400
        End If
    End Sub
    Private Sub TmCloseFilter_Tick(sender As Object, e As EventArgs) Handles TmCloseFilter.Tick
        If PnlFilter.Width <= 0 Then
            TmCloseFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width - 500
            PnlFilter.Height = PnlFilter.Height - 400
        End If
    End Sub
    Private Sub BtnCloseFilter_Click(sender As Object, e As EventArgs) Handles BtnCloseFilter.Click
        TmCloseFilter.Enabled = True
        open_close_filter = 0
    End Sub

    '==================================================================






End Class