
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
    Dim efrpd_stat = "", efrpd_date = "", efrpd_remarks As String = ""
    Dim pdod_stat = "", pdod_date = "", pdod_remarks As String = ""
    Dim csor_stat = "", csor_date = "", csor_remarks As String = ""

    'reuirements for research group adviser variables
    Dim al_stat = "", al_date = "", al_remarks As String = ""
    Dim cf_stat = "", cf_date = "", cf_remarks As String = ""

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

    'FORM LOAD
    Private Sub AddResearchCourseFacilitatorGroupAdviserRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        GenerateRecordID()
    End Sub

   

    'STAGE RADIO BUTTONS CLICK 
    Private Sub RdStageResearchProposal_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStageResearchProposal.MouseClick
        LblStage.Text = RdStageResearchProposal.Text
        LblStage.Visible = True
        LblChangeabletext.Text = "Evaluation Forms for Research Proposal 
Defense from the panel members"
        PnlInfo.Enabled = True
        PnlRRCF.Enabled = True
        PnlRRGA.Enabled = True

        stage = "Research Proposal"
    End Sub

    Private Sub RdStageFinalThesis_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStageFinalThesis.MouseClick
        LblStage.Text = RdStageFinalThesis.Text
        LblStage.Visible = True
        LblChangeabletext.Text = "Evaluation Forms for FINAL THESIS DEFENSE 
from the panel members"
        PnlInfo.Enabled = True
        PnlRRCF.Enabled = True
        PnlRRGA.Enabled = True

        stage = "Final Thesis"
    End Sub

    'STATUS RADIO BUTTONS CLICK
    Private Sub RdStatusFullTime_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatusFullTime.MouseClick

        status = RdStatusFullTime.Text
    End Sub

    Private Sub RdStatusPartTime_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatusPartTime.MouseClick
        status = RdStatusPartTime.Text
    End Sub

    Private Sub ChckBxRCF_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxRCF.MouseClick
        If ChckBxRCF.Checked = True Then
            role1 = ChckBxRCF.Text
        Else
            role1 = ""
        End If

    End Sub

    Private Sub ChckBxRGA_MouseClick(sender As Object, e As MouseEventArgs) Handles ChckBxRGA.MouseClick
        If ChckBxRGA.Checked = True Then
            role2 = ChckBxRGA.Text
        Else
            role2 = ""
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
        efrpd_stat = Rd2ndStatSubmitted.Text
    End Sub
    Private Sub Rd2ndStatUnsubmitted_MouseClick(sender As Object, e As MouseEventArgs) Handles Rd2ndStatUnsubmitted.MouseClick
        efrpd_stat = Rd2ndStatUnsubmitted.Text
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
        name_input = TxtName.Text
        college = TxtCollege.Text
        department = TxtDepartment.Text
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
        elod_remarks = TxtRemarksEvaluation.Text.Trim

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
            If elod_stat = "" Or efrpd_stat = "" Or pdod_stat = "" Or csor_stat = "" Then
                MessageBox.Show("Please select status on Requirements for Research Course Facilitator", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ElseIf al_stat = "" Or cf_stat = "" Then
                MessageBox.Show("Requirements for Research Group’s Adviser:", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                is_save_ready = True
            End If
        End If

        If is_save_ready Then
            con.Close()

            Try
                con.Open()
                Dim query As String = "INSERT INTO `rcf_rga`
                        (`no#`, `record_id`, `semester`, `school_year`, `stage`, `name`, `college`, `dept`, `status`, `role`)
                    VALUES
                        (@no, @record_id, @semester, @school_year, @stage, @name, @college, @dept, @status, @role)"
                Using cmd As New MySqlCommand(query, con)
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
                ClearTextBox_UncheckedRd(Me)
                MessageBox.Show("Record Saved Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred on saving record", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
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
    End Sub
End Class

