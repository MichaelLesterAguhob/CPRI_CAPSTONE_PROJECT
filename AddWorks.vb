
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class AddWorks



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
            MsgBox("Greater Than 50 is not allowed")
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
    Private Sub RdStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatCmpltd.MouseClick
        If RdStatCmpltd.Checked = True Then
            PnlStatCmpltd.Visible = True
        End If
    End Sub

    Private Sub RdStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatOngng.MouseClick
        PnlStatCmpltd.Visible = False
        CbxSftCpySbmttd.Checked = False
        CbxHrdCpySbmttd.Checked = False
        CbxDgiSbmttd.Checked = False
        CbxRgaEfSbmttd.Checked = False
    End Sub

    'SHOWING AND HIDING THEIR DATE PICKER ONCE CHECKED OR UNCHECKED
    Private Sub CbxSftCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxSftCpySbmttd.CheckedChanged
        If CbxSftCpySbmttd.Checked = True Then
            DtSftCpySbmttdDate.Visible = True
        Else
            DtSftCpySbmttdDate.Visible = False
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxHrdCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxHrdCpySbmttd.CheckedChanged
        If CbxHrdCpySbmttd.Checked = True Then
            DtHrdCpySbmttdDate.Visible = True
        Else
            DtHrdCpySbmttdDate.Visible = False
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxDgiSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxDgiSbmttd.CheckedChanged
        If CbxDgiSbmttd.Checked = True Then
            DtDgiSbmttdDate.Visible = True
        Else
            DtDgiSbmttdDate.Visible = False
        End If
        ShowPrintThesisClearance()
    End Sub

    Private Sub CbxRgaEfSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxRgaEfSbmttd.CheckedChanged
        If CbxRgaEfSbmttd.Checked = True Then
            DtRgaSbmttdDate.Visible = True
        Else
            DtRgaSbmttdDate.Visible = False
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
    Private Sub RdBtnPub_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPub.MouseClick
        If RdBtnPub.Checked = True Then
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
            PnlPublished.Enabled = True
            PnlPublished.Height = 230
        Else
            PnlPublished.Enabled = False
            PnlPublished.Height = 0
        End If
    End Sub

    Private Sub RdBtnPresented_MouseClick(sender As Object, e As MouseEventArgs) Handles RdBtnPresented.MouseClick
        If RdBtnPresented.Checked = True Then
            PnlPublished.Enabled = False
            PnlPublished.Height = 0
            PnlPresented.Enabled = True
            PnlPresented.Height = 230
        Else
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
        End If
    End Sub
    '===============================END=====================================

    '|||||||||||||||||||||||||||||||||| END OF UI RESPONSE OR FUNCTIONALITES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||




    '|||||||||||||||||||||||||||||||||| BEGINNING CODES FOR MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||

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
        TxtAddAuthX.Text = "6"
        BtnAddNewCoAuthor.PerformClick()
        GenerateControlNumber()
    End Sub
    '===============================END=====================================

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


    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF OR WORD FILES
    'whole file
    Dim whole_file_path As String
    Dim whole_file_data As Byte()
    Dim whole_file_extension As String
    Private Sub BtnBrowseWholeFile_Click(sender As Object, e As EventArgs) Handles BtnBrowseWholeFile.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            whole_file_path = openFileDialog.FileName
            whole_file_data = File.ReadAllBytes(whole_file_path)
            whole_file_extension = Path.GetExtension(whole_file_path)
            TxtBrowsedFileWhl.Text = whole_file_path.ToString
        Else
            whole_file_path = ""
            MessageBox.Show("No selected file")
        End If
    End Sub

    'abstract file
    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()
    Dim abstract_file_extension As String
    Private Sub BtnBrowseAbstractFile_Click(sender As Object, e As EventArgs) Handles BtnBrowseAbstractFile.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            abstract_file_path = openFileDialog.FileName
            abstract_file_data = File.ReadAllBytes(abstract_file_path)
            abstract_file_extension = Path.GetExtension(abstract_file_path)
            TxtBrowsedFileAbs.Text = abstract_file_path.ToString
        Else
            abstract_file_path = ""
            MessageBox.Show("No selected file")
        End If
    End Sub
    '==============================END=====================================

    'CODES FOR SAVING/UPLOADING RESEARCH
    Private Sub BtnSaveResearch_Click(sender As Object, e As EventArgs) Handles BtnSaveResearch.Click
        BtnSaveResearch.Enabled = False
        Dim control_no As Integer = Convert.ToInt64(TxtResearchID.Text)
        Dim research_agenda As String = TxtRsrchAgenda.Text.Trim
        Dim research_title As String = TxtRsrchTitle.Text.Trim
        Dim author_name As String = TxtAuthorName.Text.Trim
        Dim author_deg As String = TxtAthrDegprog.Text.Trim
        Dim author_role As String = TxtAthrRole.Text.Trim

        con.Close()

        If control_no <> 0 And research_agenda <> "" And research_title <> "" And author_name <> "" And author_deg <> "" And author_role <> "" And whole_file_path <> "" And abstract_file_path <> "" Then
            ProgressBar1.Visible = True
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
                    `stat_completed_id`, 
                    `author_id`,
                    `co-author_id`, 
                    `published`, 
                    `published_id`, 
                    `presented`, 
                    `presented_id`, 
                    `whole_file_id`,
                    `abstract_file_id`
                ) 
                VALUES 
                (
                    @no, 
                    @sw_id, 
                    @title, 
                    @research_agenda, 
                    @date_completed, 
                    @status_ongoing_completed, 
                    @stat_completed_id, 
                    @author_id, 
                    @co_author_id, 
                    @published, 
                    @published_id, 
                    @presented, 
                    @presented_id, 
                    @whole_file_id,
                    @abstract_file_id
                )"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@no", Nothing)
                    cmd.Parameters.AddWithValue("@sw_id", control_no)
                    cmd.Parameters.AddWithValue("@title", research_title)
                    cmd.Parameters.AddWithValue("@research_agenda", research_agenda)
                    cmd.Parameters.AddWithValue("@date_completed", "sample==")
                    cmd.Parameters.AddWithValue("@status_ongoing_completed", 0)
                    cmd.Parameters.AddWithValue("@stat_completed_id", control_no)
                    cmd.Parameters.AddWithValue("@author_id", control_no)
                    cmd.Parameters.AddWithValue("@co_author_id", control_no)
                    cmd.Parameters.AddWithValue("@published", "sample==")
                    cmd.Parameters.AddWithValue("@published_id", control_no)
                    cmd.Parameters.AddWithValue("@presented", "sample==")
                    cmd.Parameters.AddWithValue("@presented_id", control_no)
                    cmd.Parameters.AddWithValue("@whole_file_id", control_no)
                    cmd.Parameters.AddWithValue("@abstract_file_id", control_no)
                    cmd.ExecuteNonQuery()
                End Using

                SaveAuthor()
                SaveWholeFiles()
                SaveAbstractFiles()
                SaveCoAuthors()
                MessageBox.Show("Successfully Saved", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed on Saving Research")
                ProgressBar1.Visible = False
            Finally
                con.Close()
                GenerateControlNumber()
                ProgressBar1.Value = 0
                ProgressBar1.Visible = False
                ClearTextBox(Me)
            End Try

        Else
            MessageBox.Show("Fill in the blank field(s) before saving", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        BtnSaveResearch.Enabled = True
    End Sub


    '1 BY 1 SAVING CO-AUTHOR USING WHILE LOOP
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
        Finally
            con.Close()
        End Try
    End Sub
    '==============================END=====================================

    '1 BY 1 SAVING CO-AUTHOR TO DATABASE LOOP
    Private Sub SaveCoAuthors()
        con.Close()
        Dim control_no As Integer = Convert.ToInt64(TxtResearchID.Text)
        Dim auth_no As Integer = 1
        Dim co_author_fields_to_save As Integer = Convert.ToInt64(total_fields)
        'name used in dynamic textbox for co-authors  [CoAuthor, CoAuthorDeg, CoAuthorRole & auth_count]
        While auth_no <> co_author_fields_to_save
            Dim CoAuth_dynmc_name As String = "CoAuthor" & auth_no.ToString()
            Dim CoAuth_name_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_name, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_deg As String = "CoAuthorDeg" & auth_no.ToString()
            Dim CoAuth_deg_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_deg, True).FirstOrDefault(), TextBox)

            Dim CoAuth_dynmc_role As String = "CoAuthorRole" & auth_no.ToString()
            Dim CoAuth_role_field As TextBox = CType(Me.Controls.Find(CoAuth_dynmc_role, True).FirstOrDefault(), TextBox)

            If CoAuth_name_field.Text = "" Or CoAuth_deg_field.Text = "" Or CoAuth_role_field.Text = "" Then
                MessageBox.Show("Fill in the blanks in Co-Author Fields.", "Fill in the Blank(s)")
            Else
                Try
                    con.Open()
                    Dim query As String = "INSERT INTO `co_authors`(`no#`, `co_authors_id`, `co_authors_name`, `degree_program`, `role`)
                    VALUES(@no, @c_a_i, @c_a_n, @d_p, @role)"
                    Using cmd As New MySqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@no", Nothing)
                        cmd.Parameters.AddWithValue("@c_a_i", control_no)
                        cmd.Parameters.AddWithValue("@c_a_n", CoAuth_name_field)
                        cmd.Parameters.AddWithValue("@d_p", CoAuth_deg_field)
                        cmd.Parameters.AddWithValue("@role", CoAuth_role_field)
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Failed on Saving Co-Authors", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                Finally
                    con.Close()
                End Try
            End If

            auth_no += 1
        End While
    End Sub
    '==============================END=====================================


    'INSERTING PDF OR WORD FILES IN THA DATABASE
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
        End Try
    End Sub

    'INSERTING PDF OR WORD FILES IN THA DATABASE
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
    End Sub
    '==============================END=====================================

    'RETRIEVING FILES IN DATABASE AND OPENING IT IN PDF OR WORD
    Private Sub PreviewFileButton_Click(sender As Object, e As EventArgs) Handles PreviewFileButton.Click
        Dim pdfByteArray As Byte() = RetrievePdfFile()

        If pdfByteArray IsNot Nothing AndAlso pdfByteArray.Length > 0 Then
            Dim tempFilePath As String = Path.GetTempFileName()
            tempFilePath = Path.ChangeExtension(tempFilePath, ".pdf")
            Try
                File.WriteAllBytes(tempFilePath, pdfByteArray)
                File.SetAttributes(tempFilePath, FileAttributes.ReadOnly)
                If File.Exists(tempFilePath) Then

                    Process.Start(tempFilePath)
                Else
                    MessageBox.Show("File not exists")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Failed to open PDF file", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Console.WriteLine(ex.Message)
            End Try
        End If
    End Sub

    Function RetrievePdfFile() As Byte()
        Dim pdfByteArray As Byte() = Nothing
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT file_data FROM sw_abstract WHERE abstract_id=@abs_id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@abs_id", 1234)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                If reader.Read() Then
                    pdfByteArray = DirectCast(reader("file_data"), Byte())
                End If
                reader.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to retrieve PDF file from database. Error: " & ex.Message)
            Console.WriteLine(ex.Message)
        Finally
            con.Close()
        End Try
        Return pdfByteArray
    End Function
    '==============================END=====================================


    '|||||||||||||||||||||||||||||||||| END OF MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

End Class



