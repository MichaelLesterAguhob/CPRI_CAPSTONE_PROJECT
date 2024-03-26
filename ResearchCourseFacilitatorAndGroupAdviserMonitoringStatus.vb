
Imports MySql.Data.MySqlClient

Public Class ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
    Private Sub BtnAddRCFGARecord_Click(sender As Object, e As EventArgs) Handles BtnAddRCFGARecord.Click
        Dim open_add_form As New AddResearchCourseFacilitatorGroupAdviserRecord(Me)
        open_add_form.Show()
    End Sub

    'FORM LOAD
    Private Sub ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConOpen()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
        LoadRcfRgaRecords()
    End Sub

    'LOADING DATA
    Public Sub LoadRcfRgaRecords()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM `rcf_rga` ORDER BY `no#` DESC"
            Using cmd As New MySqlCommand(query, con)
                Dim adptr As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                adptr.Fill(dt)

                DgvSwData.DataSource = dt
                DgvSwData.Refresh()
            End Using

            For i = 0 To DgvSwData.Rows.Count - 1
                DgvSwData.Rows(i).Height = 50
            Next
            DgvSwData.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    'HANDLES DATAGRIDVIEW EVENTS
    Dim open_req_rcf_id As Integer
    Dim open_req_rga_id As Integer
    Public selected_record As Integer = 0
    Private Sub DgvSwData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            If e.ColumnIndex = 10 Then
                Dim i As Integer = DgvSwData.CurrentRow.Index
                open_req_rcf_id = DgvSwData.Item(1, i).Value
            ElseIf e.ColumnIndex = 11 Then
                Dim i As Integer = DgvSwData.CurrentRow.Index
                open_req_rga_id = DgvSwData.Item(1, i).Value
            Else
                Dim i As Integer = DgvSwData.CurrentRow.Index
                selected_record = DgvSwData.Item(1, i).Value
                BtnRemoveSelection.Visible = True
                BtnDelete.Enabled = True
                BtnEdit.Enabled = True
            End If
        End If
    End Sub

    Private Sub DgvSwData_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellMouseEnter

        If e.ColumnIndex = 10 Then
            DgvSwData.Cursor = Cursors.Hand
        ElseIf e.ColumnIndex = 11 Then
            DgvSwData.Cursor = Cursors.Hand
        End If

    End Sub
    Private Sub DgvSwData_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellMouseLeave
        If e.ColumnIndex = 10 Then
            DgvSwData.Cursor = Cursors.Default
        ElseIf e.ColumnIndex = 11 Then
            DgvSwData.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub BtnRemoveSelection_Click(sender As Object, e As EventArgs) Handles BtnRemoveSelection.Click
        DgvSwData.ClearSelection()
        selected_record = 0
        BtnDelete.Enabled = False
        BtnEdit.Enabled = False
        BtnRemoveSelection.Visible = False
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim edit_record_form As New EditRCF_RGA(Me, selected_record)
        edit_record_form.Show()
    End Sub
End Class