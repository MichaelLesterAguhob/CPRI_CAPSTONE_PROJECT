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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DtTo = New System.Windows.Forms.DateTimePicker()
        Me.PnlFilter = New System.Windows.Forms.Panel()
        Me.LblFilteredDate3 = New System.Windows.Forms.Label()
        Me.BtnClearDate3 = New System.Windows.Forms.Button()
        Me.DtFrom3 = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DtTo3 = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.LblFilteredDate2 = New System.Windows.Forms.Label()
        Me.BtnClearDate2 = New System.Windows.Forms.Button()
        Me.DtFrom2 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DtTo2 = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.LblFilteredDate = New System.Windows.Forms.Label()
        Me.BtnClearDate = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.BtnClearStatus = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RdCompleted = New System.Windows.Forms.RadioButton()
        Me.RdOngoing = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnClearPublished = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RdPubNo = New System.Windows.Forms.RadioButton()
        Me.RdPubYes = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnClearPresented = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RdPreNo = New System.Windows.Forms.RadioButton()
        Me.RdPreYes = New System.Windows.Forms.RadioButton()
        Me.BtnResetFilter = New System.Windows.Forms.Button()
        Me.BtnApplyFilter = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnCloseFilter = New System.Windows.Forms.Button()
        Me.TmOpenFilter = New System.Windows.Forms.Timer(Me.components)
        Me.TmCloseFilter = New System.Windows.Forms.Timer(Me.components)
        Me.DgvSwData = New System.Windows.Forms.DataGridView()
        Me.count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Control_No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.research_agenda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_btn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.semester = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.school_year = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status_ongoing_completed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.published = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presented = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnRemoveSelection = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblSrchFnd = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.PnlFilter.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvSwData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.ForeColor = System.Drawing.Color.Gray
        Me.TxtSearch.Location = New System.Drawing.Point(91, 57)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(325, 21)
        Me.TxtSearch.TabIndex = 3
        Me.TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc."
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
        'DtFrom
        '
        Me.DtFrom.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtFrom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtFrom.Location = New System.Drawing.Point(72, 64)
        Me.DtFrom.Name = "DtFrom"
        Me.DtFrom.Size = New System.Drawing.Size(123, 20)
        Me.DtFrom.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "From"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(205, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "To"
        '
        'DtTo
        '
        Me.DtTo.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtTo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtTo.Enabled = False
        Me.DtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtTo.Location = New System.Drawing.Point(236, 64)
        Me.DtTo.Name = "DtTo"
        Me.DtTo.Size = New System.Drawing.Size(123, 20)
        Me.DtTo.TabIndex = 15
        '
        'PnlFilter
        '
        Me.PnlFilter.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.PnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlFilter.Controls.Add(Me.LblFilteredDate3)
        Me.PnlFilter.Controls.Add(Me.BtnClearDate3)
        Me.PnlFilter.Controls.Add(Me.DtFrom3)
        Me.PnlFilter.Controls.Add(Me.Label14)
        Me.PnlFilter.Controls.Add(Me.DtTo3)
        Me.PnlFilter.Controls.Add(Me.Label15)
        Me.PnlFilter.Controls.Add(Me.Label16)
        Me.PnlFilter.Controls.Add(Me.LblFilteredDate2)
        Me.PnlFilter.Controls.Add(Me.BtnClearDate2)
        Me.PnlFilter.Controls.Add(Me.DtFrom2)
        Me.PnlFilter.Controls.Add(Me.Label7)
        Me.PnlFilter.Controls.Add(Me.DtTo2)
        Me.PnlFilter.Controls.Add(Me.Label11)
        Me.PnlFilter.Controls.Add(Me.Label12)
        Me.PnlFilter.Controls.Add(Me.LblFilteredDate)
        Me.PnlFilter.Controls.Add(Me.BtnClearDate)
        Me.PnlFilter.Controls.Add(Me.Panel3)
        Me.PnlFilter.Controls.Add(Me.Panel2)
        Me.PnlFilter.Controls.Add(Me.Panel1)
        Me.PnlFilter.Controls.Add(Me.DtFrom)
        Me.PnlFilter.Controls.Add(Me.Label4)
        Me.PnlFilter.Controls.Add(Me.DtTo)
        Me.PnlFilter.Controls.Add(Me.Label3)
        Me.PnlFilter.Controls.Add(Me.BtnResetFilter)
        Me.PnlFilter.Controls.Add(Me.BtnApplyFilter)
        Me.PnlFilter.Controls.Add(Me.Label6)
        Me.PnlFilter.Controls.Add(Me.BtnCloseFilter)
        Me.PnlFilter.Controls.Add(Me.Label2)
        Me.PnlFilter.ForeColor = System.Drawing.Color.Black
        Me.PnlFilter.Location = New System.Drawing.Point(87, 57)
        Me.PnlFilter.Name = "PnlFilter"
        Me.PnlFilter.Size = New System.Drawing.Size(0, 0)
        Me.PnlFilter.TabIndex = 19
        '
        'LblFilteredDate3
        '
        Me.LblFilteredDate3.AutoSize = True
        Me.LblFilteredDate3.Location = New System.Drawing.Point(71, 211)
        Me.LblFilteredDate3.Name = "LblFilteredDate3"
        Me.LblFilteredDate3.Size = New System.Drawing.Size(0, 13)
        Me.LblFilteredDate3.TabIndex = 67
        '
        'BtnClearDate3
        '
        Me.BtnClearDate3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearDate3.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearDate3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearDate3.FlatAppearance.BorderSize = 0
        Me.BtnClearDate3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearDate3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearDate3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearDate3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearDate3.Location = New System.Drawing.Point(-33, 188)
        Me.BtnClearDate3.Name = "BtnClearDate3"
        Me.BtnClearDate3.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearDate3.TabIndex = 66
        Me.BtnClearDate3.Text = "X"
        Me.BtnClearDate3.UseVisualStyleBackColor = False
        Me.BtnClearDate3.Visible = False
        '
        'DtFrom3
        '
        Me.DtFrom3.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtFrom3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtFrom3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtFrom3.Location = New System.Drawing.Point(72, 186)
        Me.DtFrom3.Name = "DtFrom3"
        Me.DtFrom3.Size = New System.Drawing.Size(123, 20)
        Me.DtFrom3.TabIndex = 61
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(205, 190)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 13)
        Me.Label14.TabIndex = 63
        Me.Label14.Text = "To"
        '
        'DtTo3
        '
        Me.DtTo3.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtTo3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtTo3.Enabled = False
        Me.DtTo3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtTo3.Location = New System.Drawing.Point(236, 186)
        Me.DtTo3.Name = "DtTo3"
        Me.DtTo3.Size = New System.Drawing.Size(123, 20)
        Me.DtTo3.TabIndex = 64
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(32, 190)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(30, 13)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "From"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(24, 167)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(103, 13)
        Me.Label16.TabIndex = 65
        Me.Label16.Text = "Presented Date :"
        '
        'LblFilteredDate2
        '
        Me.LblFilteredDate2.AutoSize = True
        Me.LblFilteredDate2.Location = New System.Drawing.Point(71, 142)
        Me.LblFilteredDate2.Name = "LblFilteredDate2"
        Me.LblFilteredDate2.Size = New System.Drawing.Size(0, 13)
        Me.LblFilteredDate2.TabIndex = 60
        '
        'BtnClearDate2
        '
        Me.BtnClearDate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearDate2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearDate2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearDate2.FlatAppearance.BorderSize = 0
        Me.BtnClearDate2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearDate2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearDate2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearDate2.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearDate2.Location = New System.Drawing.Point(-33, 121)
        Me.BtnClearDate2.Name = "BtnClearDate2"
        Me.BtnClearDate2.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearDate2.TabIndex = 59
        Me.BtnClearDate2.Text = "X"
        Me.BtnClearDate2.UseVisualStyleBackColor = False
        Me.BtnClearDate2.Visible = False
        '
        'DtFrom2
        '
        Me.DtFrom2.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtFrom2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtFrom2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtFrom2.Location = New System.Drawing.Point(72, 119)
        Me.DtFrom2.Name = "DtFrom2"
        Me.DtFrom2.Size = New System.Drawing.Size(123, 20)
        Me.DtFrom2.TabIndex = 54
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(205, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(20, 13)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "To"
        '
        'DtTo2
        '
        Me.DtTo2.CalendarMonthBackground = System.Drawing.Color.DarkGray
        Me.DtTo2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DtTo2.Enabled = False
        Me.DtTo2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtTo2.Location = New System.Drawing.Point(236, 119)
        Me.DtTo2.Name = "DtTo2"
        Me.DtTo2.Size = New System.Drawing.Size(123, 20)
        Me.DtTo2.TabIndex = 57
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(32, 123)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 13)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "From"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(24, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 13)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "Published Date :"
        '
        'LblFilteredDate
        '
        Me.LblFilteredDate.AutoSize = True
        Me.LblFilteredDate.Location = New System.Drawing.Point(71, 87)
        Me.LblFilteredDate.Name = "LblFilteredDate"
        Me.LblFilteredDate.Size = New System.Drawing.Size(0, 13)
        Me.LblFilteredDate.TabIndex = 40
        '
        'BtnClearDate
        '
        Me.BtnClearDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearDate.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearDate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearDate.FlatAppearance.BorderSize = 0
        Me.BtnClearDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearDate.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearDate.Location = New System.Drawing.Point(-30, 64)
        Me.BtnClearDate.Name = "BtnClearDate"
        Me.BtnClearDate.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearDate.TabIndex = 39
        Me.BtnClearDate.Text = "X"
        Me.BtnClearDate.UseVisualStyleBackColor = False
        Me.BtnClearDate.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.BtnClearStatus)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.RdCompleted)
        Me.Panel3.Controls.Add(Me.RdOngoing)
        Me.Panel3.Location = New System.Drawing.Point(16, 234)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(276, 29)
        Me.Panel3.TabIndex = 36
        '
        'BtnClearStatus
        '
        Me.BtnClearStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearStatus.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearStatus.FlatAppearance.BorderSize = 0
        Me.BtnClearStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearStatus.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearStatus.Location = New System.Drawing.Point(249, 4)
        Me.BtnClearStatus.Name = "BtnClearStatus"
        Me.BtnClearStatus.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearStatus.TabIndex = 38
        Me.BtnClearStatus.Text = "X"
        Me.BtnClearStatus.UseVisualStyleBackColor = False
        Me.BtnClearStatus.Visible = False
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
        'RdCompleted
        '
        Me.RdCompleted.AutoSize = True
        Me.RdCompleted.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdCompleted.Location = New System.Drawing.Point(166, 5)
        Me.RdCompleted.Name = "RdCompleted"
        Me.RdCompleted.Size = New System.Drawing.Size(75, 17)
        Me.RdCompleted.TabIndex = 29
        Me.RdCompleted.TabStop = True
        Me.RdCompleted.Text = "Completed"
        Me.RdCompleted.UseVisualStyleBackColor = True
        '
        'RdOngoing
        '
        Me.RdOngoing.AutoSize = True
        Me.RdOngoing.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdOngoing.Location = New System.Drawing.Point(97, 5)
        Me.RdOngoing.Name = "RdOngoing"
        Me.RdOngoing.Size = New System.Drawing.Size(65, 17)
        Me.RdOngoing.TabIndex = 28
        Me.RdOngoing.TabStop = True
        Me.RdOngoing.Text = "Ongoing"
        Me.RdOngoing.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.BtnClearPublished)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.RdPubNo)
        Me.Panel2.Controls.Add(Me.RdPubYes)
        Me.Panel2.Location = New System.Drawing.Point(16, 269)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(276, 29)
        Me.Panel2.TabIndex = 35
        '
        'BtnClearPublished
        '
        Me.BtnClearPublished.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearPublished.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearPublished.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearPublished.FlatAppearance.BorderSize = 0
        Me.BtnClearPublished.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearPublished.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearPublished.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearPublished.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearPublished.Location = New System.Drawing.Point(249, 4)
        Me.BtnClearPublished.Name = "BtnClearPublished"
        Me.BtnClearPublished.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearPublished.TabIndex = 37
        Me.BtnClearPublished.Text = "X"
        Me.BtnClearPublished.UseVisualStyleBackColor = False
        Me.BtnClearPublished.Visible = False
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
        'RdPubNo
        '
        Me.RdPubNo.AutoSize = True
        Me.RdPubNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdPubNo.Location = New System.Drawing.Point(97, 5)
        Me.RdPubNo.Name = "RdPubNo"
        Me.RdPubNo.Size = New System.Drawing.Size(39, 17)
        Me.RdPubNo.TabIndex = 33
        Me.RdPubNo.TabStop = True
        Me.RdPubNo.Text = "No"
        Me.RdPubNo.UseVisualStyleBackColor = True
        '
        'RdPubYes
        '
        Me.RdPubYes.AutoSize = True
        Me.RdPubYes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdPubYes.Location = New System.Drawing.Point(166, 5)
        Me.RdPubYes.Name = "RdPubYes"
        Me.RdPubYes.Size = New System.Drawing.Size(43, 17)
        Me.RdPubYes.TabIndex = 32
        Me.RdPubYes.TabStop = True
        Me.RdPubYes.Text = "Yes"
        Me.RdPubYes.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.BtnClearPresented)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.RdPreNo)
        Me.Panel1.Controls.Add(Me.RdPreYes)
        Me.Panel1.Location = New System.Drawing.Point(16, 304)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(276, 29)
        Me.Panel1.TabIndex = 34
        '
        'BtnClearPresented
        '
        Me.BtnClearPresented.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearPresented.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.BtnClearPresented.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClearPresented.FlatAppearance.BorderSize = 0
        Me.BtnClearPresented.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearPresented.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearPresented.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnClearPresented.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnClearPresented.Location = New System.Drawing.Point(249, 4)
        Me.BtnClearPresented.Name = "BtnClearPresented"
        Me.BtnClearPresented.Size = New System.Drawing.Size(20, 20)
        Me.BtnClearPresented.TabIndex = 38
        Me.BtnClearPresented.Text = "X"
        Me.BtnClearPresented.UseVisualStyleBackColor = False
        Me.BtnClearPresented.Visible = False
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
        'RdPreNo
        '
        Me.RdPreNo.AutoSize = True
        Me.RdPreNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdPreNo.Location = New System.Drawing.Point(97, 5)
        Me.RdPreNo.Name = "RdPreNo"
        Me.RdPreNo.Size = New System.Drawing.Size(39, 17)
        Me.RdPreNo.TabIndex = 33
        Me.RdPreNo.TabStop = True
        Me.RdPreNo.Text = "No"
        Me.RdPreNo.UseVisualStyleBackColor = True
        '
        'RdPreYes
        '
        Me.RdPreYes.AutoSize = True
        Me.RdPreYes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RdPreYes.Location = New System.Drawing.Point(166, 5)
        Me.RdPreYes.Name = "RdPreYes"
        Me.RdPreYes.Size = New System.Drawing.Size(43, 17)
        Me.RdPreYes.TabIndex = 32
        Me.RdPreYes.TabStop = True
        Me.RdPreYes.Text = "Yes"
        Me.RdPreYes.UseVisualStyleBackColor = True
        '
        'BtnResetFilter
        '
        Me.BtnResetFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnResetFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnResetFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnResetFilter.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_reset_14
        Me.BtnResetFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnResetFilter.Location = New System.Drawing.Point(208, 369)
        Me.BtnResetFilter.Name = "BtnResetFilter"
        Me.BtnResetFilter.Size = New System.Drawing.Size(61, 23)
        Me.BtnResetFilter.TabIndex = 27
        Me.BtnResetFilter.Text = "Reset"
        Me.BtnResetFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnResetFilter.UseVisualStyleBackColor = False
        '
        'BtnApplyFilter
        '
        Me.BtnApplyFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnApplyFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnApplyFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApplyFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnApplyFilter.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_filtered_file_14
        Me.BtnApplyFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnApplyFilter.Location = New System.Drawing.Point(114, 369)
        Me.BtnApplyFilter.Name = "BtnApplyFilter"
        Me.BtnApplyFilter.Size = New System.Drawing.Size(61, 23)
        Me.BtnApplyFilter.TabIndex = 26
        Me.BtnApplyFilter.Text = "Apply"
        Me.BtnApplyFilter.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.BtnApplyFilter.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(24, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Completed Date :"
        '
        'BtnCloseFilter
        '
        Me.BtnCloseFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCloseFilter.BackColor = System.Drawing.Color.Red
        Me.BtnCloseFilter.Cursor = System.Windows.Forms.Cursors.Hand
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
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DgvSwData.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.DgvSwData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvSwData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvSwData.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DgvSwData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.Padding = New System.Windows.Forms.Padding(0, 10, 0, 10)
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DgvSwData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSwData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.count, Me.Control_No, Me.research_agenda, Me.title, Me.col_btn, Me.Column1, Me.Column3, Me.Column2, Me.Column5, Me.Column6, Me.semester, Me.school_year, Me.status_ongoing_completed, Me.Column4, Me.published, Me.Column9, Me.presented, Me.Column10, Me.Column7, Me.Column8})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.DefaultCellStyle = DataGridViewCellStyle17
        Me.DgvSwData.Location = New System.Drawing.Point(14, 85)
        Me.DgvSwData.MultiSelect = False
        Me.DgvSwData.Name = "DgvSwData"
        Me.DgvSwData.ReadOnly = True
        Me.DgvSwData.RowHeadersVisible = False
        DataGridViewCellStyle18.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.RowsDefaultCellStyle = DataGridViewCellStyle18
        Me.DgvSwData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSwData.ShowRowErrors = False
        Me.DgvSwData.Size = New System.Drawing.Size(1326, 581)
        Me.DgvSwData.TabIndex = 21
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
        Me.Control_No.HeaderText = "CONTROL NO."
        Me.Control_No.MinimumWidth = 100
        Me.Control_No.Name = "Control_No"
        Me.Control_No.ReadOnly = True
        '
        'research_agenda
        '
        Me.research_agenda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.research_agenda.DataPropertyName = "research_agenda"
        Me.research_agenda.FillWeight = 85.10638!
        Me.research_agenda.HeaderText = "RESEARCH AGENDA"
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
        Me.title.HeaderText = "TITLE"
        Me.title.MinimumWidth = 200
        Me.title.Name = "title"
        Me.title.ReadOnly = True
        Me.title.Width = 300
        '
        'col_btn
        '
        Me.col_btn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.col_btn.DataPropertyName = "display_text"
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_btn.DefaultCellStyle = DataGridViewCellStyle12
        Me.col_btn.FillWeight = 85.10638!
        Me.col_btn.HeaderText = "ABSTRACT"
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
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle13
        Me.Column1.HeaderText = "FILE"
        Me.Column1.MinimumWidth = 100
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column3.DataPropertyName = "authors"
        Me.Column3.FillWeight = 85.10638!
        Me.Column3.HeaderText = "AUTHOR"
        Me.Column3.MinimumWidth = 200
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 200
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column2.DataPropertyName = "co_authors"
        Me.Column2.HeaderText = "CO-AUTHORS"
        Me.Column2.MinimumWidth = 200
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 200
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column5.DataPropertyName = "auth_and_co_auth_deg_prog"
        Me.Column5.FillWeight = 85.10638!
        Me.Column5.HeaderText = "DEGREE PROGRAM"
        Me.Column5.MinimumWidth = 200
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 200
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column6.DataPropertyName = "auth_and_co_auth_role"
        Me.Column6.FillWeight = 85.10638!
        Me.Column6.HeaderText = "ROLE"
        Me.Column6.MinimumWidth = 70
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'semester
        '
        Me.semester.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.semester.DataPropertyName = "semester"
        Me.semester.FillWeight = 85.10638!
        Me.semester.HeaderText = "SEMESTER"
        Me.semester.MinimumWidth = 100
        Me.semester.Name = "semester"
        Me.semester.ReadOnly = True
        '
        'school_year
        '
        Me.school_year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.school_year.DataPropertyName = "school_year"
        Me.school_year.HeaderText = "SCHOOL YEAR"
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
        Me.status_ongoing_completed.HeaderText = "STATUS"
        Me.status_ongoing_completed.MinimumWidth = 90
        Me.status_ongoing_completed.Name = "status_ongoing_completed"
        Me.status_ongoing_completed.ReadOnly = True
        Me.status_ongoing_completed.Width = 90
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column4.DataPropertyName = "date_completed"
        Me.Column4.HeaderText = "DATE COMPLETED (MM-DD-YYYY)"
        Me.Column4.MinimumWidth = 155
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 155
        '
        'published
        '
        Me.published.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.published.DataPropertyName = "published"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.published.DefaultCellStyle = DataGridViewCellStyle14
        Me.published.HeaderText = "PUBLISHED"
        Me.published.MinimumWidth = 90
        Me.published.Name = "published"
        Me.published.ReadOnly = True
        Me.published.Visible = False
        Me.published.Width = 90
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column9.DataPropertyName = "date_published"
        Me.Column9.HeaderText = "DATE PUBLISHED"
        Me.Column9.MinimumWidth = 95
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 95
        '
        'presented
        '
        Me.presented.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.presented.DataPropertyName = "presented"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.presented.DefaultCellStyle = DataGridViewCellStyle15
        Me.presented.HeaderText = "PRESENTED"
        Me.presented.MinimumWidth = 95
        Me.presented.Name = "presented"
        Me.presented.ReadOnly = True
        Me.presented.Visible = False
        Me.presented.Width = 95
        '
        'Column10
        '
        Me.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column10.DataPropertyName = "date_presented"
        Me.Column10.HeaderText = "DATE PRESENTED"
        Me.Column10.MinimumWidth = 95
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 95
        '
        'Column7
        '
        Me.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column7.DataPropertyName = "quantity"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column7.DefaultCellStyle = DataGridViewCellStyle16
        Me.Column7.HeaderText = "COPIES"
        Me.Column7.MinimumWidth = 60
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Visible = False
        Me.Column7.Width = 80
        '
        'Column8
        '
        Me.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column8.DataPropertyName = "location"
        Me.Column8.HeaderText = "LOCATION"
        Me.Column8.MinimumWidth = 120
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 120
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
        Me.BtnRemoveSelection.Location = New System.Drawing.Point(1238, 54)
        Me.BtnRemoveSelection.Name = "BtnRemoveSelection"
        Me.BtnRemoveSelection.Size = New System.Drawing.Size(102, 24)
        Me.BtnRemoveSelection.TabIndex = 22
        Me.BtnRemoveSelection.Text = "Remove Selection"
        Me.BtnRemoveSelection.UseVisualStyleBackColor = False
        Me.BtnRemoveSelection.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button3.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_view_21
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(914, 680)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(102, 34)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "VIEW"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'BtnFilter
        '
        Me.BtnFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFilter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnFilter.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_filter_14
        Me.BtnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnFilter.Location = New System.Drawing.Point(14, 57)
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
        Me.BtnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnDelete.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_delete_211
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(1238, 680)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(102, 34)
        Me.BtnDelete.TabIndex = 11
        Me.BtnDelete.Text = "DELETE"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnEdit.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_edit_211
        Me.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEdit.Location = New System.Drawing.Point(1130, 680)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(102, 34)
        Me.BtnEdit.TabIndex = 9
        Me.BtnEdit.Text = "EDIT"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnSearch
        '
        Me.BtnSearch.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BtnSearch.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_search_13
        Me.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnSearch.Location = New System.Drawing.Point(422, 57)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(65, 21)
        Me.BtnSearch.TabIndex = 4
        Me.BtnSearch.Text = "Search"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_add_21
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(1022, 680)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 34)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "ADD"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SteelBlue
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1350, 48)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "RESEARCH REPOSITORY MANAGER"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSrchFnd
        '
        Me.LblSrchFnd.AutoSize = True
        Me.LblSrchFnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSrchFnd.ForeColor = System.Drawing.Color.DarkBlue
        Me.LblSrchFnd.Location = New System.Drawing.Point(507, 61)
        Me.LblSrchFnd.Name = "LblSrchFnd"
        Me.LblSrchFnd.Size = New System.Drawing.Size(0, 15)
        Me.LblSrchFnd.TabIndex = 25
        Me.LblSrchFnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Interval = 1200
        '
        'BtnReport
        '
        Me.BtnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnReport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReport.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReport.Location = New System.Drawing.Point(14, 683)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(199, 34)
        Me.BtnReport.TabIndex = 26
        Me.BtnReport.Text = "GENERATE REPORT"
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'ResearchRepoManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnReport)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.BtnRemoveSelection)
        Me.Controls.Add(Me.BtnFilter)
        Me.Controls.Add(Me.PnlFilter)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DgvSwData)
        Me.Controls.Add(Me.LblSrchFnd)
        Me.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MinimumSize = New System.Drawing.Size(980, 580)
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
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents DtFrom As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DtTo As DateTimePicker
    Friend WithEvents PnlFilter As Panel
    Friend WithEvents BtnFilter As Button
    Friend WithEvents TmOpenFilter As Timer
    Friend WithEvents BtnCloseFilter As Button
    Friend WithEvents TmCloseFilter As Timer
    Friend WithEvents BtnResetFilter As Button
    Friend WithEvents BtnApplyFilter As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents RdPreNo As RadioButton
    Friend WithEvents RdPreYes As RadioButton
    Friend WithEvents RdCompleted As RadioButton
    Friend WithEvents RdOngoing As RadioButton
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents RdPubNo As RadioButton
    Friend WithEvents RdPubYes As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DgvSwData As DataGridView
    Friend WithEvents BtnRemoveSelection As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LblSrchFnd As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents BtnClearStatus As Button
    Friend WithEvents BtnClearPublished As Button
    Friend WithEvents BtnClearPresented As Button
    Friend WithEvents BtnClearDate As Button
    Friend WithEvents LblFilteredDate As Label
    Friend WithEvents BtnClearDate3 As Button
    Friend WithEvents DtFrom3 As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents DtTo3 As DateTimePicker
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents LblFilteredDate2 As Label
    Friend WithEvents BtnClearDate2 As Button
    Friend WithEvents DtFrom2 As DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents DtTo2 As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents LblFilteredDate3 As Label
    Friend WithEvents count As DataGridViewTextBoxColumn
    Friend WithEvents Control_No As DataGridViewTextBoxColumn
    Friend WithEvents research_agenda As DataGridViewTextBoxColumn
    Friend WithEvents title As DataGridViewTextBoxColumn
    Friend WithEvents col_btn As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents semester As DataGridViewTextBoxColumn
    Friend WithEvents school_year As DataGridViewTextBoxColumn
    Friend WithEvents status_ongoing_completed As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents published As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents presented As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents BtnReport As Button
End Class
