<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportRcf
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
        Me.CrvRCF = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CrvRCF
        '
        Me.CrvRCF.ActiveViewIndex = -1
        Me.CrvRCF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvRCF.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrvRCF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrvRCF.Location = New System.Drawing.Point(0, 0)
        Me.CrvRCF.Name = "CrvRCF"
        Me.CrvRCF.Size = New System.Drawing.Size(1224, 521)
        Me.CrvRCF.TabIndex = 0
        Me.CrvRCF.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'ReportRcf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1224, 521)
        Me.Controls.Add(Me.CrvRCF)
        Me.Name = "ReportRcf"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReportRcf"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CrvRCF As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
