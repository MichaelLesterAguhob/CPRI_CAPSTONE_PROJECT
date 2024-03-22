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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpRange1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DtpRange2 = New System.Windows.Forms.DateTimePicker()
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
        Me.DgvSwData = New System.Windows.Forms.DataGridView()
        Me.BtnRemoveSelection = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Control_No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.research_agenda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_btn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.semester = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.school_year = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status_ongoing_completed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.published = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presented = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlFilter.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvSwData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.TextBox1.Location = New System.Drawing.Point(113, 57)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(303, 21)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "Search Title, Author, Keyword, Abstract, Etc."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Filter:"
        '
        'DtpRange1
        '
        Me.DtpRange1.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtpRange1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpRange1.Location = New System.Drawing.Point(63, 303)
        Me.DtpRange1.Name = "DtpRange1"
        Me.DtpRange1.Size = New System.Drawing.Size(123, 20)
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
        Me.Label4.Location = New System.Drawing.Point(204, 307)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "To"
        '
        'DtpRange2
        '
        Me.DtpRange2.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtpRange2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpRange2.Location = New System.Drawing.Point(244, 303)
        Me.DtpRange2.Name = "DtpRange2"
        Me.DtpRange2.Size = New System.Drawing.Size(123, 20)
        Me.DtpRange2.TabIndex = 15
        '
        'PnlFilter
        '
        Me.PnlFilter.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
        Me.PnlFilter.ForeColor = System.Drawing.Color.Black
        Me.PnlFilter.Location = New System.Drawing.Point(87, 56)
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
        Me.Label9.ForeColor = System.Drawing.Color.Black
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
        Me.Label10.ForeColor = System.Drawing.Color.Black
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
        Me.Label8.ForeColor = System.Drawing.Color.Black
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
        Me.Label6.ForeColor = System.Drawing.Color.Black
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
        Me.Label5.ForeColor = System.Drawing.Color.Black
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
        'DgvSwData
        '
        Me.DgvSwData.AllowUserToAddRows = False
        Me.DgvSwData.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.DgvSwData.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvSwData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvSwData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvSwData.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DgvSwData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvSwData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSwData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.count, Me.Control_No, Me.research_agenda, Me.title, Me.col_btn, Me.Column1, Me.Column3, Me.Column5, Me.Column6, Me.semester, Me.school_year, Me.status_ongoing_completed, Me.published, Me.presented})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.DefaultCellStyle = DataGridViewCellStyle7
        Me.DgvSwData.Location = New System.Drawing.Point(14, 85)
        Me.DgvSwData.MultiSelect = False
        Me.DgvSwData.Name = "DgvSwData"
        Me.DgvSwData.ReadOnly = True
        Me.DgvSwData.RowHeadersVisible = False
        DataGridViewCellStyle8.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.DgvSwData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSwData.ShowRowErrors = False
        Me.DgvSwData.Size = New System.Drawing.Size(1231, 365)
        Me.DgvSwData.TabIndex = 21
        '
        'BtnRemoveSelection
        '
        Me.BtnRemoveSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRemoveSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnRemoveSelection.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnRemoveSelection.FlatAppearance.BorderSize = 0
        Me.BtnRemoveSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemoveSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRemoveSelection.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnRemoveSelection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnRemoveSelection.Location = New System.Drawing.Point(1143, 51)
        Me.BtnRemoveSelection.Name = "BtnRemoveSelection"
        Me.BtnRemoveSelection.Size = New System.Drawing.Size(102, 26)
        Me.BtnRemoveSelection.TabIndex = 22
        Me.BtnRemoveSelection.Text = "Remove Selection"
        Me.BtnRemoveSelection.UseVisualStyleBackColor = False
        Me.BtnRemoveSelection.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button3.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_view_21
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(819, 464)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 34)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "View"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'BtnFilter
        '
        Me.BtnFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnFilter.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_filter_14
        Me.BtnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnFilter.Location = New System.Drawing.Point(14, 56)
        Me.BtnFilter.Name = "BtnFilter"
        Me.BtnFilter.Size = New System.Drawing.Size(67, 21)
        Me.BtnFilter.TabIndex = 20
        Me.BtnFilter.Text = "Filter"
        Me.BtnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFilter.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnDelete.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_delete_211
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(1143, 464)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(102, 34)
        Me.BtnDelete.TabIndex = 11
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnEdit.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_edit_211
        Me.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEdit.Location = New System.Drawing.Point(1035, 464)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(102, 34)
        Me.BtnEdit.TabIndex = 9
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Button2.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_search_13
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button2.Location = New System.Drawing.Point(422, 57)
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
        Me.Button1.Location = New System.Drawing.Point(927, 464)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 34)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SteelBlue
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1255, 39)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "RESEARCH REPOSITORY MANAGER"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'count
        '
        Me.count.DataPropertyName = "no#"
        Me.count.Frozen = True
        Me.count.HeaderText = "count"
        Me.count.Name = "count"
        Me.count.ReadOnly = True
        Me.count.Visible = False
        '
        'Control_No
        '
        Me.Control_No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Control_No.DataPropertyName = "sw_id"
        Me.Control_No.Frozen = True
        Me.Control_No.HeaderText = "Control No"
        Me.Control_No.MinimumWidth = 70
        Me.Control_No.Name = "Control_No"
        Me.Control_No.ReadOnly = True
        '
        'research_agenda
        '
        Me.research_agenda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.research_agenda.DataPropertyName = "research_agenda"
        Me.research_agenda.FillWeight = 85.10638!
        Me.research_agenda.HeaderText = "Research Agenda"
        Me.research_agenda.MinimumWidth = 200
        Me.research_agenda.Name = "research_agenda"
        Me.research_agenda.ReadOnly = True
        Me.research_agenda.Width = 300
        '
        'title
        '
        Me.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.title.DataPropertyName = "title"
        Me.title.FillWeight = 85.10638!
        Me.title.HeaderText = "Title"
        Me.title.MinimumWidth = 200
        Me.title.Name = "title"
        Me.title.ReadOnly = True
        Me.title.Width = 300
        '
        'col_btn
        '
        Me.col_btn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.col_btn.DataPropertyName = "display_text"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_btn.DefaultCellStyle = DataGridViewCellStyle3
        Me.col_btn.FillWeight = 85.10638!
        Me.col_btn.HeaderText = "Abstract"
        Me.col_btn.MinimumWidth = 100
        Me.col_btn.Name = "col_btn"
        Me.col_btn.ReadOnly = True
        Me.col_btn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_btn.ToolTipText = "Open Abstract File"
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column1.DataPropertyName = "whole_file_text"
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column1.HeaderText = "File"
        Me.Column1.MinimumWidth = 100
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column3.DataPropertyName = "authors_and_co_authors"
        Me.Column3.FillWeight = 85.10638!
        Me.Column3.HeaderText = "Authors / Co-Authors"
        Me.Column3.MinimumWidth = 200
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column5.DataPropertyName = "auth_and_co_auth_deg_prog"
        Me.Column5.FillWeight = 85.10638!
        Me.Column5.HeaderText = "Degree Program"
        Me.Column5.MinimumWidth = 250
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 300
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column6.DataPropertyName = "auth_and_co_auth_role"
        Me.Column6.FillWeight = 85.10638!
        Me.Column6.HeaderText = "Role"
        Me.Column6.MinimumWidth = 70
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'semester
        '
        Me.semester.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.semester.DataPropertyName = "semester"
        Me.semester.FillWeight = 85.10638!
        Me.semester.HeaderText = "Semester"
        Me.semester.MinimumWidth = 100
        Me.semester.Name = "semester"
        Me.semester.ReadOnly = True
        '
        'school_year
        '
        Me.school_year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.school_year.DataPropertyName = "school_year"
        Me.school_year.HeaderText = "School Year"
        Me.school_year.MinimumWidth = 120
        Me.school_year.Name = "school_year"
        Me.school_year.ReadOnly = True
        Me.school_year.Width = 120
        '
        'status_ongoing_completed
        '
        Me.status_ongoing_completed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.status_ongoing_completed.DataPropertyName = "status_ongoing_completed"
        Me.status_ongoing_completed.FillWeight = 204.2553!
        Me.status_ongoing_completed.HeaderText = "Status"
        Me.status_ongoing_completed.MinimumWidth = 90
        Me.status_ongoing_completed.Name = "status_ongoing_completed"
        Me.status_ongoing_completed.ReadOnly = True
        Me.status_ongoing_completed.Width = 90
        '
        'published
        '
        Me.published.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.published.DataPropertyName = "published"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.published.DefaultCellStyle = DataGridViewCellStyle5
        Me.published.HeaderText = "Published"
        Me.published.MinimumWidth = 70
        Me.published.Name = "published"
        Me.published.ReadOnly = True
        Me.published.Width = 80
        '
        'presented
        '
        Me.presented.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.presented.DataPropertyName = "presented"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.presented.DefaultCellStyle = DataGridViewCellStyle6
        Me.presented.HeaderText = "Presented"
        Me.presented.MinimumWidth = 70
        Me.presented.Name = "presented"
        Me.presented.ReadOnly = True
        Me.presented.Width = 80
        '
        'ResearchRepoManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1255, 513)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.BtnRemoveSelection)
        Me.Controls.Add(Me.BtnFilter)
        Me.Controls.Add(Me.PnlFilter)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DgvSwData)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Name = "ResearchRepoManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RESEARCH REPOSITORY MANAGER"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PnlFilter.ResumeLayout(False)
        Me.PnlFilter.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvSwData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents DtpRange1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DtpRange2 As DateTimePicker
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
    Friend WithEvents CbxAllRole As CheckBox
    Friend WithEvents DgvSwData As DataGridView
    Friend WithEvents BtnRemoveSelection As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents count As DataGridViewTextBoxColumn
    Friend WithEvents Control_No As DataGridViewTextBoxColumn
    Friend WithEvents research_agenda As DataGridViewTextBoxColumn
    Friend WithEvents title As DataGridViewTextBoxColumn
    Friend WithEvents col_btn As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents semester As DataGridViewTextBoxColumn
    Friend WithEvents school_year As DataGridViewTextBoxColumn
    Friend WithEvents status_ongoing_completed As DataGridViewTextBoxColumn
    Friend WithEvents published As DataGridViewTextBoxColumn
    Friend WithEvents presented As DataGridViewTextBoxColumn
End Class
