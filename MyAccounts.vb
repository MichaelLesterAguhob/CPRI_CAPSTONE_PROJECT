Imports System.Net.Mail
Imports MySql.Data.MySqlClient
Imports System.Net.NetworkInformation

Public Class MyAccounts

    Dim original_pass As String = ""
    Dim pass_to_apply As String = ""
    Dim original_email As String = ""
    Dim email_to_apply As String = ""
    Dim Vcode As Integer = 0
    Private Sub MyAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LBL_UID.Text = loggedin_id
        LBL_UTYPE.Text = account_type_loggedin
        ConOpen()
        LoadMyAccountData()
        LblEmailMsg.Visible = False
    End Sub

    'LOADING ACCOUNT DATA
    Private Sub LoadMyAccountData()
        con.Close()

        Try
            con.Open()
            Dim query As String = "SELECT * FROM accounts WHERE user_id = @user_id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@user_id", loggedin_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    If reader.Read() Then
                        TXT_FNAME.Text = reader("full_name").ToString()
                        TXT_UNAME.Text = reader("username").ToString()
                        TXT_EMAIL.Text = reader("email").ToString()
                        TXT_PASS.Text = reader("password").ToString()
                        original_pass = reader("password").ToString()
                        original_email = reader("email").ToString()
                    End If
                Else
                    MessageBox.Show("Problem while loading account data.", "No account data loaded", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'reader.Close()
            End Using
            LblPassMsg.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error occurred while loading account data.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    'ENABLING FIELDS TO EDIT INFO
    Private Sub BTN_EDIT_Click(sender As Object, e As EventArgs) Handles BTN_EDIT.Click
        TXT_FNAME.ReadOnly = False
        TXT_UNAME.ReadOnly = False
        TXT_EMAIL.ReadOnly = False
        TXT_PASS.ReadOnly = False

        TXT_NEWPASS.ReadOnly = False
        TXT_NEWPASS.Visible = True
        Label8.Visible = True
        Label7.Visible = True

        BTN_UPDT.Enabled = True
        BTN_CANCEL.Enabled = True
    End Sub

    'DETERMINE IF PASSWORD IS MATCHED
    Function isPassMatched() As Boolean
        If TXT_PASS.Text.Trim = TXT_NEWPASS.Text.Trim Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub TXT_PASS_TextChanged(sender As Object, e As EventArgs) Handles TXT_PASS.TextChanged
        If TXT_PASS.Text.Trim <> "" Then
            If isPassMatched() Then
                LblPassMsg.Visible = True
                LblPassMsg.Text = "Password Matched!"
                LblPassMsg.ForeColor = Color.Green
            Else
                LblPassMsg.Visible = True
                LblPassMsg.Text = "Password Unmatched!"
                LblPassMsg.ForeColor = Color.Maroon
            End If
        End If
    End Sub

    Private Sub TXT_NEWPASS_TextChanged(sender As Object, e As EventArgs) Handles TXT_NEWPASS.TextChanged
        If TXT_NEWPASS.Text.Trim <> "" Then
            If isPassMatched() Then
                LblPassMsg.Visible = True
                LblPassMsg.Text = "Password Matched!"
                LblPassMsg.ForeColor = Color.Green
            Else
                LblPassMsg.Visible = True
                LblPassMsg.Text = "Password Unmatched!"
                LblPassMsg.ForeColor = Color.Maroon
            End If
        Else
            LblPassMsg.Visible = False
        End If
    End Sub

    'CHECK IF DEVICE HAS INTERNET
    Public Function IsInternetAvailable() As Boolean
        Try
            Dim pingSender As New Ping()
            Dim reply As PingReply = pingSender.Send("8.8.8.8")
            Return (reply.Status = IPStatus.Success)
        Catch ex As Exception
            Return False
        End Try
    End Function

    'GENERATE RANDOM NUMBER FOR VERIFICATION CODE
    Dim rand As New Random()
    Private Sub SendVcode()
        Vcode = rand.Next(10000, 99999)
        If IsInternetAvailable() Then
            Try
                BTN_ENTER_UPDT.Enabled = False
                Dim smtp_server As New SmtpClient
                smtp_server.UseDefaultCredentials = False
                smtp_server.Credentials = New Net.NetworkCredential("cdsga.cpri@gmail.com", "xmuc gwab jeua dxss")
                smtp_server.Port = 587
                smtp_server.EnableSsl = True
                smtp_server.Host = "smtp.gmail.com"

                Dim email As New MailMessage
                email = New MailMessage()
                email.From = New MailAddress("cdsga.cpri@gmail.com")
                email.To.Add(TXT_EMAIL.Text.Trim)
                email.Subject = "Verification Code"
                email.Body = "<span style='font-size: 25px; color: maroon;'>" & "CDSGA-CPRI" & "</span> <br><br>" & "Your account change email verification code : " & "<br> <span style='font-size: 35px;'>" & Vcode.ToString() & "</span>"
                email.IsBodyHtml = True

                smtp_server.Send(email)

                MessageBox.Show("Verification Code sent! Check your email and enter the code.", "Verification Code sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BTN_ENTER_UPDT.Enabled = True
                Panel1.Visible = True
                LblEmailMsg.Visible = False
                TXT_VCODE.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred while sending Verification Code", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Make sure you have an internet connection.", "No Internet Connection.. Try Again", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BTN_ENTER_UPDT.Enabled = True
            BTN_UPDT.Enabled = True
        End If
    End Sub

    'UPDATING ACCOUNT DETAILS
    Private Sub BTN_UPDT_Click(sender As Object, e As EventArgs) Handles BTN_UPDT.Click
        If TXT_FNAME.Text.Trim <> "" Or TXT_UNAME.Text.Trim <> "" Or TXT_EMAIL.Text.Trim <> "" Or TXT_PASS.Text.Trim <> "" Then
            If TXT_NEWPASS.Text.Trim <> "" Then
                If isPassMatched() Then
                    pass_to_apply = TXT_PASS.Text.Trim
                    If Not isEmailUnique(TXT_EMAIL.Text.Trim) Then
                        MessageBox.Show(" The Email you have entered was already used", "Already Used, Try Another", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        'means email has been changed
                        If TXT_EMAIL.Text.Trim <> original_email Then
                            LblEmailMsg.Visible = True
                            LblEmailMsg.Text = "Sending Verification code"
                            BTN_UPDT.Enabled = False
                            SendVcode()
                        Else
                            email_to_apply = original_email
                            UpdateMyAccountDetails("pass_changed")
                        End If

                    End If
                Else
                    MessageBox.Show("Password did not matched!", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                pass_to_apply = original_pass
                If Not isEmailUnique(TXT_EMAIL.Text.Trim) Then
                    MessageBox.Show(" The Email you have entered was already used", "Already Used, Try Another", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    'means email has been changed
                    If TXT_EMAIL.Text.Trim <> original_email Then
                        LblEmailMsg.Visible = True
                        LblEmailMsg.Text = "Sending Verification code"
                        BTN_UPDT.Enabled = False
                        SendVcode()
                    Else
                        email_to_apply = original_email
                        UpdateMyAccountDetails("pass_unchange")
                    End If

                End If
            End If
        Else
            MessageBox.Show("Fill in the blank(s)", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub UpdateMyAccountDetails(action)
        con.Close()
        con.Open()

        If action = "pass_changed" Then
            Try
                Dim update_query As String = "
                UPDATE accounts 
                SET 
                    full_name = @fname, 
                    username = @uname, 
                    email = @email, 
                    code_sent = @vcode, 
                    password = MD5('" & pass_to_apply & "')
                WHERE user_id = @id
"
                Using cmd As New MySqlCommand(update_query, con)
                    cmd.Parameters.AddWithValue("@fname", TXT_FNAME.Text.Trim)
                    cmd.Parameters.AddWithValue("@uname", TXT_UNAME.Text.Trim)
                    cmd.Parameters.AddWithValue("@email", email_to_apply)
                    cmd.Parameters.AddWithValue("@vcode", Vcode)
                    cmd.Parameters.AddWithValue("@id", loggedin_id)
                    cmd.ExecuteNonQuery()
                End Using
                LoadMyAccountData()
                TXT_FNAME.ReadOnly = True
                TXT_UNAME.ReadOnly = True
                TXT_EMAIL.ReadOnly = True
                TXT_PASS.ReadOnly = True

                TXT_NEWPASS.ReadOnly = True
                TXT_NEWPASS.Visible = False
                Label8.Visible = False
                Label7.Visible = False

                BTN_UPDT.Enabled = False
                BTN_CANCEL.Enabled = False
                LblPassMsg.Visible = False
                LblEmailMsg.Visible = False

                Panel1.Visible = False
                TXT_VCODE.Clear()

                MessageBox.Show("Successfully updated account. Other changes will take effect after logging out and then login.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error 01 occurred while updating account data.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        Else
            Try
                Dim update_query As String = "
                UPDATE accounts 
                SET 
                    full_name = @fname, 
                    username = @uname, 
                    email = @email, 
                    code_sent = @vcode, 
                    password = '" & pass_to_apply & "'
                WHERE user_id = @id
"
                Using cmd As New MySqlCommand(update_query, con)
                    cmd.Parameters.AddWithValue("@fname", TXT_FNAME.Text.Trim)
                    cmd.Parameters.AddWithValue("@uname", TXT_UNAME.Text.Trim)
                    cmd.Parameters.AddWithValue("@email", email_to_apply)
                    cmd.Parameters.AddWithValue("@vcode", Vcode)
                    cmd.Parameters.AddWithValue("@id", loggedin_id)
                    cmd.ExecuteNonQuery()
                End Using
                LoadMyAccountData()
                TXT_FNAME.ReadOnly = True
                TXT_UNAME.ReadOnly = True
                TXT_EMAIL.ReadOnly = True
                TXT_PASS.ReadOnly = True

                TXT_NEWPASS.ReadOnly = True
                TXT_NEWPASS.Visible = False
                Label8.Visible = False
                Label7.Visible = False

                BTN_UPDT.Enabled = False
                BTN_CANCEL.Enabled = False
                LblPassMsg.Visible = False
                LblEmailMsg.Visible = False

                Panel1.Visible = False
                TXT_VCODE.Clear()
                MessageBox.Show("Successfully updated account. Other changes will take effect after logging out and then login.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error 02 occurred while updating account data.", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        End If

    End Sub

    Private Sub BTN_CLOSE_Click(sender As Object, e As EventArgs) Handles BTN_CLOSE.Click
        Me.Close()
    End Sub

    Private Sub BTN_CANCEL_Click(sender As Object, e As EventArgs) Handles BTN_CANCEL.Click
        TXT_FNAME.ReadOnly = True
        TXT_UNAME.ReadOnly = True
        TXT_EMAIL.ReadOnly = True
        TXT_PASS.ReadOnly = True

        TXT_NEWPASS.ReadOnly = True
        TXT_NEWPASS.Visible = False
        TXT_NEWPASS.Clear()
        Label8.Visible = False
        Label7.Visible = False

        BTN_UPDT.Enabled = False
        BTN_CANCEL.Enabled = False
        LblPassMsg.Visible = False
        LblEmailMsg.Visible = False

        Panel1.Visible = False
        TXT_VCODE.Clear()
        LoadMyAccountData()
    End Sub

    Private Sub TXT_EMAIL_TextChanged(sender As Object, e As EventArgs) Handles TXT_EMAIL.TextChanged
        If isEmailValid(TXT_EMAIL.Text.Trim) Then
            LblEmailMsg.Text = "Valid Email"
            LblEmailMsg.ForeColor = Color.Green
            LblEmailMsg.Visible = True

        Else
            LblEmailMsg.ForeColor = Color.Maroon
            LblEmailMsg.Text = "Invalid Email"
            LblEmailMsg.Visible = True
        End If
    End Sub

    Function isEmailValid(email) As Boolean
        Try
            Dim address As New MailAddress(email)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function isEmailUnique(email) As Boolean
        con.Close()
        Dim ret_val As Boolean
        Try
            con.Open()
            Dim query As String = "SELECT email FROM accounts WHERE email=@email AND user_id != @user_id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@user_id", loggedin_id)
                Dim reader As MySqlDataReader = cmd.ExecuteReader
                If reader.HasRows Then
                    ret_val = False
                Else
                    ret_val = True
                End If
                reader.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error occurred while checking email uniqueness.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
        Return ret_val
    End Function

    Private Sub BTN_ENTER_UPDT_Click(sender As Object, e As EventArgs) Handles BTN_ENTER_UPDT.Click
        If Val(TXT_VCODE.Text.Trim) = Vcode Then
            email_to_apply = TXT_EMAIL.Text.Trim
            If TXT_PASS.Text.Trim <> "" And TXT_NEWPASS.Text.Trim <> "" Then
                UpdateMyAccountDetails("pass_changed")
            Else
                UpdateMyAccountDetails("pass_unchange")
            End If
        Else
            MessageBox.Show("Incorrect Verification Code", "Try again.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub TXT_VCODE_TextChanged(sender As Object, e As EventArgs) Handles TXT_VCODE.TextChanged
        If TXT_VCODE.Text.Trim <> "" Then
            If Not IsNumeric(TXT_VCODE.Text.Trim) Then
                MessageBox.Show("You have entered a non-numeric character!", "Invalid input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TXT_VCODE.Clear()
            End If
        End If

    End Sub

    Private Sub TXT_VCODE_KeyDown(sender As Object, e As KeyEventArgs) Handles TXT_VCODE.KeyDown
        If e.KeyCode = 13 Then
            BTN_ENTER_UPDT.PerformClick()
        End If
    End Sub
End Class