Imports System.Net.Mail
Imports MySql.Data.MySqlClient
Imports System.Net.NetworkInformation

Public Class CreateLoginAccount

    Dim date_now As DateTime = DateTime.Now
    Dim current_year As Integer = date_now.Year

    Private Shared ReadOnly rnd As New Random()
    Dim initial_account_id As Integer = 0
    Dim final_user_id As Integer = 0
    Dim verification_code As Integer = 0

    '===========CREATE ACCOUNT
    'OPEN CREATE ACCOUNT FORM
    Private Sub BtnGoToCreateAcct_Click(sender As Object, e As EventArgs) Handles BtnGoToCreateAcct.Click
        GbxCreate.Visible = True
        GbxLogin.Visible = False
    End Sub

    'GENERATE UNIQUE ACCOUNT ID
    Private Sub GenerateAccountID()
        initial_account_id = rnd.Next(10000, 99999)
        IsInitialIDUnique()
    End Sub
    Private Sub IsInitialIDUnique()
        con.Close()
        Try
            con.Open()
            Dim query As String = "SELECT user_id FROM accounts WHERE user_id=@id"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", current_year.ToString & initial_account_id)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    GenerateAccountID()
                Else
                    final_user_id = current_year.ToString & initial_account_id
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to check the uniqueness of generated Account ID")
        Finally
            con.Close()
        End Try
    End Sub

    'FORM LOAD
    Private Sub CreateLoginAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConOpen()
        If loggedin <= 0 Then
            BtnGoToStudentViewing.Visible = True
        Else
            BtnGoToStudentViewing.Visible = False
        End If
    End Sub

    'CHECKING IF USERNAME OF EMAIL ALREADY EXISTS
    Function IsEmailUsernameExists() As Boolean
        con.Close()
        Dim exists As Boolean
        Try
            con.Open()
            Using cmd As New MySqlCommand("SELECT * FROM accounts WHERE `username`=@username OR `email`=@email", con)
                cmd.Parameters.AddWithValue("@username", TxtUsernameCreate.Text.Trim)
                cmd.Parameters.AddWithValue("@email", TxtEmailCreate.Text.Trim)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    exists = True
                Else
                    exists = False
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Checking Email and Username Existence Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
        Return exists
    End Function

    Function IsEmailValid(email As String) As Boolean
        Dim email_validity As Boolean
        Try
            Dim address As New MailAddress(email)
            email_validity = True
            Label25.Visible = False
        Catch ex As Exception
            email_validity = False
            Label25.Visible = True
        End Try

        Return email_validity
    End Function

    'CREATE ACCOUNT
    Private Sub BtnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        If TxtNameCreate.Text.Trim <> "" Or TxtUsernameCreate.Text.Trim <> "" Or TxtEmailCreate.Text.Trim <> "" Or TxtPasswordCreate.Text.Trim <> "" Or TxtConfirmPassCreate.Text.Trim <> "" Then

            If IsEmailValid(TxtEmailCreate.Text.Trim) Then
                If IsEmailUsernameExists() Then
                    MessageBox.Show("Email or Username Already Exists", "Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If TxtPasswordCreate.Text.Trim = TxtConfirmPassCreate.Text.Trim Then
                        GenerateVcode()
                        BtnCreate.Enabled = False
                        SendVerificationCode()
                    Else
                        MessageBox.Show("Password did not match!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            Else
                MessageBox.Show("Enter valid email", "Invalid Email!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("Fill in the blank(s)", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'CREATE ACCOUNT
    Dim type As String = ""
    Private Sub CreateAccount()
        con.Close()
        Try
            con.Open()

            Dim id As Integer = final_user_id
            Dim fname As String = TxtNameCreate.Text.Trim
            Dim uname As String = TxtUsernameCreate.Text.Trim
            Dim email As String = TxtEmailCreate.Text.Trim
            Dim pass As String = TxtPasswordCreate.Text.Trim
            Dim code_sent As String = Convert.ToInt32(TxtVerificationCreate.Text.Trim)

            If RdTypeAdmin.Checked = True Then
                type = "admin"
            Else
                type = "staff"
            End If

            Dim query As String = "
                    INSERT INTO accounts 
                        (`user_id`,`full_name`,`username`,`email`,`password`,`account_type`,`code_sent`)
                    VALUES
                        (@id, @fullname, @uname, @email, MD5('" & pass & "'), @acct_type, @code_sent)
                    "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@fullname", fname)
                cmd.Parameters.AddWithValue("@uname", uname)
                cmd.Parameters.AddWithValue("@email", email)
                cmd.Parameters.AddWithValue("@acct_type", type)
                cmd.Parameters.AddWithValue("@code_sent", code_sent)
                cmd.ExecuteNonQuery()
            End Using
            MessageBox.Show("Created Successfully")
            LblVcodeMsg.Text = ""
            TxtNameCreate.Clear()
            TxtUsernameCreate.Clear()
            TxtEmailCreate.Clear()
            TxtPasswordCreate.Clear()
            TxtConfirmPassCreate.Clear()
            TxtVerificationCreate.Clear()
            RdTypeAdmin.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Creating Account Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'SENDING VERIFICATION CODE
    Public Function IsInternetAvailable() As Boolean
        Try
            Dim pingSender As New Ping()
            Dim reply As PingReply = pingSender.Send("8.8.8.8")
            Return (reply.Status = IPStatus.Success)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub GenerateVcode()
        verification_code = rnd.Next(10000, 99999)
    End Sub
    Private Sub SendVerificationCode()
        If IsInternetAvailable() Then
            Try
                Label24.Visible = True
                Dim smtp_server As New SmtpClient
                smtp_server.UseDefaultCredentials = False
                smtp_server.Credentials = New Net.NetworkCredential("cdsga.cpri@gmail.com", "xmuc gwab jeua dxss")
                smtp_server.Port = 587
                smtp_server.EnableSsl = True
                smtp_server.Host = "smtp.gmail.com"

                Dim email As New MailMessage
                email = New MailMessage()
                email.From = New MailAddress("cdsga.cpri@gmail.com")
                email.To.Add(TxtEmailCreate.Text.Trim)
                email.Subject = "Account Verification Code"
                email.Body = "<span style='font-size: 25px; color: maroon;'>" & "CDSGA-CPRI" & "</span> <br><br>" & "Create Account Verification Code : " & "<br> <span style='font-size: 35px;'>" & verification_code.ToString() & "</span>"
                email.IsBodyHtml = True

                smtp_server.Send(email)
                Label24.Visible = False
                MessageBox.Show("Verification code sent! Check your email and enter code here.")
                BtnCreate.Enabled = True
                PnlVCode.Visible = True
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred while sending verification code", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Make sure you have an internet connection.", "No Internet Connection..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnCreate.Enabled = True
        End If
    End Sub

    'check verification code
    Private Sub TxtVerificationCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtVerificationCreate.KeyDown
        If e.KeyCode = 13 Then
            BtnEnterVCode.PerformClick()
        End If
    End Sub

    Private Sub BtnEnterVCode_Click(sender As Object, e As EventArgs) Handles BtnEnterVCode.Click
        If Convert.ToInt32(TxtVerificationCreate.Text.Trim) = verification_code Then
            GenerateAccountID()
            LblVcodeMsg.Text = "Valid code"
            LblVcodeMsg.ForeColor = Color.Green
            CreateAccount()
            PnlVCode.Visible = False
            GbxCreate.Visible = False
            GbxLogin.Visible = True
        Else
            LblVcodeMsg.Text = "Invalid Code!"
            LblVcodeMsg.ForeColor = Color.Red
        End If

    End Sub

    'btn close vcode panel
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim confirmation As DialogResult = MessageBox.Show("Close Verification Code Input?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
            PnlVCode.Visible = False
        End If

    End Sub

    Private Sub TxtNameCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtNameCreate.KeyDown
        If e.KeyCode = 13 Then
            If TxtNameCreate.Text.Trim = "" Then
                MessageBox.Show("Enter your Fullname!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtUsernameCreate.Focus()
            End If
        End If
    End Sub

    Private Sub TxtUsernameCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUsernameCreate.KeyDown
        If e.KeyCode = 13 Then
            If TxtUsernameCreate.Text.Trim = "" Then
                MessageBox.Show("Enter your desired Username!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtEmailCreate.Focus()
            End If
        End If
    End Sub

    Private Sub TxtEmailCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtEmailCreate.KeyDown
        If e.KeyCode = 13 Then
            If TxtEmailCreate.Text.Trim = "" Then
                MessageBox.Show("Enter your Email!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtPasswordCreate.Focus()
            End If
        End If
    End Sub

    Private Sub TxtPasswordCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPasswordCreate.KeyDown
        If e.KeyCode = 13 Then
            If TxtPasswordCreate.Text.Trim = "" Then
                MessageBox.Show("Enter your desired Password!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtConfirmPassCreate.Focus()
            End If
        End If
    End Sub

    Private Sub TxtConfirmPassCreate_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtConfirmPassCreate.KeyDown
        If e.KeyCode = 13 Then
            If TxtConfirmPassCreate.Text.Trim = "" Then
                MessageBox.Show("Re-enter your Password!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                BtnCreate.PerformClick()
            End If
        End If
    End Sub






    ' ================= ACCOUNT LOGIN

    'OPEN LOGIN FORM
    Private Sub BtnGoToLogin_Click(sender As Object, e As EventArgs) Handles BtnGoToLogin.Click
        GbxCreate.Visible = False
        GbxLogin.Visible = True
    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If TxtUnameEmailLogin.Text.Trim <> "" And TxtPassLogin.Text.Trim <> "" Then
            Login()
        Else
            LblLoginMsg.Text = "Fill in the Blank(s)"
            LblLoginMsg.Visible = True
            LblLoginMsg.ForeColor = Color.DarkOrange
            timer_fade_out_msg.Start()
        End If
    End Sub

    ' FUNCTION TO LOGIN TO THE SYSTEM
    Private Sub Login()
        con.Close()
        Dim uname_email As String = TxtUnameEmailLogin.Text.Trim
        Dim login_pass As String = TxtPassLogin.Text.Trim
        Try
            con.Open()
            Dim query As String = "
            SELECT 
                *
            FROM accounts 
            WHERE 
                password = MD5('" & login_pass & "')
                AND (`username` = @uname_email OR `email` = @uname_email)
                
                "
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@uname_email", uname_email)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then

                    If reader.Read() Then
                        account_loggedin = reader("full_name").ToString
                        account_type_loggedin = reader("account_type").ToString
                        loggedin = 1

                        LblLoginMsg.Text = "Login Successfully please wait.."
                        LblLoginMsg.Visible = True
                        LblLoginMsg.ForeColor = Color.Green
                        ' MsgBox(account_loggedin & " -- " & account_type_loggedin)
                        timer_fade_out_msg.Start()
                        TxtUnameEmailLogin.Clear()
                        TxtPassLogin.Clear()
                    End If

                Else
                    LblLoginMsg.Text = "Incorrect Username/Email OR Password"
                    LblLoginMsg.Visible = True
                    LblLoginMsg.ForeColor = Color.Red
                    timer_fade_out_msg.Start()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub timer_fade_out_msg_Tick(sender As Object, e As EventArgs) Handles timer_fade_out_msg.Tick
        LblLoginMsg.Visible = False

        If loggedin <> 0 Then
            If account_type_loggedin = "admin" Then
                Dim main_form As New Form1
                main_form.Show()
                Me.Hide()
            Else account_type_loggedin = "staff"
                Dim brrwng_rtrnng As New BorrowingAndReturning
                brrwng_rtrnng.Show()
                Me.Hide()
            End If
        End If

        timer_fade_out_msg.Stop()
    End Sub

    Private Sub TxtUnameEmailLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUnameEmailLogin.KeyDown
        If e.KeyCode = 13 Then
            If TxtUnameEmailLogin.Text.Trim = "" Then
                MessageBox.Show("Enter Username or Email!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtPassLogin.Focus()
            End If
        End If
    End Sub

    Private Sub TxtPassLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPassLogin.KeyDown
        If e.KeyCode = 13 Then
            If TxtPassLogin.Text.Trim = "" Then
                MessageBox.Show("Enter your Password!", "No Input.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                BtnLogin.PerformClick()
                TxtPassLogin.Focus()
            End If
        End If
    End Sub

    Private Sub TxtEmailCreate_TextChanged(sender As Object, e As EventArgs) Handles TxtEmailCreate.TextChanged
        If TxtEmailCreate.Text.Trim <> "" Then
            IsEmailValid(TxtEmailCreate.Text.Trim)
        Else
            Label25.Visible = False
        End If

    End Sub


    'FORGOT PASSWORD
    Private Sub BtnOpenResetPassPanel_Click(sender As Object, e As EventArgs) Handles BtnOpenResetPassPanel.Click
        PnlForgotPass.Visible = True
        PnlFpEnterEmail.Width = 332
        TxtFpEmail.Focus()
    End Sub


    Dim isFpEmailValid As Boolean
    Private Sub TxtFpEmail_TextChanged(sender As Object, e As EventArgs) Handles TxtFpEmail.TextChanged
        If TxtFpEmail.Text.Trim <> "" Then
            'chcking email format
            Try
                Dim email As New MailAddress(TxtFpEmail.Text.Trim)
                TxtFpEmail.ForeColor = Color.Black
                isFpEmailValid = True
                Label29.Visible = False
            Catch ex As Exception
                TxtFpEmail.ForeColor = Color.Red
                isFpEmailValid = False
                Label29.Visible = True
                Label29.Text = "Invalid email"
                Label29.ForeColor = Color.Red
            End Try
        End If
    End Sub

    Dim reset_code As Integer = 0
    Private Sub BtnFpSendCode_Click(sender As Object, e As EventArgs) Handles BtnFpSendCode.Click
        If isFpEmailValid And TxtFpEmail.Text.Trim <> "" Then
            'checking if email is in the database
            con.Close()
            Try
                con.Open()
                Using cmd As New MySqlCommand("SELECT * FROM accounts WHERE `email`=@email", con)
                    cmd.Parameters.AddWithValue("@email", TxtFpEmail.Text.Trim)
                    Dim reader As MySqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        BtnFpSendCode.Enabled = False
                        SendResetCode()
                    Else
                        MessageBox.Show("The email you've entered do not exists in the system", "Email not exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    reader.Close()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Checking email existence for forgot password", MessageBoxButtons.OK, MessageBoxIcon.Error)
                con.Close()
            Finally
                con.Close()
            End Try
        Else
            MessageBox.Show("Invalid email address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    'GENERATE RESET CODE
    Private Sub GenerateResetCode()
        reset_code = rnd.Next(10000, 99999)
    End Sub

    'SEND RESET CODE TO EMAIL 
    Private Sub SendResetCode()
        If IsInternetAvailable() Then
            Try
                GenerateResetCode()

                Dim smtp_server As New SmtpClient
                smtp_server.UseDefaultCredentials = False
                smtp_server.Credentials = New Net.NetworkCredential("cdsga.cpri@gmail.com", "xmuc gwab jeua dxss")
                smtp_server.Port = 587
                smtp_server.EnableSsl = True
                smtp_server.Host = "smtp.gmail.com"

                Dim email As New MailMessage
                email = New MailMessage()
                email.From = New MailAddress("cdsga.cpri@gmail.com")
                email.To.Add(TxtFpEmail.Text.Trim)
                email.Subject = "Your account reset code"
                email.Body = "<span style='font-size: 25px; color: maroon;'>" & "CDSGA-CPRI" & "</span> <br><br>" & "Your Account Reset Code : " & "<br> <span style='font-size: 35px;'>" & reset_code.ToString() & "</span>"
                email.IsBodyHtml = True

                smtp_server.Send(email)

                MessageBox.Show("Reset code sent! Check your email and enter code here.", "Reset Code sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnFpSendCode.Enabled = False
                PnlFpEnterEmail.Width = 0
                PnlFpCode.Width = 332
                TxtFpRecCode.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Occurred while sending reset code", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Make sure you have an internet connection.", "No Internet Connection..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnFpSendCode.Enabled = True
        End If
    End Sub

    'CHECKING INPUT RESET CODE
    Private Sub TxtFpRecCode_TextChanged(sender As Object, e As EventArgs) Handles TxtFpRecCode.TextChanged
        If TxtFpRecCode.Text <> "" Then
            If IsNumeric(TxtFpRecCode.Text.Trim) Then
                If Convert.ToInt32(TxtFpRecCode.Text.Trim) = reset_code Then
                    Label27.Visible = True
                    Label27.Text = "Valid reset code"
                    Label27.ForeColor = Color.Green
                Else
                    Label27.Visible = True
                    Label27.Text = "Invalid reset code"
                    Label27.ForeColor = Color.Red
                End If
            Else
                MessageBox.Show("Enter number only!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtFpRecCode.Text = ""
            End If
        Else
            Label27.Visible = True
            Label27.Text = "Enter reset code"
            Label27.ForeColor = Color.Blue
        End If
    End Sub

    Private Sub TxtFpRecCode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFpRecCode.KeyDown
        If e.KeyCode = 13 And TxtFpRecCode.Text <> "" Then
            BtnFpEnter.PerformClick()
        End If
    End Sub

    'BUTTON ENTER RESET CODE 
    Dim email_to_change_pass As String = ""
    Private Sub BtnFpEnter_Click(sender As Object, e As EventArgs) Handles BtnFpEnter.Click
        If TxtFpRecCode.Text.Trim <> "" Then
            If reset_code = Convert.ToInt32(TxtFpRecCode.Text.Trim) Then
                PnlFpCode.Width = 0
                PnlFpNewPass.Width = 332
                email_to_change_pass = TxtFpEmail.Text.Trim
                TxtFpNewPass.Focus()
            Else
                MessageBox.Show("Incorrect reset code", "Try again!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

    End Sub

    Private Sub TxtFpEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFpEmail.KeyDown
        If e.KeyCode = 13 And TxtFpEmail.Text <> "" Then
            BtnFpSendCode.PerformClick()
            Label29.Visible = False
        Else
            Label29.Visible = True
            Label29.Text = "Enter email"
            Label29.ForeColor = Color.Blue
        End If
    End Sub

    Private Sub TxtFpNewPass_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFpNewPass.KeyDown
        If e.KeyCode = 13 Then
            If TxtFpNewPass.Text.Trim = "" Then
                MessageBox.Show("Enter your desired New Password", "No Input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                TxtFpConfirmNewPass.Focus()
            End If
        End If
    End Sub

    Private Sub TxtFpConfirmNewPass_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFpConfirmNewPass.KeyDown
        If e.KeyCode = 13 Then
            If TxtFpConfirmNewPass.Text.Trim = "" Then
                MessageBox.Show("Re-Enter your desired New Password", "No Input!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                BtnResetPass.PerformClick()
            End If
        End If

    End Sub

    'RESETTING PASS
    Private Sub TxtFpNewPass_TextChanged(sender As Object, e As EventArgs) Handles TxtFpNewPass.TextChanged
        If TxtFpNewPass.Text.Trim <> "" And TxtFpConfirmNewPass.Text.Trim <> "" Then
            If TxtFpNewPass.Text.Trim = TxtFpConfirmNewPass.Text.Trim Then
                Label28.Text = "Password matched"
                Label28.ForeColor = Color.Green
            Else
                Label28.Text = "Password not matched"
                Label28.ForeColor = Color.Red
            End If
        Else
            Label28.Text = ""
        End If
    End Sub

    Private Sub TxtFpConfirmNewPass_TextChanged(sender As Object, e As EventArgs) Handles TxtFpConfirmNewPass.TextChanged
        If TxtFpNewPass.Text.Trim <> "" And TxtFpConfirmNewPass.Text.Trim <> "" Then
            If TxtFpNewPass.Text.Trim = TxtFpConfirmNewPass.Text.Trim Then
                Label28.Text = "Password matched"
                Label28.ForeColor = Color.Green
            Else
                Label28.Text = "Password not matched"
                Label28.ForeColor = Color.Red
            End If
        Else
            Label28.Text = ""
        End If
    End Sub

    Private Sub BtnResetPass_Click(sender As Object, e As EventArgs) Handles BtnResetPass.Click
        If TxtFpNewPass.Text.Trim <> "" And TxtFpConfirmNewPass.Text.Trim <> "" Then
            If TxtFpNewPass.Text.Trim = TxtFpConfirmNewPass.Text.Trim Then
                ResetPassword()
                Label28.Text = ""
            End If
        Else
            Label28.Text = ""
        End If
    End Sub

    Private Sub ResetPassword()
        con.Close()
        Try
            con.Open()
            Using cmd As New MySqlCommand("UPDATE `accounts` SET `password`= MD5('" & TxtFpNewPass.Text.Trim & "') WHERE `email`=@email", con)
                cmd.Parameters.AddWithValue("@email", email_to_change_pass)
                cmd.ExecuteNonQuery()
            End Using
            PnlForgotPass.Visible = False

            PnlFpEnterEmail.Width = 0
            TxtFpEmail.Clear()

            PnlFpCode.Width = 0
            TxtFpRecCode.Clear()
            Label27.Visible = False

            PnlFpNewPass.Width = 0
            TxtFpNewPass.Clear()
            TxtFpConfirmNewPass.Clear()
            Label28.Text = ""

            TxtUnameEmailLogin.Focus()
            MessageBox.Show("You can now login to your account", "Password reset Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error while Resetting Password", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub BtnClosePnlForgotPass_Click(sender As Object, e As EventArgs) Handles BtnClosePnlForgotPass.Click
        PnlForgotPass.Visible = False

        PnlFpEnterEmail.Width = 0
        TxtFpEmail.Clear()

        PnlFpCode.Width = 0
        TxtFpRecCode.Clear()
        Label27.Visible = False

        PnlFpNewPass.Width = 0
        TxtFpNewPass.Clear()
        TxtFpConfirmNewPass.Clear()
        Label28.Text = ""

        TxtUnameEmailLogin.Focus()
    End Sub

    Private Sub BtnGoToStudentViewing_Click(sender As Object, e As EventArgs) Handles BtnGoToStudentViewing.Click
        Dim st As New StudentTerminal
        st.Show()
        Me.Hide()
    End Sub
End Class