<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResearchRepoManager
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtpRange1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DtpRange2 = New System.Windows.Forms.DateTimePicker()
        Me.CbbxSearch = New System.Windows.Forms.ComboBox()
        Me.PnlFilter = New System.Windows.Forms.Panel()
        Me.CbxAllRole = New System.Windows.Forms.CheckBox()
        Me.CbxStud = New System.Windows.Forms.CheckBox()
        Me.CbxStaff = New System.Windows.Forms.CheckBox()
        Me.CbxAdmin = New System.Windows.Forms.CheckBox()
        Me.CbxFaculty = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RadioButton7 = New System.Windows.Forms.RadioButton()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BtnCloseFilter = New System.Windows.Forms.Button()
        Me.TmOpenFilter = New System.Windows.Forms.Timer(Me.components)
        Me.TmCloseFilter = New System.Windows.Forms.Timer(Me.components)
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlFilter.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.TextBox1.Location = New System.Drawing.Point(230, 37)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(303, 21)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "Search Title, Author, Keyword, Abstract, Etc."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Search By :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Filter:"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.Info
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10})
        Me.DataGridView2.Location = New System.Drawing.Point(12, 73)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.Size = New System.Drawing.Size(1231, 376)
        Me.DataGridView2.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.FillWeight = 85.10638!
        Me.Column1.HeaderText = "Research Agenda"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.FillWeight = 85.10638!
        Me.Column2.HeaderText = "Title"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.FillWeight = 85.10638!
        Me.Column3.HeaderText = "Authors / Co-Authors"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.FillWeight = 85.10638!
        Me.Column4.HeaderText = "Abstract"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.FillWeight = 85.10638!
        Me.Column5.HeaderText = "Degree Program"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column6.FillWeight = 85.10638!
        Me.Column6.HeaderText = "Role"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 85
        '
        'Column7
        '
        Me.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column7.FillWeight = 85.10638!
        Me.Column7.HeaderText = "Date Completed"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 75
        '
        'Column8
        '
        Me.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column8.FillWeight = 204.2553!
        Me.Column8.HeaderText = "Status (Ongoing/Completed)"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 115
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column9.HeaderText = "Published (Yes/No)"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 65
        '
        'Column10
        '
        Me.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column10.HeaderText = "Presented (Year/No)"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 65
        '
        'DtpRange1
        '
        Me.DtpRange1.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtpRange1.Location = New System.Drawing.Point(63, 303)
        Me.DtpRange1.Name = "DtpRange1"
        Me.DtpRange1.Size = New System.Drawing.Size(182, 20)
        Me.DtpRange1.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 307)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "From"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(251, 307)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "To"
        '
        'DtpRange2
        '
        Me.DtpRange2.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtpRange2.Location = New System.Drawing.Point(279, 303)
        Me.DtpRange2.Name = "DtpRange2"
        Me.DtpRange2.Size = New System.Drawing.Size(182, 20)
        Me.DtpRange2.TabIndex = 15
        '
        'CbbxSearch
        '
        Me.CbbxSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbbxSearch.FormattingEnabled = True
        Me.CbbxSearch.Items.AddRange(New Object() {"Default", "Title", "Research Agenda", "Author(s)", "Abstract", "Degree Program", "Date Completed"})
        Me.CbbxSearch.Location = New System.Drawing.Point(86, 37)
        Me.CbbxSearch.Name = "CbbxSearch"
        Me.CbbxSearch.Size = New System.Drawing.Size(136, 21)
        Me.CbbxSearch.TabIndex = 17
        '
        'PnlFilter
        '
        Me.PnlFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlFilter.Controls.Add(Me.CbxAllRole)
        Me.PnlFilter.Controls.Add(Me.CbxStud)
        Me.PnlFilter.Controls.Add(Me.CbxStaff)
        Me.PnlFilter.Controls.Add(Me.CbxAdmin)
        Me.PnlFilter.Controls.Add(Me.CbxFaculty)
        Me.PnlFilter.Controls.Add(Me.Panel3)
        Me.PnlFilter.Controls.Add(Me.Panel2)
        Me.PnlFilter.Controls.Add(Me.Panel1)
        Me.PnlFilter.Controls.Add(Me.DtpRange1)
        Me.PnlFilter.Controls.Add(Me.Label4)
        Me.PnlFilter.Controls.Add(Me.DtpRange2)
        Me.PnlFilter.Controls.Add(Me.Label3)
        Me.PnlFilter.Controls.Add(Me.Button7)
        Me.PnlFilter.Controls.Add(Me.Button6)
        Me.PnlFilter.Controls.Add(Me.Label6)
        Me.PnlFilter.Controls.Add(Me.Label5)
        Me.PnlFilter.Controls.Add(Me.BtnCloseFilter)
        Me.PnlFilter.Controls.Add(Me.Label2)
        Me.PnlFilter.ForeColor = System.Drawing.Color.White
        Me.PnlFilter.Location = New System.Drawing.Point(86, 12)
        Me.PnlFilter.Name = "PnlFilter"
        Me.PnlFilter.Size = New System.Drawing.Size(0, 0)
        Me.PnlFilter.TabIndex = 19
        '
        'CbxAllRole
        '
        Me.CbxAllRole.AutoSize = True
        Me.CbxAllRole.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbxAllRole.Location = New System.Drawing.Point(68, 50)
        Me.CbxAllRole.Name = "CbxAllRole"
        Me.CbxAllRole.Size = New System.Drawing.Size(64, 16)
        Me.CbxAllRole.TabIndex = 41
        Me.CbxAllRole.Text = "Check All"
        Me.CbxAllRole.UseVisualStyleBackColor = True
        '
        'CbxStud
        '
        Me.CbxStud.AutoSize = True
        Me.CbxStud.Location = New System.Drawing.Point(119, 72)
        Me.CbxStud.Name = "CbxStud"
        Me.CbxStud.Size = New System.Drawing.Size(63, 17)
        Me.CbxStud.TabIndex = 40
        Me.CbxStud.Text = "Student"
        Me.CbxStud.UseVisualStyleBackColor = True
        '
        'CbxStaff
        '
        Me.CbxStaff.AutoSize = True
        Me.CbxStaff.Location = New System.Drawing.Point(23, 118)
        Me.CbxStaff.Name = "CbxStaff"
        Me.CbxStaff.Size = New System.Drawing.Size(48, 17)
        Me.CbxStaff.TabIndex = 39
        Me.CbxStaff.Text = "Staff"
        Me.CbxStaff.UseVisualStyleBackColor = True
        '
        'CbxAdmin
        '
        Me.CbxAdmin.AutoSize = True
        Me.CbxAdmin.Location = New System.Drawing.Point(23, 95)
        Me.CbxAdmin.Name = "CbxAdmin"
        Me.CbxAdmin.Size = New System.Drawing.Size(55, 17)
        Me.CbxAdmin.TabIndex = 38
        Me.CbxAdmin.Text = "Admin"
        Me.CbxAdmin.UseVisualStyleBackColor = True
        '
        'CbxFaculty
        '
        Me.CbxFaculty.AutoSize = True
        Me.CbxFaculty.Location = New System.Drawing.Point(23, 72)
        Me.CbxFaculty.Name = "CbxFaculty"
        Me.CbxFaculty.Size = New System.Drawing.Size(60, 17)
        Me.CbxFaculty.TabIndex = 37
        Me.CbxFaculty.Text = "Faculty"
        Me.CbxFaculty.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.RadioButton2)
        Me.Panel3.Controls.Add(Me.RadioButton1)
        Me.Panel3.Location = New System.Drawing.Point(16, 160)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(248, 29)
        Me.Panel3.TabIndex = 36
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(3, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Status :"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(166, 5)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(75, 17)
        Me.RadioButton2.TabIndex = 29
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Completed"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(97, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(65, 17)
        Me.RadioButton1.TabIndex = 28
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Ongoing"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.RadioButton7)
        Me.Panel2.Controls.Add(Me.RadioButton8)
        Me.Panel2.Location = New System.Drawing.Point(16, 195)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(248, 29)
        Me.Panel2.TabIndex = 35
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(3, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Published :"
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Location = New System.Drawing.Point(97, 5)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(39, 17)
        Me.RadioButton7.TabIndex = 33
        Me.RadioButton7.TabStop = True
        Me.RadioButton7.Text = "No"
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Location = New System.Drawing.Point(166, 5)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(43, 17)
        Me.RadioButton8.TabIndex = 32
        Me.RadioButton8.TabStop = True
        Me.RadioButton8.Text = "Yes"
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.RadioButton5)
        Me.Panel1.Controls.Add(Me.RadioButton6)
        Me.Panel1.Location = New System.Drawing.Point(16, 230)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(248, 29)
        Me.Panel1.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(3, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Presented :"
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(97, 5)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(39, 17)
        Me.RadioButton5.TabIndex = 33
        Me.RadioButton5.TabStop = True
        Me.RadioButton5.Text = "No"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(166, 5)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(43, 17)
        Me.RadioButton6.TabIndex = 32
        Me.RadioButton6.TabStop = True
        Me.RadioButton6.Text = "Yes"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button7.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_reset_14
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button7.Location = New System.Drawing.Point(269, 357)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(61, 23)
        Me.Button7.TabIndex = 27
        Me.Button7.Text = "Reset"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button6.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_filtered_file_14
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.Location = New System.Drawing.Point(175, 357)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(61, 23)
        Me.Button6.TabIndex = 26
        Me.Button6.Text = "Apply"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(20, 282)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Date :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(21, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Role :"
        '
        'BtnCloseFilter
        '
        Me.BtnCloseFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCloseFilter.BackColor = System.Drawing.Color.Red
        Me.BtnCloseFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCloseFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCloseFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnCloseFilter.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCloseFilter.Location = New System.Drawing.Point(-30, 4)
        Me.BtnCloseFilter.Name = "BtnCloseFilter"
        Me.BtnCloseFilter.Size = New System.Drawing.Size(25, 21)
        Me.BtnCloseFilter.TabIndex = 21
        Me.BtnCloseFilter.Text = "X"
        Me.BtnCloseFilter.UseVisualStyleBackColor = False
        '
        'TmOpenFilter
        '
        Me.TmOpenFilter.Interval = 10
        '
        'TmCloseFilter
        '
        Me.TmCloseFilter.Interval = 10
        '
        'BtnFilter
        '
        Me.BtnFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnFilter.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_filter_14
        Me.BtnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnFilter.Location = New System.Drawing.Point(17, 10)
        Me.BtnFilter.Name = "BtnFilter"
        Me.BtnFilter.Size = New System.Drawing.Size(67, 21)
        Me.BtnFilter.TabIndex = 20
        Me.BtnFilter.Text = "Filter"
        Me.BtnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFilter.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button4.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_delete_211
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.Location = New System.Drawing.Point(1140, 465)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(102, 34)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "Delete"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button5.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_update_done_21__1_
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(1020, 465)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 34)
        Me.Button5.TabIndex = 10
        Me.Button5.Text = "Update"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button3.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_edit_211
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(900, 465)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 34)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Edit"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button2.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_search_13
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button2.Location = New System.Drawing.Point(539, 37)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(65, 21)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Search"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_add_21
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(780, 465)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 34)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ResearchRepoManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1255, 513)
        Me.Controls.Add(Me.BtnFilter)
        Me.Controls.Add(Me.PnlFilter)
        Me.Controls.Add(Me.CbbxSearch)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView2)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Name = "ResearchRepoManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RESEARCH REPOSITORY MANAGER"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlFilter.ResumeLayout(False)
        Me.PnlFilter.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents DtpRange1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DtpRange2 As DateTimePicker
    Friend WithEvents CbbxSearch As ComboBox
    Friend WithEvents PnlFilter As Panel
    Friend WithEvents BtnFilter As Button
    Friend WithEvents TmOpenFilter As Timer
    Friend WithEvents BtnCloseFilter As Button
    Friend WithEvents TmCloseFilter As Timer
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents RadioButton5 As RadioButton
    Friend WithEvents RadioButton6 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents RadioButton7 As RadioButton
    Friend WithEvents RadioButton8 As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CbxStud As CheckBox
    Friend WithEvents CbxStaff As CheckBox
    Friend WithEvents CbxAdmin As CheckBox
    Friend WithEvents CbxFaculty As CheckBox
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents CbxAllRole As CheckBox
End Class
