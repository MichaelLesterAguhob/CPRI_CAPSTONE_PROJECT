<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportBorrowingAndReturning
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
        Me.CrvBaR = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CrvBaR
        '
        Me.CrvBaR.ActiveViewIndex = -1
        Me.CrvBaR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvBaR.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrvBaR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrvBaR.Location = New System.Drawing.Point(0, 0)
        Me.CrvBaR.Name = "CrvBaR"
        Me.CrvBaR.Size = New System.Drawing.Size(1042, 526)
        Me.CrvBaR.TabIndex = 3
        Me.CrvBaR.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'ReportBorrowingAndReturning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 526)
        Me.Controls.Add(Me.CrvBaR)
        Me.Name = "ReportBorrowingAndReturning"
        Me.Text = "Borrowing And Returning Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CrvBaR As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
