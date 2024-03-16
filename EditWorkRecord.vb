Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class EditWorkRecord

    ' Variables declaration
    Dim publish_level As String = ""
    Dim presented_level As String = ""

    Dim whole_file_path As String
    Dim whole_file_data As Byte()
    Dim whole_file_extension As String

    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()
    Dim abstract_file_extension As String

    Dim auth_count As Integer = 0

    Dim co_author_name As String = ""
    Dim co_author_deg As String = ""
    Dim co_author_role As String = ""



    ReadOnly edit_id As Integer
    Public Sub New(sw_edit_id As Integer)
        InitializeComponent()
        edit_id = sw_edit_id
    End Sub


    '|||||||||||||||||||||||||||||||||| BEGINNING CODES FOR MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||

    Private Sub EditWorkRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadToEditRecord()

    End Sub

    Private Sub LoadToEditRecord()
        con.Close()

        Try
            ConOpen()
            Dim query As String = "
                                SELECT 
                                    scholarly_works.sw_id,
                                    scholarly_works.title,
                                    scholarly_works.research_agenda,
                                    scholarly_works.status_ongoing_completed,
                                    authors.authors_name,
                                    authors.degree_program,
                                    authors.role,
                                    sw_whole_file.file_name AS whl_file_name,
                                    sw_abstract.file_name AS abs_file_name
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
                If reader.HasRows Then
                    If reader.Read() Then
                        TxtEditResearchID.Text = reader("sw_id").ToString()
                        TxtEditRsrchTitle.Text = reader("title").ToString()
                        TxtEditRsrchAgenda.Text = reader("research_agenda").ToString()

                        TxtEditAuthName.Text = reader("authors_name").ToString()
                        TxtEditAuthDeg.Text = reader("degree_program").ToString()
                        TxtEditAuthRole.Text = reader("role").ToString()

                        TxtUplddWhlFileName.Text = reader("whl_file_name").ToString()
                        TxtUplddAbsFileName.Text = reader("abs_file_name").ToString()

                        ' check if the status is completed and execute codes if true
                        If reader("status_ongoing_completed") = "Completed" Then
                            'get completed checked list
                            reader.Close()

                            Dim clrnc_chcklst As String = "SELECT * FROM status_completed_info WHERE stat_completed_id=@edit_id"
                                Using clrnc_cmd As New MySqlCommand(clrnc_chcklst, con)
                                    clrnc_cmd.Parameters.AddWithValue("@edit_id", edit_id)
                                Dim clrnc_reader As MySqlDataReader = clrnc_cmd.ExecuteReader()

                                If clrnc_reader.HasRows Then
                                    If clrnc_reader.Read() Then

                                        If clrnc_reader("soft_copy_sbmttd_date") <> "" Then

                                            Dim dateString As String = clrnc_reader("soft_copy_sbmttd_date")
                                            Dim parsedDate As DateTime
                                            If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                                                ' if Parsing
                                                CbxSftCpySbmttdEdtMode.Checked = True
                                                DtSftCpySbmttdDateEdtMode.Value = parsedDate
                                            Else
                                                ' if Parsing faile
                                                MessageBox.Show("soft_copy_sbmttd_date Invalid date format")
                                            End If
                                        End If


                                        If clrnc_reader("hard_copy_sbmttd_date") <> "" Then
                                            Dim dateString As String = clrnc_reader("hard_copy_sbmttd_date")
                                            Dim parsedDate As DateTime
                                            If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                                                ' if Parsing
                                                CbxHrdCpySbmttdEdtMode.Checked = True
                                                DtHrdCpySbmttdDateEdtMode.Value = parsedDate
                                            Else
                                                ' if Parsing faile
                                                MessageBox.Show("hard_copy_sbmttd_date Invalid date format")
                                            End If
                                        End If


                                        If clrnc_reader("dgi_sbmttd_date") <> "" Then
                                            Dim dateString As String = clrnc_reader("dgi_sbmttd_date")
                                            Dim parsedDate As DateTime
                                            If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                                                ' if Parsing
                                                CbxDgiSbmttdEdtMode.Checked = True
                                                DtDgiSbmttdDateEdtMode.Value = parsedDate
                                            Else
                                                ' if Parsing faile
                                                MessageBox.Show("Dgi_copy_sbmttd_date Invalid date format")
                                            End If
                                        End If


                                        If clrnc_reader("rga_ef_sbmttd_date") <> "" Then
                                            Dim dateString As String = clrnc_reader("rga_ef_sbmttd_date")
                                            Dim parsedDate As DateTime

                                            If DateTime.TryParseExact(dateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, parsedDate) Then
                                                ' if Parsing
                                                CbxRgaEfSbmttdEdtMode.Checked = True
                                                DtRgaSbmttdDateEdtMode.Value = parsedDate
                                            Else
                                                ' if Parsing faile
                                                MessageBox.Show("Rga_copy_sbmttd_date Invalid date format")
                                            End If
                                        End If

                                    End If
                                End If
                                    clrnc_reader.Close()
                                End Using

                                'check the Conmpleted Radio Button and show the panel for clearance checklist
                                RdEdtStatCmpltd.Checked = True
                                PnlStatCmpltdEdtMode.Visible = True

                        Else
                            'set the ongoing radio button checked
                            RdEdtStatOngng.Checked = True
                        End If


                        'getting co authors record and loaded it to dynamic textbix component
                        Dim query2 As String = "SELECT * FROM co_authors WHERE co_authors_id= @edit_id"
                            Using cmd2 As New MySqlCommand(query2, con)
                                cmd2.Parameters.AddWithValue("@edit_id", edit_id)

                                Dim reader2 As MySqlDataReader = cmd2.ExecuteReader()
                                While reader2.Read()
                                    co_author_name = reader2("co_authors_name").ToString
                                    co_author_deg = reader2("degree_program").ToString
                                    co_author_role = reader2("role").ToString
                                    AddOrLoadCoAuthField()
                                End While
                                LblTotalCoAuthFlds.Text = auth_count.ToString()
                                co_author_name = ""
                                co_author_deg = ""
                            co_author_role = ""
                            reader2.Close()
                        End Using

                    End If
                    Else
                    MessageBox.Show("No Data Found")

                End If
                reader.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine(ex.Message)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub
    '===============================END=====================================

    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF FILE
    'whole file
    Private Sub BtnBrowseWholeFile_Click(sender As Object, e As EventArgs) Handles BtnBrwsWhl.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            whole_file_path = openFileDialog.FileName
            whole_file_data = File.ReadAllBytes(whole_file_path)
            whole_file_extension = Path.GetExtension(whole_file_path)
            TxtUplddWhlFileName.Text = whole_file_path.ToString
        Else
            whole_file_path = ""
            TxtUplddWhlFileName.Text = "No Selected File"
        End If
    End Sub

    'abstract file
    Private Sub BtnBrowseAbstractFile_Click(sender As Object, e As EventArgs) Handles BtnBrwsAbstrct.Click
        Dim openFileDialog As New OpenFileDialog With {
        .Filter = "PDF Files (*.pdf)|*.pdf",
        .InitialDirectory = "C:\"
        }

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            abstract_file_path = openFileDialog.FileName
            abstract_file_data = File.ReadAllBytes(abstract_file_path)
            abstract_file_extension = Path.GetExtension(abstract_file_path)
            TxtUplddAbsFileName.Text = abstract_file_path.ToString
        Else
            abstract_file_path = ""
            TxtUplddAbsFileName.Text = "No Selected File"
        End If
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
        PnlStatCmpltdEdtMode.Visible = False
        CbxSftCpySbmttdEdtMode.Checked = False
        CbxHrdCpySbmttdEdtMode.Checked = False
        CbxDgiSbmttdEdtMode.Checked = False
        CbxRgaEfSbmttdEdtMode.Checked = False
        RdEdtStatCmpltd.Checked = False
        RdEdtStatOngng.Checked = False
        status = ""
        BtnEdtCnclSlctn.PerformClick()
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
    Private Sub RdPreLevelInsti_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtPreLevelInsti.MouseClick
        presented_level = "Intitutional"
    End Sub
    Private Sub RdPreLevelInter_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtPreLevelInter.MouseClick
        presented_level = "International"
    End Sub
    Private Sub RdPreLevelLoc_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtPreLevelLoc.MouseClick
        presented_level = "Local"
    End Sub
    Private Sub RdPreLevelNat_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtPreLevelNat.MouseClick
        presented_level = "National"
    End Sub
    '==============================END=====================================

    '|||||||||||||||||||||||||||||||||| END OF MAIN FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||


    ' |||||||||||||||||||||||||||||||| BEGINNING CODES BELOW ARE FOR UI RESPONSES OR FUNCTIONALITIES ||||||||||||||||||||||||||||||||||||||

    'ADDING NEW TEXTBOX COMPONENT OR CONTROL INSIDE THE PANEL CONTAINER
    Public Sub AddOrLoadCoAuthField()
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
        lbl_auth.Location = New Point(25, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth.Location = New Point(114, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))

        lbl_auth_deg.Location = New Point(498, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth_deg.Location = New Point(619, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))

        lbl_auth_role.Location = New Point(1048, 10 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))
        new_co_auth_role.Location = New Point(1099, 8 + ((PnlEditCoAuthFlds.Controls.Count * 30) / 6))


        'ADDING ELEMENTS INTO THE CONTAINER
        PnlEditCoAuthFlds.Controls.Add(lbl_auth)
        PnlEditCoAuthFlds.Controls.Add(lbl_auth_deg)
        PnlEditCoAuthFlds.Controls.Add(lbl_auth_role)

        PnlEditCoAuthFlds.Controls.Add(new_co_auth)
        PnlEditCoAuthFlds.Controls.Add(new_co_auth_deg)
        PnlEditCoAuthFlds.Controls.Add(new_co_auth_role)

        PnlEditCoAuthFlds.Height = PnlEditCoAuthFlds.Height + 24
    End Sub
    '==============================END=====================================

    'CODES FOR BUTTON ADD NEW CO-AUTHOR
    Dim cntr As String 'holder of number that system will generate textbox component for author
    Dim total_fields As String 'var holder of total field of existing co-auhtor displayed in label
    Private Sub BtnAddNewCoAuthor_Click_1(sender As Object, e As EventArgs) Handles BtnAddCoAuthFldEdt.Click

        cntr = TxtAuthToAddCount.Text
        total_fields = LblTotalCoAuthFlds.Text
        BtnAddCoAuthFldEdt.Enabled = False

        If cntr > 50 Or cntr < 1 Then
            MsgBox("Maximum of 50 every adding of fields")
        Else
            While cntr <> 0
                AddOrLoadCoAuthField()
                cntr -= 1
                total_fields += 1
                LblTotalCoAuthFlds.Text = total_fields.ToString()
            End While
            BtnAddCoAuthFldEdt.Enabled = True
            TxtAuthToAddCount.Text = "1"
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
            Else
                MessageBox.Show("Field(s) not found!", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("There must be Minimum of 1 Co-Author Field", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Sub
    '==============================END=====================================

    'ADDING AND SUBTRACTING TO FIELDS COUNT
    Private Sub BtnAddOnTxt_Click(sender As Object, e As EventArgs) Handles BtnAddOnTxt.Click
        Dim taax As Integer = TxtAuthToAddCount.Text
        TxtAuthToAddCount.Text = taax + 1
    End Sub

    Private Sub BtnSubOnTxt_Click(sender As Object, e As EventArgs) Handles BtnSubOnTxt.Click
        Dim taax As Integer = TxtAuthToAddCount.Text
        TxtAuthToAddCount.Text = taax - 1
    End Sub
    '==============================END=====================================

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
    '==============================END=====================================

    'ONGOING OR COMPLETED EVENT HANDLES SHOW / HIDE 4 CHECKBOXES PANEL
    Dim status As String = ""
    Private Sub RdStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtStatCmpltd.MouseClick
        If RdEdtStatCmpltd.Checked = True Then
            PnlStatCmpltdEdtMode.Visible = True
            status = "Completed"
        End If
    End Sub

    Private Sub RdStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtStatOngng.MouseClick
        PnlStatCmpltdEdtMode.Visible = False
        CbxSftCpySbmttdEdtMode.Checked = False
        CbxHrdCpySbmttdEdtMode.Checked = False
        CbxDgiSbmttdEdtMode.Checked = False
        CbxRgaEfSbmttdEdtMode.Checked = False
        status = "Ongoing"
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
    '==============================END=====================================

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
    '==============================END=====================================

    'RADIO BUTTON PUBLISHED AND PRESENTED , HIDING AND SHOWING OF UI
    Dim isPublished As String = "NO"
    Dim isPresented As String = "NO"
    'published radio button
    Private Sub RdBtnPub_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtBtnPub.MouseClick
        If RdEdtBtnPub.Checked = True Then
            PnlPresented.Enabled = False
            PnlPresented.Height = 0
            PnlPublished.Enabled = True
            PnlPublished.Height = 230
            isPublished = "YES"
            isPresented = "NO"
            BtnEdtCnclSlctn.Visible = True
        Else
            isPublished = "NO"
        End If
    End Sub
    'presented radio button
    Private Sub RdBtnPresented_MouseClick(sender As Object, e As MouseEventArgs) Handles RdEdtBtnPresented.MouseClick
        If RdEdtBtnPresented.Checked = True Then
            PnlPublished.Height = 0
            PnlPresented.Enabled = True
            PnlPresented.Height = 230
            isPresented = "YES"
            isPublished = "NO"
            BtnEdtCnclSlctn.Visible = True
        Else
            isPresented = "NO"
        End If
    End Sub

    Private Sub BtnCancelSelection_Click(sender As Object, e As EventArgs) Handles BtnEdtCnclSlctn.Click

        isPublished = "NO"
        PnlPublished.Enabled = False
        RdEdtBtnPub.Checked = False

        isPresented = "NO"
        PnlPresented.Enabled = False
        RdEdtBtnPresented.Checked = False

        PnlPresented.Enabled = False
        PnlPresented.Height = 0
        PnlPublished.Height = 230

        BtnEdtCnclSlctn.Visible = False

        RdEdtBtnPresented.Checked = False
        RdEdtBtnPub.Checked = False

        RdPubLevelInsti.Checked = False
        RdPubLevelInter.Checked = False
        RdPubLevelLoc.Checked = False
        RdPubLevelNat.Checked = False

        RdEdtPreLevelInsti.Checked = False
        RdEdtPreLevelInter.Checked = False
        RdEdtPreLevelLoc.Checked = False
        RdEdtPreLevelNat.Checked = False
    End Sub




    '==============================END=====================================
    '|||||||||||||||||||||||||||||||||| END OF UI RESPONSE OR FUNCTIONALITES ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
End Class