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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateLoginAccount))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GbxCreate = New System.Windows.Forms.GroupBox()
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
        Me.Panel1.SuspendLayout()
        Me.GbxCreate.SuspendLayout()
        Me.PnlVCode.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(237, 561)
        Me.Panel1.TabIndex = 3
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
        Me.GbxCreate.Location = New System.Drawing.Point(246, -2)
        Me.GbxCreate.Name = "GbxCreate"
        Me.GbxCreate.Size = New System.Drawing.Size(450, 561)
        Me.GbxCreate.TabIndex = 4
        Me.GbxCreate.TabStop = False
        Me.GbxCreate.Text = "Create"
        '
        'PnlVCode
        '
        Me.PnlVCode.BackColor = System.Drawing.SystemColors.Window
        Me.PnlVCode.Controls.Add(Me.LblVcodeMsg)
        Me.PnlVCode.Controls.Add(Me.BtnEnterVCode)
        Me.PnlVCode.Controls.Add(Me.Button1)
        Me.PnlVCode.Controls.Add(Me.Label9)
        Me.PnlVCode.Controls.Add(Me.TxtVerificationCreate)
        Me.PnlVCode.Location = New System.Drawing.Point(30, 172)
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
        Me.Label17.Location = New System.Drawing.Point(94, 526)
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
        Me.BtnGoToLogin.Location = New System.Drawing.Point(294, 528)
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
        Me.BtnCreate.Location = New System.Drawing.Point(54, 453)
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
        Me.Label10.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label10.Location = New System.Drawing.Point(55, 385)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 16)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Account Type :"
        '
        'CreateLoginAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(705, 561)
        Me.Controls.Add(Me.GbxCreate)
        Me.Controls.Add(Me.Panel1)
        Me.MaximumSize = New System.Drawing.Size(721, 600)
        Me.MinimumSize = New System.Drawing.Size(721, 600)
        Me.Name = "CreateLoginAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.GbxCreate.ResumeLayout(False)
        Me.GbxCreate.PerformLayout()
        Me.PnlVCode.ResumeLayout(False)
        Me.PnlVCode.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GbxCreate As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
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
End Class
