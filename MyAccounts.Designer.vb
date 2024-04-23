<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyAccounts
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
        Me.BTN_EDIT = New System.Windows.Forms.Button()
        Me.TXT_FNAME = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TXT_UNAME = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TXT_EMAIL = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXT_PASS = New System.Windows.Forms.TextBox()
        Me.LBL_UTYPE = New System.Windows.Forms.Label()
        Me.BTN_UPDT = New System.Windows.Forms.Button()
        Me.BTN_CLOSE = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TXT_NEWPASS = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LBL_UID = New System.Windows.Forms.Label()
        Me.BTN_CANCEL = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LblPassMsg = New System.Windows.Forms.Label()
        Me.LblEmailMsg = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BTN_ENTER_UPDT = New System.Windows.Forms.Button()
        Me.TXT_VCODE = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BTN_EDIT
        '
        Me.BTN_EDIT.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_EDIT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_EDIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_EDIT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_EDIT.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_edit_21
        Me.BTN_EDIT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_EDIT.Location = New System.Drawing.Point(551, 108)
        Me.BTN_EDIT.Name = "BTN_EDIT"
        Me.BTN_EDIT.Size = New System.Drawing.Size(128, 36)
        Me.BTN_EDIT.TabIndex = 0
        Me.BTN_EDIT.Text = "EDIT"
        Me.BTN_EDIT.UseVisualStyleBackColor = False
        '
        'TXT_FNAME
        '
        Me.TXT_FNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_FNAME.Location = New System.Drawing.Point(153, 112)
        Me.TXT_FNAME.Name = "TXT_FNAME"
        Me.TXT_FNAME.ReadOnly = True
        Me.TXT_FNAME.Size = New System.Drawing.Size(368, 26)
        Me.TXT_FNAME.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "FULL NAME :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(702, 47)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "MY ACCOUNT"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(37, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "USERNAME :"
        '
        'TXT_UNAME
        '
        Me.TXT_UNAME.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_UNAME.Location = New System.Drawing.Point(153, 149)
        Me.TXT_UNAME.Name = "TXT_UNAME"
        Me.TXT_UNAME.ReadOnly = True
        Me.TXT_UNAME.Size = New System.Drawing.Size(368, 26)
        Me.TXT_UNAME.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(78, 187)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "EMAIL :"
        '
        'TXT_EMAIL
        '
        Me.TXT_EMAIL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_EMAIL.Location = New System.Drawing.Point(153, 186)
        Me.TXT_EMAIL.Name = "TXT_EMAIL"
        Me.TXT_EMAIL.ReadOnly = True
        Me.TXT_EMAIL.Size = New System.Drawing.Size(368, 26)
        Me.TXT_EMAIL.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(33, 245)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "PASSWORD :"
        '
        'TXT_PASS
        '
        Me.TXT_PASS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_PASS.Location = New System.Drawing.Point(153, 244)
        Me.TXT_PASS.Name = "TXT_PASS"
        Me.TXT_PASS.ReadOnly = True
        Me.TXT_PASS.Size = New System.Drawing.Size(368, 26)
        Me.TXT_PASS.TabIndex = 9
        Me.TXT_PASS.UseSystemPasswordChar = True
        '
        'LBL_UTYPE
        '
        Me.LBL_UTYPE.AutoSize = True
        Me.LBL_UTYPE.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.LBL_UTYPE.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_UTYPE.Location = New System.Drawing.Point(1, 27)
        Me.LBL_UTYPE.Name = "LBL_UTYPE"
        Me.LBL_UTYPE.Size = New System.Drawing.Size(119, 20)
        Me.LBL_UTYPE.TabIndex = 11
        Me.LBL_UTYPE.Text = "USER TYPE :"
        Me.LBL_UTYPE.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BTN_UPDT
        '
        Me.BTN_UPDT.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_UPDT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_UPDT.Enabled = False
        Me.BTN_UPDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_UPDT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_UPDT.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_update_28
        Me.BTN_UPDT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_UPDT.Location = New System.Drawing.Point(551, 155)
        Me.BTN_UPDT.Name = "BTN_UPDT"
        Me.BTN_UPDT.Size = New System.Drawing.Size(128, 36)
        Me.BTN_UPDT.TabIndex = 12
        Me.BTN_UPDT.Text = "UPDATE"
        Me.BTN_UPDT.UseVisualStyleBackColor = False
        '
        'BTN_CLOSE
        '
        Me.BTN_CLOSE.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_CLOSE.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_CLOSE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_CLOSE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_CLOSE.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_cancel_28
        Me.BTN_CLOSE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_CLOSE.Location = New System.Drawing.Point(558, 275)
        Me.BTN_CLOSE.Name = "BTN_CLOSE"
        Me.BTN_CLOSE.Size = New System.Drawing.Size(121, 36)
        Me.BTN_CLOSE.TabIndex = 13
        Me.BTN_CLOSE.Text = "CLOSE"
        Me.BTN_CLOSE.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 288)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(132, 40)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "CONFIRM NEW :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PASSWORD"
        Me.Label8.Visible = False
        '
        'TXT_NEWPASS
        '
        Me.TXT_NEWPASS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_NEWPASS.Location = New System.Drawing.Point(153, 285)
        Me.TXT_NEWPASS.Name = "TXT_NEWPASS"
        Me.TXT_NEWPASS.ReadOnly = True
        Me.TXT_NEWPASS.Size = New System.Drawing.Size(368, 26)
        Me.TXT_NEWPASS.TabIndex = 14
        Me.TXT_NEWPASS.UseSystemPasswordChar = True
        Me.TXT_NEWPASS.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(46, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "USER ID :"
        '
        'LBL_UID
        '
        Me.LBL_UID.AutoSize = True
        Me.LBL_UID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_UID.Location = New System.Drawing.Point(149, 57)
        Me.LBL_UID.Name = "LBL_UID"
        Me.LBL_UID.Size = New System.Drawing.Size(84, 20)
        Me.LBL_UID.TabIndex = 16
        Me.LBL_UID.Text = "USER ID :"
        Me.LBL_UID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BTN_CANCEL
        '
        Me.BTN_CANCEL.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_CANCEL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_CANCEL.Enabled = False
        Me.BTN_CANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_CANCEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_CANCEL.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_cancel_30
        Me.BTN_CANCEL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_CANCEL.Location = New System.Drawing.Point(551, 200)
        Me.BTN_CANCEL.Name = "BTN_CANCEL"
        Me.BTN_CANCEL.Size = New System.Drawing.Size(128, 36)
        Me.BTN_CANCEL.TabIndex = 17
        Me.BTN_CANCEL.Text = "CANCEL"
        Me.BTN_CANCEL.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(156, 317)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(237, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Leave this blank if you're not changing password"
        Me.Label7.Visible = False
        '
        'LblPassMsg
        '
        Me.LblPassMsg.AutoSize = True
        Me.LblPassMsg.ForeColor = System.Drawing.Color.Green
        Me.LblPassMsg.Location = New System.Drawing.Point(150, 228)
        Me.LblPassMsg.Name = "LblPassMsg"
        Me.LblPassMsg.Size = New System.Drawing.Size(0, 13)
        Me.LblPassMsg.TabIndex = 19
        Me.LblPassMsg.Visible = False
        '
        'LblEmailMsg
        '
        Me.LblEmailMsg.AutoSize = True
        Me.LblEmailMsg.ForeColor = System.Drawing.Color.Green
        Me.LblEmailMsg.Location = New System.Drawing.Point(400, 215)
        Me.LblEmailMsg.Name = "LblEmailMsg"
        Me.LblEmailMsg.Size = New System.Drawing.Size(0, 13)
        Me.LblEmailMsg.TabIndex = 20
        Me.LblEmailMsg.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BTN_ENTER_UPDT)
        Me.Panel1.Controls.Add(Me.TXT_VCODE)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Location = New System.Drawing.Point(153, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(368, 116)
        Me.Panel1.TabIndex = 21
        Me.Panel1.Visible = False
        '
        'BTN_ENTER_UPDT
        '
        Me.BTN_ENTER_UPDT.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BTN_ENTER_UPDT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BTN_ENTER_UPDT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTN_ENTER_UPDT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_ENTER_UPDT.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_update_28
        Me.BTN_ENTER_UPDT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BTN_ENTER_UPDT.Location = New System.Drawing.Point(107, 74)
        Me.BTN_ENTER_UPDT.Name = "BTN_ENTER_UPDT"
        Me.BTN_ENTER_UPDT.Size = New System.Drawing.Size(156, 32)
        Me.BTN_ENTER_UPDT.TabIndex = 22
        Me.BTN_ENTER_UPDT.Text = "ENTER AND UPDATE"
        Me.BTN_ENTER_UPDT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BTN_ENTER_UPDT.UseVisualStyleBackColor = False
        '
        'TXT_VCODE
        '
        Me.TXT_VCODE.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXT_VCODE.Location = New System.Drawing.Point(12, 36)
        Me.TXT_VCODE.Name = "TXT_VCODE"
        Me.TXT_VCODE.Size = New System.Drawing.Size(342, 31)
        Me.TXT_VCODE.TabIndex = 22
        Me.TXT_VCODE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(238, 20)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "ENTER VERIFICATION CODE :"
        '
        'MyAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(702, 347)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblEmailMsg)
        Me.Controls.Add(Me.LblPassMsg)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.BTN_CANCEL)
        Me.Controls.Add(Me.LBL_UID)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TXT_NEWPASS)
        Me.Controls.Add(Me.BTN_CLOSE)
        Me.Controls.Add(Me.BTN_UPDT)
        Me.Controls.Add(Me.LBL_UTYPE)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TXT_PASS)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TXT_EMAIL)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TXT_UNAME)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TXT_FNAME)
        Me.Controls.Add(Me.BTN_EDIT)
        Me.MaximumSize = New System.Drawing.Size(718, 386)
        Me.MinimumSize = New System.Drawing.Size(718, 386)
        Me.Name = "MyAccounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MyAccounts"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BTN_EDIT As Button
    Friend WithEvents TXT_FNAME As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TXT_UNAME As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TXT_EMAIL As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TXT_PASS As TextBox
    Friend WithEvents LBL_UTYPE As Label
    Friend WithEvents BTN_UPDT As Button
    Friend WithEvents BTN_CLOSE As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents TXT_NEWPASS As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LBL_UID As Label
    Friend WithEvents BTN_CANCEL As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents LblPassMsg As Label
    Friend WithEvents LblEmailMsg As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BTN_ENTER_UPDT As Button
    Friend WithEvents TXT_VCODE As TextBox
    Friend WithEvents Label9 As Label
End Class
