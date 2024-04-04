Imports System.Net.Mail
Imports MySql.Data.MySqlClient

Public Class CreateLoginAccount

    Dim date_now As DateTime = DateTime.Now
    Dim current_year As Integer = date_now.Year

    Private Shared ReadOnly rnd As New Random()
    Dim initial_account_id As Integer = 0
    Dim final_user_id As Integer = 0
    Dim verification_code As Integer = 0

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
    End Sub

    'CREATE ACCOUNT
    Private Sub BtnCreate_Click(sender As Object, e As EventArgs) Handles BtnCreate.Click
        If TxtNameCreate.Text.Trim <> "" Or TxtUsernameCreate.Text.Trim <> "" Or TxtEmailCreate.Text.Trim <> "" Or TxtPasswordCreate.Text.Trim <> "" Or TxtConfirmPassCreate.Text.Trim <> "" Then
            If TxtPasswordCreate.Text.Trim = TxtConfirmPassCreate.Text.Trim Then
                GenerateVcode()
                SendVerificationCode()
                PnlVCode.Visible = True
            Else
                MessageBox.Show("Password did not match!", "Try Again!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("Fill in the blank(s)", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

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

            TxtNameCreate.Clear()
            TxtUsernameCreate.Clear()
            TxtEmailCreate.Clear()
            TxtPasswordCreate.Clear()
            TxtConfirmPassCreate.Clear()
            TxtVerificationCreate.Clear()
            RdTypeAdmin.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred while Creating Account", MessageBoxButtons.OK, MessageBoxIcon.Error)
            con.Close()
        Finally
            con.Close()
        End Try
    End Sub

    'SENDING VERIFICATION CODE
    Private Sub GenerateVcode()
        verification_code = rnd.Next(10000, 99999)
    End Sub
    Private Sub SendVerificationCode()
        Try
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
            email.Body = "CDSGA.CPRI | This is your Verification Code." & verification_code.ToString()
            email.IsBodyHtml = False
            smtp_server.Send(email)
            MessageBox.Show("Verification code sent!")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Occurred while sending verification code", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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
            LblVcodeMsg.Text = "Correct!"
            LblVcodeMsg.ForeColor = Color.Green
            CreateAccount()
            PnlVCode.Visible = False
        Else
            LblVcodeMsg.Text = "Incorrect!"
            LblVcodeMsg.ForeColor = Color.Red
        End If

    End Sub

    'btn close vcode panel
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PnlVCode.Visible = False
    End Sub


End Class