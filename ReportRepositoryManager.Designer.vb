﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportRepositoryManager
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
        Me.CrvRRM = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CrvRRM
        '
        Me.CrvRRM.ActiveViewIndex = -1
        Me.CrvRRM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvRRM.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrvRRM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrvRRM.Location = New System.Drawing.Point(0, 0)
        Me.CrvRRM.Name = "CrvRRM"
        Me.CrvRRM.Size = New System.Drawing.Size(1229, 520)
        Me.CrvRRM.TabIndex = 0
        Me.CrvRRM.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'ReportRepositoryManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1229, 520)
        Me.Controls.Add(Me.CrvRRM)
        Me.Name = "ReportRepositoryManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Research Repository Manager Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CrvRRM As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
