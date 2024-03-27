
Imports MySql.Data.MySqlClient

Public Class ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus

    Public on_edit_mode As Integer = 0

    Private Sub BtnAddRCFGARecord_Click(sender As Object, e As EventArgs) Handles BtnAddRCFGARecord.Click
        Dim open_add_form As New AddResearchCourseFacilitatorGroupAdviserRecord(Me)
        open_add_form.Show()
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim edit_record_form As New EditRCF_RGA(Me, selected_record)
        edit_record_form.Show()
        on_edit_mode = selected_record
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
        PanelRcfrgareq.AutoScroll = True
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
    Dim open_req_id As Integer = 0
    Public selected_record As Integer = 0
    Private Sub DgvSwData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSwData.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            If e.ColumnIndex = 10 Then
                Dim i As Integer = DgvSwData.CurrentRow.Index
                open_req_id = DgvSwData.Item(1, i).Value
                CheckRequirement()
                PanelRcfrgareq.Visible = True
                PanelRcfrgareq.AutoScroll = True
                PanelRcfrgareq.VerticalScroll.Visible = True
            Else
                Dim i As Integer = DgvSwData.CurrentRow.Index
                selected_record = DgvSwData.Item(1, i).Value
                BtnRemoveSelection.Visible = True
                BtnDelete.Enabled = True
                BtnEdit.Enabled = True
            End If
        End If
    End Sub

    Private Sub DgvSwData_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvSwData.ColumnHeaderMouseClick
        For i = 0 To DgvSwData.Rows.Count - 1
            DgvSwData.Rows(i).Height = 60
        Next
    End Sub

    Private Sub CheckRequirement()

        If open_req_id = 0 Then
            MessageBox.Show("No record selected.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            DgvRcf.Rows.Clear()
            DgvRga.Rows.Clear()

            con.Close()
            Try
                con.Open()
                Dim select_requirement As String = "
                        SELECT 
                            rcf_rga_req.*, rcf_rga.* 
                        FROM 
                            rcf_rga_req 
                        INNER JOIN 
                            rcf_rga ON rcf_rga.record_id = rcf_rga_req.rcf_rga_req_id 
                        WHERE 
                            rcf_rga_req_id=@id"

                Using cmd As New MySqlCommand(select_requirement, con)
                    cmd.Parameters.AddWithValue("@id", open_req_id)
                    Dim reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        If reader.Read() Then

                            If reader("stage") = "Final Thesis" Then
                                LblStageChckPnl.Text = "Stage: Final Thesis/Capstone"
                            Else
                                LblStageChckPnl.Text = "Stage: Research Proposal"
                            End If

                            'RCF REQUIREMENTS
                            If reader("role").ToString.Contains("Research Course Facilitator") Then

                                Dim dynamic_label As String = ""
                                Dim final_proposal As String = ""

                                If reader("stage") = "Final Thesis" Then
                                    dynamic_label = "Evaluation Forms for FINAL THESIS DEFENSE from the panel members"
                                    final_proposal = reader("status_final_eval_form")
                                Else
                                    dynamic_label = "Evaluation Forms for Research Proposal 
Defense from the panel members"
                                    final_proposal = reader("status_evaluation_form")
                                End If

                                'ADD ROWS TO DATAGRID
                                DgvRcf.Rows.Add("Endorsement Letters for Oral Defense")
                                DgvRcf.Rows.Add(dynamic_label)
                                DgvRcf.Rows.Add("Pictures or documentation of oral defense")
                                DgvRcf.Rows.Add("Copy of students’ official receipt for research fee")

                                'endorsement
                                DgvRcf.Rows(0).Cells(1).Value = reader("status_endorsement_letter").ToString
                                DgvRcf.Rows(0).Cells(2).Value = reader("endo_lett_sbmttd_date").ToString
                                DgvRcf.Rows(0).Cells(3).Value = reader("endo_lett_remarks").ToString

                                'proposal/final
                                DgvRcf.Rows(1).Cells(1).Value = final_proposal
                                DgvRcf.Rows(1).Cells(2).Value = reader("eval_sbmttd_date").ToString
                                DgvRcf.Rows(1).Cells(3).Value = reader("eval_remarks").ToString

                                'documentation
                                DgvRcf.Rows(2).Cells(1).Value = reader("status_pict_documentation").ToString
                                DgvRcf.Rows(2).Cells(2).Value = reader("pict_docu_sbmttd_date").ToString
                                DgvRcf.Rows(2).Cells(3).Value = reader("pict_docu_remarks").ToString

                                'OR
                                DgvRcf.Rows(3).Cells(1).Value = reader("status_copy_student_or").ToString
                                DgvRcf.Rows(3).Cells(2).Value = reader("stud_or_sbmttd_date").ToString
                                DgvRcf.Rows(3).Cells(3).Value = reader("stud_or_remarks").ToString

                                DgvRcf.RowHeadersDefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Bold)

                                For i = 0 To DgvRcf.Rows.Count - 1
                                    DgvRcf.Rows(i).Height = 50
                                Next
                            End If


                            'RGA REQUIREMENTS
                            If reader("role").ToString.Contains("Research Group’s Adviser") Then
                                DgvRga.Rows.Add("Appointment Letter")
                                DgvRga.Rows.Add("Consultation Form")

                                DgvRga.Rows(0).Cells(1).Value = reader("status_appoint_lett").ToString
                                DgvRga.Rows(0).Cells(2).Value = reader("appoint_lett_sbmttd_date").ToString
                                DgvRga.Rows(0).Cells(3).Value = reader("appoint_lett_remarks").ToString

                                DgvRga.Rows(1).Cells(1).Value = reader("status_consult_form").ToString
                                DgvRga.Rows(1).Cells(2).Value = reader("consult_form_sbmttd_date").ToString
                                DgvRga.Rows(1).Cells(3).Value = reader("consult_form_remarks").ToString

                                DgvRga.RowHeadersDefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Bold)
                                For i = 0 To DgvRga.Rows.Count - 1
                                    DgvRga.Rows(i).Height = 50
                                Next
                            End If

                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
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

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If on_edit_mode = selected_record Then
            MessageBox.Show("Record is currently on edit mode, close the edit window and try again.", "Unable to delete this record.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim confirm_deletion As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Click 'Yes' to confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm_deletion = DialogResult.Yes Then
                con.Close()

                Dim delete_queries() As String = {
                        "DELETE FROM rcf_rga WHERE record_id=@id",
                        "DELETE FROM rcf_rga_req WHERE rcf_rga_req_id=@id"
                    }
                con.Open()
                Using delete_transaction As MySqlTransaction = con.BeginTransaction()
                    Try
                        For Each queries As String In delete_queries
                            Using cmd As New MySqlCommand(queries, con, delete_transaction)
                                cmd.Parameters.AddWithValue("@id", selected_record)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        delete_transaction.Commit()
                        LoadRcfRgaRecords()
                        BtnRemoveSelection.PerformClick()
                        MessageBox.Show("Record updated Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Error Occcurred in deleting record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        delete_transaction.Rollback()
                    Finally
                        con.Close()
                    End Try

                End Using
            End If
        End If
    End Sub

    'CLOSE PANEL
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PanelRcfrgareq.Visible = False
    End Sub


End Class