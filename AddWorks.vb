
Imports System.Windows.Forms

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

    Dim cntr As String
    Dim total_fields As String
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax + 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim taax As Integer = TxtAddAuthX.Text
        TxtAddAuthX.Text = taax - 1
    End Sub

    Private Sub TxtAddAuthX_TextChanged(sender As Object, e As EventArgs) Handles TxtAddAuthX.TextChanged
        Dim field_count As String = TxtAddAuthX.Text
        If field_count = "" Then
            TxtAddAuthX.Text = "1"
        ElseIf field_count < 1 Then
            TxtAddAuthX.Text = "1"
        ElseIf field_count > 50 Then
            TxtAddAuthX.Text = "50"
        End If
    End Sub

    Private Sub RdStatCmpltd_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatCmpltd.MouseClick
        If RdStatCmpltd.Checked = True Then
            PnlStatCmpltd.Visible = True
        End If
    End Sub

    Private Sub RdStatOngng_MouseClick(sender As Object, e As MouseEventArgs) Handles RdStatOngng.MouseClick
        PnlStatCmpltd.Visible = False
    End Sub

    Private Sub CbxSftCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxSftCpySbmttd.CheckedChanged
        If CbxSftCpySbmttd.Checked = True Then
            DtSftCpySbmttdDate.Visible = True
        Else
            DtSftCpySbmttdDate.Visible = False
        End If
        PrintThesisClearance()
    End Sub

    Private Sub CbxHrdCpySbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxHrdCpySbmttd.CheckedChanged
        If CbxHrdCpySbmttd.Checked = True Then
            DtHrdCpySbmttdDate.Visible = True
        Else
            DtHrdCpySbmttdDate.Visible = False
        End If
        PrintThesisClearance()
    End Sub

    Private Sub CbxDgiSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxDgiSbmttd.CheckedChanged
        If CbxDgiSbmttd.Checked = True Then
            DtDgiSbmttdDate.Visible = True
        Else
            DtDgiSbmttdDate.Visible = False
        End If
        PrintThesisClearance()
    End Sub

    Private Sub CbxRgaEfSbmttd_CheckedChanged(sender As Object, e As EventArgs) Handles CbxRgaEfSbmttd.CheckedChanged
        If CbxRgaEfSbmttd.Checked = True Then
            DtRgaSbmttdDate.Visible = True
        Else
            DtRgaSbmttdDate.Visible = False
        End If
        PrintThesisClearance()
    End Sub

    Public Sub PrintThesisClearance()
        If CbxSftCpySbmttd.Checked = True And CbxHrdCpySbmttd.Checked = True And CbxDgiSbmttd.Checked = True And CbxRgaEfSbmttd.Checked = True Then
            BtnThssClrnc.Enabled = True
            BtnThssClrnc.BackColor = Color.LimeGreen
        Else
            BtnThssClrnc.Enabled = False
            BtnThssClrnc.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub AddWorks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TxtAddAuthX.Text = "6"
        BtnAddNewCoAuthor.PerformClick()

    End Sub
End Class