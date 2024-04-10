<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportBorAndRet
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
        Me.CRV2 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.CrvBookList = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CRV2
        '
        Me.CRV2.ActiveViewIndex = -1
        Me.CRV2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRV2.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRV2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRV2.Location = New System.Drawing.Point(0, 0)
        Me.CRV2.Name = "CRV2"
        Me.CRV2.Size = New System.Drawing.Size(1246, 515)
        Me.CRV2.TabIndex = 3
        Me.CRV2.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'CrvBookList
        '
        Me.CrvBookList.ActiveViewIndex = -1
        Me.CrvBookList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvBookList.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrvBookList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrvBookList.Location = New System.Drawing.Point(0, 0)
        Me.CrvBookList.Name = "CrvBookList"
        Me.CrvBookList.Size = New System.Drawing.Size(1246, 515)
        Me.CrvBookList.TabIndex = 4
        '
        'ReportBorAndRet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 515)
        Me.Controls.Add(Me.CrvBookList)
        Me.Controls.Add(Me.CRV2)
        Me.Name = "ReportBorAndRet"
        Me.Text = "BorrowingAndReturningReports"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CRV2 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents CrvBookList As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
