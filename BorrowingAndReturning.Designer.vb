<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BorrowingAndReturning
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnOverduesBooks = New System.Windows.Forms.Button()
        Me.BtnReturnedBooks = New System.Windows.Forms.Button()
        Me.BtnBorrowedBooks = New System.Windows.Forms.Button()
        Me.BtnBorrower = New System.Windows.Forms.Button()
        Me.BtnBooks = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1246, 100)
        Me.Panel1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Old English Text MT", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(122, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(617, 44)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "CPRI Borrowing and Returning"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.logo
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 88)
        Me.Label1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel2.Controls.Add(Me.BtnOverduesBooks)
        Me.Panel2.Controls.Add(Me.BtnReturnedBooks)
        Me.Panel2.Controls.Add(Me.BtnBorrowedBooks)
        Me.Panel2.Controls.Add(Me.BtnBorrower)
        Me.Panel2.Controls.Add(Me.BtnBooks)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 100)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(214, 561)
        Me.Panel2.TabIndex = 3
        '
        'BtnOverduesBooks
        '
        Me.BtnOverduesBooks.BackColor = System.Drawing.Color.Transparent
        Me.BtnOverduesBooks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnOverduesBooks.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnOverduesBooks.FlatAppearance.BorderSize = 0
        Me.BtnOverduesBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOverduesBooks.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOverduesBooks.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.overdue__1_
        Me.BtnOverduesBooks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnOverduesBooks.Location = New System.Drawing.Point(0, 456)
        Me.BtnOverduesBooks.Name = "BtnOverduesBooks"
        Me.BtnOverduesBooks.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.BtnOverduesBooks.Size = New System.Drawing.Size(214, 114)
        Me.BtnOverduesBooks.TabIndex = 6
        Me.BtnOverduesBooks.Text = "     Overdues" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     Books"
        Me.BtnOverduesBooks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnOverduesBooks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnOverduesBooks.UseVisualStyleBackColor = False
        '
        'BtnReturnedBooks
        '
        Me.BtnReturnedBooks.BackColor = System.Drawing.Color.Transparent
        Me.BtnReturnedBooks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnReturnedBooks.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnReturnedBooks.FlatAppearance.BorderSize = 0
        Me.BtnReturnedBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReturnedBooks.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReturnedBooks.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.book
        Me.BtnReturnedBooks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReturnedBooks.Location = New System.Drawing.Point(0, 342)
        Me.BtnReturnedBooks.Name = "BtnReturnedBooks"
        Me.BtnReturnedBooks.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.BtnReturnedBooks.Size = New System.Drawing.Size(214, 114)
        Me.BtnReturnedBooks.TabIndex = 5
        Me.BtnReturnedBooks.Text = "   Returned " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Books"
        Me.BtnReturnedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReturnedBooks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnReturnedBooks.UseVisualStyleBackColor = False
        '
        'BtnBorrowedBooks
        '
        Me.BtnBorrowedBooks.BackColor = System.Drawing.Color.Transparent
        Me.BtnBorrowedBooks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnBorrowedBooks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnBorrowedBooks.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnBorrowedBooks.FlatAppearance.BorderSize = 0
        Me.BtnBorrowedBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBorrowedBooks.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBorrowedBooks.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.delivering
        Me.BtnBorrowedBooks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBorrowedBooks.Location = New System.Drawing.Point(0, 228)
        Me.BtnBorrowedBooks.Name = "BtnBorrowedBooks"
        Me.BtnBorrowedBooks.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.BtnBorrowedBooks.Size = New System.Drawing.Size(214, 114)
        Me.BtnBorrowedBooks.TabIndex = 4
        Me.BtnBorrowedBooks.Text = "   Borrowed " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Books"
        Me.BtnBorrowedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBorrowedBooks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnBorrowedBooks.UseVisualStyleBackColor = False
        '
        'BtnBorrower
        '
        Me.BtnBorrower.BackColor = System.Drawing.Color.Transparent
        Me.BtnBorrower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnBorrower.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnBorrower.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnBorrower.FlatAppearance.BorderSize = 0
        Me.BtnBorrower.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBorrower.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBorrower.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.group
        Me.BtnBorrower.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBorrower.Location = New System.Drawing.Point(0, 114)
        Me.BtnBorrower.Name = "BtnBorrower"
        Me.BtnBorrower.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.BtnBorrower.Size = New System.Drawing.Size(214, 114)
        Me.BtnBorrower.TabIndex = 3
        Me.BtnBorrower.Text = "   Borrower"
        Me.BtnBorrower.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnBorrower.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnBorrower.UseVisualStyleBackColor = False
        '
        'BtnBooks
        '
        Me.BtnBooks.BackColor = System.Drawing.Color.Transparent
        Me.BtnBooks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BtnBooks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnBooks.Dock = System.Windows.Forms.DockStyle.Top
        Me.BtnBooks.FlatAppearance.BorderSize = 0
        Me.BtnBooks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBooks.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnBooks.Image = Global.CPRI_CAPSTONE_PROJECT.My.Resources.Resources.archive
        Me.BtnBooks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBooks.Location = New System.Drawing.Point(0, 0)
        Me.BtnBooks.Name = "BtnBooks"
        Me.BtnBooks.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.BtnBooks.Size = New System.Drawing.Size(214, 114)
        Me.BtnBooks.TabIndex = 2
        Me.BtnBooks.Text = "   Books"
        Me.BtnBooks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnBooks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnBooks.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(214, 100)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1032, 561)
        Me.TabControl1.TabIndex = 4
        Me.TabControl1.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DarkGray
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1024, 535)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Books"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Silver
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1024, 535)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Borrowers"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.DimGray
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1024, 535)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Borrowed Books"
        '
        'TabPage4
        '
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1024, 535)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Returned Books"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.Color.Wheat
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(1024, 535)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Overdues"
        '
        'BorrowingAndReturning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 661)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "BorrowingAndReturning"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BorrowingAndReturning"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BtnOverduesBooks As Button
    Friend WithEvents BtnReturnedBooks As Button
    Friend WithEvents BtnBorrowedBooks As Button
    Friend WithEvents BtnBorrower As Button
    Friend WithEvents BtnBooks As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
End Class
