<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IndexManagementForm
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
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.groupBoxQuery = New System.Windows.Forms.GroupBox()
        Me.btnAnalyzeQuery = New System.Windows.Forms.Button()
        Me.txtQuery = New System.Windows.Forms.TextBox()
        Me.labelQuery = New System.Windows.Forms.Label()
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.groupBoxSuggestions = New System.Windows.Forms.GroupBox()
        Me.dataGridViewIndexes = New System.Windows.Forms.DataGridView()
        Me.panelIndexControls = New System.Windows.Forms.Panel()
        Me.btnSelectHighPriority = New System.Windows.Forms.Button()
        Me.btnSelectNone = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.panelBottom = New System.Windows.Forms.Panel()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnExecuteSelected = New System.Windows.Forms.Button()
        Me.btnGenerateScript = New System.Windows.Forms.Button()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.groupBoxQuery.SuspendLayout()
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.groupBoxSuggestions.SuspendLayout()
        CType(Me.dataGridViewIndexes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelIndexControls.SuspendLayout()
        Me.panelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer1.Name = "splitContainer1"
        Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.groupBoxQuery)
        Me.splitContainer1.Panel1MinSize = 150
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
        Me.splitContainer1.Panel2.Controls.Add(Me.panelBottom)
        Me.splitContainer1.Size = New System.Drawing.Size(1200, 700)
        Me.splitContainer1.SplitterDistance = 200
        Me.splitContainer1.TabIndex = 0
        '
        'groupBoxQuery
        '
        Me.groupBoxQuery.Controls.Add(Me.btnAnalyzeQuery)
        Me.groupBoxQuery.Controls.Add(Me.txtQuery)
        Me.groupBoxQuery.Controls.Add(Me.labelQuery)
        Me.groupBoxQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxQuery.Location = New System.Drawing.Point(0, 0)
        Me.groupBoxQuery.Name = "groupBoxQuery"
        Me.groupBoxQuery.Padding = New System.Windows.Forms.Padding(8)
        Me.groupBoxQuery.Size = New System.Drawing.Size(1200, 200)
        Me.groupBoxQuery.TabIndex = 0
        Me.groupBoxQuery.TabStop = False
        Me.groupBoxQuery.Text = "Query Analysis - Enter SQL query to get index suggestions"
        '
        'btnAnalyzeQuery
        '
        Me.btnAnalyzeQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnalyzeQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnAnalyzeQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnalyzeQuery.ForeColor = System.Drawing.Color.White
        Me.btnAnalyzeQuery.Location = New System.Drawing.Point(1080, 160)
        Me.btnAnalyzeQuery.Name = "btnAnalyzeQuery"
        Me.btnAnalyzeQuery.Size = New System.Drawing.Size(105, 32)
        Me.btnAnalyzeQuery.TabIndex = 2
        Me.btnAnalyzeQuery.Text = "Analyze Query"
        Me.btnAnalyzeQuery.UseVisualStyleBackColor = False
        '
        'txtQuery
        '
        Me.txtQuery.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuery.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtQuery.Location = New System.Drawing.Point(11, 45)
        Me.txtQuery.Multiline = True
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.PlaceholderText = "Enter your SQL query here (SELECT, UPDATE, DELETE, etc.)"
        Me.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtQuery.Size = New System.Drawing.Size(1174, 109)
        Me.txtQuery.TabIndex = 1
        Me.txtQuery.WordWrap = False
        '
        'labelQuery
        '
        Me.labelQuery.AutoSize = True
        Me.labelQuery.Location = New System.Drawing.Point(11, 24)
        Me.labelQuery.Name = "labelQuery"
        Me.labelQuery.Size = New System.Drawing.Size(348, 15)
        Me.labelQuery.TabIndex = 0
        Me.labelQuery.Text = "SQL Query (we'll analyze WHERE, JOIN, ORDER BY, GROUP BY clauses):"
        '
        'splitContainer2
        '
        Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer2.Name = "splitContainer2"
        Me.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.groupBoxSuggestions)
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.panelIndexControls)
        Me.splitContainer2.Panel2MinSize = 50
        Me.splitContainer2.Size = New System.Drawing.Size(1200, 446)
        Me.splitContainer2.SplitterDistance = 392
        Me.splitContainer2.TabIndex = 1
        '
        'groupBoxSuggestions
        '
        Me.groupBoxSuggestions.Controls.Add(Me.dataGridViewIndexes)
        Me.groupBoxSuggestions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBoxSuggestions.Location = New System.Drawing.Point(0, 0)
        Me.groupBoxSuggestions.Name = "groupBoxSuggestions"
        Me.groupBoxSuggestions.Padding = New System.Windows.Forms.Padding(8)
        Me.groupBoxSuggestions.Size = New System.Drawing.Size(1200, 392)
        Me.groupBoxSuggestions.TabIndex = 0
        Me.groupBoxSuggestions.TabStop = False
        Me.groupBoxSuggestions.Text = "Index Suggestions (Double-click to view script)"
        '
        'dataGridViewIndexes
        '
        Me.dataGridViewIndexes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dataGridViewIndexes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewIndexes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGridViewIndexes.Location = New System.Drawing.Point(8, 24)
        Me.dataGridViewIndexes.Name = "dataGridViewIndexes"
        Me.dataGridViewIndexes.RowHeadersWidth = 51
        Me.dataGridViewIndexes.Size = New System.Drawing.Size(1184, 360)
        Me.dataGridViewIndexes.TabIndex = 0
        '
        'panelIndexControls
        '
        Me.panelIndexControls.Controls.Add(Me.btnSelectHighPriority)
        Me.panelIndexControls.Controls.Add(Me.btnSelectNone)
        Me.panelIndexControls.Controls.Add(Me.btnSelectAll)
        Me.panelIndexControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelIndexControls.Location = New System.Drawing.Point(0, 0)
        Me.panelIndexControls.Name = "panelIndexControls"
        Me.panelIndexControls.Padding = New System.Windows.Forms.Padding(8)
        Me.panelIndexControls.Size = New System.Drawing.Size(1200, 50)
        Me.panelIndexControls.TabIndex = 0
        '
        'btnSelectHighPriority
        '
        Me.btnSelectHighPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectHighPriority.Location = New System.Drawing.Point(164, 11)
        Me.btnSelectHighPriority.Name = "btnSelectHighPriority"
        Me.btnSelectHighPriority.Size = New System.Drawing.Size(100, 29)
        Me.btnSelectHighPriority.TabIndex = 2
        Me.btnSelectHighPriority.Text = "Select High Priority"
        Me.btnSelectHighPriority.UseVisualStyleBackColor = True
        '
        'btnSelectNone
        '
        Me.btnSelectNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectNone.Location = New System.Drawing.Point(86, 11)
        Me.btnSelectNone.Name = "btnSelectNone"
        Me.btnSelectNone.Size = New System.Drawing.Size(72, 29)
        Me.btnSelectNone.TabIndex = 1
        Me.btnSelectNone.Text = "Select None"
        Me.btnSelectNone.UseVisualStyleBackColor = True
        '
        'btnSelectAll
        '
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Location = New System.Drawing.Point(11, 11)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(69, 29)
        Me.btnSelectAll.TabIndex = 0
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'panelBottom
        '
        Me.panelBottom.Controls.Add(Me.progressBar)
        Me.panelBottom.Controls.Add(Me.lblStatus)
        Me.panelBottom.Controls.Add(Me.btnClose)
        Me.panelBottom.Controls.Add(Me.btnExecuteSelected)
        Me.panelBottom.Controls.Add(Me.btnGenerateScript)
        Me.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelBottom.Location = New System.Drawing.Point(0, 446)
        Me.panelBottom.Name = "panelBottom"
        Me.panelBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.panelBottom.Size = New System.Drawing.Size(1200, 50)
        Me.panelBottom.TabIndex = 0
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(370, 15)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(200, 23)
        Me.progressBar.TabIndex = 4
        Me.progressBar.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(580, 19)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(39, 15)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Ready"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(1120, 11)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 29)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnExecuteSelected
        '
        Me.btnExecuteSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExecuteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecuteSelected.ForeColor = System.Drawing.Color.White
        Me.btnExecuteSelected.Location = New System.Drawing.Point(130, 11)
        Me.btnExecuteSelected.Name = "btnExecuteSelected"
        Me.btnExecuteSelected.Size = New System.Drawing.Size(115, 29)
        Me.btnExecuteSelected.TabIndex = 1
        Me.btnExecuteSelected.Text = "Execute Selected"
        Me.btnExecuteSelected.UseVisualStyleBackColor = False
        '
        'btnGenerateScript
        '
        Me.btnGenerateScript.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnGenerateScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateScript.ForeColor = System.Drawing.Color.White
        Me.btnGenerateScript.Location = New System.Drawing.Point(11, 11)
        Me.btnGenerateScript.Name = "btnGenerateScript"
        Me.btnGenerateScript.Size = New System.Drawing.Size(113, 29)
        Me.btnGenerateScript.TabIndex = 0
        Me.btnGenerateScript.Text = "Generate Script"
        Me.btnGenerateScript.UseVisualStyleBackColor = False
        '
        'IndexManagementForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.splitContainer1)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "IndexManagementForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Index Management - Analyze and Create Indexes"
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.groupBoxQuery.ResumeLayout(False)
        Me.groupBoxQuery.PerformLayout()
        CType(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel2.ResumeLayout(False)
        Me.splitContainer2.ResumeLayout(False)
        Me.groupBoxSuggestions.ResumeLayout(False)
        CType(Me.dataGridViewIndexes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelIndexControls.ResumeLayout(False)
        Me.panelBottom.ResumeLayout(False)
        Me.panelBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents splitContainer1 As SplitContainer
    Friend WithEvents groupBoxQuery As GroupBox
    Friend WithEvents txtQuery As TextBox
    Friend WithEvents labelQuery As Label
    Friend WithEvents btnAnalyzeQuery As Button
    Friend WithEvents splitContainer2 As SplitContainer
    Friend WithEvents groupBoxSuggestions As GroupBox
    Friend WithEvents dataGridViewIndexes As DataGridView
    Friend WithEvents panelIndexControls As Panel
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnSelectNone As Button
    Friend WithEvents btnSelectHighPriority As Button
    Friend WithEvents panelBottom As Panel
    Friend WithEvents btnGenerateScript As Button
    Friend WithEvents btnExecuteSelected As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents progressBar As ProgressBar
End Class
