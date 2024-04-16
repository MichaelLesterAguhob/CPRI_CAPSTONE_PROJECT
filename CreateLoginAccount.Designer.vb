<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateLoginAccount
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateLoginAccount))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LogOut = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GbxCreate = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.PnlVCode = New System.Windows.Forms.Panel()
        Me.LblVcodeMsg = New System.Windows.Forms.Label()
        Me.BtnEnterVCode = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtVerificationCreate = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TxtUsernameCreate = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.BtnGoToLogin = New System.Windows.Forms.Button()
        Me.BtnCreate = New System.Windows.Forms.Button()
        Me.TxtConfirmPassCreate = New System.Windows.Forms.TextBox()
        Me.TxtPasswordCreate = New System.Windows.Forms.TextBox()
        Me.TxtEmailCreate = New System.Windows.Forms.TextBox()
        Me.TxtNameCreate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RdTypeStaff = New System.Windows.Forms.RadioButton()
        Me.RdTypeAdmin = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.GbxLogin = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BtnGoToStudentViewing = New System.Windows.Forms.Button()
        Me.PnlForgotPass = New System.Windows.Forms.Panel()
        Me.PnlFpCode = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtFpRecCode = New System.Windows.Forms.TextBox()
        Me.BtnFpEnter = New System.Windows.Forms.Button()
        Me.PnlFpNewPass = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TxtFpConfirmNewPass = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TxtFpNewPass = New System.Windows.Forms.TextBox()
        Me.BtnResetPass = New System.Windows.Forms.Button()
        Me.PnlFpEnterEmail = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TxtFpEmail = New System.Windows.Forms.TextBox()
        Me.BtnFpSendCode = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.BtnClosePnlForgotPass = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.BtnGoToCreateAcct = New System.Windows.Forms.Button()
        Me.BtnLogin = New System.Windows.Forms.Button()
        Me.TxtPassLogin = New System.Windows.Forms.TextBox()
        Me.TxtUnameEmailLogin = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.BtnOpenResetPassPanel = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.LblLoginMsg = New System.Windows.Forms.Label()
        Me.timer_fade_out_msg = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GbxCreate.SuspendLayout()
        Me.PnlVCode.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GbxLogin.SuspendLayout()
        Me.PnlForgotPass.SuspendLayout()
        Me.PnlFpCode.SuspendLayout()
        Me.PnlFpNewPass.SuspendLayout()
        Me.PnlFpEnterEmail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LogOut)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(237, 561)
        Me.Panel1.TabIndex = 3
        '
        'LogOut
        '
        Me.LogOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogOut.BackColor = System.Drawing.Color.MistyRose
        Me.LogOut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LogOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LogOut.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_logout_20
        Me.LogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LogOut.Location = New System.Drawing.Point(35, 518)
        Me.LogOut.Name = "LogOut"
        Me.LogOut.Size = New System.Drawing.Size(151, 30)
        Me.LogOut.TabIndex = 30
        Me.LogOut.Text = "EXIT"
        Me.LogOut.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.Location = New System.Drawing.Point(47, 220)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(139, 155)
        Me.Label3.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(3, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(233, 59)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "CPRI - CDSGA"
        '
        'Label1
        '
        Me.Label1.Image = CType(resources.GetObject("Label1.Image"), System.Drawing.Image)
        Me.Label1.Location = New System.Drawing.Point(65, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 90)
        Me.Label1.TabIndex = 0
        '
        'GbxCreate
        '
        Me.GbxCreate.Controls.Add(Me.Label24)
        Me.GbxCreate.Controls.Add(Me.PnlVCode)
        Me.GbxCreate.Controls.Add(Me.Label17)
        Me.GbxCreate.Controls.Add(Me.TxtUsernameCreate)
        Me.GbxCreate.Controls.Add(Me.Label16)
        Me.GbxCreate.Controls.Add(Me.BtnGoToLogin)
        Me.GbxCreate.Controls.Add(Me.BtnCreate)
        Me.GbxCreate.Controls.Add(Me.TxtConfirmPassCreate)
        Me.GbxCreate.Controls.Add(Me.TxtPasswordCreate)
        Me.GbxCreate.Controls.Add(Me.TxtEmailCreate)
        Me.GbxCreate.Controls.Add(Me.TxtNameCreate)
        Me.GbxCreate.Controls.Add(Me.Label8)
        Me.GbxCreate.Controls.Add(Me.Label7)
        Me.GbxCreate.Controls.Add(Me.Label6)
        Me.GbxCreate.Controls.Add(Me.Label5)
        Me.GbxCreate.Controls.Add(Me.Label4)
        Me.GbxCreate.Controls.Add(Me.Panel2)
        Me.GbxCreate.Controls.Add(Me.Label10)
        Me.GbxCreate.Controls.Add(Me.Label25)
        Me.GbxCreate.Location = New System.Drawing.Point(246, 12)
        Me.GbxCreate.Name = "GbxCreate"
        Me.GbxCreate.Size = New System.Drawing.Size(450, 537)
        Me.GbxCreate.TabIndex = 4
        Me.GbxCreate.TabStop = False
        Me.GbxCreate.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label24.Location = New System.Drawing.Point(54, 423)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(243, 16)
        Me.Label24.TabIndex = 20
        Me.Label24.Text = "Sending verification code. Please wait..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label24.Visible = False
        '
        'PnlVCode
        '
        Me.PnlVCode.BackColor = System.Drawing.SystemColors.Window
        Me.PnlVCode.Controls.Add(Me.LblVcodeMsg)
        Me.PnlVCode.Controls.Add(Me.BtnEnterVCode)
        Me.PnlVCode.Controls.Add(Me.Button1)
        Me.PnlVCode.Controls.Add(Me.Label9)
        Me.PnlVCode.Controls.Add(Me.TxtVerificationCreate)
        Me.PnlVCode.Location = New System.Drawing.Point(36, 136)
        Me.PnlVCode.Name = "PnlVCode"
        Me.PnlVCode.Size = New System.Drawing.Size(379, 239)
        Me.PnlVCode.TabIndex = 17
        Me.PnlVCode.Visible = False
        '
        'LblVcodeMsg
        '
        Me.LblVcodeMsg.AutoSize = True
        Me.LblVcodeMsg.ForeColor = System.Drawing.Color.Red
        Me.LblVcodeMsg.Location = New System.Drawing.Point(289, 133)
        Me.LblVcodeMsg.Name = "LblVcodeMsg"
        Me.LblVcodeMsg.Size = New System.Drawing.Size(0, 13)
        Me.LblVcodeMsg.TabIndex = 19
        '
        'BtnEnterVCode
        '
        Me.BtnEnterVCode.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnEnterVCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnEnterVCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEnterVCode.Location = New System.Drawing.Point(139, 171)
        Me.BtnEnterVCode.Name = "BtnEnterVCode"
        Me.BtnEnterVCode.Size = New System.Drawing.Size(95, 28)
        Me.BtnEnterVCode.TabIndex = 18
        Me.BtnEnterVCode.Text = "ENTER"
        Me.BtnEnterVCode.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(345, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(23, 19)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "X"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(11, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(184, 20)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "* Enter verification code :"
        '
        'TxtVerificationCreate
        '
        Me.TxtVerificationCreate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtVerificationCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtVerificationCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVerificationCreate.Location = New System.Drawing.Point(24, 103)
        Me.TxtVerificationCreate.Name = "TxtVerificationCreate"
        Me.TxtVerificationCreate.Size = New System.Drawing.Size(342, 26)
        Me.TxtVerificationCreate.TabIndex = 9
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label17.Location = New System.Drawing.Point(94, 503)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(200, 20)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Already have an account? :"
        '
        'TxtUsernameCreate
        '
        Me.TxtUsernameCreate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtUsernameCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtUsernameCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUsernameCreate.Location = New System.Drawing.Point(54, 170)
        Me.TxtUsernameCreate.Name = "TxtUsernameCreate"
        Me.TxtUsernameCreate.Size = New System.Drawing.Size(344, 26)
        Me.TxtUsernameCreate.TabIndex = 15
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Maroon
        Me.Label16.Location = New System.Drawing.Point(37, 147)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(101, 20)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "* Username :"
        '
        'BtnGoToLogin
        '
        Me.BtnGoToLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnGoToLogin.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnGoToLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGoToLogin.Location = New System.Drawing.Point(294, 505)
        Me.BtnGoToLogin.Name = "BtnGoToLogin"
        Me.BtnGoToLogin.Size = New System.Drawing.Size(54, 19)
        Me.BtnGoToLogin.TabIndex = 13
        Me.BtnGoToLogin.Text = "LOGIN"
        Me.BtnGoToLogin.UseVisualStyleBackColor = True
        '
        'BtnCreate
        '
        Me.BtnCreate.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnCreate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreate.Location = New System.Drawing.Point(54, 445)
        Me.BtnCreate.Name = "BtnCreate"
        Me.BtnCreate.Size = New System.Drawing.Size(355, 44)
        Me.BtnCreate.TabIndex = 12
        Me.BtnCreate.Text = "CREATE !"
        Me.BtnCreate.UseVisualStyleBackColor = False
        '
        'TxtConfirmPassCreate
        '
        Me.TxtConfirmPassCreate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtConfirmPassCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtConfirmPassCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtConfirmPassCreate.Location = New System.Drawing.Point(56, 346)
        Me.TxtConfirmPassCreate.Name = "TxtConfirmPassCreate"
        Me.TxtConfirmPassCreate.Size = New System.Drawing.Size(344, 26)
        Me.TxtConfirmPassCreate.TabIndex = 11
        Me.TxtConfirmPassCreate.UseSystemPasswordChar = True
        '
        'TxtPasswordCreate
        '
        Me.TxtPasswordCreate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtPasswordCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPasswordCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPasswordCreate.Location = New System.Drawing.Point(54, 292)
        Me.TxtPasswordCreate.Name = "TxtPasswordCreate"
        Me.TxtPasswordCreate.Size = New System.Drawing.Size(344, 26)
        Me.TxtPasswordCreate.TabIndex = 10
        Me.TxtPasswordCreate.UseSystemPasswordChar = True
        '
        'TxtEmailCreate
        '
        Me.TxtEmailCreate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtEmailCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmailCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmailCreate.Location = New System.Drawing.Point(52, 223)
        Me.TxtEmailCreate.Name = "TxtEmailCreate"
        Me.TxtEmailCreate.Size = New System.Drawing.Size(344, 26)
        Me.TxtEmailCreate.TabIndex = 8
        '
        'TxtNameCreate
        '
        Me.TxtNameCreate.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtNameCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNameCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNameCreate.Location = New System.Drawing.Point(52, 117)
        Me.TxtNameCreate.Name = "TxtNameCreate"
        Me.TxtNameCreate.Size = New System.Drawing.Size(344, 26)
        Me.TxtNameCreate.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(41, 323)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(155, 20)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "* Confirm Password :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(41, 269)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 20)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "* Password :"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(71, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(313, 40)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "CREATE ACCOUNT"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(37, 200)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "* Email :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(35, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "* Full Name :"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.RdTypeStaff)
        Me.Panel2.Controls.Add(Me.RdTypeAdmin)
        Me.Panel2.Location = New System.Drawing.Point(157, 380)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(137, 29)
        Me.Panel2.TabIndex = 18
        '
        'RdTypeStaff
        '
        Me.RdTypeStaff.AutoSize = True
        Me.RdTypeStaff.Location = New System.Drawing.Point(73, 6)
        Me.RdTypeStaff.Name = "RdTypeStaff"
        Me.RdTypeStaff.Size = New System.Drawing.Size(47, 17)
        Me.RdTypeStaff.TabIndex = 1
        Me.RdTypeStaff.TabStop = True
        Me.RdTypeStaff.Text = "Staff"
        Me.RdTypeStaff.UseVisualStyleBackColor = True
        '
        'RdTypeAdmin
        '
        Me.RdTypeAdmin.AutoSize = True
        Me.RdTypeAdmin.Checked = True
        Me.RdTypeAdmin.Location = New System.Drawing.Point(3, 6)
        Me.RdTypeAdmin.Name = "RdTypeAdmin"
        Me.RdTypeAdmin.Size = New System.Drawing.Size(54, 17)
        Me.RdTypeAdmin.TabIndex = 0
        Me.RdTypeAdmin.TabStop = True
        Me.RdTypeAdmin.Text = "Admin"
        Me.RdTypeAdmin.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(55, 385)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 16)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Account Type :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(177, 252)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(221, 15)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = " Invalid Email! Please Enter Valid Email"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Label25.Visible = False
        '
        'GbxLogin
        '
        Me.GbxLogin.Controls.Add(Me.Button2)
        Me.GbxLogin.Controls.Add(Me.BtnGoToStudentViewing)
        Me.GbxLogin.Controls.Add(Me.PnlForgotPass)
        Me.GbxLogin.Controls.Add(Me.Label13)
        Me.GbxLogin.Controls.Add(Me.BtnGoToCreateAcct)
        Me.GbxLogin.Controls.Add(Me.BtnLogin)
        Me.GbxLogin.Controls.Add(Me.TxtPassLogin)
        Me.GbxLogin.Controls.Add(Me.TxtUnameEmailLogin)
        Me.GbxLogin.Controls.Add(Me.Label18)
        Me.GbxLogin.Controls.Add(Me.Label19)
        Me.GbxLogin.Controls.Add(Me.Label20)
        Me.GbxLogin.Controls.Add(Me.BtnOpenResetPassPanel)
        Me.GbxLogin.Controls.Add(Me.Label14)
        Me.GbxLogin.Controls.Add(Me.LblLoginMsg)
        Me.GbxLogin.Location = New System.Drawing.Point(243, 12)
        Me.GbxLogin.Name = "GbxLogin"
        Me.GbxLogin.Size = New System.Drawing.Size(450, 541)
        Me.GbxLogin.TabIndex = 5
        Me.GbxLogin.TabStop = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(57, 72)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(344, 33)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "Shorcut"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'BtnGoToStudentViewing
        '
        Me.BtnGoToStudentViewing.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnGoToStudentViewing.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnGoToStudentViewing.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGoToStudentViewing.Location = New System.Drawing.Point(62, 431)
        Me.BtnGoToStudentViewing.Name = "BtnGoToStudentViewing"
        Me.BtnGoToStudentViewing.Size = New System.Drawing.Size(344, 33)
        Me.BtnGoToStudentViewing.TabIndex = 21
        Me.BtnGoToStudentViewing.Text = "STUDENT VIEWING --->"
        Me.BtnGoToStudentViewing.UseVisualStyleBackColor = False
        Me.BtnGoToStudentViewing.Visible = False
        '
        'PnlForgotPass
        '
        Me.PnlForgotPass.BackColor = System.Drawing.SystemColors.Window
        Me.PnlForgotPass.Controls.Add(Me.PnlFpCode)
        Me.PnlForgotPass.Controls.Add(Me.PnlFpNewPass)
        Me.PnlForgotPass.Controls.Add(Me.PnlFpEnterEmail)
        Me.PnlForgotPass.Controls.Add(Me.Label23)
        Me.PnlForgotPass.Controls.Add(Me.Label11)
        Me.PnlForgotPass.Controls.Add(Me.BtnClosePnlForgotPass)
        Me.PnlForgotPass.Location = New System.Drawing.Point(42, 133)
        Me.PnlForgotPass.Name = "PnlForgotPass"
        Me.PnlForgotPass.Size = New System.Drawing.Size(382, 247)
        Me.PnlForgotPass.TabIndex = 17
        Me.PnlForgotPass.Visible = False
        '
        'PnlFpCode
        '
        Me.PnlFpCode.Controls.Add(Me.Label27)
        Me.PnlFpCode.Controls.Add(Me.Label12)
        Me.PnlFpCode.Controls.Add(Me.TxtFpRecCode)
        Me.PnlFpCode.Controls.Add(Me.BtnFpEnter)
        Me.PnlFpCode.Location = New System.Drawing.Point(22, 51)
        Me.PnlFpCode.Name = "PnlFpCode"
        Me.PnlFpCode.Size = New System.Drawing.Size(0, 166)
        Me.PnlFpCode.TabIndex = 20
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Blue
        Me.Label27.Location = New System.Drawing.Point(221, 89)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(96, 15)
        Me.Label27.TabIndex = 19
        Me.Label27.Text = "Enter reset code"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(2, 30)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(178, 20)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "* Enter Recovery Code :"
        '
        'TxtFpRecCode
        '
        Me.TxtFpRecCode.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFpRecCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFpRecCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFpRecCode.Location = New System.Drawing.Point(15, 62)
        Me.TxtFpRecCode.Name = "TxtFpRecCode"
        Me.TxtFpRecCode.Size = New System.Drawing.Size(302, 26)
        Me.TxtFpRecCode.TabIndex = 9
        '
        'BtnFpEnter
        '
        Me.BtnFpEnter.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnFpEnter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFpEnter.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFpEnter.Location = New System.Drawing.Point(112, 123)
        Me.BtnFpEnter.Name = "BtnFpEnter"
        Me.BtnFpEnter.Size = New System.Drawing.Size(95, 28)
        Me.BtnFpEnter.TabIndex = 18
        Me.BtnFpEnter.Text = "ENTER"
        Me.BtnFpEnter.UseVisualStyleBackColor = False
        '
        'PnlFpNewPass
        '
        Me.PnlFpNewPass.Controls.Add(Me.Label28)
        Me.PnlFpNewPass.Controls.Add(Me.Label22)
        Me.PnlFpNewPass.Controls.Add(Me.TxtFpConfirmNewPass)
        Me.PnlFpNewPass.Controls.Add(Me.Label21)
        Me.PnlFpNewPass.Controls.Add(Me.TxtFpNewPass)
        Me.PnlFpNewPass.Controls.Add(Me.BtnResetPass)
        Me.PnlFpNewPass.Location = New System.Drawing.Point(25, 40)
        Me.PnlFpNewPass.Name = "PnlFpNewPass"
        Me.PnlFpNewPass.Size = New System.Drawing.Size(0, 189)
        Me.PnlFpNewPass.TabIndex = 22
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Green
        Me.Label28.Location = New System.Drawing.Point(14, 128)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(0, 15)
        Me.Label28.TabIndex = 21
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Maroon
        Me.Label22.Location = New System.Drawing.Point(2, 77)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(186, 20)
        Me.Label22.TabIndex = 19
        Me.Label22.Text = "* Confirm New Password:"
        '
        'TxtFpConfirmNewPass
        '
        Me.TxtFpConfirmNewPass.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFpConfirmNewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFpConfirmNewPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFpConfirmNewPass.Location = New System.Drawing.Point(15, 100)
        Me.TxtFpConfirmNewPass.Name = "TxtFpConfirmNewPass"
        Me.TxtFpConfirmNewPass.Size = New System.Drawing.Size(302, 26)
        Me.TxtFpConfirmNewPass.TabIndex = 20
        Me.TxtFpConfirmNewPass.UseSystemPasswordChar = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Maroon
        Me.Label21.Location = New System.Drawing.Point(2, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(170, 20)
        Me.Label21.TabIndex = 6
        Me.Label21.Text = "* Enter New Password:"
        '
        'TxtFpNewPass
        '
        Me.TxtFpNewPass.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFpNewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFpNewPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFpNewPass.Location = New System.Drawing.Point(15, 40)
        Me.TxtFpNewPass.Name = "TxtFpNewPass"
        Me.TxtFpNewPass.Size = New System.Drawing.Size(302, 26)
        Me.TxtFpNewPass.TabIndex = 9
        Me.TxtFpNewPass.UseSystemPasswordChar = True
        '
        'BtnResetPass
        '
        Me.BtnResetPass.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnResetPass.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnResetPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnResetPass.Location = New System.Drawing.Point(86, 157)
        Me.BtnResetPass.Name = "BtnResetPass"
        Me.BtnResetPass.Size = New System.Drawing.Size(173, 28)
        Me.BtnResetPass.TabIndex = 18
        Me.BtnResetPass.Text = "Reset Password!"
        Me.BtnResetPass.UseVisualStyleBackColor = False
        '
        'PnlFpEnterEmail
        '
        Me.PnlFpEnterEmail.Controls.Add(Me.Label29)
        Me.PnlFpEnterEmail.Controls.Add(Me.Label15)
        Me.PnlFpEnterEmail.Controls.Add(Me.TxtFpEmail)
        Me.PnlFpEnterEmail.Controls.Add(Me.BtnFpSendCode)
        Me.PnlFpEnterEmail.Location = New System.Drawing.Point(25, 40)
        Me.PnlFpEnterEmail.Name = "PnlFpEnterEmail"
        Me.PnlFpEnterEmail.Size = New System.Drawing.Size(332, 166)
        Me.PnlFpEnterEmail.TabIndex = 21
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Red
        Me.Label29.Location = New System.Drawing.Point(19, 89)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(0, 15)
        Me.Label29.TabIndex = 20
        Me.Label29.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Maroon
        Me.Label15.Location = New System.Drawing.Point(2, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(143, 20)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "* Enter Your Email:"
        '
        'TxtFpEmail
        '
        Me.TxtFpEmail.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFpEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtFpEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFpEmail.Location = New System.Drawing.Point(15, 62)
        Me.TxtFpEmail.Name = "TxtFpEmail"
        Me.TxtFpEmail.Size = New System.Drawing.Size(302, 26)
        Me.TxtFpEmail.TabIndex = 9
        '
        'BtnFpSendCode
        '
        Me.BtnFpSendCode.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnFpSendCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFpSendCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFpSendCode.Location = New System.Drawing.Point(86, 126)
        Me.BtnFpSendCode.Name = "BtnFpSendCode"
        Me.BtnFpSendCode.Size = New System.Drawing.Size(173, 28)
        Me.BtnFpSendCode.TabIndex = 18
        Me.BtnFpSendCode.Text = "Send Reset Code"
        Me.BtnFpSendCode.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Maroon
        Me.Label23.Location = New System.Drawing.Point(24, 13)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(315, 27)
        Me.Label23.TabIndex = 3
        Me.Label23.Text = "RESET PASSWORD"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(289, 133)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(0, 13)
        Me.Label11.TabIndex = 19
        '
        'BtnClosePnlForgotPass
        '
        Me.BtnClosePnlForgotPass.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClosePnlForgotPass.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnClosePnlForgotPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClosePnlForgotPass.Location = New System.Drawing.Point(345, 11)
        Me.BtnClosePnlForgotPass.Name = "BtnClosePnlForgotPass"
        Me.BtnClosePnlForgotPass.Size = New System.Drawing.Size(23, 19)
        Me.BtnClosePnlForgotPass.TabIndex = 18
        Me.BtnClosePnlForgotPass.Text = "X"
        Me.BtnClosePnlForgotPass.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label13.Location = New System.Drawing.Point(106, 501)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(185, 20)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Don't have an account? :"
        '
        'BtnGoToCreateAcct
        '
        Me.BtnGoToCreateAcct.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnGoToCreateAcct.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnGoToCreateAcct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGoToCreateAcct.Location = New System.Drawing.Point(297, 502)
        Me.BtnGoToCreateAcct.Name = "BtnGoToCreateAcct"
        Me.BtnGoToCreateAcct.Size = New System.Drawing.Size(54, 19)
        Me.BtnGoToCreateAcct.TabIndex = 13
        Me.BtnGoToCreateAcct.Text = "CREATE"
        Me.BtnGoToCreateAcct.UseVisualStyleBackColor = True
        '
        'BtnLogin
        '
        Me.BtnLogin.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLogin.Location = New System.Drawing.Point(61, 378)
        Me.BtnLogin.Name = "BtnLogin"
        Me.BtnLogin.Size = New System.Drawing.Size(344, 44)
        Me.BtnLogin.TabIndex = 12
        Me.BtnLogin.Text = "LOGIN !"
        Me.BtnLogin.UseVisualStyleBackColor = False
        '
        'TxtPassLogin
        '
        Me.TxtPassLogin.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtPassLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPassLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassLogin.Location = New System.Drawing.Point(61, 228)
        Me.TxtPassLogin.Name = "TxtPassLogin"
        Me.TxtPassLogin.Size = New System.Drawing.Size(344, 26)
        Me.TxtPassLogin.TabIndex = 10
        Me.TxtPassLogin.UseSystemPasswordChar = True
        '
        'TxtUnameEmailLogin
        '
        Me.TxtUnameEmailLogin.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TxtUnameEmailLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtUnameEmailLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnameEmailLogin.Location = New System.Drawing.Point(59, 159)
        Me.TxtUnameEmailLogin.Name = "TxtUnameEmailLogin"
        Me.TxtUnameEmailLogin.Size = New System.Drawing.Size(344, 26)
        Me.TxtUnameEmailLogin.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Maroon
        Me.Label18.Location = New System.Drawing.Point(48, 205)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 20)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "* Password :"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Times New Roman", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Maroon
        Me.Label19.Location = New System.Drawing.Point(71, 26)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(313, 40)
        Me.Label19.TabIndex = 3
        Me.Label19.Text = "LOGIN"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Maroon
        Me.Label20.Location = New System.Drawing.Point(44, 136)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(205, 20)
        Me.Label20.TabIndex = 1
        Me.Label20.Text = "* Enter Username or Email :"
        '
        'BtnOpenResetPassPanel
        '
        Me.BtnOpenResetPassPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOpenResetPassPanel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnOpenResetPassPanel.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOpenResetPassPanel.Location = New System.Drawing.Point(168, 266)
        Me.BtnOpenResetPassPanel.Name = "BtnOpenResetPassPanel"
        Me.BtnOpenResetPassPanel.Size = New System.Drawing.Size(53, 20)
        Me.BtnOpenResetPassPanel.TabIndex = 19
        Me.BtnOpenResetPassPanel.Text = "Click me"
        Me.BtnOpenResetPassPanel.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label14.Location = New System.Drawing.Point(61, 268)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(109, 15)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Forgot Password? "
        '
        'LblLoginMsg
        '
        Me.LblLoginMsg.AutoSize = True
        Me.LblLoginMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLoginMsg.ForeColor = System.Drawing.Color.DarkGreen
        Me.LblLoginMsg.Location = New System.Drawing.Point(59, 347)
        Me.LblLoginMsg.Name = "LblLoginMsg"
        Me.LblLoginMsg.Size = New System.Drawing.Size(176, 18)
        Me.LblLoginMsg.TabIndex = 20
        Me.LblLoginMsg.Text = "LOGIN SUCCESSFULLY"
        Me.LblLoginMsg.Visible = False
        '
        'timer_fade_out_msg
        '
        Me.timer_fade_out_msg.Interval = 1000
        '
        'CreateLoginAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(705, 561)
        Me.Controls.Add(Me.GbxLogin)
        Me.Controls.Add(Me.GbxCreate)
        Me.Controls.Add(Me.Panel1)
        Me.MaximumSize = New System.Drawing.Size(721, 600)
        Me.MinimumSize = New System.Drawing.Size(721, 600)
        Me.Name = "CreateLoginAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CREATE OR LOGIN ACCOUNT"
        Me.Panel1.ResumeLayout(False)
        Me.GbxCreate.ResumeLayout(False)
        Me.GbxCreate.PerformLayout()
        Me.PnlVCode.ResumeLayout(False)
        Me.PnlVCode.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GbxLogin.ResumeLayout(False)
        Me.GbxLogin.PerformLayout()
        Me.PnlForgotPass.ResumeLayout(False)
        Me.PnlForgotPass.PerformLayout()
        Me.PnlFpCode.ResumeLayout(False)
        Me.PnlFpCode.PerformLayout()
        Me.PnlFpNewPass.ResumeLayout(False)
        Me.PnlFpNewPass.PerformLayout()
        Me.PnlFpEnterEmail.ResumeLayout(False)
        Me.PnlFpEnterEmail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GbxCreate As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents BtnGoToLogin As Button
    Friend WithEvents BtnCreate As Button
    Friend WithEvents TxtConfirmPassCreate As TextBox
    Friend WithEvents TxtPasswordCreate As TextBox
    Friend WithEvents TxtVerificationCreate As TextBox
    Friend WithEvents TxtEmailCreate As TextBox
    Friend WithEvents TxtNameCreate As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtUsernameCreate As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents PnlVCode As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents BtnEnterVCode As Button
    Friend WithEvents LblVcodeMsg As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents RdTypeStaff As RadioButton
    Friend WithEvents RdTypeAdmin As RadioButton
    Friend WithEvents GbxLogin As GroupBox
    Friend WithEvents PnlForgotPass As Panel
    Friend WithEvents PnlFpCode As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtFpRecCode As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents BtnFpEnter As Button
    Friend WithEvents BtnClosePnlForgotPass As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents BtnGoToCreateAcct As Button
    Friend WithEvents BtnLogin As Button
    Friend WithEvents TxtPassLogin As TextBox
    Friend WithEvents TxtUnameEmailLogin As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents PnlFpNewPass As Panel
    Friend WithEvents Label22 As Label
    Friend WithEvents TxtFpConfirmNewPass As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents TxtFpNewPass As TextBox
    Friend WithEvents BtnResetPass As Button
    Friend WithEvents PnlFpEnterEmail As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents TxtFpEmail As TextBox
    Friend WithEvents BtnFpSendCode As Button
    Friend WithEvents BtnOpenResetPassPanel As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents LblLoginMsg As Label
    Friend WithEvents timer_fade_out_msg As Timer
    Friend WithEvents Label24 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents BtnGoToStudentViewing As Button
    Friend WithEvents LogOut As Button
    Friend WithEvents Button2 As Button
End Class
