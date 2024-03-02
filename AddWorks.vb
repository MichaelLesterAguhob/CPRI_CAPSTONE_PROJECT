
Imports System.Windows.Forms
Imports System.IO
Imports MySql.Data.MySqlClient
Imports PdfiumViewer
Public Class AddWorks


    'ADDING NEW TEXTBOX COMPONENT 
    Dim auth_count As Integer = 0

    Public Sub AddNewAuthField()
        auth_count += 1

        'CREATING LABEL ELEMENTS AND CONFIGURE ITS PROPERTIES
        Dim lbl_auth As New Label()
        lbl_auth.Text = "Co-Author " & auth_count.ToString & " :"
        lbl_auth.AutoSize = True
        lbl_auth.Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)

        Dim lbl_auth_deg As New Label()
        lbl_auth_deg.Text = "Degree Program :"
        lbl_auth_deg.AutoSize = True
        lbl_auth_deg.Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)

        Dim lbl_auth_role As New Label()
        lbl_auth_role.Text = "Role :"
        lbl_auth_role.AutoSize = True
        lbl_auth_role.Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Regular)


        'CREATING TEXTBOX ELEMENTSD, CONFIGURE ITS PROPERTIES
        Dim new_co_auth As New TextBox()
        new_co_auth.Name = "CoAuthor" & auth_count
        new_co_auth.Size = New System.Drawing.Size(379, 22)
        new_co_auth.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        new_co_auth.BackColor = Color.WhiteSmoke

        Dim new_co_auth_deg As New TextBox()
        new_co_auth_deg.Name = "CoAuthorDeg" & auth_count
        new_co_auth_deg.Size = New System.Drawing.Size(424, 22)
        new_co_auth_deg.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        new_co_auth_deg.BackColor = Color.WhiteSmoke

        Dim new_co_auth_role As New TextBox()
        new_co_auth_role.Name = "CoAuthorRole" & auth_count
        new_co_auth_role.Size = New System.Drawing.Size(160, 22)
        new_co_auth_role.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        new_co_auth_role.BackColor = Color.WhiteSmoke


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

    Dim cntr As String ' HOLDER OF NUMBER THAT SYSTEM WILL GENERATE TEXTBOX COMPONENT FOR AUTHOR
    Dim total_fields As String 'VAR HOLDER OF TOTAL FIELD OF EXISTING CO-AUHTOR PRINTED IN LABEL
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
                LblTotalFields.Text = total_fields.ToString
            End While
            BtnAddNewCoAuthor.Enabled = True
            TxtAddAuthX.Text = "1"
            Dim co_author_dynamic_name As String = "CoAuthor" & auth_count.ToString()
            Dim co_auth_field_name As TextBox = CType(Me.Controls.Find(co_author_dynamic_name, True).FirstOrDefault(), TextBox)
            co_auth_field_name.Focus()

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
    '=======================================

    'RESTRICTING TEXTBOX FOR TOTAL COUNT FOR NEW CO-AUTHOR FIELDS
    'FIELDS IS NOT ALLOWED TO BE BLANK, 0, GREATER THAN 50,
    Private Sub TxtAddAuthX_TextChanged(sender As Object, e As EventArgs) Handles TxtAddAuthX.TextChanged
        Dim field_count As String = TxtAddAuthX.Text.Trim
        If IsNumeric(field_count) Then
            If field_count = "" Then
                TxtAddAuthX.Text = "1"
            ElseIf field_count < 1 Then
                TxtAddAuthX.Text = "1"
            ElseIf field_count > 50 Then
                TxtAddAuthX.Text = "50"
            End If
        Else
            MessageBox.Show("Invalid!")
            TxtAddAuthX.Text = "1"
        End If

    End Sub

    'ONGOING OR COMPLETED EVENT HANDLES. SHOW OR HIDE 4 CHECKBOXES PANEL
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
    '===================================================================

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
    '============================================================

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
    '====================================================================

    'FUNCTIONS WHEN ADDWORKS FORM IS LOAD
    Private Sub AddWorks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen() 'trying to connect to database to determine if connection is ready

        'generate 6 fields for new co-author
        TxtAddAuthX.Text = "6"
        BtnAddNewCoAuthor.PerformClick()
    End Sub


    'OPENING FILE DIALOG TO LET USER FIND AND SELECT THE PDF OR WORD FILES
    Dim abstract_file_path As String
    Dim abstract_file_data As Byte()

    Private Sub BtnBrowseAbstractFile_Click(sender As Object, e As EventArgs) Handles BtnBrowseAbstractFile.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "All Files (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx"
        openFileDialog.InitialDirectory = "C:\"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            abstract_file_path = openFileDialog.FileName
            abstract_file_data = File.ReadAllBytes(abstract_file_path)
        End If
    End Sub

    Private Sub BtnSaveResearch_Click(sender As Object, e As EventArgs) Handles BtnSaveResearch.Click

        con.Close()

        Try
            con.Open()

            Dim query As String = "INSERT INTO `sw_abstract` (`no#`, `abstract_id`, `file_name`, `file_data`, `file_type`) 
                                 VALUES(@no, @id, @filename, @filedata, @filetype)"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@no", 0)
                cmd.Parameters.AddWithValue("@id", 123)
                cmd.Parameters.AddWithValue("@filename", Path.GetFileName(abstract_file_path))
                cmd.Parameters.AddWithValue("@filedata", abstract_file_data)
                cmd.Parameters.AddWithValue("@filetype", "pdf")
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("File saved to database successfully.")
            cmd.Parameters.Clear()
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            con.Close()
        End Try

    End Sub

    Private Sub PreviewFileButton_Click(sender As Object, e As EventArgs) Handles PreviewFileButton.Click
        pdfId = 1234

        Dim pdf_viewer As New pdf_viewer
        pdf_viewer.Show()

    End Sub


End Class