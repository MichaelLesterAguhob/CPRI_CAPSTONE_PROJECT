Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class EditWorkRecord

    ' Variables declaration
    Dim co_authors_no As Integer
    Dim co_author_name As String = ""
    Dim co_author_deg As String = ""
    Dim co_author_role As String = ""
    Dim isDynamicFieldsNotBlanks As Boolean = True

    Dim whole_file_path As String
    Dim whole_file_data As Byte()
    Dim whole_file_extension As String

    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()
    Dim abstract_file_extension As String
    Dim auth_count As Integer = 0
    Dim is_files_changed As Boolean = False
    Dim is_abs_files_changed As Boolean = False

    Dim record_status As String = "none"
    Dim new_status As String = "none"

    Dim isPublished As String = ""
    Dim isPresented As String = ""

    Dim record_addtnl_info As String = "none"
    Dim new_addtnl_info As String = "none"

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


    Dim cntr As String 'holder of number that system will generate textbox component for author
    Dim total_fields As Integer = 0 'var holder of total field of existing co-auhtor displayed in label

    ReadOnly edit_id As Integer
    'Public Sub New(sw_edit_id As Integer)
    '   InitializeComponent()
    '  edit_id = rrm.sw_edit_id
    'End Sub

    'CODES TO LOAD RESEARCH WORK
    Private ReadOnly rrm As ResearchRepoManager
    Public Sub New(ByVal rrm As ResearchRepoManager, ByVal sw_edit_id As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.rrm = rrm
        Me.edit_id = sw_edit_id
    End Sub

    Private Sub EditWorkRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadToEditRecord()
    End Sub

    Private Sub LoadToEditRecord()
        con.Close()

        Try
            ConOpen()
            Dim query As String = "
                            SELECT 
                                scholarly_works.*,
                                authors.authors_name,
                                authors.degree_program,
                                authors.role,
                                sw_whole_file.file_name AS whl_file_name,
                                sw_whole_file.file_data AS whl_file_data,
                                sw_abstract.file_name AS abs_file_name,
                                sw_abstract.file_data AS abs_file_data
                            FROM scholarly_works 
                            INNER JOIN authors
                                ON authors.authors_id = @edit_id
                            INNER JOIN sw_abstract
                                ON abstract_id = @edit_id
                            INNER JOIN sw_whole_file
                                ON whole_file_id = @edit_id
                            WHERE sw_id=@edit_id"

            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@edit_id", edit_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Dim status As String = ""
                If reader.HasRows Then
                    If reader.Read() Then
                        TxtEditResearchID.Text = reader("sw_id").ToString()
                        TxtEditRsrchTitle.Text = reader("title").ToString()
                        TxtEditRsrchAgenda.Text = reader("research_agenda").ToString()
                        CbxSem.Text = reader("semester").ToString()
                        TxtSchoolYear.Text = reader("school_year").ToString()

                        TxtEditAuthName.Text = reader("authors_name").ToString()
                        TxtEditAuthDeg.Text = reader("degree_program").ToString()
                        TxtEditAuthRole.Text = reader("role").ToString()

                        whole_file_data = reader("whl_file_data")
                        abstract_file_data = reader("abs_file_data")
                        TxtUplddWhlFileName.Text = reader("whl_file_name").ToString()
                        TxtUplddAbsFileName.Text = reader("abs_file_name").ToString()

                        status = reader("status_ongoing_completed").ToString()
                        record_status = reader("status_ongoing_completed").ToString()

                        isPublished = reader("published")
                        isPresented = reader("presented")
                        reader.Close()
                    End If
                    GetCoAuthors()

                Else
                    MessageBox.Show("Failed to load research details", "No Data Found")
                End If

                ' if status is completed 
                If status = "Completed" Then
                    GetCompletedCheckedDetails()
                    RdEdtStatCmpltd.Checked = True
                    PnlStatCmpltdEdtMode.Visible = True
                ElseIf status = "Ongoing" Then
                    'set the ongoing radio button checked
                    RdEdtStatOngng.Checked = True
                End If

                'checking additional information details
                If isPublished = "Published" Then
                    RdBtnPubEdtMode.Checked = True

                    PnlPresented.Enabled = False
                    PnlPresented.Height = 0

                    PnlPublished.Enabled = True
                    PnlPublished.Height = 230

                    isPublished = "Published"
                    isPresented = "NO"
                    BtnEdtCnclSlctn.Visible = True
                    record_addtnl_info = "published"
                    Addtnl_Info("published")

                ElseIf isPresented = "Presented" Then
                    RdBtnPresentedEdtMode.Checked = True
                    PnlPublished.Height = 0

                    PnlPresented.Enabled = True
                    PnlPresented.Height = 230

                    isPresented = "Presented"
                    isPublished = "NO"
                    BtnEdtCnclSlctn.Visible = True
                    record_addtnl_info = "presented"
                    Addtnl_Info("presented")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred in loading record", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine(ex.Message)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub


    Private Sub GetCoAuthors()
        'getting co authors record and loaded it to dynamic textbix component
        Dim query2 As String = "SELECT * FROM co_authors WHERE co_authors_id= @edit_id"
        Using cmd2 As New MySqlCommand(query2, con)
            cmd2.Parameters.AddWithValue("@edit_id", edit_id)

            Dim reader2 As MySqlDataReader = cmd2.ExecuteReader()
            While reader2.Read()
                co_authors_no = reader2("no#").ToString
                co_author_name = reader2("co_authors_name").ToString
                co_author_deg = reader2("degree_program").ToString
                co_author_role = reader2("role").ToString
                AddOrLoadCoAuthField()
            End While
            If total_fields <= 0 Then
                AddOrLoadCoAuthField()
            Else
                LblTotalCoAuthFlds.Text = total_fields.ToString()
            End If

            co_author_name = ""
            co_author_deg = ""
            co_author_role = ""
            reader2.Close()
        End Using
    End Sub

    'ADDING NEW TEXTBOX COMPONENT OR CONTROL INSIDE THE PANEL CONTAINER
    Public Sub AddOrLoadCoAuthField()
        auth_count += 1

        'CREATING LABEL ELEMENTS AND CONFIGURE ITS PROPERTIES
        Dim lbl_auth_no_id As New Label With {
        .Name = "LblCoAuthNoId" & auth_count.ToString(),
        .Text = co_authors_no.ToString,
        .AutoSize = True,
        .Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular),
        .Visible = False
        }

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

        'CREATING TEXTBOX ELEMENTS, CONFIGURED ITS PROPERTIES
        Dim new_co_auth As New TextBox With {
        .Name = "CoAuthor" & auth_count,
        .Text = co_author_name,
        .Size = New System.Drawing.Size(379, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        Dim new_co_auth_deg As New TextBox With {
        .Name = "CoAuthorDeg" & auth_count,
        .Text = co_author_deg,
        .Size = New System.Drawing.Size(424, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        Dim new_co_auth_role As New TextBox With {
        .Name = "CoAuthorRole" & auth_count,
        .Text = co_author_role,
        .Size = New System.Drawing.Size(160, 22),
        .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular),
        .BackColor = Color.WhiteSmoke
        }

        'SET THE LOCATION OF ELEMENTS
        lbl_auth_no_id.Location = New Point(25, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        lbl_auth.Location = New Point(25, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth.Location = New Point(114, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))

        lbl_auth_deg.Location = New Point(498, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth_deg.Location = New Point(619, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))

        lbl_auth_role.Location = New Point(1048, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth_role.Location = New Point(1099, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))


        'ADDING ELEMENTS INTO THE CONTAINER
        PnlEditCoAuthFlds.Controls.Add(lbl_auth_no_id)
        PnlEditCoAuthFlds.Controls.Add(lbl_auth)
        PnlEditCoAuthFlds.Controls.Add(lbl_auth_deg)
        PnlEditCoAuthFlds.Controls.Add(lbl_auth_role)

        PnlEditCoAuthFlds.Controls.Add(new_co_auth)
        PnlEditCoAuthFlds.Controls.Add(new_co_auth_deg)
        PnlEditCoAuthFlds.Controls.Add(new_co_auth_role)

        PnlEditCoAuthFlds.Height = PnlEditCoAuthFlds.Height + 24
        total_fields += 1
    End Sub

    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF FILE
    'whole file
    Private Sub BtnBrowseWholeFile_Click(sender As Object, e As EventArgs) Handles BtnBrwsWhl.Click
        Dim openFileDialog As New OpenFileDialog With {
    .Filter = "PDF Files (*.pdf)|*.pdf",
    .InitialDirectory = "C:\"
    }
        Dim open_file As DialogResult = openFileDialog.ShowDialog()

        If open_file = DialogResult.OK Then
            whole_file_path = openFileDialog.FileName
            whole_file_data = File.ReadAllBytes(whole_file_path)
            whole_file_extension = Path.GetExtension(whole_file_path)
            TxtUplddWhlFileName.Text = whole_file_path.ToString
            is_files_changed = True
        ElseIf open_file = DialogResult.Cancel Then
            is_files_changed = False
        End If
    End Sub

    'abstract file
    Private Sub BtnBrowseAbstractFile_Click(sender As Object, e As EventArgs) Handles BtnBrwsAbstrct.Click
        Dim openFileDialog As New OpenFileDialog With {
    .Filter = "PDF Files (*.pdf)|*.pdf",
    .InitialDirectory = "C:\"
    }
        Dim open_file As DialogResult = openFileDialog.ShowDialog()

        If open_file = DialogResult.OK Then
            abstract_file_path = openFileDialog.FileName
            abstract_file_data = File.ReadAllBytes(abstract_file_path)
            abstract_file_extension = Path.GetExtension(abstract_file_path)
            TxtUplddAbsFileName.Text = abstract_file_path.ToString
            is_abs_files_changed = True
        ElseIf open_file = DialogResult.Cancel Then
            is_abs_files_changed = False
        End If
    End Sub

    'INSERTING PDF ABSTRACT FILES IN THA DATABASE
    Private Sub UpdateFiles()
        If is_abs_files_changed Then
            con.Close()
            Try
                con.Open()
                Dim query As String = "
            UPDATE `sw_abstract` 
            SET 
                `file_name`= @filename, 
                `file_data`= @filedata, 
                `file_type` = @filetype

            WHERE `abstract_id` = @id
            "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", edit_id)
                    cmd.Parameters.AddWithValue("@filename", Path.GetFileName(abstract_file_path))
                    cmd.Parameters.AddWithValue("@filedata", abstract_file_data)
                    cmd.Parameters.AddWithValue("@filetype", abstract_file_extension)
                    cmd.ExecuteNonQuery()
                End Using
                cmd.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed on Saving Abstract File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If
        If is_files_changed Then
            con.Close()
            Try
                con.Open()
                Dim query As String = "
            UPDATE `sw_whole_file` 
            SET 
                `file_name`= @filename, 
                `file_data`= @filedata, 
                `file_type` = @filetype

            WHERE `whole_file_id` = @id
            "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", edit_id)
                    cmd.Parameters.AddWithValue("@filename", Path.GetFileName(whole_file_path))
                    cmd.Parameters.AddWithValue("@filedata", whole_file_data)
                    cmd.Parameters.AddWithValue("@filetype", whole_file_extension)
                    cmd.ExecuteNonQuery()
                End Using
                cmd.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed on Saving Whole File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If
    End Sub

    'CODES TO GET THE DETAILS OF COMPLETED STATUS
    Private Sub GetCompletedCheckedDetails()
        Dim clrnc_chcklst As String = "SELECT * FROM status_completed_info WHERE stat_completed_id=@edit_id"
        Using clrnc_cmd As New MySqlCommand(clrnc_chcklst, con)
            clrnc_cmd.Parameters.AddWithValue("@edit_id", edit_id)
            Dim clrnc_reader As MySqlDataReader = clrnc_cmd.ExecuteReader()

            If clrnc_reader.HasRows Then
                If clrnc_reader.Read() Then

                    'get the date of submitted soft copy
                    If clrnc_reader("soft_copy_sbmttd_date") <> "NO" Then
                        Dim dateString As String = clrnc_reader("soft_copy_sbmttd_date")
                        Dim parsedDate As DateTime
                        If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                            CbxSftCpySbmttdEdtMode.Checked = True
                            DtSftCpySbmttdDateEdtMode.Value = parsedDate
                        Else
                            MessageBox.Show("soft_copy_sbmttd_date Invalid date format")
                        End If
                    End If

                    'get the date of submitted hard copy
                    If clrnc_reader("hard_copy_sbmttd_date") <> "NO" Then
                        Dim dateString As String = clrnc_reader("hard_copy_sbmttd_date")
                        Dim parsedDate As DateTime
                        If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                            CbxHrdCpySbmttdEdtMode.Checked = True
                            DtHrdCpySbmttdDateEdtMode.Value = parsedDate
                        Else
                            MessageBox.Show("hard_copy_sbmttd_date Invalid date format")
                        End If
                    End If

                    ' get the date of submitted dgi
                    If clrnc_reader("dgi_sbmttd_date") <> "NO" Then
                        Dim dateString As String = clrnc_reader("dgi_sbmttd_date")
                        Dim parsedDate As DateTime
                        If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                            CbxDgiSbmttdEdtMode.Checked = True
                            DtDgiSbmttdDateEdtMode.Value = parsedDate
                        Else
                            MessageBox.Show("Dgi_copy_sbmttd_date Invalid date format")
                        End If
                    End If

                    'get the date of submitted rga
                    If clrnc_reader("rga_ef_sbmttd_date") <> "NO" Then
                        Dim dateString As String = clrnc_reader("rga_ef_sbmttd_date")
                        Dim parsedDate As DateTime
                        If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                            CbxRgaEfSbmttdEdtMode.Checked = True
                            DtRgaSbmttdDateEdtMode.Value = parsedDate
                        Else
                            MessageBox.Show("Rga_copy_sbmttd_date Invalid date format")
                        End If
                    End If

                End If
                clrnc_reader.Close()
            Else
                clrnc_reader.Close()
            End If

        End Using
    End Sub


    Private Sub Addtnl_Info(action)
        If action = "published" Then
            Dim query As String = "SELECT * FROM published_details WHERE published_id=@edit_id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@edit_id", edit_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        'check the level
                        If reader("level").ToString() = "Institutional" Then
                            RdPubLevelInstiEdtMode.Checked = True
                            publish_level = "Institutional"
                        ElseIf reader("level").ToString() = "Local" Then
                            RdPubLevelLocEdtMode.Checked = True
                            publish_level = "Local"
                        ElseIf reader("level").ToString() = "National" Then
                            RdPubLevelNatEdtMode.Checked = True
                            publish_level = "National"
                        ElseIf reader("level").ToString() = "International" Then
                            RdPubLevelInterEdtMode.Checked = True
                            publish_level = "International"
                        End If

                        TxtPubAcadJournalEdtMode.Text = reader("academic_journal")
                        TxtPubVolNumEdtMode.Text = reader("volume_no")
                        TxtPubIssueNoEdtMode.Text = reader("issue_no")
                        TxtPubPageRangeEdtMode.Text = reader("page_range")
                        TxtPubDoiUrlEdtMode.Text = reader("doi_url")

                        Dim published_date As String = reader("date_published")
                        Dim parsed_pub_date As DateTime
                        If DateTime.TryParseExact(published_date, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsed_pub_date) Then
                            DtPubDateEdtMode.Value = parsed_pub_date
                        End If
                    End If
                End If
                reader.Close()
            End Using
        ElseIf action = "presented" Then
            Dim query As String = "SELECT * FROM presented_details WHERE presented_id=@edit_id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@edit_id", edit_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    If reader.Read() Then
                        'check the level
                        If reader("level").ToString() = "Institutional" Then
                            RdPreLevelInstiEdtMode.Checked = True
                            presented_level = "Institutional"
                        ElseIf reader("level").ToString() = "Local" Then
                            RdPreLevelLocEdtMode.Checked = True
                            presented_level = "Local"
                        ElseIf reader("level").ToString() = "National" Then
                            RdPreLevelNatEdtMode.Checked = True
                            presented_level = "National"
                        ElseIf reader("level").ToString() = "International" Then
                            RdPreLevelInterEdtMode.Checked = True
                            presented_level = "International"
                        End If

                        TxtPreResConfNameEdtMode.Text = reader("research_conference_name")
                        TxtPrePlaceEdtMode.Text = reader("place_presentation")

                        Dim presented_date As String = reader("date_presented")
                        Dim parsed_pre_date As DateTime
                        If DateTime.TryParseExact(presented_date, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsed_pre_date) Then
                            DtPrsntdDateEdtMode.Value = parsed_pre_date
                        End If
                    End If
                End If
                reader.Close()
            End Using
        End If
    End Sub




    'ONGOING OR COMPLETED EVENT HANDLES SHOW / HIDE 4 CHECKBOXES PANEL
    Private Sub RdEdtStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtStatCmpltd.MouseClick
        If record_status = "Ongoing" Then
            PnlStatCmpltdEdtMode.Visible = True
            new_status = "Completed"
        ElseIf record_status = "Completed" Then
            PnlStatCmpltdEdtMode.Visible = True
            new_status = "Completed"
        Else
            new_status = "none"
        End If

    End Sub

    Private Sub RdEdtStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtStatOngng.MouseClick

        If record_status = "Completed" Then
            PnlStatCmpltdEdtMode.Visible = False
            CbxSftCpySbmttdEdtMode.Checked = False
            CbxHrdCpySbmttdEdtMode.Checked = False
            CbxDgiSbmttdEdtMode.Checked = False
            CbxRgaEfSbmttdEdtMode.Checked = False
            new_status = "Ongoing"
        ElseIf record_status = "Ongoing" Then
            PnlStatCmpltdEdtMode.Visible = False
            CbxSftCpySbmttdEdtMode.Checked = False
            CbxHrdCpySbmttdEdtMode.Checked = False
            CbxDgiSbmttdEdtMode.Checked = False
            CbxRgaEfSbmttdEdtMode.Checked = False
            new_status = "Ongoing"
        Else
            new_status = "none"
        End If

    End Sub

    'UPDATING COMPLETED CHECKBOXES
    Public Sub SaveUpdatedStatCmpltdChckdbx()
        con.Close()
        If record_status = "Completed" And new_status = "Ongoing" Then
            'delete completed statuss
            Try
                con.Open()
                Dim query As String = "DELETE FROM `status_completed_info` WHERE `stat_completed_id` = @sci"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@sci", edit_id)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to Delete Updated Completed Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try

        ElseIf record_status = "Ongoing" And new_status = "Completed" Then
            'insert completed status checked list
            Dim sftCpyDate As String
            Dim hrdCopyDate As String
            Dim dgiDate As String
            Dim rgaDate As String

            If isSftCpySubmttd = "YES" Then
                sftCpyDate = DtSftCpySbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                sftCpyDate = "NO"
            End If
            If isHrdCpySubmttd = "YES" Then
                hrdCopyDate = DtHrdCpySbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                hrdCopyDate = "NO"
            End If
            If isDgiSubmttd = "YES" Then
                dgiDate = DtDgiSbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                dgiDate = "NO"
            End If
            If isRgaSubmttd = "YES" Then
                rgaDate = DtRgaSbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
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
                    cmd.Parameters.AddWithValue("@sci", edit_id)
                    cmd.Parameters.AddWithValue("@scsd", sftCpyDate)
                    cmd.Parameters.AddWithValue("@hcsd", hrdCopyDate)
                    cmd.Parameters.AddWithValue("@dgi", dgiDate)
                    cmd.Parameters.AddWithValue("@rga", rgaDate)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to insert completed details", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try

        ElseIf record_status = "Completed" And new_status = "none" Then
            Dim sftCpyDate As String
            Dim hrdCopyDate As String
            Dim dgiDate As String
            Dim rgaDate As String

            If isSftCpySubmttd = "YES" Then
                sftCpyDate = DtSftCpySbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                sftCpyDate = "NO"
            End If
            If isHrdCpySubmttd = "YES" Then
                hrdCopyDate = DtHrdCpySbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                hrdCopyDate = "NO"
            End If
            If isDgiSubmttd = "YES" Then
                dgiDate = DtDgiSbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                dgiDate = "NO"
            End If
            If isRgaSubmttd = "YES" Then
                rgaDate = DtRgaSbmttdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
            Else
                rgaDate = "NO"
            End If

            Try
                con.Open()
                Dim query As String = "
                    UPDATE `status_completed_info`
                        SET 
                            `soft_copy_sbmttd_date` = @scsd, 
                            `hard_copy_sbmttd_date` = @hcsd, 
                            `dgi_sbmttd_date` = @dgi,
                            `rga_ef_sbmttd_date` = @rga
                        WHERE   
                            `stat_completed_id` = @sci
                 "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@sci", edit_id)
                    cmd.Parameters.AddWithValue("@scsd", sftCpyDate)
                    cmd.Parameters.AddWithValue("@hcsd", hrdCopyDate)
                    cmd.Parameters.AddWithValue("@dgi", dgiDate)
                    cmd.Parameters.AddWithValue("@rga", rgaDate)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to update Completed details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try

        End If
    End Sub

    'UPDATING MODIFIED DETAILS
    Private Sub Btn_Update_Click(sender As Object, e As EventArgs) Handles Btn_Update.Click

        Dim cntrl_no As Integer = Convert.ToInt64(TxtEditResearchID.Text)
        Dim agenda As String = TxtEditRsrchAgenda.Text.Trim
        Dim title As String = TxtEditRsrchTitle.Text.Trim
        Dim sem As String = CbxSem.Text.Trim
        Dim schl_yr As String = TxtSchoolYear.Text.Trim

        Dim author_name As String = TxtEditAuthName.Text.Trim
        Dim auth_deg_prog As String = TxtEditAuthDeg.Text.Trim
        Dim auth_role As String = TxtEditAuthRole.Text.Trim

        'checking required fields if not blanks
        If cntrl_no <> 0 And agenda <> "" And title <> "" And author_name <> "" And auth_deg_prog <> "" And auth_role <> "" And sem <> "Select Semester" And schl_yr <> "Enter School Year" And whole_file_data IsNot Nothing AndAlso whole_file_data.Length > 0 And abstract_file_data IsNot Nothing AndAlso abstract_file_data.Length > 0 Then

            'Checking the dynamic textbox components if not blank
            Dim auth_no As Integer = 0
            Dim co_author_fields_to_save As Integer = Convert.ToInt64(LblTotalCoAuthFlds.Text)

            While auth_no <> co_author_fields_to_save
                auth_no += 1 ' responsible for changing dynamic name
                Dim CoAuth_dynmc_id As String = "LblCoAuthNoId" & auth_no.ToString()
                Dim CoAuth_Id As Label = CType(Me.Controls.Find(CoAuth_dynmc_id, True).FirstOrDefault(), Label)

                Dim CoAuth_dynmc_name As String = "CoAuthor" & auth_no.ToString()
                Dim CoAuth_name_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_name, True).FirstOrDefault(), TextBox)

                Dim CoAuth_dynmc_deg As String = "CoAuthorDeg" & auth_no.ToString()
                Dim CoAuth_deg_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_deg, True).FirstOrDefault(), TextBox)

                Dim CoAuth_dynmc_role As String = "CoAuthorRole" & auth_no.ToString()
                Dim CoAuth_role_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_role, True).FirstOrDefault(), TextBox)

                'checking the newly added fields if not blank
                If CoAuth_Id.Text = "0" Then
                    If CoAuth_name_field.Text = "" Or CoAuth_deg_field.Text = "" Or CoAuth_role_field.Text = "" Then
                        MessageBox.Show("Fill in the blanks in Co-Author Fields.", "Fill in the Blank(s)")
                        auth_no = co_author_fields_to_save
                        isDynamicFieldsNotBlanks = False
                    End If

                    'letting the formload-generated fields to be blank for deleting once it saved [a row must be blank]
                ElseIf CoAuth_Id.Text <> "0" And CoAuth_name_field.Text = "" And CoAuth_deg_field.Text = "" And CoAuth_role_field.Text = "" Then

                    isDynamicFieldsNotBlanks = True

                    'set to blank if there is a blank in a row of formload generated fields
                ElseIf CoAuth_Id.Text <> "0" And CoAuth_name_field.Text = "" Or CoAuth_deg_field.Text = "" Or CoAuth_role_field.Text = "" Then
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

                    'checking the fields if not blank
                    If publish_level <> "" And TxtPubAcadJournalEdtMode.Text <> "" And TxtPubVolNumEdtMode.Text <> "" And TxtPubIssueNoEdtMode.Text <> "" And TxtPubPageRangeEdtMode.Text <> "" And TxtPubDoiUrlEdtMode.Text <> "" Then

                        'checking the input in textbox that dedicated for number input
                        If IsNumeric(TxtPubVolNumEdtMode.Text) And IsNumeric(TxtPubIssueNoEdtMode.Text) And IsNumeric(TxtPubPageRangeEdtMode.Text) And TxtPubDoiUrlEdtMode.Text <> "" Then

                            'if all condition is met, then get all data from txtbox
                            pub_lvl = publish_level
                            acad_jrnl = TxtPubAcadJournalEdtMode.Text.ToString
                            vol_no = Convert.ToInt64(TxtPubVolNumEdtMode.Text)
                            issue_no = Convert.ToInt64(TxtPubIssueNoEdtMode.Text)
                            page_range = TxtPubPageRangeEdtMode.Text.ToString
                            date_pub = DtPubDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
                            doi_url = TxtPubDoiUrlEdtMode.Text.ToString

                            UpdateUpperFields(title, agenda, sem, schl_yr)
                            UpdateAuthors(author_name, auth_deg_prog, auth_role)
                            UpdateFiles()
                            SaveUpdatedStatCmpltdChckdbx()

                            UpdateAddtnlInfo()

                            isEditModeActive = False
                            iscloseUsingWinControl = False
                            rrm.LoadScholarlyWorks()
                            rrm.BtnRemoveSelection.PerformClick()
                            Me.Close()
                            MessageBox.Show("Successfully Updated", "Successful")
                        Else
                            MessageBox.Show("You entered non-numeric in the in additional information field(s)", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

                    Else
                        MessageBox.Show("Fill in the blank(s)", "Check field in Additional Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If


                    'check if additonal info-presented is checked
                ElseIf isPresented = "Presented" Then

                    'checking the fields if not blank
                    If TxtPreResConfNameEdtMode.Text <> "" And TxtPrePlaceEdtMode.Text <> "" And presented_level <> "" Then

                        'if all condition is met, then get all data from txtbox
                        pre_lvl = presented_level
                        res_conf_name = TxtPreResConfNameEdtMode.Text.ToString
                        date_prsntd = DtPrsntdDateEdtMode.Value.Date.ToString("MM-dd-yyyy")
                        place_prsnttn = TxtPrePlaceEdtMode.Text.ToString

                        UpdateUpperFields(title, agenda, sem, schl_yr)
                        UpdateAuthors(author_name, auth_deg_prog, auth_role)
                        UpdateFiles()
                        SaveUpdatedStatCmpltdChckdbx()

                        UpdateAddtnlInfo()

                        isEditModeActive = False
                        iscloseUsingWinControl = False
                        rrm.LoadScholarlyWorks()
                        rrm.BtnRemoveSelection.PerformClick()
                        Me.Close()
                        MessageBox.Show("Successfully Updated", "Successful")
                    Else
                        MessageBox.Show("Fill in the blank(s)", "Check field in Additional Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If

                    'if loaded additional info was deleted or cleared, then it will be deleted once update button is clicked
                ElseIf isPresented = "NO" And isPublished = "NO" And record_addtnl_info <> "none" And new_addtnl_info = "cleared" Then
                    UpdateUpperFields(title, agenda, sem, schl_yr)
                    UpdateAuthors(author_name, auth_deg_prog, auth_role)
                    UpdateFiles()
                    SaveUpdatedStatCmpltdChckdbx()
                    UpdateAddtnlInfo()
                    isEditModeActive = False
                    iscloseUsingWinControl = False
                    rrm.LoadScholarlyWorks()
                    rrm.BtnRemoveSelection.PerformClick()
                    Me.Close()
                    MessageBox.Show("Successfully Updated", "Successful")

                Else
                    UpdateUpperFields(title, agenda, sem, schl_yr)
                    UpdateAuthors(author_name, auth_deg_prog, auth_role)
                    UpdateFiles()
                    SaveUpdatedStatCmpltdChckdbx()

                    isEditModeActive = False
                    iscloseUsingWinControl = False
                    rrm.LoadScholarlyWorks()
                    rrm.BtnRemoveSelection.PerformClick()
                    Me.Close()
                    MessageBox.Show("Successfully Updated", "Successful")
                End If
            End If

        Else
            MessageBox.Show("001 Fill in the blank field(s) before saving", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'Function to update fields at the top
    Private Sub UpdateUpperFields(ByVal title As String, ByVal agenda As String, ByVal sem As String, ByVal schl_yr As String)
        Dim status As String
        If new_status <> "none" Then
            status = new_status
        Else
            status = record_status
        End If

        con.Close()
        Try
            con.Open()
            Dim query As String = "
            UPDATE `scholarly_works` 
            SET  
                `title` = @title,
                `research_agenda` = @re_ag, 
                `semester` = @sem,
                `school_year` = @schl_yr,
                `status_ongoing_completed` = @stat,
                `published` = @published, 
                `presented` = @presented
            WHERE `sw_id`= @id
                "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@re_ag", agenda)
                cmd.Parameters.AddWithValue("@title", title)
                cmd.Parameters.AddWithValue("@sem", sem)
                cmd.Parameters.AddWithValue("@schl_yr", schl_yr)
                cmd.Parameters.AddWithValue("@stat", status)
                cmd.Parameters.AddWithValue("@published", isPublished)
                cmd.Parameters.AddWithValue("@presented", isPresented)
                cmd.Parameters.AddWithValue("@id", edit_id)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to update details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()

        End Try
    End Sub


    'METHOD TO UPDATE AUTHORS AND CO-AUTHORS
    Private Sub UpdateAuthors(author_name, auth_deg_prog, auth_role)

        'updating author
        con.Close()

        Try
            con.Open()

            'update co_authors
            Dim query As String = "
                            UPDATE authors 
                            SET 
                                `authors_name`= @auth_name, 
                                `degree_program`= @deg_pro, 
                                `role`= @role
                            WHERE 
                                `authors_id`= @id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@auth_name", author_name)
                cmd.Parameters.AddWithValue("@deg_pro", auth_deg_prog)
                cmd.Parameters.AddWithValue("@role", auth_role)
                cmd.Parameters.AddWithValue("@id", edit_id)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to update authors.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try

        'updating co_authors
        Dim auth_no As Integer = 0
        Dim co_author_fields_to_save As Integer = Convert.ToInt64(LblTotalCoAuthFlds.Text)
        Dim co_auth_to_save_id_no As String
        While auth_no <> co_author_fields_to_save
            auth_no += 1
            Dim CoAuth_dynmc_id As String = "LblCoAuthNoId" & auth_no.ToString()
            Dim CoAuth_Id As Label = CType(Me.Controls.Find(CoAuth_dynmc_id, True).FirstOrDefault(), Label)
            co_auth_to_save_id_no = CoAuth_Id.Text

            Dim CoAuth_dynmc_name As String = "CoAuthor" & auth_no.ToString()
            Dim CoAuth_name_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_name, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_deg As String = "CoAuthorDeg" & auth_no.ToString()
            Dim CoAuth_deg_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_deg, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_role As String = "CoAuthorRole" & auth_no.ToString()
            Dim CoAuth_role_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_role, True).FirstOrDefault(), TextBox)


            If co_auth_to_save_id_no <> 0 And CoAuth_name_field.Text = "" And CoAuth_deg_field.Text = "" And CoAuth_role_field.Text = "" Then
                'delete co-author here
                con.Close()
                Try
                    con.Open()

                    'update co_authors
                    Dim del_query As String = "
                            DELETE FROM `co_authors`
                            WHERE `co_authors_id`=@id AND `no#`=@no
                                    "
                    Using del_cmd As New MySqlCommand(del_query, con)
                        del_cmd.Parameters.AddWithValue("@id", edit_id)
                        del_cmd.Parameters.AddWithValue("@no", co_auth_to_save_id_no)
                        del_cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Failed to update co_authors.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try

            ElseIf co_auth_to_save_id_no = 0 Then
                'insert new co authors
                con.Close()
                Try
                    con.Open()

                    'update co_authors
                    Dim query As String = "
                            INSERT INTO co_authors
                            VALUES( 
                                 '',
                                 @id,
                                 @co_auth_name, 
                                 @deg_pro,
                                 @role
                                  )
                                    "
                    Using cmd As New MySqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@co_auth_name", CoAuth_name_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@deg_pro", CoAuth_deg_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@role", CoAuth_role_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@id", edit_id)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Failed to update co_authors.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try

            Else
                Try
                    con.Open()

                    'update co_authors
                    Dim query As String = "
                            UPDATE co_authors 
                            SET 
                                `co_authors_name`= @co_auth_name, 
                                `degree_program`= @deg_pro,
                                `role`= @role
                            WHERE 
                                `co_authors_id`= @id AND `no#`= @id_no"
                    Using cmd As New MySqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@co_auth_name", CoAuth_name_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@deg_pro", CoAuth_deg_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@role", CoAuth_role_field.Text.Trim)
                        cmd.Parameters.AddWithValue("@id_no", co_auth_to_save_id_no)
                        cmd.Parameters.AddWithValue("@id", edit_id)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Failed to update co_authors.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try

            End If

        End While
    End Sub

    'UPDATING ADDITIONAL INFORMATION
    Private Sub UpdateAddtnlInfo()

        'insert if there is no selected
        If record_addtnl_info = "none" And new_addtnl_info = "presented" Then
            con.Close()
            Try
                con.Open()
                Dim insert_qry As String = "
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
		                )
			        "
                Using insert_cmd As New MySqlCommand(insert_qry, con)
                    insert_cmd.Parameters.AddWithValue("@no", Nothing)
                    insert_cmd.Parameters.AddWithValue("@prsntd_id", edit_id)
                    insert_cmd.Parameters.AddWithValue("@prsntd_lvl", pre_lvl)
                    insert_cmd.Parameters.AddWithValue("@rcn", res_conf_name)
                    insert_cmd.Parameters.AddWithValue("@date_prsntd", date_prsntd)
                    insert_cmd.Parameters.AddWithValue("@place_prsntd", place_prsnttn)
                    insert_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        ElseIf record_addtnl_info = "none" And new_addtnl_info = "published" Then
            con.Close()

            Try
                con.Open()
                Dim insert_qry As String = "
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
                Using insert_cmd As New MySqlCommand(insert_qry, con)
                    insert_cmd.Parameters.AddWithValue("@no", Nothing)
                    insert_cmd.Parameters.AddWithValue("@pub_id", edit_id)
                    insert_cmd.Parameters.AddWithValue("@pub_lvl", pub_lvl)
                    insert_cmd.Parameters.AddWithValue("@acd_jrnl", acad_jrnl)
                    insert_cmd.Parameters.AddWithValue("@vol_no", vol_no)
                    insert_cmd.Parameters.AddWithValue("@issue_no", issue_no)
                    insert_cmd.Parameters.AddWithValue("@page_range", page_range)
                    insert_cmd.Parameters.AddWithValue("@date_pub", date_pub)
                    insert_cmd.Parameters.AddWithValue("@doi_url", doi_url)
                    insert_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Published details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        End If

        'updating existing details
        If record_addtnl_info = "presented" Then
            If new_addtnl_info = "presented" Or new_addtnl_info = "none" Then
                con.Close()

                Try
                    con.Open()
                    Dim qry As String = "
                    UPDATE 
                        `presented_details` 
                    SET
                        `level`=@lvl,
                        `research_conference_name`=@rcn,
                        `date_presented`=@date,
                        `place_presentation`=@place
                    WHERE `presented_id`=@id
                    "
                    Using cmd As New MySqlCommand(qry, con)
                        cmd.Parameters.AddWithValue("@lvl", pre_lvl)
                        cmd.Parameters.AddWithValue("@rcn", res_conf_name)
                        cmd.Parameters.AddWithValue("@date", date_prsntd)
                        cmd.Parameters.AddWithValue("@place", place_prsnttn)
                        cmd.Parameters.AddWithValue("@id", edit_id)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try
            End If
            'update published details
        ElseIf record_addtnl_info = "published" Then
            If new_addtnl_info = "published" Or new_addtnl_info = "none" Then
                con.Close()

                Try
                    con.Open()
                    Dim qry As String = "
                    UPDATE 
                        `published_details` 
                    SET
		                `level` = @pub_lvl,
		                `academic_journal` = @acd_jrnl,
		                `volume_no` = @vol_no,
		                `issue_no` = @issue_no,
		                `page_range` = @page_range,
		                `date_published` = @date_pub,
		                `doi_url` = @doi_url

                    WHERE `published_id`=@id
                    "
                    Using cmd As New MySqlCommand(qry, con)
                        cmd.Parameters.AddWithValue("@id", edit_id)
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
                    MessageBox.Show(ex.Message, "Error on Updating Published details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try
            End If
        End If

        'update from existing to new selection in addtional info
        If record_addtnl_info = "presented" And new_addtnl_info = "published" Then
            'delete existing
            con.Close()
            Try
                con.Open()
                Dim delete_qry As String = "
                    DELETE FROM 
                        `presented_details` 
                    WHERE `presented_id`=@id
                    "
                Using delete_cmd As New MySqlCommand(delete_qry, con)
                    delete_cmd.Parameters.AddWithValue("@id", edit_id)
                    delete_cmd.ExecuteNonQuery()
                End Using

                'inserting new selected (published) details
                Dim insert_qry As String = "
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
                Using insert_cmd As New MySqlCommand(insert_qry, con)
                    insert_cmd.Parameters.AddWithValue("@no", Nothing)
                    insert_cmd.Parameters.AddWithValue("@pub_id", edit_id)
                    insert_cmd.Parameters.AddWithValue("@pub_lvl", pub_lvl)
                    insert_cmd.Parameters.AddWithValue("@acd_jrnl", acad_jrnl)
                    insert_cmd.Parameters.AddWithValue("@vol_no", vol_no)
                    insert_cmd.Parameters.AddWithValue("@issue_no", issue_no)
                    insert_cmd.Parameters.AddWithValue("@page_range", page_range)
                    insert_cmd.Parameters.AddWithValue("@date_pub", date_pub)
                    insert_cmd.Parameters.AddWithValue("@doi_url", doi_url)
                    insert_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        ElseIf record_addtnl_info = "published" And new_addtnl_info = "presented" Then
            'delete existing
            con.Close()
            Try
                con.Open()
                Dim delete_qry As String = "
                    DELETE FROM 
                        `published_details` 
                    WHERE `published_id`=@id
                    "
                Using delete_cmd As New MySqlCommand(delete_qry, con)
                    delete_cmd.Parameters.AddWithValue("@id", edit_id)
                    delete_cmd.ExecuteNonQuery()
                End Using

                'inserting new selected (published) details
                Dim insert_qry As String = "
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
		                )
			        "
                Using insert_cmd As New MySqlCommand(insert_qry, con)
                    insert_cmd.Parameters.AddWithValue("@no", Nothing)
                    insert_cmd.Parameters.AddWithValue("@prsntd_id", edit_id)
                    insert_cmd.Parameters.AddWithValue("@prsntd_lvl", pre_lvl)
                    insert_cmd.Parameters.AddWithValue("@rcn", res_conf_name)
                    insert_cmd.Parameters.AddWithValue("@date_prsntd", date_prsntd)
                    insert_cmd.Parameters.AddWithValue("@place_prsntd", place_prsnttn)
                    insert_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        End If


        'delete additional info if cleared details
        If record_addtnl_info = "published" And new_addtnl_info = "cleared" Then
            'delete if cleared
            con.Close()
            Try
                con.Open()
                Dim delete_qry As String = "
                    DELETE FROM 
                        `published_details` 
                    WHERE `published_id`=@id
                    "
                Using delete_cmd As New MySqlCommand(delete_qry, con)
                    delete_cmd.Parameters.AddWithValue("@id", edit_id)
                    delete_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try

        ElseIf record_addtnl_info = "presented" And new_addtnl_info = "cleared" Then
            'delete if cleared
            con.Close()
            Try
                con.Open()
                Dim delete_qry As String = "
                    DELETE FROM 
                        `presented_details` 
                    WHERE `presented_id`=@id
                    "
                Using delete_cmd As New MySqlCommand(delete_qry, con)
                    delete_cmd.Parameters.AddWithValue("@id", edit_id)
                    delete_cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error on Updating Presented details.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        End If
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
        PnlStatCmpltdEdtMode.Visible = False
        CbxSftCpySbmttdEdtMode.Checked = False
        CbxHrdCpySbmttdEdtMode.Checked = False
        CbxDgiSbmttdEdtMode.Checked = False
        CbxRgaEfSbmttdEdtMode.Checked = False
        RdEdtStatCmpltd.Checked = False
        RdEdtStatOngng.Checked = False
        BtnEdtCnclSlctn.PerformClick()
    End Sub

    'PUBLISHED LEVEL RADIO BUTTON EVENT
    Private Sub RdPubLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelInstiEdtMode.MouseClick
        publish_level = "Institutional"
    End Sub
    Private Sub RdPubLevelInter_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelInterEdtMode.MouseClick
        publish_level = "International"
    End Sub
    Private Sub RdPubLevelLoc_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelLocEdtMode.MouseClick
        publish_level = "Local"
    End Sub
    Private Sub RdPubLevelNat_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPubLevelNatEdtMode.MouseClick
        publish_level = "National"
    End Sub

    'PRESENTED LEVEL RADIO BUTTON EVENT
    Private Sub RdPreLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelInstiEdtMode.MouseClick
        presented_level = "Institutional"
    End Sub
    Private Sub RdPreLevelInter_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelInterEdtMode.MouseClick
        presented_level = "International"
    End Sub
    Private Sub RdPreLevelLoc_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelLocEdtMode.MouseClick
        presented_level = "Local"
    End Sub
    Private Sub RdPreLevelNat_MouseClick(sender As Object, e As MouseEventArgs) Handles RdPreLevelNatEdtMode.MouseClick
        presented_level = "National"
    End Sub



    'CODES FOR BUTTON ADD NEW CO-AUTHOR
    Private Sub BtnAddNewCoAuthor_Click_1(sender As Object, e As EventArgs) Handles BtnAddCoAuthFldEdt.Click

        cntr = TxtAuthToAddCount.Text
        total_fields = LblTotalCoAuthFlds.Text
        BtnAddCoAuthFldEdt.Enabled = False
        co_authors_no = 0
        If cntr > 50 Or cntr < 1 Then
            MsgBox("Maximum of 50 every adding of fields")
        Else
            While cntr <> 0
                AddOrLoadCoAuthField()
                cntr -= 1
            End While
            LblTotalCoAuthFlds.Text = total_fields.ToString()
            BtnAddCoAuthFldEdt.Enabled = True
            TxtAuthToAddCount.Text = "1"
            Dim co_author_dynamic_name As String = "CoAuthor" & auth_count.ToString()
            Dim co_auth_field_name As TextBox = CType(Me.Controls.Find(co_author_dynamic_name, True).FirstOrDefault(), TextBox)
            co_auth_field_name.Focus()

        End If
    End Sub


    'REMOVING UNUSED CO-AUTHOR FIELDS AT THE BOTTOM OF PANEL CONTAINER
    Private Sub BtnRemoveField_Click(sender As Object, e As EventArgs) Handles BtnRemoveField.Click
        If auth_count > 1 Then
            Dim lbl_id_no As String = "LblCoAuthNoId" & auth_count.ToString()
            Dim last_lbl_id_no As Label = TryCast(PnlEditCoAuthFlds.Controls(lbl_id_no), Label)

            Dim txtbox_author As String = "CoAuthor" & auth_count.ToString()
            Dim last_author_txtbox As TextBox = TryCast(PnlEditCoAuthFlds.Controls(txtbox_author), TextBox)

            Dim lbl_auth As String = "LabelAuth" & auth_count.ToString()
            Dim last_author_lbl As Label = TryCast(PnlEditCoAuthFlds.Controls(lbl_auth), Label)

            Dim txtbox_deg As String = "CoAuthorDeg" & auth_count.ToString()
            Dim last_deg_txtbox As TextBox = TryCast(PnlEditCoAuthFlds.Controls(txtbox_deg), TextBox)

            Dim lbl_deg As String = "LabelDeg" & auth_count.ToString()
            Dim last_deg_lbl As Label = TryCast(PnlEditCoAuthFlds.Controls(lbl_deg), Label)

            Dim txtbox_Role As String = "CoAuthorRole" & auth_count.ToString()
            Dim last_Role_txtbox As TextBox = TryCast(PnlEditCoAuthFlds.Controls(txtbox_Role), TextBox)

            Dim lbl_role As String = "LabelRole" & auth_count.ToString()
            Dim last_role_lbl As Label = TryCast(PnlEditCoAuthFlds.Controls(lbl_role), Label)

            If last_author_txtbox IsNot Nothing And last_deg_txtbox IsNot Nothing And last_Role_txtbox IsNot Nothing Then
                If last_author_txtbox.Text <> "" Or last_deg_txtbox.Text <> "" Or last_Role_txtbox.Text <> "" Then
                    MessageBox.Show("The fields you want to remove contains data.", "Unable to delete this field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf last_lbl_id_no IsNot Nothing And last_lbl_id_no.Text <> "0" Then
                    MessageBox.Show("The fields you want to remove is connected to the database data. If you want to delete this co-author, just leave it blank then click update.", "Unable to delete this field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else

                    PnlEditCoAuthFlds.Controls.Remove(last_lbl_id_no)
                    PnlEditCoAuthFlds.Controls.Remove(last_author_txtbox)
                    PnlEditCoAuthFlds.Controls.Remove(last_author_lbl)

                    PnlEditCoAuthFlds.Controls.Remove(last_deg_txtbox)
                    PnlEditCoAuthFlds.Controls.Remove(last_deg_lbl)

                    PnlEditCoAuthFlds.Controls.Remove(last_Role_txtbox)
                    PnlEditCoAuthFlds.Controls.Remove(last_role_lbl)

                    auth_count -= 1
                    total_fields -= 1
                    LblTotalCoAuthFlds.Text = total_fields.ToString()
                    PnlEditCoAuthFlds.Height = PnlEditCoAuthFlds.Height - 24
                End If

            Else
                MessageBox.Show("Field(s) not found!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("There must be Minimum of 1 Co-Author Field", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub


    'ADDING AND SUBTRACTING TO FIELDS COUNT
    Private Sub BtnAddOnTxt_Click(sender As Object, e As EventArgs) Handles BtnAddOnTxt.Click
        Dim taax As Integer = TxtAuthToAddCount.Text
        TxtAuthToAddCount.Text = taax + 1
    End Sub

    Private Sub BtnSubOnTxt_Click(sender As Object, e As EventArgs) Handles BtnSubOnTxt.Click
        Dim taax As Integer = TxtAuthToAddCount.Text
        TxtAuthToAddCount.Text = taax - 1
    End Sub

    'RESTRICTING TEXTBOX FOR TOTAL COUNT FOR NEW CO-AUTHOR FIELDS | FIELDS ARE NOT ALLOWED TO BE BLANK, 0, GREATER THAN 50,
    Private Sub TxtAuthToAddCount_TextChanged(sender As Object, e As EventArgs) Handles TxtAuthToAddCount.TextChanged
        Dim field_count As String = TxtAuthToAddCount.Text.Trim
        If field_count <> "" And IsNumeric(field_count) Then
            If field_count < 1 Then
                TxtAuthToAddCount.Text = "1"
            ElseIf field_count > 50 Then
                TxtAuthToAddCount.Text = "50"
            End If
        End If
    End Sub
    Private Sub TxtAuthToAddCount_Leave(sender As Object, e As EventArgs) Handles TxtAuthToAddCount.Leave
        If TxtAuthToAddCount.Text = "" Then
            TxtAuthToAddCount.Text = "1"
        End If

    End Sub


    'VARIABLE TO HOLD SUBMITTED REQUIREMENTS WHEN COMPLETED STATUS WAS SELECTED
    Dim isSftCpySubmttd As String = "NO"
    Dim isHrdCpySubmttd As String = "NO"
    Dim isDgiSubmttd As String = "NO"
    Dim isRgaSubmttd As String = "NO"

    'SHOWING AND HIDING THEIR DATE PICKER ONCE CHECKED OR UNCHECKED
    Private Sub CbxSftCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxSftCpySbmttdEdtMode.CheckedChanged
        If CbxSftCpySbmttdEdtMode.Checked = True Then
            DtSftCpySbmttdDateEdtMode.Visible = True
            isSftCpySubmttd = "YES"
        Else
            DtSftCpySbmttdDateEdtMode.Visible = False
            isSftCpySubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxHrdCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxHrdCpySbmttdEdtMode.CheckedChanged
        If CbxHrdCpySbmttdEdtMode.Checked = True Then
            DtHrdCpySbmttdDateEdtMode.Visible = True
            isHrdCpySubmttd = "YES"
        Else
            DtHrdCpySbmttdDateEdtMode.Visible = False
            isHrdCpySubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxDgiSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxDgiSbmttdEdtMode.CheckedChanged
        If CbxDgiSbmttdEdtMode.Checked = True Then
            DtDgiSbmttdDateEdtMode.Visible = True
            isDgiSubmttd = "YES"
        Else
            DtDgiSbmttdDateEdtMode.Visible = False
            isDgiSubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxRgaEfSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxRgaEfSbmttdEdtMode.CheckedChanged
        If CbxRgaEfSbmttdEdtMode.Checked = True Then
            DtRgaSbmttdDateEdtMode.Visible = True
            isRgaSubmttd = "YES"
        Else
            DtRgaSbmttdDateEdtMode.Visible = False
            isRgaSubmttd = "NO"
        End If
        ShowPrintThesisClearance()
    End Sub


    'SHOW THESIS CLEARANCE BUTTON WHEN 4 OF CHECKBOX CONDITION IS CHECKED
    Public Sub ShowPrintThesisClearance()
        If CbxSftCpySbmttdEdtMode.Checked = True And CbxHrdCpySbmttdEdtMode.Checked = True And CbxDgiSbmttdEdtMode.Checked = True And CbxRgaEfSbmttdEdtMode.Checked = True Then
            BtnThssClrnc.Enabled = True
            BtnThssClrnc.BackColor = Color.LimeGreen
        Else
            BtnThssClrnc.Enabled = False
            BtnThssClrnc.BackColor = Color.LightGray
        End If
    End Sub


    'RADIO BUTTON PUBLISHED AND PRESENTED , HIDING AND SHOWING OF UI
    'published radio button

    Private Sub RdBtnPubEdtMode_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPubEdtMode.MouseClick
        If RdBtnPubEdtMode.Checked = True Then
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
            PnlPublished.Enabled = True
            PnlPublished.Height = 230
            isPublished = "Published"
            isPresented = "NO"
            BtnEdtCnclSlctn.Visible = True
            new_addtnl_info = "published"
        Else
            isPublished = "NO"
        End If
    End Sub
    'presented radio button
    Private Sub RdBtnPresentedEdtMode_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPresentedEdtMode.MouseClick
        If RdBtnPresentedEdtMode.Checked = True Then
            PnlPublished.Height = 0
            PnlPresented.Enabled = True
            PnlPresented.Height = 230
            isPresented = "Presented"
            isPublished = "NO"
            BtnEdtCnclSlctn.Visible = True
            new_addtnl_info = "presented"
        Else
            isPresented = "NO"
        End If
    End Sub

    Private Sub BtnCancelSelection_Click(sender As Object, e As EventArgs) Handles BtnEdtCnclSlctn.Click
        new_addtnl_info = "cleared"
        isPublished = "NO"
        PnlPublished.Enabled = False
        RdBtnPubEdtMode.Checked = False

        isPresented = "NO"
        PnlPresented.Enabled = False
        RdBtnPresentedEdtMode.Checked = False

        PnlPresented.Enabled = False
        PnlPresented.Height = 0
        PnlPublished.Height = 230

        BtnEdtCnclSlctn.Visible = False

        RdBtnPresentedEdtMode.Checked = False
        RdBtnPubEdtMode.Checked = False

        RdPubLevelInstiEdtMode.Checked = False
        RdPubLevelInterEdtMode.Checked = False
        RdPubLevelLocEdtMode.Checked = False
        RdPubLevelNatEdtMode.Checked = False

        RdPreLevelInstiEdtMode.Checked = False
        RdPreLevelInterEdtMode.Checked = False
        RdPreLevelLocEdtMode.Checked = False
        RdPreLevelNatEdtMode.Checked = False
    End Sub

    'execute codes if form window is close by user
    Private Sub EditWorkRecord_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If iscloseUsingWinControl = True Then
            Dim close_window As DialogResult = MessageBox.Show("Are you sure you want to cancel editing and close this form? All changes will not be saved.", "Click Yes to close this form.", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If close_window = DialogResult.Yes Then
                isEditModeActive = False
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = False
        End If

    End Sub
    Private Sub EditWorkRecord_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        iscloseUsingWinControl = True
        on_edit_mode = 0
        isEditModeActive = False
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        iscloseUsingWinControl = True
        isEditModeActive = False
        Me.Close()
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
            End if
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


End Class