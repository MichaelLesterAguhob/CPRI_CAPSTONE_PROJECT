
Imports MySql.Data.MySqlClient

Public Class AddResearchCourseFacilitatorGroupAdviserRecord

    Dim record_id = 0, to_check_record_id As Integer = 0
    Dim current_year As Integer = DateTime.Now.Year

    'main info variables
    Dim semester As String = ""
    Dim school_year As String = ""
    Dim stage As String = ""

    Dim name_input As String = ""
    Dim college As String = ""
    Dim department As String = ""
    Dim status As String = ""
    Dim role1 = "", role2 As String = ""

    'reuirements for research course facilitator variables
    Dim elod_stat = "", elod_date = "", elod_remarks As String = ""
    Dim efrpd_stat = "", efrFd_stat = "", efrpd_date = "", efrpd_remarks As String = ""
    Dim pdod_stat = "", pdod_date = "", pdod_remarks As String = ""
    Dim csor_stat = "", csor_date = "", csor_remarks As String = ""

    'reuirements for research group adviser variables
    Dim al_stat = "", al_date = "", al_remarks As String = ""
    Dim cf_stat = "", cf_date = "", cf_remarks As String = ""

    Dim is_form_close_by_user As Boolean = False


    Private Sub GenerateRecordID()
        Dim rnd As New Random()
        to_check_record_id = rnd.Next(10000, 99999)
        IsRecordIdUnique()
    End Sub

    Private Sub IsRecordIdUnique()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT record_id FROM rcf_rga WHERE record_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & to_check_record_id)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateRecordID()
                Else
                    record_id = to_check_record_id
                    TxtID.Text = current_year.ToString & record_id.ToString
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 001: Failed to check the uniqueness of generated ID")
        Finally
            con.Close()
        End Try
    End Sub


    'CODE TO USE FUNCTION FROM MY OTHER FORM
    Private ReadOnly rcf_rga_form As ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
    Public Sub New(ByVal rcf_rga_form As ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.rcf_rga_form = rcf_rga_form
    End Sub


    'FORM LOAD
    Private Sub AddResearchCourseFacilitatorGroupAdviserRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        GenerateRecordID()
        TxtSchoolYear.Select()
    End Sub

   

    'STAGE RADIO BUTTONS CLICK 
    Private Sub RdStageResearchProposal_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStageResearchProposal.MouseClick
        LblStage.Text = RdStageResearchProposal.Text
        LblStage.Visible = True
        LblChangeabletext.Text = "Evaluation Forms for Research Proposal 
Defense from the panel members"
        PnlInfo.Enabled = True
        PnlRRCF.Enabled = False
        PnlRRGA.Enabled = False
        efrpd_stat = ""
        efrFd_stat = ""
        Rd2ndStatSubmitted.Checked = False
        Rd2ndStatUnsubmitted.Checked = False
        stage = "Research Proposal"
    End Sub

    Private Sub RdStageFinalThesis_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStageFinalThesis.MouseClick
        LblStage.Text = RdStageFinalThesis.Text
        LblStage.Visible = True
        LblChangeabletext.Text = "Evaluation Forms for FINAL THESIS DEFENSE 
from the panel members"
        PnlInfo.Enabled = True
        PnlRRCF.Enabled = False
        PnlRRGA.Enabled = False
        efrpd_stat = ""
        efrFd_stat = ""
        Rd2ndStatSubmitted.Checked = False
        Rd2ndStatUnsubmitted.Checked = False
        stage = "Final Thesis"
    End Sub

    'STATUS RADIO BUTTONS CLICK
    Private Sub RdStatusFullTime_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatusFullTime.MouseClick

        status = RdStatusFullTime.Text
    End Sub

    Private Sub RdStatusPartTime_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatusPartTime.MouseClick
        status = RdStatusPartTime.Text
    End Sub

    'CLEAR QUIREMENTS FUNCTIONS
    Private Sub ClearRCF()
        PnlRRCF.Enabled = False

        Rd1stStatSubmitted.Checked = False
        Rd1stStatUnsubmitted.Checked = False
        elod_stat = ""
        TxtRemarksEndorsement.Clear()

        Rd2ndStatSubmitted.Checked = False
        Rd2ndStatUnsubmitted.Checked = False
        efrpd_stat = ""
        efrFd_stat = ""
        TxtRemarksEvaluation.Clear()

        Rd3rdStatSubmitted.Checked = False
        Rd3rdStatUnsubmitted.Checked = False
        pdod_stat = ""
        TxtRemarksDocumentation.Clear()

        Rd4thStatSubmitted.Checked = False
        Rd4thStatUnsubmitted.Checked = False
        pdod_stat = ""
        TxtRemarksReceipt.Clear()
    End Sub

    Private Sub ClearRga()
        PnlRRGA.Enabled = False

        RdStatSubmittedAL.Checked = False
        RdStatUnsubmittedAL.Checked = False
        al_stat = ""
        TxtRemarksAL.Clear()

        RdStatSubmittedCF.Checked = False
        RdStatUnsubmittedCF.Checked = False
        cf_stat = ""
        TxtRemarksCF.Clear()
    End Sub

    Private Sub ChckBxRCF_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxRCF.MouseClick
        If ChckBxRCF.Checked = True Then
            role1 = ChckBxRCF.Text
            PnlRRCF.Enabled = True
        Else
            role1 = ""
            ClearRCF()
        End If
    End Sub

    Private Sub ChckBxRGA_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxRGA.MouseClick
        If ChckBxRGA.Checked = True Then
            role2 = ChckBxRGA.Text
            PnlRRGA.Enabled = True
        Else
            role2 = ""
            ClearRga()
        End If

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        is_form_close_by_user = True
        Me.Close()
    End Sub

    Private Sub CmbxSemester_TextChanged(sender As Object, e As EventArgs) Handles CmbxSemester.TextChanged
        If CmbxSemester.Text <> "Select Semester" And CmbxSemester.Text <> "" Then
            CmbxSemester.ForeColor = Color.Black
            TxtSchoolYear.Focus()
        ElseIf CmbxSemester.Text = "" Then
            CmbxSemester.ForeColor = Color.Gray
            CmbxSemester.Text = "Select Semester"
            TxtSchoolYear.Focus()
        End If
    End Sub

    Private Sub DtSubmittedDateEndorsement_ValueChanged(sender As Object, e As EventArgs) Handles DtSubmittedDateEndorsement.ValueChanged
        If DtSubmittedDateEndorsement.Value > DateTime.Now Then
            DtSubmittedDateEndorsement.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtSubmittedDateEvaluation_ValueChanged(sender As Object, e As EventArgs) Handles DtSubmittedDateEvaluation.ValueChanged
        If DtSubmittedDateEvaluation.Value > DateTime.Now Then
            DtSubmittedDateEvaluation.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtSubmittedDateDocumentation_ValueChanged(sender As Object, e As EventArgs) Handles DtSubmittedDateDocumentation.ValueChanged
        If DtSubmittedDateDocumentation.Value > DateTime.Now Then
            DtSubmittedDateDocumentation.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtSubmittedDateReceipt_ValueChanged(sender As Object, e As EventArgs) Handles DtSubmittedDateReceipt.ValueChanged
        If DtSubmittedDateReceipt.Value > DateTime.Now Then
            DtSubmittedDateReceipt.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtDateSubmittedAL_ValueChanged(sender As Object, e As EventArgs) Handles DtDateSubmittedAL.ValueChanged
        If DtDateSubmittedAL.Value > DateTime.Now Then
            DtDateSubmittedAL.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtDateSubmittedCF_ValueChanged(sender As Object, e As EventArgs) Handles DtDateSubmittedCF.ValueChanged
        If DtDateSubmittedCF.Value > DateTime.Now Then
            DtDateSubmittedCF.Value = DateTime.Now
            MessageBox.Show("You've selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    'Requirements for Research Course Facilitator
    'Endorsement Letters for Oral Defense radio button
    Private Sub Rd1stStatSubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd1stStatSubmitted.MouseClick
        elod_stat = Rd1stStatSubmitted.Text
    End Sub
    Private Sub Rd1stStatUnsubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd1stStatUnsubmitted.MouseClick
        elod_stat = Rd1stStatUnsubmitted.Text
    End Sub

    'Evaluation Forms for Research Proposal Defense from the panel members
    Private Sub Rd2ndStatSubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd2ndStatSubmitted.MouseClick
        If RdStageResearchProposal.Checked = True Then
            efrpd_stat = Rd2ndStatSubmitted.Text
            efrFd_stat = "-------"
        Else
            efrFd_stat = Rd2ndStatSubmitted.Text
            efrpd_stat = "-------"
        End If
    End Sub
    Private Sub Rd2ndStatUnsubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd2ndStatUnsubmitted.MouseClick
        If RdStageResearchProposal.Checked = True Then
            efrpd_stat = Rd2ndStatUnsubmitted.Text
            efrFd_stat = "-------"
        Else
            efrFd_stat = Rd2ndStatUnsubmitted.Text
            efrpd_stat = "-------"
        End If
    End Sub

    'Pictures or documentation of oral defense
    Private Sub Rd3rdStatSubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd3rdStatSubmitted.MouseClick
        pdod_stat = Rd3rdStatSubmitted.Text
    End Sub
    Private Sub Rd3rdStatUnsubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd3rdStatUnsubmitted.MouseClick
        pdod_stat = Rd3rdStatUnsubmitted.Text
    End Sub

    'Copy of students’ official receipt for research fee
    Private Sub Rd4thStatSubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd4thStatSubmitted.MouseClick
        csor_stat = Rd4thStatSubmitted.Text
    End Sub
    Private Sub Rd4thStatUnsubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd4thStatUnsubmitted.MouseClick
        csor_stat = Rd4thStatUnsubmitted.Text
    End Sub

    'Requirements for Research Group’s Adviser:
    'Appointment Letter 
    Private Sub RdStatSubmittedAL_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatSubmittedAL.MouseClick
        al_stat = RdStatSubmittedAL.Text
    End Sub
    Private Sub RdStatunSubmittedAL_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatUnsubmittedAL.MouseClick
        al_stat = RdStatUnsubmittedAL.Text
    End Sub
    'Consultation Form
    Private Sub RdStatSubmittedCF_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatSubmittedCF.MouseClick
        cf_stat = RdStatSubmittedCF.Text
    End Sub
    Private Sub RdStatunSubmittedCF_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatUnsubmittedCF.MouseClick
        cf_stat = RdStatUnsubmittedCF.Text
    End Sub


    'SAVING RECORD
    Private Sub BtnSaveRecord_Click(sender As Object, e As EventArgs) Handles BtnSaveRecord.Click

        semester = CmbxSemester.Text.ToString
        school_year = TxtSchoolYear.Text.Trim.ToString
        name_input = TxtName.Text.Trim
        college = TxtCollege.Text.Trim
        department = TxtDepartment.Text.Trim
        Dim no_blank_main_info As Boolean = False
        Dim is_save_ready As Boolean = False

        '1st 
        If Rd1stStatSubmitted.Checked = True Then
            elod_date = DtSubmittedDateEndorsement.Value.Date.ToString("MM-dd-yyyy")
        Else
            elod_date = "-------"
        End If
        elod_remarks = TxtRemarksEndorsement.Text.Trim

        '2nd
        If Rd2ndStatSubmitted.Checked = True Then
            efrpd_date = DtSubmittedDateEvaluation.Value.Date.ToString("MM-dd-yyyy")
        Else
            efrpd_date = "-------"
        End If
        efrpd_remarks = TxtRemarksEvaluation.Text.Trim

        '3rd
        If Rd3rdStatSubmitted.Checked = True Then
            pdod_date = DtSubmittedDateDocumentation.Value.Date.ToString("MM-dd-yyyy")
        Else
            pdod_date = "-------"
        End If
        pdod_remarks = TxtRemarksDocumentation.Text.Trim

        '4th
        If Rd4thStatSubmitted.Checked = True Then
            csor_date = DtSubmittedDateReceipt.Value.Date.ToString("MM-dd-yyyy")
        Else
            csor_date = "-------"
        End If
        csor_remarks = TxtRemarksReceipt.Text.Trim

        'AL
        If RdStatSubmittedAL.Checked = True Then
            al_date = DtDateSubmittedAL.Value.Date.ToString("MM-dd-yyyy")
        Else
            al_date = "-------"
        End If
        al_remarks = TxtRemarksAL.Text.Trim
        'cf
        If RdStatSubmittedCF.Checked = True Then
            cf_date = DtDateSubmittedCF.Value.Date.ToString("MM-dd-yyyy")
        Else
            cf_date = "-------"
        End If
        cf_remarks = TxtRemarksCF.Text.Trim

        If semester = "Select Semester" Or school_year = "" Then

            MessageBox.Show("Please select Semester / Enter School Year)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If stage = "" Then
                MessageBox.Show("Please Select Stage", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If name_input = "" Or college = "" Or department = "" Then
                    MessageBox.Show("Please enter Name/College/Department", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If status = "" Then
                        MessageBox.Show("Please select Status", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        If role1 = "" And role2 = "" Then
                            MessageBox.Show("Please select Role", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Else
                            no_blank_main_info = True
                        End If
                    End If
                End If
            End If
        End If

        'IF ALL MAIN INFO WAS NOT BLANK
        If no_blank_main_info Then
            If ChckBxRCF.Checked Then
                If elod_stat = "" Or efrpd_stat = "" Or pdod_stat = "" Or csor_stat = "" Then
                    MessageBox.Show("Please select status on Requirements for Research Course Facilitator", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf ChckBxRGA.Checked Then
                    If al_stat = "" Or cf_stat = "" Then
                        MessageBox.Show("Requirements for Research Group’s Adviser:", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        is_save_ready = True
                    End If
                Else
                    is_save_ready = True
                End If

            ElseIf ChckBxRGA.Checked Then
                If al_stat = "" Or cf_stat = "" Then
                    MessageBox.Show("Requirements for Research Group’s Adviser:", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    is_save_ready = True
                End If

            End If
        End If

            If is_save_ready Then
            con.Close()

            Try
                con.Open()
                Dim insert_main_info As String = "
                    INSERT INTO `rcf_rga`
                        (`no#`, `record_id`, `semester`, `school_year`, `stage`, `name`, `college`, `dept`, `status`, `role`)
                    VALUES
                        (@no, @record_id, @semester, @school_year, @stage, @name, @college, @dept, @status, @role)"
                Using cmd As New MySqlCommand(insert_main_info, con)
                    cmd.Parameters.AddWithValue("@no", Nothing)
                    cmd.Parameters.AddWithValue("@record_id", TxtID.Text)
                    cmd.Parameters.AddWithValue("@semester", semester)
                    cmd.Parameters.AddWithValue("@school_year", school_year)
                    cmd.Parameters.AddWithValue("@stage", stage)
                    cmd.Parameters.AddWithValue("@name", name_input)
                    cmd.Parameters.AddWithValue("@college", college)
                    cmd.Parameters.AddWithValue("@dept", department)
                    cmd.Parameters.AddWithValue("@status", status)
                    cmd.Parameters.AddWithValue("@role", role1 & "
" & role2)
                    cmd.ExecuteNonQuery()
                End Using
                con.Close()

                con.Open()
                Dim insert_rcf_rga As String = "
                    INSERT INTO `rcf_rga_req`
                        (
                            `no#`, 
                            `rcf_rga_req_id`, 

                            `status_endorsement_letter`, 
                            `endo_lett_sbmttd_date`, 
                            `endo_lett_remarks`, 

                            `status_evaluation_form`, 
                            `status_final_eval_form`, 
                            `eval_sbmttd_date`, 
                            `eval_remarks`, 

                            `status_pict_documentation`, 
                            `pict_docu_sbmttd_date`,
                            `pict_docu_remarks`,

                            `status_copy_student_or`, 
                            `stud_or_sbmttd_date`,
                            `stud_or_remarks`,

                            `status_appoint_lett`, 
                            `appoint_lett_sbmttd_date`,
                            `appoint_lett_remarks`,

                            `status_consult_form`, 
                            `consult_form_sbmttd_date`,
                            `consult_form_remarks`
                        )
                    VALUES
                        (
                            @no, 
                            @id, 

                            @status_endorsement_letter, 
                            @endo_lett_sbmttd_date, 
                            @endo_lett_remaks, 

                            @status_evaluation_form, 
                            @status_final_eval_form, 
                            @eval_sbmttd_date, 
                            @eval_remarks, 

                            @status_pict_documentation, 
                            @pict_docu_sbmttd_date,
                            @pict_docu_remarks,

                            @status_copy_student_or, 
                            @stud_or_sbmttd_date,
                            @stud_or_remarks,

                            @status_appoint_lett, 
                            @appoint_lett_sbmttd_date,
                            @appoint_lett_remarks,

                            @status_consult_form, 
                            @consult_form_sbmttd_date,
                            @consult_form_remarks
                        )"
                Using cmd2 As New MySqlCommand(insert_rcf_rga, con)
                    cmd2.Parameters.AddWithValue("@no", Nothing)
                    cmd2.Parameters.AddWithValue("@id", TxtID.Text)

                    cmd2.Parameters.AddWithValue("@status_endorsement_letter", elod_stat)
                    cmd2.Parameters.AddWithValue("@endo_lett_sbmttd_date", elod_date)
                    cmd2.Parameters.AddWithValue("@endo_lett_remaks", elod_remarks)

                    cmd2.Parameters.AddWithValue("@status_evaluation_form", efrpd_stat)
                    cmd2.Parameters.AddWithValue("@status_final_eval_form", efrFd_stat)
                    cmd2.Parameters.AddWithValue("@eval_sbmttd_date", efrpd_date)
                    cmd2.Parameters.AddWithValue("@eval_remarks", efrpd_remarks)

                    cmd2.Parameters.AddWithValue("@status_pict_documentation", pdod_stat)
                    cmd2.Parameters.AddWithValue("@pict_docu_sbmttd_date", pdod_date)
                    cmd2.Parameters.AddWithValue("@pict_docu_remarks", pdod_remarks)

                    cmd2.Parameters.AddWithValue("@status_copy_student_or", csor_stat)
                    cmd2.Parameters.AddWithValue("@stud_or_sbmttd_date", csor_date)
                    cmd2.Parameters.AddWithValue("@stud_or_remarks", csor_remarks)

                    cmd2.Parameters.AddWithValue("@status_appoint_lett", al_stat)
                    cmd2.Parameters.AddWithValue("@appoint_lett_sbmttd_date", al_date)
                    cmd2.Parameters.AddWithValue("@appoint_lett_remarks", al_remarks)

                    cmd2.Parameters.AddWithValue("@status_consult_form", cf_stat)
                    cmd2.Parameters.AddWithValue("@consult_form_sbmttd_date", cf_date)
                    cmd2.Parameters.AddWithValue("@consult_form_remarks", cf_remarks)

                    cmd2.ExecuteNonQuery()
                End Using

                MessageBox.Show("Record Saved Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearTextBox_UncheckedRd(Me)
                GenerateRecordID()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred on saving record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
                rcf_rga_form.LoadRcfRgaRecords()
                rcf_rga_form.BtnRemoveSelection.PerformClick()
                rcf_rga_form.PanelRcfrgareq.Visible = False
            End Try
        End If
    End Sub

    Private Sub ClearTextBox_UncheckedRd(ByVal parent_control As Control)
        For Each cntrl As Control In parent_control.Controls
            If TypeOf cntrl Is TextBox Then
                Dim txt_box As TextBox = CType(cntrl, TextBox)
                txt_box.Text = String.Empty
            ElseIf cntrl.HasChildren Then
                ClearTextBox_UncheckedRd(cntrl)
            End If
        Next
        For Each cntrl As Control In parent_control.Controls
            If TypeOf cntrl Is RadioButton Then
                Dim rd_btn As RadioButton = CType(cntrl, RadioButton)
                rd_btn.Checked = False
            ElseIf cntrl.HasChildren Then
                ClearTextBox_UncheckedRd(cntrl)
            End If
        Next
        For Each cntrl As Control In parent_control.Controls
            If TypeOf cntrl Is CheckBox Then
                Dim chckbx_btn As CheckBox = CType(cntrl, CheckBox)
                chckbx_btn.Checked = False
            ElseIf cntrl.HasChildren Then
                ClearTextBox_UncheckedRd(cntrl)
            End If
        Next

        CmbxSemester.Text = "Select Semester"
        PnlRRCF.Enabled = False
        PnlRRGA.Enabled = False

        role1 = ""
        role2 = ""
        stage = ""
        status = ""

        elod_stat = ""
        efrFd_stat = ""
        efrpd_stat = ""
        pdod_stat = ""
        csor_stat = ""
        al_stat = ""
        cf_stat = ""

        elod_remarks = ""
        efrpd_remarks = ""
        pdod_remarks = ""
        csor_remarks = ""
        al_remarks = ""
        cf_remarks = ""
    End Sub

    Private Sub AddResearchCourseFacilitatorGroupAdviserRecord_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isForm1Closed Then
            e.Cancel = False
        Else
            If is_form_close_by_user Then
                Dim confirm_exit As DialogResult = MessageBox.Show("Are you sure you want to Close this Form? By clicking 'Yes', Changes will not be saved.", "Click 'Yes' to Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                If confirm_exit = DialogResult.Yes Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If
            Else
                e.Cancel = False
            End If
        End If
    End Sub
End Class

