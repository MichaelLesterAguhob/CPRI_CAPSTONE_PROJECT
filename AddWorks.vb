
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class AddWorks

    '|||||||||||||||||||||||||||||||||| BEGINNING CODES FOR MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||

    'GENERATING UNIQUE CONTROL NUMBER THAT WILL BE ASSIGNED TO RESEARCH RECORDS and CHECKING ITS UNIQUENESS
    Dim control_number As Integer
    Dim initial_cntrl_nmbr As Long
    ReadOnly date_time As DateTime = DateTime.Now
    ReadOnly current_year As Integer = date_time.Year

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
                cmd.Parameters.AddWithValue("@id", initial_cntrl_nmbr)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateControlNumber()
                Else
                    control_number = initial_cntrl_nmbr
                    TxtResearchID.Text = current_year.ToString & control_number.ToString
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to check the uniqueness of generated number")
        Finally
            con.Close()
        End Try
    End Sub
    '===============================END=====================================

    'FUNCTIONS WHEN ADDWORKS FORM IS LOAD
    Private Sub AddWorks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'trying to connect to database to determine if connection is ready
        Try
            ConOpen()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
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
    '===============================END=====================================


    'CODES TO LOAD RESEARCH WORK
    Private ReadOnly rrm As ResearchRepoManager
    Public Sub New(ByVal rrm As ResearchRepoManager)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.rrm = rrm
    End Sub


    'CODES FOR SAVING/UPLOADING RESEARCH
    Dim publish_level As String = ""
    Dim presented_level As String = ""

    'variables for upper fields
    Dim control_no As Integer = 0
    Dim research_agenda As String = ""
    Dim research_title As String = ""
    Dim author_name As String = ""
    Dim author_deg As String = ""
    Dim author_role As String = ""
    Dim completed_date As String = ""
    Dim isDynamicFieldsNotBlanks As Boolean = True

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

    Private Sub BtnSaveResearch_Click(sender As Object, e As EventArgs) Handles BtnSaveResearch.Click
        BtnSaveResearch.Enabled = False

        'variables for upper fields
        control_no = Convert.ToInt64(TxtResearchID.Text)
        research_agenda = TxtRsrchAgenda.Text.Trim
        research_title = TxtRsrchTitle.Text.Trim
        author_name = TxtAuthorName.Text.Trim
        author_deg = TxtAthrDegprog.Text.Trim
        author_role = TxtAthrRole.Text.Trim
        completed_date = DtDateCompltd.Value.Date.ToString("MM-dd-yyyy")

        con.Close()

        If control_no <> 0 And research_agenda <> "" And research_title <> "" And author_name <> "" And author_deg <> "" And author_role <> "" And whole_file_path <> "" And abstract_file_path <> "" Then

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

                If CoAuth_name_field.Text = "" Or CoAuth_deg_field.Text = "" Or CoAuth_role_field.Text = "" Then
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
                If isPublished = "YES" Then
                    If publish_level <> "" And TxtPubAcadJournal.Text <> "" And TxtPubVolNum.Text <> "" And TxtPubIssueNo.Text <> "" And TxtPubPageRange.Text <> "" And TxtPubDoiUrl.Text <> "" Then

                        pub_lvl = publish_level
                        acad_jrnl = TxtPubAcadJournal.Text.ToString
                        vol_no = Convert.ToInt64(TxtPubVolNum.Text)
                        issue_no = Convert.ToInt64(TxtPubIssueNo.Text)
                        page_range = TxtPubPageRange.Text.ToString
                        date_pub = TxtPubDate.Value.Date.ToString("MM-dd-yyyy")
                        doi_url = TxtPubDoiUrl.Text.ToString

                        SaveUpperInputs()
                        SavePublishedInfo()
                    Else
                        MessageBox.Show("Fill in the blank(s)", "No Input | Check Additional Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    'check if additonal info-presented is checked
                ElseIf isPresented = "YES" Then

                    If TxtPreResConfName.Text <> "" And TxtPrePlace.Text <> "" And presented_level <> "" Then

                        pre_lvl = presented_level
                        res_conf_name = TxtPreResConfName.Text.ToString
                        date_prsntd = DtPrsntdDate.Value.Date.ToString("MM-dd-yyyy")
                        place_prsnttn = TxtPrePlace.Text.ToString

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
                            `date_completed`, 
                            `status_ongoing_completed`, 
                            `published`, 
                            `presented` 
                        ) 
                        VALUES 
                        (
                            @no, 
                            @sw_id, 
                            @title, 
                            @research_agenda, 
                            @date_completed, 
                            @status_ongoing_completed, 
                            @published,  
                            @presented
                        )"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", Nothing)
                cmd.Parameters.AddWithValue("@sw_id", control_no)
                cmd.Parameters.AddWithValue("@title", research_title)
                cmd.Parameters.AddWithValue("@research_agenda", research_agenda)
                cmd.Parameters.AddWithValue("@date_completed", completed_date)
                cmd.Parameters.AddWithValue("@status_ongoing_completed", status)
                cmd.Parameters.AddWithValue("@published", isPublished)
                cmd.Parameters.AddWithValue("@presented", isPresented)
                cmd.ExecuteNonQuery()
            End Using

            SaveAuthor()
            SaveCoAuthors()
            SaveWholeFiles()
            SaveAbstractFiles()
            SaveStatCmpltdChckdbx()
            MessageBox.Show("Successfully Saved", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            'MessageBox.Show(ex.Message, "Failed on Saving Research")

            con.Close()
        Finally

            con.Close()
            ClearTextBox(Me)
            GenerateControlNumber()
            TxtAddAuthX.Text = "1"
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
            MessageBox.Show(ex.Message, "Failed on Saving Author")
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub
    '==============================END=====================================

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
                    cmd.Parameters.AddWithValue("@c_a_n", CoAuth_name_field.Text.ToString)
                    cmd.Parameters.AddWithValue("@d_p", CoAuth_deg_field.Text.ToString)
                    cmd.Parameters.AddWithValue("@role", CoAuth_role_field.Text.ToString)
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
    '==============================END=====================================


    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF FILES
    'whole file
    Dim whole_file_path As String
    Dim whole_file_data As Byte()
    Dim whole_file_extension As String
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
    '==============================END=====================================

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
                cmd.Parameters.AddWithValue("@filename", Path.GetFileName(abstract_file_path))
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
    '==============================END=====================================


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
    '==============================END=====================================


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
		            @cmpltd_dt,
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
                cmd.Parameters.AddWithValue("@cmpltd_dt", completed_date)
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
    '==============================END=====================================

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
    '==============================END=====================================

    'PUBLISHED LEVEL RADIO BUTTON EVENT
    Private Sub RdPubLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelInsti.MouseClick
        publish_level = "Intitutional"
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
        presented_level = "Intitutional"
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
    '==============================END=====================================

    '|||||||||||||||||||||||||||||||||| END OF MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||


    ' |||||||||||||||||||||||||||||||| BEGINNING CODES BELOW ARE FOR UI RESPONSES OR FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||

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
    '==============================END=====================================

    'CODES FOR BUTTON ADD NEW CO-AUTHOR
    Dim cntr As String 'holder of number that system will generate textbox component for author
    Dim total_fields As String 'var holder of total field of existing co-auhtor displayed in label
    Private Sub BtnAddNewCoAuthor_Click_1(sender As Object, e As EventArgs) Handles BtnAddNewCoAuthor.Click

        cntr = TxtAddAuthX.Text
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
    '==============================END=====================================

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
    '==============================END=====================================

    'ADDING AND SUBTRACTING TO FIELDS COUNT
    Private Sub BtnAddToFieldsCnt_Click(sender As Object, e As EventArgs) Handles BtnAddToFieldsCnt.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax + 1
    End Sub

    Private Sub BtnMinusToFieldsCnt_Click(sender As Object, e As EventArgs) Handles BtnMinusToFieldsCnt.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax - 1
    End Sub
    '==============================END=====================================

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
    '==============================END=====================================

    'ONGOING OR COMPLETED EVENT HANDLES SHOW / HIDE 4 CHECKBOXES PANEL
    Dim status As String = ""
    Private Sub RdStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatCmpltd.MouseClick
        If RdStatCmpltd.Checked = True Then
            PnlStatCmpltd.Visible = True
            status = "Completed"
        End If
    End Sub

    Private Sub RdStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatOngng.MouseClick
        PnlStatCmpltd.Visible = False
        CbxSftCpySbmttd.Checked = False
        CbxHrdCpySbmttd.Checked = False
        CbxDgiSbmttd.Checked = False
        CbxRgaEfSbmttd.Checked = False
        status = "Ongoing"
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
    '==============================END=====================================

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
    '==============================END=====================================

    'RADIO BUTTON PUBLISHED AND PRESENTED , HIDING AND SHOWING OF UI
    Dim isPublished As String = "NO"
    Dim isPresented As String = "NO"
    'published radio button
    Private Sub RdBtnPub_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPub.MouseClick
        If RdBtnPub.Checked = True Then
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
            PnlPublished.Enabled = True
            PnlPublished.Height = 230
            isPublished = "YES"
            isPresented = "NO"
            BtnCancelSelection.Visible = True
        Else
            isPublished = "NO"
        End If
    End Sub
    'presented radio button
    Private Sub RdBtnPresented_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPresented.MouseClick
        If RdBtnPresented.Checked = True Then
            PnlPublished.Height = 0
            PnlPresented.Enabled = True
            PnlPresented.Height = 230
            isPresented = "YES"
            isPublished = "NO"
            BtnCancelSelection.Visible = True
        Else
            isPresented = "NO"
        End If
    End Sub

    Private Sub BtnCancelSelection_Click(sender As Object, e As EventArgs) Handles BtnCancelSelection.Click

        isPublished = "NO"
        PnlPublished.Enabled = False
        RdBtnPub.Checked = False

        isPresented = "NO"
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


    '==============================END=====================================
    '|||||||||||||||||||||||||||||||||| END OF UI RESPONSE OR FUNCTIONALITES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||





End Class



