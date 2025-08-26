<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ScriptViewerForm
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
        Me.txtScript = New System.Windows.Forms.TextBox()
        Me.panelBottom = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSaveToFile = New System.Windows.Forms.Button()
        Me.btnCopyToClipboard = New System.Windows.Forms.Button()
        Me.panelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtScript
        '
        Me.txtScript.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtScript.Font = New System.Drawing.Font("Consolas", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtScript.Location = New System.Drawing.Point(0, 0)
        Me.txtScript.Multiline = True
        Me.txtScript.Name = "txtScript"
        Me.txtScript.ReadOnly = True
        Me.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtScript.Size = New System.Drawing.Size(800, 550)
        Me.txtScript.TabIndex = 0
        Me.txtScript.WordWrap = False
        '
        'panelBottom
        '
        Me.panelBottom.Controls.Add(Me.lblStatus)
        Me.panelBottom.Controls.Add(Me.btnClose)
        Me.panelBottom.Controls.Add(Me.btnSaveToFile)
        Me.panelBottom.Controls.Add(Me.btnCopyToClipboard)
        Me.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottom.Location = New System.Drawing.Point(0, 550)
        Me.panelBottom.Name = "panelBottom"
        Me.panelBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.panelBottom.Size = New System.Drawing.Size(800, 50)
        Me.panelBottom.TabIndex = 1
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(300, 19)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 15)
        Me.lblStatus.TabIndex = 3
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(720, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 29)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSaveToFile
        '
        Me.btnSaveToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveToFile.Location = New System.Drawing.Point(135, 11)
        Me.btnSaveToFile.Name = "btnSaveToFile"
        Me.btnSaveToFile.Size = New System.Drawing.Size(90, 29)
        Me.btnSaveToFile.TabIndex = 1
        Me.btnSaveToFile.Text = "Save to File"
        Me.btnSaveToFile.UseVisualStyleBackColor = True
        '
        'btnCopyToClipboard
        '
        Me.btnCopyToClipboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyToClipboard.ForeColor = System.Drawing.Color.White
        Me.btnCopyToClipboard.Location = New System.Drawing.Point(11, 11)
        Me.btnCopyToClipboard.Name = "btnCopyToClipboard"
        Me.btnCopyToClipboard.Size = New System.Drawing.Size(118, 29)
        Me.btnCopyToClipboard.TabIndex = 0
        Me.btnCopyToClipboard.Text = "Copy to Clipboard"
        Me.btnCopyToClipboard.UseVisualStyleBackColor = False
        '
        'ScriptViewerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.txtScript)
        Me.Controls.Add(Me.panelBottom)
        Me.MinimumSize = New System.Drawing.Size(600, 400)
        Me.Name = "ScriptViewerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Script Viewer"
        Me.panelBottom.ResumeLayout(False)
        Me.panelBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtScript As TextBox
    Friend WithEvents panelBottom As Panel
    Friend WithEvents btnCopyToClipboard As Button
    Friend WithEvents btnSaveToFile As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lblStatus As Label
End Class
