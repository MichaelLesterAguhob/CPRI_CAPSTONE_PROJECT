
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class AddWorks

    'variables for upper fields
    Dim control_no As Integer = 0
    Dim research_agenda As String = ""
    Dim research_title As String = ""
    Dim author_name As String = ""
    Dim author_deg As String = ""
    Dim author_role As String = ""
    Dim semester As String = ""
    Dim school_year As String = ""
    Dim date_completed As String = ""
    Dim isDynamicFieldsNotBlanks As Boolean = True

    Dim status As String = ""

    Dim whole_file_path As String
    Dim whole_file_data As Byte()
    Dim whole_file_extension As String

    Dim publish_level As String = ""
    Dim presented_level As String = ""

    'variables published presented
    Dim pub_lvl As String = ""
    Dim acad_jrnl As String = ""
    Dim vol_no As Integer = 0
    Dim issue_no As Integer = 0
    Dim page_range As String = ""
    Dim date_pub As String = ""
    Dim doi_url As String = ""

    Dim pre_lvl As String = ""
    Dim res_conf_name As String = ""
    Dim date_prsntd As String = ""
    Dim place_prsnttn As String = ""

    Dim control_number As Integer
    Dim initial_cntrl_nmbr As Long
    ReadOnly date_time As DateTime = DateTime.Now
    ReadOnly current_year As Integer = date_time.Year
    '|||||||||||||||||||||||||||||||||| CODES FOR MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||

    'GENERATING UNIQUE CONTROL NUMBER THAT WILL BE ASSIGNED TO RESEARCH RECORDS and CHECKING ITS UNIQUENESS
    Private Sub GenerateControlNumber()
        Dim rnd As New Random()
        initial_cntrl_nmbr = rnd.Next(10000, 99999)
        IsControlNumberUnique()
    End Sub

    Private Sub IsControlNumberUnique()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT abstract_id FROM sw_abstract WHERE abstract_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & initial_cntrl_nmbr)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateControlNumber()
                Else
                    control_number = initial_cntrl_nmbr
                    TxtResearchID.Text = current_year.ToString & control_number.ToString
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 001: Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub

    'FUNCTIONS WHEN ADDWORKS FORM IS LOAD
    Private Sub AddWorks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'trying to connect to database to determine if connection is ready
        Try
            ConOpen()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 002: Form Load")
        Finally
            con.Close()
        End Try

        'generate 6 fields for new co-author
        TxtAddAuthX.Text = "5"
        BtnAddNewCoAuthor.PerformClick()

        'find the component to enable focusing on it
        Dim co_author_dynamic_name As String = "CoAuthor1".ToString()
        Dim co_auth_field_name As TextBox = CType(Me.Controls.Find(co_author_dynamic_name, True).FirstOrDefault(), TextBox)
        co_auth_field_name.Focus()
        GenerateControlNumber()
    End Sub


    'CODES TO LOAD RESEARCH WORK IN REPO MANAGER
    Private ReadOnly rrm As ResearchRepoManager
    Private ReadOnly frm1 As Form1
    Public Sub New(ByVal rrm As ResearchRepoManager, ByVal frm1 As Form1)
        InitializeComponent()
        Me.rrm = rrm
        Me.frm1 = frm1
    End Sub

    'SAVING INFORMATION ENETERED
    Dim print_clearance As Boolean = False
    Private Sub BtnSaveResearch_Click(sender As Object, e As EventArgs) Handles BtnSaveResearch.Click
        BtnSaveResearch.Enabled = False
        control_no = Convert.ToInt64(TxtResearchID.Text)
        research_agenda = TxtRsrchAgenda.Text.Trim
        research_title = TxtRsrchTitle.Text.Trim
        author_name = TxtAuthorName.Text.Trim
        author_deg = TxtAthrDegprog.Text.Trim
        author_role = TxtAthrRole.Text.Trim
        semester = CbxSem.Text.Trim
        school_year = TxtSchoolYear.Text.Trim


        If RdStatCmpltd.Checked = True Then
            date_completed = DtCompletedDate.Value.Date.ToString("MM-dd-yyyy")
        Else
            date_completed = "None"
        End If


        con.Close()

        If control_no <> 0 And research_agenda <> "" And research_title <> "" And author_name <> "" And author_deg <> "" And author_role <> "" And whole_file_path <> "" And abstract_file_path <> "" And semester <> "Select Semester" And school_year <> "Enter School Year" And status <> "" Then

            'Checking the dynamic textbox component if not blank
            Dim auth_no As Integer = 0
            Dim co_author_fields_to_save As Integer = Convert.ToInt64(LblTotalFields.Text)
            'name used in dynamic textbox for co-authors  [CoAuthor, CoAuthorDeg, CoAuthorRole & auth_count]
            While auth_no <> co_author_fields_to_save
                auth_no += 1
                Dim CoAuth_dynmc_name As String = "CoAuthor" & auth_no.ToString()
                Dim CoAuth_name_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_name, True).FirstOrDefault(), TextBox)

                Dim CoAuth_dynmc_deg As String = "CoAuthorDeg" & auth_no.ToString()
                Dim CoAuth_deg_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_deg, True).FirstOrDefault(), TextBox)

                Dim CoAuth_dynmc_role As String = "CoAuthorRole" & auth_no.ToString()
                Dim CoAuth_role_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_role, True).FirstOrDefault(), TextBox)

                If total_fields = 1 And CoAuth_name_field.Text = "" And CoAuth_deg_field.Text = "" And CoAuth_role_field.Text = "" Then
                    MsgBox("no co author")
                    isDynamicFieldsNotBlanks = True
                ElseIf CoAuth_name_field.Text = "" Or CoAuth_deg_field.Text = "" Or CoAuth_role_field.Text = "" Then
                    MessageBox.Show("Fill in the blanks in Co-Author Fields.", "Fill in the Blank(s)")
                    auth_no = co_author_fields_to_save
                    isDynamicFieldsNotBlanks = False
                Else
                    isDynamicFieldsNotBlanks = True
                End If
            End While

            ' check if generated dynamic textbox are not blank
            If isDynamicFieldsNotBlanks Then

                'check if addtional info- published is checked
                If isPublished = "Published" Then
                    If publish_level <> "" And TxtPubAcadJournal.Text.Trim <> "" And TxtPubVolNum.Text.Trim <> "" And TxtPubIssueNo.Text.Trim <> "" And TxtPubPageRange.Text.Trim <> "" And TxtPubDoiUrl.Text.Trim <> "" Then
                        If IsNumeric(TxtPubVolNum.Text) And IsNumeric(TxtPubIssueNo.Text.Trim) And TxtPubDoiUrl.Text.Trim <> "" Then
                            pub_lvl = publish_level
                            acad_jrnl = TxtPubAcadJournal.Text.Trim
                            vol_no = Convert.ToInt64(TxtPubVolNum.Text.Trim)
                            issue_no = Convert.ToInt64(TxtPubIssueNo.Text.Trim)
                            page_range = TxtPubPageRange.Text.Trim.ToString
                            date_pub = DtPubDate.Value.Date.ToString("MM-dd-yyyy")
                            doi_url = TxtPubDoiUrl.Text.Trim

                            SaveUpperInputs()
                            SavePublishedInfo()
                        Else
                            MessageBox.Show("You entered non-numeric in the in additional information field(s)", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Else
                        MessageBox.Show("Fill in the blank(s)", "No Input | Check Additional Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    'check if additonal info-presented is checked
                ElseIf isPresented = "Presented" Then

                    If TxtPreResConfName.Text.Trim <> "" And TxtPrePlace.Text.Trim <> "" And presented_level <> "" Then

                        pre_lvl = presented_level
                        res_conf_name = TxtPreResConfName.Text.Trim
                        date_prsntd = DtPrsntdDate.Value.Date.ToString("MM-dd-yyyy")
                        place_prsnttn = TxtPrePlace.Text.Trim

                        SaveUpperInputs()
                        SavePresentedInfo()
                    Else
                        MessageBox.Show("Fill in the blank(s)", "No Input | Check Additional Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    SaveUpperInputs()

                End If

            End If

        Else
            MessageBox.Show("Fill in the blank field(s) before saving", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        BtnSaveResearch.Enabled = True
        rrm.LoadScholarlyWorks()
        frm1.LoadAllDisplayData()
        rrm.BtnRemoveSelection.PerformClick()
    End Sub


    Private Sub SaveUpperInputs()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
                        INSERT INTO `scholarly_works`
                        (
                            `no#`, 
                            `sw_id`, 
                            `title`, 
                            `research_agenda`, 
                            `semester`, 
                            `school_year`, 
                            `status_ongoing_completed`, 
                            `date_completed`,
                            `published`, 
                            `presented` 
                        ) 
                        VALUES 
                        (
                            @no, 
                            @sw_id, 
                            @title, 
                            @research_agenda, 
                            @sem, 
                            @sc, 
                            @status_ongoing_completed, 
                            @date_cmpltd,
                            @published,  
                            @presented
                        )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@sw_id", control_no)
                cmd.Parameters.AddWithValue("@title", research_title)
                cmd.Parameters.AddWithValue("@research_agenda", research_agenda)
                cmd.Parameters.AddWithValue("@sem", semester)
                cmd.Parameters.AddWithValue("@sc", school_year)
                cmd.Parameters.AddWithValue("@date_cmpltd", date_completed)
                cmd.Parameters.AddWithValue("@status_ongoing_completed", status)
                cmd.Parameters.AddWithValue("@published", isPublished)
                cmd.Parameters.AddWithValue("@presented", isPresented)
                cmd.ExecuteNonQuery()
            End Using

            'adding quantity and location
            Dim query2 As String = "
            INSERT INTO `qnty_loc`
                 (  
                    `sw_id`,
                    `quantity`,
                    `location`
                 )
            VALUES
                (  
                    @id,
                    @qnty,
                    @loc
                 )
                "
            Using cmd2 As New MySqlCommand(query2, con)
                cmd2.Parameters.AddWithValue("@id", control_no)
                cmd2.Parameters.AddWithValue("@qnty", Convert.ToInt64(TxtCopies.Text.Trim))
                cmd2.Parameters.AddWithValue("@loc", TxtLoc.Text.Trim)
                cmd2.ExecuteNonQuery()
            End Using

            SaveAuthor()
            SaveCoAuthors()
            SaveWholeFiles()
            SaveAbstractFiles()
            SaveStatCmpltdChckdbx()

            MessageBox.Show("Successfully Saved", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If print_clearance = True Then
                Dim print_clearance As New ReportPrintThesisClearance
                print_clearance.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 003: Failed to save upper inputs")
            con.Close()
        Finally

            con.Close()
            ClearTextBox(Me)
            GenerateControlNumber()
            TxtAddAuthX.Text = "1"
            TxtBrowsedFileAbs.Text = "No file selected"
            TxtBrowsedFileWhl.Text = "No file selected"
            DtCompletedDate.Enabled = False
        End Try
    End Sub

    'SAVE AUTHOR
    Private Sub SaveAuthor()
        con.Close()
        Dim author_id As Integer = Convert.ToInt64(TxtResearchID.Text)
        Dim author_name As String = TxtAuthorName.Text.Trim
        Dim author_deg As String = TxtAthrDegprog.Text.Trim
        Dim author_role As String = TxtAthrRole.Text.Trim

        Try
            con.Open()
            Dim query As String = "
            INSERT INTO `authors` 
            (
                `no#`, 
                `authors_id`, 
                `authors_name`, 
                `degree_program`, 
                `role`
            ) 
            VALUES 
            (
                @no,    
                @auth_id, 
                @auth_name, 
                @deg_prog, 
                @role
            )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@auth_id", author_id)
                cmd.Parameters.AddWithValue("@auth_name", author_name)
                cmd.Parameters.AddWithValue("@deg_prog", author_deg)
                cmd.Parameters.AddWithValue("@role", author_role)
                cmd.ExecuteNonQuery()
            End Using
            cmd.Parameters.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error 004: Failed on Saving Author")
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub


    '1 BY 1 SAVING CO-AUTHOR TO DATABASE LOOP
    Private Sub SaveCoAuthors()
        con.Close()
        Dim CoAuthorsID As Integer = Convert.ToInt64(TxtResearchID.Text)
        Dim auth_no As Integer = 0
        Dim co_author_fields_to_save As Integer = Convert.ToInt64(LblTotalFields.Text)
        'name used in dynamic textbox for co-authors  [CoAuthor, CoAuthorDeg, CoAuthorRole & auth_count]
        While auth_no <> co_author_fields_to_save
            auth_no += 1
            Dim CoAuth_dynmc_name As String = "CoAuthor" & auth_no.ToString()
            Dim CoAuth_name_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_name, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_deg As String = "CoAuthorDeg" & auth_no.ToString()
            Dim CoAuth_deg_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_deg, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_role As String = "CoAuthorRole" & auth_no.ToString()
            Dim CoAuth_role_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_role, True).FirstOrDefault(), TextBox)

            Try
                con.Open()
                Dim query As String = "INSERT INTO `co_authors`(`no#`, `co_authors_id`, `co_authors_name`, `degree_program`, `role`)
                VALUES(@no, @c_a_i, @c_a_n, @d_p, @role)"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@no", Nothing)
                    cmd.Parameters.AddWithValue("@c_a_i", CoAuthorsID)
                    cmd.Parameters.AddWithValue("@c_a_n", CoAuth_name_field.Text.Trim.ToString)
                    cmd.Parameters.AddWithValue("@d_p", CoAuth_deg_field.Text.Trim.ToString)
                    cmd.Parameters.AddWithValue("@role", CoAuth_role_field.Text.Trim.ToString)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed on Saving Co-Authors", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try

        End While
    End Sub


    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF FILES
    'whole file
    Private Sub BtnBrowseWholeFile_Click(sender As Object, e As EventArgs) Handles BtnBrowseWholeFile.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            whole_file_path = openFileDialog.FileName
            whole_file_data = File.ReadAllBytes(whole_file_path)
            whole_file_extension = Path.GetExtension(whole_file_path)
            TxtBrowsedFileWhl.Text = whole_file_path.ToString
        Else
            whole_file_path = ""
            TxtBrowsedFileWhl.Text = "No file selected"
        End If
    End Sub

    'abstract file
    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()
    Dim abstract_file_extension As String
    Private Sub BtnBrowseAbstractFile_Click(sender As Object, e As EventArgs) Handles BtnBrowseAbstractFile.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            abstract_file_path = openFileDialog.FileName
            abstract_file_data = File.ReadAllBytes(abstract_file_path)
            abstract_file_extension = Path.GetExtension(abstract_file_path)
            TxtBrowsedFileAbs.Text = abstract_file_path.ToString
        Else
            abstract_file_path = ""
            TxtBrowsedFileAbs.Text = "No file selected"
        End If
    End Sub


    'INSERTING PDF ABSTRACT FILES INTO DATABASE
    Private Sub SaveWholeFiles()
        con.Close()
        Dim whole_file_id As Integer = Convert.ToInt64(TxtResearchID.Text)
        Try
            con.Open()
            Dim query As String = "
            INSERT INTO `sw_whole_file` 
            (
                `no#`, 
                `whole_file_id`, 
                `file_name`, 
                `file_data`, 
                `file_type`
            )
            VALUES
            (
                @no, 
                @id, 
                @filename, 
                @filedata, 
                @filetype
            )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@id", whole_file_id)
                cmd.Parameters.AddWithValue("@filename", Path.GetFileName(whole_file_path))
                cmd.Parameters.AddWithValue("@filedata", whole_file_data)
                cmd.Parameters.AddWithValue("@filetype", whole_file_extension)
                cmd.ExecuteNonQuery()
            End Using
            cmd.Parameters.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed on Saving Abstract File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'INSERTING PDF ABSTRACT FILES IN THA DATABASE
    Private Sub SaveAbstractFiles()
        con.Close()
        Dim abstract_id As Integer = Convert.ToInt64(TxtResearchID.Text)
        Try
            con.Open()
            Dim query As String = "
            INSERT INTO `sw_abstract` 
            (
                `no#`, 
                `abstract_id`, 
                `file_name`, 
                `file_data`, 
                `file_type`
            ) 
            VALUES
            (
                @no, 
                @id, 
                @filename, 
                @filedata, 
                @filetype
            )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@id", abstract_id)
                cmd.Parameters.AddWithValue("@filename", Path.GetFileName(abstract_file_path))
                cmd.Parameters.AddWithValue("@filedata", abstract_file_data)
                cmd.Parameters.AddWithValue("@filetype", abstract_file_extension)
                cmd.ExecuteNonQuery()
            End Using
            cmd.Parameters.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed on Saving Whole File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub



    'SAVING COMPLETED CHECKED BOX INFO
    Public Sub SaveStatCmpltdChckdbx()
        If status = "Completed" Then
            con.Close()
            Dim sftCpyDate As String
            Dim hrdCopyDate As String
            Dim dgiDate As String
            Dim rgaDate As String
            Dim id As Integer = Convert.ToInt64(TxtResearchID.Text)
            If isSftCpySubmttd = "YES" Then
                sftCpyDate = DtSftCpySbmttdDate.Value.Date.ToString("MM-dd-yyyy")
            Else
                sftCpyDate = "NO"
            End If
            If isHrdCpySubmttd = "YES" Then
                hrdCopyDate = DtHrdCpySbmttdDate.Value.Date.ToString("MM-dd-yyyy")
            Else
                hrdCopyDate = "NO"
            End If
            If isDgiSubmttd = "YES" Then
                dgiDate = DtDgiSbmttdDate.Value.Date.ToString("MM-dd-yyyy")
            Else
                dgiDate = "NO"
            End If
            If isRgaSubmttd = "YES" Then
                rgaDate = DtRgaSbmttdDate.Value.Date.ToString("MM-dd-yyyy")
            Else
                rgaDate = "NO"
            End If

            Try
                con.Open()
                Dim query As String = "
            INSERT INTO `status_completed_info`
                (
                    `no#`,
                    `stat_completed_id`,
                    `soft_copy_sbmttd_date`,
                    `hard_copy_sbmttd_date`,
                    `dgi_sbmttd_date`,
                    `rga_ef_sbmttd_date`
                )
                 VALUES
                (
                    @no, 
                    @sci, 
                    @scsd, 
                    @hcsd, 
                    @dgi,
                    @rga
                )"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@no", Nothing)
                    cmd.Parameters.AddWithValue("@sci", id)
                    cmd.Parameters.AddWithValue("@scsd", sftCpyDate)
                    cmd.Parameters.AddWithValue("@hcsd", hrdCopyDate)
                    cmd.Parameters.AddWithValue("@dgi", dgiDate)
                    cmd.Parameters.AddWithValue("@rga", rgaDate)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to Save Completed Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If

    End Sub



    'SAVE ADDITIONAL PUBLISHED DETAILS
    Public Sub SavePublishedInfo()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
		        INSERT INTO `published_details`
		        (
		            `no#`,
		            `published_id`,
		            `level`,
		            `academic_journal`,
		            `volume_no`,
		            `issue_no`,
		            `page_range`,
		            `date_published`,
		            `doi_url`
		        )
		        VALUES
		        (
		            @no,
		            @pub_id,
		            @pub_lvl,
		            @acd_jrnl,
		            @vol_no,
		            @issue_no,
		            @page_range,
		            @date_pub,
                    @doi_url
		        )
			    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@pub_id", control_no)
                cmd.Parameters.AddWithValue("@pub_lvl", pub_lvl)
                cmd.Parameters.AddWithValue("@acd_jrnl", acad_jrnl)
                cmd.Parameters.AddWithValue("@vol_no", vol_no)
                cmd.Parameters.AddWithValue("@issue_no", issue_no)
                cmd.Parameters.AddWithValue("@page_range", page_range)
                cmd.Parameters.AddWithValue("@date_pub", date_pub)
                cmd.Parameters.AddWithValue("@doi_url", doi_url)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to Save Published Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try

    End Sub

    Public Sub SavePresentedInfo()
        con.Close()
        Try
            con.Open()
            Dim query As String = "
		        INSERT INTO `presented_details`
		        (
                    `no#`,
		           `presented_id`,
		            `level`,
		            `research_conference_name`,
		            `date_presented`,
		            `place_presentation`
		        )
		        VALUES
		        (
		            @no,
		            @prsntd_id,
		            @prsntd_lvl,
		            @rcn,
		            @date_prsntd,
		            @place_prsntd
		        )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@prsntd_id", control_no)
                cmd.Parameters.AddWithValue("@prsntd_lvl", pre_lvl)
                cmd.Parameters.AddWithValue("@rcn", res_conf_name)
                cmd.Parameters.AddWithValue("@date_prsntd", date_prsntd)
                cmd.Parameters.AddWithValue("@place_prsntd", place_prsnttn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to Save Presented Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub


    'CLEARING ALL TEXTBOX FIELDS
    Private Sub ClearTextBox(ByVal parent_control As Control)
        For Each cntrl As Control In parent_control.Controls
            If TypeOf cntrl Is TextBox Then
                Dim txt_box As TextBox = CType(cntrl, TextBox)
                txt_box.Text = String.Empty
            ElseIf cntrl.HasChildren Then
                ClearTextBox(cntrl)
            End If
        Next
        PnlStatCmpltd.Visible = False
        CbxSftCpySbmttd.Checked = False
        CbxHrdCpySbmttd.Checked = False
        CbxDgiSbmttd.Checked = False
        CbxRgaEfSbmttd.Checked = False
        RdStatCmpltd.Checked = False
        RdStatOngng.Checked = False
        status = ""
        BtnCancelSelection.PerformClick()
    End Sub


    'PUBLISHED LEVEL RADIO BUTTON EVENT
    Private Sub RdPubLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelInsti.MouseClick
        publish_level = "Institutional"
    End Sub
    Private Sub RdPubLevelInter_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelInter.MouseClick
        publish_level = "International"
    End Sub
    Private Sub RdPubLevelLoc_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelLoc.MouseClick
        publish_level = "Local"
    End Sub
    Private Sub RdPubLevelNat_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelNat.MouseClick
        publish_level = "National"
    End Sub

    'PRESENTED LEVEL RADIO BUTTON EVENT
    Private Sub RdPreLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelInsti.MouseClick
        presented_level = "Institutional"
    End Sub
    Private Sub RdPreLevelInter_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelInter.MouseClick
        presented_level = "International"
    End Sub
    Private Sub RdPreLevelLoc_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelLoc.MouseClick
        presented_level = "Local"
    End Sub
    Private Sub RdPreLevelNat_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelNat.MouseClick
        presented_level = "National"
    End Sub


    'ADDING NEW TEXTBOX COMPONENT OR CONTROL INSIDE THE PANEL CONTAINER
    Dim auth_count As Integer = 0

    Public Sub AddNewAuthField()
        auth_count += 1

        'CREATING LABEL ELEMENTS AND CONFIGURE ITS PROPERTIES
        Dim lbl_auth As New Label With {
            .Name = "LabelAuth" & auth_count.ToString(),
            .Text = "Co-Author " & auth_count.ToString & " :",
            .AutoSize = True,
            .Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)
        }

        Dim lbl_auth_deg As New Label With {
            .Name = "LabelDeg" & auth_count.ToString(),
            .Text = "Degree Program :",
            .AutoSize = True,
            .Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)
        }

        Dim lbl_auth_role As New Label With {
            .Name = "LabelRole" & auth_count.ToString(),
            .Text = "Role :",
            .AutoSize = True,
            .Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)
        }

        'CREATING TEXTBOX ELEMENTSD, CONFIGURE ITS PROPERTIES
        Dim new_co_auth As New TextBox With {
        .Name = "CoAuthor" & auth_count,
        .Size = New System.Drawing.Size(379, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        Dim new_co_auth_deg As New TextBox With {
        .Name = "CoAuthorDeg" & auth_count,
        .Size = New System.Drawing.Size(424, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        Dim new_co_auth_role As New TextBox With {
        .Name = "CoAuthorRole" & auth_count,
        .Size = New System.Drawing.Size(160, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        'SET THE LOCATION OF ELEMENTS
        lbl_auth.Location = New Point(25, 10 + ((Panel1.Controls.Count * 30) / 6))
        new_co_auth.Location = New Point(114, 8 + ((Panel1.Controls.Count * 30) / 6))

        lbl_auth_deg.Location = New Point(498, 10 + ((Panel1.Controls.Count * 30) / 6))
        new_co_auth_deg.Location = New Point(619, 8 + ((Panel1.Controls.Count * 30) / 6))

        lbl_auth_role.Location = New Point(1048, 10 + ((Panel1.Controls.Count * 30) / 6))
        new_co_auth_role.Location = New Point(1099, 8 + ((Panel1.Controls.Count * 30) / 6))


        'ADDING ELEMENTS INTO THE CONTAINER
        Panel1.Controls.Add(lbl_auth)
        Panel1.Controls.Add(lbl_auth_deg)
        Panel1.Controls.Add(lbl_auth_role)

        Panel1.Controls.Add(new_co_auth)
        Panel1.Controls.Add(new_co_auth_deg)
        Panel1.Controls.Add(new_co_auth_role)

        Panel1.Height = Panel1.Height + 24
    End Sub


    'CODES FOR BUTTON ADD NEW CO-AUTHOR
    Dim cntr As String 'holder of number that system will generate textbox component for author
    Dim total_fields As String 'var holder of total field of existing co-auhtor displayed in label
    Private Sub BtnAddNewCoAuthor_Click_1(sender As Object, e As EventArgs) Handles BtnAddNewCoAuthor.Click

        cntr = TxtAddAuthX.Text.Trim
        total_fields = LblTotalFields.Text
        BtnAddNewCoAuthor.Enabled = False

        If cntr > 50 Or cntr < 1 Then
            MsgBox("Maximum of 50 every adding of fields")
        Else
            While cntr <> 0
                AddNewAuthField()
                cntr -= 1
                total_fields += 1
                LblTotalFields.Text = total_fields.ToString()
            End While
            BtnAddNewCoAuthor.Enabled = True
            TxtAddAuthX.Text = "1"
            Dim co_author_dynamic_name As String = "CoAuthor" & auth_count.ToString()
            Dim co_auth_field_name As TextBox = CType(Me.Controls.Find(co_author_dynamic_name, True).FirstOrDefault(), TextBox)
            co_auth_field_name.Focus()

        End If
    End Sub


    'REMOVING UNUSED CO-AUTHOR FIELDS AT THE BOTTOM OF PANEL CONTAINER
    Private Sub BtnRemoveField_Click(sender As Object, e As EventArgs) Handles BtnRemoveField.Click
        If auth_count > 1 Then
            Dim txtbox_author As String = "CoAuthor" & auth_count.ToString()
            Dim last_author_txtbox As TextBox = TryCast(Panel1.Controls(txtbox_author), TextBox)

            Dim lbl_auth As String = "LabelAuth" & auth_count.ToString()
            Dim last_author_lbl As Label = TryCast(Panel1.Controls(lbl_auth), Label)

            Dim txtbox_deg As String = "CoAuthorDeg" & auth_count.ToString()
            Dim last_deg_txtbox As TextBox = TryCast(Panel1.Controls(txtbox_deg), TextBox)

            Dim lbl_deg As String = "LabelDeg" & auth_count.ToString()
            Dim last_deg_lbl As Label = TryCast(Panel1.Controls(lbl_deg), Label)

            Dim txtbox_Role As String = "CoAuthorRole" & auth_count.ToString()
            Dim last_Role_txtbox As TextBox = TryCast(Panel1.Controls(txtbox_Role), TextBox)

            Dim lbl_role As String = "LabelRole" & auth_count.ToString()
            Dim last_role_lbl As Label = TryCast(Panel1.Controls(lbl_role), Label)

            If last_author_txtbox IsNot Nothing And last_deg_txtbox IsNot Nothing And last_Role_txtbox IsNot Nothing Then

                Panel1.Controls.Remove(last_author_txtbox)
                Panel1.Controls.Remove(last_author_lbl)

                Panel1.Controls.Remove(last_deg_txtbox)
                Panel1.Controls.Remove(last_deg_lbl)

                Panel1.Controls.Remove(last_Role_txtbox)
                Panel1.Controls.Remove(last_role_lbl)

                auth_count -= 1
                total_fields -= 1
                LblTotalFields.Text = total_fields.ToString()
                Panel1.Height = Panel1.Height - 24
            Else
                MessageBox.Show("Field(s) not found!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("There must be Minimum of 1 Co-Author Field", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub


    'ADDING AND SUBTRACTING TO FIELDS COUNT
    Private Sub BtnAddToFieldsCnt_Click(sender As Object, e As EventArgs) Handles BtnAddToFieldsCnt.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax + 1
    End Sub

    Private Sub BtnMinusToFieldsCnt_Click(sender As Object, e As EventArgs) Handles BtnMinusToFieldsCnt.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax - 1
    End Sub


    'RESTRICTING TEXTBOX FOR TOTAL COUNT FOR NEW CO-AUTHOR FIELDS | FIELDS ARE NOT ALLOWED TO BE BLANK, 0, GREATER THAN 50,
    Private Sub TxtAddAuthX_TextChanged(sender As Object, e As EventArgs) Handles TxtAddAuthX.TextChanged
        Dim field_count As String = TxtAddAuthX.Text.Trim
        If field_count <> "" And IsNumeric(field_count) Then
            If field_count < 1 Then
                TxtAddAuthX.Text = "1"
            ElseIf field_count > 50 Then
                TxtAddAuthX.Text = "50"
            End If
        End If
    End Sub
    Private Sub TxtAddAuthX_Leave(sender As Object, e As EventArgs) Handles TxtAddAuthX.Leave
        If TxtAddAuthX.Text = "" Then
            TxtAddAuthX.Text = "1"
        End If

    End Sub


    'ONGOING OR COMPLETED EVENT HANDLES SHOW / HIDE 4 CHECKBOXES PANEL
    Private Sub RdStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatCmpltd.MouseClick
        If RdStatCmpltd.Checked = True Then
            PnlStatCmpltd.Visible = True
            status = "Completed"
            DtCompletedDate.Enabled = True
        End If
    End Sub

    Private Sub RdStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatOngng.MouseClick
        PnlStatCmpltd.Visible = False
        CbxSftCpySbmttd.Checked = False
        CbxHrdCpySbmttd.Checked = False
        CbxDgiSbmttd.Checked = False
        CbxRgaEfSbmttd.Checked = False
        status = "Ongoing"
        DtCompletedDate.Enabled = False
    End Sub

    'VARIABLE TO HOLD SUBMITTED REQUIREMENTS WHEN COMPLETED STATUS WAS SELECTED
    Dim isSftCpySubmttd As String = "NO"
    Dim isHrdCpySubmttd As String = "NO"
    Dim isDgiSubmttd As String = "NO"
    Dim isRgaSubmttd As String = "NO"

    'SHOWING AND HIDING THEIR DATE PICKER ONCE CHECKED OR UNCHECKED
    Private Sub CbxSftCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxSftCpySbmttd.CheckedChanged
        If CbxSftCpySbmttd.Checked = True Then
            DtSftCpySbmttdDate.Visible = True
            isSftCpySubmttd = "YES"
        Else
            DtSftCpySbmttdDate.Visible = False
            isSftCpySubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxHrdCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxHrdCpySbmttd.CheckedChanged
        If CbxHrdCpySbmttd.Checked = True Then
            DtHrdCpySbmttdDate.Visible = True
            isHrdCpySubmttd = "YES"
        Else
            DtHrdCpySbmttdDate.Visible = False
            isHrdCpySubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxDgiSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxDgiSbmttd.CheckedChanged
        If CbxDgiSbmttd.Checked = True Then
            DtDgiSbmttdDate.Visible = True
            isDgiSubmttd = "YES"
        Else
            DtDgiSbmttdDate.Visible = False
            isDgiSubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxRgaEfSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxRgaEfSbmttd.CheckedChanged
        If CbxRgaEfSbmttd.Checked = True Then
            DtRgaSbmttdDate.Visible = True
            isRgaSubmttd = "YES"
        Else
            DtRgaSbmttdDate.Visible = False
            isRgaSubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub


    'SHOW THESIS CLEARANCE BUTTON WHEN 4 OF CHECKBOX CONDITION IS CHECKED
    Public Sub ShowPrintThesisClearance()
        If CbxSftCpySbmttd.Checked = True And CbxHrdCpySbmttd.Checked = True And CbxDgiSbmttd.Checked = True And CbxRgaEfSbmttd.Checked = True Then
            BtnThssClrnc.Enabled = True
            BtnThssClrnc.BackColor = Color.LimeGreen
        Else
            BtnThssClrnc.Enabled = False
            BtnThssClrnc.BackColor = Color.LightGray
        End If
    End Sub


    'RADIO BUTTON PUBLISHED AND PRESENTED , HIDING AND SHOWING OF UI
    Dim isPublished As String = "Unpublished"
    Dim isPresented As String = "Unpresented"
    'published radio button
    Private Sub RdBtnPub_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPub.MouseClick
        If RdBtnPub.Checked = True Then
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
            PnlPublished.Enabled = True
            PnlPublished.Height = 230
            isPublished = "Published"
            isPresented = "Unpresented"
            BtnCancelSelection.Visible = True
        Else
            isPublished = "Unpublished"
        End If
    End Sub
    'presented radio button
    Private Sub RdBtnPresented_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPresented.MouseClick
        If RdBtnPresented.Checked = True Then
            PnlPublished.Height = 0
            PnlPresented.Enabled = True
            PnlPresented.Height = 230
            isPresented = "Presented"
            isPublished = "Unpublished"
            BtnCancelSelection.Visible = True
        Else
            isPresented = "Unpresented"
        End If
    End Sub

    Private Sub BtnCancelSelection_Click(sender As Object, e As EventArgs) Handles BtnCancelSelection.Click

        isPublished = "Unpublished"
        PnlPublished.Enabled = False
        RdBtnPub.Checked = False

        isPresented = "Unpresented"
        PnlPresented.Enabled = False
        RdBtnPresented.Checked = False

        PnlPresented.Enabled = False
        PnlPresented.Height = 0
        PnlPublished.Height = 230

        BtnCancelSelection.Visible = False

        RdBtnPresented.Checked = False
        RdBtnPub.Checked = False

        RdPubLevelInsti.Checked = False
        RdPubLevelInter.Checked = False
        RdPubLevelLoc.Checked = False
        RdPubLevelNat.Checked = False

        RdPreLevelInsti.Checked = False
        RdPreLevelInter.Checked = False
        RdPreLevelLoc.Checked = False
        RdPreLevelNat.Checked = False
    End Sub

    Private Sub CbxSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxSem.SelectedIndexChanged
        If CbxSem.Text = "" Then
            CbxSem.Text = "Select Semester"
            CbxSem.ForeColor = Color.Gray
        ElseIf CbxSem.Text <> "Select Semester" Then
            CbxSem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub CbxSem_Leave(sender As Object, e As EventArgs) Handles CbxSem.Leave
        If CbxSem.Text = "" Then
            CbxSem.Text = "Select Semester"
            CbxSem.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub TxtSchoolYear_TextChanged(sender As Object, e As EventArgs) Handles TxtSchoolYear.TextChanged
        If TxtSchoolYear.Text <> "Enter School Year" Then
            TxtSchoolYear.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TxtSchoolYear_Leave(sender As Object, e As EventArgs) Handles TxtSchoolYear.Leave
        If TxtSchoolYear.Text = "" Then
            TxtSchoolYear.Text = "Enter School Year"
            TxtSchoolYear.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub TxtSchoolYear_GotFocus(sender As Object, e As EventArgs) Handles TxtSchoolYear.GotFocus

        TxtSchoolYear.Text = ""
        TxtSchoolYear.ForeColor = Color.Black

    End Sub



    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub AddWorks_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isForm1Closed Then
            e.Cancel = False
        Else
            Dim close_window As DialogResult = MessageBox.Show("Are you sure you want to close this form? Entered data will not be saved.", "Click Yes to close this form.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If close_window = DialogResult.Yes Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub BtnThssClrnc_Click(sender As Object, e As EventArgs) Handles BtnThssClrnc.Click
        print_clearance_id = Convert.ToInt64(TxtResearchID.Text)
        Dim save_to_print As DialogResult = MessageBox.Show("We will save informations you've entered before opening Print Clearance Preview", "Click 'Yes' to proceed saving.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If save_to_print = DialogResult.Yes Then
            print_clearance = True
            BtnSaveResearch.PerformClick()
        End If

    End Sub


    'MAKING SURE THAT QUANTITY AND LOCATION TEXTBOX HAS EXPECTED VALUE
    Private Sub TxtCopies_TextChanged(sender As Object, e As EventArgs) Handles TxtCopies.TextChanged
        If TxtCopies.Text <> "" And Not IsNumeric(TxtCopies.Text) Then
            TxtCopies.Text = "1"
        End If
    End Sub

    Private Sub TxtCopies_Leave(sender As Object, e As EventArgs) Handles TxtCopies.Leave
        If TxtCopies.Text = "" Then
            TxtCopies.Text = "1"
        End If
    End Sub

    Private Sub TxtLoc_Leave(sender As Object, e As EventArgs) Handles TxtLoc.Leave
        If TxtLoc.Text = "" Then
            TxtLoc.Text = "Not set"
        End If
    End Sub

    'not allowing user to select date ahead of the current date
    Private Sub DtCompletedDate_ValueChanged(sender As Object, e As EventArgs) Handles DtCompletedDate.ValueChanged
        If DtCompletedDate.Value > DateTime.Now Then
            DtCompletedDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtSftCpySbmttdDate_ValueChanged(sender As Object, e As EventArgs) Handles DtSftCpySbmttdDate.ValueChanged
        If DtSftCpySbmttdDate.Value > DateTime.Now Then
            DtSftCpySbmttdDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtHrdCpySbmttdDate_ValueChanged(sender As Object, e As EventArgs) Handles DtHrdCpySbmttdDate.ValueChanged
        If DtHrdCpySbmttdDate.Value > DateTime.Now Then
            DtHrdCpySbmttdDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtDgiSbmttdDate_ValueChanged(sender As Object, e As EventArgs) Handles DtDgiSbmttdDate.ValueChanged
        If DtDgiSbmttdDate.Value > DateTime.Now Then
            DtDgiSbmttdDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtRgaSbmttdDate_ValueChanged(sender As Object, e As EventArgs) Handles DtRgaSbmttdDate.ValueChanged
        If DtRgaSbmttdDate.Value > DateTime.Now Then
            DtRgaSbmttdDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtPrsntdDate_ValueChanged(sender As Object, e As EventArgs) Handles DtPrsntdDate.ValueChanged
        If DtPrsntdDate.Value > DateTime.Now Then
            DtPrsntdDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub DtPubDate_ValueChanged(sender As Object, e As EventArgs) Handles DtPubDate.ValueChanged
        If DtPubDate.Value > DateTime.Now Then
            DtPubDate.Value = DateTime.Now
            MessageBox.Show("You selected ahead of present date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
End Class



