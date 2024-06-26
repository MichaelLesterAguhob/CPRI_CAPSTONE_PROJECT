﻿
Imports MySql.Data.MySqlClient

Public Class ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus

    Public on_edit_mode As Integer = 0
    Dim rcf_data As New DataTable()

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
            LblSearchFound.Text = ""

            LoadReportData("load")
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

    Dim isJustClicked As Boolean = True
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        If Me.IsHandleCreated Then
            If Not isJustClicked Then
                LoadRcfRgaRecords()
            End If
        End If

    End Sub

    Private Sub TxtSearch_Click(sender As Object, e As EventArgs) Handles TxtSearch.Click
        isJustClicked = True
        If TxtSearch.Text = "Seacrh ID, name, semester,  stage, college etc..." Then
            TxtSearch.Text = ""
            TxtSearch.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TxtSearch_Leave(sender As Object, e As EventArgs) Handles TxtSearch.Leave
        isJustClicked = False
        If TxtSearch.Text = "" Then
            TxtSearch.Text = "Seacrh ID, name, semester,  stage, college etc..."
            TxtSearch.ForeColor = Color.Gray
            LoadRcfRgaRecords()
        End If
    End Sub

    Private Sub TxtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSearch.KeyDown
        If e.KeyCode = 13 Then
            If TxtSearch.Text <> "Seacrh ID, name, semester,  stage, college etc..." And TxtSearch.Text <> "" Then
                Search()
            End If
        End If
    End Sub

    Private Sub Search()
        con.Close()

        Try
            con.Open()
            Dim query As String = "
                        SELECT * FROM `rcf_rga` 
                        WHERE
                            record_id LIKE @to_search
                            OR semester LIKE @to_search
                            OR school_year LIKE @to_search
                            OR stage LIKE @to_search
                            OR name LIKE @to_search
                            OR college LIKE @to_search
                            OR dept LIKE @to_search
                            OR status LIKE @to_search
                            OR role LIKE @to_search
                        ORDER BY `no#` DESC
                        "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearch.Text.Trim & "%")
                Dim adptr As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                adptr.Fill(dt)

                If dt.Rows.Count > 0 Then
                    DgvSwData.DataSource = dt
                    DgvSwData.Refresh()
                    For i = 0 To DgvSwData.Rows.Count - 1
                        DgvSwData.Rows(i).Height = 50
                    Next
                    BtnRemoveSelection.PerformClick()
                    LblSearchFound.Text = dt.Rows.Count.ToString & " Result(s) found"
                    LoadReportData("searched")
                Else
                    LblSearchFound.Text = dt.Rows.Count.ToString & " Result(s) found"
                    MessageBox.Show("Your search do not match to any records. Please try different keywords", "No data found.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occcurred Searching record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
            BtnRemoveSelection.PerformClick()
        End Try
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click

        If TxtSearch.Text <> "Seacrh ID, name, semester,  stage, college etc..." And TxtSearch.Text <> "" Then
            Search()

        End If
    End Sub

    'OPENING AND CLOSING OF FILTER PANEL 
    Dim open_close_filter As Integer = 0
    Private Sub BtnFilter_Click(sender As Object, e As EventArgs) Handles BtnFilter.Click
        TxtSearch.Text = "Seacrh ID, name, semester,  stage, college etc..."
        TxtSearch.ForeColor = Color.Gray
        BtnFilter.Enabled = False
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
            PnlFilter.Height = PnlFilter.Height + 250
        End If
    End Sub
    Private Sub TmCloseFilter_Tick(sender As Object, e As EventArgs) Handles TmCloseFilter.Tick
        If PnlFilter.Width <= 0 Then
            TmCloseFilter.Enabled = False
            BtnFilter.Enabled = True
        Else
            PnlFilter.Width = PnlFilter.Width - 500
            PnlFilter.Height = PnlFilter.Height - 250
        End If
    End Sub
    Private Sub BtnCloseFilter_Click(sender As Object, e As EventArgs) Handles BtnCloseFilter.Click
        TmCloseFilter.Enabled = True
        open_close_filter = 0
    End Sub

    Private Sub BtnResetFilter_Click(sender As Object, e As EventArgs) Handles BtnResetFilter.Click
        sem1 = ""
        sem2 = ""
        stage1 = ""
        stage2 = ""
        stat1 = ""
        stat2 = ""
        schoolyear = ""
        collg = ""
        dept = ""
        ChckBx1st.Checked = False
        ChckBx2nd.Checked = False
        ChckBxSumm.Checked = False
        ChckBxStageRp.Checked = False
        ChckBxStageFtc.Checked = False
        ChckBxStatFT.Checked = False
        ChckBxStatPt.Checked = False
        TxtCol.Clear()
        CbxSc.Text = ""
        TxtDept.Clear()
        LoadRcfRgaRecords()
        LblSearchFound.Text = ""
        TxtSearch.Text = "Seacrh ID, name, semester,  stage, college etc..."
    End Sub

    'Variables for holding filter
    Dim sem1, sem2, summer As String
    Dim stage1, stage2 As String
    Dim stat1, stat2 As String
    Dim schoolyear As String
    Dim collg As String
    Dim dept As String
    'FILTER SEARCH FUNCTION
    Private Sub FilterSearch()

        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM `rcf_rga` WHERE " & schoolyear & collg & dept & sem1 & sem2 & summer & stage1 & stage2 & stat1 & stat2
            Using cmd As New MySqlCommand(query, con)
                Dim adptr As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable
                adptr.Fill(dt)

                If dt.Rows.Count > 0 Then
                    DgvSwData.DataSource = dt
                    DgvSwData.Refresh()
                    For i = 0 To DgvSwData.Rows.Count - 1
                        DgvSwData.Rows(i).Height = 50
                    Next
                    BtnRemoveSelection.PerformClick()
                    LblSearchFound.Text = dt.Rows.Count.ToString & " Result(s) found"
                Else
                    DgvSwData.DataSource = dt
                    DgvSwData.Refresh()
                    LblSearchFound.Text = dt.Rows.Count.ToString & " Result(s) found"
                    MessageBox.Show("Your search do not match to any records. Please try different keywords", "No data found in filtered.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                LoadReportData("filtered")
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occcurred Searching record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
            BtnRemoveSelection.PerformClick()
        End Try
    End Sub

    'APPLYING filter
    Private Sub BtnApplyFilter_Click(sender As Object, e As EventArgs) Handles BtnApplyFilter.Click
        If sem1 = "" And sem2 = "" And summer = "" And stage1 = "" And stage2 = "" And stat1 = "" And stat2 = "" And schoolyear = "" And collg = "" And dept = "" Then
            MessageBox.Show("Please Select or Enter to filter ", "No Entered/Selected filter", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            BtnCloseFilter.PerformClick()
            FilterSearch()
            LoadReportData("filtered")
            'MessageBox.Show(schoolyear & " " & collg & " " & dept & " " & sem1 & " " & sem2 & " " & summer & " " & stage1 & " " & stage2 & " " & stat1 & " " & stat2)

        End If
    End Sub

    Private Sub CbxSc_TextChanged(sender As Object, e As EventArgs) Handles CbxSc.TextChanged
        If CbxSc.Text.Trim <> "" Then
            schoolyear = " school_year LIKE'%" & CbxSc.Text & "%' "
        Else
            schoolyear = ""
        End If
        TxtDept_TextChanged(Nothing, EventArgs.Empty)
        TxtCol_TextChanged(Nothing, EventArgs.Empty)
        CheckedUnchecked()
    End Sub

    Private Sub TxtCol_TextChanged(sender As Object, e As EventArgs) Handles TxtCol.TextChanged
        If TxtCol.Text.Trim <> "" And schoolyear <> "" Then
            collg = " AND college LIKE '%" & TxtCol.Text.Trim.ToString & "%' "
        ElseIf TxtCol.Text.Trim <> "" And schoolyear = "" Then
            collg = " college LIKE '%" & TxtCol.Text.Trim.ToString & "%' "
        Else
            collg = ""
        End If
        TxtDept_TextChanged(Nothing, EventArgs.Empty)
        CheckedUnchecked()
    End Sub

    Private Sub TxtDept_TextChanged(sender As Object, e As EventArgs) Handles TxtDept.TextChanged
        If TxtDept.Text.Trim <> "" And collg <> "" And schoolyear <> "" Then
            dept = " AND dept LIKE '%" & TxtDept.Text.Trim.ToString & "%' "
        ElseIf TxtDept.Text.Trim <> "" Then
            If collg <> "" Or schoolyear <> "" Then
                dept = " AND dept LIKE '%" & TxtDept.Text.Trim.ToString & "%' "
            Else
                dept = " dept LIKE '%" & TxtDept.Text.Trim.ToString & "%' "
            End If
        Else
            dept = ""
        End If
        CheckedUnchecked()
    End Sub


    'SEMESTER FILTER CHECKBOX
    Private Sub ChckBx1st_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBx1st.MouseClick

        CheckedUnchecked()
    End Sub

    Private Sub ChckBx2nd_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBx2nd.MouseClick

        CheckedUnchecked()
    End Sub

    Private Sub ChckBxSumm_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxSumm.MouseClick

        CheckedUnchecked()
    End Sub

    'STAGE
    Private Sub ChckBxStageRp_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxStageRp.MouseClick

        CheckedUnchecked()
    End Sub

    Private Sub BtReport_Click(sender As Object, e As EventArgs) Handles BtReport.Click

        Dim rrf As New ReportRcf
        rrf.Show()
        'report book is my report 
        Dim rcf_rga_report As New report_rcf_rga
        rcf_rga_report.Database.Tables("rcf_rga").SetDataSource(rcf_data)
        'setting crystal report viewer'source
        rrf.CrvRCF.ReportSource = rcf_rga_report

    End Sub

    Private Sub LoadReportData(filter_load_search As String)
        'get all data from rcf rga and requirements
        con.Close()
        Dim qry As String = ""
        Try
            con.Open()
            If filter_load_search = "load" Then
                qry = "
                    SELECT 
                        rcf_rga.*,
                        rcf_rga_req.*
                    FROM rcf_rga
                    INNER JOIN rcf_rga_req
                        ON rcf_rga_req.rcf_rga_req_id = rcf_rga.record_id
                      "

            ElseIf filter_load_search = "searched" Then
                qry = "
                    SELECT 
                        rcf_rga.*,
                        rcf_rga_req.*
                    FROM rcf_rga
                    INNER JOIN rcf_rga_req
                        ON rcf_rga_req.rcf_rga_req_id = rcf_rga.record_id
                    WHERE 
                        record_id LIKE @to_search
                        OR semester LIKE @to_search
                        OR school_year LIKE @to_search
                        OR stage LIKE @to_search
                        OR name LIKE @to_search
                        OR college LIKE @to_search
                        OR dept LIKE @to_search
                        OR status LIKE @to_search
                        OR role LIKE @to_search
"
            ElseIf filter_load_search = "filtered" Then
                qry = "
                    SELECT 
                        rcf_rga.*,
                        rcf_rga_req.*
                    FROM rcf_rga
                    INNER JOIN rcf_rga_req
                        ON rcf_rga_req.rcf_rga_req_id = rcf_rga.record_id
                    WHERE " & schoolyear & collg & dept & sem1 & sem2 & summer & stage1 & stage2 & stat1 & stat2
            End If

            Using cmd As New MySqlCommand(qry, con)
                cmd.Parameters.AddWithValue("@to_search", "%" & TxtSearch.Text.Trim & "%")
                Dim adptr As New MySqlDataAdapter(cmd)
                rcf_data.Clear()
                adptr.Fill(rcf_data)
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occcurred generating report of record", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub ChckBxStageFtc_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxStageFtc.MouseClick

        CheckedUnchecked()
    End Sub

    'STATUS
    Private Sub ChckBxStatFT_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxStatFT.MouseClick

        CheckedUnchecked()
    End Sub
    Private Sub ChckBxStatPt_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxStatPt.MouseClick

        CheckedUnchecked()
    End Sub


    Private Sub CheckedUnchecked()

        'SEMESTER
        If ChckBx1st.Checked = True Then

            If collg <> "" Or dept <> "" Or schoolyear <> "" Then

                sem1 = " AND semester = '1st Semester' "

                If ChckBx2nd.Checked = True And ChckBxSumm.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    sem2 = " OR semester = '2nd Semester'"
                    summer = " OR semester = 'Summer')"
                ElseIf ChckBx2nd.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    sem2 = " OR semester = '2nd Semester')"
                ElseIf ChckBxSumm.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    summer = " OR semester = 'Summer')"
                End If
            Else
                sem1 = " semester = '1st Semester'"
                If ChckBx2nd.Checked = True Then
                    sem2 = " OR semester = '2nd Semester'"
                End If
                If ChckBxSumm.Checked = True Then
                    summer = " OR semester = 'Summer'"
                End If
            End If
        Else
            sem1 = ""
        End If

        If ChckBx2nd.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Then

                If ChckBx1st.Checked = True And ChckBxSumm.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    sem2 = " OR semester = '2nd Semester'"
                    summer = " OR semester = 'Summer')"
                ElseIf ChckBx1st.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    sem2 = " OR semester = '2nd Semester')"
                ElseIf ChckBxSumm.Checked = True Then
                    sem2 = " AND (semester = '2nd Semester'"
                    summer = " OR semester = 'Summer')"
                Else
                    sem2 = " AND semester = '2nd Semester'"
                End If
            ElseIf sem1 <> "" Then

                If ChckBx1st.Checked = True Then
                    sem2 = " OR semester = '2nd Semester'"
                Else
                    sem2 = " AND semester = '2nd Semester'"
                End If
                If ChckBxSumm.Checked = True Then
                    summer = " OR semester = 'Summer'"
                End If
            Else
                sem2 = " semester = '2nd Semester'"
                If ChckBxSumm.Checked = True Then
                    summer = " OR semester = 'Summer'"
                End If
            End If
        Else
            sem2 = ""
        End If

        If ChckBxSumm.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Then


                If ChckBx1st.Checked = True And ChckBx2nd.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    sem2 = " OR semester = '2nd Semester'"
                    summer = " OR semester = 'Summer')"
                ElseIf ChckBx1st.Checked = True Then
                    sem1 = " AND (semester = '1st Semester'"
                    summer = " OR semester = 'Summer')"
                ElseIf ChckBx2nd.Checked = True Then
                    sem2 = " AND (semester = '2nd Semester'"
                    summer = " OR semester = 'Summer')"
                Else
                    summer = " AND semester = 'Summer'"
                End If
            ElseIf sem1 <> "" Or sem2 <> "" Then
                If ChckBx1st.Checked = True Or ChckBx2nd.Checked = True Then
                    summer = " OR semester = 'Summer'"
                Else
                    summer = " AND semester = 'Summer'"
                End If
            Else
                summer = " semester = 'Summer'"
            End If
        Else
            summer = ""
        End If

        'STAGE
        If ChckBxStageRp.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Or sem1 <> "" Or sem2 <> "" Or summer <> "" Then

                stage1 = " AND stage = 'Research Proposal'"

                If ChckBxStageFtc.Checked = True Then
                    stage1 = " AND (stage = 'Research Proposal'"
                    stage2 = " OR stage = 'Final Thesis')"
                End If

            Else
                stage1 = " stage = 'Research Proposal'"
                If ChckBxStageFtc.Checked = True Then
                    stage2 = " OR stage = 'Final Thesis'"
                End If
            End If
        Else
            stage1 = ""
        End If

        If ChckBxStageFtc.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Or sem1 <> "" Or sem2 <> "" Or summer <> "" Then

                stage2 = " AND stage = 'Final Thesis'"
                If ChckBxStageRp.Checked = True Then
                    stage1 = " AND (stage = 'Research Proposal'"
                    stage2 = " OR stage = 'Final Thesis')"
                End If

            ElseIf stage1 <> "" Then
                stage2 = " OR stage = 'Final Thesis'"
            Else
                stage2 = " stage = 'Final Thesis'"
            End If
        Else
            stage2 = ""
        End If

        'STATUS
        If ChckBxStatFT.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Or sem1 <> "" Or sem2 <> "" Or summer <> "" Or stage1 <> "" Or stage2 <> "" Then

                stat1 = " AND status = 'Full Time'"

                If ChckBxStatPt.Checked = True Then
                    stat1 = " AND (status = 'Full Time'"
                    stat2 = " OR status = 'Part Time')"
                End If

            Else
                stat1 = " status = 'Full Time'"
                If ChckBxStatPt.Checked = True Then
                    stat2 = " OR status = 'Part Time'"
                End If
            End If
        Else
            stat1 = ""
        End If

        If ChckBxStatPt.Checked = True Then
            If collg <> "" Or dept <> "" Or schoolyear <> "" Or sem1 <> "" Or sem2 <> "" Or summer <> "" Or stage1 <> "" Or stage2 <> "" Then

                stat2 = " AND status = 'Part Time'"
                If ChckBxStatFT.Checked = True Then
                    stat1 = " AND (status = 'Full Time'"
                    stat2 = " OR status = 'Part Time')"
                End If
            ElseIf stat1 <> "" Then
                stat2 = " OR status = 'Part Time'"
            Else
                stat2 = " status = 'Part Time'"
            End If
        Else
            stat2 = ""
        End If
    End Sub




End Class