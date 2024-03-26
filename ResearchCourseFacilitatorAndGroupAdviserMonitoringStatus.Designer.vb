<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnRemoveSelection = New System.Windows.Forms.Button()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.DgvSwData = New System.Windows.Forms.DataGridView()
        Me.LblSrchFnd = New System.Windows.Forms.Label()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.BtnAddRCFGARecord = New System.Windows.Forms.Button()
        CType(Me.DgvSwData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SteelBlue
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1255, 48)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "RCF AND RGA MONITORING STATUS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.BtnRemoveSelection.Location = New System.Drawing.Point(1140, 60)
        Me.BtnRemoveSelection.Name = "BtnRemoveSelection"
        Me.BtnRemoveSelection.Size = New System.Drawing.Size(102, 24)
        Me.BtnRemoveSelection.TabIndex = 33
        Me.BtnRemoveSelection.Text = "Remove Selection"
        Me.BtnRemoveSelection.UseVisualStyleBackColor = False
        Me.BtnRemoveSelection.Visible = False
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.ForeColor = System.Drawing.Color.Gray
        Me.TxtSearch.Location = New System.Drawing.Point(88, 63)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(325, 21)
        Me.TxtSearch.TabIndex = 27
        Me.TxtSearch.Text = "Search Title, Author, Keyword, Abstract, Etc."
        '
        'DgvSwData
        '
        Me.DgvSwData.AllowUserToAddRows = False
        Me.DgvSwData.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DgvSwData.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvSwData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvSwData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvSwData.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DgvSwData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(0, 10, 0, 10)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvSwData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSwData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column12, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.DefaultCellStyle = DataGridViewCellStyle3
        Me.DgvSwData.Location = New System.Drawing.Point(11, 91)
        Me.DgvSwData.MultiSelect = False
        Me.DgvSwData.Name = "DgvSwData"
        Me.DgvSwData.ReadOnly = True
        Me.DgvSwData.RowHeadersVisible = False
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSwData.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvSwData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSwData.ShowRowErrors = False
        Me.DgvSwData.Size = New System.Drawing.Size(1231, 365)
        Me.DgvSwData.TabIndex = 32
        '
        'LblSrchFnd
        '
        Me.LblSrchFnd.AutoSize = True
        Me.LblSrchFnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSrchFnd.ForeColor = System.Drawing.Color.DarkBlue
        Me.LblSrchFnd.Location = New System.Drawing.Point(504, 67)
        Me.LblSrchFnd.Name = "LblSrchFnd"
        Me.LblSrchFnd.Size = New System.Drawing.Size(0, 15)
        Me.LblSrchFnd.TabIndex = 35
        Me.LblSrchFnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Column12
        '
        Me.Column12.DataPropertyName = "no#"
        Me.Column12.HeaderText = "no"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Visible = False
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column1.DataPropertyName = "record_id"
        Me.Column1.HeaderText = "Record ID"
        Me.Column1.MinimumWidth = 105
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 105
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column2.DataPropertyName = "semester"
        Me.Column2.HeaderText = "Semester"
        Me.Column2.MinimumWidth = 110
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 110
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column3.DataPropertyName = "school_year"
        Me.Column3.HeaderText = "School Year"
        Me.Column3.MinimumWidth = 120
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column4.DataPropertyName = "stage"
        Me.Column4.HeaderText = "Stage"
        Me.Column4.MinimumWidth = 140
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 140
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column5.DataPropertyName = "name"
        Me.Column5.HeaderText = "Name"
        Me.Column5.MinimumWidth = 200
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 200
        '
        'Column6
        '
        Me.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column6.DataPropertyName = "college"
        Me.Column6.HeaderText = "College"
        Me.Column6.MinimumWidth = 200
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 200
        '
        'Column7
        '
        Me.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column7.DataPropertyName = "dept"
        Me.Column7.HeaderText = "Department"
        Me.Column7.MinimumWidth = 200
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 200
        '
        'Column8
        '
        Me.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column8.DataPropertyName = "status"
        Me.Column8.HeaderText = "Status"
        Me.Column8.MinimumWidth = 80
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 80
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column9.DataPropertyName = "role"
        Me.Column9.HeaderText = "Role"
        Me.Column9.MinimumWidth = 200
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 200
        '
        'Column10
        '
        Me.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column10.DataPropertyName = "r_rcf"
        Me.Column10.HeaderText = "Research Course Facilitator Requirements"
        Me.Column10.MinimumWidth = 200
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        Me.Column10.Width = 200
        '
        'Column11
        '
        Me.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column11.DataPropertyName = "r_rga"
        Me.Column11.HeaderText = "Research Group's Adviser Requirements"
        Me.Column11.MinimumWidth = 200
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 200
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button3.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_view_21
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(788, 470)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(118, 34)
        Me.Button3.TabIndex = 34
        Me.Button3.Text = "View Record"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.BtnFilter.Location = New System.Drawing.Point(11, 63)
        Me.BtnFilter.Name = "BtnFilter"
        Me.BtnFilter.Size = New System.Drawing.Size(67, 21)
        Me.BtnFilter.TabIndex = 31
        Me.BtnFilter.Text = "Filter"
        Me.BtnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFilter.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDelete.Enabled = False
        Me.BtnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnDelete.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_delete_211
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(1140, 470)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(102, 34)
        Me.BtnDelete.TabIndex = 30
        Me.BtnDelete.Text = "Delete"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEdit.Enabled = False
        Me.BtnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnEdit.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_edit_211
        Me.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEdit.Location = New System.Drawing.Point(1032, 470)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(102, 34)
        Me.BtnEdit.TabIndex = 29
        Me.BtnEdit.Text = "Edit"
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
        Me.BtnSearch.Location = New System.Drawing.Point(419, 63)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(65, 21)
        Me.BtnSearch.TabIndex = 28
        Me.BtnSearch.Text = "Search"
        Me.BtnSearch.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.BtnSearch.UseVisualStyleBackColor = False
        '
        'BtnAddRCFGARecord
        '
        Me.BtnAddRCFGARecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAddRCFGARecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddRCFGARecord.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BtnAddRCFGARecord.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.icons8_add_21
        Me.BtnAddRCFGARecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAddRCFGARecord.Location = New System.Drawing.Point(912, 470)
        Me.BtnAddRCFGARecord.Name = "BtnAddRCFGARecord"
        Me.BtnAddRCFGARecord.Size = New System.Drawing.Size(114, 34)
        Me.BtnAddRCFGARecord.TabIndex = 26
        Me.BtnAddRCFGARecord.Text = "Add Record"
        Me.BtnAddRCFGARecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnAddRCFGARecord.UseVisualStyleBackColor = True
        '
        'ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1255, 513)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.BtnRemoveSelection)
        Me.Controls.Add(Me.BtnFilter)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.BtnAddRCFGARecord)
        Me.Controls.Add(Me.DgvSwData)
        Me.Controls.Add(Me.LblSrchFnd)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ResearchCourseFacilitatorAndGroupAdviserMonitoringStatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MONITORING STATUS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DgvSwData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents BtnRemoveSelection As Button
    Friend WithEvents BtnFilter As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents BtnAddRCFGARecord As Button
    Friend WithEvents DgvSwData As DataGridView
    Friend WithEvents LblSrchFnd As Label
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
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
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
End Class
